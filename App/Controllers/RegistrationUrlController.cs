using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Models;
namespace App.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegistrationUrlController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RegistrationUrlController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/RegistrationUrl
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegistrationUrl>>> GetRegistrationUrls()
        {
            var value = await _context.RegistrationUrls.ToListAsync();
            return Ok(new Response<List<RegistrationUrl>>(value));
        }

        // GET: api/RegistrationUrl/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RegistrationUrl>> Getregistration_url(int id)
        {
            var registration_url = await _context.RegistrationUrls.FindAsync(id);

            if (registration_url == null)
            {
                return NotFound();
            }

            return registration_url;
        }

        // PUT: api/RegistrationUrl/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putregistration_url(int id, RegistrationUrl registration_url)
        {
            if (id != registration_url.Id)
            {
                return BadRequest();
            }

            _context.Entry(registration_url).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!registration_urlExists(id))
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

        // POST: api/RegistrationUrl
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RegistrationUrl>> Postregistration_url(RegistrationUrl registration_url)
        {
            _context.RegistrationUrls.Add(registration_url);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getregistration_url", new { id = registration_url.Id }, registration_url);
        }

        // DELETE: api/RegistrationUrl/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteregistration_url(int id)
        {
            var registration_url = await _context.RegistrationUrls.FindAsync(id);
            if (registration_url == null)
            {
                return NotFound();
            }

            _context.RegistrationUrls.Remove(registration_url);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool registration_urlExists(int id)
        {
            return _context.RegistrationUrls.Any(e => e.Id == id);
        }
    }
}
