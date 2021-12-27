using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using static System.Console;

namespace MINAMESPACE.UI.Consola
{
    public class Vista
    {
        // String de cancelación de la entrada de datos.
        const string CANCELINPUT = "fin";
        // Helpers
        public List<T> EnumToList<T>() => new List<T>(Enum.GetValues(typeof(T)).Cast<T>());

        // ===== METODOS DE PRESNTACION =====
        public void LimpiarPantalla() => Clear();
        public void MostrarYReturn(Object obj, ConsoleColor color = ConsoleColor.White)
        {
            ForegroundColor = color;
            Write(obj.ToString() + " ");
            ForegroundColor = ConsoleColor.White;
            ReadLine();
        }
        public void Mostrar(Object obj, ConsoleColor color = ConsoleColor.White)
        {
            ForegroundColor = color;
            WriteLine(obj.ToString());
            ForegroundColor = ConsoleColor.White;
        }
        public void MostrarListaEnumerada<T>(string titulo, List<T> datos)
        {
            Mostrar(titulo, ConsoleColor.Yellow);
            WriteLine();
            for (int i = 0; i < datos.Count; i++)
            {
                WriteLine($"  {i + 1:##}.- {datos[i].ToString()}");
            }
            WriteLine();
        }
                public void MostrarParrilla(string titulo, List<string>[,] parrilla)
        {
            var alto = parrilla[0, 0].Count; //Numero de lineas de los elementos
            const int ancho = 16; //Ancho de cada elemento
            var filas = parrilla.GetLength(0);
            var columnas = parrilla.GetLength(1);

            const char charH = '-';
            const char charV = '|';
            const char charHV = '+';
            var border = new string(charH, ancho);
            
            Mostrar(titulo, ConsoleColor.Yellow);
            MostrarIndiceColumnas(columnas);
            MostrarSeparadorH(columnas);
            for (var f = 0; f < filas; f++)
            {
                for (var i = 0; i < parrilla[0, 0].Count; i++)
                {
                    MostrarIndiceFila(f, i == alto % 2);
                    for (var c = 0; c < columnas; c++)
                    {
                        var s = parrilla[f, c][i]+ new string(' ', ancho);
                        Write($"{charV}{s.Substring(0,ancho)}");
                        //Write($"{charV}{parrilla[f, c][i],-ancho}");
                    }
                    WriteLine(charV);
                }
                MostrarSeparadorH(columnas);
            }
            void MostrarSeparadorH(int len)
            {
                Write("  ");
                for (var i = 0; i < len; i++) Write($"{charHV}{border}");
                WriteLine(charHV);
            }
            void MostrarIndiceColumnas(int len)
            {
                var fmt = new string(' ', ancho/2);
                for (var c = 0; c < len; c++) Write($"{fmt}{c}{fmt}");
                WriteLine();
            }
            void MostrarIndiceFila(int f, bool show) =>
                Write(show ? $"{f,-2}" : "  ");
        }

