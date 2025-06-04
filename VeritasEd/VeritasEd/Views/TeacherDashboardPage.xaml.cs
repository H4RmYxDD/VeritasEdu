using System.Linq;
using VeritasEd.Api.Models;
using VeritasEd.Resources.Styles;
using VeritasEd.Services;

namespace VeritasEd.Views;

public partial class TeacherDashboardPage : ContentPage
{
    private readonly Models.User _user;
    private readonly ApiService _apiService = new();
    private List<Models.User> _students = new();

    public Models.User SelectedStudent { get; set; }
    private bool _isDark = true;
    public TeacherDashboardPage()
    {
        InitializeComponent();
        BindingContext = this;
        _user = new Models.User { Username = "Teacher" };
        InitAsync();
        WelcomeLabel.Text = $"Welcome, {_user.Username} (Teacher)";
    }

    private void OnThemeToggleClicked(object sender, EventArgs e)
    {
        ToggleTheme();
    }
    private async void OnLogoutClicked(object sender, EventArgs e)
    {

        Application.Current.MainPage = new NavigationPage(new LoginPage());
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


    public TeacherDashboardPage(Models.User user)
    {
        InitializeComponent();
        BindingContext = this;
        _user = user;
        InitAsync();
        WelcomeLabel.Text = $"Welcome, {_user.Username} (Teacher)";
    }

    private async void InitAsync()
    {
        await LoadStudents();
        await LoadGrades();
    }

    private async Task LoadStudents()
    {
        var apiStudents = await _apiService.GetStudentsAsync();
        _students = apiStudents.Select(apiStudent => new Models.User
        {
            Id = apiStudent.Id,
            Username = apiStudent.Username,
            Password = apiStudent.Password,
            Role = apiStudent.Role
        }).ToList();
        StudentPicker.ItemsSource = _students;
    }

    private async Task LoadGrades()
    {
        var allGrades = await _apiService.GetAllGradesAsync();
        var gradeDisplays = allGrades.Select(g =>
        {
            var student = _students.FirstOrDefault(s => s.Id == g.UserId);
            return new GradeDisplay
            {
                Id = g.Id,
                UserId = g.UserId,
                Username = student?.Username ?? g.UserId.ToString(),
                Subject = g.Subject,
                Value = g.Value
            };
        }).ToList();
        GradesCollectionView.ItemsSource = gradeDisplays;
    }

    public class GradeDisplay
    {
        public int Id { get; set; }
        public int UserId { get; set; } 
        public string Username { get; set; } = "";
        public string Subject { get; set; } = "";
        public string Value { get; set; } = "";
    }



    private async void OnAddGradeClicked(object sender, EventArgs e)
    {
        if (StudentPicker.SelectedItem is Models.User selectedStudent &&
            !string.IsNullOrWhiteSpace(SubjectEntry.Text) &&
            !string.IsNullOrWhiteSpace(GradeValueEntry.Text))
        {
            bool success;
            if (_editingGrade == null)
            {
                var grade = new Grade
                {
                    UserId = selectedStudent.Id,
                    Subject = SubjectEntry.Text,
                    Value = GradeValueEntry.Text
                };

                success = await _apiService.AddGradeAsync(grade);
                if (success)
                    await DisplayAlert("Success", "Grade added.", "OK");
                else
                    await DisplayAlert("Error", "Failed to add grade.", "OK");
            }
            else
            {
                _editingGrade.UserId = selectedStudent.Id;
                _editingGrade.Subject = SubjectEntry.Text;
                _editingGrade.Value = GradeValueEntry.Text;

                success = await _apiService.UpdateGradeAsync(_editingGrade);
                if (success)
                    await DisplayAlert("Success", "Grade updated.", "OK");
                else
                    await DisplayAlert("Error", "Failed to update grade.", "OK");

                _editingGrade = null;
            }

            StudentPicker.SelectedItem = null;
            SubjectEntry.Text = "";
            GradeValueEntry.Text = "";
            LoadGrades();
        }
        else
        {
            await DisplayAlert("Error", "Please select a student and fill all fields.", "OK");
        }
    }
    private async void OnDeleteGradeClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is int gradeId)
        {
            bool confirm = await DisplayAlert("Confirm", "Delete this grade?", "Yes", "No");
            if (confirm)
            {
                bool success = await _apiService.DeleteGradeAsync(gradeId);
                if (success)
                {
                    await DisplayAlert("Deleted", "Grade deleted.", "OK");
                    LoadGrades();
                }
                else
                {
                    await DisplayAlert("Error", "Failed to delete grade.", "OK");
                }
            }
        }
    }
    private Grade? _editingGrade = null;

    private void OnEditGradeClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is GradeDisplay gradeDisplay)
        {
            StudentPicker.SelectedItem = _students.FirstOrDefault(s => s.Id == gradeDisplay.Id);
            SubjectEntry.Text = gradeDisplay.Subject;
            GradeValueEntry.Text = gradeDisplay.Value;

            _editingGrade = new Grade
            {
                Id = gradeDisplay.Id,
                UserId = _students.FirstOrDefault(s => s.Username == gradeDisplay.Username)?.Id ?? 0,
                Subject = gradeDisplay.Subject,
                Value = gradeDisplay.Value
            };
        }
    }
}