using VeritasEd.Services;
using VeritasEd.Views;
using VeritasEd.Models;

namespace VeritasEd.Views;

public partial class LoginPage : ContentPage
{
    private readonly ApiService _apiService = new();

    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var apiUser = await _apiService.LoginAsync(UsernameEntry.Text, PasswordEntry.Text);
        if (apiUser != null)
        {
            var user = new VeritasEd.Models.User
            {
                Id = apiUser.Id,
                Username = apiUser.Username,
                Password = apiUser.Password,
                Role = apiUser.Role
            };

            if (user.Role == "Teacher")
                await Navigation.PushAsync(new TeacherDashboardPage(user));
            else
                await Navigation.PushAsync(new StudentDashboardPage(user));
        }
        else
        {
            await DisplayAlert("Error", "Invalid credentials.", "OK");
        }
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }
}