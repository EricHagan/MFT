using System;

namespace MFT
{
    public static class Messenger
    {
        public static void SendMessage(object sender, Message.Types type, object _object)
        {
            MessageAvailable?.Invoke(sender, new Message(type, _object));
        }

        public static event EventHandler<Message> MessageAvailable;
    }
}
