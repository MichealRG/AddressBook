using KsiazkaAdresowa.Data;
using KsiazkaAdresowa.Interfaces;
using KsiazkaAdresowa.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KsiazkaAdresowa.Extensions
{
    public static class AddApplicationServiceExtensions
    {
        public static IServiceCollection AddApplciationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IDataRepository, DataRepository>();
            services.AddScoped<IDtoServiceInterface, DtoService>();
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("AddressBookDatabase"));
            });
            return services;
        }
    }
}
