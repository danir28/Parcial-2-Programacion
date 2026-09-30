using AccesoDatos.Data;
using AccesoDatos.Models;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Repositories
{
    public class CancionRepository : GenericRepository<Cancion>
    {
        public CancionRepository(AplicacionDbContext context) : base(context) {}

        public List<Cancion> ObtenerMasLargas()
        {
            if (!_context.Canciones.Any())
                return new List<Cancion>();

            int duracionMaxima = _context.Canciones.Max(c => c.Duracion);

            return _context.Canciones
                .AsNoTracking()
                .Include(c => c.Artista)
                .Where(c => c.Duracion == duracionMaxima)
                .ToList();
        }

        public int ContarCanciones()
        {
            return _context.Canciones.Count();
        }
        public List<Cancion> OrdenarAlfabeticamente()
        {
            return _context.Canciones
                .AsNoTracking()
                .Include(c => c.Artista)
                .OrderBy(c => c.Titulo)
                .ToList();
        }
        public bool ExistenCanciones()
        {
            return _context.Canciones.Any();
        }
    }
}