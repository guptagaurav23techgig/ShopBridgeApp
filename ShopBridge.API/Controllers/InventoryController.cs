using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopBridge.Application.Interface;
using ShopBridge.Application.Logic.Interface;
using ShopBridge.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventory;
        private readonly IAppClient _appClient;

        public InventoryController(IInventoryService inventory, IAppClient appClient)
        {
            _inventory = inventory;
            _appClient = appClient;
        }

        [HttpGet, Route("ListOfInventory")]
        public async Task<IActionResult> GetInventories()
        {
            return _appClient.CallbackResponse(await _inventory.GetInventoriesAsync(0));
        }

        [HttpGet, Route("FetchInventory")]
        public async Task<IActionResult> GetInventory(long inventoryId)
        {
            return _appClient.CallbackResponse((await _inventory.GetInventoriesAsync(inventoryId)).FirstOrDefault());
        }

        [HttpPost, Route("Add")]
        public async Task<IActionResult> AddInventory([FromBody] InventoryModel inventory)
        {
            inventory.InventoryId = 0;
            var res = await _inventory.AddInventoryAsync(inventory, inventory.ip);

            if(res== "Added")
            {
                return Ok(new { Type = "success", Message = "Successfully added" });
            }
            else
            {
                return BadRequest(new { Type = "fail", Message = "Not Successful" });
            }
        }

        [HttpPost, Route("Edit")]
        public async Task<IActionResult> EditInventory([FromBody] InventoryModel inventory)
        {
            var res = await _inventory.UpdateInventoryAsync(inventory, inventory.ip);

            if (res == "Updated")
            {
                return Ok(new { Type = "success", Message = "Successfully updated" });
            }
            else
            {
                return BadRequest(new { Type = "fail", Message = "Not Successful" });
            }
        }

        [HttpPost, Route("Delete")]
        public async Task<IActionResult> DeleteInventory([FromBody] InventoryModel inventory)
        {
            var res = await _inventory.DeleteInventoryAsync(inventory.InventoryId, inventory.ip);

            if(res== "success")
            {
                return Ok(new { Type = "success", Message = "Successfully deleted" });
            }
            else
            {
                return BadRequest(new { Type = "fail", Message = "Not Successful" });
            }
        }
    }
}
