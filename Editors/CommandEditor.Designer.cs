namespace Awbful.Editors
{
    partial class CommandEditor
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
            panel1 = new Panel();
            commandsGrid = new DataGridView();
            menuStrip1 = new MenuStrip();
            addRowToolStripMenuItem = new ToolStripMenuItem();
            applyChangesToolStripMenuItem = new ToolStripMenuItem();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)commandsGrid).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(commandsGrid);
            panel1.Controls.Add(menuStrip1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 450);
            panel1.TabIndex = 0;
            // 
            // commandsGrid
            // 
            commandsGrid.AllowUserToAddRows = false;
            commandsGrid.AllowUserToDeleteRows = false;
            commandsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            commandsGrid.Dock = DockStyle.Fill;
            commandsGrid.Location = new Point(0, 24);
            commandsGrid.Name = "commandsGrid";
            commandsGrid.Size = new Size(800, 426);
            commandsGrid.TabIndex = 1;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { addRowToolStripMenuItem, applyChangesToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // addRowToolStripMenuItem
            // 
            addRowToolStripMenuItem.Name = "addRowToolStripMenuItem";
            addRowToolStripMenuItem.Size = new Size(67, 20);
            addRowToolStripMenuItem.Text = "Add Row";
            // 
            // applyChangesToolStripMenuItem
            // 
            applyChangesToolStripMenuItem.Name = "applyChangesToolStripMenuItem";
            applyChangesToolStripMenuItem.Size = new Size(99, 20);
            applyChangesToolStripMenuItem.Text = "Apply Changes";
            applyChangesToolStripMenuItem.Click += applyChangesToolStripMenuItem_Click;
            // 
            // CommandEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            MainMenuStrip = menuStrip1;
            Name = "CommandEditor";
            Text = "CommandEditor";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)commandsGrid).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem addRowToolStripMenuItem;
        private ToolStripMenuItem applyChangesToolStripMenuItem;
        private DataGridView commandsGrid;
    }
}