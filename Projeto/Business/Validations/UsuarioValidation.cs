using Business.IO.Users;
using FluentValidation;
using System;

namespace Business.Validations
{
    public class UsuarioValidation : AbstractValidator<UsuarioView>
    {
        public UsuarioValidation()
        {
        RuleFor(f => f.Nome)
              .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
            RuleFor(f => f.Imagem)
             .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
            RuleFor(f => f.Senha)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
            RuleFor(f => f.Role)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
            RuleFor(f => f.Cnpj)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
            RuleFor(f => f.Telefone)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
            RuleFor(f => f.Email)
                  .NotEmpty()
                 .WithMessage("O campo {PropertyName} precisa ser fornecido")
                 .EmailAddress()
                 .WithMessage("Email inválido");

        }    
    }
}