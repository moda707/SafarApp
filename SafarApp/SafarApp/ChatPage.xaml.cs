using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SafarApp.ViewModels;
using SafarObjects.ChatsClasses;
using SafarSDK;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SafarApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChatPage : ContentPage
	{
	    public string tripId { get; set; }
	    public List<ChatMessage> ChatMessages { get; set; }
	    readonly ChatMessageViewModel vm;

        public ChatPage ()
		{
			InitializeComponent ();

		}

	    public ChatPage(string tripId)
	    {
	        InitializeComponent();
	        this.tripId = tripId;

            vm = new ChatMessageViewModel(Navigation, tripId);
	        BindingContext = vm;
	        
	    }

	    
	    //private async void SendMessage_OnClick(object sender, EventArgs e)
	    //{
	    //    var message = new ChatMessage()
	    //    {
     //           TripId = "123",
     //           MessageText = txtMessageToSend.Text,
     //           MessageId = Guid.NewGuid().ToString(),
     //           MessageDate = DateTime.Now,
     //           FromId = "312",
     //           FromName = "mm",
     //           MessageSeenStatus = ChatMessageSeenStatus.Unseen,
     //           MessageStatus = ChatMessageStatus.Original,
     //           MessageType = ChatMessageType.Text,
     //           UriLink = ""
	    //    };

     //       var t = await ChatManager.SendMessage(message);
	    //    if (t.Result == ResultEnum.Successfull)
	    //    {
	    //        LoadMessages();
	    //    }
	    //}

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();

	        vm.Connect();
	    }
    }
}