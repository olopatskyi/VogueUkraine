namespace VogueUkraine.Management.Api.Extensions;

public static class FormFileExtensions
{
    public static async Task<List<byte[]>> ConvertToByteArrayCollectionAsync(this IEnumerable<IFormFile> files)
    {
        var byteArrayCollection = new List<byte[]>();

        foreach (var file in files)
        {
            if (file == null || file.Length == 0)
                continue;

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            byteArrayCollection.Add(memoryStream.ToArray());
        }

        return byteArrayCollection;
    }
}