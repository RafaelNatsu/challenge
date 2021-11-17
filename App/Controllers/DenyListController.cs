using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Models;
using App.Contracts;

namespace App.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DenyListController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DenyListController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Response<List<Denylist>>>> GetAllDenyList()
        {
            var query = await _context.Denylists
                .FromSqlRaw(
                    "SELECT d.id, l.ipAddress, d.IdRef, d.Inserted FROM DenyList d left join ListUrl l on l.Id = d.IdRef")
                .ToListAsync();
            return new Response<List<Denylist>>(query);
        }
        
        [HttpPost]
        public async Task<ActionResult<Denylist>> Postlist(ListUrl raw)
        {
            // string[] keys = Request.Form.Keys.ToArray();
            // Console.WriteLine(keys);
            // Console.WriteLine(raw);
            var value = new Denylist();
            var get = _context.Lists.Where(x => x.IpAddress == raw.IpAddress).FirstOrDefault();
            // Console.WriteLine(get);
            value.IdRef = get.Id;
            _context.Denylists.Add(value);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAllDenyList", new { id = value.IdRef }, value);
        }
    }
}