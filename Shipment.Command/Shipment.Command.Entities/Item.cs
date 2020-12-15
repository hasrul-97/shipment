using System;
using System.Collections.Generic;
using System.Text;

namespace Shipment.Command.Entities
{
    public class Item
    {
        public string Item_Code { get; set; }
        public long Quantity { get; set; }
        public string UOM_Code { get; set; }
        public long Quantity_Processed { get; set; }
        public decimal Weight { get; set; }
        public decimal Volume { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public string Item_Note { get; set; }
    }
}
