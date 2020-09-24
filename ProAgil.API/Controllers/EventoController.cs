using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.API.Dtos;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IProAgilRepository _repo;
        private readonly IMapper _mapper;

        public EventoController(IProAgilRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET api/evento
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos =await _repo.GetAllEventoAsync(true);

                var results = _mapper.Map<EventoDto[]>(eventos);

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
                var evento =await _repo.GetAllEventoAsyncById(EventoId,true);

                var results = _mapper.Map<EventoDto>(evento);

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
                var evento =await _repo.GetAllEventoAsyncByTema(tema,true);

                var results = _mapper.Map<EventoDto>(evento);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Bando de dados Falhou!");
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> Post(EventoDto model)
        {
            try
            {
                var evento = _mapper.Map<Evento>(model);
                
                _repo.Add(evento);

                if( await _repo.SaveChangesAsync())
                {
                    return Created($"/api/evento/{evento.Id}",_mapper.Map<EventoDto>(evento)) ;
                }
                
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Bando de dados Falhou!");
            }
            
            return BadRequest();
        }
        [HttpPut("{EventoId}")]
        public async Task<IActionResult> Put(int EventoId,EventoDto model)
        {
            try
            {
                
                var evento =  await _repo.GetAllEventoAsyncById(EventoId,false);
                if (evento ==null) return NotFound();
                
                _mapper.Map(model,evento);
                
                _repo.Update(evento);
                
                if( await _repo.SaveChangesAsync())
                {
                    return Created($"/api/evento/{evento.Id}",_mapper.Map<EventoDto>(evento)) ;
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