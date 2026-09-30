namespace AccesoDatos.Models
{
   public class Artista
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public List<Cancion> Canciones { get; set; } = new();
    }
}
