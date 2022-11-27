﻿using System;

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
                case Message.Types.CAMERA_CONNECT:
                    ConnectCamera(msg.Object as ICamera);
                    break;
                case Message.Types.EXPOSURE_SETTINGS_CREATE:
                    CreateExposureSettings(msg.Object as ExposureSettings);
                    break;
                case Message.Types.EXPOSURE_SETTINGS_UPDATED:
                    UpdateExposureSettings(sender, msg.Object as ExposureSettings);
                    break;
                case Message.Types.EXPOSURE_SETTINGS_APPLY:
                    ApplyExposureSettingsToSpectrometer(msg.Object as ExposureSettings);
                    break;
                case Message.Types.EXPOSURE_SETTINGS_SET_DEFAULT:
                    SetDefaultExposureSettings(msg.Object as ExposureSettings);
                    break;
                case Message.Types.SPECTROMETER_CONNECT:
                    ConnectSpectrometer((SpectrometerTypes)msg.Object);
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

        void UpdateExposureSettings(object sender, ExposureSettings settings)
        {
            // if we're just referencing ExposureSettings, do we need to do anything here?
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
            Messenger.SendMessage(this, Message.Types.SPECTROMETER_CONNECTED, workspace.Spectrometer);
        }
    }
}
