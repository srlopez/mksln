using System;
using System.IO;
using Newtonsoft.Json;


namespace MINAMESPACE.Data
{
    using MINAMESPACE.Modelos;

    public class RepoJson : IRepo
    {
        private string _dataPath;
        private string _file;

        void IRepo.Inicializar()
        {
            // TODO: Parametizar 
            _dataPath = "../../data/";
            _file = _dataPath + "Modelo.json";
        }
        void IRepo.Guardar(Modelo modelo)
        {
            string json = JsonConvert.SerializeObject(modelo, Formatting.Indented);
            File.WriteAllText(_file, json);
        }
        Modelo IRepo.Cargar()
        {
            var txtJson = File.ReadAllText(_file);
            Modelo modelo = JsonConvert.DeserializeObject<Modelo>(txtJson);
            return modelo;
        }
    }
}



