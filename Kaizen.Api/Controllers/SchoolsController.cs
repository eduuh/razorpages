using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kaizen.DataAccess;
using Kaizen.Models;
using Kaizen.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UploadandDowloadService.Services;

namespace uploaddownloadfiles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper mapper;
        private readonly IUser user;

        public SchoolsController(AppDbContext context, IMapper mapper, IUser user)
        {
            _context = context;
            this.mapper = mapper;
            this.user = user;
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

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchool(string id, SchoolDto schooldto)
        {
            if (id == null)
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

        [HttpPost]
        // [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<ActionResult<School>> PostSchool(SchoolDto schooldto)
        {

            var @me = await user.GetCurrentLoginDetails();
            var school = new School()
            {
                Name = schooldto.Name,
                Motto = schooldto.Motto,
                Contact = new Contact
                {
                    Email = schooldto.Email,
                    Address = schooldto.Address,
                    Region = schooldto.Region,
                    Pobox = schooldto.Pobox,
                }
                ,
                Stakeholders = new List<AppUser>(){
                  @me
                }
            };

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

