using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UploadandDowloadService.Areas.Identity;
using UploadandDowloadService.Dto;
using UploadandDowloadService.Models;

namespace uploaddownloadfiles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper mapper;

        public SchoolsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Schools
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SchoolDto>>> GetSchools()
        {
            var schools = await _context.Schools.ToListAsync();
            return mapper.Map<List<School>, List<SchoolDto>>(schools);
        }

        // GET: api/Schools/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolDto>> GetSchool(string id)
        {
            var school = await _context.Schools.FindAsync(id);

            if (school == null)
            {
                return NotFound();
            }

            return mapper.Map<School, SchoolDto>(school);
        }

        // PUT: api/Schools/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchool(string id, SchoolDto schooldto)
        {
            if (id != schooldto.Id)
            {
                return BadRequest();
            }
            var school = mapper.Map<SchoolDto, School>(schooldto);
            _context.Entry(school).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchoolExists(id))
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

        // POST: api/Schools
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<School>> PostSchool(SchoolDto schooldto)
        {

            var school = mapper.Map<SchoolDto, School>(schooldto);
            _context.Schools.Add(school);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SchoolExists(school.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSchool", new { id = school.Id }, school);
        }

        // DELETE: api/Schools/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchool(string id)
        {
            var school = await _context.Schools.FindAsync(id);
            if (school == null)
            {
                return NotFound();
            }

            _context.Schools.Remove(school);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SchoolExists(string id)
        {
            return _context.Schools.Any(e => e.Id == id);
        }
    }
}

