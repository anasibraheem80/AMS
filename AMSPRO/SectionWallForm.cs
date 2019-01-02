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
    public partial class SectionWallForm : Form
    {
        public SectionWallForm()
        {
            InitializeComponent();
        }
        Form sectionDrawForm = Application.OpenForms["SectionDrawForm"];
        int SelectedFlangedWallShape = 0;
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
                    double length = Convert.ToDouble(textBox3.Text) - CoverED2 - CoverED4;
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
                    double length = Convert.ToDouble(textBox4.Text) - CoverED1 - CoverED3;
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
                    double length = Convert.ToDouble(textBox3.Text) - CoverED2 - CoverED4; ;
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
                    double length = Convert.ToDouble(textBox4.Text) - CoverED1 - CoverED3;
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
                    double length = Convert.ToDouble(textBox5.Text) - CoverED1 - CoverED3;//*****
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
                    double length = Convert.ToDouble(textBox5.Text) - CoverED1 - CoverED3;//*********
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
        private void calcrecRECALLR()
        {
            int K = SelectedInED;
            int diameter1 = 0;
            int TheBars = 0;
            double TheDistance = 0;
            #region
            if (comboBox1.SelectedIndex == 1)
            {
                diameter1 = Convert.ToInt32(textBox69.Text);
                double CoverED1 = Convert.ToDouble(textBox92.Text);
                double CoverED2 = Convert.ToDouble(textBox86.Text);
                double CoverED3 = Convert.ToDouble(textBox80.Text);
                double CoverED4 = Convert.ToDouble(textBox74.Text);
                if (K == 1)
                {
                    double length = Convert.ToDouble(textBox9.Text) - CoverED2 - CoverED4;//********
                    if (BarOrDis == 1)
                    {
                        TheBars = Convert.ToInt32(textBox95.Text);
                        TheDistance = Math.Round((length - 2 * diameter1) / (TheBars + 1), 3);
                        if (loaded == 1)
                        {
                            textBox96.Text = TheDistance.ToString();
                            textBox93.Text = TheDistance.ToString();
                            textBox94.Text = Math.Round(length, 0).ToString();
                        }
                    }
                    if (BarOrDis == 2)
                    {
                        TheDistance = Convert.ToDouble(textBox96.Text);
                        TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                        TheDistance = Math.Round((length - 2 * diameter1) / (TheBars + 1), 3);
                        textBox95.Text = TheBars.ToString();
                        textBox93.Text = TheDistance.ToString();
                        textBox94.Text = Math.Round(length, 0).ToString();
                    }
                }
                if (K == 2)
                {
                    double length = Convert.ToDouble(textBox8.Text) - CoverED1 - CoverED3;//*****
                    if (BarOrDis == 1)
                    {
                        TheBars = Convert.ToInt32(textBox89.Text);
                        TheDistance = Math.Round((length - 2 * diameter1) / (TheBars + 1), 3);
                        if (loaded == 1)
                        {
                            textBox90.Text = TheDistance.ToString();
                            textBox87.Text = TheDistance.ToString();
                            textBox88.Text = Math.Round(length, 0).ToString();
                        }
                    }
                    if (BarOrDis == 2)
                    {
                        TheDistance = Convert.ToDouble(textBox90.Text);
                        TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                        TheDistance = Math.Round((length - 2 * diameter1) / (TheBars + 1), 3);
                        textBox89.Text = TheBars.ToString();
                        textBox87.Text = TheDistance.ToString();
                        textBox88.Text = Math.Round(length, 0).ToString();
                    }
                }
                if (K == 3)
                {
                    double length = Convert.ToDouble(textBox9.Text) - CoverED2 - CoverED4;//********
                    if (BarOrDis == 1)
                    {
                        TheBars = Convert.ToInt32(textBox83.Text);
                        TheDistance = Math.Round((length - 2 * diameter1) / (TheBars + 1), 3);
                        if (loaded == 1)
                        {
                            textBox84.Text = TheDistance.ToString();
                            textBox81.Text = TheDistance.ToString();
                            textBox82.Text = Math.Round(length, 0).ToString();
                        }
                    }
                    if (BarOrDis == 2)
                    {
                        TheDistance = Convert.ToDouble(textBox84.Text);
                        TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                        TheDistance = Math.Round((length - 2 * diameter1) / (TheBars + 1), 3);
                        textBox83.Text = TheBars.ToString();
                        textBox81.Text = TheDistance.ToString();
                        textBox82.Text = Math.Round(length, 0).ToString();
                    }
                }
                if (K == 4)
                {
                    double length = Convert.ToDouble(textBox8.Text) - CoverED1 - CoverED3;//*********
                    if (BarOrDis == 1)
                    {
                        TheBars = Convert.ToInt32(textBox77.Text);
                        TheDistance = Math.Round((length - 2 * diameter1) / (TheBars + 1), 3);
                        if (loaded == 1)
                        {
                            textBox78.Text = TheDistance.ToString();
                            textBox75.Text = TheDistance.ToString();
                            textBox76.Text = Math.Round(length, 0).ToString();
                        }
                    }
                    if (BarOrDis == 2)
                    {
                        TheDistance = Convert.ToDouble(textBox78.Text);
                        TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                        TheDistance = Math.Round((length - 2 * diameter1) / (TheBars + 1), 3);
                        textBox77.Text = TheBars.ToString();
                        textBox75.Text = TheDistance.ToString();
                        textBox76.Text = Math.Round(length, 0).ToString();
                    }
                }
            }
            #endregion
        }
        
        private void SectionWallForm_Load(object sender, EventArgs e)
        {
            kx[1] = 1;
            ky[1] = -1;
            kx[2] = -1;
            ky[2] = -1;
            kx[3] = -1;
            ky[3] = 1;
            kx[4] = 1;
            ky[4] = 1;
            SelectedFlangedWallShape = ((SectionDrawForm)sectionDrawForm).SelectedFlangedWallShape;
            textBox1.Text = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[SelectedFlangedWallShape].CenterX.ToString();
            textBox2.Text = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[SelectedFlangedWallShape].CenterY.ToString();
            textBox3.Text = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[SelectedFlangedWallShape].Length.ToString();
            textBox4.Text = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[SelectedFlangedWallShape].StemWidth.ToString();
            textBox5.Text = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[SelectedFlangedWallShape].LeftFlangWidth.ToString();
            textBox6.Text = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[SelectedFlangedWallShape].LeftFlangLength.ToString();
            textBox7.Text = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[SelectedFlangedWallShape].LeftFlangEccen.ToString();
            textBox8.Text = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[SelectedFlangedWallShape].RightFlangWidth.ToString();
            textBox9.Text = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[SelectedFlangedWallShape].RightFlangLength.ToString();
            textBox10.Text = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[SelectedFlangedWallShape].RightFlangEccen.ToString();
            textBox47.Text = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[SelectedFlangedWallShape].ED1CoverR.ToString();
            textBox48.Text = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[SelectedFlangedWallShape].ED2CoverR.ToString();
            textBox49.Text = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[SelectedFlangedWallShape].ED3CoverR.ToString();
            textBox50.Text = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[SelectedFlangedWallShape].ED4CoverR.ToString();
         
            textBox63.Text = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[SelectedFlangedWallShape].ED1LCoverR.ToString();
            textBox57.Text = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[SelectedFlangedWallShape].ED2LCoverR.ToString();
            textBox51.Text = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[SelectedFlangedWallShape].ED3LCoverR.ToString();
            textBox21.Text = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[SelectedFlangedWallShape].ED4LCoverR.ToString();

            textBox92.Text = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[SelectedFlangedWallShape].ED1RCoverR.ToString();
            textBox86.Text = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[SelectedFlangedWallShape].ED2RCoverR.ToString();
            textBox80.Text = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[SelectedFlangedWallShape].ED3RCoverR.ToString();
            textBox74.Text = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[SelectedFlangedWallShape].ED4RCoverR.ToString();

            comboBox1.SelectedIndex=((SectionDrawForm)sectionDrawForm).FlangedWallShape[SelectedFlangedWallShape].HasReinforcment;
            if (((SectionDrawForm)sectionDrawForm).FlangedWallShape[SelectedFlangedWallShape].StemSurrounded == 1) checkBox1.Checked =true ;
            if (((SectionDrawForm)sectionDrawForm).FlangedWallShape[SelectedFlangedWallShape].LeftFlangSurrounded == 1)checkBox2.Checked =true ;;
            if (((SectionDrawForm)sectionDrawForm).FlangedWallShape[SelectedFlangedWallShape].RightFlangSurrounded == 1) checkBox3.Checked = true; ;
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
                #region//كامل المستطيل
                L = ((SectionDrawForm)sectionDrawForm).SelectedRecBar3;
                diameter1 = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].TIEDR;
                textBox69.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].TIEDR.ToString();
                textBox73.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].CORDR[1].ToString();
                textBox72.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].CORDR[2].ToString();
                textBox70.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].CORDR[3].ToString();
                textBox71.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].CORDR[4].ToString();

                textBox97.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDDR[1].ToString();
                textBox96.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDDistance[1].ToString();
                textBox95.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDBarsNumbers[1].ToString();
                textBox94.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].Width.ToString();
                Length = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].Width;
                Number = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDBarsNumbers[1];
                realdis = Math.Round((Length - 2 * diameter1) / (Number + 1), 3);
                textBox93.Text = realdis.ToString();

                textBox91.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDDR[2].ToString();
                textBox90.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDDistance[2].ToString();
                textBox89.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDBarsNumbers[2].ToString();
                textBox88.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].Height.ToString();
                Length = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].Height;
                Number = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDBarsNumbers[2];
                realdis = Math.Round((Length - 2 * diameter1) / (Number + 1), 3);
                textBox87.Text = realdis.ToString();

                textBox85.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDDR[3].ToString();
                textBox84.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDDistance[3].ToString();
                textBox83.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDBarsNumbers[3].ToString();
                textBox82.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].Width.ToString();
                Length = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].Width;
                Number = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDBarsNumbers[3];
                realdis = Math.Round((Length - 2 * diameter1) / (Number + 1), 3);
                textBox81.Text = realdis.ToString();

                textBox79.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDDR[4].ToString();
                textBox78.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDDistance[4].ToString();
                textBox77.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDBarsNumbers[4].ToString();
                textBox76.Text = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].Height.ToString();
                Length = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].Height;
                Number = ((SectionDrawForm)sectionDrawForm).RecLineBar[L].EDBarsNumbers[4];
                realdis = Math.Round((Length - 2 * diameter1) / (Number + 1), 3);
                textBox75.Text = realdis.ToString();
                #endregion
            }
           loaded = 1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int SelectedFlangedWallShape = ((SectionDrawForm)sectionDrawForm).SelectedFlangedWallShape;
            int SelectedRecBar = ((SectionDrawForm)sectionDrawForm).SelectedRecBar;
            int SelectedRecBar1 = ((SectionDrawForm)sectionDrawForm).SelectedRecBar1;
            int SelectedRecBar2 = ((SectionDrawForm)sectionDrawForm).SelectedRecBar2;
            int SelectedRecBar3 = ((SectionDrawForm)sectionDrawForm).SelectedRecBar3;
            int RecShapeNumber = ((SectionDrawForm)sectionDrawForm).RecShapeNumber;
            int BarsNumber = ((SectionDrawForm)sectionDrawForm).BarsNumber;
            int RecBarsNumber = ((SectionDrawForm)sectionDrawForm).RecBarsNumber;
            int FlangedWallShapeNumber = ((SectionDrawForm)sectionDrawForm).FlangedWallShapeNumber;
            int TeeShapeNumber = ((SectionDrawForm)sectionDrawForm).TeeShapeNumber;
            #region////رسم شكل جدار
                double CenterX = Convert.ToDouble (textBox1.Text );
                double CenterY = Convert.ToDouble(textBox2.Text);
                double Length = Convert.ToDouble(textBox3.Text);
                double StemWidth = Convert.ToDouble(textBox4.Text);
                double LeftFlangWidth = Convert.ToDouble(textBox5.Text);
                double LeftFlangLength = Convert.ToDouble(textBox6.Text);
                double RightFlangWidth = Convert.ToDouble(textBox8.Text);
                double RightFlangLength = Convert.ToDouble(textBox9.Text);
                double LeftFlangEccen = Convert.ToDouble(textBox7.Text);
                double RightFlangEccen = Convert.ToDouble(textBox10.Text);

                double ED1CoverR = Convert.ToDouble(textBox47.Text);
                double ED2CoverR = Convert.ToDouble(textBox48.Text);
                double ED3CoverR = Convert.ToDouble(textBox49.Text);
                double ED4CoverR = Convert.ToDouble(textBox50.Text);

                double ED1LCoverR = Convert.ToDouble(textBox63.Text);
                double ED2LCoverR = Convert.ToDouble(textBox57.Text);
                double ED3LCoverR = Convert.ToDouble(textBox51.Text);
                double ED4LCoverR = Convert.ToDouble(textBox21.Text);

                double ED1RCoverR = Convert.ToDouble(textBox92.Text);
                double ED2RCoverR = Convert.ToDouble(textBox86.Text);
                double ED3RCoverR = Convert.ToDouble(textBox80.Text);
                double ED4RCoverR = Convert.ToDouble(textBox74.Text);              
                if (LeftFlangEccen != 0)
                {
                    double MAXECCLEFT = (LeftFlangWidth - StemWidth) / 2;
                    int ishara = Convert.ToInt32(LeftFlangEccen / Math.Abs(LeftFlangEccen));
                    if (Math.Abs(LeftFlangEccen) > Math.Abs(MAXECCLEFT)) LeftFlangEccen = Math.Abs(MAXECCLEFT);
                    LeftFlangEccen = ishara* Math.Abs(LeftFlangEccen);
                }
                if (RightFlangEccen != 0)
                {
                    double MAXECCRIGHT = (RightFlangWidth - StemWidth) / 2;
                    int ishara = Convert.ToInt32(RightFlangEccen / Math.Abs(RightFlangEccen));
                    if (Math.Abs(RightFlangEccen) > Math.Abs(MAXECCRIGHT)) RightFlangEccen = Math.Abs(MAXECCRIGHT);
                    RightFlangEccen = ishara * Math.Abs(RightFlangEccen);
                }
                double[] PointXReal = new double[13];
                double[] PointYReal = new double[13];
                FlangedWalls emp0 = new FlangedWalls();
                int ch1 = 0;
                int ch2 = 0;
                int ch3 = 0;
                if (checkBox1.Checked == true) ch1 = 1;
                if (checkBox2.Checked == true) ch2 = 1;
                if (checkBox3.Checked == true) ch3 = 1;
                emp0.StemSurrounded = ch1;
                emp0.LeftFlangSurrounded = ch2;
                emp0.RightFlangSurrounded = ch3;             
                emp0.CenterX = CenterX;
                emp0.CenterY = CenterY;
                emp0.Length = Length;
                emp0.StemWidth = StemWidth;
                emp0.LeftFlangWidth = LeftFlangWidth;
                emp0.LeftFlangLength = LeftFlangLength;
                emp0.RightFlangWidth = RightFlangWidth;
                emp0.RightFlangLength = RightFlangLength;
                emp0.LeftFlangEccen = LeftFlangEccen;
                emp0.RightFlangEccen = RightFlangEccen;
                emp0.Selected = 0;
                emp0.PointXReal[1] = CenterX - Length / 2;
                emp0.PointXReal[2] = CenterX - Length / 2 + LeftFlangLength;
                emp0.PointXReal[3] = CenterX - Length / 2 + LeftFlangLength;
                emp0.PointXReal[4] = CenterX + Length / 2 - RightFlangLength;
                emp0.PointXReal[5] = CenterX + Length / 2 - RightFlangLength;
                emp0.PointXReal[6] = CenterX + Length / 2;
                emp0.PointXReal[7] = CenterX + Length / 2;
                emp0.PointXReal[8] = CenterX + Length / 2 - RightFlangLength;
                emp0.PointXReal[9] = CenterX + Length / 2 - RightFlangLength;
                emp0.PointXReal[10] = CenterX - Length / 2 + LeftFlangLength;
                emp0.PointXReal[11] = CenterX - Length / 2 + LeftFlangLength;
                emp0.PointXReal[12] = CenterX - Length / 2;

                emp0.PointYReal[1] = CenterY + LeftFlangWidth / 2 + LeftFlangEccen;
                emp0.PointYReal[2] = CenterY + LeftFlangWidth / 2 + LeftFlangEccen;
                emp0.PointYReal[3] = CenterY + StemWidth / 2;
                emp0.PointYReal[4] = CenterY + StemWidth / 2;
                emp0.PointYReal[5] = CenterY + RightFlangWidth / 2 + RightFlangEccen;
                emp0.PointYReal[6] = CenterY + RightFlangWidth / 2 + RightFlangEccen;
                emp0.PointYReal[7] = CenterY - RightFlangWidth / 2 + RightFlangEccen;
                emp0.PointYReal[8] = CenterY - RightFlangWidth / 2 + RightFlangEccen;
                emp0.PointYReal[9] = CenterY - StemWidth / 2;
                emp0.PointYReal[10] = CenterY - StemWidth / 2;
                emp0.PointYReal[11] = CenterY - LeftFlangWidth / 2 + LeftFlangEccen;
                emp0.PointYReal[12] = CenterY - LeftFlangWidth / 2 + LeftFlangEccen;
                emp0.ED1CoverR = ED1CoverR;
                emp0.ED2CoverR = ED2CoverR;
                emp0.ED3CoverR = ED3CoverR;
                emp0.ED4CoverR = ED4CoverR;

                emp0.ED1LCoverR = ED1LCoverR;
                emp0.ED2LCoverR = ED2LCoverR;
                emp0.ED3LCoverR = ED3LCoverR;
                emp0.ED4LCoverR = ED4LCoverR;

                emp0.ED1RCoverR = ED1RCoverR;
                emp0.ED2RCoverR = ED2RCoverR;
                emp0.ED3RCoverR = ED3RCoverR;
                emp0.ED4RCoverR = ED4RCoverR;

            #endregion
            #region/// شكل مستطيل
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
                        #region
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
                        #region
                        for (int k = SelectedFlangedWallShape; k < FlangedWallShapeNumber; k++)//مضاف
                        {
                            ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k + 1];
                        }
                        for (int k = 1; k < RecBarsNumber; k++)//مضاف
                        {
                            if (((SectionDrawForm)sectionDrawForm).RecLineBar[k].InFlangedWallShape > SelectedFlangedWallShape)
                            {
                                ((SectionDrawForm)sectionDrawForm).RecLineBar[k].InFlangedWallShape = ((SectionDrawForm)sectionDrawForm).RecLineBar[k].InFlangedWallShape - 1;
                            }
                        }
                        #endregion
                        #region
                        for (int k = 1; k < RecShapeNumber+1; k++)//مضاف///////////////////////
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
                        for (int k = 1; k < FlangedWallShapeNumber; k++)//مضاف
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
                        if (SelectedRecBar3 > SelectedRecBar1) SelectedRecBar3 = SelectedRecBar3 - 1;
                        SelectedFlangedWallShape = FlangedWallShapeNumber;
                        SelectedRecBar1 = RecBarsNumber;
                        RecLineBars emp = new RecLineBars();
                        emp.InFlangedWallShape = SelectedFlangedWallShape;
                        emp.InRecShape = 0;
                        emp.Width = emp0.PointXReal[6] - emp0.PointXReal[1] - emp0.ED2CoverR - emp0.ED4CoverR;
                        emp.Height = emp0.PointYReal[3] - emp0.PointYReal[10] - emp0.ED1CoverR - emp0.ED3CoverR;
                        emp.CenterX = emp0.PointXReal[1] + emp0.ED4CoverR + emp.Width / 2;
                        emp.CenterY = emp0.PointYReal[3] - emp0.ED1CoverR - emp.Height / 2;
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
                                emp1.YR = Math.Round(emp0.PointYReal[3] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED1CoverR, 3);
                            }
                            if (k == 2)
                            {
                                emp1.XR = Math.Round(emp0.PointXReal[6] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED2CoverR, 3);
                                emp1.YR = Math.Round(emp0.PointYReal[4] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED1CoverR, 3);
                            }
                            if (k == 3)
                            {
                                emp1.XR = Math.Round(emp0.PointXReal[7] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED2CoverR, 3);
                                emp1.YR = Math.Round(emp0.PointYReal[9] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED3CoverR, 3);
                            }
                            if (k == 4)
                            {
                                emp1.XR = Math.Round(emp0.PointXReal[12] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED4CoverR, 3);
                                emp1.YR = Math.Round(emp0.PointYReal[10] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED3CoverR, 3);
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
                        #region
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
                        #region
                        for (int k = SelectedFlangedWallShape; k < FlangedWallShapeNumber; k++)//مضاف
                        {
                            ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k + 1];
                        }
                        for (int k = 1; k < RecBarsNumber; k++)//مضاف
                        {
                            if (((SectionDrawForm)sectionDrawForm).RecLineBar[k].InFlangedWallShape > SelectedFlangedWallShape)
                            {
                                ((SectionDrawForm)sectionDrawForm).RecLineBar[k].InFlangedWallShape = ((SectionDrawForm)sectionDrawForm).RecLineBar[k].InFlangedWallShape - 1;
                            }
                        }
                        #endregion
                        for (int k = 1; k < RecShapeNumber+1; k++)//مضاف///////////////////////
                        {
                            if (((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars > SelectedRecBar2)
                            {
                                ((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars = ((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars - 1;
                            }
                        }
                        for (int k = 1; k < TeeShapeNumber + 1; k++)//مضاف
                        {
                            if (((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] > SelectedRecBar2)
                            {
                                ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] = ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] - 1;
                                ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[2] = ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[2] - 1;
                            }
                        }
                        for (int k = 1; k < FlangedWallShapeNumber; k++)//مضاف
                        {
                            if (((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[2] > SelectedRecBar2)
                            {
                                ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[2] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[2] - 1;
                                ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] - 1;
                                ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[3] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[3] - 1;
                            }
                        }
                        if (SelectedRecBar3 > SelectedRecBar2) SelectedRecBar3 = SelectedRecBar3 - 1;
                        SelectedFlangedWallShape = FlangedWallShapeNumber;
                        SelectedRecBar2 = RecBarsNumber;
                        emp = new RecLineBars();
                        emp.InFlangedWallShape = SelectedFlangedWallShape;
                        emp.InRecShape = 0;
                        emp.Width = emp0.PointXReal[2] - emp0.PointXReal[1] - emp0.ED2LCoverR - emp0.ED4LCoverR;
                        emp.Height = emp0.PointYReal[1] - emp0.PointYReal[12] - emp0.ED1LCoverR - emp0.ED3LCoverR;
                        emp.CenterX = emp0.PointXReal[1] + emp0.ED4LCoverR + emp.Width / 2;
                        emp.CenterY = emp0.PointYReal[1] - emp0.ED1LCoverR - emp.Height / 2;
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
                                emp1.XR = Math.Round(emp0.PointXReal[1] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED4LCoverR, 3);
                                emp1.YR = Math.Round(emp0.PointYReal[1] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED1LCoverR, 3);
                            }
                            if (k == 2)
                            {
                                emp1.XR = Math.Round(emp0.PointXReal[2] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED2LCoverR, 3);
                                emp1.YR = Math.Round(emp0.PointYReal[2] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED1LCoverR, 3);
                            }
                            if (k == 3)
                            {
                                emp1.XR = Math.Round(emp0.PointXReal[11] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED2LCoverR, 3);
                                emp1.YR = Math.Round(emp0.PointYReal[11] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED3LCoverR, 3);
                            }
                            if (k == 4)
                            {
                                emp1.XR = Math.Round(emp0.PointXReal[12] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED4LCoverR, 3);
                                emp1.YR = Math.Round(emp0.PointYReal[12] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED3LCoverR, 3);
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
                            SelectedRecBar3 = RecBarsNumber;
                        }
                    startloopR: { }
                        #region
                        for (int k = 1; k < BarsNumber + 1; k++)
                        {
                            if (((SectionDrawForm)sectionDrawForm).Bar[k].InREC == SelectedRecBar3)
                            {
                                BarsNumber = BarsNumber - 1;
                                for (int l = k; l < BarsNumber + 1; l++)
                                {
                                    ((SectionDrawForm)sectionDrawForm).Bar[l] = ((SectionDrawForm)sectionDrawForm).Bar[l + 1];
                                }
                                if (k <= BarsNumber) goto startloopR;
                            }
                        }
                        for (int k = 1; k < BarsNumber + 1; k++)
                        {
                            if (((SectionDrawForm)sectionDrawForm).Bar[k].InREC > SelectedRecBar3)
                            {
                                ((SectionDrawForm)sectionDrawForm).Bar[k].InREC = ((SectionDrawForm)sectionDrawForm).Bar[k].InREC - 1;
                            }
                        }
                        for (int k = SelectedRecBar3; k < RecBarsNumber; k++)
                        {
                            ((SectionDrawForm)sectionDrawForm).RecLineBar[k] = ((SectionDrawForm)sectionDrawForm).RecLineBar[k + 1];
                        }
                        #endregion
                        #region
                        for (int k = SelectedFlangedWallShape; k < FlangedWallShapeNumber; k++)//مضاف
                        {
                            ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k + 1];
                        }
                        for (int k = 1; k < RecBarsNumber; k++)//مضاف
                        {
                            if (((SectionDrawForm)sectionDrawForm).RecLineBar[k].InFlangedWallShape > SelectedFlangedWallShape)
                            {
                                ((SectionDrawForm)sectionDrawForm).RecLineBar[k].InFlangedWallShape = ((SectionDrawForm)sectionDrawForm).RecLineBar[k].InFlangedWallShape - 1;
                            }
                        }
                        #endregion
                        for (int k = 1; k < RecShapeNumber + 1; k++)//مضاف///////////////////////
                        {
                            if (((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars > SelectedRecBar3)
                            {
                                ((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars = ((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars - 1;
                            }
                        }
                        for (int k = 1; k < TeeShapeNumber + 1; k++)//مضاف
                        {
                            if (((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] > SelectedRecBar3)
                            {
                                ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] = ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] - 1;
                                ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[2] = ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[2] - 1;
                            }
                        }
                        for (int k = 1; k < FlangedWallShapeNumber; k++)//مضاف
                        {
                            if (((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[3] > SelectedRecBar3)
                            {
                                ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[2] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[2] - 1;
                                ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] - 1;
                                ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[3] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[3] - 1;
                            }
                        }
                        SelectedFlangedWallShape = FlangedWallShapeNumber;
                        SelectedRecBar3 = RecBarsNumber;
                        emp = new RecLineBars();
                        emp.InFlangedWallShape = SelectedFlangedWallShape;
                        emp.InRecShape = 0;
                        emp.Width = emp0.PointXReal[6] - emp0.PointXReal[5] - emp0.ED2RCoverR - emp0.ED4RCoverR;
                        emp.Height = emp0.PointYReal[6] - emp0.PointYReal[7] - emp0.ED1RCoverR - emp0.ED3RCoverR;
                        emp.CenterX = emp0.PointXReal[5] + emp0.ED4RCoverR + emp.Width / 2;
                        emp.CenterY = emp0.PointYReal[5] - emp0.ED1RCoverR - emp.Height / 2;
                        emp.TIEDR = Convert.ToInt32(textBox69.Text);
                        emp.CORDR[1] = Convert.ToInt32(textBox73.Text);
                        emp.CORDR[2] = Convert.ToInt32(textBox72.Text);
                        emp.CORDR[3] = Convert.ToInt32(textBox70.Text);
                        emp.CORDR[4] = Convert.ToInt32(textBox71.Text);
                        emp.EDDR[1] = Convert.ToInt32(textBox97.Text);
                        emp.EDDistance[1] = Convert.ToDouble(textBox96.Text);
                        emp.EDBarsNumbers[1] = Convert.ToInt32(textBox95.Text);
                        emp.EDX1R[1] = emp.CenterX - emp.Width / 2;
                        emp.EDY1R[1] = emp.CenterY + emp.Height / 2;
                        emp.EDX2R[1] = emp.CenterX + emp.Width / 2;
                        emp.EDY2R[1] = emp.CenterY + emp.Height / 2;
                        emp.EDDR[2] = Convert.ToInt32(textBox91.Text);
                        emp.EDDistance[2] = Convert.ToDouble(textBox90.Text);
                        emp.EDBarsNumbers[2] = Convert.ToInt32(textBox89.Text);
                        emp.EDX1R[2] = emp.CenterX + emp.Width / 2;
                        emp.EDY1R[2] = emp.CenterY + emp.Height / 2;
                        emp.EDX2R[2] = emp.CenterX + emp.Width / 2;
                        emp.EDY2R[2] = emp.CenterY - emp.Height / 2;
                        emp.EDDR[3] = Convert.ToInt32(textBox85.Text);
                        emp.EDDistance[3] = Convert.ToDouble(textBox84.Text);
                        emp.EDBarsNumbers[3] = Convert.ToInt32(textBox83.Text);
                        emp.EDX1R[3] = emp.CenterX + emp.Width / 2;
                        emp.EDY1R[3] = emp.CenterY - emp.Height / 2;
                        emp.EDX2R[3] = emp.CenterX - emp.Width / 2;
                        emp.EDY2R[3] = emp.CenterY - emp.Height / 2;
                        emp.EDDR[4] = Convert.ToInt32(textBox79.Text);
                        emp.EDDistance[4] = Convert.ToDouble(textBox78.Text);
                        emp.EDBarsNumbers[4] = Convert.ToInt32(textBox77.Text);
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
                                emp1.XR = Math.Round(emp0.PointXReal[5] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED4RCoverR, 3);
                                emp1.YR = Math.Round(emp0.PointYReal[5] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED1RCoverR, 3);
                            }
                            if (k == 2)
                            {
                                emp1.XR = Math.Round(emp0.PointXReal[6] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED2RCoverR, 3);
                                emp1.YR = Math.Round(emp0.PointYReal[6] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED1RCoverR, 3);
                            }
                            if (k == 3)
                            {
                                emp1.XR = Math.Round(emp0.PointXReal[7] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED2RCoverR, 3);
                                emp1.YR = Math.Round(emp0.PointYReal[7] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED3RCoverR, 3);
                            }
                            if (k == 4)
                            {
                                emp1.XR = Math.Round(emp0.PointXReal[8] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED4RCoverR, 3);
                                emp1.YR = Math.Round(emp0.PointYReal[8] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED3RCoverR, 3);
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
                       //emp0.ApplyedToRecBars[1] = RecBarsNumber - 1;
                        //emp0.ApplyedToRecBars[2] = RecBarsNumber;
                        #endregion
                        emp0.ApplyedToRecBars[1] = RecBarsNumber - 2;
                        emp0.ApplyedToRecBars[2] = RecBarsNumber - 1;
                        emp0.ApplyedToRecBars[3] = RecBarsNumber;
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
                            for (int k = 1; k < RecShapeNumber + 1; k++)//مضاف///////////////////////
                            {
                                if (((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars > SelectedRecBar1)
                                {
                                    ((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars = ((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars - 1;
                                }
                            }
                            for (int k = 1; k < TeeShapeNumber + 1; k++)//مضاف
                            {
                                if (((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] > SelectedRecBar1)
                                {
                                    ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] = ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] - 1;
                                    ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[2] = ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[2] - 1;
                                }
                            }
                            for (int k = 1; k < FlangedWallShapeNumber + 1; k++)//مضاف
                            {
                                if (((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] > SelectedRecBar1)
                                {
                                    ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] - 1;
                                    ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[2] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[2] - 1;
                                    ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[3] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[3] - 1;
                                }
                            }
                            RecBarsNumber = RecBarsNumber - 1;
                            ///////////////////////////////////////////////////////////////////////////////////////
                            if (SelectedRecBar2 > SelectedRecBar1) SelectedRecBar2 = SelectedRecBar2 - 1;
                            if (SelectedRecBar3 > SelectedRecBar1) SelectedRecBar3 = SelectedRecBar3 - 1;
                            ((SectionDrawForm)sectionDrawForm).RecLineBar[SelectedRecBar2].InFlangedWallShape = 0;
                        startloopL: { }
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
                            for (int k = 1; k < RecShapeNumber + 1; k++)//مضاف///////////////////////
                            {
                                if (((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars > SelectedRecBar2)
                                {
                                    ((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars = ((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars - 1;
                                }
                            }
                            for (int k = 1; k < TeeShapeNumber + 1; k++)//مضاف
                            {
                                if (((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] > SelectedRecBar2)
                                {
                                    ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] = ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] - 1;
                                    ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[2] = ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[2] - 1;
                                }
                            }
                            for (int k = 1; k < FlangedWallShapeNumber + 1; k++)//مضاف
                            {
                                if (((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[2] > SelectedRecBar2)
                                {
                                    ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[2] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[2] - 1;
                                    ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] - 1;
                                    ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[3] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[3] - 1;
                                }
                            }
                            RecBarsNumber = RecBarsNumber - 1;
                            /////////////////////////////////////////////////////////////////////////////////
                            if (SelectedRecBar3 > SelectedRecBar2) SelectedRecBar3 = SelectedRecBar3 - 1;
                            ((SectionDrawForm)sectionDrawForm).RecLineBar[SelectedRecBar3].InFlangedWallShape = 0;
                        startloopR: { }
                            for (int k = 1; k < BarsNumber + 1; k++)
                            {
                                if (((SectionDrawForm)sectionDrawForm).Bar[k].InREC == SelectedRecBar3)
                                {
                                    BarsNumber = BarsNumber - 1;
                                    for (int l = k; l < BarsNumber + 1; l++)
                                    {
                                        ((SectionDrawForm)sectionDrawForm).Bar[l] = ((SectionDrawForm)sectionDrawForm).Bar[l + 1];
                                    }
                                    if (k <= BarsNumber) goto startloopR;
                                }
                            }
                            for (int k = 1; k < BarsNumber + 1; k++)
                            {
                                if (((SectionDrawForm)sectionDrawForm).Bar[k].InREC > SelectedRecBar3)
                                {
                                    ((SectionDrawForm)sectionDrawForm).Bar[k].InREC = ((SectionDrawForm)sectionDrawForm).Bar[k].InREC - 1;
                                }
                            }
                            for (int k = SelectedRecBar3; k < RecBarsNumber; k++)
                            {
                                ((SectionDrawForm)sectionDrawForm).RecLineBar[k] = ((SectionDrawForm)sectionDrawForm).RecLineBar[k + 1];
                            }
                            for (int k = 1; k < RecShapeNumber + 1; k++)//مضاف///////////////////////
                            {
                                if (((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars > SelectedRecBar3)
                                {
                                    ((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars = ((SectionDrawForm)sectionDrawForm).RecShape[k].ApplyedToRecBars - 1;
                                }
                            }
                            for (int k = 1; k < TeeShapeNumber + 1; k++)//مضاف
                            {
                                if (((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] > SelectedRecBar3)
                                {
                                    ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] = ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[1] - 1;
                                    ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[2] = ((SectionDrawForm)sectionDrawForm).TeeShape[k].ApplyedToRecBars[2] - 1;
                                }
                            }
                            for (int k = 1; k < FlangedWallShapeNumber + 1; k++)//مضاف
                            {
                                if (((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[3] > SelectedRecBar3)
                                {
                                    ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[2] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[2] - 1;
                                    ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[1] - 1;
                                    ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[3] = ((SectionDrawForm)sectionDrawForm).FlangedWallShape[k].ApplyedToRecBars[3] - 1;
                                }
                            }
                            RecBarsNumber = RecBarsNumber - 1;
                            /////////////////////////////////////////////////////////////////////////////////
                            ((SectionDrawForm)sectionDrawForm).RecBarsNumber = RecBarsNumber;
                            ((SectionDrawForm)sectionDrawForm).BarsNumber = BarsNumber;
                        }
                    }
                    #endregion
                #endregion
                ((SectionDrawForm)sectionDrawForm).FlangedWallShape[SelectedFlangedWallShape] = emp0;
                ((SectionDrawForm)sectionDrawForm).MakeTempFiles();
                ((SectionDrawForm)sectionDrawForm).Render2d();
            this.Close(); 
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

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
                calcrecRECALLR();
                SelectedInED = 2;
                calcrecRECALL();
                calcrecRECALLL();
                calcrecRECALLR();
                SelectedInED = 3;
                calcrecRECALL();
                calcrecRECALLL();
                calcrecRECALLR();
                SelectedInED = 4;
                calcrecRECALL();
                calcrecRECALLL();
                calcrecRECALLR();
            }
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

        private void textBox54_KeyUp(object sender, KeyEventArgs e)
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

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            if (comboBox1.SelectedIndex != 0)
            {
                BarOrDis = 2;
                SelectedInED = 1;
                calcrecRECALL();
                calcrecRECALLL();
                calcrecRECALLR();
                SelectedInED = 2;
                calcrecRECALL();
                calcrecRECALLL();
                calcrecRECALLR();
                SelectedInED = 3;
                calcrecRECALL();
                calcrecRECALLL();
                calcrecRECALLR();
                SelectedInED = 4;
                calcrecRECALL();
                calcrecRECALLL();
                calcrecRECALLR();
            }
        }

        private void textBox4_KeyUp(object sender, KeyEventArgs e)
        {
            if (comboBox1.SelectedIndex != 0)
            {
                BarOrDis = 2;
                SelectedInED = 1;
                calcrecRECALL();
                calcrecRECALLL();
                calcrecRECALLR();
                SelectedInED = 2;
                calcrecRECALL();
                calcrecRECALLL();
                calcrecRECALLR();
                SelectedInED = 3;
                calcrecRECALL();
                calcrecRECALLL();
                calcrecRECALLR();
                SelectedInED = 4;
                calcrecRECALL();
                calcrecRECALLL();
                calcrecRECALLR();
            }
        }

        private void textBox5_KeyUp(object sender, KeyEventArgs e)
        {
            if (comboBox1.SelectedIndex != 0)
            {
                BarOrDis = 2;
                SelectedInED = 1;
                calcrecRECALL();
                calcrecRECALLL();
                calcrecRECALLR();
                SelectedInED = 2;
                calcrecRECALL();
                calcrecRECALLL();
                calcrecRECALLR();
                SelectedInED = 3;
                calcrecRECALL();
                calcrecRECALLL();
                calcrecRECALLR();
                SelectedInED = 4;
                calcrecRECALL();
                calcrecRECALLL();
                calcrecRECALLR();
            }
        }

        private void textBox6_KeyUp(object sender, KeyEventArgs e)
        {
            if (comboBox1.SelectedIndex != 0)
            {
                BarOrDis = 2;
                SelectedInED = 1;
                calcrecRECALL();
                calcrecRECALLL();
                calcrecRECALLR();
                SelectedInED = 2;
                calcrecRECALL();
                calcrecRECALLL();
                calcrecRECALLR();
                SelectedInED = 3;
                calcrecRECALL();
                calcrecRECALLL();
                calcrecRECALLR();
                SelectedInED = 4;
                calcrecRECALL();
                calcrecRECALLL();
                calcrecRECALLR();
            }
        }

        private void textBox8_KeyUp(object sender, KeyEventArgs e)
        {
            if (comboBox1.SelectedIndex != 0)
            {
                BarOrDis = 2;
                SelectedInED = 1;
                calcrecRECALL();
                calcrecRECALLL();
                calcrecRECALLR();
                SelectedInED = 2;
                calcrecRECALL();
                calcrecRECALLL();
                calcrecRECALLR();
                SelectedInED = 3;
                calcrecRECALL();
                calcrecRECALLL();
                calcrecRECALLR();
                SelectedInED = 4;
                calcrecRECALL();
                calcrecRECALLL();
                calcrecRECALLR();
            }
        }

        private void textBox9_KeyUp(object sender, KeyEventArgs e)
        {
            if (comboBox1.SelectedIndex != 0)
            {
                BarOrDis = 2;
                SelectedInED = 1;
                calcrecRECALL();
                calcrecRECALLL();
                calcrecRECALLR();
                SelectedInED = 2;
                calcrecRECALL();
                calcrecRECALLL();
                calcrecRECALLR();
                SelectedInED = 3;
                calcrecRECALL();
                calcrecRECALLL();
                calcrecRECALLR();
                SelectedInED = 4;
                calcrecRECALL();
                calcrecRECALLL();
                calcrecRECALLR();
            }
        }

        private void textBox96_KeyUp(object sender, KeyEventArgs e)
        {
            SelectedInED = 1;
            BarOrDis = 2;
            calcrecRECALLR();
        }

        private void textBox90_KeyUp(object sender, KeyEventArgs e)
        {
            SelectedInED = 2;
            BarOrDis = 2;
            calcrecRECALLR();
        }

        private void textBox84_KeyUp(object sender, KeyEventArgs e)
        {
            SelectedInED = 3;
            BarOrDis = 2;
            calcrecRECALLR();
        }

        private void textBox78_KeyUp(object sender, KeyEventArgs e)
        {
            SelectedInED = 4;
            BarOrDis = 2;
            calcrecRECALLR();
        }

        private void textBox95_KeyUp(object sender, KeyEventArgs e)
        {
            SelectedInED = 1;
            BarOrDis = 1;
            calcrecRECALLR();
        }

        private void textBox89_KeyUp(object sender, KeyEventArgs e)
        {
            SelectedInED = 2;
            BarOrDis = 1;
            calcrecRECALLR();
        }

        private void textBox83_KeyUp(object sender, KeyEventArgs e)
        {
            SelectedInED = 3;
            BarOrDis = 1;
            calcrecRECALLR();
        }

        private void textBox77_KeyUp(object sender, KeyEventArgs e)
        {
            SelectedInED = 4;
            BarOrDis = 1;
            calcrecRECALLR();
        }

        private void textBox92_KeyUp(object sender, KeyEventArgs e)
        {
            BarOrDis = 2;
            SelectedInED = 2;
            calcrecRECALLR();
            SelectedInED = 4;
            calcrecRECALLR();
        }

        private void textBox80_KeyUp(object sender, KeyEventArgs e)
        {
            BarOrDis = 2;
            SelectedInED = 2;
            calcrecRECALLR();
            SelectedInED = 4;
            calcrecRECALLR();
        }

        private void textBox86_KeyUp(object sender, KeyEventArgs e)
        {
            BarOrDis = 2;
            SelectedInED = 1;
            calcrecRECALLR();
            SelectedInED = 3;
            calcrecRECALLR();
        }

        private void textBox74_KeyUp(object sender, KeyEventArgs e)
        {
            BarOrDis = 2;
            SelectedInED = 1;
            calcrecRECALLR();
            SelectedInED = 3;
            calcrecRECALLR();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double Length1 = Convert.ToDouble(textBox3.Text) - Convert.ToDouble(textBox6.Text) - Convert.ToDouble(textBox9.Text);
            if (comboBox1.Text == "Yes")
            {
                Globals.secType = 1;
                if (tabControl1.SelectedIndex == 0)  // web
                {
                    Globals.secH1 = Length1 / 1000;
                    Globals.secB1 = Convert.ToDouble(textBox4.Text) / 1000;
                    Globals.secC1 = Convert.ToDouble(textBox47.Text) / 1000;
                    Globals.ASLn1 = 4 + Convert.ToInt16(textBox29.Text) + Convert.ToInt16(textBox34.Text);
                    Globals.ASLn1 = Globals.ASLn1 + Convert.ToInt16(textBox39.Text) + Convert.ToInt16(textBox44.Text);
                    Globals.dbar1 = Convert.ToInt16(textBox11.Text);
                    Globals.Secsegment = 0;
                }
                if (tabControl1.SelectedIndex == 1)  // Left
                {
                    Globals.secH1 = Convert.ToDouble(textBox6.Text) / 1000;
                    Globals.secB1 = Convert.ToDouble(textBox5.Text) / 1000;
                    Globals.secC1 = Convert.ToDouble(textBox63.Text) / 1000;
                    Globals.ASLn1 = 4 + Convert.ToInt16(textBox24.Text) + Convert.ToInt16(textBox54.Text);
                    Globals.ASLn1 = Globals.ASLn1 + Convert.ToInt16(textBox66.Text) + Convert.ToInt16(textBox60.Text);
                    Globals.dbar1 = Convert.ToInt16(textBox20.Text);
                    Globals.Secsegment = 1;
                }
                if (tabControl1.SelectedIndex == 2)  // right
                {
                    Globals.secH1 = Convert.ToDouble(textBox8.Text) / 1000;
                    Globals.secB1 = Convert.ToDouble(textBox9.Text) / 1000;
                    Globals.secC1 = Convert.ToDouble(textBox92.Text) / 1000;
                    Globals.ASLn1 = 4 + Convert.ToInt16(textBox77.Text) + Convert.ToInt16(textBox83.Text);
                    Globals.ASLn1 = Globals.ASLn1 + Convert.ToInt16(textBox89.Text) + Convert.ToInt16(textBox95.Text);
                    Globals.dbar1 = Convert.ToInt16(textBox73.Text);
                    Globals.Secsegment = 2;
                }
            }
            if (comboBox1.Text == "No")
            {
                Globals.secH1 = Convert.ToDouble(textBox3.Text) / 1000;
                Globals.secB1 = Convert.ToDouble(textBox4.Text) / 1000;
                Globals.secC1 = 0.05;
                Globals.Secsegment = 3;
                Globals.secType = 0;
            }
            Material_properties theform = new Material_properties();
            theform.ShowDialog();
        }

    }
}
