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
            public ItemHolder(ItemTypes type, TabPage page, object _object)
            {
                Type = type;
                Page = page;
                Object = _object;
            }

            public enum ItemTypes { CAMERA, SPECTROMETER, EXPOSURE, EXPOSURE_SETTINGS }

            public ItemTypes Type { get; set; }
            public TabPage Page { get; set; }
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
                case Message.Types.SPECTROMETER_UPDATED:
                    UpdateSpectrometer(sender, msg.Object as ISpectrometer);
                    break;
            }
        }

        Dictionary<object, ItemHolder> items { get; set; } = new Dictionary<object, ItemHolder>();
        ItemHolder AddItem(ItemHolder.ItemTypes type, string name, Control control, object obj)
        {
            if (GetItem(obj) != null)
                return null;
            var tabPage = new TabPage(name);
            tabPage.Controls.Add(control);
            var item = new ItemHolder(type, tabPage, obj);
            items.Add(obj, item);
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;
            return item;
        }

        ItemHolder GetItem(object obj)
        {
            if (items.ContainsKey(obj))
                return items[obj];
            else
                return null;
        }

        bool ActivateItem(object obj)
        {
            var item = GetItem(obj);
            if (item == null)
                return false;
            if (!tabControl1.TabPages.Contains(item.Page))
                return false;
            tabControl1.SelectedTab = item.Page;
            return true;
        }

        bool DeleteItem(object obj)
        {
            var item = GetItem(obj);
            if (item == null)
                return false;
            items.Remove(item);
            if (!tabControl1.TabPages.Contains(item.Page))
                return false;
            tabControl1.TabPages.Remove(item.Page);
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
                var tabpage = AddItem(ItemHolder.ItemTypes.CAMERA, name, camDialog, camera);
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
                if (GetItem(settings) != null)
                    throw new Exception($"Settings '{settings}' already exists in documentHolder");
                var control = new ExposureSettingsControl();
                control.Quiet = true; // otherwise stack overflow
                control.Settings = settings;
                var tabpage = AddItem(ItemHolder.ItemTypes.EXPOSURE_SETTINGS, settings.ToString(), control, settings);
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
                var item = GetItem(settings);
                if (item == null)
                    return;
                item.Page.Text = settings.ToString();
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
                //TabPage tabPage;
                ////if it's in the tree, update it:
                //if (spectrometerNode != null)
                //{
                //    var h = spectrometerNode.Tag as ItemHolder;
                //    tabPage = h.Page;
                //    var c = tabPage.Controls[0] as SpectrometerControl;
                //    spectrometerNode.Text = spectrometer.GetDeviceDescription();
                //    h.Object = spectrometer;
                //    c.SetSpectrometer(spectrometer);
                //    settingsNode = spectrometerNode.Nodes[0];

                //    if (spectrometer.DarkReference != null)
                //    {
                //        if (darkRefNode == null)
                //        {
                //            darkRefNode = new TreeNode("Dark Reference");
                //            var darkControl = new SingleSpectrumGraph();
                //            darkControl.Exposure = spectrometer.DarkReference;
                //            darkControl.Dock = DockStyle.Fill;
                //            var darkTab = new TabPage("Dark Reference");
                //            darkTab.Controls.Add(darkControl);
                //            tabControl1.TabPages.Add(darkTab);
                //            var darkHolder = new ItemHolder(ItemHolder.ItemTypes.EXPOSURE, darkTab, spectrometer.DarkReference);
                //            darkRefNode.Tag = darkHolder;
                //            spectrometerNode.Nodes.Add(darkRefNode);
                //        }
                //    }
                //}
                ////if not, add it to the tree:
                //else
                //{
                //    tabPage = new TabPage(spectrometer.GetDeviceDescription());
                //    var control = new SpectrometerControl(spectrometer);
                //    control.Dock = DockStyle.Fill;
                //    tabPage.Controls.Add(control);
                //    tabControl1.TabPages.Add(tabPage);

                //    if (spectrometer.DarkReference != null)
                //    {
                //        darkRefNode = new TreeNode("Dark Reference");
                //        var darkControl = new SingleSpectrumGraph();
                //        darkControl.Exposure = spectrometer.DarkReference;
                //        darkControl.Dock = DockStyle.Fill;
                //        var darkTab = new TabPage("Dark Reference");
                //        darkTab.Controls.Add(darkControl);
                //        tabControl1.TabPages.Add(darkTab);
                //        var darkHolder = new ItemHolder(ItemHolder.ItemTypes.EXPOSURE, darkTab, spectrometer.DarkReference);
                //        darkRefNode.Tag = darkHolder;
                //        spectrometerNode.Nodes.Add(darkRefNode);
                //    }
                //    spectrometerSettingsNode.EnsureVisible();
                //    tabControl1.SelectTab(tabPage);
                //}
            }
        }




    }
}
