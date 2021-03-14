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
    class RiskScaleCommand: IExternalCommand
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

            #region Risk window
            RiskScaleWPF x = new RiskScaleWPF();
            x.Height = 305;
            x.Width = 500;
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = x.Width;
            double windowHeight = x.Height;
            x.Left = (screenWidth / 2) - (windowWidth / 2);
            x.Top = (screenHeight / 2) - (windowHeight / 2);
            x.ShowDialog();
            #endregion

            return Result.Succeeded;
        }
    }
}
