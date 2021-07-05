using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using webapicurso.DTOs.DiretorDto;
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
        public async Task<ActionResult<List<DiretorOutputGetAllDTO>>> Get()
        {
            try
            {
                var diretores = await _context.Diretores.Include(d => d.Filmes).ToListAsync();

                if (diretores.Count == 0)
                {
                    return Conflict("Diretores não encontrados");
                }

                var diretoresDto = new List<DiretorOutputGetAllDTO>();

                foreach (Diretor diretor in diretores)
                {
                    var diretorDto = new DiretorOutputGetAllDTO(diretor.Id, diretor.Nome, diretor.Filmes);
                    diretoresDto.Add(diretorDto);
                }

                return diretoresDto;
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DiretorOutputGetByIdDTO>> Get(long id)
        {
            try
            {
                var diretor = await _context.Diretores.FindAsync(id);

                if (diretor == null)
                {
                    return Conflict("Diretor não encontrado");
                }

                var outputDto = new DiretorOutputGetByIdDTO(diretor.Id, diretor.Nome);

                return Ok(outputDto);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<DiretorOutputPostDTO>> Post([FromBody] DiretorInputPostDTO diretorInputPostDTO)
        {
            try
            {
                var diretor = new Diretor(diretorInputPostDTO.Nome);
                if (diretor.Nome == "")
                {
                    return Conflict("Digite o nome do diretor");
                }

                _context.Diretores.Add(diretor);
                await _context.SaveChangesAsync();

                var diretorOutputPostDTO = new DiretorOutputPostDTO(diretor.Id, diretor.Nome);

                return Ok(diretorOutputPostDTO);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

        }

        [HttpPut]
        public async Task<ActionResult<DiretorOutputPutDTO>> Put(long id, [FromBody] DiretorInputPutDTO diretorInputPutDTO)
        {
            try
            {
                var diretor = await _context.Diretores.FindAsync(id);
                if (diretor == null)
                {
                    return Conflict("Diretor não encontrado");
                }

                var diretor = new Diretor(diretorInputPutDTO.Nome);
                diretor.Id = id;
                _context.Diretores.Update(diretor);
                await _context.SaveChangesAsync();

                var diretorOutputPutDTO = new DiretorOutputPostDTO(diretor.Id, diretor.Nome);

                return Ok(diretorOutputPutDTO);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Diretor>> Delete(long id)
        {
            try
            {
                var diretor = await _context.Diretores.FindAsync(id);

                if (diretor == null)
                {
                    return Conflict("Diretor não encontrado");
                }

                _context.Diretores.Remove(diretor);
                await _context.SaveChangesAsync();

                return Ok(diretor);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}