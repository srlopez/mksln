using System;
using MINAMESPACE.UI.Consola;
using MINAMESPACE;

var sistema = new MISISTEMA();
var vista = new Vista();
var controlador = new Controlador(sistema, vista);

controlador.Run();