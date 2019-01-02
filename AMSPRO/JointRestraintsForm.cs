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
    public partial class JointRestraintsForm : Form
    {
        public JointRestraintsForm()
        {
            InitializeComponent();
        }
        Form mainForm = Application.OpenForms["MainForm"];
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int FixX = 0;
            int FixY = 0;
            int FixZ = 0;
            int FixRX = 0;
            int FixRY = 0;
            int FixRZ = 0;
            if (checkBox1.Checked == true) FixX = 1;
            if (checkBox2.Checked == true) FixY = 1;
            if (checkBox3.Checked == true) FixZ = 1;
            if (checkBox4.Checked == true) FixRX = 1;
            if (checkBox5.Checked == true) FixRY = 1;
            if (checkBox6.Checked == true) FixRZ = 1;
            for (int i = 1; i < Joint.Number3d + 1; i++)
            {
                if (Joint.Selected[i] == 1)
                {
                    Joint.FixX[i] = FixX;
                    Joint.FixY[i] = FixY;
                    Joint.FixZ[i] = FixZ;
                    Joint.FixRX[i] = FixRX;
                    Joint.FixRY[i] = FixRY;
                    Joint.FixRZ[i] = FixRZ;
                }
            }
            for (int i = 1; i < Frame.Number + 1; i++)
            {
                ((MainForm)mainForm).FrameElement[i].Selected = 0;
            }
            for (int i = 1; i < Shell.Number + 1; i++)
            {
                for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                {
                    Shell.SelectedLine[i, j] = 0;
                }
            }
            for (int add = 1; add < Joint.Number3d + 1; add++)
            {
                Joint.Selected[add] = 0;
            }
            ((MainForm)mainForm).MakeTempFiles();
            DROWcls callmee = new DROWcls();
            callmee.Render2d();
            callmee.Render3d();
            //((MainForm)mainForm).pictureBox1.Refresh();
            //((MainForm)mainForm).pictureBox2.Refresh();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            int FixX = 0;
            int FixY = 0;
            int FixZ = 0;
            int FixRX = 0;
            int FixRY = 0;
            int FixRZ = 0;
            if (checkBox1.Checked == true) FixX = 1;
            if (checkBox2.Checked == true) FixY = 1;
            if (checkBox3.Checked == true) FixZ = 1;
            if (checkBox4.Checked == true) FixRX = 1;
            if (checkBox5.Checked == true) FixRY = 1;
            if (checkBox6.Checked == true) FixRZ = 1;
            for (int i = 1; i < Joint.Number3d + 1; i++)
            {
                if (Joint.Selected[i] == 1)
                {
                    Joint.FixX[i] = FixX;
                    Joint.FixY[i] = FixY;
                    Joint.FixZ[i] = FixZ;
                    Joint.FixRX[i] = FixRX;
                    Joint.FixRY[i] = FixRY;
                    Joint.FixRZ[i] = FixRZ;
                }
            }

            for (int i = 1; i < Frame.Number + 1; i++)
            {
                ((MainForm)mainForm).FrameElement[i].Selected = 0;
            }
            for (int i = 1; i < Shell.Number + 1; i++)
            {
                for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                {
                    Shell.SelectedLine[i, j] = 0;
                }
            }
            for (int add = 1; add < Joint.Number3d + 1; add++)
            {
                Joint.Selected[add] = 0;
            }
            ((MainForm)mainForm).MakeTempFiles();
            DROWcls callmee = new DROWcls();
            callmee.Render2d();
            callmee.Render3d();
            //((MainForm)mainForm).pictureBox1.Refresh();
            //((MainForm)mainForm).pictureBox2.Refresh();


            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            checkBox4.Checked = true;
            checkBox5.Checked = true;
            checkBox6.Checked = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            checkBox4.Checked = false ;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = true;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
        }
    }
}
