namespace webapicurso.DTOs.Diretor
{
    public class DiretorOutputPutDTO
    {
        public long Id { get; set; }
        public string Nome { get; set; }

        public DiretorOutputPutDTO(long id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}