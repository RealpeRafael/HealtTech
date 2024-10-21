import pandas as pd

class Cama:
    def __init__(self, numero_cama):
        self.numero_cama = numero_cama
        # Abrir el archivo con 'open()' y manejarlo correctamente
        with open("Archivos/camas.csv", "r") as camasfile:
            for linea in camasfile:
                lineaSeparada = linea.strip().split(",")
                numeroCama = lineaSeparada[0]
                if numeroCama == self.numero_cama:
                    self.tipoCama = lineaSeparada[1]
                    # Asumimos que ocupacion es una cadena que dice 'True' o 'False'
                    self.ocupacion = lineaSeparada[2] == 'True'  # Convertir a booleano
                    self.precio = lineaSeparada[4]

    def consultarDisponibilidadCama(self):
        if not self.ocupacion:
            print("La cama se encuentra disponible y se le va a asignar al paciente")
            return True
        else: 
            print("La cama NO se encuentra disponible, asigne otra cama")
            return False

    @staticmethod
    def consultarDisponibilidadGeneral():
        with open("Archivos/camas.csv", "r") as camasfile:
            for linea in camasfile:
                lineaSeparada = linea.strip().split(",")
                numeroCama = lineaSeparada[0]
                tipoCama = lineaSeparada[1]
                ocupacion = lineaSeparada[2] == 'True'  # Convertir a booleano
                precio = lineaSeparada[3]

                if not ocupacion:
                    print(f"La cama {numeroCama} que es {tipoCama} se encuentra desocupada y tiene el precio ${precio}")

    @staticmethod
    def BuscarCamaPorTipo(tipoCama):
        ruta_camas = "Archivos/camas.csv"
        
        # Leer el archivo camas.csv en un DataFrame
        df = pd.read_csv(ruta_camas)
        
        # Filtrar para encontrar la primera cama que cumpla con el tipo y esté libre (ocupacion == False)
        cama_disponible = df[(df['TipoCama'] == tipoCama) & (df['Ocupada'] == False)]
        
        if not cama_disponible.empty:
            # Obtener el índice de la primera cama disponible y actualizar ocupación a True
            index_cama = cama_disponible.index[0]
            df.at[index_cama, 'Ocupacion'] = True
            numero_cama = df.at[index_cama, 'NumeroCama']
            
            # Guardar el DataFrame de nuevo en el archivo CSV
            df.to_csv(ruta_camas, index=False)
            
            print(f"El paciente fue asignado a la cama N°{numero_cama}.")
            return numero_cama
        else:
            print("No se encontraron camas disponibles de ese tipo.")
            return None
        
    def liberar_cama(self):
        if self.ocupacion:
            self.ocupacion = False  # Corregir la asignación, no comparación
            print(f"La cama {self.numero_cama} ha sido liberada.")
