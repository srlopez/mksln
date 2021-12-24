# mksln.sh

Script para crear una solución `dotnet`.  
Permanentemente en `Beta`.

La solución completa trata de organizar 5 proyectos relacionados para estudiar la organización de proyectos, las distintas capas que se pueden dar, y las maneras de abordar proyectos orientados a objetos.

Consta de 5 proyectos
- La Lógica de la aplicación. `App`
- Los Modelos de la aplicación. `App.Modelos`
- Una interfaz tipo consola. `UI.Consola`
- Los Test de la Lógica. `App.Tests`
- Los Test de la consola. `UI.Consola.Tests`

Para utilizarlo clonar este repositorio, y usarlo de la sigiente manera
- Ejemplo.1  ./mksln.sh 
- Ejemplo.2  ./mksln.sh AppCalculadora 
- Ejemplo.3  ./mksln.sh AppCalculo Calculadora
- Ejemplo.4  ./mksln.sh SlnSanitaria Urgencias
- Ejemplo.5  ./mksln.sh SlnInstituto GestorDeNotas

El primer parámetro (por defecto SlnAplicacion) será el nombre del directorio, del cual a partir de la última mayúscula se extrae el namespace de la aplicación, el segundo parámetro (por defecto Sistema) será la clase que agrupe la lógica del negocio.

La ejecución de comando crea un directorio con esos proyectos:

```bash
santi@slimbook:~/dev/net/SlnNet$ ./mksln.sh 
Directorio de la aplicación SlnAplicacion ...
Proyecto src/Aplicacion.App.Modelos ...
The template "Class library" was created successfully.
...
Proyecto src/Aplicacion.App ...
The template "Class library" was created successfully.
...
Reference `..\Aplicacion.App.Modelos\Aplicacion.App.Modelos.csproj` added to the project.
...
Proyecto src/Aplicacion.UI.Consola ...
The template "Console Application" was created successfully.
...
Reference `..\Aplicacion.App\Aplicacion.App.csproj` added to the project.
Reference `..\Aplicacion.App.Modelos\Aplicacion.App.Modelos.csproj` added to the project.
Proyecto test/Aplicacion.App.Tests ...
The template "xUnit Test Project" was created successfully.
...
Reference `..\..\src\Aplicacion.App\Aplicacion.App.csproj` added to the project.
...
Test run for /home/santi/dev/net/SlnNet/SlnAplicacion/test/Aplicacion.App.Tests/...
Starting test execution, please wait...
A total of 1 test files matched the specified pattern.

Passed!  - Failed:     0, Passed:     2, Skipped:     0, Total:     2, 
...
Proyecto test/Aplicacion.UI.Consola.Tests ...
The template "xUnit Test Project" was created successfully.
...
Reference `..\..\src\Aplicacion.UI.Consola\Aplicacion.UI.Consola.csproj` added to the project.
...
Starting test execution, please wait...
A total of 1 test files matched the specified pattern.
...
Passed!  - Failed:     0, Passed:     5, Skipped:     0, Total:     5, 
...
The template "Solution File" was created successfully.
The template "dotnet gitignore file" was created successfully.
/home/santi/dev/net/SlnNet/SlnAplicacion
├── src
│ ├── Aplicacion.App
│ │ └── Sistema.cs
│ ├── Aplicacion.App.Modelos
│ │ └── Modelo.cs
│ └── Aplicacion.UI.Consola
│     ├── Controlador.cs
│     ├── Program.cs
│     └── Vista.cs
└── test
    ├── Aplicacion.App.Tests
    │ └── SistemaTests.cs
    └── Aplicacion.UI.Consola.Tests
        └── VistaTests.cs

7 directories, 7 files
```