using Microsoft.AspNetCore.Mvc;
using Phygital.UI_MVC.Services;

namespace Phygital.UI_MVC.Controllers.Api;

[ApiController]
[Route("/api/[controller]")]
public class FilesController : ControllerBase
{
    private readonly CloudStorageService _cloudStorageService;

    public FilesController(CloudStorageService cloudStorageService)
    {
        _cloudStorageService = cloudStorageService;
    }

    [HttpPost("UploadFile")]
    public IActionResult UploadFile(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        file.CopyTo(memoryStream);
        var url = _cloudStorageService.UploadFileToBucket(memoryStream, file.FileName);
        return Ok(new { url });
    }
}