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
    public partial class FrameSectionReinforcementDataForm : Form
    {
        public FrameSectionReinforcementDataForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = Section.Selected;
            if(radioButton1.Checked ==true ) Section.DesignTyped[i] = 1;
            if (radioButton2.Checked == true) Section.DesignTyped[i] = 2;
            //Section.RebarMaterial1d[i] = comboBox1.SelectedIndex ;
            //Section.RebarMaterial2d[i] = comboBox2.SelectedIndex;
            Section.CoverTopd[i] =Convert.ToDouble (textBox1.Text) ;
            Section.CoverBottomd[i] = Convert.ToDouble(textBox2.Text);
            Section.ReinTopId[i] = Convert.ToDouble(textBox3.Text);
            Section.ReinTopJd[i] = Convert.ToDouble(textBox4.Text);
            Section.ReinBottomId[i] = Convert.ToDouble(textBox5.Text);
            Section.ReinBottomJd[i] = Convert.ToDouble(textBox6.Text);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void FrameSectionReinforcementDataForm_Load(object sender, EventArgs e)
        {
            int i = Section.Selected;
            if (Section.DesignTyped[i] == 1) radioButton1.Checked = true;
            if (Section.DesignTyped[i] == 2) radioButton2.Checked = true;
            //comboBox1.SelectedIndex = Section.RebarMaterial1d[i];
            //comboBox2.SelectedIndex = Section.RebarMaterial2d[i];
            textBox1.Text = Section.CoverTopd[i].ToString();
            textBox2.Text = Section.CoverBottomd[i].ToString();
            textBox3.Text = Section.ReinTopId[i].ToString();
            textBox4.Text = Section.ReinTopJd[i].ToString();
            textBox5.Text = Section.ReinBottomId[i].ToString();
            textBox6.Text = Section.ReinBottomJd[i].ToString();
        }

        private void groupBox9_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                groupBox3.Visible = false;
                groupBox4.Visible = false;
                groupBox5.Visible = true;
                groupBox6.Visible = true;
                groupBox7.Visible = true;
                groupBox8.Visible = true;
                groupBox9.Visible = true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                groupBox3.Visible = true;
                groupBox4.Visible = true;
                groupBox5.Visible = false;
                groupBox6.Visible = false;
                groupBox7.Visible = false;
                groupBox8.Visible = false;
                groupBox9.Visible = false;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true)
            {
                radioButton5.Enabled= false;
                radioButton6.Checked = true;
                textBox13.Visible = true;
                textBox16.Visible = true;
                label27.Visible = true;
                label29.Visible = true;

                label18.Visible = true;
                textBox7.Visible = true;

                label23.Visible = true;
                textBox11.Visible = true;
                label19.Visible = true;
                comboBox4.Visible = true;
                button6.Visible = true;
                label21.Text = "Number of Longitudinal Bars Along 3-dir Face";
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                radioButton5.Enabled = true;
                textBox13.Visible = false;
                textBox16.Visible = false;
                label27.Visible = false;
                label29.Visible = false;

                label18.Visible = false;
                textBox7.Visible = false;

                label23.Visible = false;
                textBox11.Visible = false;
                label19.Visible = false;
                comboBox4.Visible = false;
                button6.Visible = false;
                label21.Text = "Number of Longitudinal Bars";
            }
        }
    }
}
