using System;

namespace MFT
{
    public static class Messenger
    {
        public static void SendMessage(object sender, Message msg)
        {
            MessageAvailable?.Invoke(sender, msg);
        }

        public static void SendMessage(object sender, Message.Types type, object _object)
        {
            SendMessage(sender, new Message(type, _object));
        }

        public static event EventHandler<Message> MessageAvailable;
    }
}
