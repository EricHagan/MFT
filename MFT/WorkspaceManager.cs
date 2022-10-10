using RgbDriverKit;
using System.Windows.Forms;
using System;

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
                    // todo
                    break;
                case Message.Types.SPECTROMETER_CONNECT:
                    ConnectSpectrometer((SpectrometerTypes)msg.Object);
                    break;
            }
        }

        void CreateExposureSettings()
        {
            var s = new ExposureSettings(workspace.DefaultExposureSettings);
            Register(s);
            workspace.ExposureSettings.Add(s.Handle, s);
            Messenger.SendMessage(this, new Message(
                Message.Types.EXPOSURE_SETTINGS_UPDATED, s));
        }

        void ConnectSpectrometer(SpectrometerTypes type)
        {
            try
            {
                workspace.Spectrometer = SpectrometerFactory.GetSpectrometer(type);
            }
            catch (NotImplementedException)
            {
                Messenger.SendMessage(this, new Message(Message.Types.ERROR,
                    $"Internal error: unknown device '{type}'"));
                return;
            }
            string ErrMsg;
            if (!workspace.Spectrometer.Connect(out ErrMsg))
            {
                Messenger.SendMessage(this, new Message(
                    Message.Types.ERROR, $"Problem connecting: {ErrMsg}"));
                return;
            }
            Messenger.SendMessage(this, new Message(
                Message.Types.SPECTROMETER_UPDATED, workspace.Spectrometer));
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
