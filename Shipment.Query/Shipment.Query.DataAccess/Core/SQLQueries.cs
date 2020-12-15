using System;
using System.Collections.Generic;
using System.Text;

namespace Shipment.Query.DataAccess.Core
{
    /// <summary>
    /// This class contains the list of SQL Queries used by the data access layer
    /// </summary>
    public class SQLQueries
    {
        public static readonly string GetAllShipmentData = "SELECT * FROM shipment.shipment_data;";
    }
}
