using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace webapicurso.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiretorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DiretorController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Diretor>> Get()
        {
            return await _context.Diretores.ToListAsync();
        }

        [HttpGet("{id}")]
        public async  Task<ActionResult<Diretor>> Get(long id)
        {
           var diretor = await _context.Diretores.FindAsync(id);

           if (diretor == null) 
           {
               return Conflict("Digite um Id válido!");
           }

           return Ok(diretor);
        }

        [HttpPost]
        public async Task<ActionResult<Diretor>> Post([FromBody] Diretor diretor)
        {
            if(diretor.Nome == "")
            {
                return Conflict("Digite o nome do diretor");
            }

            _context.Diretores.Add(diretor);
            await _context.SaveChangesAsync();

            return Ok(diretor);
        }

        [HttpPut]
        public async Task<ActionResult<Diretor>> Put([FromBody] Diretor diretor)
        {
            if(diretor == null)
            {
                return Conflict("Verifique os campos!");
            }

            _context.Diretores.Update(diretor);
            await _context.SaveChangesAsync();

            return Ok(diretor);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Diretor>> Delete(long id)
        {
            if(id == 0)
            {
                return Conflict("Insira um id!");
            }

            var diretor = await _context.Diretores.FindAsync(id);

            _context.Diretores.Remove(diretor);
            await _context.SaveChangesAsync();

            return Ok(diretor);
        }
    }
}