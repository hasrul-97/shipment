using Shipment.Query.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shipment.Query.Abstraction.DataAccess
{
    public interface IDataAccessManager
    {
        Task<List<ShipmentData>> GetShipments();
    }
}
