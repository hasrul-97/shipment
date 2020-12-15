using Npoi.Mapper.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shipment.Command.Entities
{
    public class ShipmentData
    {
        [Column("Shipment No")]
        public string Shipment_No { get; set; }

        [Column("Customer Code")]
        public string Customer_Code { get; set; }

        [Column("Delivery Zone Code")]
        public string Delivery_Zone_Code { get; set; }

        [Column("Delivery Address")]
        public string Delivery_Address { get; set; }

        [Column("Delivery Postal")]
        public string Delivery_Postal { get; set; }

        [Column("Delivery Status")]
        public string Delivery_Status { get; set; }

        //public List<TaskLog> Tasks { get; set; }
        //public List<Item> Items { get; set; }

    }
}
