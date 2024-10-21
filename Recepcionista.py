from ingreso import Ingreso
from cama import Cama
from paciente import Paciente

class Recepcionista:
    def IngresarPaciente():
        PACIENTE = Ingreso()
        Ingreso.registrar_ingreso(PACIENTE)

    def ConsultarDisponibilidadCamas():
        Cama.consultarDisponibilidadGeneral()
    
    def CambiarEspecialidad():
        ID = input("Ingrese el ID del paciente que cambiara de especialidad: ")
        nuevaEspecialidad = input("Ingrese la nueva especialidad: ")
        PACIENTE = Paciente(ID)
        PACIENTE.cambiar_especialidad(nuevaEspecialidad)

    