namespace PhotoArchiver
{
    partial class photoArchiverForm
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
            this.fileLoaderButton = new System.Windows.Forms.Button();
            this.fileOverview = new System.Windows.Forms.TreeView();
            this.fileNameTextbox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.formatsCombobox = new System.Windows.Forms.ComboBox();
            this.previewListbox = new System.Windows.Forms.ListView();
            this.optionsButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.amountToRenameBar = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // fileLoaderButton
            // 
            this.fileLoaderButton.Location = new System.Drawing.Point(10, 11);
            this.fileLoaderButton.Margin = new System.Windows.Forms.Padding(2);
            this.fileLoaderButton.Name = "fileLoaderButton";
            this.fileLoaderButton.Size = new System.Drawing.Size(186, 29);
            this.fileLoaderButton.TabIndex = 0;
            this.fileLoaderButton.Text = "Bestanden Inladen";
            this.fileLoaderButton.UseVisualStyleBackColor = true;
            this.fileLoaderButton.Click += new System.EventHandler(this.fileLoaderButton_Click);
            // 
            // fileOverview
            // 
            this.fileOverview.Location = new System.Drawing.Point(10, 45);
            this.fileOverview.Margin = new System.Windows.Forms.Padding(2);
            this.fileOverview.Name = "fileOverview";
            this.fileOverview.Size = new System.Drawing.Size(186, 344);
            this.fileOverview.TabIndex = 1;
            this.fileOverview.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.fileOverview_AfterSelect);
            // 
            // fileNameTextbox
            // 
            this.fileNameTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileNameTextbox.Location = new System.Drawing.Point(208, 10);
            this.fileNameTextbox.Margin = new System.Windows.Forms.Padding(2);
            this.fileNameTextbox.Name = "fileNameTextbox";
            this.fileNameTextbox.Size = new System.Drawing.Size(210, 26);
            this.fileNameTextbox.TabIndex = 2;
            this.fileNameTextbox.Text = "Bestandsnaam";
            this.fileNameTextbox.TextChanged += new System.EventHandler(this.fileNameTextbox_TextChanged);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(422, 10);
            this.saveButton.Margin = new System.Windows.Forms.Padding(2);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(86, 24);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Opslaan";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // formatsCombobox
            // 
            this.formatsCombobox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.formatsCombobox.FormattingEnabled = true;
            this.formatsCombobox.Location = new System.Drawing.Point(208, 39);
            this.formatsCombobox.Margin = new System.Windows.Forms.Padding(2);
            this.formatsCombobox.Name = "formatsCombobox";
            this.formatsCombobox.Size = new System.Drawing.Size(301, 28);
            this.formatsCombobox.TabIndex = 4;
            this.formatsCombobox.SelectedIndexChanged += new System.EventHandler(this.formatsCombobox_SelectedIndexChanged);
            // 
            // previewListbox
            // 
            this.previewListbox.HideSelection = false;
            this.previewListbox.Location = new System.Drawing.Point(208, 71);
            this.previewListbox.Margin = new System.Windows.Forms.Padding(2);
            this.previewListbox.Name = "previewListbox";
            this.previewListbox.Size = new System.Drawing.Size(301, 318);
            this.previewListbox.TabIndex = 5;
            this.previewListbox.UseCompatibleStateImageBehavior = false;
            this.previewListbox.View = System.Windows.Forms.View.List;
            // 
            // optionsButton
            // 
            this.optionsButton.Location = new System.Drawing.Point(520, 10);
            this.optionsButton.Margin = new System.Windows.Forms.Padding(2);
            this.optionsButton.Name = "optionsButton";
            this.optionsButton.Size = new System.Drawing.Size(58, 24);
            this.optionsButton.TabIndex = 6;
            this.optionsButton.Text = "Opties";
            this.optionsButton.UseVisualStyleBackColor = true;
            this.optionsButton.Click += new System.EventHandler(this.optionsButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.amountToRenameBar,
            this.toolStripStatusLabel4});
            this.statusStrip1.Location = new System.Drawing.Point(0, 406);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(587, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // amountToRenameBar
            // 
            this.amountToRenameBar.Name = "amountToRenameBar";
            this.amountToRenameBar.Size = new System.Drawing.Size(13, 17);
            this.amountToRenameBar.Text = "0";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(90, 17);
            this.toolStripStatusLabel4.Text = "Files to rename.";
            // 
            // photoArchiverForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 428);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.optionsButton);
            this.Controls.Add(this.previewListbox);
            this.Controls.Add(this.formatsCombobox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.fileNameTextbox);
            this.Controls.Add(this.fileOverview);
            this.Controls.Add(this.fileLoaderButton);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "photoArchiverForm";
            this.Text = "Photo Archiver";
            this.Load += new System.EventHandler(this.photoArchiverForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button fileLoaderButton;
        private System.Windows.Forms.TreeView fileOverview;
        private System.Windows.Forms.TextBox fileNameTextbox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.ComboBox formatsCombobox;
        private System.Windows.Forms.ListView previewListbox;
        private System.Windows.Forms.Button optionsButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel amountToRenameBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
    }
}

