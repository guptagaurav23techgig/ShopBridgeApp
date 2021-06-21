using Dapper;
using Microsoft.Data.SqlClient;
using ShopBridge.Infrastructure.Data.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Infrastructure.Data.Context
{
    public class DbContext : IDbContext
    {
        private readonly ConnectionString _connectionString;

        public DbContext(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> ExecuteWithDataListAsync<T>(string sp, DynamicParameters dynamicParameters, CommandType commandType = CommandType.StoredProcedure, int commandTimeout = 0)
        {
            using (IDbConnection con = GetDbConnection())
            {
                return await con.QueryAsync<T>(sp, dynamicParameters, commandType: commandType, commandTimeout: commandTimeout);
            }
        }

        public async Task<int> ExecuteWithoutDataAsync(string sp, DynamicParameters dynamicParameters, CommandType commandType = CommandType.StoredProcedure)
        {
            using (IDbConnection con = GetDbConnection())
            {
                return await con.ExecuteAsync(sp, dynamicParameters, commandType: commandType);
            }
        }

        public DbConnection GetDbConnection()
        {
            return new SqlConnection(_connectionString.Value);
        }
    }
}
