using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryTree
{
    internal class DirectoryTree
    {
        private string dirPath;
        private Node rootNode;

        /// <summary>
        /// Creates an instance of type <see cref="DirectoryTree"/> for the given directory.
        /// </summary>
        /// <param name="dirPath">The given directory path.</param>
        public DirectoryTree(string dirPath)
        {
            this.dirPath = dirPath;
            this.rootNode = new Node(dirPath, true, null);
        }

        /// <summary>
        /// Builds the directory tree by recursively traversing the set directory path.
        /// </summary>
        internal void Build()
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(this.dirPath);
                build(dirInfo, ref this.rootNode);
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("ERROR: Directory \"" + this.dirPath + "\" not found!");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("ERROR: Insuffiecient access rights for \"" + this.dirPath + "\"!");
            }
            catch (Exception exception)
            {
                Console.WriteLine("ERROR while processing directory \"" + this.dirPath + "\": \""
                    + exception.Message + "\"!");
            }

            updateTreeInfo();
        }

        /// <summary>
        /// Helper method to recursively build the (sub)tree from the given directory.
        /// </summary>
        /// <param name="dirInfo">The given directory information.</param>
        /// <param name="node">The parent node.</param>
        private void build(DirectoryInfo dirInfo, ref Node node)
        {
            try
            {
                foreach (FileSystemInfo info in dirInfo.EnumerateFileSystemInfos())
                {
                    FileInfo fileInfo = info as FileInfo;
                    if (fileInfo != null)
                    {
                        Node fileNode = new Node(fileInfo.FullName, false, this.rootNode);
                        fileNode.Size = fileInfo.Length;
                        node.AddChild(fileNode);
                        continue;
                    }

                    DirectoryInfo subDirInfo = info as DirectoryInfo;
                    if (dirInfo != null)
                    {
                        Node dirNode = new Node(subDirInfo.FullName, true, this.rootNode);
                        node.AddChild(dirNode);
                        build(subDirInfo, ref dirNode);
                        continue;
                    }
                }
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("ERROR: Directory \"" + dirInfo + "\" not found!");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("ERROR: Insuffiecient access rights for \"" + dirInfo + "\"!");
            }
            catch (Exception exception)
            {
                Console.WriteLine("ERROR while processing directory \"" + this.dirPath + "\": \""
                    + exception.Message + "\"!");
            }
        }

        internal void updateTreeInfo() 
        {
            setAccumulatedSizes(this.rootNode);
            setFilesOnlyCount(this.rootNode);
        }

        private long setAccumulatedSizes(Node node)
        {
            // this node
            long accSize = node.Size;

            // check child nodes
            if (node.IsDirectory && node.HasChildren())
            {
                foreach (Node child in node.Children)
                {
                    accSize += setAccumulatedSizes(child);
                }
            }

            node.AccSize = accSize;
            return accSize;
        }
        
        private int setFilesOnlyCount(Node node)
        {
            // this node
            int count = 0;

            // check child nodes
            if (node.IsDirectory && node.HasChildren())
            {
                foreach (Node child in node.Children)
                {
                    count += setFilesOnlyCount(child);
                }
            }
            else
            {
                count++;
            }

            node.FilesOnlyCount = count;
            return count;
        }

        /// <summary>
        /// Removes the given list of file or folder paths from the directory tree.
        /// </summary>
        /// <param name="paths">The given list of paths to delete.</param>
        /// <param name="byHalfName">Depicts if the given paths are in half name format.</param>
        public void Remove(List<string> paths, bool byHalfName)
        {
            foreach (string path in paths)
            {
                Node node = find(path, this.rootNode, byHalfName);
                if (node != null)
                {
                    Node parent = node.Parent;
                    Node childToRemove = null;
                    if (byHalfName)
                    {
                        childToRemove = parent.Children.SingleOrDefault(item => item.HalfName.Equals(path));
                    }
                    else
                    {
                        childToRemove = parent.Children.SingleOrDefault(item => item.FullName.Equals(path));
                    }
                    if (childToRemove != null)
                    {
                        parent.Children.Remove(childToRemove);
                    }
                }
            }

            updateTreeInfo();
        }

        /// <summary>
        /// Helper method to recursively find the file or directory node with the given path.
        /// </summary>
        /// <param name="pathToFind">The given path.</param>
        /// <param name="node">The root node to start from.</param>
        /// <param name="byHalfName">Depicts if the given path is in half name format.</param>
        /// <returns>The node to be found or <code>null</code> otherwise.</returns>
        private Node find(string path, Node node, bool byHalfName)
        {
            // check this node
            string name = (byHalfName) ? node.HalfName : node.FullName;
            if (name.Equals(path))
            {
                return node;
            }

            // check child nodes
            if (node.IsDirectory && node.HasChildren())
            {
                foreach (Node child in node.Children)
                {
                    Node result = find(path, child, byHalfName);
                    if (result != null) return result;
                }
            }

            return null;
        }

        /// <summary>
        /// Creates a sorted list from the directory tree.
        /// </summary>
        /// <returns>The list of files (and folders).</returns>
        internal IEnumerable<string> ToList()
        {
            List<string> treeList = new List<string>();
            treeList.AddRange(recursiveToList(rootNode, ""));
            return treeList;
        }

        /// <summary>
        /// Helper method to recursively traverse the given node for creating a list.
        /// </summary>
        /// <param name="node">The given node.</param>
        /// <param name="pathSuffix">The suffix path.</param>
        /// <returns>The (sub)list for the given node.</returns>
        private List<string> recursiveToList(Node node, string pathSuffix)
        {
            List<string> treeList = new List<string>();
            string curFullPath = pathSuffix + node.Name;

            // add child nodes
            if (node.IsDirectory)
            {
                if (node.HasChildren())
                {
                    curFullPath += @"\";
                    foreach (Node childNode in node.Children)
                    {
                        treeList.AddRange(recursiveToList(childNode, curFullPath));
                    }
                }
                else
                {
                    // add empty folder node
                    treeList.Add(curFullPath);
                }
            }
            else
            {
                // add file node
                treeList.Add(curFullPath);
            }

            return treeList;
        }

        #region Field Accessors
        internal string DirPath
        {
            get { return this.dirPath; }
        }

        internal Node RootNode
        {
            get { return this.rootNode; }
        }
        #endregion
    }

    internal class Node : IComparable
    {
        private string name;
        private string halfName;
        private string fullName;
        private Node parent;
        private bool isDirectory;
        private int filesOnlyCount;
        private long size;
        private long accSize;
        private SortedSet<Node> children;

        internal Node(string fullName, bool isDirectory, Node root)
        {
            this.fullName = fullName;
            this.name = Path.GetFileName(fullName);
            this.halfName = getHalfName(fullName, root);
            this.parent = null;
            this.isDirectory = isDirectory;
            this.filesOnlyCount = 0;
            this.size = 0;
            this.accSize = 0;
            this.children = new SortedSet<Node>();
        }

        private string getHalfName(string fullName, Node root)
        {
            if (root != null)
            {
                try
                {
                    return fullName.Substring(fullName.IndexOf(root.Name));
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Error occured while creating half name of \"" + fullName + "\".");
                }
            }
            return this.name;
        }

        internal bool HasChildren()
        {
            return this.children.Any();
        }

        internal void AddChild(Node node)
        {
            if (this.isDirectory)
            {
                this.children.Add(node);
                node.Parent = this;
            }
        }

        public int CompareTo(object obj)
        {
            return CompareTo(obj as Node);
        }

        public int CompareTo(Node node)
        {
            if (node.IsDirectory)
            {
                return (this.isDirectory) ? string.Compare(this.name, node.Name) : 1;
            }
            else
            {
                return (this.isDirectory) ? -1 : string.Compare(this.name, node.Name);
            }
        }

        #region Field Accessors
        internal string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        internal string HalfName
        {
            get { return this.halfName; }
            set { this.halfName = value; }
        }

        internal string FullName
        {
            get { return this.fullName; }
            set { this.fullName = value; }
        }

        internal Node Parent
        {
            get { return this.parent; }
            set { this.parent = value; }
        }

        internal bool IsDirectory
        {
            get { return this.isDirectory; }
            set { this.isDirectory = value; }
        }

        internal int FilesOnlyCount
        {
            get { return this.filesOnlyCount; }
            set { this.filesOnlyCount = value; }
        }

        internal long Size
        {
            get { return this.size; }
            set { this.size = value; }
        }

        internal long AccSize
        {
            get { return this.accSize; }
            set { this.accSize = value; }
        }

        internal SortedSet<Node> Children
        {
            get { return this.children; }
            set { this.children = value; }
        }
        #endregion
    }
}
