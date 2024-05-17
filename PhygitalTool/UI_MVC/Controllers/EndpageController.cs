using Microsoft.AspNetCore.Mvc;
using Phygital.BL;
using Phygital.UI_MVC.Models.Dto.Session;

namespace Phygital.UI_MVC.Controllers;

public class EndpageController : Controller
{
    private readonly ILogger<FlowController> _logger;
    private readonly ISessionManager _sessionManager;
    private readonly UnitOfWork _uow;

    public EndpageController(ILogger<FlowController> logger, ISessionManager sessionManager, UnitOfWork uow)
    {
        _logger = logger;
        _sessionManager = sessionManager;
        _uow = uow;
    }

    public IActionResult Index()
    {
        var participations = _sessionManager.GetAllParticipations().Count();
        var participation = _sessionManager.GetParticipationById(participations);
        
        var participationDto = new ParticipationDto
        {
            Id = participation.Id,
            StartTime = participation.StartTime,
            EndTime = DateTime.Now.ToUniversalTime(),
        };
        _uow.BeginTransaction();
        _sessionManager.ChangeParticipation(participationDto.Id, participationDto.StartTime, participationDto.EndTime);
        _uow.Commit();
        
        return View();
    }
}