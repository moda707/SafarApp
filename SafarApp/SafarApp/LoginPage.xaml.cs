using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PusherServer;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SafarSDK;

namespace SafarApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
		}

	    private async void Login_Clicked(object sender, EventArgs e)
	    {
	        var progressbar = new ProgressBar()
	        {
                Progress = 0.2
	        };
            
            var u = await UsersManager.GetUserByEmailPass(txtEmail.Text, txtPassword.Text);
	        await progressbar.ProgressTo(.8, 250, Easing.Linear);

            if (u != null)
            {
                var mainPage = new MainPage(u);
                await Navigation.PushAsync(mainPage);
            }

	    }
	}
}