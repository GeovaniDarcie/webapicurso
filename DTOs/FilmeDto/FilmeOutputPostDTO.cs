namespace webapicurso.DTOs.FilmeDto
{
    public class FilmeOutputPostDTO
    {
        public string Titulo { get; set; }
        public string Ano { get; set; }
        public string Genero { get; set; }
        public string NomeDiretor { get; set; }

        public FilmeOutputPostDTO(string titulo, string ano, string genero, string nomeDiretor) {
            Titulo = titulo;
            Ano = ano;
            Genero = genero;
            NomeDiretor = nomeDiretor;
        }
    }
}