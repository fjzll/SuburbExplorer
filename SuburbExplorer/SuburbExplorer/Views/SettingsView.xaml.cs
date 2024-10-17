namespace SuburbExplorer.Views;

public partial class SettingsView : ContentPage
{
	public SettingsView()
	{
		InitializeComponent();
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		//Preferences.Set(PreferencesTypes.UserName.ToString(), EntryName.Text);
		Preferences.Set(PreferencesTypes.ThemeLight.ToString(), SwitchTheme.IsToggled);
    }

	private void UpdateUI()
	{
		//EntryName.Text = Preferences.Get(PreferencesTypes.UserName.ToString(), "New User");
		SwitchTheme.IsToggled = Preferences.Get(PreferencesTypes.ThemeLight.ToString(), true);
		string theme = SwitchTheme.IsToggled ? "Light" : "Gray";
		LabelTheme.Text = $"Theme {theme}";
		SettingsPage.BackgroundColor = SwitchTheme.IsToggled ? Colors.White: Colors.Gray;
	}

    private void SwitchTheme_Toggled(object sender, ToggledEventArgs e)
    {
        Preferences.Set(PreferencesTypes.ThemeLight.ToString(), SwitchTheme.IsToggled);
        UpdateUI();
    }
}

enum PreferencesTypes
{
	UserName,
	ThemeLight
}