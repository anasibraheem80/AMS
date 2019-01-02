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
    public partial class SectionDesignerForm : Form
    {
        public SectionDesignerForm()
        {
            InitializeComponent();
        }
        Form sectionDrawForm = Application.OpenForms["SectionDrawForm"];
        private void SectionDesignerForm_Load(object sender, EventArgs e)
        {
            try
            {
                Globals.secType = 1;
                int BarsNumber = ((SectionDrawForm)sectionDrawForm).BarsNumber;
                MSFlex2.RowCount = BarsNumber + 1;
                for (int i = 1; i < BarsNumber + 1; i++)
                {
                    MSFlex2.Rows[i - 1].Cells[0].Value = i;
                    MSFlex2.Rows[i - 1].Cells[1].Value = ((SectionDrawForm)sectionDrawForm).Bar[i].DR;
                    MSFlex2.Rows[i - 1].Cells[2].Value = Math.Round(((SectionDrawForm)sectionDrawForm).Bar[i].XR, 2);
                    MSFlex2.Rows[i - 1].Cells[3].Value = Math.Round(((SectionDrawForm)sectionDrawForm).Bar[i].YR, 2);
                }
                textBox5.Text = (((SectionDrawForm)sectionDrawForm).RecShape[1].Height/1000).ToString();
                textBox6.Text = (((SectionDrawForm)sectionDrawForm).RecShape[1].Width/1000).ToString();
            }
            catch { };
        }
        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox9.Text == "Circular")
            {
                Globals.secType = 2;
                textBox6.Visible = false;
                label6.Visible = false;
                label5.Text = "D";
            }
            if (comboBox9.Text == "Rectangular")
            {
                Globals.secType = 1;

                textBox6.Visible = true;
                label6.Visible = true;
                label5.Text = "H";
            }
        }
        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            Globals.UNfac = 0;
            Globals.UNfacL = 1;
            Globals.UNfacF = 1;
            Globals.UNfacSeg = 1;
            if (comboBox10.Text == " N-m")
            {
                Globals.UNfac = 0;
                Globals.UNfacL = 1;
                Globals.UNfacF = 1;
                Globals.UNfacSeg = 1;
            }
            if (comboBox10.Text == "N-mm")
            {
                Globals.UNfac = 1;
                Globals.UNfacL = 0.001;
                Globals.UNfacF = 1;
                Globals.UNfacSeg = 1000000;
            }
            if (comboBox10.Text == "N-cm")
            {
                Globals.UNfac = 2;
                Globals.UNfacL = 0.01;
                Globals.UNfacF = 1;
                Globals.UNfacSeg = 10000;
            }
            if (comboBox10.Text == " Kg-m")
            {
                Globals.UNfac = 3;
                Globals.UNfacL = 1;
                Globals.UNfacF = 9.81;
                Globals.UNfacSeg = 9.81;
            }
            if (comboBox10.Text == "Kg-cm")
            {
                Globals.UNfac = 4;
                Globals.UNfacL = 0.01;
                Globals.UNfacF = 9.81;
                Globals.UNfacSeg = 9.81 * 10000;
            }
            if (comboBox10.Text == "kg-mm")
            {
                Globals.UNfac = 5;
                Globals.UNfacL = 0.001;
                Globals.UNfacF = 9.81;
                Globals.UNfacSeg = 9.81 * 1000000;
            }
            if (comboBox10.Text == " ton-m")
            {
                Globals.UNfac = 6;
                Globals.UNfacL = 1;
                Globals.UNfacF = 9.81 * 1000;
                Globals.UNfacSeg = 9.81 * 1000;
            }
            if (comboBox10.Text == "ton-cm")
            {
                Globals.UNfac = 7;
                Globals.UNfacL = 0.01;
                Globals.UNfacF = 9.81 * 1000;
                Globals.UNfacSeg = 9.81 * 1000 * 10000;
            }
            if (comboBox10.Text == "ton-mm")
            {
                Globals.UNfac = 8;
                Globals.UNfacL = 0.001;
                Globals.UNfacF = 9.81 * 1000;
                Globals.UNfacSeg = 9.81 * 1000 * 1000000;
            }
        }
        private void comboBox10_Leave(object sender, EventArgs e)
        {
            Globals.UNfac = 0;
            Globals.UNfacL = 1;
            Globals.UNfacF = 1;
            Globals.UNfacSeg = 1;
            if (comboBox10.Text == " N-m")
            {
                Globals.UNfac = 0;
                Globals.UNfacL = 1;
                Globals.UNfacF = 1;
                Globals.UNfacSeg = 1;
            }
            if (comboBox10.Text == "N-mm")
            {
                Globals.UNfac = 1;
                Globals.UNfacL = 0.001;
                Globals.UNfacF = 1;
                Globals.UNfacSeg = 1000000;
            }
            if (comboBox10.Text == "N-cm")
            {
                Globals.UNfac = 2;
                Globals.UNfacL = 0.01;
                Globals.UNfacF = 1;
                Globals.UNfacSeg = 10000;
            }
            if (comboBox10.Text == " Kg-m")
            {
                Globals.UNfac = 3;
                Globals.UNfacL = 1;
                Globals.UNfacF = 9.81;
                Globals.UNfacSeg = 9.81;
            }
            if (comboBox10.Text == "Kg-cm")
            {
                Globals.UNfac = 4;
                Globals.UNfacL = 0.01;
                Globals.UNfacF = 9.81;
                Globals.UNfacSeg = 9.81 * 10000;
            }
            if (comboBox10.Text == "kg-mm")
            {
                Globals.UNfac = 5;
                Globals.UNfacL = 0.001;
                Globals.UNfacF = 9.81;
                Globals.UNfacSeg = 9.81 * 1000000;
            }
            if (comboBox10.Text == " ton-m")
            {
                Globals.UNfac = 6;
                Globals.UNfacL = 1;
                Globals.UNfacF = 9.81 * 1000;
                Globals.UNfacSeg = 9.81 * 1000;
            }
            if (comboBox10.Text == "ton-cm")
            {
                Globals.UNfac = 7;
                Globals.UNfacL = 0.01;
                Globals.UNfacF = 9.81 * 1000;
                Globals.UNfacSeg = 9.81 * 1000 * 10000;
            }
            if (comboBox10.Text == "ton-mm")
            {
                Globals.UNfac = 8;
                Globals.UNfacL = 0.001;
                Globals.UNfacF = 9.81 * 1000;
                Globals.UNfacSeg = 9.81 * 1000 * 1000000;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (Globals.secType == 1)
            {
                if (textBox5.Text != "") Globals.secH1 = Convert.ToDouble(textBox5.Text);
                if (textBox6.Text != "") Globals.secB1 = Convert.ToDouble(textBox6.Text);
                if (textBox7.Text != "") Globals.secC1 = Convert.ToDouble(textBox7.Text);
            }
            if (Globals.secType == 2)
            {
                if (textBox5.Text != "") Globals.secD1 = Convert.ToDouble(textBox5.Text);
                if (textBox7.Text != "") Globals.secC1 = Convert.ToDouble(textBox7.Text);
            }
           Material_properties theform = new Material_properties();
           theform.ShowDialog();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Globals.ASLn1 = MSFlex2.RowCount;
            if (textBox8.Text != "") Globals.fc01 = Convert.ToDouble(textBox8.Text);
            if (textBox5.Text != "") Globals.secH1 = Convert.ToDouble(textBox5.Text);
            if (textBox6.Text != "") Globals.secB1 = Convert.ToDouble(textBox6.Text);
            if (textBox7.Text != "") Globals.secC1 = Convert.ToDouble(textBox7.Text);
            if (textBox14.Text != "") Globals.fybar1 = Convert.ToDouble(textBox14.Text);
            if (textBox20.Text != "") Globals.ES1 = Convert.ToDouble(textBox20.Text);

            Globals.ASbar1 = new double[Globals.ASLn1, 5];

            for (int i = 0; i < Globals.ASLn1; i++)
            {
                Globals.ASbar1[i, 0] = i;
                Globals.ASbar1[i, 1] = Convert.ToDouble(MSFlex2.Rows[i].Cells[1].Value);
                Globals.ASbar1[i, 2] = Convert.ToDouble(MSFlex2.Rows[i].Cells[2].Value);
                Globals.ASbar1[i, 3] = Convert.ToDouble(MSFlex2.Rows[i].Cells[3].Value);
                Globals.dbar1 = Convert.ToDouble(MSFlex2.Rows[i].Cells[1].Value);
                Globals.ASbar1[i, 4] = Math.Pow(Globals.dbar1, 2) * Math.PI / 4;
            }

            Failure_Interaction_Surface theform = new Failure_Interaction_Surface();
            theform.ShowDialog();
        }
    }
}
