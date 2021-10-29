using BetFriend.Infrastructure.Abstractions;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace BetFriend.Infrastructure
{
    public class HttpService : IHttpService
    {
        private readonly IRestClient _client;

        public HttpService(IRestClient restClient)
        {
            _client = restClient;
        }

        public async Task<T> PostAsync<T>(string url, string json, string token)
        {
            try
            {
                var req = new RestRequest(url, Method.POST, DataFormat.Json);
                req.AddHeader("Content-Type", "application/json");
                req.AddHeader("Language", System.Globalization.CultureInfo.CurrentUICulture.Name);
                if (!string.IsNullOrEmpty(token))
                    req.AddHeader("Authorization", token);
                req.AddJsonBody(json);
                var response = await _client.ExecuteAsync(req).ConfigureAwait(false);
                if (!response.IsSuccessful)
                    throw new Exception(response.Content);
                return JsonConvert.DeserializeObject<T>(response.Content);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
