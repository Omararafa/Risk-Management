using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Risk_Management
{
    class CsvData
    {
        public class Foo
        {
            public string Id { get; set; }
            public int Time { get; set; }
            public int cost { get; set; }
        }
        public Dictionary<string, string> Values { get; private set; }

        public Foo[] CsvDataa(string path)
        {
            Foo[] test = null;
            using (var csvReader = new StreamReader(path))
            using (var csv = new CsvReader(csvReader, CultureInfo.InvariantCulture))
            {
                csvReader.Read();
                csv.Configuration.HasHeaderRecord = false;
                test= csv.GetRecords<Foo>().ToArray();

                //_keys = _facadesDb[0].Keys.ToList();
            }
            return test;
        }

       /* public void SetStyle(string style)
        {
            int styleIndex = _keys.IndexOf(style);

            if (styleIndex == -1)
            {
                throw new ArgumentException($"Style '{style}' cannot be find.");
            }

            IEnumerable<List<string>> table = _facadesDb.Select(row => row.Values.Select(value => value.ToString()).ToList());

            Values = table.ToDictionary(row => row[0], row => row[styleIndex]);
        }*/
    }
}
