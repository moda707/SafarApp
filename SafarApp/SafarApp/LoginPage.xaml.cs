using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PusherServer;
using SafarApp.UserClasses;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SafarApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
		}

	    private void Login_Clicked(object sender, EventArgs e)
	    {
	        var usr = Users.LoginUser(txtEmail.Text, txtPassword.Text);
	        if (usr != null)
	        {
	            var options = new PusherOptions
	            {
	                Cluster = "us2",
	                Encrypted = true
	            };

	            var pusher = new Pusher(
	                "650608",
	                "87da8c910fa9a303c2f4",
	                "be9679542369b5035426",
	                options);

	            var result = pusher.TriggerAsync(
	                "my-channel",
	                "my-event",
	                new { message = "hello world" });
                Application.Current.MainPage = new NavigationPage(new MainPage());
            }
	    }
	}
}