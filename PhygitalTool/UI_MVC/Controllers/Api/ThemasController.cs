using BL;
using Microsoft.AspNetCore.Mvc;
using Phygital.BL;
using Phygital.DAL.EF;
using Phygital.Domain.Themas;
using Phygital.UI_MVC.Models.Dto;

namespace Phygital.UI_MVC.Controllers.Api;

[ApiController]
[Route("/api/[controller]")]
public class ThemasController : ControllerBase
{
    private readonly IFlowManager _flowManager;
    private readonly UnitOfWork _unitOfWork;

    public ThemasController(IFlowManager flowManager, UnitOfWork unitOfWork)
    {
        _flowManager = flowManager;
        _unitOfWork = unitOfWork;
    }

    [HttpGet("subthemas")]
    public ActionResult<IEnumerable<SubThemasDto>> GetSubThemas()
    {
        var subthemas = _flowManager.GetAllSubThemas();

        if (!subthemas.Any())
        {
            return NoContent();
        }

        return Ok(subthemas.Select(subthema => new SubThemasDto()
        {
            Title = subthema.Title,
            Description = subthema.Description
        }));
    }

    [HttpPost("AddSubThemas")]
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
        _unitOfWork.BeginTransaction();
        _flowManager.AddSubThema(subThema);
        _unitOfWork.Commit();
        return Ok(subThema);
    }

    [HttpDelete("deleteThemeById/{id}")]
    public IActionResult DeleteThemeById(int id)
    {
        _flowManager.GetThemeById(id);
        return Ok();
    }
}