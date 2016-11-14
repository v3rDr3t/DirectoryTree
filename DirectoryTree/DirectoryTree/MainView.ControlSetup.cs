using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryTree {
    public partial class MainView {

        private void setupTreeList() {
            this.foldersTLV.CanExpandGetter = delegate (object x) {
                return ((Node)x).HasChildren();
            };

            this.foldersTLV.ChildrenGetter = delegate (object x) {
                return ((Node)x).Children;
            };
        }

        private void setupTreeColumns() {
            // 'Folder' column
            this.folderTLVCol.AspectGetter = delegate (object rowObject) {
                if (rowObject is Node) {
                    return ((Node)rowObject).Name;
                } else {
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
                if (bytesToFormat == 0) {
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

        private string getIconFor(string path) {
            string ext = "directory";
            Bitmap img;

            // check for file/folder
            FileAttributes attr = File.GetAttributes(path);
            if (attr.HasFlag(FileAttributes.Directory)) {
                img = (defaultIcons)
                    ? Properties.Resources.folder16
                    : img = IconTools.GetFolderIcon(ShellIconSize.SmallIcon).ToBitmap();
            } else {
                if (defaultIcons) {
                    ext = "file";
                    img = Properties.Resources.file16;
                } else {
                    ext = Path.GetExtension(path);
                    img = IconTools.GetIconForExtension(ext, ShellIconSize.SmallIcon).ToBitmap();
                }
            }
            
            // check to see if the image collection contains an image for this extension,
            // using the extension as a key
            if (!this.foldersTLV.SmallImageList.Images.ContainsKey(ext)) {
                try {
                    this.foldersTLV.SmallImageList.Images.Add(ext, img);
                } catch (Exception e) {
                    Console.WriteLine(e.ToString());
                }
            }
            return ext;
        }
    }
}
