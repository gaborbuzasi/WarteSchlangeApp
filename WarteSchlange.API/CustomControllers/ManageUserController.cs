using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarteSchlange.API.Helpers;
using WarteSchlange.API.Models;

namespace WarteSchlange.API.CustomControllers
{
    [Produces("application/json")]
    [Route("api/ManageUser")]
    public class ManageUserController : Controller
    {
        MainContext _context;
        QueueHelper queueHelper;

        public ManageUserController(MainContext context)
        {
            _context = context;
            queueHelper = new QueueHelper(context);
        }

        [Route("createUser")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserModel newUser)
        {
            if (_context.Users.Where(user => user.Email == newUser.Email).Count() > 0)
                return BadRequest("Email is already in use");

            _context.Add(newUser);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest("Failed to update database");
            }

            return Ok(newUser.Id);
        }
    }
}