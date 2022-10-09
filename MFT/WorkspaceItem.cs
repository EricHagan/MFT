namespace MFT
{
    public class WorkspaceItem
    {
        public WorkspaceItem(string name, long handle)
        {
            Name = name;
            Handle = handle;
        }
        public string Name { get; private set; }
        public long Handle { get; private set; }

        public static long Register(WorkspaceItem item)
        {
            lock (Lock)
            {
                item.Handle = NextHandle++;
            }
            return item.Handle;
        }
        static long NextHandle = 1;
        internal void SetNextHandle(int handle) { NextHandle = handle; }
        static object Lock = new object();
    }
}
