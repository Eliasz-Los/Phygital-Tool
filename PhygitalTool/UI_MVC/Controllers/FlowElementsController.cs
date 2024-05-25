using Microsoft.AspNetCore.Mvc;
using Phygital.BL;
using Phygital.UI_MVC.Models.Dto.Feedback;
using Phygital.UI_MVC.Models.Dto.Infos;

namespace Phygital.UI_MVC.Controllers;

public class FlowElementsController : Controller
{
    private readonly ILogger<FlowController> _logger;
    private readonly UnitOfWork _uow;

    public FlowElementsController(ILogger<FlowController> logger,UnitOfWork uow)
    {
        _logger = logger;
        _uow = uow;
    }


    // Todo route fixen
    [HttpPost]
    public IActionResult AddImage()
    {
        ImageDto image = new ImageDto();
        return View(image);
    }
    
    // public IActionResult AddImage()
    // {
    //     return View();
    // }
}