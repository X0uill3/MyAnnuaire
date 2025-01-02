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
    public class SiegeController : ControllerBase
    {
        private readonly MyAnnuaireContext _context;

        public SiegeController(MyAnnuaireContext context)
        {
            _context = context;
        }

        // GET: api/Sieges
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SiegeDto>>> GetSiege()
        {
            var Siege = await _context.Sieges
                .Include(a => a.Ville)
                .Include(a => a.TypeSiege)
                .Select(Siege => new SiegeDto
                {
                    Id = Siege.Id,
                    VilleId = Siege.VilleId,
                    Ville = Siege.Ville.Name,
                    TypeSiegeId = Siege.TypeSiegeId,
                    TypeSiege = Siege.TypeSiege.Name,
                })
                .ToListAsync();

            return Ok(Siege);
        }

        // GET: api/Sieges/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SiegeDto>> GetSiege(int id)
        {
            var Siege = await _context.Sieges
                .Include(a => a.Ville)
                .Include(a => a.TypeSiege)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (Siege == null)
            {
                return NotFound();
            }

            return new SiegeDto
            {
                Id = Siege.Id,
                VilleId = Siege.VilleId,
                Ville = Siege.Ville.Name,
                TypeSiegeId = Siege.TypeSiegeId,
                TypeSiege = Siege.TypeSiege.Name,
            };
        }


        // GET: api/Sieges/5
        [HttpGet]
        [Route("search/{Siegename}")]
        public async Task<ActionResult<IEnumerable<SiegeDto>>> GetSiegeByName(string Siegename)
        {
            return await _context.Sieges
                .Include(a => a.Ville)
                .Include(a => a.TypeSiege)
                .Where(Siege => Siege.Ville.Name == Siegename)
                .Select(Siege => new SiegeDto
                {
                    Id = Siege.Id,
                    VilleId = Siege.VilleId,
                    Ville = Siege.Ville.Name,
                    TypeSiegeId = Siege.TypeSiegeId,
                    TypeSiege = Siege.TypeSiege.Name,
                }).ToListAsync();
        }

        // PUT: api/Sieges/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSiege(int id, Siege SiegePUT)
        {
            if (id != SiegePUT.Id)
            {
                return BadRequest();
            }

            var Siege = await _context.Sieges
                .Include(a => a.Ville)
                .Include(a => a.TypeSiege)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (SiegePUT == null)
            {
                return NotFound();
            }

            Siege.Id = SiegePUT.Id;
            Siege.VilleId = SiegePUT.VilleId;
            Siege.Ville = SiegePUT.Ville;
            Siege.TypeSiegeId = SiegePUT.TypeSiegeId;
            Siege.TypeSiege = SiegePUT.TypeSiege;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SiegeExists(id))
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

        // POST: api/Sieges
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Siege>> PostSiege(Siege SiegePOST)
        {


            var Siege = new Siege
            {
                Id = SiegePOST.Id,
                VilleId = SiegePOST.VilleId,
                Ville = SiegePOST.Ville,
                TypeSiegeId = SiegePOST.TypeSiegeId,
                TypeSiege = SiegePOST.TypeSiege,
            };



            // Ajout de l'entité à la base de données
            _context.Sieges.Add(Siege);
            await _context.SaveChangesAsync();



            return CreatedAtAction("GetSiege", new { id = SiegePOST.Id }, SiegePOST);

        }

        // DELETE: api/Sieges/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSiege(int id)
        {
            var Siege = await _context.Sieges.FindAsync(id);
            if (Siege == null)
            {
                return NotFound();
            }

            _context.Sieges.Remove(Siege);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SiegeExists(int id)
        {
            return _context.Sieges.Any(e => e.Id == id);
        }

    }


}
