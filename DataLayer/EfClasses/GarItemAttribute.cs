using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.EfClasses
{
    public class GarItemAttribute : Attribute
    {
        public string Name { get; private set; }
        public GarItemAttribute(string name)
        {
            Name = name;
        }
    }
}