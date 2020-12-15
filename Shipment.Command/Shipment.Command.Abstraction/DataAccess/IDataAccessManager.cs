using Microsoft.AspNetCore.Http;
using Shipment.Command.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shipment.Command.Abstraction.DataAccess
{
    public interface IDataAccessManager
    {
        Task<string> StoreShipmentData(List<ShipmentData> data);
        Task<string> UploadFile(IFormFile file);
    }
}
