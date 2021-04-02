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
    public class TrainingSubjectsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper mapper;

        public TrainingSubjectsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/TrainingSubjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrainingContentDto>>> GetTrainingContents()
        {
            var traincontent = await _context.TrainingContents.ToListAsync();
            return mapper.Map<List<TrainingSubject>, List<TrainingContentDto>>(traincontent);
        }

        // GET: api/TrainingSubjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrainingContentDto>> GetTrainingSubject([FromQuery] string id)
        {
            var trainingSubject = await _context.TrainingContents.FindAsync(id);

            if (trainingSubject == null)
            {
                return NotFound();
            }

            return mapper.Map<TrainingSubject, TrainingContentDto>(trainingSubject);
        }

        // PUT: api/TrainingSubjects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainingSubject([FromQuery] string id, [FromBody] TrainingContentDto trainingSubject)
        {
            if (id != trainingSubject.Id)
            {
                return BadRequest();
            }

            var mapped = mapper.Map<TrainingContentDto, TrainingSubject>(trainingSubject);

            _context.Entry(mapped).State = EntityState.Modified;

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
        public async Task<ActionResult<TrainingSubject>> PostTrainingSubject([FromBody] TrainingContentDto trainingSubject)
        {
            var trainsub = mapper.Map<TrainingContentDto, TrainingSubject>(trainingSubject);
            _context.TrainingContents.Add(trainsub);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TrainingSubjectExists(trainsub.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTrainingSubject", new { id = trainsub.Id }, trainingSubject);
        }

        // DELETE: api/TrainingSubjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainingSubject([FromQuery] string id)
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
