using System;
using System.Collections.Generic;
using System.Linq;
using MINAMESPACE.Modelos;

namespace MINAMESPACE.UI.Consola
{
    enum Modo { Anonimo, Usuario, Admin }

    public class Controlador
    {
        // ===== DEPENDENCIAS =====
        private MISISTEMA _sistema;
        private Vista _vista;

        // ===== MODO DE USO =====
        private string _usuario; // Usuario Logeado
        private Modo _modo; //Modo de la Interfaz
        private string[] _menuModo = { "Login", "Logout", "Logout Admin" }; // Opciones del menu en funcion del modo

        // ===== CASOS DE USO ==== 
        private Dictionary<(string titulo, Modo modo), Action> _casosDeUso;
        public Controlador(MISISTEMA sistema, Vista vista)
        {
            _sistema = sistema;
            _vista = vista;
            _modo = Modo.Anonimo;
            _usuario = "Anonimo";
            _casosDeUso = new Dictionary<(string, Modo), Action>(){
                { ("Caso De Uso No Implementado",Modo.Anonimo), NoImplementado },
                { (_menuModo[(int)_modo],Modo.Anonimo), establecerModoInterfaz },
            };
        }

        // ======== CICLO PRINCIPAL =====
        public void Run()
        {
            _vista.LimpiarPantalla();

            while (true)
                try
                {
                    // Menu y Obtener opción 
                    var menu = _casosDeUso.Keys.Where(k => k.modo <= _modo).Select(k => k.titulo).ToList<String>();
                    var opcion = _vista.TryObtenerElementoDeLista($"Menu de {_modo}", menu, "Seleciona una opción");
                    // Ejecución de la opción escogida
                    _vista.Mostrar(""); // (opcion);
                    var casoDeUso = _casosDeUso.FirstOrDefault(k => k.Key.titulo == opcion).Value;
                    casoDeUso.Invoke();
                    // Fin opción
                    _vista.MostrarYReturn("Pulsa <Return> para continuar", ConsoleColor.DarkGray);
                    _vista.LimpiarPantalla();
                }
                catch
                {
                    return;
                }
        }

        // =======  CASOS DE USO ========
        private void NoImplementado()
        {
            _vista.Mostrar("No implementado");
        }

        // ====== MODO DEL TERMINAL =====
        private void establecerModoInterfaz()
        {
            switch (_modo)
            {
                case Modo.Usuario:
                case Modo.Admin:
                    establecerAnonimo();
                    break;
                case Modo.Anonimo:
                    try
                    {
                        var username = _vista.TryObtenerDatoDeTipo<string>("Username");
                        var password = _vista.TryObtenerDatoDeTipo<string>("Password").ToLower().Trim(); ;
                        // Sólo validamos que el primer caracter para pasar a modo Admin/User
                        if (!"au".Contains(password[0]))
                            _vista.Mostrar("Acceso no permitido", ConsoleColor.DarkRed);
                        if (password[0] == 'a')
                            establecerAdmin(username);
                        if (password[0] == 'u')
                            establecerUsuario(username);
                    }
                    catch { return; };
                    break;
            }
            void establecerAnonimo()
            {
                _usuario = "anomious";
                _modo = Modo.Anonimo;
                establecerOpcionDeMenu(_menuModo[(int)_modo]);
            };
            void establecerUsuario(string username)
            {
                _usuario = username.ToLower().Trim();
                _modo = Modo.Usuario;
                establecerOpcionDeMenu($"Logout {username}");
            };
            void establecerAdmin(string username)
            {
                _usuario = username.ToLower().Trim();
                _modo = Modo.Admin;
                establecerOpcionDeMenu(_menuModo[(int)_modo]);
            };
            void establecerOpcionDeMenu(string opcion)
            {
                var modoKey = _casosDeUso.FirstOrDefault(x => x.Value == establecerModoInterfaz).Key;
                _casosDeUso.Remove(modoKey);
                _casosDeUso.Add((opcion, _modo), establecerModoInterfaz);
            }
        }
    }
}