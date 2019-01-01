using System;
using Autofac;
using Inx.Networking;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SafarSDK.Core;
using SafarSDK.Manager;
using SafarSDK.Services;
using MessageClient = SafarSDK.MessageClient;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SafarApp
{
    public partial class App : Application
    {
        public IContainer Container { get; private set; }

        public App()
        {
            InitializeComponent();

            //Register
            var builder = new ContainerBuilder();

            builder.Register(cg => new DefaultNetworkingClient(
                    () => new Newtonsoft.Json.JsonSerializer(),
                    () => Container.Resolve<IAuthManager>().AuthorizeHeader,
                    GetBaseUrl()
                ))
                .As<INetworkingClient>()
                .SingleInstance();

            builder.RegisterType<AuthClient>();
            builder.RegisterType<UserClient>();
            builder.RegisterType<MessageClient>();

            builder.RegisterType<AuthManager>()
                .As<IAuthManager>()
                .SingleInstance();
            builder.RegisterType<CurrentUserManager>()
                .As<ICurrentUserManager>()
                .SingleInstance();

            builder.RegisterType<AuthService>().As<IAuthService>();
            //builder.RegisterType<FriendService>().As<IFriendService>();
            builder.RegisterType<ChatService>().As<IChatService>();

            Container = builder.Build();

            var isLoggedIn = Preferences.Get("IsLoggedIn", bool.FalseString);

            if (isLoggedIn == bool.TrueString)
            {
                var userId = Preferences.Get("UserId", "");
                var tokenId = Preferences.Get("TokenId", "");

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

        string GetBaseUrl()
        {
            return Device.RuntimePlatform == Device.iOS
                ? "http://127.0.0.1:5000/api/"
                : "http://10.0.2.2:5000/api/";
        }
    }

    public static class AppExtensions
    {
        public static IContainer GetContainer(this Application app)
        {
            return ((App)app).Container;
        }
    }
}
