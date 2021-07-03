
using Business.IO.Herois;
using FluentValidation;

namespace Business.Validations
{
    public class FavoritosUpdateValidation : AbstractValidator<FavoritosViewModel>
    {
        public FavoritosUpdateValidation()
        {
            RuleFor(f => f.IdFavorito)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
            RuleFor(f => f.comentario)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
        }    
    }
}