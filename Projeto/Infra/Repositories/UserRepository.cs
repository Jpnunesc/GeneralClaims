using Business.Interfaces.Repositories;
using Business.IO.Users;
using Domain.Entity;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class UserRepository : RepositoryBase<GeneralClaimsContext, UserEntity>, IUserRepository
    {
        public UserRepository(GeneralClaimsContext context) : base(context)
        {

        }

        public async Task<UserEntity> Get(string username, string password)
        {
            //var query =  DbSet as IQueryable<UserEntity>;
            //return await query.Where(x => x.Nome.ToLower() == username.ToLower() && x.Senha == password).FirstOrDefaultAsync();
            return await Task.Run(() => new UserEntity { IdUsuario = 1, Nome = "sup", Senha = "sup", Role = "manager" });

        }
    }
}
