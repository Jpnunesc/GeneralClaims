using Business.Interfaces.Repositories;
using Domain.Entitys;
using Infra.Context;

namespace Infra.Repositories
{
    public class FavoritosRepository : RepositoryBase<GeneralClaimsContext, FavoritosEntity>, IFavoritosRepository
    {
        public FavoritosRepository(GeneralClaimsContext context) : base(context)
        {
        }
    }
}
