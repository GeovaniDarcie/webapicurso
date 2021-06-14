using System.Collections.Generic;
using webapicurso.Models;

public class Diretor 
{
    public Diretor(string nome)
    {
        Nome = nome;
        Filmes = new List<Filme>();
    }
    public long Id { get; set; }
    public string Nome { get; set; }
    public ICollection<Filme> Filmes { get; set; }
}