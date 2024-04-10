using BL;
using Microsoft.AspNetCore.Mvc;
using Phygital.Domain.Themas;
using Phygital.UI_MVC.Models.Dto;

namespace Phygital.UI_MVC.Controllers.Api;

[ApiController]
[Route("/api/[controller]")]
public class ThemasController : ControllerBase
{
    private readonly IFlowManager _flowManager;

    public ThemasController(IFlowManager flowManager)
    {
        _flowManager = flowManager;
    }
    [HttpPost("{flowId}/AddSubThemas")]
    public ActionResult CreateSubThema([FromBody] SubThemasDto newSubThema)
    {
        if (string.IsNullOrEmpty(newSubThema.Title) || string.IsNullOrEmpty(newSubThema.Description))
        {
            return BadRequest("Titel en beschrijving zijn verplicht.");
        }
        var subThema = new Theme()
        {
            Title = newSubThema.Title,
            Description = newSubThema.Description
        };
        _flowManager.AddSubThema(subThema);
        return Ok();
    }

}