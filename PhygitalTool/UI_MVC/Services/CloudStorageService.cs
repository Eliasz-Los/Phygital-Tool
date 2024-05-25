using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
namespace Phygital.UI_MVC.Services;

public class CloudStorageService
{
    
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
    
    private static string FormFileName(string title, string fileName)
    {
        var fileExtension = Path.GetExtension(fileName);
        var fileNameForStorage = $"{title}-{DateTime.Now.ToString("yyyyMMddHHmmss")}{fileExtension}";
        return fileNameForStorage;
    }

}