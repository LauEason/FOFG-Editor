using System;
using System.Drawing;
using System.Windows.Forms;

// override a new extended TabControl to show 'close' button - take 'x' as close icon
namespace FofgEditor
{
    class ExtTabControl : TabControl
    {
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

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
