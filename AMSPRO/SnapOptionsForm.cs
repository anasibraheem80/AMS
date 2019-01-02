using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AMSPRO
{
    public partial class SnapOptionsForm : Form
    {
        public SnapOptionsForm()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SnapOptionsForm_Load(object sender, EventArgs e)
        {
            if (Snap.Joints == 1) checkBox1.Checked = true;
            if (Snap.LineEndsandMidpoints == 1) checkBox2.Checked = true;
            if (Snap.GridIntersections == 1) checkBox3.Checked = true;
            if (Snap.LinesandFrames == 1) checkBox4.Checked = true;
            if (Snap.Edges == 1) checkBox5.Checked = true;
            if (Snap.PerpendicularProjections == 1) checkBox6.Checked = true;
            if (Snap.Intersections == 1) checkBox7.Checked = true;
            if (Snap.FineGrid == 1) checkBox8.Checked = true;
            if (Snap.Extensions == 1) checkBox9.Checked = true;
            if (Snap.Prallels == 1) checkBox10.Checked = true;
            if (Snap.IntelligentSnap == 1) checkBox10.Checked = true;
            if (Snap.ArchLayer == 1) checkBox12.Checked = true;
            textBox1.Text = Snap.FineGridValue.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Snap.Joints = 0;
            Snap.LineEndsandMidpoints = 0;
            Snap.GridIntersections = 0;
            Snap.LinesandFrames = 0;
            Snap.Edges = 0;
            Snap.PerpendicularProjections = 0;
            Snap.Intersections = 0;
            Snap.FineGrid = 0;
            Snap.Extensions = 0;
            Snap.Prallels = 0;
            Snap.IntelligentSnap = 0;
            Snap.ArchLayer = 0;
            if (checkBox1.Checked == true) Snap.Joints = 1;
            if (checkBox2.Checked == true) Snap.LineEndsandMidpoints = 1;
            if (checkBox3.Checked == true) Snap.GridIntersections = 1;
            if (checkBox4.Checked == true) Snap.LinesandFrames = 1;
            if (checkBox5.Checked == true) Snap.Edges = 1;
            if (checkBox6.Checked == true) Snap.PerpendicularProjections = 1;
            if (checkBox7.Checked == true) Snap.Intersections = 1;
            if (checkBox8.Checked == true) Snap.FineGrid = 1;
            if (checkBox9.Checked == true) Snap.Extensions = 1;
            if (checkBox10.Checked == true) Snap.Prallels = 1;
            if (checkBox10.Checked == true) Snap.IntelligentSnap = 1;
            if (checkBox12.Checked == true) Snap.ArchLayer = 1;
            Snap.FineGridValue = Convert.ToDouble (textBox1.Text);
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            checkBox4.Checked = true;
            checkBox5.Checked = true;
            checkBox6.Checked = true;
            checkBox7.Checked = true;
            checkBox8.Checked = true;
            checkBox9.Checked = true;
            checkBox10.Checked = true;
            checkBox11.Checked = true;
            checkBox12.Checked = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
            checkBox9.Checked = false;
            checkBox10.Checked = false;
            checkBox11.Checked = false;
            checkBox12.Checked = false;
        }
    }
}