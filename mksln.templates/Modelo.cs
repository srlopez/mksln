using System;

namespace MINAMESPACE.Modelos
{
    public class Modelo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        
        public override string ToString() => $"{Id} {Name}";
    }
}