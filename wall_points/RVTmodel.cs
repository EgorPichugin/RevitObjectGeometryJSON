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
        public Dictionary<string, Dictionary<string, object>> Categories = new Dictionary<string, Dictionary<string, object>>();

        public RVTlevel(Level level) 
        {
            ID = level.Id.IntegerValue;
            Name = level.Name.ToString();
            foreach (BuiltInCategory car in StaticData.BuiltCats)
            {
                Categories[car.ToString().Replace("OST_", "")] = new Dictionary<string, object>();
            }
        }
    }
    
}
