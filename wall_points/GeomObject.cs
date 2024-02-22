using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wall_points
{
    public class GeomObject
    {
        public int ID {  get; set; }
        public List<Dictionary<string, List<double>>> geometry { get; set; } = new List<Dictionary<string, List<double>>>();
        public List<double> material { get; set; } = null;
    }
}
