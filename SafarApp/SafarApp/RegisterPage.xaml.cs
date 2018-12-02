using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using SafarApp.UserClasses;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SafarApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
		public RegisterPage ()
		{
			InitializeComponent ();
		}

	    private void Register_Clicked(object sender, EventArgs e)
	    {
            var user = new Users()
            {
                Email = txtEmail.Text,
                Password = txtPassword.Text,
                UserId = "123",
                ProfileImage = "",
                DisplayName = "",
                LastActivity = DateTime.Now
            };

            Users.AddUser(user);

            //Users.LoginUser("", "");

        }
	}
}