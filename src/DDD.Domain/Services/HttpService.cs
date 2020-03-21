using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DDD.Domain.Services
{
    public class HttpService : IHttpService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly HttpClient httpClientA;
        public Response PullRequests { get; private set; }
        public bool GetPullRequestsError { get; private set; }
        private readonly ILogger _logger;

        public HttpService(IHttpClientFactory clientFactory, ILogger<HttpService> logger)
        {
            _clientFactory = clientFactory;
            httpClientA = _clientFactory.CreateClient("HttpClientA");
            _logger = logger;
        }

        #region Wrapper
        private async Task<Response> GetAsync(HttpClient httpClient, string url, Dictionary<string, string> headers)
        {
            try
            {
                var request = CreateGetRequest(url, headers);
                var response = await httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    using var responseStream = await response.Content.ReadAsStreamAsync();
                    PullRequests = await JsonSerializer.DeserializeAsync<Response>(responseStream);
                }
                else
                {
                    GetPullRequestsError = true;
                    PullRequests = null;
                }
                return PullRequests;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong: {ex}");
                throw new Exception("Exception inside GetAsync", ex);
            }
        }

        private Response Get(HttpClient httpClient, string url, Dictionary<string, string> headers)
        {
            try
            {
                var request = CreateGetRequest(url, headers);
                var response = httpClient.SendAsync(request).Result;
                if (response.IsSuccessStatusCode)
                {
                    using var responseStream = response.Content.ReadAsStreamAsync().Result;
                    PullRequests = JsonSerializer.DeserializeAsync<Response>(responseStream).Result;
                }
                else
                {
                    GetPullRequestsError = true;
                    PullRequests = null;
                }
                return PullRequests;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong: {ex}");
                throw new Exception("Exception inside Get(HttpClientService)", ex);
            }
        }

        private async Task<Response> PostAsync(HttpClient httpClient, string url, object data, Dictionary<string, string> headers)
        {
            try
            {
                var request = CreatePostRequest(url, data, headers);
                var response = await httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    using var responseStream = await response.Content.ReadAsStreamAsync();
                    PullRequests = await JsonSerializer.DeserializeAsync<Response>(responseStream);
                }
                else
                {
                    GetPullRequestsError = true;
                    PullRequests = null;
                }
                return PullRequests;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong: {ex}");
                throw new Exception("Exception inside PostAsync(HttpClientService)", ex);
            }
        }

        private Response Post(HttpClient httpClient, string url, object data, Dictionary<string, string> headers)
        {
            try
            {
                var request = CreatePostRequest(url, data, headers);
                var response = httpClient.SendAsync(request).Result;
                if (response.IsSuccessStatusCode)
                {
                    using var responseStream = response.Content.ReadAsStreamAsync().Result;
                    PullRequests = JsonSerializer.DeserializeAsync<Response>(responseStream).Result;
                }
                else
                {
                    GetPullRequestsError = true;
                    PullRequests = null;
                }
                return PullRequests;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong: {ex}");
                throw new Exception("Exception inside Post(HttpClientService)", ex);
            }
        }

        private HttpRequestMessage CreatePostRequest(string url, object data, Dictionary<string, string> headers)
        {
            var dataAsString = JsonSerializer.Serialize(data);
            var content = new StringContent(dataAsString, System.Text.Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = content,
            };
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> entry in headers)
                {
                    request.Headers.Add(entry.Key, entry.Value);
                }
            }
            return request;
        }

        private HttpRequestMessage CreateGetRequest(string url, Dictionary<string, string> headers)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> entry in headers)
                {
                    request.Headers.Add(entry.Key, entry.Value);
                }
            }
            return request;
        }

        private async Task<Stream> GetStreamAsync(HttpClient httpClient, string url)
        {
            try
            {
                var stream = await httpClient.GetStreamAsync(url);
                return stream;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong: {ex}");
                throw new Exception("Exception inside GetStreamAsync", ex);
            }
        }
        #endregion

        #region Get, Post

        public async Task<Response> GetAsync(string url, Dictionary<string, string> headers = null)
        {
            return await GetAsync(httpClientA, url, headers);
        }

        public Response Get(string url, Dictionary<string, string> headers = null)
        {
            return Get(httpClientA, url, headers);
        }

        public async Task<Response> PostAsync(string url, object data, Dictionary<string, string> headers = null)
        {
            return await PostAsync(httpClientA, url, data, headers);
        }
        public Response Post(string url, object data, Dictionary<string, string> headers = null)
        {
            return Post(httpClientA, url, data, headers);
        }
        #endregion

        #region Stream
        public async Task<Stream> GetStreamAsync(string url)
        {
            return await GetStreamAsync(httpClientA, url);
        }
        #endregion
    }
}
