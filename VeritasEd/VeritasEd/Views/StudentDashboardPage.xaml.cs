using VeritasEd.Models;
using VeritasEd.Resources.Styles;
using VeritasEd.Services;

namespace VeritasEd.Views;

public partial class StudentDashboardPage : ContentPage
{
    private readonly User _user;
    private readonly ApiService _apiService = new();
    private bool _isDark = true;

    public StudentDashboardPage(User user)
    {
        InitializeComponent();
        _user = user;
        WelcomeLabel.Text = $"Welcome, {user.Username} (Student)";
        LoadGrades();
    }

    private async void LoadGrades()
    {
        var myGrades = await _apiService.GetGradesByUserIdAsync(_user.Id);
        GradesCollectionView.ItemsSource = myGrades;
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