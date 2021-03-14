#region Namespaces
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
#endregion

namespace Risk_Management
{
    class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication a)
        {
            RibbonPanel panel = ribbonpanel(a);

            string thisassemblypath = Assembly.GetExecutingAssembly().Location;


            Image img = Properties.Resources.Button;
            ImageSource imgsc = GetImageSource(img);
            Image imgCost = Properties.Resources.CostView;
            ImageSource imgscCost = GetImageSource(imgCost);
            Image imgTime = Properties.Resources.TimeImpact;
            ImageSource imgscTime = GetImageSource(imgTime);
            Image RiskImg = Properties.Resources.RiskScale;
            ImageSource imgscRisk = GetImageSource(RiskImg);

            PushButton button = panel.AddItem(new PushButtonData("BIM Risks Eliminator", "BIM Risks Eliminator", thisassemblypath, "Risk_Management.Command")) as PushButton;
            PushButton buttonZero = panel.AddItem(new PushButtonData("Edit Risk Scale", "Edit Risk Scale", thisassemblypath, "Risk_Management.RiskScaleCommand")) as PushButton;
            PushButton buttonOne = panel.AddItem(new PushButtonData("Cost View", "Show Cost 3D View", thisassemblypath, "Risk_Management.ActiveCostView")) as PushButton;
            PushButton buttonTwo = panel.AddItem(new PushButtonData("Time View", "Show 3D Time View", thisassemblypath, "Risk_Management.ActiveTimeView")) as PushButton;
            
            // button.ToolTip = "Export sheets as PDF and CAD with the ability of sorting them in folders";

            // button.LongDescription = "User can filter the sheets and select the sheet to be exported and after that it can be exported as PDF or CAD or both then sheets can be sorted in specific folders using the grouping option";

            button.Image = imgsc;

            button.LargeImage = imgsc;

            button.Enabled = true;

            buttonOne.Image = imgscCost;
            buttonOne.LargeImage = imgscCost;
            buttonOne.Enabled = true;

            buttonTwo.Image = imgscTime;
            buttonTwo.LargeImage = imgscTime;
            buttonTwo.Enabled = true;

            buttonZero.Image = imgscRisk;
            buttonZero.LargeImage = imgscRisk;
            buttonZero.Enabled = true;

            //  ContextualHelp ContextHelp = new ContextualHelp(ContextualHelpType.Url, "http://arcades-arc.com/");

            //button.SetContextualHelp(ContextHelp);

            a.ApplicationClosing += a_ApplicationClosing;
            a.Idling += a_Idling;
            return Result.Succeeded;
        }

        private BitmapSource GetImageSource(Image img)
        {
            BitmapImage bmp = new BitmapImage();

            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, ImageFormat.Png);
                ms.Position = 0;
                bmp.BeginInit();
                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.UriSource = null;
                bmp.StreamSource = ms;
                bmp.EndInit();
            }
            return bmp;
        }
        public RibbonPanel ribbonpanel(UIControlledApplication a)
        {
            string tab = "Risk Management";
            RibbonPanel ribbonpanel = null;
            //create tab
            try
            {
                a.CreateRibbonTab(tab);

            }
            catch { }
            //create panel  
            try
            {
                //a.createRibbonPanel(Tab Name, Panel Name)
                RibbonPanel panel = a.CreateRibbonPanel(tab, "Risk Management");
            }
            catch { }
            //check if panel exist
            List<RibbonPanel> panels = a.GetRibbonPanels(tab);
            foreach (RibbonPanel p in panels)
            {
                //check if the pannel exist if it exist return the pannel if not return the new pannel
                if (p.Name == "Risk Management")
                {
                    ribbonpanel = p;
                    break;
                }
            }
            return ribbonpanel;

        }
        void a_ApplicationClosing(object sender, Autodesk.Revit.UI.Events.ApplicationClosingEventArgs e)
        {
            throw new NotImplementedException();
        }
        void a_Idling(object sender, Autodesk.Revit.UI.Events.IdlingEventArgs e)
        {

        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
    }
}
