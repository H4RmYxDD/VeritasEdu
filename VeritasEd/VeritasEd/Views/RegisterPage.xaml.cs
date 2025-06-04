using VeritasEd.Services;
namespace VeritasEd.Views;

public partial class RegisterPage : ContentPage
{
    private readonly ApiService _apiService = new();
    public RegisterPage()
    {
        InitializeComponent();
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        var user = new VeritasEd.Api.Models.User
        {
            Username = UsernameEntry.Text,
            Password = PasswordEntry.Text,
            Role = RolePicker.SelectedItem?.ToString() ?? "Student"
        };

        bool success = await _apiService.RegisterAsync(user);
        if (success)
            await DisplayAlert("Success", "Registration complete!", "OK");
        else
            await DisplayAlert("Error", "Registration failed.", "OK");
    }
}