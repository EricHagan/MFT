﻿using System;
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
                long handle = settings.Handle;
                var t = FindTreeNode(treeView.TopNode, handle);
                if (t != null)
                    throw new Exception($"Internal error. There's already an ExposureSettings object with handle {handle} in the tree.");
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
                long handle = settings.Handle;
                var t = FindTreeNode(treeView.TopNode, handle);
                if (t != null)
                {
                    var h = t.Tag as ItemHolder;
                    var s = h.Object as ExposureSettings;
                    if (s.Handle == handle)
                    {
                        h.Object = settings;
                        t.Text = settings.ToString();
                    }
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
                long handle = settings.Handle;
                bool success = false;
                foreach (var o in exposureSettingsNode.Nodes)
                {
                    var t = o as TreeNode;
                    var h = t.Tag as ItemHolder;
                    var s = h.Object as ExposureSettings;
                    t.NodeFont = new Font(treeView.Font, FontStyle.Regular);
                    if (s.Handle == handle)
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
                TreeNode settingsNode;
                // if it's in the tree, update it:
                if (spectrometerNode != null)
                {
                    var h = spectrometerNode.Tag as ItemHolder;
                    spectrometerNode.Text = spectrometer.GetDeviceDescription();
                    h.Object = spectrometer;
                    settingsNode = spectrometerNode.Nodes[0];
                }
                // if not, add it to the tree:
                else
                {
                    if (spectrometerNode == null)
                    {
                        spectrometerNode = new TreeNode();
                        spectrometerTitleNode.Nodes.Add(spectrometerNode);
                    }

                    var control = new SpectrometerControl(spectrometer);
                    control.Dock = DockStyle.Fill;
                    var item = new ItemHolder(ItemHolder.ItemTypes.SPECTROMETER, spectrometer);

                    spectrometerNode.Tag = item;
                    spectrometerNode.Text = spectrometer.GetDeviceDescription();

                    settingsNode = new TreeNode();
                    spectrometerNode.Nodes.Add(settingsNode);
                    var settingsItem = new ItemHolder(ItemHolder.ItemTypes.EXPOSURE_SETTINGS, spectrometer.Settings);
                    settingsNode.Tag = settingsItem;
                    settingsNode.Text = spectrometer.Settings.ToString();
                }
                settingsNode.EnsureVisible();
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

        private void treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // make sure that we select the node we right-click, so context menus know
            // what node opened them
            if (e.Button == MouseButtons.Right)
                treeView.SelectedNode = e.Node;
        }

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

        TreeNode FindTreeNode(TreeNode baseNode, long handle)
        {
            foreach (var node in FlattenTreeView(baseNode))
            {
                var itemHolder = (ItemHolder)node.Tag;
                if (itemHolder != null)
                {
                    var o = itemHolder.Object as IWorkspaceItem;
                    if (o != null)
                    {
                        if (o.Handle == handle)
                            return node;
                    }
                }
            }
            return null;
        }
    }
}