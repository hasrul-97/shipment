using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Shipment.Query.Abstraction.BusinessLogic;
using Shipment.Query.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Shipment.Query.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShipmentController : ControllerBase
    {
        private readonly IBusinessManager _business;
        private readonly IConfiguration _configuration;
        public ShipmentController(IBusinessManager business,IConfiguration configuration)
        {
            _business = business;
            _configuration = configuration;
        }

        /// <summary>
        /// This method is used to fetch the list of shipment data from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetShipments")]
        public async Task<IActionResult> GetShipments()
        {
            ResponsePayload<ShipmentData> response = new ResponsePayload<ShipmentData>();
            try
            {
                response.StatusCode = 200;
                response.DataList = await _business.GetShipments();
                response.Message = "The data is fetched successfully";
            }
            catch (Exception exception)
            {
                ExceptionDetail exceptionData = await PushException(exception);

                response.StatusCode = 400;
                response.ErrorMessage = "There has been an error while processing the request. Kindly note this ID for your future references: " + exceptionData.ExceptionGUID;
            }
            return Ok(response);
        }



        /// <summary>
        /// This is a helper method to push the exception to a service to log it in the database
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private async Task<ExceptionDetail> PushException(Exception exception)
        {
            HttpClient _client = new HttpClient();
            ExceptionDetail exceptionData = new ExceptionDetail(exception.Message, exception.StackTrace, User.Identity.Name, "Shipment.Query");
            var json = JsonConvert.SerializeObject(exceptionData);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            string postURL = _configuration.GetSection("ExceptionHandler").Value;
            var exceptionHandlerResponse = await _client.PostAsync(postURL + "api/ExceptionHandler/Log", stringContent);

            var responseString = await exceptionHandlerResponse.Content.ReadAsStringAsync();
            return exceptionData;
        }
    }
}
