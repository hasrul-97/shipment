using Microsoft.AspNetCore.Http;
using Shipment.Command.Abstraction.BusinessLogic;
using Shipment.Command.Abstraction.DataAccess;
using Shipment.Command.Abstraction.Utilities;
using Shipment.Command.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shipment.Command.BusinessLogic
{
    public class BusinessManager : IBusinessManager
    {
        private readonly IDataAccessManager _dataAccessManager;
        private readonly IExcelParser _excelParser;
        public BusinessManager(IDataAccessManager dataAccessManager,IExcelParser excelParser)
        {
            _dataAccessManager = dataAccessManager;
            _excelParser = excelParser;
        }
        /// <summary>
        /// This method accepts a IFormFile as a parameter and send it forth to extract the data from the worksheet.
        /// On completion, the method saves the data to the database by passing the list to the appropriate data acess layer
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<string> Upload(IFormFile file)
        {
            try
            {
                await _dataAccessManager.UploadFile(file);
                List<ShipmentData> shipmentData = _excelParser.ParseShipmentFile(file, "Data");
                return await _dataAccessManager.StoreShipmentData(shipmentData);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This method is used upload the incoming IFormFile from the controller with the help of the respective method in the Data Access layer
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<string> UploadFile(IFormFile file)
        {
            try
            {
                return await _dataAccessManager.UploadFile(file);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
