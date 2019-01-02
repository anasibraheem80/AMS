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
    public partial class SectionTeeForm : Form
    {
        public SectionTeeForm()
        {
            InitializeComponent();
        }
        Form sectionDrawForm = Application.OpenForms["SectionDrawForm"];
        int SelectedTeeShape = 0;
        int[] kx = new int[5];
        int[] ky = new int[5];
        int BarOrDis = 1;
        int SelectedInED = 0;
        int loaded = 0;
        private void calcrecRECALL()
        {
            int K = SelectedInED;
            int diameter1 = 0;
            int TheBars = 0;
            double TheDistance = 0;
            #region
            if (comboBox1.SelectedIndex == 1)
            {
                diameter1 = Convert.ToInt32(textBox19.Text);
                double CoverED1 = Convert.ToDouble(textBox47.Text);
                double CoverED2 = Convert.ToDouble(textBox48.Text);
                double CoverED3 = Convert.ToDouble(textBox49.Text);
                double CoverED4 = Convert.ToDouble(textBox50.Text);
                if (K == 1)
                {
                    double length = Convert.ToDouble(textBox4.Text) - CoverED2 - CoverED4;
                    if (BarOrDis == 1)
                    {
                        TheBars = Convert.ToInt32(textBox29.Text);
                        TheDistance = Math.Round((length - 2 * diameter1) / (TheBars + 1), 3);
                        if (loaded == 1)
                        {
                            textBox30.Text = TheDistance.ToString();
                            textBox27.Text = TheDistance.ToString();
                            textBox28.Text = Math.Round(length, 0).ToString();
                        }
                    }
                    if (BarOrDis == 2)
                    {
                        TheDistance = Convert.ToDouble(textBox30.Text);
                        TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                        TheDistance = Math.Round((length - 2 * diameter1) / (TheBars + 1), 3);
                        textBox29.Text = TheBars.ToString();
                        textBox27.Text = TheDistance.ToString();
                        textBox28.Text = Math.Round(length, 0).ToString();
                    }
                }
                if (K == 2)
                {
                    double length = Convert.ToDouble(textBox5.Text) - CoverED1 - CoverED3;
                    if (BarOrDis == 1)
                    {
                        TheBars = Convert.ToInt32(textBox34.Text);
                        TheDistance = Math.Round((length - 2 * diameter1) / (TheBars + 1), 3);
                        if (loaded == 1)
                        {
                            textBox35.Text = TheDistance.ToString();
                            textBox32.Text = TheDistance.ToString();
                            textBox33.Text = Math.Round(length, 0).ToString();
                        }
                    }
                    if (BarOrDis == 2)
                    {
                        TheDistance = Convert.ToDouble(textBox35.Text);
                        TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                        TheDistance = Math.Round((length - 2 * diameter1) / (TheBars + 1), 3);
                        textBox34.Text = TheBars.ToString();
                        textBox32.Text = TheDistance.ToString();
                        textBox33.Text = Math.Round(length, 0).ToString();
                    }
                }
                if (K == 3)
                {
                    double length = Convert.ToDouble(textBox4.Text) - CoverED2 - CoverED4; ;
                    if (BarOrDis == 1)
                    {
                        TheBars = Convert.ToInt32(textBox39.Text);
                        TheDistance = Math.Round((length - 2 * diameter1) / (TheBars + 1), 3);
                        if (loaded == 1)
                        {
                            textBox40.Text = TheDistance.ToString();
                            textBox37.Text = TheDistance.ToString();
                            textBox38.Text = Math.Round(length, 0).ToString();
                        }
                    }
                    if (BarOrDis == 2)
                    {
                        TheDistance = Convert.ToDouble(textBox40.Text);
                        TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                        TheDistance = Math.Round((length - 2 * diameter1) / (TheBars + 1), 3);
                        textBox39.Text = TheBars.ToString();
                        textBox37.Text = TheDistance.ToString();
                        textBox38.Text = Math.Round(length, 0).ToString();
                    }
                }
                if (K == 4)
                {
                    double length = Convert.ToDouble(textBox5.Text) - CoverED1 - CoverED3;
                    if (BarOrDis == 1)
                    {
                        TheBars = Convert.ToInt32(textBox44.Text);
                        TheDistance = Math.Round((length - 2 * diameter1) / (TheBars + 1), 3);
                        if (loaded == 1)
                        {
                            textBox45.Text = TheDistance.ToString();
                            textBox42.Text = TheDistance.ToString();
                            textBox43.Text = Math.Round(length, 0).ToString();
                        }
                    }
                    if (BarOrDis == 2)
                    {
                        TheDistance = Convert.ToDouble(textBox45.Text);
                        TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                        TheDistance = Math.Round((length - 2 * diameter1) / (TheBars + 1), 3);
                        textBox44.Text = TheBars.ToString();
                        textBox42.Text = TheDistance.ToString();
                        textBox43.Text = Math.Round(length, 0).ToString();
                    }
                }
            }
            #endregion
        }
        private void calcrecRECALLL()
        {
            int K = SelectedInED;
            int diameter1 = 0;
            int TheBars = 0;
            double TheDistance = 0;
            #region
            if (comboBox1.SelectedIndex == 1)
            {
                diameter1 = Convert.ToInt32(textBox15.Text);
                double CoverED1 = Convert.ToDouble(textBox63.Text);
                double CoverED2 = Convert.ToDouble(textBox57.Text);
                double CoverED3 = Convert.ToDouble(textBox51.Text);
                double CoverED4 = Convert.ToDouble(textBox21.Text);
                if (K == 1)
                {
                    double length = Convert.ToDouble(textBox6.Text) - CoverED2 - CoverED4;//********
                    if (BarOrDis == 1)
                    {
                        TheBars = Convert.ToInt32(textBox66.Text);
                        TheDistance = Math.Round((length - 2 * diameter1) / (TheBars + 1), 3);
                        if (loaded == 1)
                        {
                            textBox67.Text = TheDistance.ToString();
                            textBox64.Text = TheDistance.ToString();
                            textBox65.Text = Math.Round(length, 0).ToString();
                        }
                    }
                    if (BarOrDis == 2)
                    {
                        TheDistance = Convert.ToDouble(textBox67.Text);
                        TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                        TheDistance = Math.Round((length - 2 * diameter1) / (TheBars + 1), 3);
                        textBox66.Text = TheBars.ToString();
                        textBox64.Text = TheDistance.ToString();
                        textBox65.Text = Math.Round(length, 0).ToString();
                    }
                }
                if (K == 2)
                {
                    double length = Convert.ToDouble(textBox3.Text) - CoverED1 - CoverED3;//*****
                    if (BarOrDis == 1)
                    {
                        TheBars = Convert.ToInt32(textBox60.Text);
                        TheDistance = Math.Round((length - 2 * diameter1) / (TheBars + 1), 3);
                        if (loaded == 1)
                        {
                            textBox61.Text = TheDistance.ToString();
                            textBox58.Text = TheDistance.ToString();
                            textBox59.Text = Math.Round(length, 0).ToString();
                        }
                    }
                    if (BarOrDis == 2)
                    {
                        TheDistance = Convert.ToDouble(textBox61.Text);
                        TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                        TheDistance = Math.Round((length - 2 * diameter1) / (TheBars + 1), 3);
                        textBox60.Text = TheBars.ToString();
                        textBox58.Text = TheDistance.ToString();
                        textBox59.Text = Math.Round(length, 0).ToString();
                    }
                }
                if (K == 3)
                {
                    double length = Convert.ToDouble(textBox6.Text) - CoverED2 - CoverED4;//********
                    if (BarOrDis == 1)
                    {
                        TheBars = Convert.ToInt32(textBox54.Text);
                        TheDistance = Math.Round((length - 2 * diameter1) / (TheBars + 1), 3);
                        if (loaded == 1)
                        {
                            textBox55.Text = TheDistance.ToString();
                            textBox52.Text = TheDistance.ToString();
                            textBox53.Text = Math.Round(length, 0).ToString();
                        }
                    }
                    if (BarOrDis == 2)
                    {
                        TheDistance = Convert.ToDouble(textBox55.Text);
                        TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                        TheDistance = Math.Round((length - 2 * diameter1) / (TheBars + 1), 3);
                        textBox54.Text = TheBars.ToString();
                        textBox52.Text = TheDistance.ToString();
                        textBox53.Text = Math.Round(length, 0).ToString();
                    }
                }
                if (K == 4)
                {
                    double length = Convert.ToDouble(textBox3.Text) - CoverED1 - CoverED3;//*********
                    if (BarOrDis == 1)
                    {
                        TheBars = Convert.ToInt32(textBox24.Text);
                        TheDistance = Math.Round((length - 2 * diameter1) / (TheBars + 1), 3);
                        if (loaded == 1)
                        {
                            textBox25.Text = TheDistance.ToString();
                            textBox22.Text = TheDistance.ToString();
                            textBox23.Text = Math.Round(length, 0).ToString();
                        }
                    }
                    if (BarOrDis == 2)
                    {
                        TheDistance = Convert.ToDouble(textBox25.Text);
                        TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                        TheDistance = Math.Round((length - 2 * diameter1) / (TheBars + 1), 3);
                        textBox24.Text = TheBars.ToString();
                        textBox22.Text = TheDistance.ToString();
                        textBox23.Text = Math.Round(length, 0).ToString();
                    }
                }
            }
            #endregion
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int SelectedTeeShape = ((SectionDrawForm)sectionDrawForm).SelectedTeeShape;
            int SelectedRecBar = ((SectionDrawForm)sectionDrawForm).SelectedRecBar;
            int SelectedRecBar1 = ((SectionDrawForm)sectionDrawForm).SelectedRecBar1;
            int SelectedRecBar2 = ((SectionDrawForm)sectionDrawForm).SelectedRecBar2;
            int RecShapeNumber = ((SectionDrawForm)sectionDrawForm).RecShapeNumber;
            int FlangedWallShapeNumber = ((SectionDrawForm)sectionDrawForm).FlangedWallShapeNumber;
            int TeeShapeNumber = ((SectionDrawForm)sectionDrawForm).TeeShapeNumber;
            int BarsNumber = ((SectionDrawForm)sectionDrawForm).BarsNumber;
            int RecBarsNumber = ((SectionDrawForm)sectionDrawForm).RecBarsNumber;
            #region////رسم شكل تيه
            double CenterX = Convert.ToDouble(textBox1.Text);
            double CenterY = Convert.ToDouble(textBox2.Text);
            double Height = Convert.ToDouble(textBox3.Text);
            double FlangWidth = Convert.ToDouble(textBox4.Text);
            double FlangThickness = Convert.ToDouble(textBox5.Text);
            double WebThickness = Convert.ToDouble(textBox6.Text);
            double ED1CoverR = Convert.ToDouble(textBox47.Text);
            double ED2CoverR = Convert.ToDouble(textBox48.Text);
            double ED3CoverR = Convert.ToDouble(textBox49.Text);
            double ED4CoverR = Convert.ToDouble(textBox50.Text);
            double ED1cCoverR = Convert.ToDouble(textBox63.Text);
            double ED2cCoverR = Convert.ToDouble(textBox57.Text);
            double ED3cCoverR = Convert.ToDouble(textBox51.Text);
            double ED4cCoverR = Convert.ToDouble(textBox21.Text);
            double[] PointXReal = new double[9];
            double[] PointYReal = new double[9];
            TeeShapes emp0 = new TeeShapes();
            emp0.CenterX = CenterX;
            emp0.CenterY = CenterY;
            emp0.ED1CoverR = ED1CoverR;
            emp0.ED2CoverR = ED2CoverR;
            emp0.ED3CoverR = ED3CoverR;
            emp0.ED4CoverR = ED4CoverR;
            emp0.ED1cCoverR = ED1cCoverR;
            emp0.ED2cCoverR = ED2cCoverR;
            emp0.ED3cCoverR = ED3cCoverR;
            emp0.ED4cCoverR = ED4cCoverR;
            emp0.HasReinforcment = 0;
            emp0.CenterX = CenterX;
            emp0.CenterY = CenterY;
            emp0.Height = Height;
            emp0.WebThickness = WebThickness;
            emp0.FlangWidth = FlangWidth;
            emp0.FlangThickness = FlangThickness;
            emp0.Selected = 0;
            emp0.PointXReal[1] = CenterX - FlangWidth / 2;
            emp0.PointXReal[2] = CenterX + FlangWidth / 2;
            emp0.PointXReal[3] = CenterX + FlangWidth / 2;
            emp0.PointXReal[4] = CenterX + WebThickness / 2;
            emp0.PointXReal[5] = CenterX + WebThickness / 2;
            emp0.PointXReal[6] = CenterX - WebThickness / 2;
            emp0.PointXReal[7] = CenterX - WebThickness / 2;
            emp0.PointXReal[8] = CenterX - FlangWidth / 2;
            double WebHeight = (Height - FlangThickness);
            double aria1 = FlangWidth * FlangThickness;
            double aria2 = WebHeight * WebThickness;
            double aria = aria1 + aria2;
            double way = ((aria1 * FlangThickness / 2) + aria2 * (FlangThickness + (WebHeight / 2))) / aria;
            double way1 = way - FlangThickness;
            double way2 = Height - way;
            emp0.PointYReal[1] = CenterY + way;
            emp0.PointYReal[2] = CenterY + way;
            emp0.PointYReal[3] = CenterY + way1;
            emp0.PointYReal[4] = CenterY + way1;
            emp0.PointYReal[5] = CenterY - way2;
            emp0.PointYReal[6] = CenterY - way2;
            emp0.PointYReal[7] = CenterY + way1;
            emp0.PointYReal[8] = CenterY + way1;
            #endregion
            #region/// أساور تسليح
            if (comboBox1.SelectedIndex == 1)
            #region//يوجد تسليح
            {
                double X1 = 0;
                double Y1 = 0;
                double length = 0;
                int NBAR = 0;
                int diameter = 0;
                double DIS = 0;
                #region//تحميل مستطيلات التسليح
                if (SelectedRecBar == 0)
                {
                    RecBarsNumber = RecBarsNumber + 1;
                    RecLineBars EMP0 = new RecLineBars();
                    ((SectionDrawForm)sectionDrawForm).RecLineBar[RecBarsNumber] = EMP0;
                    SelectedRecBar1 = RecBarsNumber;
                }
            startloop: { }
                #region//خاصة بالقضبان و الاساور
                for (int k = 1; k < BarsNumber + 1; k++)
                {
                    if (((SectionDrawForm)sectionDrawForm).Bar[k].InREC == SelectedRecBar1)
                    {
                        BarsNumber = BarsNumber - 1;
                        for (int l = k; l < BarsNumber + 1; l++)
                        {
                            ((SectionDrawForm)sectionDrawForm).Bar[l] = ((SectionDrawForm)sectionDrawForm).Bar[l + 1];
                        }
                        if (k <= BarsNumber) goto startloop;
                    }
                }
                for (int k = 1; k < BarsNumber + 1; k++)
                {
                    if (((SectionDrawForm)sectionDrawForm).Bar[k].InREC > SelectedRecBar1)
                    {
                        ((SectionDrawForm)sectionDrawForm).Bar[k].InREC = ((SectionDrawForm)sectionDrawForm).Bar[k].InREC - 1;
                    }
                }
                for (int k = SelectedRecBar1; k < RecBarsNumber; k++)
                {
                    ((SectionDrawForm)sectionDrawForm).RecLineBar[k] = ((SectionDrawForm)sectionDrawForm).RecLineBar[k + 1];
                }
                #endregion
                #region//خاصة بنوع المقطع
                for (int k = SelectedTeeShape; k < TeeShapeNumber; k++)//مضاف
                {
                    ((SectionDrawForm)sectionDrawForm).TeeShape[k] = ((SectionDrawForm)sectionDrawForm).TeeShape[k + 1];
                }
                for (int k = 1; k < RecBarsNumber; k++)//مضاف
                {
                    if (((SectionDrawForm)sectionDrawForm).RecLineBar[k].InTeeShape > SelectedTeeShape)
                    {
                        ((SectionDrawForm)sectionDrawForm).RecLineBar[k].InTeeShape = ((SectionDrawForm)sectionDrawForm).RecLineBar[k].InTeeShape - 1;
                    }
                }
                #endregion
                #region//خاصة بترتيب الاشكال
                for (int k = 1; k < RecShapeNumber + 1; k++)//مضاف///////////////////////
                {
                    if (((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars > SelectedRecBar1)
                    {
                        ((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars = ((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars - 1;
                    }
                }
                for (int k = 1; k < TeeShapeNumber; k++)//مضاف
                {
                    if (((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] > SelectedRecBar1)
                    {
                        ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] = ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] - 1;
                        ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[2] = ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[2] - 1;
                    }
                }
                for (int k = 1; k < FlangedWallShapeNumber+1; k++)//مضاف
                {
                    if (((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] > SelectedRecBar1)
                    {
                        ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] - 1;
                        ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[2] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[2] - 1;
                        ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[3] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[3] - 1;
                    }
                }
                #endregion
                if (SelectedRecBar2 > SelectedRecBar1) SelectedRecBar2 = SelectedRecBar2 - 1;
                SelectedTeeShape = TeeShapeNumber;
                SelectedRecBar1 = RecBarsNumber;
                RecLineBars emp = new RecLineBars();
                emp.InTeeShape = SelectedTeeShape;
                emp.InRecShape = 0;
                emp.InFlangedWallShape = 0;
                emp.Width = Math.Round (emp0.PointXReal[2] - emp0.PointXReal[1] - emp0.ED2CoverR - emp0.ED4CoverR,0);
                emp.Height = Math.Round (emp0.PointYReal[1] - emp0.PointYReal[8] - emp0.ED1CoverR - emp0.ED3CoverR,0);
                emp.CenterX = emp0.PointXReal[1] + emp0.ED4CoverR + emp.Width / 2;
                emp.CenterY = emp0.PointYReal[1] - emp0.ED1CoverR - emp.Height / 2;
                emp.TIEDR = Convert.ToInt32(textBox19.Text);
                emp.CORDR[1] = Convert.ToInt32(textBox11.Text);
                emp.CORDR[2] = Convert.ToInt32(textBox12.Text);
                emp.CORDR[3] = Convert.ToInt32(textBox13.Text);
                emp.CORDR[4] = Convert.ToInt32(textBox14.Text);
                emp.EDDR[1] = Convert.ToInt32(textBox31.Text);
                emp.EDDistance[1] = Convert.ToDouble(textBox30.Text);
                emp.EDBarsNumbers[1] = Convert.ToInt32(textBox29.Text);
                emp.EDX1R[1] = emp.CenterX - emp.Width / 2;
                emp.EDY1R[1] = emp.CenterY + emp.Height / 2;
                emp.EDX2R[1] = emp.CenterX + emp.Width / 2;
                emp.EDY2R[1] = emp.CenterY + emp.Height / 2;
                emp.EDDR[2] = Convert.ToInt32(textBox36.Text);
                emp.EDDistance[2] = Convert.ToDouble(textBox35.Text);
                emp.EDBarsNumbers[2] = Convert.ToInt32(textBox34.Text);
                emp.EDX1R[2] = emp.CenterX + emp.Width / 2;
                emp.EDY1R[2] = emp.CenterY + emp.Height / 2;
                emp.EDX2R[2] = emp.CenterX + emp.Width / 2;
                emp.EDY2R[2] = emp.CenterY - emp.Height / 2;
                emp.EDDR[3] = Convert.ToInt32(textBox41.Text);
                emp.EDDistance[3] = Convert.ToDouble(textBox40.Text);
                emp.EDBarsNumbers[3] = Convert.ToInt32(textBox39.Text);
                emp.EDX1R[3] = emp.CenterX + emp.Width / 2;
                emp.EDY1R[3] = emp.CenterY - emp.Height / 2;
                emp.EDX2R[3] = emp.CenterX - emp.Width / 2;
                emp.EDY2R[3] = emp.CenterY - emp.Height / 2;
                emp.EDDR[4] = Convert.ToInt32(textBox46.Text);
                emp.EDDistance[4] = Convert.ToDouble(textBox45.Text);
                emp.EDBarsNumbers[4] = Convert.ToInt32(textBox44.Text);
                emp.EDX1R[4] = emp.CenterX - emp.Width / 2;
                emp.EDY1R[4] = emp.CenterY - emp.Height / 2;
                emp.EDX2R[4] = emp.CenterX - emp.Width / 2;
                emp.EDY2R[4] = emp.CenterY + emp.Height / 2;
                int diameter1 = emp.TIEDR;
                for (int s = 1; s < 5; s++)
                {
                    int kl1 = 0;
                    int kl2 = 0;
                    if (s == 1 || s == 3)
                    {
                        length = emp.Width;
                        kl1 = 1;
                        kl2 = 0;
                    }
                    if (s == 2 || s == 4)
                    {
                        length = emp.Height;
                        kl1 = 0;
                        kl2 = 1;
                    }
                    NBAR = emp.EDBarsNumbers[s];
                    diameter = emp.EDDR[s];
                    DIS = Math.Round((length - 2 * diameter1) / (NBAR + 1), 3);
                    X1 = emp.EDX1R[s];
                    Y1 = emp.EDY1R[s];
                    for (int k = 1; k < NBAR + 1; k++)
                    {
                        BarsNumber = BarsNumber + 1;
                        Bars emp1 = new Bars();
                        emp1.XR = Math.Round(X1 + kl1 * kx[s] * (k * DIS) + kx[s] * diameter1 + kl2 * ky[s] * (diameter / 2), 3);
                        emp1.YR = Math.Round(Y1 + kl2 * ky[s] * (k * DIS) + ky[s] * diameter1 + kl1 * ky[s] * (diameter / 2), 3);
                        emp1.DR = diameter;
                        emp1.Selected = 0;
                        emp1.InLine = 0;
                        emp1.Type = 3;
                        emp1.InED = s;
                        emp1.InREC = RecBarsNumber;
                        emp1.Corner = 0;
                        emp1.InCircle = 0;
                        ((SectionDrawForm)sectionDrawForm).Bar[BarsNumber] = emp1;
                    }
                }
                for (int k = 1; k < 5; k++)
                {
                    BarsNumber = BarsNumber + 1;
                    Bars emp1 = new Bars();
                    diameter = emp.CORDR[k];
                    if (k == 1)
                    {
                        emp1.XR = Math.Round(emp0.PointXReal[1] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED4CoverR, 3);
                        emp1.YR = Math.Round(emp0.PointYReal[1] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED1CoverR, 3);
                    }
                    if (k == 2)
                    {
                        emp1.XR = Math.Round(emp0.PointXReal[2] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED2CoverR, 3);
                        emp1.YR = Math.Round(emp0.PointYReal[2] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED1CoverR, 3);
                    }
                    if (k == 3)
                    {
                        emp1.XR = Math.Round(emp0.PointXReal[3] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED2CoverR, 3);
                        emp1.YR = Math.Round(emp0.PointYReal[3] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED3CoverR, 3);
                    }
                    if (k == 4)
                    {
                        emp1.XR = Math.Round(emp0.PointXReal[8] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED4CoverR, 3);
                        emp1.YR = Math.Round(emp0.PointYReal[8] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED3CoverR, 3);
                    }
                    emp1.DR = diameter;
                    emp1.Selected = 0;
                    emp1.InLine = 0;
                    emp1.Type = 4;
                    emp1.InED = 0;
                    emp1.InCircle = 0;
                    emp1.Corner = k;
                    emp1.InREC = RecBarsNumber;
                    ((SectionDrawForm)sectionDrawForm).Bar[BarsNumber] = emp1;
                }
                ((SectionDrawForm)sectionDrawForm).RecLineBar[RecBarsNumber] = emp;
                ((SectionDrawForm)sectionDrawForm).BarsNumber = BarsNumber;
                #endregion
                #region//تحميل مستطيلات التسليح
                if (SelectedRecBar == 0)
                {
                    RecBarsNumber = RecBarsNumber + 1;
                    RecLineBars EMP00 = new RecLineBars();
                    ((SectionDrawForm)sectionDrawForm).RecLineBar[RecBarsNumber] = EMP00;
                    SelectedRecBar2 = RecBarsNumber;
                }
            startloopL: { }
                #region//خاصة بالقضبان و الاساور
                for (int k = 1; k < BarsNumber + 1; k++)
                {
                    if (((SectionDrawForm)sectionDrawForm).Bar[k].InREC == SelectedRecBar2)
                    {
                        BarsNumber = BarsNumber - 1;
                        for (int l = k; l < BarsNumber + 1; l++)
                        {
                            ((SectionDrawForm)sectionDrawForm).Bar[l] = ((SectionDrawForm)sectionDrawForm).Bar[l + 1];
                        }
                        if (k <= BarsNumber) goto startloopL;
                    }
                }
                for (int k = 1; k < BarsNumber + 1; k++)
                {
                    if (((SectionDrawForm)sectionDrawForm).Bar[k].InREC > SelectedRecBar2)
                    {
                        ((SectionDrawForm)sectionDrawForm).Bar[k].InREC = ((SectionDrawForm)sectionDrawForm).Bar[k].InREC - 1;
                    }
                }
                for (int k = SelectedRecBar2; k < RecBarsNumber; k++)
                {
                    ((SectionDrawForm)sectionDrawForm).RecLineBar[k] = ((SectionDrawForm)sectionDrawForm).RecLineBar[k + 1];
                }
                #endregion
                #region//خاصة بنوع المقطع
                for (int k = SelectedTeeShape; k < TeeShapeNumber; k++)//مضاف
                {
                    ((SectionDrawForm)sectionDrawForm).TeeShape[k] = ((SectionDrawForm)sectionDrawForm).TeeShape[k + 1];
                }
                for (int k = 1; k < RecBarsNumber; k++)//مضاف
                {
                    if (((SectionDrawForm)sectionDrawForm).RecLineBar[k].InTeeShape > SelectedTeeShape)
                    {
                        ((SectionDrawForm)sectionDrawForm).RecLineBar[k].InTeeShape = ((SectionDrawForm)sectionDrawForm).RecLineBar[k].InTeeShape - 1;
                    }
                }
                #endregion
                #region//خاصة بترتيب الاشكال
                for (int k = 1; k < RecShapeNumber + 1; k++)//مضاف///////////////////////
                {
                    if (((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars > SelectedRecBar2)
                    {
                        ((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars = ((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars - 1;
                    }
                }
                for (int k = 1; k < TeeShapeNumber; k++)//مضاف
                {
                    if (((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] > SelectedRecBar2)
                    {
                        ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] = ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] - 1;
                        ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[2] = ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[2] - 1;
                    }
                }
                for (int k = 1; k < FlangedWallShapeNumber+1; k++)//مضاف
                {
                    if (((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] > SelectedRecBar2)
                    {
                        ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] - 1;
                        ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[2] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[2] - 1;
                        ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[3] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[3] - 1;
                    }
                }
                #endregion
                SelectedTeeShape = TeeShapeNumber;
                SelectedRecBar2 = RecBarsNumber;
                emp = new RecLineBars();
                emp.InTeeShape = SelectedTeeShape;
                emp.InFlangedWallShape = 0;
                emp.InRecShape = 0;
                emp.Width = Math.Round (emp0.PointXReal[4] - emp0.PointXReal[7] - emp0.ED2cCoverR - emp0.ED4cCoverR,0);
                emp.Height = Math.Round (emp0.PointYReal[1] - emp0.PointYReal[6] - emp0.ED1cCoverR - emp0.ED3cCoverR,0);
                emp.CenterX = emp0.PointXReal[7] + emp0.ED4cCoverR + emp.Width / 2;
                emp.CenterY = emp0.PointYReal[1] - emp0.ED1cCoverR - emp.Height / 2;
                emp.TIEDR = Convert.ToInt32(textBox15.Text);
                emp.CORDR[1] = Convert.ToInt32(textBox20.Text);
                emp.CORDR[2] = Convert.ToInt32(textBox18.Text);
                emp.CORDR[3] = Convert.ToInt32(textBox16.Text);
                emp.CORDR[4] = Convert.ToInt32(textBox17.Text);
                emp.EDDR[1] = Convert.ToInt32(textBox68.Text);
                emp.EDDistance[1] = Convert.ToDouble(textBox67.Text);
                emp.EDBarsNumbers[1] = Convert.ToInt32(textBox66.Text);
                emp.EDX1R[1] = emp.CenterX - emp.Width / 2;
                emp.EDY1R[1] = emp.CenterY + emp.Height / 2;
                emp.EDX2R[1] = emp.CenterX + emp.Width / 2;
                emp.EDY2R[1] = emp.CenterY + emp.Height / 2;
                emp.EDDR[2] = Convert.ToInt32(textBox62.Text);
                emp.EDDistance[2] = Convert.ToDouble(textBox61.Text);
                emp.EDBarsNumbers[2] = Convert.ToInt32(textBox60.Text);
                emp.EDX1R[2] = emp.CenterX + emp.Width / 2;
                emp.EDY1R[2] = emp.CenterY + emp.Height / 2;
                emp.EDX2R[2] = emp.CenterX + emp.Width / 2;
                emp.EDY2R[2] = emp.CenterY - emp.Height / 2;
                emp.EDDR[3] = Convert.ToInt32(textBox56.Text);
                emp.EDDistance[3] = Convert.ToDouble(textBox55.Text);
                emp.EDBarsNumbers[3] = Convert.ToInt32(textBox54.Text);
                emp.EDX1R[3] = emp.CenterX + emp.Width / 2;
                emp.EDY1R[3] = emp.CenterY - emp.Height / 2;
                emp.EDX2R[3] = emp.CenterX - emp.Width / 2;
                emp.EDY2R[3] = emp.CenterY - emp.Height / 2;
                emp.EDDR[4] = Convert.ToInt32(textBox26.Text);
                emp.EDDistance[4] = Convert.ToDouble(textBox25.Text);
                emp.EDBarsNumbers[4] = Convert.ToInt32(textBox24.Text);
                emp.EDX1R[4] = emp.CenterX - emp.Width / 2;
                emp.EDY1R[4] = emp.CenterY - emp.Height / 2;
                emp.EDX2R[4] = emp.CenterX - emp.Width / 2;
                emp.EDY2R[4] = emp.CenterY + emp.Height / 2;
                diameter1 = emp.TIEDR;
                for (int s = 1; s < 5; s++)
                {
                    int kl1 = 0;
                    int kl2 = 0;
                    if (s == 1 || s == 3)
                    {
                        length = emp.Width;
                        kl1 = 1;
                        kl2 = 0;
                    }
                    if (s == 2 || s == 4)
                    {
                        length = emp.Height;
                        kl1 = 0;
                        kl2 = 1;
                    }
                    NBAR = emp.EDBarsNumbers[s];
                    diameter = emp.EDDR[s];
                    DIS = Math.Round((length - 2 * diameter1) / (NBAR + 1), 3);
                    X1 = emp.EDX1R[s];
                    Y1 = emp.EDY1R[s];
                    for (int k = 1; k < NBAR + 1; k++)
                    {
                        BarsNumber = BarsNumber + 1;
                        Bars emp1 = new Bars();
                        emp1.XR = Math.Round(X1 + kl1 * kx[s] * (k * DIS) + kx[s] * diameter1 + kl2 * ky[s] * (diameter / 2), 3);
                        emp1.YR = Math.Round(Y1 + kl2 * ky[s] * (k * DIS) + ky[s] * diameter1 + kl1 * ky[s] * (diameter / 2), 3);
                        emp1.DR = diameter;
                        emp1.Selected = 0;
                        emp1.InLine = 0;
                        emp1.Type = 3;
                        emp1.InED = s;
                        emp1.InREC = RecBarsNumber;
                        emp1.Corner = 0;
                        emp1.InCircle = 0;
                        ((SectionDrawForm)sectionDrawForm).Bar[BarsNumber] = emp1;
                    }
                }
                for (int k = 1; k < 5; k++)
                {
                    BarsNumber = BarsNumber + 1;
                    Bars emp1 = new Bars();
                    diameter = emp.CORDR[k];
                    if (k == 1)
                    {
                        emp1.XR = Math.Round(emp0.PointXReal[7] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED4cCoverR, 3);
                        emp1.YR = Math.Round(emp0.PointYReal[1] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED1cCoverR, 3);
                    }
                    if (k == 2)
                    {
                        emp1.XR = Math.Round(emp0.PointXReal[4] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED2cCoverR, 3);
                        emp1.YR = Math.Round(emp0.PointYReal[1] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED1cCoverR, 3);
                    }
                    if (k == 3)
                    {
                        emp1.XR = Math.Round(emp0.PointXReal[5] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED2cCoverR, 3);
                        emp1.YR = Math.Round(emp0.PointYReal[5] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED3cCoverR, 3);
                    }
                    if (k == 4)
                    {
                        emp1.XR = Math.Round(emp0.PointXReal[6] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED4cCoverR, 3);
                        emp1.YR = Math.Round(emp0.PointYReal[6] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED3cCoverR, 3);
                    }
                    emp1.DR = diameter;
                    emp1.Selected = 0;
                    emp1.InLine = 0;
                    emp1.Type = 4;
                    emp1.InED = 0;
                    emp1.InCircle = 0;
                    emp1.Corner = k;
                    emp1.InREC = RecBarsNumber;
                    ((SectionDrawForm)sectionDrawForm).Bar[BarsNumber] = emp1;
                }
                ((SectionDrawForm)sectionDrawForm).RecLineBar[RecBarsNumber] = emp;
                ((SectionDrawForm)sectionDrawForm).BarsNumber = BarsNumber;
                #endregion
                emp0.ApplyedToRecBars[1] = RecBarsNumber - 1;
                emp0.ApplyedToRecBars[2] = RecBarsNumber;
                emp0.HasReinforcment = comboBox1.SelectedIndex;
                ((SectionDrawForm)sectionDrawForm).RecBarsNumber = RecBarsNumber;
            }
            #endregion
            else
            #region//لا يوجد تسليح
            {
                if (SelectedRecBar != 0)
                {
                    emp0.ApplyedToRecBars[1] = 0;
                    emp0.ApplyedToRecBars[2] = 0;
                    emp0.HasReinforcment = 0;
                    ((SectionDrawForm)sectionDrawForm).RecLineBar[SelectedRecBar1].InFlangedWallShape = 0;
                startloop: { }
                    #region
                    #region//خاصة بالقضبان و الاساور
                    for (int k = 1; k < BarsNumber + 1; k++)
                    {
                        if (((SectionDrawForm)sectionDrawForm).Bar[k].InREC == SelectedRecBar1)
                        {
                            BarsNumber = BarsNumber - 1;
                            for (int l = k; l < BarsNumber + 1; l++)
                            {
                                ((SectionDrawForm)sectionDrawForm).Bar[l] = ((SectionDrawForm)sectionDrawForm).Bar[l + 1];
                            }
                            if (k <= BarsNumber) goto startloop;
                        }
                    }
                    for (int k = 1; k < BarsNumber + 1; k++)
                    {
                        if (((SectionDrawForm)sectionDrawForm).Bar[k].InREC > SelectedRecBar1)
                        {
                            ((SectionDrawForm)sectionDrawForm).Bar[k].InREC = ((SectionDrawForm)sectionDrawForm).Bar[k].InREC - 1;
                        }
                    }
                    for (int k = SelectedRecBar1; k < RecBarsNumber; k++)
                    {
                        ((SectionDrawForm)sectionDrawForm).RecLineBar[k] = ((SectionDrawForm)sectionDrawForm).RecLineBar[k + 1];
                    }
                    #endregion
                    #region//خاصة بترتيب الاشكال
                    for (int k = 1; k < RecShapeNumber + 1; k++)//مضاف///////////////////////
                    {
                        if (((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars > SelectedRecBar1)
                        {
                            ((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars = ((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars - 1;
                        }
                    }
                    for (int k = 1; k < TeeShapeNumber+1; k++)//مضاف
                    {
                        if (((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] > SelectedRecBar1)
                        {
                            ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] = ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] - 1;
                            ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[2] = ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[2] - 1;
                        }
                    }
                    for (int k = 1; k < FlangedWallShapeNumber+1; k++)//مضاف
                    {
                        if (((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] > SelectedRecBar1)
                        {
                            ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] - 1;
                            ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[2] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[2] - 1;
                            ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[3] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[3] - 1;
                        }
                    }
                    #endregion
                    RecBarsNumber = RecBarsNumber - 1;
                    #endregion
                    #region
                    if (SelectedRecBar2 > SelectedRecBar1) SelectedRecBar2 = SelectedRecBar2 - 1;
                    ((SectionDrawForm)sectionDrawForm).RecLineBar[SelectedRecBar2].InFlangedWallShape = 0;
                startloopL: { }
                    #region//خاصة بالقضبان و الاساور
                    for (int k = 1; k < BarsNumber + 1; k++)
                    {
                        if (((SectionDrawForm)sectionDrawForm).Bar[k].InREC == SelectedRecBar2)
                        {
                            BarsNumber = BarsNumber - 1;
                            for (int l = k; l < BarsNumber + 1; l++)
                            {
                                ((SectionDrawForm)sectionDrawForm).Bar[l] = ((SectionDrawForm)sectionDrawForm).Bar[l + 1];
                            }
                            if (k <= BarsNumber) goto startloopL;
                        }
                    }
                    for (int k = 1; k < BarsNumber + 1; k++)
                    {
                        if (((SectionDrawForm)sectionDrawForm).Bar[k].InREC > SelectedRecBar2)
                        {
                            ((SectionDrawForm)sectionDrawForm).Bar[k].InREC = ((SectionDrawForm)sectionDrawForm).Bar[k].InREC - 1;
                        }
                    }
                    for (int k = SelectedRecBar2; k < RecBarsNumber; k++)
                    {
                        ((SectionDrawForm)sectionDrawForm).RecLineBar[k] = ((SectionDrawForm)sectionDrawForm).RecLineBar[k + 1];
                    }
                    #endregion
                    #region//خاصة بترتيب الاشكال
                    for (int k = 1; k < RecShapeNumber + 1; k++)//مضاف///////////////////////
                    {
                        if (((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars > SelectedRecBar2)
                        {
                            ((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars = ((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars - 1;
                        }
                    }
                    for (int k = 1; k < TeeShapeNumber+1; k++)//مضاف
                    {
                        if (((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] > SelectedRecBar2)
                        {
                            ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] = ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] - 1;
                            ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[2] = ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[2] - 1;
                        }
                    }
                    for (int k = 1; k < FlangedWallShapeNumber+1; k++)//مضاف
                    {
                        if (((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] > SelectedRecBar2)
                        {
                            ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] - 1;
                            ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[2] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[2] - 1;
                            ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[3] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[3] - 1;
                        }
                    }
                    #endregion
                    RecBarsNumber = RecBarsNumber - 1;
                    #endregion
                    ((SectionDrawForm)sectionDrawForm).RecBarsNumber = RecBarsNumber;
                    ((SectionDrawForm)sectionDrawForm).BarsNumber = BarsNumber;
                }
            }
            #endregion
            #endregion
            ((SectionDrawForm)sectionDrawForm).TeeShape[SelectedTeeShape] = emp0;
            ((SectionDrawForm)sectionDrawForm).MakeTempFiles();
            ((SectionDrawForm)sectionDrawForm).Render2d();
            this.Close(); 
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                tabControl1.Visible = false;
            }
            else
            {
                tabControl1.Visible = true;
            }
            if (loaded == 1)
            {
                BarOrDis = 2;
                SelectedInED = 1;
                calcrecRECALL();
                calcrecRECALLL();
                SelectedInED = 2;
                calcrecRECALL();
                calcrecRECALLL();
                SelectedInED = 3;
                calcrecRECALL();
                calcrecRECALLL();
                SelectedInED = 4;
                calcrecRECALL();
                calcrecRECALLL();
            }
        }

        private void SectionTeeForm_Load(object sender, EventArgs e)
        {
            kx[1] = 1;
            ky[1] = -1;
            kx[2] = -1;
            ky[2] = -1;
            kx[3] = -1;
            ky[3] = 1;
            kx[4] = 1;
            ky[4] = 1;
            SelectedTeeShape = ((SectionDrawForm)sectionDrawForm).SelectedTeeShape;
            textBox1.Text = ((SectionDrawForm)sectionDrawForm).TeeShape[SelectedTeeShape].CenterX.ToString();
            textBox2.Text = ((SectionDrawForm)sectionDrawForm).TeeShape[SelectedTeeShape].CenterY.ToString();
            textBox3.Text = ((SectionDrawForm)sectionDrawForm).TeeShape[SelectedTeeShape].Height.ToString();
            textBox4.Text = ((SectionDrawForm)sectionDrawForm).TeeShape[SelectedTeeShape].FlangWidth.ToString();
            textBox5.Text = ((SectionDrawForm)sectionDrawForm).TeeShape[SelectedTeeShape].FlangThickness.ToString();
            textBox6.Text = ((SectionDrawForm)sectionDrawForm).TeeShape[SelectedTeeShape].WebThickness.ToString();

            textBox47.Text = ((SectionDrawForm)sectionDrawForm).TeeShape[SelectedTeeShape].ED1CoverR.ToString();
            textBox48.Text = ((SectionDrawForm)sectionDrawForm).TeeShape[SelectedTeeShape].ED2CoverR.ToString();
            textBox49.Text = ((SectionDrawForm)sectionDrawForm).TeeShape[SelectedTeeShape].ED3CoverR.ToString();
            textBox50.Text = ((SectionDrawForm)sectionDrawForm).TeeShape[SelectedTeeShape].ED4CoverR.ToString();

            textBox63.Text = ((SectionDrawForm)sectionDrawForm).TeeShape[SelectedTeeShape].ED1cCoverR.ToString();
            textBox57.Text = ((SectionDrawForm)sectionDrawForm).TeeShape[SelectedTeeShape].ED2cCoverR.ToString();
            textBox51.Text = ((SectionDrawForm)sectionDrawForm).TeeShape[SelectedTeeShape].ED3cCoverR.ToString();
            textBox21.Text = ((SectionDrawForm)sectionDrawForm).TeeShape[SelectedTeeShape].ED4cCoverR.ToString();
            comboBox1.SelectedIndex = ((SectionDrawForm)sectionDrawForm).TeeShape[SelectedTeeShape].HasReinforcment;
            if (comboBox1.SelectedIndex != 0)
            {
                tabControl1.Visible = true;
                #region//كامل المستطيل
                double realdis = 0;
                double Length = 0;
                int Number = 0;
                int L = ((SectionDrawForm)sectionDrawForm).SelectedRecBar1;
                int diameter1 = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].TIEDR;
                textBox19.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].TIEDR.ToString();
                textBox11.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].CORDR[1].ToString();
                textBox12.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].CORDR[2].ToString();
                textBox13.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].CORDR[3].ToString();
                textBox14.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].CORDR[4].ToString();
                textBox31.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDDR[1].ToString();
                textBox30.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDDistance[1].ToString();
                textBox29.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDBarsNumbers[1].ToString();
                textBox28.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].Width.ToString();
                Length = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].Width;
                Number = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDBarsNumbers[1];
                realdis = Math.Round((Length - 2 * diameter1) / (Number + 1), 3);
                textBox27.Text = realdis.ToString();
                textBox36.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDDR[2].ToString();
                textBox35.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDDistance[2].ToString();
                textBox34.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDBarsNumbers[2].ToString();
                textBox33.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].Height.ToString();
                Length = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].Height;
                Number = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDBarsNumbers[2];
                realdis = Math.Round((Length - 2 * diameter1) / (Number + 1), 3);
                textBox32.Text = realdis.ToString();
                textBox41.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDDR[3].ToString();
                textBox40.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDDistance[3].ToString();
                textBox39.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDBarsNumbers[3].ToString();
                textBox38.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].Width.ToString();
                Length = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].Width;
                Number = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDBarsNumbers[3];
                realdis = Math.Round((Length - 2 * diameter1) / (Number + 1), 3);
                textBox37.Text = realdis.ToString();
                textBox46.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDDR[4].ToString();
                textBox45.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDDistance[4].ToString();
                textBox44.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDBarsNumbers[4].ToString();
                textBox43.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].Height.ToString();
                Length = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].Height;
                Number = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDBarsNumbers[4];
                realdis = Math.Round((Length - 2 * diameter1) / (Number + 1), 3);
                textBox42.Text = realdis.ToString();
                #endregion
                #region//كامل المستطيل
                L = ((SectionDrawForm)sectionDrawForm).SelectedRecBar2;
                diameter1 = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].TIEDR;
                textBox15.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].TIEDR.ToString();
                textBox20.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].CORDR[1].ToString();
                textBox18.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].CORDR[2].ToString();
                textBox16.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].CORDR[3].ToString();
                textBox17.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].CORDR[4].ToString();

                textBox68.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDDR[1].ToString();
                textBox67.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDDistance[1].ToString();
                textBox66.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDBarsNumbers[1].ToString();
                textBox65.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].Width.ToString();
                Length = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].Width;
                Number = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDBarsNumbers[1];
                realdis = Math.Round((Length - 2 * diameter1) / (Number + 1), 3);
                textBox64.Text = realdis.ToString();

                textBox62.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDDR[2].ToString();
                textBox61.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDDistance[2].ToString();
                textBox60.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDBarsNumbers[2].ToString();
                textBox59.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].Height.ToString();
                Length = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].Height;
                Number = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDBarsNumbers[2];
                realdis = Math.Round((Length - 2 * diameter1) / (Number + 1), 3);
                textBox58.Text = realdis.ToString();

                textBox56.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDDR[3].ToString();
                textBox55.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDDistance[3].ToString();
                textBox54.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDBarsNumbers[3].ToString();
                textBox53.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].Width.ToString();
                Length = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].Width;
                Number = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDBarsNumbers[3];
                realdis = Math.Round((Length - 2 * diameter1) / (Number + 1), 3);
                textBox52.Text = realdis.ToString();

                textBox26.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDDR[4].ToString();
                textBox25.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDDistance[4].ToString();
                textBox24.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDBarsNumbers[4].ToString();
                textBox23.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].Height.ToString();
                Length = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].Height;
                Number = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDBarsNumbers[4];
                realdis = Math.Round((Length - 2 * diameter1) / (Number + 1), 3);
                textBox22.Text = realdis.ToString();
                #endregion
            }
            loaded = 1;
        }

        private void textBox30_KeyUp(object sender, KeyEventArgs e)
        {
            SelectedInED = 1;
            BarOrDis = 2;
            calcrecRECALL();
        }

        private void textBox35_KeyUp(object sender, KeyEventArgs e)
        {
            SelectedInED = 2;
            BarOrDis = 2;
            calcrecRECALL();
        }

        private void textBox40_KeyUp(object sender, KeyEventArgs e)
        {
            SelectedInED = 3;
            BarOrDis = 2;
            calcrecRECALL();
        }

        private void textBox45_KeyUp(object sender, KeyEventArgs e)
        {
            SelectedInED = 4;
            BarOrDis = 2;
            calcrecRECALL();
        }

        private void textBox67_KeyUp(object sender, KeyEventArgs e)
        {
            SelectedInED = 1;
            BarOrDis = 2;
            calcrecRECALLL();
        }

        private void textBox61_KeyUp(object sender, KeyEventArgs e)
        {
            SelectedInED = 2;
            BarOrDis = 2;
            calcrecRECALLL();
        }

        private void textBox55_KeyUp(object sender, KeyEventArgs e)
        {
            SelectedInED = 3;
            BarOrDis = 2;
            calcrecRECALLL();
        }

        private void textBox25_KeyUp(object sender, KeyEventArgs e)
        {
            SelectedInED = 4;
            BarOrDis = 2;
            calcrecRECALLL();
        }

        private void textBox29_KeyUp(object sender, KeyEventArgs e)
        {
            SelectedInED = 1;
            BarOrDis = 1;
            calcrecRECALL();
        }

        private void textBox34_KeyUp(object sender, KeyEventArgs e)
        {
            SelectedInED = 2;
            BarOrDis = 1;
            calcrecRECALL();
        }

        private void textBox39_KeyUp(object sender, KeyEventArgs e)
        {
            SelectedInED = 3;
            BarOrDis = 1;
            calcrecRECALL();
        }

        private void textBox44_KeyUp(object sender, KeyEventArgs e)
        {
            SelectedInED = 4;
            BarOrDis = 1;
            calcrecRECALL();
        }

        private void textBox66_KeyUp(object sender, KeyEventArgs e)
        {
            SelectedInED = 1;
            BarOrDis = 1;
            calcrecRECALLL();
        }

        private void textBox60_KeyUp(object sender, KeyEventArgs e)
        {
            SelectedInED = 2;
            BarOrDis = 1;
            calcrecRECALLL();
        }

        private void textBox54_MouseUp(object sender, MouseEventArgs e)
        {
            SelectedInED = 3;
            BarOrDis = 1;
            calcrecRECALLL();
        }

        private void textBox24_KeyUp(object sender, KeyEventArgs e)
        {
            SelectedInED = 4;
            BarOrDis = 1;
            calcrecRECALLL();
        }

        private void textBox47_KeyUp(object sender, KeyEventArgs e)
        {
            BarOrDis = 2;
            SelectedInED = 2;
            calcrecRECALL();
            SelectedInED = 4;
            calcrecRECALL();
        }

        private void textBox49_KeyUp(object sender, KeyEventArgs e)
        {
            BarOrDis = 2;
            SelectedInED = 2;
            calcrecRECALL();
            SelectedInED = 4;
            calcrecRECALL();
        }

        private void textBox48_KeyUp(object sender, KeyEventArgs e)
        {
            BarOrDis = 2;
            SelectedInED = 1;
            calcrecRECALL();
            SelectedInED = 3;
            calcrecRECALL();
        }

        private void textBox50_KeyUp(object sender, KeyEventArgs e)
        {
            BarOrDis = 2;
            SelectedInED = 1;
            calcrecRECALL();
            SelectedInED = 3;
            calcrecRECALL();
        }

        private void textBox63_KeyUp(object sender, KeyEventArgs e)
        {
            BarOrDis = 2;
            SelectedInED = 2;
            calcrecRECALLL();
            SelectedInED = 4;
            calcrecRECALLL();
        }

        private void textBox51_KeyUp(object sender, KeyEventArgs e)
        {
            BarOrDis = 2;
            SelectedInED = 2;
            calcrecRECALLL();
            SelectedInED = 4;
            calcrecRECALLL();
        }

        private void textBox57_KeyUp(object sender, KeyEventArgs e)
        {
            BarOrDis = 2;
            SelectedInED = 1;
            calcrecRECALLL();
            SelectedInED = 3;
            calcrecRECALLL();
        }

        private void textBox21_KeyUp(object sender, KeyEventArgs e)
        {
            BarOrDis = 2;
            SelectedInED = 1;
            calcrecRECALLL();
            SelectedInED = 3;
            calcrecRECALLL();
        }
    }
}
