using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShopBridge.Application.Logic.Interface;
using ShopBridge.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.UI.Controllers
{
    public class APIClientController : Controller
    {
        private readonly IConfiguration _configure;
        public string apiBaseUrl;
        private readonly IAppClient _appClient;
        private readonly IAppUtility _appUtility;
        public APIClientController(IConfiguration configuration, IAppClient appClient, IAppUtility appUtility)
        {
            _configure = configuration;
            apiBaseUrl = _configure["GlobalizationString:APIUrl"] + "/api";
            _appClient = appClient;
            _appUtility = appUtility;
        }

        [HttpPost]
        public async Task<IActionResult> RquestToAPI([FromBody] RequestData request)
        {
            if (!string.IsNullOrEmpty(request.url))
            {
                var requestUrl = _appClient.CreateRequestUri(apiBaseUrl + request.url);

                var result = await _appClient.PostAsync<ResponseResult<string>>(requestUrl, request.data);

                if (result != null)
                {
                    return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                }
                else
                {
                    ResponseResult<string> result1 = new ResponseResult<string>();
                    result1.Type = "fail";
                    result1.Message = "Request Failed";

                    return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(result1));
                }
            }
            else
            {
                ResponseResult<string> result = new ResponseResult<string>();
                result.Type = "fail";                
                result.Message = "Invalid request";

                return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(result));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetRquestToAPI(string url, string queryString, string isGetAll)
        {
            if (!string.IsNullOrEmpty(url))
            {
                var requestUrl = _appClient.CreateRequestUri(apiBaseUrl + url, queryString);
                if (isGetAll == "1")
                {
                    var result = await _appClient.GetAllAsync<string>(requestUrl);
                    //return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(result));

                    if (result != null)
                    {
                        return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    }
                    else
                    {
                        ResponseResult<string> result1 = new ResponseResult<string>();
                        result1.Type = "fail";
                        result1.Message = "Request Failed";

                        return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(result1));
                    }
                }
                else
                {
                    var result = await _appClient.GetAsync<string>(requestUrl);
                    //return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(result));

                    if (result != null)
                    {
                        return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    }
                    else
                    {
                        ResponseResult<string> result1 = new ResponseResult<string>();
                        result1.Type = "fail";
                        result1.Message = "Request Failed";

                        return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(result1));
                    }
                }
            }
            else
            {
                ResponseResult<string> result = new ResponseResult<string>();
                result.Type = "fail";                
                result.Message = "Invalid request";

                return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(result));
            }
        }
    }
}
