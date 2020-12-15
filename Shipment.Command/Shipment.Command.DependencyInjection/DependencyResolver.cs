using Microsoft.Extensions.DependencyInjection;
using Shipment.Command.Abstraction.BusinessLogic;
using Shipment.Command.Abstraction.DataAccess;
using Shipment.Command.Abstraction.Utilities;
using Shipment.Command.BusinessLogic;
using Shipment.Command.DataAccess;
using Shipment.Command.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shipment.Command.DependencyInjection
{
    public class DependencyResolver
    {
        public static void ConfigureService(IServiceCollection service)
        {
            //  DI FOR BUSINESS LOGIC LAYER
            service.AddScoped<IBusinessManager, BusinessManager>();

            //  DI FOR DATA ACCESS LAYER
            service.AddScoped<IDataAccessManager, DataAccessManager>();

            //  DI FOR REPOSITORY LAYER
            service.AddScoped<IRepository, Repository>();

            //  DI FOR EXCEL PARSER
            service.AddScoped<IExcelParser, ExcelParser>();
        }
    }
}
