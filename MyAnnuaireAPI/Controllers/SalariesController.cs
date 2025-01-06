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
    public class SalariesController : ControllerBase
    {
        private readonly MyAnnuaireContext _context;

        public SalariesController(MyAnnuaireContext context)
        {
            _context = context;
        }

        // GET: api/Salaries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalarieDto>>> GetSalaries()
        {
            var Salaries = await _context.Salaries
                .Include(a => a.Siege)
                .Include(a => a.Service)
                .OrderBy(a => a.Id)
                .Select(Salarie => new SalarieDto
                {
                    Id = Salarie.Id,
                    Nom = Salarie.Nom,
                    Prenom = Salarie.Prenom,
                    Email = Salarie.Email,
                    TelephoneFixe = Salarie.TelephoneFixe,
                    TelephonePortable = Salarie.TelephonePortable,
                    Service = Salarie.Service.Name,
                    ServiceId = Salarie.Service.Id,
                    Siege = Salarie.Siege.Ville.Name,
                    SiegeId = Salarie.Siege.Id,
                })
                .ToListAsync();

            return Ok(Salaries);
        }

        // GET: api/Salaries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalarieDto>> GetSalarie(int id)
        {
            var Salarie = await _context.Salaries
                .Include(a => a.Siege)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (Salarie == null)
            {
                return NotFound();
            }

            return new SalarieDto
            {
                Id = Salarie.Id,
                Nom = Salarie.Nom,
                Prenom = Salarie.Prenom,
                Email = Salarie.Email,
                TelephoneFixe = Salarie.TelephoneFixe,
                TelephonePortable = Salarie.TelephonePortable,
                Service = Salarie.Service.Name,
                Siege = Salarie.Siege.Ville.Name,
            };
        }


        // GET: api/Salaries/search/{Salariename}
        [HttpGet]
        [Route("search/{Salariename}")]
        public async Task<ActionResult<IEnumerable<SalarieDto>>> GetSalarieByName(string Salariename)
        {
            return await _context.Salaries
                .Include(s => s.Service)
                .Include(s => s.Siege)
                .ThenInclude(s => s.Ville)
                .Where(s => EF.Functions.Like(s.Nom, $"%{Salariename}%") ||
                            EF.Functions.Like(s.Service.Name, $"%{Salariename}%") ||
                            EF.Functions.Like(s.Siege.Ville.Name, $"%{Salariename}%"))
                .Select(s => new SalarieDto
                {
                    Id = s.Id,
                    Nom = s.Nom,
                    Prenom = s.Prenom,
                    Email = s.Email,
                    TelephoneFixe = s.TelephoneFixe,
                    TelephonePortable = s.TelephonePortable,
                    Service = s.Service.Name,
                    ServiceId = s.Service.Id,
                    Siege = s.Siege.Ville.Name,
                    SiegeId = s.Siege.Id,
                }).ToListAsync();
        }

        // PUT: api/Salaries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalarie(int id, Salarie SalariePUT)
        {
            if (id != SalariePUT.Id)
            {
                return BadRequest();
            }

            var Salarie = await _context.Salaries
                .Include(a => a.Siege)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (SalariePUT == null)
            {
                return NotFound();
            }
            Salarie.Nom = SalariePUT.Nom;
            Salarie.Prenom = SalariePUT.Prenom;
            Salarie.Email = SalariePUT.Email;
            Salarie.TelephoneFixe = SalariePUT.TelephoneFixe;
            Salarie.TelephonePortable = SalariePUT.TelephonePortable;
            Salarie.ServiceId = SalariePUT.ServiceId;
            Salarie.SiegeId = SalariePUT.SiegeId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalarieExists(id))
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

        // POST: api/Salaries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Salarie>> PostSalarie(Salarie SalariePOST)
        {


            var Salarie = new Salarie
            {
                Nom = SalariePOST.Nom,
                Prenom = SalariePOST.Prenom,
                Email = SalariePOST.Email,
                TelephoneFixe = SalariePOST.TelephoneFixe,
                TelephonePortable = SalariePOST.TelephonePortable,
                ServiceId = SalariePOST.ServiceId,
                SiegeId = SalariePOST.SiegeId,
            };



            // Ajout de l'entité à la base de données
            _context.Salaries.Add(Salarie);
            await _context.SaveChangesAsync();



            return CreatedAtAction("GetSalarie", new { id = SalariePOST.Id }, SalariePOST);

        }

        // DELETE: api/Salaries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalarie(int id)
        {
            var Salarie = await _context.Salaries.FindAsync(id);
            if (Salarie == null)
            {
                return NotFound();
            }

            _context.Salaries.Remove(Salarie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SalarieExists(int id)
        {
            return _context.Salaries.Any(e => e.Id == id);
        }

    }


}
