using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IProAgilRepository _repo;
        public EventoController(IProAgilRepository repo)
        {
            _repo = repo;
        }

        // GET api/evento
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results =await _repo.GetAllEventoAsync(true);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Bando de dados Falhou!");
            }
            
        }
        [HttpGet("{EventoId}")]
        public async Task<IActionResult> Get(int EventoId)
        {
            try
            {
                var results =await _repo.GetAllEventoAsyncById(EventoId,true);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Bando de dados Falhou!");
            }
            
        }
        [HttpGet("getByTema/{Tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                var results =await _repo.GetAllEventoAsyncByTema(tema,true);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Bando de dados Falhou!");
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                _repo.Add(model);
                if( await _repo.SaveChangesAsync())
                {
                    return Created($"/api/evento/{model.Id}",model) ;
                }
                
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Bando de dados Falhou!");
            }
            
            return BadRequest();
        }
        [HttpPut("{EventoId}")]
        public async Task<IActionResult> Put(int EventoId,Evento model)
        {
            try
            {
                var evento =  await _repo.GetAllEventoAsyncById(EventoId,false);
                
                if (evento ==null) return NotFound();
                
                _repo.Update(model);
                
                if( await _repo.SaveChangesAsync())
                {
                    return Created($"/api/evento/{model.Id}",model) ;
                }
                
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Bando de dados Falhou!");
            }
            
            return BadRequest();
        }
        [HttpDelete("{EventoId}")]
        public async Task<IActionResult> Delete(int EventoId)
        {
            try
            {
                var evento =  await _repo.GetAllEventoAsyncById(EventoId,false);
                
                if (evento ==null) return NotFound();
                
                _repo.Delete(evento);
                
                if( await _repo.SaveChangesAsync())
                {
                    return Ok();
                }
                
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Bando de dados Falhou!");
            }
            
            return BadRequest();
        }
    }
}