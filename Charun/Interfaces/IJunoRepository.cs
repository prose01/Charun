using Charun.Model;
using MongoDB.Driver;

namespace Charun.Interfaces
{
    public interface IJunoRepository
    {
        Task<DeleteResult> DeleteOldMessages();
        Task<DeleteResult> DeleteEmptyGroups();
        Task DeleteNoActivityGroups();

        Task<IEnumerable<MessageModel>> ViewDeleteOldMessages();
        Task<IEnumerable<GroupModel>> ViewDeleteEmptyGroups();
        Task<IEnumerable<GroupModel>> ViewDeleteNoActivityGroups();
    }
}
