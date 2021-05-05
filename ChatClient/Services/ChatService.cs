using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatClient.Enums;
using ChatClient.Models;
using System.Net;
using Microsoft.AspNet.SignalR.Client;

namespace ChatClient.Services
{
    public class ChatService : IChatService
    {
        public event Action<User> ParticipantLoggedIn;
        public event Action<string> ParticipantLoggedOut;
        public event Action<string> ParticipantDisconnected;
        public event Action<string> ParticipantReconnected;
        public event Action ConnectionReconnecting;
        public event Action ConnectionReconnected;
        public event Action ConnectionClosed;
        public event Action<string, string, MessageType> NewTextMessage;
        public event Action<string, byte[], MessageType> NewImageMessage;
        public event Action<string> ParticipantTyping;

        private IHubProxy hubProxy;
        private HubConnection connection;
        private string url = "http://localhost:8080/signalrchat";

        public async Task ConnectAsync()
        {
            connection = new HubConnection(url);
            hubProxy = connection.CreateHubProxy("ChatHub");
            hubProxy.On<User>("ParticipantLogin", (login)=> ParticipantLoggedIn?.Invoke(login));
            hubProxy.On<string>("ParticipantLogout", (logout) => ParticipantLoggedOut?.Invoke(logout));
            hubProxy.On<string>("ParticipantReconnection", (recon) => ParticipantReconnected?.Invoke(recon));
            hubProxy.On<string>("ParticipantDisconnection", (discon) => ParticipantDisconnected?.Invoke(discon));
            hubProxy.On<string, string>("BroadcastTextMessage", (sender, message) => NewTextMessage?.Invoke(sender,message,MessageType.Broadcast));
            hubProxy.On<string, byte[]>("BroadcastPicturePhoto", (sender, img) => NewImageMessage?.Invoke(sender,img, MessageType.Broadcast));
            hubProxy.On<string, byte[]>("UnicastPicturePhoto", (sender, img) => NewImageMessage?.Invoke(sender, img, MessageType.Unicast));
            hubProxy.On<string, string>("UnicastTextMessage", (sender, message) => NewTextMessage?.Invoke(sender, message, MessageType.Unicast));
            hubProxy.On<string>("ParticipantTyping", (sender) => ParticipantTyping?.Invoke(sender));

            connection.Reconnecting += Connection_Reconnecting;
            connection.Reconnected += Connection_Reconnected;
            connection.Closed += Disconnected;

            ServicePointManager.DefaultConnectionLimit = 7;
            await connection.Start();

        }

        private void Disconnected()
        {
            ConnectionClosed?.Invoke();
        }

        private void Connection_Reconnected()
        {
            ConnectionReconnected?.Invoke();
        }

        private void Connection_Reconnecting()
        {
            ConnectionReconnecting?.Invoke();
        }

        public async Task<List<User>> LoginAsync(string name, byte[] photo)
        {
            return await hubProxy.Invoke<List<User>>("Login", new object[] { name, photo });
        }

        public async Task LogoutAsync()
        {
            await hubProxy.Invoke("Logout");
        }

        public async Task SendBroadcastMessageAsync(string msg)
        {
            await hubProxy.Invoke("BroadcastTextMessage", msg);
        }

        public async Task SendBroadcastMessageAsync(byte[] img)
        {
            await hubProxy.Invoke("BroadcastTextMessage",img);
        }

        public async Task SendUnicastMessageAsync(string recepient, string msg)
        {
            await hubProxy.Invoke("UnicastTextMessage",new object[] { recepient, msg });
        }

        public async Task SendUnicastMessageAsync(string recepient, byte[] img)
        {
            await hubProxy.Invoke("UnicastTextMessage", new object[] { recepient, img });
        }

        public async Task TypingAsync(string recepient)
        {
            await hubProxy.Invoke("Typing", recepient);
        }
    }
}
