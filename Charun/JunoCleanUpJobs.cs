using Charun.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Charun
{
    public class JunoCleanUpJobs
    {
        private readonly ILogger _logger;
        private readonly IJunoRepository _junoRepository;
        private readonly IHelperMethods _helper;

        public JunoCleanUpJobs(ILogger<AvalonCleanUpJobs> logger, IJunoRepository junoRepository, IHelperMethods helper)
        {
            _logger = logger;
            _junoRepository = junoRepository;
            _helper = helper;
        }

        [Function("DeleteOldMessages")]
        public async Task DeleteOldMessages([TimerTrigger("0 */2 * * * *")] MyInfo myTimer)
        {
            try
            {
                //await _junoRepository.DeleteOldMessages();
            }
            catch (Exception e)
            {
                _logger.LogInformation($"We had an error : {e}");
            }
        }

        //[Function("DeleteEmptyGroups")]
        //public async Task DeleteEmptyGroups([TimerTrigger("0 */2 * * * *")] MyInfo myTimer) // TODO: Last one closes the group
        //{
        //    try
        //    {
        //        //await _junoRepository.DeleteEmptyGroups();
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogInformation($"We had an error : {e}");
        //    }
        //}

        [Function("DeleteNoActivityGroups")]
        public async Task DeleteNoActivityGroups([TimerTrigger("0 */2 * * * *")] MyInfo myTimer)
        {
            try
            {
                //await _junoRepository.DeleteNoActivityGroups();
            }
            catch (Exception e)
            {
                _logger.LogInformation($"We had an error : {e}");
            }
        }
    }
}
