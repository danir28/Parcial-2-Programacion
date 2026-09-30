using AccesoDatos.Data;
using AccesoDatos.Models;
using AccesoDatos.Repositories;
using Microsoft.EntityFrameworkCore;

var context = new AplicacionDbContext();
context.Database.Migrate();
IGenericRepository<Artista> artistaRepository = new GenericRepository<Artista>(context);
CancionRepository cancionRepository = new CancionRepository(context);

bool salir = false;

while (!salir)
{
    Console.WriteLine();
    Console.WriteLine("=== MENÚ ===");
    Console.WriteLine("1. Alta Artista");
    Console.WriteLine("2. Alta Canción");
    Console.WriteLine("3. Ver canciones");
    Console.WriteLine("4. Mostrar canciones mas largas");
    Console.WriteLine("5. Cantidad total de canciones");
    Console.WriteLine("6. Mostrar canciones ordenadas alfabeticamente por titulo");
    Console.WriteLine("7. Verificar si existen canciones registradas");
    Console.WriteLine("0. Salir");

    Console.Write("Elegí una opción: ");
    string opcion = Console.ReadLine() ?? string.Empty;

    switch (opcion)
    {
        case "1":
            AgregarArtista();
            break;
        case "2":
            AgregarCancion();
            break;
        case "3":
            VerCanciones();
            break;
        case "4":
            CancionesMasLargas();
            break;
        case "5":
            CantidadCanciones();
            break;
        case "6":
            CancionesOrdenadasAlfabeticamente();
            break;
        case "7":
            VerificarSiExisteUnaCancion();
            break;
        case "0":
            salir = true;
            break;
        default:
            Console.WriteLine("Opción inválida.");
            break;
    }
}

void AgregarArtista()
{
    string nombre = LeerTextoObligatorio("Ingrese nombre del artista: ");
    string apellido = LeerTextoObligatorio("Ingrese apellido del artista: ");

    Artista artista = new Artista { Nombre = nombre, Apellido = apellido };
    artistaRepository.agregar(artista);

    Console.WriteLine($"Artista '{nombre} {apellido}' registrado con Id {artista.Id}.");
}

void AgregarCancion()
{
    List<Artista> artistas = artistaRepository.ObtenerTodosCon();
    if (artistas.Count == 0)
    {
        Console.WriteLine("No hay artistas registrados. Primero dé de alta un artista (opción 1).");
        return;
    }

    string titulo = LeerTextoObligatorio("Ingrese título de la canción: ");
    int duracion = LeerEnteroPositivo("Ingrese duración en segundos: ");

    MostrarArtistas(artistas);
    Artista? artista = null;
    while (artista == null)
    {
        int artistaId = LeerEnteroPositivo("Ingrese el Id del artista: ");
        artista = artistaRepository.ObtenerPorId(artistaId);
        if (artista == null)
            Console.WriteLine("No existe un artista con ese Id. Intente de nuevo.");
    }

    Cancion cancion = new Cancion { Titulo = titulo, Duracion = duracion, ArtistaId = artista.Id };
    cancionRepository.agregar(cancion);

    Console.WriteLine($"Canción '{titulo}' registrada para {artista.Nombre} {artista.Apellido}.");
}

void VerCanciones()
{
    List<Cancion> canciones = cancionRepository.ObtenerTodosCon(nameof(Cancion.Artista));
    MostrarCanciones(canciones, "Canciones registradas");
}

void CancionesMasLargas()
{
    List<Cancion> canciones = cancionRepository.ObtenerMasLargas();
    MostrarCanciones(canciones, "Canción/es más larga/s");
}

void CantidadCanciones()
{
    int cantidad = cancionRepository.ContarCanciones();
    Console.WriteLine($"Cantidad total de canciones: {cantidad}");
}

void CancionesOrdenadasAlfabeticamente()
{
    List<Cancion> canciones = cancionRepository.OrdenarAlfabeticamente();
    MostrarCanciones(canciones, "Canciones ordenadas alfabéticamente");
}

void VerificarSiExisteUnaCancion()
{
    if (cancionRepository.ExistenCanciones())
        Console.WriteLine("Sí, existen canciones registradas.");
    else
        Console.WriteLine("No hay canciones registradas.");
}

void MostrarArtistas(List<Artista> artistas)
{
    Console.WriteLine("--- Artistas registrados ---");
    foreach (Artista a in artistas)
        Console.WriteLine($"{a.Id}. {a.Nombre} {a.Apellido}");
}

void MostrarCanciones(List<Cancion> canciones, string encabezado)
{
    if (canciones.Count == 0)
    {
        Console.WriteLine("No hay canciones registradas.");
        return;
    }

    Console.WriteLine($"--- {encabezado} ---");
    foreach (Cancion c in canciones)
        Console.WriteLine($"{c.Id}. {c.Titulo} - {c.Duracion} seg - {c.Artista.Nombre} {c.Artista.Apellido}");
}

string LeerTextoObligatorio(string mensaje)
{
    while (true)
    {
        Console.Write(mensaje);
        string texto = (Console.ReadLine() ?? string.Empty).Trim();
        if (texto.Length > 0)
            return texto;
        Console.WriteLine("El valor no puede estar vacío.");
    }
}

int LeerEnteroPositivo(string mensaje)
{
    while (true)
    {
        Console.Write(mensaje);
        if (int.TryParse(Console.ReadLine(), out int numero) && numero > 0)
            return numero;
        Console.WriteLine("Debe ingresar un número entero mayor a cero.");
    }
}
