using ShopBridge.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Application.Interface
{
    public interface IInventoryService
    {
        Task<string> AddInventoryAsync(InventoryModel inventory, string changesByIP);

        Task<string> UpdateInventoryAsync(InventoryModel inventory, string changesByIP);

        Task<string> DeleteInventoryAsync(Int64 inventoryId, string changesByIP);

        Task<IEnumerable<InventoryModel>> GetInventoriesAsync(Int64 inventoryId);
    }
}
