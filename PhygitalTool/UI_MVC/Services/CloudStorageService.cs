using Google.Cloud.Storage.V1;
namespace Phygital.UI_MVC.Services;

public class CloudStorageService
{
    
    private readonly string projectId;
    private readonly string bucketName;

    /// <summary>
    /// Check the documentation:
    ///   https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-8.0
    /// </summary>
    public CloudStorageService(IConfiguration configuration)
    {
        projectId = configuration["GCloud:ProjectId"];
        bucketName = configuration["GCloud:Storage:BucketName"];
    }

    /// <summary>
    /// Check the sample code from Google:
    ///   https://cloud.google.com/dotnet/docs/reference/Google.Cloud.Storage.V1/latest#sample-code
    /// </summary>
    public string UploadFileToBucket(MemoryStream memoryStream)
    {
        var objectName = "a_very_hardcoded_object_name";
        var client = StorageClient.Create();
        var storageObject = client.UploadObject(bucketName, objectName,
            "image/jpeg", // A very hardcoded MIME type ...
            memoryStream);
        // TODO: IMPORTANT: store the url (MediaLink) somewhere ...
        return storageObject.MediaLink;
    }

}