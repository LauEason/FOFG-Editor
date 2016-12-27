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
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        public bool SaveFileHelper(bool saveAs)
        {
            // query for saved path if current file is new file
            if (filePath == "" && this.Modified)
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
