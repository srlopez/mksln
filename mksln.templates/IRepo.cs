using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace MINAMESPACE.Data
{
    using MINAMESPACE.Modelos;

    public interface IRepo
    {
        void Inicializar();
        Modelo Cargar();
        void Guardar(Modelo data);
    }
}