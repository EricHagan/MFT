using MFT.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

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
            InitTreeView();
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
                case Message.Types.EXPOSURE_SETTINGS_UPDATED:
                    UpdateExposureSettings(sender, msg.Object as ExposureSettings);
                    break;
                case Message.Types.EXPOSURE_SETTINGS_DEFAULT_SET:
                    SetDefaultExposureSettings(msg.Object as ExposureSettings);
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
                foreach (var node in camerasNode.Nodes)
                {
                    var treeNode = (TreeNode)node;
                    var itemHolder = treeNode.Tag as ItemHolder;
                    var existingCamera = itemHolder.Object as ICamera;
                    if (existingCamera == camera)
                    {
                        treeNode.EnsureVisible();
                        return;
                    }
                }

                string name = camera.Name;
                var camNode = new TreeNode();

                var camDialog = new CameraControl();
                camDialog.SetCamera(camera);
                var tabpage = AddTabpage(name, camDialog);

                camNode.Tag = new ItemHolder(ItemHolder.ItemTypes.CAMERA, tabpage, camera);
                camNode.Text = name;
                camerasNode.Nodes.Add(camNode);
                camNode.EnsureVisible();
            }
        }

        void OnError(string msg)
        {
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                long handle = settings.Handle;
                // if it's in the tree, update it:
                foreach (var o in exposureSettingsNode.Nodes)
                {
                    var t = o as TreeNode;
                    var h = t.Tag as ItemHolder;
                    var s = h.Object as ExposureSettings;
                    if (s.Handle == handle)
                    {
                        h.Object = settings;
                        t.Text = settings.ToString();
                        h.Page.Text = settings.ToString();
                        var c = h.Page.Controls[0] as ExposureSettingsControl;
                        if (sender != c)
                            c.Settings = settings;
                        return;
                    }
                }
                // if not, add it to the tree:
                var control = new ExposureSettingsControl();
                control.Quiet = true; // otherwise stack overflow
                control.Settings = settings;
                var tabpage = AddTabpage(settings.ToString(), control);
                var node = new TreeNode();
                node.Tag = new ItemHolder(ItemHolder.ItemTypes.EXPOSURE_SETTINGS, tabpage, settings);
                node.Text = settings.ToString();
                node.ContextMenuStrip = exposureSettingsItemContextMenuStrip;
                exposureSettingsNode.Nodes.Add(node);
                node.EnsureVisible();
                control.Quiet = false;
            }
        }

        void SetDefaultExposureSettings(ExposureSettings settings)
        {
            if (InvokeRequired)
            {
                Action safeUpdate = delegate { SetDefaultExposureSettings(settings); };
                Invoke(safeUpdate);
            }
            else
            {
                long handle = settings.Handle;
                bool success = false;
                foreach (var o in exposureSettingsNode.Nodes)
                {
                    var t = o as TreeNode;
                    var h = t.Tag as ItemHolder;
                    var s = h.Object as ExposureSettings;
                    t.NodeFont = new Font(workspaceTreeView.Font, FontStyle.Regular);
                    if (s.Handle == handle)
                    {
                        t.NodeFont = new Font(workspaceTreeView.Font, FontStyle.Underline);
                        success = true;
                    }
                }
                if (!success)
                    Messenger.SendMessage(this, new Message(Message.Types.ERROR,
                        "Internal error. Can't find ExposureSettings in view."));
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
                // if it's in the tree, update it:
                if (spectrometerNode != null)
                {
                    var h = spectrometerNode.Tag as ItemHolder;
                    var t = h.Page;
                    var c = t.Controls[0] as SpectrometerControl;
                    spectrometerNode.Text = spectrometer.GetDeviceDescription();
                    h.Object = spectrometer;
                    c.SetSpectrometer(spectrometer);
                }
                // if not, add it to the tree:
                else
                {
                    var tabPage = new TabPage(spectrometer.GetDeviceDescription());
                    var control = new SpectrometerControl(spectrometer);
                    control.Dock = DockStyle.Fill;
                    tabPage.Controls.Add(control);
                    tabControl1.TabPages.Add(tabPage);
                    tabControl1.SelectedIndex = tabControl1.TabCount - 1;
                    var item = new ItemHolder(ItemHolder.ItemTypes.SPECTROMETER, tabPage, spectrometer);
                    if (spectrometerNode == null)
                    {
                        spectrometerNode = new TreeNode();
                        spectrometerTitleNode.Nodes.Add(spectrometerNode);
                    }
                    spectrometerNode.Tag = item;
                    spectrometerNode.Text = spectrometer.GetDeviceDescription();
                    spectrometerNode.EnsureVisible();
                }
            }
        }

        TreeNode root;
        TreeNode camerasNode;
        TreeNode spectrometerTitleNode;
        TreeNode spectrometerNode;
        TreeNode exposureSettingsNode;
        TreeNode spectrumProcessorChainsNode;
        TreeNode testsNode;
        void InitTreeView()
        {
            workspaceTreeView.Nodes.Clear();

            // root node
            root = workspaceTreeView.Nodes.Add("Workspace");

            // cameras
            camerasNode = root.Nodes.Add("Cameras");
            camerasNode.ContextMenuStrip = camerasContextMenuStrip;
            camerasContextMenuStrip.Items.Clear();
            foreach (var camera in CameraCollection.GetCameras())
            {
                var t = new ToolStripMenuItem();
                t.Tag = camera;
                t.Text = "Connect " + camera.Name;
                camerasContextMenuStrip.Items.Add(t);
            }

            // spectrometer
            spectrometerTitleNode = root.Nodes.Add("Spectrometer");
            spectrometerTitleNode.ContextMenuStrip = spectrometerTitleContextMenuStrip;
            spectrometerTitleContextMenuStrip.Items.Clear();
            foreach (var t in Enum.GetValues(typeof(SpectrometerTypes)))
            {
                var d = new SpectrometerSelectionView();
                d.Type = (SpectrometerTypes)t; //explicit cast
                var toolStripItem = new ToolStripMenuItem();
                toolStripItem.Tag = d;
                toolStripItem.Text = "Connect " + d.ToString();
                spectrometerTitleContextMenuStrip.Items.Add(toolStripItem);
            }

            // exposure settings
            exposureSettingsNode = root.Nodes.Add("Exposure Settings");
            exposureSettingsNode.ContextMenuStrip = exposureSettingsTitleContextMenuStrip;
            exposureSettingsTitleContextMenuStrip.Items.Clear();
            var createExposureSettings = new ToolStripMenuItem();
            createExposureSettings.Text = "Create Exposure Settings";
            createExposureSettings.Click += CreateExposureSettings_Click;
            exposureSettingsTitleContextMenuStrip.Items.Add(createExposureSettings);

            // spectrum processor chains
            spectrumProcessorChainsNode = root.Nodes.Add("Spectrum Processor Chains");

            // tests
            testsNode = root.Nodes.Add("Tests");

            root.Expand();
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

        #region Context Menu Item Clicked Handlers

        private void spectrometerContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // assumes item clicked is a connect instruction
            var selected = (SpectrometerSelectionView)e.ClickedItem.Tag;
            Messenger.SendMessage(this, new Message(
                Message.Types.SPECTROMETER_CONNECT, selected.Type));
        }

        // todo: refactor with Messages:
        private void camerasContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var camera = (ICamera)e.ClickedItem.Tag;
            Messenger.SendMessage(this, Message.Types.CAMERA_CONNECT, camera);
        }

        private void CreateExposureSettings_Click(object sender, EventArgs e)
        {
            Messenger.SendMessage(
                this, new Message(Message.Types.EXPOSURE_SETTINGS_CREATE, null));
        }

        private void workspaceTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // make sure that we select the node we right-click, so context menus know
            // what node opened them
            if (e.Button == MouseButtons.Right)
                workspaceTreeView.SelectedNode = e.Node;
        }

        private void setAsDefaultExposureSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var n = workspaceTreeView.SelectedNode;
            var h = n.Tag as ItemHolder;
            var settings = h.Object as ExposureSettings;
            Messenger.SendMessage(
                this, new Message(Message.Types.EXPOSURE_SETTINGS_SET_DEFAULT, settings));
        }

        #endregion

        private void workspaceTreeView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var tree = (TreeView)sender;
                var node = tree?.GetNodeAt(new Point(e.X, e.Y));
                if (node == null)
                    return;

                var itemHolder = (ItemHolder)node.Tag;
                if (itemHolder == null)
                    return;
                switch (itemHolder.Type)
                {
                    case ItemHolder.ItemTypes.CAMERA:
                    case ItemHolder.ItemTypes.SPECTROMETER:
                    case ItemHolder.ItemTypes.EXPOSURE_SETTINGS:
                        tabControl1.SelectTab(itemHolder.Page);
                        break;
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (TreeNode cameraNode in camerasNode.Nodes)
            {
                var itemHolder = (ItemHolder)cameraNode.Tag;
                var camera = (ICamera)itemHolder.Object;
                camera.Stop();
            }
        }


    }
}
