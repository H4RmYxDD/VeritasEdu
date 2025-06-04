using VeritasEd.Resources.Styles;
using VeritasEd.Services;
namespace VeritasEd.Views;

public partial class RegisterPage : ContentPage
{
    private readonly ApiService _apiService = new();
    private bool _isDark = true;

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
    } }