using VeritasEd.Models;
using VeritasEd.Views;

namespace VeritasEd.Views;

public partial class ProfilePage : ContentPage
{
    private User? _user;

    public ProfilePage()
    {
        InitializeComponent();
    }

    public ProfilePage(User user)
    {
        InitializeComponent();
        _user = user;
        UsernameLabel.Text = $"Username: {_user.Username}";
        RoleLabel.Text = $"Role: {_user.Role}";
    }

    private void OnLogoutClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new LoginPage());
    }
}