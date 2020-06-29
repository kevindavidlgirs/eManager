using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_1920_g04.Model
{
    class Message
    {
        private DateTime dt { get; set; }
        private string message { get; set; }
        private bool warningMessage { get; set; }
        public override string ToString()
        {
            return warningMessage ? dt.ToString("H:mm:ss tt") + " !!! WARNING !!! " + ": " + message
                                  : dt.ToString("H:mm:ss tt") + " Message " + ": " + message;
        }

        public Message(bool typeMessage, string message)
        {
            this.dt = DateTime.Now;
            this.message = message;
            this.warningMessage = typeMessage;
        }
    }
}
