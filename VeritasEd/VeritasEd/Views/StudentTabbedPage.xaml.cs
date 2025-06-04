using VeritasEd.Models;

namespace VeritasEd.Views;

public partial class StudentTabbedPage : TabbedPage
{
    public StudentTabbedPage(User user)
    {
        InitializeComponent();
        DashboardTab.BindingContext = user;
        ProfileTab.BindingContext = user;
    }
}