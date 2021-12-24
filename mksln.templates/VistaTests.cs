using System;
using System.IO;
using System.Collections.Generic;
using Xunit;

namespace MINAMESPACE.UI.Consola
{
    /*
 
    */
    public class VistaTests
    {

        [Fact]
        public void TryObtenerDatoDeTipo_default_Test()
        {
            // Given
            var input = new StringReader("\n");//Enter
            Console.SetIn(input);

            var vista = new Vista();
            int esperado = 16;
            // When
            int resultado = vista.TryObtenerDatoDeTipo<int>("int", "16");
            // Then
            Assert.Equal(esperado, resultado);
        }

        [Fact]
        public void TryObtenerDatoDeTipo_Int_Test()
        {
            // Given
            var input = new StringReader("16");
            Console.SetIn(input);

            var vista = new Vista();
            int esperado = 16;
            // When
            int resultado = vista.TryObtenerDatoDeTipo<int>("int");
            // Then
            Assert.Equal(esperado, resultado);
        }

        [Fact]
        public void TryObtenerDatoDeTipo_Exception_Test()
        {
            // Given
            var input = new StringReader("abc");
            Console.SetIn(input);
            var vista = new Vista();
            Action testCode = () => vista.TryObtenerDatoDeTipo<int>("int");
            // When
            var ex = Record.Exception(testCode);
            // Then
            Assert.NotNull(ex);
            Assert.IsType<NullReferenceException>(ex);
            //Assert.Throws<NullReferenceException>(testCode);
        }

        [Fact]
        public void TryObtenerElementoDeLista_String_Test()
        {
            // Given
            var idx = 2;
            var datos = new List<string> { "dato1", "dato2", "dato3" };

            var input = new StringReader(idx.ToString());
            Console.SetIn(input);

            var vista = new Vista();
            var esperado = datos[idx - 1];
            // When
            var resultado = vista.TryObtenerElementoDeLista<string>("tit", datos, "prompt");
            // Then
            Assert.Equal(esperado, resultado);
        }

        [Fact]
        public void MostrarParrilla_Test()
        {
            // Given
            List<string>[,] datos = {
                {
                    new List<string>{"a1","a2"},
                    new List<string>{"b1","b2"},
                    new List<string>{"c1","c2"}
                },
                {
                    new List<string>{"d1","d2"},
                    new List<string>{"e1","e2"},
                    new List<string>{"f1","f2"}
                }
            };
            var vista = new Vista();
            var output = new StringWriter();
            Console.SetOut(output);
            // When
            vista.MostrarParrilla("hola", datos);
            // Then

            Assert.True(output.ToString().Contains("|a1        |b1        |"));
            Assert.True(output.ToString().Contains("|a2        |b2        |"));
            Assert.True(output.ToString().Contains("|e1        |f1        |"));
            Assert.True(output.ToString().Contains("|e2        |f2        |"));
            //....
        }


    }
}
