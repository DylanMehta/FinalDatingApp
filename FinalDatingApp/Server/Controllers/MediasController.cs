using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalDatingApp.Server.Data;
using FinalDatingApp.Shared.Domain;
using FinalDatingApp.Server.IRepository;

namespace FinalDatingApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediasController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public MediasController(ApplicationDbContext context)
        public MediasController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Medias
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Media>>> GetMedias()
        public async Task<IActionResult> GetMedias()
        {
            //return await _context.Medias.ToListAsync();
            var Medias = await _unitOfWork.Medias.GetAll(includes: q => q.Include(x => x.User));
            return Ok(Medias);
        }

        // GET: api/Medias/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Media>> GetMedia(int id)
        public async Task<IActionResult> GetMedia(int id)
        {
            //var Media = await _context.Medias.FindAsync(id);
            var Media = await _unitOfWork.Medias.Get(q => q.Id == id);

            if (Media == null)
            {
                return NotFound();
            }

            //return Media;
            return Ok(Media);
        }

        // PUT: api/Medias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedia(int id, Media Media)
        {
            if (id != Media.Id)
            {
                return BadRequest();
            }

            //_context.Entry(Media).State = EntityState.Modified;
            _unitOfWork.Medias.Update(Media);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!MediaExists(id))
                if (!await MediaExists(id))
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

        // POST: api/Medias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Media>> PostMedia(Media Media)
        {
            //_context.Medias.Add(Media);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Medias.Insert(Media);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetMedia", new { id = Media.Id }, Media);
        }

        // DELETE: api/Medias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedia(int id)
        {
            //var Media = await _context.Medias.FindAsync(id);
            var Media = await _unitOfWork.Medias.Get(q => q.Id == id);
            if (Media == null)
            {
                return NotFound();
            }

            //_context.Medias.Remove(Media);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Medias.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool MediaExists(int id)
        private async Task<bool> MediaExists(int id)
        {
            //return _context.Medias.Any(e => e.Id == id);
            var Media = await _unitOfWork.Medias.Get(q => q.Id == id);
            return Media != null;
        }
    }
}
