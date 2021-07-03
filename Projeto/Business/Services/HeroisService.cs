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
        readonly HttpClient _client = new HttpClient();
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
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
            if (!valid.IsValid) return new ReturnView() { Object = null, Message = valid.Errors[0].ErrorMessage, Status = false };
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
            var personagens = JsonConvert.DeserializeObject<HeroisViewModel>(await _client.GetStringAsync(String.Concat(url.Substring(0, 51),"/",id,url.Substring(51))));
            var result = IsFavoritos(personagens.Data.Results);
            _client.Dispose();
             retorno = new ReturnView() { Object = result, Message = "Operação realizada com sucesso!", Status = true };

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
                    return new ReturnView() { Status = true, Message = "Id inválido! Favor escolher id de um personagem favorito!" };
                }
            }
            catch (Exception ex)
            {
                return new ReturnView() { Status = false, Message = ex.Message };
            }
        }

        public async Task<ReturnView> Get(FiltroHerois filtro)
        {
            try
            {
                var url = String.Concat(_configuration.GetSection("ApiMarvel").Value, "&limit=100");
                var result = JsonConvert.DeserializeObject<HeroisViewModel>(await _client.GetStringAsync(url));
                _client.Dispose();
                if (filtro.Favorito == true)
                {
                    return GetFavoritos(filtro, result);
                } else if(filtro.Favorito == false) 
                {
                    return GetNaoFavorito(filtro, result);
                } else
                {
                    return GetTodos(filtro, result);
                }
            }
            catch (Exception ex)
            {
                return new ReturnView() { Status = false, Message = ex.Message };
            }
        }

        private ReturnView GetNaoFavorito(FiltroHerois filtro, HeroisViewModel result)
        {
            var personagens = IsFavoritos(result.Data.Results);
            if(!string.IsNullOrEmpty(filtro.Nome)) personagens = personagens.Where(x => x.Name.Contains(filtro.Nome));
            return new ReturnView() { Object = personagens.Where(x => !x.Favorito), Message = "Operação realizada com sucesso!", Status = true };
        }

        private ReturnView GetTodos(FiltroHerois filtro, HeroisViewModel herois)
        {
            IEnumerable<Results> result = herois?.Data.Results;
            if (!string.IsNullOrEmpty(filtro.Nome)) result = result.Where(x => x.Name.Contains(filtro.Nome));
            var personagens = IsFavoritos(result);
            return new ReturnView() { Object = personagens.OrderByDescending(x => x.Favorito), Message = "Operação realizada com sucesso!", Status = true }; 
        }

        public IEnumerable<Results> IsFavoritos(IEnumerable<Results> result)
        {
            foreach (var item in result)
            {
                var temp = _repository.Get(x => x.IdFavorito == item.Id).Result;
                item.Favorito = temp != null;
                item.Comentario = temp != null ? temp.Comentario : "";
            }
            return result;
        }

        private ReturnView GetFavoritos(FiltroHerois filtro, HeroisViewModel herois)
        {
            IEnumerable<Results> results = herois?.Data.Results;
            try
            {
                if(!string.IsNullOrEmpty(filtro.Nome))
                {
                    results = results.Where(x => x.Name.Contains(filtro.Nome));
                }
                results = IsFavoritos(results);
                return new ReturnView() { Object = results.Where(x => x.Favorito), Message = "Operação realizada com sucesso!", Status = true };


            }
            catch (Exception ex)
            {
                 return new ReturnView() { Object = null, Message = ex.Message, Status = false };
            }
        }
        public async Task<ReturnView> Put(FavoritosViewModel favorito)
        {

            ReturnView retorno = new ReturnView();
            FavoritosUpdateValidation validator = new FavoritosUpdateValidation();
            var valid = validator.Validate(favorito);
            if (!valid.IsValid) return new ReturnView() { Object = null, Message = valid.Errors[0].ErrorMessage, Status = false };
            var register = _mapper.Map<FavoritosViewModel, FavoritosEntity>(favorito);
            try
            {
                var personagem = await _repository.Get(x => x.IdFavorito == register.IdFavorito);
                if (personagem != null)
                {
                    personagem.Comentario = favorito.comentario;
                    var result = _mapper.Map<FavoritosEntity, FavoritosViewModel>(await _repository.Update(register));
                    retorno = new ReturnView() { Object = result, Message = "Operação realizada com sucesso!", Status = true };
                }
                else
                {
                    retorno = new ReturnView() { Object = null, Message = "idFaforito inválido! Favor escolher id de um personagem favorito!", Status = false };
                }
            }
            catch (Exception ex)
            {
                retorno = new ReturnView() { Object = null, Message = ex.Message, Status = false };
            }
            return retorno;
        }
    }
}
