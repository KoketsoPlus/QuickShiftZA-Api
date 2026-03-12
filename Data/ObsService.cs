using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon;

namespace QuickShiftZA.Api.Services;

public class ObsService
{
    private readonly IConfiguration _config;

    public ObsService(IConfiguration config)
    {
        _config = config;
    }

    public async Task<string> UploadImageAsync(IFormFile file, string folder)
    {
        var accessKey = _config["OBS:AccessKey"];
        var secretKey = _config["OBS:SecretKey"];
        var bucketName = _config["OBS:BucketName"];
        var serviceUrl = _config["OBS:ServiceURL"];

        var s3Config = new AmazonS3Config
        {
            ServiceURL = serviceUrl,
            ForcePathStyle = true
        };

        using var client = new AmazonS3Client(accessKey, secretKey, s3Config);
        using var stream = file.OpenReadStream();

        var fileName = $"{folder}/{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

        var uploadRequest = new TransferUtilityUploadRequest
        {
            BucketName = bucketName,
            Key = fileName,
            InputStream = stream,
            ContentType = file.ContentType,
            CannedACL = S3CannedACL.PublicRead
        };

        var transferUtility = new TransferUtility(client);
        await transferUtility.UploadAsync(uploadRequest);

        return $"{serviceUrl}/{bucketName}/{fileName}";
    }
}
