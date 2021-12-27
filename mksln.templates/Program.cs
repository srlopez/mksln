using System;
using MINAMESPACE.UI.Consola;
using MINAMESPACE.Data;
using MINAMESPACE;

var repositorio = new RepoJson();
var sistema = new MISISTEMA(repositorio);
var vista = new Vista();
var controlador = new Controlador(sistema, vista);

controlador.Run();