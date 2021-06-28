using System.Collections.Generic;
using webapicurso.Models;

namespace webapicurso.DTOs.DiretorDto
{
    public class DiretorOutputGetAllDTO
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        
        public List<string> FilmesTitulos { get; set; }
        
        public DiretorOutputGetAllDTO(long id, string nome, List<Filme> filmes) {
            Id = id;
            Nome = nome;
            
            FilmesTitulos = new List<string>();
            foreach(var filme in filmes) {
                FilmesTitulos.Add(filme.Titulo);
            }
        }
    }
}