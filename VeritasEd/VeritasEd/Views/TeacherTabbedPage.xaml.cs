using VeritasEd.Models;

namespace VeritasEd.Views;

public partial class TeacherTabbedPage : TabbedPage
{
    public TeacherTabbedPage(User user)
    {
        InitializeComponent();
        DashboardTab.BindingContext = user;
        ProfileTab.BindingContext = user;
    }
}