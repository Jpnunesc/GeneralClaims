using AutoMapper;
using Business.Interfaces.Repositories;
using Business.Interfaces.Services;
using Business.IO;
using Business.Validations;
using Domain.Entitys;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Business.IO.Herois;

namespace Business.Services
{
    public class HeroisService : IHeroisService
    {
        HttpClient _client = new HttpClient();
        private readonly IMapper _mapper;
        private IConfiguration _configuration;
        private readonly IFavoritosRepository _repository;
        public HeroisService(IMapper mapper, IFavoritosRepository repository, IConfiguration configuration)
        {
            _configuration = configuration;
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<ReturnView> Save(FavoritosViewModel _favorito)
        {

            ReturnView retorno = new ReturnView();
            FavoritosValidation validator = new FavoritosValidation();
            var valid = validator.Validate(_favorito);
            if (!valid.IsValid)
            {
                retorno.Status = false;
                retorno.Message = valid.Errors[0].ErrorMessage;
            }
            var register = _mapper.Map<FavoritosViewModel, FavoritosEntity>(_favorito);
            try
            {
                var result = _mapper.Map<FavoritosEntity, FavoritosViewModel>(await _repository.Add(register));
                retorno = new ReturnView() { Object = result, Message = "Operação realizada com sucesso!", Status = true };
            }
            catch (Exception ex)
            {
                retorno = new ReturnView() { Object = null, Message = ex.Message, Status = false };
            }
            return retorno;
        }
        public async Task<ReturnView> GetId(int id)
        {
            ReturnView retorno = new ReturnView();
            try
            {
            var url = _configuration.GetSection("ApiMarvel").Value;
            var urlParams = url.Substring(0, 51) + "/" + id + url.Substring(51);
            var result = JsonConvert.DeserializeObject<HeroisViewModel>(await _client.GetStringAsync(urlParams));
            retorno = new ReturnView() { Object = result.Data, Message = "Operação realizada com sucesso!", Status = true };
            _client.Dispose();

            }
            catch (Exception ex)
            {
                retorno = new ReturnView() { Object = null, Message = ex.Message, Status = false };

            }
            return retorno;
        }
        public async Task<ReturnView> Delete(int id)
        {
            try
            {
                await _repository.Remove(id);
                return new ReturnView() { Status = true, Message = "Operação realizada com sucesso!" };
            }
            catch (Exception ex)
            {
                return new ReturnView() { Status = false, Message = ex.Message };
            }
        }

        public async Task<ReturnView> Get()
        {
            try
            {
                var url = _configuration.GetSection("ApiMarvel").Value;
                var result = JsonConvert.DeserializeObject<HeroisViewModel>(await _client.GetStringAsync(url));
                _client.Dispose();
                return new ReturnView() { Object = result.Data, Message = "Operação realizada com sucesso!", Status = true };
            }
            catch (Exception ex)
            {
                return new ReturnView() { Status = false, Message = ex.Message };
            }


        }
        public void Dispose()
        {
            _repository?.Dispose();
        }

    }
}
