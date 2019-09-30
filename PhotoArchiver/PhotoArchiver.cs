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
        List<Picture> pictures = new List<Picture>();
        List<string> formats = new List<string>();
        List<string> newNames = new List<string>();
        string currentDateTimeFormat;
        string FilePath;
        string fileSelf;


        public photoArchiverForm()
        {
            InitializeComponent();
        }

        private void photoArchiverForm_Load(object sender, EventArgs e)
        {
            FillFormats();
            foreach (string format in formats)
            {
                formatsCombobox.Items.Add(format);
            }
            formatsCombobox.SelectedIndex = 0;
        }

        public void FillFormats()
        {
            formats.Add("<naam><YYYY-MM-DD><tijd>");
            formats.Add("<naam><YYYY-DD-MM><tijd>");
            formats.Add("<naam><DD-MM-YYYY><tijd>");
            formats.Add("<naam><MM-DD-YYYY><tijd>");
        }



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

            SetNewNames();
        }

        private void BuildTree(DirectoryInfo directoryInfo, TreeNodeCollection addInMe)
        {
            TreeNode curNode = addInMe.Add(directoryInfo.Name);

            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                curNode.Nodes.Add(file.FullName, file.Name);

                var info = new FileInfo(file.FullName);
                DateTime created = info.CreationTime;

                pictures.Add(new Picture { PictureName = file.Name, DateTimeCreated = created, FilePath = file.FullName });
            }
            foreach (DirectoryInfo subdir in directoryInfo.GetDirectories())
            {
                BuildTree(subdir, curNode.Nodes);
            }
        }

        private void optionsButton_Click(object sender, EventArgs e)
        {

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

        private void fileOverview_AfterSelect(object sender, TreeViewEventArgs e)
        {
            fileSelf = @FilePath + "\\" + fileOverview.SelectedNode.Text;
            fileNameTextbox.Text = fileOverview.SelectedNode.Text;
        }

        private void fileNameTextbox_TextChanged(object sender, EventArgs e)
        {
            SetNewNames();
        }

        private void formatsCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string newName = fileNameTextbox.Text;
            string newFormat = formatsCombobox.Text;

            newNames.Clear();
            previewListbox.Clear();

            if (newFormat == "<naam><YYYY-MM-DD><tijd>")
            {
                currentDateTimeFormat = "yyyy-MM-dd_ss:mm:hh";
            }

            else if (newFormat == "<naam><YYYY-DD-MM><tijd>")
            {
                currentDateTimeFormat = "yyyy-dd-MM_ss:mm:hh";
            }

            else if (newFormat == "<naam><DD-MM-YYYY><tijd>")
            {
                currentDateTimeFormat = "dd-MM-yyyy_ss:mm:hh";
            }

            else if (newFormat == "<naam><MM-DD-YYYY><tijd>")
            {
                currentDateTimeFormat = "MM-dd-yyyy_ss:mm:hh";
            }

            else
            {
                MessageBox.Show("Geen Format Gekozen");
            }

            SetNewNames();
        }

        void SetNewNames()
        {
            newNames.Clear();
            previewListbox.Clear();

            var groupByDateAndTime = from picture in pictures
                                orderby picture.DateTimeCreated
                                group picture by picture.DateTimeCreated.ToString("yyyy-MM-dd HH:mm:ss"); //query that creates subcollections of all unique date and timestamp

            var groupedPictureList = groupByDateAndTime.ToList();

            foreach (var currentPictureList in groupedPictureList) //loop through all subcollections
            {
                int i = 0;

                foreach (var currentPicture in currentPictureList) //loop through pictures in current subcollection
                {
                    string newDateTime = currentPicture.DateTimeCreated.ToString(currentDateTimeFormat); //get datetime based on selected format
                    string newFullName;
                    if (i == 0)
                    {
                        newFullName = fileNameTextbox.Text + "_" + newDateTime;
                    }
                    else
                    {
                        newFullName = fileNameTextbox.Text + "_" + newDateTime + "(" + i + ")";
                    }

                    previewListbox.Items.Add(newFullName);
                    currentPicture.PictureName = newFullName;

                    newNames.Add(newFullName);

                    i++;
                }
            }
        }
    }
}
