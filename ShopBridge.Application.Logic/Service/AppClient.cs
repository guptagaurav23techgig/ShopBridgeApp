using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopBridge.Application.Logic.Interface;
using ShopBridge.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Application.Logic.Service
{
    public class AppClient : IAppClient
    {
        private HttpContent CreateHttpContent<T>(T content)
        {
            var json = JsonConvert.SerializeObject(content);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private HttpContent CreateHttpContent(string content)
        {
            var json = content;
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public OkObjectResult CallbackResponse<T>(T Content, HttpContext httpContext)
        {
            var queryStrings = httpContext.Request.QueryString.Value.Split('&');

            if (!string.IsNullOrEmpty(queryStrings[0]))
            {
                var queryStringParam = queryStrings[0].Split('=');
                var callback = queryStringParam[1];

                if (queryStringParam[0] == "?callback" && !string.IsNullOrEmpty(callback))
                {
                    httpContext.Response.WriteAsync(string.Format("{0}({1});", callback, Newtonsoft.Json.JsonConvert.SerializeObject(Content)));
                }
            }            
            return new OkObjectResult(Content);
        }

        public OkObjectResult CallbackResponse<T>(T Content)
        { 
            return new OkObjectResult(Content); 
        }

        public Uri CreateRequestUri(string relativePath, string queryString = "")
        {
            var endpoint = new Uri(relativePath);
            var uriBuilder = new UriBuilder(endpoint);
            uriBuilder.Query = queryString;
            return uriBuilder.Uri;
        }

        public async Task<T> GetAsync<T>(Uri requestUrl)
        {
            HttpClient httpClient = new HttpClient();           
            
            var response = await httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(data);
            }
            else
            {
                return default(T);
            }
        }

        public async Task<T> PostAsync<T>(Uri requestUrl, T content)
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T>(content));
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(data);
            }
            else
            {
                return default(T);
            }
        }

        public async Task<ResponseResult<T1>> PostAsync<T1, T2>(Uri requestUrl, T2 content)
        {
            HttpClient httpClient = new HttpClient();
            
            var response = await httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T2>(content));

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ResponseResult<T1>>(data);
            }
            else
            {
                return null;
            }
        }        

        public async Task<List<T>> GetAllAsync<T>(Uri requestUrl)
        {
            HttpClient httpClient = new HttpClient();
            
            var response = await httpClient.GetAsync(requestUrl, HttpCompletionOption.ResponseHeadersRead);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string code = response.ReasonPhrase;
                string codeText = response.StatusCode.ToString();
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<T>>(data);
            }
            else
            {
                return default(List<T>);
            }
        }

        public async Task<T> PostAsync<T>(Uri requestUrl, string content)
        {
            HttpClient httpClient = new HttpClient();
            
            var response = await httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent(content));
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(data);
            }
            else
            {
                return default(T);
            }
        }        
    }
}
