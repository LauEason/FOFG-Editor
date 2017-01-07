using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace FofgEditor
{
    class ExtTabControl : TabControl
    {
        // new file indexes... keep increasing
        private int newFileIndex = 1;
        public int NewFileIndex
        {
            set { this.newFileIndex = value; }
            get { return this.newFileIndex; }
        }

        public ExtTabControl() : base()
        {
            // commented - set DrawMode to support override OnDrawItem(); or override function will not get called.
            // this.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.MouseDown += ExtTabControl_MouseDown;
        }

        private void ExtTabControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < this.TabPages.Count; i++)
                    if (GetTabRect(i).Contains(new Point(e.X, e.Y)))
                    {
                        this.SelectedIndex = i;
                        this.Invalidate();
                        break;
                    }
            }
        }

        public void AddOpenedFilePage(string filePath)
        {
            // Create a rich text box and initialize style
            ExtRichTextBox fileRichTextBox = new ExtRichTextBox();
            fileRichTextBox.Dock = DockStyle.Fill;

            if (filePath.Length != 0)
            {
                // save path in rich text control name
                fileRichTextBox.Name = filePath + " ";
                fileRichTextBox.filePath = filePath;
                fileRichTextBox.LoadFile(filePath, RichTextBoxStreamType.PlainText);
            }
            else
            {
                fileRichTextBox.Name = "NewFile" + this.NewFileIndex.ToString() + " ";
                fileRichTextBox.Text = "";
                fileRichTextBox.filePath = "";
                this.NewFileIndex++;
                // set filePath with a name since it will be displayed as tab page name
                filePath = fileRichTextBox.Name;
            }

            ExtTabPage newPage = new ExtTabPage(Path.GetFileName(filePath));

            TabPages.Add(newPage);
            TabPages[TabPages.Count - 1].Controls.Add(fileRichTextBox);            
            SelectTab(TabPages.Count - 1);

            // open existed file and add it into tabcontrol always trigger Modifed; reset it
            fileRichTextBox.Modified = false;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
