using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.Xml.Serialization;


namespace SuburbExplorer.Services
{
    public class APIService
    {
        //const string apiKey = "";
        const string apiKey = "Em2FsFNxt33xNsJKFBunIkn6DMSMosK41JFaFw0c";
        const string baseURL = "https://api.data.abs.gov.au/data/";
        //Get an Http Client
        HttpClient client = new HttpClient();
        ExcelService excelService;

        public APIService() { }

        public async Task<int?> GetHouseholdIncomeBySuburbName(string suburbName, string stateName)
        {
            // Get the suburb code and state code from Excel;
            var (stateCode, suburbCode) = await excelService.LookUpStateAndSuburbCodeAsync(suburbName, stateName);

            //Form a request
            string dataflow = "C21_G02_SAL/";
            string fullURLSuburbName = $"{baseURL}{dataflow}4.{suburbCode}.SAL.{stateCode}";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, fullURLSuburbName);
            request.Headers.Add("x-api-key", apiKey);
            request.Headers.Add("Accept", "application/vnd.sdmx.data+json");

            //Send the request
            HttpResponseMessage response = await client.SendAsync(request);

            //Check if the response is successful
            if(!response.IsSuccessStatusCode) { throw new HttpRequestException(); }

            // Read the response as a string
            string responseString = await response.Content.ReadAsStringAsync();

            //convert to C# object
            APIResponeMedIncome? responseMedIncome = JsonConvert.DeserializeObject<APIResponeMedIncome>(responseString);
            if(responseMedIncome?.data?.dataSets != null)
            {
                var series = responseMedIncome.data.dataSets[0].series;
                if (series != null)
                {
                    var observations = series["0:0:0:0"].observations;
                    if (observations != null && observations.ContainsKey("0"))
                    {
                        return observations["0"][0];
                    }
                }

                
            }
            return null;

        }

        //public async Task<string> GetByPostcode(string postcode)
        
            //return await GetBySuburbName(postcode);
        
    }
}
