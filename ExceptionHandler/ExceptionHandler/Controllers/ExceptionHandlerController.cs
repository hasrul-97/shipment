using ExceptionHandler.Abstraction.BusinessLogic;
using ExceptionHandler.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExceptionHandler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExceptionHandlerController : ControllerBase
    {
        private readonly IBusinessManager _businessManager;
        public ExceptionHandlerController(IBusinessManager businessManager)
        {
            _businessManager = businessManager;
        }

        /// <summary>
        /// This method is used to log an exception to the database. It accepts a parameter which consists of the details of the exception.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns>A response with status code 200</returns>
        [HttpPost]
        [Route("Log")]
        public async Task<IActionResult> Log(ExceptionDetail exception)
        {
            ResponsePayload<string> response = new ResponsePayload<string>();
            try
            {
                response.StatusCode = 200;
                response.Message = await _businessManager.StoreException(exception);
            }
            catch (Exception ex)
            {
                response.StatusCode = 400;
                response.ErrorMessage = ex.Message;
            }
            return Ok(response);
        }

        /// <summary>
        /// This method is used to fetch all the exception details from the database.
        /// The method accepts a param to verify if the user is an admin user from the user table,
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>A response with status code 200</returns>
        [HttpGet]
        [Route("GetAllLogs")]
        public async Task<IActionResult> GetAllLogs(string userID)
        {
            ResponsePayload<ExceptionDetail> response = new ResponsePayload<ExceptionDetail>();
            try
            {
                response.StatusCode = 200;
                response.DataList = await _businessManager.GetAllExceptions(userID);
            }
            catch(Exception exception)
            {
                response.StatusCode = 400;
                response.ErrorMessage = exception.Message;
            }
            return Ok(response);
        }
    }
}
