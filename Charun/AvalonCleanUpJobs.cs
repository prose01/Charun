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
        private readonly IImageUtil _imageUtil;
        private readonly IHelperMethods _helper;

        public AvalonCleanUpJobs(ILogger<AvalonCleanUpJobs> logger, IOptions<Settings> settings, IProfilesQueryRepository profilesQueryRepository, IFeedbackRepository feedbackRepository, IImageUtil imageUtil, IHelperMethods helper)
        {
            _logger = logger;
            _settings = settings;
            _profilesQueryRepository = profilesQueryRepository;
            _feedbackRepository = feedbackRepository;
            _imageUtil = imageUtil;
            _helper = helper;
        }

        [Function("DeleteOldProfiles")]
        public async Task DeleteOldProfiles([TimerTrigger("0 */2 * * * *")] MyInfo myTimer)
        {
            try
            {
                var _deleteProfileDaysBack = _settings.Value.DeleteProfileDaysBack;
                var _deleteProfileLimit = _settings.Value.DeleteProfileLimit;

                var oldProfiles = await _profilesQueryRepository.GetOldProfiles(_deleteProfileDaysBack, _deleteProfileLimit);

                foreach (var profile in oldProfiles)
                {
                    _logger.LogInformation($"DeleteOldProfiles - deleting this profile : {profile.Name}");
                    //await _helper.DeleteProfileFromAuth0(profile.ProfileId);
                    //await _imageUtil.DeleteAllImagesForProfile(profile.ProfileId);
                    //await _profilesQueryRepository.DeleteProfile(profile.ProfileId);
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation($"We had an error : {e}");
            }
        }

        [Function("DeleteOldFeedbacks")]
        public async Task DeleteOldFeedbacks([TimerTrigger("0 */2 * * * *")] MyInfo myTimer)
        {
            try
            {
                //await _feedbackRepository.DeleteOldFeedbacks();
            }
            catch (Exception e)
            {
                _logger.LogInformation($"We had an error : {e}");
            }
        }
    }
}
