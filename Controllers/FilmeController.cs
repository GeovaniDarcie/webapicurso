using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using webapicurso.Models;
using webapicurso.DTOs.DiretorDto;
using webapicurso.DTOs.FilmeDto;

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
            try
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
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FilmeOutputGetByIdDTO>> Get(long id)
        {
            try
            {
                var filme = await _context.Filmes.FindAsync(id);

                if (filme == null)
                {
                    return Conflict("Filme não encontrado!");
                }

                var filmeOutputGetByIdDTO = new FilmeOutputGetByIdDTO(
                     filme.Titulo,
                     filme.Ano,
                     filme.Genero,
                     filme.Diretor.Nome
                );

                return Ok(filmeOutputGetByIdDTO);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult<FilmeOutputPostDTO>> Post([FromBody] FilmeInputPostDTO filmeInputPostDTO)
        {
            try
            {
                var diretor = await _context.Diretores.FindAsync(filmeInputPostDTO.DiretorId);

                if (diretor == null)
                {
                    return Conflict("Cadastre um diretor válido para esse filme!");
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
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

        }

        [HttpPut]
        public async Task<ActionResult<Filme>> Put(long id, [FromBody] FilmeInputPutDTO filmeInputPutDTO)
        {
            try
            {
                var filme = await _context.Filmes.FindAsync(id);

                if (filme == null)
                {
                    return Conflict("Filme não encontrado");
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
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Filme>> Delete(long id)
        {
            try
            {
                var filme = await _context.Filmes.FindAsync(id);

                if (filme == null)
                {
                    return Conflict("Filme não encontrado");
                }

                _context.Filmes.Remove(filme);
                await _context.SaveChangesAsync();

                return Ok(filme);
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

        }
    }
}