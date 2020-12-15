using Shipment.Query.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shipment.Query.Abstraction.BusinessLogic
{
    public interface IBusinessManager
    {
        Task<List<ShipmentData>> GetShipments();
    }
}
