using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Analysis;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risk_Management
{
    class Util
    {
        public static string RealString(double a)
        {
            return a.ToString("0.##");
        }

        public static string PointString(XYZ p)
        {
            return string.Format("({0},{1},{2})",
              RealString(p.X),
              RealString(p.Y),
              RealString(p.Z));
        }

        public static void ShowTaskDialog(string title, string info, string content)
        {
            Debug.Print(title + ": " + info + ": " + content);

            TaskDialog dialog = new TaskDialog(title);
            dialog.MainInstruction = info;
            dialog.MainContent = content;
            dialog.Show();
        }

        public static void ShowElapsedTime(Stopwatch stopwatch, string info)
        {
            Debug.Print(stopwatch.ElapsedMilliseconds.ToString() + " milliseconds");

            TaskDialog dialog = new TaskDialog("Time");
            dialog.MainInstruction = "milliseconds: " + stopwatch.ElapsedMilliseconds.ToString();
            dialog.MainContent = info;
            dialog.Show();
        }

        public static Material FindMaterial(Document doc, string name)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(Material));

            // Now use LINQ to see if one exists with provided name.
            Material materialReturn = null;
            try
            {
                Element e = collector.First(material => material.Name.Equals(name));
                materialReturn = e as Material;
                if (e != null)
                    return materialReturn;
            }
            catch (System.InvalidOperationException)
            {
            }
            catch (System.ArgumentNullException)
            {
            }
            return materialReturn;
        }

        public static ElementId FindLevelId(Document doc, string name)
        {
            // Simple example of finding all levels.
            FilteredElementCollector collectorLevels = new FilteredElementCollector(doc);
            collectorLevels.OfClass(typeof(Level));

            // Now use LINQ to see if one exists with provided name.
            ElementId idLevel = ElementId.InvalidElementId;
            try
            {
                Element levelMatched = collectorLevels.First(level => level.Name.Equals(name));
                if (levelMatched != null)
                    idLevel = levelMatched.Id;
            }
            catch (System.InvalidOperationException)
            {
            }
            catch (System.ArgumentNullException)
            {
            }
            return idLevel;
        }

        public static Level FindLevel(Document doc, string name)
        {
            // Simple example of finding all levels.
            FilteredElementCollector collectorLevels = new FilteredElementCollector(doc);
            collectorLevels.OfClass(typeof(Level));

            // Now use LINQ to see if one exists with provided name.
            Level levelMatched = null;
            try
            {
                levelMatched = collectorLevels.First(level => level.Name.Equals(name)) as Level;
            }
            catch (System.InvalidOperationException)
            {
            }
            catch (System.ArgumentNullException)
            {
            }
            return levelMatched;
        }

        public static ElementId FindAnalysisDisplayStyleId(Document doc, string name)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(AnalysisDisplayStyle));

            // Now use LINQ to see if one exists with provided name.
            ElementId idAds = ElementId.InvalidElementId;
            try
            {
                Element adsMatched = collector.First(ads => ads.Name.Equals(name));
                if (adsMatched != null)
                    idAds = adsMatched.Id;
            }
            catch (System.InvalidOperationException)
            {
            }
            catch (System.ArgumentNullException)
            {
            }
            return idAds;
        }

    }
}
