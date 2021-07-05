using EdTech.Api.Entities.Requests;
using FluentValidation;

namespace EdTech.Api.Validators
{
    public class CreateAlunoRequestValidator : AbstractValidator<CreateAlunoRequest>
    {
        public CreateAlunoRequestValidator()
        {
            RuleFor(x => x.RA)
                .NotEmpty().WithMessage("Registro do Aluno não informado")
                .MaximumLength(20).WithMessage("Registro do Aluno não pode ser maior que 20 caracteres");

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome não informado")
                .MaximumLength(100).WithMessage("Nome não pode ser maior que 100 caracteres");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email não informado")
                .EmailAddress().WithMessage("Endereço de email inválido")
                .MaximumLength(100).WithMessage("Email não pode ser maior que 100 caracteres");

            RuleFor(x => x.Cpf.Replace("-", string.Empty).Replace(".", string.Empty))
                .NotEmpty().WithMessage("Cpf não informado")
                .MaximumLength(14).WithMessage("Cpf não pode ser maior que 14 caracteres");
        }
    }
}
