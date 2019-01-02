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
    public partial class Stress_Strain : Form
    {
        public Stress_Strain()
        {
            InitializeComponent();
        }

        private void Stress_Strain_Load(object sender, EventArgs e)
        {


            if (Globals.selectchart == 1)
            {
                if (Globals.StressType == 1)
                {
                    MSFlex1.RowCount = 22;
                    int i = 0;
                    int j = 0;
                    for (i = 0; i < 22; i++)
                    {
                        MSFlex1.Rows[i].Cells[0].Value = i + 1;
                        MSFlex1.Rows[i].Cells[1].Value = Math.Round(Globals.StressV[i], 2);
                        MSFlex1.Rows[i].Cells[2].Value = Math.Round(Globals.StrainV[i], 5);
                    }
                    button1.Visible = false;
                }

                if (Globals.StressType == 2)
                {
                    MSFlex1.RowCount = 22;
                    int i = 0;
                    int j = 0;
                    for (i = 0; i < 22; i++)
                    {
                        MSFlex1.Rows[i].Cells[0].Value = i + 1;
                        MSFlex1.Rows[i].Cells[1].Value = Math.Round(Globals.StressVc[i], 2);
                        MSFlex1.Rows[i].Cells[2].Value = Math.Round(Globals.StrainVc[i], 5);
                    }
                    button1.Visible = false;
                }

                if (Globals.StressType == 3)
                {

                    button1.Visible = true;
                }
            }


            if (Globals.selectchart == 2)
            {
                if (Globals.StressType == 1)
                {
                    MSFlex1.RowCount = 11;
                    int i = 0;
                    int j = 0;
                    for (i = 0; i < 11; i++)
                    {
                        MSFlex1.Rows[i].Cells[0].Value = i + 1;
                        MSFlex1.Rows[i].Cells[1].Value = Math.Round(Globals.StressVs[i], 2);
                        MSFlex1.Rows[i].Cells[2].Value = Math.Round(Globals.StrainVs[i], 5);
                    }
                    button1.Visible = false;
                }

                if (Globals.StressType == 2)
                {

                    button1.Visible = true;
                }
            }

       
        }

        private void button1_Click(object sender, EventArgs e)
        {



            Globals.StressVc = new double[MSFlex1.RowCount];
            Globals.StrainVc = new double[MSFlex1.RowCount];

            for (int i = 0; i < MSFlex1.RowCount; i++)
            {
                Globals.StressVc[i] = Convert.ToDouble(MSFlex1.Rows[i].Cells[1].Value);
                Globals.StrainVc[i] = Convert.ToDouble(MSFlex1.Rows[i].Cells[2].Value);
            }

            this.Close();

            //Material_properties.chart1.Series["unconfined"].Points.Clear();
            //this.chart1.Series["confined"].Points.Clear();
            //for (int k = 0; k < MSFlex1.RowCount; k++)
            //{
            //    this.chart1.Series["confined"].Points.AddXY(Globals.StrainVc[k], Globals.StressVc[k]);
           // }

            //this.Hide();
            


        }
    }
}
