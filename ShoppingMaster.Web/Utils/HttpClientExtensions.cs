﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShoppingMaster.Web.Utils
{
    public static class HttpClientExtensions
    {
        private static readonly MediaTypeHeaderValue ContentType
            = new MediaTypeHeaderValue("application/json");

        public static async Task<T> ReadContentAs<T>(
            this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(
                    $"Something went wrong calling the API: {response.ReasonPhrase}");
            }

            var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonSerializer.Deserialize<T>(dataAsString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public static async Task<HttpResponseMessage> PostAsJson<T>(
            this HttpClient httpClient,
            string url,
            T data)
        {
            var dataAsString = JsonSerializer.Serialize(data);
            var content = new StringContent(dataAsString, Encoding.UTF8, "application/json");
            return await httpClient.PostAsync(url, content).ConfigureAwait(false);
        }

        public static async Task<HttpResponseMessage> PutAsJson<T>(
            this HttpClient httpClient,
            string url,
            T data)
        {
            var dataAsString = JsonSerializer.Serialize(data);
            var content = new StringContent(dataAsString, Encoding.UTF8, "application/json");
            return await httpClient.PutAsync(url, content).ConfigureAwait(false);
        }
    }
}
