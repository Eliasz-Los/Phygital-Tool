using Microsoft.AspNetCore.Mvc;
using Phygital.BL;
using Phygital.UI_MVC.Models.Dto.Feedback;
using Phygital.UI_MVC.Models.Dto.Infos;

namespace Phygital.UI_MVC.Controllers;

public class FlowElementsController : Controller
{
    private readonly ILogger<FlowController> _logger;
    private readonly UnitOfWork _uow;

    [HttpGet]
    public IActionResult AddImage()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult AddImage(ImageDto image)
    {
        // todo process image
        return View(image);
    }
    
}