using System.Collections.Generic;
using System.Threading.Tasks;

public interface IDiretorService
{
    Task<List<Diretor>> GetAll();
}
