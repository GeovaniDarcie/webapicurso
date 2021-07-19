using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using webapicurso.DTOs.DiretorDto;

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

    public async Task<Diretor> GetById(long id)
    {
        var diretor = await _context.Diretores.FindAsync(id);

        if (diretor == null)
        {
            throw new Exception("Diretor não encontrado");
        }

        return diretor;
    }

    public async Task<Diretor> CriaDiretor(DiretorInputPostDTO diretorInputPostDTO)
    {
        var diretor = new Diretor(diretorInputPostDTO.Nome);
        if (diretor.Nome == "")
        {
            throw new Exception("Digite o nome do diretor");
        }

        _context.Diretores.Add(diretor);
        await _context.SaveChangesAsync();

        return diretor;
    }

    public async Task<Diretor> AtualizaDiretor(long id, DiretorInputPutDTO diretorInputPutDTO)
    {
        var diretorDb = await _context.Diretores.FindAsync(id);
        if (diretorDb == null)
        {
            throw new Exception("Diretor não encontrado");
        }

        var diretor = new Diretor(diretorInputPutDTO.Nome);
        diretor.Id = id;
        _context.Diretores.Update(diretor);
        await _context.SaveChangesAsync();

        return diretor;
    }

    public async Task<Diretor> ExcluiDiretor(long id)
    {
        var diretor = await _context.Diretores.FindAsync(id);

        if (diretor == null)
        {
            throw new Exception("Diretor não encontrado");
        }

        _context.Diretores.Remove(diretor);
        await _context.SaveChangesAsync();

        return diretor;
    }
}