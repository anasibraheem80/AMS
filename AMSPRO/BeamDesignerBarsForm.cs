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
    public partial class BeamDesignerBarsForm : Form
    {
        public BeamDesignerBarsForm()
        {
            InitializeComponent();
        }
        Form beamDesignerForm = Application.OpenForms["BeamDesignerForm"];
        //((BeamDesignerForm)beamDesignerForm)
        public int SelectedSpan ;
        public double AlValue;
        int ifShear ;
        int ColumnIndex = 0;
        int RowIndex = 0;
        string KindForBars = "";
        private void BeamDesignerBarsForm_Load(object sender, EventArgs e)
        {
            AlValue=((BeamDesignerForm)beamDesignerForm).AlValue;
            textBox1.Text = AlValue.ToString();
            ifShear = 0;
            int numbers = 0;
            int[] n = new int[100];
            int[] d = new int[100];
            int[] Ki = new int[100];
            int[] dis = new int[100];

            int SelectedSpan = ((BeamDesignerForm)beamDesignerForm).SelectedSpan;
            KindForBars = ((BeamDesignerForm)beamDesignerForm).KindForBars;
            if (KindForBars == "SpMomentUp")
            {
                numbers = ((BeamDesignerForm)beamDesignerForm).BarsSpUpGR[SelectedSpan];
                for (int i = 1; i < numbers+1 ; i++)
                {
                     n[i] = ((BeamDesignerForm)beamDesignerForm).BarsSpUpNo[SelectedSpan, i];
                     d[i] = ((BeamDesignerForm)beamDesignerForm).BarsSpUpD[SelectedSpan, i];
                     Ki[i] = ((BeamDesignerForm)beamDesignerForm).BarsSpUpKi[SelectedSpan, i];
                }
            }
            if (KindForBars == "SpMomentDn")
            {
                numbers = ((BeamDesignerForm)beamDesignerForm).BarsSpDnGR[SelectedSpan];
                for (int i = 1; i < numbers + 1; i++)
                {
                    n[i] = ((BeamDesignerForm)beamDesignerForm).BarsSpDnNo[SelectedSpan, i];
                    d[i] = ((BeamDesignerForm)beamDesignerForm).BarsSpDnD[SelectedSpan, i];
                    Ki[i] = ((BeamDesignerForm)beamDesignerForm).BarsSpDnKi[SelectedSpan, i];
                }
            }
            if (KindForBars == "SuMomentUp")
            {
                numbers = ((BeamDesignerForm)beamDesignerForm).BarsSuUpGR[SelectedSpan];
                for (int i = 1; i < numbers + 1; i++)
                {
                    n[i] = ((BeamDesignerForm)beamDesignerForm).BarsSuUpNo[SelectedSpan, i];
                    d[i] = ((BeamDesignerForm)beamDesignerForm).BarsSuUpD[SelectedSpan, i];
                    Ki[i] = ((BeamDesignerForm)beamDesignerForm).BarsSuUpKi[SelectedSpan, i];
                }
            }
            if (KindForBars == "SuMomentDn")
            {
                numbers = ((BeamDesignerForm)beamDesignerForm).BarsSuDnGR[SelectedSpan];
                for (int i = 1; i < numbers + 1; i++)
                {
                    n[i] = ((BeamDesignerForm)beamDesignerForm).BarsSuDnNo[SelectedSpan, i];
                    d[i] = ((BeamDesignerForm)beamDesignerForm).BarsSuDnD[SelectedSpan, i];
                    Ki[i] = ((BeamDesignerForm)beamDesignerForm).BarsSuDnKi[SelectedSpan, i];
                }
            }

            if (KindForBars == "SpShear")
            {
                ifShear = 1;
                numbers = ((BeamDesignerForm)beamDesignerForm).ShearBarsSpGR[SelectedSpan];
                for (int i = 1; i < numbers + 1; i++)
                {
                    n[i] = ((BeamDesignerForm)beamDesignerForm).ShearBarsSpNo[SelectedSpan, i];
                    d[i] = ((BeamDesignerForm)beamDesignerForm).ShearBarsSpD[SelectedSpan, i];
                    Ki[i] = ((BeamDesignerForm)beamDesignerForm).ShearBarsSpKi[SelectedSpan, i];
                    dis[i] = ((BeamDesignerForm)beamDesignerForm).ShearBarsSpDis[SelectedSpan, i];
                }
            }
            if (KindForBars == "SuRShear")
            {
                ifShear = 1;
                numbers = ((BeamDesignerForm)beamDesignerForm).ShearBarsSuRGR[SelectedSpan];
                for (int i = 1; i < numbers + 1; i++)
                {
                    n[i] = ((BeamDesignerForm)beamDesignerForm).ShearBarsSuRNo[SelectedSpan, i];
                    d[i] = ((BeamDesignerForm)beamDesignerForm).ShearBarsSuRD[SelectedSpan, i];
                    Ki[i] = ((BeamDesignerForm)beamDesignerForm).ShearBarsSuRKi[SelectedSpan, i];
                    dis[i] = ((BeamDesignerForm)beamDesignerForm).ShearBarsSuRDis[SelectedSpan, i];
                }
            }
            if (KindForBars == "SuLShear")
            {
                ifShear = 1;
                numbers = ((BeamDesignerForm)beamDesignerForm).ShearBarsSuLGR[SelectedSpan];
                for (int i = 1; i < numbers + 1; i++)
                {
                    n[i] = ((BeamDesignerForm)beamDesignerForm).ShearBarsSuLNo[SelectedSpan, i];
                    d[i] = ((BeamDesignerForm)beamDesignerForm).ShearBarsSuLD[SelectedSpan, i];
                    Ki[i] = ((BeamDesignerForm)beamDesignerForm).ShearBarsSuLKi[SelectedSpan, i];
                    dis[i] = ((BeamDesignerForm)beamDesignerForm).ShearBarsSuLDis[SelectedSpan, i];
                }
            }
            if (ifShear == 1)
            {
                SpansData.Columns[4].Visible = true;
                SpansData.Width = 405;
                this.Width = 429; 
            }
            double AmountSum = 0;
            for (int i = 1; i < numbers + 1; i++)
            {
                if (Ki[i] == 1)
                {
                    SpansData.Rows.Add(i, true, n[i], d[i], dis[i]);
                }
                else
                {
                    SpansData.Rows.Add(i, false, n[i], d[i], dis[i]);
                }
                double Aria = Math.PI * Math.Pow(d[i], 2) / 4;
                AmountSum = (Aria * n[i]) + AmountSum;
            }
            textBox2.Text = Math.Round (AmountSum,0).ToString ();
        }
        private void CalculateNoDforSpans()
        {
                int i = SpansData.CurrentCell.RowIndex;
                int j = SpansData.CurrentCell.ColumnIndex;
                if (RowIndex != 0) i = RowIndex;
                if (ColumnIndex != 0) j = ColumnIndex;
                double Amount = 0;
                double Aria = 0;
                int diameter = 0;
                int Num = 0;
                int count = SpansData.RowCount;
                double AmountSum = 0;
                Amount = AlValue;
                for (int k = 0; k < count; k++)
                {
                    if (SpansData.Rows[k].Cells[1].Value.ToString() == "False")
                    {
                        Num = Convert.ToInt32(SpansData.Rows[k].Cells[2].Value);
                        diameter = Convert.ToInt32(SpansData.Rows[k].Cells[3].Value);
                        Aria = Math.PI * Math.Pow(diameter, 2) / 4;
                        AmountSum = (Aria * Num) + AmountSum;
                        Amount = AlValue - AmountSum;
                    }
                    if (Amount <= 0)
                    {
                        for (int l = 0; l < count; l++)
                        {
                            if (SpansData.Rows[l].Cells[1].Value.ToString() == "True")
                            {
                                SpansData.Rows[l].Cells[2].Value = 0;
                            }
                        }
                        goto EndOne;
                    }
                }

                for (int k = 0; k < count; k++)
                {
                    if (SpansData.Rows[k].Cells[1].Value.ToString() == "True")
                    {
                        Num = Convert.ToInt32(SpansData.Rows[k].Cells[2].Value);
                        if (j == 3 || k != i || Num == 0 || j == 1)
                        {
                            diameter = Convert.ToInt32(SpansData.Rows[k].Cells[3].Value);
                            Aria = Math.PI * Math.Pow(diameter, 2) / 4;
                            Num = 0;
                            if (Aria > 0) Num = Convert.ToInt32(Math.Ceiling(Amount / Aria));
                            SpansData.Rows[k].Cells[2].Value = Num;
                            goto EndForJ;
                        }
                        if (j == 2)
                        {
                            Num = Convert.ToInt32(SpansData.Rows[k].Cells[2].Value);
                            if (Num == 0) goto NextK;
                            Aria = Amount / Num;
                            diameter = Convert.ToInt32(Math.Ceiling((Math.Sqrt(Aria * 4 / Math.PI))));
                            int diameter1 = diameter;
                            if (diameter1 < 6) diameter = 6;
                            if (diameter1 == 7) diameter = 8;
                            if (diameter1 == 9) diameter = 10;
                            if (diameter1 == 11) diameter = 12;
                            if (diameter1 == 13) diameter = 14;
                            if (diameter1 == 15) diameter = 16;
                            if (diameter1 == 17) diameter = 18;
                            if (diameter1 == 19) diameter = 20;
                            if (diameter1 == 21) diameter = 22;
                            if (diameter1 == 23) diameter = 25;
                            if (diameter1 == 24) diameter = 25;
                            if (diameter1 == 26) diameter = 28;
                            if (diameter1 == 27) diameter = 28;
                            if (diameter1 == 29) diameter = 32;
                            if (diameter1 == 30) diameter = 32;
                            if (diameter1 == 31) diameter = 32;
                            if (Amount == 0) diameter = 0;
                            SpansData.Rows[k].Cells[3].Value = diameter;
                        }
                    EndForJ: { };
                        Num = Convert.ToInt32(SpansData.Rows[k].Cells[2].Value);
                        diameter = Convert.ToInt32(SpansData.Rows[k].Cells[3].Value);
                        Aria = Math.PI * Math.Pow(diameter, 2) / 4;
                        AmountSum = (Aria * Num) + AmountSum;
                        Amount = AlValue - AmountSum;
                        if (Amount <= 0)
                        {
                            for (int l = k + 1; l < count; l++)
                            {
                                if (SpansData.Rows[l].Cells[1].Value.ToString() == "True")
                                {
                                    SpansData.Rows[l].Cells[2].Value = 0;
                                }
                            }
                            goto EndOne;
                        }
                    }
                NextK: { };
                }
            EndOne: { };
            textBox2.Text = Math.Round(AmountSum, 0).ToString();
        }
        private void CalculateNoDforSpansShear()
        {
            int i = SpansData.CurrentCell.RowIndex;
            int j = SpansData.CurrentCell.ColumnIndex;
            if (RowIndex != 0) i = RowIndex;
            if (ColumnIndex != 0) j = ColumnIndex;
            double Amount = 0;
            double Aria = 0;
            int diameter = 0;
            int Num = 0;
            int count = SpansData.RowCount;
            double AmountSum = 0;
            Amount = AlValue;
            for (int k = 0; k < count; k++)
            {
                if (SpansData.Rows[k].Cells[1].Value.ToString() == "False")
                {
                    Num = Convert.ToInt32(SpansData.Rows[k].Cells[2].Value) ;
                    diameter = Convert.ToInt32(SpansData.Rows[k].Cells[3].Value);
                    Aria = Math.PI * Math.Pow(diameter, 2) / 4 * 2;
                    AmountSum = (Aria * Num) + AmountSum;
                    Amount = AlValue - AmountSum;
                }
                if (Amount <= 0)
                {
                    for (int l = 0; l < count; l++)
                    {
                        if (SpansData.Rows[l].Cells[1].Value.ToString() == "True")
                        {
                            SpansData.Rows[l].Cells[2].Value = 0;
                        }
                    }
                    goto EndOne;
                }
            }

            for (int k = 0; k < count; k++)
            {
                if (SpansData.Rows[k].Cells[1].Value.ToString() == "True")
                {
                    Num = Convert.ToInt32(SpansData.Rows[k].Cells[2].Value);
                    if (j == 3 || k != i || Num == 0 || j == 1)
                    {
                        diameter = Convert.ToInt32(SpansData.Rows[k].Cells[3].Value);
                        Aria = Math.PI * Math.Pow(diameter, 2) / 4 * 2;
                        Num = 0;
                        if (Aria > 0) Num = Convert.ToInt32(Math.Ceiling((Amount / Aria)));
                        SpansData.Rows[k].Cells[2].Value = Num;
                        goto EndForJ;
                    }
                    if (j == 2)
                    {
                        Num = Convert.ToInt32(SpansData.Rows[k].Cells[2].Value)*2;
                        if (Num == 0) goto NextK;
                        Aria = Amount / Num;
                        diameter = Convert.ToInt32(Math.Ceiling((Math.Sqrt(Aria * 4 / Math.PI))));
                        int diameter1 = diameter;
                        if (diameter1 < 6) diameter = 6;
                        if (diameter1 == 7) diameter = 8;
                        if (diameter1 == 9) diameter = 10;
                        if (diameter1 == 11) diameter = 12;
                        if (diameter1 == 13) diameter = 14;
                        if (diameter1 == 15) diameter = 16;
                        if (diameter1 == 17) diameter = 18;
                        if (diameter1 == 19) diameter = 20;
                        if (diameter1 == 21) diameter = 22;
                        if (diameter1 == 23) diameter = 25;
                        if (diameter1 == 24) diameter = 25;
                        if (diameter1 == 26) diameter = 28;
                        if (diameter1 == 27) diameter = 28;
                        if (diameter1 == 29) diameter = 32;
                        if (diameter1 == 30) diameter = 32;
                        if (diameter1 == 31) diameter = 32;
                        if (Amount == 0) diameter = 0;
                        SpansData.Rows[k].Cells[3].Value = diameter;
                    }
                EndForJ: { };
                    Num = Convert.ToInt32(SpansData.Rows[k].Cells[2].Value);
                    diameter = Convert.ToInt32(SpansData.Rows[k].Cells[3].Value);
                    Aria = Math.PI * Math.Pow(diameter, 2) / 4*2;
                    AmountSum = (Aria * Num) + AmountSum;
                    Amount = AlValue - AmountSum;
                    if (Amount <= 0)
                    {
                        for (int l = k + 1; l < count; l++)
                        {
                            if (SpansData.Rows[l].Cells[1].Value.ToString() == "True")
                            {
                                SpansData.Rows[l].Cells[2].Value = 0;
                            }
                        }
                        goto EndOne;
                    }
                }
            NextK: { };
            }
        EndOne: { };
            textBox2.Text = Math.Round(AmountSum, 0).ToString();
        } 

        private void SpansData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            ColumnIndex = 0;
            RowIndex = 0;
            if (KindForBars == "SpMomentUp" || KindForBars == "SpMomentDn" || KindForBars == "SuMomentUp" || KindForBars == "SuMomentDn")
            {
                CalculateNoDforSpans();
            }
            else
            {
                CalculateNoDforSpansShear();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            
            int count = SpansData.RowCount+1;
            if (KindForBars == "SpMomentUp" || KindForBars == "SpMomentDn" || KindForBars == "SuMomentUp" || KindForBars == "SuMomentDn")
            {
                SpansData.Rows.Add(count, true, "0", "14");
                ColumnIndex = 3;
                RowIndex = count - 1;
                CalculateNoDforSpans();
            }
            else
            {
                if (KindForBars == "SuRShear" || KindForBars == "SuLShear")
                {
                    SpansData.Rows.Add(count, true, "0", "8", "10");
                }
                else
                {
                    SpansData.Rows.Add(count, true, "0", "8", "20");
                }
                ColumnIndex = 3;
                RowIndex = count - 1;
                CalculateNoDforSpansShear();
            }

        }
        private void SpansData_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (SpansData.SelectedRows.Count > 0)
            {
                int j = SpansData.CurrentCell.ColumnIndex;
                ColumnIndex = 0;
                RowIndex = 0;
                if (j == 1)
                {
                    ColumnIndex = 3;
                    if (KindForBars == "SpMomentUp" || KindForBars == "SpMomentDn" || KindForBars == "SuMomentUp" || KindForBars == "SuMomentDn")
                    {
                        CalculateNoDforSpans();
                    }
                    else
                    {
                        CalculateNoDforSpansShear();
                    }
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int count = SpansData.RowCount - 1;
            if (count >= 0)
            {
                foreach (DataGridViewCell oneCell in SpansData.SelectedCells)
                {
                    if (oneCell.Selected)
                        SpansData.Rows.RemoveAt(oneCell.RowIndex);
                }
                ColumnIndex = 3;
                RowIndex = count - 1;
                if (RowIndex >= 0)
                {
                    if (KindForBars == "SpMomentUp" || KindForBars == "SpMomentDn" || KindForBars == "SuMomentUp" || KindForBars == "SuMomentDn")
                    {
                        CalculateNoDforSpans();
                    }
                    else
                    {
                        CalculateNoDforSpansShear();
                    }
                }

                for (int l = 0; l < count; l++)
                {
                    SpansData.Rows[l].Cells[0].Value = l + 1;
                }
            }      
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int SelectedSpan = ((BeamDesignerForm)beamDesignerForm).SelectedSpan;
            string KindForBars = ((BeamDesignerForm)beamDesignerForm).KindForBars;
            int count = SpansData.RowCount;
            int m = 0;
            int dis=20;
            for (int k = 0; k < count; k++)
            {
               int Num = Convert.ToInt32(SpansData.Rows[k].Cells[2].Value);
               int diameter = Convert.ToInt32(SpansData.Rows[k].Cells[3].Value);
               if (Num != 0 & diameter != 0)
               {
                   m = m + 1;
                   int Ki = 1;
                   if (SpansData.Rows[k].Cells[1].Value.ToString() == "False") Ki = 0;

                   if (KindForBars == "SpMomentUp")
                   {
                       ((BeamDesignerForm)beamDesignerForm).BarsSpUpGR[SelectedSpan] = m;
                       ((BeamDesignerForm)beamDesignerForm).BarsSpUpNo[SelectedSpan, m] = Num;
                       ((BeamDesignerForm)beamDesignerForm).BarsSpUpD[SelectedSpan, m] = diameter;
                       ((BeamDesignerForm)beamDesignerForm).BarsSpUpKi[SelectedSpan, m] = Ki;
                   }
                   if (KindForBars == "SpMomentDn")
                   {
                       ((BeamDesignerForm)beamDesignerForm).BarsSpDnGR[SelectedSpan] = m;
                       ((BeamDesignerForm)beamDesignerForm).BarsSpDnNo[SelectedSpan, m] = Num;
                       ((BeamDesignerForm)beamDesignerForm).BarsSpDnD[SelectedSpan, m] = diameter;
                       ((BeamDesignerForm)beamDesignerForm).BarsSpDnKi[SelectedSpan, m] = Ki;
                   }
                   if (KindForBars == "SuMomentUp")
                   {
                       ((BeamDesignerForm)beamDesignerForm).BarsSuUpGR[SelectedSpan] = m;
                       ((BeamDesignerForm)beamDesignerForm).BarsSuUpNo[SelectedSpan, m] = Num;
                       ((BeamDesignerForm)beamDesignerForm).BarsSuUpD[SelectedSpan, m] = diameter;
                       ((BeamDesignerForm)beamDesignerForm).BarsSuUpKi[SelectedSpan, m] = Ki;
                   }
                   if (KindForBars == "SuMomentDn")
                   {
                       ((BeamDesignerForm)beamDesignerForm).BarsSuDnGR[SelectedSpan] = m;
                       ((BeamDesignerForm)beamDesignerForm).BarsSuDnNo[SelectedSpan, m] = Num;
                       ((BeamDesignerForm)beamDesignerForm).BarsSuDnD[SelectedSpan, m] = diameter;
                       ((BeamDesignerForm)beamDesignerForm).BarsSuDnKi[SelectedSpan, m] = Ki;
                   }

                   if (KindForBars == "SpShear")
                   {
                       dis = Convert.ToInt32(SpansData.Rows[k].Cells[4].Value);
                       ((BeamDesignerForm)beamDesignerForm).ShearBarsSpGR[SelectedSpan] = m;
                       ((BeamDesignerForm)beamDesignerForm).ShearBarsSpNo[SelectedSpan, m] = Num;
                       ((BeamDesignerForm)beamDesignerForm).ShearBarsSpD[SelectedSpan, m] = diameter;
                       ((BeamDesignerForm)beamDesignerForm).ShearBarsSpKi[SelectedSpan, m] = Ki;
                       ((BeamDesignerForm)beamDesignerForm).ShearBarsSpDis[SelectedSpan, m] = dis;
                   }
                   if (KindForBars == "SuRShear")
                   {
                       dis = Convert.ToInt32(SpansData.Rows[k].Cells[4].Value);
                       ((BeamDesignerForm)beamDesignerForm).ShearBarsSuRGR[SelectedSpan] = m;
                       ((BeamDesignerForm)beamDesignerForm).ShearBarsSuRNo[SelectedSpan, m] = Num;
                       ((BeamDesignerForm)beamDesignerForm).ShearBarsSuRD[SelectedSpan, m] = diameter;
                       ((BeamDesignerForm)beamDesignerForm).ShearBarsSuRKi[SelectedSpan, m] = Ki;
                       ((BeamDesignerForm)beamDesignerForm).ShearBarsSuRDis[SelectedSpan, m] = dis;
                   }
                   if (KindForBars == "SuLShear")
                   {
                       dis = Convert.ToInt32(SpansData.Rows[k].Cells[4].Value);
                       ((BeamDesignerForm)beamDesignerForm).ShearBarsSuLGR[SelectedSpan] = m;
                       ((BeamDesignerForm)beamDesignerForm).ShearBarsSuLNo[SelectedSpan, m] = Num;
                       ((BeamDesignerForm)beamDesignerForm).ShearBarsSuLD[SelectedSpan, m] = diameter;
                       ((BeamDesignerForm)beamDesignerForm).ShearBarsSuLKi[SelectedSpan, m] = Ki;
                       ((BeamDesignerForm)beamDesignerForm).ShearBarsSuLDis[SelectedSpan, m] = dis;
                   }
               }
               ((BeamDesignerForm)beamDesignerForm).FillInTable();
               ((BeamDesignerForm)beamDesignerForm).DrawDiagram();
            }
            this.Close();
        }
    }
}
