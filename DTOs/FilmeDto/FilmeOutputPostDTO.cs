namespace webapicurso.DTOs.FilmeDto
{
    public class FilmeOutputPostDTO
    {
        public long Id { get; set; }
        
        public string Titulo { get; set; }
        public string Ano { get; set; }
        public string Genero { get; set; }
        public string NomeDiretor { get; set; }

        public FilmeOutputPostDTO(long id, string titulo, string ano, string genero, string nomeDiretor) {
            Id = id;
            Titulo = titulo;
            Ano = ano;
            Genero = genero;
            NomeDiretor = nomeDiretor;
        }
    }
}