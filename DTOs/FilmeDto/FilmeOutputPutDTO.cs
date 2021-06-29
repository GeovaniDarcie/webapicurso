namespace webapicurso.DTOs.FilmeDto
{
    public class FilmeOutputPutDTO
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public string Ano { get; set; }
        public string Genero { get; set; }
        public string NomeDiretor { get; set; }

        public FilmeOutputPutDTO(string titulo, string ano, string genero, string nomeDiretor) {
            Titulo = titulo;
            Ano = ano;
            Genero = genero;
            NomeDiretor = nomeDiretor;
        }
    }
}