using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SuburbExplorer.Services
{
    public class APIResponseRentedTypeAndTotal
    {
        [JsonProperty("data")]
        public RentedTypeAndTotalDataSet data { get; set; }
    }


    public class RentedTypeAndTotalDataSet
    {
        [JsonProperty("dataSets")]
        public List<RentedTypeAndTotalDataSeries> dataSets { get; set; }
    }

    public class RentedTypeAndTotalDataSeries
    {
        [JsonProperty("series")]
        public Dictionary<string, RentedTypeAndTotalSeriesObservations> series { get; set; }
    }

    public class RentedTypeAndTotalSeriesObservations
    {
        [JsonProperty("observations")]
        public Dictionary<string, List<int>> observations { get; set; }
    }

}

/* example api response in json in short
{
  "data": 
  {
    "dataSets": 
    [
      {
        "series": 
        {
          "0:0:0:0:0": 
           {
            "observations": {"0": [ 774 ]}
           }
          "1:0:0:0:0": 
           {
            "observations": {"0": [ 2966 ]}
           }
        }
      }
    ]
  }
}

 */