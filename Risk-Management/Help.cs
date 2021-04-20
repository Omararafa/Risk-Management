using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risk_Management
{
    [Transaction(TransactionMode.Manual)]
    class Help : IExternalCommand
    {
public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            string path = @"%AppData%\Autodesk\REVIT\Addins\2019\Software Tutorial - Mitigation BIM Risks.pdf";
            path = Environment.ExpandEnvironmentVariables(path);
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(path)
            {
                UseShellExecute = true
            };
            p.Start();
            return Result.Succeeded;
        }
    }
}
