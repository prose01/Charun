using Charun.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Charun
{
    public class AvalonCleanUpJobs
    {
        private readonly ILogger _logger;
        private readonly IProfilesQueryRepository _profilesQueryRepository;
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IAzureBlobStorage _azureBlobStorage;
        private readonly IHelperMethods _helper;

        public AvalonCleanUpJobs(ILogger<AvalonCleanUpJobs> logger, IProfilesQueryRepository profilesQueryRepository, IFeedbackRepository feedbackRepository, IAzureBlobStorage azureBlobStorage, IHelperMethods helper)
        {
            _logger = logger;
            _profilesQueryRepository = profilesQueryRepository;
            _feedbackRepository = feedbackRepository;
            _azureBlobStorage = azureBlobStorage;
            _helper = helper;
        }

        [Function("DeleteOldProfiles")]
        public async Task DeleteOldProfiles([TimerTrigger("%DeleteOldProfilesTimerTrigger%")] MyInfo myTimer)
        {
            try
            {
                var _deleteProfileDaysBack = int.Parse(Environment.GetEnvironmentVariable("DeleteProfileDaysBack"));
                var _deleteProfileLimit = int.Parse(Environment.GetEnvironmentVariable("DeleteProfileLimit"));

                var oldProfiles = await _profilesQueryRepository.GetOldProfiles(_deleteProfileDaysBack, _deleteProfileLimit);

                foreach (var profile in oldProfiles)
                {
                    _logger.LogInformation($"DeleteOldProfiles - deleting this profile : {profile.ProfileId}");
                    //await _helper.DeleteProfileFromAuth0(profile);
                    //await _azureBlobStorage.DeleteAllImagesAsync(profile.ProfileId);
                    //await _profilesQueryRepository.DeleteProfile(profile.ProfileId);
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation($"We had an error : {e}");
            }
        }

        [Function("DeleteOldFeedbacks")]
        public async Task DeleteOldFeedbacks([TimerTrigger("%DeleteOldFeedbackssTimerTrigger%")] MyInfo myTimer)
        {
            try
            {
                var items = await _feedbackRepository.ViewDeleteOldFeedbacks();

                foreach (var item in items)
                {
                    _logger.LogInformation($"DeleteOldFeedbacks - deleting this feedback : {item.FeedbackId}");
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation($"We had an error : {e}");
            }
        }
    }
}
