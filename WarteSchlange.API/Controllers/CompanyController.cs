using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarteSchlange.API.Models;

namespace WarteSchlange.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Company")]
    public class CompanyController : Controller
    {
        private readonly MainContext _context;

        public CompanyController(MainContext context)
        {
            _context = context;
        }

        // GET: api/Company
        [HttpGet]
        public IEnumerable<CompanyModel> GetCompanies()
        {
            return _context.Companies;
        }

        // GET: api/Company/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var companyModel = await _context.Companies.SingleOrDefaultAsync(m => m.Id == id);

            if (companyModel == null)
            {
                return NotFound();
            }

            return Ok(companyModel);
        }

        // PUT: api/Company/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompanyModel([FromRoute] int id, [FromBody] CompanyModel companyModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != companyModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(companyModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Company
        [HttpPost]
        public async Task<IActionResult> PostCompanyModel([FromBody] CompanyModel companyModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Companies.Add(companyModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompanyModel", new { id = companyModel.Id }, companyModel);
        }

        // DELETE: api/Company/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompanyModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var companyModel = await _context.Companies.SingleOrDefaultAsync(m => m.Id == id);
            if (companyModel == null)
            {
                return NotFound();
            }

            _context.Companies.Remove(companyModel);
            await _context.SaveChangesAsync();

            return Ok(companyModel);
        }

        private bool CompanyModelExists(int id)
        {
            return _context.Companies.Any(e => e.Id == id);
        }
    }
}