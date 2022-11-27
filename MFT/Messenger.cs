using System;

namespace MFT
{
    public static class Messenger
    {
        public static void SendMessage(object sender, Message.Types type, object _object)
        {
            activeEvents++;
            MessageAvailable?.Invoke(sender, new Message(type, _object));
            activeEvents--;
        }

        public static event EventHandler<Message> MessageAvailable;

        static int activeEvents = 0;
    }
}
