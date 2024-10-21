import pandas as pd
import matplotlib.pyplot as plt
import numpy as np

class ADMINISTRADOR:
    def __init__(self) -> None:
        self.df = pd.read_csv("Archivos/camas.csv")

    def GanaciasEsperadas(self):
        # Simulación de datos para estudios y camas con factores estacionales y variabilidad de la demanda

        # Demandas y precios
        demanda_estudios_mensual = np.random.normal(loc=450, scale=30, size=24)  # 450 estudios al mes con variabilidad
        precio_estudio_promedio = 200000  # Precio promedio para estudios
        estacionalidad = np.array([1.0, 0.95, 1.02, 1.05, 1.07, 1.05, 1.02, 0.98, 0.97, 1.0, 1.1, 1.15])  # Patrón estacional
        estacionalidad = np.tile(estacionalidad, 2)  # Para 2 años (24 meses)

        # Calcular ingresos mensuales proyectados para estudios aplicando variabilidad estacional
        ingresos_mensuales_estudios = (precio_estudio_promedio * demanda_estudios_mensual) * estacionalidad

        # Datos y suposiciones para el ingreso de camas
        uso_individual_diario = 0.6 * 8  # Uso del 60% para camas individuales
        uso_compartida_diario = 0.4 * 8  # Uso del 40% para camas compartidas
        precio_individual = 350000  # Precio de camas individuales
        precio_compartida = 200000  # Precio de camas compartidas

        # Ingreso mensual por camas basado en demanda diaria y factores estacionales
        ingreso_mensual_camas = ((precio_individual * uso_individual_diario * 30) + 
                                (precio_compartida * uso_compartida_diario * 30)) * estacionalidad

        # Ingreso total mensual (estudios + camas)
        ingreso_total_mensual = ingresos_mensuales_estudios + ingreso_mensual_camas

        # Crear DataFrame con los ingresos proyectados mensuales
        ingresos_proyectados_df = pd.DataFrame({
            "Mes": list(range(1, 25)),
            "Ingreso Mensual Estudio": ingresos_mensuales_estudios,
            "Ingreso Mensual Camas": ingreso_mensual_camas,
            "Ingreso Total Mensual": ingreso_total_mensual
        })

        # Graficar los ingresos mensuales para ver las tendencias de crecimiento y decrecimiento
        plt.figure(figsize=(14, 8))

        # Gráfico de Ingreso Mensual por Estudios
        plt.subplot(3, 1, 1)
        plt.plot(ingresos_proyectados_df["Mes"], ingresos_proyectados_df["Ingreso Mensual Estudio"], label="Ingreso por Estudios", marker='o')
        plt.title("Ingreso Mensual Proyectado por Estudios")
        plt.xlabel("Mes")
        plt.ylabel("Ingreso (COP)")
        plt.grid(True)
        plt.legend()

        # Gráfico de Ingreso Mensual por Camas
        plt.subplot(3, 1, 2)
        plt.plot(ingresos_proyectados_df["Mes"], ingresos_proyectados_df["Ingreso Mensual Camas"], label="Ingreso por Camas", color='orange', marker='o')
        plt.title("Ingreso Mensual Proyectado por Camas")
        plt.xlabel("Mes")
        plt.ylabel("Ingreso (COP)")
        plt.grid(True)
        plt.legend()

        # Gráfico de Ingreso Total Mensual
        plt.subplot(3, 1, 3)
        plt.plot(ingresos_proyectados_df["Mes"], ingresos_proyectados_df["Ingreso Total Mensual"], label="Ingreso Total Mensual", color='green', marker='o')
        plt.title("Ingreso Total Mensual Proyectado (Estudios y Camas)")
        plt.xlabel("Mes")
        plt.ylabel("Ingreso Total (COP)")
        plt.grid(True)
        plt.legend()

        # Ajustar el diseño de subplots
        plt.tight_layout()
        plt.show()

    def ReporteOcupacion(self):
        # 1. Gráfico de torta de porcentaje de ocupación total
        ocupadas = self.df["Ocupada"].value_counts()
        plt.figure(figsize=(8, 6))
        ocupadas.plot(kind="pie", labels=["Ocupadas", "Disponibles"], autopct='%1.1f%%', startangle=90, colors=["#66c2a5", "#fc8d62"])
        plt.title("Porcentaje de Ocupación Total")
        plt.ylabel("")
        plt.show()

        # 2. Gráfico de barras de ocupación por tipo de cama
        ocupacion_por_tipo = self.df.groupby("TipoCama")["Ocupada"].value_counts().unstack()
        ocupacion_por_tipo.plot(kind="bar", stacked=True, color=["#fc8d62", "#66c2a5"], figsize=(8, 6))
        plt.title("Ocupación por Tipo de Cama")
        plt.xlabel("Tipo de Cama")
        plt.ylabel("Número de Camas")
        plt.legend(["Disponibles", "Ocupadas"])
        plt.show()

        # 4. Gráfico de torta de distribución de camas por tipo
        tipo_cama = self.df["TipoCama"].value_counts()
        plt.figure(figsize=(8, 6))
        tipo_cama.plot(kind="pie", labels=["Individual", "Compartida"], autopct='%1.1f%%', startangle=90, colors=["#8da0cb", "#fc8d62"])
        plt.title("Distribución de Camas por Tipo")
        plt.ylabel("")
        plt.show()
