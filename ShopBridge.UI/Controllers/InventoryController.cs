using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShopBridge.Application.Logic.Interface;
using ShopBridge.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.UI.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IConfiguration _configure;
        public readonly string apiBaseUrl;
        public readonly string appBaseUrl;
        private readonly IAppClient _appClient;
        private readonly IAppUtility _appUtility;

        public InventoryController(IConfiguration configuration, IAppClient appClient, IAppUtility appUtility)
        {
            _configure = configuration;
            apiBaseUrl = _configure["GlobalizationString:APIUrl"] + "/api";
            appBaseUrl = _configure["GlobalizationString:UIUrl"];
            _appClient = appClient;
            _appUtility = appUtility;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpContext.Response.Cookies.Append("appBaseUrl", appBaseUrl,
            new Microsoft.AspNetCore.Http.CookieOptions()
            {
                Path = "/",
                HttpOnly = false,
                Secure = false
            });
            HttpContext.Response.Cookies.Append("apiBaseUrl", apiBaseUrl,
            new Microsoft.AspNetCore.Http.CookieOptions()
            {
                Path = "/",
                HttpOnly = false,
                Secure = false
            });

            SearchInventoryViewModel searchInventory = new SearchInventoryViewModel();

            var requestUrl = _appClient.CreateRequestUri(apiBaseUrl + "/Inventory/ListOfInventory");

            searchInventory.GetInventories = await _appClient.GetAllAsync<InventoryViewModel>(requestUrl);

            return View(searchInventory);
        }

        [HttpGet]        
        public async Task<IActionResult> Add()
        {
            var requestUrl = _appClient.CreateRequestUri(apiBaseUrl + "/Inventory/FetchInventory", "inventoryId=-1");

            InventoryViewModel inventory = await _appClient.GetAsync<InventoryViewModel>(requestUrl);

            if (inventory == null)
            {
                inventory = new InventoryViewModel();
            }

            return View("Save", inventory);
        }

        [HttpGet]        
        public async Task<IActionResult> Edit(int inventoryId)
        {
            var requestUrl = _appClient.CreateRequestUri(apiBaseUrl + "/Inventory/FetchInventory", "inventoryId=" + inventoryId);
            
            InventoryViewModel inventory = await _appClient.GetAsync<InventoryViewModel>(requestUrl);

            if (inventory == null)
            {
                ViewBag.isAllow = false;
                ModelState.AddModelError("msg", "No record found");
                inventory = new InventoryViewModel();
            }
            else
            {
                ViewBag.isAllow = true;
            }

            return View("Save", inventory);
        }        
    }
}
