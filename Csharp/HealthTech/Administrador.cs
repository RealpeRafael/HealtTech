using System;
using Python.Runtime;  // Asegúrate de que Python.Runtime esté correctamente referenciado
using System.IO;

namespace HealthTechApp
{
    public class ADMINISTRADOR
    {
        private string rutaCamas;

        // Constructor de la clase Administrador
        public ADMINISTRADOR(string archivoCamas)
        {
            this.rutaCamas = archivoCamas;
        }

        // Método para ejecutar el script Python para generar los gráficos de ingresos esperados
        public void GananciasEsperadas()
        {
            try
            {
                using (Py.GIL())
                {
                    // Script Python como string incrustado
                    string scriptPythonGananciasEsperadas = @"
import pandas as pd
import matplotlib.pyplot as plt
import numpy as np

class Administrador:
    def __init__(self, archivo_camas):
        self.df = pd.read_csv(archivo_camas)

    def GananciasEsperadas(self):
        demanda_estudios_mensual = np.random.normal(loc=450, scale=30, size=24)
        precio_estudio_promedio = 200000
        estacionalidad = np.array([1.0, 0.95, 1.02, 1.05, 1.07, 1.05, 1.02, 0.98, 0.97, 1.0, 1.1, 1.15])
        estacionalidad = np.tile(estacionalidad, 2)

        ingresos_mensuales_estudios = (precio_estudio_promedio * demanda_estudios_mensual) * estacionalidad

        uso_individual_diario = 0.6 * 8
        uso_compartida_diario = 0.4 * 8
        precio_individual = 350000
        precio_compartida = 200000

        ingreso_mensual_camas = ((precio_individual * uso_individual_diario * 30) +
                                (precio_compartida * uso_compartida_diario * 30)) * estacionalidad

        ingreso_total_mensual = ingresos_mensuales_estudios + ingreso_mensual_camas

        ingresos_proyectados_df = pd.DataFrame({
            'Mes': list(range(1, 25)),
            'Ingreso Mensual Estudio': ingresos_mensuales_estudios,
            'Ingreso Mensual Camas': ingreso_mensual_camas,
            'Ingreso Total Mensual': ingreso_total_mensual
        })

        plt.figure(figsize=(14, 8))

        plt.subplot(3, 1, 1)
        plt.plot(ingresos_proyectados_df['Mes'], ingresos_proyectados_df['Ingreso Mensual Estudio'], label='Ingreso por Estudios', marker='o')
        plt.title('Ingreso Mensual Proyectado por Estudios')
        plt.xlabel('Mes')
        plt.ylabel('Ingreso (COP)')
        plt.grid(True)
        plt.legend()

        plt.subplot(3, 1, 2)
        plt.plot(ingresos_proyectados_df['Mes'], ingresos_proyectados_df['Ingreso Mensual Camas'], label='Ingreso por Camas', color='orange', marker='o')
        plt.title('Ingreso Mensual Proyectado por Camas')
        plt.xlabel('Mes')
        plt.ylabel('Ingreso (COP)')
        plt.grid(True)
        plt.legend()

        plt.subplot(3, 1, 3)
        plt.plot(ingresos_proyectados_df['Mes'], ingresos_proyectados_df['Ingreso Total Mensual'], label='Ingreso Total Mensual', color='green', marker='o')
        plt.title('Ingreso Total Mensual Proyectado (Estudios y Camas)')
        plt.xlabel('Mes')
        plt.ylabel('Ingreso Total (COP)')
        plt.grid(True)
        plt.legend()

        plt.tight_layout()
        plt.show()

# Instanciar y ejecutar el método
admin = Administrador('" + this.rutaCamas + @"')
admin.GananciasEsperadas()";

                    // Ejecutar el script Python
                    dynamic pyScript = PythonEngine.RunString(scriptPythonGananciasEsperadas);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al generar gráficos de ganancias esperadas: {ex.Message}");
            }
        }

        // Método para ejecutar el script Python para generar el gráfico de reporte de ocupación
        public void ReporteOcupacion()
        {
            try
            {
                using (Py.GIL())
                {
                    // Script Python como string incrustado
                    string scriptPythonReporteOcupacion = @"
import pandas as pd
import matplotlib.pyplot as plt

class Administrador:
    def __init__(self, archivo_camas):
        self.df = pd.read_csv(archivo_camas)

    def ReporteOcupacion(self):
        ocupadas = self.df['Ocupada'].value_counts()
        plt.figure(figsize=(8, 6))
        ocupadas.plot(kind='pie', labels=['Ocupadas', 'Disponibles'], autopct='%1.1f%%', startangle=90, colors=['#66c2a5', '#fc8d62'])
        plt.title('Porcentaje de Ocupación Total')
        plt.ylabel('')
        plt.show()

        ocupacion_por_tipo = self.df.groupby('TipoCama')['Ocupada'].value_counts().unstack()
        ocupacion_por_tipo.plot(kind='bar', stacked=True, color=['#fc8d62', '#66c2a5'], figsize=(8, 6))
        plt.title('Ocupación por Tipo de Cama')
        plt.xlabel('Tipo de Cama')
        plt.ylabel('Número de Camas')
        plt.legend(['Disponibles', 'Ocupadas'])
        plt.show()

        tipo_cama = self.df['TipoCama'].value_counts()
        plt.figure(figsize=(8, 6))
        tipo_cama.plot(kind='pie', labels=['Individual', 'Compartida'], autopct='%1.1f%%', startangle=90, colors=['#8da0cb', '#fc8d62'])
        plt.title('Distribución de Camas por Tipo')
        plt.ylabel('')
        plt.show()

# Instanciar y ejecutar el método
admin = Administrador('" + this.rutaCamas + @"')
admin.ReporteOcupacion()";

                    // Ejecutar el script Python
                    dynamic pyScript = PythonEngine.RunString(scriptPythonReporteOcupacion);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al generar reporte de ocupación: {ex.Message}");
            }
        }
    }
}
