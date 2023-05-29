using System;

namespace MFT
{
    internal class WorkspaceManager
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
            Messenger.MessageAvailable += OnMessageReceived;
        }

        Workspace workspace { get; set; } = new Workspace();

        void OnMessageReceived(object sender, Message msg)
        {
            switch (msg.Type)
            {
                case Message.Types.CAMERA_CONNECT:
                    ConnectCamera(msg.Object as ICamera);
                    break;
                case Message.Types.EXPOSURE_SETTINGS_CREATE:
                    CreateExposureSettings(msg.Object as ExposureSettings);
                    break;
                case Message.Types.EXPOSURE_SETTINGS_APPLY:
                    ApplyExposureSettingsToSpectrometer(msg.Object as ExposureSettings);
                    break;
                case Message.Types.EXPOSURE_SETTINGS_SET_DEFAULT:
                    SetDefaultExposureSettings(msg.Object as ExposureSettings);
                    break;
                case Message.Types.EXPOSURE_SETTINGS_DELETE:
                    DeleteExposureSettings(msg.Object as ExposureSettings);
                    break;
                case Message.Types.SPECTROMETER_CONNECT:
                    ConnectSpectrometer((SpectrometerTypes)msg.Object);
                    break;
                case Message.Types.SPECTRUM_PROCESSOR_CHAIN_CREATE:
                    CreateSpectrumProcessorChain();
                    break;
                case Message.Types.EXITING:
                    Cleanup();
                    break;
            }
        }

        void ConnectCamera(ICamera camera)
        {
            string name = camera.Name;

            if (workspace.Cameras.Contains(camera))
            {
                Messenger.SendMessage(this, Message.Types.CAMERA_UPDATED, camera);
                return;
            }

            // todo: figure out how to synchronize AForge camera object with physical camera
            // so we can tell if physical camera is running
            if (camera.IsRunning)
            {
                Messenger.SendMessage(this, Message.Types.ERROR,
                    $"'{name}' appears to be running in another application.");
                return;
            }

            workspace.Cameras.Add(camera);
            camera.Start();
            Messenger.SendMessage(this, Message.Types.CAMERA_UPDATED, camera);
        }

        void CreateExposureSettings(ExposureSettings settings)
        {
            ExposureSettings s;
            if (settings == null)
                s = new ExposureSettings(workspace.DefaultExposureSettings);
            else
                s = settings;
            workspace.ExposureSettings.Add(s);
            Messenger.SendMessage(this, Message.Types.EXPOSURE_SETTINGS_CREATED, s);
        }

        void ApplyExposureSettingsToSpectrometer(ExposureSettings settings)
        {
            if (workspace.Spectrometer == null)
            {
                Messenger.SendMessage(this, Message.Types.ERROR, "No spectrometer connected.");
                return;
            }
            workspace.Spectrometer.Settings = new ExposureSettings(settings);
            Messenger.SendMessage(this, Message.Types.SPECTROMETER_UPDATED, workspace.Spectrometer);
        }

        void SetDefaultExposureSettings(ExposureSettings settings)
        {
            workspace.DefaultExposureSettings = settings;
            Messenger.SendMessage(this, Message.Types.EXPOSURE_SETTINGS_DEFAULT_SET, settings);
        }

        void DeleteExposureSettings(ExposureSettings settings)
        {
            if (!workspace.ExposureSettings.Contains(settings))
                throw new Exception($"Could not find '{settings}' to delete.");
            if (workspace.ExposureSettings.Remove(settings))
                Messenger.SendMessage(this, Message.Types.EXPOSURE_SETTINGS_DELETED, settings);
            else
                Messenger.SendMessage(this, Message.Types.ERROR, "Could not delete exposure settings.");
        }

        void ConnectSpectrometer(SpectrometerTypes type)
        {
            try
            {
                workspace.Spectrometer = SpectrometerFactory.GetSpectrometer(type);
            }
            catch (NotImplementedException)
            {
                Messenger.SendMessage(this, Message.Types.ERROR, $"Internal error: unknown device '{type}'");
                return;
            }
            string ErrMsg;
            if (!workspace.Spectrometer.Connect(out ErrMsg))
            {
                Messenger.SendMessage(this, Message.Types.ERROR, $"Problem connecting: {ErrMsg}");
                return;
            }
            workspace.Spectrometer.Settings = new ExposureSettings(workspace.DefaultExposureSettings);
            workspace.Spectrometer.Chain = new SpectrumProcessorChain(workspace.DefaultSpectrumProcessorChain);
            Messenger.SendMessage(this, Message.Types.SPECTROMETER_CONNECTED, workspace.Spectrometer);
        }

        void CreateSpectrumProcessorChain()
        {
            var c = new SpectrumProcessorChain();
            c.Name = GetSpectrumProcessorAutoName();
            workspace.SpectrumProcessorChains.Add(c);
            Messenger.SendMessage(this, Message.Types.SPECTRUM_PROCESSOR_CHAIN_CREATED, c);
        }

        string GetSpectrumProcessorAutoName()
        {
            string name = "Chain " + workspace.NextSpectrumProcessorChainNameIndex.ToString();
            while (workspace.SpectrumProcessorChainNames.Contains(name))
            {
                workspace.NextSpectrumProcessorChainNameIndex++;
                name = "Chain " + workspace.NextSpectrumProcessorChainNameIndex.ToString();
            }
            return name;
        }

        void Cleanup()
        {
            foreach (var camera in workspace.Cameras)
            {
                camera.Stop();
            }
        }
    }
}
