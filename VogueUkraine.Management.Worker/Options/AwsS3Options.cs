namespace VogueUkraine.Management.Worker.Options;

public class AwsS3Options
{
    public string AccessKey { get; set; }

    public string SecretKey { get; set; }

    public string BucketName { get; set; }

    public string Region { get; set; }
}