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
        private  readonly IHeroisService _service;

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
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] FiltroHerois filtro)
        {
            try
            {
                var produto = await _service.Get(filtro);
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return Ok(new ReturnView() { Object = null, Message = ex.Message, Status = false });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] FavoritosViewModel _favorito)
        {
            try
            {
                var produto = await _service.Put(_favorito);
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return Ok(new ReturnView() { Object = null, Message = ex.Message, Status = false });
            }

        }
    }
}
