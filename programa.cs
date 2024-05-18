using System;
using System.Collections.Generic;
using System.Linq;

public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string ApellidoPaterno { get; set; }
    public string ApellidoMaterno { get; set; }
    public string Genero { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public string Correo { get; set; }
    public string Contraseña { get; set; }
    public string TipoUsuario { get; set; } 
    public string Domicilio { get; set; }
    public string NumeroContacto { get; set; }
    public List<SolicitudServicio> Solicitudes { get; set; }
}

public class Servicio
{
    public int Id { get; set; }
    public string NombreServicio { get; set; }
    public decimal ValorEstadarServicio { get; set; }
    public decimal RetencionAdministrador { get; set; }
}

public class Proveedor : Usuario
{
    public string TipoServicio { get; set; }
    public int Calificacion { get; set; }
    public List<SolicitudServicio> SolicitudesRecibidas { get; set; }
}

public class SolicitudServicio
{
    public int Id { get; set; }
    public string Estado { get; set; }
    public Proveedor Proveedor { get; set; }
    public string Comentario { get; set; }
}

public class BaseDeDatos
{
    public List<Usuario> Usuarios { get; set; }
    public List<Servicio> Servicios { get; set; }
    public List<Proveedor> Proveedores { get; set; }
    public List<SolicitudServicio> Solicitudes { get; set; }

    public BaseDeDatos()
    {
        
        InicializarDatosFicticios();
    }

    private void InicializarDatosFicticios()
    {
        
        var usuario1 = new Usuario
        {
            Id = 1,
            Nombre = "Juan",
            ApellidoPaterno = "Perez",
            Genero = "Masculino",
            FechaNacimiento = new DateTime(1990, 1, 1),
            Correo = "juan@gmail.com",
            Contraseña = "contraseña",
            TipoUsuario = "Cliente",
            Domicilio = "Calle 123",
            NumeroContacto = "123456789",
            Solicitudes = new List<SolicitudServicio>()
        };

        var usuario2 = new Proveedor
        {
            Id = 2,
            Nombre = "Maria",
            ApellidoPaterno = "Gomez",
            Genero = "Femenino",
            FechaNacimiento = new DateTime(1985, 5, 10),
            Correo = "maria@gmail.com",
            Contraseña = "contraseña",
            TipoUsuario = "Proveedor",
            Domicilio = "Avenida 456",
            NumeroContacto = "987654321",
            TipoServicio = "Limpieza",
            Calificacion = 4,
            SolicitudesRecibidas = new List<SolicitudServicio>()
        };

        var servicio1 = new Servicio
        {
            Id = 1,
            NombreServicio = "Limpieza del hogar",
            ValorEstadarServicio = 50.00m,
            RetencionAdministrador = 5.00m
        };

        Usuarios = new List<Usuario> { usuario1, usuario2 };
        Servicios = new List<Servicio> { servicio1 };
        Proveedores = new List<Proveedor> { (Proveedor)usuario2 };
        Solicitudes = new List<SolicitudServicio>();
    }

    public void AgregarUsuario(Usuario usuario)
    {
        Usuarios.Add(usuario);
    }

    public void AgregarServicio(Servicio servicio)
    {
        Servicios.Add(servicio);
    }

    public void AgregarProveedor(Proveedor proveedor)
    {
        Proveedores.Add(proveedor);
    }

    public void AgregarSolicitud(SolicitudServicio solicitud)
    {
        Solicitudes.Add(solicitud);
    }

    public Usuario AutenticarUsuario(string correo, string contraseña)
    {
        return Usuarios.FirstOrDefault(u => u.Correo == correo && u.Contraseña == contraseña);
    }

    public Proveedor ObtenerProveedorPorCorreo(string correo)
    {
        return Proveedores.FirstOrDefault(p => p.Correo == correo);
    }
}

class Program
{
    static BaseDeDatos baseDeDatos = new BaseDeDatos();
    static Usuario usuarioAutenticado;

