using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Design_Plug_in_Updates
{
    class ClustersData
    {

        public string Serial { get; set; }

        public string Definition { get; set; }

        public string SerialInApp { get; set; }

        public static ObservableCollection<ClustersData> GetData(List<string> SerialInAPP, List<string> Definition)
        {

            var Data = new ObservableCollection<ClustersData>();

            #region making rows with the data
            int count = 0;
            foreach (string Serial in SerialInAPP)
            {
                var SerialInApp = "0";
                #region
                

                if (Serial == "1")
                {
                    SerialInApp = "1";
                }
                else if (Serial == "2")
                {
                    SerialInApp = "4";
                }
                else if (Serial == "3")
                {
                    SerialInApp = "5";
                }
                else if (Serial == "4")
                {
                    SerialInApp = "6";
                }
                else if (Serial == "5")
                {
                    SerialInApp = "7";
                }
                else if (Serial == "6")
                {
                    SerialInApp = "9";
                }
                else if (Serial == "7")
                {
                    SerialInApp = "10";
                }
                else if (Serial == "8")
                {
                    SerialInApp = "11";
                }
                else if (Serial == "9")
                {
                    SerialInApp = "12";
                }
                else if (Serial == "10")
                {
                    SerialInApp = "14";
                }
                else if (Serial == "11")
                {
                    SerialInApp = "15";
                }
                else if (Serial == "12")
                {
                    SerialInApp = "17";
                }
                else if (Serial == "13")
                {
                    SerialInApp = "18";
                }
                else if (Serial == "14")
                {
                    SerialInApp = "19";
                }
                else if (Serial == "15")
                {
                    SerialInApp = "20";
                }
                else if (Serial == "16")
                {
                    SerialInApp = "20";
                }
                else if (Serial == "17")
                {
                    SerialInApp = "20";
                }
                else if (Serial == "18")
                {
                    SerialInApp = "21";
                }
                else if (Serial == "19")
                {
                    SerialInApp = "23";
                }
                else if (Serial == "20")
                {
                    SerialInApp = "24";
                }
                else if (Serial == "21")
                {
                    SerialInApp = "27";
                }
                else if (Serial == "22")
                {
                    SerialInApp = "40";
                }
                #endregion

                Data.Add(new ClustersData() { Serial=Serial,SerialInApp=SerialInApp,Definition=Definition[count] });
                count = count + 1;
            }

            #endregion
            return Data;
        }

    }
}
