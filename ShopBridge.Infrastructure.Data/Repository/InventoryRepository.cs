using Dapper;
using ShopBridge.Domain.Interface;
using ShopBridge.Domain.Model;
using ShopBridge.Infrastructure.Data.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Infrastructure.Data.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        private IDbContext _dbContext;

        public InventoryRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> DeleteInventoryAsync(long inventoryId, string changesByIP)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("InventoryId", inventoryId, System.Data.DbType.Int64);
            parameters.Add("ChangesByIP", changesByIP, System.Data.DbType.String);

            return await _dbContext.ExecuteWithoutDataAsync("proc_Delete_Inventory", parameters);
        }

        public async Task<IEnumerable<InventoryModel>> GetInventoriesAsync(long inventoryId)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("InventoryId", inventoryId, System.Data.DbType.Int64);
            
            return await _dbContext.ExecuteWithDataListAsync<InventoryModel>("proc_List_Inventory", parameters);
        }

        public async Task<InventoryModel> InsertUpdateAsync(InventoryModel inventory, string changesByIP)
        {
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("InventoryId", inventory.InventoryId, System.Data.DbType.Int64);
            parameters.Add("InventoryName", inventory.InventoryName, System.Data.DbType.String);
            parameters.Add("InventoryDescription", inventory.InventoryDescription, System.Data.DbType.String);
            parameters.Add("InventoryPrice", inventory.InventoryPrice, System.Data.DbType.Decimal);
            parameters.Add("isAvailable", inventory.isAvailable, System.Data.DbType.Boolean);
            parameters.Add("ChangesByIP", changesByIP, System.Data.DbType.String);

            return (await _dbContext.ExecuteWithDataListAsync<InventoryModel>("proc_InsertUpdate_Inventory", parameters)).FirstOrDefault();
        }
    }
}
