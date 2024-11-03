using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Xml;

namespace Awbful.Editors
{
    public partial class CommandTable : UserControl
    {
        private XmlNode commandTableNode;
        public CommandTable()
        {
            InitializeComponent();

            this.cmdTableGrid.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.cmdTableGrid_RowPostPaint);
        }

        public void LoadTable(XmlNode tableNode)
        {
            // Clear any existing columns and rows in the DataGridView
            cmdTableGrid.Columns.Clear();
            cmdTableGrid.Rows.Clear();

            commandTableNode = tableNode;

            cmdTableGrid.Columns.Add("Command", "Command");

            foreach (DataGridViewColumn column in cmdTableGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // Parse rows and add data to the DataGridView
            XmlNode rowsNode = tableNode.SelectSingleNode("rows");
            if (rowsNode == null)
                return;

            foreach (XmlNode rowNode in rowsNode.SelectNodes("row"))
            {
                var rowValues = new List<object>();

                //XmlNode acbCmd = tableNode.SelectSingleNode("ACBCMD");
                rowValues.Add("Click To Edit");

                // Add row to DataGridView
                cmdTableGrid.Rows.Add(rowValues.ToArray());
            }
        }

        private void cmdTableGrid_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(cmdTableGrid.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        public XmlNode SaveTable()
        {
            XmlNode editedCommand = commandTableNode; // Capture the edited value

            return editedCommand;
        }

        private void cmdTableGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return; // Ignore header clicks
            int rowIndex = e.RowIndex;

            var rows = commandTableNode.SelectSingleNode("rows");

            // Open the editing dialog
            using (var editForm = new CommandEditor())
            {
                editForm.LoadTable(rows.ChildNodes.Item(rowIndex));
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    
                    XmlNode editedRow = editForm.editedCommand;
                    rows.ReplaceChild(editedRow, rows.ChildNodes.Item(rowIndex));
                }
            }
        }
    }
}
