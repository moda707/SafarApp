using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafarObjects.UserClasses;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SafarSDK;
using Xamarin.Essentials;

namespace SafarApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            var u = await UsersManager.GetUserByEmailPass(txtEmail.Text, txtPassword.Text);

            if (u == null) return;
            var uu = new Users();
            
            Preferences.Set("IsLoggedIn", bool.TrueString);
            Preferences.Set("UserId", u.UserId);
            Preferences.Set("TokenId", u.token);

            var mainPage = new MainPage(u);
            await Navigation.PushAsync(mainPage);
        }

        private async void Logout_Clicked(object sender, EventArgs e)
        {
            Preferences.Set("IsLoggedIn", bool.FalseString);
            Preferences.Remove("UserId");

            var wellcomePage = new WelcomePage();
            await Navigation.PushAsync(wellcomePage);
        }
    }
}