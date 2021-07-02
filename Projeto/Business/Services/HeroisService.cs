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
using System.Collections.Generic;
using System.Linq;

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
                if(!await _repository.Exists(x => x.IdFavorito == register.IdFavorito))
                {
                    var result = _mapper.Map<FavoritosEntity, FavoritosViewModel>(await _repository.Add(register));
                    retorno = new ReturnView() { Object = result, Message = "Operação realizada com sucesso!", Status = true };
                } else
                {
                    retorno = new ReturnView() { Object = null, Message = "Esse personagem já é favorito!", Status = false };
                }
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
            var result = GetFavoritos(JsonConvert.DeserializeObject<HeroisViewModel>(await _client.GetStringAsync(url.Substring(0, 51) + "/" + id + url.Substring(51))));
            _client.Dispose();
             retorno = new ReturnView() { Object = result.Data.Results, Message = "Operação realizada com sucesso!", Status = true };

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
                var result = _repository.Get(x => x.IdFavorito == id).Result;
                if(result != null)
                {
                    await _repository.Remove(result.Id);
                    return new ReturnView() { Status = true, Message = "Operação realizada com sucesso!" };
                } else
                {
                    return new ReturnView() { Status = true, Message = "Este personagem não é favorito seu!" };
                }
            }
            catch (Exception ex)
            {
                return new ReturnView() { Status = false, Message = ex.Message };
            }
        }

        public async Task<ReturnView> Get(bool? favorito)
        {
            try
            {
                if(favorito.Value)
                {
                    return GetFavoritos().Result;
                } else if(favorito.Value == false) 
                {
                    return GetOutros().Result;
                } else
                {
                    return await GetTodos();
                }
            }
            catch (Exception ex)
            {
                return new ReturnView() { Status = false, Message = ex.Message };
            }
        }

        private async Task<ReturnView> GetOutros()
        {
            var url = _configuration.GetSection("ApiMarvel").Value;
            var result = JsonConvert.DeserializeObject<HeroisViewModel>(_client.GetStringAsync(url).Result);
            var personagens = GetFavoritos();
            _client.Dispose();
            return await Task.Run(() => new ReturnView() { Object = personagens, Message = "Operação realizada com sucesso!", Status = true });
        }

        private async Task<ReturnView> GetTodos()
        {
            var url = _configuration.GetSection("ApiMarvel").Value;
            var result = JsonConvert.DeserializeObject<HeroisViewModel>(_client.GetStringAsync(url).Result);
            var personagens = GetFavoritos(result);
            _client.Dispose();
            return await Task.Run(() => new ReturnView() { Object = personagens, Message = "Operação realizada com sucesso!", Status = true }); 
        }

        public  HeroisViewModel GetFavoritos(HeroisViewModel result)
        {
            foreach (var item in result.Data.Results)
            {
                item.Favorito = _repository.Exists(x => x.IdFavorito == item.Id).Result;
            }
            return result;
        }

        public void Dispose()
        {
            _repository?.Dispose();
        }

        public async Task<ReturnView> GetFavoritos()
        {
            ReturnView retorno = new ReturnView();
            try
            {
                var url = _configuration.GetSection("ApiMarvel").Value + "&limit=200";
                var personagens = await _client.GetStringAsync(url);
                var result = GetFavoritos(JsonConvert.DeserializeObject<HeroisViewModel>(personagens));
                _client?.Dispose();
                var el = result.Data.Results.Where(x => x.Favorito == true).ToList();
                retorno = new ReturnView() { Object = el, Message = "Operação realizada com sucesso!", Status = true };


            }
            catch (Exception ex)
            {
                retorno = new ReturnView() { Object = null, Message = ex.Message, Status = false };

            }
            return retorno;
        }
    }
}
