namespace HealtTech
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class Cama
    {
        public string NumeroCama { get; set; }
        public string TipoCama { get; set; }
        public bool Ocupacion { get; set; }
        public string Precio { get; set; }

        private string rutaCamas = @"C:\Users\Luis Angel Seoanes O\OneDrive\Escritorio\HealtTech\Csharp\Archivos\camas.csv";

        // Constructor para inicializar una cama específica
        public Cama(string numeroCama)
        {
            NumeroCama = numeroCama;

            // Leer el archivo de camas
            using (var reader = new StreamReader(rutaCamas))
            {
                while (!reader.EndOfStream)
                {
                    var linea = reader.ReadLine().Split(',');
                    var numero = linea[0];
                    if (numero == NumeroCama)
                    {
                        TipoCama = linea[1];
                        Ocupacion = linea[2].ToLower() == "true"; // Convertir a booleano
                        Precio = linea[3];
                    }
                }
            }
        }

        // Método para consultar disponibilidad de una cama específica
        public bool ConsultarDisponibilidadCama()
        {
            if (!Ocupacion)
            {
                Console.WriteLine("La cama se encuentra disponible y se le va a asignar al paciente.");
                return true;
            }
            else
            {
                Console.WriteLine("La cama NO se encuentra disponible, asigne otra cama.");
                return false;
            }
        }

        // Método estático para consultar disponibilidad de todas las camas
        public static void ConsultarDisponibilidadGeneral()
        {
            var rutaCamas = @"C:\Users\Luis Angel Seoanes O\OneDrive\Escritorio\HealtTech\Csharp\Archivos\camas.csv";
            using (var reader = new StreamReader(rutaCamas))
            {
                while (!reader.EndOfStream)
                {
                    var linea = reader.ReadLine().Split(',');
                    var numeroCama = linea[0];
                    var tipoCama = linea[1];
                    var ocupacion = linea[2].ToLower() == "true";
                    var precio = linea[3];

                    if (!ocupacion)
                    {
                        Console.WriteLine($"La cama {numeroCama}, que es {tipoCama}, se encuentra desocupada y tiene un precio de ${precio}.");
                    }
                }
            }
        }

        // Método estático para buscar una cama por tipo
        public static string BuscarCamaPorTipo(string tipoCama)
        {
            var rutaCamas = @"C:\Users\Luis Angel Seoanes O\OneDrive\Escritorio\HealtTech\Csharp\Archivos\camas.csv";
            var lineas = File.ReadAllLines(rutaCamas).ToList();
            for (int i = 1; i < lineas.Count; i++) // Comienza en 1 para saltar el encabezado
            {
                var linea = lineas[i].Split(',');
                if (linea[1] == tipoCama && linea[2].ToLower() == "false")
                {
                    // Marcar la cama como ocupada
                    linea[2] = "true";
                    lineas[i] = string.Join(",", linea);
                    File.WriteAllLines(rutaCamas, lineas);

                    Console.WriteLine($"El paciente fue asignado a la cama N°{linea[0]}.");
                    return linea[0]; // Retornar el número de cama
                }
            }

            Console.WriteLine("No se encontraron camas disponibles de ese tipo.");
            return null;
        }

        // Método para liberar una cama ocupada
        public void LiberarCama()
        {
            if (Ocupacion)
            {
                Ocupacion = false;
                var lineas = File.ReadAllLines(rutaCamas).ToList();
                for (int i = 1; i < lineas.Count; i++) // Comienza en 1 para saltar el encabezado
                {
                    var linea = lineas[i].Split(',');
                    if (linea[0] == NumeroCama)
                    {
                        linea[2] = "false";
                        lineas[i] = string.Join(",", linea);
                        File.WriteAllLines(rutaCamas, lineas);
                        Console.WriteLine($"La cama {NumeroCama} ha sido liberada.");
                        return;
                    }
                }
            }
            else
            {
                Console.WriteLine("La cama ya está liberada.");
            }
        }
    }

}