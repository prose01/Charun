using Charun.Interfaces;
using Charun.Model;
using Microsoft.Extensions.Caching.Memory;
using RestSharp;
using System.Text.Json;

namespace Charun.Helpers
{
    public class HelperMethods : IHelperMethods
    {
        private readonly string _auth0ApiIdentifier;
        private readonly string _auth0TokenAddress;
        private readonly string _auth0_Client_id;
        private readonly string _auth0_Client_secret;
        private readonly int _millisecondsAbsoluteExpiration = 28800000;
        private IMemoryCache _cache;

        public HelperMethods()
        {
            _auth0ApiIdentifier = Environment.GetEnvironmentVariable("Auth0ApiIdentifier");
            _auth0TokenAddress = Environment.GetEnvironmentVariable("Auth0TokenAddress");
            _auth0_Client_id = Environment.GetEnvironmentVariable("Client_id");
            _auth0_Client_secret = Environment.GetEnvironmentVariable("Client_secret");
            _millisecondsAbsoluteExpiration = int.Parse(Environment.GetEnvironmentVariable("MillisecondsAbsoluteExpirationCache"));
        }

        /// <summary>Deletes the profile from Auth0. There is no going back!</summary>
        /// <param name="profileId">The profile identifier.</param>
        public async Task DeleteProfileFromAuth0(Profile profile)
        {
            try
            {
                if (profile == null) return;

                _ = _cache.TryGetValue("Auth0Token", out string token);

                var accessToken = string.IsNullOrEmpty(token) ? await GetAuth0Token() : token;

                var url = _auth0ApiIdentifier + "users/" + profile.Auth0Id;
                var client = new RestClient(url);
                var request = new RestRequest(url, Method.Delete);
                request.AddHeader("authorization", "Bearer " + accessToken);
                //var response = await client.ExecuteAsync(request, CancellationToken.None);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>Gets the auth0 token.</summary>
        /// <returns></returns>
        private async Task<string> GetAuth0Token()
        {
            try
            {
                // Save token until it expires, to minimize the number of tokens requested. Auth0 customers are billed based on the number of Machine to Machine Access Tokens issued by Auth0. 
                var client = new RestClient(_auth0TokenAddress);
                var request = new RestRequest(_auth0TokenAddress, Method.Post);
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", $"{{\"client_id\":\"{_auth0_Client_id}\",\"client_secret\":\"{_auth0_Client_secret}\",\"audience\":\"{_auth0ApiIdentifier}\",\"grant_type\":\"client_credentials\"}}", ParameterType.RequestBody);
                var response = await client.ExecuteAsync(request);

                var token = JsonSerializer.Deserialize<AccessToken>(response.Content).access_token;

                MemoryCacheEntryOptions options = new()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMilliseconds(_millisecondsAbsoluteExpiration)
                };

                _cache.Set("Auth0Token", token, options);

                return token;
            }
            catch
            {
                throw;
            }
        }
    }
}
