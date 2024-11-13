using Awbful.Editors;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Awbful
{
    public partial class MainWindow : Form
    {
        CriUtf cueNode, synthNode, sequenceNode, trackNode, waveformNode;
        XmlElement awbNode, waveformElement, synthElement, trackEventElement, trackElement;
        CommandTable trackEventNode;
        XmlDocument xmlDoc;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void OpenFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string filePath = openFileDialog1.FileName;
            xmlDoc = new XmlDocument();

            try
            {
                xmlDoc.Load(filePath);

                // Get the first Table node for demonstration
                XmlNode? tableNode = xmlDoc.SelectSingleNode("/CRIUTF");

                if (tableNode != null)
                {
                    tabHolder.TabPages.Clear();

                    XmlNode? schemaNode = tableNode.SelectSingleNode("schema");
                    if (schemaNode == null)
                        return;

                    /*foreach (XmlNode columnNode in schemaNode.SelectNodes("column"))
                    {
                        string columnName = columnNode.Attributes["name"]?.Value;
                        if (!string.IsNullOrEmpty(columnName))
                        {
                            Debug.WriteLine(columnName);
                        }
                    }*/

                    XmlNode? cueTable = tableNode.SelectSingleNode("rows/row/CRIUTF[@name='Cue']");
                    cueNode = AddNewTab(cueTable, "Cue");

                    awbNode = (XmlElement)tableNode.SelectSingleNode("rows/row/AWB[@subkey='0']");

                    XmlNode? synthTable = tableNode.SelectSingleNode("rows/row/CRIUTF[@name='Synth']");
                    synthNode = AddNewTab(synthTable, "Synth");
                    synthElement = (XmlElement)synthTable;

                    XmlNode? trackTable = tableNode.SelectSingleNode("rows/row/CRIUTF[@name='Track']");
                    trackElement = (XmlElement)trackTable;
                    if (trackTable != null)
                        trackNode = AddNewTab(trackTable, "Track");

                    XmlNode? sequenceTable = tableNode.SelectSingleNode("rows/row/CRIUTF[@name='Sequence']");
                    if (trackTable != null)
                        sequenceNode = AddNewTab(sequenceTable, "Sequence");

                    XmlNode? waveformTable = tableNode.SelectSingleNode("rows/row/CRIUTF[@name='Waveform']");
                    waveformNode = AddNewTab(waveformTable, "Waveform");
                    waveformElement = (XmlElement)waveformTable;

                    XmlNode? trackEventTable = tableNode.SelectSingleNode("rows/row/CRIUTF[@name='TrackEvent']");
                    trackEventElement = (XmlElement)trackEventTable;
                    if (trackTable != null)
                        trackEventNode = AddCommandTab(trackEventTable, "TrackEvent");
                }
                else
                {
                    MessageBox.Show("No Table node found in the XML file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading XML file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private CriUtf AddNewTab(XmlNode table, string tableName)
        {
            // Create a new TabPage
            TabPage newTabPage = new TabPage(tableName);

            // Instantiate the custom UserControl
            CriUtf customView = new CriUtf()
            {
                Dock = DockStyle.Fill // Ensure it fills the tab
            };

            customView.LoadTable(table);

            // Add the UserControl to the TabPage
            newTabPage.Controls.Add(customView);

            // Add the TabPage to the TabControl
            tabHolder.TabPages.Add(newTabPage);

            // Select the new tab
            tabHolder.SelectedTab = newTabPage;

            return customView;
        }

        private CommandTable AddCommandTab(XmlNode table, string tableName)
        {
            // Create a new TabPage
            TabPage newTabPage = new TabPage(tableName);

            // Instantiate the custom UserControl
            CommandTable customView = new CommandTable()
            {
                Dock = DockStyle.Fill // Ensure it fills the tab
            };

            customView.LoadTable(table);

            // Add the UserControl to the TabPage
            newTabPage.Controls.Add(customView);

            // Add the TabPage to the TabControl
            tabHolder.TabPages.Add(newTabPage);

            // Select the new tab
            tabHolder.SelectedTab = newTabPage;

            return customView;
        }

        private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Save the XML document to the selected file path
            string filePath = saveFileDialog1.FileName;

            XmlNode? tableNode = xmlDoc.SelectSingleNode("/CRIUTF/rows/row");

            if (tableNode != null)
            {
                XmlNode? cueTable = tableNode.SelectSingleNode("CRIUTF[@name='Cue']");
                XmlNode editedTable = cueNode.SaveTable();
                tableNode.ReplaceChild(editedTable, cueTable);


                XmlNode? synthTable = tableNode.SelectSingleNode("CRIUTF[@name='Synth']");
                editedTable = synthNode.SaveTable();
                tableNode.ReplaceChild(editedTable, synthTable);

                XmlNode? trackTable = tableNode.SelectSingleNode("CRIUTF[@name='Track']");
                if (trackTable != null)
                {
                    editedTable = trackNode.SaveTable();
                    tableNode.ReplaceChild(editedTable, trackTable);
                }

                XmlNode? sequenceTable = tableNode.SelectSingleNode("CRIUTF[@name='Sequence']");
                if (trackTable != null)
                {
                    editedTable = sequenceNode.SaveTable();
                    tableNode.ReplaceChild(editedTable, sequenceTable);
                }

                XmlNode? waveformTable = tableNode.SelectSingleNode("CRIUTF[@name='Waveform']");
                editedTable = waveformNode.SaveTable();
                tableNode.ReplaceChild(editedTable, waveformTable);

                XmlNode? trackEventTable = tableNode.SelectSingleNode("CRIUTF[@name='TrackEvent']");
                if (trackTable != null)
                {
                    editedTable = trackEventNode.SaveTable();
                    tableNode.ReplaceChild(editedTable, trackEventTable);
                }

                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = "\t", // Use a tab character for indentation
                    NewLineChars = "\r\n",
                    NewLineHandling = NewLineHandling.Replace
                };

                using (XmlWriter writer = XmlWriter.Create(filePath, settings))
                {
                    xmlDoc.Save(writer);
                }
            }
            else
            {
                MessageBox.Show("No Table node found in the XML file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }


        private void createWaveformsFromFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openHcaFolder.ShowDialog();

            if (result == DialogResult.OK)
            {
                string folderPath = openHcaFolder.SelectedPath;
                // 1. Read all .hca files from the folder
                string[] audioFiles = Directory.GetFiles(folderPath, "*.hca");

                // Get the <files> node from the AWB element
                XmlNode filesNode = awbNode.GetElementsByTagName("files")[0];
                // Clear the existing <file> entries
                filesNode.RemoveAll();

                // Get the <order> node from the AWB element
                XmlNode orderNode = awbNode.GetElementsByTagName("order")[0];
                orderNode.RemoveAll();

                // Get the <rows> node from the CRIUTF node
                XmlNode rowsNode = waveformElement.GetElementsByTagName("rows")[0];
                XmlNodeList rowNodes = rowsNode.ChildNodes;

                XmlNode synthRows = synthElement.GetElementsByTagName("rows")[0];
                XmlNodeList synthRowNodes = synthRows.ChildNodes;

                XmlNode trackEventRows = trackEventElement.GetElementsByTagName("rows")[0];
                XmlNodeList trackEventRowNodes = trackEventRows.ChildNodes;

                XmlNode trackRows = trackElement.GetElementsByTagName("rows")[0];
                XmlNodeList trackRowNodes = trackRows.ChildNodes;

                // Keep track of the number of files
                int fileCount = audioFiles.Length;
                int rowCount = rowNodes.Count;
                int synthCount = synthRowNodes.Count;
                int trackEventRowCount = trackEventRowNodes.Count;
                int trackRowCount = trackRowNodes.Count;

                // Add new <file> entries to the <files> node
                for (int i = 0; i < fileCount; i++)
                {
                    XmlElement fileElement = awbNode.OwnerDocument.CreateElement("file");
                    fileElement.SetAttribute("seq", i.ToString());
                    fileElement.SetAttribute("path", audioFiles[i]);
                    filesNode.AppendChild(fileElement);

                    // If there are more files than rows, duplicate the last row
                    if (i >= rowCount)
                    {
                        // Copy the last row (duplicating)
                        XmlNode lastRow = rowNodes[rowCount - 1];
                        XmlNode newRow = lastRow.CloneNode(true);

                        // Update the MemoryAwbId for the new row
                        XmlNode memoryAwbIdNode = newRow.SelectSingleNode("record[@name='MemoryAwbId']");
                        if (memoryAwbIdNode != null)
                        {
                            memoryAwbIdNode.Attributes["value"].Value = (i).ToString();
                        }

                        // Append the new row to the <rows> node
                        rowsNode.AppendChild(newRow);
                    }
                    if (i >= synthCount)
                    {
                        // Copy the last row (duplicating)
                        XmlNode lastRow = synthRowNodes[synthCount - 1];
                        XmlNode newRow = lastRow.CloneNode(true);

                        // Update the MemoryAwbId for the new row
                        XmlNode referenceItemNode = newRow.SelectSingleNode("record[@name='ReferenceItems']");
                        if (referenceItemNode != null)
                        {
                            string newReferenceItemValue = "0001" + i.ToString("x4"); // Ensure 4 lowercase hexadecimal digits
                            referenceItemNode.Attributes["value"].Value = newReferenceItemValue;
                        }

                        // Append the new row to the <rows> node
                        synthRows.AppendChild(newRow);
                    }
                    if (i >= trackRowCount)
                    {
                        // Copy the last row (duplicating)
                        XmlNode lastRow = trackRowNodes[trackRowCount - 1];
                        XmlNode newRow = lastRow.CloneNode(true);

                        // Update the MemoryAwbId for the new row
                        XmlNode eventIndexNode = newRow.SelectSingleNode("record[@name='EventIndex']");
                        if (eventIndexNode != null)
                        {
                            eventIndexNode.Attributes["value"].Value = (i).ToString();
                        }

                        // Append the new row to the <rows> node
                        trackRows.AppendChild(newRow);
                    }
                    if (i >= trackEventRowCount)
                    {
                        // Copy the last row (duplicating)
                        XmlNode lastRow = trackEventRowNodes[trackEventRowCount - 1];
                        XmlNode newRow = lastRow.CloneNode(true);

                        XmlNode opNode = newRow.SelectSingleNode("ACBCMD/op[@opcode='2000']");

                        if (opNode != null)
                        {
                            // Calculate the new value: type << 16 | index
                            int type = 2;
                            int newValue = (type << 16) | i;

                            // Set the "value" attribute to the calculated new value
                            opNode.Attributes["value"].Value = newValue.ToString();
                        }
                        else
                        {
                            Console.WriteLine("No <op> element with opcode='2000' found.");
                        }

                        // Append the new row to the <rows> node
                        trackEventRows.AppendChild(newRow);
                    }
                }

                // Update the order nodes to match the file sequence
                for (int i = 0; i < fileCount; i++)
                {
                    XmlElement orderElement = awbNode.OwnerDocument.CreateElement("entry");
                    orderElement.SetAttribute("id", i.ToString());
                    orderNode.AppendChild(orderElement);
                }

                waveformNode.LoadTable(waveformElement);
                synthNode.LoadTable(synthElement);
                trackEventNode.LoadTable(trackEventElement);
                trackNode.LoadTable(trackElement);
            }
        }
    }
}
