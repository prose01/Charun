using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Charun.Interfaces;
using Charun.Model;
using Microsoft.Extensions.Options;

namespace Charun.Data
{
    public class AzureBlobStorage : IAzureBlobStorage
    {
        private readonly BlobContainerClient _container;

        public AzureBlobStorage(IOptions<Settings> settings)
        {
            _container = new BlobContainerClient(settings.Value.Storage_ConnectionString, "photos");
        }

        public async Task DeleteAllImagesAsync(string profileId)
        {
            try
            {
                // Get a reference to a blob
                var blobItems = _container.GetBlobsAsync(prefix: profileId);

                // Delete everything Async
                await foreach (BlobItem blobItem in blobItems)
                {
                    BlobClient blobClient = _container.GetBlobClient(blobItem.Name);
                    await blobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
                }

            }
            catch
            {
                throw;
            }
        }
    }
}
