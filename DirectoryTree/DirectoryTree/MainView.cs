using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DirectoryTree
{
    public partial class MainView : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = CharSet.Auto)]
        extern static bool DestroyIcon(IntPtr handle);

        private string dirPath = "";
        DirectoryTree dirTree;
        Stopwatch stopwatch;

        public MainView()
        {
            InitializeComponent();
            stopwatch = new Stopwatch();

            // setup folder tree
            setupTreeList();
            setupTreeColumns();
        }

        private void setupTreeList()
        {
            this.foldersTLV.CanExpandGetter = delegate (object x) {
                return ((Node)x).HasChildren();
            };

            this.foldersTLV.ChildrenGetter = delegate (object x) {
                return ((Node)x).Children;
            };
        }
        
        private void setupTreeColumns()
        {
            // 'Folder' column
            this.folderTLVCol.AspectGetter = delegate (object rowObject) {
                if (rowObject is Node)
                {
                    return ((Node)rowObject).Name;
                }
                else {
                    return "";
                }
            };

            // 'FileCount' column
            this.fileCountTLVCol.AspectGetter = delegate (object rowObject) {
                Node node = (Node)rowObject;
                return (node.IsDirectory) ? node.FilesOnlyCount.ToString() : "";
            };

            // 'Size' column
            this.sizeTLVCol.AspectGetter = delegate (object rowObject) {
                return ((Node)rowObject).AccSize;
            };

            this.sizeTLVCol.AspectToStringConverter = delegate (object cellValue) {
                long bytesToFormat = (long)cellValue;
                string[] suffixes = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
                if (bytesToFormat == 0)
                {
                    return "0 " + suffixes[0];
                }
                long bytes = Math.Abs(bytesToFormat);
                int dimension = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
                double fileSize = Math.Round(bytes / Math.Pow(1024, dimension), 2);
                return (Math.Sign(bytesToFormat) * fileSize).ToString() + " " + suffixes[dimension];
            };

            // icons
            this.folderTLVCol.ImageGetter = delegate (object rowObject) {
                return getIconFor(((Node)rowObject).FullName);
            };
        }

        private string getIconFor(string path)
        {
            // Set a default icon for the file.
            string ext = "file";
            Image img = Properties.Resources.file16;
            FileAttributes attr = File.GetAttributes(path);
            if (attr.HasFlag(FileAttributes.Directory))
            {
                ext = "folder";
                img = Properties.Resources.folder16;
            }
            // check to see if the image collection contains an image for this extension, using the extension as a key
            if (!this.foldersTLV.SmallImageList.Images.ContainsKey(ext))
            {
                try
                {
                    this.foldersTLV.SmallImageList.Images.Add(ext, img);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            return ext;
        }

        private void onBrowseTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                string selectedPath = (sender as TextBox).Text;
                if (Directory.Exists(selectedPath))
                {
                    this.dirPath = selectedPath;
                    this.toListBtn.Enabled = false;
                }
            }
        }

        private void onBrowseBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDiag = new FolderBrowserDialog();
            if (folderBrowserDiag.ShowDialog() == DialogResult.OK)
            {
                string selectedPath = folderBrowserDiag.SelectedPath;
                if (Directory.Exists(selectedPath))
                {
                    this.dirPath = selectedPath;
                    this.browseTextBox.Text = selectedPath;
                }
            }
        }

        private void onBuildBtn_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(this.dirPath))
            {
                this.dirTree = new DirectoryTree(this.dirPath);
                this.statusLabel.Text = "Building...";
                stopwatch.Start();
                dirTree.Build();
                stopwatch.Stop();
                this.statusLabel.Text = "Build time: " + formatMS(stopwatch.ElapsedMilliseconds);
                this.foldersTLV.Roots = new List<Node>() { this.dirTree.RootNode };
            }
            else
            {
                this.foldersTLV.Roots = null;
            }
            
            this.toListBtn.Enabled = true;

        }

        private void toListBtn_Click(object sender, EventArgs e)
        {
            if (!dirPath.Equals(string.Empty))
            {
                this.statusLabel.Text = "Convertring directory tree to list...";
                stopwatch.Restart();
                dirTree.ToList();
                stopwatch.Stop();
                this.statusLabel.Text = "ToList time: " + formatMS(stopwatch.ElapsedMilliseconds);
            }
        }

        private string formatMS(long milliseconds)
        {
            TimeSpan timeSpan = TimeSpan.FromMilliseconds(milliseconds);
            return string.Format("{0:D2}m {1:D2}s {2:D3}ms",
                timeSpan.Minutes,
                timeSpan.Seconds,
                timeSpan.Milliseconds);
        }

        private void onFolderOLV_RightClick(object sender, BrightIdeasSoftware.CellRightClickEventArgs e)
        {
            // default enability
            this.foldersCMSCreate.Enabled = true;
            this.foldersCMSDelete.Enabled = true;
            this.foldersCMSRename.Enabled = true;

            // only show context menu if any items are selected
            if (foldersTLV.SelectedObjects.Count > 0)
            {
                // show reduced context menu if multiple items are selected
                if (foldersTLV.SelectedObjects.Count > 1)
                {
                    this.foldersCMSCreate.Enabled = false;
                    this.foldersCMSRename.Enabled = false;
                }
                e.MenuStrip = this.foldersCMS;
            }
        }

        private void foldersCMSCreate_Click(object sender, EventArgs e)
        {
            this.statusLabel.Text = "Creating...";
            stopwatch.Start();

            if (foldersTLV.SelectedIndices.Count == 1)
            {
                Node selectedNode = (Node)foldersTLV.SelectedObject;
                string dirPath = selectedNode.FullName;
                FileAttributes attr = File.GetAttributes(dirPath);

                // get parenting directory path to create in
                if (!attr.HasFlag(FileAttributes.Directory))
                {
                    dirPath = selectedNode.Parent.FullName;
                }

                // TODO: Implement (dialog: create file or folder with name @ dirPath)
                // TODO: Implement (create new node in directory tree)
            }

            stopwatch.Stop();
            this.statusLabel.Text = "Create time: " + formatMS(stopwatch.ElapsedMilliseconds);

            updateFoldersView();
        }

        private void foldersCMSDelete_Click(object sender, EventArgs e)
        {
            this.statusLabel.Text = "Deleting...";
            stopwatch.Start();

            if (foldersTLV.SelectedIndices.Count > 0)
            {
                // convert to list of paths
                List<string> paths = new List<string>(foldersTLV.SelectedObjects.Count);
                foreach (var selectedObj in foldersTLV.SelectedObjects)
                {
                    paths.Add(((Node)selectedObj).HalfName);
                }
                dirTree.Remove(paths, true);
            }

            stopwatch.Stop();
            this.statusLabel.Text = "Delete time: " + formatMS(stopwatch.ElapsedMilliseconds);

            updateFoldersView();
        }

        private void foldersCMSRename_Click(object sender, EventArgs e)
        {
            this.statusLabel.Text = "Renaming...";
            stopwatch.Start();

            if (foldersTLV.SelectedIndices.Count == 1)
            {
                Node selectedNode = (Node)foldersTLV.SelectedObject;
                string nodeName = selectedNode.Name;

                // TODO: Implement (dialog: enter new name for nodeName)
                // TODO: Implement (change node in directory tree)
            }

            stopwatch.Stop();
            this.statusLabel.Text = "Rename time: " + formatMS(stopwatch.ElapsedMilliseconds);

            updateFoldersView();
        }

        private void updateFoldersView()
        {
            foreach (var root in this.foldersTLV.Roots)
            {
                this.foldersTLV.RefreshObject(root);
            }

            // avoid random selection
            this.foldersTLV.SelectedObject = null;
        }
    }
}
