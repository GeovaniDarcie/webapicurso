using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using webapicurso.Models;
using webapicurso.DTOs.DiretorDto;
using webapicurso.DTOs.FilmeDto;
using System;

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

        /// <summary>
        /// Lista todos os filmes
        /// </summary>
        /// <returns>Filmes listados</returns>
        /// <response code="200">Lista de filmes retornada com sucesso</response>
        /// <response code="500">A solicitação não foi concluída devido a um erro interno no lado do servidor.</response>

        [HttpGet]
        public async Task<ActionResult<List<FilmeOutputGetAllDTO>>> Get()
        {

            var filmes = await _context.Filmes.Include(d => d.Diretor).ToListAsync();
            var filmesDto = new List<FilmeOutputGetAllDTO>();


            if (filmes.Count == 0)
            {
                return Conflict("Filmes não encontrados");
            }

            foreach (Filme filme in filmes)
            {
                var filmeDto = new FilmeOutputGetAllDTO(
                    filme.Id,
                    filme.Titulo,
                    filme.Ano,
                    filme.Genero,
                    filme.Diretor.Nome
                );

                filmesDto.Add(filmeDto);
            }

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
            var filme = await _context.Filmes.FindAsync(id);

            if (filme == null)
            {
                return NotFound("Filme não encontrado!");
            }

            var filmeOutputGetByIdDTO = new FilmeOutputGetByIdDTO(
                 filme.Titulo,
                 filme.Ano,
                 filme.Genero,
                 filme.Diretor.Nome
            );

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

            var diretor = await _context.Diretores.FindAsync(filmeInputPostDTO.DiretorId);

            if (diretor == null)
            {
                return NotFound("Cadastre um diretor válido para esse filme!");
            }

            var filme = new Filme(
                filmeInputPostDTO.Titulo,
                filmeInputPostDTO.Ano,
                filmeInputPostDTO.Genero
            );

            filme.DiretorId = filmeInputPostDTO.DiretorId;
            filme.Diretor = diretor;

            _context.Filmes.Add(filme);
            await _context.SaveChangesAsync();

            var filmeOutPutDto = new FilmeOutputPostDTO(
                filme.Id,
                filme.Titulo,
                filme.Ano,
                filme.Genero,
                filme.Diretor.Nome
            );

            return Ok(filmeOutPutDto);

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
        public async Task<ActionResult<Filme>> Put(long id, [FromBody] FilmeInputPutDTO filmeInputPutDTO)
        {

            var filmeDb = await _context.Filmes.FindAsync(id);

            if (filmeDb == null)
            {
                return NotFound("Filme não encontrado");
            }

            var filme = new Filme(
                filmeInputPutDTO.Titulo,
                filmeInputPutDTO.Ano,
                filmeInputPutDTO.Genero
            );

            filme.Id = id;

            _context.Filmes.Update(filme);

            var filmeOutputPutDto = new FilmeOutputPutDTO(
                filme.Titulo,
                filme.Ano,
                filme.Genero,
                filme.Diretor.Nome
            );

            await _context.SaveChangesAsync();
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

            var filme = await _context.Filmes.FindAsync(id);

            if (filme == null)
            {
                return NotFound("Filme não encontrado");
            }

            _context.Filmes.Remove(filme);
            await _context.SaveChangesAsync();

            return Ok(filme);

        }
    }
}