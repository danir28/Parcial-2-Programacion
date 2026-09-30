using AccesoDatos.Data;
using AccesoDatos.Repositories;

var context = new AplicacionDbContext();

// Acá vas a instanciar tus repositorios específicos, ej:
// var entidadRepository = new GenericRepository<Entidad>(context);

bool salir = false;

while (!salir)
{
    Console.WriteLine("=== MENÚ ===");
    Console.WriteLine("1. Opción 1");
    Console.WriteLine("2. Opción 2");
    Console.WriteLine("0. Salir");
    Console.Write("Elegí una opción: ");
    string opcion = Console.ReadLine();

    switch (opcion)
    {
        case "1":
            // TODO
            break;
        case "2":
            // TODO
            break;
        case "0":
            salir = true;
            break;
        default:
            Console.WriteLine("Opción inválida.");
            break;
    }
}
