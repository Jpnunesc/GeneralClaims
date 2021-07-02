using Business.Interfaces.Services;
using Business.IO;
using Business.IO.Herois;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Api.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class HeroisController : ControllerBase
    {
        private IHeroisService _service;

        public HeroisController(IHeroisService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FavoritosViewModel _favorito)
        {
            try
            {
                var produto = await _service.Save(_favorito);
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return Ok(new ReturnView() { Object = null, Message = ex.Message, Status = false });
            }

        }

        [HttpGet("Favoritos")]
        public async Task<IActionResult> GetFavoritos()
        {
            try
            {
                var produto = await _service.GetFavoritos();
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return Ok(new ReturnView() { Object = null, Message = ex.Message, Status = false });
            }

        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var produto = await _service.Delete(id);
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return Ok(new ReturnView() { Object = null, Message = ex.Message, Status = false });
            }

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetId(int id)
        {
            try
            {
                var produto = await _service.GetId(id);
                return Ok(produto);
            }
            catch (Exception ex)
            {
                 return Ok(new ReturnView() { Object = null, Message = ex.Message, Status = false });
            }
        }
        [HttpGet("{favorito}")]
        public async Task<IActionResult> Get(bool? favorito)
        {
            try
            {
                var produto = await _service.Get(favorito);
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return Ok(new ReturnView() { Object = null, Message = ex.Message, Status = false });
            }
        }
    }
}
