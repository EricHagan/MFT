using System;

namespace MFT
{
    public static class Messenger
    {
        public static void SendMessage(object sender, Message msg)
        {
            MessageAvailable?.Invoke(sender, msg);
        }

        public static event EventHandler<Message> MessageAvailable;
    }
}
