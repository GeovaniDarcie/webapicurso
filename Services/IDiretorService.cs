using System.Collections.Generic;
using System.Threading.Tasks;
using webapicurso.DTOs.DiretorDto;

public interface IDiretorService
{
    Task<List<Diretor>> GetAll();
    Task<Diretor> GetById(long id);
    Task<Diretor> CriaDiretor(DiretorInputPostDTO diretorInputPostDTO);
    Task<Diretor> AtualizaDiretor(long id, DiretorInputPutDTO diretorInputPutDTO);
    Task<Diretor> ExcluiDiretor(long id);
}
