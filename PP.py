from Usuario import USUARIO
from administrador import ADMINISTRADOR
from Recepcionista import Recepcionista
from PersonalMedico import PersonalMedico
import pandas as pd

while True:
    OPCION1 = input(""" Bienvenido a Helixa, Software de Gestion Hospitalaria, llevando la atencion al siguiente nivel
                   1. Iniciar Sesion
                   2. Salir
""")
        


    match OPCION1:
        case "1": 
            Usuario = input("Ingrese el usuario: ")
            Contrasena = input("Ingrese la contraseña: ")

            sesion = USUARIO(Usuario, Contrasena)
            RespuestaSesion = sesion.InicioSesion()

            if RespuestaSesion:
                usuarioss = sesion.cargarUsuarios()
                TipoCuenta = usuarioss[Usuario]["Tcuenta"]

                if TipoCuenta == "Recepcionista":
                    while True:
                        OPCION2 = input("""Que desea hacer?
                                        1. Ingresar un paciente
                                        2. Consultar disponibilidad de camas
                                        3. Cambiar de especialidad medica a paciente
                                        4. Salir
                                        """)
                        match OPCION2:
                            case "1":
                                Recepcionista.IngresarPaciente()
                            case "2":
                                Recepcionista.ConsultarDisponibilidadCamas()
                            case "3":
                                Recepcionista.CambiarEspecialidad()
                            case "4":
                                break

                elif TipoCuenta == "Administrador":
                    while True:
                        Admin = ADMINISTRADOR()
                        OPCION2 = input("""Que dese hacer?
                                        1. Mostrar Reportes ocupacion
                                        2. Calcular Ingresos Proyectados
                                        3. Salir""")
                        match OPCION2:
                            case "1": 
                                print(Admin.ReporteOcupacion())
                            case "2":
                                print(Admin.GanaciasEsperadas())
                            case "3":
                                break

                elif TipoCuenta == "PersonalMedico":
                    df_usuarios = pd.read_csv("Archivos/Usuarios.csv")
                    if df_usuarios is not None:
                        usuario_info = df_usuarios[df_usuarios['Usuario'] == Usuario]
                        if not usuario_info.empty:
                            ID = usuario_info.iloc[0]['ID']
                            Nombre = usuario_info.iloc[0]['Nombre']
                            Especialidad = usuario_info.iloc[0]['EspecialidadMedica']
                    medico = PersonalMedico(ID,Nombre,Especialidad)
                    while True:
                        OPCION2 = input("""Que desea hacer=
                                        1. Solicitar estudios adicionales
                                        2. Consultar informacion de paciente
                                        3. Registrar informacion de un paciente
                                        4. salir
                                        """)
                        match OPCION2:
                            case"1":
                                medico.solicitarEstudios()
                            case"2":
                                medico.consultarInformacionPaciente()
                            case "3":
                                IDpaciente = input("Ingrese el ID del paciente: ")
                                NotasMedicas = input("Ingrese las notas medicas que quedaran en el historial medico: ")
                                Diagnostico = input("Ingrese el diagnostico (enfermedad) del paciente: ")
                                EspecialidadAsignada = input("Ingrese la especialidad que le va a asignar: ")
                                medico.registrarInformacion(IDpaciente, NotasMedicas, Diagnostico, EspecialidadAsignada)
                            case "4":
                                break
        case "2":
            break