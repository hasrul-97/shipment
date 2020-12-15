using Shipment.Query.Abstraction.DataAccess;
using Shipment.Query.DataAccess.Core;
using Shipment.Query.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shipment.Query.DataAccess
{
    public class DataAccessManager : IDataAccessManager
    {
        private readonly IRepository _repository;
        public DataAccessManager(IRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// This method fetches the list of shipment data from the database
        /// </summary>
        /// <returns></returns>
        public async Task<List<ShipmentData>> GetShipments()
        {
            try
            {
                List<ShipmentData> shipmentData = await _repository.FetchList<ShipmentData>(SQLQueries.GetAllShipmentData);
                return shipmentData;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
