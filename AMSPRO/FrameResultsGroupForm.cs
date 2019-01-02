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
    public partial class FrameResultsGroupForm : Form
    {
        public FrameResultsGroupForm()
        {
            InitializeComponent();
        }
        Form mainForm = Application.OpenForms["MainForm"];
        int xmove = 0;
        int ymove = 0;
        int[] anyJ = new int[9];
        double[] MAXpowerj = new double[9];
        double disancValue = 0;
        double plus = 0;
        int AnyDiagram = 0;
        int m = 0;
        double tx1R = 0;
        double ty1R = 0;
        double tz1R = 0;
        double tx2R = 0;
        double ty2R = 0;
        double tz2R = 0;
        int[] selectedBeams2 = new int[100];
        int[] anystart = new int[100];
        double BeamLengthALL = 0;
        int intlengthALL = 0;
        private void FrameResultsGroupForm_Load(object sender, EventArgs e)
        {
            AnyDiagram = 5;
            pictureBox1.Controls.Add(pictureBox11);
            pictureBox11.Location = new Point(0, 0);
            pictureBox11.BackColor = Color.Transparent;
            int i = Frame.SelectedforProp;
            int[] selectedBeams = new int[100];
             m = 1;
            selectedBeams[m] = i;
            tx1R = Joint.XReal[((MainForm)mainForm).FrameElement[selectedBeams[m]].FirstJoint];
            ty1R = Joint.YReal[((MainForm)mainForm).FrameElement[selectedBeams[m]].FirstJoint];
            tz1R = Joint.ZReal[((MainForm)mainForm).FrameElement[selectedBeams[m]].FirstJoint];
            tx2R = Joint.XReal[((MainForm)mainForm).FrameElement[selectedBeams[m]].SecondJoint];
            ty2R = Joint.YReal[((MainForm)mainForm).FrameElement[selectedBeams[m]].SecondJoint];
            tz2R = Joint.ZReal[((MainForm)mainForm).FrameElement[selectedBeams[m]].SecondJoint];
            if (tz1R == tz2R)
            {
                #region//تحديد الجوائز المرتبطة من اول جهة
            stertloop: { };
                for (int k = 1; k < Frame.Number + 1; k++)
                {
                    for (int l = 1; l < m + 1; l++)
                    {
                        if (k == selectedBeams[l]) goto nextk;
                    }
                    tx1R = Joint.XReal[((MainForm)mainForm).FrameElement[selectedBeams[m]].FirstJoint];
                    ty1R = Joint.YReal[((MainForm)mainForm).FrameElement[selectedBeams[m]].FirstJoint];
                    tz1R = Joint.ZReal[((MainForm)mainForm).FrameElement[selectedBeams[m]].FirstJoint];
                    tx2R = Joint.XReal[((MainForm)mainForm).FrameElement[selectedBeams[m]].SecondJoint];
                    ty2R = Joint.YReal[((MainForm)mainForm).FrameElement[selectedBeams[m]].SecondJoint];
                    tz2R = Joint.ZReal[((MainForm)mainForm).FrameElement[selectedBeams[m]].SecondJoint];
                    double angel0 = Angulo(tx1R, ty1R, tx2R, ty2R);
                    double tx11R = Joint.XReal[((MainForm)mainForm).FrameElement[k].FirstJoint];
                    double ty11R = Joint.YReal[((MainForm)mainForm).FrameElement[k].FirstJoint];
                    double tz11R = Joint.ZReal[((MainForm)mainForm).FrameElement[k].FirstJoint];
                    double tx21R = Joint.XReal[((MainForm)mainForm).FrameElement[k].SecondJoint];
                    double ty21R = Joint.YReal[((MainForm)mainForm).FrameElement[k].SecondJoint];
                    double tz21R = Joint.ZReal[((MainForm)mainForm).FrameElement[k].SecondJoint];
                    if (tz11R == tz21R & tz1R == tz2R & tz11R == tz1R)
                    {
                        if (((MainForm)mainForm).FrameElement[k].FirstJoint == ((MainForm)mainForm).FrameElement[selectedBeams[m]].SecondJoint)
                        {
                            double angel = Angulo(tx11R, ty11R, tx21R, ty21R);
                            if (Math.Abs(angel0 - angel) <= 15)
                            {
                                m = m + 1;
                                selectedBeams[m] = k;
                                goto stertloop;
                            }
                        }
                        if (((MainForm)mainForm).FrameElement[k].SecondJoint == ((MainForm)mainForm).FrameElement[selectedBeams[m]].SecondJoint)
                        {
                            double angel = Angulo(tx11R, ty11R, tx21R, ty21R);
                            if (Math.Abs(angel0 - angel) <= 15)
                            {
                                m = m + 1;
                                selectedBeams[m] = k;
                                goto stertloop;
                            }
                        }
                        if (((MainForm)mainForm).FrameElement[k].SecondJoint == ((MainForm)mainForm).FrameElement[selectedBeams[m]].FirstJoint)
                        {
                            double angel = Angulo(tx11R, ty11R, tx21R, ty21R);
                            if (Math.Abs(angel0 - angel) <= 15)
                            {
                                m = m + 1;
                                selectedBeams[m] = k;
                                goto stertloop;
                            }
                        }
                        if (((MainForm)mainForm).FrameElement[k].FirstJoint == ((MainForm)mainForm).FrameElement[selectedBeams[m]].FirstJoint)
                        {
                            double angel = Angulo(tx11R, ty11R, tx21R, ty21R);
                            if (Math.Abs(angel0 - angel) <= 15)
                            {
                                m = m + 1;
                                selectedBeams[m] = k;
                                goto stertloop;
                            }
                        }
                    }
                nextk: { };
                }
                #endregion
                int m1 = 0;
                int mm = 0;
                #region//تحديد الجوائز المرتبطة من ثاني جهة
            stertloop1: { };
                for (int k = 1; k < Frame.Number + 1; k++)
                {
                    for (int l = 1; l < m + 1; l++)
                    {
                        if (k == selectedBeams[l]) goto nextk1;
                    }
                    if (m1 != 0)
                    {
                        mm = m;
                        tx1R = Joint.XReal[((MainForm)mainForm).FrameElement[mm].FirstJoint];
                        ty1R = Joint.YReal[((MainForm)mainForm).FrameElement[mm].FirstJoint];
                        tz1R = Joint.ZReal[((MainForm)mainForm).FrameElement[mm].FirstJoint];
                        tx2R = Joint.XReal[((MainForm)mainForm).FrameElement[mm].SecondJoint];
                        ty2R = Joint.YReal[((MainForm)mainForm).FrameElement[mm].SecondJoint];
                        tz2R = Joint.ZReal[((MainForm)mainForm).FrameElement[mm].SecondJoint];
                    }
                    else
                    {
                        mm = 1;
                        tx1R = Joint.XReal[((MainForm)mainForm).FrameElement[mm].FirstJoint];
                        ty1R = Joint.YReal[((MainForm)mainForm).FrameElement[mm].FirstJoint];
                        tz1R = Joint.ZReal[((MainForm)mainForm).FrameElement[mm].FirstJoint];
                        tx2R = Joint.XReal[((MainForm)mainForm).FrameElement[mm].SecondJoint];
                        ty2R = Joint.YReal[((MainForm)mainForm).FrameElement[mm].SecondJoint];
                        tz2R = Joint.ZReal[((MainForm)mainForm).FrameElement[mm].SecondJoint];
                    }
                    double angel0 = Angulo(tx1R, ty1R, tx2R, ty2R);
                    double tx11R = Joint.XReal[((MainForm)mainForm).FrameElement[k].FirstJoint];
                    double ty11R = Joint.YReal[((MainForm)mainForm).FrameElement[k].FirstJoint];
                    double tz11R = Joint.ZReal[((MainForm)mainForm).FrameElement[k].FirstJoint];
                    double tx21R = Joint.XReal[((MainForm)mainForm).FrameElement[k].SecondJoint];
                    double ty21R = Joint.YReal[((MainForm)mainForm).FrameElement[k].SecondJoint];
                    double tz21R = Joint.ZReal[((MainForm)mainForm).FrameElement[k].SecondJoint];
                    if (tz11R == tz21R & tz1R == tz2R & tz11R == tz1R)
                    {
                        if (((MainForm)mainForm).FrameElement[k].FirstJoint == ((MainForm)mainForm).FrameElement[mm].SecondJoint)
                        {
                            double angel = Angulo(tx11R, ty11R, tx21R, ty21R);
                            if (Math.Abs(angel0 - angel) <= 15)
                            {
                                m1 = 1;
                                m = m + 1;
                                selectedBeams[m] = k;
                                goto stertloop1;
                            }
                        }
                        if (((MainForm)mainForm).FrameElement[k].SecondJoint == ((MainForm)mainForm).FrameElement[mm].SecondJoint)
                        {
                            double angel = Angulo(tx11R, ty11R, tx21R, ty21R);
                            if (Math.Abs(angel0 - angel) <= 15)
                            {
                                m1 = 1;
                                m = m + 1;
                                selectedBeams[m] = k;
                                goto stertloop1;
                            }
                        }
                        if (((MainForm)mainForm).FrameElement[k].SecondJoint == ((MainForm)mainForm).FrameElement[mm].FirstJoint)
                        {
                            double angel = Angulo(tx11R, ty11R, tx21R, ty21R);
                            if (Math.Abs(angel0 - angel) <= 15)
                            {
                                m1 = 1;
                                m = m + 1;
                                selectedBeams[m] = k;
                                goto stertloop1;
                            }
                        }
                        if (((MainForm)mainForm).FrameElement[k].FirstJoint == ((MainForm)mainForm).FrameElement[mm].FirstJoint)
                        {
                            double angel = Angulo(tx11R, ty11R, tx21R, ty21R);
                            if (Math.Abs(angel0 - angel) <= 15)
                            {
                                m1 = 1;
                                m = m + 1;
                                selectedBeams[m] = k;
                                goto stertloop1;
                            }
                        }
                    }
                nextk1: { };
                }
                #endregion
            }
            #region//إيجاد أول جائز في المجموعة
            int FirstBeam = 0;
            for (int k = 1; k < m + 1; k++)
            {
                int tah1 = 0;
                int tah2 = 0;
                for (int l = 1; l < m + 1; l++)
                {
                    if (k != l)
                    {
                        if (((MainForm)mainForm).FrameElement[selectedBeams[k]].FirstJoint == ((MainForm)mainForm).FrameElement[selectedBeams[l]].FirstJoint) tah1 = 1;
                        if (((MainForm)mainForm).FrameElement[selectedBeams[k]].FirstJoint == ((MainForm)mainForm).FrameElement[selectedBeams[l]].SecondJoint) tah1 = 1;
                    }
                }
                for (int l = 1; l < m + 1; l++)
                {
                    if (k != l)
                    {
                       if (((MainForm)mainForm).FrameElement[selectedBeams[k]].SecondJoint == ((MainForm)mainForm).FrameElement[selectedBeams[l]].FirstJoint) tah2 = 1;
                       if (((MainForm)mainForm).FrameElement[selectedBeams[k]].SecondJoint == ((MainForm)mainForm).FrameElement[selectedBeams[l]].SecondJoint) tah2 = 1;
                    }
                }
                if (tah1 == 0 || tah2 == 0)
                {
                    if (tah1 == 0 )anystart[1] = 1;
                    if (tah2 == 0) anystart[1] = 2;
                    FirstBeam = k;
                    break;
                }
            }
            #endregion
            #region//ترتيب الجوائز
            int m2 = 1;
            selectedBeams2[m2] = selectedBeams[FirstBeam];
            int[] FirstBeam0 = new int[100];
            FirstBeam0[1] = FirstBeam;
        startloop2: { };
            for (int k = 1; k < m + 1; k++)
            {
                for (int l = 1; l < m + 1; l++)
                {
                    if (k == FirstBeam0[l]) goto nextk2;
                }
                if (((MainForm)mainForm).FrameElement[selectedBeams[k]].FirstJoint ==((MainForm)mainForm).FrameElement[selectedBeams[FirstBeam0[m2]]].FirstJoint)
                {
                    m2 = m2 + 1;
                    FirstBeam0[m2] = k;
                    selectedBeams2[m2] = selectedBeams[k];
                    anystart[m2] = 1;
                    goto startloop2;
                }
                if (((MainForm)mainForm).FrameElement[selectedBeams[k]].FirstJoint ==((MainForm)mainForm).FrameElement[selectedBeams[FirstBeam0[m2]]].SecondJoint)
                {
                    m2 = m2 + 1;
                    FirstBeam0[m2] = k;
                    selectedBeams2[m2] = selectedBeams[k];
                    anystart[m2] = 1;
                    goto startloop2;
                }
                if (((MainForm)mainForm).FrameElement[selectedBeams[k]].SecondJoint ==((MainForm)mainForm).FrameElement[selectedBeams[FirstBeam0[m2]]].FirstJoint)
                {
                    m2 = m2 + 1;
                    FirstBeam0[m2] = k;
                    selectedBeams2[m2] = selectedBeams[k];
                    anystart[m2] = 2;
                    goto startloop2;
                }
                if (((MainForm)mainForm).FrameElement[selectedBeams[k]].SecondJoint ==((MainForm)mainForm).FrameElement[selectedBeams[FirstBeam0[m2]]].SecondJoint)
                {
                    m2 = m2 + 1;
                    FirstBeam0[m2] = k;
                    selectedBeams2[m2] = selectedBeams[k];
                    anystart[m2] = 2;
                    goto startloop2;
                }
            nextk2: { };
            }
            #endregion
            BeamLengthALL = 0;
            for (int k = 1; k < m + 1; k++)
            {
                tx1R = Joint.XReal[((MainForm)mainForm).FrameElement[selectedBeams2[k]].FirstJoint];
                ty1R = Joint.YReal[((MainForm)mainForm).FrameElement[selectedBeams2[k]].FirstJoint];
                tz1R = Joint.ZReal[((MainForm)mainForm).FrameElement[selectedBeams2[k]].FirstJoint];
                tx2R = Joint.XReal[((MainForm)mainForm).FrameElement[selectedBeams2[k]].SecondJoint];
                ty2R = Joint.YReal[((MainForm)mainForm).FrameElement[selectedBeams2[k]].SecondJoint];
                tz2R = Joint.ZReal[((MainForm)mainForm).FrameElement[selectedBeams2[k]].SecondJoint];
                 BeamLengthALL = BeamLengthALL+ (Math.Sqrt(Math.Pow(tx1R - tx2R, 2) + Math.Pow(ty1R - ty2R, 2) + Math.Pow(tz1R - tz2R, 2)));
            }
            DrawDiagram();
          /////////////////////////////
        }

        private void DrawDiagram()
        {
            Bitmap finalBmp1 = new Bitmap(1100, 600);
            if (pictureBox1.Image != null) pictureBox1.Image = null;
            pictureBox1.Image = finalBmp1;
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            Pen pen = new Pen(Color.Green, 1f);
            Font drawFont = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            #region//رسم أول خط
            int tx1 = 10;
            int ty1 = pictureBox1.Height / 3;
            int tx2 = pictureBox1.Width - tx1;
            int ty2 = ty1;
            int tx = 0;
            int ty = 0;
            int tx0 = 0;
            int ty0 = 0;
            intlengthALL = pictureBox1.Width - 2 * tx1;
            int intpower = ty1 - 5;
            g.DrawLine(pen, tx1, ty1, tx2, ty2);
            #endregion
            #region//تحديد اعظم قيمة
            double MAXpower = -1000000000;
            double ThePowerValue = 0;
            for (int k = 1; k < m + 1; k++)
            {
                for (int j = 0; j < Frame.AnalisesSecNumbers + 1; j++)
                {
                    if (AnyDiagram == 1) ThePowerValue = Frame.ResultValue1[selectedBeams2[k], j];//axial
                    if (AnyDiagram == 2) ThePowerValue = Frame.ResultValue2[selectedBeams2[k], j];//s22
                    if (AnyDiagram == 3) ThePowerValue = Frame.ResultValue4[selectedBeams2[k], j];//s33
                    if (AnyDiagram == 4) ThePowerValue = Frame.ResultValue5[selectedBeams2[k], j];//m22
                    if (AnyDiagram == 5) ThePowerValue = Frame.ResultValue3[selectedBeams2[k], j];//m33
                    if (AnyDiagram == 6) ThePowerValue = Frame.ResultValue6[selectedBeams2[k], j];//torsion
                    if (AnyDiagram == 7) ThePowerValue = Frame.ResultValue7[selectedBeams2[k], j];//u1
                    if (AnyDiagram == 8) ThePowerValue = Frame.ResultValue8[selectedBeams2[k], j];//u2
                    if (Math.Abs(ThePowerValue) > MAXpower)
                    {
                        MAXpowerj[AnyDiagram] = Math.Abs(ThePowerValue);
                        anyJ[AnyDiagram] = j;
                        MAXpower = Math.Abs(ThePowerValue);
                    }
                }
            }
            #endregion
            #region  ///الرسم
            if (MAXpower > 0)
            {
                double startx = 0;
                double TheDistanceValue = 0;
                for (int k = 1; k < m + 1; k++)
                {
                    tx1R = Joint.XReal[((MainForm)mainForm).FrameElement[selectedBeams2[k]].FirstJoint];
                    ty1R = Joint.YReal[((MainForm)mainForm).FrameElement[selectedBeams2[k]].FirstJoint];
                    tz1R = Joint.ZReal[((MainForm)mainForm).FrameElement[selectedBeams2[k]].FirstJoint];
                    tx2R = Joint.XReal[((MainForm)mainForm).FrameElement[selectedBeams2[k]].SecondJoint];
                    ty2R = Joint.YReal[((MainForm)mainForm).FrameElement[selectedBeams2[k]].SecondJoint];
                    tz2R = Joint.ZReal[((MainForm)mainForm).FrameElement[selectedBeams2[k]].SecondJoint];
                    double BeamLength = 0;
                    BeamLength = (Math.Sqrt(Math.Pow(tx1R - tx2R, 2) + Math.Pow(ty1R - ty2R, 2) + Math.Pow(tz1R - tz2R, 2)));
                    plus = BeamLength / Frame.AnalisesSecNumbers;
                    pen = new Pen(Color.LightGray, 1f);
                    int Tx3 = Convert.ToInt32(tx1 + startx);
                    int Ty3 = 0;
                    int Tx4 = Convert.ToInt32(tx1 + startx);
                    int Ty4 = pictureBox1.Height;
                    g.DrawLine(pen, Tx3, Ty3, Tx4, Ty4);
                    if (k == m)
                    {
                        Tx3 = tx1 + intlengthALL;
                        Ty3 = 0;
                        Tx4 = Tx3;
                        Ty4 = pictureBox1.Height;
                        g.DrawLine(pen, Tx3, Ty3, Tx4, Ty4);
                    }

                    pen = new Pen(Color.Black, 1f);
                    for (int j = 0; j < Frame.AnalisesSecNumbers + 1; j++)
                    {
                        if (anystart[k] == 1)
                        {
                            if (AnyDiagram == 1) ThePowerValue = Frame.ResultValue1[selectedBeams2[k], j];//axial
                            if (AnyDiagram == 2) ThePowerValue = Frame.ResultValue2[selectedBeams2[k], j];//s22
                            if (AnyDiagram == 3) ThePowerValue = Frame.ResultValue4[selectedBeams2[k], j];//s33
                            if (AnyDiagram == 4) ThePowerValue = Frame.ResultValue5[selectedBeams2[k], j];//m22
                            if (AnyDiagram == 5) ThePowerValue = Frame.ResultValue3[selectedBeams2[k], j];//m33
                            if (AnyDiagram == 6) ThePowerValue = Frame.ResultValue6[selectedBeams2[k], j];//torsion
                            if (AnyDiagram == 7) ThePowerValue = Frame.ResultValue7[selectedBeams2[k], j];//u1
                            if (AnyDiagram == 8) ThePowerValue = Frame.ResultValue8[selectedBeams2[k], j];//u2
                        }
                        if (anystart[k] == 2)
                        {
                            if (AnyDiagram == 1) ThePowerValue = Frame.ResultValue1[selectedBeams2[k], Frame.AnalisesSecNumbers - j];//axial
                            if (AnyDiagram == 2) ThePowerValue = Frame.ResultValue2[selectedBeams2[k], Frame.AnalisesSecNumbers - j];//s22
                            if (AnyDiagram == 3) ThePowerValue = Frame.ResultValue4[selectedBeams2[k], Frame.AnalisesSecNumbers - j];//s33
                            if (AnyDiagram == 4) ThePowerValue = Frame.ResultValue5[selectedBeams2[k], Frame.AnalisesSecNumbers - j];//m22
                            if (AnyDiagram == 5) ThePowerValue = Frame.ResultValue3[selectedBeams2[k], Frame.AnalisesSecNumbers - j];//m33
                            if (AnyDiagram == 6) ThePowerValue = Frame.ResultValue6[selectedBeams2[k], Frame.AnalisesSecNumbers - j];//torsion
                            if (AnyDiagram == 7) ThePowerValue = Frame.ResultValue7[selectedBeams2[k], Frame.AnalisesSecNumbers - j];//u1
                            if (AnyDiagram == 8) ThePowerValue = Frame.ResultValue8[selectedBeams2[k], Frame.AnalisesSecNumbers - j];//u2
                        }
                        TheDistanceValue =(j * plus / BeamLengthALL * intlengthALL);
                        if (j == Frame.AnalisesSecNumbers) TheDistanceValue = (BeamLength / BeamLengthALL * intlengthALL);
                        tx = Convert.ToInt32(tx1 + TheDistanceValue + startx);
                        ty = ty1 + Convert.ToInt32(ThePowerValue / MAXpower * intpower);
                        g.DrawLine(pen, tx, ty, tx, ty1);
                        if (j > 0) g.DrawLine(pen, tx, ty, tx0, ty0);
                        tx0 = tx;
                        ty0 = ty;
                    }

                    int Tx1 = Convert.ToInt32(tx1+startx);
                    int Ty1 = pictureBox1.Height*3 / 4;
                    int Tx2 =Convert.ToInt32( tx1+startx + TheDistanceValue);
                    int Ty2 = Ty1;
                    g.DrawLine(pen, Tx1, Ty1, Tx2, Ty2);
                    Tx3 = Tx1 - 5;
                    Ty3 = Ty1 + 5;
                    Tx4 = Tx1 + 5;
                    Ty4 = Ty1 - 5;
                    g.DrawLine(pen, Tx3, Ty3, Tx4, Ty4);

                    string ghg = "Joint ";
                    if (anystart[k] == 1) ghg = ghg + ((MainForm)mainForm).FrameElement[selectedBeams2[k]].FirstJoint;
                    if (anystart[k] == 2) ghg = ghg + ((MainForm)mainForm).FrameElement[selectedBeams2[k]].SecondJoint;
                    int aa1 = Convert.ToInt32(g.MeasureString(ghg, Font).Width/2);
                    if (k == 1) aa1 = -5;
                    Tx3 = Tx1 - aa1;
                    Ty3 = Ty1 - 20;
                    g.DrawString(ghg, drawFont, drawBrush, Tx3, Ty3);

                    ghg = BeamLength.ToString() + " m";
                    aa1 = Convert.ToInt32(g.MeasureString(ghg, Font).Width / 2);
                    Tx3 = (Tx1 + Tx2) / 2 - aa1;
                    Ty3 = Ty1 - 15;
                    g.DrawString(ghg, drawFont, drawBrush, Tx3, Ty3);

                    if (anystart[k] == 1) ghg = "I <<-- B " + selectedBeams2[k].ToString() + " -->> J";
                    if (anystart[k] == 2) ghg = "J <<-- B " + selectedBeams2[k].ToString() + " -->> I";
                    aa1 = Convert.ToInt32(g.MeasureString(ghg, Font).Width / 2);
                    Tx3 = (Tx1 + Tx2) / 2 - aa1;
                    Ty3 = Ty1 + 15;
                    g.DrawString(ghg, drawFont, drawBrush, Tx3, Ty3);
                    startx = startx + TheDistanceValue;

                    if (k == m)
                    {
                        Tx3 =Convert.ToInt32(tx1 + startx - 5);
                        Ty3 = Ty1 + 5;
                        Tx4 =Convert.ToInt32( tx1 + startx + 5);
                        Ty4 = Ty1 - 5;
                        g.DrawLine(pen, Tx3, Ty3, Tx4, Ty4);

                        ghg = "Joint ";
                        if (anystart[k] == 1) ghg = ghg + ((MainForm)mainForm).FrameElement[selectedBeams2[k]].SecondJoint;
                        if (anystart[k] == 2) ghg = ghg + ((MainForm)mainForm).FrameElement[selectedBeams2[k]].FirstJoint;
                        aa1 =Convert.ToInt32 (g.MeasureString(ghg, Font).Width);
                        Tx3 =Convert.ToInt32( tx1 + startx - aa1);
                        Ty3 = Ty1 - 20;
                        g.DrawString(ghg, drawFont, drawBrush, Tx3, Ty3);
                    }
                }
            }
            #endregion
        }

        private void movemouse()
        {
            Bitmap finalBmp1 = new Bitmap(1100, 600);
            if (pictureBox11.Image != null) pictureBox11.Image = null;
            pictureBox11.Image = finalBmp1;
            Graphics g = Graphics.FromImage(pictureBox11.Image);
            Pen pen = new Pen(Color.Red, 1f);
            Font drawFont = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            #region//حساب مكان القيم
            int tx1 = 10;
            int ty1 = pictureBox1.Height / 3;
            int tx2 = pictureBox1.Width - tx1;
            int ty2 = ty1;

            disancValue = (Convert.ToDouble(xmove - tx1) / Convert.ToDouble(intlengthALL)) * BeamLengthALL;
            if (xmove < tx1)
            {
                xmove = tx1;
                disancValue = 0;
            }
            if (xmove > tx1 + intlengthALL)
            {
                xmove = tx1 + intlengthALL;
                disancValue = BeamLengthALL;
            }
            pen = new Pen(Color.Red, 1f);
            g.DrawLine(pen, xmove, ty1 - 300, xmove, ty1 + 300);//رسم الخط الأحمر

            double BeamLength = 0;
            double BeamLength0 = 0;
            int SelectedBeam = 0;
            for (int k = 1; k < m + 1; k++)
            {
                tx1R = Joint.XReal[((MainForm)mainForm).FrameElement[selectedBeams2[k]].FirstJoint];
                ty1R = Joint.YReal[((MainForm)mainForm).FrameElement[selectedBeams2[k]].FirstJoint];
                tz1R = Joint.ZReal[((MainForm)mainForm).FrameElement[selectedBeams2[k]].FirstJoint];
                tx2R = Joint.XReal[((MainForm)mainForm).FrameElement[selectedBeams2[k]].SecondJoint];
                ty2R = Joint.YReal[((MainForm)mainForm).FrameElement[selectedBeams2[k]].SecondJoint];
                tz2R = Joint.ZReal[((MainForm)mainForm).FrameElement[selectedBeams2[k]].SecondJoint];
                BeamLength = (Math.Sqrt(Math.Pow(tx1R - tx2R, 2) + Math.Pow(ty1R - ty2R, 2) + Math.Pow(tz1R - tz2R, 2)));
                BeamLength0 = BeamLength0 + (Math.Sqrt(Math.Pow(tx1R - tx2R, 2) + Math.Pow(ty1R - ty2R, 2) + Math.Pow(tz1R - tz2R, 2)));
                if (disancValue <= BeamLength0)
                {
                    SelectedBeam = k;
                    break;
                }
            }
            int i = selectedBeams2[SelectedBeam];
            textBox10.Text = Math.Round(disancValue, 2).ToString();
            disancValue = BeamLength - (BeamLength0 - disancValue);
            textBox9.Text = Math.Round(disancValue, 2).ToString();
            tx1R = Joint.XReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
            ty1R = Joint.YReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
            tz1R = Joint.ZReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
            tx2R = Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
            ty2R = Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
            tz2R = Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
            BeamLength = (Math.Sqrt(Math.Pow(tx1R - tx2R, 2) + Math.Pow(ty1R - ty2R, 2) + Math.Pow(tz1R - tz2R, 2)));
            plus = BeamLength / Frame.AnalisesSecNumbers;

            double ThePowerValue = 0;
            double ThePowerValue1 = 0;
            double ThePowerValue2 = 0;
            double TheDistanceValue1 = 0;
            double TheDistanceValue2 = 0;
            double farkV12 = 0;
            double farkD12 = 0;
            double farkD = 0;
            double farkV = 0;
            ThePowerValue = ThePowerValue1 + farkV;
            for (int j = 0; j < Frame.AnalisesSecNumbers; j++)
            {
                TheDistanceValue1 = j * plus;
                TheDistanceValue2 = (j + 1) * plus;
                if (j == Frame.AnalisesSecNumbers - 1)
                {
                    TheDistanceValue2 = BeamLength;
                }
                if (disancValue >= TheDistanceValue1 & disancValue <= TheDistanceValue2)
                {
                    #region
                    if (anystart[SelectedBeam] == 1)
                    {
                        ThePowerValue1 = Frame.ResultValue1[i, j];
                        ThePowerValue2 = Frame.ResultValue1[i, j + 1];
                        farkV12 = ThePowerValue2 - ThePowerValue1;
                        farkD12 = TheDistanceValue2 - TheDistanceValue1;
                        farkD = disancValue - TheDistanceValue1;
                        farkV = farkV12 * farkD / farkD12;
                        ThePowerValue = ThePowerValue1 + farkV;
                        textBox1.Text = Math.Round(ThePowerValue, 0).ToString();//axial

                        ThePowerValue1 = Frame.ResultValue2[i, j];
                        ThePowerValue2 = Frame.ResultValue2[i, j + 1];
                        farkV12 = ThePowerValue2 - ThePowerValue1;
                        farkD12 = TheDistanceValue2 - TheDistanceValue1;
                        farkD = disancValue - TheDistanceValue1;
                        farkV = farkV12 * farkD / farkD12;
                        ThePowerValue = ThePowerValue1 + farkV;
                        textBox2.Text = Math.Round(ThePowerValue, 0).ToString();//s22

                        ThePowerValue1 = Frame.ResultValue4[i, j];
                        ThePowerValue2 = Frame.ResultValue4[i, j + 1];
                        farkV12 = ThePowerValue2 - ThePowerValue1;
                        farkD12 = TheDistanceValue2 - TheDistanceValue1;
                        farkD = disancValue - TheDistanceValue1;
                        farkV = farkV12 * farkD / farkD12;
                        ThePowerValue = ThePowerValue1 + farkV;
                        textBox3.Text = Math.Round(ThePowerValue, 0).ToString();//s33

                        ThePowerValue1 = Frame.ResultValue5[i, j];
                        ThePowerValue2 = Frame.ResultValue5[i, j + 1];
                        farkV12 = ThePowerValue2 - ThePowerValue1;
                        farkD12 = TheDistanceValue2 - TheDistanceValue1;
                        farkD = disancValue - TheDistanceValue1;
                        farkV = farkV12 * farkD / farkD12;
                        ThePowerValue = ThePowerValue1 + farkV;
                        textBox4.Text = Math.Round(ThePowerValue, 0).ToString();//m22

                        ThePowerValue1 = Frame.ResultValue3[i, j];
                        ThePowerValue2 = Frame.ResultValue3[i, j + 1];
                        farkV12 = ThePowerValue2 - ThePowerValue1;
                        farkD12 = TheDistanceValue2 - TheDistanceValue1;
                        farkD = disancValue - TheDistanceValue1;
                        farkV = farkV12 * farkD / farkD12;
                        ThePowerValue = ThePowerValue1 + farkV;
                        textBox5.Text = Math.Round(ThePowerValue, 0).ToString();//m33

                        ThePowerValue1 = Frame.ResultValue6[i, j];
                        ThePowerValue2 = Frame.ResultValue6[i, j + 1];
                        farkV12 = ThePowerValue2 - ThePowerValue1;
                        farkD12 = TheDistanceValue2 - TheDistanceValue1;
                        farkD = disancValue - TheDistanceValue1;
                        farkV = farkV12 * farkD / farkD12;
                        ThePowerValue = ThePowerValue1 + farkV;
                        textBox6.Text = Math.Round(ThePowerValue, 0).ToString();//torsion

                        ThePowerValue1 = Frame.ResultValue7[i, j];
                        ThePowerValue2 = Frame.ResultValue7[i, j + 1];
                        farkV12 = ThePowerValue2 - ThePowerValue1;
                        farkD12 = TheDistanceValue2 - TheDistanceValue1;
                        farkD = disancValue - TheDistanceValue1;
                        farkV = farkV12 * farkD / farkD12;
                        ThePowerValue = ThePowerValue1 + farkV;
                        textBox7.Text = Math.Round(ThePowerValue, 6).ToString();//u1

                        ThePowerValue1 = Frame.ResultValue8[i, j];
                        ThePowerValue2 = Frame.ResultValue8[i, j + 1];
                        farkV12 = ThePowerValue2 - ThePowerValue1;
                        farkD12 = TheDistanceValue2 - TheDistanceValue1;
                        farkD = disancValue - TheDistanceValue1;
                        farkV = farkV12 * farkD / farkD12;
                        ThePowerValue = ThePowerValue1 + farkV;
                        textBox8.Text = Math.Round(ThePowerValue, 6).ToString();//u2
                        break;
                    }
                    #endregion
                    #region
                    if (anystart[SelectedBeam] == 2)
                    {

                        ThePowerValue1 = Frame.ResultValue1[i, Frame.AnalisesSecNumbers - j];
                        ThePowerValue2 = Frame.ResultValue1[i, Frame.AnalisesSecNumbers - (j + 1)];
                        farkV12 = ThePowerValue2 - ThePowerValue1;
                        farkD12 = TheDistanceValue2 - TheDistanceValue1;
                        farkD = disancValue - TheDistanceValue1;
                        farkV = farkV12 * farkD / farkD12;
                        ThePowerValue = ThePowerValue1 + farkV;
                        textBox1.Text = Math.Round(ThePowerValue, 0).ToString();//axial

                        ThePowerValue1 = Frame.ResultValue2[i, Frame.AnalisesSecNumbers - j];
                        ThePowerValue2 = Frame.ResultValue2[i, Frame.AnalisesSecNumbers - (j + 1)];
                        farkV12 = ThePowerValue2 - ThePowerValue1;
                        farkD12 = TheDistanceValue2 - TheDistanceValue1;
                        farkD = disancValue - TheDistanceValue1;
                        farkV = farkV12 * farkD / farkD12;
                        ThePowerValue = ThePowerValue1 + farkV;
                        textBox2.Text = Math.Round(ThePowerValue, 0).ToString();//s22

                        ThePowerValue1 = Frame.ResultValue4[i, Frame.AnalisesSecNumbers - j];
                        ThePowerValue2 = Frame.ResultValue4[i, Frame.AnalisesSecNumbers - (j + 1)];
                        farkV12 = ThePowerValue2 - ThePowerValue1;
                        farkD12 = TheDistanceValue2 - TheDistanceValue1;
                        farkD = disancValue - TheDistanceValue1;
                        farkV = farkV12 * farkD / farkD12;
                        ThePowerValue = ThePowerValue1 + farkV;
                        textBox3.Text = Math.Round(ThePowerValue, 0).ToString();//s33

                        ThePowerValue1 = Frame.ResultValue5[i, Frame.AnalisesSecNumbers - j];
                        ThePowerValue2 = Frame.ResultValue5[i, Frame.AnalisesSecNumbers - (j + 1)];
                        farkV12 = ThePowerValue2 - ThePowerValue1;
                        farkD12 = TheDistanceValue2 - TheDistanceValue1;
                        farkD = disancValue - TheDistanceValue1;
                        farkV = farkV12 * farkD / farkD12;
                        ThePowerValue = ThePowerValue1 + farkV;
                        textBox4.Text = Math.Round(ThePowerValue, 0).ToString();//m22

                        ThePowerValue1 = Frame.ResultValue3[i, Frame.AnalisesSecNumbers - j];
                        ThePowerValue2 = Frame.ResultValue3[i, Frame.AnalisesSecNumbers - (j + 1)];
                        farkV12 = ThePowerValue2 - ThePowerValue1;
                        farkD12 = TheDistanceValue2 - TheDistanceValue1;
                        farkD = disancValue - TheDistanceValue1;
                        farkV = farkV12 * farkD / farkD12;
                        ThePowerValue = ThePowerValue1 + farkV;
                        textBox5.Text = Math.Round(ThePowerValue, 0).ToString();//m33

                        ThePowerValue1 = Frame.ResultValue6[i, Frame.AnalisesSecNumbers - j];
                        ThePowerValue2 = Frame.ResultValue6[i, Frame.AnalisesSecNumbers - (j + 1)];
                        farkV12 = ThePowerValue2 - ThePowerValue1;
                        farkD12 = TheDistanceValue2 - TheDistanceValue1;
                        farkD = disancValue - TheDistanceValue1;
                        farkV = farkV12 * farkD / farkD12;
                        ThePowerValue = ThePowerValue1 + farkV;
                        textBox6.Text = Math.Round(ThePowerValue, 0).ToString();//torsion

                        ThePowerValue1 = Frame.ResultValue7[i, Frame.AnalisesSecNumbers - j];
                        ThePowerValue2 = Frame.ResultValue7[i, Frame.AnalisesSecNumbers - (j + 1)];
                        farkV12 = ThePowerValue2 - ThePowerValue1;
                        farkD12 = TheDistanceValue2 - TheDistanceValue1;
                        farkD = disancValue - TheDistanceValue1;
                        farkV = farkV12 * farkD / farkD12;
                        ThePowerValue = ThePowerValue1 + farkV;
                        textBox7.Text = Math.Round(ThePowerValue, 6).ToString();//u1

                        ThePowerValue1 = Frame.ResultValue8[i, Frame.AnalisesSecNumbers - j];
                        ThePowerValue2 = Frame.ResultValue8[i, Frame.AnalisesSecNumbers - (j + 1)];
                        farkV12 = ThePowerValue2 - ThePowerValue1;
                        farkD12 = TheDistanceValue2 - TheDistanceValue1;
                        farkD = disancValue - TheDistanceValue1;
                        farkV = farkV12 * farkD / farkD12;
                        ThePowerValue = ThePowerValue1 + farkV;
                        textBox8.Text = Math.Round(ThePowerValue, 6).ToString();//u2
                        break;
                    }
                    #endregion
                }
            }
            #endregion
        }
        
        private double Angulo(double x1, double y1, double x2, double y2)
        {
            double degrees;
            if (x2 - x1 == 0)
            {
                if (y2 > y1)
                    degrees = 90;
                else
                    degrees = 270;
            }
            else
            {
                double riseoverrun = (double)(y2 - y1) / (double)(x2 - x1);
                double radians = Math.Atan(riseoverrun);
                degrees = radians * ((double)180 / Math.PI);
                if ((x2 - x1) < 0 || (y2 - y1) < 0)
                    degrees += 180;
                if ((x2 - x1) > 0 && (y2 - y1) < 0)
                    degrees -= 180;
                if (degrees < 0)
                    degrees += 360;
            }

            if (degrees >= 180) degrees = degrees - 180;

            return degrees;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            AnyDiagram = 1;
            groupBox1.Text = "Axial Force P";
            DrawDiagram();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            AnyDiagram = 2;
            groupBox1.Text = "Shear V2";
            DrawDiagram();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            AnyDiagram = 3;
            groupBox1.Text = "Shear V3";
            DrawDiagram();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            AnyDiagram = 4;
            groupBox1.Text = "Moment M2";
            DrawDiagram();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            AnyDiagram = 5;
            groupBox1.Text = "Moment M3";
            DrawDiagram();
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            AnyDiagram = 6;
            groupBox1.Text = "Torsion";
            DrawDiagram();
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            AnyDiagram = 8;
            groupBox1.Text = "U2";
            DrawDiagram();
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            AnyDiagram = 7;
            groupBox1.Text = "U1";
            DrawDiagram();
        }

        private void pictureBox11_MouseMove(object sender, MouseEventArgs e)
        {
            xmove = e.X;
            ymove = e.Y;
            movemouse();
        }


    }
}
