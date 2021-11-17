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
    public class ListUrlController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IUriService _uriService;

        public ListUrlController(AppDbContext context, IUriService uriService)
        {
            _context = context;
            _uriService = uriService;
        }

        // GET: api/List
        [HttpGet]
        public async Task<ActionResult<List<ListUrl>>> GetLists([FromQuery] PaginationFilter filter )
        {
            string route = Request.Path.Value;
            PaginationFilter validFilter = new PaginationFilter(filter.PageNumber,filter.PageSize);
            int offset = (validFilter.PageNumber - 1) * validFilter.PageSize;
            var query = await _context.Lists
                .FromSqlRaw(
                    $"SELECT * FROM ListUrl li LIMIT { validFilter.PageSize.ToString()} OFFSET {offset.ToString()}")
                .ToListAsync();
            // var pageDate = await _context.Lists
            //     .OrderBy(x => x.Id)
            //     .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            //     .Take(validFilter.PageSize)
            //     .ToListAsync();
            var totalRecords = await _context.Lists.FromSqlRaw("SELECT li.Id FROM ListUrl li WHERE li.Id NOT  IN ( select IdRef from DenyList )").CountAsync();
            var pageResponse = PaginationHelper.CreatePagedReponse<ListUrl>(query,validFilter,totalRecords,_uriService,route);
            return Ok(pageResponse);
        }
        
        [HttpGet("deny/")]
        public async Task<ActionResult<List<ListUrl>>> GetListsDeny([FromQuery] PaginationFilter filter )
        {
            string route = Request.Path.Value;
            PaginationFilter validFilter = new PaginationFilter(filter.PageNumber,filter.PageSize);
            int offset = (validFilter.PageNumber - 1) * validFilter.PageSize;
            var query = await _context.Lists
                .FromSqlRaw(
                    $"SELECT * FROM ListUrl li WHERE li.Id NOT  IN  ( select IdRef from DenyList ) LIMIT { validFilter.PageSize.ToString()} OFFSET {offset.ToString()}")
                .ToListAsync();
            // var pageDate = await _context.Lists
            //     .OrderBy(x => x.Id)
            //     .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            //     .Take(validFilter.PageSize)
            //     .ToListAsync();
            var totalRecords = await _context.Lists.FromSqlRaw("SELECT li.Id FROM ListUrl li WHERE li.Id NOT  IN ( select IdRef from DenyList )").CountAsync();
            var pageResponse = PaginationHelper.CreatePagedReponse<ListUrl>(query,validFilter,totalRecords,_uriService,route);
            return Ok(pageResponse);
        }
        
        // GET: api/List/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ListUrl>> Getlist(int id)
        {
            var route = Request.Path.Value;
            var list = await _context.Lists.Where(l => l.Id == id).FirstOrDefaultAsync();

            if (list == null)
            {
                return NotFound();
            }

            return Ok(new Response<ListUrl>(list));
        }

        // PUT: api/List/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putlist(int id, ListUrl listUrl)
        {
            if (id != listUrl.Id)
            {
                return BadRequest();
            }

            _context.Entry(listUrl).State = EntityState.Modified;

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
        public async Task<ActionResult<ListUrl>> Postlist(ListUrl listUrl)
        {
            _context.Lists.Add(listUrl);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getlist", new { id = listUrl.Id }, listUrl);
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
            return _context.Lists.Any(e => e.Id == id);
        }
    }
}
