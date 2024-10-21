
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
            Console.Write("Ingrese el ID del paciente a consultar: ");
            string idPaciente = Console.ReadLine();

            Paciente paciente = new Paciente(idPaciente);
            paciente.ConsultarPaciente();
        }

        // Método para registrar información médica en el archivo CSV
        public void RegistrarInformacion(string idPaciente, string notasMedicas, string diagnostico, string especialidadAsignada)
        {
            var pacientes = LeerPacientes();

            var paciente = pacientes.FirstOrDefault(p => p.ID == idPaciente);
            if (paciente != null)
            {
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
                Console.WriteLine("Paciente no encontrado.");
            }
        }

        // Método para solicitar estudios
        public void SolicitarEstudios()
        {
            Console.Write("Ingrese el ID del paciente que desea solicitar estudios: ");
            string idPaciente = Console.ReadLine();

            Console.Write("Ingrese el ID del estudio a realizar: ");
            string idEstudio = Console.ReadLine();

            // Crear una instancia de Estudio usando los parámetros necesarios
            Estudio estudio = new Estudio(idPaciente, idEstudio);

            // Llamar al método SolicitarEstudio en la instancia de Estudio
            estudio.SolicitarEstudio(idPaciente, idEstudio);
        }

        // Método para leer el archivo de pacientes y devolver una lista de pacientes
        private List<Paciente> LeerPacientes()
        {
            List<Paciente> pacientes = new List<Paciente>();

            using (var reader = new StreamReader(RutaPacientes))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var registros = csv.GetRecords<Paciente>().ToList();
                pacientes.AddRange(registros);
            }

            return pacientes;
        }

        // Método para guardar la lista de pacientes actualizada en el archivo CSV
        private void GuardarPacientes(List<Paciente> pacientes)
        {
            using (var writer = new StreamWriter(RutaPacientes))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.WriteRecords(pacientes);
            }
        }
    }

}
