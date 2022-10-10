namespace MFT
{
    public class WorkspaceManager
    {
        public static WorkspaceManager GetWorkspaceManager()
        {
            if (theWorkspaceManager == null)
                theWorkspaceManager = new WorkspaceManager();
            return theWorkspaceManager;
        }

        static WorkspaceManager theWorkspaceManager;

        WorkspaceManager()
        {
            Messenger.MessageAvailable += OnMessageRecieved;
        }

        Workspace workspace { get; set; } = new Workspace();

        void OnMessageRecieved(object sender, Message msg)
        {
            switch (msg.Type)
            {
                case Message.Types.EXPOSURE_SETTINGS_CREATE:
                    CreateExposureSettings();
                    break;
                case Message.Types.EXPOSURE_SETTINGS_SET_DEFAULT:
                    break;
            }
        }

        void CreateExposureSettings()
        {
            var s = new ExposureSettings(workspace.DefaultExposureSettings);
            Register(s);
            workspace.ExposureSettings.Add(s.Handle, s);
            Messenger.SendMessage(workspace, new Message(
                Message.Types.EXPOSURE_SETTINGS_UPDATED, s));
        }

        long Register(WorkspaceItem item)
        {
            lock (Lock)
            {
                item.Handle = NextHandle++;
            }
            return item.Handle;
        }
        long NextHandle = 1;
        void SetNextHandle(int handle) { NextHandle = handle; }
        static object Lock = new object();
    }
}
