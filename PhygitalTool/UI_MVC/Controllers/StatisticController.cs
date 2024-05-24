using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Phygital.BL;

namespace Phygital.UI_MVC.Controllers;

public class StatisticController : Controller
{
    private readonly ILogger<StatisticController> _logger;
    private readonly IStatisticsManager _statisticsManager;
    private readonly ISessionManager _sessionManager;

    public StatisticController(ILogger<StatisticController> logger, IStatisticsManager statisticsManager, ISessionManager sessionManager)
    {
        _logger = logger;
        _statisticsManager = statisticsManager;
        _sessionManager = sessionManager;
    }

    [HttpGet]
    [Authorize(Roles = "Admin, SubAdmin")]
    public IActionResult Index(long id)
    {
        var stats = _statisticsManager.GetFlowStatistics(id);
        var participations = _sessionManager.GetParticipationsByFlowId(id).OrderBy(p => p.Id);
        
        ViewBag.FlowId = id;
        ViewBag.Participations = participations;
        ViewBag.TotalParticipations = _sessionManager.GetTotalParticipationsByFlowId(id);
        ViewBag.AverageTimeSpent = _sessionManager.GetAverageTimeSpentByFlowId(id);
        
        return View(stats);
    }
    
    [HttpGet("api/GetParticipationCountsByTimeSpentCategories")]
    public IActionResult GetParticipationCountsByTimeSpentCategories(long flowId)
    {
        var data = _statisticsManager.GetParticipationCountsByTimeSpentCategories(flowId);
        return Ok(data);
    }
}