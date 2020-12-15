using Microsoft.AspNetCore.Http;
using Shipment.Command.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shipment.Command.Abstraction.Utilities
{
    public interface IExcelParser
    {
        List<ShipmentData> ParseShipmentFile(IFormFile file,string sheetName);
    }
}
