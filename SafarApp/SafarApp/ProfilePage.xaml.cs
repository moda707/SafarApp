using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafarObjects.UserClasses;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SafarApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : TabbedPage
    {
        public ProfilePage ()
        {
            InitializeComponent();
        }

        public ProfilePage(Users user)
        {
            InitializeComponent();
            DisplayAlert(user.Email, user.DisplayName, "yes", "no");

        }
    }
}