using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DialogueTreeTool
{
    public partial class Form1 : Form
    {
        // used for dragging
        private Point start;

        public Form1()
        {
            InitializeComponent();
        }

        private void addBranchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            addDialogueBranch(this.Bounds.Width / 2, this.Bounds.Height / 2);
        }

        private void addDialogueBranch(int x, int y)
        {
            Panel p = new Panel();
            p.Bounds = new Rectangle(x, y, 100, 200);
            p.Controls.Add(new CheckBox());
            p.BorderStyle = BorderStyle.FixedSingle;
            p.MouseDown += new MouseEventHandler(panel_MouseDown);
            this.splitContainer1.Panel1.Controls.Add(p);
        }

        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                start = e.Location;
                ((Panel) sender).MouseUp += new MouseEventHandler(panel_MouseUp);
                ((Panel) sender).MouseMove += new MouseEventHandler(panel_MouseMove);
            }
        }

        void panel_MouseUp(object sender, MouseEventArgs e)
        {
            ((Panel)sender).MouseMove -= new MouseEventHandler(panel_MouseMove);
            ((Panel)sender).MouseUp -= new MouseEventHandler(panel_MouseUp);
        }

        void panel_MouseMove(object sender, MouseEventArgs e)
        {
            Panel panel = (Panel) sender;
            panel.Location = new Point(panel.Location.X - (start.X - e.X), panel.Location.Y - (start.Y - e.Y));
        }


    }
}
