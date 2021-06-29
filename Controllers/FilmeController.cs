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
        public async Task<List<FilmeOutputGetAllDTO>> Get()
        {
            var filmes = await _context.Filmes.Include(d => d.Diretor).ToListAsync();
            var filmesDto = new List<FilmeOutputGetAllDTO>();

            foreach (Filme filme in filmes)
            {
                var filmeDto = new FilmeOutputGetAllDTO(
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
        public async  Task<ActionResult<FilmeOutputGetByIdDTO>> Get(long id)
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

        [HttpPost]
        public async Task<ActionResult<FilmeOutputPostDTO>> Post([FromBody] FilmeInputPostDTO filmeInputPostDTO)
        {
            var diretor = await _context.Diretores.FindAsync(filmeInputPostDTO.DiretorId);

            if(diretor == null) {
                return Conflict("Cadastre um diretor para esse filme!");
            }

            var filme = new Filme(
                filmeInputPostDTO.Titulo, 
                filmeInputPostDTO.Ano, 
                filmeInputPostDTO.Genero 
            );

            filme.DiretorId = filme.Id;
            filme.Diretor = diretor;

            _context.Filmes.Add(filme);
            await _context.SaveChangesAsync();

            var filmeOutPutDto = new FilmeOutputPostDTO(
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
            if(filmeInputPutDTO == null)
            {
                return Conflict("Verifique os campos!");
            }

            var filme = new Filme(
                filmeInputPutDTO.Titulo, 
                filmeInputPutDTO.Ano, 
                filmeInputPutDTO.Genero
            );

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

            _context.Filmes.Remove(filme);
            await _context.SaveChangesAsync();

            return Ok(filme);
        }
    }
}