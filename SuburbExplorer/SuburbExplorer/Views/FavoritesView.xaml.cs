using SuburbExplorer.Models;
using SuburbExplorer.Services;
using System.Collections.Generic;
using SuburbExplorer.Controllers;

namespace SuburbExplorer.Views;

public partial class FavoritesView : ContentPage
{
    public readonly SQLService sqlService;
    public Suburb SelectedSuburb { get; set; }


    public FavoritesView()
	{
		InitializeComponent();
		sqlService = App.sqlService;
	}

    public async Task UpdateFavoriteSuburbAsync()
    {
        List<Suburb> favoriteSuburbList = await sqlService.GetFavoriteSuburbsAsync();
        if (favoriteSuburbList != null) 
        {
            ListViewFavoriteSuburbs.ItemsSource = favoriteSuburbList;
        }
        else
        {
            await DisplayAlert("No Favorites", "You have no favorite suburbs saved", "OK");
        }
    }
    protected override async void OnAppearing()
	{
		base.OnAppearing();
		await UpdateFavoriteSuburbAsync();		
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        if (SelectedSuburb == null)
        {
            await DisplayAlert("No suburb selected", "Please select a suburb", "OK");
        }
        else
        {

            int result = await sqlService.DeleteFavoriteSuburbAsync(SelectedSuburb.SuburbName, SelectedSuburb.StateName);
            if (result == 1)
            {
                await DisplayAlert("Success", $"{SelectedSuburb.SuburbName} has been deleted from favorite suburbs", "OK");
                await UpdateFavoriteSuburbAsync();
            }
            else
            {
                await DisplayAlert("Fail", $"Error when deleting {SelectedSuburb.SuburbName}, please try again", "OK");
            }
        }


    }
}