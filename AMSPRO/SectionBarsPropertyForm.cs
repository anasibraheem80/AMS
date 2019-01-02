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
    public partial class SectionBarsPropertyForm : Form
    {
        public SectionBarsPropertyForm()
        {
            InitializeComponent();
        }
        Form sectionDrawForm = Application.OpenForms["SectionDrawForm"];
        int WallType = 0;
        int loaded = 0;
        int ListChanged = 0;
        int BarOrDis = 1;
        int HasEndBars = 0;
        int TheBars = 0;
        double TheDistance = 0;
        int SelecteLinedBar=0;
        int SelecteRecLinedBar = 0;
        int SelectedInED = 0;
        int[] kx = new int[5];
        int[] ky = new int[5];
        private void calc()
        {
            double theX1 = Convert.ToDouble(textBox1.Text);
            double theY1 = Convert.ToDouble(textBox2.Text);
            int theD = Convert.ToInt32(textBox3.Text);
            double theX2 = Convert.ToDouble(textBox4.Text);
            double theY2 = Convert.ToDouble(textBox5.Text);
            double DX = theX2 - theX1;
            double DY = theY2 - theY1;
            double length = Math.Sqrt(Math.Pow(DX, 2) + Math.Pow(DY, 2));
            if (BarOrDis == 1)
            {
                if (ListChanged == 1)
                {
                    if (HasEndBars == 1)
                    {
                        textBox6.Text = (Convert.ToInt32(textBox6.Text) + 2).ToString();
                    }
                    else
                    {
                        textBox6.Text = (Convert.ToInt32(textBox6.Text) - 2).ToString();
                    }
                }
                TheBars = Convert.ToInt32(textBox6.Text);
                if (HasEndBars == 1) TheBars = TheBars - 2;
                TheDistance = Math.Round(length / (TheBars + 1), 3);
                textBox7.Text = TheDistance.ToString();
            }
            if (BarOrDis == 2)
            {
                TheDistance = Convert.ToDouble(textBox7.Text);
                TheBars = Convert.ToInt32(Math.Ceiling(length / TheDistance)) - 1;
                TheDistance = Math.Round(length / (TheBars + 1), 3);
                if (HasEndBars == 1) TheBars = TheBars + 2;
                textBox6.Text = TheBars.ToString();
            }
            textBox10.Text = TheDistance.ToString();
            textBox8.Text = Math.Round(length, 0).ToString();
        }
        private void calcrecED()
        {
            int K = ((SectionDrawForm)sectionDrawForm).SelectedInED;
            int L = ((SectionDrawForm)sectionDrawForm).SelectedRecBar;
            double theX1 = 0;
            double theY1 = 0;
            double theX2 = 0;
            double theY2 = 0;
            int diameter1 = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].TIEDR;
                theX1 = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDX1R[K];
                theY1 = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDY1R[K];
                theX2 = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDX2R[K];
                theY2 = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDY2R[K];
            double DX = theX2 - theX1;
            double DY = theY2 - theY1;
            double length = Math.Sqrt(Math.Pow(DX, 2) + Math.Pow(DY, 2));
            if (BarOrDis == 1)
            {
                TheBars = Convert.ToInt32(EDNTxt.Text);
                TheDistance = Math.Round((length-2*diameter1) / (TheBars + 1), 3);
               if (loaded == 1) EDMaxSTxt.Text = TheDistance.ToString();
            }
            if (BarOrDis == 2)
            {
                TheDistance = Convert.ToDouble(EDMaxSTxt.Text);
                TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                TheDistance = Math.Round((length - 2 * diameter1) / (TheBars + 1), 3);
                EDNTxt.Text = TheBars.ToString();
            }
            textBox22.Text = TheDistance.ToString();
            textBox21.Text = Math.Round(length, 0).ToString();
        }
        private void calcrecRECALL()
        {
            int K = SelectedInED;
            int diameter1 = 0;
            #region
            if (comboBox2.SelectedIndex != 1)
            {
                diameter1 = Convert.ToInt32(textBox19.Text);
                if (K == 1)
                {
                    double length = Convert.ToDouble(textBox15.Text);
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
                    double length = Convert.ToDouble(textBox16.Text);
                    if (BarOrDis == 1)
                    {
                        TheBars = Convert.ToInt32(textBox34.Text);
                        TheDistance = Math.Round((length - 2 * diameter1) / (TheBars + 1),3);
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
                    double length = Convert.ToDouble(textBox15.Text);
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
                    double length = Convert.ToDouble(textBox16.Text);
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
            #region
            if (comboBox2.SelectedIndex == 1)
            {
                diameter1 = diameter1 = Convert.ToInt32(textBox19.Text);
                double CoverED1 = Convert.ToDouble(textBox47.Text);
                double CoverED2 = Convert.ToDouble(textBox48.Text);
                double CoverED3 = Convert.ToDouble(textBox49.Text);
                double CoverED4 = Convert.ToDouble(textBox50.Text);
                if (K == 1)
                {
                    double length = Convert.ToDouble(textBox15.Text) - CoverED2 - CoverED4;
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
                    double length = Convert.ToDouble(textBox16.Text) - CoverED1 - CoverED3; 
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
                    double length = Convert.ToDouble(textBox15.Text) - CoverED2 - CoverED4; ;
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
                    double length = Convert.ToDouble(textBox16.Text) - CoverED1 - CoverED3; 
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
        private void button2_Click(object sender, EventArgs e)
        {
            ((SectionDrawForm)sectionDrawForm).UnSellectAll();
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int i = ((SectionDrawForm)sectionDrawForm).SelectedBar;
            int j = ((SectionDrawForm)sectionDrawForm).SelecteLinedBar;
            int O = ((SectionDrawForm)sectionDrawForm).SelectedType;
            int KK = ((SectionDrawForm)sectionDrawForm).SelectedInED;
            int MM = ((SectionDrawForm)sectionDrawForm).SelectedCorner;
            int SelectedRecBar = ((SectionDrawForm)sectionDrawForm).SelectedRecBar;
            int SelectedRecShape = ((SectionDrawForm)sectionDrawForm).SelectedRecShape;
            int SelectedCircleLineBar = ((SectionDrawForm)sectionDrawForm).SelectedCircleLineBar;
            int SelectedCircleShape = ((SectionDrawForm)sectionDrawForm).SelectedCircleShape;
            int FlangedWallShapeNumber = ((SectionDrawForm)sectionDrawForm).FlangedWallShapeNumber;
            int TeeShapeNumber = ((SectionDrawForm)sectionDrawForm).TeeShapeNumber;
            #region///قضيب نقطة
            if (O == 1)
            {
                double theX1 = Convert.ToDouble(textBox25.Text);
                double theY1 = Convert.ToDouble(textBox26.Text);
                int theD = Convert.ToInt32(textBox24.Text);
                ((SectionDrawForm)sectionDrawForm).Bar[i].XR = theX1;
                ((SectionDrawForm)sectionDrawForm).Bar[i].YR = theY1;
                ((SectionDrawForm)sectionDrawForm).Bar[i].DR = theD;
                ((SectionDrawForm)sectionDrawForm).MakeTempFiles();
                ((SectionDrawForm)sectionDrawForm).Render2d();
            }
            #endregion
            #region///قضيب خط
            if (O == 2)
            {
                double theX1 = Convert.ToDouble(textBox1.Text);
                double theY1 = Convert.ToDouble(textBox2.Text);
                int theD = Convert.ToInt32(textBox3.Text);
                double theX2 = Convert.ToDouble(textBox4.Text);
                double theY2 = Convert.ToDouble(textBox5.Text);
                int theBars = Convert.ToInt32(textBox6.Text);
                double theDistance = Convert.ToDouble(textBox7.Text);
                int BarsNumber = ((SectionDrawForm)sectionDrawForm).BarsNumber;
                int LineBarsNumber = ((SectionDrawForm)sectionDrawForm).LineBarsNumber;
            startloop: { }
                for (int k = 1; k < BarsNumber + 1; k++)
                {
                    if (((SectionDrawForm)sectionDrawForm).Bar[k].InLine == j)
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
                    if (((SectionDrawForm)sectionDrawForm).Bar[k].InLine > j)
                    {
                        ((SectionDrawForm)sectionDrawForm).Bar[k].InLine = ((SectionDrawForm)sectionDrawForm).Bar[k].InLine - 1;
                    }
                }
                LineBarsNumber = LineBarsNumber - 1;
                for (int k = j; k < LineBarsNumber + 1; k++)
                {
                    ((SectionDrawForm)sectionDrawForm).LineBar[k] = ((SectionDrawForm)sectionDrawForm).LineBar[k + 1];
                }

                double DX = theX2 - theX1;
                double DY = theY2 - theY1;
                double length = Math.Sqrt(Math.Pow(DX, 2) + Math.Pow(DY, 2));
                int NBAR = theBars;

                double DIS = Math.Round(length / (NBAR + 1), 3);
                if (HasEndBars == 1) DIS = Math.Round(length / (NBAR - 1), 3);

                LineBarsNumber = LineBarsNumber + 1;
                LineBars emp1 = new LineBars();
                emp1.X1R = theX1;
                emp1.Y1R = theY1;
                emp1.X2R = theX2;
                emp1.Y2R = theY2;
                emp1.Selected = 1;
                emp1.DR = theD;
                emp1.BarsNumbers = NBAR;
                emp1.Distance = theDistance;
                emp1.HasEndBars = comboBox1.SelectedIndex;
                ((SectionDrawForm)sectionDrawForm).LineBar[LineBarsNumber] = emp1;
                if (HasEndBars == 0)
                {
                    for (int k = 1; k < NBAR + 1; k++)
                    {
                        BarsNumber = BarsNumber + 1;
                        Bars emp = new Bars();
                        emp.XR = Math.Round(theX1 + (k * DIS) * DX / length, 3);
                        emp.YR = Math.Round(theY1 + (k * DIS) * DY / length, 3);
                        emp.DR = theD;
                        emp.Selected = 0;
                        emp.InLine = LineBarsNumber;
                        emp.Type = 2;
                        emp.InED = 0;
                        emp.InREC = 0;
                        emp.Corner = 0;
                        emp.InCircle = 0;
                        ((SectionDrawForm)sectionDrawForm).Bar[BarsNumber] = emp;
                    }
                }
                if (HasEndBars == 1)
                {
                    for (int k = 0; k < NBAR; k++)
                    {
                        BarsNumber = BarsNumber + 1;
                        Bars emp = new Bars();
                        emp.XR = Math.Round(theX1 + (k * DIS) * DX / length, 3);
                        emp.YR = Math.Round(theY1 + (k * DIS) * DY / length, 3);
                        if (k == NBAR - 1)
                        {
                            emp.XR = Math.Round(theX2, 3);
                            emp.YR = Math.Round(theY2, 3);
                        }
                        emp.DR = theD;
                        emp.Selected = 0;
                        emp.InLine = LineBarsNumber;
                        emp.Type = 2;
                        emp.InED = 0;
                        emp.InREC = 0;
                        emp.Corner = 0;
                        emp.InCircle = 0;
                        ((SectionDrawForm)sectionDrawForm).Bar[BarsNumber] = emp;
                    }
                }
                ((SectionDrawForm)sectionDrawForm).BarsNumber = BarsNumber;
                ((SectionDrawForm)sectionDrawForm).MakeTempFiles();
                ((SectionDrawForm)sectionDrawForm).Render2d();
            }
            #endregion
            #region//مستطيل
            if (O == 3)
            {
                #region//حافة
                if (KK != 0)
                {
                    int BarsNumber = ((SectionDrawForm)sectionDrawForm).BarsNumber;
                    int RecBarsNumber = ((SectionDrawForm)sectionDrawForm).RecBarsNumber;
                    int RecShapeNumber = ((SectionDrawForm)sectionDrawForm).RecShapeNumber;
                    RecLineBars emp0 = new RecLineBars();
                    emp0 = ((SectionDrawForm)sectionDrawForm).RecLineBar[SelectedRecBar];
                startloop: { }
                    for (int k = 1; k < BarsNumber + 1; k++)
                    {
                        if (((SectionDrawForm)sectionDrawForm).Bar[k].InREC == SelectedRecBar)
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
                        if (((SectionDrawForm)sectionDrawForm).Bar[k].InREC > SelectedRecBar)
                        {
                            ((SectionDrawForm)sectionDrawForm).Bar[k].InREC = ((SectionDrawForm)sectionDrawForm).Bar[k].InREC - 1;
                        }
                    }
                    for (int k = SelectedRecBar; k < RecBarsNumber; k++)
                    {
                        ((SectionDrawForm)sectionDrawForm).RecLineBar[k] = ((SectionDrawForm)sectionDrawForm).RecLineBar[k + 1];
                    }
                    for (int k = 1; k < RecShapeNumber+1; k++)//مضاف
                    {
                        if (((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars > SelectedRecBar)
                        {
                            ((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars = ((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars - 1;
                        }
                    }
                    for (int k = 1; k < TeeShapeNumber + 1; k++)//مضاف
                    {
                        if (((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] > SelectedRecBar)
                        {
                            ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] - 1;
                            ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[2] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[2] - 1;
                        }
                    }
                    for (int k = 1; k < FlangedWallShapeNumber + 1; k++)//مضاف
                    {
                        if (((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] > SelectedRecBar)
                        {
                            ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] - 1;
                            ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[2] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[2] - 1;
                            ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[3] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[3] - 1;
                        }
                    }
                    double X1 = 0;
                    double Y1 = 0;
                    double length = 0;
                    int NBAR = 0;
                    int diameter = 0;
                    int diameter1 = emp0.TIEDR;
                    double DIS = 0;
                    if (checkBox1.Checked == false)
                    {
                        emp0.EDDR[KK] = Convert.ToInt32(EDBarSizeTxt.Text);
                        emp0.EDDistance[KK] = Convert.ToDouble(EDMaxSTxt.Text);
                        emp0.EDBarsNumbers[KK] = Convert.ToInt32(EDNTxt.Text);
                    }
                    if (checkBox1.Checked == true)
                    {
                        BarOrDis = 2;
                        if (KK == 1 || KK == 3)
                        {
                            emp0.EDDR[1] = Convert.ToInt32(EDBarSizeTxt.Text);
                            emp0.EDDistance[1] = Convert.ToDouble(EDMaxSTxt.Text);
                            emp0.EDBarsNumbers[1] = Convert.ToInt32(EDNTxt.Text);
                            emp0.EDDR[3] = emp0.EDDR[1];
                            emp0.EDDistance[3] = emp0.EDDistance[1];
                            emp0.EDBarsNumbers[3] = emp0.EDBarsNumbers[1];
                            length = emp0.Height;
                            if (BarOrDis == 1)
                            {
                                TheBars = Convert.ToInt32(EDNTxt.Text);
                                TheDistance = Math.Round((length - 2 * diameter1) / (TheBars + 1), 3);
                            }
                            if (BarOrDis == 2)
                            {
                                TheDistance = Convert.ToDouble(EDMaxSTxt.Text);
                                TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                            }
                            emp0.EDDR[2] = emp0.EDDR[1];
                            emp0.EDDistance[2] = TheDistance;
                            emp0.EDBarsNumbers[2] = TheBars;
                            emp0.EDDR[4] = emp0.EDDR[2];
                            emp0.EDDistance[4] = emp0.EDDistance[2];
                            emp0.EDBarsNumbers[4] = emp0.EDBarsNumbers[2];
                        }
                        if (KK == 2 || KK == 4)
                        {
                            emp0.EDDR[2] = Convert.ToInt32(EDBarSizeTxt.Text);
                            emp0.EDDistance[2] = Convert.ToDouble(EDMaxSTxt.Text);
                            emp0.EDBarsNumbers[2] = Convert.ToInt32(EDNTxt.Text);
                            emp0.EDDR[4] = emp0.EDDR[2];
                            emp0.EDDistance[4] = emp0.EDDistance[2];
                            emp0.EDBarsNumbers[4] = emp0.EDBarsNumbers[2];
                            length = emp0.Width;
                            if (BarOrDis == 1)
                            {
                                TheBars = Convert.ToInt32(EDNTxt.Text);
                                TheDistance = Math.Round((length - 2 * diameter1) / (TheBars + 1), 3);
                            }
                            if (BarOrDis == 2)
                            {
                                TheDistance = Convert.ToDouble(EDMaxSTxt.Text);
                                TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                            }
                            emp0.EDDR[1] = emp0.EDDR[2];
                            emp0.EDDistance[1] = TheDistance;
                            emp0.EDBarsNumbers[1] = TheBars;
                            emp0.EDDR[3] = emp0.EDDR[1];
                            emp0.EDDistance[3] = emp0.EDDistance[1];
                            emp0.EDBarsNumbers[3] = emp0.EDBarsNumbers[1];
                        }
                    }
                    for (int s = 1; s < 5; s++)
                    {
                        int kl1 = 0;
                        int kl2 = 0;
                        if (s == 1 || s==3)
                        {
                            length = emp0.Width;
                            kl1 = 1;
                            kl2 = 0;
                        }
                        if (s == 2 || s == 4)
                        {
                            length = emp0.Height;
                            kl1 = 0;
                            kl2 = 1;
                        }
                        NBAR = emp0.EDBarsNumbers[s];
                        diameter = emp0.EDDR[s];
                        DIS = Math.Round((length - 2 * diameter1) / (NBAR + 1), 3);
                        X1 = emp0.EDX1R[s];
                        Y1 = emp0.EDY1R[s];
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
                        diameter = emp0.CORDR[k];
                        emp1.XR = Math.Round(emp0.EDX1R[k] + kx[k] * (diameter / 2) + kx[k] * diameter1, 3);
                        emp1.YR = Math.Round(emp0.EDY1R[k] + ky[k] * (diameter / 2) + ky[k] * diameter1, 3);
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
                    ((SectionDrawForm)sectionDrawForm).RecLineBar[RecBarsNumber] = emp0;
                    ((SectionDrawForm)sectionDrawForm).BarsNumber = BarsNumber;
                    ((SectionDrawForm)sectionDrawForm).MakeTempFiles();
                    ((SectionDrawForm)sectionDrawForm).Render2d();
                }
                #endregion
                #region//كامل المستطيل
                if (KK == 0)
                {
                    int BarsNumber = ((SectionDrawForm)sectionDrawForm).BarsNumber;
                    int RecBarsNumber = ((SectionDrawForm)sectionDrawForm).RecBarsNumber;
                    int RecShapeNumber = ((SectionDrawForm)sectionDrawForm).RecShapeNumber;
                    double X1 = 0;
                    double Y1 = 0;
                    double length = 0;
                    int NBAR = 0;
                    int diameter = 0;
                    double DIS = 0;
                    int thetype = ((SectionDrawForm)sectionDrawForm).RecLineBar[SelectedRecBar].Type;
                startloop: { }
                    for (int k = 1; k < BarsNumber + 1; k++)
                    {
                        if (((SectionDrawForm)sectionDrawForm).Bar[k].InREC == SelectedRecBar)
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
                        if (((SectionDrawForm)sectionDrawForm).Bar[k].InREC > SelectedRecBar)
                        {
                            ((SectionDrawForm)sectionDrawForm).Bar[k].InREC = ((SectionDrawForm)sectionDrawForm).Bar[k].InREC - 1;
                        }
                    }
                    for (int k = SelectedRecBar; k < RecBarsNumber; k++)
                    {
                        ((SectionDrawForm)sectionDrawForm).RecLineBar[k] = ((SectionDrawForm)sectionDrawForm).RecLineBar[k + 1];
                    }

                    for (int k = 1; k < RecShapeNumber + 1; k++)//مضاف
                    {
                        if (((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars > SelectedRecBar)
                        {
                            ((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars = ((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars - 1;
                        }
                    }
                    for (int k = 1; k < TeeShapeNumber + 1; k++)//مضاف
                    {
                        if (((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] > SelectedRecBar)
                        {
                            ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] - 1;
                            ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[2] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[2] - 1;
                        }
                    }
                    for (int k = 1; k < FlangedWallShapeNumber + 1; k++)//مضاف
                    {
                        if (((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] > SelectedRecBar)
                        {
                            ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] - 1;
                            ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[2] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[2] - 1;
                            ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[3] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[3] - 1;
                         }
                    }

                    RecLineBars emp0 = new RecLineBars();
                    emp0.Width = Convert.ToDouble(textBox15.Text);
                    emp0.Height = Convert.ToDouble(textBox16.Text);
                    emp0.CenterX = Convert.ToDouble(textBox17.Text);
                    emp0.CenterY = Convert.ToDouble(textBox18.Text);
                    emp0.TIEDR = Convert.ToInt32(textBox19.Text);
                    emp0.EDX1R[1] = emp0.CenterX - emp0.Width / 2;
                    emp0.EDY1R[1] = emp0.CenterY + emp0.Height / 2;
                    emp0.EDX2R[1] = emp0.CenterX + emp0.Width / 2;
                    emp0.EDY2R[1] = emp0.CenterY + emp0.Height / 2;
                    emp0.EDX1R[2] = emp0.CenterX + emp0.Width / 2;
                    emp0.EDY1R[2] = emp0.CenterY + emp0.Height / 2;
                    emp0.EDX2R[2] = emp0.CenterX + emp0.Width / 2;
                    emp0.EDY2R[2] = emp0.CenterY - emp0.Height / 2;
                    emp0.EDX1R[3] = emp0.CenterX + emp0.Width / 2;
                    emp0.EDY1R[3] = emp0.CenterY - emp0.Height / 2;
                    emp0.EDX2R[3] = emp0.CenterX - emp0.Width / 2;
                    emp0.EDY2R[3] = emp0.CenterY - emp0.Height / 2;
                    emp0.EDX1R[4] = emp0.CenterX - emp0.Width / 2;
                    emp0.EDY1R[4] = emp0.CenterY - emp0.Height / 2;
                    emp0.EDX2R[4] = emp0.CenterX - emp0.Width / 2;
                    emp0.EDY2R[4] = emp0.CenterY + emp0.Height / 2;
                    emp0.Type = thetype;
                    if (thetype == 1)
                    {
                        emp0.EDDistance[1] = 1000000;
                        emp0.EDDistance[2] = 1000000;
                        emp0.EDDistance[3] = 1000000;
                        emp0.EDDistance[4] = 1000000;
                    }

                    if (thetype == 0)
                    {
                        emp0.CORDR[1] = Convert.ToInt32(textBox11.Text);
                        emp0.CORDR[2] = Convert.ToInt32(textBox12.Text);
                        emp0.CORDR[3] = Convert.ToInt32(textBox13.Text);
                        emp0.CORDR[4] = Convert.ToInt32(textBox14.Text);
                        emp0.EDDR[1] = Convert.ToInt32(textBox31.Text);
                        emp0.EDDistance[1] = Convert.ToDouble(textBox30.Text);
                        emp0.EDBarsNumbers[1] = Convert.ToInt32(textBox29.Text);
                        emp0.EDDR[2] = Convert.ToInt32(textBox36.Text);
                        emp0.EDDistance[2] = Convert.ToDouble(textBox35.Text);
                        emp0.EDBarsNumbers[2] = Convert.ToInt32(textBox34.Text);
                        emp0.EDDR[3] = Convert.ToInt32(textBox41.Text);
                        emp0.EDDistance[3] = Convert.ToDouble(textBox40.Text);
                        emp0.EDBarsNumbers[3] = Convert.ToInt32(textBox39.Text);
                        emp0.EDDR[4] = Convert.ToInt32(textBox46.Text);
                        emp0.EDDistance[4] = Convert.ToDouble(textBox45.Text);
                        emp0.EDBarsNumbers[4] = Convert.ToInt32(textBox44.Text);
                        int diameter1 = emp0.TIEDR;
                        for (int s = 1; s < 5; s++)
                        {
                            int kl1 = 0;
                            int kl2 = 0;
                            if (s == 1 || s == 3)
                            {
                                length = emp0.Width;
                                kl1 = 1;
                                kl2 = 0;
                            }
                            if (s == 2 || s == 4)
                            {
                                length = emp0.Height;
                                kl1 = 0;
                                kl2 = 1;
                            }
                            NBAR = emp0.EDBarsNumbers[s];
                            diameter = emp0.EDDR[s];
                            DIS = Math.Round((length - 2 * diameter1) / (NBAR + 1), 3);
                            X1 = emp0.EDX1R[s];
                            Y1 = emp0.EDY1R[s];
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
                            diameter = emp0.CORDR[k];
                            diameter = emp0.CORDR[k];
                            emp1.XR = Math.Round(emp0.EDX1R[k] + kx[k] * (diameter / 2) + kx[k] * diameter1, 3);
                            emp1.YR = Math.Round(emp0.EDY1R[k] + ky[k] * (diameter / 2) + ky[k] * diameter1, 3);
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
                    }
                    ((SectionDrawForm)sectionDrawForm).RecLineBar[RecBarsNumber] = emp0;
                    ((SectionDrawForm)sectionDrawForm).BarsNumber = BarsNumber;
                    ((SectionDrawForm)sectionDrawForm).MakeTempFiles();
                    ((SectionDrawForm)sectionDrawForm).Render2d();
                }
                #endregion
            }
            #endregion
            #region/// الزاوية
            if (O == 4)
            {
                RecLineBars emp0 = new RecLineBars();
                emp0 = ((SectionDrawForm)sectionDrawForm).RecLineBar[SelectedRecBar];
                int diameter1 = emp0.TIEDR;
                double theX1 = 0;
                double theY1 = 0;
                int theD = Convert.ToInt32(textBox23.Text);
                if (checkBox2.Checked == false)
                {
                        ((SectionDrawForm)sectionDrawForm).RecLineBar[SelectedRecBar].CORDR[MM] = theD;
                        theX1 = Math.Round(((SectionDrawForm)sectionDrawForm).RecLineBar[SelectedRecBar].EDX1R[MM] + kx[MM] * (theD / 2) + kx[MM] * diameter1, 3);
                        theY1 = Math.Round(((SectionDrawForm)sectionDrawForm).RecLineBar[SelectedRecBar].EDY1R[MM] + ky[MM] * (theD / 2) + ky[MM] * diameter1, 3);
                    ((SectionDrawForm)sectionDrawForm).Bar[i].XR = theX1;
                    ((SectionDrawForm)sectionDrawForm).Bar[i].YR = theY1;
                    ((SectionDrawForm)sectionDrawForm).Bar[i].DR = theD;
                }
                if (checkBox2.Checked == true)
                {
                    for (int k = 1; k < 5; k++)
                    {
                        ((SectionDrawForm)sectionDrawForm).RecLineBar[SelectedRecBar].CORDR[k] = theD;
                        theX1 = Math.Round(((SectionDrawForm)sectionDrawForm).RecLineBar[SelectedRecBar].EDX1R[k] + kx[k] * (theD / 2) + kx[k] * diameter1, 3);
                        theY1 = Math.Round(((SectionDrawForm)sectionDrawForm).RecLineBar[SelectedRecBar].EDY1R[k] + ky[k] * (theD / 2) + ky[k] * diameter1, 3);
                        ((SectionDrawForm)sectionDrawForm).Bar[i + k - MM].XR = theX1;
                        ((SectionDrawForm)sectionDrawForm).Bar[i + k - MM].YR = theY1;
                        ((SectionDrawForm)sectionDrawForm).Bar[i + k - MM].DR = theD;
                    }
                }
                ((SectionDrawForm)sectionDrawForm).MakeTempFiles();
                ((SectionDrawForm)sectionDrawForm).Render2d();
            }
            #endregion
            #region//تسليح دائرة
            if (O == 6)
            {
                int BarsNumber = ((SectionDrawForm)sectionDrawForm).BarsNumber;
                int CircleBarsNumber = ((SectionDrawForm)sectionDrawForm).CircleBarsNumber;
            startloop: { }
                for (int k = 1; k < BarsNumber + 1; k++)
                {
                    if (((SectionDrawForm)sectionDrawForm).Bar[k].InCircle == SelectedCircleLineBar)
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
                    if (((SectionDrawForm)sectionDrawForm).Bar[k].InCircle > SelectedCircleLineBar)
                    {
                        ((SectionDrawForm)sectionDrawForm).Bar[k].InCircle = ((SectionDrawForm)sectionDrawForm).Bar[k].InCircle - 1;
                    }
                }
                for (int k = SelectedCircleLineBar; k < CircleBarsNumber; k++)
                {
                    ((SectionDrawForm)sectionDrawForm).CircleLineBar[k] = ((SectionDrawForm)sectionDrawForm).CircleLineBar[k + 1];
                }

                double CenterX = Convert.ToDouble(textBox60.Text);
                double CenterY = Convert.ToDouble(textBox61.Text);
                double Diameter = Convert.ToDouble(textBox62.Text);
                int DR = Convert.ToInt32(textBox63.Text);
                int TIEDR = Convert.ToInt32(textBox65.Text);
                int BarN = Convert.ToInt32(textBox64.Text);
                CircleLineBars emp = new CircleLineBars();
                emp.BarsNumbers = BarN;
                emp.Diameter = Diameter;
                emp.DR = DR;
                emp.TIEDR = TIEDR;
                emp.CenterX = CenterX;
                emp.CenterY = CenterY;
                emp.Selected = 0;
                emp.InCircleShape = 0;
                int pointN = 32;
                double zaweaa = 2 * Math.PI / pointN;
                double Diameter1 = Diameter / 2;// -(DR / 2 + TIEDR);
                for (int s = 1; s < pointN + 1; s++)
                {
                    emp.PointXR[s] = Math.Round(CenterX + Math.Cos(s * zaweaa) * Diameter1, 3);
                    emp.PointYR[s] = Math.Round(CenterY + Math.Sin(s * zaweaa) * Diameter1, 3);
                }
                zaweaa = 2 * Math.PI / BarN;
                Diameter1 = Diameter / 2 -(DR / 2 + TIEDR);
                for (int s = 1; s < BarN + 1; s++)
                {
                    BarsNumber = BarsNumber + 1;
                    Bars emp1 = new Bars();
                    emp1.XR = Math.Round(CenterX + Math.Cos(s * zaweaa) * Diameter1, 3);
                    emp1.YR = Math.Round(CenterY + Math.Sin(s * zaweaa) * Diameter1, 3);
                    emp1.DR = DR;
                    emp1.Selected = 0;
                    emp1.InLine = 0;
                    emp1.Type = 5;
                    emp1.InED = 0;
                    emp1.InREC = 0;
                    emp1.InCircle = CircleBarsNumber;
                    ((SectionDrawForm)sectionDrawForm).Bar[BarsNumber] = emp1;
                }
                ((SectionDrawForm)sectionDrawForm).CircleLineBar[CircleBarsNumber] = emp;
                ((SectionDrawForm)sectionDrawForm).BarsNumber = BarsNumber;
                ((SectionDrawForm)sectionDrawForm).MakeTempFiles();
                ((SectionDrawForm)sectionDrawForm).Render2d();
            }
              #endregion
            #region/// شكل مستطيل
            if (O == 5)
            {
                #region //تعبئة مواصفات مستطيل البيتون من التيكسات
                RecShapes emp0 = new RecShapes();
               // emp0.IsWall = 0;
                emp0.Width = Convert.ToDouble(textBox15.Text);
                emp0.Height = Convert.ToDouble(textBox16.Text);
                emp0.CenterX = Convert.ToDouble(textBox17.Text);
                emp0.CenterY = Convert.ToDouble(textBox18.Text);
                emp0.EDCoverR[1] = Convert.ToDouble(textBox47.Text);
                emp0.EDCoverR[2] = Convert.ToDouble(textBox48.Text);
                emp0.EDCoverR[3] = Convert.ToDouble(textBox49.Text);
                emp0.EDCoverR[4] = Convert.ToDouble(textBox50.Text);
                emp0.HasReinforcment = comboBox2.SelectedIndex;
                emp0.EDX1R[1] = emp0.CenterX - emp0.Width / 2;
                emp0.EDY1R[1] = emp0.CenterY + emp0.Height / 2;
                emp0.EDX2R[1] = emp0.CenterX + emp0.Width / 2;
                emp0.EDY2R[1] = emp0.CenterY + emp0.Height / 2;
                emp0.EDX1R[2] = emp0.CenterX + emp0.Width / 2;
                emp0.EDY1R[2] = emp0.CenterY + emp0.Height / 2;
                emp0.EDX2R[2] = emp0.CenterX + emp0.Width / 2;
                emp0.EDY2R[2] = emp0.CenterY - emp0.Height / 2;
                emp0.EDX1R[3] = emp0.CenterX + emp0.Width / 2;
                emp0.EDY1R[3] = emp0.CenterY - emp0.Height / 2;
                emp0.EDX2R[3] = emp0.CenterX - emp0.Width / 2;
                emp0.EDY2R[3] = emp0.CenterY - emp0.Height / 2;
                emp0.EDX1R[4] = emp0.CenterX - emp0.Width / 2;
                emp0.EDY1R[4] = emp0.CenterY - emp0.Height / 2;
                emp0.EDX2R[4] = emp0.CenterX - emp0.Width / 2;
                emp0.EDY2R[4] = emp0.CenterY + emp0.Height / 2;
                #endregion
                if (comboBox2.SelectedIndex == 1)
                #region//يوجد تسليح
                {
                    int BarsNumber = ((SectionDrawForm)sectionDrawForm).BarsNumber;
                    int RecBarsNumber = ((SectionDrawForm)sectionDrawForm).RecBarsNumber;
                    int RecShapeNumber = ((SectionDrawForm)sectionDrawForm).RecShapeNumber;
                    if (SelectedRecBar == 0)
                    {
                        RecBarsNumber = RecBarsNumber + 1;
                        RecLineBars EMP00 = new RecLineBars();
                        ((SectionDrawForm)sectionDrawForm).RecLineBar[RecBarsNumber] = EMP00;
                        SelectedRecBar = RecBarsNumber;
                    }
                    #region//كامل المستطيل
                    double X1 = 0;
                    double Y1 = 0;
                    double length = 0;
                    int NBAR = 0;
                    int diameter = 0;
                    double DIS = 0;
                    startloop: { }
                        for (int k = 1; k < BarsNumber + 1; k++)
                        {
                            if (((SectionDrawForm)sectionDrawForm).Bar[k].InREC == SelectedRecBar)
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
                            if (((SectionDrawForm)sectionDrawForm).Bar[k].InREC > SelectedRecBar)
                            {
                                ((SectionDrawForm)sectionDrawForm).Bar[k].InREC = ((SectionDrawForm)sectionDrawForm).Bar[k].InREC - 1;
                            }
                        }
                        for (int k = SelectedRecBar; k < RecBarsNumber; k++)
                        {
                            ((SectionDrawForm)sectionDrawForm).RecLineBar[k] = ((SectionDrawForm)sectionDrawForm).RecLineBar[k + 1];
                        }

                        for (int k = SelectedRecShape; k < RecShapeNumber; k++)//مضاف
                        {
                            ((SectionDrawForm)sectionDrawForm).RecShape[k] = ((SectionDrawForm)sectionDrawForm).RecShape[k + 1];
                        }
                        for (int k = 1; k < RecBarsNumber ; k++)//مضاف
                        {
                            if (((SectionDrawForm)sectionDrawForm).RecLineBar[k].InRecShape > SelectedRecShape)
                            {
                                ((SectionDrawForm)sectionDrawForm).RecLineBar[k].InRecShape = ((SectionDrawForm)sectionDrawForm).RecLineBar[k].InRecShape - 1;
                            }
                        }
                        for (int k = 1; k < RecShapeNumber ; k++)//مضاف
                        {
                            if (((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars > SelectedRecBar)
                            {
                                ((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars = ((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars - 1;
                            }
                        }
                        for (int k = 1; k < TeeShapeNumber + 1; k++)//مضاف
                        {
                            if (((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] > SelectedRecBar)
                            {
                                ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] - 1;
                                ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[2] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[2] - 1;
                            }
                        }
                        for (int k = 1; k < FlangedWallShapeNumber+1; k++)//مضاف
                        {
                            if (((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] > SelectedRecBar)
                            {
                                ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] - 1;
                                ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[2] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[2] - 1;
                                ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[3] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[3] - 1;
                            }
                        }

                    SelectedRecShape = RecShapeNumber;
                    SelectedRecBar = RecBarsNumber;
                    RecLineBars emp = new RecLineBars();
                    emp.InRecShape = SelectedRecShape;
                    emp.Width = Convert.ToDouble(textBox15.Text) - emp0.EDCoverR[2] - emp0.EDCoverR[4];
                    emp.Height = Convert.ToDouble(textBox16.Text) - emp0.EDCoverR[1] - emp0.EDCoverR[3];
                    emp.CenterX = emp0.EDX1R[1] + emp0.EDCoverR[4] + emp.Width / 2;
                    emp.CenterY = emp0.EDY1R[1] - emp0.EDCoverR[1] - emp.Height / 2;
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
                            emp1.XR = Math.Round(emp0.EDX1R[1] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.EDCoverR[4], 3);
                            emp1.YR = Math.Round(emp0.EDY1R[1] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.EDCoverR[1], 3);
                        }
                        if (k == 2)
                        {
                            emp1.XR = Math.Round(emp0.EDX2R[1] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.EDCoverR[2], 3);
                            emp1.YR = Math.Round(emp0.EDY2R[1] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.EDCoverR[1], 3);
                        }
                        if (k == 3)
                        {
                            emp1.XR = Math.Round(emp0.EDX1R[3] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.EDCoverR[2], 3);
                            emp1.YR = Math.Round(emp0.EDY1R[3] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.EDCoverR[3], 3);
                        }
                        if (k == 4)
                        {
                            emp1.XR = Math.Round(emp0.EDX2R[3] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.EDCoverR[4], 3);
                            emp1.YR = Math.Round(emp0.EDY2R[3] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.EDCoverR[3], 3);
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
                    emp0.ApplyedToRecBars = SelectedRecBar;
                    ((SectionDrawForm)sectionDrawForm).RecBarsNumber = RecBarsNumber;
                }
                #endregion
                else
                #region//لا يوجد تسليح
                {
                    if (SelectedRecBar != 0)
                    {
                        emp0.ApplyedToRecBars = 0;
                        emp0.HasReinforcment = 0;
                        int BarsNumber = ((SectionDrawForm)sectionDrawForm).BarsNumber;
                        int RecBarsNumber = ((SectionDrawForm)sectionDrawForm).RecBarsNumber;
                        int RecShapeNumber = ((SectionDrawForm)sectionDrawForm).RecShapeNumber;
                        ((SectionDrawForm)sectionDrawForm).RecLineBar[SelectedRecBar].InRecShape = 0;
                    startloop: { }
                        for (int k = 1; k < BarsNumber + 1; k++)
                        {
                            if (((SectionDrawForm)sectionDrawForm).Bar[k].InREC == SelectedRecBar)
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
                            if (((SectionDrawForm)sectionDrawForm).Bar[k].InREC > SelectedRecBar)
                            {
                                ((SectionDrawForm)sectionDrawForm).Bar[k].InREC = ((SectionDrawForm)sectionDrawForm).Bar[k].InREC - 1;
                            }
                        }
                        for (int k = SelectedRecBar; k < RecBarsNumber; k++)
                        {
                            ((SectionDrawForm)sectionDrawForm).RecLineBar[k] = ((SectionDrawForm)sectionDrawForm).RecLineBar[k + 1];
                        }
                        for (int k = 1; k < RecShapeNumber+1; k++)//مضاف
                        {
                            if (((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars > SelectedRecBar)
                            {
                                ((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars = ((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars - 1;
                            }
                        }
                        for (int k = 1; k < TeeShapeNumber + 1; k++)//مضاف
                        {
                            if (((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] > SelectedRecBar)
                            {
                                ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] - 1;
                                ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[2] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[2] - 1;
                            }
                        }
                        for (int k = 1; k < FlangedWallShapeNumber + 1; k++)//مضاف
                        {
                            if (((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] > SelectedRecBar)
                            {
                                ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] - 1;
                                ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[2] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[2] - 1;
                                ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[3] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[3] - 1;
                            }
                        }
                        RecBarsNumber = RecBarsNumber - 1;
                        ((SectionDrawForm)sectionDrawForm).RecBarsNumber = RecBarsNumber;
                        ((SectionDrawForm)sectionDrawForm).BarsNumber = BarsNumber;
                    }
                }
                #endregion
                ((SectionDrawForm)sectionDrawForm).RecShape[SelectedRecShape] = emp0;
                ((SectionDrawForm)sectionDrawForm).MakeTempFiles();
                ((SectionDrawForm)sectionDrawForm).Render2d();
            }
            #endregion
            #region//شكل دائرة
            if (O == 7)
            {
                #region //تعبئة مواصفات دائرة البيتون من التيكسات
                double CenterX = Convert.ToDouble(textBox54.Text);
                double CenterY = Convert.ToDouble(textBox55.Text);
                double Diameter = Convert.ToDouble(textBox56.Text);
                CircleShapes emp0 = new CircleShapes();
                emp0.Diameter = Diameter;
                emp0.CenterX = CenterX;
                emp0.CenterY = CenterY;
                emp0.Selected = 0;
                emp0.CoverR = Convert.ToDouble(textBox53.Text);
                emp0.HasReinforcment = comboBox3.SelectedIndex;
                int pointN = 32;
                double zaweaa = 2 * Math.PI / pointN;
                double Diameter1 = Diameter / 2;
                for (int s = 1; s < pointN + 1; s++)
                {
                    emp0.PointXR[s] = Math.Round(CenterX + Math.Cos(s * zaweaa) * Diameter1, 3);
                    emp0.PointYR[s] = Math.Round(CenterY + Math.Sin(s * zaweaa) * Diameter1, 3);
                }
                #endregion
                if (comboBox3.SelectedIndex == 1)
                #region//يوجد تسليح
                {
                    int BarsNumber = ((SectionDrawForm)sectionDrawForm).BarsNumber;
                    int CircleBarsNumber = ((SectionDrawForm)sectionDrawForm).CircleBarsNumber;
                    int CircleShapeNumber = ((SectionDrawForm)sectionDrawForm).CircleShapeNumber;
                    if (SelectedCircleLineBar == 0)
                    {
                        CircleBarsNumber = CircleBarsNumber + 1;
                        CircleLineBars EMP00 = new CircleLineBars();
                        ((SectionDrawForm)sectionDrawForm).CircleLineBar[CircleBarsNumber] = EMP00;
                        SelectedCircleLineBar = CircleBarsNumber;
                    }
                startloop: { }
                    for (int k = 1; k < BarsNumber + 1; k++)
                    {
                        if (((SectionDrawForm)sectionDrawForm).Bar[k].InCircle == SelectedCircleLineBar)
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
                        if (((SectionDrawForm)sectionDrawForm).Bar[k].InCircle > SelectedCircleLineBar)
                        {
                            ((SectionDrawForm)sectionDrawForm).Bar[k].InCircle = ((SectionDrawForm)sectionDrawForm).Bar[k].InCircle - 1;
                        }
                    }
                    for (int k = SelectedCircleLineBar; k < CircleBarsNumber; k++)
                    {
                        ((SectionDrawForm)sectionDrawForm).CircleLineBar[k] = ((SectionDrawForm)sectionDrawForm).CircleLineBar[k + 1];
                    }
                    for (int k = SelectedCircleShape; k < CircleShapeNumber; k++)//مضاف
                    {
                        ((SectionDrawForm)sectionDrawForm).CircleShape[k] = ((SectionDrawForm)sectionDrawForm).CircleShape[k + 1];
                    }
                    for (int k = 1; k < CircleBarsNumber; k++)//مضاف
                    {
                        if (((SectionDrawForm)sectionDrawForm).CircleLineBar[k].InCircleShape > SelectedCircleShape)
                        {
                            ((SectionDrawForm)sectionDrawForm).CircleLineBar[k].InCircleShape = ((SectionDrawForm)sectionDrawForm).CircleLineBar[k].InCircleShape - 1;
                        }
                    }
                    for (int k = 1; k < CircleShapeNumber; k++)//مضاف
                    {
                        if (((SectionDrawForm)sectionDrawForm).CircleShape[k].ApplyedToCircleBars > SelectedCircleLineBar)
                        {
                            ((SectionDrawForm)sectionDrawForm).CircleShape[k].ApplyedToCircleBars = ((SectionDrawForm)sectionDrawForm).CircleShape[k].ApplyedToCircleBars - 1;
                        }
                    }
                    SelectedCircleShape = CircleShapeNumber;
                    SelectedCircleLineBar = CircleBarsNumber;
                    CircleLineBars emp = new CircleLineBars();
                    Diameter = emp0.Diameter - 2 * emp0.CoverR;
                    emp.InCircleShape = SelectedCircleShape;
                    emp.Diameter = Diameter;
                    emp.CenterX = emp0.CenterX;
                    emp.CenterY = emp0.CenterY;
                    emp.TIEDR = Convert.ToInt32(textBox65.Text);
                    emp.DR = Convert.ToInt32(textBox63.Text);
                    emp.BarsNumbers = Convert.ToInt32(textBox64.Text);
                    pointN = 32;
                    zaweaa = 2 * Math.PI / pointN;
                    Diameter1 = Diameter / 2;
                    for (int s = 1; s < pointN + 1; s++)
                    {
                        emp.PointXR[s] = Math.Round(CenterX + Math.Cos(s * zaweaa) * Diameter1, 3);
                        emp.PointYR[s] = Math.Round(CenterY + Math.Sin(s * zaweaa) * Diameter1, 3);
                    }
                    zaweaa = 2 * Math.PI / emp.BarsNumbers;
                    Diameter1 = Diameter / 2 - (emp.DR / 2 + emp.TIEDR);
                    for (int s = 1; s < emp.BarsNumbers + 1; s++)
                    {
                        BarsNumber = BarsNumber + 1;
                        Bars emp1 = new Bars();
                        emp1.XR = Math.Round(CenterX + Math.Cos(s * zaweaa) * Diameter1 , 3);
                        emp1.YR = Math.Round(CenterY + Math.Sin(s * zaweaa) * Diameter1 , 3);
                        emp1.DR = emp.DR;
                        emp1.Selected = 0;
                        emp1.InLine = 0;
                        emp1.Type = 5;
                        emp1.InED = 0;
                        emp1.InREC = 0;
                        emp1.InCircle = CircleBarsNumber;
                        ((SectionDrawForm)sectionDrawForm).Bar[BarsNumber] = emp1;
                    }
                    ((SectionDrawForm)sectionDrawForm).CircleLineBar[CircleBarsNumber] = emp;
                    ((SectionDrawForm)sectionDrawForm).BarsNumber = BarsNumber;
                    ((SectionDrawForm)sectionDrawForm).CircleBarsNumber = CircleBarsNumber;
                    emp0.ApplyedToCircleBars = SelectedCircleLineBar;
                }
                #endregion
                else
                #region//لا يوجد تسليح
                {
                    if (SelectedCircleLineBar != 0)
                    {
                        emp0.ApplyedToCircleBars = 0;
                        emp0.HasReinforcment = 0;
                        int BarsNumber = ((SectionDrawForm)sectionDrawForm).BarsNumber;
                        int CircleBarsNumber = ((SectionDrawForm)sectionDrawForm).CircleBarsNumber;
                        int CircleShapeNumber = ((SectionDrawForm)sectionDrawForm).CircleShapeNumber;
                        if (SelectedCircleLineBar == 0)
                        {
                            CircleBarsNumber = CircleBarsNumber + 1;
                            CircleLineBars EMP00 = new CircleLineBars();
                            ((SectionDrawForm)sectionDrawForm).CircleLineBar[CircleBarsNumber] = EMP00;
                            SelectedCircleLineBar = CircleBarsNumber;
                        }
                    startloop: { }
                        for (int k = 1; k < BarsNumber + 1; k++)
                        {
                            if (((SectionDrawForm)sectionDrawForm).Bar[k].InCircle == SelectedCircleLineBar)
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
                            if (((SectionDrawForm)sectionDrawForm).Bar[k].InCircle > SelectedCircleLineBar)
                            {
                                ((SectionDrawForm)sectionDrawForm).Bar[k].InCircle = ((SectionDrawForm)sectionDrawForm).Bar[k].InCircle - 1;
                            }
                        }
                        for (int k = SelectedCircleLineBar; k < CircleBarsNumber; k++)
                        {
                            ((SectionDrawForm)sectionDrawForm).CircleLineBar[k] = ((SectionDrawForm)sectionDrawForm).CircleLineBar[k + 1];
                        }
                        for (int k = 1; k < CircleShapeNumber; k++)//مضاف
                        {
                            if (((SectionDrawForm)sectionDrawForm).CircleShape[k].ApplyedToCircleBars > SelectedCircleLineBar)
                            {
                                ((SectionDrawForm)sectionDrawForm).CircleShape[k].ApplyedToCircleBars = ((SectionDrawForm)sectionDrawForm).CircleShape[k].ApplyedToCircleBars - 1;
                            }
                        }
                        CircleBarsNumber = CircleBarsNumber - 1;
                        ((SectionDrawForm)sectionDrawForm).CircleBarsNumber = CircleBarsNumber;
                        ((SectionDrawForm)sectionDrawForm).BarsNumber = BarsNumber;
                    }
                }
                #endregion
                ((SectionDrawForm)sectionDrawForm).CircleShape[SelectedCircleShape] = emp0;
                ((SectionDrawForm)sectionDrawForm).MakeTempFiles();
                ((SectionDrawForm)sectionDrawForm).Render2d();
            }
            #endregion
            this.Close();
       }
        private void SectionBarsPropertyForm_Load(object sender, EventArgs e)
        {
            kx[1] = 1;
            ky[1] = -1;
            kx[2] = -1;
            ky[2] = -1;
            kx[3] = -1;
            ky[3] = 1;
            kx[4] = 1;
            ky[4] = 1;
            int O = ((SectionDrawForm)sectionDrawForm).SelectedType;
            int i = ((SectionDrawForm)sectionDrawForm).SelectedBar;
            int j = ((SectionDrawForm)sectionDrawForm).SelecteLinedBar;
            int K = ((SectionDrawForm)sectionDrawForm).SelectedInED;
            int L = ((SectionDrawForm)sectionDrawForm).SelectedRecBar;
            int M = ((SectionDrawForm)sectionDrawForm).SelectedCorner;
            int N = ((SectionDrawForm)sectionDrawForm).SelectedRecShape;
            int P = ((SectionDrawForm)sectionDrawForm).SelectedCircleLineBar;
            int Q = ((SectionDrawForm)sectionDrawForm).SelectedCircleShape;
            SelecteLinedBar = ((SectionDrawForm)sectionDrawForm).SelecteLinedBar;
            SelecteRecLinedBar = ((SectionDrawForm)sectionDrawForm).SelecteRecLinedBar;
            #region///قضيب نقطة
            if (O == 1)
            {
                groupBox7.Visible = true;
                groupBox7.Location = new Point(10, 5);
                this.Width = 280; 
                textBox25.Text = ((SectionDrawForm)sectionDrawForm).Bar[i].XR.ToString();
                textBox26.Text = ((SectionDrawForm)sectionDrawForm).Bar[i].YR.ToString();
                textBox24.Text = ((SectionDrawForm)sectionDrawForm).Bar[i].DR.ToString();
            }
            #endregion
            #region///قضيب خط
            if (O == 2)
            {
                groupBox3.Visible = true;
                groupBox3.Location = new Point(10, 5);
                this.Width = 280; 
                textBox1.Text = ((SectionDrawForm)sectionDrawForm).LineBar[j].X1R.ToString();
                textBox2.Text = ((SectionDrawForm)sectionDrawForm).LineBar[j].Y1R.ToString();
                textBox4.Text = ((SectionDrawForm)sectionDrawForm).LineBar[j].X2R.ToString();
                textBox5.Text = ((SectionDrawForm)sectionDrawForm).LineBar[j].Y2R.ToString();
                textBox3.Text = ((SectionDrawForm)sectionDrawForm).LineBar[j].DR.ToString();
                textBox6.Text = ((SectionDrawForm)sectionDrawForm).LineBar[j].BarsNumbers.ToString();
                textBox7.Text = ((SectionDrawForm)sectionDrawForm).LineBar[j].Distance.ToString();
                comboBox1.SelectedIndex = ((SectionDrawForm)sectionDrawForm).LineBar[j].HasEndBars;
                HasEndBars = comboBox1.SelectedIndex;
                double theX1 = Convert.ToDouble(textBox1.Text);
                double theY1 = Convert.ToDouble(textBox2.Text);
                double theX2 = Convert.ToDouble(textBox4.Text);
                double theY2 = Convert.ToDouble(textBox5.Text);
                TheBars = Convert.ToInt32(textBox6.Text);
                if (HasEndBars == 1) TheBars = TheBars - 2;
                double DX = theX2 - theX1;
                double DY = theY2 - theY1;
                double length = Math.Sqrt(Math.Pow(DX, 2) + Math.Pow(DY, 2));
                TheDistance = Math.Round(length / (TheBars + 1), 3);
                textBox10.Text = TheDistance.ToString();
                textBox8.Text = Math.Round(length, 0).ToString();
            }
            #endregion
            #region/// مستطيل تسليح
            if (O == 3 || O == 30)
            {
                #region//حافة
                if (K != 0)
                {
                    groupBox5.Visible = true;
                    groupBox5.Location = new Point(10, 5);
                    this.Width = 280;
                    EDBarSizeTxt.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDDR[K].ToString();
                    EDMaxSTxt.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDDistance[K].ToString();
                    EDNTxt.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDBarsNumbers[K].ToString();
                    calcrecED();
                }
                #endregion
                #region//كامل المستطيل
                if (K == 0)
                {
                    groupBox4.Visible = true;
                    groupBox4.Location = new Point(10, 5);
                    this.Width = 540; 
                    double realdis = 0;
                    double Length = 0;
                    int Number = 0;
                    int thetype = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].Type  ;
                    if (thetype == 1)
                    {
                        groupBox8.Visible = false;
                        groupBox9.Visible = false;
                        groupBox10.Visible = false;
                        groupBox11.Visible = false;
                        groupBox12.Visible = false;
                        groupBox13.Visible = false;
                        groupBox14.Visible = false;
                        groupBox15.Visible = false;
                    }
                    int diameter1 = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].TIEDR;
                    textBox15.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].Width.ToString();
                    textBox16.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].Height.ToString();
                    textBox17.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].CenterX .ToString();
                    textBox18.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].CenterY.ToString();
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
                    realdis = Math.Round((Length-2*diameter1) / (Number+1), 3);
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
                }
                #endregion
            }
            #endregion
            #region/// الزاوية
            if (O == 4)
            {
                groupBox6.Visible = true;
                groupBox6.Location = new Point(10, 5);
                this.Width = 280; 
                if (M == 1) textBox23.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].CORDR[1].ToString();
                if (M == 2) textBox23.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].CORDR[2].ToString();
                if (M == 3) textBox23.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].CORDR[3].ToString();
                if (M == 4) textBox23.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].CORDR[4].ToString();
            }
            #endregion
            #region/// شكل مستطيل
            if (O == 5)
            {
                groupBox4.Visible = true;
                groupBox19.Visible = true;
                groupBox4.Location = new Point(10, 5);
                this.Width = 540; 
                textBox15.Text = ((SectionDrawForm)sectionDrawForm).RecShape[N].Width.ToString();
                textBox16.Text = ((SectionDrawForm)sectionDrawForm).RecShape[N].Height.ToString();
                textBox17.Text = ((SectionDrawForm)sectionDrawForm).RecShape[N].CenterX.ToString();
                textBox18.Text = ((SectionDrawForm)sectionDrawForm).RecShape[N].CenterY.ToString();
                textBox47.Text = ((SectionDrawForm)sectionDrawForm).RecShape[N].EDCoverR[1].ToString();
                textBox48.Text = ((SectionDrawForm)sectionDrawForm).RecShape[N].EDCoverR[2].ToString();
                textBox49.Text = ((SectionDrawForm)sectionDrawForm).RecShape[N].EDCoverR[3].ToString();
                textBox50.Text = ((SectionDrawForm)sectionDrawForm).RecShape[N].EDCoverR[4].ToString();
                textBox47.Visible = true;
                textBox48.Visible = true;
                textBox49.Visible = true;
                textBox50.Visible = true;
                label51.Visible = true;
                label52.Visible = true;
                label53.Visible = true;
                label54.Visible = true;
                comboBox2.SelectedIndex = ((SectionDrawForm)sectionDrawForm).RecShape[N].HasReinforcment;
                if (comboBox2.SelectedIndex != 0)
                {
                #region//كامل المستطيل
                    double realdis = 0;
                    double Length = 0;
                    int Number = 0;
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
                }
            }
            #endregion
            #region/// دائرة تسليح
            if (O == 6)
            {
                groupBox20.Visible = true;
                groupBox20.Location = new Point(10, 5);
                this.Width = 280;
                textBox60.Text = ((SectionDrawForm)sectionDrawForm).CircleLineBar[P].CenterX.ToString();
                textBox61.Text = ((SectionDrawForm)sectionDrawForm).CircleLineBar[P].CenterY.ToString();
                textBox62.Text = ((SectionDrawForm)sectionDrawForm).CircleLineBar[P].Diameter.ToString();
                textBox63.Text = ((SectionDrawForm)sectionDrawForm).CircleLineBar[P].DR.ToString();
                textBox64.Text = ((SectionDrawForm)sectionDrawForm).CircleLineBar[P].BarsNumbers.ToString();
                textBox65.Text = ((SectionDrawForm)sectionDrawForm).CircleLineBar[P].TIEDR.ToString();
            }
            #endregion
            #region/// دائرة شكل
            if (O == 7)
            {
                groupBox21.Visible = true;
                groupBox21.Location = new Point(10, 5);
                groupBox20.Location = new Point(10, 140);
                textBox60.Enabled  = false;
                textBox61.Enabled = false;
                textBox62.Enabled = false;
                this.Width = 280;
                textBox53.Text = ((SectionDrawForm)sectionDrawForm).CircleShape[Q].CoverR.ToString();
                textBox54.Text = ((SectionDrawForm)sectionDrawForm).CircleShape[Q].CenterX.ToString();
                textBox55.Text = ((SectionDrawForm)sectionDrawForm).CircleShape[Q].CenterY.ToString();
                textBox56.Text = ((SectionDrawForm)sectionDrawForm).CircleShape[Q].Diameter.ToString();
                double diameter = ((SectionDrawForm)sectionDrawForm).CircleShape[Q].Diameter;
                double cover = ((SectionDrawForm)sectionDrawForm).CircleShape[Q].CoverR;
                double diameter1 = diameter - 2 * cover;
                textBox60.Text = ((SectionDrawForm)sectionDrawForm).CircleShape[Q].CenterX.ToString();
                textBox61.Text = ((SectionDrawForm)sectionDrawForm).CircleShape[Q].CenterY.ToString();
                textBox62.Text = diameter1.ToString();
                comboBox3.SelectedIndex = ((SectionDrawForm)sectionDrawForm).CircleShape[Q].HasReinforcment;
                if (comboBox3.SelectedIndex != 0)
                {
                    textBox60.Text = ((SectionDrawForm)sectionDrawForm).CircleLineBar[P].CenterX.ToString();
                    textBox61.Text = ((SectionDrawForm)sectionDrawForm).CircleLineBar[P].CenterY.ToString();
                    textBox62.Text = ((SectionDrawForm)sectionDrawForm).CircleLineBar[P].Diameter.ToString();
                    textBox63.Text = ((SectionDrawForm)sectionDrawForm).CircleLineBar[P].DR.ToString();
                    textBox64.Text = ((SectionDrawForm)sectionDrawForm).CircleLineBar[P].BarsNumbers.ToString();
                    textBox65.Text = ((SectionDrawForm)sectionDrawForm).CircleLineBar[P].TIEDR.ToString();
                }
            }
            #endregion
            loaded = 1;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           if (loaded == 1 & HasEndBars != comboBox1.SelectedIndex)
            {
                HasEndBars = comboBox1.SelectedIndex;
                TheBars = 1;
                ListChanged = 1;
                calc();
                ListChanged = 0;
            }
        }
        private void textBox6_KeyUp(object sender, KeyEventArgs e)
        {
            BarOrDis = 1;
            calc();
        }
        private void textBox7_KeyUp(object sender, KeyEventArgs e)
        {
            BarOrDis = 2;
            calc();
        }
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (SelecteLinedBar != 0)///قضيب خط
            {
                BarOrDis = 1;
                calc();
            }
        }
        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (SelecteLinedBar != 0)///قضيب خط
            {
                BarOrDis = 1;
                calc();
            }
        }
        private void textBox4_KeyUp(object sender, KeyEventArgs e)
        {
            if (SelecteLinedBar != 0)///قضيب خط
            {
                BarOrDis = 1;
                calc();
            }
        }
        private void textBox5_KeyUp(object sender, KeyEventArgs e)
        {
            if (SelecteLinedBar != 0)///قضيب خط
            {
                BarOrDis = 1;
                calc();
            }
        }
        private void EDNTxt_KeyUp(object sender, KeyEventArgs e)
        {
            BarOrDis = 1;
            calcrecED();
        }
        private void EDMaxSTxt_KeyUp(object sender, KeyEventArgs e)
        {
            BarOrDis = 2;
            calcrecED();
        }
        private void textBox30_KeyUp(object sender, KeyEventArgs e)
        {
            SelectedInED = 1;
            BarOrDis = 2;
            calcrecRECALL();
        }
        private void textBox29_KeyUp(object sender, KeyEventArgs e)
        {
            SelectedInED = 1;
            BarOrDis = 1;
            calcrecRECALL();
        }
        private void textBox35_KeyUp(object sender, KeyEventArgs e)
        {
            SelectedInED = 2;
            BarOrDis = 2;
            calcrecRECALL();
        }
        private void textBox34_KeyUp(object sender, KeyEventArgs e)
        {
            SelectedInED = 2;
            BarOrDis = 1;
            calcrecRECALL();
        }
        private void textBox40_KeyUp(object sender, KeyEventArgs e)
        {
            SelectedInED = 3;
            BarOrDis = 2;
            calcrecRECALL();
        }
        private void textBox39_KeyUp(object sender, KeyEventArgs e)
        {
            SelectedInED = 3;
            BarOrDis = 1;
            calcrecRECALL();
        }
        private void textBox45_KeyUp(object sender, KeyEventArgs e)
        {
            SelectedInED = 4;
            BarOrDis = 2;
            calcrecRECALL();
        }
        private void textBox44_KeyUp(object sender, KeyEventArgs e)
        {
            SelectedInED = 4;
            BarOrDis = 1;
            calcrecRECALL();
        }
        private void textBox15_KeyUp(object sender, KeyEventArgs e)
        {
                BarOrDis = 2;
                SelectedInED = 1;
                calcrecRECALL();
                SelectedInED = 3;
                calcrecRECALL();
        }
        private void textBox16_KeyUp(object sender, KeyEventArgs e)
        {
                BarOrDis = 2;
                SelectedInED = 2;
                calcrecRECALL();
                SelectedInED = 4;
                calcrecRECALL();
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int O = ((SectionDrawForm)sectionDrawForm).SelectedType;
            int N = ((SectionDrawForm)sectionDrawForm).SelectedRecShape;
            if (O == 5)
            {
                if (comboBox2.SelectedIndex == 0)
                {
                    groupBox8.Visible = false;
                    groupBox9.Visible = false;
                    groupBox10.Visible = false;
                    groupBox11.Visible = false;
                    groupBox12.Visible = false;
                    groupBox13.Visible = false;
                    groupBox14.Visible = false;
                    groupBox15.Visible = false;
                    groupBox18.Visible = false;
                }
                else
                {
                    groupBox8.Visible = true;
                    groupBox9.Visible = true;
                    groupBox10.Visible = true;
                    groupBox11.Visible = true;
                    groupBox12.Visible = true;
                    groupBox13.Visible = true;
                    groupBox14.Visible = true;
                    groupBox15.Visible = true;
                    groupBox18.Visible = true;
                }
            }
            if (loaded == 1)
            {
                BarOrDis = 2;
                SelectedInED = 1;
                calcrecRECALL();
                SelectedInED = 2;
                calcrecRECALL();
                SelectedInED = 3;
                calcrecRECALL();
                SelectedInED = 4;
                calcrecRECALL();
            }
        }
        private void textBox19_KeyUp(object sender, KeyEventArgs e)
        {
            BarOrDis = 2;
            SelectedInED = 1;
            calcrecRECALL();
            SelectedInED = 2;
            calcrecRECALL();
            SelectedInED = 3;
            calcrecRECALL();
            SelectedInED = 4;
            calcrecRECALL();
        }
        private void textBox47_KeyUp(object sender, KeyEventArgs e)
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
        private void textBox49_KeyUp(object sender, KeyEventArgs e)
        {
            BarOrDis = 2;
            SelectedInED = 2;
            calcrecRECALL();
            SelectedInED = 4;
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
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int O = ((SectionDrawForm)sectionDrawForm).SelectedType;
            int N = ((SectionDrawForm)sectionDrawForm).SelectedCircleShape;
            if (O == 7)
            {
                if (comboBox3.SelectedIndex == 0)
                {
                    groupBox20.Visible = false;
                    textBox53.Visible = false;
                    label64.Visible = false;
                }
                else
                {
                    groupBox20.Visible = true;
                    textBox53.Visible = true;
                    label64.Visible = true;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int O = ((SectionDrawForm)sectionDrawForm).SelectedType;
            if (O == 5)  ////rectangular
            {
                Globals.Secsegment = 3;
                Globals.secH1 = Convert.ToDouble(textBox16.Text) / 1000;
                Globals.secB1 = Convert.ToDouble(textBox15.Text) / 1000;
                if (comboBox2.Text == "Yes")
                {
                    Globals.secType = 1;
                    Globals.secC1 = Convert.ToDouble(textBox47.Text) / 1000;

                    Globals.ASLn1 = 4 + Convert.ToInt16(textBox29.Text) + Convert.ToInt16(textBox34.Text);
                    Globals.ASLn1 = Globals.ASLn1 + Convert.ToInt16(textBox39.Text) + Convert.ToInt16(textBox44.Text);
                    Globals.dbar1 = Convert.ToInt16(textBox11.Text);

                }
                if (comboBox2.Text == "No")
                {
                    Globals.secType = 0;
                    Globals.secC1 = 0.05;
                }
            }
            if (O == 7)   //// circle
            {
                Globals.Secsegment = 3;
                Globals.secD1 = Convert.ToDouble(textBox56.Text) / 1000;
                if (comboBox3.Text == "Yes")
                {
                    Globals.secType = 2;
                    Globals.secC1 = Convert.ToDouble(textBox53.Text) / 1000;

                    Globals.ASLn1 = Convert.ToInt16(textBox64.Text);
                    Globals.dbar1 = Convert.ToInt16(textBox63.Text);
                }
                if (comboBox3.Text == "No")
                {
                    Globals.secC1 = 0.05;
                    Globals.secType = 0;
                }
            }
            Material_properties theform = new Material_properties();
            theform.ShowDialog();
        }
    }
}
