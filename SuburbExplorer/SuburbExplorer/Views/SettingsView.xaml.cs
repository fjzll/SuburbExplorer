using OfficeOpenXml.Drawing.Controls;
using SuburbExplorer.Resources.Styles;

namespace SuburbExplorer.Views;

public partial class SettingsView : ContentPage
{
	public SettingsView()
	{
		InitializeComponent();
		PickerFontSize.SelectedItem = "Medium";
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		//Save preference setting for theme
		Preferences.Set(PreferencesTypes.ThemeLight.ToString(), !SwitchTheme.IsToggled);

		//Preference settings for font size
		Preferences.Set(PreferencesTypes.FontSize.ToString(), PickerFontSize.SelectedItem?.ToString());

    }

	private void UpdateUI()
	{
		//Update the theme when user changes the setting
		SwitchTheme.IsToggled = !Preferences.Get(PreferencesTypes.ThemeLight.ToString(), true);
		//Update the theme label
		string theme = SwitchTheme.IsToggled ? "Dark" : "Light";
		LabelTheme.Text = $"Theme {theme}";
		//Update the font size when user changes the setting
		string selectedFontSize = Preferences.Get(PreferencesTypes.FontSize.ToString(), "Medium");
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
        // Preferences.Set(PreferencesTypes.ThemeLight.ToString(), SwitchTheme.IsToggled);
        ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
		mergedDictionaries.Clear();
        if (mergedDictionaries != null)
        {
            if (e.Value)
			// If the swithc is toggled, apply DarkTheme
            {
                mergedDictionaries.Add(new DarkTheme());
				Preferences.Set(PreferencesTypes.ThemeLight.ToString(), false);
            }
            else 
			// If the switch is toggled off, apply LightTheme
            {
                mergedDictionaries.Add(new LightTheme());
				Preferences.Set(PreferencesTypes.ThemeLight.ToString(), true);
            }
        }
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