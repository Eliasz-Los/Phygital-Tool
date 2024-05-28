using Microsoft.AspNetCore.Mvc;
using Phygital.BL;
using Phygital.BL.Managers;
using Phygital.Domain.User;
using Phygital.UI_MVC.Models.Dto;

namespace Phygital.UI_MVC.Controllers.Api;

[ApiController]
[Route("/api/[controller]")]
public class OrganisationsController : ControllerBase
{
    private readonly IUserManager _organisationManager;
    private readonly UnitOfWork _unitOfWork;

    public OrganisationsController(IUserManager organisationManager, UnitOfWork unitOfWork)
    {
        _organisationManager = organisationManager;
        _unitOfWork = unitOfWork;
    }
    
    [HttpGet("organisations")]
    public ActionResult<IEnumerable<OrganisatieDto>> GetOrganisations()
    {
        var organisations = _organisationManager.GetAllOrganisations();

        if (!organisations.Any())
        {
            return NoContent();
        }

        return Ok(organisations.Select(organisation => new OrganisatieDto()
        {
            Id = organisation.Id,   
            Name = organisation.Name,
            Description = organisation.Description
        }));
    }
    
    [HttpPost("AddOrganisation")]
    public ActionResult CreateOrganisation([FromBody] OrganisatieDto newOrganisation)
    {
        if (string.IsNullOrEmpty(newOrganisation.Name) || string.IsNullOrEmpty(newOrganisation.Description))
        {
            return BadRequest("Naam en beschrijving zijn verplicht.");
        }

        var organisation = new Organisation()
        {
            Name = newOrganisation.Name,
            Description = newOrganisation.Description
        };
        _unitOfWork.BeginTransaction();
        _organisationManager.AddOrganisation(organisation);
        _unitOfWork.Commit();
        return Ok(organisation);
    }
}