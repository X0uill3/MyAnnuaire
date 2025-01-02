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
    public class TypeSiegeController : ControllerBase
    {
        private readonly MyAnnuaireContext _context;

        public TypeSiegeController(MyAnnuaireContext context)
        {
            _context = context;
        }

        // GET: api/TypeSieges
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeSiegeDto>>> GetTypeSiege()
        {
            var TypeSiege = await _context.TypeSieges
                .Select(TypeSiege => new TypeSiegeDto
                {
                    Id = TypeSiege.Id,
                    Name = TypeSiege.Name
                })
                .ToListAsync();

            return Ok(TypeSiege);
        }

        // GET: api/TypeSieges/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeSiegeDto>> GetTypeSiege(int id)
        {
            var TypeSiege = await _context.TypeSieges
                .FirstOrDefaultAsync(a => a.Id == id);

            if (TypeSiege == null)
            {
                return NotFound();
            }

            return new TypeSiegeDto
            {
                Id = TypeSiege.Id,
                Name = TypeSiege.Name
            };
        }


        // GET: api/TypeSieges/5
        [HttpGet]
        [Route("search/{TypeSiegename}")]
        public async Task<ActionResult<IEnumerable<TypeSiegeDto>>> GetTypeSiegeByName(string TypeSiegename)
        {
            return await _context.TypeSieges
                .Where(TypeSiege => TypeSiege.Name == TypeSiegename)
                .Select(TypeSiege => new TypeSiegeDto
                {
                    Id = TypeSiege.Id,
                    Name = TypeSiege.Name,
                }).ToListAsync();
        }

        // PUT: api/TypeSieges/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeSiege(int id, TypeSiege TypeSiegePUT)
        {
            if (id != TypeSiegePUT.Id)
            {
                return BadRequest();
            }

            var TypeSiege = await _context.TypeSieges
                .FirstOrDefaultAsync(a => a.Id == id);

            if (TypeSiegePUT == null)
            {
                return NotFound();
            }

            TypeSiege.Id = TypeSiegePUT.Id;
            TypeSiege.Name = TypeSiegePUT.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeSiegeExists(id))
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

        // POST: api/TypeSieges
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypeSiege>> PostTypeSiege(TypeSiege TypeSiegePOST)
        {


            var TypeSiege = new TypeSiege
            {
                Id = TypeSiegePOST.Id,
                Name = TypeSiegePOST.Name
            };



            // Ajout de l'entité à la base de données
            _context.TypeSieges.Add(TypeSiege);
            await _context.SaveChangesAsync();



            return CreatedAtAction("GetTypeSiege", new { id = TypeSiegePOST.Id }, TypeSiegePOST);

        }

        // DELETE: api/TypeSieges/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeSiege(int id)
        {
            var TypeSiege = await _context.TypeSieges.FindAsync(id);
            if (TypeSiege == null)
            {
                return NotFound();
            }

            _context.TypeSieges.Remove(TypeSiege);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TypeSiegeExists(int id)
        {
            return _context.TypeSieges.Any(e => e.Id == id);
        }

    }


}
