using System;
using System.Collections.Generic;
using System.Text;

namespace Shipment.Command.DataAccess.Core
{
    /// <summary>
    /// This class contains the list of SQL Queries used by the data access layer
    /// </summary>
    public class SQLQueries
    {
        public static readonly string InsertFileUploadData = "INSERT into shipment.file (FileGUID,FileName,FileSize,FileType,UploadedOn) VALUES(@FileGUID,@FileName,@FileSize,@FileType,@UploadedOn)";
        public static readonly string InsertData = "INSERT INTO `shipment`.`shipment_data` (`shipment_no`,`customer_code`,`delivery_zone_code`,`delivery_address`,`delivery_postal`,`delivery_status`) VALUES" +
            "(@Shipment_No,@Customer_Code,@Delivery_Zone_Code,@Delivery_Address,@Delivery_Postal,@Delivery_Status) ON DUPLICATE KEY UPDATE customer_code=@Customer_Code," +
            "delivery_zone_code=@Delivery_Zone_Code,delivery_address=@Delivery_Address,delivery_postal=@Delivery_Postal,delivery_status=@Delivery_Status";
    }
}
