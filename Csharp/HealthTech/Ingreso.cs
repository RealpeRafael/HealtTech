namespace HealtTech
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class Ingreso
    {
        public string ID { get; set; }
        public string FechaIngreso { get; set; }
        public string MotivoIngreso { get; set; }
        public string CamaAsignada { get; set; }
        public string Nombre { get; set; }
        public string Edad { get; set; }

        public Ingreso()
        {
            // Validación de entrada del ID
            Console.WriteLine("Ingrese el documento de la persona: ");
            ID = Console.ReadLine();
            while (string.IsNullOrEmpty(ID) || !ID.All(char.IsDigit))
            {
                Console.WriteLine("ID inválido. Ingrese el documento de la persona (solo números): ");
                ID = Console.ReadLine();
            }

            // Validación de fecha
            Console.WriteLine("Ingrese una fecha de ingreso (formato yyyy-mm-dd): ");
            FechaIngreso = Console.ReadLine();
            while (!DateTime.TryParse(FechaIngreso, out _))
            {
                Console.WriteLine("Fecha inválida. Ingrese una fecha de ingreso (formato yyyy-mm-dd): ");
                FechaIngreso = Console.ReadLine();
            }

            // Ingreso del motivo
            Console.WriteLine("Ingrese la enfermedad o el motivo de ingreso: ");
            MotivoIngreso = Console.ReadLine();

            // Validación de cama asignada
            Console.WriteLine("Ingrese la cama que se le va a asignar (Escriba [AUTOMATICO] para asignar cualquiera disponible): ");
            CamaAsignada = Console.ReadLine();
            if (CamaAsignada == "AUTOMATICO")
            {
                Console.WriteLine("Ingrese el tipo de cama que desea (Individual o Compartida): ");
                string tipoCama = Console.ReadLine();
                CamaAsignada = Cama.BuscarCamaPorTipo(tipoCama);
            }
            else
            {
                var cama = new Cama(CamaAsignada);
            }

            // Ingreso del nombre
            Console.WriteLine("Ingrese el nombre del paciente: ");
            Nombre = Console.ReadLine();

            // Validación de edad
            Console.WriteLine("Ingrese la edad del paciente: ");
            Edad = Console.ReadLine();
            while (!int.TryParse(Edad, out _))
            {
                Console.WriteLine("Edad inválida. Ingrese la edad del paciente (números solamente): ");
                Edad = Console.ReadLine();
            }
        }

        // Registrar el ingreso del paciente en el archivo pacientes.csv
        public void RegistrarIngreso()
        {
            string rutaPacientes = @"C:\Users\Luis Angel Seoanes O\OneDrive\Escritorio\HealtTech\Csharp\Archivos\pacientes.csv";

            // Estructura del nuevo registro
            var nuevoPaciente = new Dictionary<string, string>
        {
            { "ID", ID },
            { "Nombre", Nombre },
            { "Edad", Edad },
            { "Diagnóstico", "" }, // Vacío por el momento
            { "EspecialidadAsignada", "" }, // Vacío por el momento
            { "CamaAsignada", CamaAsignada },
            { "FechaIngreso", FechaIngreso },
            { "HistorialMedico", "" } // Vacío por el momento
        };

            // Verificar si el archivo ya existe y añadir el encabezado si no existe
            bool archivoExiste = File.Exists(rutaPacientes);

            // Guardar los datos en el archivo CSV
            using (StreamWriter writer = new StreamWriter(rutaPacientes, true))
            {
                var fieldnames = new List<string> { "ID", "Nombre", "Edad", "Diagnóstico", "EspecialidadAsignada", "CamaAsignada", "FechaIngreso", "HistorialMedico" };

                // Escribir el encabezado solo si el archivo es nuevo
                if (!archivoExiste)
                {
                    writer.WriteLine(string.Join(",", fieldnames));
                }

                // Escribir el nuevo registro de paciente
                writer.WriteLine(string.Join(",", nuevoPaciente.Values));
            }

            Console.WriteLine($"Ingreso registrado para el paciente {Nombre} con ID {ID} en la cama {CamaAsignada}.");
        }

        // Registrar la fecha de alta del paciente
        public void RegistrarAlta(string fechaAlta)
        {
            Console.WriteLine($"Alta registrada para el paciente {Nombre} el {fechaAlta}");
            LiberarCama();
        }

        // Liberar la cama asignada al paciente
        public void LiberarCama()
        {
            // Liberar la cama en el archivo camas.csv
            var cama = new Cama(CamaAsignada);
            cama.LiberarCama();
            Console.WriteLine($"La cama {CamaAsignada} ha sido liberada.");
        }
    }


}