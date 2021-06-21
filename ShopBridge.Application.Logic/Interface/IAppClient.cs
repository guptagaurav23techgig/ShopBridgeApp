using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopBridge.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Application.Logic.Interface
{
    public interface IAppClient
    {
        OkObjectResult CallbackResponse<T>(T Content, HttpContext httpContext);
        OkObjectResult CallbackResponse<T>(T Content);
        public Uri CreateRequestUri(string relativePath, string queryString = "");
        public Task<T> GetAsync<T>(Uri requestUrl);
        public Task<T> PostAsync<T>(Uri requestUrl, T content);
        public Task<ResponseResult<T1>> PostAsync<T1, T2>(Uri requestUrl, T2 content);        
        public Task<List<T>> GetAllAsync<T>(Uri requestUrl);
        public Task<T> PostAsync<T>(Uri requestUrl, string content);
    }
}
