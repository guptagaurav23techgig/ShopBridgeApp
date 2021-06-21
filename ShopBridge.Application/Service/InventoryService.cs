using ShopBridge.Application.Interface;
using ShopBridge.Application.Logic.Interface;
using ShopBridge.Domain.Interface;
using ShopBridge.Domain.Model;
using ShopBridge.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Application.Service
{
    public class InventoryService : IInventoryService
    {
        private IInventoryRepository _inventoryRepository;
        private IAppUtility _appUtility;

        public InventoryService(IInventoryRepository inventoryRepository, IAppUtility appUtility)
        {
            _inventoryRepository = inventoryRepository;
            _appUtility = appUtility;
        }

        public async Task<string> AddInventoryAsync(InventoryModel inventory, string changesByIP)
        {
            if (inventory.InventoryId == 0)
            {
                var result = await _inventoryRepository.InsertUpdateAsync(inventory, changesByIP);

                if (result != null)
                {
                    return result.msg;
                }
                else
                {
                    return "Error occurred while saving";
                }
            }
            else
            {
                return "Invalid Data";
            }
        }

        public async Task<string> DeleteInventoryAsync(long inventoryId, string changesByIP)
        {
            if (inventoryId > 0)
            {
                var result = await _inventoryRepository.DeleteInventoryAsync(inventoryId, changesByIP);

                if (result > 0)
                {
                    return "success";
                }
                else
                {
                    return "Error occurred while saving";
                }
            }
            else
            {
                return "Invalid Data";
            }
        }

        public async Task<IEnumerable<InventoryModel>> GetInventoriesAsync(long inventoryId)
        {
            return await _inventoryRepository.GetInventoriesAsync(inventoryId);
        }

        public async Task<string> UpdateInventoryAsync(InventoryModel inventory, string changesByIP)
        {
            if (inventory.InventoryId > 0)
            {
                var result = await _inventoryRepository.InsertUpdateAsync(inventory, changesByIP);

                if (result != null)
                {
                    return result.msg;
                }
                else
                {
                    return "Error occurred while saving";
                }
            }
            else
            {
                return "Invalid Data";
            }
        }
    }
}
