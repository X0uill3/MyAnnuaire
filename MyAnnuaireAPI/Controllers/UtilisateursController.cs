using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAnnuaireModel.Context;
using MyAnnuaireModel.Dto;
using MyAnnuaireModel.Entities;
using Microsoft.AspNetCore.Authorization;

namespace MyAnnuaireAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UtilisateursController : ControllerBase
    {
        private readonly MyAnnuaireContext _context;

        public UtilisateursController(MyAnnuaireContext context)
        {
            _context = context;
        }

        // GET: api/Utilisateurs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UtilisateurDto>>> GetUtilisateurs()
        {
            var users = await _context.Utilisateurs
                .Select(user => new UtilisateurDto
                {
                    Id = user.Id,
                    login = user.login,
                    password = user.password,
                    EstAdmin = user.EstAdmin,
                })
                .ToListAsync();

            return Ok(users);
        }

        // GET: api/Utilisateur/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UtilisateurDto>> GetUtilisateur(int id)
        {
            var user = await _context.Utilisateurs.FirstOrDefaultAsync(a => a.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return new UtilisateurDto
            {
                Id = user.Id,
                login = user.login,
                password = user.password,
                EstAdmin = user.EstAdmin,
            };
        }


        // GET: api/Articles/5
        [HttpGet]
        [Route("search/{login}")]
        public async Task<ActionResult<IEnumerable<UtilisateurDto>>> GetUtilisateurByLogin(string login)
        {
            return await _context.Utilisateurs
                .Where(user => user.login == login)
                .Select(user => new UtilisateurDto
                {
                    Id = user.Id,
                    login = user.login,
                    password= user.password,
                    EstAdmin = user.EstAdmin,
                }).ToListAsync();
        }

        // PUT: api/Articles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUtilisateur(int id, UtilisateurDto userPUT)
        {
            if (id != userPUT.Id)
            {
                return BadRequest();
            }

            var user = await _context.Utilisateurs
                .FirstOrDefaultAsync(a => a.Id == id);

            if (userPUT == null)
            {
                return NotFound();
            }
            user.Id = userPUT.Id;
            user.login = userPUT.login;
            user.password = userPUT.password;
            user.EstAdmin = userPUT.EstAdmin;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
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

        // POST: api/Utilisateurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Utilisateur>> PostArticle(UtilisateurDto userPOST)
        {


            var user = new Utilisateur
            {
                Id = userPOST.Id,
                login = userPOST.login,
                password = userPOST.password,
                EstAdmin = userPOST.EstAdmin
            };



            // Ajout de l'entité à la base de données
            _context.Utilisateurs.Add(user);
            await _context.SaveChangesAsync();



            return CreatedAtAction("GetUtilisateur", new { id = userPOST.Id }, userPOST);

        }

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilisateur(int id)
        {
            var article = await _context.Utilisateurs.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            _context.Utilisateurs.Remove(article);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArticleExists(int id)
        {
            return _context.Utilisateurs.Any(e => e.Id == id);
        }

    }


}
