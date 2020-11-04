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

        public string SerialInApp { get; set; }

        public string Definition { get; set; }

        public static ObservableCollection<ClustersData> GetData(List<string> SerialInAPP, List<string> Definition)
        {

            var Data = new ObservableCollection<ClustersData>();

            #region making rows with the data
            int count = 0;
            foreach (string Serial in SerialInAPP)
            {
                Data.Add(new ClustersData() { SerialInApp=Serial,Definition=Definition[count] });
                count = count + 1;
            }

            #endregion
            return Data;
        }

    }
}
