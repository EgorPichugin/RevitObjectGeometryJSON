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
using System.Text.Json.Nodes;
using System.Collections;

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
            //Reference wRef = commandData.Application.ActiveUIDocument.Selection.PickObject(ObjectType.Element);
            //Element wElem = doc.GetElement(wRef.ElementId);
            //tmp
            //TaskDialog.Show("id", wElem.UniqueId);
            //tmp
            // path to save
            //string filePath = $"C:\\Users\\pichu\\OneDrive\\Документы\\DAE_for_Unity\\BuildingTest\\{doc.Title}.txt";
            //string info = string.Empty;

            //List<string> strings = new List<string>();
            //Options sgo = new Options();
            //GeometryElement gElem = ((Element)wElem).get_Geometry(sgo);

            //GeomObject geomObj = new GeomObject();
            //geomObj.UniqueID = wElem.UniqueId;
            //geomObj.material = new List<double> { 1, 0, 0 };
            //foreach (GeometryObject gObj in gElem)
            //{
            //    //strings.Add($"wall: {wElem.Id}\n");
            //    Solid solid = gObj as Solid;
            //    if (solid != null)
            //    {
            //        foreach (Face face in solid.Faces)
            //        {
                        
            //            Mesh mesh = face.Triangulate();
            //            PlanarFace planarFace = face as PlanarFace;
            //            XYZ n = planarFace.FaceNormal;

                        
            //            List<double> normals = new List<double> { n.X, n.Y, n.Z };

            //            for (int i = 0; i < mesh.NumTriangles; i++)
            //            {
            //                List<double> triangles = new List<double>();
            //                MeshTriangle mtrngl = mesh.get_Triangle(i);
            //                triangles.Add(mtrngl.get_Vertex(0).X);
            //                triangles.Add(mtrngl.get_Vertex(0).Y);
            //                triangles.Add(mtrngl.get_Vertex(0).Z);
            //                triangles.Add(mtrngl.get_Vertex(1).X);
            //                triangles.Add(mtrngl.get_Vertex(1).Y);
            //                triangles.Add(mtrngl.get_Vertex(1).Z);
            //                triangles.Add(mtrngl.get_Vertex(2).X);
            //                triangles.Add(mtrngl.get_Vertex(2).Y);
            //                triangles.Add(mtrngl.get_Vertex(2).Z);
            //                Dictionary<string, List<double>> tri = new Dictionary<string, List<double>> { { "vertices", triangles }, { "normals", normals } };
            //                geomObj.geometry.Add(tri);
            //            }
            //        }
            //    }
            //    else if (gObj is GeometryInstance)
            //    {
            //        GeometryInstance geoInst = gObj as GeometryInstance;

            //        GeometryElement geoElem = geoInst.SymbolGeometry;

            //        Transform transform = geoInst.Transform;

            //        XYZ b0 = transform.get_Basis(0);
            //        XYZ b1 = transform.get_Basis(1);
            //        XYZ b2 = transform.get_Basis(2);
            //        XYZ origin = transform.Origin;

            //        foreach (GeometryObject go in geoElem)
            //        {
            //            Solid solid2 = go as Solid;

            //            if (solid2 != null)
            //            {
            //                foreach (Face face in solid2.Faces)
            //                {

            //                    Mesh mesh = face.Triangulate();
            //                    PlanarFace planarFace = face as PlanarFace;
            //                    XYZ n = planarFace.FaceNormal;


            //                    List<double> normals = new List<double> { n.X, n.Y, n.Z };

            //                    for (int i = 0; i < mesh.NumTriangles; i++)
            //                    {
            //                        List<double> triangles = new List<double>();
            //                        MeshTriangle mtrngl = mesh.get_Triangle(i);
            //                        triangles.Add(mtrngl.get_Vertex(0).X * b0.X + mtrngl.get_Vertex(0).Y * b1.X + mtrngl.get_Vertex(0).Z * b2.X + origin.X);
            //                        triangles.Add(mtrngl.get_Vertex(0).X * b0.Y + mtrngl.get_Vertex(0).Y * b1.Y + mtrngl.get_Vertex(0).Z * b2.Y + origin.Y);
            //                        triangles.Add(mtrngl.get_Vertex(0).X * b0.Z + mtrngl.get_Vertex(0).Y * b1.Z + mtrngl.get_Vertex(0).Z * b2.Z + origin.Z);
            //                        triangles.Add(mtrngl.get_Vertex(1).X * b0.X + mtrngl.get_Vertex(1).Y * b1.X + mtrngl.get_Vertex(1).Z * b2.X + origin.X);
            //                        triangles.Add(mtrngl.get_Vertex(1).X * b0.Y + mtrngl.get_Vertex(1).Y * b1.Y + mtrngl.get_Vertex(1).Z * b2.Y + origin.Y);
            //                        triangles.Add(mtrngl.get_Vertex(1).X * b0.Z + mtrngl.get_Vertex(1).Y * b1.Z + mtrngl.get_Vertex(1).Z * b2.Z + origin.Z);
            //                        triangles.Add(mtrngl.get_Vertex(2).X * b0.X + mtrngl.get_Vertex(2).Y * b1.X + mtrngl.get_Vertex(2).Z * b2.X + origin.X);
            //                        triangles.Add(mtrngl.get_Vertex(2).X * b0.Y + mtrngl.get_Vertex(2).Y * b1.Y + mtrngl.get_Vertex(2).Z * b2.Y + origin.Y);
            //                        triangles.Add(mtrngl.get_Vertex(2).X * b0.Z + mtrngl.get_Vertex(2).Y * b1.Z + mtrngl.get_Vertex(2).Z * b2.Z + origin.Z);

            //                        //triangles.Add(mtrngl.get_Vertex(0).X);
            //                        //triangles.Add(mtrngl.get_Vertex(0).Y);
            //                        //triangles.Add(mtrngl.get_Vertex(0).Z);
            //                        //triangles.Add(mtrngl.get_Vertex(1).X);
            //                        //triangles.Add(mtrngl.get_Vertex(1).Y);
            //                        //triangles.Add(mtrngl.get_Vertex(1).Z);
            //                        //triangles.Add(mtrngl.get_Vertex(2).X);
            //                        //triangles.Add(mtrngl.get_Vertex(2).Y);
            //                        //triangles.Add(mtrngl.get_Vertex(2).Z);
            //                        Dictionary<string, List<double>> tri = new Dictionary<string, List<double>> { { "vertices", triangles }, { "normals", normals } };
            //                        geomObj.geometry.Add(tri);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}


            List<Level> levels = new FilteredElementCollector(doc)
                .WhereElementIsNotElementType()
                .OfCategory(BuiltInCategory.OST_Levels)
                .OrderBy(e => ((Level)e).Elevation)
                .Select(e => (Level)e)
                .ToList();

            

            foreach (Level level in levels)
            {
                RVTlevel rvtLevel = new RVTlevel(level);
                RVTlevels.rvtLevels[level.Id.IntegerValue] = rvtLevel;

            }

            

            foreach (BuiltInCategory bltnCat in StaticData.BuiltCats)
            {
                List<Element> elems = new FilteredElementCollector(doc, doc.ActiveView.Id)
                .WhereElementIsNotElementType()
                .OfCategory(bltnCat)
                .ToList();

                foreach (Element elem in elems)
                {
                    Options sgo = new Options();
                    GeometryElement gElem = ((Element)elem).get_Geometry(sgo);
                    GeomObject geomObj = new GeomObject();

                    // find color
                    //ElementId matId = elem.get_Parameter(BuiltInParameter.MATERIAL_ID_PARAM).AsElementId();
                    //List<double> RVTcolor = new List<double>();
                    //if (matId != null)
                    //{
                    //    Material mat = doc.GetElement(matId) as Material;
                    //    Color color = mat.Color;
                    //    RVTcolor.Add(color.Red / 255);
                    //    RVTcolor.Add(color.Green / 255);
                    //    RVTcolor.Add(color.Blue / 255);
                    //}

                    //if (RVTcolor.Count > 0)
                    //    geomObj.material = RVTcolor;
                    //else
                    geomObj.material = new List<double> { 1, 1, 1 };

                    foreach (GeometryObject gObj in gElem)
                    {
                        //strings.Add($"wall: {wElem.Id}\n");
                        Solid solid = gObj as Solid;
                        if (solid != null)
                        {
                            foreach (Face face in solid.Faces)
                            {
                                //tmp face material
                                ElementId mtrlId = face.MaterialElementId;
                                Material mtrl = (Material)doc.GetElement(mtrlId);
                                Color color = mtrl.Color;
                                byte r = color.Red;
                                byte g = color.Green;
                                byte b = color.Blue;
                                //tmp

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
                            }
                        }
                        else if (gObj is GeometryInstance)
                        {
                            GeometryInstance geoInst = gObj as GeometryInstance;

                            GeometryElement geoElem = geoInst.SymbolGeometry;

                            Transform transform = geoInst.Transform;

                            XYZ b0 = transform.get_Basis(0);
                            XYZ b1 = transform.get_Basis(1);
                            XYZ b2 = transform.get_Basis(2);
                            XYZ origin = transform.Origin;

                            foreach (GeometryObject go in geoElem)
                            {
                                Solid solid2 = go as Solid;

                                if (solid2 != null)
                                {
                                    foreach (Face face in solid2.Faces)
                                    {

                                        Mesh mesh = face.Triangulate();
                                        PlanarFace planarFace = face as PlanarFace;
                                        XYZ n = planarFace.FaceNormal;


                                        List<double> normals = new List<double> { n.X, n.Y, n.Z };

                                        for (int i = 0; i < mesh.NumTriangles; i++)
                                        {
                                            List<double> triangles = new List<double>();
                                            MeshTriangle mtrngl = mesh.get_Triangle(i);
                                            triangles.Add(mtrngl.get_Vertex(0).X * b0.X + mtrngl.get_Vertex(0).Y * b1.X + mtrngl.get_Vertex(0).Z * b2.X + origin.X);
                                            triangles.Add(mtrngl.get_Vertex(0).X * b0.Y + mtrngl.get_Vertex(0).Y * b1.Y + mtrngl.get_Vertex(0).Z * b2.Y + origin.Y);
                                            triangles.Add(mtrngl.get_Vertex(0).X * b0.Z + mtrngl.get_Vertex(0).Y * b1.Z + mtrngl.get_Vertex(0).Z * b2.Z + origin.Z);
                                            triangles.Add(mtrngl.get_Vertex(1).X * b0.X + mtrngl.get_Vertex(1).Y * b1.X + mtrngl.get_Vertex(1).Z * b2.X + origin.X);
                                            triangles.Add(mtrngl.get_Vertex(1).X * b0.Y + mtrngl.get_Vertex(1).Y * b1.Y + mtrngl.get_Vertex(1).Z * b2.Y + origin.Y);
                                            triangles.Add(mtrngl.get_Vertex(1).X * b0.Z + mtrngl.get_Vertex(1).Y * b1.Z + mtrngl.get_Vertex(1).Z * b2.Z + origin.Z);
                                            triangles.Add(mtrngl.get_Vertex(2).X * b0.X + mtrngl.get_Vertex(2).Y * b1.X + mtrngl.get_Vertex(2).Z * b2.X + origin.X);
                                            triangles.Add(mtrngl.get_Vertex(2).X * b0.Y + mtrngl.get_Vertex(2).Y * b1.Y + mtrngl.get_Vertex(2).Z * b2.Y + origin.Y);
                                            triangles.Add(mtrngl.get_Vertex(2).X * b0.Z + mtrngl.get_Vertex(2).Y * b1.Z + mtrngl.get_Vertex(2).Z * b2.Z + origin.Z);

                                            //triangles.Add(mtrngl.get_Vertex(0).X);
                                            //triangles.Add(mtrngl.get_Vertex(0).Y);
                                            //triangles.Add(mtrngl.get_Vertex(0).Z);
                                            //triangles.Add(mtrngl.get_Vertex(1).X);
                                            //triangles.Add(mtrngl.get_Vertex(1).Y);
                                            //triangles.Add(mtrngl.get_Vertex(1).Z);
                                            //triangles.Add(mtrngl.get_Vertex(2).X);
                                            //triangles.Add(mtrngl.get_Vertex(2).Y);
                                            //triangles.Add(mtrngl.get_Vertex(2).Z);
                                            Dictionary<string, List<double>> tri = new Dictionary<string, List<double>> { { "vertices", triangles }, { "normals", normals } };
                                            geomObj.geometry.Add(tri);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //
                    RVTlevel rvtLevel = RVTlevels.rvtLevels[elem.LevelId.IntegerValue];
                    
                    if (rvtLevel.Categories.ContainsKey(elem.Category.Name))
                    {
                        rvtLevel.Categories[elem.Category.Name].Add(new Dictionary<string, GeomObject> { { elem.UniqueId.ToString(), geomObj } });
                    }
                    //
                }
            }

            //TaskDialog.Show("info", str);
            //tmp
            //List<Element> levels = new FilteredElementCollector(doc)
            //    .WhereElementIsNotElementType()
            //    .OfCategory(BuiltInCategory.OST_Levels)
            //    .OrderBy(e => ((Level)e).Elevation)
            //    .ToList();

            //RVTmodel rvtModel = new RVTmodel();

            //foreach (Element l in levels)
            //{
            //    RVTlevel rvtLevel = new RVTlevel { Height = ((Level)l).Elevation};

            //    rvtModel.ModelInfo[l.Name] = rvtLevel;
            //}

            //Dictionary<string, Dictionary<string, RVTlevel>> d = new Dictionary<string, Dictionary<string, RVTlevel>> { { doc.Title, rvtModel.ModelInfo } };
            ////tmp

            Dictionary<string, object> allLevels = new Dictionary<string, object>();
            foreach (RVTlevel rvtLvl in RVTlevels.rvtLevels.Values)
            {
                allLevels[rvtLvl.Name] = rvtLvl.Categories;
            }
            Dictionary<string, object> jsonObj = new Dictionary<string, object> { { $"{doc.Title}", allLevels } };
            string filePath = $"C:\\Users\\pichu\\OneDrive\\Документы\\DAE_for_Unity\\BuildingTest\\{doc.Title}.json";
            string jsonString = JsonSerializer.Serialize(jsonObj, new JsonSerializerOptions { WriteIndented = true });

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine(jsonString);
            }

            return Result.Succeeded;
        }
    }
}
