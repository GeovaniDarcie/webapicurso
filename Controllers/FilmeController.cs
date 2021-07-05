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