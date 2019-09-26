using MetadataExtractor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoArchiver
{
    public partial class photoArchiverForm : Form
    {
        string FilePath;
        string fileSelf;
        public photoArchiverForm()
        {
            InitializeComponent();
        }

        List<Picture> pictures = new List<Picture>();
        
        private void fileLoaderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                fileOverview.Nodes.Clear();
                fileNameTextbox.Text = "Bestandsnaam";
                DirectoryInfo directoryInfo = new DirectoryInfo(fbd.SelectedPath);
                if (directoryInfo.Exists)
                {
                    fileOverview.Nodes.Clear();
                    BuildTree(directoryInfo, fileOverview.Nodes);
                }
            }
        }

        private void BuildTree(DirectoryInfo directoryInfo, TreeNodeCollection addInMe)
        {
            TreeNode curNode = addInMe.Add(directoryInfo.Name);

            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                curNode.Nodes.Add(file.FullName, file.Name);

                var info = new FileInfo(file.FullName);
                DateTime created = info.CreationTime;

                pictures.Add(new Picture { PictureName = file.Name,
                                           DateTimeCreated = created,
                                           FilePath = file.FullName,
                                           itemType = file.Extension,
                                           parentDirName = file.Directory.ToString(),
                                           parentDirPath = file.DirectoryName});
            }
            foreach (DirectoryInfo subdir in directoryInfo.GetDirectories())
            {
                BuildTree(subdir, curNode.Nodes);
            }
            FilePath = directoryInfo.ToString();
        }

        private void fileOverview_AfterSelect(object sender, TreeViewEventArgs e)
        {
            fileSelf = @FilePath + "\\" + fileOverview.SelectedNode.Text;

            fileNameTextbox.Text = fileOverview.SelectedNode.Text;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                File.Move(fileSelf, FilePath + "\\" + fileNameTextbox.Text);
                Console.WriteLine(fileSelf);
                MessageBox.Show("OK");
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong");
            }
        }
    }
}
