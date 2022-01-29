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
    public class MessagesController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public MessagesController(ApplicationDbContext context)
        public MessagesController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Messages
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        public async Task<IActionResult> GetMessages()
        {
            //return await _context.Messages.ToListAsync();
            var Messages = await _unitOfWork.Messages.GetAll(includes: q => q.Include(x => x.Match));
            return Ok(Messages);
        }

        // GET: api/Messages/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Message>> GetMessage(int id)
        public async Task<IActionResult> GetMessage(int id)
        {
            //var Message = await _context.Messages.FindAsync(id);
            var Message = await _unitOfWork.Messages.Get(q => q.Id == id);

            if (Message == null)
            {
                return NotFound();
            }

            //return Message;
            return Ok(Message);
        }

        // PUT: api/Messages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessage(int id, Message Message)
        {
            if (id != Message.Id)
            {
                return BadRequest();
            }

            //_context.Entry(Message).State = EntityState.Modified;
            _unitOfWork.Messages.Update(Message);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!MessageExists(id))
                if (!await MessageExists(id))
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

        // POST: api/Messages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Message>> PostMessage(Message Message)
        {
            //_context.Messages.Add(Message);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Messages.Insert(Message);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetMessage", new { id = Message.Id }, Message);
        }

        // DELETE: api/Messages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            //var Message = await _context.Messages.FindAsync(id);
            var Message = await _unitOfWork.Messages.Get(q => q.Id == id);
            if (Message == null)
            {
                return NotFound();
            }

            //_context.Messages.Remove(Message);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Messages.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool MessageExists(int id)
        private async Task<bool> MessageExists(int id)
        {
            //return _context.Messages.Any(e => e.Id == id);
            var Message = await _unitOfWork.Messages.Get(q => q.Id == id);
            return Message != null;
        }
    }
}
