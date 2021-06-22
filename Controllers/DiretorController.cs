using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using webapicurso.DTOs.Diretor;
using webapicurso.Models;

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
        public async Task<ActionResult<DiretorOutputPostDTO>> Post([FromBody] DiretorInputPostDTO diretorInputPostDTO)
        {
            var diretor = new Diretor(diretorInputPostDTO.Nome);
            if(diretor.Nome == "")
            {
                return Conflict("Digite o nome do diretor");
            }

            _context.Diretores.Add(diretor);
            await _context.SaveChangesAsync();

            var diretorOutputPostDTO = new DiretorOutputPostDTO(diretor.Id, diretor.Nome);
                
            return Ok(diretorOutputPostDTO);
        }

        [HttpPut]
        public async Task<ActionResult<DiretorOutputPutDTO>> Put(long id, [FromBody] DiretorInputPutDTO diretorInputPutDTO)
        {
            var diretor = new Diretor(diretorInputPutDTO.Nome);
            diretor.Id = id;
            _context.Diretores.Update(diretor);
            await _context.SaveChangesAsync();

            var diretorOutputPutDTO = new DiretorOutputPostDTO(diretor.Id, diretor.Nome);
    
            return Ok(diretorOutputPutDTO);
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