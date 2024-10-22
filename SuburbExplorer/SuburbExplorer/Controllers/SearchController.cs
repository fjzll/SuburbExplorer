using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchView = SuburbExplorer.Views.SearchView;
using FavoritesView = SuburbExplorer.Views.FavoritesView;
using SuburbExplorer.Services;
using SuburbExplorer.Models;
using System.Security.Cryptography.X509Certificates;

namespace SuburbExplorer.Controllers
{
    public class SearchController
    {
        public SearchView searchView;
        public FavoritesView favoritesView;
        public APIService apiService;
        public ExcelService excelService;
        public SQLService sqlService;
        public Suburb suburb;
        public DemographicData demographicData;

        public SearchController(SearchView view) 
        {
            suburb = new Suburb();
            demographicData = new DemographicData();
            searchView = view;
            favoritesView = new FavoritesView();
            apiService = new APIService();
            excelService = new ExcelService();
            SQLService sqlService = App.SqlService;
        }

        //ToDo
        public async Task CalculateSuburbScore(string suburbName, string stateName)
        {
            // look up the state code and suburb code
            var (stateCode, suburbCode) = await excelService.LookUpStateAndSuburbCodeAsync(suburbName, stateName);
            List<int?> mediumHousehouldIncomeAndRentAndAgeList = await apiService.GetHouseholdIncomeAndRentAsync(suburbName, stateName);
            int? mediumAge = mediumHousehouldIncomeAndRentAndAgeList[0];
            int? householdIncome = mediumHousehouldIncomeAndRentAndAgeList[1];
            int? mediumRent = mediumHousehouldIncomeAndRentAndAgeList[2];
            List<int?> tenureRentedAndTotalList = await apiService.GetRentedTypeAndTotalAsync(suburbName, stateName);
            int? rentedHouseholdNumber = tenureRentedAndTotalList[0];
            int? totalDwellings = tenureRentedAndTotalList[1];
            decimal rentedRate = Convert.ToDecimal(rentedHouseholdNumber) / Convert.ToDecimal(totalDwellings);
            suburb.SuburbScore = demographicData.CalculateOverallScore(); 
            
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

        public async Task UpdateFavoriteSuburb()
        {
            favoritesView.ListViewFavoriteSuburbs.ItemsSource = null;
            favoritesView.ListViewFavoriteSuburbs.ItemsSource = await sqlService.GetFavoriteSuburbsAsync();
        }
        public async Task SaveAsFavoriteSuburb(string suburbName)
        {
            DemographicData demographicData = new DemographicData();
            decimal suburbScore = demographicData.CalculateOverallScore();
            FavoriteSuburb newFavoriteSuburb = new FavoriteSuburb()
            {
                SuburbName = suburbName,
                Score = suburbScore,
            };
            int result = await sqlService.AddFavoriteSuburbAsync(newFavoriteSuburb);
            if (result == 1)
            {
                await UpdateFavoriteSuburb();
                searchView.ButtonSaveAsFavorite.IsEnabled = false;
               
            }
        }

    }
}
