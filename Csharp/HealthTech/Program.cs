using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Diagnostics;
using HealtTech;

namespace HealthTechApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(@"Bienvenido a Helixa, Software de Gestión Hospitalaria, llevando la atención al siguiente nivel
                                   1. Iniciar Sesión 
                                   2. Salir");

                string opcion1 = Console.ReadLine();

                switch (opcion1)
                {
                    case "1":
                        // Ingresar Usuario y Contraseña
                        Console.Write("Ingrese el usuario: ");
                        string usuario = Console.ReadLine();
                        Console.Write("Ingrese la contraseña: ");
                        string contrasena = Console.ReadLine();

                        // Creamos una instancia del objeto USUARIO con los parámetros proporcionados
                        USUARIO sesion = new USUARIO(usuario, contrasena);
                        bool respuestaSesion = sesion.InicioSesion();

                        if (respuestaSesion)
                        {
                            var usuarios = sesion.CargarUsuarios();
                            string tipoCuenta = usuarios[usuario]["Tcuenta"].ToString();

                            // Dependiendo del tipo de cuenta, ejecutamos diferentes opciones
                            if (tipoCuenta == "Recepcionista")
                            {
                                Recepcionista recepcionista = new Recepcionista();

                                while (true)
                                {
                                    Console.Clear();
                                    Console.WriteLine(@"
                                    ¿Qué desea hacer?
                                    1. Ingresar un paciente
                                    2. Consultar disponibilidad de camas
                                    3. Cambiar de especialidad médica a paciente
                                    4. Salir");

                                    string opcion2 = Console.ReadLine();

                                    switch (opcion2)
                                    {
                                        case "1":
                                            recepcionista.IngresarPaciente();
                                            break;
                                        case "2":
                                            recepcionista.ConsultarDisponibilidadCamas();
                                            break;
                                        case "3":
                                            recepcionista.CambiarEspecialidad();
                                            break;
                                        case "4":
                                            return;
                                    }
                                }
                            }
                            else if (tipoCuenta == "Administrador")
                            {
                                // El administrador también puede tener un constructor que lo inicializa
                                ADMINISTRADOR admin = new ADMINISTRADOR(@"C:\Users\Luis Angel Seoanes O\OneDrive\Escritorio\HealtTech\Csharp\Archivos\camas.csv");

                                while (true)
                                {
                                    Console.Clear();
                                    Console.WriteLine(@"
                                    ¿Qué desea hacer?
                                    1. Mostrar reportes de ocupación
                                    2. Calcular ingresos proyectados
                                    3. Salir");

                                    string opcion2 = Console.ReadLine();

                                    switch (opcion2)
                                    {
                                        case "1":
                                            admin.ReporteOcupacion();
                                            break;
                                        case "2":
                                            // Ejecutar el script de Python para graficar los ingresos proyectados
                                            admin.GananciasEsperadas();
                                            break;
                                        case "3":
                                            return;
                                    }
                                }
                            }
                            else if (tipoCuenta == "PersonalMedico")
                            {
                                // Obtener información del usuario médico desde el CSV
                                var dfUsuarios = File.ReadAllLines("Archivos/Usuarios.csv")
                                    .Skip(1)
                                    .Select(line => line.Split(','))
                                    .Where(columns => columns[0] == usuario)
                                    .FirstOrDefault();

                                if (dfUsuarios != null)
                                {
                                    string id = dfUsuarios[0];
                                    string nombre = dfUsuarios[1];
                                    string especialidad = dfUsuarios[2];

                                    // Crear una instancia de PersonalMedico usando el constructor
                                    PersonalMedico medico = new PersonalMedico(id, nombre, especialidad);

                                    while (true)
                                    {
                                        Console.Clear();
                                        Console.WriteLine(@"
                                        ¿Qué desea hacer?
                                        1. Solicitar estudios adicionales
                                        2. Consultar información de paciente
                                        3. Registrar información de un paciente
                                        4. Salir");

                                        string opcion2 = Console.ReadLine();

                                        switch (opcion2)
                                        {
                                            case "1":
                                                medico.SolicitarEstudios();
                                                break;
                                            case "2":
                                                medico.ConsultarInformacionPaciente();
                                                break;
                                            case "3":
                                                Console.Write("Ingrese el ID del paciente: ");
                                                string idPaciente = Console.ReadLine();
                                                Console.Write("Ingrese las notas médicas: ");
                                                string notasMedicas = Console.ReadLine();
                                                Console.Write("Ingrese el diagnóstico del paciente: ");
                                                string diagnostico = Console.ReadLine();
                                                Console.Write("Ingrese la especialidad asignada: ");
                                                string especialidadAsignada = Console.ReadLine();
                                                medico.RegistrarInformacion(idPaciente, notasMedicas, diagnostico, especialidadAsignada);
                                                break;
                                            case "4":
                                                return;
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Usuario no encontrado.");
                                }
                            }
                        }
                        break;

                    case "2":
                        return;
                }
            }
        }
    }
}
