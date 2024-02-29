using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace wall_points
{
    public class RVTlevel
    {
        public int ID;
        public string Name;
        public Dictionary<string, List<Dictionary<string, GeomObject>>> Categories = new Dictionary<string, List<Dictionary<string, GeomObject>>>();

        public RVTlevel(Level level) 
        {
            ID = level.Id.IntegerValue;
            Name = level.Name.ToString();
            foreach (BuiltInCategory car in StaticData.BuiltCats)
            {
                Categories[car.ToString().Replace("OST_", "")] = new List<Dictionary<string, GeomObject>>();
            }
        }
    }
    
}
