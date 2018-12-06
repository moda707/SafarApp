using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafarCore.UserClasses;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace SafarApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            var navigationPage = new NavigationPage(new ExplorePage())
            {
                Icon = "schedule.png",
                Title = "Explore"
            };

            Children.Add(new ProfilePage());
            Children.Add(navigationPage);
        }
        public MainPage(Users user)
        {
            InitializeComponent();

            var navigationPage = new NavigationPage(new ExplorePage())
            {
                Icon = "schedule.png",
                Title = "Explore"
            };

            Children.Add(new ProfilePage());
            Children.Add(navigationPage);
        }

    }
}
