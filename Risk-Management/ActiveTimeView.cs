using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risk_Management
{
    [Transaction(TransactionMode.Manual)]
    class ActiveTimeView : IExternalCommand
    {
        public Result Execute(
  ExternalCommandData commandData,
  ref string message,
  ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            // Modify document within a transaction

            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Transaction Name");
                View CostImpactView = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Views).Where(q => q.Name== "Time Risk Impact").FirstOrDefault() as View;
                tx.Commit();
                if (CostImpactView != null)
                {
                    uidoc.ActiveView = CostImpactView;
                }
                else
                {
                    TaskDialog.Show("Error", "Time 3D view doesn't exist", TaskDialogCommonButtons.Ok);
                }

            }

            return Result.Succeeded;
        }
    }
}
