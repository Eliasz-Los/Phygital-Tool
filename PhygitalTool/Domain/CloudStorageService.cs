using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Phygital.Domain;

public class CloudStorageService : ICloudStorage
{
    
    private readonly GoogleCredential _googleCredential;
    private readonly StorageClient _storageClient;
    private readonly string _bucketName;
    
    public CloudStorageService(IConfiguration configuration)
    {
        _googleCredential = GoogleCredential.FromFile("./bucket-credential-key.json");
        _storageClient = StorageClient.Create(_googleCredential);
        _bucketName = configuration["GoogleCloudStorageBucket"];
        Console.WriteLine(_googleCredential);
        Console.WriteLine(_storageClient);
        Console.WriteLine(_bucketName);
    }
    
    public async Task<string> UploadFileToBucket(IFormFile imageFile, string fileNameForStorage)
    {
        using (var memoryStream = new MemoryStream())
        {
            await imageFile.CopyToAsync(memoryStream);
            var dataObject =
                await _storageClient.UploadObjectAsync(_bucketName, fileNameForStorage, null, memoryStream);
            return dataObject.MediaLink;
        }
    }

}