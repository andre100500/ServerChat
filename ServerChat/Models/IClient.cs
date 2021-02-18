using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerChat.Models
{
    public interface IClient
    {
        void Disconnection(string name);
        void Reconnection(string name);
        void Login(User user);
        void Logout(string name);
        void BroadcastTextMessage(string sender, string message);
        void BroadcastPicturePhoto(string sender, byte[] img);
        void UnicastTextMessage(string sender, string message);
        void UnicastPicturePhoto(string sender, byte[] img);
        void ParticipantTyping(string sender);
    }
}
