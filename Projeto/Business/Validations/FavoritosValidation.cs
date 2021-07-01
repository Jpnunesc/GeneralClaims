
using Business.IO.Herois;
using FluentValidation;

namespace Business.Validations
{
    public class FavoritosValidation : AbstractValidator<FavoritosViewModel>
    {
        public FavoritosValidation()
        {
            RuleFor(f => f.IdFavorito)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
        }    
    }
}