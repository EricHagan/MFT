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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            averagingNumericUpDown.Value = 10;
            integrationTimeMsNumericUpDown.Value = 50;
            dwellTimeNumericUpDown.Value = 100;


            workspace = new Workspace();
            InitTreeView();
            ResetSpectrometer();
        }

        Workspace workspace { get; set; }

        TreeNode root;
        TreeNode camerasNode;
        TreeNode spectrometerNode;
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

        public event EventHandler<ControlsAdjustedEventArgs> ControlsAdjusted;

        private TabPage DarkSpectrum { get; set; }
        private TabPage WhiteSpectrum { get; set; }

        void ResetSpectrometer()
        {
            spectrometerNode.Tag = null;
            spectrometerNode.Text = noSpectrometerMessage;
            DisallowNormalized();
            if (DarkSpectrum != null)
                tabControl1.TabPages.Remove(DarkSpectrum);
            if (WhiteSpectrum != null)
                tabControl1.TabPages.Remove(WhiteSpectrum);
            SpectrometerChanged?.Invoke(this, new SpectrometerChangedEventArgs()); // null spectrometer
        }

        public event EventHandler<SpectrometerChangedEventArgs> SpectrometerChanged;

        private void singleSpectrumButton_Click(object sender, EventArgs e)
        {
            //var exposure = Exposure.GetExposure(spectrometer, (float)integrationTimeMsNumericUpDown.Value / 1000,
            //    (int)averagingNumericUpDown.Value, normalizedCheckBox.Checked, out string errMsg);
            //if (exposure != null)
            //{
            //    AddSingleSpectrumTab(exposure);
            //}
            //else
            //    MessageBox.Show(this, $"Problem collecting spectrum: {errMsg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        TabPage AddSingleSpectrumTab(Exposure exposure, bool forbidNormalizing = false, string tabName = "")
        {
            //if (tabName == "")
            //    tabName = exposure.Name;
            //var singleGraph = new SingleSpectrumGraph();
            //singleGraph.Exposure = exposure;
            //singleGraph.ExposureSettings.Spectrometer = spectrometer;
            //singleGraph.ExposureSettings.ExposureResampled += singleGraph.ExposureResampledHandler;
            //singleGraph.ExposureSettings.IntegrationTimeMs = (int)integrationTimeMsNumericUpDown.Value;
            //singleGraph.ExposureSettings.Averaging = (int)averagingNumericUpDown.Value;
            //singleGraph.ExposureSettings.ForbidNormalizing = forbidNormalizing;
            //singleGraph.ExposureSettings.AllowNormalize = normalizedCheckBox.Enabled;
            //singleGraph.ExposureSettings.Normalize = normalizedCheckBox.Checked;
            //if (!forbidNormalizing)
            //    spectrometer.NormalizeAllowedChanged += singleGraph.ExposureSettings.HandleNormalizeAllowedChanged;
            //SpectrometerChanged += singleGraph.ExposureSettings.HandleSpectrometerChanged;
            //var tabPage = new TabPage(tabName);
            //tabPage.Controls.Add(singleGraph);
            //tabControl1.TabPages.Add(tabPage);
            //tabControl1.SelectedTab = tabPage;
            //return tabPage;
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

        void HandleAllowNormalizedChanged(object sender, NormalizeAllowedChangedEventArgs e)
        {
            if (e.NormalizeAllowed)
                AllowNormalized();
            else
                DisallowNormalized();
        }

        void AllowNormalized()
        {
            normalizedCheckBox.Enabled = true;
        }

        void DisallowNormalized()
        {
            normalizedCheckBox.Checked = false;
            normalizedCheckBox.Enabled = false;
        }

        string GetNextContinuousName()
        {
            return "";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //var exposureStream = new ExposureStream(spectrometer);
            //ControlsAdjusted += exposureStream.ControlsAdjustedEventHandler;
            //var graph = new ContinuousSpectrumGraph();
            //graph.ExposureStream = exposureStream;

            //var tabPage = new TabPage("Continuous");
            //tabPage.Controls.Add(graph);
            //tabControl1.TabPages.Add(tabPage);
            //tabControl1.SelectedTab = tabPage;

            //exposureStream.Start();
        }

        private void averagingNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            RaiseControlsAdjustedEvent(averagingNumericUpDown);
        }

        private void integrationTimeMsNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            RaiseControlsAdjustedEvent(integrationTimeMsNumericUpDown);
        }

        private void dwellTimeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            RaiseControlsAdjustedEvent(dwellTimeNumericUpDown);
        }

        void RaiseControlsAdjustedEvent(object sender)
        {
            if (ControlsAdjusted == null)
                return;
            ControlsAdjusted(sender, new ControlsAdjustedEventArgs()
            {
                Averaging = (int)averagingNumericUpDown.Value,
                DwellTimeMs = (int)dwellTimeNumericUpDown.Value,
                IntegrationTimeS = (float)(integrationTimeMsNumericUpDown.Value / 1000),
            });
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
                spectrometerNode.Tag = spectrometer;
                spectrometerNode.Text = spectrometer.GetDeviceDescription();
                spectrometer.NormalizeAllowedChanged += HandleAllowNormalizedChanged;
                SpectrometerChanged?.Invoke(this, new SpectrometerChangedEventArgs() { Spectrometer = spectrometer });
            }
            spectrometerNode.EnsureVisible();
        }

        private void camerasContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var camera = (ICamera)e.ClickedItem.Tag;
            string name = camera.Name;
            var camNode = new TreeNode();

            var camDialog = new CameraDialog();
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
                        //camera stuff
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

    public class ControlsAdjustedEventArgs : EventArgs
    {
        public float IntegrationTimeS { get; set; }
        public int Averaging { get; set; }
        public int DwellTimeMs { get; set; }
    }

    public class SpectrometerChangedEventArgs : EventArgs
    {
        public ISpectrometer Spectrometer { get; set; }
    }
}
