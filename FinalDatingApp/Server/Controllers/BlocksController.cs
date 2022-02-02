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
    public class BlocksController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public BlocksController(ApplicationDbContext context)
        public BlocksController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Blocks
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Block>>> GetBlocks()
        public async Task<IActionResult> GetBlocks()
        {
            //return await _context.Blocks.ToListAsync();
            var Blocks = await _unitOfWork.Blocks.GetAll(includes: q => q.Include(x => x.BlockerPerson).Include(x => x.BlockedPerson));
            return Ok(Blocks);
        }

        // GET: api/Blocks/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Block>> GetBlock(int id)
        public async Task<IActionResult> GetBlock(int id)
        {
            //var Block = await _context.Blocks.FindAsync(id);
            var Block = await _unitOfWork.Blocks.Get(q => q.Id == id);

            if (Block == null)
            {
                return NotFound();
            }

            //return Block;
            return Ok(Block);
        }

        // PUT: api/Blocks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlock(int id, Block Block)
        {
            if (id != Block.Id)
            {
                return BadRequest();
            }

            //_context.Entry(Block).State = EntityState.Modified;
            _unitOfWork.Blocks.Update(Block);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!BlockExists(id))
                if (!await BlockExists(id))
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

        // POST: api/Blocks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Block>> PostBlock(Block Block)
        {
            //_context.Blocks.Add(Block);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Blocks.Insert(Block);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetBlock", new { id = Block.Id }, Block);
        }

        // DELETE: api/Blocks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlock(int id)
        {
            //var Block = await _context.Blocks.FindAsync(id);
            var Block = await _unitOfWork.Blocks.Get(q => q.Id == id);
            if (Block == null)
            {
                return NotFound();
            }

            //_context.Blocks.Remove(Block);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Blocks.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool BlockExists(int id)
        private async Task<bool> BlockExists(int id)
        {
            //return _context.Blocks.Any(e => e.Id == id);
            var Block = await _unitOfWork.Blocks.Get(q => q.Id == id);
            return Block != null;
        }
    }
}