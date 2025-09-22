// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;


public abstract class Vehiculo
{
    protected string marca;
    protected string modelo;
    protected int año;

    public Vehiculo(string marca, string modelo, int año)
    {
        this.marca = marca;
        this.modelo = modelo;
        this.año = año;
    }

    
    public abstract double CalcularImpuesto();

   
    public string GetMarca() => marca;
    public string GetModelo() => modelo;
    public int GetAño() => año;

    
    public override string ToString() => $"Marca: {marca}, Modelo: {modelo}, Año: {año}";
}


public class Auto : Vehiculo
{
    private int cilindraje;

    public Auto(string marca, string modelo, int año, int cilindraje) : base(marca, modelo, año)
    {
        this.cilindraje = cilindraje;
    }

    public override double CalcularImpuesto() => (cilindraje * 0.05) * año;
    public int GetCilindraje() => cilindraje;
}


public class Motocicleta : Vehiculo
{
    private int cilindraje;

    public Motocicleta(string marca, string modelo, int año, int cilindraje) : base(marca, modelo, año)
    {
        this.cilindraje = cilindraje;
    }

    public override double CalcularImpuesto() => cilindraje * 0.03;
    public int GetCilindraje() => cilindraje;
}


public class Camion : Vehiculo
{
    private int capacidadToneladas;

    public Camion(string marca, string modelo, int año, int capacidadToneladas) : base(marca, modelo, año)
    {
        this.capacidadToneladas = capacidadToneladas;
    }

    public override double CalcularImpuesto()
    {
        int añoActual = 2024;
        int antiguedad = añoActual - año;
        return (capacidadToneladas * 100) + (antiguedad * 50);
    }
    public int GetCapacidadToneladas() => capacidadToneladas;
}


public class RegistroVehicular
{
    private List<Vehiculo> listaVehiculos = new List<Vehiculo>();

    public void AgregarVehiculo(Vehiculo v)
    {
        listaVehiculos.Add(v);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(" Vehículo agregado exitosamente!");
        Console.ResetColor();
    }

    public void MostrarImpuestos()
    {
        if (listaVehiculos.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  No hay vehículos registrados.");
            Console.ResetColor();
            return;
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n🚗=== IMPUESTOS DE VEHÍCULOS ===");
        Console.ResetColor();

        foreach (var vehiculo in listaVehiculos)
        {
            double impuesto = vehiculo.CalcularImpuesto();
            Console.WriteLine($" {vehiculo} |  Impuesto: Q{impuesto:F2}");
        }
    }

   
    public double CalcularImpuestoTotal()
    {
        double total = 0;
        foreach (var vehiculo in listaVehiculos)
        {
            total += vehiculo.CalcularImpuesto();
        }
        return total;
    }

    public void MostrarImpuestoTotal()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n TOTAL DE IMPUESTOS ");
        Console.WriteLine($" Total: Q{CalcularImpuestoTotal():F2}");
        Console.ResetColor();
    }

  
    public void BuscarPorMarca(string marcaBuscar)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"\n BUSCANDO VEHÍCULOS {marcaBuscar.ToUpper()} ");
        Console.ResetColor();

        bool encontrado = false;
        foreach (var vehiculo in listaVehiculos)
        {
            if (vehiculo.GetMarca().Equals(marcaBuscar, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($" {vehiculo} |  Impuesto: Q{vehiculo.CalcularImpuesto():F2}");
                encontrado = true;
            }
        }

        if (!encontrado)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("❌ No se encontraron vehículos de esa marca.");
            Console.ResetColor();
        }
    }

    
    public void MostrarEstadisticas()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"\n📊=== ESTADÍSTICAS DEL REGISTRO ===");
        Console.ResetColor();
        Console.WriteLine($"🚗 Total de vehículos: {listaVehiculos.Count}");
        Console.WriteLine($"💰 Impuesto total: Q{CalcularImpuestoTotal():F2}");
    }
}


class ProgramaPrincipal
{
    static void Main(string[] args)
    {
        RegistroVehicular registro = new RegistroVehicular();

        string opcion;
        do
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n SISTEMA DE GESTIÓN VEHICULAR ");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("A.  Agregar vehículo");
            Console.WriteLine("M.  Mostrar impuestos");
            Console.WriteLine("T.  Total de impuestos");
            Console.WriteLine("B.  Buscar por marca");
            Console.WriteLine("E.  Estadísticas del registro");
            Console.WriteLine("S.  Salir");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\n Seleccione una opción: ");
            opcion = Console.ReadLine().ToUpper();
            Console.ResetColor();

            switch (opcion)
            {
                case "A":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n AGREGAR VEHÍCULO ");
                    Console.ResetColor();

                    Console.Write(" Tipo (A=Auto, M=Moto, C=Camion): ");
                    string tipo = Console.ReadLine().ToUpper();

                    Console.Write(" Marca: ");
                    string marca = Console.ReadLine();

                    Console.Write(" Modelo: ");
                    string modelo = Console.ReadLine();

                    Console.Write(" Año: ");
                    int año = int.Parse(Console.ReadLine());

                    Vehiculo nuevo = null;

                    if (tipo == "A")
                    {
                        Console.Write("  Cilindraje: ");
                        int cil = int.Parse(Console.ReadLine());
                        nuevo = new Auto(marca, modelo, año, cil);
                    }
                    else if (tipo == "M")
                    {
                        Console.Write("  Cilindraje: ");
                        int cil = int.Parse(Console.ReadLine());
                        nuevo = new Motocicleta(marca, modelo, año, cil);
                    }
                    else if (tipo == "C")
                    {
                        Console.Write(" Capacidad en toneladas: ");
                        int ton = int.Parse(Console.ReadLine());
                        nuevo = new Camion(marca, modelo, año, ton);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Tipo de vehículo inválido.");
                        Console.ResetColor();
                        break;
                    }

                    if (nuevo != null)
                    {
                        registro.AgregarVehiculo(nuevo);
                    }
                    break;

                case "M":
                    registro.MostrarImpuestos();
                    break;

                case "T":
                    registro.MostrarImpuestoTotal();
                    break;

                case "B":
                    Console.Write(" Marca a buscar: ");
                    string marcaB = Console.ReadLine();
                    registro.BuscarPorMarca(marcaB);
                    break;

                case "E":
                    registro.MostrarEstadisticas();
                    break;

                case "S":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n ¡Hasta luego! Que tenga un buen día. 🚗");
                    Console.ResetColor();
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" Opción inválida. Intente nuevamente.");
                    Console.ResetColor();
                    break;
            }

            if (opcion != "S")
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("\n Presione Enter para continuar...");
                Console.ResetColor();
                Console.ReadLine();
            }

        } while (opcion != "S");
    }
}

