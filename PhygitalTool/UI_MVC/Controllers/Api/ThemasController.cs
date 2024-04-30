using Phygital.BL;
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
    private readonly IThemeManager _themeManager;
    private readonly UnitOfWork _unitOfWork;

    public ThemasController(IFlowManager flowManager, IThemeManager themeManager, UnitOfWork unitOfWork)
    {
        _flowManager = flowManager;
        _themeManager = themeManager;
        _unitOfWork = unitOfWork;
    }

    [HttpGet("subthemas")]
    public ActionResult<IEnumerable<SubThemasDto>> GetSubThemas()
    {
        var subthemas = _themeManager.GetAllSubThemas();

        if (!subthemas.Any())
        {
            return NoContent();
        }

        return Ok(subthemas.Select(subthema => new SubThemasDto()
        {
            Id = subthema.Id,   
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
        _themeManager.AddSubThema(subThema);
        _unitOfWork.Commit();
        return Ok(subThema);
    }

    /*
    [HttpDelete("deleteSubTheme/{id}")]
    public IActionResult DeleteTheme(int id)
    {
        try
        {
            _themeManager.RemoveTheme(id);
            return Ok(); // Theme deleted successfully, return 200
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while deleting the theme."); // Return 500 for server error
        }
    }
    */
}