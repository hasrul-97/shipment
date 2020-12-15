using Microsoft.Extensions.DependencyInjection;
using Shipment.Query.Abstraction.BusinessLogic;
using Shipment.Query.Abstraction.DataAccess;
using Shipment.Query.BusinessLogic;
using Shipment.Query.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shipment.Query.DependencyInjection
{
    public class DependencyResolver
    {
        public static void ConfigureService(IServiceCollection services)
        {
            //  DI FOR BUSINESS LAYER
            services.AddScoped<IBusinessManager, BusinessManager>();

            //  DI FOR DATAACCESS LAYER
            services.AddScoped<IDataAccessManager, DataAccessManager>();

            //  DI FOR REPOSITORY LAYER
            services.AddScoped<IRepository, Repository>();
        }
    }
}
