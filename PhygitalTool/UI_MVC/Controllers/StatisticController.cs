using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Phygital.BL;
using ILogger = Castle.Core.Logging.ILogger;

namespace Phygital.UI_MVC.Controllers;

public class StatisticController : Controller
{
    private readonly ILogger<StatisticController> _logger;
    private readonly IStatisticsManager _statisticsManager;
    
    public StatisticController(ILogger<StatisticController> logger,IStatisticsManager statisticsManager)
    {
        _logger = logger;
        _statisticsManager = statisticsManager;
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin, SubAdmin")]
    public IActionResult Index(long id)
    {
        var stats = _statisticsManager.GetFlowStatistics(id);
        return View(stats);
    }
}