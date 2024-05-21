using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
namespace Phygital.UI_MVC.Services;

public class CloudStorageService
{
    
    /*
    private readonly string projectId;
            projectId = configuration["GCloud:ProjectId"];
              "image/jpeg", // A very hardcoded MIME type ...
    */
    private readonly GoogleCredential _googleCredential;
    private readonly StorageClient _storageClient;
    private readonly string _bucketName;
    
    public CloudStorageService(IConfiguration configuration)
    {
        _googleCredential = GoogleCredential.FromFile("./secret.json");
        _storageClient = StorageClient.Create(_googleCredential);
        _bucketName = configuration["GCloud:Storage:BucketName"];
    }
    
    public string UploadFileToBucket(MemoryStream memoryStream, string fileNameForStorage)
    {
        var storageObject = _storageClient.UploadObject(_bucketName, fileNameForStorage, null, memoryStream);
        // TODO: IMPORTANT: store the url (MediaLink) somewhere ...
        return storageObject.MediaLink;
    }

}