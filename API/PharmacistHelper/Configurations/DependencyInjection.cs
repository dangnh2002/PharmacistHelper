using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PharmacistHelper.Models.Database;
using PharmacistHelper.Services;
using PharmacistHelper.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacistHelper.Configurations
{
    public class DependencyInjection
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped<DbContext, PHContext>();

            services.AddScoped<IUserService, UserService>();
        }
    }
}
