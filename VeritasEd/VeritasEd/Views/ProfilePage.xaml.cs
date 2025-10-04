using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Storage;
using VeritasEd.Models;

namespace VeritasEd.Views;

public partial class ProfilePage : ContentPage, INotifyPropertyChanged
{
    private string _profileImageUrl = "default_profile.png";
    private string _username = "";
    private string _role = "";

    public string ProfileImageUrl
    {
        get => _profileImageUrl;
        set { _profileImageUrl = value; OnPropertyChanged(); }
    }

    public string Username
    {
        get => _username;
        set { _username = value; OnPropertyChanged(); }
    }

    public string Role
    {
        get => _role;
        set { _role = value; OnPropertyChanged(); }
    }

    public ProfilePage()
    {
        InitializeComponent();
        // Make sure the image is in Resources/Images and set as MauiImage in csproj
        ProfileImageUrl = "default_profile.png";
        Username = Preferences.Get("Username", "User");
        Role = Preferences.Get("Role", "Student");
        BindingContext = this;
    }

    public ProfilePage(User user)
    {
        InitializeComponent();
        ProfileImageUrl = string.IsNullOrEmpty(user.ProfileImageUrl) ? "default_profile.png" : user.ProfileImageUrl;
        Username = user.Username;
        Role = user.Role;
        BindingContext = this;
    }

    private async void OnChangePictureClicked(object sender, EventArgs e)
    {
        var result = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Select a profile picture",
            FileTypes = FilePickerFileType.Images
        });
        if (result != null)
        {
            ProfileImageUrl = result.FullPath;
            Preferences.Set("ProfileImageUrl", ProfileImageUrl);
        }
    }

    private async void OnSaveChangesClicked(object sender, EventArgs e)
    {
        Preferences.Set("Username", Username);
        await DisplayAlert("Saved", "Your changes have been saved.", "OK");
    }

    private void OnLogoutClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new LoginPage());
    }

    public new event PropertyChangedEventHandler? PropertyChanged;
    protected new void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}