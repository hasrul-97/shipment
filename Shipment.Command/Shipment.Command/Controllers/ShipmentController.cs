using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Shipment.Command.Abstraction.BusinessLogic;
using Shipment.Command.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Shipment.Command.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private readonly IBusinessManager _businessManager;
        private readonly IConfiguration _configuration;
        public ShipmentController(IBusinessManager businessManager,IConfiguration configuration)
        {
            _businessManager = businessManager;
            _configuration = configuration;
        }

        /// <summary>
        /// This method is the exposed controller which allows the end user to upload an excel file.
        /// </summary>
        /// <returns>A response with status code 200</returns>
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload()
        {
            IFormFile file = Request.Form.Files[0];
            ResponsePayload<string> response = new ResponsePayload<string>();
            try
            {
                response.StatusCode = 200;
                response.Message = await _businessManager.Upload(file);
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
        /// This method is the exposed controller whic alllows the end user to upload file directly to Azure Blob Storge
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("UploadFile")]
        public async Task<IActionResult> UploadFile()
        {
            ResponsePayload<string> response = new ResponsePayload<string>();
            try
            {
                IFormFile file = Request.Form.Files[0];
                response.StatusCode = 200;
                response.Message = await _businessManager.UploadFile(file);
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
        /// This is a sample method exposed to showcase the exception handling mechanism of this application
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("ThrowException")]
        public async Task<IActionResult> ThrowException()
        {
            ResponsePayload<string> response = new ResponsePayload<string>();
            try
            {
                throw new Exception("This is a sample exception");
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
        /// This is a helper method which allows to push the exception details to the respective API
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private async Task<ExceptionDetail> PushException(Exception exception)
        {
            HttpClient _client = new HttpClient();
            ExceptionDetail exceptionData = new ExceptionDetail(exception.Message, exception.StackTrace, User.Identity.Name, "Shipment.Command");
            var json = JsonConvert.SerializeObject(exceptionData);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            string postURL = _configuration.GetSection("ExceptionHandler").Value;
            var exceptionHandlerResponse = await _client.PostAsync(postURL + "api/ExceptionHandler/Log", stringContent);

            var responseString = await exceptionHandlerResponse.Content.ReadAsStringAsync();
            return exceptionData;
        }
    }
}
