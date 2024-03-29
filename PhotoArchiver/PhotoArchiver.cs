using MetadataExtractor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Directory = System.IO.Directory;

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
        string FilePathFolder;
        FolderBrowserDialog fbd = new FolderBrowserDialog();
        new Keys LoadKeyCode = Keys.L;
        new Keys SaveKeyCode = Keys.S;
        new Keys RenameSelectKeyCode = Keys.N;

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

            fileNameTextBox.PlaceHolderText = "Enter filename here....";
            progressBar.Visible = false;
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
            previewListbox.Clear();
            pictures.Clear();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                if (isDriveFixed(fbd.SelectedPath) == true)
                {
                    fileOverview.Nodes.Clear();
                    DirectoryInfo directoryInfo = new DirectoryInfo(fbd.SelectedPath);
                    if (directoryInfo.Exists)
                    {
                        fileOverview.Nodes.Clear();
                        BuildTree(directoryInfo, fileOverview.Nodes);
                    }
                }
                else
                {
                    // bestand staat niet op een fixed schijf 
                    MessageBox.Show("Zet de bestanden eerst op een systeem schijf.");
                }
            }

            SetNewNames();
        }

        public void BuildTree(DirectoryInfo directoryInfo, TreeNodeCollection addInMe)
        {
            TreeNode curNode = addInMe.Add(directoryInfo.Name);

            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                curNode.Nodes.Add(file.FullName, file.Name);

                var info = new FileInfo(file.FullName);
                DateTime created = info.CreationTime;

                pictures.Add(new Picture { PictureName = file.Name, DateTimeCreated = created, FilePath = file.FullName, FileType = file.Extension.ToString()});
                amountToRenameBar.Text = (int.Parse(amountToRenameBar.Text)+1).ToString();
            }
            foreach (DirectoryInfo subdir in directoryInfo.GetDirectories())
            {
                BuildTree(subdir, curNode.Nodes);
            }
            fileOverview.ExpandAll();

            FilePathFolder = directoryInfo.FullName;
        }

        private void formatsCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string newName = fileNameTextBox.Text;
            string newFormat = formatsCombobox.Text;

            newNames.Clear();
            previewListbox.Clear();

            if (newFormat == "<naam><YYYY-MM-DD><tijd>")
            {
                currentDateTimeFormat = "yyyy-MM-dd_ss-mm-hh";
            }

            else if (newFormat == "<naam><YYYY-DD-MM><tijd>")
            {
                currentDateTimeFormat = "yyyy-dd-MM_ss-mm-hh";
            }

            else if (newFormat == "<naam><DD-MM-YYYY><tijd>")
            {
                currentDateTimeFormat = "dd-MM-yyyy_ss-mm-hh";
            }

            else if (newFormat == "<naam><MM-DD-YYYY><tijd>")
            {   
                currentDateTimeFormat = "MM-dd-yyyy_ss-mm-hh";
            }

            else
            {
                MessageBox.Show("Geen Format Gekozen");
            }

            SetNewNames();
        }

        public void SetNewNames()
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
                        newFullName = fileNameTextBox.Text + "_" + newDateTime;
                    }
                    else
                    {
                        newFullName = fileNameTextBox.Text + "_" + newDateTime + "(" + i + ")";
                    }

                    previewListbox.Items.Add(newFullName);
                    currentPicture.PictureName = newFullName;

                    newNames.Add(newFullName);

                    i++;
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            int amountOfPictures = pictures.Count;
            int progressbarStep = 100 / amountOfPictures;

            progressBar.Value = 1 + progressbarStep;
            progressBar.Visible = true;
            progressBar.Maximum = 100;
            progressBar.Step = progressbarStep;
            progressBar.Minimum = 0;

            try
            {
                FolderBrowserDialog fbd1 = new FolderBrowserDialog();
                if (fbd1.ShowDialog() == DialogResult.OK)
                {
                    Console.WriteLine(fileSelf);
                    foreach (var picture in pictures)
                    {
                        picture.AddedText = fileNameTextBox.Text;

                        FilePath = picture.FilePath;
                        string NewFile = Path.Combine(FilePathFolder + "\\" + picture.PictureName + picture.FileType);
                        renameFile(FilePath, NewFile);


                        string NewFolderPath = Path.Combine(FilePathFolder + "\\" + picture.PictureName);

                        moveFile(NewFile, picture, fbd1.SelectedPath);
                        progressBar.PerformStep();
                    }

                    MessageBox.Show("Done!");
                    progressBar.Visible = false;

                    DirectoryInfo directoryInfo = new DirectoryInfo(fbd.SelectedPath);

                    fileOverview.Nodes.Clear();
                    BuildTree(directoryInfo, fileOverview.Nodes);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong");
            }
        }

        private void renameFile(string filePath, string newFile)
        {
            File.Move(filePath, newFile);
        }

        private void moveFile(string filePath, Picture picture, string SelectedPath)
        {
            
            string newFolderPath = Path.Combine(SelectedPath + "\\" + picture.AddedText);

            string root = @newFolderPath;

            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
                Debug.WriteLine(filePath);
                Debug.WriteLine(newFolderPath);
                File.Move(filePath, newFolderPath + "\\" + picture.PictureName + picture.FileType);
            }
            else
            {
                File.Move(filePath, newFolderPath + "\\" + picture.PictureName + picture.FileType);
            }
            

            // check file names en maak subfolder aan als isset == false is
            // in selected folder moet een subfolder komen.
            // renameFile.newfile == moveFile.filePath
            // moveFile.newfile == selected folder path + new file name
        }

        private void fileOverview_AfterSelect(object sender, TreeViewEventArgs e)
        {
            fileSelf = @FilePath + "\\" + fileOverview.SelectedNode.Text;
            fileNameTextBox.Text = fileOverview.SelectedNode.Text;
        }

        private void fileNameTextBox_TextChanged(object sender, EventArgs e)
        {
            SetNewNames();
        }

        public bool isDriveFixed(string filepath)
        {
            // define string  
            String str = filepath;

            // retrieve the substring from index 0 to length 8  
            string location = str.Substring(0, 3);

            DriveInfo[] allDrives = System.IO.DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                if (location == d.Name.ToString())
                {
                    if (d.IsReady && d.DriveType == DriveType.Fixed)
                    {
                        // // schijf is fixed 
                        return true;
                    }
                    return false;
                }
            }
            return false;
        }
    }
}
