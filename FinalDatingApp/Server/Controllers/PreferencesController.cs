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
    public class PreferencesController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public PreferencesController(ApplicationDbContext context)
        public PreferencesController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Preferences
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Preference>>> GetPreferences()
        public async Task<IActionResult> GetPreferences()
        {
            //return await _context.Preferences.ToListAsync();
            var Preferences = await _unitOfWork.Preferences.GetAll();
            return Ok(Preferences);
        }

        // GET: api/Preferences/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Preference>> GetPreference(int id)
        public async Task<IActionResult> GetPreference(int id)
        {
            //var Preference = await _context.Preferences.FindAsync(id);
            var Preference = await _unitOfWork.Preferences.Get(q => q.Id == id);

            if (Preference == null)
            {
                return NotFound();
            }

            //return Preference;
            return Ok(Preference);
        }

        // PUT: api/Preferences/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPreference(int id, Preference Preference)
        {
            if (id != Preference.Id)
            {
                return BadRequest();
            }

            //_context.Entry(Preference).State = EntityState.Modified;
            _unitOfWork.Preferences.Update(Preference);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!PreferenceExists(id))
                if (!await PreferenceExists(id))
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

        // POST: api/Preferences
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Preference>> PostPreference(Preference Preference)
        {
            //_context.Preferences.Add(Preference);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Preferences.Insert(Preference);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetPreference", new { id = Preference.Id }, Preference);
        }

        // DELETE: api/Preferences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePreference(int id)
        {
            //var Preference = await _context.Preferences.FindAsync(id);
            var Preference = await _unitOfWork.Preferences.Get(q => q.Id == id);
            if (Preference == null)
            {
                return NotFound();
            }

            //_context.Preferences.Remove(Preference);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Preferences.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool PreferenceExists(int id)
        private async Task<bool> PreferenceExists(int id)
        {
            //return _context.Preferences.Any(e => e.Id == id);
            var Preference = await _unitOfWork.Preferences.Get(q => q.Id == id);
            return Preference != null;
        }
    }
}
