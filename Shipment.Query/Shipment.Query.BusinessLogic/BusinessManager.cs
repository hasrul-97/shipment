using Shipment.Query.Abstraction.BusinessLogic;
using Shipment.Query.Abstraction.DataAccess;
using Shipment.Query.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shipment.Query.BusinessLogic
{
    public class BusinessManager : IBusinessManager
    {
        private readonly IDataAccessManager _dataAccessManager;
        public BusinessManager(IDataAccessManager dataAccessManager)
        {
            _dataAccessManager = dataAccessManager;
        }

        /// <summary>
        /// This method inturn calls the a method in the data access layer to fetch the list of data.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ShipmentData>> GetShipments()
        {
            try
            {
                return await _dataAccessManager.GetShipments();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
