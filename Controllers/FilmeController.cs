using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using webapicurso.Models;

namespace webapicurso.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public FilmeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Filme>> Get()
        {
            return await _context.Filmes.Include(f => f.Diretor).ToListAsync();
        }

        [HttpGet("{id}")]
        public async  Task<ActionResult<Filme>> Get(long id)
        {
           var filme = await _context.Filmes.FindAsync(id);

           if (filme == null) 
           {
               return Conflict("Filme não encontrado!");
           }

           return Ok(filme);
        }

        [HttpPost]
        public async Task<ActionResult<Filme>> Post([FromBody] Filme filme)
        {
            var diretor = await _context.Diretores.FindAsync(filme.DiretorId);

            if(diretor == null) {
                return Conflict("Cadastre um diretor para esse filme!");
            }

            filme.Diretor = diretor;

            _context.Filmes.Add(filme);
            await _context.SaveChangesAsync();

            return Ok(filme);
        }

        [HttpPut]
        public async Task<ActionResult<Filme>> Put([FromBody] Filme filme)
        {
            if(filme == null)
            {
                return Conflict("Verifique os campos!");
            }

            _context.Filmes.Update(filme);
            await _context.SaveChangesAsync();

            return Ok(filme);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Filme>> Delete(long id)
        {
            var filme = await _context.Filmes.FindAsync(id);

            _context.Filmes.Remove(filme);
            await _context.SaveChangesAsync();

            return Ok(filme);
        }
    }
}