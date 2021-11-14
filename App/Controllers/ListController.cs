using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Models;
using App.Contracts;
using App.Services;
using App.Helpers;
namespace App.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IUriService _uriService;

        public ListController(AppDbContext context, IUriService uriService)
        {
            _context = context;
            _uriService = uriService;
        }

        // GET: api/List
        [HttpGet]
        public async Task<ActionResult<List<list>>> GetLists([FromQuery] PaginationFilter filter )
        {
            string route = Request.Path.Value;
            PaginationFilter validFilter = new PaginationFilter(filter.PageNumber,filter.PageSize);
            var pageDate = await _context.Lists
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            var totalRecords = await _context.Lists.CountAsync();
            var pageResponse = PaginationHelper.CreatePagedReponse<list>(pageDate,validFilter,totalRecords,_uriService,route);
            return Ok(pageResponse);
        }
        
        // [HttpGet("/teste/")]
        // public async Task<IActionResult> testeQuery([FromQuery] PaginationFilter filter )
        // {
        //     string route = Request.Path.Value;
        //     PaginationFilter validFilter = new PaginationFilter(filter.PageNumber,filter.PageSize);
        //     var pageDate = await _context.Lists
        //                                 .Skip((validFilter.PageNumber - 1) * validFilter.PageNumber)
        //                                 .Take(validFilter.PageSize)
        //                                 .OrderBy(l => l.id)
        //                                 .ToListAsync();
        //     var totalRecords = await _context.Lists.CountAsync();
        //     var pageResponse = PaginationHelper.CreatePagedReponse<list>(pageDate,validFilter,totalRecords,_uriService,route);
        //     return Ok(pageResponse);
        // }
        // GET: api/List/5
        [HttpGet("{id}")]
        public async Task<ActionResult<list>> Getlist(int id)
        {
            var route = Request.Path.Value;
            var list = await _context.Lists.Where(l => l.id == id).FirstOrDefaultAsync();

            if (list == null)
            {
                return NotFound();
            }

            return Ok(new Response<list>(list));
        }

        // PUT: api/List/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putlist(int id, list list)
        {
            if (id != list.id)
            {
                return BadRequest();
            }

            _context.Entry(list).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!listExists(id))
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

        // POST: api/List
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<list>> Postlist(list list)
        {
            _context.Lists.Add(list);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getlist", new { id = list.id }, list);
        }

        // DELETE: api/List/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletelist(int id)
        {
            var list = await _context.Lists.FindAsync(id);
            if (list == null)
            {
                return NotFound();
            }

            _context.Lists.Remove(list);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool listExists(int id)
        {
            return _context.Lists.Any(e => e.id == id);
        }
    }
}
