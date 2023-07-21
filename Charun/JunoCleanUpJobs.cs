using Charun.Interfaces;
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
                var items = await _junoRepository.ViewDeleteOldMessages();

                foreach (var item in items)
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
                var items = await _junoRepository.ViewDeleteNoActivityGroups();

                foreach (var item in items)
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
