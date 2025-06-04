using VeritasEd.Models;
using VeritasEd.Services;

namespace VeritasEd.Views;

public partial class StudentDashboardPage : ContentPage
{
    private readonly User _user;
    private readonly ApiService _apiService = new();

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
}