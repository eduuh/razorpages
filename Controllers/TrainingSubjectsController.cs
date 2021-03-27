using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UploadandDowloadService.Areas.Identity;
using UploadandDowloadService.Models;

namespace uploaddownloadfiles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingSubjectsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TrainingSubjectsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TrainingSubjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingSubject>>> GetTrainingContents()
        {
            return await _context.TrainingContents.ToListAsync();
        }

        // GET: api/TrainingSubjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingSubject>> GetTrainingSubject([FromQuery]string id)
        {
            var trainingSubject = await _context.TrainingContents.FindAsync(id);

            if (trainingSubject == null)
            {
                return NotFound();
            }

            return trainingSubject;
        }

        // PUT: api/TrainingSubjects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainingSubject([FromQuery]string id, [FromBody]TrainingSubject trainingSubject)
        {
            if (id != trainingSubject.Id)
            {
                return BadRequest();
            }

            _context.Entry(trainingSubject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingSubjectExists(id))
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

        // POST: api/TrainingSubjects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TrainingSubject>> PostTrainingSubject([FromBody]TrainingSubject trainingSubject)
        {
            _context.TrainingContents.Add(trainingSubject);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TrainingSubjectExists(trainingSubject.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTrainingSubject", new { id = trainingSubject.Id }, trainingSubject);
        }

        // DELETE: api/TrainingSubjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainingSubject([FromQuery]string id)
        {
            var trainingSubject = await _context.TrainingContents.FindAsync(id);
            if (trainingSubject == null)
            {
                return NotFound();
            }

            _context.TrainingContents.Remove(trainingSubject);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrainingSubjectExists(string id)
        {
            return _context.TrainingContents.Any(e => e.Id == id);
        }
    }
}
