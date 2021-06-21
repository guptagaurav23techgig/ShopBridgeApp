using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Infrastructure.Data.Service
{
    public interface IDbContext : IDisposable
    {
        DbConnection GetDbConnection();

        Task<IEnumerable<T>> ExecuteWithDataListAsync<T>(string sp, DynamicParameters dynamicParameters, CommandType commandType = CommandType.StoredProcedure, int commandTimeout = 0);

        Task<int> ExecuteWithoutDataAsync(string sp, DynamicParameters dynamicParameters, CommandType commandType = CommandType.StoredProcedure);
    }
}
