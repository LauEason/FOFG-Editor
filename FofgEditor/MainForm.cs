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
            filesTab.AddOpenedFilePage(filePath);
        }

        private void prvSaveSpecficedFile(TabPage page, bool saveAs, bool showHint, bool disposePage)
        {
            // A tabpages' close menu is triggered; save file if it is not been saved
            foreach (Control control in page.Controls)
                if (control is ExtRichTextBox)
                {
                    ExtRichTextBox extRichTextBoxItem = control as ExtRichTextBox;                    
                    extRichTextBoxItem.SaveFileHelper(saveAs, showHint);
                }

            if (disposePage)
            {
                page = this.filesTab.SelectedTab;
                this.filesTab.TabPages.Remove(page);
                foreach (Control control in page.Controls)
                    control.Dispose();
                page.Dispose();
            }
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
            prvSaveSpecficedFile(this.filesTab.TabPages[this.filesTab.SelectedIndex], false, true, true);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (TabPage page in this.filesTab.TabPages)
            {
                this.filesTab.SelectTab(page);
                prvSaveSpecficedFile(page, false, true, true);

            }
        }

        private void filesTab_DragDrop(object sender, DragEventArgs e)
        {
            // drag? just call open.
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            prvOpenFilePath(path);
        }

        private void filesTab_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            prvSaveSpecficedFile(this.filesTab.TabPages[this.filesTab.SelectedIndex], false, false, false);
        }
    }
}
