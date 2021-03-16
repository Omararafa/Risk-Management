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
                Data.Add(new Risk_Management.Data() { Checked = false, ItemName = data, PercentageCost =PercentagesCost[count],PercentageTime=PercentagesTime[count],Mitigation="test"});
                count++;
            }

            #endregion
            return Data;

        }


            
}
        }

