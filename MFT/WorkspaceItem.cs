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
        public long Handle { get; internal set; }


    }
}
