using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AuthTokenService;
using ExamsApiConsumer.Constants;
using ExamsApiConsumer.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ExamsApiConsumer.Core
{
    public class ExamsApiClient : IApiClient
    {
        private readonly string _serverAddress;

        private readonly HttpClient _client;

        private string _accessToken;

        public ExamsApiClient(string serverAddress)
        {
            this._serverAddress = serverAddress;
            
            this._client = new HttpClient();
            this._client.BaseAddress = new Uri(serverAddress);
            this._client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Response<string>> PostExam(Exam exam)
        {
            var response = await this._client.PostAsync(Urls.ExamsUrl, this.ConstructContent(exam));

            var content = await response.Content.ReadAsStringAsync();

            return new Response<string>(content, GetResponseStatus((int)response.StatusCode));
        }

        public void Dispose()
        {
            this._client.Dispose();
        }

        public void UpdateToken(object sender, TokenEventArgs eventArgs)
        {
            if (eventArgs.AccessToken == this._accessToken)
                return;

            this._accessToken = eventArgs.AccessToken;
            this._client.SetBearerToken(this._accessToken);
        }

        private StringContent ConstructContent(object data)
        {
            if (data == null)
                return null;

            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private ResponseStatus GetResponseStatus(int statusCode)
        {
            switch (statusCode)
            {
                case StatusCodes.Status200OK:
                case StatusCodes.Status201Created:
                case StatusCodes.Status202Accepted:
                    return ResponseStatus.Success;
                case StatusCodes.Status204NoContent:
                case StatusCodes.Status404NotFound:
                case StatusCodes.Status401Unauthorized:
                case StatusCodes.Status403Forbidden:
                    return ResponseStatus.Fail;
                default:
                    return ResponseStatus.Unknown;
            }
        }
    }
}