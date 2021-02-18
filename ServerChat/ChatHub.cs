using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Microsoft.AspNet.SignalR;
using ServerChat.Models;

namespace ServerChat
{
    public class ChatHub : Hub<IClient>
    {
        private static Dictionary<string, User> ChatClients = new Dictionary<string, User>();


        public override Task OnDisconnected(bool stopCalled)
        {
            var userName = ChatClients.SingleOrDefault((c) => c.Value.ID == Context.ConnectionId).Key;
            if (userName != null)
            {
                Clients.Others.Disconnection(userName);
                Console.WriteLine($"{userName} not found");
            }
            return OnDisconnected(stopCalled);
        }
        public override Task OnReconnected()
        {
            var userName = ChatClients.SingleOrDefault((c) => c.Value.ID == Context.ConnectionId).Key;
            if (userName != null)
            {
                Clients.Others.Reconnection(userName);
                Console.WriteLine($"{userName} reconnected");
            }
            return OnReconnected();
        }
        public List<User> Login(string name, byte[] photo)
        {

            if (!ChatClients.ContainsKey(name))
            {
                Console.WriteLine($"{name} logged in");
                List<User> users = new List<User>(ChatClients.Values);
                User newUser = new User { Name = name, ID = Context.ConnectionId, Photo = photo };
                var added = ChatClients.TryGetValue(name, out newUser);
                if (!added) return null;
                Clients.CallerState.UserName = name;
                Clients.Others.Login(newUser);
                return users;
            }
            return null;
        }
        public void Logout()
        {
            var name = Clients.CallerState.UserName;
            if (!string.IsNullOrEmpty(name))
            {
                User client = new User();
                ChatClients.Remove(name);
                Clients.Others.Logout(name);
                Console.WriteLine($"{name} is logout");
            }
        }
        public void BroadcastImageMessage(byte[] img)
        {
            var sender = Clients.CallerState.UserName;
            if (img != null)
            {
                Clients.Others.BroadcastPicturePhoto(sender, img);
            }
        }
        public void BroadcastTextMessage(string message)
        {
            var sender = Clients.CallerState.UserName;
            if (!string.IsNullOrEmpty(sender) && !string.IsNullOrEmpty(message))
            {
                Clients.Others.BroadcastTextMessage(sender, message);
            }
        }
        public void UnicastTextMessage(string recipient , string message)
        {
            var sender = Clients.CallerState.UserName;
            if(!string.IsNullOrEmpty(recipient) 
                && recipient!=sender 
                && !string.IsNullOrEmpty(message) 
                && ChatClients.ContainsKey(recipient))
            {
                User client = new User();
                ChatClients.TryGetValue(recipient, out client);
                Clients.Client(client.ID).UnicastTextMessage(sender, message);
            }
        }
        public void UnicastPicturePhoto(string recipient , byte [] img)
        {
            var sender = Clients.CallerState.UserName;
            if(!string.IsNullOrEmpty(recipient)
                && recipient != sender
                && img !=null
                && ChatClients.ContainsKey(recipient))
            {
                User client = new User();
                ChatClients.TryGetValue(recipient, out client);
                Clients.Client(client.ID).UnicastPicturePhoto(sender, img);
            }
        }
        public void Typing(string recipient)
        {
            if (string.IsNullOrEmpty(recipient))
                return;
            var sender = Clients.CallerState.UserName;
            User client = new User();
            ChatClients.TryGetValue(recipient, out client);
            Clients.Client(client.ID).ParticipantTyping(sender);
        }
    }
}
