﻿using System;
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
	        var u = await UsersManager.GetUserByEmailPass(txtEmail.Text, txtPassword.Text);

	        if (u != null)
	        {
	            await Application.Current.MainPage.DisplayAlert("Succesfully loged in", u.DisplayName + " Logged in", "Yes");
	        }

	    }
	}
}