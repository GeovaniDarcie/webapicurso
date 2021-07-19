using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

public class DiretorService : IDiretorService
{
    private readonly ApplicationDbContext _context;
    public DiretorService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Diretor>> GetAll()
    {
        var diretores = await _context.Diretores.Include(d => d.Filmes).ToListAsync();

        if (diretores.Count == 0)
        {
            throw new Exception("Diretores não encontrados");
        }

        return diretores;
    }
}