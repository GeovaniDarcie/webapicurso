namespace webapicurso.Models
{
    public class Filme
    {
        public Filme(string titulo)
        {
            Titulo = titulo;
        }
        public long Id { get; set; }
        public string Titulo { get; set; }
        public string Ano { get; set; }
        public string Genero { get; set; }
        public long DiretorId { get; set; }
        public Diretor Diretor { get; set; }

         public Filme(string titulo, string ano, string genero) {
            Titulo = titulo;
            Ano = ano;
            Genero = genero;
        }
    }
}