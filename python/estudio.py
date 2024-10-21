import pandas as pd
import os
import csv

class Estudios:
    def __init__(self, IDpaciente, IDestudio) -> None:
        self.EstudioID = IDestudio
        self.PacienteID = IDpaciente
        self.ruta = "Archivos/EstudiosRealizados.csv"

        df = pd.read_csv(self.ruta)
        # Buscar la fila donde el ID del paciente y el ID del estudio coinciden
        EstudioRealizado = df[(df['IDpaciente'] == self.PacienteID) & (df['IDestudio'] == self.EstudioID)]
        if not EstudioRealizado.empty:
            # Asignar valores a los atributos de la instancia
            self.resultado = EstudioRealizado.iloc[0]['Resultado']
            self.FechaSolicitud = EstudioRealizado.iloc[0]['FechaSolicitud']
        else:
            self.resultado = None
            self.FechaSolicitud = None

    def solicitarEstudio(id_paciente, id_estudio):
        """Solicita un estudio para un paciente y registra la solicitud en un archivo de estudios."""
        ruta_estudiosRealizados = "Archivos/EstudiosRealizados.csv"
        ruta_Estudios = "Archivos/estudios.csv"

        # Asegurar que el archivo existe
        archivo_existe = os.path.exists(ruta_estudiosRealizados)
        
        # Generar los datos del estudio
        nuevo_estudio = {
            "IDPaciente": id_paciente,
            "IDEstudio": id_estudio,
            "Resultado": "Pendiente",
            "FechaSolicitud": pd.Timestamp.now().strftime('%Y-%m-%d')
        }
        
        # Registrar el estudio en el archivo estudiosRealizados.csv
        with open(ruta_estudiosRealizados, mode='a', newline='', encoding='utf-8') as file:
            fieldnames = ["IDPaciente", "IDEstudio", "Resultado", "FechaSolicitud"]
            writer = csv.DictWriter(file, fieldnames=fieldnames)
            
            # Escribir el encabezado si el archivo es nuevo
            if not archivo_existe:
                writer.writeheader()
            
            # Escribir el nuevo estudio
            writer.writerow(nuevo_estudio)

            dP = pd.read_csv(ruta_Estudios)

            # Asegurarte de que el id_estudio y la columna 'ID' tengan el mismo tipo (por ejemplo, ambos enteros o ambos cadenas)
            id_estudio = str(id_estudio)  # o int, según cómo sea en el archivo CSV
            dP['ID'] = dP['ID'].astype(str)  # Esto hace que los IDs sean comparables

            # Buscar el estudio por ID
            NombreEstudio = dP.loc[dP['ID'] == id_estudio, 'Nombre']

            # Verificar si se encontró el estudio
            if not NombreEstudio.empty:
                # Obtener el valor como cadena en lugar de una serie
                NombreEstudio = NombreEstudio.iloc[0]
                print(f"Estudio de tipo '{NombreEstudio}' solicitado para el paciente con ID {id_paciente}.")
            else:
                print(f"No se encontró el estudio con ID {id_estudio}.")


    def registrarResultado(self, Resultado):
        self.resultado = Resultado
        df = pd.read_csv(self.ruta)
        # Localizar la fila del paciente con el ID y el ID del estudio, y cambiar el resultado
        df.loc[(df['IDpaciente'] == self.PacienteID) & (df['IDestudio'] == self.EstudioID), 'Resultado'] = Resultado
        
        # Guardar el DataFrame actualizado de nuevo en el archivo CSV
        df.to_csv(self.ruta, index=False)
