using VeritasEd.Models;
using VeritasEd.Resources.Styles;
using VeritasEd.Services;
using VeritasEd.Views;

namespace VeritasEd.Views;

public partial class LoginPage : ContentPage
{
    private readonly ApiService _apiService = new();
    private bool _isDark = true;


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
                await Navigation.PushAsync(new TeacherTabbedPage(user));
            else
                await Navigation.PushAsync(new StudentTabbedPage(user));
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

    private void OnThemeToggleClicked(object sender, EventArgs e)
    {
        ToggleTheme();
    }
    private void ToggleTheme()
    {
        var dicts = Application.Current.Resources.MergedDictionaries;


        var existingTheme = dicts.FirstOrDefault(d => d.GetType().Name.Contains("Colors"));
        if (existingTheme != null)
            dicts.Remove(existingTheme);


        ResourceDictionary newTheme;
        if (_isDark)
            newTheme = new ColorsLight();
        else
            newTheme = new ColorsDark();

        dicts.Add(newTheme);
        _isDark = !_isDark;
    }

}