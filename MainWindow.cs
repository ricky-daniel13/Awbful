using Awbful.Editors;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;

namespace Awbful
{
    public partial class MainWindow : Form
    {
        CriUtf cueNode, synthNode, sequenceNode, trackNode, waveformNode;
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

                    XmlNode? synthTable = tableNode.SelectSingleNode("rows/row/CRIUTF[@name='Synth']");
                    synthNode = AddNewTab(synthTable, "Synth");

                    XmlNode? trackTable = tableNode.SelectSingleNode("rows/row/CRIUTF[@name='Track']");
                    if(trackTable != null)
                        trackNode = AddNewTab(trackTable, "Track");

                    XmlNode? sequenceTable = tableNode.SelectSingleNode("rows/row/CRIUTF[@name='Sequence']");
                    if (trackTable != null)
                        sequenceNode = AddNewTab(sequenceTable, "Sequence");

                    XmlNode? waveformTable = tableNode.SelectSingleNode("rows/row/CRIUTF[@name='Waveform']");
                    waveformNode = AddNewTab(waveformTable, "Waveform");

                    XmlNode? trackEventTable = tableNode.SelectSingleNode("rows/row/CRIUTF[@name='TrackEvent']");
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
    }
}
