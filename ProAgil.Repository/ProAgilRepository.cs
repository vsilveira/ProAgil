using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgiRepository
    {
        public ProAgilContext _context { get; }

        public ProAgilRepository(ProAgilContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
           _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) >0 ;
        }

        public async Task<Evento[]> GetAllEventoAsyncBy(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(c => c.Lotes)
                .Include(c => c.RedeSociais);

             if (includePalestrantes)
             {
                 query = query
                 .Include(pe=> pe.PalestrantesEventos)
                 .ThenInclude(p => p.Palestrante);
             }   
             query = query.OrderByDescending(c => c.DataEvento);
             
             return await query.ToArrayAsync();
        }

        public async Task<Evento> GetAllEventoAsyncById(int EventoId, bool includePalestrantes = false)
        {
             IQueryable<Evento> query = _context.Eventos
                .Include(c => c.Lotes)
                .Include(c => c.RedeSociais);

             if (includePalestrantes)
             {
                 query = query
                 .Include(pe=> pe.PalestrantesEventos)
                 .ThenInclude(p => p.Palestrante);
             }   
             query = query.OrderByDescending(c => c.DataEvento)
                        .Where(c => c.Id == EventoId);
                    
             return await query.FirstOrDefaultAsync();
        }

        public async Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(c => c.Lotes)
                .Include(c => c.RedeSociais);

             if (includePalestrantes)
             {
                 query = query
                 .Include(pe=> pe.PalestrantesEventos)
                 .ThenInclude(p => p.Palestrante);
             }   
             query = query.OrderByDescending(c => c.DataEvento)
                        .Where(c => c.Tema.ToLower().Contains(tema.ToLower()));
                    
             return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetAllPalestranteAsyncById(int PaletranteId, bool includeEvento = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(c => c.RedeSociais);

             if (includeEvento)
             {
                 query = query
                 .Include(pe=> pe.PalestrantesEventos)
                 .ThenInclude(e => e.Evento);
             }   
             query = query.OrderBy(p => p.Nome)
                        .Where(p => p.Id == PaletranteId);
                    
             return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestranteAsyncByName(string name, bool includeEvento = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(c => c.RedeSociais);

            if (includeEvento)
            {
                query = query
                .Include(pe=> pe.PalestrantesEventos)
                .ThenInclude(e => e.Evento);
            }   
            query = query.Where(p => p.Nome.ToLower().Contains(name.ToLower()));
            
            return await query.ToArrayAsync();
        }
    }
}