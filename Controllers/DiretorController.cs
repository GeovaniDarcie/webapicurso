using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using webapicurso.DTOs.DiretorDto;
using webapicurso.Models;
using Microsoft.AspNetCore.Authorization;
using System;

namespace webapicurso.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DiretorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IDiretorService _diretorService;
        public DiretorController(ApplicationDbContext context, IDiretorService diretorService)
        {
            _context = context;
            _diretorService = diretorService;
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
            var diretores = await _diretorService.GetAll();

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
            var diretor = await _diretorService.GetById(id);

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
            var diretor = await _diretorService.CriaDiretor(diretorInputPostDTO);
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
            var diretor = await _diretorService.AtualizaDiretor(id, diretorInputPutDTO);

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
            var diretor = await _diretorService.ExcluiDiretor(id);

            return Ok(diretor);
        }
    }
}