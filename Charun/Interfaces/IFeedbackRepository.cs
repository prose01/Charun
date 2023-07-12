using MongoDB.Driver;

namespace Charun.Interfaces
{
    public interface IFeedbackRepository
    {
        Task<DeleteResult> DeleteOldFeedbacks();
    }
}
