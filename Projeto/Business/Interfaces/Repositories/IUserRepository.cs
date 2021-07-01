using Business.IO.Users;
using Domain.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces.Repositories
{
    public interface IUserRepository : IRepositoryBase<UserEntity>
    {
        Task<UserEntity> Get(string username, string password);
    }
}
