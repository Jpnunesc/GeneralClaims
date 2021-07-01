using AutoMapper;
using Business.Interfaces.Repositories;
using Business.Interfaces.Services;
using Business.IO;
using Business.IO.Users;
using Business.Validations;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{

    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _repository;
        public UserService(IMapper mapper, IUserRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<UserAuthView> Get(string username, string password)
        {
            return _mapper.Map<UserEntity, UserAuthView>(await _repository.Get(username, password));

        }

    }
}