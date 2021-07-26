using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using webapicurso.DTOs.DiretorDto;
using webapicurso.Models;
using System;
using System.Web;

namespace webapicurso.Services.DiretorService
{
    public class DiretorService : IDiretorService
    {
        private readonly ApplicationDbContext _context;
        public DiretorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DiretorOutputGetAllDTO>> GetAll()
        {
            var diretores = await _context.Diretores.Include(d => d.Filmes).ToListAsync();

            if (diretores.Count == 0) {
                throw new StatusCode(404);
            }

            var diretoresDto = new List<DiretorOutputGetAllDTO>();

            foreach (Diretor diretor in diretores)
            {
                var diretorDto = new DiretorOutputGetAllDTO(diretor.Id, diretor.Nome, diretor.Filmes);
                diretoresDto.Add(diretorDto);
            }

            return diretoresDto;
        }

        public async Task<DiretorOutputGetByIdDTO> GetById(long id)
        {
            var diretor = await _context.Diretores.FindAsync(id);
            DiretorOutputGetByIdDTO outputDto = null;
            
            if (diretor != null) {
                outputDto = new DiretorOutputGetByIdDTO(diretor.Id, diretor.Nome);
            }

            return outputDto;
        }
        public async Task<DiretorOutputPostDTO> Post(DiretorInputPostDTO diretorInputPostDTO)
        {
            var diretor = new Diretor(diretorInputPostDTO.Nome);

            _context.Diretores.Add(diretor);
            await _context.SaveChangesAsync();

            var diretorOutputPostDTO = new DiretorOutputPostDTO(diretor.Id, diretor.Nome);

            return diretorOutputPostDTO;
        }
        public async Task<DiretorOutputPutDTO> Put(long id, DiretorInputPutDTO diretorInputPutDTO)
        {
            var diretorDb = await _context.Diretores.FindAsync(id);
            Diretor diretor = null;

             if (diretorInputPutDTO != null) {
                diretor = new Diretor(diretorInputPutDTO.Nome);
            }

            diretor.Id = id;
            _context.Diretores.Update(diretor);
            await _context.SaveChangesAsync();

            var diretorOutputPutDTO = new DiretorOutputPutDTO(diretor.Id, diretor.Nome);

            return diretorOutputPutDTO;
        }

        public async Task<Diretor> Delete(long id)
        {
            var diretor = await _context.Diretores.FindAsync(id);

            if (diretor != null)
            {
                _context.Diretores.Remove(diretor);
                await _context.SaveChangesAsync();
            }

            return diretor;
        }
    }
}