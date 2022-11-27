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
    public partial class WorkspaceControl : UserControl
    {
        public class ItemHolder
        {
            public ItemHolder(ItemTypes type, object _object)
            {
                Type = type;
                Object = _object;
            }

            public enum ItemTypes { CAMERA, SPECTROMETER, EXPOSURE, EXPOSURE_SETTINGS }

            public ItemTypes Type { get; set; }
            public object Object { get; set; }
        }

        public WorkspaceControl()
        {
            InitializeComponent();
            Messenger.MessageAvailable += OnMessageReceived;
            InitTreeView();
        }

        TreeNode root;
        TreeNode camerasNode;
        TreeNode spectrometerTitleNode;
        TreeNode spectrometerNode;
        TreeNode spectrometerSettingsNode;
        TreeNode darkRefNode;
        TreeNode whiteRefNode;
        TreeNode exposureSettingsNode;
        TreeNode spectrumProcessorChainsNode;
        TreeNode testsNode;
        void InitTreeView()
        {
            treeView.Nodes.Clear();

            // root node
            root = treeView.Nodes.Add("Workspace");

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

                camNode.Tag = new ItemHolder(ItemHolder.ItemTypes.CAMERA, camera);
                camNode.Text = name;
                camerasNode.Nodes.Add(camNode);
                camNode.EnsureVisible();
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
                var t = FindTreeNode(treeView.TopNode, settings);
                if (t != null)
                    throw new Exception($"Internal error. That ExposureSettings object is already in the tree.");
                var control = new ExposureSettingsControl();
                control.Quiet = true; // otherwise stack overflow
                control.Settings = settings;
                var node = new TreeNode();
                node.Tag = new ItemHolder(ItemHolder.ItemTypes.EXPOSURE_SETTINGS, settings);
                node.Text = settings.ToString();
                node.ContextMenuStrip = exposureSettingsItemContextMenuStrip;
                exposureSettingsNode.Nodes.Add(node);
                node.EnsureVisible();
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
                var t = FindTreeNode(treeView.TopNode, settings);
                if (t != null)
                {
                    var h = t.Tag as ItemHolder;
                    var s = h.Object as ExposureSettings;
                    t.Text = settings.ToString();
                }
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
                bool success = false;
                foreach (var o in exposureSettingsNode.Nodes)
                {
                    var t = o as TreeNode;
                    var h = t.Tag as ItemHolder;
                    var s = h.Object as ExposureSettings;
                    t.NodeFont = new Font(treeView.Font, FontStyle.Regular);
                    if (s == settings)
                    {
                        t.NodeFont = new Font(treeView.Font, FontStyle.Underline);
                        success = true;
                    }
                }
                if (!success)
                    Messenger.SendMessage(this, Message.Types.ERROR,
                        "Internal error. Can't find ExposureSettings in view.");
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
                spectrometerTitleNode.Nodes.Clear();

                spectrometerNode = new TreeNode();
                spectrometerTitleNode.Nodes.Add(spectrometerNode);
                var item = new ItemHolder(ItemHolder.ItemTypes.SPECTROMETER, spectrometer);
                spectrometerNode.Tag = item;
                spectrometerNode.Text = spectrometer.GetDeviceDescription();

                spectrometerSettingsNode = new TreeNode();
                spectrometerNode.Nodes.Add(spectrometerSettingsNode);
                var settingsItem = new ItemHolder(ItemHolder.ItemTypes.EXPOSURE_SETTINGS, spectrometer.Settings);
                spectrometerSettingsNode.Tag = settingsItem;
                spectrometerSettingsNode.Text = spectrometer.Settings.ToString();
                spectrometerSettingsNode.EnsureVisible();

                if (spectrometer.DarkReference != null)
                {
                    darkRefNode = new TreeNode("Dark Reference");
                    var darkHolder = new ItemHolder(ItemHolder.ItemTypes.EXPOSURE, spectrometer.DarkReference);
                    darkRefNode.Tag = darkHolder;
                    spectrometerNode.Nodes.Add(darkRefNode);
                    darkRefNode.EnsureVisible();
                }
            }
        }

        private void CreateExposureSettings_Click(object sender, EventArgs e)
        {
            Messenger.SendMessage(this, Message.Types.EXPOSURE_SETTINGS_CREATE, null);
        }

        private void applyToSpectrometerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var n = treeView.SelectedNode;
            var h = n.Tag as ItemHolder;
            var settings = h.Object as ExposureSettings;
            Messenger.SendMessage(this, Message.Types.EXPOSURE_SETTINGS_APPLY, settings);
        }

        private void setAsDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var n = treeView.SelectedNode;
            var h = n.Tag as ItemHolder;
            var settings = h.Object as ExposureSettings;
            Messenger.SendMessage(this, Message.Types.EXPOSURE_SETTINGS_SET_DEFAULT, settings);
        }

        #region Context Menu Item Clicked Handlers

        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // make sure that we select the node we right-click, so context menus know
            // what node opened them
            if (e.Button == MouseButtons.Right)
                treeView.SelectedNode = e.Node;
        }

        private void spectrometerTitleContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // assumes item clicked is a connect instruction
            var selected = (SpectrometerSelectionView)e.ClickedItem.Tag;
            Messenger.SendMessage(this, Message.Types.SPECTROMETER_CONNECT, selected.Type);
        }

        private void camerasContextMenuStrip_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {
            var camera = (ICamera)e.ClickedItem.Tag;
            Messenger.SendMessage(this, Message.Types.CAMERA_CONNECT, camera);
        }

        #endregion



        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var node = e.Node;
                var itemHolder = (ItemHolder)node.Tag;
                if (itemHolder == null)
                    return;
                switch (itemHolder.Type)
                {
                    case ItemHolder.ItemTypes.CAMERA:
                        Messenger.SendMessage(this, Message.Types.CAMERA_ACTIVATED, itemHolder.Object);
                        break;
                    case ItemHolder.ItemTypes.SPECTROMETER:
                        Messenger.SendMessage(this, Message.Types.SPECTROMETER_ACTIVATED, itemHolder.Object);
                        break;
                    case ItemHolder.ItemTypes.EXPOSURE_SETTINGS:
                        Messenger.SendMessage(this, Message.Types.EXPOSURE_SETTINGS_ACTIVATED, itemHolder.Object);
                        break;
                }
            }
        }

        List<TreeNode> FlattenTreeView(TreeNode topNode)
        {
            var nodes = new List<TreeNode>();
            nodes.Add(topNode);
            foreach (var node in topNode.Nodes)
                nodes.AddRange(FlattenTreeView((TreeNode)node));
            return nodes;
        }

        TreeNode FindTreeNode(TreeNode baseNode, object obj)
        {
            foreach (var node in FlattenTreeView(baseNode))
            {
                var itemHolder = (ItemHolder)node.Tag;
                if (itemHolder != null)
                {
                    if (itemHolder.Object == obj)
                        return node;
                }
            }
            return null;
        }

    }
}
