using Azure.Storage.Blobs;

namespace HW1.Services
{
    public class BlobService
    {
        private readonly BlobContainerClient _container;

        public BlobService(IConfiguration config)
        {
            var service = new BlobServiceClient(config.GetConnectionString("AzureStorage"));

            _container = service.GetBlobContainerClient("files");

            _container.CreateIfNotExists();
        }

        public async Task Upload(IFormFile file)
        {
            var blob = _container.GetBlobClient(file.FileName);

            using var stream = file.OpenReadStream();

            await blob.UploadAsync(stream, true);
        }
    }
}