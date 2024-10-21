namespace HealtTech
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Globalization;
    using CsvHelper;
    using CsvHelper.Configuration;
    using System.Linq;

    public class Estudio
    {
        public string EstudioID { get; set; }
        public string PacienteID { get; set; }
        public string Resultado { get; set; }
        public string FechaSolicitud { get; set; }

        // Rutas de archivos
        private string rutaPacientes = @"C:\Users\Luis Angel Seoanes O\OneDrive\Escritorio\HealtTech\Csharp\Archivos\pacientes.csv";
        private string rutaEstudios = @"C:\Users\Luis Angel Seoanes O\OneDrive\Escritorio\HealtTech\Csharp\Archivos\estudios.csv";
        private string rutaEstudiosRealizados = @"C:\Users\Luis Angel Seoanes O\OneDrive\Escritorio\HealtTech\Csharp\Archivos\EstudiosRealizados.csv";

        // Constructor de la clase Estudio
        public Estudio(string IDpaciente, string IDestudio)
        {
            EstudioID = IDestudio;
            PacienteID = IDpaciente;

            // Leer archivo CSV de estudios realizados para verificar si el estudio ya existe
            using (var reader = new StreamReader(rutaEstudiosRealizados))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var estudiosRealizados = csv.GetRecords<dynamic>().ToList();
                var estudio = estudiosRealizados
                    .FirstOrDefault(e => e.IDPaciente == PacienteID && e.IDEstudio == EstudioID);

                if (estudio != null)
                {
                    // Asignar valores si ya existe
                    Resultado = estudio.Resultado;
                    FechaSolicitud = estudio.FechaSolicitud;
                }
                else
                {
                    Resultado = null;
                    FechaSolicitud = null;
                }
            }
        }

        // Método para solicitar un nuevo estudio
        public void SolicitarEstudio(string id_paciente, string id_estudio)
        {
            // Verificar si el archivo existe
            bool archivoExiste = File.Exists(rutaEstudiosRealizados);

            // Generar los datos del nuevo estudio usando un diccionario
            var nuevoEstudio = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "IDPaciente", id_paciente },
                    { "IDEstudio", id_estudio },
                    { "Resultado", "Pendiente" },
                    { "FechaSolicitud", DateTime.Now.ToString("yyyy-MM-dd") }
                }
            };

            // Leer el archivo de estudios para obtener el nombre del estudio
            using (var reader = new StreamReader(rutaEstudios))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var estudios = csv.GetRecords<dynamic>().ToList();
                var estudioEncontrado = estudios.FirstOrDefault(e => e.ID == id_estudio);

                if (estudioEncontrado != null)
                {
                    Console.WriteLine($"Estudio de tipo '{estudioEncontrado.Nombre}' solicitado para el paciente con ID {id_paciente}.");
                }
                else
                {
                    Console.WriteLine($"No se encontró el estudio con ID {id_estudio}.");
                }
            }

            // Registrar el nuevo estudio en el archivo de estudios realizados
            using (var writer = new StreamWriter(rutaEstudiosRealizados, true))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                if (!archivoExiste)
                {
                    // Escribir el encabezado si el archivo es nuevo
                    csv.WriteHeader<Dictionary<string, object>>();
                    csv.NextRecord();
                }
                // Escribir el nuevo estudio
                csv.WriteRecords(nuevoEstudio);
            }
        }

        // Método para registrar el resultado del estudio
        public void RegistrarResultado(string resultado)
        {
            // Leer los estudios realizados
            var estudiosRealizados = new List<Dictionary<string, object>>();
            using (var reader = new StreamReader(rutaEstudiosRealizados))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                estudiosRealizados = csv.GetRecords<Dictionary<string, object>>().ToList();
            }

            // Actualizar el resultado del estudio solicitado
            foreach (var estudio in estudiosRealizados)
            {
                if (estudio["IDPaciente"].ToString() == PacienteID && estudio["IDEstudio"].ToString() == EstudioID)
                {
                    estudio["Resultado"] = resultado;
                    break;
                }
            }

            // Guardar los cambios de vuelta al archivo CSV
            using (var writer = new StreamWriter(rutaEstudiosRealizados))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.WriteRecords(estudiosRealizados);
            }

            Console.WriteLine("Resultado registrado con éxito.");
        }
    }
}
