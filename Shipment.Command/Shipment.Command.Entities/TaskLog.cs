using System;
using System.Collections.Generic;
using System.Text;

namespace Shipment.Command.Entities
{
    public class TaskLog
    {
        public long Task_No { get; set; }
        public string Task_Status { get; set; }
        public string Driver_Code { get; set; }
        public string Pickup_Address { get; set; }
        public string Pickup_Postal { get; set; }
        public string Delivery_Address { get; set; }
        public string Delivery_Postal { get; set; }
        public string Task_Note { get; set; }
    }
}
