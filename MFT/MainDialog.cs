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
    public partial class MainDialog : Form
    {
        public MainDialog()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            workspace = new Workspace();
            InitTreeView();
            ResetSpectrometer();
        }

        Workspace workspace { get; set; }

        TreeNode root;
        TreeNode camerasNode;
        TreeNode spectrometerNode;
        TreeNode darkNode;
        const string noSpectrometerMessage = "No spectrometer connected";
        TreeNode exposureSetttingsNode;
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
            foreach (var camera in CameraCollection.GetCameras())
            {
                var t = new ToolStripMenuItem();
                t.Tag = camera;
                t.Text = "Connect " + camera.Name;
                camerasContextMenuStrip.Items.Add(t);
            }

            // spectrometer
            var spectrometerTitleNode = root.Nodes.Add("Spectrometer");
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
            spectrometerNode = spectrometerTitleNode.Nodes.Add(noSpectrometerMessage);

            // exposure settings
            exposureSetttingsNode = root.Nodes.Add("Exposure Settings");

            // spectrum processor chains
            spectrumProcessorChainsNode = root.Nodes.Add("Spectrum Processor Chains");

            // tests
            testsNode = root.Nodes.Add("Tests");

            root.Expand();
        }

        void UpdateFormFromWorkspace()
        {
            InitTreeView();

            // spectrometer
            if (workspace.Spectrometer != null)
            {
                spectrometerNode.Tag = workspace.Spectrometer;
                spectrometerNode.Text = workspace.Spectrometer.GetDeviceDescription();
            }
        }

        void UpdateWorkspaceFromForm()
        {
            workspace.Clear();

            // spectrometer
            if (spectrometerNode.Tag != null)
                workspace.Spectrometer = (ISpectrometer)spectrometerNode.Tag;
        }

        private TabPage DarkSpectrum { get; set; }
        private TabPage WhiteSpectrum { get; set; }

        void ResetSpectrometer()
        {
        //    spectrometerNode.Tag = null;
        //    spectrometerNode.Text = noSpectrometerMessage;
        //    DisallowNormalized();
        //    if (DarkSpectrum != null)
        //        tabControl1.TabPages.Remove(DarkSpectrum);
        //    if (WhiteSpectrum != null)
        //        tabControl1.TabPages.Remove(WhiteSpectrum);
        //    SpectrometerChanged?.Invoke(this, new SpectrometerChangedEventArgs()); // null spectrometer
        }

        public event EventHandler<SpectrometerChangedEventArgs> SpectrometerChanged;

        TabPage AddSingleSpectrumTab(Exposure exposure, bool forbidNormalizing = false, string tabName = "")
        {
          
            return new TabPage();
        }

        TabPage AddTabpage(string name, Control control)
        {
            var tabPage = new TabPage(name);
            tabPage.Controls.Add(control);
            tabControl1.TabPages.Add(tabPage);
            tabControl1.SelectedTab = tabPage;
            return tabPage;
        }

        private void darkRefButton_Click(object sender, EventArgs e)
        {
            //if (DarkSpectrum != null)
            //    tabControl1.TabPages.Remove(DarkSpectrum);
            //if (spectrometer == null)
            //{
            //    MessageBox.Show(this, $"Problem collecting spectrum: Spectrometer not connected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //if (!spectrometer.CollectDarkReferenceExposure((float)integrationTimeMsNumericUpDown.Value / 1000, (int)averagingNumericUpDown.Value, out string errMsg))
            //    MessageBox.Show(this, $"Problem collecting spectrum: {errMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //DarkSpectrum = AddSingleSpectrumTab(spectrometer.DarkReference, forbidNormalizing: true, "Dark");
        }

        private void whiteRefButton_Click(object sender, EventArgs e)
        {
            //if (WhiteSpectrum != null)
            //    tabControl1.TabPages.Remove(WhiteSpectrum);
            //if (spectrometer == null)
            //{
            //    MessageBox.Show(this, $"Problem collecting spectrum: Spectrometer not connected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //if (!spectrometer.CollectWhiteReferenceExposure((float)integrationTimeMsNumericUpDown.Value / 1000, (int)averagingNumericUpDown.Value, out string errMsg))
            //    MessageBox.Show(this, $"Problem collecting spectrum: {errMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //WhiteSpectrum = AddSingleSpectrumTab(spectrometer.WhiteReference, forbidNormalizing: true, "White");
        }

        string GetNextContinuousName()
        {
            return "";
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var d = new AboutDialog();
            d.ShowDialog();
        }

        private void spectrometerContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // assumes item clicked is a connect instruction
            ResetSpectrometer();
            ISpectrometer spectrometer;
            var selected = (SpectrometerSelectionView)e.ClickedItem.Tag;
            try
            {
                spectrometer = SpectrometerFactory.GetSpectrometer(selected.Type);
            }
            catch (NotImplementedException)
            {
                MessageBox.Show(this, $"Internal error: unknown device", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string ErrMsg;
            if (!spectrometer.Connect(out ErrMsg))
                MessageBox.Show(this, $"Problem connecting: {ErrMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                var tabPage = new TabPage(spectrometer.GetDeviceDescription());
                var control = new SpectrometerControl(spectrometer);
                control.SpectrometerChanged += UpdateSpectrometerNode;
                control.Dock = DockStyle.Fill;
                tabPage.Controls.Add(control);
                tabControl1.TabPages.Add(tabPage);
                tabControl1.SelectedIndex = tabControl1.TabCount - 1;
                var item = new ItemHolder(ItemHolder.ItemTypes.SPECTROMETER, tabPage, spectrometer);
                spectrometerNode.Tag = item;
                spectrometerNode.Text = spectrometer.GetDeviceDescription();
            }
            spectrometerNode.EnsureVisible();
        }

        void UpdateSpectrometerNode(object sender, SpectrometerChangedEventArgs e)
        {
            if (spectrometerNode == null)
            {
                // create a new node from e
            }
            spectrometerNode.Nodes.Clear();
            var item = spectrometerNode.Tag as ItemHolder;
            if (item == null)
                return;
            var spectrometer = item.Object as ISpectrometer;
            if (spectrometer != e.Spectrometer)
                MessageBox.Show(this, "Internal Error", "Spectrometer mismatch", MessageBoxButtons.OK, MessageBoxIcon.Error);
            spectrometerNode.Text = spectrometer.GetDeviceDescription();
            if (spectrometer.DarkReference != null)
            {
                darkNode = new TreeNode("Dark Reference");
                var itm = new ItemHolder(ItemHolder.ItemTypes.EXPOSURE, null, spectrometer.DarkReference);
                darkNode.Tag = itm;
                spectrometerNode.Nodes.Add(darkNode);
            }
        }


        private void camerasContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var camera = (ICamera)e.ClickedItem.Tag;
            string name = camera.Name;

            // todo: figure out how to synchronize AForge camera object with physical camera
            // so we can tell if physical camera is running
            if (camera.IsRunning)
            {
                MessageBox.Show(this, $"'{name}' appears to be running in another application.",
                    "Problem adding camera", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            foreach (var node in camerasNode.Nodes)
            {
                var treeNode = (TreeNode)node;
                var itemHolder = treeNode.Tag as ItemHolder;
                var existingCamera = itemHolder.Object as ICamera;
                if (existingCamera == camera)
                    return;
            }


            var camNode = new TreeNode();

            var camDialog = new CameraControl();
            camDialog.SetCamera(camera);
            var tabpage = AddTabpage(name, camDialog);

            camNode.Tag = new ItemHolder(ItemHolder.ItemTypes.CAMERA, tabpage, camera);
            camNode.Text = name;
            camerasNode.Nodes.Add(camNode);
            camNode.EnsureVisible();
        }

        private void workspaceTreeView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var tree = (TreeView)sender;
                var node = tree?.GetNodeAt(new Point(e.X, e.Y));
                if (node == null)
                    return;

                var itemHolder = (ItemHolder)node.Tag;
                switch (itemHolder.Type)
                {
                    case ItemHolder.ItemTypes.CAMERA:
                    case ItemHolder.ItemTypes.SPECTROMETER:
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
