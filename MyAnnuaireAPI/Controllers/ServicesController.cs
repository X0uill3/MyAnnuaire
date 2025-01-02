using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAnnuaireModel.Context;
using MyAnnuaireModel.Dto;
using Microsoft.AspNetCore.Authorization;
using MyAnnuaireModel.Entities;

namespace MyAnnuaireModel.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly MyAnnuaireContext _context;

        public ServiceController(MyAnnuaireContext context)
        {
            _context = context;
        }

        // GET: api/Services
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetService()
        {
            var Service = await _context.Services
                .Select(Service => new ServiceDto
                {
                    Id = Service.Id,
                    Name = Service.Name
                })
                .ToListAsync();

            return Ok(Service);
        }

        // GET: api/Services/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceDto>> GetService(int id)
        {
            var Service = await _context.Services
                .FirstOrDefaultAsync(a => a.Id == id);

            if (Service == null)
            {
                return NotFound();
            }

            return new ServiceDto
            {
                Id = Service.Id,
                Name = Service.Name
            };
        }


        // GET: api/Services/5
        [HttpGet]
        [Route("search/{Servicename}")]
        public async Task<ActionResult<IEnumerable<ServiceDto>>> GetServiceByName(string Servicename)
        {
            return await _context.Services
                .Where(Service => Service.Name == Servicename)
                .Select(Service => new ServiceDto
                {
                    Id = Service.Id,
                    Name = Service.Name,
                }).ToListAsync();
        }

        // PUT: api/Services/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(int id, Service ServicePUT)
        {
            if (id != ServicePUT.Id)
            {
                return BadRequest();
            }

            var Service = await _context.Services
                .FirstOrDefaultAsync(a => a.Id == id);

            if (ServicePUT == null)
            {
                return NotFound();
            }

            Service.Id = ServicePUT.Id;
            Service.Name = ServicePUT.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
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

        // POST: api/Services
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Service>> PostService(Service ServicePOST)
        {


            var Service = new Service
            {
                Id = ServicePOST.Id,
                Name = ServicePOST.Name
            };



            // Ajout de l'entité à la base de données
            _context.Services.Add(Service);
            await _context.SaveChangesAsync();



            return CreatedAtAction("GetService", new { id = ServicePOST.Id }, ServicePOST);

        }

        // DELETE: api/Services/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            var Service = await _context.Services.FindAsync(id);
            if (Service == null)
            {
                return NotFound();
            }

            _context.Services.Remove(Service);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.Id == id);
        }

    }


}
