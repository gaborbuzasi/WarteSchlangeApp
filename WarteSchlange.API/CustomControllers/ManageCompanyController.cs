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
    [Route("api/ManageCompany")]
    public class ManageCompanyController : Controller
    {
        MainContext _context;
        QueueHelper queueHelper;

        public ManageCompanyController(MainContext context)
        {
            _context = context;
        }

        [Route("createCompany")]
        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyModel newCompany)
        {
            if (_context.Companies.Where(company => company.Email == newCompany.Email).Count() > 0)
                return BadRequest("Email is already taken");

            if (_context.Companies.Where(company => company.Name == newCompany.Name).Count() > 0)
                return BadRequest("Company name is already taken");

            _context.Add(newCompany);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return BadRequest("Failed to update Database");
            }

            return Ok(newCompany.Id);
        }
    }
}