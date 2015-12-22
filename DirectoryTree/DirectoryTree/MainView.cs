using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DirectoryTree
{
    public partial class MainView : Form
    {
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
            folderOLVCol.AspectGetter = delegate (object rowObject) {
                if (rowObject is Node)
                {
                    return ((Node)rowObject).Name;
                }
                else {
                    return "";
                }
            };

            // icons
            this.folderOLVCol.ImageGetter = delegate (object rowObject) {
                return getIconFor(((Node)rowObject).FullName);
            };
        }

        private string getIconFor(string path)
        {
            // Set a default icon for the file.
            Icon iconForFile = SystemIcons.WinLogo;
            FileInfo file = new FileInfo(path);
            string ext = Path.GetExtension(path);

            // Check to see if the image collection contains an image
            // for this extension, using the extension as a key.
            if (!this.foldersTLV.SmallImageList.Images.ContainsKey(ext))
            {
                // If not, add the image to the image list.
                //try
                //{
                //    // TODO: Get Icon
                //    this.foldersTLV.SmallImageList.Images.Add(ext, iconForFile);
                //}
                //catch (Exception)
                //{
                //    Console.WriteLine("Error for: " + path);

                //}
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
            this.dirTree = new DirectoryTree(this.dirPath);
            this.statusLabel.Text = "Building...";
            stopwatch.Start();
            dirTree.Build();
            stopwatch.Stop();
            this.statusLabel.Text = "Build time: " + formatMS(stopwatch.ElapsedMilliseconds);
            
            this.foldersTLV.Roots = new List<Node>() { this.dirTree.RootNode };
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
            if (!foldersTLV.VirtualMode)
            {
                if (foldersTLV.SelectedItems.Count > 0)
                {
                    this.foldersCMSDelete.Enabled = true;
                    this.foldersCMSRename.Enabled = true;
                }
                else
                {
                    this.foldersCMSDelete.Enabled = false;
                    this.foldersCMSRename.Enabled = false;
                }
                e.MenuStrip = this.foldersCMS;
            }
        }

        private void foldersCMSCreate_Click(object sender, EventArgs e)
        {
            // TODO: Implement
        }

        private void foldersCMSDelete_Click(object sender, EventArgs e)
        {
            // TODO: Implement
        }

        private void foldersCMSRename_Click(object sender, EventArgs e)
        {
            // TODO: Implement
        }

        private void filesCMSCreate_Click(object sender, EventArgs e)
        {
            // TODO: Implement
        }

        private void filesCMSDelete_Click(object sender, EventArgs e)
        {
            // TODO: Implement
        }

        private void filesCMSRename_Click(object sender, EventArgs e)
        {
            // TODO: Implement
        }
    }
}
