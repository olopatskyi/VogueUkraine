using Amazon.S3;
using Amazon.S3.Model;
using VogueUkraine.Profile.Worker.Options;
using VogueUkraine.Profile.Worker.Services.Abstractions;

namespace VogueUkraine.Profile.Worker.Services;

public class S3Service : IS3Service
{
    private readonly IAmazonS3 _s3Client;
    private readonly AwsS3Options _s3Options;

    public S3Service(IAmazonS3 s3Client, AwsS3Options s3Options)
    {
        _s3Client = s3Client;
        _s3Options = s3Options;
    }

    public async Task<List<string>> AddFilesAsync(IEnumerable<byte[]> images,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var request = new PutObjectRequest
            {
                BucketName = _s3Options.BucketName,
                ContentType = "image/jpeg"
            };

            var uploadTasks = images.Select(async image =>
            {
                var fileName = Guid.NewGuid() + ".jpg";

                using var stream = new MemoryStream(image);

                request.Key = fileName;
                request.InputStream = stream;

                await _s3Client.PutObjectAsync(request, cancellationToken);

                return fileName;
            });

            var uploadedUrls = await Task.WhenAll(uploadTasks);

            return uploadedUrls.ToList();
        }
        catch (AmazonS3Exception ex)
        {
            Console.WriteLine($"Error uploading files: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error uploading files: {ex.Message}");
            throw;
        }
    }

    public async Task DeleteFilesAsync(IEnumerable<string> files, CancellationToken cancellationToken = default)
    {
        try
        {
            var tasks = new List<Task>();
            var request = new DeleteObjectRequest
            {
                BucketName = _s3Options.BucketName,
            };

            foreach (var fileName in files)
            {
                request.Key = fileName;
                tasks.Add(_s3Client.DeleteObjectAsync(request, cancellationToken));
            }

            await Task.WhenAll(tasks);
        }
        catch (AmazonS3Exception ex)
        {
            Console.WriteLine($"Error deleting file: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting file: {ex.Message}");
        }
    }
}