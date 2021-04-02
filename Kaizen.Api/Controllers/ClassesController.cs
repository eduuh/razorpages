using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.DataAccess;
using Kaizen.Models;
using Kaizen.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace uploaddownloadfiles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper mapper;

        public ClassesController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Classes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClassDto>>> GetClasses()
        {
            var classes = await _context.Classes.ToListAsync();
            return mapper.Map<List<Class>, List<ClassDto>>(classes);
        }

        // GET: api/Classes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClassDto>> GetClass(string id)
        {
            var @class = await _context.Classes.FindAsync(id);

            if (@class == null)
            {
                return NotFound();
            }

            return mapper.Map<Class, ClassDto>(@class);
        }

        // PUT: api/Classes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClass(string id, ClassDto @class)
        {
            if (id != @class.Id)
            {
                return BadRequest();
            }

            var @classmapped = mapper.Map<ClassDto, Class>(@class);
            _context.Entry(@classmapped).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassExists(id))
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

        // POST: api/Classes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Class>> PostClass(ClassDto @class)
        {

            var classedmapped = mapper.Map<ClassDto, Class>(@class);
            _context.Classes.Add(classedmapped);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClassExists(@class.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetClass", new { id = @class.Id }, classedmapped);
        }

        // DELETE: api/Classes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(string id)
        {
            var @class = await _context.Classes.FindAsync(id);
            if (@class == null)
            {
                return NotFound();
            }

            _context.Classes.Remove(@class);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool ClassExists(string id)
        {
            return _context.Classes.Any(e => e.Id == id);
        }
    }
}
