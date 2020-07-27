using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace ProAgil.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]

    public class PalestranteController: ControllerBase
    {
        private readonly IProAgilRepository _repo;
        public PalestranteController(IProAgilRepository repo)
        {
            _repo = repo;
        }

        // GET api/paletrante
        [HttpGet("{PalestranteId}")]
        public async Task<IActionResult> Get(int PalestranteId)
        {
            try
            {
                var results =await _repo.GetAllPalestranteAsyncById(PalestranteId,true);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Bando de dados Falhou!");
            }
            
        }
        [HttpGet("getByNome/{Nome}")]
        public async Task<IActionResult> Get(string nome)
        {
            try
            {
                var results =await _repo.GetAllPalestranteAsyncByName(nome,true);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Bando de dados Falhou!");
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> Post(Palestrante model)
        {
            try
            {
                _repo.Add(model);
                if( await _repo.SaveChangesAsync())
                {
                    return Created($"/api/palestrante/{model.Id}",model) ;
                }
                
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Bando de dados Falhou!");
            }
            
            return BadRequest();
        }
        [HttpPut("{PalestranteId}")]
        public async Task<IActionResult> Put(int PalestranteId,Palestrante model)
        {
            try
            {
                var palestrante =  await _repo.GetAllPalestranteAsyncById(PalestranteId,false);
                
                if (palestrante ==null) return NotFound();
                
                _repo.Update(palestrante);
                
                if( await _repo.SaveChangesAsync())
                {
                    return Created($"/api/palestrante/{model.Id}",model) ;
                }
                
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Bando de dados Falhou!");
            }
            
            return BadRequest();
        }
        [HttpDelete("{PalestranteId}")]
        public async Task<IActionResult> Delete(int PalestranteId)
        {
            try
            {
                var evento =  await _repo.GetAllPalestranteAsyncById(PalestranteId,false);
                
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
