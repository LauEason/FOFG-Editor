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
            RichTextBox fileRichTextBox = new RichTextBox();
            fileRichTextBox.Dock = DockStyle.Fill;

            if (filePath.Length != 0)
            {
                // save path in rich text control name
                fileRichTextBox.Name = filePath;
                fileRichTextBox.LoadFile(filePath, RichTextBoxStreamType.PlainText);
            }
            else
            {
                fileRichTextBox.Name = "NewFile*";                
                fileRichTextBox.Text = "";
                // set filePath with a name since it will be displayed as tab page name
                filePath = fileRichTextBox.Name;
            }

            filesTab.TabPages.Add(Path.GetFileName(filePath));
            filesTab.TabPages[filesTab.TabPages.Count - 1].Controls.Add(fileRichTextBox);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            prvOpenFilePath("");            
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogMain.ShowDialog() == DialogResult.OK)
            {
                prvOpenFilePath(openFileDialogMain.FileName);
            }
        }
    }
}
