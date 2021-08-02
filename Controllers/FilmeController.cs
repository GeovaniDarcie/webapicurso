using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using webapicurso.Models;
using webapicurso.DTOs.DiretorDto;
using webapicurso.DTOs.FilmeDto;
using System;
using webapicurso.Services.FilmesServices;

namespace webapicurso.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private readonly IFilmeService _filmeService;
        public FilmeController(IFilmeService filmeService)
        {
            _filmeService = filmeService;
        }

        /// <summary>
        /// Lista todos os filmes
        /// </summary>
        /// <returns>Filmes listados</returns>
        /// <response code="200">Lista de filmes retornada com sucesso</response>
        /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>

        [HttpGet]
        public async Task<ActionResult<List<FilmeOutputGetAllDTO>>> Get()
        {
           var filmesDto = await _filmeService.GetAll();
           return filmesDto;
        }


        /// <summary>
        /// Busca um filme
        /// </summary>
        /// <returns>Filme buscado com sucesso</returns>
        /// <param name="id">Filme id</param>
        /// <response code="200">Busca um filme</response>
        /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>

        [HttpGet("{id}")]
        public async Task<ActionResult<FilmeOutputGetByIdDTO>> Get(long id)
        {
            var filmeOutputGetByIdDTO = await _filmeService.GetById(id);
            return Ok(filmeOutputGetByIdDTO);
        }

        /// <summary>
        /// Cria um filme
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /filme
        ///     {
        ///        "titulo": "Tropa de Elite",
        ///        "ano": "2010",
        ///        "genero": "acao",
        ///        "diretorId": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="filmeInputPostDTO">Dados do filme</param>
        /// <returns>O filme foi criado</returns>
        /// <response code="200">Filme foi criado com sucesso</response>
        /// <response code="400">Requisição sem corpo</response>
        /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>


        [HttpPost]
        public async Task<ActionResult<FilmeOutputPostDTO>> Post([FromBody] FilmeInputPostDTO filmeInputPostDTO)
        {
            var filmeOutputGetByIdDTO = await _filmeService.CriaFilme(filmeInputPostDTO);
            return Ok(filmeOutputGetByIdDTO);
        }

        /// <summary>
        /// Edita um filme
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /filme
        ///     {
        ///        "titulo": "Tropa de Elite",
        ///        "ano": "2010",
        ///        "genero": "acao",
        ///     }
        /// </remarks>
        /// <param name="id">Id do filme</param>
        /// <param name="filmeInputPutDTO">Dados de atualização do filme</param>
        /// <returns>O Filme foi atualizado</returns>
        /// <response code="200">Filme foi atualizado com sucesso</response>
        /// <response code="400">Requisição sem corpo</response>
        /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>

        [HttpPut]
        public async Task<ActionResult<FilmeOutputPutDTO>> Put(long id, [FromBody] FilmeInputPutDTO filmeInputPutDTO)
        {
            var filmeOutputPutDto = await _filmeService.AtualizaFilme(id, filmeInputPutDTO);
            return Ok(filmeOutputPutDto);
        }

        /// <summary>
        /// Deleta um filme
        /// </summary>
        /// <returns>Delete um filme</returns>
        /// <param name="id">Id do filme</param>
        /// <response code="200">Filme excluído com sucesso</response>
        /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>

        [HttpDelete("{id}")]
        public async Task<ActionResult<Filme>> Delete(long id)
        {

            var filme = await _filmeService.ExcluiFilme(id);

            return Ok(filme);

        }
    }
}