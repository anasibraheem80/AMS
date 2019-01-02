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
    public partial class JointLoadAssignmentForm : Form
    {
        public JointLoadAssignmentForm()
        {
            InitializeComponent();
        }
        int ifapplay = 0;
        Form mainForm = Application.OpenForms["MainForm"];
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ifapplay = 1;
            double PowerX = 0;
            double PowerY = 0;
            double PowerZ = 0;
            double MomentXX = 0;
            double MomentYY = 0;
            double MomentZZ = 0;
            if (textBox1.Text != "") PowerX = Convert.ToDouble(textBox1.Text);
            if (textBox2.Text != "") PowerY = Convert.ToDouble(textBox2.Text);
            if (textBox3.Text != "") PowerZ = Convert.ToDouble(textBox3.Text);
            if (textBox4.Text != "") MomentXX = Convert.ToDouble(textBox4.Text);
            if (textBox5.Text != "") MomentYY = Convert.ToDouble(textBox5.Text);
            if (textBox6.Text != "") MomentZZ = Convert.ToDouble(textBox6.Text);
            for (int i = 1; i < Joint.Number3d + 1; i++)
            {
                if (Joint.Selected[i] == 1)
                {
                    if (radioButton1.Checked == true)
                    {
                        Joint.PowerX[i] = Joint.PowerX[i]+PowerX;
                        Joint.PowerY[i] = Joint.PowerY[i]+PowerY;
                        Joint.PowerZ[i] = Joint.PowerZ[i]+PowerZ;
                        Joint.MomentXX[i] = Joint.MomentXX[i]+MomentXX;
                        Joint.MomentYY[i] = Joint.MomentYY[i]+MomentYY;
                        Joint.MomentZZ[i] = Joint.MomentZZ[i]+MomentZZ;
                    }
                    if (radioButton2.Checked == true)
                    {
                        Joint.PowerX[i] = PowerX;
                        Joint.PowerY[i] = PowerY;
                        Joint.PowerZ[i] = PowerZ;
                        Joint.MomentXX[i] = MomentXX;
                        Joint.MomentYY[i] = MomentYY;
                        Joint.MomentZZ[i] = MomentZZ;
                    }
                    if (radioButton3.Checked == true)
                    {
                        Joint.PowerX[i] = 0;
                        Joint.PowerY[i] = 0;
                        Joint.PowerZ[i] = 0;
                        Joint.MomentXX[i] = 0;
                        Joint.MomentYY[i] = 0;
                        Joint.MomentZZ[i] = 0;
                    }
                }
            }
            DROWcls callmee = new DROWcls();
            callmee.Render2d();
            callmee.Render3d();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (ifapplay == 0)
            {
                double PowerX = 0;
                double PowerY = 0;
                double PowerZ = 0;
                double MomentXX = 0;
                double MomentYY = 0;
                double MomentZZ = 0;
                if (textBox1.Text != "") PowerX = Convert.ToDouble(textBox1.Text);
                if (textBox2.Text != "") PowerY = Convert.ToDouble(textBox2.Text);
                if (textBox3.Text != "") PowerZ = Convert.ToDouble(textBox3.Text);
                if (textBox4.Text != "") MomentXX = Convert.ToDouble(textBox4.Text);
                if (textBox5.Text != "") MomentYY = Convert.ToDouble(textBox5.Text);
                if (textBox6.Text != "") MomentZZ = Convert.ToDouble(textBox6.Text);
                for (int i = 1; i < Joint.Number3d + 1; i++)
                {
                    if (Joint.Selected[i] == 1)
                    {
                        if (radioButton1.Checked == true)
                        {
                            Joint.PowerX[i] = Joint.PowerX[i] + PowerX;
                            Joint.PowerY[i] = Joint.PowerY[i] + PowerY;
                            Joint.PowerZ[i] = Joint.PowerZ[i] + PowerZ;
                            Joint.MomentXX[i] = Joint.MomentXX[i] + MomentXX;
                            Joint.MomentYY[i] = Joint.MomentYY[i] + MomentYY;
                            Joint.MomentZZ[i] = Joint.MomentZZ[i] + MomentZZ;
                        }
                        if (radioButton2.Checked == true)
                        {
                            Joint.PowerX[i] = PowerX;
                            Joint.PowerY[i] = PowerY;
                            Joint.PowerZ[i] = PowerZ;
                            Joint.MomentXX[i] = MomentXX;
                            Joint.MomentYY[i] = MomentYY;
                            Joint.MomentZZ[i] = MomentZZ;
                        }
                        if (radioButton3.Checked == true)
                        {
                            Joint.PowerX[i] = 0;
                            Joint.PowerY[i] = 0;
                            Joint.PowerZ[i] = 0;
                            Joint.MomentXX[i] = 0;
                            Joint.MomentYY[i] = 0;
                            Joint.MomentZZ[i] = 0;
                        }
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
                this.Close();
            }
        }
        private void JointLoadAssignmentForm_Load(object sender, EventArgs e)
        {
            if (Loads.Number > 0)
            {
                for (int i = 1; i < Loads.Number + 1; i++)
                {
                    comboBox1.Items.Add(Loads.Load[i]);
                }
                comboBox1.SelectedIndex = 0;
            }
        }
    }
}
