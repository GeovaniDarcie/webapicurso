namespace webapicurso.DTOs.FilmeDto
{
    public class FilmeOutputGetAllDTO
    {
        public string Titulo { get; set; }
        public string Ano { get; set; }
        public string Genero { get; set; }
        public string DiretorNome { get; set; }

        public FilmeOutputGetAllDTO(string titulo, string ano, string genero, string diretorNome) {
            Titulo = titulo;
            Ano = ano;
            Genero = genero;
            DiretorNome = diretorNome;
        }
    }
}