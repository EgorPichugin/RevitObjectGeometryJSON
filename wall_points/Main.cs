using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace wall_points
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;

            // all levels
            List<Element> lvls = new FilteredElementCollector(doc)
                .WhereElementIsNotElementType()
                .OfCategory(BuiltInCategory.OST_Levels)
                .OrderBy(e => ((Level)e).Elevation)
                .ToList();

            // level id
            Level lowLvl = (Level)lvls[0];
            ElementId elemId = lowLvl.Id;

            // pick element
            Reference wRef = commandData.Application.ActiveUIDocument.Selection.PickObject(ObjectType.Element);
            Element wElem = doc.GetElement(wRef.ElementId);

            // path to save
            string filePath = $"C:\\Users\\pichu\\OneDrive\\Документы\\DAE_for_Unity\\DAE_builder\\BuidingTest\\{wElem.Id}.txt";
            string info = string.Empty;

            //List<string> strings = new List<string>();
            Options sgo = new Options();
            GeometryElement gElem = ((Element)wElem).get_Geometry(sgo);

            GeomObject geomObj = new GeomObject();
            geomObj.ID = wElem.Id.IntegerValue;
            geomObj.material = new List<double> { 1, 0, 0 };
            foreach (GeometryObject gObj in gElem)
            {
                //strings.Add($"wall: {wElem.Id}\n");
                Solid solid = gObj as Solid;
                if (solid != null)
                {
                    foreach (Face face in solid.Faces)
                    {
                        
                        Mesh mesh = face.Triangulate();
                        PlanarFace planarFace = face as PlanarFace;
                        XYZ n = planarFace.FaceNormal;

                        
                        List<double> normals = new List<double> { n.X, n.Y, n.Z };

                        for (int i = 0; i < mesh.NumTriangles; i++)
                        {
                            List<double> triangles = new List<double>();
                            MeshTriangle mtrngl = mesh.get_Triangle(i);
                            triangles.Add(mtrngl.get_Vertex(0).X);
                            triangles.Add(mtrngl.get_Vertex(0).Y);
                            triangles.Add(mtrngl.get_Vertex(0).Z);
                            triangles.Add(mtrngl.get_Vertex(1).X);
                            triangles.Add(mtrngl.get_Vertex(1).Y);
                            triangles.Add(mtrngl.get_Vertex(1).Z);
                            triangles.Add(mtrngl.get_Vertex(2).X);
                            triangles.Add(mtrngl.get_Vertex(2).Y);
                            triangles.Add(mtrngl.get_Vertex(2).Z);
                            Dictionary<string, List<double>> tri = new Dictionary<string, List<double>> { { "vertices", triangles }, { "normals", normals } };
                            geomObj.geometry.Add(tri);
                        }
                        //foreach (XYZ ii in mesh.Vertices)
                        //{
                        //    triangles.Add (ii.X);
                        //    triangles.Add (ii.Y);
                        //    triangles.Add (ii.Z);
                        //    //List<Dictionary<string, List<double>>> list = new List<Dictionary<string, List<double>>>();
                        //    //List<double> vertices = new List<double> { ii.X, ii.Y, ii.Z };
                        //    //List<double> normals = new List<double> { n.X, n.Y, n.Z };
                        //    //Dictionary<string, List<double>> tri = new Dictionary<string, List<double>> { { "vertices", vertices }, { "normals", normals } };
                        //    //geomObj.geometry.Add(tri);
                        //    //string temp = $"x: {ii.X.ToString().PadRight(20)}| y: {ii.Y.ToString().PadRight(20)}| z: {ii.Z.ToString().PadRight(20)}";

                        //    //strings.Add(temp);
                        //}
                        
                        
                        //strings.Add("------------------------------------------------");
                    }
                }
            }

            //foreach (string str in strings)
            //{
            //    info += str + '\n';
            //}
            //info += '\n';
            // tmp


            //string filePath = "C:\\Users\\pichu\\OneDrive\\Документы\\DAE_for_Unity\\coords.txt";
            //string info = string.Empty;

            //foreach (Line line in lines)
            //{
            //    Wall w = null;
            //    using (Transaction tr = new Transaction(doc, "Creating walls"))
            //    {
            //        try
            //        {
            //            tr.Start();

            //            w = Wall.Create(doc, line, elemId, true);

            //            tr.Commit();
            //        }
            //        catch (Exception ex)
            //        {
            //            tr.RollBack();
            //        }
            //    }

            //    List<string> strings = new List<string>();
            //    Options sgo = new Options();
            //    GeometryElement gElem = ((Element)w).get_Geometry(sgo);

            //    foreach (GeometryObject gObj in gElem)
            //    {
            //        strings.Add($"wall: {w.Id}\n");
            //        Solid solid = gObj as Solid;
            //        if (solid != null)
            //        {
            //            foreach (Face face in solid.Faces)
            //            {
            //                Mesh mesh = face.Triangulate(1);

            //                foreach (XYZ ii in mesh.Vertices)
            //                {
            //                    string temp = $"x: {ii.X.ToString().PadRight(20)}| y: {ii.Y.ToString().PadRight(20)}| z: {ii.Z.ToString().PadRight(20)}";

            //                    if (!strings.Contains(temp))
            //                    {
            //                        strings.Add(temp);
            //                    }
            //                }
            //            }
            //        }
            //    }

            //    foreach (string str in strings)
            //    {
            //        info += str + '\n';
            //    }
            //    info += '\n';
            //}

            

            string jsonString = JsonSerializer.Serialize(geomObj, new JsonSerializerOptions { WriteIndented = true });

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(jsonString);
            }

            return Result.Succeeded;
        }
    }
}
