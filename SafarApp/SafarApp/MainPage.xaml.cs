using SafarObjects.UserClasses;
using SafarSDK;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SafarApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public Users user { get; set; }

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
        public MainPage(Users usert)
        {
            InitializeComponent();
            user = usert;

            var navigationPage = new NavigationPage(new ExplorePage())
            {
                Icon = "schedule.png",
                Title = "Explore"
            };

            Children.Add(new ProfilePage(usert));
            Children.Add(navigationPage);
        }

        public MainPage(string userId)
        {
            InitializeComponent();

            LoadUser(userId);


            var navigationPage = new NavigationPage(new ExplorePage())
            {
                Icon = "schedule.png",
                Title = "Explore"
            };

            //Children.Add(new ProfilePage(user));
            Children.Add(navigationPage);

            var navToChatPage = new NavigationPage(new ChatPage("123"))
            {
                Icon = "schedule.png",
                Title = "Chat Page"
            };

            Children.Add(navToChatPage);
        }

        private async void LoadUser(string userId)
        {
            user = await UsersManager.GetUserById(userId);
            Children.Add(new ProfilePage(user));
        }
    }
}
