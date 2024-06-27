using System;
using System.IO;
using System.Windows.Forms;

namespace FileTest
{
    // Displays contents of files and directories
    public partial class FileTestForm : Form
    {
        public FileTestForm()
        {
            InitializeComponent();
        }

        // Triggered when the user presses a key in the inputTextBox
        private void inputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string fileName = inputTextBox.Text;

                if (File.Exists(fileName))
                {
                    GetInformation(fileName);
                    try
                    {
                        using (var stream = new StreamReader(fileName))
                        {
                            outputTextBox.AppendText(stream.ReadToEnd());
                        }
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("Error reading from file", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (Directory.Exists(fileName))
                {
                    GetInformation(fileName);
                    string[] directoryList = Directory.GetDirectories(fileName);
                    outputTextBox.AppendText("Directory contents:\n");
                    foreach (var directory in directoryList)
                    {
                        outputTextBox.AppendText(directory + "\n");
                    }
                }
                else
                {
                    MessageBox.Show($"{inputTextBox.Text} does not exist", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Displays information about the file or directory
        private void GetInformation(string fileName)
        {
            outputTextBox.Clear();
            outputTextBox.AppendText($"{fileName} exists\n");
            outputTextBox.AppendText($"Created: {File.GetCreationTime(fileName)}\n");
            outputTextBox.AppendText($"Last modified: {File.GetLastWriteTime(fileName)}\n");
            outputTextBox.AppendText($"Last accessed: {File.GetLastAccessTime(fileName)}\n");
        }
    }
}
