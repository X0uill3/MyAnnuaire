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
    public class VilleController : ControllerBase
    {
        private readonly MyAnnuaireContext _context;

        public VilleController(MyAnnuaireContext context)
        {
            _context = context;
        }

        // GET: api/Villes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VilleDto>>> GetVille()
        {
            var Ville = await _context.Villes
                .Include(a => a.Pays)
                .Select(Ville => new VilleDto
                {
                    Id = Ville.Id,
                    Name = Ville.Name,
                    Pays = Ville.Pays.Name,
                    PaysId = Ville.PaysId,
                })
                .ToListAsync();

            return Ok(Ville);
        }

        // GET: api/Villes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VilleDto>> GetVille(int id)
        {
            var Ville = await _context.Villes.Include(a => a.Pays)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (Ville == null)
            {
                return NotFound();
            }

            return new VilleDto
            {
                Id = Ville.Id,
                Name = Ville.Name,
                Pays = Ville.Pays.Name,
                PaysId = Ville.PaysId,
            };
        }


        // GET: api/Villes/5
        [HttpGet]
        [Route("search/{Villename}")]
        public async Task<ActionResult<IEnumerable<VilleDto>>> GetVilleByName(string Villename)
        {
            return await _context.Villes
                .Include(a => a.Pays)
                .Where(Ville => Ville.Name == Villename)
                .Select(Ville => new VilleDto
                {
                    Id = Ville.Id,
                    Name = Ville.Name,
                    Pays = Ville.Pays.Name,
                    PaysId = Ville.PaysId,
                }).ToListAsync();
        }

        // PUT: api/Villes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVille(int id, Ville VillePUT)
        {
            if (id != VillePUT.Id)
            {
                return BadRequest();
            }

            var Ville = await _context.Villes
                .FirstOrDefaultAsync(a => a.Id == id);

            if (VillePUT == null)
            {
                return NotFound();
            }
            Ville.Name = VillePUT.Name;
            Ville.PaysId = VillePUT.PaysId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VilleExists(id))
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

        // POST: api/Villes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ville>> PostVille(Ville VillePOST)
        {


            var Ville = new Ville
            {
                Name = VillePOST.Name,
                PaysId = VillePOST.PaysId,
            };



            // Ajout de l'entité à la base de données
            _context.Villes.Add(Ville);
            await _context.SaveChangesAsync();



            return CreatedAtAction("GetVille", new { id = VillePOST.Id }, VillePOST);

        }

        // DELETE: api/Villes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVille(int id)
        {
            var Ville = await _context.Villes.FindAsync(id);
            if (Ville == null)
            {
                return NotFound();
            }

            _context.Villes.Remove(Ville);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VilleExists(int id)
        {
            return _context.Villes.Any(e => e.Id == id);
        }

    }


}
