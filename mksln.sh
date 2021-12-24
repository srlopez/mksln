#Ejemplo.1  ./mksln.sh 
#Ejemplo.2  ./mksln.sh AppCalculadora 
#Ejemplo.3  ./mksln.sh AppCalculo Calculadora
#Ejemplo.4  ./mksln.sh SlnSanitaria Urgencias
#Ejemplo.5  ./mksln.sh SlnInstituto GestorDeNotas
#================== SETUP DE VARIABLES
APP=$1 && [ -z "$1" ] && APP=SlnAplicacion
SISTEMA=$2 && [ -z "$2" ] && SISTEMA=Sistema
TEMPLATES=$(dirname $(readlink -e $0))/mksln.templates

# namespace
NS=`echo $APP | sed 's/\([A-Za-z]\+[^A-Z]\)\([A-Z].*\)/\2/g'`
# proyectos
LIB=$NS.App           #LIB=$APP.Lib        #App #App.Lib #App.Log #App.BLL #App.Core
LIBTESTS=$LIB.Tests
MODELOS=$LIB.Modelos  
ENTRY=$NS.UI.Consola  #ENTRY=$APP.Consola  #Consola #UI.Console
ENTRYTESTS=$NS.UI.Consola.Tests
# directorios
ENTRYPATH=src/$ENTRY
LIBPATH=src/$LIB
LIBTESTSPATH=test/$LIBTESTS
MODELOSPATH=src/$MODELOS
ENTRYTESTSPATH=test/$ENTRYTESTS

cbecho() { tput setaf 14; tput bold; echo $*; tput sgr0; }
#==================  DIRECTORIO DE APLICACION
cbecho Directorio de la aplicación $APP ...
rm -rf ${APP}
mkdir ${APP}
cd ${APP}

#================== LIBRERIA DE MODELOS
cbecho Proyecto $MODELOSPATH ...
dotnet new classlib -o $MODELOSPATH
rm $MODELOSPATH/*.cs

cp $TEMPLATES/Modelo.cs $MODELOSPATH/
sed -i 's/MINAMESPACE/'${NS}'/g' $MODELOSPATH/Modelo.cs

dotnet build $LIBPATH
#================== LIBRERIA PPAL DEL NEGOCIO
cbecho Proyecto $LIBPATH ...
dotnet new classlib -o $LIBPATH
dotnet add $LIBPATH reference $MODELOSPATH/$MODELOS.csproj

#dotnet restore $LIBPATH
rm $LIBPATH/*.cs

cp $TEMPLATES/Sistema.cs $LIBPATH/${SISTEMA}.cs
sed -i 's/MINAMESPACE/'${NS}'/g' $LIBPATH/${SISTEMA}.cs
sed -i 's/MISISTEMA/'${SISTEMA}'/g' $LIBPATH/${SISTEMA}.cs

dotnet build $LIBPATH
#===================  PROYECTO MAIN DE PUNTO DE ENTRADA
cbecho Proyecto $ENTRYPATH ...
dotnet new console -o $ENTRYPATH
dotnet add $ENTRYPATH/$ENTRY.csproj reference $LIBPATH/$LIB.csproj
dotnet add $ENTRYPATH/$ENTRY.csproj reference $MODELOSPATH/$MODELOS.csproj

cp $TEMPLATES/Vista.cs $ENTRYPATH
cp $TEMPLATES/Program.cs $ENTRYPATH
cp $TEMPLATES/Controlador.cs $ENTRYPATH

sed -i 's/MINAMESPACE/'${NS}'/g' $ENTRYPATH/Vista.cs
sed -i 's/MINAMESPACE/'${NS}'/g' $ENTRYPATH/Program.cs
sed -i 's/MISISTEMA/'${SISTEMA}'/g' $ENTRYPATH/Program.cs
sed -i 's/MINAMESPACE/'${NS}'/g' $ENTRYPATH/Controlador.cs
sed -i 's/MISISTEMA/'${SISTEMA}'/g' $ENTRYPATH/Controlador.cs

# dotnet run -p $ENTRYPATH/$ENTRY.csproj
#=================== PROYECTO DE PRUEBAS UNITARIAS PPAL
cbecho Proyecto $LIBTESTSPATH ...
#cbecho class $NS.${SISTEMA}Test en ${SISTEMA}Tests.cs
dotnet new xunit -o $LIBTESTSPATH
dotnet add $LIBTESTSPATH/$LIBTESTS.csproj reference $LIBPATH/$LIB.csproj
rm $LIBTESTSPATH/*.cs

cp $TEMPLATES/SistemaTests.cs $LIBTESTSPATH/${SISTEMA}Tests.cs
sed -i 's/MINAMESPACE/'${NS}'/g' $LIBTESTSPATH/${SISTEMA}Tests.cs
sed -i 's/MISISTEMA/'${SISTEMA}'/g' $LIBTESTSPATH/${SISTEMA}Tests.cs

dotnet test $LIBTESTSPATH/$LIBTESTS.csproj
#=================== PROYECTO DE PRUEBAS UNITARIAS CONSOLA
cbecho Proyecto $ENTRYTESTSPATH ...
dotnet new xunit -o $ENTRYTESTSPATH
dotnet add $ENTRYTESTSPATH/$ENTRYTESTS.csproj reference $ENTRYPATH/$ENTRY.csproj
rm $ENTRYTESTSPATH/*.cs

cp $TEMPLATES/VistaTests.cs $ENTRYTESTSPATH/
sed -i 's/MINAMESPACE/'${NS}'/g' $ENTRYTESTSPATH/VistaTests.cs

dotnet test $ENTRYTESTSPATH/$ENTRYTESTS.csproj
#=================== VSCODE WORKSPACE
# cat <<EOF >${APP}.code-workspace
# {
#   "folders": [
#       {"path": "."},
#   ],
#   "settings": {
#     "dotnet-test-explorer.testProjectPath": "test/*",
#     "dotnet-test-explorer.enableTelemetry": false,
#     "dotnet-test-explorer.treeMode": "full"
#   }
# }
# EOF
# rm ${APP}.code-workspace
# code ${APP}.code-workspace 
#=================== VSTUDIO SOLUTION
dotnet new sln 
dotnet sln add $ENTRYPATH/$ENTRY.csproj
dotnet sln add $LIBPATH/$LIB.csproj
dotnet sln add $MODELOSPATH/$MODELOS.csproj
dotnet sln add $LIBTESTSPATH/$LIBTESTS.csproj
dotnet sln add $ENTRYTESTSPATH/$ENTRYTESTS.csproj
#=================== GIT
dotnet new gitignore
#=================== RUN VSCODE
#tree `pwd` -d -L 2
tree `pwd` -P *.cs  -I bin\|obj 
code .