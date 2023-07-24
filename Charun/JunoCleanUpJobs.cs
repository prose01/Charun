using Charun.Interfaces;
using Charun.Model;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Charun
{
    public class JunoCleanUpJobs
    {
        private readonly ILogger _logger;
        private readonly IJunoRepository _junoRepository;

        public JunoCleanUpJobs(ILogger<AvalonCleanUpJobs> logger, IJunoRepository junoRepository)
        {
            _logger = logger;
            _junoRepository = junoRepository;
            var tt = "%Values:DeleteProfileDaysBack%";
        }

        [Function("DeleteOldMessages")]
        public async Task DeleteOldMessages([TimerTrigger("%DeleteOldMessagesTimerTrigger%")] MyInfo myTimer)
        {
            try
            {
                var result = await _junoRepository.ViewDeleteOldMessages();
                //_logger.LogInformation($"DeleteOldMessages : {result.DeletedCount}");

                // TODO: Delete the following foreach, uncomment the previous _logger and change method call from View... to Delete... Remember to update the TimerTrigger etc. in settings before you start!
                foreach (var item in result)
                {
                    _logger.LogInformation($"DeleteOldMessages - deleting this Message : {item._id}");
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation($"We had an error : {e}");
            }
        }

        [Function("DeleteNoActivityGroups")]
        public async Task DeleteNoActivityGroups([TimerTrigger("%DeleteNoActivityGroupsTimerTrigger%")] MyInfo myTimer)
        {
            try
            {
                var result = await _junoRepository.ViewDeleteNoActivityGroups();
                //_logger.LogInformation($"DeleteNoActivityGroups : {result.DeletedCount}");

                // TODO: Delete the following foreach, uncomment the previous _logger and change method call from View... to Delete... Remember to update the TimerTrigger etc. in settings before you start!
                foreach (var item in result)
                {
                    _logger.LogInformation($"DeleteNoActivityGroups - deleting this Group : {item.GroupId}");
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation($"We had an error : {e}");
            }
        }
    }
}
