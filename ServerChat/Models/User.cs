using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerChat.Models
{
    public class User
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public byte [] Photo { get; set; }
    }
}
