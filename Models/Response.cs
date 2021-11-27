using System;
using System.Collections;
using System.Collections.Generic;

namespace SMS_Sender2.Models
{
    public class Response
    {
        public int id { get; set; }

        public string FIO { get; set; }

        public string TelefonAddress { get; set; }

        public string Text { get; set; }

        public int Type { get; set; }

        public string IsSend { get; set; }

        public DateTime createdAt { get; set; }

        public DateTime updatedAt { get; set; }

        public string Error { get; set; }

    }

    public class body
    {
        public IList<Response> response { get; set; }

    }

}