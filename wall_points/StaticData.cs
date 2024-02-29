using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wall_points
{
    public static class StaticData
    {
        public static List<BuiltInCategory> BuiltCats = new List<BuiltInCategory> { 
            BuiltInCategory.OST_Walls, 
            //BuiltInCategory.OST_Floors,
            //BuiltInCategory.OST_Roofs,
            //BuiltInCategory.OST_Windows,
            //BuiltInCategory.OST_Doors,
            //BuiltInCategory.OST_Columns,
            //BuiltInCategory.OST_StructuralColumns,
        };
    }

    public static class RVTlevels
    {
        public static Dictionary<int, RVTlevel> rvtLevels = new Dictionary<int, RVTlevel>();
    }
}
