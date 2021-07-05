namespace webapicurso.DTOs.FilmeDto
{
    public class FilmeOutputGetAllDTO
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public string Ano { get; set; }
        public string Genero { get; set; }
        public string DiretorNome { get; set; }

        public FilmeOutputGetAllDTO(long id, string titulo, string ano, string genero, string diretorNome) {
            Id = id;
            Titulo = titulo;
            Ano = ano;
            Genero = genero;
            DiretorNome = diretorNome;
        }
    }
}