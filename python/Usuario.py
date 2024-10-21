import csv
import os

class USUARIO:
    def __init__(self, usuario, contrasena):
        self.usuario = usuario
        self.contrasena = contrasena
        self.ruta_usuarios = 'Archivos/Usuarios.csv'  # Archivo CSV para almacenar usuarios

    def cargarUsuarios(self):
        """Cargar todos los usuarios desde el archivo CSV."""
        usuarios = {}
        if os.path.exists(self.ruta_usuarios):
            with open(self.ruta_usuarios, mode='r', newline='', encoding='utf-8') as file:
                reader = csv.DictReader(file)
                for row in reader:
                    usuarios[row['Usuario']] = {
                        "ID": int(row['ID']),  # Convertimos el ID a entero
                        "Contrasena": row['Contrasena'],
                        "Tcuenta": row['Tcuenta']
                    }
        return usuarios

    def InicioSesion(self):
        """Verificar si el usuario existe y si la contraseña es correcta."""
        usuarios = self.cargarUsuarios()
        if self.usuario in usuarios:
            if usuarios[self.usuario]["Contrasena"] == self.contrasena:
                print("Inicio de Sesión exitoso.")
                return True
            else:
                print("Contraseña incorrecta.")
                return False
        else:
            print("El usuario no existe.")
            return False


            

    def guardarUsuarios(self, usuarios):
        """Guardar todos los usuarios en el archivo CSV."""
        with open(self.ruta_usuarios, mode='w', newline='', encoding='utf-8') as file:
            fieldnames = ['ID', 'Usuario', 'Contrasena', 'Tcuenta']  # Aseguramos que los nombres de campo coincidan
            writer = csv.DictWriter(file, fieldnames=fieldnames)
            writer.writeheader()
            for usuario, datos in usuarios.items():
                writer.writerow({
                    'ID': datos['ID'],
                    'Usuario': usuario,
                    'Contrasena': datos['Contrasena'],
                    'Tcuenta': datos['Tcuenta'],
                })

    def CrearCuenta(self, TipoCuenta):
        """Crear una nueva cuenta y guardarla en el archivo CSV."""
        usuarios = self.cargarUsuarios()
        if self.usuario in usuarios:
            print("El usuario ya existe.")
            return False
        else:
            # Determinamos el nuevo ID basado en el ID más alto actual
            if not usuarios:
                Nuevoid = 1  # Si no hay usuarios, empezamos con el ID 1
            else:
                max_id = max(datos["ID"] for datos in usuarios.values())
                Nuevoid = max_id + 1

            # Agregamos el nuevo usuario
            usuarios[self.usuario] = {
                "ID": Nuevoid,
                "Contrasena": self.contrasena,
                "Tcuenta": TipoCuenta,
            }
            # Guardamos la lista de usuarios actualizada
            self.guardarUsuarios(usuarios)
            print(f"Usuario {self.usuario} creado con éxito con ID {Nuevoid}.")
            return True
