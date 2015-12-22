namespace DirectoryTree
{
    partial class MainView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.browseBtn = new System.Windows.Forms.Button();
            this.browseTextBox = new System.Windows.Forms.TextBox();
            this.browseGB = new System.Windows.Forms.GroupBox();
            this.toListBtn = new System.Windows.Forms.Button();
            this.buildBtn = new System.Windows.Forms.Button();
            this.treeOLV = new System.Windows.Forms.GroupBox();
            this.foldersTLV = new BrightIdeasSoftware.TreeListView();
            this.folderOLVCol = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.filesOLVCol = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.sizeOLVCol = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.foldersCMS = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.foldersCMSCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.foldersCMSDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.foldersCMSRename = new System.Windows.Forms.ToolStripMenuItem();
            this.browseGB.SuspendLayout();
            this.treeOLV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.foldersTLV)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.foldersCMS.SuspendLayout();
            this.SuspendLayout();
            // 
            // browseBtn
            // 
            this.browseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.browseBtn.Location = new System.Drawing.Point(524, 26);
            this.browseBtn.Name = "browseBtn";
            this.browseBtn.Size = new System.Drawing.Size(30, 22);
            this.browseBtn.TabIndex = 1;
            this.browseBtn.Text = "...";
            this.browseBtn.UseVisualStyleBackColor = true;
            this.browseBtn.Click += new System.EventHandler(this.onBrowseBtn_Click);
            // 
            // browseTextBox
            // 
            this.browseTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.browseTextBox.Location = new System.Drawing.Point(6, 27);
            this.browseTextBox.Name = "browseTextBox";
            this.browseTextBox.Size = new System.Drawing.Size(512, 20);
            this.browseTextBox.TabIndex = 2;
            this.browseTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.onBrowseTB_KeyPress);
            // 
            // browseGB
            // 
            this.browseGB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.browseGB.Controls.Add(this.toListBtn);
            this.browseGB.Controls.Add(this.buildBtn);
            this.browseGB.Controls.Add(this.browseTextBox);
            this.browseGB.Controls.Add(this.browseBtn);
            this.browseGB.Location = new System.Drawing.Point(12, 12);
            this.browseGB.Name = "browseGB";
            this.browseGB.Size = new System.Drawing.Size(560, 93);
            this.browseGB.TabIndex = 3;
            this.browseGB.TabStop = false;
            this.browseGB.Text = "Choose Directory";
            // 
            // toListBtn
            // 
            this.toListBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.toListBtn.Enabled = false;
            this.toListBtn.Location = new System.Drawing.Point(292, 58);
            this.toListBtn.Name = "toListBtn";
            this.toListBtn.Size = new System.Drawing.Size(120, 22);
            this.toListBtn.TabIndex = 4;
            this.toListBtn.Text = "To List";
            this.toListBtn.UseVisualStyleBackColor = true;
            this.toListBtn.Click += new System.EventHandler(this.toListBtn_Click);
            // 
            // buildBtn
            // 
            this.buildBtn.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buildBtn.Location = new System.Drawing.Point(149, 58);
            this.buildBtn.Name = "buildBtn";
            this.buildBtn.Size = new System.Drawing.Size(120, 22);
            this.buildBtn.TabIndex = 3;
            this.buildBtn.Text = "Build";
            this.buildBtn.UseVisualStyleBackColor = true;
            this.buildBtn.Click += new System.EventHandler(this.onBuildBtn_Click);
            // 
            // treeOLV
            // 
            this.treeOLV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeOLV.Controls.Add(this.foldersTLV);
            this.treeOLV.Location = new System.Drawing.Point(12, 111);
            this.treeOLV.Name = "treeOLV";
            this.treeOLV.Size = new System.Drawing.Size(560, 305);
            this.treeOLV.TabIndex = 5;
            this.treeOLV.TabStop = false;
            this.treeOLV.Text = "Directory Tree";
            // 
            // foldersTLV
            // 
            this.foldersTLV.AllColumns.Add(this.folderOLVCol);
            this.foldersTLV.AllColumns.Add(this.filesOLVCol);
            this.foldersTLV.AllColumns.Add(this.sizeOLVCol);
            this.foldersTLV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.foldersTLV.CellEditUseWholeCell = false;
            this.foldersTLV.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.folderOLVCol,
            this.filesOLVCol,
            this.sizeOLVCol});
            this.foldersTLV.Cursor = System.Windows.Forms.Cursors.Default;
            this.foldersTLV.FullRowSelect = true;
            this.foldersTLV.HighlightBackgroundColor = System.Drawing.Color.Empty;
            this.foldersTLV.HighlightForegroundColor = System.Drawing.Color.Empty;
            this.foldersTLV.Location = new System.Drawing.Point(6, 19);
            this.foldersTLV.Name = "foldersTLV";
            this.foldersTLV.ShowGroups = false;
            this.foldersTLV.Size = new System.Drawing.Size(548, 280);
            this.foldersTLV.TabIndex = 0;
            this.foldersTLV.UseCompatibleStateImageBehavior = false;
            this.foldersTLV.View = System.Windows.Forms.View.Details;
            this.foldersTLV.VirtualMode = true;
            this.foldersTLV.CellRightClick += new System.EventHandler<BrightIdeasSoftware.CellRightClickEventArgs>(this.onFolderOLV_RightClick);
            // 
            // folderOLVCol
            // 
            this.folderOLVCol.AspectName = "";
            this.folderOLVCol.Groupable = false;
            this.folderOLVCol.Hideable = false;
            this.folderOLVCol.Text = "Folder";
            this.folderOLVCol.Width = 422;
            // 
            // filesOLVCol
            // 
            this.filesOLVCol.AspectName = "";
            this.filesOLVCol.Groupable = false;
            this.filesOLVCol.Text = "Files";
            // 
            // sizeOLVCol
            // 
            this.sizeOLVCol.AspectName = "";
            this.sizeOLVCol.FillsFreeSpace = true;
            this.sizeOLVCol.Groupable = false;
            this.sizeOLVCol.Text = "Size";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 419);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(584, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(105, 17);
            this.statusLabel.Text = "Waiting for build...";
            // 
            // foldersCMS
            // 
            this.foldersCMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.foldersCMSCreate,
            this.foldersCMSDelete,
            this.foldersCMSRename});
            this.foldersCMS.Name = "foldersCMS";
            this.foldersCMS.Size = new System.Drawing.Size(118, 70);
            // 
            // foldersCMSCreate
            // 
            this.foldersCMSCreate.Name = "foldersCMSCreate";
            this.foldersCMSCreate.Size = new System.Drawing.Size(117, 22);
            this.foldersCMSCreate.Text = "Create";
            this.foldersCMSCreate.Click += new System.EventHandler(this.foldersCMSCreate_Click);
            // 
            // foldersCMSDelete
            // 
            this.foldersCMSDelete.Name = "foldersCMSDelete";
            this.foldersCMSDelete.Size = new System.Drawing.Size(117, 22);
            this.foldersCMSDelete.Text = "Delete";
            this.foldersCMSDelete.Click += new System.EventHandler(this.foldersCMSDelete_Click);
            // 
            // foldersCMSRename
            // 
            this.foldersCMSRename.Name = "foldersCMSRename";
            this.foldersCMSRename.Size = new System.Drawing.Size(117, 22);
            this.foldersCMSRename.Text = "Rename";
            this.foldersCMSRename.Click += new System.EventHandler(this.foldersCMSRename_Click);
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 441);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.treeOLV);
            this.Controls.Add(this.browseGB);
            this.Name = "MainView";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Directory Tree";
            this.browseGB.ResumeLayout(false);
            this.browseGB.PerformLayout();
            this.treeOLV.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.foldersTLV)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.foldersCMS.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button browseBtn;
        private System.Windows.Forms.TextBox browseTextBox;
        private System.Windows.Forms.GroupBox browseGB;
        private System.Windows.Forms.GroupBox treeOLV;
        private BrightIdeasSoftware.TreeListView foldersTLV;
        private System.Windows.Forms.Button buildBtn;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private BrightIdeasSoftware.OLVColumn folderOLVCol;
        private BrightIdeasSoftware.OLVColumn filesOLVCol;
        private BrightIdeasSoftware.OLVColumn sizeOLVCol;
        private System.Windows.Forms.ContextMenuStrip foldersCMS;
        private System.Windows.Forms.ToolStripMenuItem foldersCMSCreate;
        private System.Windows.Forms.ToolStripMenuItem foldersCMSDelete;
        private System.Windows.Forms.ToolStripMenuItem foldersCMSRename;
        private System.Windows.Forms.Button toListBtn;
    }
}

