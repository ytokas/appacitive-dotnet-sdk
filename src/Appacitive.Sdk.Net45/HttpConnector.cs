﻿using Appacitive.Sdk.Internal;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Appacitive.Sdk.Net45
{
    public class HttpConnector : IHttpConnector
    {
        public static readonly HttpConnector Instance = new HttpConnector();

        public HttpClient _client = new HttpClient();

        public async Task<byte[]> GetAsync(string url, IDictionary<string, string> headers)
        {   
            return await ExecuteAsync("GET", url, headers, null);
        }

        public async Task<byte[]> DeleteAsync(string url, IDictionary<string, string> headers)
        {
            return await ExecuteAsync("DELETE", url, headers, null);
        }

        public async Task<byte[]> PutAsync(string url, IDictionary<string, string> headers, byte[] data)
        {
            return await ExecuteAsync("PUT", url, headers, data);
        }

        public async Task<byte[]> PostAsync(string url, IDictionary<string, string> headers, byte[] data)
        {
            return await ExecuteAsync("POST", url, headers, data);
        }

        private static readonly byte[] Empty = new byte[0];
        private async Task<byte[]> ExecuteAsync(string httpMethod, string url, IDictionary<string, string> headers, byte[] data)
        {
            var request = new HttpRequestMessage(GetHttpMethod(httpMethod), url);

            if (data != null)
            {
                var contents = new ByteArrayContent(data);
                contents.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                request.Content = contents;
            }
            if (headers != null)
            {
                foreach (var key in headers.Keys)
                    request.Headers.Add(key, headers[key]);
            }
            
            HttpResponseMessage response = await _client.SendAsync(request);
            var responseData = await response.Content.ReadAsByteArrayAsync();
            return responseData;
        }

        private static readonly Dictionary<string, HttpMethod> HttpMethods = new Dictionary<string, HttpMethod>(StringComparer.OrdinalIgnoreCase)
        {
            { "GET", HttpMethod.Get },
            { "POST", HttpMethod.Post },
            { "PUT", HttpMethod.Put },
            { "DELETE", HttpMethod.Delete },
            { "HEAD", HttpMethod.Head },
            { "OPTIONS", HttpMethod.Options },
        };

        private HttpMethod GetHttpMethod(string httpMethod)
        {
            return HttpMethods[httpMethod];
        }
    }
}
