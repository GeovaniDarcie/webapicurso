using FluentValidation;

namespace webapicurso.DTOs.DiretorDto
{
    public class DiretorInputPostDTO
    {
        public string Nome { get; set; }
    }

    public class DiretorInputPostDTOValidator : AbstractValidator<DiretorInputPostDTO>
    {
        public DiretorInputPostDTOValidator()
        {
            RuleFor(diretor => diretor.Nome).NotNull().NotEmpty();
            RuleFor(diretor => diretor.Nome).Length(2, 250).WithMessage("Tamanho {TotalLength} inválido.");
        }
    }
}
