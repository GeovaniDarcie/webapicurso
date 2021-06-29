namespace webapicurso.DTOs.FilmeDto
{
    public class FilmeOutputGetByIdDTO
    {
        public string Titulo { get; set; }
        public string Ano { get; set; }
        public string Genero { get; set; }
        public string DiretorNome { get; set; }

        public FilmeOutputGetByIdDTO(string titulo, string ano, string genero, string diretorNome) {
            Titulo = titulo;
            Ano = ano;
            Genero = genero;
            DiretorNome = diretorNome;
        }
    }
}