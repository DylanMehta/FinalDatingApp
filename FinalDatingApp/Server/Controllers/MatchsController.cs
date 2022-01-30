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
    public class MatchsController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public MatchsController(ApplicationDbContext context)
        public MatchsController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Matchs
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Match>>> GetMatchs()
        public async Task<IActionResult> GetMatchs()
        {
            //return await _context.Matchs.ToListAsync();
            var Matchs = await _unitOfWork.Matchs.GetAll(includes: q => q.Include(x => x.FirstPerson).Include(x => x.SecondPerson));
            return Ok(Matchs);
        }

        // GET: api/Matchs/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Match>> GetMatch(int id)
        public async Task<IActionResult> GetMatch(int id)
        {
            //var Match = await _context.Matchs.FindAsync(id);
            var Match = await _unitOfWork.Matchs.Get(q => q.Id == id);

            if (Match == null)
            {
                return NotFound();
            }

            //return Match;
            return Ok(Match);
        }

        // PUT: api/Matchs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatch(int id, Match Match)
        {
            if (id != Match.Id)
            {
                return BadRequest();
            }

            //_context.Entry(Match).State = EntityState.Modified;
            _unitOfWork.Matchs.Update(Match);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!MatchExists(id))
                if (!await MatchExists(id))
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

        // POST: api/Matchs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Match>> PostMatch(Match Match)
        {
            //_context.Matchs.Add(Match);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Matchs.Insert(Match);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetMatch", new { id = Match.Id }, Match);
        }

        // DELETE: api/Matchs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatch(int id)
        {
            //var Match = await _context.Matchs.FindAsync(id);
            var Match = await _unitOfWork.Matchs.Get(q => q.Id == id);
            if (Match == null)
            {
                return NotFound();
            }

            //_context.Matchs.Remove(Match);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Matchs.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool MatchExists(int id)
        private async Task<bool> MatchExists(int id)
        {
            //return _context.Matchs.Any(e => e.Id == id);
            var Match = await _unitOfWork.Matchs.Get(q => q.Id == id);
            return Match != null;
        }
    }
}
