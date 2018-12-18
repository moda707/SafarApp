using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

		public ChatPage ()
		{
			InitializeComponent ();
		}

	    public ChatPage(string tripIds)
	    {
	        InitializeComponent();
	        this.tripId = tripIds;
	        LoadMessages();
	    }

	    private async void LoadMessages()
	    {
	        ChatMessages = await ChatManager.GetMessages(tripId, 0, 20);
            
	        ChatMessagesListView.ItemsSource = ChatMessages;

	    }

	    private async void SendMessage_OnClick(object sender, EventArgs e)
	    {
	        var message = new ChatMessage()
	        {
                TripId = "123",
                MessageText = txtMessageToSend.Text,
                MessageId = Guid.NewGuid().ToString(),
                MessageDate = DateTime.Now,
                FromId = "312",
                FromName = "mm",
                MessageSeenStatus = ChatMessageSeenStatus.Unseen,
                MessageStatus = ChatMessageStatus.Original,
                MessageType = ChatMessageType.Text,
                UriLink = ""
	        };

            var t = await ChatManager.SendMessage(message);
	        if (t.Result == ResultEnum.Successfull)
	        {
	            LoadMessages();
	        }
	    }
	}
}