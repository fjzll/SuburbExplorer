using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Xml.Linq;


namespace SuburbExplorer.Services
{
    public class APIService
    {
        //const string apiKey = "";
        const string apiKey = "Em2FsFNxt33xNsJKFBunIkn6DMSMosK41JFaFw0c";
        const string baseURL = "https://api.data.abs.gov.au/data/";
        //Get an Http Client
        HttpClient client = new HttpClient();
        ExcelService excelService = new ExcelService();

        public APIService() { }

        public async Task<List<int?>> GetHouseholdIncomeAndRentAsync(string suburbName, string stateName)
        {
            // Get the suburb code and state code from Excel;
            var (stateCode, suburbCode) = await excelService.LookUpStateAndSuburbCodeAsync(suburbName, stateName);

            //Form a request
            string dataflow = "C21_G02_SAL/";
            string fullURLSuburbName = $"{baseURL}{dataflow}1+4+6.{suburbCode}.SAL.{stateCode}";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, fullURLSuburbName);
            request.Headers.Add("x-api-key", apiKey);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.sdmx.data+json"));
            request.Headers.Add("User-Agent", "Mozilla/5.0");

            //Send the request
            HttpResponseMessage response = await client.SendAsync(request);

            //var error = await response.Content.ReadAsStringAsync();

            //Check if the response is successful
            if(!response.IsSuccessStatusCode) { throw new HttpRequestException(); }

            // Read the response as a string
            string responseString = await response.Content.ReadAsStringAsync();

            //convert to C# object
            APIResponseMedIncomeAndRentAndAge? responseMedIncomeAndRentAndAge = JsonConvert.DeserializeObject<APIResponseMedIncomeAndRentAndAge>(responseString);
            var observations_list = new List<int?>();

            //Extract the obs value
            if(responseMedIncomeAndRentAndAge?.data?.dataSets != null)
            {
                var series = responseMedIncomeAndRentAndAge.data.dataSets[0].series;
                if (series != null)
                {
                    var seriesKeys = new[] { "0:0:0:0", "1:0:0:0", "2:0:0:0" };
                    foreach (var key in seriesKeys)
                    {
                        var observations = series[key].observations;
                        if (observations != null && observations.ContainsKey("0"))
                        {
                            observations_list.Add(observations["0"][0]);
                        }
                        else
                        {
                            observations_list.Add(null);
                        }
                    }


                }              
            }
            return observations_list;

        }

        public async Task<List<int?>> GetRentedTypeAndTotalAsync(string suburbName, string stateName)
        {
            // Get the suburb code and state code from Excel;
            var (stateCode, suburbCode) = await excelService.LookUpStateAndSuburbCodeAsync(suburbName, stateName);

            //Form a request
            string dataflow = "C21_G37_SAL/";
            string fullURLSuburbName = $"{baseURL}{dataflow}_T+R_T._T.{suburbCode}.SAL.{stateCode}";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, fullURLSuburbName);
            request.Headers.Add("x-api-key", apiKey);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.sdmx.data+json"));
            request.Headers.Add("User-Agent", "Mozilla/5.0");

            //Send the request
            HttpResponseMessage response = await client.SendAsync(request);

            //Check if the response is successful
            if (!response.IsSuccessStatusCode) { throw new HttpRequestException(); }

            // Read the response as a string
            string responseString = await response.Content.ReadAsStringAsync();

            //convert to C# object
            APIResponseRentedTypeAndTotal? responseRentedTypeAndTotal = JsonConvert.DeserializeObject<APIResponseRentedTypeAndTotal>(responseString);
            var observations_tenure_list = new List<int?>();

            //Extract the obs value
            if (responseRentedTypeAndTotal?.data?.dataSets != null)
            {
                var series = responseRentedTypeAndTotal.data.dataSets[0].series;
                if (series != null)
                {
                    var seriesKeys = new[] { "0:0:0:0:0", "1:0:0:0:0" };
                    foreach (var key in seriesKeys)
                    {
                        var observations = series[key].observations;
                        if (observations != null && observations.ContainsKey("0"))
                        {
                            observations_tenure_list.Add(observations["0"][0]);
                        }
                        else
                        {
                            observations_tenure_list.Add(null);
                        }
                    }


                }
            }
            return observations_tenure_list;

        }



    }
}
