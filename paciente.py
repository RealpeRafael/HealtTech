import csv
import pandas as pd


class Paciente:
    def __init__(self, Id):
        self.ID = Id
        self.ruta = "Archivos/pacientes.csv"
        df = pd.read_csv(self.ruta)
        # Buscar la fila donde el ID coincide
        df['ID'] = df['ID'].astype(str)
        self.ID = str(self.ID)

        
        paciente_data = df[df['ID'] == self.ID]
        if not paciente_data.empty:
            # Asignar valores a los atributos de la instancia
            self.nombre = paciente_data.iloc[0]['Nombre']
            self.edad = paciente_data.iloc[0]['Edad']
            self.diagnostico = paciente_data.iloc[0]['Diagnóstico']
            self.especialidad = paciente_data.iloc[0]['EspecialidadAsignada']
            self.cama_asignada = paciente_data.iloc[0]['CamaAsignada']
            self.fecha_ingreso = paciente_data.iloc[0]['FechaIngreso']
            self.historial_medico = paciente_data.iloc[0]['HistorialMedico']

    def consultarPaciente(self):
        """Mostrar la información del paciente de la instancia actual."""
        if self.nombre:
            print(f"Información del Paciente:")
            print(f"ID: {self.ID}")
            print(f"Nombre: {self.nombre}")
            print(f"Edad: {self.edad}")
            print(f"Diagnóstico: {self.diagnostico}")
            print(f"Especialidad Asignada: {self.especialidad}")
            print(f"Cama Asignada: {self.cama_asignada}")
            print(f"Fecha de Ingreso: {self.fecha_ingreso}")
            print(f"Historial Médico: {self.historial_medico}")

    def consultar_historial(self):
        print(self.historial_medico)

    # Método para cambiar la especialidad
    def cambiar_especialidad(self, especialidad_nueva):
        self.especialidad = especialidad_nueva
                
        # Leer el archivo CSV
        df = pd.read_csv(self.ruta)

        # Asegurarse de que ID y la columna 'ID' tienen el mismo tipo
        df['ID'] = df['ID'].astype(str)
        self.ID = str(self.ID)

        # Verificar si el ID existe en el archivo CSV
        if self.ID in df['ID'].values:
            # Localizar la fila del paciente con el ID y cambiar la especialidad
            df.loc[df['ID'] == self.ID, 'EspecialidadAsignada'] = especialidad_nueva
            
            # Guardar el DataFrame actualizado en el archivo CSV
            df.to_csv(self.ruta, index=False)
            print("La especialidad se cambió con éxito.")
        else:
            print(f"No se encontró el ID {self.ID} en el archivo CSV.")

