using Business.IO;
using Business.IO.Herois;
using System.Threading.Tasks;

namespace Business.Interfaces.Services
{
    public interface IHeroisService
    {
        Task<ReturnView> Save(FavoritosViewModel _favorito);
        Task<ReturnView> GetId(int id);
        Task<ReturnView> Delete(int id);
        Task<ReturnView> Get(FiltroHerois filtro);
       // Task<ReturnView> GetFavoritos();
    }
}
