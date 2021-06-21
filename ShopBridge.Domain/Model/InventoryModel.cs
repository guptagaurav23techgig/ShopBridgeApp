using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Domain.Model
{
    public class InventoryModel
    {
        public Int64 InventoryId { get; set; }

        public string InventoryName { get; set; }

        public string InventoryDescription { get; set; }

        public decimal InventoryPrice { get; set; }

        public Int16? isAvailable { get; set; }

        public DateTime? CreateDateTime { get; set; }

        public DateTime? UpdateDateTime { get; set; }

        public Int16? isDeleted { get; set; }

        public string CreatedByIP { get; set; }

        public string UpdatedByIP { get; set; }

        public string msg { get; set; }

        public string ip { get; set; }
    }
}
