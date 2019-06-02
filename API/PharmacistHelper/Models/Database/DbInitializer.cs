using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacistHelper.Models.Database
{
    public class DbInitializer
    {
        public static void Seed(PHContext context)
        {
            context.Database.Migrate();
        }
    }
}
