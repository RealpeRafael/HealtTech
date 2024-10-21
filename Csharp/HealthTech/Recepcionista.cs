using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealtTech
{
    using System;

    public class Recepcionista
    {
        // Método para ingresar un nuevo paciente
        public void IngresarPaciente()
        {
            // Crear el objeto Ingreso, esto debe haber sido previamente definido
            Ingreso ingreso = new Ingreso();
            ingreso.RegistrarIngreso(); // Llamar al método para registrar el ingreso
        }

        // Método para consultar la disponibilidad de las camas
        public void ConsultarDisponibilidadCamas()
        {
            Cama.ConsultarDisponibilidadGeneral(); // Llamamos al método estático de la clase Cama
        }

        // Método para cambiar la especialidad de un paciente
        public void CambiarEspecialidad()
        {
            // Pedir el ID del paciente
            Console.WriteLine("Ingrese el ID del paciente que cambiará de especialidad:");
            string ID = Console.ReadLine();

            // Pedir la nueva especialidad
            Console.WriteLine("Ingrese la nueva especialidad:");
            string nuevaEspecialidad = Console.ReadLine();

            // Crear el objeto Paciente y cambiar la especialidad
            Paciente paciente = new Paciente(ID);
            paciente.CambiarEspecialidad(nuevaEspecialidad); // Método de la clase Paciente
        }
    }

}
