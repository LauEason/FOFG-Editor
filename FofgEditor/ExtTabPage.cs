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
    public partial class ExtTabPage : TabPage
    {
        public ExtTabPage()
        {
            InitializeComponent();
        }

        public ExtTabPage(string text) : base(text)
        {
            InitializeComponent();

            this.Padding = new Padding(20, 0, 0, 0);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
