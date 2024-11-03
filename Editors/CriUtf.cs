using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Awbful
{
    public partial class CriUtf : UserControl
    {
        private List<string> columnNames = new List<string>(); // Store column names for indexing
        private XmlNode criUtfNode;
        public CriUtf()
        {
            InitializeComponent();

            this.criUtfDataGrid.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.criUtfDataGrid_RowPostPaint);
        }

        public void LoadTable(XmlNode tableNode)
        {
            // Clear any existing columns and rows in the DataGridView
            criUtfDataGrid.Columns.Clear();
            criUtfDataGrid.Rows.Clear();
            columnNames.Clear();

            criUtfNode = tableNode;

            // Parse the schema to create columns
            XmlNode schemaNode = tableNode.SelectSingleNode("schema");
            if (schemaNode == null)
                return;

            foreach (XmlNode columnNode in schemaNode.SelectNodes("column"))
            {
                string columnName = columnNode.Attributes["name"]?.Value;
                if (!string.IsNullOrEmpty(columnName))
                {
                    criUtfDataGrid.Columns.Add(columnName, columnName);
                    columnNames.Add(columnName);
                }

                foreach (DataGridViewColumn column in criUtfDataGrid.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }

            // Parse rows and add data to the DataGridView
            XmlNode rowsNode = tableNode.SelectSingleNode("rows");
            if (rowsNode == null)
                return;

            foreach (XmlNode rowNode in rowsNode.ChildNodes)
            {
                var rowValues = new List<object>();

                // Loop through columns by name to maintain structure
                for (int i = 0; i < columnNames.Count; i++)
                {
                    string columnName = columnNames[i];
                    XmlNode recordNode = rowNode.ChildNodes.Item(i);  // Assume structure matches

                    string recordValue = recordNode?.Attributes["value"]?.Value ?? string.Empty;

                    // Custom parsing logic based on column name or type
                    object parsedValue = Parse(columnName, recordValue);

                    rowValues.Add(parsedValue);
                }

                // Add row to DataGridView
                criUtfDataGrid.Rows.Add(rowValues.ToArray());
            }


        }

        private static object Parse(string columnName, string value)
        {
            // Example custom parsing based on column name or type
            switch (columnName)
            {
                default:
                    // Default to string value
                    return value;
            }
        }

        public XmlNode SaveTable()
        {
            XmlNode editedCommand = criUtfNode; // Capture the edited value
            XmlNode rowsNode = editedCommand.SelectSingleNode("rows");

            // Loop through child nodes and remove them
            while (rowsNode.HasChildNodes)
            {
                rowsNode.RemoveChild(rowsNode.FirstChild);
            }

            foreach (DataGridViewRow dgvRow in criUtfDataGrid.Rows)
            {
                if (dgvRow.IsNewRow) continue; // Skip the new row placeholder

                // Create a new XML element for the row
                XmlElement rowRoot = rowsNode.OwnerDocument.CreateElement("row");
                for (int i = 0; i < columnNames.Count; i++)
                {
                    string columnName = columnNames[i];
                    XmlElement recordNode = rowsNode.OwnerDocument.CreateElement("record");

                    XmlAttribute attribute = rowsNode.OwnerDocument.CreateAttribute("value");
                    attribute.Value = dgvRow.Cells[i].Value.ToString();
                    recordNode.Attributes.Append(attribute);

                    XmlAttribute nameAttribute = rowsNode.OwnerDocument.CreateAttribute("name");
                    nameAttribute.Value = columnName;
                    recordNode.Attributes.Append(nameAttribute);


                    // Append the row element to the parent node
                    rowRoot.AppendChild(recordNode);
                }
                rowsNode.AppendChild(rowRoot);
            }

            return editedCommand;
        }

        private void criUtfDataGrid_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(criUtfDataGrid.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void criUtfDataGrid_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {

        }

        private void createRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check if there are any rows in the DataGridView
            if (criUtfDataGrid.Rows.Count > 0)
            {
                // Get the last row's values
                var lastRow = criUtfDataGrid.Rows[criUtfDataGrid.Rows.Count - 1];
                var rowData = new List<string>();

                foreach (DataGridViewCell cell in lastRow.Cells)
                {
                    rowData.Add(cell.Value?.ToString() ?? string.Empty);
                }

                // Add a new row with the duplicated data
                criUtfDataGrid.Rows.Add(rowData.ToArray());
            }
            else
            {
                // If there are no rows, add an empty row as the first entry
                criUtfDataGrid.Rows.Add();
            }
        }
    }
}
