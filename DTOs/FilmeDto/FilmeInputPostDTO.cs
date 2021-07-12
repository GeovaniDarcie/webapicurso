using FluentValidation;

namespace webapicurso.DTOs.FilmeDto
{
    public class FilmeInputPostDTO
    {
        public string Titulo { get; set; }
        public string Ano { get; set; }
        public string Genero { get; set; }
        public long DiretorId { get; set; }
    }

    public class FilmeInputPosttDTOValidator : AbstractValidator<FilmeInputPostDTO>
    {
        public FilmeInputPosttDTOValidator()
        {
            RuleFor(filme => filme.Titulo).NotNull().NotEmpty();
            RuleFor(filme => filme.Titulo).Length(2, 250).WithMessage("Tamanho {TotalLength} inválido.");
            RuleFor(filme => filme.Ano).MinimumLength(4);
            RuleFor(filme => filme.Genero).Length(2, 250).WithMessage("Tamanho {TotalLength} inválido.");
            RuleFor(filme => filme.DiretorId).NotNull().NotEmpty();
        }
    }
}