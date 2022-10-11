﻿using System;
using System.Windows.Forms;

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
                    CreateExposureSettings();
                    break;
                case Message.Types.EXPOSURE_SETTINGS_UPDATED:
                    UpdateExposureSettings(sender, msg.Object as ExposureSettings);
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
            Messenger.SendMessage(this, Message.Types.CAMERA_UPDATED, camera);
        }

        void CreateExposureSettings()
        {
            var s = new ExposureSettings(workspace.DefaultExposureSettings);
            Register(s);
            workspace.ExposureSettings.Add(s.Handle, s);
            Messenger.SendMessage(this, new Message(
                Message.Types.EXPOSURE_SETTINGS_UPDATED, s));
        }

        void UpdateExposureSettings(object sender, ExposureSettings settings)
        {
            if (sender == this)
                return;
            if (workspace.ExposureSettings.ContainsKey(settings.Handle))
                workspace.ExposureSettings[settings.Handle] = settings;
            if (workspace.DefaultExposureSettings.Handle == settings.Handle)
                workspace.DefaultExposureSettings = settings;
        }

        void SetDefaultExposureSettings(ExposureSettings settings)
        {
            workspace.DefaultExposureSettings = settings;
            Messenger.SendMessage(this, new Message(
                Message.Types.EXPOSURE_SETTINGS_DEFAULT_SET, settings));
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
