namespace Awbful
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openFile = new ToolStripMenuItem();
            saveXMLToolStripMenuItem = new ToolStripMenuItem();
            panel1 = new Panel();
            tabHolder = new TabControl();
            openFileDialog1 = new OpenFileDialog();
            saveFileDialog1 = new SaveFileDialog();
            createWaveformsFromFolderToolStripMenuItem = new ToolStripMenuItem();
            openHcaFolder = new FolderBrowserDialog();
            menuStrip1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, createWaveformsFromFolderToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(802, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openFile, saveXMLToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // openFile
            // 
            openFile.Name = "openFile";
            openFile.Size = new Size(180, 22);
            openFile.Text = "Open XML";
            openFile.Click += OpenFile_Click;
            // 
            // saveXMLToolStripMenuItem
            // 
            saveXMLToolStripMenuItem.Name = "saveXMLToolStripMenuItem";
            saveXMLToolStripMenuItem.Size = new Size(180, 22);
            saveXMLToolStripMenuItem.Text = "Save XML";
            saveXMLToolStripMenuItem.Click += saveXMLToolStripMenuItem_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(tabHolder);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 24);
            panel1.Name = "panel1";
            panel1.Size = new Size(802, 567);
            panel1.TabIndex = 1;
            // 
            // tabHolder
            // 
            tabHolder.Dock = DockStyle.Fill;
            tabHolder.Location = new Point(0, 0);
            tabHolder.Name = "tabHolder";
            tabHolder.SelectedIndex = 0;
            tabHolder.Size = new Size(802, 567);
            tabHolder.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            openFileDialog1.DefaultExt = "xml";
            openFileDialog1.DereferenceLinks = false;
            openFileDialog1.Filter = "cri_utf_tool Xml (*.xml)|*.xml";
            openFileDialog1.Title = "Select XML file";
            openFileDialog1.FileOk += OpenFileDialog1_FileOk;
            // 
            // saveFileDialog1
            // 
            saveFileDialog1.DefaultExt = "xml";
            saveFileDialog1.Filter = "cri_utf_tool Xml (*.xml)|*.xml";
            saveFileDialog1.Title = "Save XML";
            saveFileDialog1.FileOk += saveFileDialog1_FileOk;
            // 
            // createWaveformsFromFolderToolStripMenuItem
            // 
            createWaveformsFromFolderToolStripMenuItem.Name = "createWaveformsFromFolderToolStripMenuItem";
            createWaveformsFromFolderToolStripMenuItem.Size = new Size(181, 20);
            createWaveformsFromFolderToolStripMenuItem.Text = "Create Waveforms from Folder";
            createWaveformsFromFolderToolStripMenuItem.Click += createWaveformsFromFolderToolStripMenuItem_Click;
            // 
            // openHcaFolder
            // 
            openHcaFolder.AddToRecent = false;
            openHcaFolder.Description = "Path with your Hca files";
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(802, 591);
            Controls.Add(panel1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainWindow";
            Text = "Awbful Editor";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openFile;
        private Panel panel1;
        private OpenFileDialog openFileDialog1;
        private TabControl tabHolder;
        private SaveFileDialog saveFileDialog1;
        private ToolStripMenuItem saveXMLToolStripMenuItem;
        private ToolStripMenuItem createWaveformsFromFolderToolStripMenuItem;
        private FolderBrowserDialog openHcaFolder;
    }
}
