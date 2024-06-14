namespace VogueUkraine.Management.Worker.Services.Abstractions;

public interface IS3Service
{
    Task<List<string>> AddFilesAsync(IEnumerable<byte[]> images, CancellationToken cancellationToken = default);

    Task DeleteFilesAsync(IEnumerable<string> files, CancellationToken cancellationToken = default);
}