using System;
using System.Threading;

namespace MFT
{
    public static class Messenger
    {
        public static void SendMessage(object sender, Message.Types type, object _object)
        {
            Interlocked.Increment(ref activeEvents);
            MessageAvailable?.Invoke(sender, new Message(type, _object));
            Interlocked.Decrement(ref activeEvents);
        }

        public static event EventHandler<Message> MessageAvailable;

        static int activeEvents = 0;
    }
}
