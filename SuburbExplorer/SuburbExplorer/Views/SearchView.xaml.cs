using SuburbExplorer.Controllers;

namespace SuburbExplorer.Views;

public partial class SearchView : ContentPage
{
	public SearchController searchController;

	public SearchView()
	{
		InitializeComponent();
		PickerState.ItemsSource = new List<string> { "New South Wales", "Victoria",
													"Queensland", "South Australia",
													 "Western Australia", "Tasmania",
													 "Northern Territory", "Australian Capital Territory",
													 "Other Territories"};
		searchController = new SearchController(this);
	}


    private async void ButtonSearch_Clicked(object sender, EventArgs e)
    {
        await searchController.UpdateSearchUIAsync(EntrySuburb.Text.Trim(), (string)PickerState.SelectedItem);
    }
}