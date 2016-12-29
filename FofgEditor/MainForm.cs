using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FofgEditor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            // set filesTab styling to fill window
            filesTab.Dock = DockStyle.Fill;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void prvOpenFilePath(string filePath)
        {
            // Create a rich text box and initialize style
            ExtRichTextBox fileRichTextBox = new ExtRichTextBox();
            fileRichTextBox.Dock = DockStyle.Fill;

            if (filePath.Length != 0)
            {
                // save path in rich text control name
                fileRichTextBox.Name = filePath;
                fileRichTextBox.filePath = filePath;
                fileRichTextBox.LoadFile(filePath, RichTextBoxStreamType.PlainText);
            }
            else
            {
                fileRichTextBox.Name = "NewFile" + filesTab.NewFileIndex.ToString() + "*";
                fileRichTextBox.Text = "";
                fileRichTextBox.filePath = "";
                filesTab.NewFileIndex++;
                // set filePath with a name since it will be displayed as tab page name
                filePath = fileRichTextBox.Name;
            }

            filesTab.TabPages.Add(Path.GetFileName(filePath));
            filesTab.TabPages[filesTab.TabPages.Count - 1].Controls.Add(fileRichTextBox);
            filesTab.SelectTab(filesTab.TabPages.Count - 1);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            prvOpenFilePath("");            
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialogLocal = new OpenFileDialog();
            if (openFileDialogLocal.ShowDialog() == DialogResult.OK)
            {
                prvOpenFilePath(openFileDialogLocal.FileName);
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // A tabpages' close menu is triggered; save file if it is not been saved
            foreach (Control control in this.filesTab.TabPages[this.filesTab.SelectedIndex].Controls)
                if (control is ExtRichTextBox)
                {
                    ExtRichTextBox extRichTextBoxItem = (ExtRichTextBox)control;
                    extRichTextBoxItem.SaveFileHelper(false);
                }

            this.filesTab.TabPages.Remove(this.filesTab.SelectedTab);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (TabPage page in this.filesTab.TabPages)
                foreach (Control control in page.Controls)
                    if (control is ExtRichTextBox)
                    {
                        ExtRichTextBox extRichTextBoxItem = (ExtRichTextBox)control;
                        // first switch to closing document
                        this.filesTab.SelectTab(page);
                        extRichTextBoxItem.SaveFileHelper(false);
                        this.filesTab.TabPages.Remove(page);
                    }
        }
    }
}
