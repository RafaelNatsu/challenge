using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Models;

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
        public async Task<ActionResult<IEnumerable<Denylist>>> GetAllDenyList()
        {
            return await _context.Denylists.ToListAsync();
        }
    }
}