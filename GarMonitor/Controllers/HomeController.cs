using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GarMonitor.Models;
using GarPullerClient;
using GarMonitor.Models.ViewModels;
using FlowControl;
using GarServices;

namespace GarMonitor.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task<ViewResult> Index([FromServices] PullerClient client)
    {
        IEnumerable<GarFile>? list = Enumerable.Empty<GarFile>();
        try {
            list = await client.GarFilesList();
        } catch (Exception ex) {
            _logger.LogError($"Index {ex.Message}");
        }

        return View(new GarFileListViewModel{ GarFiles = list ?? Enumerable.Empty<GarFile>()});
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public ViewResult Go()
    {
        return View();
    }
    [HttpPost]
    public async Task<ViewResult> Go([FromServices] PullerClient client)
    {
        if (ModelState.IsValid) {
            ServiceState state = ServiceState.Failed;
            try {
                state = await client.Go();
            } catch (Exception ex) {
                _logger.LogError($"GarPuller Go {ex.Message}");
            }
            return View("GoIsPressed", state.ToString());
        } else {
            return View();
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
