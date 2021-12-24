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


