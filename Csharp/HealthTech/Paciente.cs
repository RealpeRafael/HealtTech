
namespace HealtTech
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using CsvHelper;
    using CsvHelper.Configuration;
    using System.Globalization;

    public class Paciente
    {
        public string ID { get; set; }
        public string Nombre { get; set; }
        public string Edad { get; set; }
        public string Diagnostico { get; set; }
        public string EspecialidadAsignada { get; set; }
        public string CamaAsignada { get; set; }
        public string FechaIngreso { get; set; }
        public string HistorialMedico { get; set; }
        private string ruta;

        public Paciente(string id)
        {
            ID = id;
            ruta = @"C:\Users\Luis Angel Seoanes O\OneDrive\Escritorio\HealtTech\Csharp\Archivos\pacientes.csv";

            // Leer el archivo CSV con CsvHelper
            using (var reader = new StreamReader(ruta))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var pacientes = csv.GetRecords<PacienteData>().ToList();

                // Buscar el paciente por ID
                var paciente = pacientes.FirstOrDefault(p => p.ID == ID);

                if (paciente != null)
                {
                    // Asignar los valores a los atributos de la instancia
                    Nombre = paciente.Nombre;
                    Edad = paciente.Edad;
                    Diagnostico = paciente.Diagnostico;
                    EspecialidadAsignada = paciente.EspecialidadAsignada;
                    CamaAsignada = paciente.CamaAsignada;
                    FechaIngreso = paciente.FechaIngreso;
                    HistorialMedico = paciente.HistorialMedico;
                }
            }
        }

        public void ConsultarPaciente()
        {
            // Mostrar la información del paciente
            if (!string.IsNullOrEmpty(Nombre))
            {
                Console.WriteLine("Información del Paciente:");
                Console.WriteLine($"ID: {ID}");
                Console.WriteLine($"Nombre: {Nombre}");
                Console.WriteLine($"Edad: {Edad}");
                Console.WriteLine($"Diagnóstico: {Diagnostico}");
                Console.WriteLine($"Especialidad Asignada: {EspecialidadAsignada}");
                Console.WriteLine($"Cama Asignada: {CamaAsignada}");
                Console.WriteLine($"Fecha de Ingreso: {FechaIngreso}");
                Console.WriteLine($"Historial Médico: {HistorialMedico}");
            }
        }

        public void ConsultarHistorial()
        {
            Console.WriteLine(HistorialMedico);
        }

        public void CambiarEspecialidad(string especialidadNueva)
        {
            // Cambiar la especialidad del paciente
            EspecialidadAsignada = especialidadNueva;

            // Leer el archivo CSV
            var pacientes = new List<PacienteData>();
            using (var reader = new StreamReader(ruta))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                pacientes = csv.GetRecords<PacienteData>().ToList();
            }

            // Verificar si el ID existe en el archivo CSV
            var paciente = pacientes.FirstOrDefault(p => p.ID == ID);
            if (paciente != null)
            {
                // Cambiar la especialidad
                paciente.EspecialidadAsignada = especialidadNueva;

                // Guardar el DataFrame actualizado en el archivo CSV
                using (var writer = new StreamWriter(ruta))
                using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    csv.WriteRecords(pacientes);
                }

                Console.WriteLine("La especialidad se cambió con éxito.");
            }
            else
            {
                Console.WriteLine($"No se encontró el ID {ID} en el archivo CSV.");
            }
        }
    }

    // Clase auxiliar para representar los datos del archivo CSV
    public class PacienteData
    {
        public string ID { get; set; }
        public string Nombre { get; set; }
        public string Edad { get; set; }
        public string Diagnostico { get; set; }
        public string EspecialidadAsignada { get; set; }
        public string CamaAsignada { get; set; }
        public string FechaIngreso { get; set; }
        public string HistorialMedico { get; set; }
    }

}
