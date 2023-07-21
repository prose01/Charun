using Charun.Model;
using MongoDB.Driver;

namespace Charun.Interfaces
{
    public interface IJunoRepository
    {
        Task<DeleteResult> DeleteOldMessages();
        Task DeleteNoActivityGroups();

        Task<IEnumerable<MessageModel>> ViewDeleteOldMessages();
        Task<IEnumerable<GroupModel>> ViewDeleteNoActivityGroups();
    }
}
