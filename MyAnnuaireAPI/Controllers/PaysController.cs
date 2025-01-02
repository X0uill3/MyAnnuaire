using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAnnuaireModel.Context;
using MyAnnuaireModel.Dto;
using MyAnnuaireModel.Entities;
using Microsoft.AspNetCore.Authorization;

namespace MyAnnuaireModel.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PaysController : ControllerBase
    {
        private readonly MyAnnuaireContext _context;

        public PaysController(MyAnnuaireContext context)
        {
            _context = context;
        }

        // GET: api/Payss
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaysDto>>> GetPays()
        {
            var pays = await _context.Pays
                .Select(Pays => new PaysDto
                {
                    Id = Pays.Id,
                    Name = Pays.Name,
                })
                .ToListAsync();

            return Ok(pays);
        }

        // GET: api/Payss/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaysDto>> GetPays(int id)
        {
            var Pays = await _context.Pays
                .FirstOrDefaultAsync(a => a.Id == id);

            if (Pays == null)
            {
                return NotFound();
            }

            return new PaysDto
            {
                Id = Pays.Id,
                Name = Pays.Name,
            };
        }


        // GET: api/Payss/5
        [HttpGet]
        [Route("search/{Paysname}")]
        public async Task<ActionResult<IEnumerable<PaysDto>>> GetPaysByName(string Paysname)
        {
            return await _context.Pays
                .Where(Pays => Pays.Name == Paysname)
                .Select(Pays => new PaysDto
                {
                    Id = Pays.Id,
                    Name = Pays.Name,
                }).ToListAsync();
        }

        // PUT: api/Payss/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPays(int id, Pays PaysPUT)
        {
            if (id != PaysPUT.Id)
            {
                return BadRequest();
            }

            var Pays = await _context.Pays
                .FirstOrDefaultAsync(a => a.Id == id);

            if (PaysPUT == null)
            {
                return NotFound();
            }
            Pays.Name = PaysPUT.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaysExists(id))
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

        // POST: api/Payss
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pays>> PostPays(Pays PaysPOST)
        {


            var Pays = new Pays
            {
                Name = PaysPOST.Name,
            };



            // Ajout de l'entité à la base de données
            _context.Pays.Add(Pays);
            await _context.SaveChangesAsync();



            return CreatedAtAction("GetPays", new { id = PaysPOST.Id }, PaysPOST);

        }

        // DELETE: api/Payss/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePays(int id)
        {
            var Pays = await _context.Pays.FindAsync(id);
            if (Pays == null)
            {
                return NotFound();
            }

            _context.Pays.Remove(Pays);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaysExists(int id)
        {
            return _context.Pays.Any(e => e.Id == id);
        }

    }


}
