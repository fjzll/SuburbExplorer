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
        public List<DemographicData> demographicDataList {  get; set; }

        public SearchController(SearchView view) 
        {
            suburb = new Suburb();
            demographicData = new DemographicData();
            searchView = view;
            favoritesView = new FavoritesView();
            apiService = new APIService();
            excelService = new ExcelService();
            sqlService = App.SqlService;
            demographicDataList = new List<DemographicData>();
        }

        // Fetch data from ABS API and Calculate the suburb score
        public async Task<int> CalculateSuburbScoreAynsc(string suburbName, string stateName)
        {
            // Initialise demographic data classes
            demographicData.MedianAge = new MedianAge();
            demographicData.IncomeLevel = new IncomeLevel();
            demographicData.RentalYield = new RentalYield();
            demographicData.RentalRate = new RentalRate();

            // Assign the state values for household income, rental rate, rental yield
            demographicData.IncomeLevel.IncomeHouseholdState = 1507;
            demographicData.RentalRate.RentalRateState = 0.306m;
            demographicData.RentalYield.MedianRentState = 375;

            // look up the state code and suburb code
            var (stateCode, suburbCode) = await excelService.LookUpStateAndSuburbCodeAsync(suburbName, stateName);

            // Get median household income, rent and age and assign to demographic data 
            List<int?> medianHouseholdIncomeAndRentAndAgeList = await apiService.GetHouseholdIncomeAndRentAsync(suburbName, stateName);
            demographicData.MedianAge.Age = medianHouseholdIncomeAndRentAndAgeList[0] ?? 0;
            demographicData.IncomeLevel.IncomeHousehold = medianHouseholdIncomeAndRentAndAgeList[1] ?? 0;
            demographicData.RentalYield.MedianRentSuburb = medianHouseholdIncomeAndRentAndAgeList[2] ?? 0;

            // Get rented household numbers and total dwellings, calculate the rented rate
            List<int?> tenureRentedAndTotalList = await apiService.GetRentedTypeAndTotalAsync(suburbName, stateName);
            int? rentedHouseholdNumber = tenureRentedAndTotalList[0];
            int? totalDwellings = tenureRentedAndTotalList[1];
            demographicData.RentalRate.RentalRateSuburb = (totalDwellings != 0)? 
                Math.Round(Convert.ToDecimal(rentedHouseholdNumber) / Convert.ToDecimal(totalDwellings), 3)
                : 0m;

            // Add the demographic data to the list
            demographicDataList.Clear();
            demographicDataList.Add(demographicData);

            // Calculate the suburb Score
            suburb.SuburbScore = demographicData.CalculateOverallScore();        
            return suburb.SuburbScore;
        }
        public async Task UpdateSearchUIAsync(string suburbName, string stateName)
        {
            /* Test the output
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
            */
            // Display the Score
            int suburbScore = await CalculateSuburbScoreAynsc(suburbName, stateName);
            if (suburbScore >= 75)
            {
                searchView.EntryABSdata.BackgroundColor = Colors.Green;
            }
            else if (suburbScore < 75 && suburbScore >= 50) 
            {
                searchView.EntryABSdata.BackgroundColor = Colors.Orange;
            }
            else { searchView.EntryABSdata.BackgroundColor = Colors.Red; }
            searchView.EntryABSdata.Text = suburbScore.ToString();

            // Display the listview with demographic data. Data binding in ListView
            searchView.ListViewDemographicData.ItemsSource = null;
            searchView.ListViewDemographicData.ItemsSource = demographicDataList;
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
