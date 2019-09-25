using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoArchiver
{
    public partial class photoArchiverForm : Form
    {
        public photoArchiverForm()
        {
            InitializeComponent();
        }

        FolderBrowserDialog fbd = new FolderBrowserDialog();

        private void fileLoaderButton_Click(object sender, EventArgs e)
        {
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string currentPath = fbd.SelectedPath;
                string[] allfiles = Directory.GetFiles(currentPath, "*", SearchOption.AllDirectories);

                fileOverview.Nodes.Clear();
                if (currentPath != "" && Directory.Exists(currentPath))
                {
                    LoadDirectory(currentPath);
                }
            }
        }

        public void LoadDirectory(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            TreeNode tds = fileOverview.Nodes.Add(di.Name);
            tds.Tag = di.FullName;
            tds.StateImageIndex = 0;
            LoadFiles(path, tds);
            LoadSubDirectories(path, tds);
        }

        private void LoadSubDirectories(string path, TreeNode td)
        {
            // Get all subdirectories
            string[] subdirs = Directory.GetDirectories(path);

            // loop through them to see if they have any other subdirectories
            foreach (string sub in subdirs)
            {
                DirectoryInfo di = new DirectoryInfo(sub);
                TreeNode tds = td.Nodes.Add(di.Name);
                tds.StateImageIndex = 0;
                tds.Tag = di.FullName;
                LoadSubDirectories(sub, tds);
            }
        }

        private void LoadFiles(string path, TreeNode td)
        {
            string[] Files = Directory.GetFiles(path, "*");

            // loop through them to show files
            foreach (string file in Files)
            {
                FileInfo fi = new FileInfo(file);
                TreeNode tds = td.Nodes.Add(fi.Name);
                tds.Tag = fi.FullName;
                tds.StateImageIndex = 1;
            }
        }
    }
}
