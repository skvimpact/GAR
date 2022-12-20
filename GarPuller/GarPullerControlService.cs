using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowControl;
using GarPublicClient;
using GarPuller.Queue;
using GarPuller.ServiceLayer;
using GarServices;
using ServiceLayer;

namespace GarPuller
{
    public class GarPullerControlService : BackgroundService
    {
        private long _previousSize;
        private readonly TimeSpan _refreshInterval = TimeSpan.FromSeconds(5);
        private readonly GarPullerGoQueue _goQueue;
        private readonly GarPullerDownloadedQueue _downloadedQueue;
        private readonly GarPullerProcessQueue _processQueue;
        private readonly IConfiguration _configuration;
        private readonly DirectoryService _directoryService;
        private readonly IServiceProvider _provider;
        private readonly ILogger<GarPullerControlService> _logger;    
        public GarPullerControlService(
            IConfiguration configuration,
            GarPullerGoQueue garPullerGoQueue,
            GarPullerDownloadedQueue garPullerDownloadedQueue,
            GarPullerProcessQueue garPullerProcessQueue,
            DirectoryService directoryService,
            IServiceProvider provider,
            ILogger<GarPullerControlService> logger) {
            _configuration = configuration;   
            _goQueue = garPullerGoQueue;
            _downloadedQueue = garPullerDownloadedQueue;
            _processQueue = garPullerProcessQueue;
            _provider = provider;
            _logger = logger;
            _directoryService = directoryService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {


            var proxy = _provider.GetRequiredService<PublicClient>();
            GarFile? fileToHandle = default(GarFile);
            GarFile? fileToDownload = default(GarFile);
            GarFile? fileToProcess = default(GarFile);
            while (!stoppingToken.IsCancellationRequested) {  
                if (_goQueue.Go()) {
                    using var scope = _provider.CreateScope();
                    var fileService = scope.ServiceProvider.GetRequiredService<GarFileService>();
                    var processor   = scope.ServiceProvider.GetRequiredService<DownloadService>();

                    if (_goQueue.CheckUpdate())
                        try {
                            fileService.UpdateList();
                        } catch (Exception ex) {
                            _logger.LogError($"UpdateList failed. {ex.Message}");
                        }

                    if ((fileToDownload = fileService.GarFileToDownload) is not null) {
                        try {
                            await proxy.DownloadFiasFile(fileToDownload.DeltaUrl, fileToDownload.CorrelationId);
                            try {
                                await fileService.UpdateWhenDownloadRequested(fileToDownload.CorrelationId);
                            } catch(Exception ex) {
                                _logger.LogError($"UpdateWhenDownloadRequested failed. {ex.Message}");
                            }
                        } catch(Exception ex) {
                            _logger.LogError($"DownloadFiasFile failed. {ex.Message}");
                        }
                    }

                    var downloaded = _downloadedQueue.Current(true);
                    if (downloaded.Item2 != Guid.Empty) {
                        try {
                            await fileService.UpdateWhenDownloaded(downloaded.Item2, downloaded.Item1);
                            await _processQueue.Enqueue(downloaded);
                        } catch (Exception ex) {
                            _logger.LogError($"UpdateWhenDownloaded failed. {ex.Message}");
                        }
                    }

                    var toProcess = _processQueue.Current(true);
                    if (toProcess.Item2 != Guid.Empty) {
                        try {
                            await fileService.UpdateWhenProcessRequested(toProcess.Item2);
                            try {
                                processor.HandleZipFile(toProcess.Item1);
                                try {
                                    await fileService.UpdateWhenProcessed(toProcess.Item2);
                                } catch (Exception ex) {
                                    _logger.LogError($"UpdateWhenProcessed failed. {ex.Message}");
                                }
                            } catch (Exception ex) {
                                _logger.LogError($"HandleZipFile failed. {ex.Message}");
                            }
                        } catch(Exception ex) {
                            _logger.LogError($"UpdateWhenProcessRequested failed. {ex.Message}");
                        }
                    }

                    if ((fileToHandle = fileService.GarFileToHandle) is null)
                        _goQueue.Dismiss();

                    if (await IsHangedDownloadingState()) {
                       _logger.LogInformation($"Potential HangedDownloadState detected {await _directoryService.Size()}");
                        try {
                            if (await fileService.ResetHangedDownloadState() is not null)
                                _logger.LogInformation($"Yeah! HangedDownloadState's realy happened");
                        } catch (Exception ex) {
                            _logger.LogError($"ResetHangedDownloadState failed. {ex.Message}");
                        }
                    }        

                    try {
                        if ((fileToProcess = await fileService.ResetHangedProcessState()) is not null) {
                            _logger.LogInformation($"Yeah! HangedProcessState's realy happened");
                            await _processQueue.Enqueue((fileToProcess.LocalPath, fileToProcess.CorrelationId));
                        }
                    } catch (Exception ex) {                        
                        _logger.LogError($"ResetHangedProcessState failed. {ex.Message}");
                    }

                }                
                await Task.Delay(_refreshInterval, stoppingToken);
            }                
        }
        public async Task<bool> IsHangedDownloadingState() {
            var currentSize = await _directoryService.Size();

            if (_previousSize == currentSize)
                return true;
            else {
                _previousSize = currentSize;
                return false;
            }
        }  
        public async Task<bool> IsHangedProcessingState() {
            var currentSize = await _directoryService.Size();

            if (_previousSize == currentSize)
                return true;
            else {
                _previousSize = currentSize;
                return false;
            }
        }                
    }
}