using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;


namespace SafarApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            PusherOptions options = new PusherOptions();
            options.Cluster = "us2";
            Pusher pusher = new Pusher("650608", "87da8c910fa9a303c2f4", "be9679542369b5035426", options);
            
            ChannelsList channel = pusher.subscribe("my-channel");

            channel.bind("my-event", new SubscriptionEventListener() {
                @Override
                public void onEvent(String channelName, String eventName, final String data)
                {
                System.out.println(data);
            }
            });
        }
    }
}
