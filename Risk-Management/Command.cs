#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using CsvHelper;
#endregion

namespace Risk_Management
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        static string _level1_name = "FOUNDATION";
        static string _level2_name = "BAS";
        static string _level3_name = "GRD";
        static string _level4_name = "1ST";
        static string _level5_name = "2ND";
        static string _level6_name = "3RD";
        static string _level7_name = "L.ROOF";
        static string _level8_name = "U.ROOF";
        private CsvData _csvData;
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            // Access current selection

            #region Welcome window
            WelcomeWindow x = new WelcomeWindow();
            x.Height = 425;
            x.Width = 673.171;
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = x.Width;
            double windowHeight = x.Height;
            x.Left = (screenWidth / 2) - (windowWidth / 2);
            x.Top = (screenHeight / 2) - (windowHeight / 2);
            x.ShowDialog();
            string path = x.ExcelLocation;
            #endregion

            Selection sel = uidoc.Selection;

            // Retrieve elements from database

            FilteredElementCollector col
              = new FilteredElementCollector(doc)
                .WhereElementIsNotElementType()
                .OfCategory(BuiltInCategory.INVALID)
                .OfClass(typeof(Wall));

            #region Get Excel Data
            path = Environment.ExpandEnvironmentVariables(path);
            _csvData = new CsvData();
            var Data = _csvData.CsvDataa(path);
            //_csvData.SetStyle("2");

            #endregion
            // Modify document within a transaction

            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Transaction Name");
                var collector = new FilteredElementCollector(doc);
                var viewFamilyType = collector.OfClass(typeof(ViewFamilyType)).Cast<ViewFamilyType>()
                  .FirstOrDefault(m => m.ViewFamily == ViewFamily.ThreeDimensional);
                var CostImpactView=View3D.CreateIsometric(doc, viewFamilyType.Id);
                CostImpactView.Name = "Cost Risk Impact";
                CostImpactView.DisplayStyle = DisplayStyle.ShadingWithEdges;
                var TimeImpactView = View3D.CreateIsometric(doc, viewFamilyType.Id);
                TimeImpactView.DisplayStyle = DisplayStyle.ShadingWithEdges;
                TimeImpactView.Name = "Time Risk Impact";


                #region Get Elements Filters
                #region     LevelFilter         
                /*
                ElementLevelFilter filterElementsOnLevel = new ElementLevelFilter(levelId);
                FilteredElementCollector collector1 = new FilteredElementCollector(doc);
                collector1.OfClass(typeof(FamilyInstance)) .WherePasses(filterElementsOnLevel);
                ICollection<Element> listLevel = collector1.ToElements();
                */
                #endregion
                #region     Parameter Filter
                /*
                ElementId level1Id = Util.FindLevelId(doc, _level1_name);
                //rule parameters
                BuiltInParameter LBIP = BuiltInParameter.FAMILY_LEVEL_PARAM;
                ParameterValueProvider provider = new ParameterValueProvider(new ElementId(LBIP));
                FilterNumericRuleEvaluator evaluator = new FilterNumericEquals();
                //rule
                FilterRule LevelRule = new FilterElementIdRule(provider, evaluator, level1Id);
                //filter: using rule
                ElementParameterFilter LevelFilter1x = new ElementParameterFilter(LevelRule);
                */
                #endregion

                //Foundation Level
                #region ElementParameterFilter

                ElementId level1Id = Util.FindLevelId(doc, _level1_name);

                ElementLevelFilter LevelFilter1 = new ElementLevelFilter(level1Id);

                //rule parameters
                BuiltInParameter XLBIP = BuiltInParameter.INSTANCE_REFERENCE_LEVEL_PARAM;
                ParameterValueProvider provider = new ParameterValueProvider(new ElementId(XLBIP));
                FilterNumericRuleEvaluator evaluator = new FilterNumericEquals();
                //rule
                FilterRule LevelRule = new FilterElementIdRule(provider, evaluator, level1Id);
                //filter: using rule
                ElementParameterFilter LevelFilter1x = new ElementParameterFilter(LevelRule);
                #endregion

                //Basement Level
                #region ElementParameterFilter
                BuiltInParameter XXLBIP = BuiltInParameter.STAIRS_BASE_LEVEL_PARAM;
                ElementId level2Id = Util.FindLevelId(doc, _level2_name);
                ElementLevelFilter Level2Filter = new ElementLevelFilter(level2Id);

                //rule parameters
                ParameterValueProvider provider2 = new ParameterValueProvider(new ElementId(XLBIP));
                FilterNumericRuleEvaluator evaluator2 = new FilterNumericEquals();
                //rule
                FilterRule LevelRule2 = new FilterElementIdRule(provider2, evaluator2, level2Id);
                //filter: using rule
                ElementParameterFilter LevelFilter2x = new ElementParameterFilter(LevelRule2);

                //rule parameters
                ParameterValueProvider provider2xx = new ParameterValueProvider(new ElementId(XXLBIP));
                FilterNumericRuleEvaluator evaluator2xx = new FilterNumericEquals();
                //rule
                FilterRule LevelRule2xx = new FilterElementIdRule(provider2xx, evaluator2xx, level2Id);
                //filter: using rule
                ElementParameterFilter LevelFilter2xx = new ElementParameterFilter(LevelRule2xx);
                #endregion

                //Ground Level
                #region ElementParameterFilter
                ElementId level3Id = Util.FindLevelId(doc, _level3_name);
                ElementLevelFilter Level3Filter = new ElementLevelFilter(level3Id);

                //rule parameters
                ParameterValueProvider provider3 = new ParameterValueProvider(new ElementId(XLBIP));
                FilterNumericRuleEvaluator evaluator3 = new FilterNumericEquals();
                //rule
                FilterRule LevelRule3 = new FilterElementIdRule(provider3, evaluator3, level3Id);
                //filter: using rule
                ElementParameterFilter LevelFilter3x = new ElementParameterFilter(LevelRule3);

                //rule parameters
                ParameterValueProvider provider3xx = new ParameterValueProvider(new ElementId(XXLBIP));
                FilterNumericRuleEvaluator evaluator3xx = new FilterNumericEquals();
                //rule
                FilterRule LevelRule3xx = new FilterElementIdRule(provider3xx, evaluator3xx, level3Id);
                //filter: using rule
                ElementParameterFilter LevelFilter3xx = new ElementParameterFilter(LevelRule3xx);
                #endregion

                //First Level
                #region ElementParameterFilter
                ElementId level4Id = Util.FindLevelId(doc, _level4_name);
                ElementLevelFilter Level4Filter = new ElementLevelFilter(level4Id);

                //rule parameters
                ParameterValueProvider provider4 = new ParameterValueProvider(new ElementId(XLBIP));
                FilterNumericRuleEvaluator evaluator4 = new FilterNumericEquals();
                //rule
                FilterRule LevelRule4 = new FilterElementIdRule(provider4, evaluator4, level4Id);
                //filter: using rule
                ElementParameterFilter LevelFilter4x = new ElementParameterFilter(LevelRule4);

                //rule parameters
                ParameterValueProvider provider4xx = new ParameterValueProvider(new ElementId(XXLBIP));
                FilterNumericRuleEvaluator evaluator4xx = new FilterNumericEquals();
                //rule
                FilterRule LevelRule4xx = new FilterElementIdRule(provider4xx, evaluator4xx, level4Id);
                //filter: using rule
                ElementParameterFilter LevelFilter4xx = new ElementParameterFilter(LevelRule4xx);
                #endregion

                //Second Level
                #region ElementParameterFilter
                ElementId level5Id = Util.FindLevelId(doc, _level5_name);
                ElementLevelFilter Level5Filter = new ElementLevelFilter(level5Id);

                //rule parameters
                ParameterValueProvider provider5 = new ParameterValueProvider(new ElementId(XLBIP));
                FilterNumericRuleEvaluator evaluator5 = new FilterNumericEquals();
                //rule
                FilterRule LevelRule5 = new FilterElementIdRule(provider5, evaluator5, level5Id);
                //filter: using rule
                ElementParameterFilter LevelFilter5x = new ElementParameterFilter(LevelRule5);

                //rule parameters
                ParameterValueProvider provider5xx = new ParameterValueProvider(new ElementId(XXLBIP));
                FilterNumericRuleEvaluator evaluator5xx = new FilterNumericEquals();
                //rule
                FilterRule LevelRule5xx = new FilterElementIdRule(provider5xx, evaluator5xx, level5Id);
                //filter: using rule
                ElementParameterFilter LevelFilter5xx = new ElementParameterFilter(LevelRule5xx);
                #endregion

                //Third Level
                #region ElementParameterFilter
                ElementId level6Id = Util.FindLevelId(doc, _level6_name);
                ElementLevelFilter Level6Filter = new ElementLevelFilter(level6Id);

                //rule parameters
                ParameterValueProvider provider6 = new ParameterValueProvider(new ElementId(XLBIP));
                FilterNumericRuleEvaluator evaluator6 = new FilterNumericEquals();
                //rule
                FilterRule LevelRule6 = new FilterElementIdRule(provider6, evaluator6, level6Id);
                //filter: using rule
                ElementParameterFilter LevelFilter6x = new ElementParameterFilter(LevelRule6);

                //rule parameters
                ParameterValueProvider provider6xx = new ParameterValueProvider(new ElementId(XXLBIP));
                FilterNumericRuleEvaluator evaluator6xx = new FilterNumericEquals();
                //rule
                FilterRule LevelRule6xx = new FilterElementIdRule(provider6xx, evaluator6xx, level6Id);
                //filter: using rule
                ElementParameterFilter LevelFilter6xx = new ElementParameterFilter(LevelRule6xx);
                #endregion

                //Lower Roof Level
                #region ElementParameterFilter
                ElementId level7Id = Util.FindLevelId(doc, _level7_name);
                ElementLevelFilter Level7Filter = new ElementLevelFilter(level7Id);

                //rule parameters
                ParameterValueProvider provider7 = new ParameterValueProvider(new ElementId(XLBIP));
                FilterNumericRuleEvaluator evaluator7 = new FilterNumericEquals();
                //rule
                FilterRule LevelRule7 = new FilterElementIdRule(provider7, evaluator7, level7Id);
                //filter: using rule
                ElementParameterFilter LevelFilter7x = new ElementParameterFilter(LevelRule7);

                //rule parameters
                ParameterValueProvider provider7xx = new ParameterValueProvider(new ElementId(XXLBIP));
                FilterNumericRuleEvaluator evaluator7xx = new FilterNumericEquals();
                //rule
                FilterRule LevelRule7xx = new FilterElementIdRule(provider7xx, evaluator7xx, level7Id);
                //filter: using rule
                ElementParameterFilter LevelFilter7xx = new ElementParameterFilter(LevelRule7xx);
                #endregion

                //Lower Roof Level
                #region ElementParameterFilter
                ElementId level8Id = Util.FindLevelId(doc, _level8_name);
                ElementLevelFilter Level8Filter = new ElementLevelFilter(level8Id);

                //rule parameters
                ParameterValueProvider provider8 = new ParameterValueProvider(new ElementId(XLBIP));
                FilterNumericRuleEvaluator evaluator8 = new FilterNumericEquals();
                //rule
                FilterRule LevelRule8 = new FilterElementIdRule(provider8, evaluator8, level8Id);
                //filter: using rule
                ElementParameterFilter LevelFilter8x = new ElementParameterFilter(LevelRule8);

                //rule parameters
                ParameterValueProvider provider8xx = new ParameterValueProvider(new ElementId(XXLBIP));
                FilterNumericRuleEvaluator evaluator8xx = new FilterNumericEquals();
                //rule
                FilterRule LevelRule8xx = new FilterElementIdRule(provider8xx, evaluator8xx, level8Id);
                //filter: using rule
                ElementParameterFilter LevelFilter8xx = new ElementParameterFilter(LevelRule8xx);
                #endregion


                #region 1 PC Foundation Works
                //Category Filter:StructuralFoundation
                BuiltInCategory[] bicSF = new BuiltInCategory[] { BuiltInCategory.OST_StructuralFoundation };
                IList<ElementFilter> a1 = new List<ElementFilter>(bicSF.Count());
                foreach (BuiltInCategory bic in bicSF)
                {
                    a1.Add(new ElementCategoryFilter(bic));
                }
                LogicalOrFilter categoryFilter1 = new LogicalOrFilter(a1);
                LogicalAndFilter familyInstanceFilter1 = new LogicalAndFilter(categoryFilter1, new ElementClassFilter(typeof(FamilyInstance)));
                IList<ElementFilter> b1 = new List<ElementFilter>(1);
                b1.Add(familyInstanceFilter1);
                LogicalOrFilter CatFilter1 = new LogicalOrFilter(b1);
                //Rule Type Comment 1
                BuiltInParameter bipTC = BuiltInParameter.ALL_MODEL_TYPE_COMMENTS;
                ParameterValueProvider provider1 = new ParameterValueProvider(new ElementId(bipTC));
                FilterStringRuleEvaluator evaluator1 = new FilterStringContains();
                FilterRule RuleTypeComment1 = new FilterStringRule(provider1, evaluator1, "PC", false);
                //Filter Type Comment 1: Rule Type Comment 1
                ElementParameterFilter TypeCommentFilter1 = new ElementParameterFilter(RuleTypeComment1);
                //Collector
                FilteredElementCollector collector1 = new FilteredElementCollector(doc);
                //Collecting Elements of type & Filter
                collector1.WherePasses(CatFilter1).WherePasses(LevelFilter1).WherePasses(TypeCommentFilter1);
                //Collection of elements
                ICollection<Element> listofele1 = collector1.ToElements();

                #endregion

                #region 2 RC Foundation Works

                //Category Filter:StructuralFoundation
                //BuiltInCategory.OST_Stairs
                BuiltInCategory[] bicSF_FR = new BuiltInCategory[] { BuiltInCategory.OST_StructuralFraming };
                IList<ElementFilter> a0 = new List<ElementFilter>(bicSF_FR.Count());
                foreach (BuiltInCategory bic in bicSF_FR)
                {
                    a0.Add(new ElementCategoryFilter(bic));
                }
                LogicalOrFilter categoryFilter0 = new LogicalOrFilter(a0);
                LogicalAndFilter familyInstanceFilter0 = new LogicalAndFilter(categoryFilter0, new ElementClassFilter(typeof(FamilyInstance)));
                IList<ElementFilter> b0 = new List<ElementFilter>(1);
                b0.Add(familyInstanceFilter0);
                LogicalOrFilter CatFilter2 = new LogicalOrFilter(b0);

                //Rule Type Comment 1
                ParameterValueProvider provider20 = new ParameterValueProvider(new ElementId(bipTC));
                FilterStringRuleEvaluator evaluator20 = new FilterStringContains();
                FilterRule RuleTypeComment20 = new FilterStringRule(provider20, evaluator20, "RC", false);
                //Filter Type Comment 1: Rule Type Comment 1
                ElementParameterFilter TypeCommentFilter2 = new ElementParameterFilter(RuleTypeComment20);

                //Filter Combining Filters of Framing And Foundation in Or Logical Filter
                IList<ElementFilter> b1_1 = new List<ElementFilter>(3);
                b1_1.Add(CatFilter1);
                b1_1.Add(LevelFilter1);
                b1_1.Add(TypeCommentFilter2);
                LogicalAndFilter Filter1_1 = new LogicalAndFilter(b1_1);

                IList<ElementFilter> b1_2 = new List<ElementFilter>(2);
                b1_2.Add(CatFilter2);
                b1_2.Add(LevelFilter1x);
                LogicalAndFilter Filter1_2 = new LogicalAndFilter(b1_2);

                IList<ElementFilter> b1_3 = new List<ElementFilter>(2);
                b1_3.Add(Filter1_1);
                b1_3.Add(Filter1_2);
                LogicalOrFilter Filter1_3 = new LogicalOrFilter(b1_3);
                //Collector
                FilteredElementCollector collector2 = new FilteredElementCollector(doc);
                //Collecting Elements of type & Filter
                //collector2.OfClass(typeof(FamilyInstance)).OfCategory(BuiltInCategory.OST_StructuralFraming).WherePasses(LevelFilter1x);
                collector2.WherePasses(Filter1_3);
                //Collection of elements
                ICollection<Element> listofele2 = collector2.ToElements();
                #endregion

                #region 3 Column Neck Works Foundation
                //BuiltInCategory[] bics = new BuiltInCategory[] { BuiltInCategory.OST_StructuralColumns, BuiltInCategory.OST_StructuralFraming, BuiltInCategory.OST_StructuralFoundation };
                BuiltInCategory[] bicSC = new BuiltInCategory[] { BuiltInCategory.OST_StructuralColumns };
                IList<ElementFilter> a = new List<ElementFilter>(bicSC.Count());
                foreach (BuiltInCategory bic in bicSC)
                {
                    a.Add(new ElementCategoryFilter(bic));
                }
                LogicalOrFilter categoryFilter3 = new LogicalOrFilter(a);
                LogicalAndFilter familyInstanceFilter = new LogicalAndFilter(categoryFilter3, new ElementClassFilter(typeof(FamilyInstance)));
                IList<ElementFilter> b = new List<ElementFilter>(2);
                #region
                /*

                b.Add(new ElementClassFilter( typeof(Wall)));

                b.Add(new ElementClassFilter( typeof(Floor)));

                b.Add(new ElementClassFilter( typeof(ContFooting)));

                b.Add(new ElementClassFilter( typeof(PointLoad)));

                b.Add(new ElementClassFilter( typeof(LineLoad)));

                b.Add(new ElementClassFilter( typeof(AreaLoad))); */
                #endregion
                b.Add(familyInstanceFilter);
                b.Add(new ElementClassFilter(typeof(Wall)));

                LogicalOrFilter CatFilterSC = new LogicalOrFilter(b);
                FilteredElementCollector collector3 = new FilteredElementCollector(doc);
                collector3.WherePasses(CatFilterSC).WherePasses(LevelFilter1);
                ICollection<Element> listofele3 = collector3.ToElements();
                #endregion


                #region 4 Structural Column Filter Basement

                FilteredElementCollector collector4 = new FilteredElementCollector(doc);
                collector4.WherePasses(CatFilterSC).WherePasses(Level2Filter);
                ICollection<Element> listofele4 = collector4.ToElements();

                #endregion

                #region 5 Floor Filter Basement
                ElementFilter categoryFilter5 = new ElementCategoryFilter(BuiltInCategory.OST_Floors);
                LogicalAndFilter familyInstanceFilter5 = new LogicalAndFilter(categoryFilter5, new ElementClassFilter(typeof(FamilyInstance)));

                IList<ElementFilter> b5 = new List<ElementFilter>(2);
                b5.Add(new ElementClassFilter(typeof(Floor)));
                b5.Add(familyInstanceFilter5);
                LogicalOrFilter CatFilterSF = new LogicalOrFilter(b5);

                IList<ElementFilter> b5_1 = new List<ElementFilter>(2);
                b5_1.Add(CatFilterSF);
                b5_1.Add(Level2Filter);
                LogicalAndFilter Filter5_1 = new LogicalAndFilter(b5_1);

                IList<ElementFilter> b5_3 = new List<ElementFilter>(2);
                b5_3.Add(CatFilter2);
                b5_3.Add(LevelFilter2x);
                LogicalAndFilter Filter5_3 = new LogicalAndFilter(b5_3);

                ElementFilter CatFilterStair = new ElementCategoryFilter(BuiltInCategory.OST_Stairs);
                IList<ElementFilter> b5_4 = new List<ElementFilter>(2);
                b5_4.Add(CatFilterStair);
                b5_4.Add(LevelFilter2xx);
                LogicalAndFilter Filter5_4 = new LogicalAndFilter(b5_4);

                IList<ElementFilter> b5_2 = new List<ElementFilter>(3);
                b5_2.Add(Filter5_1);
                b5_2.Add(Filter5_3);
                b5_2.Add(Filter5_4);
                LogicalOrFilter Filter5_2 = new LogicalOrFilter(b5_2);

                FilteredElementCollector collector5 = new FilteredElementCollector(doc);
                collector5.WherePasses(Filter5_2);

                ICollection<Element> listofele5 = collector5.ToElements();
                #endregion


                #region 6 Structural Column Filter Ground

                FilteredElementCollector collector6 = new FilteredElementCollector(doc);
                collector6.WherePasses(CatFilterSC).WherePasses(Level3Filter);
                ICollection<Element> listofele6 = collector6.ToElements();

                #endregion

                #region 7 Floor Filter Ground
                IList<ElementFilter> b7_1 = new List<ElementFilter>(2);
                b7_1.Add(CatFilterSF);
                b7_1.Add(Level3Filter);
                LogicalAndFilter Filter7_1 = new LogicalAndFilter(b7_1);

                IList<ElementFilter> b7_3 = new List<ElementFilter>(2);
                b7_3.Add(CatFilter2);
                b7_3.Add(LevelFilter3x);
                LogicalAndFilter Filter7_3 = new LogicalAndFilter(b7_3);

                IList<ElementFilter> b7_4 = new List<ElementFilter>(2);
                b7_4.Add(CatFilterStair);
                b7_4.Add(LevelFilter3xx);
                LogicalAndFilter Filter7_4 = new LogicalAndFilter(b7_4);

                IList<ElementFilter> b7_2 = new List<ElementFilter>(3);
                b7_2.Add(Filter7_1);
                b7_2.Add(Filter7_3);
                b7_2.Add(Filter7_4);
                LogicalOrFilter Filter7_2 = new LogicalOrFilter(b7_2);

                FilteredElementCollector collector7 = new FilteredElementCollector(doc);
                collector7.WherePasses(Filter7_2);

                ICollection<Element> listofele7 = collector7.ToElements();
                #endregion


                #region 8 Structural Column Filter First
                FilteredElementCollector collector8 = new FilteredElementCollector(doc);
                collector8.WherePasses(CatFilterSC).WherePasses(Level4Filter);
                ICollection<Element> listofele8 = collector8.ToElements();
                #endregion

                #region 9 Floor Filter First
                IList<ElementFilter> b9_1 = new List<ElementFilter>(2);
                b9_1.Add(CatFilterSF);
                b9_1.Add(Level4Filter);
                LogicalAndFilter Filter9_1 = new LogicalAndFilter(b9_1);

                IList<ElementFilter> b9_3 = new List<ElementFilter>(2);
                b9_3.Add(CatFilter2);
                b9_3.Add(LevelFilter4x);
                LogicalAndFilter Filter9_3 = new LogicalAndFilter(b9_3);

                IList<ElementFilter> b9_4 = new List<ElementFilter>(2);
                b9_4.Add(CatFilterStair);
                b9_4.Add(LevelFilter4xx);
                LogicalAndFilter Filter9_4 = new LogicalAndFilter(b9_4);

                IList<ElementFilter> b9_2 = new List<ElementFilter>(3);
                b9_2.Add(Filter9_1);
                b9_2.Add(Filter9_3);
                b9_2.Add(Filter9_4);
                LogicalOrFilter Filter9_2 = new LogicalOrFilter(b9_2);

                FilteredElementCollector collector9 = new FilteredElementCollector(doc);
                collector9.WherePasses(Filter9_2);

                ICollection<Element> listofele9 = collector9.ToElements();
                #endregion

                //10

                #region 11 Structural Column Filter Second
                FilteredElementCollector collector11 = new FilteredElementCollector(doc);
                collector11.WherePasses(CatFilterSC).WherePasses(Level5Filter);
                ICollection<Element> listofele11 = collector11.ToElements();
                #endregion

                #region 12 Floor Filter Second
                IList<ElementFilter> b12_1 = new List<ElementFilter>(2);
                b12_1.Add(CatFilterSF);
                b12_1.Add(Level5Filter);
                LogicalAndFilter Filter12_1 = new LogicalAndFilter(b12_1);

                IList<ElementFilter> b12_3 = new List<ElementFilter>(2);
                b12_3.Add(CatFilter2);
                b12_3.Add(LevelFilter5x);
                LogicalAndFilter Filter12_3 = new LogicalAndFilter(b12_3);

                IList<ElementFilter> b12_4 = new List<ElementFilter>(2);
                b12_4.Add(CatFilterStair);
                b12_4.Add(LevelFilter5xx);
                LogicalAndFilter Filter12_4 = new LogicalAndFilter(b12_4);

                IList<ElementFilter> b12_2 = new List<ElementFilter>(3);
                b12_2.Add(Filter12_1);
                b12_2.Add(Filter12_3);
                b12_2.Add(Filter12_4);
                LogicalOrFilter Filter12_2 = new LogicalOrFilter(b12_2);

                FilteredElementCollector collector12 = new FilteredElementCollector(doc);
                collector12.WherePasses(Filter12_2);

                ICollection<Element> listofele12 = collector12.ToElements();
                #endregion


                #region 13 Structural Column Filter Third
                FilteredElementCollector collector13 = new FilteredElementCollector(doc);
                collector13.WherePasses(CatFilterSC).WherePasses(Level6Filter);
                ICollection<Element> listofele13 = collector13.ToElements();
                #endregion

                #region 14 Floor Filter Third
                IList<ElementFilter> b14_1 = new List<ElementFilter>(2);
                b14_1.Add(CatFilterSF);
                b14_1.Add(Level6Filter);
                LogicalAndFilter Filter14_1 = new LogicalAndFilter(b14_1);

                IList<ElementFilter> b14_3 = new List<ElementFilter>(2);
                b14_3.Add(CatFilter2);
                b14_3.Add(LevelFilter6x);
                LogicalAndFilter Filter14_3 = new LogicalAndFilter(b14_3);

                IList<ElementFilter> b14_4 = new List<ElementFilter>(2);
                b14_4.Add(CatFilterStair);
                b14_4.Add(LevelFilter6xx);
                LogicalAndFilter Filter14_4 = new LogicalAndFilter(b14_4);

                IList<ElementFilter> b14_2 = new List<ElementFilter>(3);
                b14_2.Add(Filter14_1);
                b14_2.Add(Filter14_3);
                b14_2.Add(Filter14_4);
                LogicalOrFilter Filter14_2 = new LogicalOrFilter(b14_2);

                FilteredElementCollector collector14 = new FilteredElementCollector(doc);
                collector14.WherePasses(Filter14_2);

                ICollection<Element> listofele14 = collector14.ToElements();
                #endregion


                #region 15 Structural Column Filter Roof
                FilteredElementCollector collector15 = new FilteredElementCollector(doc);
                collector15.WherePasses(CatFilterSC).WherePasses(Level7Filter);
                ICollection<Element> listofele15 = collector15.ToElements();
                #endregion

                #region 16 Floor Filter Roof
                IList<ElementFilter> b16_1 = new List<ElementFilter>(2);
                b16_1.Add(CatFilterSF);
                b16_1.Add(Level7Filter);
                LogicalAndFilter Filter16_1 = new LogicalAndFilter(b16_1);

                IList<ElementFilter> b16_3 = new List<ElementFilter>(2);
                b16_3.Add(CatFilter2);
                b16_3.Add(LevelFilter7x);
                LogicalAndFilter Filter16_3 = new LogicalAndFilter(b16_3);

                IList<ElementFilter> b16_4 = new List<ElementFilter>(2);
                b16_4.Add(CatFilterStair);
                b16_4.Add(LevelFilter7xx);
                LogicalAndFilter Filter16_4 = new LogicalAndFilter(b16_4);

                IList<ElementFilter> b16_2 = new List<ElementFilter>(3);
                b16_2.Add(Filter16_1);
                b16_2.Add(Filter16_3);
                b16_2.Add(Filter16_4);
                LogicalOrFilter Filter16_2 = new LogicalOrFilter(b16_2);

                FilteredElementCollector collector16 = new FilteredElementCollector(doc);
                collector16.WherePasses(Filter16_2);

                ICollection<Element> listofele16 = collector16.ToElements();
                #endregion

                //17


                #region Dictionary
                IDictionary<string, ICollection<Element>> MainDictionary = new Dictionary<string, ICollection<Element>>();


                string Key01 = "PC Foundation Works";
                string Key02 = "RC Foundation Works & Sump Pits";
                string Key03 = "Column Neck Works";
                string Key04 = "Basement Columns & RW Works";
                string Key05 = "Basement Slab Works";
                string Key06 = "Ground Floor Columns Works";
                string Key07 = "Ground Floor Slab Works";
                string Key08 = "First Floor Columns Works";
                string Key09 = "First Floor Slab Works";
                string Key11 = "Second Floor Columns Works";
                string Key12 = "Second Floor Slab Works";
                string Key13 = "Third Floor Columns Works";
                string Key14 = "Third Floor Slab Works";
                string Key15 = "Roof Floor Columns Works";
                string Key16 = "Roof Floor Slab Works";

                //adding a key/value using the Add() method
                MainDictionary.Add(Key01, listofele1);
                MainDictionary.Add(Key02, listofele2);
                MainDictionary.Add(Key03, listofele3);
                MainDictionary.Add(Key04, listofele4);
                MainDictionary.Add(Key05, listofele5);
                MainDictionary.Add(Key06, listofele6);
                MainDictionary.Add(Key07, listofele7);
                MainDictionary.Add(Key08, listofele8);
                MainDictionary.Add(Key09, listofele9);
                MainDictionary.Add(Key11, listofele11);
                MainDictionary.Add(Key12, listofele12);
                MainDictionary.Add(Key13, listofele13);
                MainDictionary.Add(Key14, listofele14);
                MainDictionary.Add(Key15, listofele15);
                MainDictionary.Add(Key16, listofele16);

                #endregion



                #endregion
                OverrideGraphicSettings ogs = new OverrideGraphicSettings();
                Element solidFill = new FilteredElementCollector(doc).OfClass(typeof(FillPatternElement)).Where(q => q.Name.Contains("Solid")).First();
                ogs.SetProjectionLineWeight(8);
                ogs.SetProjectionFillPatternId(solidFill.Id);
                Color TimeColor = null;
                Color CostColor = null;
                foreach (var item in MainDictionary)
                {

                    foreach (var Ex in Data)
                    {
                        if (item.Key == Ex.Id)
                        {

                            if (Ex.Time < 5)
                            {
                                TimeColor = new Color(0, 255, 0);
                            }
                            else if (Ex.Time >= 5&& Ex.Time < 23)
                            {
                                TimeColor = new Color(255, 255, 0);
                            }
                            else if (Ex.Time > 23)
                            {
                                TimeColor = new Color(255, 0, 0);
                            }

                            if (Ex.cost < 5)
                            {
                                CostColor = new Color(0, 255, 0);
                            }
                            else if (Ex.cost >= 5 && Ex.cost < 23)
                            {
                                CostColor = new Color(255, 255, 0);
                            }
                            else if (Ex.cost > 23)
                            {
                                CostColor = new Color(255, 0, 0);
                            }
                            break;
                        }
                    }
                    foreach(var el in item.Value)
                    {
                        ogs.SetProjectionLineColor(TimeColor);
                        ogs.SetProjectionFillColor(TimeColor);
                        TimeImpactView.SetElementOverrides(el.Id, ogs);
                        ogs.SetProjectionLineColor(CostColor);
                        ogs.SetProjectionFillColor(CostColor);
                        CostImpactView.SetElementOverrides(el.Id, ogs);
                    }
                }
                tx.Commit();
                uidoc.ActiveView = TimeImpactView;
                uidoc.ActiveView = CostImpactView;
            }

            return Result.Succeeded;
        }
    }
    public class MyClass
    {
        private List<string> RowContent = new List<string>();
        public List<string> GetList()
        {
            return RowContent;
        }
    }
}
