using ShopBridge.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Domain.Interface
{
    public interface IInventoryRepository
    {
        Task<InventoryModel> InsertUpdateAsync(InventoryModel inventory, string changesByIP);

        Task<int> DeleteInventoryAsync(Int64 inventoryId, string changesByIP);

        Task<IEnumerable<InventoryModel>> GetInventoriesAsync(Int64 inventoryId);
    }
}
