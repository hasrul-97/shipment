using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shipment.Command.Abstraction.BusinessLogic
{
    public interface IBusinessManager
    {
        Task<string> Upload(IFormFile file);
        Task<string> UploadFile(IFormFile file);
    }
}
