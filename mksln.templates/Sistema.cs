using System.Collections.Generic;
using System.Linq;
using MINAMESPACE.Modelos;
using MINAMESPACE.Data;

namespace MINAMESPACE
{
    public class MISISTEMA
    {
        IRepo _repositorio;
        public MISISTEMA(IRepo repositorio)
        {
            _repositorio = repositorio;
        }

    }
}
