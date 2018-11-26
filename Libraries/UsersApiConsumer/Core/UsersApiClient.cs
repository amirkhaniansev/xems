using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AuthTokenService;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using UsersAPI.Models;

namespace UsersApiConsumer.Core
{
    public class UsersApiClient : IApiClient
    {
        private readonly string _serverAddress;

        private readonly HttpClient _client;

        private string _accessToken;

        private const string USERS_URI = "api/users";

        private const string VERIFICATIONS_URI = "api/verifications";

        private const string LECTURERS_URI = "api/lecturers";

        private const string STUDENTS_URI = "api/students";

        private const string APPLICATION_JSON = "application/json";       

        public UsersApiClient(string serverAddress)
        {
            this._serverAddress = serverAddress;

            this._client = new HttpClient();
            this._client.BaseAddress = new Uri(serverAddress);
            this._client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(APPLICATION_JSON));
        }

        public async Task<Response<string>> SignUpUserAsync(UserSignUpInfo userSignUpInfo)
        {
            return await this.SendNoContentPostRequest(USERS_URI, userSignUpInfo);
        }

        public async Task<Response<string>> VerifyUserAsync(UserVerificationInfo userVerificationInfo)
        {
            return await this.SendNoContentPostRequest(VERIFICATIONS_URI, userVerificationInfo);
        }

        public async Task<Response<string>> AddLecturerProfileAsync(LecturerBase lecturer)
        {
            return await this.SendNoContentPostRequest(LECTURERS_URI, lecturer);
        }

        public async Task<Response<string>> AddStudentProfileAsync(StudentBase student)
        {
            return await this.SendNoContentPostRequest(STUDENTS_URI, student);
        }

        public async Task<Response<User>> GetUserByUsernameAsync(string username)
        {
            return await this.SendGetRequest<User, string>(USERS_URI, username);
        }

        private async Task<Response<TData>> SendGetRequest<TData, TParameter>(
            string uri, TParameter parameter)
        {
            if(uri == null)
                throw new ArgumentNullException(nameof(uri));

            var response = await this._client.GetAsync($"{uri}/{parameter}");

            return new Response<TData>(
                JsonConvert.DeserializeObject<TData>(await response.Content.ReadAsStringAsync()),
                this.GetResponseStatus((int)response.StatusCode));
        }

        private async Task<Response<string>> SendNoContentPostRequest<T>(string uri, T data)
        {
            if(data == null)
                throw new ArgumentNullException(nameof(data));

            var response = await this._client.PostAsync(uri, this.ConstructContent(data));
            
            return new Response<string>(
                await response.Content.ReadAsStringAsync(),
                this.GetResponseStatus((int)response.StatusCode));
        }

        private StringContent ConstructContent(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json,Encoding.UTF8,APPLICATION_JSON);
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
                    return ResponseStatus.NoContent;
                case StatusCodes.Status404NotFound:
                    return ResponseStatus.NotFound;
                case StatusCodes.Status401Unauthorized:
                case StatusCodes.Status403Forbidden:
                    return ResponseStatus.Fail;
                case StatusCodes.Status500InternalServerError:
                    return ResponseStatus.InternalServerError;
                default:
                    return ResponseStatus.UnknownError;
            }
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
            this._client.SetBearerToken(eventArgs.AccessToken);
        }
    }
}
