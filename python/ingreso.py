import os
import csv
from cama import Cama
import pandas as pd

class Ingreso:
    def __init__(self):
        self.ID = input("Ingrese el documento de la persona: ")
        self.fecha_ingreso = input("Ingrese una fecha de ingreso: (separadas por [-])")  # str
        self.motivo_ingreso = input("Ingrese la enfermedad o el motivo de ingreso")  # str
        
        self.cama_asignada = input("Ingrese la cama que se le va a asignar: (Escriba [AUTOMATICO] para asignarle cualquiera disponible)")  # int
        if self.cama_asignada == "AUTOMATICO":
            TipoCama = input("Ingrese el tipo de cama que desea (Individual o Compartida)")
            self.cama_asignada = Cama.BuscarCamaPorTipo(TipoCama)
        else:
            cama = Cama(self.cama_asignada)

        self.nombre = input("Ingrese el nombre del paciente: ")
        self.edad = input("Ingrese la edad del paciente: ")

    def registrar_ingreso(self):
            """Registrar el ingreso del paciente en el archivo pacientes.csv."""
            PACIENTES = "Archivos/pacientes.csv"
            
            # Estructura del nuevo registro
            nuevo_paciente = {
                "ID": self.ID,
                "Nombre": self.nombre,
                "Edad": self.edad,
                "Diagnóstico": "",  # Vacío por el momento
                "EspecialidadAsignada": "",  # Vacío por el momento
                "CamaAsignada": self.cama_asignada,
                "FechaIngreso": self.fecha_ingreso,
                "HistorialMedico": ""  # Vacío por el momento
            }

            # Verificar si el archivo ya existe y añadir el encabezado si no existe
            archivo_existe = os.path.exists(PACIENTES)
            
            # Guardar los datos en el archivo CSV
            with open(PACIENTES, mode='a', newline='', encoding='utf-8') as file:
                fieldnames = ["ID", "Nombre", "Edad", "Diagnóstico", "EspecialidadAsignada", "CamaAsignada", "FechaIngreso", "HistorialMedico"]
                writer = csv.DictWriter(file, fieldnames=fieldnames)

                # Escribir el encabezado solo si el archivo es nuevo
                if not archivo_existe:
                    writer.writeheader()
                
                # Escribir el nuevo registro de paciente
                writer.writerow(nuevo_paciente)
            
            print(f"Ingreso registrado para el paciente {self.nombre} con ID {self.id_ingreso} en la cama {self.cama_asignada}.")

    # Registrar la fecha de alta del paciente
    def registrar_alta(self, fecha_alta):
        if self.fecha_alta:
            raise ValueError("El paciente ya tiene una fecha de alta registrada.")
        self.fecha_alta = fecha_alta
        self.liberar_cama()
        print(f"Alta registrada para el paciente {self.paciente.nombre} el {self.fecha_alta}")

    # Liberar la cama asignada al paciente
    def liberar_cama(self):
        print(f"La cama {self.cama_asignada} ha sido liberada.")
