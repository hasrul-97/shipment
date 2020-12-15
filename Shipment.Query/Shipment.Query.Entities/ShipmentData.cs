using System;
using System.Collections.Generic;
using System.Text;

namespace Shipment.Query.Entities
{
    public class ShipmentData
    {
        public string Shipment_No { get; set; }

        public string Customer_Code { get; set; }

        public string Delivery_Zone_Code { get; set; }

        public string Delivery_Address { get; set; }

        public string Delivery_Postal { get; set; }

        public string Delivery_Status { get; set; }
        //public List<TaskLog> Tasks { get; set; }
        //public List<Item> Items { get; set; }

    }
}
