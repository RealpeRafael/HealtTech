import pandas as pd
from paciente import Paciente
from estudio import Estudios

class PersonalMedico:
    def __init__(self, id_personal, nombre, especialidad):
        self.id_personal = id_personal
        self.nombre = nombre
        self.especialidad = especialidad
        self.ruta = "Archivos/pacientes.csv"

    def consultarInformacionPaciente(self):
        """Consulta la información de un paciente dado su ID."""
        IDPaciente = input("Ingrese el ID del paciente a consultar: ")
        paciente = Paciente(IDPaciente)
        paciente.consultarPaciente()

    def registrarInformacion(self, id_paciente, notas_medicas, diagnostico, especialidad_asignada):
            """Registra notas médicas, diagnóstico y especialidad asignada para un paciente en el archivo pacientes.csv."""    
            df = pd.read_csv(self.ruta)
            df['ID'] = df['ID'].astype(str)

            # Verificar si el paciente existe
            if id_paciente in df['ID'].values:
                # Añadir la nueva nota médica al historial existente
                df.loc[df['ID'] == id_paciente, 'HistorialMedico'] = df.loc[df['ID'] == id_paciente, 'HistorialMedico'].astype(str) + "; " + notas_medicas
                # Actualizar el diagnóstico y la especialidad asignada
                df.loc[df['ID'] == id_paciente, 'Diagnóstico'] = diagnostico
                df.loc[df['ID'] == id_paciente, 'EspecialidadAsignada'] = especialidad_asignada
                
                # Guardar el DataFrame actualizado en el archivo CSV
                df.to_csv(self.ruta, index=False)
                print("Información médica, diagnóstico y especialidad registrada correctamente.")
            else:
                print("Paciente no encontrado.")
    
    def solicitarEstudios(self):
        IDpaciente = input("Ingrese el ID del paciente que desea solicitar estudios: ")
        IDestudio = input("Ingrese el ID del estudio a realizar: ")
        Estudios.solicitarEstudio(IDpaciente, IDestudio)