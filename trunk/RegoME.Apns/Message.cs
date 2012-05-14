using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegoME.Apns
{
    public class Message
    {
        public string Id { get; set; }
        public int SenderId { get; set; }
        public string SenderName { get; set; }
        public int ReceiverId { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
