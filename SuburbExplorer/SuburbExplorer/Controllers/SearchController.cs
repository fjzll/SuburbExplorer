using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchView = SuburbExplorer.Views.SearchView;
using SuburbExplorer.Services;
using SuburbExplorer.Models;

namespace SuburbExplorer.Controllers
{
    public class SearchController
    {
        public SearchView searchView;
        public APIService apiService;
        public ExcelService excelService;

        public SearchController(SearchView view) 
        {
            searchView = view;
            apiService = new APIService();
            excelService = new ExcelService();

        }

        public async Task UpdateSearchUIAsync(string suburbName, string stateName)
        {
            // look up the state code and suburb code
            var (stateCode, suburbCode) = await excelService.LookUpStateAndSuburbCodeAsync(suburbName, stateName);
            List<int?> mediumHousehouldIncomeAndRentAndAgeList = await apiService.GetHouseholdIncomeAndRentAsync(suburbName, stateName);
            List<int?> tenureRentedAndTotalList = await apiService.GetRentedTypeAndTotalAsync(suburbName, stateName);
            // string response = await service.Get();
            searchView.EntryABSdata.Text = 
                $"Suburb code: {suburbCode} || " + 
                $"State code: {stateCode} || " +
                $"Medium age: {mediumHousehouldIncomeAndRentAndAgeList[0]} || " + 
                $"Medium income: {mediumHousehouldIncomeAndRentAndAgeList[1]} || " +
                $"Medium rent: {mediumHousehouldIncomeAndRentAndAgeList[2]} ||"+
                $"Rented household: {tenureRentedAndTotalList[0]} || " +
                $"Total dwellings: {tenureRentedAndTotalList[1]} || ";
        }

    }
}
