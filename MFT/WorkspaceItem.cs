namespace MFT
{
    public class WorkspaceItem : IWorkspaceItem
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
