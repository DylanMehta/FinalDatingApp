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
    public class PersonsController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public PersonsController(ApplicationDbContext context)
        public PersonsController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Persons
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
        public async Task<IActionResult> GetPersons()
        {
            //return await _context.Persons.ToListAsync();
            var Persons = await _unitOfWork.Persons.GetAll();
            return Ok(Persons);
        }

        // GET: api/Persons/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Person>> GetPerson(int id)
        public async Task<IActionResult> GetPerson(int id)
        {
            //var Person = await _context.Persons.FindAsync(id);
            var Person = await _unitOfWork.Persons.Get(q => q.Id == id);

            if (Person == null)
            {
                return NotFound();
            }

            //return Person;
            return Ok(Person);
        }

        // PUT: api/Persons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Person Person)
        {
            if (id != Person.Id)
            {
                return BadRequest();
            }

            //_context.Entry(Person).State = EntityState.Modified;
            _unitOfWork.Persons.Update(Person);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!PersonExists(id))
                if (!await PersonExists(id))
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

        // POST: api/Persons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person Person)
        {
            //_context.Persons.Add(Person);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Persons.Insert(Person);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetPerson", new { id = Person.Id }, Person);
        }

        // DELETE: api/Persons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            //var Person = await _context.Persons.FindAsync(id);
            var Person = await _unitOfWork.Persons.Get(q => q.Id == id);
            if (Person == null)
            {
                return NotFound();
            }

            //_context.Persons.Remove(Person);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Persons.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool PersonExists(int id)
        private async Task<bool> PersonExists(int id)
        {
            //return _context.Persons.Any(e => e.Id == id);
            var Person = await _unitOfWork.Persons.Get(q => q.Id == id);
            return Person != null;
        }
    }
}
