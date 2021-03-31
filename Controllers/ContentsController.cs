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
    public class ContentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper mapper;

        public ContentsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Contents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContentDto>>> GetContents()
        {
            var content = await _context.Contents.ToListAsync();
            return mapper.Map<List<Content>, List<ContentDto>>(content);
        }

        // GET: api/Contents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContentDto>> GetContent(string id)
        {
            var content = await _context.Contents.FindAsync(id);

            if (content == null)
            {
                return NotFound();
            }
            return mapper.Map<Content, ContentDto>(content);
        }

        // PUT: api/Contents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContent(string id, ContentDto contentdto)
        {
            var content = mapper.Map<ContentDto, Content>(contentdto);
            if (id != content.Id)
            {
                return BadRequest();
            }

            _context.Entry(content).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContentExists(id))
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

        // POST: api/Contents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContentDto>> PostContent(ContentDto contentdto)
        {
            var content = mapper.Map<ContentDto, Content>(contentdto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ContentExists(content.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetContent", new { id = content.Id }, content);
        }

        // DELETE: api/Contents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContent(string id)
        {
            var content = await _context.Contents.FindAsync(id);
            if (content == null)
            {
                return NotFound();
            }

            _context.Contents.Remove(content);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContentExists(string id)
        {
            return _context.Contents.Any(e => e.Id == id);
        }
    }
}
