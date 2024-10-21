using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealtTech
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using CsvHelper;
    using CsvHelper.Configuration;
    using System.Globalization;
    using System.Linq;

    public class USUARIO
    {
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        private string rutaUsuarios = @"C:\Users\Luis Angel Seoanes O\OneDrive\Escritorio\HealtTech\Csharp\Archivos\Usuarios.csv"; // Ruta del archivo CSV

        public USUARIO(string usuario, string contrasena)
        {
            Usuario = usuario;
            Contrasena = contrasena;
        }

        // Cargar usuarios desde el archivo CSV
        public Dictionary<string, Dictionary<string, object>> CargarUsuarios()
        {
            var usuarios = new Dictionary<string, Dictionary<string, object>>();
            if (File.Exists(rutaUsuarios))
            {
                using (var reader = new StreamReader(rutaUsuarios))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    csv.Read();
                    csv.ReadHeader();

                    while (csv.Read())
                    {
                        var usuarioData = new Dictionary<string, object>
                    {
                        { "ID", csv.GetField<int>("ID") },
                        { "Contrasena", csv.GetField<string>("Contrasena") },
                        { "Tcuenta", csv.GetField<string>("Tcuenta") }
                    };

                        usuarios[csv.GetField<string>("Usuario")] = usuarioData;
                    }
                }
            }
            return usuarios;
        }

        // Verificar inicio de sesión
        public bool InicioSesion()
        {
            var usuarios = CargarUsuarios();
            if (usuarios.ContainsKey(Usuario))
            {
                if (usuarios[Usuario]["Contrasena"].ToString() == Contrasena)
                {
                    Console.WriteLine("Inicio de sesión exitoso.");
                    return true;
                }
                else
                {
                    Console.WriteLine("Contraseña incorrecta.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("El usuario no existe.");
                return false;
            }
        }

        // Guardar los usuarios en el archivo CSV
        public void GuardarUsuarios(Dictionary<string, Dictionary<string, object>> usuarios)
        {
            using (var writer = new StreamWriter(rutaUsuarios))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.WriteField("ID");
                csv.WriteField("Usuario");
                csv.WriteField("Contrasena");
                csv.WriteField("Tcuenta");
                csv.NextRecord();

                foreach (var usuario in usuarios)
                {
                    csv.WriteField(usuario.Value["ID"]);
                    csv.WriteField(usuario.Key);
                    csv.WriteField(usuario.Value["Contrasena"]);
                    csv.WriteField(usuario.Value["Tcuenta"]);
                    csv.NextRecord();
                }
            }
        }

        // Crear una nueva cuenta de usuario
        public bool CrearCuenta(string tipoCuenta)
        {
            var usuarios = CargarUsuarios();
            if (usuarios.ContainsKey(Usuario))
            {
                Console.WriteLine("El usuario ya existe.");
                return false;
            }
            else
            {
                // Determinar el nuevo ID basado en el ID más alto actual
                int nuevoId = usuarios.Count == 0 ? 1 : usuarios.Values.Max(u => (int)u["ID"]) + 1;

                // Agregar el nuevo usuario
                usuarios[Usuario] = new Dictionary<string, object>
            {
                { "ID", nuevoId },
                { "Contrasena", Contrasena },
                { "Tcuenta", tipoCuenta }
            };

                // Guardar los usuarios actualizados
                GuardarUsuarios(usuarios);

                Console.WriteLine($"Usuario {Usuario} creado con éxito con ID {nuevoId}.");
                return true;
            }
        }
    }

}
