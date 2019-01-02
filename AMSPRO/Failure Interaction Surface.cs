using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AMSPRO
{
    public partial class Failure_Interaction_Surface : Form
    {
        public Failure_Interaction_Surface()
        {
            InitializeComponent();
        }

        Form sectionDrawForm = Application.OpenForms["SectionDrawForm"];
        public double[,] secCoor1 = new double[20, 20];
        public int secPointNo = 0;

        private void Chart5_PostPaint(object sender, ChartPaintEventArgs e)
        {
            Chart chart = sender as Chart;
            if (chart.Series.Count < 1) return;
            if (chart.Series[0].Points.Count < 1) return;
            ChartArea ca = chart.ChartAreas[0];
            //e.ChartGraphics.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            List<List<PointF>> data = new List<List<PointF>>();
            foreach (Series s in chart.Series)
                data.Add(GetPointsFrom3D(ca, s, s.Points.ToList(), e.ChartGraphics));
            renderLines(data, e.ChartGraphics.Graphics, chart, true);  // pick one!
            renderPoints(data, e.ChartGraphics.Graphics, chart, 6);   // pick one!
        }
        void prepare3dChart(Chart chart, ChartArea ca)
        {
            ca.Area3DStyle.Enable3D = true;  // set the chartarea to 3D!


            ca.AxisX.Minimum = -60;
            ca.AxisY.Minimum = -200;
            ca.AxisX.Maximum = 60;
            ca.AxisY.Maximum = 550;
            // ca.AxisX.Interval = 50;
            // ca.AxisY.Interval = 50;
            ca.AxisX.Title = "M2";
            ca.AxisY.Title = "P";
            // ca.AxisX.MajorGrid.Interval = 200;
            //ca.AxisY.MajorGrid.Interval = 600;

            //ca.AxisX.MinorGrid.Interval = 50;
            //ca.AxisY.MinorGrid.Interval = 50;
            // ca.AxisX.MinorGrid.LineColor = Color.White;// LightSlateGray;
            //ca.AxisY.MinorGrid.LineColor = Color.White;// LightSlateGray;

            ca.AxisX.MinorGrid.Enabled = false;
            ca.AxisY.MinorGrid.Enabled = false;
            ca.AxisX.MajorGrid.Enabled = false;
            ca.AxisY.MajorGrid.Enabled = false;
            ca.BackColor = Color.White;



            // we add two series:
            chart.Series.Clear();
            for (int i = 0; i < Globals.curvNo; i++)
            {
                Series s = chart.Series.Add("C" + i.ToString("00"));
                s.ChartType = SeriesChartType.Bubble;   // this ChartType has a YValue array
                s.MarkerStyle = MarkerStyle.Circle;
                s["PixelPointWidth"] = "10";
                s["PixelPointGapDepth"] = "0.5";
            }
            chart.ApplyPaletteColors();
            addTestData(chart);
        }
        void addTestData(Chart chart)
        {
            Random rnd = new Random(9);
            for (int i = 0; i < Globals.curvNo; i++)
            {
                for (int j = 0; j < Globals.pointNO; j++)
                {
                    double y = Math.Round(Globals.PaL[i, j] / 10000, 0);
                    double x = Math.Round(Globals.Mom2[i, j] / 10000000, 0) * 2;
                    double z = Math.Round(Globals.Mom3[i, j] / 10000000, 0) * 2;
                    AddXY3d(chart.Series[i], x, y, z);
                }
            }

        }
        int AddXY3d(Series s, double xVal, double yVal, double zVal)
        {
            int p = s.Points.AddXY(xVal, yVal, zVal);
            // the DataPoint are transparent to the regular chart drawing:
            s.Points[p].Color = Color.Transparent;
            return p;
        }

        List<PointF> GetPointsFrom3D(ChartArea ca, Series s, List<DataPoint> dPoints, ChartGraphics cg)
        {
            var p3t = dPoints.Select(x => new Point3D((float)ca.AxisX.ValueToPosition(x.XValue), (float)ca.AxisY.ValueToPosition(x.YValues[0]), (float)ca.AxisY.ValueToPosition(x.YValues[1]))).ToArray();
            ca.TransformPoints(p3t.ToArray());
            return p3t.Select(x => cg.GetAbsolutePoint(new PointF(x.X, x.Y))).ToList();
        }
        void renderLines(List<List<PointF>> data, Graphics graphics, Chart chart, bool curves)
        {
            for (int i = 0; i < chart.Series.Count; i++)
            {
                if (data[i].Count > 1)
                    // using (Pen pen = new Pen(Color.FromArgb(64, chart.Series[i].Color), 2.5f))
                    using (Pen pen = new Pen(Color.FromArgb(64, Color.Red), 2.5f))
                        if (curves) graphics.DrawCurve(pen, data[i].ToArray());
                        else graphics.DrawLines(pen, data[i].ToArray());
            }
        }
        void renderPoints(List<List<PointF>> data, Graphics graphics, Chart chart, float width)
        {
            for (int s = 0; s < chart.Series.Count; s++)
            {
                Series S = chart.Series[s];
                // for (int p = 0; p < S.Points.Count; p++)
                //  using (SolidBrush brush = new SolidBrush(Color.FromArgb(64, S.Color)))
                //       graphics.FillEllipse(brush, data[s][p].X - width / 2,
                //                  data[s][p].Y - width / 2, width, width);
            }
        }





        private void button3_Click(object sender, EventArgs e)
        {

           
        }


        private void Chart5_PostPaint_1(object sender, ChartPaintEventArgs e)
        {
            Chart chart = sender as Chart;
            if (chart.Series.Count < 1) return;
            if (chart.Series[0].Points.Count < 1) return;
            ChartArea ca = chart.ChartAreas[0];
            //e.ChartGraphics.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            List<List<PointF>> data = new List<List<PointF>>();
            foreach (Series s in chart.Series)
                data.Add(GetPointsFrom3D(ca, s, s.Points.ToList(), e.ChartGraphics));
            renderLines(data, e.ChartGraphics.Graphics, chart, true);  // pick one!
            renderPoints(data, e.ChartGraphics.Graphics, chart, 6);   // pick one!
        }
        private void button7_Click(object sender, EventArgs e)
        {
            this.chart4.Series["P-M2"].Points.Clear();
            this.chart3.Series["P-M3"].Points.Clear();
            int k = 0;
            int k1 = 0;
            if (textBox4.Text != "") Globals.pointNO = Convert.ToInt32(textBox4.Text);
            if (textBox3.Text != "") Globals.curvNo = Convert.ToInt32(textBox3.Text);
            if (label3.Text != "") k1 = Convert.ToInt32(label3.Text);
            if (k1 == 24)
            {
                if (label3.Text != "") k = Convert.ToInt32(label3.Text) - 1;
                label4.Text = (Globals.angle[k] * 180 / Math.PI).ToString();

                //====Draw
                for (int add = 0; add < Globals.pointNO; add++)
                {
                    this.chart4.Series["P-M2"].Points.AddXY(Globals.Mom2[k, add] / 10000000, Globals.PaL[k, add] / 10000);
                    this.chart3.Series["P-M3"].Points.AddXY(Globals.Mom3[k, add] / 10000000, Globals.PaL[k, add] / 10000);
                    //this.chart3.Series["P M2"].Points.AddXY(Mom2[0, k] / 10000000, PaL[0, k] / 10000);
                }
                MSFlex1.RowCount = Globals.pointNO;

                for (int i = 0; i < Globals.pointNO; i++)
                {
                    MSFlex1.Rows[i].Cells[0].Value = i + 1;
                    MSFlex1.Rows[i].Cells[1].Value = Math.Round(Globals.PaL[k, i] / 10000, 2);
                    MSFlex1.Rows[i].Cells[2].Value = Math.Round(Globals.Mom2[k, i] / 10000000, 2);
                    MSFlex1.Rows[i].Cells[3].Value = Math.Round(Globals.Mom3[k, i] / 10000000, 2);
                    // MSFlex1.Rows[i].Cells[4].Value = Math.Round(Mom[k, i] / 10000000, 2);
                }
                goto End20;
            }

            label3.Text = (k1 + 1).ToString();
            if (label3.Text != "") k = Convert.ToInt32(label3.Text) - 1;
            label4.Text = (Globals.angle[k] * 180 / Math.PI).ToString();
            //====Draw
            for (int add = 0; add < Globals.pointNO; add++)
            {
                this.chart4.Series["P-M2"].Points.AddXY(Globals.Mom2[k, add] / 10000000, Globals.PaL[k, add] / 10000);
                this.chart3.Series["P-M3"].Points.AddXY(Globals.Mom3[k, add] / 10000000, Globals.PaL[k, add] / 10000);
                //this.chart3.Series["P M2"].Points.AddXY(Mom2[0, k] / 10000000, PaL[0, k] / 10000);
            }
            MSFlex1.RowCount = Globals.pointNO;
            for (int i = 0; i < Globals.pointNO; i++)
            {
                MSFlex1.Rows[i].Cells[0].Value = i + 1;
                MSFlex1.Rows[i].Cells[1].Value = Math.Round(Globals.PaL[k, i] / 10000, 2);
                MSFlex1.Rows[i].Cells[2].Value = Math.Round(Globals.Mom2[k, i] / 10000000, 2);
                MSFlex1.Rows[i].Cells[3].Value = Math.Round(Globals.Mom3[k, i] / 10000000, 2);
                // MSFlex1.Rows[i].Cells[4].Value = Math.Round(Mom[k, i] / 10000000, 2);
            }
        End20: { }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.chart4.Series["P-M2"].Points.Clear();
            this.chart3.Series["P-M3"].Points.Clear();
            int k = 0;
            int k1 = 0;
            if (textBox4.Text != "") Globals.pointNO = Convert.ToInt32(textBox4.Text);
            if (textBox3.Text != "") Globals.curvNo = Convert.ToInt32(textBox3.Text);
            if (label3.Text != "") k1 = Convert.ToInt32(label3.Text);
            if (k1 == 1)
            {
                if (label3.Text != "") k = Convert.ToInt32(label3.Text) - 1;
                label4.Text = (Globals.angle[k] * 180 / Math.PI).ToString();
                //====Draw
                for (int add = 0; add < Globals.pointNO; add++)
                {
                    this.chart4.Series["P-M2"].Points.AddXY(Globals.Mom2[k, add] / 10000000, Globals.PaL[k, add] / 10000);
                    this.chart3.Series["P-M3"].Points.AddXY(Globals.Mom3[k, add] / 10000000, Globals.PaL[k, add] / 10000);
                    //this.chart3.Series["P M2"].Points.AddXY(Mom2[0, k] / 10000000, PaL[0, k] / 10000);
                }
                MSFlex1.RowCount = Globals.pointNO;

                for (int i = 0; i < Globals.pointNO; i++)
                {
                    MSFlex1.Rows[i].Cells[0].Value = i + 1;
                    MSFlex1.Rows[i].Cells[1].Value = Math.Round(Globals.PaL[k, i] / 10000, 2);
                    MSFlex1.Rows[i].Cells[2].Value = Math.Round(Globals.Mom2[k, i] / 10000000, 2);
                    MSFlex1.Rows[i].Cells[3].Value = Math.Round(Globals.Mom3[k, i] / 10000000, 2);
                    // MSFlex1.Rows[i].Cells[4].Value = Math.Round(Mom[k, i] / 10000000, 2);
                }
                goto End20;
            }
            label3.Text = (k1 - 1).ToString();
            if (label3.Text != "") k = Convert.ToInt32(label3.Text) - 1;
            label4.Text = (Globals.angle[k] * 180 / Math.PI).ToString();
            //====Draw
            for (int add = 0; add < Globals.pointNO; add++)
            {
                this.chart4.Series["P-M2"].Points.AddXY(Globals.Mom2[k, add] / 10000000, Globals.PaL[k, add] / 10000);
                this.chart3.Series["P-M3"].Points.AddXY(Globals.Mom3[k, add] / 10000000, Globals.PaL[k, add] / 10000);
                //this.chart3.Series["P M2"].Points.AddXY(Mom2[0, k] / 10000000, PaL[0, k] / 10000);
            }
            MSFlex1.RowCount = Globals.pointNO;
            for (int i = 0; i < Globals.pointNO; i++)
            {
                MSFlex1.Rows[i].Cells[0].Value = i + 1;
                MSFlex1.Rows[i].Cells[1].Value = Math.Round(Globals.PaL[k, i] / 10000, 2);
                MSFlex1.Rows[i].Cells[2].Value = Math.Round(Globals.Mom2[k, i] / 10000000, 2);
                MSFlex1.Rows[i].Cells[3].Value = Math.Round(Globals.Mom3[k, i] / 10000000, 2);
                // MSFlex1.Rows[i].Cells[4].Value = Math.Round(Mom[k, i] / 10000000, 2);
            }
        End20: { }
        }

        private void Failure_Interaction_Surface_Load(object sender, EventArgs e)
        {
            //try
          //  {
            double Xc = 0;
            double Yc = 0;
            int O = ((SectionDrawForm)sectionDrawForm).SelectedType;


                        //======rectangular section data OR rectangular WALL section data
 
                        if (O == 5)  ////rectangular single
                        {
                            Xc = ((SectionDrawForm)sectionDrawForm).RecShape[1].CenterX;
                            Yc = ((SectionDrawForm)sectionDrawForm).RecShape[1].CenterY;
                            secPointNo = 4;
                            secCoor1 = new double[secPointNo, 2];

                            secCoor1[0, 0] = (((SectionDrawForm)sectionDrawForm).RecShape[1].EDX1R[1] - Xc)/1000;
                            secCoor1[0, 1] = (((SectionDrawForm)sectionDrawForm).RecShape[1].EDY1R[1] - Yc)/1000;
                            secCoor1[1, 0] = (((SectionDrawForm)sectionDrawForm).RecShape[1].EDX1R[2] - Xc)/1000;
                            secCoor1[1, 1] = (((SectionDrawForm)sectionDrawForm).RecShape[1].EDY1R[2] - Yc)/1000;
                            secCoor1[2, 0] = (((SectionDrawForm)sectionDrawForm).RecShape[1].EDX1R[3] - Xc)/1000;
                            secCoor1[2, 1] = (((SectionDrawForm)sectionDrawForm).RecShape[1].EDY1R[3] - Yc)/1000;
                            secCoor1[3, 0] = (((SectionDrawForm)sectionDrawForm).RecShape[1].EDX1R[4] - Xc)/1000;
                            secCoor1[3, 1] = (((SectionDrawForm)sectionDrawForm).RecShape[1].EDY1R[4] - Yc) / 1000;
                           // if (((SectionDrawForm)sectionDrawForm).RecShapeNumber == 1)
                            //{
                               // textBox5.Text = (((SectionDrawForm)sectionDrawForm).RecShape[1].Height / 1000).ToString();
                                //textBox6.Text = (((SectionDrawForm)sectionDrawForm).RecShape[1].Width / 1000).ToString();
                              //  Globals.secType = 1;
                              //  comboBox9.Text = "Rectangular".ToString();
                               // textBox6.Visible = true;
                                //label6.Visible = true;
                              //  label10.Text = "H";
                            //}
                        }


                        if (O == 7)  ////circle single
                        {
                            Xc = ((SectionDrawForm)sectionDrawForm).CircleShape[1].CenterX;
                            Yc = ((SectionDrawForm)sectionDrawForm).CircleShape[1].CenterY;

                            secPointNo = 32;
                            secCoor1 = new double[secPointNo, 2];

                            for (int s = secPointNo-1; s > -1; s--)
                             {
                                 secCoor1[31-s, 0] = (((SectionDrawForm)sectionDrawForm).CircleShape[1].PointXR[s+1] - Xc) / 1000;
                                 secCoor1[31-s, 1] = (((SectionDrawForm)sectionDrawForm).CircleShape[1].PointYR[s+1] - Yc) / 1000;
                             }



                        }
                        if (O == 10)  ////flanged wall
                        {
                            Xc = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[1].CenterX;
                            Yc = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[1].CenterY;
                            secPointNo = 12;
                            secCoor1 = new double[secPointNo, 2];

                            for (int s = 0; s < secPointNo; s++)
                            {
                                secCoor1[s, 0] = (((SectionDrawForm)sectionDrawForm).FlangedWallShape[1].PointXReal[s + 1] - Xc) / 1000;
                                secCoor1[s, 1] = (((SectionDrawForm)sectionDrawForm).FlangedWallShape[1].PointYReal[s + 1] - Yc) / 1000;
                            }
                        }

                Globals.secType = 1;
                int BarsNumber = ((SectionDrawForm)sectionDrawForm).BarsNumber;
                MSFlex2.RowCount = BarsNumber + 1;
                for (int i = 1; i < BarsNumber + 1; i++)
                {
                    MSFlex2.Rows[i - 1].Cells[0].Value = i;
                    MSFlex2.Rows[i - 1].Cells[1].Value = ((SectionDrawForm)sectionDrawForm).Bar[i].DR;
                    MSFlex2.Rows[i - 1].Cells[2].Value = ((SectionDrawForm)sectionDrawForm).Bar[i].XR - Xc;
                    MSFlex2.Rows[i - 1].Cells[3].Value = ((SectionDrawForm)sectionDrawForm).Bar[i].YR - Yc;
                }



                //======circular section data

                if (((SectionDrawForm)sectionDrawForm).CircleShapeNumber == 1)
                {
                  /*  textBox5.Text = (((SectionDrawForm)sectionDrawForm).CircleShape[1].Diameter / 1000).ToString();
                    comboBox9.Text = "Circular".ToString();
                    Globals.secType = 2;
                    textBox6.Visible = false;
                    label6.Visible = false;
                    label10.Text = "D";*/
                }


                /*  if (((SectionDrawForm)sectionDrawForm).FlangedWallShapeNumber == 1)
                  {
                      textBox5.Text = (((SectionDrawForm)sectionDrawForm).RecShape[1].Height / 1000).ToString();
                      textBox6.Text = (((SectionDrawForm)sectionDrawForm).RecShape[1].Width / 1000).ToString();
                      Globals.secType = 1;
                      comboBox9.Text = "Flanged Shear wall".ToString();
                      textBox6.Visible = true;
                      label6.Visible = true;
                      label5.Text = "H";
                  }*/


          //  }
            //  catch { };
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

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {


        }

       

        private void button4_Click_1(object sender, EventArgs e)
        {
            Globals.codeNo = 0;  // ACI318

            double phi = 1;
            double phic = 1;
            double phit = 0.9;
            double phii = 0;
            double maxP = 0;

            int confinment = 0;
            int ControlF0 = 0;
            int ControlF1 = 0;
            int ControlF2 = 0;
            double sum = 0.9;

            if (radioButton1.Checked)
            {
                confinment = 0;
                maxP = 0.8;
            }
            if (radioButton2.Checked)
            {
                confinment = 1;
                maxP = 0.85;
            }

            if (radioButton3.Checked)
            {
                phit = 1;
                phic = 1;
            }

            if (radioButton4.Checked)
            {
                if (Globals.codeNo == 1)
                {
                    phit = 0.9;

                    if (confinment == 0) phic = 0.65;
                    if (confinment == 1) phic = 0.75;
                }

            }

            Globals.ASLn1 = MSFlex2.RowCount - 1;
            if (textBox8.Text != "") Globals.fc01 = Convert.ToDouble(textBox8.Text);
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

            this.chart4.Series["P-M2"].Points.Clear();
            this.chart3.Series["P-M3"].Points.Clear();
            double strainC = 0.003;
            double strainSy = 0.002;
            double angle1 = 0;
            if (textBox3.Text != "") Globals.curvNo = Convert.ToInt32(textBox3.Text);
            if (textBox4.Text != "") Globals.pointNO = Convert.ToInt32(textBox4.Text);
            Globals.angle = new double[Globals.curvNo];
            double fc0 = 0;
            double fybar = 0;
           // double ET = 0;

            double[,] secCoor2 = new double[secPointNo, 2];
            int ASLn = 0;
            double AStotal = 0;

            double[,] Mom = new double[Globals.curvNo, Globals.pointNO];
            Globals.PaL = new double[Globals.curvNo, Globals.pointNO];
            Globals.Mom2 = new double[Globals.curvNo, Globals.pointNO];
            Globals.Mom3 = new double[Globals.curvNo, Globals.pointNO];

            double[] Cs = new double[Globals.pointNO];  //===height of comperssion area in strain diagram
           
            double ES = 0;
            double dbar = 0;

            fc0 = Globals.fc01;
           // secH = Globals.secH1;
           // secB = Globals.secB1;
           // secC = Globals.secC1;
            fybar = Globals.fybar1;
            ES = Globals.ES1;
            ASLn = Globals.ASLn1;

            double[,] ASbar = new double[ASLn, 5];
            double[,] Coor1 = new double[ASLn, 2];
            double[,] Coor2 = new double[ASLn, 2];
            double[] ds = new double[ASLn];
            double[] strainS = new double[ASLn];
            double[] Fsi = new double[ASLn];
            double[] FC = new double[Globals.pointNO];
            double h1, cosa, sina, axb, areab, areab1 = 0;
            double Fss = 0;
            double FssM2 = 0;
            double FssM3 = 0;
            double xcg = 0;
            double ycg = 0;
            double deltac = 0;


            ////////////////
            double xi = 0;
            double xj = 0;
            double yi = 0;
            double yj = 0;
            double xxi = 0;
            double xxj = 0;
            double yyi = 0;
            double yyj = 0;
            double x1 = 0;
            double y1 = 0;
            double x2 = 0;
            double y2 = 0;
            double xs = 0;
            double ys = 0;
            double tol = 0.0000001;

            double beta = 0.85;

            if (Globals.codeNo == 1)
            {
                if (fc0 <= 27.58) beta = 0.85;

                if (fc0 <= 2 * 27.58 && fc0 > 27.58)
                {
                    beta = 0.85-0.00727*(fc0-27.58);
                    if (beta < 0.65) beta = 0.65;
                }
                if (fc0 > 27.58 * 2) beta = 0.65; ;  
            }

            ///======1======calculate unit angle
            Globals.angleNo = Globals.curvNo;
            angle1 = Math.PI * 2 / (Globals.angleNo);
            // ET = 5000 * Math.Pow(fc0, 0.5);
            for (int i = 0; i < ASLn; i++)
            {
                ASbar[i, 0] = i;
                ASbar[i, 1] = Globals.ASbar1[i, 1];
                ASbar[i, 2] = Globals.ASbar1[i, 2];
                ASbar[i, 3] = Globals.ASbar1[i, 3];
                dbar = Globals.dbar1;
                ASbar[i, 4] = Globals.ASbar1[i, 4];
                AStotal = AStotal + ASbar[i, 4];
                Coor1[i, 0] = ASbar[i, 2];
                Coor1[i, 1] = ASbar[i, 3];
            }

            ///=========================== 
            for (int k = 0; k < Globals.angleNo ; k++)
            {
                Globals.angle[k] = k * angle1;
                //=====3 ==caculat h1
                cosa = Math.Cos(-Globals.angle[k]);
                sina = Math.Sin(-Globals.angle[k]);
                //=====4== Transfer coordinates of bar and section points from main orgin to secondry orgin

                for (int s = 0; s < secPointNo; s++)
                {
                    secCoor2[s, 0] = secCoor1[s, 0] * cosa + secCoor1[s, 1] * sina;
                    secCoor2[s, 1] = -secCoor1[s, 0] * sina + secCoor1[s, 1] * cosa;
                }

                h1 = Math.Abs(2 * secCoor2[0, 1]);

                    for (int ss = 0; ss < secPointNo; ss++)
                    {
                        if (Math.Abs(2 * secCoor2[ss, 1]) > h1)
                        {
                            h1 = Math.Abs(2 * secCoor2[ss, 1]);
                        }
                    }



            for (int i = 0; i < ASLn; i++)
            {
                Coor2[i, 0] = Coor1[i, 0] * cosa + Coor1[i, 1] * sina;
                Coor2[i, 1] = -Coor1[i, 0] * sina + Coor1[i, 1] * cosa;
                ds[i] = h1/2 - Coor2[i, 1] / 1000;
            }
            deltac = h1 / (Globals.pointNO - 2);

                //=====5===== caculat bar strains ,forces and concrete force
                for (int i = 0; i < Globals.pointNO - 2; i++)
                {

                Fss = 0;
                FssM2 = 0;
                FssM3 = 0;
                ycg = 0;
                xcg = 0;
                Cs[i + 1] = h1 - i * deltac;
                axb = Cs[i + 1] * beta;
                xxj = -(h1 / 2 - axb) * sina + 1000 * cosa;
                yyj = (h1 / 2 - axb) * cosa + 1000 * sina;
                xxi = -(h1 / 2 - axb) * sina - 1000 * cosa;
                yyi = (h1 / 2 - axb) * cosa - 1000 * sina;
                xj = xxj * cosa + yyj * sina;
                yj = -xxj * sina + yyj * cosa;
                xi = xxi * cosa + yyi * sina;
                yi = -xxi * sina + yyi * cosa;
                int jj = -1;
                int ii = 0;
                double dche = 0;
                double[,] AreaPoint = new double[2*secPointNo, 2];
                    double L12 = 0;
                    double xc = 0;
                    double yc = 0;
                    double Lgs = 0;


                for (ii = 0; ii < secPointNo; ii++)
                {
                    if (secCoor2[ii, 1] > yj && secCoor2[ii, 1] > yi)
                    {
                        jj = jj + 1;
                        AreaPoint[jj, 0] = secCoor1[ii, 0];
                        AreaPoint[jj, 1] = secCoor1[ii, 1];

                    }

                    if (ii == secPointNo-1) goto END130;
                    x1 = secCoor1[ii, 0];
                    y1 = secCoor1[ii, 1];
                    x2 = secCoor1[ii + 1, 0];
                    y2 = secCoor1[ii + 1, 1];
                    dche = (x1 - x2) * (yyi - yyj) - (y1 - y2) * (xxi - xxj);
                    if (dche == 0) goto END130;
                    xs = ((x1 * y2 - y1 * x2) * (xxi - xxj) - (x1 - x2) * (xxi * yyj - yyi * xxj)) / dche;
                    ys = ((x1 * y2 - y1 * x2) * (yyi - yyj) - (y1 - y2) * (xxi * yyj - yyi * xxj)) / dche;
                    L12 = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
                    xc = (x1 + x2) / 2;
                    yc = (y1 + y2) / 2;
                    Lgs = Math.Sqrt(Math.Pow(xc - xs, 2) + Math.Pow(yc - ys, 2)); 

                    //if ((Math.Abs(xs) - (Math.Abs(x1) + Math.Abs(x2)) / 2) <= tol && (Math.Abs(ys) - (Math.Abs(y1) + Math.Abs(y2)) / 2) <= tol)
                    if (Lgs-L12 / 2 <=tol  )
                    {
                        jj = jj + 1;
                        AreaPoint[jj, 0] = xs;
                        AreaPoint[jj, 1] = ys;

                    }
                END130: { }
                }

                x1 = secCoor1[ii-1, 0];
                y1 = secCoor1[ii-1, 1];
                x2 = secCoor1[0, 0];
                y2 = secCoor1[0, 1];
                dche = (x1 - x2) * (yyi - yyj) - (y1 - y2) * (xxi - xxj);
                if (dche == 0) goto END110;
                xs = ((x1 * y2 - y1 * x2) * (xxi - xxj) - (x1 - x2) * (xxi * yyj - yyi * xxj)) / dche;
                ys = ((x1 * y2 - y1 * x2) * (yyi - yyj) - (y1 - y2) * (xxi * yyj - yyi * xxj)) / dche;
                L12 = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
                xc = (x1 + x2) / 2;
                yc = (y1 + y2) / 2;
                Lgs = Math.Sqrt(Math.Pow(xc - xs, 2) + Math.Pow(yc - ys, 2)); 

              //  if ((Math.Abs(xs) - (Math.Abs(x1) + Math.Abs(x2)) / 2) <= tol && (Math.Abs(ys) - (Math.Abs(y1) + Math.Abs(y2)) / 2) <= tol)
                if (Lgs - L12 / 2 <= tol)
                    {
                    jj = jj + 1;
                    AreaPoint[jj, 0] = xs;
                    AreaPoint[jj, 1] = ys;

                }
            END110: { }


                  areab = 0;
                for (ii = jj; ii > 0; ii--)
                {
                    areab = areab + (AreaPoint[ii, 0] * AreaPoint[ii - 1, 1] - AreaPoint[ii - 1, 0] * AreaPoint[ii, 1]) / 2;
                    xcg = xcg + (AreaPoint[ii, 0] + AreaPoint[ii - 1, 0]) * (AreaPoint[ii, 0] * AreaPoint[ii - 1, 1] - AreaPoint[ii - 1, 0] * AreaPoint[ii, 1]);
                    ycg = ycg + (AreaPoint[ii, 1] + AreaPoint[ii - 1, 1]) * (AreaPoint[ii, 0] * AreaPoint[ii - 1, 1] - AreaPoint[ii - 1, 0] * AreaPoint[ii, 1]);
                }

                areab = Math.Abs(areab + (AreaPoint[0, 0] * AreaPoint[jj, 1] - AreaPoint[jj, 0] * AreaPoint[0, 1]) / 2);
                xcg = xcg + (AreaPoint[jj, 0] + AreaPoint[0, 0]) * (AreaPoint[0, 0] * AreaPoint[jj, 1] - AreaPoint[jj, 0] * AreaPoint[0, 1]);
                ycg = ycg + (AreaPoint[0, 1] + AreaPoint[jj, 1]) * (AreaPoint[0, 0] * AreaPoint[jj, 1] - AreaPoint[jj, 0] * AreaPoint[0, 1]);
                xcg = xcg / (6 * areab);
                ycg = ycg / (6 * areab);

                FC[i + 1] = areab * 0.85 * fc0 * 1000000;


                    for (int j = 0; j < ASLn; j++)
                    {

                        strainS[j] = strainC * (Cs[i + 1] - ds[j]) / Cs[i + 1];
                        Fsi[j] = ES * strainS[j] * ASbar[j, 4];
                        if (Math.Abs(strainS[j]) >= strainSy)
                        {
                            Fsi[j] = -fybar * ASbar[j, 4];
                            if (strainS[j] > 0)
                            {
                                Fsi[j] = fybar * ASbar[j, 4];
                            }
                        }
                        Fss = Fss + Fsi[j];
                        FssM2 = FssM2 + Fsi[j] * Coor1[j, 0];
                        FssM3 = FssM3 + Fsi[j] * Coor1[j, 1];
                    }
                    //======calculate Strength reduction factor ϕ for moment, axial force, or combined moment and axial force
                    sum = 0;

                    if (radioButton3.Checked)
                    {
                        phi = 1;
                        phit = 1;
                        phic = 1;
                    }

                    if (radioButton4.Checked)
                    {
                        if (Globals.codeNo == 1)
                        {
                            phit = 0.9;
                            phic = 0.65;
                            for (int j = 0; j < ASLn; j++)
                            {
                                if (strainS[j] < 0 && Math.Abs(strainS[j]) < strainSy) ControlF0 = ControlF0 + 1; //Compression-controlled

                                if (strainS[j] < 0 && Math.Abs(strainS[j]) >= strainSy && Math.Abs(strainS[j]) <= 0.005) // Transitioncontrolled
                                {
                                    ControlF1 = ControlF1 + 1;
                                    if (confinment == 0)
                                    {
                                        phii = (0.65 + 0.25 * (Math.Abs(strainS[j]) - strainSy) / (0.005 - strainSy));
                                        if (phii < 0.65) phii = 0.65;
                                        if (phii > 0.9) phii = 0.9;
                                        sum = sum + phii;
                                    }
                                    if (confinment == 01)
                                    {
                                        phii =  0.75 + (0.15 * (Math.Abs(strainS[j]) - strainSy) / (0.005 - strainSy));
                                        if (phii < 0.75) phii = 0.75;
                                        if (phii > 0.9) phii = 0.9;
                                        sum = sum + phii;

                                    }
                                }

                                if (strainS[j] < 0 && Math.Abs(strainS[j]) > 0.005) ControlF2 = ControlF2 + 1; //tension controlled
                            }

                        }

                    }

                    if (Globals.codeNo == 1)
                    {
                        phit = 0.9;
                        phic = 0.65;
                        if (ControlF0 + ControlF1 + ControlF2 == 0)
                        {
                            if (confinment == 0) phi = 0.65;
                            if (confinment == 1) phi = 0.75;
                            goto END99;
                        }

                    if (confinment == 0)
                    {
                        phi = (0.65 * ControlF0 + sum + 0.9 * ControlF2) / (ControlF0 + ControlF1 + ControlF2);
                        if (phi < 0.65) phi = 0.65;
                        if (phi > 0.9) phi = 0.9;
                    }
                    if (confinment == 1)
                    {
                        phi = (0.75 * ControlF0 + sum + 0.9 * ControlF2) / (ControlF0 + ControlF1 + ControlF2);
                        if (phi < 0.75) phi = 0.75;
                        if (phi > 0.9) phi = 0.9;
                    }


                    }

                END99: { }

                     Globals.PaL[k, i + 1] =  (Fss + FC[i + 1]);
                     if (Globals.PaL[k, i + 1] < 0) phi = 0.9;
                    Globals.PaL[k, i + 1] = phi * (Fss + FC[i + 1]);
                    Globals.Mom2[k, i + 1] = phi*(FC[i + 1] * xcg * 1000 + FssM2);
                    Globals.Mom3[k, i + 1] = phi*(FC[i + 1] * ycg * 1000 + FssM3);
                  //  Mom[k, i + 1] = Math.Sqrt(Math.Pow(Globals.Mom2[0, i + 1], 2) + Math.Pow(Globals.Mom3[0, i + 1], 2));
                }
                //=======first==pure compression

                areab1 = 0;
                int g = 0;
                for (g = secPointNo - 1; g > 0; g--)
                {
                    areab1 = areab1 + (secCoor1[g, 0] * secCoor1[g - 1, 1] - secCoor1[g - 1, 0] * secCoor1[g, 1]) / 2;
                }
                areab1 = Math.Abs(areab1 + (secCoor1[0, 0] * secCoor1[g, 1] - secCoor1[g, 0] * secCoor1[0, 1]) / 2);


                Globals.PaL[k, 0] = phic * maxP*(0.85 * fc0 * (areab1 * 1000000-AStotal ) + fybar * AStotal);
                Mom[k, 0] = 0;
                Globals.Mom2[k, 0] = 0;
                Globals.Mom3[k, 0] = 0;
                //=======final ==pure Tension
                Globals.PaL[k, Globals.pointNO - 1] = phit *(- fybar * AStotal);
                Mom[k, Globals.pointNO - 1] = 0;
                Globals.Mom2[k, Globals.pointNO - 1] = 0;
                Globals.Mom3[k, Globals.pointNO - 1] = 0;

            }



            //====Draw
            for (int add = 0; add < Globals.pointNO; add++)
            {
                this.chart4.Series["P-M2"].Points.AddXY(Globals.Mom2[0, add] / 10000000, Globals.PaL[0, add] / 10000);
                this.chart3.Series["P-M3"].Points.AddXY(Globals.Mom3[0, add] / 10000000, Globals.PaL[0, add] / 10000);
                //this.chart3.Series["P M2"].Points.AddXY(Mom2[0, k] / 10000000, PaL[0, k] / 10000);
            }

            label3.Text = (1).ToString();
            label4.Text = (0).ToString();


            MSFlex1.RowCount = Globals.pointNO;

            for (int i = 0; i < Globals.pointNO; i++)
            {
                MSFlex1.Rows[i].Cells[0].Value = i + 1;
                MSFlex1.Rows[i].Cells[1].Value = Math.Round(Globals.PaL[0, i] / 10000, 2);
                MSFlex1.Rows[i].Cells[2].Value = Math.Round(Globals.Mom2[0, i] / 10000000, 2);
                MSFlex1.Rows[i].Cells[3].Value = Math.Round(Globals.Mom3[0, i] / 10000000, 2);
                //MSFlex1.Rows[i].Cells[4].Value = Math.Round(Mom[0, i] / 10000000, 2);

            }
            //=====show  3D curve
            prepare3dChart(Chart5, Chart5.ChartAreas[0]);
        }

        private void button5_Click(object sender, EventArgs e)
        {

            Globals.ASLn1 = MSFlex2.RowCount - 1;
            if (textBox8.Text != "") Globals.fc01 = Convert.ToDouble(textBox8.Text);
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


            this.chart4.Series["P-M2"].Points.Clear();
            this.chart3.Series["P-M3"].Points.Clear();
            double strainC = 0.003;
            double strainSy = 0.002;
            double angle1 = 0;
            if (textBox4.Text != "") Globals.pointNO = Convert.ToInt32(textBox4.Text);
            double fc0 = 0;
            double fybar = 0;
            double ET = 0;
            double[,] secCoor1 = new double[4, 2];
            double[,] secCoor2 = new double[4, 2];
            int ASLn = 0;
            double AStotal = 0;

            double[] Mom1 = new double[Globals.pointNO];
            double[] PaL1 = new double[Globals.pointNO];
            double[] Mom21 = new double[Globals.pointNO];
            double[] Mom31 = new double[Globals.pointNO];

            double[] Cs = new double[Globals.pointNO];  //===height of comperssion area in strain diagram
            double beta = 0.85;
            double ES = 0;
            double dbar = 0;

            ASLn = Globals.ASLn1;

            fc0 = Globals.fc01;
            fybar = Globals.fybar1;
            ES = Globals.ES1;


            double[,] ASbar = new double[ASLn, 5];
            double[,] Coor1 = new double[ASLn, 2];
            double[,] Coor2 = new double[ASLn, 2];
            double[] ds = new double[ASLn];
            double[] strainS = new double[ASLn];
            double[] Fsi = new double[ASLn];
            double[] FC = new double[Globals.pointNO];
            double h1, cosa, sina, axb, areab, areab1 = 0;
            double Fss = 0;
            double FssM2 = 0;
            double FssM3 = 0;
            double xcg = 0;
            double ycg = 0;
            double deltac = 0;

            ////////////////
            double xi = 0;
            double xj = 0;
            double yi = 0;
            double yj = 0;
            double xxi = 0;
            double xxj = 0;
            double yyi = 0;
            double yyj = 0;
            //double[,] AreaPoint = new double[5, 2];
            double x1 = 0;
            double y1 = 0;
            double x2 = 0;
            double y2 = 0;
            double xs = 0;
            double ys = 0;
            double tol = 0.0000001;

            ///======1======calculate unit angle
            if (textBox16.Text != "") angle1 = Convert.ToDouble(textBox16.Text);

            angle1 = angle1 * Math.PI / 180;
            //====2==calculate the coordinates of section poins

            for (int i = 0; i < ASLn; i++)
            {
                ASbar[i, 0] = i;
                ASbar[i, 1] = Globals.ASbar1[i, 1];
                ASbar[i, 2] = Globals.ASbar1[i, 2];
                ASbar[i, 3] = Globals.ASbar1[i, 3];
                dbar = Globals.dbar1;
                ASbar[i, 4] = Globals.ASbar1[i, 4];
                AStotal = AStotal + ASbar[i, 4];
                Coor1[i, 0] = ASbar[i, 2];
                Coor1[i, 1] = ASbar[i, 3];
            }

            //=====3 ==caculat h1

            cosa = Math.Cos(-angle1);
            sina = Math.Sin(-angle1);

            //deltac = h1 / (pointNO - 2);
            //=====4== Transfer coordinates of bar and section points from main orgin to secondry orgin
            secCoor2[0, 0] = secCoor1[0, 0] * cosa + secCoor1[0, 1] * sina;
            secCoor2[0, 1] = -secCoor1[0, 0] * sina + secCoor1[0, 1] * cosa;
            secCoor2[1, 0] = secCoor1[1, 0] * cosa + secCoor1[1, 1] * sina;
            secCoor2[1, 1] = -secCoor1[1, 0] * sina + secCoor1[1, 1] * cosa;
            secCoor2[2, 0] = secCoor1[2, 0] * cosa + secCoor1[2, 1] * sina;
            secCoor2[2, 1] = -secCoor1[2, 0] * sina + secCoor1[2, 1] * cosa;
            secCoor2[3, 0] = secCoor1[3, 0] * cosa + secCoor1[3, 1] * sina;
            secCoor2[3, 1] = -secCoor1[3, 0] * sina + secCoor1[3, 1] * cosa;
           /* for (int i = 0; i < ASLn; i++)
            {
                Coor2[i, 0] = Coor1[i, 0] * cosa + Coor1[i, 1] * sina;
                Coor2[i, 1] = -Coor1[i, 0] * sina + Coor1[i, 1] * cosa;
                ds[i] = secCoor2[0, 1] - Coor2[i, 1] / 1000;
            }*/
            h1 = Math.Abs(2 * secCoor2[0, 1]);
            if (Math.Abs(2 * secCoor2[1, 1]) > Math.Abs(2 * secCoor2[1, 1]))
            {
                h1 = Math.Abs(2 * secCoor2[1, 1]);
            }
            if (Math.Abs(2 * secCoor2[2, 1]) > Math.Abs(2 * secCoor2[1, 1]))
            {
                h1 = Math.Abs(2 * secCoor2[2, 1]);
            }
            if (Math.Abs(2 * secCoor2[3, 1]) > Math.Abs(2 * secCoor2[2, 1]))
            {
                h1 = Math.Abs(2 * secCoor2[3, 1]);
            }
            for (int i = 0; i < ASLn; i++)
            {
                Coor2[i, 0] = Coor1[i, 0] * cosa + Coor1[i, 1] * sina;
                Coor2[i, 1] = -Coor1[i, 0] * sina + Coor1[i, 1] * cosa;
                ds[i] = h1/2 - Coor2[i, 1] / 1000;
            }
            deltac = h1 / (Globals.pointNO - 2);
            //=====5===== caculat bar strains ,forces and concrete force
            for (int i = 0; i < Globals.pointNO - 2; i++)
            {
                Fss = 0;
                FssM2 = 0;
                FssM3 = 0;
                ycg = 0;
                xcg = 0;


                Cs[i + 1] = h1 - i * deltac;
                axb = Cs[i + 1] * beta;


                xxj = -(h1 / 2 - axb) * sina + 1000 * cosa;
                yyj = (h1 / 2 - axb) * cosa + 1000 * sina;
                xxi = -(h1 / 2 - axb) * sina - 1000 * cosa;
                yyi = (h1 / 2 - axb) * cosa - 1000 * sina;

                xj = xxj * cosa + yyj * sina;
                yj = -xxj * sina + yyj * cosa;
                xi = xxi * cosa + yyi * sina;
                yi = -xxi * sina + yyi * cosa;
                int jj = -1;
                int ii = 0;
                double dche = 0;
                double[,] AreaPoint = new double[5, 2];

                for (ii = 0; ii < 4; ii++)
                {
                    if (secCoor2[ii, 1] > yj && secCoor2[ii, 1] > yi)
                    {
                        jj = jj + 1;
                        AreaPoint[jj, 0] = secCoor1[ii, 0];
                        AreaPoint[jj, 1] = secCoor1[ii, 1];

                    }

                    if (ii == 3) goto END130;
                    x1 = secCoor1[ii, 0];
                    y1 = secCoor1[ii, 1];
                    x2 = secCoor1[ii + 1, 0];
                    y2 = secCoor1[ii + 1, 1];
                    dche = (x1 - x2) * (yyi - yyj) - (y1 - y2) * (xxi - xxj);
                    if (dche == 0) goto END130;
                    xs = ((x1 * y2 - y1 * x2) * (xxi - xxj) - (x1 - x2) * (xxi * yyj - yyi * xxj)) / dche;
                    ys = ((x1 * y2 - y1 * x2) * (yyi - yyj) - (y1 - y2) * (xxi * yyj - yyi * xxj)) / dche;

                    if ((Math.Abs(xs) - (Math.Abs(x1) + Math.Abs(x2)) / 2) <= tol && (Math.Abs(ys) - (Math.Abs(y1) + Math.Abs(y2)) / 2) <= tol)
                    {
                        jj = jj + 1;
                        AreaPoint[jj, 0] = xs;
                        AreaPoint[jj, 1] = ys;

                    }
                END130: { }
                }

                x1 = secCoor1[ii-1, 0];
                y1 = secCoor1[ii-1, 1];
                x2 = secCoor1[0, 0];
                y2 = secCoor1[0, 1];
                dche = (x1 - x2) * (yyi - yyj) - (y1 - y2) * (xxi - xxj);
                if (dche == 0) goto END110;
                xs = ((x1 * y2 - y1 * x2) * (xxi - xxj) - (x1 - x2) * (xxi * yyj - yyi * xxj)) / dche;
                ys = ((x1 * y2 - y1 * x2) * (yyi - yyj) - (y1 - y2) * (xxi * yyj - yyi * xxj)) / dche;

                if ((Math.Abs(xs) - (Math.Abs(x1) + Math.Abs(x2)) / 2) <= tol && (Math.Abs(ys) - (Math.Abs(y1) + Math.Abs(y2)) / 2) <= tol)
                {
                    jj = jj + 1;
                    AreaPoint[jj, 0] = xs;
                    AreaPoint[jj, 1] = ys;

                }
            END110: { }

                areab = 0;
                for (ii = jj; ii > 0; ii--)
                {
                    areab = areab + (AreaPoint[ii, 0] * AreaPoint[ii - 1, 1] - AreaPoint[ii - 1, 0] * AreaPoint[ii, 1]) / 2;
                    xcg = xcg + (AreaPoint[ii, 0] + AreaPoint[ii - 1, 0]) * (AreaPoint[ii, 0] * AreaPoint[ii - 1, 1] - AreaPoint[ii - 1, 0] * AreaPoint[ii, 1]);
                    ycg = ycg + (AreaPoint[ii, 1] + AreaPoint[ii - 1, 1]) * (AreaPoint[ii, 0] * AreaPoint[ii - 1, 1] - AreaPoint[ii - 1, 0] * AreaPoint[ii, 1]);
                }

                areab = Math.Abs(areab + (AreaPoint[0, 0] * AreaPoint[jj, 1] - AreaPoint[jj, 0] * AreaPoint[0, 1]) / 2);
                xcg = xcg + (AreaPoint[jj, 0] + AreaPoint[0, 0]) * (AreaPoint[0, 0] * AreaPoint[jj, 1] - AreaPoint[jj, 0] * AreaPoint[0, 1]);
                ycg = ycg + (AreaPoint[0, 1] + AreaPoint[jj, 1]) * (AreaPoint[0, 0] * AreaPoint[jj, 1] - AreaPoint[jj, 0] * AreaPoint[0, 1]);
                xcg = xcg / (6 * areab);
                ycg = ycg / (6 * areab);

                FC[i + 1] = areab * 0.85 * fc0 * 1000000;

                for (int j = 0; j < ASLn; j++)
                {

                    strainS[j] = strainC * (Cs[i + 1] - ds[j]) / Cs[i + 1];
                    Fsi[j] = ES * strainS[j] * ASbar[j, 4];
                    if (Math.Abs(strainS[j]) >= strainSy)
                    {
                        Fsi[j] = -fybar * ASbar[j, 4];
                        if (strainS[j] > 0)
                        {
                            Fsi[j] = fybar * ASbar[j, 4];
                        }
                    }
                    Fss = Fss + Fsi[j];

                    FssM2 = FssM2 + Fsi[j] * Coor1[j, 0];
                    FssM3 = FssM3 + Fsi[j] * Coor1[j, 1];
                }
                PaL1[i + 1] = Fss + FC[i + 1];
                Mom21[i + 1] = FC[i + 1] * xcg * 1000 + FssM2;
                Mom31[i + 1] = FC[i + 1] * ycg * 1000 + FssM3;
                Mom1[i + 1] = Math.Sqrt(Math.Pow(Mom21[i + 1], 2) + Math.Pow(Mom31[i + 1], 2));

                //=======first==pure compression
                areab1 = 0;
                int g = 0;
                for (g = secPointNo - 1; g > 0; g--)
                {
                    areab1 = areab1 + (secCoor1[g, 0] * secCoor1[g - 1, 1] - secCoor1[g - 1, 0] * secCoor1[g, 1]) / 2;
                }
                areab1 = Math.Abs(areab1 + (secCoor1[0, 0] * secCoor1[g, 1] - secCoor1[g, 0] * secCoor1[0, 1]) / 2);


                //PaL[0] = 0.8*(0.85 * fc0 * (areab1 * 1000000 - AStotal) + fybar * AStotal);
                PaL1[0] = 0.8 * (0.85 * fc0 * areab1 * 1000000 + fybar * AStotal);
                Mom1[0] = 0;
                Mom21[0] = 0;
                Mom31[0] = 0;
                //=======final ==pure Tension
                PaL1[Globals.pointNO - 1] = -fybar * AStotal;
                Mom1[Globals.pointNO - 1] = 0;
                Mom21[Globals.pointNO - 1] = 0;
                Mom31[Globals.pointNO - 1] = 0;

            }

            //====Draw
            for (int add = 0; add < Globals.pointNO; add++)
            {
                this.chart4.Series["P-M2"].Points.AddXY(Mom21[add] / 10000000, PaL1[add] / 10000);
                this.chart3.Series["P-M3"].Points.AddXY(Mom31[add] / 10000000, PaL1[add] / 10000);
                //this.chart3.Series["P M2"].Points.AddXY(Mom2[0, k] / 10000000, PaL[0, k] / 10000);
            }

            MSFlex1.RowCount = Globals.pointNO;

            for (int i = 0; i < Globals.pointNO; i++)
            {
                MSFlex1.Rows[i].Cells[0].Value = i + 1;
                MSFlex1.Rows[i].Cells[1].Value = Math.Round(PaL1[i] / 10000, 2);
                MSFlex1.Rows[i].Cells[2].Value = Math.Round(Mom21[i] / 10000000, 2);
                MSFlex1.Rows[i].Cells[3].Value = Math.Round(Mom31[i] / 10000000, 2);
                //MSFlex1.Rows[i].Cells[4].Value = Math.Round(Mom1[i] / 10000000, 2);

            }
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }
    }
}
