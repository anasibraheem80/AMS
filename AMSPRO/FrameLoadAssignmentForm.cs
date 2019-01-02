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
    public partial class FrameLoadAssignmentForm : Form
    {
        public FrameLoadAssignmentForm()
        {
            InitializeComponent();
        }
        int ifapplay = 0;
        Form mainForm = Application.OpenForms["MainForm"];
        private void FrameLoadAssignmentForm_Load(object sender, EventArgs e)
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
        private void button1_Click(object sender, EventArgs e)
        {
            if (ifapplay == 0)
            {
                double Power1 = 0;
                double Power2 = 0;
                double Power3 = 0;
                double Power4 = 0;
                double Distace1 = 0;
                double Distace2 = 0;
                double Distace3 = 0;
                double Distace4 = 0;
                int TheType = 0;
                double Uniform = 0;
                int Direction = 0;
                int Pattern = 0;
                if (textBox1.Text != "") Distace1 = Convert.ToDouble(textBox1.Text);
                if (textBox2.Text != "") Distace2 = Convert.ToDouble(textBox2.Text);
                if (textBox3.Text != "") Distace3 = Convert.ToDouble(textBox3.Text);
                if (textBox4.Text != "") Distace4 = Convert.ToDouble(textBox4.Text);
                if (textBox5.Text != "") Power1 = Convert.ToDouble(textBox5.Text);
                if (textBox6.Text != "") Power2 = Convert.ToDouble(textBox6.Text);
                if (textBox7.Text != "") Power3 = Convert.ToDouble(textBox7.Text);
                if (textBox8.Text != "") Power4 = Convert.ToDouble(textBox8.Text);
                if (textBox9.Text != "") Uniform = Convert.ToDouble(textBox9.Text);
                if (radioButton4.Checked == true) TheType = 1;
                if (radioButton5.Checked == true) TheType = 2;
                if (comboBox1.SelectedIndex >= 0) Pattern = comboBox1.SelectedIndex;
                if (comboBox2.SelectedIndex >= 0) Direction = comboBox2.SelectedIndex;
                if (Power1 == 0 & Power2 == 0 & Power3 == 0 & Power4 == 0 & Uniform == 0 & radioButton3.Checked != true) goto endloop;
                for (int i = 1; i < Frame.Number + 1; i++)
                {
                    if (((MainForm)mainForm).FrameElement[i].Selected == 1)
                    {
                        if (radioButton1.Checked == true)//إضافة
                        {
                            ((MainForm)mainForm).FrameElement[i].LoadDNumber = ((MainForm)mainForm).FrameElement[i].LoadDNumber+1;
                            int n = ((MainForm)mainForm).FrameElement[i].LoadDNumber;
                            ((MainForm)mainForm).FrameElement[i].LoadDValue1[n] = Power1;
                            ((MainForm)mainForm).FrameElement[i].LoadDValue2[n] = Power2;
                            ((MainForm)mainForm).FrameElement[i].LoadDValue3[n] = Power3;
                            ((MainForm)mainForm).FrameElement[i].LoadDValue4[n] = Power4;
                            ((MainForm)mainForm).FrameElement[i].LoadDDistance1[n] = Distace1;
                            ((MainForm)mainForm).FrameElement[i].LoadDDistance2[n] = Distace2;
                            ((MainForm)mainForm).FrameElement[i].LoadDDistance3[n] = Distace3;
                            ((MainForm)mainForm).FrameElement[i].LoadDDistance4[n] = Distace4;
                            ((MainForm)mainForm).FrameElement[i].LoadDType[n] = TheType;
                            ((MainForm)mainForm).FrameElement[i].LoadDUniform[n] = Uniform;
                            ((MainForm)mainForm).FrameElement[i].LoadDDirection[n] = Direction;
                            ((MainForm)mainForm).FrameElement[i].LoadDPattern[n] = Pattern;
                        }
                        if (radioButton2.Checked == true)//تبديل
                        {
                            ((MainForm)mainForm).FrameElement[i].LoadDNumber =  1;
                            int n = ((MainForm)mainForm).FrameElement[i].LoadDNumber;
                            ((MainForm)mainForm).FrameElement[i].LoadDValue1[n] = Power1;
                            ((MainForm)mainForm).FrameElement[i].LoadDValue2[n] = Power2;
                            ((MainForm)mainForm).FrameElement[i].LoadDValue3[n] = Power3;
                            ((MainForm)mainForm).FrameElement[i].LoadDValue4[n] = Power4;
                            ((MainForm)mainForm).FrameElement[i].LoadDDistance1[n] = Distace1;
                            ((MainForm)mainForm).FrameElement[i].LoadDDistance2[n] = Distace2;
                            ((MainForm)mainForm).FrameElement[i].LoadDDistance3[n] = Distace3;
                            ((MainForm)mainForm).FrameElement[i].LoadDDistance4[n] = Distace4;
                            ((MainForm)mainForm).FrameElement[i].LoadDType[n] = TheType;
                            ((MainForm)mainForm).FrameElement[i].LoadDUniform[n] = Uniform;
                            ((MainForm)mainForm).FrameElement[i].LoadDDirection[n] = Direction;
                            ((MainForm)mainForm).FrameElement[i].LoadDPattern[n] = Pattern;
                        }
                        if (radioButton3.Checked == true)//حذف
                        {
                            ((MainForm)mainForm).FrameElement[i].LoadDNumber = 0;
                            int n = ((MainForm)mainForm).FrameElement[i].LoadDNumber;
                            ((MainForm)mainForm).FrameElement[i].LoadDValue1[n] = Power1;
                            ((MainForm)mainForm).FrameElement[i].LoadDValue2[n] = Power2;
                            ((MainForm)mainForm).FrameElement[i].LoadDValue3[n] = Power3;
                            ((MainForm)mainForm).FrameElement[i].LoadDValue4[n] = Power4;
                            ((MainForm)mainForm).FrameElement[i].LoadDDistance1[n] = Distace1;
                            ((MainForm)mainForm).FrameElement[i].LoadDDistance2[n] = Distace2;
                            ((MainForm)mainForm).FrameElement[i].LoadDDistance3[n] = Distace3;
                            ((MainForm)mainForm).FrameElement[i].LoadDDistance4[n] = Distace4;
                            ((MainForm)mainForm).FrameElement[i].LoadDType[n] = TheType;
                            ((MainForm)mainForm).FrameElement[i].LoadDUniform[n] = Uniform;
                            ((MainForm)mainForm).FrameElement[i].LoadDDirection[n] = Direction;
                            ((MainForm)mainForm).FrameElement[i].LoadDPattern[n] = Pattern;
                        }
                    }
                }
                ((MainForm)mainForm).MakeTempFiles();
                DROWcls callmee = new DROWcls();
                callmee.Render2d();
                callmee.Render3d();
            endloop: { }
                this.Close();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
