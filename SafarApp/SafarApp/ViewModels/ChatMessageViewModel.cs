using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Autofac;
using Microsoft.AspNetCore.SignalR.Client;
using SafarObjects.ChatsClasses;
using SafarSDK.Core;
using SafarSDK.Manager;
using SafarSDK.Models;
using SafarSDK.Services;
using Xamarin.Forms;

namespace SafarApp.ViewModels
{
    public class ChatMessageViewModel : ModelBase
    {
        ObservableCollection<ChatMessageModel> _Messages;
        public ObservableCollection<ChatMessageModel> Messages
        {
            get => _Messages;
            set => SetProperty(ref _Messages, value);
        }

        string _NewText;
        public string NewText
        {
            get => _NewText;
            set => SetProperty(ref _NewText, value);
        }

        bool started;
        
        readonly HubConnection connection;
        readonly string tripId;
        readonly INavigation navigation;
        readonly ICurrentUserManager _currentUserManager;
        readonly IChatService _chatService;
        readonly IAuthManager _authManager;

        public ChatMessageViewModel(
            INavigation navigation,
            string tripId)
        {
            this.navigation = navigation;
            this.tripId = tripId;
            _currentUserManager = Application.Current.GetContainer().Resolve<ICurrentUserManager>();
            
            _chatService = Application.Current.GetContainer().Resolve<IChatService>();
            _authManager = Application.Current.GetContainer().Resolve<IAuthManager>();

            Messages = new ObservableCollection<ChatMessageModel>(new ChatMessageModel[0]);

            connection = new HubConnectionBuilder()
                .WithUrl(GetHubUrl())
                .Build();

            connection.On<ChatMessage>("message.sent", data =>
            {
                Messages.Add(data.ToModel());
            });

        }

        public async void Connect()
        {
            if (false == started)
            {
                started = await connection.StartAsync()
                    .ContinueWith(x => x.IsCompleted && x.IsFaulted == false);
            }

            if (false == started) return;
            

            var msges = await _chatService.GetMessagesAsync(tripId);
            Messages = new ObservableCollection<ChatMessageModel>(msges);

        }

        string GetHubUrl()
        {
            //return "http://nx-inx.azurewebsites.net/chat";
            var baseUrl = Device.RuntimePlatform == Device.iOS
                    ? "http://127.0.0.1:5000/chat"
                    : "http://10.0.2.2:5000/chat";

            return baseUrl + "?access_token=" + _authManager.AuthorizeHeader.Value;
        }

        ICommand _SendCommand;
        public ICommand SendCommand
        {
            get => (_SendCommand = _SendCommand ?? new Command<object>(ExecuteSendCommand));
        }
        async void ExecuteSendCommand(object parameter)
        {
            if (string.IsNullOrWhiteSpace(NewText)) return;

            if (false == started) return;
            
            await connection.SendAsync("Send", new ChatMessage
            {
                TripId = tripId,
                MessageDate = DateTime.Now,
                MessageText = NewText,
                MessageId = Guid.NewGuid().ToString(),
                FromName = _currentUserManager.Profile.DisplayName,
                MessageType = ChatMessageType.Text,
                FromId = _currentUserManager.Profile.UserId,
                MessageSeenStatus = ChatMessageSeenStatus.Unseen,
                MessageStatus = ChatMessageStatus.Original,
                UriLink = ""
            });
            NewText = string.Empty;
        }
    }
}
