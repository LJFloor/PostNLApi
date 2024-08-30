using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MiniValidation;
using Newtonsoft.Json;
using PostNLApi.Exceptions;
using PostNLApi.Json;

namespace PostNLApi.Http
{
    /// <summary>
    /// A HttpClient that can handle JSON requests and responses
    /// </summary>
    public class JsonHttpClient : HttpClient
    {
        private readonly PostNLClient _client;

        private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            Converters = new List<JsonConverter>
            {
                new PostNLStringTrimmer()
            }
        };

        private readonly JsonSerializerSettings _jsonSerializerSettingsDebug = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            Converters = new List<JsonConverter>
            {
                new PostNLStringTrimmer()
            },


            Formatting = Formatting.Indented,
        };

        public JsonHttpClient(PostNLClient client)
        {
            _client = client;
            DefaultRequestHeaders.Add("Accept", "application/json");
            DefaultRequestHeaders.Add("User-Agent", "PostNLApi/v1.0.0");
        }

        /// <summary>
        /// Perform a GET request and return the response as a deserialized object
        /// </summary>
        /// <param name="url">Url to the resource</param>
        /// <typeparam name="T">Generic type to deserialize to</typeparam>
        /// <returns>Object deserialized to the generic type</returns>
        public async Task<T> Get<T>(string url)
        {
            _client.WriteDebugMessage($"GET {url}");
            var response = await GetAsync(url);
            return await ProcessResponse<T>(response);
        }

        /// <summary>
        /// Perform a POST request and return the response as a deserialized object
        /// </summary>
        /// <param name="url">Url to post the data to</param>
        /// <param name="data">The data to send</param>
        /// <typeparam name="T">Generic type to deserialize to</typeparam>
        /// <returns>Object deserialized to the generic type</returns>
        public async Task<T> Post<T>(string url, object data)
        {
            MiniValidator.TryValidate(data, out var results);

            if (results.Count > 0 && results.Values.First().Length > 0)
            {
                var propertyName = results.Keys.First();
                var error = results[propertyName].First();
                throw new ValidationException($"{propertyName}: {error}");
            }

            var json = JsonConvert.SerializeObject(data, _jsonSerializerSettings);
            if (_client.EnableDebug)
            {
                var indentedJson = JsonConvert.SerializeObject(data, _jsonSerializerSettingsDebug);
                _client.WriteDebugMessage($"POST {url}{Environment.NewLine}{indentedJson}");
            }

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await PostAsync(url, content);
            return await ProcessResponse<T>(response);
        }

        private async Task<T> ProcessResponse<T>(HttpResponseMessage response)
        {
            _client.WriteDebugMessage($"{(int)response.StatusCode} {response.ReasonPhrase}");
            var responseString = await response.Content.ReadAsStringAsync();

            if (responseString.StartsWith("{") || responseString.StartsWith("["))
            {
                responseString = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(responseString), new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                });
            }

            _client.WriteDebugMessage($"Response is {responseString.Length} characters long:{Environment.NewLine}{responseString}");

            if (!response.IsSuccessStatusCode)
            {
                _client.WriteDebugMessage("Oh boy, not a success status code");
                try
                {
                    var serverError = JsonConvert.DeserializeObject<ServerError>(responseString);
                    if (!string.IsNullOrEmpty(serverError?.Message.Trim()))
                    {
                        throw serverError;
                    }
                }
                catch (JsonException)
                {
                    throw new ApiException($"Server responded: {responseString}");
                }
            }

            try
            {
                var content = JsonConvert.DeserializeObject<T>(responseString);
                if (content == null)
                {
                    throw new ApiException("No content returned");
                }

                return content;
            }
            catch (JsonException)
            {
                throw new ApiException(responseString);
            }
        }
    }
}