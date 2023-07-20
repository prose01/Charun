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

        //[Function("DeleteOldMessages")]
        //public async Task DeleteOldMessages([TimerTrigger("0 */2 * * * *")] MyInfo myTimer)
        //{
        //    try
        //    {
        //        var items = await _junoRepository.ViewDeleteOldMessages();

        //        foreach (var item in items)
        //        {
        //            _logger.LogInformation($"DeleteOldMessages - deleting this feedback : {item._id}");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogInformation($"We had an error : {e}");
        //    }
        //}

        //[Function("DeleteEmptyGroups")]
        //public async Task DeleteEmptyGroups([TimerTrigger("0 */2 * * * *")] MyInfo myTimer) // TODO: Last one closes the group
        //{
        //    try
        //    {
        //        var items = await _junoRepository.ViewDeleteEmptyGroups();

        //        foreach (var item in items)
        //        {
        //            _logger.LogInformation($"DeleteEmptyGroups - deleting this feedback : {item.GroupId}");
        //        }
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
                var items = await _junoRepository.ViewDeleteNoActivityGroups();

                foreach (var item in items)
                {
                    _logger.LogInformation($"DeleteNoActivityGroups - deleting this feedback : {item.GroupId}");
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation($"We had an error : {e}");
            }
        }
    }
}
