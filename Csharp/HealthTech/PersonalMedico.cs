namespace HealtTech
{
    using System;
    using System.Linq;
    using System.IO;
    using System.Collections.Generic;
    using System.Text;
    using CsvHelper;
    using CsvHelper.Configuration;
    using System.Globalization;
    using System.Threading;

    public class PersonalMedico
    {
        // Atributos de la clase
        public string IdPersonal { get; set; }
        public string Nombre { get; set; }
        public string Especialidad { get; set; }
        public string RutaPacientes { get; set; }

        // Constructor
        public PersonalMedico(string idPersonal, string nombre, string especialidad)
        {
            this.IdPersonal = idPersonal;
            this.Nombre = nombre;
            this.Especialidad = especialidad;
            this.RutaPacientes = "Archivos/pacientes.csv";  // Ruta de los pacientes
        }

        // Método para consultar la información de un paciente
        public void ConsultarInformacionPaciente()
        {
            try
            {
                Console.Write("Ingrese el ID del paciente a consultar: ");
                string idPaciente = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(idPaciente))
                {
                    Console.WriteLine("Error: El ID del paciente no puede estar vacío.");
                    return;
                }

                Paciente paciente = new Paciente(idPaciente);
                paciente.ConsultarPaciente();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al consultar la información del paciente: {ex.Message}");
            }
        }

        // Método para registrar información médica en el archivo CSV
        public void RegistrarInformacion(string idPaciente, string notasMedicas, string diagnostico, string especialidadAsignada)
        {
            try
            {
                var pacientes = LeerPacientes();

                var paciente = pacientes.FirstOrDefault(p => p.ID == idPaciente);
                if (paciente != null)
                {
                    // Validar entradas de notas y diagnósticos
                    if (string.IsNullOrWhiteSpace(notasMedicas) || string.IsNullOrWhiteSpace(diagnostico))
                    {
                        Console.WriteLine("Error: Las notas médicas y el diagnóstico no pueden estar vacíos.");
                        return;
                    }

                    // Actualizamos el historial médico y la información del paciente
                    paciente.HistorialMedico += "; " + notasMedicas;
                    paciente.Diagnostico = diagnostico;
                    paciente.EspecialidadAsignada = especialidadAsignada;

                    // Guardamos los cambios en el archivo CSV
                    GuardarPacientes(pacientes);

                    Console.WriteLine("Información médica, diagnóstico y especialidad registrada correctamente.");
                }
                else
                {
                    Console.WriteLine("Error: Paciente no encontrado.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar la información médica: {ex.Message}");
            }
        }

        // Método para solicitar estudios
        public void SolicitarEstudios()
        {
            try
            {
                Console.Write("Ingrese el ID del paciente que desea solicitar estudios: ");
                string idPaciente = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(idPaciente))
                {
                    Console.WriteLine("Error: El ID del paciente no puede estar vacío.");
                    return;
                }

                Console.Write("Ingrese el ID del estudio a realizar: ");
                string idEstudio = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(idEstudio))
                {
                    Console.WriteLine("Error: El ID del estudio no puede estar vacío.");
                    return;
                }

                // Crear una instancia de Estudio usando los parámetros necesarios
                Estudio estudio = new Estudio(idPaciente, idEstudio);

                // Llamar al método SolicitarEstudio en la instancia de Estudio
                estudio.SolicitarEstudio(idPaciente, idEstudio);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al solicitar estudios: {ex.Message}");
            }
        }

        // Método para leer el archivo de pacientes y devolver una lista de pacientes
        private List<Paciente> LeerPacientes()
        {
            try
            {
                if (!File.Exists(RutaPacientes))
                {
                    Console.WriteLine($"Error: No se encontró el archivo de pacientes en la ruta: {RutaPacientes}");
                    return new List<Paciente>();
                }

                List<Paciente> pacientes = new List<Paciente>();

                using (var reader = new StreamReader(RutaPacientes))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    var registros = csv.GetRecords<Paciente>().ToList();
                    pacientes.AddRange(registros);
                }

                return pacientes;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Error: El archivo de pacientes no se pudo encontrar.");
                return new List<Paciente>();
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Error: No tienes permisos para acceder al archivo de pacientes.");
                return new List<Paciente>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer el archivo de pacientes: {ex.Message}");
                return new List<Paciente>();
            }
        }

        // Método para guardar la lista de pacientes actualizada en el archivo CSV
        private void GuardarPacientes(List<Paciente> pacientes)
        {
            try
            {
                using (var writer = new StreamWriter(RutaPacientes))
                using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    csv.WriteRecords(pacientes);
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Error: No tienes permisos para escribir en el archivo de pacientes.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error al guardar los pacientes en el archivo CSV: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
            }
        }
    }
}
