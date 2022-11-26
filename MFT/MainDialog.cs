using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MFT
{
    public partial class MainDialog : Form
    {
        public MainDialog()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Messenger.MessageAvailable += OnMessageReceived;
        }

        void OnMessageReceived(object sender, Message msg)
        {
            switch (msg.Type)
            {
                case Message.Types.CAMERA_UPDATED:
                    OnCameraUpdated(sender, msg.Object as ICamera);
                    break;
                case Message.Types.ERROR:
                    OnError(msg.Object as string);
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
                var tabpage = AddTabpage(name, camDialog);
            }
        }

        void OnError(string msg)
        {
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                var control = new ExposureSettingsControl();
                control.Quiet = true; // otherwise stack overflow
                control.Settings = settings;
                var tabpage = AddTabpage(settings.ToString(), control);
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
                //var t = FindTreeNode(treeView.TopNode, handle);
                //if (t != null)
                //{
                //    var h = t.Tag as ItemHolder;
                //    var s = h.Object as ExposureSettings;
                //    if (s.Handle == handle)
                //    {
                //        h.Object = settings;
                //        t.Text = settings.ToString();
                //        if (h.Page != null)
                //        {
                //            h.Page.Text = settings.ToString();
                //            var c = h.Page.Controls[0] as ExposureSettingsControl;
                //            if (sender != c)
                //                c.Settings = settings;
                //        }
                //        return;
                //    }
                //}
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
                TabPage tabPage;
                // if it's in the tree, update it:
                //if (spectrometerNode != null)
                //{
                //    var h = spectrometerNode.Tag as ItemHolder;
                //    tabPage = h.Page;
                //    var c = tabPage.Controls[0] as SpectrometerControl;
                //    spectrometerNode.Text = spectrometer.GetDeviceDescription();
                //    h.Object = spectrometer;
                //    c.SetSpectrometer(spectrometer);
                //    settingsNode = spectrometerNode.Nodes[0];
                
                    //if (spectrometer.DarkReference != null)
                    //{
                    //    if (darkRefNode == null)
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
                    //}
                //}
                // if not, add it to the tree:
                //else
                {
                    tabPage = new TabPage(spectrometer.GetDeviceDescription());
                    var control = new SpectrometerControl(spectrometer);
                    control.Dock = DockStyle.Fill;
                    tabPage.Controls.Add(control);
                    tabControl1.TabPages.Add(tabPage);

                    //if (spectrometer.DarkReference != null)
                    //{
                    //    darkRefNode = new TreeNode("Dark Reference");
                    //    var darkControl = new SingleSpectrumGraph();
                    //    darkControl.Exposure = spectrometer.DarkReference;
                    //    darkControl.Dock = DockStyle.Fill;
                    //    var darkTab = new TabPage("Dark Reference");
                    //    darkTab.Controls.Add(darkControl);
                    //    tabControl1.TabPages.Add(darkTab);
                    //    var darkHolder = new ItemHolder(ItemHolder.ItemTypes.EXPOSURE, darkTab, spectrometer.DarkReference);
                    //    darkRefNode.Tag = darkHolder;
                    //    spectrometerNode.Nodes.Add(darkRefNode);
                    //}
                    //spectrometerSettingsNode.EnsureVisible();
                    tabControl1.SelectTab(tabPage);
                }
            }
        }




        TabPage AddTabpage(string name, Control control)
        {
            var tabPage = new TabPage(name);
            tabPage.Controls.Add(control);
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;
            return tabPage;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var d = new AboutDialog();
            d.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //foreach (TreeNode cameraNode in camerasNode.Nodes)
            //{
            //    var itemHolder = (ItemHolder)cameraNode.Tag;
            //    var camera = (ICamera)itemHolder.Object;
            //    camera.Stop();
            //}
        }
    }
}
