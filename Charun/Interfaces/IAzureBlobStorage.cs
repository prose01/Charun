namespace Charun.Interfaces
{
    public interface IAzureBlobStorage
    {
        Task DeleteAllImagesAsync(string profileId);
    }
}
