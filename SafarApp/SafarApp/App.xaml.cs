using System;
using SafarSDK;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SafarApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            
            var isLoggedIn = Preferences.Get("IsLoggedIn", bool.FalseString);

            if (isLoggedIn == bool.TrueString)
            {
                var userId = Preferences.Get("UserId", "");

                MainPage = new MainPage(userId);
            }
            else
            {
                MainPage = new WelcomePage();
            }

            
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
