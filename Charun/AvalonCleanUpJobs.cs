using Charun.Data;
using Charun.Interfaces;
using Charun.Model;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Charun
{
    public class AvalonCleanUpJobs
    {
        private readonly ILogger _logger;
        private readonly IOptions<Settings> _settings;
        private readonly IProfilesQueryRepository _profilesQueryRepository;
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IAzureBlobStorage _azureBlobStorage;
        private readonly IHelperMethods _helper;

        public AvalonCleanUpJobs(ILogger<AvalonCleanUpJobs> logger, IOptions<Settings> settings, IProfilesQueryRepository profilesQueryRepository, IFeedbackRepository feedbackRepository, IAzureBlobStorage azureBlobStorage, IHelperMethods helper)
        {
            _logger = logger;
            _settings = settings;
            _profilesQueryRepository = profilesQueryRepository;
            _feedbackRepository = feedbackRepository;
            _azureBlobStorage = azureBlobStorage;
            _helper = helper;
        }

        //[Function("DeleteOldProfiles")]
        //public async Task DeleteOldProfiles([TimerTrigger("0 */2 * * * *")] MyInfo myTimer)
        //{
        //    try
        //    {
        //        var _deleteProfileDaysBack = _settings.Value.DeleteProfileDaysBack;
        //        var _deleteProfileLimit = _settings.Value.DeleteProfileLimit;

        //        var oldProfiles = await _profilesQueryRepository.GetOldProfiles(_deleteProfileDaysBack, _deleteProfileLimit);

        //        foreach (var profile in oldProfiles)
        //        {
        //            _logger.LogInformation($"DeleteOldProfiles - deleting this profile : {profile.Name}");
        //            //await _helper.DeleteProfileFromAuth0(profile);
        //            //await _azureBlobStorage.DeleteAllImagesAsync(profile.ProfileId);
        //            //await _profilesQueryRepository.DeleteProfile(profile.ProfileId);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogInformation($"We had an error : {e}");
        //    }
        //}

        //[Function("DeleteOldFeedbacks")]
        //public async Task DeleteOldFeedbacks([TimerTrigger("0 */2 * * * *")] MyInfo myTimer)
        //{
        //    try
        //    {
        //        var items = await _feedbackRepository.ViewDeleteOldFeedbacks();

        //        foreach (var item in items)
        //        {
        //            _logger.LogInformation($"DeleteOldFeedbacks - deleting this feedback : {item.FeedbackId}");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogInformation($"We had an error : {e}");
        //    }
        //}
    }
}
