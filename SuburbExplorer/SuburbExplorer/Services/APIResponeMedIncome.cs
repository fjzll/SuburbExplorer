using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SuburbExplorer.Services
{
    public class APIResponeMedIncome
    {
        public DataSet data { get; set; }

    }

    public class DataSet
    {
        public List<DataSeries> dataSets { get; set; }
    }

    public class DataSeries
    {
        public Dictionary<string, SeriesObservations> series {  get; set; }
    }

    public class SeriesObservations
    {
        public Dictionary<string, List<int>> observations { get; set; }
    }

}
