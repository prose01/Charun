using Charun.Interfaces;

namespace Charun.Data
{
    public class ImageUtil : IImageUtil
    {
        private readonly IAzureBlobStorage _azureBlobStorage;

        public ImageUtil(IAzureBlobStorage azureBlobStorage)
        {
            _azureBlobStorage = azureBlobStorage;
        }

        /// <summary>Deletes all images for profile. There is no going back!</summary>
        /// <param name="currentUser">The CurrentUser.</param>
        /// <param name="profileId">The profile identifier.</param>
        /// <exception cref="Exception">You don't have admin rights to delete other people's images.</exception>
        /// <exception cref="ArgumentException">ProfileId is missing. {profileId}</exception>
        public async Task DeleteAllImagesForProfile(string profileId)
        {
            try
            {
                await _azureBlobStorage.DeleteAllImagesAsync(profileId);
            }
            catch
            {
                throw;
            }
        }
    }
}
