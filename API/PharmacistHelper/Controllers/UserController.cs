using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacistHelper.Services.Interfaces;

namespace PharmacistHelper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("v1/Addnew")]
        public async Task<IActionResult> Addnew(Models.User entity)
        {
            entity.Id = Guid.NewGuid();
            return Ok(await _userService.AddAsync(entity));
        }
    }
}