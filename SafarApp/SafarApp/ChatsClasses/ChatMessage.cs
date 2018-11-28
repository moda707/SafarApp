﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SafarApp.DbClasses;
using SafarApp.GenFunctions;
using SafarApp.UserClasses;

namespace SafarApp.ChatsClasses
{
    public class ChatMessage
    {
        public ObjectId MessageId { get; set; }
        public ObjectId TripId { get; set; }
        public string MessageText { get; set; }
        public string FromName { get; set; }
        public ObjectId FromId { get; set; }
        public string UriLink { get; set; }
        public ChatMessageType MessageType { get; set; }
        public ChatMessageStatus MessageStatus { get; set; }
        public ChatMessageSeenStatus MessageSeenStatus { get; set; }
        public DateTime MessageDate { get; set; }

        public ChatMessage()
        {
            
        }

        #region Converters

        public static ChatMessage ConvertChatMsgInDbtoChatMsg(ChatMessageInDb chatMessageInDb)
        {
            return new ChatMessage()
            {
                MessageId = chatMessageInDb.MessageId,
                TripId = chatMessageInDb.TripId,
                MessageText = chatMessageInDb.MessageText,
                FromId = chatMessageInDb.FromId,
                FromName = Users.getUserById(chatMessageInDb.FromId).DisplayName,
                MessageDate = chatMessageInDb.MessageDate,
                UriLink = chatMessageInDb.UriLink,
                MessageType = (ChatMessageType)chatMessageInDb.MessageType,
                MessageStatus = (ChatMessageStatus)chatMessageInDb.MessageStatus,
                MessageSeenStatus = (ChatMessageSeenStatus)chatMessageInDb.MessageSeenStatus
            };
        }


        public static ChatMessageInDb ConvertChatMsgTranstoChatMsgInDb(ChatMessageTrans chatMessageTrans)
        {
            return new ChatMessageInDb()
            {
                MessageId = ObjectId.Parse(chatMessageTrans.MessageId),
                TripId = ObjectId.Parse(chatMessageTrans.TripId),
                MessageText = chatMessageTrans.MessageText,
                FromId = ObjectId.Parse(chatMessageTrans.FromId),
                MessageDate = chatMessageTrans.MessageDate,
                UriLink = chatMessageTrans.UriLink,
                MessageType = chatMessageTrans.MessageType,
                MessageStatus = chatMessageTrans.MessageStatus,
                MessageSeenStatus = chatMessageTrans.MessageSeenStatus
            };
        }


        #endregion

        #region ADD UPDATE DELETE Functions

        public static async Task<FuncResult> AddUpdateMessage(ChatMessageTrans chatMessageTrans)
        {
            var chatMessageInDb = ConvertChatMsgTranstoChatMsgInDb(chatMessageTrans);

            //add it to db

            var addres = DbConnection.FastAddorUpdate(chatMessageInDb, CollectionNames.Chats);
            if (addres == FuncResult.Unsuccessful) return FuncResult.Unsuccessful;

            //call a chanel in Pusher
            var pusher = new PusherFunc();
            var t = await pusher.Push(chatMessageTrans.TripId, "NewChatMessage",
                new ChatMessageShort(chatMessageTrans.FromId, chatMessageTrans.MessageText));


            return FuncResult.Successful;
        }

        public static FuncResult DeleteMessage(string messageId)
        {
            //Delete message with messageId

            return FuncResult.Successful;
        }

        #endregion

        #region Get Messages

        public static List<ChatMessage> GetChatMessages(string tripId, int startIndex = 0, int count = 20)
        {
            var otripId = ObjectId.Parse(tripId);
            var dbConnection = new DbConnection();
            dbConnection.ConnectOpenReg();

            var filter = new List<FieldFilter>()
            {
                new FieldFilter("TripId", otripId, FieldType.ObjectId, CompareType.Equal)
            };

            var sort = new SortFilter("MessageDate", SortType.Descending);
            var chatList = dbConnection.GetFilteredList<ChatMessage>(CollectionNames.Chats, filter, sort, count);
            
            return chatList;
        }

        #endregion

    }


    public class ChatMessageInDb
    {
        public ObjectId MessageId { get; set; }
        public ObjectId TripId { get; set; }
        public string MessageText { get; set; }
        public ObjectId FromId { get; set; }
        public string UriLink { get; set; }
        public int MessageType { get; set; }
        public int MessageStatus { get; set; }
        public int MessageSeenStatus { get; set; }
        public DateTime MessageDate { get; set; }

        public ChatMessageInDb()
        {
            
        }
    }

    public class ChatMessageTrans
    {
        public string MessageId { get; set; }
        public string TripId { get; set; }
        public string MessageText { get; set; }
        public string FromId { get; set; }
        public string UriLink { get; set; }
        public int MessageType { get; set; }
        public int MessageStatus { get; set; }
        public int MessageSeenStatus { get; set; }
        public DateTime MessageDate { get; set; }

        public ChatMessageTrans()
        {
            
        }
    }

    public class ChatMessageShort
    {
        public string FromName { get; set; }
        public string MessageText { get; set; }

        public ChatMessageShort(string fromName, string messageText)
        {
            FromName = fromName;
            MessageText = messageText;
        }
    }

    public class ImageInfo
    {
        public string TripId { get; set; }
        public string MessageId { get; set; }
        public MediaType MediaType { get; set; }
    }

    public enum MediaType
    {
        Image = 0,
        Video = 1
    }
}
