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
    [Route("api/Images")]
    public class ImagesController : Controller
    {
        private readonly MainContext _context;

        public ImagesController(MainContext context)
        {
            _context = context;
        }

        // GET: api/Images
        [HttpGet]
        public IEnumerable<ImagesModel> GetImages()
        {
            return _context.Images;
        }

        // GET: api/Images/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetImagesModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var imagesModel = await _context.Images.SingleOrDefaultAsync(m => m.Id == id);

            if (imagesModel == null)
            {
                return NotFound();
            }

            return Ok(imagesModel);
        }

        // PUT: api/Images/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImagesModel([FromRoute] int id, [FromBody] ImagesModel imagesModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != imagesModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(imagesModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImagesModelExists(id))
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

        // POST: api/Images
        [HttpPost]
        public async Task<IActionResult> PostImagesModel([FromBody] ImagesModel imagesModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Images.Add(imagesModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImagesModel", new { id = imagesModel.Id }, imagesModel);
        }

        // DELETE: api/Images/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImagesModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var imagesModel = await _context.Images.SingleOrDefaultAsync(m => m.Id == id);
            if (imagesModel == null)
            {
                return NotFound();
            }

            _context.Images.Remove(imagesModel);
            await _context.SaveChangesAsync();

            return Ok(imagesModel);
        }

        private bool ImagesModelExists(int id)
        {
            return _context.Images.Any(e => e.Id == id);
        }
    }
}