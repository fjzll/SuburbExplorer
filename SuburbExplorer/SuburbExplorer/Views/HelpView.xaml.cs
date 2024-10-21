namespace SuburbExplorer.Views;

public partial class HelpView : ContentPage
{
	public HelpView()
	{
		InitializeComponent();
	}

    private async void ButtonContactForm_Clicked(object sender, EventArgs e)
    {
		string userName = EntryUserName.Text;
		string userEmail = EntryUserEmail.Text;
		string userMessage = EditorUserMessage.Text;
		string destinationEmail = "20113384@tafe.wa.edu.au";
		string emailSubject = $"{userName} message from {userEmail}";

		if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(userEmail) || string.IsNullOrEmpty(userMessage))
		{
			await DisplayAlert("Alert", "Please enter your name, email and Message", "Confirm");
			return;
		}
		
		string mailToSend = $"mailto:{destinationEmail}?subject={emailSubject}&body={userMessage}";

		await Launcher.OpenAsync(mailToSend);

    }
}