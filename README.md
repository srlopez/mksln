# mksln.sh

Script para crear una solución `dotnet`.  
Permanentemente en `Beta`.
Plugins interesantes para aprovechar esta estructura:
- vscode-solution-explorer
- .NET Core Test Explorer

La solución completa trata de organizar los proyectos relacionados para estudiar la organización de proyectos, las distintas capas que se pueden dar, y las maneras de abordar proyectos orientados a objetos.

Consta de los siguientes proyectos
- La Lógica de la aplicación. `App` (o capa de Negocio)
- Los Modelos de la aplicación. `App.Modelos` (de la capa de Negocio)
- Una interfaz tipo consola. `UI.Consola` (o capa de Presentación)
- Un proyecto de repositorio. `Data` (o capa de Acceso a Datos)
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
santi@slimbook:~/dev/net/SlnNet$ ./mksln.sh AppEjemplo MiSistema
Directorio de la aplicación AppEjemplo ...
Proyecto src/Ejemplo.App.Modelos ...
The template "Class library" was created successfully.
[...]
Proyecto src/Ejemplo.Data ...
The template "Class library" was created successfully.
[...]
Proyecto src/Ejemplo.App ...
The template "Class library" was created successfully.
[...]
Proyecto src/Ejemplo.UI.Consola ...
The template "Console Application" was created successfully.
[...]
Proyecto test/Ejemplo.App.Tests ...
The template "xUnit Test Project" was created successfully.
[...]
Starting test execution, please wait...
A total of 1 test files matched the specified pattern.

Passed!  - Failed:     0, Passed:     2, Skipped:     0, Total:     2, Duration: 4 [.[...]
Proyecto test/Ejemplo.UI.Consola.Tests ...
The template "xUnit Test Project" was created successfully.
[...]
Starting test execution, please wait...
A total of 1 test files matched the specified pattern.

Passed!  - Failed:     0, Passed:     5, Skipped:     0, Total:     5, Duration: 4 
[...]
The template "Solution File" was created successfully.
[...]
The template "dotnet gitignore file" was created successfully.
/home/santi/dev/net/SlnNet/AppEjemplo
├── src
│ ├── Ejemplo.App
│ │ └── MiSistema.cs
│ ├── Ejemplo.App.Modelos
│ │ └── Modelo.cs
│ ├── Ejemplo.Data
│ │ ├── IRepo.cs
│ │ └── RepoJson.cs
│ └── Ejemplo.UI.Consola
│     ├── Controlador.cs
│     ├── Program.cs
│     └── Vista.cs
└── test
    ├── Ejemplo.App.Tests
    │ └── MiSistemaTests.cs
    └── Ejemplo.UI.Consola.Tests
        └── VistaTests.cs

8 directories, 9 files
```