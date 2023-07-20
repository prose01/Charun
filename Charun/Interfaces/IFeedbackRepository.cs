using Charun.Model;
using MongoDB.Driver;

namespace Charun.Interfaces
{
    public interface IFeedbackRepository
    {
        Task<DeleteResult> DeleteOldFeedbacks();

        Task<IEnumerable<Feedback>> ViewDeleteOldFeedbacks();
    }
}
