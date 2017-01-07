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
    public partial class ExtRichTextBox : RichTextBox
    {
        // indicate current file location
        public string filePath;
        
        public ExtRichTextBox() : base()
        {
            InitializeComponent();
            this.AllowDrop = true;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        public bool SaveFileHelper(bool saveAs, bool showHint)
        {
            string hintFileName;

            if (filePath == "")
                hintFileName = this.Name;
            else
                hintFileName = filePath;
                        
            // query for saved path if current file is new file
            if (showHint && this.Modified == true &&
                MessageBox.Show("File: \"" + hintFileName + "\" is not saved, save it ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return false;

            if (saveAs || (filePath == "" && this.Modified))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    this.filePath = saveFileDialog.FileName;
                    this.Name = saveFileDialog.FileName;
                }
            }

            // got file path; save it.
            if (filePath != "" && this.Modified)
            {
                this.SaveFile(filePath, RichTextBoxStreamType.PlainText);
                this.Modified = false;
                return true;
            }
            return false;
        }
    }
}
