using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risk_Management
{
    class Data
    {
        public bool Checked { get; set; }

        public string ItemName { get; set; }

        public string PercentageCost { get; set; }
        public string PercentageTime { get; set; }

        public string Mitigation { get; set; }


        public static ObservableCollection<Data> GetData(IDictionary<string, ICollection<Element>> ElementsGroups, List<string> PercentagesCost, List<string> PercentagesTime)
        {

            var Data = new ObservableCollection<Data>();

            #region making rows with the data
            int count = 0;
            foreach (var data in ElementsGroups.Keys)
            {
                string mit = "";
                string cost = PercentagesCost[count];
                string time = PercentagesTime[count];
                int costnum = Convert.ToInt32(cost.Remove(cost.IndexOf("%")));
                int timenum = Convert.ToInt32(time.Remove(time.IndexOf("%")));
                if (costnum>timenum)
                {
                    if (costnum <= Convert.ToDouble(Properties.Settings.Default.Min)*100)
                    {
                        mit = "Low";
                    }
                    else if (costnum > Convert.ToDouble(Properties.Settings.Default.Min)*100&& costnum <= Convert.ToDouble(Properties.Settings.Default.Max)*100)
                    {
                        mit = "Meduim";
                    }
                    else
                    {
                        mit = "High";
                    }
                }
                else
                {
                    if (timenum <= Convert.ToDouble(Properties.Settings.Default.Min)*100)
                    {
                        mit = "Low";
                    }
                    else if (timenum > Convert.ToDouble(Properties.Settings.Default.Min)*100 && costnum <= Convert.ToDouble(Properties.Settings.Default.Max)*100)
                    {
                        mit = "Meduim";
                    }
                    else
                    {
                        mit = "High";
                    }
                }
                Data.Add(new Risk_Management.Data() { Checked = false, ItemName = data, PercentageCost =PercentagesCost[count],PercentageTime=PercentagesTime[count],Mitigation=mit});
                count++;
            }

            #endregion
            return Data;

        }


            
}
        }

