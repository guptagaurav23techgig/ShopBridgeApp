using ShopBridge.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Domain.ViewModel
{
    public class InventoryViewModel
    {
        public Int64 InventoryId { get; set; }

        [Required(ErrorMessage = "Enter Inventory Name"), MaxLength(250)]
        public string InventoryName { get; set; }

        [Required(ErrorMessage = "Enter Inventory Description"), MaxLength(500)]
        public string InventoryDescription { get; set; }

        [Required(ErrorMessage = "Enter Inventory Price")]        
        public decimal? InventoryPrice { get; set; }

        [Required(ErrorMessage = "Select availibility")]
        public Int16? isAvailable { get; set; }

        public string AvailableStatus 
        { 
            get 
            {
                if (isAvailable == 1)
                {
                    return "Available";
                }
                else
                {
                    return "Not available";
                }
            
            } 
        }

        public string ip { get; set; }
    }

    public class SearchInventoryViewModel
    {
        public string InventoryName { get; set; }

        public decimal InventoryPrice { get; set; }

        public IList<InventoryViewModel> GetInventories { get; set; }
    }
}
