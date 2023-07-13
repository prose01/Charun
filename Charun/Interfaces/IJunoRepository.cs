using MongoDB.Driver;

namespace Charun.Interfaces
{
    public interface IJunoRepository
    {
        Task<DeleteResult> DeleteOldMessages();
    }
}
