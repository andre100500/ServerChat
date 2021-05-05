using ChatClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Models
{
    public class Participant : ViewModel
    {
        public string Name { get; set; }
        public byte[] Photo { get; set; }
        public List<ChatMessage> Chatter { get; set; }

        private bool _isLoggedIn = true;
        public bool IsLoggedIn
        {
            get { return _isLoggedIn; }
            set { _isLoggedIn = value; Notify(""); }
        }

        private bool _hasSentNewMessage;
        public bool HasSentNewMessage
        {
            get { return _hasSentNewMessage; }
            set { _hasSentNewMessage = value; Notify(""); }
        }

        private bool _isTyping;
        public bool IsTyping
        {
            get { return _isTyping; }
            set { _isTyping = value; Notify(""); }
        }

        public Participant()
        {
            User = new List<User>();
        }
    }
}
