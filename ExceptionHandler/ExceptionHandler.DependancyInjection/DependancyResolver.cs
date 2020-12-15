using ExceptionHandler.Abstraction.BusinessLogic;
using ExceptionHandler.Abstraction.DataAccess;
using ExceptionHandler.BusinessLogic;
using ExceptionHandler.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExceptionHandler.DependancyInjection
{
    public class DependancyResolver
    {
        public static void ConfigureService(IServiceCollection service)
        {
            service.AddScoped<IBusinessManager, BusinessManager>();
            service.AddScoped<IDataAccessManager, DataAccessManager>();
            service.AddScoped<IRepository, Repository>();
        }
    }
}
