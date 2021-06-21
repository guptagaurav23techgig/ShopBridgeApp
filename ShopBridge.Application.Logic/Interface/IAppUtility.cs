using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Application.Logic.Interface
{
    public interface IAppUtility
    {
        string GetIPAddress(HttpContext httpContext);

        T1 ExchangeModelData<T1, T2>(T2 Content);
        
        T DapperRowToModel<T>(object Content);   
    }
}
