using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MFT
{
    public partial class DocumentsControl : UserControl
    {
        public class ItemHolder
        {
            public ItemHolder() { }

            public ItemHolder(ItemTypes type, object _object)
            {
                Type = type;
                Object = _object;
            }

            public enum ItemTypes { CAMERA,
                SPECTROMETER, DARKREF,
                EXPOSURE, EXPOSURE_SETTINGS }

            public ItemTypes Type { get; set; }
            public object Object { get; set; }
        }

        public DocumentsControl()
        {
            InitializeComponent();
            Messenger.MessageAvailable += OnMessageReceived;
        }

        void OnMessageReceived(object sender, Message msg)
        {
            switch (msg.Type)
            {
                case Message.Types.CAMERA_UPDATED:
                    OnCameraUpdated(sender, msg.Object as ICamera);
                    break;
                case Message.Types.EXPOSURE_SETTINGS_CREATED:
                    CreateExposureSettings(sender, msg.Object as ExposureSettings);
                    break;
                case Message.Types.EXPOSURE_SETTINGS_UPDATED:
                    UpdateExposureSettings(sender, msg.Object as ExposureSettings);
                    break;
                case Message.Types.SPECTROMETER_CONNECTED:
                    ConnectSpectrometer(sender, msg.Object as ISpectrometer);
                    break;
                case Message.Types.SPECTROMETER_UPDATED:
                    UpdateSpectrometer(sender, msg.Object as ISpectrometer);
                    break;
                case Message.Types.CAMERA_ACTIVATED:
                case Message.Types.SPECTROMETER_ACTIVATED:
                case Message.Types.EXPOSURE_ACTIVATED:
                case Message.Types.EXPOSURE_SETTINGS_ACTIVATED:
                    ItemActivated(msg.Object);
                    break;
            }
        }

        TabPage AddPage(ItemHolder.ItemTypes type, string name,
            Control control, object obj, bool activate = true)
        {
            if (GetPage(obj) != null)
                return null;
            var tabPage = new TabPage(name);
            tabPage.Controls.Add(control);
            tabPage.Tag = new ItemHolder(type, obj);
            tabControl1.TabPages.Add(tabPage);
            if (activate)
                tabControl1.SelectedTab = tabPage;
            return tabPage;
        }

        TabPage GetPage(object obj)
        {
            foreach (var item in tabControl1.TabPages)
            {
                var page = item as TabPage;
                var h = page.Tag as ItemHolder;
                if (h.Object == obj)
                    return page;
            }
            return null;
        }

        bool ActivatePage(object obj)
        {
            var page = GetPage(obj);
            if (page == null)
                return false;
            tabControl1.SelectedTab = page;
            return true;
        }

        bool DeletePage(object obj)
        {
            var page = GetPage(obj);
            if (page == null)
                return false;
            tabControl1.TabPages.Remove(page);
            return true;
        }

        void OnCameraUpdated(object sender, ICamera camera)
        {
            if (InvokeRequired)
            {
                Action safeUpdate = delegate { OnCameraUpdated(sender, camera); };
                Invoke(safeUpdate);
            }
            else
            {
                string name = camera.Name;
                var camDialog = new CameraControl();
                camDialog.SetCamera(camera);
                var tabpage = AddPage(ItemHolder.ItemTypes.CAMERA, name, camDialog, camera);
            }
        }

        void CreateExposureSettings(object sender, ExposureSettings settings)
        {
            if (InvokeRequired)
            {
                Action safeUpdate = delegate { CreateExposureSettings(sender, settings); };
                Invoke(safeUpdate);
            }
            else
            {
                if (GetPage(settings) != null)
                    throw new Exception($"Settings '{settings}' already exists in documentHolder");
                var control = new ExposureSettingsControl();
                control.Quiet = true; // otherwise stack overflow
                control.Settings = settings;
                var tabpage = AddPage(ItemHolder.ItemTypes.EXPOSURE_SETTINGS, settings.ToString(), control, settings, activate: false);
                control.Quiet = false;
            }
        }

        void UpdateExposureSettings(object sender, ExposureSettings settings)
        {
            if (InvokeRequired)
            {
                Action safeUpdate = delegate { UpdateExposureSettings(sender, settings); };
                Invoke(safeUpdate);
            }
            else
            {
                var page = GetPage(settings);
                if (page == null)
                    return;
                page.Text = settings.ToString();
            }
        }

        void ConnectSpectrometer(object sender, ISpectrometer spectrometer)
        {
            if (InvokeRequired)
            {
                Action safeUpdate = delegate { ConnectSpectrometer(sender, spectrometer); };
                Invoke(safeUpdate);
            }
            else
            {
                // delete any existing spectrometer pages:
                var pagesToRemove = new List<TabPage>();
                foreach (var item in tabControl1.TabPages)
                {
                    var p = item as TabPage;
                    var h = p.Tag as ItemHolder;
                    if (h.Type == ItemHolder.ItemTypes.SPECTROMETER ||
                        h.Type == ItemHolder.ItemTypes.DARKREF)
                    {
                        pagesToRemove.Add(p);
                    }
                }
                foreach (var p in pagesToRemove)
                    tabControl1.TabPages.Remove(p);

                var page = AddPage(ItemHolder.ItemTypes.SPECTROMETER, spectrometer.GetDeviceDescription(),
                    new SpectrometerControl(spectrometer), spectrometer);

                UpdateSpectrometer(sender, spectrometer);
                tabControl1.SelectedTab = page;
            }
        }

        void UpdateSpectrometer(object sender, ISpectrometer spectrometer)
        {
            if (InvokeRequired)
            {
                Action safeUpdate = delegate { UpdateSpectrometer(sender, spectrometer); };
                Invoke(safeUpdate);
            }
            else
            {
                var spectrometerPage = GetPage(spectrometer);
                if (spectrometerPage == null)
                    return;

                if (spectrometer.DarkReference != null)
                {
                    var control = new SingleSpectrumGraph();
                    control.Exposure = spectrometer.DarkReference;

                    var existingDarkRefPages = new List<TabPage>();
                    foreach (var item in tabControl1.TabPages)
                    {
                        var page = item as TabPage;
                        var h = page.Tag as ItemHolder;
                        if (h.Type == ItemHolder.ItemTypes.DARKREF)
                            existingDarkRefPages.Add(page);
                    }
                    if (existingDarkRefPages.Count > 1)
                        throw new Exception("More than 1 dark ref page exists.");

                    TabPage darkRefPage;
                    if (existingDarkRefPages.Count == 1)
                    {
                        darkRefPage = existingDarkRefPages[0];
                        darkRefPage.Text = "Dark Reference";
                        darkRefPage.Controls.Clear();
                        darkRefPage.Controls.Add(control);
                        darkRefPage.Tag = new ItemHolder(ItemHolder.ItemTypes.DARKREF, spectrometer.DarkReference);
                    }
                    else
                        AddPage(ItemHolder.ItemTypes.DARKREF, "Dark Reference", control, spectrometer.DarkReference, activate: false);
                }
            }
        }

        void ItemActivated(object obj)
        {
            var page = GetPage(obj);
            if (page == null)
                return;
            tabControl1.SelectedTab = page;
        }


    }
}