        // ===== METODOS DE CAPTURA DE INFORMACION =====
        // Refactoring C# Generics, Reflexion, PatternMaching, Tuples,
        public T TryObtenerDatoDeTipo<T>(string prompt, string @default = "")
        {
            var msg = prompt.Trim() + ": ";
            if (@default != "") msg += "\b\b (" + @default + "): ";

            while (true)
            {
                Write(msg);
                var input = ReadLine();
                // c# throw new Exception: Lanzamos una Excepción para indicar que el usuario ha cancelado la entrada
                if (input.ToLower().Trim() == CANCELINPUT) throw new Exception("Entrada cancelada por el usuario");
                if (input == "") input = @default;
                try
                {
                    // c# Reflexion
                    // https://stackoverflow.com/questions/2961656/generic-tryparse?rq=1
                    var valor = TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(input);
                    return (T)valor;
                }
                catch (Exception)
                {
                    //MostrarMensaje($"Error: {input} no reconocido como: {typeof(T).ToString()}");
                    if (input != "")
                        Mostrar($"Error: '{input}' no reconocido como entrada permitida", ConsoleColor.DarkRed);
                }
            }
        }
        public int TryObtenerValorEnRangoInt(int min, int max, string prompt)
        {
            int input = int.MaxValue;
            while (input < min || input > max)
                try
                {
                    input = TryObtenerDatoDeTipo<int>(prompt);
                }
                catch (Exception e)
                {
                    throw e;
                };
            return input;
        }
        public T TryObtenerElementoDeLista<T>(string titulo, List<T> datos, string prompt)
        {
            MostrarListaEnumerada(titulo, datos);
            try
            {
                var input = TryObtenerValorEnRangoInt(1, datos.Count, prompt);
                return datos[input - 1];
            }
            catch (Exception e)
            {
                throw e;
            };
        }
        public (int x, int y) TryObtenerTuplaInt(string prompt, (int xMax, int yMax) limites)
        {
            var msg = prompt.Trim() + ": ";
            while (true)
            {
                Write(msg);
                var input = ReadLine();
                // c# throw new Exception: Lanzamos una Excepción para indicar que el usuario ha cancelado la entrada
                if (input.ToLower().Trim() == CANCELINPUT) throw new Exception("Entrada cancelada por el usuario");
                try
                {
                    var valores = input.Split(",");
                    if (valores.Length != 2) throw new Exception();
                    var x = Int16.Parse(valores[0]);
                    var y = Int16.Parse(valores[1]);
                    if (x < 0 || x >= limites.xMax) throw new Exception();
                    if (y < 0 || y >= limites.yMax) throw new Exception();
                    return (x: x, y: y);
                }
                catch (Exception)
                {
                    if (input != "")
                        Mostrar($"Error: '{input}' no reconocido como entrada permitida", ConsoleColor.DarkRed);
                }
            }
        }
        public int[] TryObtenerArrayInt(string prompt, int size, char separador = ',')
        {
            var msg = prompt.Trim() + ": ";
            while (true)
            {
                Write(msg);
                var input = ReadLine();
                // c# throw new Exception: Lanzamos una Excepción para indicar que el usuario ha cancelado la entrada
                if (input.ToLower().Trim() == CANCELINPUT) throw new Exception("Entrada cancelada por el usuario");
                try
                {
                    var valores = input.Split(separador);
                    if (valores.Length != size) throw new Exception();
                    var ints = new int[size];
                    for (var i = 0; i < valores.Length; i++)
                        ints[i] = Int16.Parse(valores[i]);
                    return ints;
                }
                catch (Exception)
                {
                    if (input != "")
                        Mostrar($"Error: '{input}' no reconocido como entrada permitida", ConsoleColor.DarkRed);
                }
            }
        }
        public DateTime TryObtenerFecha(string prompt)
        {
            var promptF = prompt.Trim() + " (d/m/a4): ";
            while (true)
            {
                var input = TryObtenerArrayInt(promptF, 3, '/');
                try
                {
                    return new DateTime(input[2], input[1], input[0], 0, 0, 0);
                }
                catch (Exception)
                {
                    Mostrar($"Error: '{input[0]}/{input[1]}/{input[2]}' no reconocido como fecha permitida", ConsoleColor.DarkRed);
                }
            }
        }
        public char TryObtenerCaracterDeString(string prompt, string opciones, char predeterminado = 'S')
        {
            var msg = prompt.Trim() + " (" + predeterminado + "): ";
            while (true)
            {
                Write(msg);
                var input = ReadLine();
                // c# throw new Exception: Lanzamos una Excepción para indicar que el usuario ha cancelado la entrada
                if (input.ToLower().Trim() == CANCELINPUT) throw new Exception("Entrada cancelada por el usuario");
                if (input == "") input = predeterminado.ToString();
                try
                {
                    if (input.Length != 1) throw new Exception();
                    var c = input.ToUpper()[0];
                    if (!opciones.Contains(c)) throw new Exception();
                    return c;
                }
                catch (Exception)
                {
                    Mostrar($"Error: '{input}' no reconocido como valor permitido en {opciones}", ConsoleColor.DarkRed);
                }
            }
        }
        //public char TryObtenerSiNo(string prompt) => TryObtenerCharFromString(prompt, "SN", 'S');

    }
}