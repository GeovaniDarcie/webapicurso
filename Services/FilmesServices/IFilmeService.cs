using System.Collections.Generic;
using System.Threading.Tasks;
using webapicurso.DTOs.FilmeDto;
using webapicurso.Models;

namespace webapicurso.Services.FilmesServices
{
    public interface IFilmeService
    {
        Task<List<FilmeOutputGetAllDTO>> GetAll();
        Task<FilmeOutputGetByIdDTO> GetById(long id);
        Task<FilmeOutputPostDTO> CriaFilme(FilmeInputPostDTO filmeInputPostDTO);
        Task<FilmeOutputPutDTO> AtualizaFilme(long id, FilmeInputPutDTO filmeInputPutDTO);
        Task<Filme> ExcluiFilme(long id);
    }
}