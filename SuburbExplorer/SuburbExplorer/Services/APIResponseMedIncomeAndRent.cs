using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace SuburbExplorer.Services
{
    public class APIResponseMedIncomeAndRentAndAge
    {
        [JsonProperty("data")]
        public DataSet data { get; set; }

    }

    public class DataSet
    {
        [JsonProperty("dataSets")]
        public List<DataSeries> dataSets { get; set; }
    }

    public class DataSeries
    {
        [JsonProperty("series")]
        public Dictionary<string, SeriesObservations> series {  get; set; }
    }

    public class SeriesObservations
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
          "0:0:0:0": 
           {
            "observations": {"0": [ 2890 ]}
           }
          "1:0:0:0": 
           {
            "observations": {"0": [ 420 ]}
           }
        }
      }
    ]
  }
}

 */
/* example api response in json
 {
  "meta": {
    "schema": "https://raw.githubusercontent.com/sdmx-twg/sdmx-json/master/data-message/tools/schemas/1.0/sdmx-json-data-schema.json",
    "id": "IREF004245",
    "prepared": "2024-09-16T16:18:08Z",
    "test": false,
    "contentLanguages": [
      "en"
    ],
    "sender": {
      "id": "_Stat_V8",
      "name": "unknown",
      "names": {
        "en": "unknown"
      }
    }
  },
  "data": {
    "dataSets": [
      {
        "action": "Information",
        "links": [
          {
            "urn": "urn:sdmx:org.sdmx.infomodel.datastructure.DataStructure=ABS:C21_G02_SAL(1.0.0)",
            "rel": "DataStructure"
          }
        ],
        "annotations": [
          0,
          1,
          2,
          3,
          4,
          5,
          6
        ],
        "series": {
          "0:0:0:0": {
            "attributes": [],
            "annotations": [],
            "observations": {
              "0": [
                2890
              ]
            }
          }
        }
      }
    ],
    "structure": {
      "name": "Census 2021, G02 Selected medians and averages, Suburbs and Localities (SAL)",
      "names": {
        "en": "Census 2021, G02 Selected medians and averages, Suburbs and Localities (SAL)"
      },
      "description": "Selected medians and averages data for Suburbs and Localities (SAL), 2021 Census.\nMedian age of persons excludes overseas visitors.\nMedian total personal income is applicable to persons aged 15 years and over.\nMedian total family income is applicable to families in family households. It excludes families where at least one member aged 15 years and over did not state an income and families where at \nleast one member aged 15 years and over was temporarily absent on Census Night. \nMedian total household income is applicable to occupied private dwellings. It excludes households where at least one member aged 15 years and over did not state an income and \nhouseholds where at least one member aged 15 years and over was temporarily absent on Census Night. It excludes 'Visitors only' and 'Other non-classifiable' households. \nMedian mortgage repayment is applicable to occupied private dwellings being purchased and includes dwellings being purchased under a shared equity scheme. It excludes 'Visitors only' \nand 'Other non-classifiable' households. \nMedian rent is applicable to occupied private dwellings being rented. It excludes 'Visitors only' and 'Other non-classifiable' households. \nFor 2021, median rent calculations exclude dwellings being 'Occupied rent-free' and will not be comparable to 2016 Census data.\nAverage number of persons per bedroom is applicable to occupied private dwellings. It excludes 'Visitors only' and 'Other non-classifiable' households. \nAverage household size is applicable to number of persons usually resident in occupied private dwellings. It includes partners, children, and co-tenants (in group households) who were\ntemporarily absent on Census Night. A maximum of three temporary absentees can be counted in each household. It excludes 'Visitors only' and 'Other non-classifiable' households.",
      "descriptions": {
        "en": "Selected medians and averages data for Suburbs and Localities (SAL), 2021 Census.\nMedian age of persons excludes overseas visitors.\nMedian total personal income is applicable to persons aged 15 years and over.\nMedian total family income is applicable to families in family households. It excludes families where at least one member aged 15 years and over did not state an income and families where at \nleast one member aged 15 years and over was temporarily absent on Census Night. \nMedian total household income is applicable to occupied private dwellings. It excludes households where at least one member aged 15 years and over did not state an income and \nhouseholds where at least one member aged 15 years and over was temporarily absent on Census Night. It excludes 'Visitors only' and 'Other non-classifiable' households. \nMedian mortgage repayment is applicable to occupied private dwellings being purchased and includes dwellings being purchased under a shared equity scheme. It excludes 'Visitors only' \nand 'Other non-classifiable' households. \nMedian rent is applicable to occupied private dwellings being rented. It excludes 'Visitors only' and 'Other non-classifiable' households. \nFor 2021, median rent calculations exclude dwellings being 'Occupied rent-free' and will not be comparable to 2016 Census data.\nAverage number of persons per bedroom is applicable to occupied private dwellings. It excludes 'Visitors only' and 'Other non-classifiable' households. \nAverage household size is applicable to number of persons usually resident in occupied private dwellings. It includes partners, children, and co-tenants (in group households) who were\ntemporarily absent on Census Night. A maximum of three temporary absentees can be counted in each household. It excludes 'Visitors only' and 'Other non-classifiable' households."
      },
      "dimensions": {
        "dataset": [],
        "series": [
          {
            "id": "MEDAVG",
            "name": "Median/Average",
            "names": {
              "en": "Median/Average"
            },
            "keyPosition": 0,
            "roles": [
              "MEDAVG"
            ],
            "values": [
              {
                "id": "4",
                "order": 3,
                "name": "Median total household income ($/weekly)",
                "names": {
                  "en": "Median total household income ($/weekly)"
                },
                "annotations": [
                  7
                ]
              }
            ],
            "annotations": [
              8
            ]
          },
          {
            "id": "REGION",
            "name": "Region",
            "names": {
              "en": "Region"
            },
            "keyPosition": 1,
            "roles": [
              "REGION"
            ],
            "values": [
              {
                "id": "51015",
                "order": 13443,
                "name": "Mount Hawthorn",
                "names": {
                  "en": "Mount Hawthorn"
                },
                "parent": "5",
                "annotations": [
                  9,
                  10
                ]
              }
            ]
          },
          {
            "id": "REGION_TYPE",
            "name": "Region Type",
            "names": {
              "en": "Region Type"
            },
            "keyPosition": 2,
            "roles": [
              "REGION_TYPE"
            ],
            "values": [
              {
                "id": "SAL",
                "order": 42,
                "name": "Suburbs and Localities",
                "names": {
                  "en": "Suburbs and Localities"
                },
                "annotations": [
                  11
                ]
              }
            ]
          },
          {
            "id": "STATE",
            "name": "State",
            "names": {
              "en": "State"
            },
            "keyPosition": 3,
            "roles": [
              "STATE"
            ],
            "values": [
              {
                "id": "5",
                "order": 5,
                "name": "Western Australia",
                "names": {
                  "en": "Western Australia"
                },
                "parent": "AUS",
                "annotations": [
                  12
                ]
              }
            ]
          }
        ],
        "observation": [
          {
            "id": "TIME_PERIOD",
            "name": "Time Period",
            "names": {
              "en": "Time Period"
            },
            "keyPosition": 4,
            "roles": [
              "TIME_PERIOD"
            ],
            "values": [
              {
                "start": "2021-01-01T00:00:00Z",
                "end": "2021-12-31T23:59:59Z",
                "id": "2021",
                "name": "2021",
                "names": {
                  "en": "2021"
                }
              }
            ]
          }
        ]
      },
      "attributes": {
        "dataSet": [],
        "series": [],
        "observation": []
      },
      "annotations": [
        {
          "type": "NonProductionDataflow",
          "text": "true",
          "texts": {
            "en": "true"
          }
        },
        {
          "title": "TIME_PERIOD",
          "type": "LAYOUT_COLUMN"
        },
        {
          "title": "MEDAVG",
          "type": "LAYOUT_ROW"
        },
        {
          "title": "REGION",
          "type": "LAYOUT_ROW_SECTION"
        },
        {
          "title": "REGION=10001,TIME_PERIOD_START=2021",
          "type": "DEFAULT"
        },
        {
          "type": "EXT_RESOURCE",
          "text": "Guide to Census data|https://www.abs.gov.au/census/guide-census-data",
          "texts": {
            "en": "Guide to Census data|https://www.abs.gov.au/census/guide-census-data"
          }
        },
        {
          "type": "EXT_RESOURCE",
          "text": "Download all data - Census DataPacks|https://www.abs.gov.au/census/find-census-data/datapacks",
          "texts": {
            "en": "Download all data - Census DataPacks|https://www.abs.gov.au/census/find-census-data/datapacks"
          }
        },
        {
          "type": "ORDER",
          "text": "4",
          "texts": {
            "en": "4"
          }
        },
        {
          "type": "CONSIDERATION",
          "text": "Combination variable created for community profiles.",
          "texts": {
            "en": "Combination variable created for community profiles."
          }
        },
        {
          "type": "ORDER",
          "text": "67220",
          "texts": {
            "en": "67220"
          }
        },
        {
          "type": "ASGS_LOCI_URI",
          "text": "http://linked.data.gov.au/dataset/asgsed3/SAL/51015",
          "texts": {
            "en": "http://linked.data.gov.au/dataset/asgsed3/SAL/51015"
          }
        },
        {
          "type": "ORDER",
          "text": "300",
          "texts": {
            "en": "300"
          }
        },
        {
          "type": "ORDER",
          "text": "60",
          "texts": {
            "en": "60"
          }
        }
      ]
    }
  }
}
*/

