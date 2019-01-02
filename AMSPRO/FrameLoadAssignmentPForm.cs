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
    public partial class FrameLoadAssignmentPForm : Form
    {
        public FrameLoadAssignmentPForm()
        {
            InitializeComponent();
        }
        int ifapplay = 0;
        Form mainForm = Application.OpenForms["MainForm"];
        private void FrameLoadAssignmentPForm_Load(object sender, EventArgs e)
        {
            if (Loads.Number > 0)
            {
                for (int i = 1; i < Loads.Number + 1; i++)
                {
                    comboBox1.Items.Add(Loads.Load[i]);
                }
                comboBox1.SelectedIndex = 0;
            }
            comboBox2.SelectedIndex = 5;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (ifapplay == 0)
            {
                double Power1 = 0;
                double Distace1 = 0;
                int TheType = 0;
                int Direction = 0;
                int Pattern = 0;
                if (textBox1.Text != "") Distace1 = Convert.ToDouble(textBox1.Text);
                if (textBox5.Text != "") Power1 = Convert.ToDouble(textBox5.Text);
                if (radioButton4.Checked == true) TheType = 1;
                if (radioButton5.Checked == true) TheType = 2;
                if (comboBox1.SelectedIndex >= 0) Pattern = comboBox1.SelectedIndex;
                if (comboBox2.SelectedIndex >= 0) Direction = comboBox2.SelectedIndex;
                for (int i = 1; i < Frame.Number + 1; i++)
                {
                    if (((MainForm)mainForm).FrameElement[i].Selected == 1)
                    {
                        if (radioButton1.Checked == true)//إضافة
                        {
                            ((MainForm)mainForm).FrameElement[i].LoadPNumber = ((MainForm)mainForm).FrameElement[i].LoadPNumber + 1;
                            int n = ((MainForm)mainForm).FrameElement[i].LoadPNumber;
                            ((MainForm)mainForm).FrameElement[i].LoadPValue[n] = Power1;
                            ((MainForm)mainForm).FrameElement[i].LoadPDistance[n] = Distace1;
                            ((MainForm)mainForm).FrameElement[i].LoadPType[n] = TheType;
                            ((MainForm)mainForm).FrameElement[i].LoadPDirection[n] = Direction;
                            ((MainForm)mainForm).FrameElement[i].LoadPPattern[n] = Pattern;
                        }
                        if (radioButton2.Checked == true)//تبديل
                        {
                            ((MainForm)mainForm).FrameElement[i].LoadPNumber = 1;
                            int n = ((MainForm)mainForm).FrameElement[i].LoadPNumber;
                            ((MainForm)mainForm).FrameElement[i].LoadPValue[n] = Power1;
                            ((MainForm)mainForm).FrameElement[i].LoadPDistance[n] = Distace1;
                            ((MainForm)mainForm).FrameElement[i].LoadPType[n] = TheType;
                            ((MainForm)mainForm).FrameElement[i].LoadPDirection[n] = Direction;
                            ((MainForm)mainForm).FrameElement[i].LoadPPattern[n] = Pattern;
                        }
                        if (radioButton3.Checked == true)//حذف
                        {
                            ((MainForm)mainForm).FrameElement[i].LoadPNumber = 0;
                            int n = ((MainForm)mainForm).FrameElement[i].LoadPNumber;
                            ((MainForm)mainForm).FrameElement[i].LoadPValue[n] = Power1;
                            ((MainForm)mainForm).FrameElement[i].LoadPDistance[n] = Distace1;
                            ((MainForm)mainForm).FrameElement[i].LoadPType[n] = TheType;
                            ((MainForm)mainForm).FrameElement[i].LoadPDirection[n] = Direction;
                            ((MainForm)mainForm).FrameElement[i].LoadPPattern[n] = Pattern;
                        }
                    }
                }
                ((MainForm)mainForm).MakeTempFiles();
                DROWcls callmee = new DROWcls();
                callmee.Render2d();
                callmee.Render3d();
                this.Close();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
