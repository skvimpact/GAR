using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FlowControl
{
    public interface IFlowDbAccess
    {
    }
    public class FlowDbAccess : IFlowDbAccess
    {
        private readonly FlowContext _context;
        public FlowDbAccess(FlowContext context)
        {
            _context = context;
        }
        public GarFile Get(DateTime date)
        {
            return _context.GarFiles.SingleOrDefault(f => f.Date == date);
        }
        public GarFile Get(Guid correlationId)
        {
            return _context.GarFiles.SingleOrDefault(f => f.CorrelationId == correlationId);
        }
        public void AddRange(IEnumerable<GarFile> newFiles)
        {
            _context.GarFiles.AddRange(newFiles);
            _context.SaveChanges();
        }        
        public IQueryable<GarFile> GarFiles => _context.GarFiles;


        public async Task<GarFile> UpdateWhenDownloadRequested(Guid correlationId)
        {
            var file = Get(correlationId);
            if (file == null)
                throw new ArgumentException(
                    "file not found");
            // Console.WriteLine($"A file.RequestedAt = {correlationId}");       
            file.DownloadRequestedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return file; 
        }

        public async Task <GarFile> UpdateWhenDownloaded(
            Guid correlationId, 
            string filePath)
        {
            var file = Get(correlationId);
            if (file == null)
                throw new ArgumentException(
                    "file not found");
            file.LocalPath = filePath;
            file.DownloadedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return file; 
        }
        public async Task<GarFile> UpdateWhenProcessRequested(Guid correlationId)
        {
            var file = Get(correlationId);
            if (file == null)
                throw new ArgumentException(
                    "file not found");   
            file.ProcessRequestedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return file; 
        }        
        public async Task<GarFile> UpdateWhenProcessed(Guid correlationId)
        {
            var file = Get(correlationId);
            if (file == null)
                throw new ArgumentException(
                    "file not found");
            file.ProcessedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return file; 
        }        
        public async Task ClearControlTable()
        {
            await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE [flow].[GarFiles]");
        }
    } 
        
}