using Blazor.Data;
using Blazor.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return userService.GetUsers();
        }

        [HttpGet("{userId}")]
        public User Get(long userId)
        {
            return userService.GetUser(userId);
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] User user)
        {
            if (user == null) return BadRequest();
            if (user.UserName == string.Empty || user.Email == string.Empty)
            {
                ModelState.AddModelError("UserName/Email", "The user Name or email shouldn't be empty");
            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (user.Id == 0)
                userService.InsertUser(user);
            else
                userService.UpdateUser(user);
            var createdUser = userService.GetUser(user.Id);
            return Created("user", createdUser);
        }

        [HttpDelete("{userId}")]
        public void Delete(long userId)
        {
            userService.DeleteUser(userId);
        }
    }
}
