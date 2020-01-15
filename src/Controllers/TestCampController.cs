using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreCodeCamp.Data;

namespace CoreCodeCamp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestCampController : ControllerBase
    {
        private readonly CampContext _context;

        public TestCampController(CampContext context)
        {
            _context = context;
        }

        // GET: api/TestCamp
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Camp>>> GetCamps()
        {
            return await _context.Camps.ToListAsync();
        }

        // GET: api/TestCamp/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Camp>> GetCamp(int id)
        {
            var camp = await _context.Camps.FindAsync(id);

            if (camp == null)
            {
                return NotFound();
            }

            return camp;
        }

        // PUT: api/TestCamp/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCamp(int id, Camp camp)
        {
            if (id != camp.CampId)
            {
                return BadRequest();
            }

            _context.Entry(camp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CampExists(id))
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

        // POST: api/TestCamp
        [HttpPost]
        public async Task<ActionResult<Camp>> PostCamp(Camp camp)
        {
            _context.Camps.Add(camp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCamp", new { id = camp.CampId }, camp);
        }

        // DELETE: api/TestCamp/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Camp>> DeleteCamp(int id)
        {
            var camp = await _context.Camps.FindAsync(id);
            if (camp == null)
            {
                return NotFound();
            }

            _context.Camps.Remove(camp);
            await _context.SaveChangesAsync();

            return camp;
        }

        private bool CampExists(int id)
        {
            return _context.Camps.Any(e => e.CampId == id);
        }
    }
}
