using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using webapicurso.DTOs.FilmeDto;
using webapicurso.Models;

namespace webapicurso.Services.FilmesServices
{
    public class FilmeService : IFilmeService
    {
        private readonly ApplicationDbContext _context;
        public FilmeService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<FilmeOutputGetAllDTO>> GetAll()
        {
            var filmes = await _context.Filmes.Include(d => d.Diretor).ToListAsync();

            var filmesDto = new List<FilmeOutputGetAllDTO>();


            if (filmes.Count == 0)
            {
                throw new Exception("Filmes não encontrados");
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

        public async Task<FilmeOutputGetByIdDTO> GetById(long id)
        {
            var filme = await _context.Filmes.FindAsync(id);
            
            if (filme == null)
            {
                throw new Exception("Filme não encontrado!");
            }

              var filmeOutputGetByIdDTO = new FilmeOutputGetByIdDTO(
                 filme.Titulo,
                 filme.Ano,
                 filme.Genero,
                 filme.Diretor.Nome
            );

            return filmeOutputGetByIdDTO;
        }

        public async Task<FilmeOutputPostDTO> CriaFilme(FilmeInputPostDTO filmeInputPostDTO)
        {
            var diretor = await _context.Diretores.FindAsync(filmeInputPostDTO.DiretorId);

            if (diretor == null)
            {
                throw new Exception("Cadastre um diretor válido para esse filme!");
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

            return filmeOutPutDto;
        }

        public async Task<FilmeOutputPutDTO> AtualizaFilme(long id, FilmeInputPutDTO filmeInputPutDTO)
        {
            var filmeDb = await _context.Filmes.FindAsync(id);

            if (filmeDb == null)
            {
                throw new Exception("Filme não encontrado");
            }

            var filme = new Filme(
                filmeInputPutDTO.Titulo,
                filmeInputPutDTO.Ano,
                filmeInputPutDTO.Genero
            );

            filme.Id = id;

            _context.Filmes.Update(filme);
            await _context.SaveChangesAsync();
        
            var filmeOutputPutDto = new FilmeOutputPutDTO(
                filme.Titulo,
                filme.Ano,
                filme.Genero,
                filme.Diretor.Nome
            );

            return filmeOutputPutDto;
        }

        public async Task<Filme> ExcluiFilme(long id)
        {
            var filme = await _context.Filmes.FindAsync(id);

            if (filme == null)
            {
                throw new Exception("Filme não encontrado");
            }

            _context.Filmes.Remove(filme);
            await _context.SaveChangesAsync();

            return filme;
        }
    }
}