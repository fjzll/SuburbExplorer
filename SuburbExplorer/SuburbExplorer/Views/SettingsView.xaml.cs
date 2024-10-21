using OfficeOpenXml.Drawing.Controls;

namespace SuburbExplorer.Views;

public partial class SettingsView : ContentPage
{
	public SettingsView()
	{
		InitializeComponent();
		PickerFontSize.SelectedItem = "Small";
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		//Preference setting for theme
		Preferences.Set(PreferencesTypes.ThemeLight.ToString(), SwitchTheme.IsToggled);

		//Preference settings for font size
		Preferences.Set(PreferencesTypes.FontSize.ToString(), PickerFontSize.SelectedItem?.ToString());

    }

	private void UpdateUI()
	{
		//Update the theme when user changes the setting
		SwitchTheme.IsToggled = Preferences.Get(PreferencesTypes.ThemeLight.ToString(), true);
		string theme = SwitchTheme.IsToggled ? "Light" : "Gray";
		LabelTheme.Text = $"Theme {theme}";
		SettingsPage.BackgroundColor = SwitchTheme.IsToggled ? Colors.White: Colors.Gray;
		//Update the font size when user changes the setting
		string selectedFontSize = Preferences.Get(PreferencesTypes.FontSize.ToString(), "Small");
		double fontSize = 12;
		switch (selectedFontSize)
		{
			case "Small":
				fontSize = 14;
				break;
			case "Medium":
				fontSize = 16;
				break;
			case "Large":
				fontSize = 20;
				break;
		}
		//LabelFontSize.FontSize = fontSize;
		ApplyFontSizeToUI(this.Content, fontSize);

	}

    private void SwitchTheme_Toggled(object sender, ToggledEventArgs e)
    {
        Preferences.Set(PreferencesTypes.ThemeLight.ToString(), SwitchTheme.IsToggled);
        UpdateUI();
    }

	private void PickerFontSize_SelectedIndexChanged(object sender, EventArgs e)
    {
		Preferences.Set(PreferencesTypes.FontSize.ToString(), PickerFontSize.SelectedItem?.ToString());
		UpdateUI();
    }

	private void ApplyFontSizeToUI(View view, double fontSize)
	{
		if (view is Label label)
		{
			label.FontSize = fontSize;
		}
		else if (view is Button button)
		{
			button.FontSize = fontSize;
		}
		else if (view is Entry entry)
		{
			entry.FontSize = fontSize;
		}
		else if (view is Picker picker)
		{
			picker.FontSize = fontSize;
		}
		if (view is Layout layout)
		{
			foreach (var child in layout.Children)
			{
				if (child is View childView)
				{
					ApplyFontSizeToUI(childView, fontSize);
				}
			}
		}
	}
}

enum PreferencesTypes
{
	ThemeLight,
	FontSize,
}