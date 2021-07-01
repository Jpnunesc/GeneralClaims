using Business.IO;
using Business.IO.Users;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateToken(UserAuthView user);
    }
}
