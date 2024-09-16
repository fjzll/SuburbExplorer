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
            var (stateCode, suburbCode) = await excelService.LookUpStateAndSuburbCodeAsync(suburbName, stateName);
            // string response = await service.Get();
            searchView.EntryABSdata.Text = $"suburb code{suburbCode}; state code {stateCode}";
        }

    }
}
