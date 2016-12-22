using System;
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
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Fill with windows;
            filesTab.Dock = DockStyle.Fill;
            
            // Create a rich text box and initialize style
            RichTextBox fileRichTextBox = new RichTextBox();
            fileRichTextBox.Dock = DockStyle.Fill;
            fileRichTextBox.Name = "NewFile*";
            fileRichTextBox.TabIndex = 0;
            fileRichTextBox.Text = "";

            filesTab.TabPages.Add(fileRichTextBox.Name);
            filesTab.TabPages[filesTab.TabPages.Count - 1].Controls.Add(fileRichTextBox);            
        }
    }
}
