using Microsoft.EntityFrameworkCore;
using PharmacistHelper.Models;
using PharmacistHelper.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacistHelper.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