    static void Main()
    {
        Console.WriteLine("Bienvenido a HOMEHELPY");

        while (true)
        {
            Console.WriteLine("\nSeleccione una opción:");
            Console.WriteLine("1. Registro de Usuario");
            Console.WriteLine("2. Autenticación de Usuario");
            Console.WriteLine("3. Solicitud de Servicio");
            Console.WriteLine("4. Finalizar Servicio (Proveedor)");
            Console.WriteLine("5. Calificar Proveedor (Cliente)");
            Console.WriteLine("6. Salir");

            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    RegistrarUsuario();
                    break;

                case "2":
                    AutenticarUsuario();
                    break;

                case "3":
                    SolicitarServicio();
                    break;

                case "4":
                    FinalizarServicio();
                    break;

                case "5":
                    CalificarProveedor();
                    break;

                case "6":
                    Console.WriteLine("Hasta luego.");
                    return;

                default:
                    Console.WriteLine("Opción no válida. Inténtelo de nuevo.");
                    break;
            }
        }
    }

    static void RegistrarUsuario()
    {
        Console.WriteLine("\nRegistro de Usuario");

        // Solicita los datos al usuario
        Console.Write("Nombre: ");
        string nombre = Console.ReadLine();
        Console.Write("Apellido Paterno: ");
        string apellidoPaterno = Console.ReadLine();
        Console.Write("Apellido Materno: ");
        string apellidoMaterno = Console.ReadLine();
        Console.Write("Género: ");
        string genero = Console.ReadLine();
        Console.Write("Fecha de Nacimiento (YYYY-MM-DD): ");
        DateTime fechaNacimiento;
        if (!DateTime.TryParse(Console.ReadLine(), out fechaNacimiento))
        {
            Console.WriteLine("Fecha de nacimiento no válida. Inténtelo de nuevo.");
            return;
        }
        Console.Write("Correo: ");
        string correo = Console.ReadLine();
        Console.Write("Contraseña: ");
        string contraseña = Console.ReadLine();
        Console.Write("Tipo de Usuario (Cliente/Proveedor): ");
        string tipoUsuario = Console.ReadLine();
        Console.Write("Domicilio: ");
        string domicilio = Console.ReadLine();
        Console.Write("Número de Contacto: ");
        string numeroContacto = Console.ReadLine();

        // Crea una instancia de Usuario y lo agrega a la base de datos
        Usuario nuevoUsuario;
        if (tipoUsuario.ToLower() == "cliente")
        {
            nuevoUsuario = new Usuario
            {
                Id = baseDeDatos.Usuarios.Count + 1,
                Nombre = nombre,
                ApellidoPaterno = apellidoPaterno,
                ApellidoMaterno = apellidoMaterno,
                Genero = genero,
                FechaNacimiento = fechaNacimiento,
                Correo = correo,
                Contraseña = contraseña,
                TipoUsuario = "Cliente",
                Domicilio = domicilio,
                NumeroContacto = numeroContacto,
                Solicitudes = new List<SolicitudServicio>()
            };
        }
        else if (tipoUsuario.ToLower() == "proveedor")
        {
            Console.Write("Tipo de Servicio: ");
            string tipoServicio = Console.ReadLine();
            nuevoUsuario = new Proveedor
            {
                Id = baseDeDatos.Usuarios.Count + 1,
                Nombre = nombre,
                ApellidoPaterno = apellidoPaterno,
                ApellidoMaterno = apellidoMaterno,
                Genero = genero,
                FechaNacimiento = fechaNacimiento,
                Correo = correo,
                Contraseña = contraseña,
                TipoUsuario = "Proveedor",
                Domicilio = domicilio,
                NumeroContacto = numeroContacto,
                TipoServicio = tipoServicio,
                Calificacion = 0,
                SolicitudesRecibidas = new List<SolicitudServicio>()
            };
        }
        else
        {
            Console.WriteLine("Tipo de usuario no válido. Inténtelo de nuevo.");
            return;
        }

        baseDeDatos.AgregarUsuario(nuevoUsuario);

        Console.WriteLine("Usuario registrado exitosamente.");
    }

    static void AutenticarUsuario()
    {
        Console.WriteLine("\nAutenticación de Usuario");

        // Solicita las  credenciales del usuario
        Console.Write("Correo: ");
        string correo = Console.ReadLine();
        Console.Write("Contraseña: ");
        string contraseña = Console.ReadLine();

        // Autenticacion del usuario usuario
        usuarioAutenticado = baseDeDatos.AutenticarUsuario(correo, contraseña);

        if (usuarioAutenticado != null)
        {
            Console.WriteLine($"¡Bienvenido, {usuarioAutenticado.Nombre}!");
        }
        else
        {
            Console.WriteLine("Autenticación fallida. Verifique las credenciales e intente de nuevo.");
        }
    }

    static void SolicitarServicio()
    {
        Console.WriteLine("\nSolicitud de Servicio");

        // Verifica si el usuario está autenticado
        if (usuarioAutenticado == null)
        {
            Console.WriteLine("Debe autenticarse antes de realizar una solicitud de servicio.");
            return;
        }

        
        
        Console.WriteLine("Solicitud de servicio realizada con éxito.");
    }

    static void FinalizarServicio()
    {
        Console.WriteLine("\nFinalizar Servicio (Proveedor)");

        // Verifica si el usuario está autenticado como proveedor
        if (usuarioAutenticado != null && usuarioAutenticado.TipoUsuario == "Proveedor")
        {
            

            Console.WriteLine("Servicio finalizado y actualizado con éxito.");
        }
        else
        {
            Console.WriteLine("Debe autenticarse como proveedor para finalizar un servicio.");
        }
    }

    static void CalificarProveedor()
    {
        Console.WriteLine("\nCalificar Proveedor (Cliente)");

        // Verifica si el usuario está autenticado como cliente
        if (usuarioAutenticado != null && usuarioAutenticado.TipoUsuario == "Cliente")
        {
           

            Console.WriteLine("Proveedor calificado con éxito.");
        }
        else
        {
            Console.WriteLine("Debe autenticarse como cliente para calificar a un proveedor.");
        }
    }
}

