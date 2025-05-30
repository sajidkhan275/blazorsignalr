using Azure.Storage.Blobs;

namespace BlazorApp1.Services
{
    public class BlobStorageService
    {
        private readonly string _connectionString;
        private readonly string _containerName;
        public BlobStorageService(string connectionString, string containerName)
        {
            _connectionString = connectionString;
            _containerName = containerName;
        }
        public async Task<byte[]> GetFileAsync(string blobName)
        {
            var blobClient = new BlobClient(_connectionString, _containerName, blobName);

            if (await blobClient.ExistsAsync())
            {
                var downloadInfo = await blobClient.DownloadAsync();
                using var ms = new MemoryStream();
                await downloadInfo.Value.Content.CopyToAsync(ms);
                return ms.ToArray();
            }

            return null;
        }
    }
}
