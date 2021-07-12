using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using webapicurso.DTOs.DiretorDto;
using webapicurso.Models;
using System;

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

        /// <summary>
        /// Lista todos os diretores
        /// </summary>
        /// <returns>Lista dos diretores</returns>
        /// <response code="200">Sucesso ao retornar um diretor</response>
        /// <response code="404">Não existem diretores cadastrados</response>
        /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>

        [HttpGet]
        public async Task<ActionResult<List<DiretorOutputGetAllDTO>>> Get()
        {
            var diretores = await _context.Diretores.Include(d => d.Filmes).ToListAsync();

            if (diretores.Count == 0)
            {
                return NotFound("Diretores não encontrados");
            }

            var diretoresDto = new List<DiretorOutputGetAllDTO>();

            foreach (Diretor diretor in diretores)
            {
                var diretorDto = new DiretorOutputGetAllDTO(diretor.Id, diretor.Nome, diretor.Filmes);
                diretoresDto.Add(diretorDto);
            }

            return diretoresDto;
        }

        /// <summary>
        /// Busca um diretor
        /// </summary>
        /// <param name="id">Id do diretor</param>
        /// <returns>Lista dos diretores</returns>
        /// <response code="200">Sucesso ao retornar a lista dos diretores</response>
        /// <response code="404">Não existem diretores cadastrados</response>
        /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>

        [HttpGet("{id}")]
        public async Task<ActionResult<DiretorOutputGetByIdDTO>> Get(long id)
        {
            var diretor = await _context.Diretores.FindAsync(id);

            if (diretor == null)
            {
                return NotFound("Diretor não encontrado");
            }

            var outputDto = new DiretorOutputGetByIdDTO(diretor.Id, diretor.Nome);

            return Ok(outputDto);
        }

        /// <summary>
        /// Cria um diretor
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /diretor
        ///     {
        ///        "nome": "Martin Scorsese",
        ///     }
        ///
        /// </remarks>
        /// <param name="diretorInputPostDTO">Nome do diretor</param>
        /// <returns>O diretor criado</returns>
        /// <response code="200">Diretor foi criado com sucesso</response>
        /// <response code="400">Requisição sem corpo</response>
        /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>

        [HttpPost]
        public async Task<ActionResult<DiretorOutputPostDTO>> Post([FromBody] DiretorInputPostDTO diretorInputPostDTO)
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

        /// <summary>
        /// Modificia os dados de um diretor
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /diretor
        ///     {
        ///        "nome": "Martin Scorsese"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">Id do diretor</param>
        /// <param name="diretorInputPutDTO">Dados do diretor</param>
        /// <returns>O diretor foi atualizado com sucesso</returns>
        /// <response code="200">Diretor foi atualizado</response>
        /// <response code="400">Requisição sem corpo</response>
        /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>

        [HttpPut]
        public async Task<ActionResult<DiretorOutputPutDTO>> Put(long id, [FromBody] DiretorInputPutDTO diretorInputPutDTO)
        {
            var diretorDb = await _context.Diretores.FindAsync(id);
            if (diretorDb == null)
            {
                return NotFound("Diretor não encontrado");
            }

            var diretor = new Diretor(diretorInputPutDTO.Nome);
            diretor.Id = id;
            _context.Diretores.Update(diretor);
            await _context.SaveChangesAsync();

            var diretorOutputPutDTO = new DiretorOutputPostDTO(diretor.Id, diretor.Nome);

            return Ok(diretorOutputPutDTO);
        }

        /// <summary>
        /// Deleta um diretor
        /// </summary>
        /// <param name="id">Id do diretor</param>
        /// <returns>Diretor deletado</returns>
        /// <response code="200">Diretor deletado com sucesso</response>
        /// <response code="404">Não existem diretores cadastrados para excluir</response>
        /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>

        [HttpDelete("{id}")]
        public async Task<ActionResult<Diretor>> Delete(long id)
        {
            var diretor = await _context.Diretores.FindAsync(id);

            if (diretor == null)
            {
                return NotFound("Diretor não encontrado");
            }

            _context.Diretores.Remove(diretor);
            await _context.SaveChangesAsync();

            return Ok(diretor);
        }
    }
}