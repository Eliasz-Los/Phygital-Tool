using Microsoft.AspNetCore.Mvc;
using Phygital.UI_MVC.Services;

namespace Phygital.UI_MVC.Controllers.Api;

[ApiController]
[Route("/api/files")]
public class FilesController : ControllerBase
{
    private readonly CloudStorageService cloudStorageService;

    public FilesController(CloudStorageService cloudStorageService)
    {
        this.cloudStorageService = cloudStorageService;
    }

    [HttpPost]
    public IActionResult UploadFile(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        file.CopyTo(memoryStream);
        var url = cloudStorageService.UploadFileToBucket(memoryStream);
        return Ok(new { url });
    }
}