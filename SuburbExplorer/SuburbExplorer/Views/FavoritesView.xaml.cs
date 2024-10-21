using SuburbExplorer.Models;
using SuburbExplorer.Services;
using System.Collections.Generic;

namespace SuburbExplorer.Views;

public partial class FavoritesView : ContentPage
{
	public FavoritesView()
	{
		InitializeComponent();
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		List<FavoriteSuburb> favoriteSuburbs = await App.SqlService.GetFavoriteSuburbsAsync();
		if (favoriteSuburbs != null)
		{
			await DisplayAlert("No Favorites", "You have no favorite suburbs saved.", "Ok");
		}
		else
		{
			ListViewFavoriteSuburbs.ItemsSource = favoriteSuburbs;
		
		}
		
    }
}