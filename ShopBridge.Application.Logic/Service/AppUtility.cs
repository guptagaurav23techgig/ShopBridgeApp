using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ShopBridge.Application.Logic.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Application.Logic.Service
{
    public class AppUtility: IAppUtility
    {
        private readonly IConfiguration _configuration;

        public AppUtility(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetIPAddress(HttpContext httpContext)
        {
            String ip = "";
            try
            {
                ip = httpContext.Connection.RemoteIpAddress?.ToString();
            }
            catch
            {
                ip = httpContext.Connection.LocalIpAddress?.ToString();
            }
            return ip;
        }

        public T1 ExchangeModelData<T1, T2>(T2 Content)
        {
            string model = Newtonsoft.Json.JsonConvert.SerializeObject(Content);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T1>(model);
        }

        public T DapperRowToModel<T>(object Content)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(Content, Newtonsoft.Json.Formatting.Indented);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(json).FirstOrDefault();
        }      
    }
}
