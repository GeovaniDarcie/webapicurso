using System.Threading.Tasks;
using webapicurso.DTOs.DiretorDto;
using System.Collections.Generic;

namespace webapicurso.Services.DiretorService
{
    public interface IDiretorService
    {
        Task<List<DiretorOutputGetAllDTO>> GetAll();
        Task<DiretorOutputGetByIdDTO> GetById(long id);
        Task<DiretorOutputPostDTO> Post(DiretorInputPostDTO diretorInputPostDTO);
        Task<DiretorOutputPutDTO> Put(long id, DiretorInputPutDTO diretorInputPostDTO);
        Task<Diretor> Delete(long id);
    }
}