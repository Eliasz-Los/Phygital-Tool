using Microsoft.AspNetCore.Http;

namespace Phygital.Domain;

public interface ICloudStorage
{
     Task<string> UploadFileToBucket(IFormFile imageFile, string fileNameForStorage);

}