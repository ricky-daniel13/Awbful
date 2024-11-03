namespace Awbful
{
    partial class CriUtf
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            criUtfDataGrid = new DataGridView();
            panel1 = new Panel();
            menuStrip1 = new MenuStrip();
            createRowToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)criUtfDataGrid).BeginInit();
            panel1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // criUtfDataGrid
            // 
            criUtfDataGrid.AllowUserToAddRows = false;
            criUtfDataGrid.AllowUserToDeleteRows = false;
            criUtfDataGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            criUtfDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            criUtfDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            criUtfDataGrid.Location = new Point(0, 28);
            criUtfDataGrid.MultiSelect = false;
            criUtfDataGrid.Name = "criUtfDataGrid";
            criUtfDataGrid.Size = new Size(678, 547);
            criUtfDataGrid.TabIndex = 0;
            criUtfDataGrid.UserAddedRow += criUtfDataGrid_UserAddedRow;
            // 
            // panel1
            // 
            panel1.Controls.Add(criUtfDataGrid);
            panel1.Controls.Add(menuStrip1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(678, 575);
            panel1.TabIndex = 1;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { createRowToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(678, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // createRowToolStripMenuItem
            // 
            createRowToolStripMenuItem.Name = "createRowToolStripMenuItem";
            createRowToolStripMenuItem.Size = new Size(79, 20);
            createRowToolStripMenuItem.Text = "Create Row";
            createRowToolStripMenuItem.Click += createRowToolStripMenuItem_Click;
            // 
            // CriUtf
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Name = "CriUtf";
            Size = new Size(678, 575);
            ((System.ComponentModel.ISupportInitialize)criUtfDataGrid).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView criUtfDataGrid;
        private Panel panel1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem createRowToolStripMenuItem;
    }
}
