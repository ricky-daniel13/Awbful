using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Awbful.Editors
{
    public partial class CommandEditor : Form
    {
        private XmlNode commandsNode;
        public XmlNode editedCommand;
        public CommandEditor()
        {
            InitializeComponent();
            this.commandsGrid.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.commandsGrid_RowPostPaint);
        }

        public void LoadTable(XmlNode tableNode)
        {
            // Clear any existing columns and rows in the DataGridView
            commandsGrid.Columns.Clear();
            commandsGrid.Rows.Clear();

            commandsNode = tableNode;

            commandsGrid.Columns.Add("opcode", "opcode");
            commandsGrid.Columns.Add("type", "type");
            commandsGrid.Columns.Add("value", "value");

            foreach (DataGridViewColumn column in commandsGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            Debug.WriteLine(tableNode.ToString());

            // Parse rows and add data to the DataGridView
            XmlNode rowsNode = tableNode.SelectSingleNode("ACBCMD");
            if (rowsNode == null)
                return;

            foreach (XmlNode rowNode in rowsNode.SelectNodes("op"))
            {
                var rowValues = new List<object>();

                
                rowValues.Add(rowNode.Attributes["opcode"].Value);
                rowValues.Add(rowNode.Attributes["type"].Value);
                rowValues.Add(rowNode.Attributes["value"].Value);

                // Add row to DataGridView
                commandsGrid.Rows.Add(rowValues.ToArray());
            }
        }



        private void commandsGrid_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(commandsGrid.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void applyChangesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editedCommand = commandsNode; // Capture the edited value
            XmlNode rowsNode = editedCommand.SelectSingleNode("ACBCMD");

                // Loop through child nodes and remove them
            while (rowsNode.HasChildNodes)
            {
                rowsNode.RemoveChild(rowsNode.FirstChild);
            }

            foreach (DataGridViewRow dgvRow in commandsGrid.Rows)
            {
                if (dgvRow.IsNewRow) continue; // Skip the new row placeholder

                // Create a new XML element for the row
                XmlElement rowElement = rowsNode.OwnerDocument.CreateElement("op");

                // Create an attribute for the column
                XmlAttribute attribute = rowsNode.OwnerDocument.CreateAttribute("opcode");
                attribute.Value = dgvRow.Cells[0].Value.ToString();
                rowElement.Attributes.Append(attribute);

                attribute = rowsNode.OwnerDocument.CreateAttribute("type");
                attribute.Value = dgvRow.Cells[1].Value.ToString();
                rowElement.Attributes.Append(attribute);

                attribute = rowsNode.OwnerDocument.CreateAttribute("value");
                attribute.Value = dgvRow.Cells[2].Value.ToString();
                rowElement.Attributes.Append(attribute);

                // Append the row element to the parent node
                rowsNode.AppendChild(rowElement);
            }

            DialogResult = DialogResult.OK;  // Close form with OK result
            Close();
        }

        private void CommandEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
