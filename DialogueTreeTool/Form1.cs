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
        private Point start, rightClickLocation;

        public Form1()
        {
            InitializeComponent();
            this.splitContainer1.Panel1.MouseDown += new MouseEventHandler(workArea_MouseDown);
        }

        private void addBranchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addDialogueBranch(rightClickLocation.X, rightClickLocation.Y);
        }

        private void addDialogueBranch(int x, int y)
        {
            // Create a panel with dialogue branch in it:
            // * A list of linear dialogue lines (may be associated with actions)
            // * A list of choices that occur at the end of the branch, leading to other branches.
            //   For more flexibility, this can also be a streamlined (no choices) jump to another branch.
            //   e.g., A-B-C then looping C-B: A-B therefore could be streamlined but still separate.
            Panel p = new Panel();
            p.Bounds = new Rectangle(x, y, 200, 200);
            p.BorderStyle = BorderStyle.FixedSingle;
            p.MouseDown += new MouseEventHandler(panel_MouseDown);

            SplitContainer sc = new SplitContainer();
            sc.Orientation = Orientation.Horizontal;

            // Top pane: dialogue lines
            ListBox linesListBox = new ListBox();
            // TODO: apply constraint to lines listbox so when it's empty it shows a hint about adding dialogue lines?
            sc.Panel1.Controls.Add(linesListBox);
            
            // Bottom pane: choices
            ListBox choicesListBox = new ListBox();
            // TODO: listener to draw a line when you add a destination to choices
            sc.Panel2.Controls.Add(choicesListBox);
            
            p.Controls.Add(sc);
            this.splitContainer1.Panel1.Controls.Add(p);
        }

        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                start = e.Location;
                ((Control)sender).MouseUp += new MouseEventHandler(panel_MouseUp);
                ((Control)sender).MouseMove += new MouseEventHandler(panel_MouseMove);
            }
        }

        private void workArea_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                rightClickLocation = e.Location;
            }

        }

        void panel_MouseUp(object sender, MouseEventArgs e)
        {
            ((Control)sender).MouseMove -= new MouseEventHandler(panel_MouseMove);
            ((Control)sender).MouseUp -= new MouseEventHandler(panel_MouseUp);
        }

        void panel_MouseMove(object sender, MouseEventArgs e)
        {
            Control branch = (Control) sender;
            branch.Location = new Point(branch.Location.X - (start.X - e.X), branch.Location.Y - (start.Y - e.Y));
        }


    }
}
