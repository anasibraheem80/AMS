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
    public partial class Material_properties : Form
    {
        public Material_properties()
        {
            InitializeComponent();
        }
        Form sectionWallForm = Application.OpenForms["SectionWallForm"];
        Form sectionBarsPropertyForm = Application.OpenForms["SectionBarsPropertyForm"];
        Form sectionDrawForm = Application.OpenForms["SectionDrawForm"];


        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            // Globals.UNfac = 0;
            // Globals.UNfacL = 1;
            // Globals.UNfacF = 1;
            // Globals.UNfacSeg = 1;


            double strainC0 = 0;
            double strainCu = 0;
            double strainfact = 0;
            double fc0 = 0;
            //====================================caculate unconfined Stress- Strain Diagram
            double x1 = 0;
            //double strain0 = 0;
            double Esec = 0;
            double r1 = 0;
            double x = 0;
            Globals.StressV = new double[22];
            Globals.StrainV = new double[22];

            this.chart1.Series["unconfined"].Points.Clear();
            if (textBox11.Text != "") strainC0 = Convert.ToDouble(textBox11.Text);
            if (textBox10.Text != "") strainCu = Convert.ToDouble(textBox10.Text);
            if (textBox9.Text != "") strainfact = Convert.ToDouble(textBox9.Text);
            if (textBox8.Text != "") fc0 = Globals.UNfacSeg * Convert.ToDouble(textBox8.Text);
            if (textBox3.Text != "") Globals.ET = Globals.UNfacSeg * Convert.ToDouble(textBox3.Text);

            if (Globals.secType == 0)
            {
                //strain0 = 2 * fc0 / Globals.ET;
                //x1 = strainCu / 100;
                x1 = 2 * strainC0 / 20;
                Globals.StressV[0] = 0;
                Globals.StrainV[0] = 0;

                // for (int k = 1; k < 19; k++)
                //{
                //    Globals.StrainV[k] = k * x1;
                //    Globals.StressV[k] = fc0 * (2 * Globals.StrainV[k] / strain0 - Math.Pow(Globals.StrainV[k] / strain0, 2));
                // }

                Esec = fc0 / strainC0;
                r1 = Globals.ET / (Globals.ET - Esec);

                for (int k = 1; k < 21; k++)
                {
                    Globals.StrainV[k] = k * x1;
                    x = Globals.StrainV[k] / strainC0;
                    Globals.StressV[k] = fc0 * r1 * x / (r1 - 1 + Math.Pow(x, r1));
                }

                Globals.StrainV[21] = strainCu;
                Globals.StressV[21] = 0;

                for (int k = 0; k < 22; k++)
                {
                    this.chart1.Series["unconfined"].Points.AddXY(Globals.StrainV[k], Globals.StressV[k]);
                }

            }


            ///======confined
            ///
            double stirrupS = 0;
            double Rfactor = 0;
            double strainSu = 0;
            double fyh = 0;

            double ASLn = 0;
            double ASLd = 0;
            double ASLA = 0;
            double ASTXn = 0;
            double ASTXd = 0;
            double ASTYn = 0;
            double ASTYd = 0;
            double ASTXA = 0;
            double ASTYA = 0;

            if (textBox12.Text != "") fyh = Convert.ToDouble(textBox12.Text);
            if (textBox13.Text != "") strainSu = Convert.ToDouble(textBox13.Text);
            if (comboBox3.Text != "") ASTXn = Convert.ToDouble(comboBox3.Text);
            if (comboBox4.Text != "") ASTYn = Convert.ToDouble(comboBox4.Text);
            if (comboBox7.Text != "") ASTXd = Convert.ToDouble(comboBox7.Text);
            if (comboBox8.Text != "") ASTYd = Convert.ToDouble(comboBox8.Text);
            if (textBox1.Text != "") stirrupS = Convert.ToDouble(textBox1.Text);
            if (textBox2.Text != "") Rfactor = Convert.ToDouble(textBox2.Text);
            if (comboBox1.Text != "") ASLd = Convert.ToDouble(comboBox1.Text);
            if (comboBox2.Text != "") ASLn = Convert.ToDouble(comboBox2.Text);
            double ASTd = 0;
            double ASTn = 0;
            double ASTA = 0;
            if (comboBox7.Text != "") ASTd = Convert.ToDouble(comboBox7.Text);
            if (comboBox3.Text != "") ASTn = Convert.ToDouble(comboBox3.Text);

            //==================confined - Rectangular section
            if (Globals.secType == 1)
            {
                this.chart1.Series["confined"].Points.Clear();

                double secH = 0;
                double secB = 0;
                double secC = 0;
                double RoX = 0;
                double RoY = 0;
                double RoS = 0;
                double hx = 0;
                double hy = 0;
                double Rcc = 0;
                double ss = 0;
                double wi = 0;
                double ke = 0;
                double sum = 0;
                double flx = 0;
                double fly = 0;
                double fll = 0;
                double fcc = 0;
                double Kratio = 0;
                double strainCC = 0;
                double strainCuc = 0;
                double x2 = 0;
                double x3 = 0;

                Globals.StressVc = new double[22];
                Globals.StrainVc = new double[22];

                label3.Visible = true;
                label4.Visible = true;
                label31.Visible = true;
                label24.Visible = true;
                label25.Visible = true;
                label26.Visible = true;
                label27.Visible = true;
                label28.Visible = true;
                label29.Visible = true;
                label30.Visible = true;
                secH = Globals.secH1;
                secB = Globals.secB1;
                secC = Globals.secC1;


                //========start
                ASLA = Math.Pow(ASLd, 2) * Math.PI / 4;
                ASTXA = Math.Pow(ASTXd, 2) * Math.PI / 4;
                ASTYA = Math.Pow(ASTYd, 2) * Math.PI / 4;

                hx = secB - secC * 2 - ASTYd / 1000;
                hy = secH - secC * 2 - ASTXd / 1000;

                RoX = ASTXn * (ASTXA / 1000000) / (stirrupS * hy);
                RoY = ASTYn * (ASTYA / 1000000) / (stirrupS * hx);
                RoS = RoX + RoY;

                Rcc = ASLn * ASLA / (hx * hy * 1000000);
                ss = stirrupS - ASTXd / 1000;
                wi = (hx + hy) * 2 / ASLn;
                sum = Math.Pow(wi, 2) * ASLn;

                ke = (1 - sum / (6 * hx * hy)) * (1 - ss / (2 * hx)) * (1 - ss / (2 * hy)) / (1 - Rcc);
                //ke = 0.75;

                flx = ke * fyh * RoX;
                fly = ke * fyh * RoY;
                fll = ke * 0.5 * RoS * fyh / fc0;

                fcc = fc0 * (2.254 * Math.Pow(1 + 7.94 * fll, 0.5) - 2 * fll - 1.254);
                Kratio = fcc / fc0;
                strainCC = strainC0 * (1 + Rfactor * (Kratio - 1));
                strainCuc = 0.004 + 1.4 * RoS * fyh * strainSu / fcc;
                Esec = fcc / strainCC;
                r1 = Globals.ET / (Globals.ET - Esec);

                x2 = strainCuc / 21;

                Globals.StrainVc[0] = 0;
                Globals.StressVc[0] = 0;

                for (int k = 1; k < 22; k++)
                {
                    Globals.StrainVc[k] = k * x2;
                    x3 = Globals.StrainVc[k] / strainCC;
                    Globals.StressVc[k] = fcc * x3 * r1 / (r1 - 1 + Math.Pow(x3, r1));
                }

                label26.Text = Convert.ToString(Math.Round(strainCuc, 5));
                label27.Text = Convert.ToString(Math.Round(fcc, 2));
                label28.Text = Convert.ToString(Math.Round(strainCC, 5));
                label29.Text = Convert.ToString(Math.Round(Esec, 2));
                label30.Text = Convert.ToString(Math.Round(Globals.StressVc[21], 2));

                for (int k = 0; k < 22; k++)
                {
                    this.chart1.Series["confined"].Points.AddXY(Globals.StrainVc[k], Globals.StressVc[k]);
                }

                if (strainCuc < 0.01)
                {
                    chart1.ChartAreas[0].AxisX.Maximum = 0.01;
                }
                if (strainCuc < 0.02 && strainCuc >= 0.01)
                {
                    chart1.ChartAreas[0].AxisX.Maximum = 0.02;
                }
                if (strainCuc < 0.03 && strainCuc >= 0.02)
                {
                    chart1.ChartAreas[0].AxisX.Maximum = 0.03;
                }

                if (strainCuc < 0.04 && strainCuc >= 0.03)
                {
                    chart1.ChartAreas[0].AxisX.Maximum = 0.04;
                }

                if (strainCuc < 0.05 && strainCuc >= 0.04)
                {
                    chart1.ChartAreas[0].AxisX.Maximum = 0.05;
                }

                if (strainCuc < 0.06 && strainCuc >= 0.05)
                {
                    chart1.ChartAreas[0].AxisX.Maximum = 0.06;
                }

                if (strainCuc < 0.07 && strainCuc >= 0.06)
                {
                    chart1.ChartAreas[0].AxisX.Maximum = 0.07;
                }
            }



            //====================================caculate confined Stress- Strain Diagram for Circular section
            if (Globals.secType == 2)
            {
                this.chart1.Series["confined"].Points.Clear();
                double secD = 0;
                double secC = 0;
                double RoS = 0;
                double ds = 0;
                double fll = 0;
                double fcc = 0;
                double Kratio = 0;
                double strainCC = 0;
                double strainCuc = 0;
                double x2 = 0;
                double x3 = 0;
                double ke = 0;
                double ss = 0;
                double Rcc = 0;
                Globals.StressVc = new double[22];
                Globals.StrainVc = new double[22];


                label3.Visible = true;
                label4.Visible = true;
                label31.Visible = true;
                label24.Visible = true;
                label25.Visible = true;
                label26.Visible = true;
                label27.Visible = true;
                label28.Visible = true;
                label29.Visible = true;
                label30.Visible = true;

                secD = Globals.secD1;
                secC = Globals.secC1;




                //========start
                ASLA = Math.Pow(ASLd, 2) * Math.PI / 4;
                ASTA = Math.Pow(ASTd, 2) * Math.PI / 4;
                ds = secD - secC * 2 - ASTd / 1000;
                RoS = 4 * ASTA / (ds * stirrupS * 1000000);
                Rcc = ASLA * ASLn / (Math.Pow(ds, 2) * Math.PI / 4 * 1000000);
                ss = stirrupS - ASTd / 1000;

                if (comboBox5.Text == "Hoops")
                {
                    ke = Math.Pow((1 - ss / (2 * ds)), 2) / (1 - Rcc);
                }
                if (comboBox5.Text == "Spiral")
                {
                    ke = (1 - ss / (2 * ds)) / (1 - Rcc);
                }



                fll = ke * 0.5 * RoS * fyh / fc0;
                fcc = fc0 * (2.254 * Math.Pow(1 + 7.94 * fll, 0.5) - 2 * fll - 1.254);
                Kratio = fcc / fc0;
                strainCC = strainC0 * (1 + Rfactor * (Kratio - 1));
                strainCuc = 0.004 + 1.4 * RoS * fyh * strainSu / fcc;
                Esec = fcc / strainCC;
                r1 = Globals.ET / (Globals.ET - Esec);

                x2 = strainCuc / 21;

                Globals.StrainVc[0] = 0;
                Globals.StressVc[0] = 0;

                for (int k = 1; k < 22; k++)
                {
                    Globals.StrainVc[k] = k * x2;
                    x3 = Globals.StrainVc[k] / strainCC;
                    Globals.StressVc[k] = fcc * x3 * r1 / (r1 - 1 + Math.Pow(x3, r1));
                }

                label26.Text = Convert.ToString(Math.Round(strainCuc, 5));
                label27.Text = Convert.ToString(Math.Round(fcc, 2));
                label28.Text = Convert.ToString(Math.Round(strainCC, 5));
                label29.Text = Convert.ToString(Math.Round(Esec, 2));
                label30.Text = Convert.ToString(Math.Round(Globals.StressVc[21], 2));

                for (int k = 0; k < 22; k++)
                {
                    this.chart1.Series["confined"].Points.AddXY(Globals.StrainVc[k], Globals.StressVc[k]);
                }


                if (strainCuc < 0.01)
                {
                    chart1.ChartAreas[0].AxisX.Maximum = 0.01;
                }
                if (strainCuc < 0.02 && strainCuc >= 0.01)
                {
                    chart1.ChartAreas[0].AxisX.Maximum = 0.02;
                }
                if (strainCuc < 0.03 && strainCuc >= 0.02)
                {
                    chart1.ChartAreas[0].AxisX.Maximum = 0.03;
                }

                if (strainCuc < 0.04 && strainCuc >= 0.03)
                {
                    chart1.ChartAreas[0].AxisX.Maximum = 0.04;
                }

                if (strainCuc < 0.05 && strainCuc >= 0.04)
                {
                    chart1.ChartAreas[0].AxisX.Maximum = 0.05;
                }

                if (strainCuc < 0.06 && strainCuc >= 0.05)
                {
                    chart1.ChartAreas[0].AxisX.Maximum = 0.06;
                }

                if (strainCuc < 0.07 && strainCuc >= 0.06)
                {
                    chart1.ChartAreas[0].AxisX.Maximum = 0.07;
                }
            }



        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            int checkedV = 2;

            if (checkBox1.Checked)
            {
                checkedV = 1;
            }


            if (checkedV == 1)
            {
                double strainC0 = 0;
                double strainCu = 0;
                double strainfact = 0;
                double fc0 = 0;
                //====================================caculate unconfined Stress- Strain Diagram
                double x1 = 0;
                //double strain0 = 0;
                double Esec = 0;
                double r1 = 0;
                double x = 0;
                //double[] fcb = new double[15];
                // double[] x = new double[15];

                Globals.StressV = new double[22];
                Globals.StrainV = new double[22];

                this.chart1.Series["unconfined"].Points.Clear();
                if (textBox11.Text != "") strainC0 = Convert.ToDouble(textBox11.Text);
                if (textBox10.Text != "") strainCu = Convert.ToDouble(textBox10.Text);
                if (textBox9.Text != "") strainfact = Convert.ToDouble(textBox9.Text);
                if (textBox8.Text != "") fc0 = Globals.UNfacSeg * Convert.ToDouble(textBox8.Text);
                if (textBox3.Text != "") Globals.ET = Globals.UNfacSeg * Convert.ToDouble(textBox3.Text);


                //strain0 = 2 * fc0 / Globals.ET;
                //x1 = strainCu / 100;
                x1 = 2 * strainC0 / 20;
                Globals.StressV[0] = 0;
                Globals.StrainV[0] = 0;

                // for (int k = 1; k < 19; k++)
                //{
                //    Globals.StrainV[k] = k * x1;
                //    Globals.StressV[k] = fc0 * (2 * Globals.StrainV[k] / strain0 - Math.Pow(Globals.StrainV[k] / strain0, 2));
                // }

                Esec = fc0 / strainC0;
                r1 = Globals.ET / (Globals.ET - Esec);

                for (int k = 1; k < 21; k++)
                {
                    Globals.StrainV[k] = k * x1;
                    x = Globals.StrainV[k] / strainC0;
                    Globals.StressV[k] = fc0 * r1 * x / (r1 - 1 + Math.Pow(x, r1));
                }

                Globals.StrainV[21] = strainCu;
                Globals.StressV[21] = 0;

                for (int k = 0; k < 22; k++)
                {
                    this.chart1.Series["unconfined"].Points.AddXY(Globals.StrainV[k], Globals.StressV[k]);
                }



            }

            if (checkedV == 2)
            {
                this.chart1.Series["unconfined"].Points.Clear();
            }

        }

        private void Material_properties_Load(object sender, EventArgs e)
        {
            Globals.UNfac = 0;
            Globals.UNfacL = 1;
            Globals.UNfacF = 1;
            Globals.UNfacSeg = 1;

            if (comboBox9.Text == " N-m")
            {
                Globals.UNfac = 0;
                Globals.UNfacL = 1;
                Globals.UNfacF = 1;
                Globals.UNfacSeg = 1;
            }

            if (comboBox9.Text == "N-mm")
            {
                Globals.UNfac = 1;
                Globals.UNfacL = 0.001;
                Globals.UNfacF = 1;
                Globals.UNfacSeg = 1000000;
            }
            if (comboBox9.Text == "N-cm")
            {
                Globals.UNfac = 2;
                Globals.UNfacL = 0.01;
                Globals.UNfacF = 1;
                Globals.UNfacSeg = 10000;
            }


            if (comboBox9.Text == " Kg-m")
            {
                Globals.UNfac = 3;
                Globals.UNfacL = 1;
                Globals.UNfacF = 9.81;
                Globals.UNfacSeg = 9.81;
            }

            if (comboBox9.Text == "Kg-cm")
            {
                Globals.UNfac = 4;
                Globals.UNfacL = 0.01;
                Globals.UNfacF = 9.81;
                Globals.UNfacSeg = 9.81 * 10000;
            }
            if (comboBox9.Text == "kg-mm")
            {
                Globals.UNfac = 5;
                Globals.UNfacL = 0.001;
                Globals.UNfacF = 9.81;
                Globals.UNfacSeg = 9.81 * 1000000;
            }

            if (comboBox9.Text == " ton-m")
            {
                Globals.UNfac = 6;
                Globals.UNfacL = 1;
                Globals.UNfacF = 9.81 * 1000;
                Globals.UNfacSeg = 9.81 * 1000;
            }

            if (comboBox9.Text == "ton-cm")
            {
                Globals.UNfac = 7;
                Globals.UNfacL = 0.01;
                Globals.UNfacF = 9.81 * 1000;
                Globals.UNfacSeg = 9.81 * 1000 * 10000;
            }
            if (comboBox9.Text == "ton-mm")
            {
                Globals.UNfac = 8;
                Globals.UNfacL = 0.001;
                Globals.UNfacF = 9.81 * 1000;
                Globals.UNfacSeg = 9.81 * 1000 * 1000000;
            }




            ///=========
            comboBox2.Text = Convert.ToString(Globals.ASLn1);
            comboBox1.Text = Convert.ToString(Globals.dbar1);

            int O = ((SectionDrawForm)sectionDrawForm).SelectedType;

            if (O == 5)  ////rectangular single
            {
                //Globals.secType = 1;
                comboBox4.Visible = true;
                comboBox8.Visible = true;
                label11.Visible = true;
                label10.Text = "AST x (//Y)";
                comboBox5.Items[0] = "Ties";
                comboBox5.Items[1] = "";
                comboBox7.Text = ((SectionBarsPropertyForm)sectionBarsPropertyForm).textBox19.Text;
                comboBox8.Text = ((SectionBarsPropertyForm)sectionBarsPropertyForm).textBox19.Text;
                comboBox12.Enabled = false;
                comboBox12.Text = "One segment";
            }

            if (O == 7)  ////circle single
            {
                // Globals.secType = 2;
                comboBox4.Visible = false;
                comboBox8.Visible = false;
                label11.Visible = false;
                label10.Text = "AST";
                comboBox12.Enabled = false;
                comboBox12.Text = "One segment";

                comboBox5.Items[0] = "Hoops";
                comboBox5.Items[1] = "Spiral";
                comboBox7.Text = ((SectionBarsPropertyForm)sectionBarsPropertyForm).textBox65.Text;


            }

            if (O == 10)  ////flanged wall
            {
                //  Globals.secType = 1;
                if (Globals.Secsegment == 0)
                {
                    comboBox12.Text = "web";
                    comboBox12.Enabled = true;
                    comboBox7.Text = ((SectionWallForm)sectionWallForm).textBox19.Text;
                    comboBox8.Text = ((SectionWallForm)sectionWallForm).textBox19.Text;
                }
                if (Globals.Secsegment == 1)
                {
                    comboBox12.Text = " Left Flange";
                    comboBox12.Enabled = true;
                    comboBox7.Text = ((SectionWallForm)sectionWallForm).textBox15.Text;
                    comboBox8.Text = ((SectionWallForm)sectionWallForm).textBox15.Text;
                }
                if (Globals.Secsegment == 2)
                {
                    comboBox12.Text = "Right Flange";
                    comboBox12.Enabled = true;
                    comboBox7.Text = ((SectionWallForm)sectionWallForm).textBox69.Text;
                    comboBox8.Text = ((SectionWallForm)sectionWallForm).textBox69.Text;

                }
                if (Globals.Secsegment == 3)
                {
                    comboBox12.Text = "One segment";
                    comboBox12.Enabled = false;
                }



            }




        }

        private void printReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Globals.selectchart = 1;

            if (comboBox6.Text == "Unconfined")
            {
                Globals.StressType = 1;
            }
            if (comboBox6.Text == "Mander-confined")
            {
                Globals.StressType = 2;
            }
            if (comboBox6.Text == "user define")
            {
                Globals.StressType = 3;
            }

            Stress_Strain theform = new Stress_Strain();
            theform.ShowDialog();

        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            double fc0 = 0;
            double wb = 0;

            if (textBox8.Text != "") fc0 = Globals.UNfacSeg * Convert.ToDouble(textBox8.Text);
            if (textBox4.Text != "") wb = Globals.UNfacSeg * Convert.ToDouble(textBox4.Text) / Globals.UNfacL;

            if (comboBox9.Text == "Ec=5000Sqrt(f'c)")
            {
                Globals.ET = Globals.UNfacSeg * 5000 * Math.Pow(fc0 / 1000000, 0.5);
                textBox3.Enabled = false;
                textBox3.Text = Globals.ET.ToString();

            }

            if (comboBox9.Text == "Ec=4700Sqrt(f'c)")
            {
                Globals.ET = Globals.UNfacSeg * 4700 * Math.Pow(fc0 / 1000000, 0.5);
                textBox3.Enabled = false;
                textBox3.Text = Globals.ET.ToString();
            }

            if (comboBox9.Text == "Ec=33(w^1.5)Sqrt(f'c)")
            {
                Globals.ET = Globals.UNfacSeg * 33 * Math.Pow(wb / 9.81, 1.5) * Math.Pow(fc0 / 1000000, 0.5);
                textBox3.Enabled = false;
                textBox3.Text = Globals.ET.ToString();
            }
            if (comboBox9.Text == "User define")
            {
                textBox3.Enabled = true;
                Globals.ET = Globals.UNfacSeg * Convert.ToDouble(textBox3.Text);
            }
        }

        private void comboBox9_Leave(object sender, EventArgs e)
        {

            double fc0 = 0;
            double wb = 0;

            if (textBox8.Text != "") fc0 = Globals.UNfacSeg * Convert.ToDouble(textBox8.Text);
            if (textBox4.Text != "") wb = Globals.UNfacSeg * Convert.ToDouble(textBox4.Text) / Globals.UNfacL;

            if (comboBox9.Text == "Ec=5000Sqrt(f'c)")
            {
                Globals.ET = Globals.UNfacSeg * 5000 * Math.Pow(fc0 / 1000000, 0.5);
                textBox3.Enabled = false;
                textBox3.Text = Globals.ET.ToString();

            }

            if (comboBox9.Text == "Ec=4700Sqrt(f'c)")
            {
                Globals.ET = Globals.UNfacSeg * 4700 * Math.Pow(fc0 / 1000000, 0.5);
                textBox3.Enabled = false;
                textBox3.Text = Globals.ET.ToString();
            }

            if (comboBox9.Text == "Ec=33(w^1.5)Sqrt(f'c)")
            {
                Globals.ET = Globals.UNfacSeg * 33 * Math.Pow(wb / 9.81, 1.5) * Math.Pow(fc0 / 1000000, 0.5);
                textBox3.Enabled = false;
                textBox3.Text = Globals.ET.ToString();
            }
            if (comboBox9.Text == "User define")
            {
                textBox3.Enabled = true;
                Globals.ET = Globals.UNfacSeg * Convert.ToDouble(textBox3.Text);
            }


        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

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

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.chart2.Series["S-Stress-Strain"].Points.Clear();
            double strainSy = 0;
            double strainSh = 0;
            double strainSu = 0;
            double fybar = 0;
            double fybaru = 0;
            double m1 = 0;
            double r1 = 0;
            double x = 0;
            Globals.StressVs = new double[11];
            Globals.StrainVs = new double[11];
            if (textBox13.Text != "") strainSu = Convert.ToDouble(textBox13.Text);
            if (textBox6.Text != "") strainSh = Convert.ToDouble(textBox6.Text);
            if (textBox5.Text != "") strainSy = Convert.ToDouble(textBox5.Text);
            if (textBox14.Text != "") fybar = Convert.ToDouble(textBox14.Text);
            if (textBox15.Text != "") fybaru = Convert.ToDouble(textBox15.Text);

            Globals.StressVs[0] = 0;
            Globals.StrainVs[0] = 0;

            Globals.StressVs[1] = fybar;
            Globals.StrainVs[1] = strainSy;

            Globals.StressVs[2] = fybar;
            Globals.StrainVs[2] = strainSh;

            r1 = strainSu - strainSh;
            m1 = ((fybaru / fybar) * Math.Pow(30 * r1 + 1, 2) - 60 * r1 - 1) / (15 * Math.Pow(r1, 2));

            x = r1 / 8;
            for (int k = 3; k < 11; k++)
            {
                Globals.StrainVs[k] = (k - 2) * x + x;
                Globals.StressVs[k] = fybar * ((m1 * (Globals.StrainVs[k] - strainSh) + 2) / (60 * (Globals.StrainVs[k] - strainSh) + 2) + (Globals.StrainVs[k] - strainSh) * (60 - m1) / (2 * Math.Pow(30 * r1 + 1, 2)));
            }


            for (int k = 0; k < 11; k++)
            {
                this.chart2.Series["S-Stress-Strain"].Points.AddXY(Globals.StrainVs[k], Globals.StressVs[k]);
            }


        }

        private void button4_Click(object sender, EventArgs e)
        {
            Globals.selectchart = 2;

            if (comboBox11.Text == "Park")
            {
                Globals.StressType = 1;
            }

            if (comboBox6.Text == "user define")
            {
                Globals.StressType = 2;
            }

            Stress_Strain theform = new Stress_Strain();
            theform.ShowDialog();

        }

        private void comboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (comboBox12.Text == "Left Flange")
            {
                Globals.secH1 = Convert.ToDouble(((SectionWallForm)sectionWallForm).textBox6.Text) / 1000;
                Globals.secB1 = Convert.ToDouble(((SectionWallForm)sectionWallForm).textBox5.Text) / 1000;
                Globals.secC1 = Convert.ToDouble(((SectionWallForm)sectionWallForm).textBox63.Text) / 1000;
                comboBox7.Text = ((SectionWallForm)sectionWallForm).textBox15.Text;
                comboBox8.Text = ((SectionWallForm)sectionWallForm).textBox15.Text;
                Globals.Secsegment = 1;

            }

            if (comboBox12.Text == "Right Flange")
            {
                Globals.secH1 = Convert.ToDouble(((SectionWallForm)sectionWallForm).textBox8.Text) / 1000;
                Globals.secB1 = Convert.ToDouble(((SectionWallForm)sectionWallForm).textBox9.Text) / 1000;
                Globals.secC1 = Convert.ToDouble(((SectionWallForm)sectionWallForm).textBox92.Text) / 1000;
                comboBox7.Text = ((SectionWallForm)sectionWallForm).textBox69.Text;
                comboBox8.Text = ((SectionWallForm)sectionWallForm).textBox69.Text;

                Globals.Secsegment = 2;

            }

            if (comboBox12.Text == "web")
            {
                Globals.secH1 = Convert.ToDouble(((SectionWallForm)sectionWallForm).textBox3.Text) / 1000 - Convert.ToDouble(((SectionWallForm)sectionWallForm).textBox6.Text) / 1000 - Convert.ToDouble(((SectionWallForm)sectionWallForm).textBox9.Text) / 1000;
                Globals.secB1 = Convert.ToDouble(((SectionWallForm)sectionWallForm).textBox4.Text) / 1000;
                Globals.secC1 = Convert.ToDouble(((SectionWallForm)sectionWallForm).textBox47.Text) / 1000;
                comboBox7.Text = ((SectionWallForm)sectionWallForm).textBox19.Text;
                comboBox8.Text = ((SectionWallForm)sectionWallForm).textBox19.Text;
                Globals.Secsegment = 0;

            }

        }

        private void comboBox12_Leave(object sender, EventArgs e)
        {

            if (comboBox1.Text == "Left Flange")
            {
                Globals.secH1 = Convert.ToDouble(((SectionWallForm)sectionWallForm).textBox6.Text) / 1000;
                Globals.secB1 = Convert.ToDouble(((SectionWallForm)sectionWallForm).textBox5.Text) / 1000;
                Globals.secC1 = Convert.ToDouble(((SectionWallForm)sectionWallForm).textBox63.Text) / 1000;
                comboBox7.Text = ((SectionWallForm)sectionWallForm).textBox15.Text;
                comboBox8.Text = ((SectionWallForm)sectionWallForm).textBox15.Text;
                Globals.Secsegment = 1;

            }

            if (comboBox1.Text == "Right Flange")
            {
                Globals.secH1 = Convert.ToDouble(((SectionWallForm)sectionWallForm).textBox8.Text) / 1000;
                Globals.secB1 = Convert.ToDouble(((SectionWallForm)sectionWallForm).textBox9.Text) / 1000;
                Globals.secC1 = Convert.ToDouble(((SectionWallForm)sectionWallForm).textBox92.Text) / 1000;
                comboBox7.Text = ((SectionWallForm)sectionWallForm).textBox69.Text;
                comboBox8.Text = ((SectionWallForm)sectionWallForm).textBox69.Text;

                Globals.Secsegment = 2;

            }

            if (comboBox1.Text == "web")
            {
                Globals.secH1 = Convert.ToDouble(((SectionWallForm)sectionWallForm).textBox3.Text) / 1000 - Convert.ToDouble(((SectionWallForm)sectionWallForm).textBox6.Text) / 1000 - Convert.ToDouble(((SectionWallForm)sectionWallForm).textBox9.Text) / 1000;
                Globals.secB1 = Convert.ToDouble(((SectionWallForm)sectionWallForm).textBox4.Text) / 1000;
                Globals.secC1 = Convert.ToDouble(((SectionWallForm)sectionWallForm).textBox47.Text) / 1000;
                comboBox7.Text = ((SectionWallForm)sectionWallForm).textBox19.Text;
                comboBox8.Text = ((SectionWallForm)sectionWallForm).textBox19.Text;
                Globals.Secsegment = 0;

            }

        }
    }
}
