using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.PowerPacks;
using System.Drawing.Drawing2D;
namespace AMSPRO
{
    class DROWcls
    {
        Form mainForm = Application.OpenForms["MainForm"];
        int INTERSECTION = 0;
        double TheX0 = 0;
        double TheY0 = 0;
        int forarias = 0;
        double Distance = 0;
        public void CalculateIntersectionPoint()
        {
            IntersectionPoint.Number2d = 0;
            double X1 = 0;
            double Y1 = 0;
            double X2 = 0;
            double Y2 = 0;
            double X3 = 0;
            double Y3 = 0;
            double X4 = 0;
            double Y4 = 0;
            for (int i = 1; i < Frame.Number + 1; i++)
            {
                if (Joint.ZReal[((MainForm)mainForm).FrameElement[i].FirstJoint] == Myglobals.StoryLevel[Myglobals.SelectedStory] & Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint] == Myglobals.StoryLevel[Myglobals.SelectedStory])
                {
                    for (int j = i + 1; j < Frame.Number + 1; j++)
                    {
                        if (Joint.ZReal[((MainForm)mainForm).FrameElement[j].FirstJoint] == Myglobals.StoryLevel[Myglobals.SelectedStory] & Joint.ZReal[((MainForm)mainForm).FrameElement[j].SecondJoint] == Myglobals.StoryLevel[Myglobals.SelectedStory])
                        {
                            X1 = Joint.XReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                            Y1 = Joint.YReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                            X2 = Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                            Y2 = Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                            X3 = Joint.XReal[((MainForm)mainForm).FrameElement[j].FirstJoint];
                            Y3 = Joint.YReal[((MainForm)mainForm).FrameElement[j].FirstJoint];
                            X4 = Joint.XReal[((MainForm)mainForm).FrameElement[j].SecondJoint];
                            Y4 = Joint.YReal[((MainForm)mainForm).FrameElement[j].SecondJoint];
                            checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                            if (INTERSECTION == 1)
                            {
                                IntersectionPoint.Number2d = IntersectionPoint.Number2d + 1;
                                IntersectionPoint.XReal[IntersectionPoint.Number2d] = TheX0;
                                IntersectionPoint.YReal[IntersectionPoint.Number2d] = TheY0;
                                IntersectionPoint.ZReal[IntersectionPoint.Number2d] = Myglobals.StoryLevel[Myglobals.SelectedStory];
                            }
                        }
                    }
                    for (int j = 1; j < Shell.Number + 1; j++)
                    {
                        if (Shell.Type[j] == 1 & Joint.ZReal[Shell.JointNo[j, 1]] == Myglobals.StoryLevel[Myglobals.SelectedStory])
                        {
                            for (int k = 1; k < Shell.PointNumbers[j] + 1; k++)
                            {

                                X1 = Joint.XReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                                Y1 = Joint.YReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                                X2 = Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                                Y2 = Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint];

                                X3 = Joint.XReal[Shell.JointNo[j, k]];
                                Y3 = Joint.YReal[Shell.JointNo[j, k]];
                                if (k != Shell.PointNumbers[j])
                                {
                                    X4 = Joint.XReal[Shell.JointNo[j, k+1]];
                                    Y4 = Joint.YReal[Shell.JointNo[j, k+1]];
                                }
                                else
                                {
                                    X4 = Joint.XReal[Shell.JointNo[j, 1]];
                                    Y4 = Joint.YReal[Shell.JointNo[j, 1]];
                                }
                                checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                                if (INTERSECTION == 1)
                                {
                                    IntersectionPoint.Number2d = IntersectionPoint.Number2d + 1;
                                    IntersectionPoint.XReal[IntersectionPoint.Number2d] = TheX0;
                                    IntersectionPoint.YReal[IntersectionPoint.Number2d] = TheY0;
                                    IntersectionPoint.ZReal[IntersectionPoint.Number2d] = Myglobals.StoryLevel[Myglobals.SelectedStory];
                                }
                            }
                        }
                    }
                }
            }
            for (int i = 1; i < IntersectionPoint.Number2d + 1; i++)
            {
                IntersectionPoint.X2d[i] = Myglobals.startX2d + Convert.ToInt32((IntersectionPoint.XReal[i]) * Myglobals.Zoom2d);
                IntersectionPoint.Y2d[i] = Myglobals.startY2d - Convert.ToInt32((IntersectionPoint.YReal[i]) * Myglobals.Zoom2d);
            }
        }
        public void PlaneAria2d()
        {
            Bitmap finalBmp = new Bitmap(Myglobals.BitampWidth2d, Myglobals.BitampHight2d);
            if (((MainForm)mainForm).pictureBox1.Image != null)
            {
                ((MainForm)mainForm).pictureBox1.Image.Dispose();
                ((MainForm)mainForm).pictureBox1.Image = null;
            }
            ((MainForm)mainForm).pictureBox1.Image = finalBmp;
        }
        public void PlaneAria3d()
        {
            #region//تعريفات
            double tx1 = 0;
            double ty1 = 0;
            double tz1 = 0;
            int allgridsline = GridLine.OnX + GridLine.OnY + GridLine.OnXY;
            double MAXx = -10000000;
            double MINx = 10000000;
            double MAXy = -10000000;
            double MINy = 10000000;
            double MAXz = -10000000;
            double MINz = 10000000;
            #endregion
            #region//حساب مركز التدوير

            for (int i = 1; i < Joint.Number3d + 1; i++)
            {
                tx1 = Joint.XReal[i];
                ty1 = Joint.YReal[i];
                tz1 = Joint.ZReal[i];
                if (tx1 > MAXx) MAXx = tx1;
                if (ty1 > MAXy) MAXy = ty1;
                if (tz1 > MAXz) MAXz = tz1;
                if (tx1 < MINx) MINx = tx1;
                if (ty1 < MINy) MINy = ty1;
                if (tz1 < MINz) MINz = tz1;

            }
            for (int i = 1; i < GridPoint.Number3d + 1; i++)
            {
                tx1 = GridPoint.XReal[i];
                ty1 = GridPoint.YReal[i];
                tz1 = GridPoint.ZReal[i];
                if (tx1 > MAXx) MAXx = tx1;
                if (ty1 > MAXy) MAXy = ty1;
                if (tz1 > MAXz) MAXz = tz1;
                if (tx1 < MINx) MINx = tx1;
                if (ty1 < MINy) MINy = ty1;
                if (tz1 < MINz) MINz = tz1;
            }
            
            Myglobals.RotatePointX3d = (MAXx + MINx) / 2;
            Myglobals.RotatePointY3d =  (MAXy + MINy) / 2;
            Myglobals.RotatePointZ3d = (MAXz + MINz) / 2;

            #endregion
            Bitmap finalBmp = new Bitmap(Myglobals.BitampWidth3d, Myglobals.BitampHight3d);
            if (((MainForm)mainForm).pictureBox2.Image != null)
            {
                ((MainForm)mainForm).pictureBox2.Image.Dispose();
                ((MainForm)mainForm).pictureBox2.Image = null;

            }
            ((MainForm)mainForm).pictureBox2.Image = finalBmp;
        }
        public void PlaneAriaelev()
        {
            Bitmap finalBmp = new Bitmap(Myglobals.BitampWidthelev, Myglobals.BitampHightelev);
            if (((MainForm)mainForm).pictureBox5.Image != null)
            {
                ((MainForm)mainForm).pictureBox5.Image.Dispose();
                ((MainForm)mainForm).pictureBox5.Image = null;
            }
            ((MainForm)mainForm).pictureBox5.Image = finalBmp;
        }
        public void CalculateJoint2d()
        {
            for (int i = 1; i < Joint.Number2d + 1; i++)
            {
                Joint.X2d[i] = Myglobals.startX2d + Convert.ToInt32((Joint.XReal[i]) * Myglobals.Zoom2d);
                Joint.Y2d[i] = Myglobals.startY2d - Convert.ToInt32((Joint.YReal[i]) * Myglobals.Zoom2d);
            }
        }
        public void CalculateJointelev()
        {
            Joint.Numberelev = Joint.Number2d;
            for (int i = 1; i < Joint.Numberelev + 1; i++)
            {
                double XReal1 = Myglobals.elevPointX[Myglobals.Selectedelev, 1];
                double YReal1 = Myglobals.elevPointY[Myglobals.Selectedelev, 1];
                double XReal2 = Joint.XReal[i];
                double YReal2 = Joint.YReal[i];
                double length = Math.Sqrt(Math.Pow(XReal1 - XReal2, 2) + Math.Pow(YReal1 - YReal2, 2));
                double ALFA = (double)Math.Round(Angulo(XReal1, YReal1, XReal2, YReal2), 2);
                ALFA = ALFA * Math.PI  / 180;
                double shara = Math.Cos(ALFA) / Math.Abs(Math.Cos(ALFA));
                length = length * shara;
                Joint.Xelev[i] = Myglobals.startXelev + Convert.ToInt32((length) * Myglobals.Zoomelev);
                Joint.Yelev[i] = Myglobals.startYelev - Convert.ToInt32((Joint.ZReal[i]) * Myglobals.Zoomelev);
            }
        }

        public void CalculateJoint3d()
        {
            Joint.Number3d = Joint.Number2d;
            for (int i = 1; i < Joint.Number3d + 1; i++)
            {
                double tx1 = Joint.XReal[i] ;
                double ty1 = -1 * Joint.YReal[i] ;
                double tz1 = -1 * Joint.ZReal[i] ;
                Math3DP.PointG Joint3dM = new Math3DP.PointG(tx1, ty1, tz1);
                Joint3dM.RotateX = Myglobals.tXValue;
                Joint3dM.RotateY = Myglobals.tYValue;
                Joint3dM.RotateZ = Myglobals.tZValue;
                Joint3dM.DrawPoint();
                Joint.X3d[i] = Myglobals.TheX3d;
                Joint.Y3d[i] = Myglobals.TheY3d;
            }
        }
        public void DrawJoint2d()
        {
            Graphics g = Graphics.FromImage(((MainForm)mainForm).pictureBox1.Image);
            Pen pen = new Pen(Color.Blue, 1.3f);
            Font drawFont = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            #region//تحديد العقد
            pen = new Pen(Color.Blue, 1f);
            for (int i = 1; i < Joint.Number2d + 1; i++)
            {
                if (Joint.Assignments == 1 & Joint.ZReal[i] == Myglobals.StoryLevel[Myglobals.SelectedStory])
                {
                    int tx1 = Joint.X2d[i];
                    int ty1 = Joint.Y2d[i];
                    g.DrawString(i.ToString(), drawFont, drawBrush, tx1 - 15, ty1 - 15);
                }
                if (Joint.Selected[i] == 1 & Joint.ZReal[i] == Myglobals.StoryLevel[Myglobals.SelectedStory])
                {
                    int tx1 = Joint.X2d[i] - 4;
                    int ty1 = Joint.Y2d[i] - 4;
                    int tx2 = Joint.X2d[i] + 4;
                    int ty2 = Joint.Y2d[i] + 4;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx1 = Joint.X2d[i] + 4;
                    ty1 = Joint.Y2d[i] - 4;
                    tx2 = Joint.X2d[i] - 4;
                    ty2 = Joint.Y2d[i] + 4;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                }
            }
            #endregion
            g.Dispose();
        }
        public void DrawJointelev()
        {
            double tx1D = Myglobals.elevPointX[Myglobals.Selectedelev, 1];
            double ty1D = Myglobals.elevPointY[Myglobals.Selectedelev, 1];
            double tz1D = Myglobals.elevPointZ[Myglobals.Selectedelev, 1];
            double tx2D = Myglobals.elevPointX[Myglobals.Selectedelev, 1 + Myglobals.StoryNumbers];
            double ty2D = Myglobals.elevPointY[Myglobals.Selectedelev, 1 + Myglobals.StoryNumbers];
            double tz2D = Myglobals.elevPointZ[Myglobals.Selectedelev, 1 + Myglobals.StoryNumbers];
            double tx3D = Myglobals.elevPointX[Myglobals.Selectedelev, 2 + Myglobals.StoryNumbers];
            double ty3D = Myglobals.elevPointY[Myglobals.Selectedelev, 2 + Myglobals.StoryNumbers];
            double tz3D = Myglobals.elevPointZ[Myglobals.Selectedelev, 2 + Myglobals.StoryNumbers];
            double var2 = tx2D - tx1D;
            double var3 = tx3D - tx1D;
            double var5 = ty2D - ty1D;
            double var6 = ty3D - ty1D;
            double var8 = tz2D - tz1D;
            double var9 = tz3D - tz1D;
            double varA = var5 * var9 - var8 * var6;//a
            double varB = var8 * var3 - var2 * var9;//b
            double varC = var2 * var6 - var5 * var3;//c
            double varD = -varA * tx1D - varB * ty1D - varC * tz1D;//d  
            Graphics g = Graphics.FromImage(((MainForm)mainForm).pictureBox5.Image);
            Pen pen = new Pen(Color.Blue, 1.3f);
            Font drawFont = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            #region//تحديد العقد
            pen = new Pen(Color.Blue, 1f);
            for (int i = 1; i < Joint.Number2d + 1; i++)
            {
                int tah = 0;
                double xx = varA * Joint.XReal[i];
                double yy = varB * Joint.YReal[i];
                double zz = varC * Joint.ZReal[i];
                double sm = xx + yy + zz;
                if (Math.Abs(sm + varD) <= 0.5) tah = 1;
                if (tah == 1)
                {
                    if (Joint.Assignments == 1)
                    {
                        int tx1 = Joint.Xelev[i];
                        int ty1 = Joint.Yelev[i];
                        g.DrawString(i.ToString(), drawFont, drawBrush, tx1 - 15, ty1 - 15);
                    }
                    if (Joint.Selected[i] == 1)
                    {
                        int tx1 = Joint.Xelev[i] - 4;
                        int ty1 = Joint.Yelev[i] - 4;
                        int tx2 = Joint.Xelev[i] + 4;
                        int ty2 = Joint.Yelev[i] + 4;
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        tx1 = Joint.Xelev[i] + 4;
                        ty1 = Joint.Yelev[i] - 4;
                        tx2 = Joint.Xelev[i] - 4;
                        ty2 = Joint.Yelev[i] + 4;
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    }
                    #region//وثاقة
                    if (Joint.FixX[i] == 1 & Joint.FixY[i] == 1 & Joint.FixZ[i] == 1 & Joint.FixRX[i] == 1 & Joint.FixRY[i] == 1 & Joint.FixRZ[i] == 1)
                    {
                        pen = new Pen(Color.Black, 1f);
                        int tx1 = Joint.Xelev[i] - 6;
                        int ty1 = Joint.Yelev[i] + 2;
                        int tx2 = Joint.Xelev[i] + 6;
                        int ty2 = Joint.Yelev[i] + 2;
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        tx1 = Joint.Xelev[i] - 6;
                        ty1 = Joint.Yelev[i] + 6;
                        tx2 = Joint.Xelev[i] + 6;
                        ty2 = Joint.Yelev[i] + 6;
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        tx1 = Joint.Xelev[i] - 6;
                        ty1 = Joint.Yelev[i] + 2;
                        tx2 = Joint.Xelev[i] - 6;
                        ty2 = Joint.Yelev[i] + 6;
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        tx1 = Joint.Xelev[i] + 6;
                        ty1 = Joint.Yelev[i] + 2;
                        tx2 = Joint.Xelev[i] + 6;
                        ty2 = Joint.Yelev[i] + 6;
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        tx1 = Joint.Xelev[i];
                        ty1 = Joint.Yelev[i];
                        tx2 = Joint.Xelev[i];
                        ty2 = Joint.Yelev[i] + 6;
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        goto ENDLOOP;
                    }
                    #endregion
                    #region//مقيد انتقالات
                    if (Joint.FixX[i] == 1 & Joint.FixY[i] == 1 & Joint.FixZ[i] == 1)
                    {
                        pen = new Pen(Color.Black, 1f);
                        int tx1 = Joint.Xelev[i];
                        int ty1 = Joint.Yelev[i] + 2;
                        int tx2 = Joint.Xelev[i] + 8;
                        int ty2 = Joint.Yelev[i] + 8;
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        tx1 = Joint.Xelev[i];
                        ty1 = Joint.Yelev[i] + 2;
                        tx2 = Joint.Xelev[i] - 8;
                        ty2 = Joint.Yelev[i] + 8;
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        tx1 = Joint.Xelev[i] - 8;
                        ty1 = Joint.Yelev[i] + 8;
                        tx2 = Joint.Xelev[i] + 8;
                        ty2 = Joint.Yelev[i] + 8;
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        tx1 = Joint.Xelev[i];
                        ty1 = Joint.Yelev[i];
                        tx2 = Joint.Xelev[i];
                        ty2 = Joint.Yelev[i] + 8;
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        goto ENDLOOP;
                    }
                    #endregion
                    #region//مقيد دورانات
                    if (Joint.FixX[i] == 0 & Joint.FixY[i] == 0 & Joint.FixZ[i] == 0 & Joint.FixRX[i] == 1 & Joint.FixRY[i] == 1 & Joint.FixRZ[i] == 1)
                    {
                        pen = new Pen(Color.Black, 1f);
                        int tx1 = Joint.Xelev[i] - 5;
                        int ty1 = Joint.Yelev[i] + 2;
                        int tx2 = Joint.Xelev[i] + 5;
                        int ty2 = Joint.Yelev[i] + 2;
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        pen = new Pen(Color.Black, 1f);
                        tx1 = Joint.Xelev[i] - 8;
                        ty1 = Joint.Yelev[i] + 5;
                        tx2 = Joint.Xelev[i] + 8;
                        ty2 = Joint.Yelev[i] + 5;
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        tx1 = Joint.Xelev[i];
                        ty1 = Joint.Yelev[i];
                        tx2 = Joint.Xelev[i];
                        ty2 = Joint.Yelev[i] + 5;
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        goto ENDLOOP;
                    }
                    #endregion
                    #region
                    if (Joint.FixX[i] == 1 || Joint.FixY[i] == 1 || Joint.FixZ[i] == 1 || Joint.FixRX[i] == 1 || Joint.FixRY[i] == 1 || Joint.FixRZ[i] == 1)
                    {
                        pen = new Pen(Color.Black, 1f);
                        int tx1 = Joint.Xelev[i] - 4;
                        int ty1 = Joint.Yelev[i] - 4;
                        int tx2 = Joint.Xelev[i] + 4;
                        int ty2 = Joint.Yelev[i] + 4;
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        tx1 = Joint.Xelev[i] + 4;
                        ty1 = Joint.Yelev[i] - 4;
                        tx2 = Joint.Xelev[i] - 4;
                        ty2 = Joint.Yelev[i] + 4;
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        goto ENDLOOP;
                    }
                    #endregion
                ENDLOOP: { }
                }
            }
            #endregion
            g.Dispose();
        }
        public void DrawJoint3d()
        {
            Graphics g = Graphics.FromImage(((MainForm)mainForm).pictureBox2.Image);
            Pen pen = new Pen(Color.Red, 2f);
            int tx1 = 0;
            int ty1 = 0;
            int tx2 = 0;
            int ty2 = 0;
            double tx2R = 0;
            double ty2R = 0;
            double tz2R = 0;
            Font drawFont = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            for (int i = 1; i < Joint.Number3d + 1; i++)
            {
                if (Joint.Assignments == 1)
                {
                    tx1 = Joint.X3d[i];
                    ty1 = Joint.Y3d[i];
                    g.DrawString(i.ToString(), drawFont, drawBrush, tx1 - 10, ty1 - 15);
                }
                if (Joint.ShowPower == 1)
                {
                    pen = new Pen(Color.Red, 2f);
                    #region//القوى X
                    if (Joint.PowerX[i] != 0)
                    {
                        tx1 = Joint.X3d[i];
                        ty1 = Joint.Y3d[i];
                        if (Frame.ShowPowerValue == 1) g.DrawString(Joint.PowerX[i].ToString(), drawFont, drawBrush, tx1 - 10, ty1 - 20);
                        if (Joint.PowerX[i] > 0)
                        {
                            tx2R = (Joint.XReal[i] - 1.5) ;
                            ty2R = -1 * Joint.YReal[i] ;
                            tz2R = -1 * Joint.ZReal[i] ;
                            Math3DP.PointG Point3dM = new Math3DP.PointG(tx2R, ty2R, tz2R);
                            Point3dM.RotateX = Myglobals.tXValue;
                            Point3dM.RotateY = Myglobals.tYValue;
                            Point3dM.RotateZ = Myglobals.tZValue;
                            Point3dM.DrawPoint();
                            tx2 = Myglobals.TheX3d;
                            ty2 = Myglobals.TheY3d;
                            g.DrawLine(pen, tx1, ty1, tx2, ty2);

                            tx2R = (Joint.XReal[i] - 0.2) ;
                            ty2R = -1 * (Joint.YReal[i] - 0.2) ;
                            tz2R = -1 * Joint.ZReal[i] ;
                            Point3dM = new Math3DP.PointG(tx2R, ty2R, tz2R);
                            Point3dM.RotateX = Myglobals.tXValue;
                            Point3dM.RotateY = Myglobals.tYValue;
                            Point3dM.RotateZ = Myglobals.tZValue;
                            Point3dM.DrawPoint();
                            tx2 = Myglobals.TheX3d;
                            ty2 = Myglobals.TheY3d;
                            g.DrawLine(pen, tx1, ty1, tx2, ty2);

                            tx2R = (Joint.XReal[i] - 0.2) ;
                            ty2R = -1 * (Joint.YReal[i] + 0.2) ;
                            tz2R = -1 * Joint.ZReal[i] ;
                            Point3dM = new Math3DP.PointG(tx2R, ty2R, tz2R);
                            Point3dM.RotateX = Myglobals.tXValue;
                            Point3dM.RotateY = Myglobals.tYValue;
                            Point3dM.RotateZ = Myglobals.tZValue;
                            Point3dM.DrawPoint();
                            tx2 = Myglobals.TheX3d;
                            ty2 = Myglobals.TheY3d;
                            g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        }
                        else
                        {
                            tx2R = (Joint.XReal[i] + 1.5) ;
                            ty2R = -1 * Joint.YReal[i] ;
                            tz2R = -1 * Joint.ZReal[i] ;
                            Math3DP.PointG Point3dM = new Math3DP.PointG(tx2R, ty2R, tz2R);
                            Point3dM.RotateX = Myglobals.tXValue;
                            Point3dM.RotateY = Myglobals.tYValue;
                            Point3dM.RotateZ = Myglobals.tZValue;
                            Point3dM.DrawPoint();
                            tx2 = Myglobals.TheX3d;
                            ty2 = Myglobals.TheY3d;
                            g.DrawLine(pen, tx1, ty1, tx2, ty2);

                            tx2R = (Joint.XReal[i] + 0.2) ;
                            ty2R = -1 * (Joint.YReal[i] - 0.2) ;
                            tz2R = -1 * Joint.ZReal[i] ;
                            Point3dM = new Math3DP.PointG(tx2R, ty2R, tz2R);
                            Point3dM.RotateX = Myglobals.tXValue;
                            Point3dM.RotateY = Myglobals.tYValue;
                            Point3dM.RotateZ = Myglobals.tZValue;
                            Point3dM.DrawPoint();
                            tx2 = Myglobals.TheX3d;
                            ty2 = Myglobals.TheY3d;
                            g.DrawLine(pen, tx1, ty1, tx2, ty2);

                            tx2R = (Joint.XReal[i] + 0.2) ;
                            ty2R = -1 * (Joint.YReal[i] + 0.2) ;
                            tz2R = -1 * Joint.ZReal[i] ;
                            Point3dM = new Math3DP.PointG(tx2R, ty2R, tz2R);
                            Point3dM.RotateX = Myglobals.tXValue;
                            Point3dM.RotateY = Myglobals.tYValue;
                            Point3dM.RotateZ = Myglobals.tZValue;
                            Point3dM.DrawPoint();
                            tx2 = Myglobals.TheX3d;
                            ty2 = Myglobals.TheY3d;
                            g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        }
                    }
                    #endregion
                    #region//القوى Y
                    if (Joint.PowerY[i] != 0)
                    {
                        tx1 = Joint.X3d[i];
                        ty1 = Joint.Y3d[i];
                        if (Frame.ShowPowerValue == 1) g.DrawString(Joint.PowerY[i].ToString(), drawFont, drawBrush, tx1 + 20, ty1 - 10);
                        if (Joint.PowerY[i] > 0)
                        {
                            tx2R = (Joint.XReal[i]) ;
                            ty2R = -1 * (Joint.YReal[i] - 1.5) ;
                            tz2R = -1 * Joint.ZReal[i] ;
                            Math3DP.PointG Point3dM = new Math3DP.PointG(tx2R, ty2R, tz2R);
                            Point3dM.RotateX = Myglobals.tXValue;
                            Point3dM.RotateY = Myglobals.tYValue;
                            Point3dM.RotateZ = Myglobals.tZValue;
                            Point3dM.DrawPoint();
                            tx2 = Myglobals.TheX3d;
                            ty2 = Myglobals.TheY3d;
                            g.DrawLine(pen, tx1, ty1, tx2, ty2);

                            tx2R = (Joint.XReal[i] - 0.2) ;
                            ty2R = -1 * (Joint.YReal[i] - 0.2) ;
                            tz2R = -1 * (Joint.ZReal[i]) ;
                            Point3dM = new Math3DP.PointG(tx2R, ty2R, tz2R);
                            Point3dM.RotateX = Myglobals.tXValue;
                            Point3dM.RotateY = Myglobals.tYValue;
                            Point3dM.RotateZ = Myglobals.tZValue;
                            Point3dM.DrawPoint();
                            tx2 = Myglobals.TheX3d;
                            ty2 = Myglobals.TheY3d;
                            g.DrawLine(pen, tx1, ty1, tx2, ty2);

                            tx2R = (Joint.XReal[i] + 0.2) ;
                            ty2R = -1 * (Joint.YReal[i] - 0.2) ;
                            tz2R = -1 * Joint.ZReal[i] ;
                            Point3dM = new Math3DP.PointG(tx2R, ty2R, tz2R);
                            Point3dM.RotateX = Myglobals.tXValue;
                            Point3dM.RotateY = Myglobals.tYValue;
                            Point3dM.RotateZ = Myglobals.tZValue;
                            Point3dM.DrawPoint();
                            tx2 = Myglobals.TheX3d;
                            ty2 = Myglobals.TheY3d;
                            g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        }
                        else
                        {
                            tx2R = (Joint.XReal[i]) ;
                            ty2R = -1 * (Joint.YReal[i] + 1.5) ;
                            tz2R = -1 * Joint.ZReal[i] ;
                            Math3DP.PointG Point3dM = new Math3DP.PointG(tx2R, ty2R, tz2R);
                            Point3dM.RotateX = Myglobals.tXValue;
                            Point3dM.RotateY = Myglobals.tYValue;
                            Point3dM.RotateZ = Myglobals.tZValue;
                            Point3dM.DrawPoint();
                            tx2 = Myglobals.TheX3d;
                            ty2 = Myglobals.TheY3d;
                            g.DrawLine(pen, tx1, ty1, tx2, ty2);

                            tx2R = (Joint.XReal[i] - 0.2) ;
                            ty2R = -1 * (Joint.YReal[i] + 0.2) ;
                            tz2R = -1 * Joint.ZReal[i] ;
                            Point3dM = new Math3DP.PointG(tx2R, ty2R, tz2R);
                            Point3dM.RotateX = Myglobals.tXValue;
                            Point3dM.RotateY = Myglobals.tYValue;
                            Point3dM.RotateZ = Myglobals.tZValue;
                            Point3dM.DrawPoint();
                            tx2 = Myglobals.TheX3d;
                            ty2 = Myglobals.TheY3d;
                            g.DrawLine(pen, tx1, ty1, tx2, ty2);

                            tx2R = (Joint.XReal[i] + 0.2) ;
                            ty2R = -1 * (Joint.YReal[i] + 0.2) ;
                            tz2R = -1 * Joint.ZReal[i] ;
                            Point3dM = new Math3DP.PointG(tx2R, ty2R, tz2R);
                            Point3dM.RotateX = Myglobals.tXValue;
                            Point3dM.RotateY = Myglobals.tYValue;
                            Point3dM.RotateZ = Myglobals.tZValue;
                            Point3dM.DrawPoint();
                            tx2 = Myglobals.TheX3d;
                            ty2 = Myglobals.TheY3d;
                            g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        }
                    }
                    #endregion
                    #region//القوى Z
                    if (Joint.PowerZ[i] != 0)
                    {
                        tx1 = Joint.X3d[i];
                        ty1 = Joint.Y3d[i];
                        if (Frame.ShowPowerValue == 1) g.DrawString(Joint.PowerZ[i].ToString(), drawFont, drawBrush, tx1 - 20, ty1 + 10);
                        if (Joint.PowerZ[i] > 0)
                        {
                            tx2R = (Joint.XReal[i]) ;
                            ty2R = -1 * (Joint.YReal[i]) ;
                            tz2R = -1 * (Joint.ZReal[i] - 1.5) ;
                            Math3DP.PointG Point3dM = new Math3DP.PointG(tx2R, ty2R, tz2R);
                            Point3dM.RotateX = Myglobals.tXValue;
                            Point3dM.RotateY = Myglobals.tYValue;
                            Point3dM.RotateZ = Myglobals.tZValue;
                            Point3dM.DrawPoint();
                            tx2 = Myglobals.TheX3d;
                            ty2 = Myglobals.TheY3d;
                            g.DrawLine(pen, tx1, ty1, tx2, ty2);

                            tx2R = (Joint.XReal[i] - 0.2) ;
                            ty2R = -1 * (Joint.YReal[i]) ;
                            tz2R = -1 * (Joint.ZReal[i] - 0.2) ;
                            Point3dM = new Math3DP.PointG(tx2R, ty2R, tz2R);
                            Point3dM.RotateX = Myglobals.tXValue;
                            Point3dM.RotateY = Myglobals.tYValue;
                            Point3dM.RotateZ = Myglobals.tZValue;
                            Point3dM.DrawPoint();
                            tx2 = Myglobals.TheX3d;
                            ty2 = Myglobals.TheY3d;
                            g.DrawLine(pen, tx1, ty1, tx2, ty2);

                            tx2R = (Joint.XReal[i] + 0.2) ;
                            ty2R = -1 * (Joint.YReal[i]) ;
                            tz2R = -1 * (Joint.ZReal[i] - 0.2) ;
                            Point3dM = new Math3DP.PointG(tx2R, ty2R, tz2R);
                            Point3dM.RotateX = Myglobals.tXValue;
                            Point3dM.RotateY = Myglobals.tYValue;
                            Point3dM.RotateZ = Myglobals.tZValue;
                            Point3dM.DrawPoint();
                            tx2 = Myglobals.TheX3d;
                            ty2 = Myglobals.TheY3d;
                            g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        }
                        else
                        {
                            tx2R = (Joint.XReal[i]) ;
                            ty2R = -1 * (Joint.YReal[i]) ;
                            tz2R = -1 * (Joint.ZReal[i] + 1.5) ;
                            Math3DP.PointG Point3dM = new Math3DP.PointG(tx2R, ty2R, tz2R);
                            Point3dM.RotateX = Myglobals.tXValue;
                            Point3dM.RotateY = Myglobals.tYValue;
                            Point3dM.RotateZ = Myglobals.tZValue;
                            Point3dM.DrawPoint();
                            tx2 = Myglobals.TheX3d;
                            ty2 = Myglobals.TheY3d;
                            g.DrawLine(pen, tx1, ty1, tx2, ty2);

                            tx2R = (Joint.XReal[i] - 0.2) ;
                            ty2R = -1 * (Joint.YReal[i]) ;
                            tz2R = -1 * (Joint.ZReal[i] + 0.2) ;
                            Point3dM = new Math3DP.PointG(tx2R, ty2R, tz2R);
                            Point3dM.RotateX = Myglobals.tXValue;
                            Point3dM.RotateY = Myglobals.tYValue;
                            Point3dM.RotateZ = Myglobals.tZValue;
                            Point3dM.DrawPoint();
                            tx2 = Myglobals.TheX3d;
                            ty2 = Myglobals.TheY3d;
                            g.DrawLine(pen, tx1, ty1, tx2, ty2);

                            tx2R = (Joint.XReal[i] + 0.2) ;
                            ty2R = -1 * (Joint.YReal[i]) ;
                            tz2R = -1 * (Joint.ZReal[i] + 0.2) ;
                            Point3dM = new Math3DP.PointG(tx2R, ty2R, tz2R);
                            Point3dM.RotateX = Myglobals.tXValue;
                            Point3dM.RotateY = Myglobals.tYValue;
                            Point3dM.RotateZ = Myglobals.tZValue;
                            Point3dM.DrawPoint();
                            tx2 = Myglobals.TheX3d;
                            ty2 = Myglobals.TheY3d;
                            g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        }
                    }
                    #endregion
                }
                #region//تحديد العقد
                pen = new Pen(Color.Blue, 1f);
                if (Joint.Selected[i] == 1)
                {
                    pen = new Pen(Color.Blue, 1f);
                    tx1 = Joint.X3d[i] - 4;
                    ty1 = Joint.Y3d[i] - 4;
                    tx2 = Joint.X3d[i] + 4;
                    ty2 = Joint.Y3d[i] + 4;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx1 = Joint.X3d[i] + 4;
                    ty1 = Joint.Y3d[i] - 4;
                    tx2 = Joint.X3d[i] - 4;
                    ty2 = Joint.Y3d[i] + 4;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                }
                #region//وثاقة
                if (Joint.FixX[i] == 1 & Joint.FixY[i] == 1 & Joint.FixZ[i] == 1 & Joint.FixRX[i] == 1 & Joint.FixRY[i] == 1 & Joint.FixRZ[i] == 1)
                {
                    pen = new Pen(Color.Black, 1f);
                    tx1 = Joint.X3d[i] - 6;
                    ty1 = Joint.Y3d[i] + 2;
                    tx2 = Joint.X3d[i] + 6;
                    ty2 = Joint.Y3d[i] + 2;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx1 = Joint.X3d[i] - 6;
                    ty1 = Joint.Y3d[i] + 6;
                    tx2 = Joint.X3d[i] + 6;
                    ty2 = Joint.Y3d[i] + 6;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx1 = Joint.X3d[i] - 6;
                    ty1 = Joint.Y3d[i] + 2;
                    tx2 = Joint.X3d[i] - 6;
                    ty2 = Joint.Y3d[i] + 6;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx1 = Joint.X3d[i] + 6;
                    ty1 = Joint.Y3d[i] + 2;
                    tx2 = Joint.X3d[i] + 6;
                    ty2 = Joint.Y3d[i] + 6;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx1 = Joint.X3d[i];
                    ty1 = Joint.Y3d[i];
                    tx2 = Joint.X3d[i];
                    ty2 = Joint.Y3d[i] + 6;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    goto ENDLOOP;
                }
                #endregion
                #region//مقيد انتقالات
                if (Joint.FixX[i] == 1 & Joint.FixY[i] == 1 & Joint.FixZ[i] == 1)
                {
                    pen = new Pen(Color.Black, 1f);
                    tx1 = Joint.X3d[i];
                    ty1 = Joint.Y3d[i] + 2;
                    tx2 = Joint.X3d[i] + 8;
                    ty2 = Joint.Y3d[i] + 8;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx1 = Joint.X3d[i];
                    ty1 = Joint.Y3d[i] + 2;
                    tx2 = Joint.X3d[i] - 8;
                    ty2 = Joint.Y3d[i] + 8;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx1 = Joint.X3d[i] - 8;
                    ty1 = Joint.Y3d[i] + 8;
                    tx2 = Joint.X3d[i] + 8;
                    ty2 = Joint.Y3d[i] + 8;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx1 = Joint.X3d[i];
                    ty1 = Joint.Y3d[i];
                    tx2 = Joint.X3d[i];
                    ty2 = Joint.Y3d[i] + 8;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    goto ENDLOOP;
                }
                #endregion
                #region//مقيد دورانات
                if (Joint.FixX[i] == 0 & Joint.FixY[i] == 0 & Joint.FixZ[i] == 0 & Joint.FixRX[i] == 1 & Joint.FixRY[i] == 1 & Joint.FixRZ[i] == 1)
                {
                    pen = new Pen(Color.Black, 1f);
                    tx1 = Joint.X3d[i] - 5;
                    ty1 = Joint.Y3d[i] + 2;
                    tx2 = Joint.X3d[i] + 5;
                    ty2 = Joint.Y3d[i] + 2;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    pen = new Pen(Color.Black, 1f);
                    tx1 = Joint.X3d[i] - 8;
                    ty1 = Joint.Y3d[i] + 5;
                    tx2 = Joint.X3d[i] + 8;
                    ty2 = Joint.Y3d[i] + 5;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx1 = Joint.X3d[i];
                    ty1 = Joint.Y3d[i];
                    tx2 = Joint.X3d[i];
                    ty2 = Joint.Y3d[i] + 5;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    goto ENDLOOP;
                }
                #endregion
                #region
                if (Joint.FixX[i] == 1 || Joint.FixY[i] == 1 || Joint.FixZ[i] == 1 || Joint.FixRX[i] == 1 || Joint.FixRY[i] == 1 || Joint.FixRZ[i] == 1)
                {
                    pen = new Pen(Color.Black, 1f);
                    tx1 = Joint.X3d[i] - 4;
                    ty1 = Joint.Y3d[i] - 4;
                    tx2 = Joint.X3d[i] + 4;
                    ty2 = Joint.Y3d[i] + 4;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx1 = Joint.X3d[i] + 4;
                    ty1 = Joint.Y3d[i] - 4;
                    tx2 = Joint.X3d[i] - 4;
                    ty2 = Joint.Y3d[i] + 4;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    goto ENDLOOP;
                }
                #endregion
            ENDLOOP: { }
                #endregion
            }
            drawFont.Dispose();
            drawBrush.Dispose();
        }
     
        public void CalculateGridPointReal()
        {
            #region 
            int m = 0;
            int el = 0;
            int elPN = 0;
            int n = GridLine.OnY + GridLine.OnX + GridLine.OnXY;
            for (int i = 1; i < n + 1; i++)
            {
                if (GridLine.Visible[i] == 0)
                {
                    el = el + 1;
                    Myglobals.elevGridLine[el] = i;
                    double X1 = GridLine.X1Real[i];
                    double Y1 = GridLine.Y1Real[i];
                    double X2 = GridLine.X2Real[i];
                    double Y2 = GridLine.Y2Real[i];
                    elPN = 0;
                    for (int j = 1; j < n + 1; j++)
                    {
                        if (GridLine.Visible[j] == 0)
                        {
                            double X3 = GridLine.X1Real[j];
                            double Y3 = GridLine.Y1Real[j];
                            double X4 = GridLine.X2Real[j];
                            double Y4 = GridLine.Y2Real[j];
                            checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                            if (INTERSECTION == 0) goto Nextj;
                            elPN = elPN + 1;
                            for (int ADD = 0; ADD < Myglobals.StoryNumbers + 1; ADD++)
                            {
                                int nn = (elPN - 1) * (Myglobals.StoryNumbers + 1) + ADD + 1;
                                Myglobals.elevPointX[el, nn] = TheX0;
                                Myglobals.elevPointY[el, nn] = TheY0;
                                Myglobals.elevPointZ[el, nn] = Myglobals.StoryLevel[ADD];
                                if (ADD != 0)
                                {
                                    Myglobals.elevPointName[el, nn] = GridLine.Name[i] + "  " + GridLine.Name[j] + "  " + "  Story " + ADD;
                                }
                                else
                                {
                                    Myglobals.elevPointName[el, nn] = GridLine.Name[i] + "  " + GridLine.Name[j] + "  " + "  Base ";
                                }
                            }
                            for (int k = 1; k < m + 1; k++)
                            {
                                if (GridPoint.XReal[k] == TheX0 & GridPoint.YReal[k] == TheY0) goto Nextj;
                            }
                            m = m + 1;
                            GridPoint.XReal[m] = TheX0;
                            GridPoint.YReal[m] = TheY0;
                            GridPoint.Name2d[m] = GridLine.Name[i] + "  " + GridLine.Name[j];
                        Nextj: { }
                        }
                    }
                }
                Myglobals.elevPointNumbers[el] = elPN * (Myglobals.StoryNumbers + 1);
            }
            Myglobals.elevNumbers = el;
            int M = 0;
            for (int i = 1; i < n + 1; i++)
            {
                if (GridLine.Visible[i] == 0)
                {
                    M = M + 1;
                    double x0 = (GridLine.X1Real[i]);
                    double y0 = (GridLine.Y1Real[i]);
                    for (int k = 1; k < m + 1; k++)
                    {
                        if (GridPoint.XReal[k] == x0 & GridPoint.YReal[k] == y0) goto Nexti;
                    }

                    int mPOINTS = Myglobals.elevPointNumbers[M];
                    int mSTORYS = Myglobals.StoryNumbers + 1;
                    for (int k = mPOINTS + mSTORYS; k > mSTORYS ; k --)
                    {
                        Myglobals.elevPointX[M, k] = Myglobals.elevPointX[M, k - mSTORYS];
                        Myglobals.elevPointY[M, k] = Myglobals.elevPointY[M, k - mSTORYS];
                        Myglobals.elevPointZ[M, k] = Myglobals.elevPointZ[M, k - mSTORYS];
                        Myglobals.elevPointName[M, k] = Myglobals.elevPointName[M, k - mSTORYS];
                    }
                    elPN = elPN + 1;
                    for (int ADD = 0; ADD < Myglobals.StoryNumbers + 1; ADD++)
                    {
                        Myglobals.elevPointX[M, ADD+1] = x0;
                        Myglobals.elevPointY[M, ADD+1] = y0;
                        Myglobals.elevPointZ[M, ADD+1] = Myglobals.StoryLevel[ADD];
                        if (ADD != 0)
                        {
                            Myglobals.elevPointName[M, ADD+1] = GridLine.Name[i] + "  " + GridLine.Name[i] + "  " + "  Story " + ADD;
                        }
                        else
                        {
                            Myglobals.elevPointName[M, ADD+1] = GridLine.Name[i] + "  " + GridLine.Name[i] + "  " + "  Base ";
                        }
                    }
                    Myglobals.elevPointNumbers[M] = elPN * (Myglobals.StoryNumbers + 1);
                    m = m + 1;
                    GridPoint.XReal[m] = x0;
                    GridPoint.YReal[m] = y0;
                    GridPoint.Name2d[m] = GridLine.Name[i] + "  " + GridLine.Name[i];
                }
            Nexti: { }
            }
            M = 0;
            for (int i = 1; i < n + 1; i++)
            {
                if (GridLine.Visible[i] == 0)
                {
                    M = M + 1;
                    double x0 = (GridLine.X2Real[i]);
                    double y0 = (GridLine.Y2Real[i]);
                    for (int k = 1; k < m + 1; k++)
                    {
                        if (GridPoint.XReal[k] == x0 & GridPoint.YReal[k] == y0) goto Nexti;
                    }
                    elPN = elPN + 1;
                    for (int ADD = 0; ADD < Myglobals.StoryNumbers + 1; ADD++)
                    {
                        int nn = (elPN - 1) * (Myglobals.StoryNumbers + 1) + ADD + 1;
                        Myglobals.elevPointX[M, nn] = x0;
                        Myglobals.elevPointY[M, nn] = y0;
                        Myglobals.elevPointZ[M, nn] = Myglobals.StoryLevel[ADD];
                        if (ADD != 0)
                        {
                            Myglobals.elevPointName[M, nn] = GridLine.Name[i] + "  " + GridLine.Name[i] + "  " + "  Story " + ADD;
                        }
                        else
                        {
                            Myglobals.elevPointName[M, nn] = GridLine.Name[i] + "  " + GridLine.Name[i] + "  " + "  Base ";
                        }
                    }
                    Myglobals.elevPointNumbers[M] = elPN * (Myglobals.StoryNumbers + 1);
                    m = m + 1;
                    GridPoint.XReal[m] = x0;
                    GridPoint.YReal[m] = y0;
                    GridPoint.Name2d[m] = GridLine.Name[i] + "  " + GridLine.Name[i];
                }
            Nexti: { }
            }
            GridPoint.Number2d = m;
            m = 0;
            for (int i = 0; i < Myglobals.StoryNumbers + 1; i++)
            {
                for (int j = 1; j < GridPoint.Number2d + 1; j++)
                {
                    m = m + 1;
                    GridPoint.XReal[m] = GridPoint.XReal[j];
                    GridPoint.YReal[m] = GridPoint.YReal[j];
                    GridPoint.ZReal[m] = Myglobals.StoryLevel[i];
                }
            }
            GridPoint.Number3d = m;
            #endregion
            double MAXX = -1000000;
            double MINX = 1000000;
            double MAXY = -1000000;
            double MINY = 1000000;
            for (int add = 1; add < GridLine.OnX + GridLine.OnY + GridLine.OnXY + 1; add++)
            {
                if (GridLine.X1Real[add] > MAXX) MAXX = GridLine.X1Real[add];
                if (GridLine.X2Real[add] > MAXX) MAXX = GridLine.X2Real[add];
                if (GridLine.X1Real[add] < MINX) MINX = GridLine.X1Real[add];
                if (GridLine.X2Real[add] < MINX) MINX = GridLine.X2Real[add];
                if (GridLine.Y1Real[add] > MAXY) MAXY = GridLine.Y1Real[add];
                if (GridLine.Y2Real[add] > MAXY) MAXY = GridLine.Y2Real[add];
                if (GridLine.Y1Real[add] < MINY) MINY = GridLine.Y1Real[add];
                if (GridLine.Y2Real[add] < MINY) MINY = GridLine.Y2Real[add];
            }
            Myglobals.OutAriaX[1] = MINX - 1;
            Myglobals.OutAriaY[1] = MINY - 1;
            Myglobals.OutAriaZ[1] = 0;
            Myglobals.OutAriaX[2] = MINX - 1;
            Myglobals.OutAriaY[2] = MAXY + 1;
            Myglobals.OutAriaZ[2] = 0;
            Myglobals.OutAriaX[3] = MAXX + 1;
            Myglobals.OutAriaY[3] = MAXY + 1;
            Myglobals.OutAriaZ[3] = 0;
            Myglobals.OutAriaX[4] = MAXX + 1;
            Myglobals.OutAriaY[4] = MINY - 1;
            Myglobals.OutAriaZ[4] = 0;
            forarias = 1;
            FindArias();
            FindArias1();
            FindAriasELEV();
        }
        public void CalculateGridPoint2d()
        {
            for (int i = 1; i < GridPoint.Number2d + 1; i++)
            {
                GridPoint.X2d[i] = Myglobals.startX2d + Convert.ToInt32((GridPoint.XReal[i]) * Myglobals.Zoom2d);
                GridPoint.Y2d[i] = Myglobals.startY2d - Convert.ToInt32((GridPoint.YReal[i]) * Myglobals.Zoom2d);
            }
        }
        public void CalculateGridPointelev()
        {
            for (int i = 1; i < Myglobals.elevPointNumbers[Myglobals.Selectedelev] + 1; i++)
            {
                double XReal1 =  Myglobals.elevPointX[Myglobals.Selectedelev, 1];
                double YReal1 =  Myglobals.elevPointY[Myglobals.Selectedelev, 1];
                double XReal2 = Myglobals.elevPointX[Myglobals.Selectedelev, i];
                double YReal2 = Myglobals.elevPointY[Myglobals.Selectedelev, i];
                double length = Math.Sqrt(Math.Pow(XReal1 - XReal2, 2) + Math.Pow(YReal1 - YReal2, 2));
                GridPoint.Xelev[i] = Myglobals.startXelev + Convert.ToInt32((length) * Myglobals.Zoomelev);
                GridPoint.Yelev[i] = Myglobals.startYelev - Convert.ToInt32((Myglobals.elevPointZ[Myglobals.Selectedelev, i]) * Myglobals.Zoomelev);
            }
        }
        public void CalculateGridPoint3d()
        {
            int m = 0;
            for (int add1 = 0; add1 < Myglobals.StoryNumbers + 1; add1++)
            {
                for (int add = 1; add < GridPoint.Number2d + 1; add++)
                {
                    m = m + 1;
                    double tx1 = GridPoint.XReal[add] ;
                    double ty1 = -1 * GridPoint.YReal[add] ;
                    double tz1 = -1 * Myglobals.StoryLevel[add1] ;
                    Math3DP.PointG GridPoint3dM = new Math3DP.PointG(tx1, ty1, tz1);
                    GridPoint3dM.RotateX = Myglobals.tXValue;
                    GridPoint3dM.RotateY = Myglobals.tYValue;
                    GridPoint3dM.RotateZ = Myglobals.tZValue;
                    GridPoint3dM.DrawPoint();
                    GridPoint.X3d[m] = Myglobals.TheX3d;
                    GridPoint.Y3d[m] = Myglobals.TheY3d;
                    if (add1 == 0)
                    {
                        GridPoint.Name3d[m] = GridPoint.Name2d[add] + "  Base";
                    }
                    else
                    {
                        GridPoint.Name3d[m] = GridPoint.Name2d[add] + "  Story " + add1;
                    }
                }
            }
        }
        public void CalculateGridLine2d()
        {
            for (int i = 1; i < GridLine.OnX + GridLine.OnY + GridLine.OnXY + 1; i++)
            {
                GridLine.X12d[i] = Myglobals.startX2d + Convert.ToInt32((GridLine.X1Real[i]) * Myglobals.Zoom2d);
                GridLine.Y12d[i] = Myglobals.startY2d - Convert.ToInt32((GridLine.Y1Real[i]) * Myglobals.Zoom2d);
                GridLine.X22d[i] = Myglobals.startX2d + Convert.ToInt32((GridLine.X2Real[i]) * Myglobals.Zoom2d);
                GridLine.Y22d[i] = Myglobals.startY2d - Convert.ToInt32((GridLine.Y2Real[i]) * Myglobals.Zoom2d);
            }
        }
        public void CalculateGridLine3d()
        {
            #region//تعريفات
            int add = 0;
            int add1 = 0;
            double tx1 = 0;
            double ty1 = 0;
            double tx2 = 0;
            double ty2 = 0;
            double tz1 = 0;
            double tz2 = 0;
            int allgridsline = GridLine.OnX + GridLine.OnY + GridLine.OnXY;
            GridLine.Number3d = 0;
            #endregion
            #region // محاور الشبكة في مستوي الطابق
            for (add1 = 0; add1 < Myglobals.StoryNumbers + 1; add1++)
            {
                for (add = 1; add < allgridsline + 1; add++)
                {
                    if (GridLine.Visible[add] == 0)
                    {
                        GridLine.Number3d = GridLine.Number3d + 1;
                        tx1 = GridLine.X1Real[add] ;
                        ty1 = -1 * GridLine.Y1Real[add] ;
                        tx2 = GridLine.X2Real[add] ;
                        ty2 = -1 * GridLine.Y2Real[add] ;
                        tz1 = -1 * Myglobals.StoryLevel[add1] ;
                        tz2 = -1 * Myglobals.StoryLevel[add1] ;
                        Math3DP.PointG Joint3dM = new Math3DP.PointG(tx1, ty1, tz1);
                        Joint3dM.RotateX = Myglobals.tXValue;
                        Joint3dM.RotateY = Myglobals.tYValue;
                        Joint3dM.RotateZ = Myglobals.tZValue;
                        Joint3dM.DrawPoint();
                        GridLine.X13d[GridLine.Number3d] = Myglobals.TheX3d;
                        GridLine.Y13d[GridLine.Number3d] = Myglobals.TheY3d;
                        Joint3dM = new Math3DP.PointG(tx2, ty2, tz2);
                        Joint3dM.RotateX = Myglobals.tXValue;
                        Joint3dM.RotateY = Myglobals.tYValue;
                        Joint3dM.RotateZ = Myglobals.tZValue;
                        Joint3dM.DrawPoint();
                        GridLine.X23d[GridLine.Number3d] = Myglobals.TheX3d;
                        GridLine.Y23d[GridLine.Number3d] = Myglobals.TheY3d;
                    }
                }
            }
            #endregion
            #region// محاور الشبكة في الشاقول
            for (add1 = 0; add1 < Myglobals.StoryNumbers; add1++)
            {
                for (add = 1; add < GridPoint.Number2d + 1; add++)
                {
                    GridLine.Number3d = GridLine.Number3d + 1;
                    tx1 = GridPoint.XReal[add] ;
                    ty1 = -1 * GridPoint.YReal[add] ;
                    tx2 = tx1;
                    ty2 = ty1;
                    tz1 = -1 * Myglobals.StoryLevel[add1] ;
                    tz2 = -1 * Myglobals.StoryLevel[add1 + 1] ;
                    Math3DP.PointG Joint3dM = new Math3DP.PointG(tx1, ty1, tz1);
                    Joint3dM.RotateX = Myglobals.tXValue;
                    Joint3dM.RotateY = Myglobals.tYValue;
                    Joint3dM.RotateZ = Myglobals.tZValue;
                    Joint3dM.DrawPoint();
                    GridLine.X13d[GridLine.Number3d] = Myglobals.TheX3d;
                    GridLine.Y13d[GridLine.Number3d] = Myglobals.TheY3d;
                    Joint3dM = new Math3DP.PointG(tx2, ty2, tz2);
                    Joint3dM.RotateX = Myglobals.tXValue;
                    Joint3dM.RotateY = Myglobals.tYValue;
                    Joint3dM.RotateZ = Myglobals.tZValue;
                    Joint3dM.DrawPoint();
                    GridLine.X23d[GridLine.Number3d] = Myglobals.TheX3d;
                    GridLine.Y23d[GridLine.Number3d] = Myglobals.TheY3d;
                }
            }
            #endregion
            #region // خطوط نقطة مبدأ الاحداثيات
            for (add = 1; add < 4; add++)
            {
                GridLine.Number3d = GridLine.Number3d + 1;
                if (add == 1)
                {
                    Math3DP.PointG Joint3dM = new Math3DP.PointG(0, 0, 0);
                    Joint3dM.RotateX = Myglobals.tXValue;
                    Joint3dM.RotateY = Myglobals.tYValue;
                    Joint3dM.RotateZ = Myglobals.tZValue;
                    Joint3dM.DrawPoint();
                    GridLine.X13d[GridLine.Number3d] = Myglobals.TheX3d;
                    GridLine.Y13d[GridLine.Number3d] = Myglobals.TheY3d;
                    Joint3dM = new Math3DP.PointG(2, 0, 0);
                    Joint3dM.RotateX = Myglobals.tXValue;
                    Joint3dM.RotateY = Myglobals.tYValue;
                    Joint3dM.RotateZ = Myglobals.tZValue;
                    Joint3dM.DrawPoint();
                    GridLine.X23d[GridLine.Number3d] = Myglobals.TheX3d;
                    GridLine.Y23d[GridLine.Number3d] = Myglobals.TheY3d;
                }
                if (add == 2)
                {
                    Math3DP.PointG Joint3dM = new Math3DP.PointG(0, 0, 0);
                    Joint3dM.RotateX = Myglobals.tXValue;
                    Joint3dM.RotateY = Myglobals.tYValue;
                    Joint3dM.RotateZ = Myglobals.tZValue;
                    Joint3dM.DrawPoint();
                    GridLine.X13d[GridLine.Number3d] = Myglobals.TheX3d;
                    GridLine.Y13d[GridLine.Number3d] = Myglobals.TheY3d;
                    Joint3dM = new Math3DP.PointG(0, -2, 0);
                    Joint3dM.RotateX = Myglobals.tXValue;
                    Joint3dM.RotateY = Myglobals.tYValue;
                    Joint3dM.RotateZ = Myglobals.tZValue;
                    Joint3dM.DrawPoint();
                    GridLine.X23d[GridLine.Number3d] = Myglobals.TheX3d;
                    GridLine.Y23d[GridLine.Number3d] = Myglobals.TheY3d;
                }
                if (add == 3)
                {
                    Math3DP.PointG Joint3dM = new Math3DP.PointG(0, 0, 0);
                    Joint3dM.RotateX = Myglobals.tXValue;
                    Joint3dM.RotateY = Myglobals.tYValue;
                    Joint3dM.RotateZ = Myglobals.tZValue;
                    Joint3dM.DrawPoint();
                    GridLine.X13d[GridLine.Number3d] = Myglobals.TheX3d;
                    GridLine.Y13d[GridLine.Number3d] = Myglobals.TheY3d;
                    Joint3dM = new Math3DP.PointG(0, 0, -2);
                    Joint3dM.RotateX = Myglobals.tXValue;
                    Joint3dM.RotateY = Myglobals.tYValue;
                    Joint3dM.RotateZ = Myglobals.tZValue;
                    Joint3dM.DrawPoint();
                    GridLine.X23d[GridLine.Number3d] = Myglobals.TheX3d;
                    GridLine.Y23d[GridLine.Number3d] = Myglobals.TheY3d;
                }
            }
            #endregion
        }
        public void DrowGridelev()
        {
            if (Myglobals.ShowGrid == 1 & Myglobals.elevNumbers>0)
            {
                Graphics g = Graphics.FromImage(((MainForm)mainForm).pictureBox5.Image);
                Pen pen = new Pen(Color.LightGray, 1.3f);
                Font drawFont = new Font("Tahoma", 10, FontStyle.Regular, GraphicsUnit.Pixel);
                SolidBrush drawBrush = new SolidBrush(Color.Gray);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                double XReal1 = GridLine.X1Real[Myglobals.elevGridLine[Myglobals.Selectedelev]];
                double YReal1 = GridLine.Y1Real[Myglobals.elevGridLine[Myglobals.Selectedelev]];
                double XReal2 = GridLine.X2Real[Myglobals.elevGridLine[Myglobals.Selectedelev]];
                double YReal2 = GridLine.Y2Real[Myglobals.elevGridLine[Myglobals.Selectedelev]];
                double length = Math.Sqrt(Math.Pow(XReal1 - XReal2, 2) + Math.Pow(YReal1 - YReal2, 2));
                 for (int j = 0; j < Myglobals.StoryNumbers + 1; j++)
                    {
                        int tx1 = Myglobals.startXelev;
                        int ty1 = Myglobals.startYelev - Convert.ToInt32(Myglobals.StoryLevel[j]* Myglobals.Zoomelev);
                        int tx2 = Myglobals.startXelev + Convert.ToInt32((length) * Myglobals.Zoomelev);
                        int ty2 = ty1;
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        g.DrawString(Myglobals.StoryName[j], drawFont, drawBrush, tx2 + 5, ty2 - 7);
                    }
                    for (int j = 1; j < Myglobals.elevNumbers + 1; j++)
                    {
                        if (j != Myglobals.Selectedelev)
                        {
                            double X1 = GridLine.X1Real[Myglobals.elevGridLine[j]];
                            double Y1 = GridLine.Y1Real[Myglobals.elevGridLine[j]];
                            double X2 = GridLine.X2Real[Myglobals.elevGridLine[j]];
                            double Y2 = GridLine.Y2Real[Myglobals.elevGridLine[j]];
                            checkintersection(XReal1, YReal1, XReal2, YReal2, X1, Y1, X2, Y2);
                            if (INTERSECTION == 1)
                            {
                                length = Math.Sqrt(Math.Pow(XReal1 - TheX0, 2) + Math.Pow(YReal1 - TheY0, 2));
                                int tx1 = Myglobals.startXelev + Convert.ToInt32(length * Myglobals.Zoomelev);
                                int ty1 = Myglobals.startYelev ;
                                int tx2 = tx1;
                                int ty2 = ty1 - Convert.ToInt32(Myglobals.StoryLevel[Myglobals.StoryNumbers] * Myglobals.Zoomelev); ;
                                g.DrawLine(pen, tx1, ty1, tx2, ty2);
                                g.DrawString(GridLine.Name[Myglobals.elevGridLine[Myglobals.Selectedelev]], drawFont, drawBrush, tx2 - 5, ty2 - 13);
                                g.DrawString(GridLine.Name[Myglobals.elevGridLine[j]], drawFont, drawBrush, tx2 - 5, ty2 - 23);
                            }
                        }
                    }
                drawFont.Dispose();
                drawBrush.Dispose();
                g.Dispose();
            }
        }
        public void DrowGrid2d()
        {
            if (Myglobals.ShowGrid == 1)
            {
                Graphics g = Graphics.FromImage(((MainForm)mainForm).pictureBox1.Image);
                Pen pen = new Pen(Color.LightGray, 1.3f);
                Font drawFont = new Font("Tahoma", 10, FontStyle.Regular, GraphicsUnit.Pixel);
                SolidBrush drawBrush = new SolidBrush(Color.LightGray);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                #region//تسميات المحاور
                for (int i = 1; i < GridLine.OnX + GridLine.OnY + GridLine.OnXY + 1; i++)
                {
                    if (GridLine.Visible[i] == 0)
                    {
                        int tx1 = GridLine.X12d[i];
                        int ty1 = GridLine.Y12d[i];
                        int tx2 = GridLine.X22d[i];
                        int ty2 = GridLine.Y22d[i];
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        if (tx1 == tx2)
                        {
                            g.DrawEllipse(pen, tx2 - 10, ty2 - 45, 20, 20);
                            g.DrawString(GridLine.Name[i], drawFont, drawBrush, tx2 - 5, ty2 - 40);
                        }
                        else
                        {
                            g.DrawEllipse(pen, tx1 - 45, ty1 - 10, 20, 20);
                            g.DrawString(GridLine.Name[i], drawFont, drawBrush, tx1 - 40, ty1 - 5);
                        }
                    }
                }
                #endregion
                #region//تباعدات اكس
                for (int i = 1; i < GridLine.OnX; i++)
                {
                    int tx1 = (GridLine.X12d[i] + GridLine.X12d[i + 1]) / 2;
                    int ty1 = GridLine.Y22d[i];
                    int tx2 = GridLine.X12d[i + 1];
                    int ty2 = ty1;
                    g.DrawString(GridLine.Distance[i].ToString() + " m", drawFont, drawBrush, tx1 - 10, ty1 - 27);
                    tx1 = GridLine.X12d[i];
                    g.DrawLine(pen, tx1, ty1 - 15, tx2, ty2 - 15);
                }
                for (int i = 1; i < GridLine.OnX + 1; i++)
                {
                    int tx1 = GridLine.X22d[i];
                    int ty1 = GridLine.Y22d[i];
                    int tx2 = GridLine.X22d[i];
                    int ty2 = GridLine.Y22d[i] - 20;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx1 = GridLine.X22d[i] - 4;
                    ty1 = GridLine.Y22d[i] + 4 - 15;
                    tx2 = GridLine.X22d[i] + 4;
                    ty2 = GridLine.Y22d[i] - 4 - 15;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                }
                #endregion
                #region//تباعدات واي
                for (int i = GridLine.OnX + 1; i < GridLine.OnX + GridLine.OnY; i++)
                {
                    int tx1 = GridLine.X12d[i];
                    int ty1 = (GridLine.Y12d[i] + GridLine.Y12d[i + 1]) / 2;
                    int tx2 = tx1;
                    int ty2 = GridLine.Y12d[i + 1];
                    g.DrawString(GridLine.Distance[i].ToString() + " m", drawFont, drawBrush, tx1 - 35, ty1);
                    ty1 = GridLine.Y12d[i];
                    g.DrawLine(pen, tx1 - 15, ty1, tx2 - 15, ty2);
                }
                for (int i = GridLine.OnX + 1; i < GridLine.OnX + GridLine.OnY + 1; i++)
                {
                    int tx1 = GridLine.X12d[i];
                    int ty1 = GridLine.Y12d[i];
                    int tx2 = GridLine.X12d[i] - 20;
                    int ty2 = GridLine.Y12d[i];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx1 = GridLine.X12d[i] + 4 - 15;
                    ty1 = GridLine.Y12d[i] + 4;
                    tx2 = GridLine.X12d[i] - 4 - 15;
                    ty2 = GridLine.Y12d[i] - 4;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                }
                #endregion
                #region//نقطة المبدأ
                drawFont = new Font("Tahoma", 12, FontStyle.Regular, GraphicsUnit.Pixel);
                int Cx1 = Myglobals.startX2d;
                int Cy1 = Myglobals.startY2d;
                int Cx2 = Cx1 + 30;
                int Cy2 = Cy1;
                pen = new Pen(Color.Red, 1f);
                drawBrush = new SolidBrush(Color.Red);
                g.DrawLine(pen, Cx1, Cy1, Cx2, Cy2);
                g.DrawLine(pen, Cx2, Cy2, Cx2 - 5, Cy2 + 3);
                g.DrawLine(pen, Cx2, Cy2, Cx2 - 5, Cy2 - 3);
                g.DrawString("X", drawFont, drawBrush, Cx2 + 3, Cy2 - 7);
                Cx2 = Cx1;
                Cy2 = Cy1 - 30;
                pen = new Pen(Color.Green, 1f);
                drawBrush = new SolidBrush(Color.Green);
                g.DrawLine(pen, Cx1, Cy1, Cx2, Cy2);
                g.DrawLine(pen, Cx2, Cy2, Cx2 - 3, Cy2 + 5);
                g.DrawLine(pen, Cx2, Cy2, Cx2 + 3, Cy2 + 5);
                g.DrawString("Y", drawFont, drawBrush, Cx2 - 4, Cy2 - 14);
                #endregion
                drawFont.Dispose();
                drawBrush.Dispose();
                g.Dispose();
            }
        }
        public void DrawGrid3d()
        {
            if (Myglobals.ShowGrid == 1)
            {
                try
                {
                    Graphics g = Graphics.FromImage(((MainForm)mainForm).pictureBox2.Image);
                    Pen pen = new Pen(Color.LightGray, 1f);
                    Font drawFont = new Font("Tahoma", 10, FontStyle.Regular, GraphicsUnit.Pixel);
                    SolidBrush drawBrush = new SolidBrush(Color.LightGray);
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    int tx1 = 0;
                    int ty1 = 0;
                    int tx2 = 0;
                    int ty2 = 0;
                    for (int i = 1; i < GridLine.Number3d + 1; i++)
                    {
                        tx1 = GridLine.X13d[i];
                        ty1 = GridLine.Y13d[i];
                        tx2 = GridLine.X23d[i];
                        ty2 = GridLine.Y23d[i];
                        if (i < GridLine.Number3d - 2)
                        {
                            pen = new Pen(Color.LightGray, 1f);
                        }
                        #region//X
                        if (i == GridLine.Number3d - 2)
                        {
                            pen = new Pen(Color.Red, 2f);
                            drawBrush = new SolidBrush(Color.Red);
                            Math3DP.PointG Joint3dM = new Math3DP.PointG(1.8, 0, 0.1);
                            Joint3dM.RotateX = Myglobals.tXValue;
                            Joint3dM.RotateY = Myglobals.tYValue;
                            Joint3dM.RotateZ = Myglobals.tZValue;
                            Joint3dM.DrawPoint();
                            int tx3 = Myglobals.TheX3d;
                            int ty3 = Myglobals.TheY3d;
                            g.DrawLine(pen, tx2, ty2, tx3, ty3);
                            Joint3dM = new Math3DP.PointG(1.8, 0, -0.1);
                            Joint3dM.RotateX = Myglobals.tXValue;
                            Joint3dM.RotateY = Myglobals.tYValue;
                            Joint3dM.RotateZ = Myglobals.tZValue;
                            Joint3dM.DrawPoint();
                            tx3 = Myglobals.TheX3d;
                            ty3 = Myglobals.TheY3d;
                            g.DrawLine(pen, tx2, ty2, tx3, ty3);
                            g.DrawString("X", drawFont, drawBrush, tx2, ty2);
                        }
                        #endregion
                        #region//Y
                        if (i == GridLine.Number3d - 1)
                        {
                            pen = new Pen(Color.Green, 2f);
                            drawBrush = new SolidBrush(Color.Green);
                            Math3DP.PointG Joint3dM = new Math3DP.PointG(0, -1.8, 0.1);
                            Joint3dM.RotateX = Myglobals.tXValue;
                            Joint3dM.RotateY = Myglobals.tYValue;
                            Joint3dM.RotateZ = Myglobals.tZValue;
                            Joint3dM.DrawPoint();
                            int tx3 = Myglobals.TheX3d;
                            int ty3 = Myglobals.TheY3d;
                            g.DrawLine(pen, tx2, ty2, tx3, ty3);
                            Joint3dM = new Math3DP.PointG(0, -1.8, -0.1);
                            Joint3dM.RotateX = Myglobals.tXValue;
                            Joint3dM.RotateY = Myglobals.tYValue;
                            Joint3dM.RotateZ = Myglobals.tZValue;
                            Joint3dM.DrawPoint();
                            tx3 = Myglobals.TheX3d;
                            ty3 = Myglobals.TheY3d;
                            g.DrawLine(pen, tx2, ty2, tx3, ty3);
                            g.DrawString("Y", drawFont, drawBrush, tx2, ty2);
                        }
                        #endregion
                        #region//Z
                        if (i == GridLine.Number3d)
                        {
                            pen = new Pen(Color.Blue, 2f);
                            drawBrush = new SolidBrush(Color.Blue);
                            Math3DP.PointG Joint3dM = new Math3DP.PointG(-0.1, 0.1, -1.8);
                            Joint3dM.RotateX = Myglobals.tXValue;
                            Joint3dM.RotateY = Myglobals.tYValue;
                            Joint3dM.RotateZ = Myglobals.tZValue;
                            Joint3dM.DrawPoint();
                            int tx3 = Myglobals.TheX3d;
                            int ty3 = Myglobals.TheY3d;
                            g.DrawLine(pen, tx2, ty2, tx3, ty3);
                            Joint3dM = new Math3DP.PointG(0.1, -0.1, -1.8);
                            Joint3dM.RotateX = Myglobals.tXValue;
                            Joint3dM.RotateY = Myglobals.tYValue;
                            Joint3dM.RotateZ = Myglobals.tZValue;
                            Joint3dM.DrawPoint();
                            tx3 = Myglobals.TheX3d;
                            ty3 = Myglobals.TheY3d;
                            g.DrawLine(pen, tx2, ty2, tx3, ty3);
                            g.DrawString("Z", drawFont, drawBrush, tx2, ty2);
                        }
                        #endregion
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    }
                    #region//تسميات المحاور
                    drawBrush = new SolidBrush(Color.Gray);
                    pen = new Pen(Color.LightGray, 1f);
                    for (int i = 1; i < GridLine.OnX + GridLine.OnY + GridLine.OnXY + 1; i++)
                    {
                        if (GridLine.Visible[i] == 0)
                        {
                            double alfa = Angulo(GridLine.X1Real[i], GridLine.Y1Real[i], GridLine.X2Real[i], GridLine.Y2Real[i]);
                            alfa = alfa * Math.PI / 180;
                            double txr1 = GridLine.X1Real[i] - 1 * Math.Cos(alfa);
                            double tyr1 = GridLine.Y1Real[i] - 1 * Math.Sin(alfa);
                            tyr1 = -1 * tyr1;
                            double tzr1 = 0;
                            Math3DP.PointG Joint3dM = new Math3DP.PointG(txr1, tyr1, tzr1);
                            Joint3dM.RotateX = Myglobals.tXValue;
                            Joint3dM.RotateY = Myglobals.tYValue;
                            Joint3dM.RotateZ = Myglobals.tZValue;
                            Joint3dM.DrawPoint();
                            tx1 = Myglobals.TheX3d;
                            ty1 = Myglobals.TheY3d;
                            tx2 = GridLine.X13d[i];
                            ty2 = GridLine.Y13d[i];
                            //  g.DrawEllipse(pen, tx1 , ty1, 20, 20);
                            g.DrawLine(pen, tx1, ty1, tx2, ty2);
                            g.DrawString(GridLine.Name[i], drawFont, drawBrush, tx1, ty1);
                            txr1 = GridLine.X2Real[i] + 1 * Math.Cos(alfa);
                            tyr1 = GridLine.Y2Real[i] + 1 * Math.Sin(alfa);
                            tyr1 = -1 * tyr1;
                            tzr1 = 0;
                            Joint3dM = new Math3DP.PointG(txr1, tyr1, tzr1);
                            Joint3dM.RotateX = Myglobals.tXValue;
                            Joint3dM.RotateY = Myglobals.tYValue;
                            Joint3dM.RotateZ = Myglobals.tZValue;
                            Joint3dM.DrawPoint();
                            tx1 = Myglobals.TheX3d;
                            ty1 = Myglobals.TheY3d;
                            tx2 = GridLine.X23d[i];
                            ty2 = GridLine.Y23d[i];
                            //  g.DrawEllipse(pen, tx1 , ty1, 20, 20);
                            g.DrawLine(pen, tx1, ty1, tx2, ty2);
                            g.DrawString(GridLine.Name[i], drawFont, drawBrush, tx1, ty1);
                        }
                    }
                    #endregion
                    g.Dispose();
                }
                catch { }
            }
        }
        public void DrawBeam2d()
        {
            int tx1 = 0;
            int ty1 = 0;
            int tx2 = 0;
            int ty2 = 0;
            Graphics g = Graphics.FromImage(((MainForm)mainForm).pictureBox1.Image);
            Pen pen = new Pen(Color.Blue, 1f);
            Font drawFont = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            for (int i = 1; i < Frame.Number + 1; i++)
            {
                if (((MainForm)mainForm).FrameElement[i].Visible == 0)
                {
                    //جائز
                    if (Joint.ZReal[((MainForm)mainForm).FrameElement[i].FirstJoint] == Myglobals.StoryLevel[Myglobals.SelectedStory] & Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint] == Myglobals.StoryLevel[Myglobals.SelectedStory])
                    {
                        //float[] dashValues = { 5, 2, 15, 4 };
                        pen = new Pen(Color.Blue, 1f);
                        float[] dashValues = { 5, 3, 5, 3 };
                        if (((MainForm)mainForm).FrameElement[i].Selected == 1) pen.DashPattern = dashValues;
                        tx1 = Joint.X2d[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        ty1 = Joint.Y2d[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        tx2 = Joint.X2d[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        ty2 = Joint.Y2d[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        if (Frame.Assignments == 1)
                        {
                            tx1 = (tx1 + tx2) / 2;
                            ty1 = (ty1 + ty2) / 2;
                            g.DrawString(i.ToString(), drawFont, drawBrush, tx1 - 10, ty1 - 15);
                        }
                    }
                    //عمود
                    if (Joint.XReal[((MainForm)mainForm).FrameElement[i].FirstJoint] == Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint] & Joint.YReal[((MainForm)mainForm).FrameElement[i].FirstJoint] == Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint])
                    {
                        pen = new Pen(Color.Black, 1f);
                        double MaxZ = Joint.ZReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        if (MaxZ < Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint])
                        {
                            MaxZ = Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        }
                        if (MaxZ == Myglobals.StoryLevel[Myglobals.SelectedStory])
                        {
                            Point[] P = new Point[6];
                            double angelRotate = ((MainForm)mainForm).FrameElement[i].RotateAngel * Math.PI / 180;////
                            angelRotate = angelRotate + Math.PI / 2;


                            double D = Section.D[((MainForm)mainForm).FrameElement[i].Section] ;/// 100;
                            if (D == 0) D = Section.HT[((MainForm)mainForm).FrameElement[i].Section];/// 100;
                            double BF = Section.BF[((MainForm)mainForm).FrameElement[i].Section];// / 100;
                            if (BF == 0) BF = Section.B[((MainForm)mainForm).FrameElement[i].Section];// / 100;
                            double BFB = Section.BFB[((MainForm)mainForm).FrameElement[i].Section];// / 100;
                            double TF = Section.TF[((MainForm)mainForm).FrameElement[i].Section];// / 100;
                            double TFB = Section.TFB[((MainForm)mainForm).FrameElement[i].Section];// / 100;
                            double TW = Section.TW[((MainForm)mainForm).FrameElement[i].Section];// / 100;
                            double X = Section.X[((MainForm)mainForm).FrameElement[i].Section];// / 100;
                            double Y = Section.Y[((MainForm)mainForm).FrameElement[i].Section];/// 100;
                            string DESIGNATION = Section.DESIGNATION[((MainForm)mainForm).FrameElement[i].Section];
                            #region //مستطيل
                            if (DESIGNATION == "R")
                            {
                                int width = Convert.ToInt32(BF * Myglobals.Zoom2d);
                                int hight = Convert.ToInt32(D * Myglobals.Zoom2d);
                                double radious = Math.Sqrt(Math.Pow(hight / 2, 2) + Math.Pow(width / 2, 2));
                                double angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2(hight / 2, width / 2);
                                double angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                                double thelength = 2 * radious * Math.Sin(angelRotate / 2);
                                int DfBR = Convert.ToInt32(thelength * Math.Cos(angel2));
                                int DfDR = Convert.ToInt32(thelength * Math.Sin(angel2));
                                int DfBL = Convert.ToInt32(thelength * Math.Sin(angel3));
                                int DfDL = Convert.ToInt32(thelength * Math.Cos(angel3));
                                P[1].X = Joint.X2d[((MainForm)mainForm).FrameElement[i].FirstJoint] - width / 2 - DfBL;
                                P[1].Y = Joint.Y2d[((MainForm)mainForm).FrameElement[i].FirstJoint] - hight / 2 + DfDL;
                                P[2].X = Joint.X2d[((MainForm)mainForm).FrameElement[i].FirstJoint] + width / 2 - DfBR;
                                P[2].Y = Joint.Y2d[((MainForm)mainForm).FrameElement[i].FirstJoint] - hight / 2 - DfDR;
                                P[3].X = Joint.X2d[((MainForm)mainForm).FrameElement[i].FirstJoint] + width / 2 + DfBL;
                                P[3].Y = Joint.Y2d[((MainForm)mainForm).FrameElement[i].FirstJoint] + hight / 2 - DfDL;
                                P[4].X = Joint.X2d[((MainForm)mainForm).FrameElement[i].FirstJoint] - width / 2 + DfBR;
                                P[4].Y = Joint.Y2d[((MainForm)mainForm).FrameElement[i].FirstJoint] + hight / 2 + DfDR;
                                P[5].X = P[1].X;
                                P[5].Y = P[1].Y;
                                for (int j = 1; j < 4; j++)
                                {
                                    g.DrawLine(pen, P[j].X, P[j].Y, P[j + 1].X, P[j + 1].Y);
                                }
                                g.DrawLine(pen, P[1].X, P[1].Y, P[4].X, P[4].Y);
                                using (Brush brush = new SolidBrush(Color.FromArgb(50, Color.Gray)))
                                {
                                    g.FillPolygon(brush, P);
                                }
                            }
                            #endregion
                            #region //دائرة
                            if (DESIGNATION == "CR")
                            {
                                int hight = Convert.ToInt32(D * Myglobals.Zoom2d);
                                P = new Point[25];
                                double alfa = Math.PI / 12;
                                for (int j = 0; j < 25; j++)
                                {
                                    double alfa1 = alfa * (j - 1);
                                    P[j].X = (int)(Joint.X2d[((MainForm)mainForm).FrameElement[i].FirstJoint] - (Math.Cos(alfa1) * hight / 2));
                                    P[j].Y = (int)(Joint.Y2d[((MainForm)mainForm).FrameElement[i].FirstJoint] - (Math.Sin(alfa1) * hight / 2));
                                }
                                for (int j = 0; j < 24; j++)
                                {
                                    g.DrawLine(pen, P[j], P[j + 1]);
                                }
                                using (Brush brush = new SolidBrush(Color.FromArgb(50, Color.Gray)))
                                {
                                    g.FillPolygon(brush, P);
                                }
                            }
                            #endregion
                            #region //تيوب
                            if (DESIGNATION == "B")
                            {
                                int width = Convert.ToInt32(BF * Myglobals.Zoom2d);
                                int hight = Convert.ToInt32(D * Myglobals.Zoom2d);
                                double radious = Math.Sqrt(Math.Pow(hight / 2, 2) + Math.Pow(width / 2, 2));
                                double angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2(hight / 2, width / 2);
                                double angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                                double thelength = 2 * radious * Math.Sin(angelRotate / 2);
                                int DfBR = Convert.ToInt32(thelength * Math.Cos(angel2));
                                int DfDR = Convert.ToInt32(thelength * Math.Sin(angel2));
                                int DfBL = Convert.ToInt32(thelength * Math.Sin(angel3));
                                int DfDL = Convert.ToInt32(thelength * Math.Cos(angel3));
                                P[1].X = Joint.X2d[((MainForm)mainForm).FrameElement[i].FirstJoint] - width / 2 - DfBL;
                                P[1].Y = Joint.Y2d[((MainForm)mainForm).FrameElement[i].FirstJoint] - hight / 2 + DfDL;
                                P[2].X = Joint.X2d[((MainForm)mainForm).FrameElement[i].FirstJoint] + width / 2 - DfBR;
                                P[2].Y = Joint.Y2d[((MainForm)mainForm).FrameElement[i].FirstJoint] - hight / 2 - DfDR;
                                P[3].X = Joint.X2d[((MainForm)mainForm).FrameElement[i].FirstJoint] + width / 2 + DfBL;
                                P[3].Y = Joint.Y2d[((MainForm)mainForm).FrameElement[i].FirstJoint] + hight / 2 - DfDL;
                                P[4].X = Joint.X2d[((MainForm)mainForm).FrameElement[i].FirstJoint] - width / 2 + DfBR;
                                P[4].Y = Joint.Y2d[((MainForm)mainForm).FrameElement[i].FirstJoint] + hight / 2 + DfDR;
                                P[5].X = P[1].X;
                                P[5].Y = P[1].Y;
                                for (int j = 1; j < 4; j++)
                                {
                                    g.DrawLine(pen, P[j].X, P[j].Y, P[j + 1].X, P[j + 1].Y);
                                }
                                g.DrawLine(pen, P[1].X, P[1].Y, P[4].X, P[4].Y);
                                //  using (Brush brush = new SolidBrush(Color.FromArgb(50, Color.Gray)))
                                //  {
                                //      g.FillPolygon(brush, P);
                                //   }
                            }
                            #endregion
                            #region ///شكل I
                            if (DESIGNATION == "W" || DESIGNATION == "I")
                            {
                                int BFint = Convert.ToInt32(BF * Myglobals.Zoom2d);
                                int Dint = Convert.ToInt32(D * Myglobals.Zoom2d);
                                int BFBint = Convert.ToInt32(BFB * Myglobals.Zoom2d);
                                int TFint = Convert.ToInt32(TF * Myglobals.Zoom2d);
                                int TFBint = Convert.ToInt32(TFB * Myglobals.Zoom2d);
                                int TWint = Convert.ToInt32(TW * Myglobals.Zoom2d);
                                double radious = Math.Sqrt(Math.Pow(Dint / 2, 2) + Math.Pow(BFint / 2, 2));
                                double angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2(Dint / 2, BFint / 2);
                                double angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                                double thelength = 2 * radious * Math.Sin(angelRotate / 2);
                                int DfBR1 = Convert.ToInt32(thelength * Math.Cos(angel2));
                                int DfDR1 = Convert.ToInt32(thelength * Math.Sin(angel2));
                                int DfBL1 = Convert.ToInt32(thelength * Math.Sin(angel3));
                                int DfDL1 = Convert.ToInt32(thelength * Math.Cos(angel3));
                                radious = Math.Sqrt(Math.Pow(Dint / 2 - TFint, 2) + Math.Pow(BFint / 2, 2));
                                angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2(Dint / 2 - TFint, BFint / 2);
                                angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                                thelength = 2 * radious * Math.Sin(angelRotate / 2);
                                int DfBR2 = Convert.ToInt32(thelength * Math.Cos(angel2));
                                int DfDR2 = Convert.ToInt32(thelength * Math.Sin(angel2));
                                int DfBL2 = Convert.ToInt32(thelength * Math.Sin(angel3));
                                int DfDL2 = Convert.ToInt32(thelength * Math.Cos(angel3));
                                radious = Math.Sqrt(Math.Pow(Dint / 2 - TFint, 2) + Math.Pow(TWint / 2, 2));
                                angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2(Dint / 2 - TFint, TWint / 2);
                                angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                                thelength = 2 * radious * Math.Sin(angelRotate / 2);
                                int DfBR3 = Convert.ToInt32(thelength * Math.Cos(angel2));
                                int DfDR3 = Convert.ToInt32(thelength * Math.Sin(angel2));
                                int DfBL3 = Convert.ToInt32(thelength * Math.Sin(angel3));
                                int DfDL3 = Convert.ToInt32(thelength * Math.Cos(angel3));
                                radious = Math.Sqrt(Math.Pow(Dint / 2, 2) + Math.Pow(BFBint / 2, 2));
                                angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2(Dint / 2, BFBint / 2);
                                angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                                thelength = 2 * radious * Math.Sin(angelRotate / 2);
                                int DfBR4 = Convert.ToInt32(thelength * Math.Cos(angel2));
                                int DfDR4 = Convert.ToInt32(thelength * Math.Sin(angel2));
                                int DfBL4 = Convert.ToInt32(thelength * Math.Sin(angel3));
                                int DfDL4 = Convert.ToInt32(thelength * Math.Cos(angel3));
                                radious = Math.Sqrt(Math.Pow(Dint / 2 - TFBint, 2) + Math.Pow(BFBint / 2, 2));
                                angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2(Dint / 2 - TFBint, BFBint / 2);
                                angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                                thelength = 2 * radious * Math.Sin(angelRotate / 2);
                                int DfBR5 = Convert.ToInt32(thelength * Math.Cos(angel2));
                                int DfDR5 = Convert.ToInt32(thelength * Math.Sin(angel2));
                                int DfBL5 = Convert.ToInt32(thelength * Math.Sin(angel3));
                                int DfDL5 = Convert.ToInt32(thelength * Math.Cos(angel3));
                                radious = Math.Sqrt(Math.Pow(Dint / 2 - TFBint, 2) + Math.Pow(TWint / 2, 2));
                                angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2(Dint / 2 - TFBint, TWint / 2);
                                angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                                thelength = 2 * radious * Math.Sin(angelRotate / 2);
                                int DfBR6 = Convert.ToInt32(thelength * Math.Cos(angel2));
                                int DfDR6 = Convert.ToInt32(thelength * Math.Sin(angel2));
                                int DfBL6 = Convert.ToInt32(thelength * Math.Sin(angel3));
                                int DfDL6 = Convert.ToInt32(thelength * Math.Cos(angel3));
                                P = new Point[14];
                                int startX = Joint.X2d[((MainForm)mainForm).FrameElement[i].FirstJoint];
                                int startY = Joint.Y2d[((MainForm)mainForm).FrameElement[i].FirstJoint];
                                P[1].X = startX - BFint / 2 - DfBL1;
                                P[1].Y = startY - Dint / 2 + DfDL1;
                                P[2].X = startX + BFint / 2 - DfBR1;
                                P[2].Y = startY - Dint / 2 - DfDR1;
                                P[3].X = startX + BFint / 2 - DfBR2;
                                P[3].Y = startY - Dint / 2 + TFint - DfDR2;
                                P[4].X = startX + TWint / 2 - DfBR3;
                                P[4].Y = startY - Dint / 2 + TFint - DfDR3;
                                P[5].X = startX + TWint / 2 + DfBL6;
                                P[5].Y = startY + Dint / 2 - TFBint - DfDL6;
                                P[6].X = startX + BFBint / 2 + DfBL5;
                                P[6].Y = startY + Dint / 2 - TFBint - DfDL5;
                                P[7].X = startX + BFBint / 2 + DfBL4;
                                P[7].Y = startY + Dint / 2 - DfDL4;
                                P[8].X = startX - BFBint / 2 + DfBR4;
                                P[8].Y = startY + Dint / 2 + DfDR4;
                                P[9].X = startX - BFBint / 2 + DfBR5;
                                P[9].Y = startY + Dint / 2 - TFBint + DfDR5;
                                P[10].X = startX - TWint / 2 + DfBR6;
                                P[10].Y = startY + Dint / 2 - TFBint + DfDR6;
                                P[11].X = startX - TWint / 2 - DfBL3;
                                P[11].Y = startY - Dint / 2 + TFint + DfDL3;
                                P[12].X = startX - BFint / 2 - DfBL2;
                                P[12].Y = startY - Dint / 2 + TFint + DfDL2;
                                P[13] = P[1];
                                g.DrawLine(pen, P[1], P[2]);
                                g.DrawLine(pen, P[2], P[3]);
                                g.DrawLine(pen, P[3], P[4]);
                                g.DrawLine(pen, P[4], P[5]);
                                g.DrawLine(pen, P[5], P[6]);
                                g.DrawLine(pen, P[6], P[7]);
                                g.DrawLine(pen, P[7], P[8]);
                                g.DrawLine(pen, P[8], P[9]);
                                g.DrawLine(pen, P[9], P[10]);
                                g.DrawLine(pen, P[10], P[11]);
                                g.DrawLine(pen, P[11], P[12]);
                                g.DrawLine(pen, P[12], P[1]);
                                using (Brush brush = new SolidBrush(Color.FromArgb(50, Color.Gray)))
                                {
                                    g.FillPolygon(brush, P);
                                }
                            }
                            #endregion
                            #region ///شكل I
                            if (DESIGNATION == "C")
                            {
                                int BFint = Convert.ToInt32(BF * Myglobals.Zoom2d);
                                int Dint = Convert.ToInt32(D * Myglobals.Zoom2d);
                                int Xint = Convert.ToInt32(X * Myglobals.Zoom2d);
                                int TFint = Convert.ToInt32(TF * Myglobals.Zoom2d);
                                int TWint = Convert.ToInt32(TW * Myglobals.Zoom2d);

                                double radious = Math.Sqrt(Math.Pow(Dint / 2, 2) + Math.Pow(Xint, 2));
                                double angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2(Dint / 2, Xint);
                                double angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                                double thelength = 2 * radious * Math.Sin(angelRotate / 2);
                                int DfBR1 = Convert.ToInt32(thelength * Math.Cos(angel2));
                                int DfDR1 = Convert.ToInt32(thelength * Math.Sin(angel2));
                                int DfBL1 = Convert.ToInt32(thelength * Math.Sin(angel3));
                                int DfDL1 = Convert.ToInt32(thelength * Math.Cos(angel3));
                                radious = Math.Sqrt(Math.Pow(Dint / 2, 2) + Math.Pow(BFint - Xint, 2));
                                angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2(Dint / 2, BFint - Xint);
                                angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                                thelength = 2 * radious * Math.Sin(angelRotate / 2);
                                int DfBR2 = Convert.ToInt32(thelength * Math.Cos(angel2));
                                int DfDR2 = Convert.ToInt32(thelength * Math.Sin(angel2));
                                int DfBL2 = Convert.ToInt32(thelength * Math.Sin(angel3));
                                int DfDL2 = Convert.ToInt32(thelength * Math.Cos(angel3));
                                radious = Math.Sqrt(Math.Pow(Dint / 2 - TFint, 2) + Math.Pow(BFint - Xint, 2));
                                angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2(Dint / 2 - TFint, BFint - Xint);
                                angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                                thelength = 2 * radious * Math.Sin(angelRotate / 2);
                                int DfBR3 = Convert.ToInt32(thelength * Math.Cos(angel2));
                                int DfDR3 = Convert.ToInt32(thelength * Math.Sin(angel2));
                                int DfBL3 = Convert.ToInt32(thelength * Math.Sin(angel3));
                                int DfDL3 = Convert.ToInt32(thelength * Math.Cos(angel3));
                                radious = Math.Sqrt(Math.Pow(Dint / 2 - TFint, 2) + Math.Pow(Xint - TWint, 2));
                                angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2(Dint / 2 - TFint, Xint - TWint);
                                angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                                thelength = 2 * radious * Math.Sin(angelRotate / 2);
                                int DfBR4 = Convert.ToInt32(thelength * Math.Cos(angel2));
                                int DfDR4 = Convert.ToInt32(thelength * Math.Sin(angel2));
                                int DfBL4 = Convert.ToInt32(thelength * Math.Sin(angel3));
                                int DfDL4 = Convert.ToInt32(thelength * Math.Cos(angel3));

                                P = new Point[10];
                                int startX = Joint.X2d[((MainForm)mainForm).FrameElement[i].FirstJoint];
                                int startY = Joint.Y2d[((MainForm)mainForm).FrameElement[i].FirstJoint];
                                P[1].X = startX - Xint - DfBL1;
                                P[1].Y = startY - Dint / 2 + DfDL1;
                                P[2].X = startX + (BFint - Xint) - DfBR2;
                                P[2].Y = startY - Dint / 2 - DfDR2;
                                P[3].X = startX + (BFint - Xint) - DfBR3;
                                P[3].Y = startY - (Dint / 2 - TFint) - DfDR3;
                                P[4].X = startX - (Xint - TWint) - DfBL4;
                                P[4].Y = startY - (Dint / 2 - TFint) + DfDL4;
                                P[5].X = startX - (Xint - TWint) + DfBR4;
                                P[5].Y = startY + (Dint / 2 - TFint) + DfDR4;
                                P[6].X = startX + (BFint - Xint) + DfBL3;
                                P[6].Y = startY + (Dint / 2 - TFint) - DfDL3;
                                P[7].X = startX + (BFint - Xint) + DfBL2;
                                P[7].Y = startY + Dint / 2 - DfDL2;
                                P[8].X = startX - Xint + DfBR1;
                                P[8].Y = startY + Dint / 2 + DfDR1;
                                P[9] = P[1];
                                g.DrawLine(pen, P[1], P[2]);
                                g.DrawLine(pen, P[2], P[3]);
                                g.DrawLine(pen, P[3], P[4]);
                                g.DrawLine(pen, P[4], P[5]);
                                g.DrawLine(pen, P[5], P[6]);
                                g.DrawLine(pen, P[6], P[7]);
                                g.DrawLine(pen, P[7], P[8]);
                                g.DrawLine(pen, P[8], P[9]);
                                g.DrawLine(pen, P[8], P[1]);
                                using (Brush brush = new SolidBrush(Color.FromArgb(50, Color.Gray)))
                                {
                                    g.FillPolygon(brush, P);
                                }
                            }
                            #endregion
                            #region //شكل L
                            if (DESIGNATION == "L")
                            {
                                int BFint = Convert.ToInt32(BF * Myglobals.Zoom2d);
                                int Dint = Convert.ToInt32(D * Myglobals.Zoom2d);
                                int BFBint = Convert.ToInt32(BFB * Myglobals.Zoom2d);
                                int TFint = Convert.ToInt32(TF * Myglobals.Zoom2d);
                                int TFBint = Convert.ToInt32(TFB * Myglobals.Zoom2d);
                                int TWint = Convert.ToInt32(TW * Myglobals.Zoom2d);
                                int Xint = Convert.ToInt32(X * Myglobals.Zoom2d);
                                int Yint = Convert.ToInt32(Y * Myglobals.Zoom2d);

                                double radious = Math.Sqrt(Math.Pow((Dint - Yint), 2) + Math.Pow(Xint, 2));
                                double angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2((Dint - Yint), Xint);
                                double angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                                double thelength = 2 * radious * Math.Sin(angelRotate / 2);
                                int DfBL1 = Convert.ToInt32(thelength * Math.Sin(angel3));
                                int DfDL1 = Convert.ToInt32(thelength * Math.Cos(angel3));

                                radious = Math.Sqrt(Math.Pow((Dint - Yint), 2) + Math.Pow((Xint - TWint), 2));
                                angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2((Dint - Yint), (Xint - TWint));
                                angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                                thelength = 2 * radious * Math.Sin(angelRotate / 2);
                                int DfBL2 = Convert.ToInt32(thelength * Math.Sin(angel3));
                                int DfDL2 = Convert.ToInt32(thelength * Math.Cos(angel3));

                                radious = Math.Sqrt(Math.Pow((Yint - TFint), 2) + Math.Pow((Xint - TWint), 2));
                                angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2((Yint - TFint), (Xint - TWint));
                                angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                                thelength = 2 * radious * Math.Sin(angelRotate / 2);
                                int DfBL3 = Convert.ToInt32(thelength * Math.Cos(angel2));
                                int DfDL3 = Convert.ToInt32(thelength * Math.Sin(angel2));

                                radious = Math.Sqrt(Math.Pow((Yint - TFint), 2) + Math.Pow((BFint - Xint), 2));
                                angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2((Yint - TFint), (BFint - Xint));
                                angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                                thelength = 2 * radious * Math.Sin(angelRotate / 2);
                                int DfBL4 = Convert.ToInt32(thelength * Math.Sin(angel3));
                                int DfDL4 = Convert.ToInt32(thelength * Math.Cos(angel3));

                                radious = Math.Sqrt(Math.Pow(Yint, 2) + Math.Pow((BFint - Xint), 2));
                                angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2(Yint, (BFint - Xint));
                                angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                                thelength = 2 * radious * Math.Sin(angelRotate / 2);
                                int DfBL5 = Convert.ToInt32(thelength * Math.Sin(angel3));
                                int DfDL5 = Convert.ToInt32(thelength * Math.Cos(angel3));

                                radious = Math.Sqrt(Math.Pow(Yint, 2) + Math.Pow(Xint, 2));
                                angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2(Yint, Xint);
                                angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                                thelength = 2 * radious * Math.Sin(angelRotate / 2);
                                int DfBL6 = Convert.ToInt32(thelength * Math.Cos(angel2));
                                int DfDL6 = Convert.ToInt32(thelength * Math.Sin(angel2));

                                P = new Point[8];
                                int startX = Joint.X2d[((MainForm)mainForm).FrameElement[i].FirstJoint];
                                int startY = Joint.Y2d[((MainForm)mainForm).FrameElement[i].FirstJoint];
                                P[1].X = startX - Xint - DfBL1;
                                P[1].Y = startY - (Dint - Yint) + DfDL1;

                                P[2].X = startX - (Xint - TWint) - DfBL2;
                                P[2].Y = startY - (Dint - Yint) + DfDL2;

                                P[3].X = startX - (Xint - TWint) + DfBL3;
                                P[3].Y = startY + (Yint - TFint) + DfDL3;

                                P[4].X = startX + (BFint - Xint) + DfBL4;
                                P[4].Y = startY + (Yint - TFint) - DfDL4;

                                P[5].X = startX + (BFint - Xint) + DfBL5;
                                P[5].Y = startY + Yint - DfDL5;

                                P[6].X = startX - Xint + DfBL6;
                                P[6].Y = startY + Yint + DfDL6;
                                P[7] = P[1];
                                g.DrawLine(pen, P[1], P[2]);
                                g.DrawLine(pen, P[2], P[3]);
                                g.DrawLine(pen, P[3], P[4]);
                                g.DrawLine(pen, P[4], P[5]);
                                g.DrawLine(pen, P[5], P[6]);
                                g.DrawLine(pen, P[6], P[1]);
                                using (Brush brush = new SolidBrush(Color.FromArgb(80, Color.Gray)))
                                {
                                    g.FillPolygon(brush, P);
                                }
                            }
                            #endregion
                            #region ///شكل T
                            if (DESIGNATION == "T")
                            {
                                int BFint = Convert.ToInt32(BF * Myglobals.Zoom2d);
                                int Dint = Convert.ToInt32(D * Myglobals.Zoom2d);
                                int Yint = Convert.ToInt32(Y * Myglobals.Zoom2d);
                                int Xint = Convert.ToInt32(X * Myglobals.Zoom2d);
                                int TFint = Convert.ToInt32(TF * Myglobals.Zoom2d);
                                int TWint = Convert.ToInt32(TW * Myglobals.Zoom2d);
                                double radious = Math.Sqrt(Math.Pow(Yint, 2) + Math.Pow(BFint / 2, 2));
                                double angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2(Yint, BFint / 2);
                                double angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                                double thelength = 2 * radious * Math.Sin(angelRotate / 2);
                                int DfBR1 = Convert.ToInt32(thelength * Math.Cos(angel2));
                                int DfDR1 = Convert.ToInt32(thelength * Math.Sin(angel2));
                                int DfBL1 = Convert.ToInt32(thelength * Math.Sin(angel3));
                                int DfDL1 = Convert.ToInt32(thelength * Math.Cos(angel3));
                                radious = Math.Sqrt(Math.Pow(Yint - TFint, 2) + Math.Pow(BFint / 2, 2));
                                angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2(Yint - TFint, BFint / 2);
                                angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                                thelength = 2 * radious * Math.Sin(angelRotate / 2);
                                int DfBR2 = Convert.ToInt32(thelength * Math.Cos(angel2));
                                int DfDR2 = Convert.ToInt32(thelength * Math.Sin(angel2));
                                int DfBL2 = Convert.ToInt32(thelength * Math.Sin(angel3));
                                int DfDL2 = Convert.ToInt32(thelength * Math.Cos(angel3));
                                radious = Math.Sqrt(Math.Pow(Yint - TFint, 2) + Math.Pow(TWint / 2, 2));
                                angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2(Yint - TFint, TWint / 2);
                                angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                                thelength = 2 * radious * Math.Sin(angelRotate / 2);
                                int DfBR3 = Convert.ToInt32(thelength * Math.Cos(angel2));
                                int DfDR3 = Convert.ToInt32(thelength * Math.Sin(angel2));
                                int DfBL3 = Convert.ToInt32(thelength * Math.Sin(angel3));
                                int DfDL3 = Convert.ToInt32(thelength * Math.Cos(angel3));
                                radious = Math.Sqrt(Math.Pow((Dint - Yint), 2) + Math.Pow(TWint / 2, 2));
                                angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2((Dint - Yint), TWint / 2);
                                angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                                thelength = 2 * radious * Math.Sin(angelRotate / 2);
                                int DfBR4 = Convert.ToInt32(thelength * Math.Cos(angel2));
                                int DfDR4 = Convert.ToInt32(thelength * Math.Sin(angel2));
                                int DfBL4 = Convert.ToInt32(thelength * Math.Sin(angel3));
                                int DfDL4 = Convert.ToInt32(thelength * Math.Cos(angel3));
                                P = new Point[10];
                                int startX = Joint.X2d[((MainForm)mainForm).FrameElement[i].FirstJoint];
                                int startY = Joint.Y2d[((MainForm)mainForm).FrameElement[i].FirstJoint];
                                P[1].X = startX - BFint / 2 - DfBL1;
                                P[1].Y = startY - Yint + DfDL1;
                                P[2].X = startX + BFint / 2 - DfBR1;
                                P[2].Y = startY - Yint - DfDR1;
                                P[3].X = startX + BFint / 2 - DfBR2;
                                P[3].Y = startY - Yint + TFint - DfDR2;
                                P[4].X = startX + TWint / 2 - DfBR3;
                                P[4].Y = startY - Yint + TFint - DfDR3;
                                P[5].X = startX + TWint / 2 + DfBL4;
                                P[5].Y = startY + (Dint - Yint) - DfDL4;
                                P[6].X = startX - TWint / 2 + DfBR4;
                                P[6].Y = startY + (Dint - Yint) + DfDR4;
                                P[7].X = startX - TWint / 2 - DfBL3;
                                P[7].Y = startY - Yint + TFint + DfDL3;
                                P[8].X = startX - BFint / 2 - DfBL2;
                                P[8].Y = startY - Yint + TFint + DfDL2;
                                P[9] = P[1];

                                g.DrawLine(pen, P[1], P[2]);
                                g.DrawLine(pen, P[2], P[3]);
                                g.DrawLine(pen, P[3], P[4]);
                                g.DrawLine(pen, P[4], P[5]);
                                g.DrawLine(pen, P[5], P[6]);
                                g.DrawLine(pen, P[6], P[7]);
                                g.DrawLine(pen, P[7], P[8]);
                                g.DrawLine(pen, P[8], P[1]);
                                using (Brush brush = new SolidBrush(Color.FromArgb(50, Color.Gray)))
                                {
                                    g.FillPolygon(brush, P);
                                }
                            }
                            #endregion
                        }
                    }
                }
            }
        }
        public void DrawBeamelev()
        {
            double tx1D = Myglobals.elevPointX[Myglobals.Selectedelev, 1];
            double ty1D = Myglobals.elevPointY[Myglobals.Selectedelev, 1];
            double tz1D = Myglobals.elevPointZ[Myglobals.Selectedelev, 1];
            double tx2D = Myglobals.elevPointX[Myglobals.Selectedelev, 1 + Myglobals.StoryNumbers];
            double ty2D = Myglobals.elevPointY[Myglobals.Selectedelev, 1 + Myglobals.StoryNumbers];
            double tz2D = Myglobals.elevPointZ[Myglobals.Selectedelev, 1 + Myglobals.StoryNumbers];
            double tx3D = Myglobals.elevPointX[Myglobals.Selectedelev, 2 + Myglobals.StoryNumbers];
            double ty3D = Myglobals.elevPointY[Myglobals.Selectedelev, 2 + Myglobals.StoryNumbers];
            double tz3D = Myglobals.elevPointZ[Myglobals.Selectedelev, 2 + Myglobals.StoryNumbers];
            double var2 = tx2D - tx1D;
            double var3 = tx3D - tx1D;
            double var5 = ty2D - ty1D;
            double var6 = ty3D - ty1D;
            double var8 = tz2D - tz1D;
            double var9 = tz3D - tz1D;
            double varA = var5 * var9 - var8 * var6;//a
            double varB = var8 * var3 - var2 * var9;//b
            double varC = var2 * var6 - var5 * var3;//c
            double varD = -varA * tx1D - varB * ty1D - varC * tz1D;//d  
            int tx1 = 0;
            int ty1 = 0;
            int tx2 = 0;
            int ty2 = 0;
            Graphics g = Graphics.FromImage(((MainForm)mainForm).pictureBox5.Image);
            Pen pen = new Pen(Color.Blue, 1f);
            Font drawFont = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            for (int i = 1; i < Frame.Number + 1; i++)
            {
                if (((MainForm)mainForm).FrameElement[i].Visible == 0)
                {
                    int tah1 = 0;
                    int tah2 = 0;
                    int tah = 0;
                    double xx = varA * Joint.XReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                    double yy = varB * Joint.YReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                    double zz = varC * Joint.ZReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                    double sm = xx + yy + zz;
                    if (Math.Abs(sm + varD) <= 0.5) tah1 = 1;
                    xx = varA * Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                    yy = varB * Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                    zz = varC * Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                    sm = xx + yy + zz;
                    if (Math.Abs(sm + varD) <= 0.5) tah2 = 1;
                    if (tah1 == 1 & tah2 == 1) tah = 1;
                    if (tah == 1)
                    {
                        pen = new Pen(Color.Blue, 1f);
                        float[] dashValues = { 5, 3, 5, 3 };
                        if (((MainForm)mainForm).FrameElement[i].Selected == 1) pen.DashPattern = dashValues;
                        tx1 = Joint.Xelev[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        ty1 = Joint.Yelev[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        tx2 = Joint.Xelev[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        ty2 = Joint.Yelev[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        if (Frame.Assignments == 1)
                        {
                            tx1 = (tx1 + tx2) / 2;
                            ty1 = (ty1 + ty2) / 2;
                            g.DrawString(i.ToString(), drawFont, drawBrush, tx1 - 10, ty1 - 15);
                        }
                    }
                }
            }
        }
        int[] bArray = new int[1];
        int[] BeamTypeToDraw = new int[1];
        public void DrawBeam3d()
        {
            try
            {
                int tx1 = 0;
                int ty1 = 0;
                int tx2 = 0;
                int ty2 = 0;
                double tx1R = 0;
                double ty1R = 0;
                double tz1R = 0;
                double tx2R = 0;
                double ty2R = 0;
                double tz2R = 0;
                double txR = 0;
                double tyR = 0;
                double tzR = 0;
                double txRR = 0;
                double tyRR = 0;
                double tzRR = 0;
                double txR0 = 0;
                double tyR0 = 0;
                double tzR0 = 0;
                int tx0 = 0;
                int ty0 = 0;
                double BeamLength = 0;
                double MAXpower = -1000000;
                double PowerScale = 2;
                if (Frame.ShowPower == 1)
                {
                    for (int i = 1; i < Frame.Number + 1; i++)
                    {
                        for (int j = 1; j < ((MainForm)mainForm).FrameElement[i].LoadPNumber + 1; j++)
                        {
                            if (Math.Abs(((MainForm)mainForm).FrameElement[i].LoadPValue[j]) > MAXpower) MAXpower = Math.Abs(((MainForm)mainForm).FrameElement[i].LoadPValue[j]);
                        }
                        for (int j = 1; j < ((MainForm)mainForm).FrameElement[i].LoadDNumber + 1; j++)
                        {
                            if (Math.Abs(((MainForm)mainForm).FrameElement[i].LoadDValue1[j] + ((MainForm)mainForm).FrameElement[i].LoadDUniform[j]) > MAXpower) MAXpower = Math.Abs(((MainForm)mainForm).FrameElement[i].LoadDValue1[j] + ((MainForm)mainForm).FrameElement[i].LoadDUniform[j]);
                            if (Math.Abs(((MainForm)mainForm).FrameElement[i].LoadDValue2[j] + ((MainForm)mainForm).FrameElement[i].LoadDUniform[j]) > MAXpower) MAXpower = Math.Abs(((MainForm)mainForm).FrameElement[i].LoadDValue2[j] + ((MainForm)mainForm).FrameElement[i].LoadDUniform[j]);
                            if (Math.Abs(((MainForm)mainForm).FrameElement[i].LoadDValue3[j] + ((MainForm)mainForm).FrameElement[i].LoadDUniform[j]) > MAXpower) MAXpower = Math.Abs(((MainForm)mainForm).FrameElement[i].LoadDValue3[j] + ((MainForm)mainForm).FrameElement[i].LoadDUniform[j]);
                            if (Math.Abs(((MainForm)mainForm).FrameElement[i].LoadDValue4[j] + ((MainForm)mainForm).FrameElement[i].LoadDUniform[j]) > MAXpower) MAXpower = Math.Abs(((MainForm)mainForm).FrameElement[i].LoadDValue4[j] + ((MainForm)mainForm).FrameElement[i].LoadDUniform[j]);
                        }
                    }
                    PowerScale = PowerScale / MAXpower;
                }
                int X10 = 0;
                int Y10 = 0;
                int X20 = 660;
                int Y20 = 600;
                forarias = 0;
                int m = 0;
                bArray = new int[Frame.Number + 1];
                BeamTypeToDraw = new int[Frame.Number + 1];
                for (int i = 1; i < Frame.Number + 1; i++)
                {
                    if (((MainForm)mainForm).FrameElement[i].Visible == 0)
                    {
                        int X1 = Joint.X3d[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        int Y1 = Joint.Y3d[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        int X2 = Joint.X3d[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        int Y2 = Joint.Y3d[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        double length = Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2));
                        if (length < 20) BeamTypeToDraw[i]=1;
                        int X3 = X10;
                        int Y3 = Y10;
                        int X4 = X20;
                        int Y4 = Y20;
                        INTERSECTION = 0;
                        if (X1 >= X3 & X1 <= X4 & Y1 >= Y3 & Y1 <= Y4) INTERSECTION = 1;
                        if (X2 >= X3 & X2 <= X4 & Y2 >= Y3 & Y2 <= Y4) INTERSECTION = 1;
                        if (INTERSECTION == 1)
                        {
                            m = m + 1;
                            bArray[m] = i;
                            goto nexti;
                        }
                        checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                        if (INTERSECTION == 1)
                        {
                            m = m + 1;
                            bArray[m] = i;
                            goto nexti;
                        }
                        X3 = X20;
                        Y3 = Y10;
                        X4 = X20;
                        Y4 = Y20;
                        checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                        if (INTERSECTION == 1)
                        {
                            m = m + 1;
                            bArray[m] = i;
                            goto nexti;
                        }
                        X3 = X20;
                        Y3 = Y20;
                        X4 = X10;
                        Y4 = Y20;
                        checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                        if (INTERSECTION == 1)
                        {
                            m = m + 1;
                            bArray[m] = i;
                            goto nexti;
                        }
                        X3 = X10;
                        Y3 = Y10;
                        X4 = X10;
                        Y4 = Y20;
                        checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                        if (INTERSECTION == 1)
                        {
                            m = m + 1;
                            bArray[m] = i;
                            goto nexti;
                        }
                    }
                nexti: { }
                }
                if (Myglobals.DrowRealShape == 0  || Myglobals.ExtrudedShell == 0)
                {
                    Graphics g = Graphics.FromImage(((MainForm)mainForm).pictureBox2.Image);
                    Pen pen = new Pen(Color.Blue, 1f);
                    Font drawFont = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
                    SolidBrush drawBrush = new SolidBrush(Color.Black);
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    for (int iai = 1; iai < bArray.Length; iai++)
                    {
                        int i = bArray[iai];
                        if (i == 0) break;
                        float[] dashValues = { 5, 3, 5, 3 };
                        double dx = 0;
                        double dy = 0;
                        double dz = 0;
                        double dx1 = 0;
                        double dy1 = 0;
                        double dz1 = 0;
                        pen = new Pen(Color.Blue, 1f);
                        if (((MainForm)mainForm).FrameElement[i].Selected == 1) pen.DashPattern = dashValues;
                        tx1 = Joint.X3d[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        ty1 = Joint.Y3d[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        tx2 = Joint.X3d[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        ty2 = Joint.Y3d[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        if (Frame.Assignments == 1)
                        {
                            tx1 = (tx1 + tx2) / 2;
                            ty1 = (ty1 + ty2) / 2;
                            g.DrawString(i.ToString(), drawFont, drawBrush, tx1 - 10, ty1 - 20);
                        }
                        if (Frame.ShowPower == 1)
                        {
                            double ThePowerValue = 0;
                            double TheDistanceValue = 0;
                            tx1R = Joint.XReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                            ty1R = Joint.YReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                            tz1R = Joint.ZReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                            tx2R = Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                            ty2R = Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                            tz2R = Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                            BeamLength = (Math.Sqrt(Math.Pow(tx1R - tx2R, 2) + Math.Pow(ty1R - ty2R, 2) + Math.Pow(tz1R - tz2R, 2)));
                            pen = new Pen(Color.Green, 2f);
                            #region//قوى مركزة
                            for (int j = 1; j < ((MainForm)mainForm).FrameElement[i].LoadPNumber + 1; j++)
                            {
                                ThePowerValue = ((MainForm)mainForm).FrameElement[i].LoadPValue[j];
                                txR0 = tx1R + ((MainForm)mainForm).FrameElement[i].LoadPDistance[i] * (tx2R - tx1R);
                                tyR0 = ty1R + ((MainForm)mainForm).FrameElement[i].LoadPDistance[i] * (ty2R - ty1R);
                                tzR0 = tz1R + ((MainForm)mainForm).FrameElement[i].LoadPDistance[i] * (tz2R - tz1R);
                                txR = (txR0);
                                tyR = -1 * (tyR0);
                                tzR = -1 * (tzR0);
                                Math3DP.PointG Point3dM = new Math3DP.PointG(txR, tyR, tzR);
                                Point3dM.RotateX = Myglobals.tXValue;
                                Point3dM.RotateY = Myglobals.tYValue;
                                Point3dM.RotateZ = Myglobals.tZValue;
                                Point3dM.DrawPoint();
                                tx1 = Myglobals.TheX3d;
                                ty1 = Myglobals.TheY3d;
                                if (((MainForm)mainForm).FrameElement[i].LoadPDirection[i] == 0)//على محور 1
                                {
                                    dx = (((MainForm)mainForm).FrameElement[i].AxisX2[1] - ((MainForm)mainForm).FrameElement[i].AxisX1[1]);
                                    dy = (((MainForm)mainForm).FrameElement[i].AxisY2[1] - ((MainForm)mainForm).FrameElement[i].AxisY1[1]);
                                    dz = (((MainForm)mainForm).FrameElement[i].AxisZ2[1] - ((MainForm)mainForm).FrameElement[i].AxisZ1[1]);
                                    dx1 = (((MainForm)mainForm).FrameElement[i].AxisX2[3] - ((MainForm)mainForm).FrameElement[i].AxisX1[3]);
                                    dy1 = (((MainForm)mainForm).FrameElement[i].AxisY2[3] - ((MainForm)mainForm).FrameElement[i].AxisY1[3]);
                                    dz1 = (((MainForm)mainForm).FrameElement[i].AxisZ2[3] - ((MainForm)mainForm).FrameElement[i].AxisZ1[3]);
                                    txR = (txR0 - PowerScale * ThePowerValue * dx);
                                    tyR = -1 * (tyR0 - PowerScale * ThePowerValue * dy);
                                    tzR = -1 * (tzR0 - PowerScale * ThePowerValue * dz);
                                }
                                if (((MainForm)mainForm).FrameElement[i].LoadPDirection[i] == 1)//على محور 2
                                {
                                    dx = (((MainForm)mainForm).FrameElement[i].AxisX2[2] - ((MainForm)mainForm).FrameElement[i].AxisX1[2]);
                                    dy = (((MainForm)mainForm).FrameElement[i].AxisY2[2] - ((MainForm)mainForm).FrameElement[i].AxisY1[2]);
                                    dz = (((MainForm)mainForm).FrameElement[i].AxisZ2[2] - ((MainForm)mainForm).FrameElement[i].AxisZ1[2]);
                                    dx1 = (((MainForm)mainForm).FrameElement[i].AxisX2[3] - ((MainForm)mainForm).FrameElement[i].AxisX1[3]);
                                    dy1 = (((MainForm)mainForm).FrameElement[i].AxisY2[3] - ((MainForm)mainForm).FrameElement[i].AxisY1[3]);
                                    dz1 = (((MainForm)mainForm).FrameElement[i].AxisZ2[3] - ((MainForm)mainForm).FrameElement[i].AxisZ1[3]);
                                    txR = (txR0 - PowerScale * ThePowerValue * dx);
                                    tyR = -1 * (tyR0 - PowerScale * ThePowerValue * dy);
                                    tzR = -1 * (tzR0 - PowerScale * ThePowerValue * dz);
                                }
                                if (((MainForm)mainForm).FrameElement[i].LoadPDirection[i] == 2)//على محور 3
                                {
                                    dx = (((MainForm)mainForm).FrameElement[i].AxisX2[3] - ((MainForm)mainForm).FrameElement[i].AxisX1[3]);
                                    dy = (((MainForm)mainForm).FrameElement[i].AxisY2[3] - ((MainForm)mainForm).FrameElement[i].AxisY1[3]);
                                    dz = (((MainForm)mainForm).FrameElement[i].AxisZ2[3] - ((MainForm)mainForm).FrameElement[i].AxisZ1[3]);
                                    dx1 = (((MainForm)mainForm).FrameElement[i].AxisX2[1] - ((MainForm)mainForm).FrameElement[i].AxisX1[1]);
                                    dy1 = (((MainForm)mainForm).FrameElement[i].AxisY2[1] - ((MainForm)mainForm).FrameElement[i].AxisY1[1]);
                                    dz1 = (((MainForm)mainForm).FrameElement[i].AxisZ2[1] - ((MainForm)mainForm).FrameElement[i].AxisZ1[1]);
                                    txR = (txR0 - PowerScale * ThePowerValue * dx);
                                    tyR = -1 * (tyR0 - PowerScale * ThePowerValue * dy);
                                    tzR = -1 * (tzR0 - PowerScale * ThePowerValue * dz);
                                }
                                if (((MainForm)mainForm).FrameElement[i].LoadPDirection[i] == 3)//على محور X
                                {
                                    txR = (txR0 - PowerScale * ThePowerValue);
                                    tyR = -1 * (tyR0);
                                    tzR = -1 * (tzR0);
                                }
                                if (((MainForm)mainForm).FrameElement[i].LoadPDirection[i] == 4)//على محور Y
                                {
                                    txR = (txR0);
                                    tyR = -1 * (tyR0 - PowerScale * ThePowerValue);
                                    tzR = -1 * (tzR0);
                                }
                                if (((MainForm)mainForm).FrameElement[i].LoadPDirection[i] == 5)//على محور Z
                                {
                                    txR = (txR0);
                                    tyR = -1 * (tyR0);
                                    tzR = -1 * (tzR0 + PowerScale * ThePowerValue);
                                }
                                Point3dM = new Math3DP.PointG(txR, tyR, tzR);
                                Point3dM.RotateX = Myglobals.tXValue;
                                Point3dM.RotateY = Myglobals.tYValue;
                                Point3dM.RotateZ = Myglobals.tZValue;
                                Point3dM.DrawPoint();
                                tx2 = Myglobals.TheX3d;
                                ty2 = Myglobals.TheY3d;
                                g.DrawLine(pen, tx1, ty1, tx2, ty2);
                                if (ThePowerValue > 0)
                                {
                                    if (Frame.ShowPowerValue == 1) g.DrawString(ThePowerValue.ToString(), drawFont, drawBrush, tx2 - 10, ty2 + 5);
                                    if (((MainForm)mainForm).FrameElement[i].LoadPDirection[i] == 0)//على محور 1
                                    {
                                        txR = (txR0 - 0.2 * dx - 0.2 * dx1);
                                        tyR = -1 * (tyR0 - 0.2 * dy - 0.2 * dy1);
                                        tzR = -1 * (tzR0 - 0.2 * dz - 0.2 * dz1);
                                        txRR = (txR0 - 0.2 * dx + 0.2 * dx1);
                                        tyRR = -1 * (tyR0 - 0.2 * dy + 0.2 * dy1);
                                        tzRR = -1 * (tzR0 - 0.2 * dz + 0.2 * dz1);
                                    }
                                    if (((MainForm)mainForm).FrameElement[i].LoadPDirection[i] == 1)//على محور 2
                                    {
                                        txR = (txR0 - 0.2 * dx - 0.2 * dx1);
                                        tyR = -1 * (tyR0 - 0.2 * dy - 0.2 * dy1);
                                        tzR = -1 * (tzR0 - 0.2 * dz - 0.2 * dz1);
                                        txRR = (txR0 - 0.2 * dx + 0.2 * dx1);
                                        tyRR = -1 * (tyR0 - 0.2 * dy + 0.2 * dy1);
                                        tzRR = -1 * (tzR0 - 0.2 * dz + 0.2 * dz1);
                                    }
                                    if (((MainForm)mainForm).FrameElement[i].LoadPDirection[i] == 2)//على محور 3
                                    {
                                        txR = (txR0 - 0.2 * dx - 0.2 * dx1);
                                        tyR = -1 * (tyR0 - 0.2 * dy - 0.2 * dy1);
                                        tzR = -1 * (tzR0 - 0.2 * dz - 0.2 * dz1);
                                        txRR = (txR0 - 0.2 * dx + 0.2 * dx1);
                                        tyRR = -1 * (tyR0 - 0.2 * dy + 0.2 * dy1);
                                        tzRR = -1 * (tzR0 - 0.2 * dz + 0.2 * dz1);
                                    }
                                    if (((MainForm)mainForm).FrameElement[i].LoadPDirection[i] == 3)//على محور X
                                    {
                                        txR = (txR0 - 0.2);
                                        tyR = -1 * (tyR0 - 0.2);
                                        tzR = -1 * (tzR0);
                                        txRR = (txR0 - 0.2);
                                        tyRR = -1 * (tyR0 + 0.2);
                                        tzRR = -1 * (tzR0);
                                    }
                                    if (((MainForm)mainForm).FrameElement[i].LoadPDirection[i] == 4)//على محور Y
                                    {
                                        txR = (txR0 - 0.2);
                                        tyR = -1 * (tyR0 - 0.2);
                                        tzR = -1 * (tzR0);
                                        txRR = (txR0 + 0.2);
                                        tyRR = -1 * (tyR0 - 0.2);
                                        tzRR = -1 * (tzR0);
                                    }
                                    if (((MainForm)mainForm).FrameElement[i].LoadPDirection[i] == 5)//على محور Z
                                    {
                                        txR = (txR0 - 0.2);
                                        tyR = -1 * (tyR0);
                                        tzR = -1 * (tzR0 + 0.2);
                                        txRR = (txR0 + 0.2);
                                        tyRR = -1 * (tyR0);
                                        tzRR = -1 * (tzR0 + 0.2);
                                    }
                                }
                                if (ThePowerValue < 0)
                                {
                                    if (Frame.ShowPowerValue == 1) g.DrawString(ThePowerValue.ToString(), drawFont, drawBrush, tx2 - 10, ty2 - 15);
                                    if (((MainForm)mainForm).FrameElement[i].LoadPDirection[i] == 0)//على محور 1
                                    {
                                        txR = (txR0 + 0.2 * dx - 0.2 * dx1);
                                        tyR = -1 * (tyR0 + 0.2 * dy - 0.2 * dy1);
                                        tzR = -1 * (tzR0 + 0.2 * dz - 0.2 * dz1);
                                        txRR = (txR0 + 0.2 * dx + 0.2 * dx1);
                                        tyRR = -1 * (tyR0 + 0.2 * dy + 0.2 * dy1);
                                        tzRR = -1 * (tzR0 + 0.2 * dz + 0.2 * dz1);
                                    }
                                    if (((MainForm)mainForm).FrameElement[i].LoadPDirection[i] == 1)//على محور 2
                                    {
                                        txR = (txR0 + 0.2 * dx - 0.2 * dx1);
                                        tyR = -1 * (tyR0 + 0.2 * dy - 0.2 * dy1);
                                        tzR = -1 * (tzR0 + 0.2 * dz - 0.2 * dz1);
                                        txRR = (txR0 + 0.2 * dx + 0.2 * dx1);
                                        tyRR = -1 * (tyR0 + 0.2 * dy + 0.2 * dy1);
                                        tzRR = -1 * (tzR0 + 0.2 * dz + 0.2 * dz1);
                                    }
                                    if (((MainForm)mainForm).FrameElement[i].LoadPDirection[i] == 2)//على محور 3
                                    {
                                        txR = (txR0 + 0.2 * dx - 0.2 * dx1);
                                        tyR = -1 * (tyR0 + 0.2 * dy - 0.2 * dy1);
                                        tzR = -1 * (tzR0 + 0.2 * dz - 0.2 * dz1);
                                        txRR = (txR0 + 0.2 * dx + 0.2 * dx1);
                                        tyRR = -1 * (tyR0 + 0.2 * dy + 0.2 * dy1);
                                        tzRR = -1 * (tzR0 + 0.2 * dz + 0.2 * dz1);
                                    }
                                    if (((MainForm)mainForm).FrameElement[i].LoadPDirection[i] == 3)//على محور X
                                    {
                                        txR = (txR0 + 0.2);
                                        tyR = -1 * (tyR0 - 0.2);
                                        tzR = -1 * (tzR0);
                                        txRR = (txR0 + 0.2);
                                        tyRR = -1 * (tyR0 + 0.2);
                                        tzRR = -1 * (tzR0);
                                    }
                                    if (((MainForm)mainForm).FrameElement[i].LoadPDirection[i] == 4)//على محور Y
                                    {
                                        txR = (txR0 - 0.2);
                                        tyR = -1 * (tyR0 + 0.2);
                                        tzR = -1 * (tzR0);
                                        txRR = (txR0 + 0.2);
                                        tyRR = -1 * (tyR0 + 0.2);
                                        tzRR = -1 * (tzR0);
                                    }
                                    if (((MainForm)mainForm).FrameElement[i].LoadPDirection[i] == 5)//على محور Z
                                    {
                                        txR = (txR0 - 0.2);
                                        tyR = -1 * (tyR0);
                                        tzR = -1 * (tzR0 - 0.2);
                                        txRR = (txR0 + 0.2);
                                        tyRR = -1 * (tyR0);
                                        tzRR = -1 * (tzR0 - 0.2);
                                    }
                                }
                                if (ThePowerValue != 0)
                                {
                                    Point3dM = new Math3DP.PointG(txR, tyR, tzR);
                                    Point3dM.RotateX = Myglobals.tXValue;
                                    Point3dM.RotateY = Myglobals.tYValue;
                                    Point3dM.RotateZ = Myglobals.tZValue;
                                    Point3dM.DrawPoint();
                                    tx2 = Myglobals.TheX3d;
                                    ty2 = Myglobals.TheY3d;
                                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                                    Point3dM = new Math3DP.PointG(txRR, tyRR, tzRR);
                                    Point3dM.RotateX = Myglobals.tXValue;
                                    Point3dM.RotateY = Myglobals.tYValue;
                                    Point3dM.RotateZ = Myglobals.tZValue;
                                    Point3dM.DrawPoint();
                                    tx2 = Myglobals.TheX3d;
                                    ty2 = Myglobals.TheY3d;
                                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                                }
                            }
                            #endregion
                            pen = new Pen(Color.Green, 2f);
                            #region//قوى موزعة
                            for (int j = 1; j < ((MainForm)mainForm).FrameElement[i].LoadDNumber + 1; j++)
                            {
                                int tahkik = 0;
                                tx0 = Joint.X3d[((MainForm)mainForm).FrameElement[i].FirstJoint];
                                ty0 = Joint.Y3d[((MainForm)mainForm).FrameElement[i].FirstJoint];
                                int[] px1 = new int[7];
                                int[] py1 = new int[7];
                                int[] px2 = new int[7];
                                int[] py2 = new int[7];
                                for (int k = 1; k < 7; k++)
                                {
                                    if (k == 1)
                                    {
                                        ThePowerValue = ((MainForm)mainForm).FrameElement[i].LoadDValue1[j] + ((MainForm)mainForm).FrameElement[i].LoadDUniform[j];
                                        TheDistanceValue = ((MainForm)mainForm).FrameElement[i].LoadDDistance1[j];
                                    }
                                    if (k == 2)
                                    {
                                        ThePowerValue = ((MainForm)mainForm).FrameElement[i].LoadDValue2[j] + ((MainForm)mainForm).FrameElement[i].LoadDUniform[j];
                                        TheDistanceValue = ((MainForm)mainForm).FrameElement[i].LoadDDistance2[j];
                                    }
                                    if (k == 3)
                                    {
                                        ThePowerValue = ((MainForm)mainForm).FrameElement[i].LoadDValue3[j] + ((MainForm)mainForm).FrameElement[i].LoadDUniform[j];
                                        TheDistanceValue = ((MainForm)mainForm).FrameElement[i].LoadDDistance3[j];
                                    }
                                    if (k == 4)
                                    {
                                        ThePowerValue = ((MainForm)mainForm).FrameElement[i].LoadDValue4[j] + ((MainForm)mainForm).FrameElement[i].LoadDUniform[j];
                                        TheDistanceValue = ((MainForm)mainForm).FrameElement[i].LoadDDistance4[j];
                                    }
                                    if (k == 5)
                                    {
                                        if (((MainForm)mainForm).FrameElement[i].LoadDUniform[j] != 0 & ((MainForm)mainForm).FrameElement[i].LoadDDistance4[j] < 1)
                                        {
                                            tahkik = 1;
                                            ThePowerValue = ((MainForm)mainForm).FrameElement[i].LoadDUniform[j];
                                            TheDistanceValue = ((MainForm)mainForm).FrameElement[i].LoadDDistance4[j];
                                        }
                                        else
                                        {
                                            goto endI;
                                        }
                                    }
                                    if (k == 6)
                                    {
                                        if (((MainForm)mainForm).FrameElement[i].LoadDUniform[j] != 0 & ((MainForm)mainForm).FrameElement[i].LoadDDistance4[j] < 1)
                                        {
                                            ThePowerValue = ((MainForm)mainForm).FrameElement[i].LoadDUniform[j];
                                            TheDistanceValue = 1;
                                        }
                                        else
                                        {
                                            goto endI;
                                        }
                                    }
                                    txR0 = tx1R + TheDistanceValue * (tx2R - tx1R);
                                    tyR0 = ty1R + TheDistanceValue * (ty2R - ty1R);
                                    tzR0 = tz1R + TheDistanceValue * (tz2R - tz1R);
                                    txR = (txR0);
                                    tyR = -1 * (tyR0);
                                    tzR = -1 * (tzR0);
                                    Math3DP.PointG Point3dM = new Math3DP.PointG(txR, tyR, tzR);
                                    Point3dM.RotateX = Myglobals.tXValue;
                                    Point3dM.RotateY = Myglobals.tYValue;
                                    Point3dM.RotateZ = Myglobals.tZValue;
                                    Point3dM.DrawPoint();
                                    tx1 = Myglobals.TheX3d;
                                    ty1 = Myglobals.TheY3d;
                                    if (((MainForm)mainForm).FrameElement[i].LoadDDirection[j] == 0)//على محور 1
                                    {
                                        dx = (((MainForm)mainForm).FrameElement[i].AxisX2[1] - ((MainForm)mainForm).FrameElement[i].AxisX1[1]);
                                        dy = (((MainForm)mainForm).FrameElement[i].AxisY2[1] - ((MainForm)mainForm).FrameElement[i].AxisY1[1]);
                                        dz = (((MainForm)mainForm).FrameElement[i].AxisZ2[1] - ((MainForm)mainForm).FrameElement[i].AxisZ1[1]);
                                        dx1 = (((MainForm)mainForm).FrameElement[i].AxisX2[3] - ((MainForm)mainForm).FrameElement[i].AxisX1[3]);
                                        dy1 = (((MainForm)mainForm).FrameElement[i].AxisY2[3] - ((MainForm)mainForm).FrameElement[i].AxisY1[3]);
                                        dz1 = (((MainForm)mainForm).FrameElement[i].AxisZ2[3] - ((MainForm)mainForm).FrameElement[i].AxisZ1[3]);
                                        txR = (txR0 - PowerScale * ThePowerValue * dx);
                                        tyR = -1 * (tyR0 - PowerScale * ThePowerValue * dy);
                                        tzR = -1 * (tzR0 - PowerScale * ThePowerValue * dz);
                                    }
                                    if (((MainForm)mainForm).FrameElement[i].LoadDDirection[j] == 1)//على محور 2
                                    {
                                        dx = (((MainForm)mainForm).FrameElement[i].AxisX2[2] - ((MainForm)mainForm).FrameElement[i].AxisX1[2]);
                                        dy = (((MainForm)mainForm).FrameElement[i].AxisY2[2] - ((MainForm)mainForm).FrameElement[i].AxisY1[2]);
                                        dz = (((MainForm)mainForm).FrameElement[i].AxisZ2[2] - ((MainForm)mainForm).FrameElement[i].AxisZ1[2]);
                                        dx1 = (((MainForm)mainForm).FrameElement[i].AxisX2[3] - ((MainForm)mainForm).FrameElement[i].AxisX1[3]);
                                        dy1 = (((MainForm)mainForm).FrameElement[i].AxisY2[3] - ((MainForm)mainForm).FrameElement[i].AxisY1[3]);
                                        dz1 = (((MainForm)mainForm).FrameElement[i].AxisZ2[3] - ((MainForm)mainForm).FrameElement[i].AxisZ1[3]);
                                        txR = (txR0 - PowerScale * ThePowerValue * dx);
                                        tyR = -1 * (tyR0 - PowerScale * ThePowerValue * dy);
                                        tzR = -1 * (tzR0 - PowerScale * ThePowerValue * dz);
                                    }
                                    if (((MainForm)mainForm).FrameElement[i].LoadDDirection[j] == 2)//على محور 3
                                    {
                                        dx = (((MainForm)mainForm).FrameElement[i].AxisX2[3] - ((MainForm)mainForm).FrameElement[i].AxisX1[3]);
                                        dy = (((MainForm)mainForm).FrameElement[i].AxisY2[3] - ((MainForm)mainForm).FrameElement[i].AxisY1[3]);
                                        dz = (((MainForm)mainForm).FrameElement[i].AxisZ2[3] - ((MainForm)mainForm).FrameElement[i].AxisZ1[3]);
                                        dx1 = (((MainForm)mainForm).FrameElement[i].AxisX2[1] - ((MainForm)mainForm).FrameElement[i].AxisX1[1]);
                                        dy1 = (((MainForm)mainForm).FrameElement[i].AxisY2[1] - ((MainForm)mainForm).FrameElement[i].AxisY1[1]);
                                        dz1 = (((MainForm)mainForm).FrameElement[i].AxisZ2[1] - ((MainForm)mainForm).FrameElement[i].AxisZ1[1]);
                                        txR = (txR0 - PowerScale * ThePowerValue * dx);
                                        tyR = -1 * (tyR0 - PowerScale * ThePowerValue * dy);
                                        tzR = -1 * (tzR0 - PowerScale * ThePowerValue * dz);
                                    }
                                    if (((MainForm)mainForm).FrameElement[i].LoadDDirection[j] == 3)//على محور X
                                    {
                                        txR = (txR0 - PowerScale * ThePowerValue);
                                        tyR = -1 * (tyR0);
                                        tzR = -1 * (tzR0);
                                    }
                                    if (((MainForm)mainForm).FrameElement[i].LoadDDirection[j] == 4)//على محور Y
                                    {
                                        txR = (txR0);
                                        tyR = -1 * (tyR0 - PowerScale * ThePowerValue);
                                        tzR = -1 * (tzR0);
                                    }
                                    if (((MainForm)mainForm).FrameElement[i].LoadDDirection[j] == 5)//على محور Z
                                    {
                                        txR = (txR0);
                                        tyR = -1 * (tyR0);
                                        tzR = -1 * (tzR0 + PowerScale * ThePowerValue);
                                    }
                                    Point3dM = new Math3DP.PointG(txR, tyR, tzR);
                                    Point3dM.RotateX = Myglobals.tXValue;
                                    Point3dM.RotateY = Myglobals.tYValue;
                                    Point3dM.RotateZ = Myglobals.tZValue;
                                    Point3dM.DrawPoint();
                                    tx2 = Myglobals.TheX3d;
                                    ty2 = Myglobals.TheY3d;
                                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                                    px1[k] = tx1;
                                    py1[k] = ty1;
                                    px2[k] = tx2;
                                    py2[k] = ty2;
                                    if (ThePowerValue > 0)
                                    {
                                        if (Frame.ShowPowerValue == 1) g.DrawString(ThePowerValue.ToString(), drawFont, drawBrush, tx2 - 10, ty2 + 5);
                                        if (((MainForm)mainForm).FrameElement[i].LoadDDirection[j] == 0)//على محور 1
                                        {
                                            txR = (txR0 - 0.2 * dx - 0.2 * dx1);
                                            tyR = -1 * (tyR0 - 0.2 * dy - 0.2 * dy1);
                                            tzR = -1 * (tzR0 - 0.2 * dz - 0.2 * dz1);
                                            txRR = (txR0 - 0.2 * dx + 0.2 * dx1);
                                            tyRR = -1 * (tyR0 - 0.2 * dy + 0.2 * dy1);
                                            tzRR = -1 * (tzR0 - 0.2 * dz + 0.2 * dz1);
                                        }
                                        if (((MainForm)mainForm).FrameElement[i].LoadDDirection[j] == 1)//على محور 2
                                        {
                                            txR = (txR0 - 0.2 * dx - 0.2 * dx1);
                                            tyR = -1 * (tyR0 - 0.2 * dy - 0.2 * dy1);
                                            tzR = -1 * (tzR0 - 0.2 * dz - 0.2 * dz1);
                                            txRR = (txR0 - 0.2 * dx + 0.2 * dx1);
                                            tyRR = -1 * (tyR0 - 0.2 * dy + 0.2 * dy1);
                                            tzRR = -1 * (tzR0 - 0.2 * dz + 0.2 * dz1);
                                        }
                                        if (((MainForm)mainForm).FrameElement[i].LoadDDirection[j] == 2)//على محور 3
                                        {
                                            txR = (txR0 - 0.2 * dx - 0.2 * dx1);
                                            tyR = -1 * (tyR0 - 0.2 * dy - 0.2 * dy1);
                                            tzR = -1 * (tzR0 - 0.2 * dz - 0.2 * dz1);
                                            txRR = (txR0 - 0.2 * dx + 0.2 * dx1);
                                            tyRR = -1 * (tyR0 - 0.2 * dy + 0.2 * dy1);
                                            tzRR = -1 * (tzR0 - 0.2 * dz + 0.2 * dz1);
                                        }
                                        if (((MainForm)mainForm).FrameElement[i].LoadDDirection[j] == 3)//على محور X
                                        {
                                            txR = (txR0 - 0.2);
                                            tyR = -1 * (tyR0 - 0.2);
                                            tzR = -1 * (tzR0);
                                            txRR = (txR0 - 0.2);
                                            tyRR = -1 * (tyR0 + 0.2);
                                            tzRR = -1 * (tzR0);
                                        }
                                        if (((MainForm)mainForm).FrameElement[i].LoadDDirection[j] == 4)//على محور Y
                                        {
                                            txR = (txR0 - 0.2);
                                            tyR = -1 * (tyR0 - 0.2);
                                            tzR = -1 * (tzR0);
                                            txRR = (txR0 + 0.2);
                                            tyRR = -1 * (tyR0 - 0.2);
                                            tzRR = -1 * (tzR0);
                                        }
                                        if (((MainForm)mainForm).FrameElement[i].LoadDDirection[j] == 5)//على محور Z
                                        {
                                            txR = (txR0 - 0.2);
                                            tyR = -1 * (tyR0);
                                            tzR = -1 * (tzR0 + 0.2);
                                            txRR = (txR0 + 0.2);
                                            tyRR = -1 * (tyR0);
                                            tzRR = -1 * (tzR0 + 0.2);
                                        }
                                    }
                                    if (ThePowerValue < 0)
                                    {
                                        if (Frame.ShowPowerValue == 1) g.DrawString(ThePowerValue.ToString(), drawFont, drawBrush, tx2 - 10, ty2 - 15);
                                        if (((MainForm)mainForm).FrameElement[i].LoadDDirection[j] == 0)//على محور 1
                                        {
                                            txR = (txR0 + 0.2 * dx - 0.2 * dx1);
                                            tyR = -1 * (tyR0 + 0.2 * dy - 0.2 * dy1);
                                            tzR = -1 * (tzR0 + 0.2 * dz - 0.2 * dz1);
                                            txRR = (txR0 + 0.2 * dx + 0.2 * dx1);
                                            tyRR = -1 * (tyR0 + 0.2 * dy + 0.2 * dy1);
                                            tzRR = -1 * (tzR0 + 0.2 * dz + 0.2 * dz1);
                                        }
                                        if (((MainForm)mainForm).FrameElement[i].LoadDDirection[j] == 1)//على محور 2
                                        {
                                            txR = (txR0 + 0.2 * dx - 0.2 * dx1);
                                            tyR = -1 * (tyR0 + 0.2 * dy - 0.2 * dy1);
                                            tzR = -1 * (tzR0 + 0.2 * dz - 0.2 * dz1);
                                            txRR = (txR0 + 0.2 * dx + 0.2 * dx1);
                                            tyRR = -1 * (tyR0 + 0.2 * dy + 0.2 * dy1);
                                            tzRR = -1 * (tzR0 + 0.2 * dz + 0.2 * dz1);
                                        }
                                        if (((MainForm)mainForm).FrameElement[i].LoadDDirection[j] == 2)//على محور 3
                                        {
                                            txR = (txR0 + 0.2 * dx - 0.2 * dx1);
                                            tyR = -1 * (tyR0 + 0.2 * dy - 0.2 * dy1);
                                            tzR = -1 * (tzR0 + 0.2 * dz - 0.2 * dz1);
                                            txRR = (txR0 + 0.2 * dx + 0.2 * dx1);
                                            tyRR = -1 * (tyR0 + 0.2 * dy + 0.2 * dy1);
                                            tzRR = -1 * (tzR0 + 0.2 * dz + 0.2 * dz1);
                                        }
                                        if (((MainForm)mainForm).FrameElement[i].LoadDDirection[j] == 3)//على محور X
                                        {
                                            txR = (txR0 + 0.2);
                                            tyR = -1 * (tyR0 - 0.2);
                                            tzR = -1 * (tzR0);
                                            txRR = (txR0 + 0.2);
                                            tyRR = -1 * (tyR0 + 0.2);
                                            tzRR = -1 * (tzR0);
                                        }
                                        if (((MainForm)mainForm).FrameElement[i].LoadDDirection[j] == 4)//على محور Y
                                        {
                                            txR = (txR0 - 0.2);
                                            tyR = -1 * (tyR0 + 0.2);
                                            tzR = -1 * (tzR0);
                                            txRR = (txR0 + 0.2);
                                            tyRR = -1 * (tyR0 + 0.2);
                                            tzRR = -1 * (tzR0);
                                        }
                                        if (((MainForm)mainForm).FrameElement[i].LoadDDirection[j] == 5)//على محور Z
                                        {
                                            txR = (txR0 - 0.2);
                                            tyR = -1 * (tyR0);
                                            tzR = -1 * (tzR0 - 0.2);
                                            txRR = (txR0 + 0.2);
                                            tyRR = -1 * (tyR0);
                                            tzRR = -1 * (tzR0 - 0.2);
                                        }
                                    }
                                    if (ThePowerValue != 0)
                                    {
                                        Point3dM = new Math3DP.PointG(txR, tyR, tzR);
                                        Point3dM.RotateX = Myglobals.tXValue;
                                        Point3dM.RotateY = Myglobals.tYValue;
                                        Point3dM.RotateZ = Myglobals.tZValue;
                                        Point3dM.DrawPoint();
                                        tx2 = Myglobals.TheX3d;
                                        ty2 = Myglobals.TheY3d;
                                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                                        Point3dM = new Math3DP.PointG(txRR, tyRR, tzRR);
                                        Point3dM.RotateX = Myglobals.tXValue;
                                        Point3dM.RotateY = Myglobals.tYValue;
                                        Point3dM.RotateZ = Myglobals.tZValue;
                                        Point3dM.DrawPoint();
                                        tx2 = Myglobals.TheX3d;
                                        ty2 = Myglobals.TheY3d;
                                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                                    }
                                }
                            endI: { };
                                for (int k = 1; k < 6; k++)
                                {
                                    Point[] P = new Point[5];
                                    P[0].X = px2[k];
                                    P[0].Y = py2[k];
                                    P[4].X = P[0].X;
                                    P[4].Y = P[0].Y;
                                    P[1].X = px2[k + 1];
                                    P[1].Y = py2[k + 1];
                                    P[2].X = px1[k + 1];
                                    P[2].Y = py1[k + 1];
                                    P[3].X = px1[k];
                                    P[3].Y = py1[k];
                                    if (tahkik == 0 & k == 4) goto endII;
                                    g.DrawLine(pen, px2[k], py2[k], px2[k + 1], py2[k + 1]);
                                    using (Brush brush = new SolidBrush(Color.FromArgb(90, Color.Green)))
                                    {
                                        g.FillPolygon(brush, P);
                                    }
                                }
                            endII: { };
                            }
                            #endregion
                        }
                    }
                }
            }
            catch { }
        }
        public void DrawFloor2d()
        {
            Graphics g = Graphics.FromImage(((MainForm)mainForm).pictureBox1.Image);
            Pen pen = new Pen(Color.Black, 1f);
            //-----------------------------------
            int tx1 = 0;
            int ty1 = 0;
            int tx2 = 0;
            int ty2 = 0;
            //خطوط المحيطية بالبلاطة فقط عند تحديدها
            for (int i = 1; i < Shell.Number + 1; i++)
            {
                if (Shell.Visible[i] == 0)
                {
                    //بلاطة
                    if (Shell.Type[i] == 1 & Joint.ZReal[Shell.JointNo[i, 1]] == Myglobals.StoryLevel[Myglobals.SelectedStory])
                    {
                        for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                        {
                            if (Shell.SelectedLine[i, j] == 1)
                            {
                                tx1 = Joint.X2d[Shell.JointNo[i, j]];
                                ty1 = Joint.Y2d[Shell.JointNo[i, j]];
                                if (j != Shell.PointNumbers[i])
                                {
                                    tx2 = Joint.X2d[Shell.JointNo[i, j + 1]];
                                    ty2 = Joint.Y2d[Shell.JointNo[i, j + 1]];
                                }
                                else
                                {
                                    tx2 = Joint.X2d[Shell.JointNo[i, 1]];
                                    ty2 = Joint.Y2d[Shell.JointNo[i, 1]];
                                }
                                float[] dashValues = { 5, 3, 5, 3 };
                                pen = new Pen(Color.Black, 2f);
                                pen.DashPattern = dashValues;
                                g.DrawLine(pen, tx1, ty1, tx2, ty2);
                            }
                        }
                        //رسم البلاطات على المستوي        
                        Point[] P = new Point[Shell.PointNumbers[i] + 2];
                        for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                        {
                            P[j].X = Joint.X2d[Shell.JointNo[i, j]];
                            P[j].Y = Joint.Y2d[Shell.JointNo[i, j]];
                        }
                        P[Shell.PointNumbers[i] + 1].X = P[1].X;
                        P[Shell.PointNumbers[i] + 1].Y = P[1].Y;
                        using (Brush brush = new SolidBrush(Color.FromArgb(30, Color.Gray)))
                        {
                            g.FillPolygon(brush, P);
                        }
                    }
                    //جدار
                    if (Shell.Type[i] == 2)
                    {
                        double MaxZ = -1000000;
                        double selectedZ = -1000000;
                        int theX1 = 0;
                        int theY1 = 0;
                        int theX2 = 0;
                        int theY2 = 0;
                        int SELECTpoint = 0;

                        for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                        {
                            if (selectedZ < Joint.ZReal[Shell.JointNo[i, j]]) selectedZ = Joint.ZReal[Shell.JointNo[i, j]];
                        }
                        if (selectedZ == Myglobals.StoryLevel[Myglobals.SelectedStory])
                        {
                            for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                            {
                                if (Joint.ZReal[Shell.JointNo[i, j]] == Myglobals.StoryLevel[Myglobals.SelectedStory] & SELECTpoint == 0)
                                {
                                    theX1 = Joint.X2d[Shell.JointNo[i, j]];
                                    theY1 = Joint.Y2d[Shell.JointNo[i, j]];
                                    SELECTpoint = 1;
                                    goto Nexj;
                                }
                                if (Joint.ZReal[Shell.JointNo[i, j]] == Myglobals.StoryLevel[Myglobals.SelectedStory] & SELECTpoint == 1)
                                {
                                    theX2 = Joint.X2d[Shell.JointNo[i, j]];
                                    theY2 = Joint.Y2d[Shell.JointNo[i, j]];
                                    SELECTpoint = 2;
                                }
                            Nexj: { }
                            }
                               // if (Shell.SelectedLine[i, j] == 1)
                                {
                                    tx1 = theX1;
                                    ty1 = theY1;
                                    tx2 = theX2;
                                    ty2 = theY2;
                                    float[] dashValues = { 5, 3, 5, 3 };
                                    pen = new Pen(Color.Black, 2f);
                                   // pen.DashPattern = dashValues;
                                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                                }
                            //رسم الجدار على المستوي        
                          /*
                            Point[] P = new Point[Shell.PointNumbers[i] + 2];
                            for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                            {
                                P[j].X = Joint.X2d[Shell.JointNo[i, j]];
                                P[j].Y = Joint.Y2d[Shell.JointNo[i, j]];
                            }
                            P[Shell.PointNumbers[i] + 1].X = P[1].X;
                            P[Shell.PointNumbers[i] + 1].Y = P[1].Y;
                            using (Brush brush = new SolidBrush(Color.FromArgb(30, Color.Gray)))
                            {
                                g.FillPolygon(brush, P);
                            }
                            */

                        }
                    }
                }
            }
        }


   
        
        
        
        public void DrawFloorELEV()
        {
            double tx1D = Myglobals.elevPointX[Myglobals.Selectedelev, 1];
            double ty1D = Myglobals.elevPointY[Myglobals.Selectedelev, 1];
            double tz1D = Myglobals.elevPointZ[Myglobals.Selectedelev, 1];
            double tx2D = Myglobals.elevPointX[Myglobals.Selectedelev, 1 + Myglobals.StoryNumbers];
            double ty2D = Myglobals.elevPointY[Myglobals.Selectedelev, 1 + Myglobals.StoryNumbers];
            double tz2D = Myglobals.elevPointZ[Myglobals.Selectedelev, 1 + Myglobals.StoryNumbers];
            double tx3D = Myglobals.elevPointX[Myglobals.Selectedelev, 2 + Myglobals.StoryNumbers];
            double ty3D = Myglobals.elevPointY[Myglobals.Selectedelev, 2 + Myglobals.StoryNumbers];
            double tz3D = Myglobals.elevPointZ[Myglobals.Selectedelev, 2 + Myglobals.StoryNumbers];
            double var2 = tx2D - tx1D;
            double var3 = tx3D - tx1D;
            double var5 = ty2D - ty1D;
            double var6 = ty3D - ty1D;
            double var8 = tz2D - tz1D;
            double var9 = tz3D - tz1D;
            double varA = var5 * var9 - var8 * var6;//a
            double varB = var8 * var3 - var2 * var9;//b
            double varC = var2 * var6 - var5 * var3;//c
            double varD = -varA * tx1D - varB * ty1D - varC * tz1D;//d  

            Graphics g = Graphics.FromImage(((MainForm)mainForm).pictureBox5.Image);
            Pen pen = new Pen(Color.Black, 1f);
            //-----------------------------------
            int tx1 = 0;
            int ty1 = 0;
            int tx2 = 0;
            int ty2 = 0;
            //خطوط المحيطية بالبلاطة فقط عند تحديدها
            #region//الجدران أولا
            for (int i = 1; i < Shell.Number + 1; i++)
            {
                if (Shell.Visible[i] == 0)
                {
                    if (Shell.Type[i] == 2 || Shell.Type[i] == 3)
                    {
                        for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                        {
                            int tah1 = 0;
                            int tah2 = 0;
                            int tah = 0;
                            double xx = varA * Joint.XReal[Shell.JointNo[i, j]];
                            double yy = varB * Joint.YReal[Shell.JointNo[i, j]];
                            double zz = varC * Joint.ZReal[Shell.JointNo[i, j]];
                            double sm = xx + yy + zz;
                            if (Math.Abs(sm + varD) <= 0.5) tah1 = 1;
                            if (j < Shell.PointNumbers[i])
                            {
                                xx = varA * Joint.XReal[Shell.JointNo[i, j + 1]];
                                yy = varB * Joint.YReal[Shell.JointNo[i, j + 1]];
                                zz = varC * Joint.ZReal[Shell.JointNo[i, j + 1]];
                            }
                            else
                            {
                                xx = varA * Joint.XReal[Shell.JointNo[i, 1]];
                                yy = varB * Joint.YReal[Shell.JointNo[i, 1]];
                                zz = varC * Joint.ZReal[Shell.JointNo[i, 1]];
                            }
                            sm = xx + yy + zz;
                            if (Math.Abs(sm + varD) <= 0.5) tah2 = 1;
                            if (tah1 == 1 & tah2 == 1) tah = 1;
                            if (tah == 0) goto nexti;
                        }
                        for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                        {
                            if (Shell.SelectedLine[i, j] == 1)
                            {
                                tx1 = Joint.Xelev[Shell.JointNo[i, j]];
                                ty1 = Joint.Yelev[Shell.JointNo[i, j]];
                                if (j != Shell.PointNumbers[i])
                                {
                                    tx2 = Joint.Xelev[Shell.JointNo[i, j + 1]];
                                    ty2 = Joint.Yelev[Shell.JointNo[i, j + 1]];
                                }
                                else
                                {
                                    tx2 = Joint.Xelev[Shell.JointNo[i, 1]];
                                    ty2 = Joint.Yelev[Shell.JointNo[i, 1]];
                                }
                                float[] dashValues = { 5, 3, 5, 3 };
                                pen = new Pen(Color.Black, 2f);
                                pen.DashPattern = dashValues;
                                g.DrawLine(pen, tx1, ty1, tx2, ty2);
                            }
                        }
                        //رسم البلاطات على المستوي        
                        Point[] P = new Point[Shell.PointNumbers[i] + 2];
                        for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                        {
                            P[j].X = Joint.Xelev[Shell.JointNo[i, j]];
                            P[j].Y = Joint.Yelev[Shell.JointNo[i, j]];
                        }
                        P[Shell.PointNumbers[i] + 1].X = P[1].X;
                        P[Shell.PointNumbers[i] + 1].Y = P[1].Y;
                        if (Shell.Type[i] == 2)
                        {
                            using (Brush brush = new SolidBrush(Color.FromArgb(90, Color.Red)))
                            {
                                g.FillPolygon(brush, P);
                            }
                        }
                        if (Shell.Type[i] == 3)
                        {
                            HatchBrush hBrush = new HatchBrush(HatchStyle.Percent50, Color.Brown, Color.FromArgb(20, Color.White));
                            g.FillPolygon(hBrush, P);
                        }
                    }
                }
            nexti: { };
            }
            #endregion
            #region//الفتحات ثانيا
            for (int i = 1; i < Shell.Number + 1; i++)
            {
                if (Shell.Visible[i] == 0)
                {
                    if (Shell.Type[i] == 4)
                    {
                        for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                        {
                            int tah1 = 0;
                            int tah2 = 0;
                            int tah = 0;
                            double xx = varA * Joint.XReal[Shell.JointNo[i, j]];
                            double yy = varB * Joint.YReal[Shell.JointNo[i, j]];
                            double zz = varC * Joint.ZReal[Shell.JointNo[i, j]];
                            double sm = xx + yy + zz;
                            if (Math.Abs(sm + varD) <= 0.5) tah1 = 1;
                            if (j < Shell.PointNumbers[i])
                            {
                                xx = varA * Joint.XReal[Shell.JointNo[i, j + 1]];
                                yy = varB * Joint.YReal[Shell.JointNo[i, j + 1]];
                                zz = varC * Joint.ZReal[Shell.JointNo[i, j + 1]];
                            }
                            else
                            {
                                xx = varA * Joint.XReal[Shell.JointNo[i, 1]];
                                yy = varB * Joint.YReal[Shell.JointNo[i, 1]];
                                zz = varC * Joint.ZReal[Shell.JointNo[i, 1]];
                            }
                            sm = xx + yy + zz;
                            if (Math.Abs(sm + varD) <= 0.5) tah2 = 1;
                            if (tah1 == 1 & tah2 == 1) tah = 1;
                            if (tah == 0) goto nexti;
                        }
                        for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                        {
                            if (Shell.SelectedLine[i, j] == 1)
                            {
                                tx1 = Joint.Xelev[Shell.JointNo[i, j]];
                                ty1 = Joint.Yelev[Shell.JointNo[i, j]];
                                if (j != Shell.PointNumbers[i])
                                {
                                    tx2 = Joint.Xelev[Shell.JointNo[i, j + 1]];
                                    ty2 = Joint.Yelev[Shell.JointNo[i, j + 1]];
                                }
                                else
                                {
                                    tx2 = Joint.Xelev[Shell.JointNo[i, 1]];
                                    ty2 = Joint.Yelev[Shell.JointNo[i, 1]];
                                }
                                float[] dashValues = { 5, 3, 5, 3 };
                                pen = new Pen(Color.Black, 2f);
                                pen.DashPattern = dashValues;
                                g.DrawLine(pen, tx1, ty1, tx2, ty2);
                            }
                        }
                        //رسم البلاطات على المستوي        
                        Point[] P = new Point[Shell.PointNumbers[i] + 2];
                        for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                        {
                            P[j].X = Joint.Xelev[Shell.JointNo[i, j]];
                            P[j].Y = Joint.Yelev[Shell.JointNo[i, j]];
                        }
                        P[Shell.PointNumbers[i] + 1].X = P[1].X;
                        P[Shell.PointNumbers[i] + 1].Y = P[1].Y;
                            using (Brush brush = new SolidBrush(Color.White))
                            {
                                g.FillPolygon(brush, P);
                            }
                    }
                }
            nexti: { };
            }
            #endregion
        }
        public void Renderelev()
        {
            PlaneAriaelev();
            CalculateGridPointelev();
            CalculateJointelev();
            if (Myglobals.ShowEleveWindow == 1)
            {
                FindAriasELEV();
                DrowGridelev();
                DrawBeamelev();
                DrawFloorELEV();
                DrawJointelev();
            }
            if (Myglobals.IfAnalysis == 1 & Myglobals.DrawDiagram == 1)
            {
                AnalysisModel callmee1 = new AnalysisModel();
                callmee1.DrawResultselev();
            }
        }
        public void Render2d()
        {
                PlaneAria2d();
                // CalculateGridPointReal();
                CalculateIntersectionPoint();
                CalculateGridPoint2d();
                CalculateGridLine2d();
                CalculateJoint2d();
                if (Myglobals.ShowPlaneWindow == 1)
                {
                    DrowGrid2d();
                    DrawBeam2d();
                    DrawFloor2d();
                    DrawJoint2d();
                }
        }
        public void Render3d()
        {
            PlaneAria3d();
            CalculateGridPoint3d();
            CalculateGridLine3d();
            CalculateJoint3d();
            CalculateSpecialaxesBeam();
            if (Myglobals.Show3DWindow == 1)
            {
                FaceNumber = 0;
                if (Myglobals.SelectedPlan == "Plan") DrawSelectedFloor();
                DrawGrid3d();
                DrawBeam3d();
                if (Myglobals.ExtrudedFrame == 1) DrawCube3d();
                DrawFloor3d();
                if (Myglobals.ExtrudedShell == 1) DrawCubeFloor3d();
                DrawJoint3d();
                DrawFaces3d();
                if (Frame.Localaxis == 1) DrawSpecialaxesBeam3d();
                if (Shell.Localaxis == 1) DrawSpecialaxesShell3d();
                if (Myglobals.ShllAnalysisMeshVeiw == 1) DrawMeshShell3d();
            }
        }
        public void DrawSelectedFloor()
        {
            Graphics g = Graphics.FromImage(((MainForm)mainForm).pictureBox2.Image);
            Point[] P = new Point[6];
            for (int i = 1; i < 5; i++)//-----------------------------حدود الشكل
            {
                double tx1 = Myglobals.OutAriaX[i] ;
                double ty1 = -1 * Myglobals.OutAriaY[i] ;
                double tz1 = -1 * Myglobals.StoryLevel[Myglobals.SelectedStory] ;
                Math3DP.PointG Point3dM = new Math3DP.PointG(tx1, ty1, tz1);
                Point3dM.RotateX = Myglobals.tXValue;
                Point3dM.RotateY = Myglobals.tYValue;
                Point3dM.RotateZ = Myglobals.tZValue;
                Point3dM.DrawPoint();
                P[i].X = Myglobals.TheX3d;
                P[i].Y = Myglobals.TheY3d;
            }
            P[5].X = P[1].X;
            P[5].Y = P[1].Y;
            using (Brush brush = new SolidBrush(Color.FromArgb(70, Color.Cyan)))
            {
                g.FillPolygon(brush, P);
            }
            g.Dispose();
        }

        public void CalculateSpecialaxesBeam()
        {
            double tx = 0;
            double ty = 0;
            double tz = 0;
            int tx1 = 0;
            int ty1 = 0;
            int tx2 = 0;
            int ty2 = 0;
            double tx1R = 0;
            double ty1R = 0;
            double tz1R = 0;
            double tx2R = 0;
            double ty2R = 0;
            double tz2R = 0;
            double txR = 0;
            double tyR = 0;
            double tzR = 0;
            double txR0 = 0;
            double tyR0 = 0;
            double tzR0 = 0;
            double tx0 = 0;
            double ty0 = 0;
            double tz0 = 0;
            for (int i = 1; i < Frame.Number + 1; i++)
            {
                FrameElements emp = new FrameElements();
                emp = ((MainForm)mainForm).FrameElement[i];
                double txAx2 = 0;
                double tyAx2 = 0;
                double tzAx2 = 0;
                double txAx3 = 0;
                double tyAx3 = 0;
                double tzAx3 = 0;
                tx1R = Joint.XReal[emp.FirstJoint];
                ty1R = Joint.YReal[emp.FirstJoint];
                tz1R = Joint.ZReal[emp.FirstJoint];
                tx2R = Joint.XReal[emp.SecondJoint];
                ty2R = Joint.YReal[emp.SecondJoint];
                tz2R = Joint.ZReal[emp.SecondJoint];
                double BeamLength = (Math.Sqrt(Math.Pow(tx1R - tx2R, 2) + Math.Pow(ty1R - ty2R, 2) + Math.Pow(tz1R - tz2R, 2)));
                string DESIGNATION = Section.DESIGNATION[emp.Section];
                double D = Section.D[emp.Section] / 100;
                if (D == 0) D = Section.HT[emp.Section] / 100;
                double BF = Section.BF[emp.Section] / 100;
                if (BF == 0) BF = Section.B[emp.Section] / 100;
                double BFB = Section.BFB[emp.Section] / 100;
                double TF = Section.TF[emp.Section] / 100;
                double TFB = Section.TFB[emp.Section] / 100;
                double TW = Section.TW[emp.Section] / 100;
                double X = Section.X[emp.Section] / 100;
                double Y = Section.Y[emp.Section] / 100;
                double angelRotate = emp.RotateAngel * Math.PI / 180;////
                Point[] P1 = new Point[8];
                /////محور 1
                tx0 = (tx1R + tx2R) / 2;
                ty0 = (ty1R + ty2R) / 2;
                tz0 = (tz1R + tz2R) / 2;
                txR = (tx0);
                tyR = -1 * (ty0);
                tzR = -1 * (tz0);
                Math3DP.PointG Point3dM1 = new Math3DP.PointG(txR, tyR, tzR);
                Point3dM1.RotateX = Myglobals.tXValue;
                Point3dM1.RotateY = Myglobals.tYValue;
                Point3dM1.RotateZ = Myglobals.tZValue;
                Point3dM1.DrawPoint();
                tx1 = Myglobals.TheX3d;
                ty1 = Myglobals.TheY3d;
                txR0 = tx0 + (1 / BeamLength) * (tx2R - tx1R);
                tyR0 = ty0 + (1 / BeamLength) * (ty2R - ty1R);
                tzR0 = tz0 + (1 / BeamLength) * (tz2R - tz1R);
                txR = (txR0);
                tyR = -1 * (tyR0);
                tzR = -1 * (tzR0);
                Point3dM1 = new Math3DP.PointG(txR, tyR, tzR);
                Point3dM1.RotateX = Myglobals.tXValue;
                Point3dM1.RotateY = Myglobals.tYValue;
                Point3dM1.RotateZ = Myglobals.tZValue;
                Point3dM1.DrawPoint();
                tx2 = Myglobals.TheX3d;
                ty2 = Myglobals.TheY3d;
                emp.AxisX1[1] = tx0;
                emp.AxisY1[1] = ty0;
                emp.AxisZ1[1] = tz0;
                emp.AxisX2[1] = txR0;
                emp.AxisY2[1] = tyR0;
                emp.AxisZ2[1] = tzR0;
                emp.AxisX13d[1] = tx1;
                emp.AxisY13d[1] = ty1;
                emp.AxisX23d[1] = tx2;
                emp.AxisY23d[1] = ty2;
                if (DESIGNATION == "R")
                {
                    #region//شكل مستطيل
                    double xDiff = tx1R - tx2R;
                    double yDiff = ty1R - ty2R;
                    double zDiff = tz1R - tz2R;
                    double LDiff = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));
                    double angel = Math.Atan2(yDiff, xDiff);
                    double angel1 = Math.Atan2(zDiff, LDiff);
                    int thekind = 0;
                    if (tx1R == tx2R & ty1R == ty2R)//عمود
                    {
                        angelRotate = -angelRotate + Math.PI / 2;
                        if (tz1R < tz2R)
                        {
                            thekind = 1;
                        }
                        else
                        {
                            thekind = 2;
                        }
                    }
                    double radious = Math.Sqrt(Math.Pow(D / 2, 2) + Math.Pow(BF / 2, 2));
                    double angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2(D / 2, BF / 2);
                    double angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                    double thelength = 2 * radious * Math.Sin(angelRotate / 2);
                    double DfBR = thelength * Math.Cos(angel2);
                    double DfDR = thelength * Math.Sin(angel2);
                    double DfBL = thelength * Math.Sin(angel3);
                    double DfDL = thelength * Math.Cos(angel3);
                    tx = tx1R;
                    ty = ty1R;
                    tz = tz1R;
                    for (int j = 1; j < 9; j++)
                    {
                        if (j > 4)
                        {
                            tx = tx2R;
                            ty = ty2R;
                            tz = tz2R;
                        }
                        if (j == 1 || j == 5)
                        {
                            txR = tx - (BF / 2 + DfBL) * Math.Sin(angel) - ((D / 2 - DfDL) * Math.Sin(angel1) * Math.Cos(angel));
                            tyR = ty + (BF / 2 + DfBL) * Math.Cos(angel) - ((D / 2 - DfDL) * Math.Sin(angel1) * Math.Sin(angel));
                            tzR = tz + (D / 2 - DfDL) * Math.Cos(angel1);
                            if (thekind == 0)//جائز
                            {
                                txAx2 = txAx2 + txR;
                                tyAx2 = tyAx2 + tyR;
                                tzAx2 = tzAx2 + tzR;
                                txAx3 = txAx3 + txR;
                                tyAx3 = tyAx3 + tyR;
                                tzAx3 = tzAx3 + tzR;
                            }
                            if (thekind == 1)//عمود
                            {
                                txAx3 = txAx3 + txR;
                                tyAx3 = tyAx3 + tyR;
                                tzAx3 = tzAx3 + tzR;
                            }
                            if (thekind == 2)//عمود
                            {
                                txAx2 = txAx2 + txR;
                                tyAx2 = tyAx2 + tyR;
                                tzAx2 = tzAx2 + tzR;
                            }
                        }
                        if (j == 2 || j == 6)
                        {
                            txR = tx + (BF / 2 - DfBR) * Math.Sin(angel) - ((D / 2 + DfDR) * Math.Sin(angel1) * Math.Cos(angel));
                            tyR = ty - (BF / 2 - DfBR) * Math.Cos(angel) - ((D / 2 + DfDR) * Math.Sin(angel1) * Math.Sin(angel));
                            tzR = tz + (D / 2 + DfDR) * Math.Cos(angel1);
                            if (thekind == 0)//جائز
                            {
                                txAx2 = txAx2 + txR;
                                tyAx2 = tyAx2 + tyR;
                                tzAx2 = tzAx2 + tzR;
                            }
                            if (thekind == 1)//عمود
                            {
                                txAx2 = txAx2 + txR;
                                tyAx2 = tyAx2 + tyR;
                                tzAx2 = tzAx2 + tzR;
                                txAx3 = txAx3 + txR;
                                tyAx3 = tyAx3 + tyR;
                                tzAx3 = tzAx3 + tzR;
                            }
                        }
                        if (j == 3 || j == 7)
                        {
                            txR = tx + (BF / 2 + DfBL) * Math.Sin(angel) + ((D / 2 - DfDL) * Math.Sin(angel1) * Math.Cos(angel));
                            tyR = ty - (BF / 2 + DfBL) * Math.Cos(angel) + ((D / 2 - DfDL) * Math.Sin(angel1) * Math.Sin(angel));
                            tzR = tz - (D / 2 - DfDL) * Math.Cos(angel1);
                            if (thekind == 1)//عمود
                            {
                                txAx2 = txAx2 + txR;
                                tyAx2 = tyAx2 + tyR;
                                tzAx2 = tzAx2 + tzR;
                            }
                            if (thekind == 2)//عمود
                            {
                                txAx3 = txAx3 + txR;
                                tyAx3 = tyAx3 + tyR;
                                tzAx3 = tzAx3 + tzR;
                            }
                        }
                        if (j == 4 || j == 8)
                        {
                            txR = tx - (BF / 2 - DfBR) * Math.Sin(angel) + ((D / 2 + DfDR) * Math.Sin(angel1) * Math.Cos(angel));
                            tyR = ty + (BF / 2 - DfBR) * Math.Cos(angel) + ((D / 2 + DfDR) * Math.Sin(angel1) * Math.Sin(angel));
                            tzR = tz - (D / 2 + DfDR) * Math.Cos(angel1);
                            if (thekind == 0)//جائز
                            {
                                txAx3 = txAx3 + txR;
                                tyAx3 = tyAx3 + tyR;
                                tzAx3 = tzAx3 + tzR;
                            }
                            if (thekind == 2)//عمود
                            {
                                txAx2 = txAx2 + txR;
                                tyAx2 = tyAx2 + tyR;
                                tzAx2 = tzAx2 + tzR;
                                txAx3 = txAx3 + txR;
                                tyAx3 = tyAx3 + tyR;
                                tzAx3 = tzAx3 + tzR;
                            }
                        }
                    }
                    #endregion
                }
                //محور 2
                txAx2 = txAx2 / 4;
                tyAx2 = tyAx2 / 4;
                tzAx2 = tzAx2 / 4;
                BeamLength = (Math.Sqrt(Math.Pow(txAx2 - tx0, 2) + Math.Pow(tyAx2 - ty0, 2) + Math.Pow(tzAx2 - tz0, 2)));
                txR0 = tx0 + (1 / BeamLength) * (txAx2 - tx0);
                tyR0 = ty0 + (1 / BeamLength) * (tyAx2 - ty0);
                tzR0 = tz0 + (1 / BeamLength) * (tzAx2 - tz0);
                txR = (txR0);
                tyR = -1 * (tyR0);
                tzR = -1 * (tzR0);
                Point3dM1 = new Math3DP.PointG(txR, tyR, tzR);
                Point3dM1.RotateX = Myglobals.tXValue;
                Point3dM1.RotateY = Myglobals.tYValue;
                Point3dM1.RotateZ = Myglobals.tZValue;
                Point3dM1.DrawPoint();
                tx2 = Myglobals.TheX3d;
                ty2 = Myglobals.TheY3d;
                emp.AxisX1[2] = tx0;
                emp.AxisY1[2] = ty0;
                emp.AxisZ1[2] = tz0;
                emp.AxisX2[2] = txR0;
                emp.AxisY2[2] = tyR0;
                emp.AxisZ2[2] = tzR0;
                emp.AxisX13d[2] = tx1;
                emp.AxisY13d[2] = ty1;
                emp.AxisX23d[2] = tx2;
                emp.AxisY23d[2] = ty2;

                //محور 3
                txAx3 = txAx3 / 4;
                tyAx3 = tyAx3 / 4;
                tzAx3 = tzAx3 / 4;
                BeamLength = (Math.Sqrt(Math.Pow(txAx3 - tx0, 2) + Math.Pow(tyAx3 - ty0, 2) + Math.Pow(tzAx3 - tz0, 2)));
                txR0 = tx0 + (1 / BeamLength) * (txAx3 - tx0);
                tyR0 = ty0 + (1 / BeamLength) * (tyAx3 - ty0);
                tzR0 = tz0 + (1 / BeamLength) * (tzAx3 - tz0);
                txR = (txR0);
                tyR = -1 * (tyR0);
                tzR = -1 * (tzR0);
                Point3dM1 = new Math3DP.PointG(txR, tyR, tzR);
                Point3dM1.RotateX = Myglobals.tXValue;
                Point3dM1.RotateY = Myglobals.tYValue;
                Point3dM1.RotateZ = Myglobals.tZValue;
                Point3dM1.DrawPoint();
                tx2 = Myglobals.TheX3d;
                ty2 = Myglobals.TheY3d;
                emp.AxisX1[3] = tx0;
                emp.AxisY1[3] = ty0;
                emp.AxisZ1[3] = tz0;
                emp.AxisX2[3] = txR0;
                emp.AxisY2[3] = tyR0;
                emp.AxisZ2[3] = tzR0;
                emp.AxisX13d[3] = tx1;
                emp.AxisY13d[3] = ty1;
                emp.AxisX23d[3] = tx2;
                emp.AxisY23d[3] = ty2;
                ((MainForm)mainForm).FrameElement[i]=emp;
            }
        }
        public void DrawSpecialaxesBeam3d()
        {
            {
                Graphics g = Graphics.FromImage(((MainForm)mainForm).pictureBox2.Image);
                Pen pen = new Pen(Color.Blue, 1f);
                int tx1 = 0;
                int ty1 = 0;
                int tx2 = 0;
                int ty2 = 0;
                for (int i = 1; i < Frame.Number + 1; i++)
                {
                    FrameElements emp = new FrameElements();
                    emp = ((MainForm)mainForm).FrameElement[i];
                    if (emp.Visible == 0)
                    {
                        /////محور 1
                        tx1 = emp.AxisX13d[1];
                        ty1 = emp.AxisY13d[1];
                        tx2 = emp.AxisX23d[1];
                        ty2 = emp.AxisY23d[1];
                        pen = new Pen(Color.Red, 2f);
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        //محور 2
                        tx1 = emp.AxisX13d[2];
                        ty1 = emp.AxisY13d[2];
                        tx2 = emp.AxisX23d[2];
                        ty2 = emp.AxisY23d[2];
                        pen = new Pen(Color.Green, 2f);
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        //محور 3
                        tx1 = emp.AxisX13d[3];
                        ty1 = emp.AxisY13d[3];
                        tx2 = emp.AxisX23d[3];
                        ty2 = emp.AxisY23d[3];
                        pen = new Pen(Color.Blue, 2f);
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    }
                }
            }
        }

        int PP01 = 0;
        int PP02 = 0;
        int PP03 = 0;
        int PP04 = 0;
        int TopPointsNO = 0;
        int ButtomPointsNO = 0;
        public int SelectedShell = 0;
        public void CalculatePropertiesShell()
        {
            int i = SelectedShell;
            {
                int PP1 = 0;
                int PP2 = 0;
                int PP3 = 0;
                int PP4 = 0;
                double A1 = 0;
                double A2 = 0;
                double A3 = 0;
                double A4 = 0;
                double A5 = 0;
                double A6 = 0;
                int P1 = 0;
                int P2 = 0;
                double XFORCG = 0;
                double YFORCG = 0;
                double ZFORCG = 0;
                double PERIMETER = 0;

                double tx1R = 0;
                double ty1R = 0;
                double tz1R = 0;
                double tx2R = 0;
                double ty2R = 0;
                double tz2R = 0;
                double tx0 = 0;
                double ty0 = 0;
                double tz0 = 0;
                double txR0 = 0;
                double tyR0 = 0;
                double tzR0 = 0;
                for (int j = 1; j < Shell.PointNumbers[i]; j++)
                {
                    P1 = Shell.JointNo[i, j];
                    P2 = Shell.JointNo[i, j + 1];
                    A1 = A1 + Joint.XReal[P1] * Joint.YReal[P2];
                    A2 = A2 + Joint.YReal[P1] * Joint.XReal[P2];
                    A3 = A3 + Joint.XReal[P1] * Joint.ZReal[P2];
                    A4 = A4 + Joint.ZReal[P1] * Joint.XReal[P2];
                    A5 = A5 + Joint.YReal[P1] * Joint.ZReal[P2];
                    A6 = A6 + Joint.ZReal[P1] * Joint.YReal[P2];
                    XFORCG = XFORCG + Joint.XReal[P1];
                    YFORCG = YFORCG + Joint.YReal[P1];
                    ZFORCG = ZFORCG + Joint.ZReal[P1];
                    PERIMETER = PERIMETER + Math.Sqrt(Math.Pow(Joint.XReal[P1] - Joint.XReal[P2], 2) + Math.Pow(Joint.YReal[P1] - Joint.YReal[P2], 2) + Math.Pow(Joint.ZReal[P1] - Joint.ZReal[P2], 2));
                }
                P1 = Shell.JointNo[i, Shell.PointNumbers[i]];
                P2 = Shell.JointNo[i, 1];
                A1 = A1 + Joint.XReal[P1] * Joint.YReal[P2];
                A2 = A2 + Joint.YReal[P1] * Joint.XReal[P2];
                A3 = A3 + Joint.XReal[P1] * Joint.ZReal[P2];
                A4 = A4 + Joint.ZReal[P1] * Joint.XReal[P2];
                A5 = A5 + Joint.YReal[P1] * Joint.ZReal[P2];
                A6 = A6 + Joint.ZReal[P1] * Joint.YReal[P2];
                XFORCG = XFORCG + Joint.XReal[P1];
                YFORCG = YFORCG + Joint.YReal[P1];
                ZFORCG = ZFORCG + Joint.ZReal[P1];
                Shell.Aria[i] = Math.Round(0.5 * Math.Sqrt(Math.Pow((A1 - A2), 2) + Math.Pow((A3 - A4), 2) + Math.Pow((A5 - A6), 2)), 3);
                Shell.CenterX[i] = Math.Round((XFORCG) / Shell.PointNumbers[i], 3);
                Shell.CenterY[i] = Math.Round((YFORCG) / Shell.PointNumbers[i], 3);
                Shell.CenterZ[i] = Math.Round((ZFORCG) / Shell.PointNumbers[i], 3);
                PERIMETER = PERIMETER + Math.Sqrt(Math.Pow(Joint.XReal[P1] - Joint.XReal[P2], 2) + Math.Pow(Joint.YReal[P1] - Joint.YReal[P2], 2) + Math.Pow(Joint.ZReal[P1] - Joint.ZReal[P2], 2));
                Shell.Perimeter[i] = Math.Round(PERIMETER, 3);
                /////////////////////////////////////////////////////////////////////
                double thick = 1;
                int firstpoint = 1;
                int secondjoint = 2;
                double tx1D = Joint.XReal[Shell.JointNo[i, 1]];
                double ty1D = Joint.YReal[Shell.JointNo[i, 1]];
                double tz1D = Joint.ZReal[Shell.JointNo[i, 1]];
                double tx2D = Joint.XReal[Shell.JointNo[i, 2]];
                double ty2D = Joint.YReal[Shell.JointNo[i, 2]];
                double tz2D = Joint.ZReal[Shell.JointNo[i, 2]];
                double tx3D = 0;
                double ty3D = 0;
                double tz3D = 0;
                for (int k = 1; k < Shell.PointNumbers[i] + 1; k++)
                {
                    if (k != firstpoint & k != secondjoint)
                    {
                        tx3D = Joint.XReal[Shell.JointNo[i, k]];
                        ty3D = Joint.YReal[Shell.JointNo[i, k]];
                        tz3D = Joint.ZReal[Shell.JointNo[i, k]];
                        double kx = (tx3D - tx1D);
                        double ky = (ty3D - ty1D);
                        double kz = (tz3D - tz1D);
                        double kV = 0;
                        if (kx != 0) kV = (tx2D - tx1D) / kx;
                        if (ky != 0) kV = (ty2D - ty1D) / ky;
                        if (kz != 0) kV = (tz2D - tz1D) / kz;
                        int tah = 0;
                        if (kV * kx == (tx2D - tx1D) & kV * ky == (ty2D - ty1D) & kV * kz == (tz2D - tz1D)) tah = 1;
                        if (tah == 0) break;
                    }
                }
                double var2 = tx2D - tx1D;
                double var3 = tx3D - tx1D;
                double var5 = ty2D - ty1D;
                double var6 = ty3D - ty1D;
                double var8 = tz2D - tz1D;
                double var9 = tz3D - tz1D;
                double varA = var5 * var9 - var8 * var6;//a
                double varB = var8 * var3 - var2 * var9;//b
                double varC = var2 * var6 - var5 * var3;//c
                double varD = -varA * tx1D - varB * ty1D - varC * tz1D;//d                  
                double varT = thick / Math.Sqrt(Math.Pow(varA, 2) + Math.Pow(varB, 2) + Math.Pow(varC, 2));
                double tx0D = 0;
                double ty0D = 0;
                double tz0D = 0;
                tx1D = Shell.CenterX[i];
                ty1D = Shell.CenterY[i];
                tz1D = Shell.CenterZ[i];
                tx0D = tx1D + varA * varT;
                ty0D = ty1D + varB * varT;
                tz0D = tz1D + varC * varT;
                Shell.AxisX1[i, 3] = tx1D;
                Shell.AxisY1[i, 3] = ty1D;
                Shell.AxisZ1[i, 3] = tz1D;
                Shell.AxisX2[i, 3] = tx0D;
                Shell.AxisY2[i, 3] = ty0D;
                Shell.AxisZ2[i, 3] = tz0D;
                ////////////////////////////////////////////////////////////////////////////////////////////
                double MinX = 1000000;
                double MinY = 1000000;
                double MinZ = 1000000;
                double MaxX = -1000000;
                double MaxY = -1000000;
                double MaxZ = -1000000;
                double BeamLength = 0;
                for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                {
                    P1 = Shell.JointNo[i, j];
                    tx1R = Joint.XReal[P1]; ;
                    ty1R = Joint.YReal[P1]; ;
                    tz1R = Joint.ZReal[P1]; ;
                    BeamLength = (Math.Sqrt(Math.Pow(tx1R - tx1D, 2) + Math.Pow(ty1R - ty1D, 2) + Math.Pow(tz1R - tz1D, 2)));
                    if (MinZ > Joint.ZReal[P1])
                    {
                        MinZ = Joint.ZReal[P1];
                    }
                    if (MaxZ < Joint.ZReal[P1])
                    {
                        MaxZ = Joint.ZReal[P1];
                    }
                    if (MinX > Joint.XReal[P1]) MinX = Joint.XReal[P1];
                    if (MinY > Joint.YReal[P1]) MinY = Joint.YReal[P1];
                    if (MaxX < Joint.XReal[P1]) MaxX = Joint.XReal[P1];
                    if (MaxY < Joint.YReal[P1]) MaxY = Joint.YReal[P1];
                }
                TopPointsNO = 0;
                int[] TopPoints = new int[100];
                ButtomPointsNO = 0;
                int[] ButtomPoints = new int[100];
                for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                {
                    P1 = Shell.JointNo[i, j];
                    if (MinZ == Joint.ZReal[P1])
                    {
                        ButtomPointsNO = ButtomPointsNO + 1;
                        ButtomPoints[ButtomPointsNO] = P1;
                    }
                    if (MaxZ == Joint.ZReal[P1])
                    {
                        TopPointsNO = TopPointsNO + 1;
                        TopPoints[TopPointsNO] = P1;
                    }
                }

                int len = Shell.PointNumbers[i];
                int[] cArray = new int[len + 1];
                int[] dArray = new int[len + 1];
                double[] Distance = new double[len + 1];
                for (int j = 1; j < len + 1; j++)
                {
                    P1 = Shell.JointNo[i, j];
                    cArray[j] = j;
                    dArray[j] = P1;
                    Distance[j] = Joint.ZReal[P1];
                }
                var newList = cArray.OrderBy(x => Distance[x]).ToList();
                if (len == 4)
                {
                    PP01 = dArray[newList[4]];
                    PP02 = dArray[newList[3]];
                    PP03 = dArray[newList[2]];
                    PP04 = dArray[newList[1]];
                }
                if (len == 3)
                {
                    PP01 = dArray[newList[3]];
                    PP02 = dArray[newList[2]];
                    PP03 = dArray[newList[1]];
                }

                for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                {
                    P1 = Shell.JointNo[i, j];
                    if (MaxZ == Joint.ZReal[P1]) PP01 = P1;
                    if (MinZ == Joint.ZReal[P1]) PP04 = P1;
                }
                for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                {
                    P1 = Shell.JointNo[i, j];
                    if (MaxZ == Joint.ZReal[P1] & PP01 != P1) PP02 = P1;
                    if (MinZ == Joint.ZReal[P1] & PP04 != P1) PP03 = P1;

                    if (MinZ < Joint.ZReal[P1] & MaxZ > Joint.ZReal[P1] & PP02 == 0) PP04 = P1;
                }

                double X1 = 0;
                double Y1 = 0;
                double Z1 = 0;
                double X2 = 0;
                double Y2 = 0;
                double Z2 = 0;
                double X3 = 0;
                double Y3 = 0;
                double Z3 = 0;
                double Z = MinZ;//اعتبرنا بلاطة زيد نفسو   
                if (MinZ == MaxZ)
                {
                    Shell.EnvelpoX[i, 1] = Math.Round(MaxX, 3);
                    Shell.EnvelpoY[i, 1] = Math.Round(MaxY, 3);
                    Shell.EnvelpoX[i, 2] = Math.Round(MinX, 3);
                    Shell.EnvelpoY[i, 2] = Math.Round(MaxY, 3);
                    Shell.EnvelpoX[i, 3] = Math.Round(MinX, 3);
                    Shell.EnvelpoY[i, 3] = Math.Round(MinY, 3);
                    Shell.EnvelpoX[i, 4] = Math.Round(MaxX, 3);
                    Shell.EnvelpoY[i, 4] = Math.Round(MinY, 3);
                    Shell.EnvelpoZ[i, 1] = Z;
                    Shell.EnvelpoZ[i, 2] = Z;
                    Shell.EnvelpoZ[i, 3] = Z;
                    Shell.EnvelpoZ[i, 4] = Z;
                    Shell.AxisZ2[i, 3] = Z + 1;
                    goto endz;
                }
                PP1 = TopPoints[1];
                PP2 = TopPoints[TopPointsNO];
                Shell.EnvelpoX[i, 1] = Joint.XReal[PP1];
                Shell.EnvelpoY[i, 1] = Joint.YReal[PP1];
                Shell.EnvelpoZ[i, 1] = Joint.ZReal[PP1];
                Shell.EnvelpoX[i, 2] = Joint.XReal[PP2];
                Shell.EnvelpoY[i, 2] = Joint.YReal[PP2];
                Shell.EnvelpoZ[i, 2] = Joint.ZReal[PP2];
                if (TopPointsNO == 1)
                {
                    double zz = Joint.ZReal[PP1];
                    double xx = Joint.XReal[PP1] + 1;
                    double yy = -(varA * xx + varC * zz + varD) / varB;
                    if (varB == 0)
                    {
                        yy = Joint.YReal[PP1] + 1;
                        xx = Joint.XReal[PP1];
                    }
                    Shell.EnvelpoX[i, 2] = xx;
                    Shell.EnvelpoY[i, 2] = yy;
                    Shell.EnvelpoZ[i, 2] = zz;
                }
                PP3 = ButtomPoints[1];
                PP4 = ButtomPoints[ButtomPointsNO];
                Shell.EnvelpoX[i, 3] = Joint.XReal[PP3];
                Shell.EnvelpoY[i, 3] = Joint.YReal[PP3];
                Shell.EnvelpoZ[i, 3] = Joint.ZReal[PP3];
                Shell.EnvelpoX[i, 4] = Joint.XReal[PP4];
                Shell.EnvelpoY[i, 4] = Joint.YReal[PP4];
                Shell.EnvelpoZ[i, 4] = Joint.ZReal[PP4];
                if (ButtomPointsNO == 1)
                {
                    double zz = Joint.ZReal[PP3];
                    double xx = Joint.XReal[PP3] + 1;
                    double yy = -(varA * xx + varC * zz + varD) / varB;
                    if (varB == 0)
                    {
                        yy = Joint.YReal[PP3] + 1;
                        xx = Joint.XReal[PP3];
                    }
                    Shell.EnvelpoX[i, 4] = xx;
                    Shell.EnvelpoY[i, 4] = yy;
                    Shell.EnvelpoZ[i, 4] = zz;
                }
                X1 = Shell.EnvelpoX[i, 1];
                Y1 = Shell.EnvelpoY[i, 1];
                Z1 = Shell.EnvelpoZ[i, 1];
                X2 = Shell.EnvelpoX[i, 2];
                Y2 = Shell.EnvelpoY[i, 2];
                Z2 = Shell.EnvelpoZ[i, 2];
                X3 = Shell.CenterX[i];
                Y3 = Shell.CenterY[i];
                Z3 = Shell.CenterZ[i];
                FindPointOnLine3D(X1, Y1, Z1, X2, Y2, Z2, X3, Y3, Z3);
                double XT = Xr4;
                double YT = Yr4;
                double ZT = Zr4;
                double XT1 = Xr5;
                double YT1 = Yr5;
                double ZT1 = Zr5;
                X1 = Shell.EnvelpoX[i, 3];
                Y1 = Shell.EnvelpoY[i, 3];
                Z1 = Shell.EnvelpoZ[i, 3];
                X2 = Shell.EnvelpoX[i, 4];
                Y2 = Shell.EnvelpoY[i, 4];
                Z2 = Shell.EnvelpoZ[i, 4];
                X3 = Shell.CenterX[i]; ;
                Y3 = Shell.CenterY[i]; ;
                Z3 = Shell.CenterZ[i]; ;
                FindPointOnLine3D(X1, Y1, Z1, X2, Y2, Z2, X3, Y3, Z3);
                double XB = Xr4;
                double YB = Yr4;
                double ZB = Zr4;
                double XB1 = Xr5;
                double YB1 = Yr5;
                double ZB1 = Zr5;
                Shell.EnvelpoX[i, 1] = XT1;
                Shell.EnvelpoY[i, 1] = YT1;
                Shell.EnvelpoZ[i, 1] = ZT1;
                Shell.EnvelpoX[i, 2] = XT;
                Shell.EnvelpoY[i, 2] = YT;
                Shell.EnvelpoZ[i, 2] = ZT;
                Shell.EnvelpoX[i, 3] = XB;
                Shell.EnvelpoY[i, 3] = YB;
                Shell.EnvelpoZ[i, 3] = ZB;
                Shell.EnvelpoX[i, 4] = XB1;
                Shell.EnvelpoY[i, 4] = YB1;
                Shell.EnvelpoZ[i, 4] = ZB1;
            ////////////////////////////////////////////////////////
            endz: { };
                 tx1R = Shell.EnvelpoX[i, 2];
                 ty1R = Shell.EnvelpoY[i, 2];
                 tz1R = Shell.EnvelpoZ[i, 2];
                 tx2R = Shell.EnvelpoX[i, 1];
                 ty2R = Shell.EnvelpoY[i, 1];
                 tz2R = Shell.EnvelpoZ[i, 1];
                 BeamLength = (Math.Sqrt(Math.Pow(tx1R - tx2R, 2) + Math.Pow(ty1R - ty2R, 2) + Math.Pow(tz1R - tz2R, 2)));
                if (BeamLength == 0)
                {
                    tx1R = Shell.EnvelpoX[i, 4];
                    ty1R = Shell.EnvelpoY[i, 4];
                    tz1R = Shell.EnvelpoZ[i, 4];
                    tx2R = Shell.EnvelpoX[i, 3];
                    ty2R = Shell.EnvelpoY[i, 3];
                    tz2R = Shell.EnvelpoZ[i, 3];
                    BeamLength = (Math.Sqrt(Math.Pow(tx1R - tx2R, 2) + Math.Pow(ty1R - ty2R, 2) + Math.Pow(tz1R - tz2R, 2)));
                }
                /////محور 1
                tx0 = Shell.CenterX[i];
                ty0 = Shell.CenterY[i];
                tz0 = Shell.CenterZ[i];
                txR0 = tx0 + (1 / BeamLength) * (tx2R - tx1R);
                tyR0 = ty0 + (1 / BeamLength) * (ty2R - ty1R);
                tzR0 = tz0 + (1 / BeamLength) * (tz2R - tz1R);
                Shell.AxisX1[i, 1] = tx0;
                Shell.AxisY1[i, 1] = ty0;
                Shell.AxisZ1[i, 1] = tz0;
                Shell.AxisX2[i, 1] = txR0;
                Shell.AxisY2[i, 1] = tyR0;
                Shell.AxisZ2[i, 1] = tzR0;
                //محور 2
                tx1R = Shell.EnvelpoX[i, 3];
                ty1R = Shell.EnvelpoY[i, 3];
                tz1R = Shell.EnvelpoZ[i, 3];
                tx2R = Shell.EnvelpoX[i, 2];
                ty2R = Shell.EnvelpoY[i, 2];
                tz2R = Shell.EnvelpoZ[i, 2];
                BeamLength = (Math.Sqrt(Math.Pow(tx1R - tx2R, 2) + Math.Pow(ty1R - ty2R, 2) + Math.Pow(tz1R - tz2R, 2)));
                txR0 = tx0 + (1 / BeamLength) * (tx2R - tx1R);
                tyR0 = ty0 + (1 / BeamLength) * (ty2R - ty1R);
                tzR0 = tz0 + (1 / BeamLength) * (tz2R - tz1R);
                Shell.AxisX1[i, 2] = tx0;
                Shell.AxisY1[i, 2] = ty0;
                Shell.AxisZ1[i, 2] = tz0;
                Shell.AxisX2[i, 2] = txR0;
                Shell.AxisY2[i, 2] = tyR0;
                Shell.AxisZ2[i, 2] = tzR0;
            }
        }
        public void CalculateShellMeshLines()
        {
            //for (int i = 1; i < Shell.Number + 1; i++)

           int PP1= PP01 ;
           int PP2 = PP02;
           int PP3 = PP03;
           int PP4 = PP04;
            int i = SelectedShell;
            {
                /////////////////////////////////////////////////////////////////////
                ////////////////////////////////////////////////////////////////////////////////////////////
                double LX = 0;
                double LY = 0;
                double LZ = 0;
                double LEMDA = 0;
                double X1 = 0;
                double Y1 = 0;
                double Z1 = 0;
                double X2 = 0;
                double Y2 = 0;
                double Z2 = 0;
                double X3 = 0;
                double Y3 = 0;
                double Z3 = 0;
                double X4 = 0;
                double Y4 = 0;
                double Z4 = 0;
                double BeamLength = 0;
                Myglobals.NumberOfMeshLinesX = 8;
                Myglobals.NumberOfMeshLinesY = 4;
                #region //بلاطة أفقية
                if (Shell.Type[i] == 1)
                {
                    X1 = Shell.EnvelpoX[i, 1];
                    Y1 = Shell.EnvelpoY[i, 1];
                    Z1 = Shell.EnvelpoZ[i, 1];
                    X2 = Shell.EnvelpoX[i, 2];
                    Y2 = Shell.EnvelpoY[i, 2];
                    Z2 = Shell.EnvelpoZ[i, 2];
                    BeamLength = (Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2) + Math.Pow(Z2 - Z1, 2)));
                    LX = (X2 - X1) / BeamLength;
                    LY = (Y2 - Y1) / BeamLength;
                    LZ = (Z2 - Z1) / BeamLength;
                    LEMDA = BeamLength / Myglobals.NumberOfMeshLinesX;
                    Shell.MeshLineXx1[i, 1] = X1;
                    Shell.MeshLineXy1[i, 1] = Y1;
                    Shell.MeshLineXz1[i, 1] = Z1;
                    Shell.MeshLineXx1[i, Myglobals.NumberOfMeshLinesX + 1] = X2;
                    Shell.MeshLineXy1[i, Myglobals.NumberOfMeshLinesX + 1] = Y2;
                    Shell.MeshLineXz1[i, Myglobals.NumberOfMeshLinesX + 1] = Z2;
                    for (int j = 2; j < Myglobals.NumberOfMeshLinesX + 1; j++)
                    {
                        Shell.MeshLineXx1[i, j] = X1 + LX * (j - 1) * LEMDA;
                        Shell.MeshLineXy1[i, j] = Y1 + LY * (j - 1) * LEMDA;
                        Shell.MeshLineXz1[i, j] = Z1 + LZ * (j - 1) * LEMDA;
                    }
                    X1 = Shell.EnvelpoX[i, 4];
                    Y1 = Shell.EnvelpoY[i, 4];
                    Z1 = Shell.EnvelpoZ[i, 4];
                    X2 = Shell.EnvelpoX[i, 3];
                    Y2 = Shell.EnvelpoY[i, 3];
                    Z2 = Shell.EnvelpoZ[i, 3];
                    BeamLength = (Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2) + Math.Pow(Z2 - Z1, 2)));
                    LX = (X2 - X1) / BeamLength;
                    LY = (Y2 - Y1) / BeamLength;
                    LZ = (Z2 - Z1) / BeamLength;
                    LEMDA = BeamLength / Myglobals.NumberOfMeshLinesX;
                    Shell.MeshLineXx2[i, 1] = X1;
                    Shell.MeshLineXy2[i, 1] = Y1;
                    Shell.MeshLineXz2[i, 1] = Z1;
                    Shell.MeshLineXx2[i, Myglobals.NumberOfMeshLinesX + 1] = X2;
                    Shell.MeshLineXy2[i, Myglobals.NumberOfMeshLinesX + 1] = Y2;
                    Shell.MeshLineXz2[i, Myglobals.NumberOfMeshLinesX + 1] = Z2;
                    for (int j = 2; j < Myglobals.NumberOfMeshLinesX + 1; j++)
                    {
                        Shell.MeshLineXx2[i, j] = X1 + LX * (j - 1) * LEMDA;
                        Shell.MeshLineXy2[i, j] = Y1 + LY * (j - 1) * LEMDA;
                        Shell.MeshLineXz2[i, j] = Z1 + LZ * (j - 1) * LEMDA;
                    }
                    ////////////////////////////////
                    X1 = Shell.EnvelpoX[i, 1];
                    Y1 = Shell.EnvelpoY[i, 1];
                    Z1 = Shell.EnvelpoZ[i, 1];
                    X2 = Shell.EnvelpoX[i, 4];
                    Y2 = Shell.EnvelpoY[i, 4];
                    Z2 = Shell.EnvelpoZ[i, 4];
                    BeamLength = (Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2) + Math.Pow(Z2 - Z1, 2)));
                    LX = (X2 - X1) / BeamLength;
                    LY = (Y2 - Y1) / BeamLength;
                    LZ = (Z2 - Z1) / BeamLength;
                    LEMDA = BeamLength / Myglobals.NumberOfMeshLinesY;
                    Shell.MeshLineYx1[i, 1] = X1;
                    Shell.MeshLineYy1[i, 1] = Y1;
                    Shell.MeshLineYz1[i, 1] = Z1;
                    Shell.MeshLineYx1[i, Myglobals.NumberOfMeshLinesY + 1] = X2;
                    Shell.MeshLineYy1[i, Myglobals.NumberOfMeshLinesY + 1] = Y2;
                    Shell.MeshLineYz1[i, Myglobals.NumberOfMeshLinesY + 1] = Z2;
                    for (int j = 2; j < Myglobals.NumberOfMeshLinesY + 1; j++)
                    {
                        Shell.MeshLineYx1[i, j] = X1 + LX * (j - 1) * LEMDA;
                        Shell.MeshLineYy1[i, j] = Y1 + LY * (j - 1) * LEMDA;
                        Shell.MeshLineYz1[i, j] = Z1 + LZ * (j - 1) * LEMDA;
                    }
                    //////**********
                    X1 = Shell.EnvelpoX[i, 2];
                    Y1 = Shell.EnvelpoY[i, 2];
                    Z1 = Shell.EnvelpoZ[i, 2];
                    X2 = Shell.EnvelpoX[i, 3];
                    Y2 = Shell.EnvelpoY[i, 3];
                    Z2 = Shell.EnvelpoZ[i, 3];
                    BeamLength = (Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2) + Math.Pow(Z2 - Z1, 2)));
                    LX = (X2 - X1) / BeamLength;
                    LY = (Y2 - Y1) / BeamLength;
                    LZ = (Z2 - Z1) / BeamLength;
                    LEMDA = BeamLength / Myglobals.NumberOfMeshLinesY;
                    Shell.MeshLineYx2[i, 1] = X1;
                    Shell.MeshLineYy2[i, 1] = Y1;
                    Shell.MeshLineYz2[i, 1] = Z1;
                    Shell.MeshLineYx2[i, Myglobals.NumberOfMeshLinesY + 1] = X2;
                    Shell.MeshLineYy2[i, Myglobals.NumberOfMeshLinesY + 1] = Y2;
                    Shell.MeshLineYz2[i, Myglobals.NumberOfMeshLinesY + 1] = Z2;
                    for (int j = 2; j < Myglobals.NumberOfMeshLinesY + 1; j++)
                    {
                        Shell.MeshLineYx2[i, j] = X1 + LX * (j - 1) * LEMDA;
                        Shell.MeshLineYy2[i, j] = Y1 + LY * (j - 1) * LEMDA;
                        Shell.MeshLineYz2[i, j] = Z1 + LZ * (j - 1) * LEMDA;
                    }
                    goto endz;
                }
                #endregion
                #region//جدار أريع نقاط

                if (Shell.PointNumbers[i] == 4)
                {
                    X1 = Joint.XReal[PP1];
                    Y1 = Joint.YReal[PP1];
                    Z1 = Joint.ZReal[PP1];
                    X2 = Joint.XReal[PP2];
                    Y2 = Joint.YReal[PP2];
                    Z2 = Joint.ZReal[PP2];
                    BeamLength = (Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2) + Math.Pow(Z2 - Z1, 2)));
                    LX = (X2 - X1) / BeamLength;
                    LY = (Y2 - Y1) / BeamLength;
                    LZ = (Z2 - Z1) / BeamLength;
                    LEMDA = BeamLength / Myglobals.NumberOfMeshLinesX;
                    Shell.MeshLineXx1[i, 1] = X1;
                    Shell.MeshLineXy1[i, 1] = Y1;
                    Shell.MeshLineXz1[i, 1] = Z1;
                    Shell.MeshLineXx1[i, Myglobals.NumberOfMeshLinesX + 1] = X2;
                    Shell.MeshLineXy1[i, Myglobals.NumberOfMeshLinesX + 1] = Y2;
                    Shell.MeshLineXz1[i, Myglobals.NumberOfMeshLinesX + 1] = Z2;
                    for (int j = 2; j < Myglobals.NumberOfMeshLinesX + 1; j++)
                    {
                        Shell.MeshLineXx1[i, j] = X1 + LX * (j - 1) * LEMDA;
                        Shell.MeshLineXy1[i, j] = Y1 + LY * (j - 1) * LEMDA;
                        Shell.MeshLineXz1[i, j] = Z1 + LZ * (j - 1) * LEMDA;
                    }
                    //////**********
                    X1 = Joint.XReal[PP1];
                    Y1 = Joint.YReal[PP1];
                    Z1 = Joint.ZReal[PP1];
                    X2 = Joint.XReal[PP3];
                    Y2 = Joint.YReal[PP3];
                    Z2 = Joint.ZReal[PP3];
                    X3 = Joint.XReal[PP2];
                    Y3 = Joint.YReal[PP2];
                    Z3 = Joint.ZReal[PP2];
                    X4 = Joint.XReal[PP4];
                    Y4 = Joint.YReal[PP4];
                    Z4 = Joint.ZReal[PP4];
                    INTERSECTION = 0;
                    FindIntersectionPoint3D(X1, Y1, Z1, X2, Y2, Z2, X3, Y3, Z3, X4, Y4, Z4);
                    if (INTERSECTION == 1)
                    {
                        int yo = PP4;
                        PP4 = PP3;
                        PP3 = yo;
                    }
                    X1 = Joint.XReal[PP3];
                    Y1 = Joint.YReal[PP3];
                    Z1 = Joint.ZReal[PP3];
                    X2 = Joint.XReal[PP4];
                    Y2 = Joint.YReal[PP4];
                    Z2 = Joint.ZReal[PP4];
                    BeamLength = (Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2) + Math.Pow(Z2 - Z1, 2)));
                    LX = (X2 - X1) / BeamLength;
                    LY = (Y2 - Y1) / BeamLength;
                    LZ = (Z2 - Z1) / BeamLength;
                    LEMDA = BeamLength / Myglobals.NumberOfMeshLinesX;
                    Shell.MeshLineXx2[i, 1] = X1;
                    Shell.MeshLineXy2[i, 1] = Y1;
                    Shell.MeshLineXz2[i, 1] = Z1;
                    Shell.MeshLineXx2[i, Myglobals.NumberOfMeshLinesX + 1] = X2;
                    Shell.MeshLineXy2[i, Myglobals.NumberOfMeshLinesX + 1] = Y2;
                    Shell.MeshLineXz2[i, Myglobals.NumberOfMeshLinesX + 1] = Z2;
                    for (int j = 2; j < Myglobals.NumberOfMeshLinesX + 1; j++)
                    {
                        Shell.MeshLineXx2[i, j] = X1 + LX * (j - 1) * LEMDA;
                        Shell.MeshLineXy2[i, j] = Y1 + LY * (j - 1) * LEMDA;
                        Shell.MeshLineXz2[i, j] = Z1 + LZ * (j - 1) * LEMDA;
                    }
                    //////**********الأفقيات
                    X1 = Joint.XReal[PP1];
                    Y1 = Joint.YReal[PP1];
                    Z1 = Joint.ZReal[PP1];
                    X2 = Joint.XReal[PP3];
                    Y2 = Joint.YReal[PP3];
                    Z2 = Joint.ZReal[PP3];
                    BeamLength = (Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2) + Math.Pow(Z2 - Z1, 2)));
                    LX = (X2 - X1) / BeamLength;
                    LY = (Y2 - Y1) / BeamLength;
                    LZ = (Z2 - Z1) / BeamLength;
                    LEMDA = BeamLength / Myglobals.NumberOfMeshLinesY;
                    Shell.MeshLineYx1[i, 1] = X1;
                    Shell.MeshLineYy1[i, 1] = Y1;
                    Shell.MeshLineYz1[i, 1] = Z1;
                    Shell.MeshLineYx1[i, Myglobals.NumberOfMeshLinesY + 1] = X2;
                    Shell.MeshLineYy1[i, Myglobals.NumberOfMeshLinesY + 1] = Y2;
                    Shell.MeshLineYz1[i, Myglobals.NumberOfMeshLinesY + 1] = Z2;
                    for (int j = 2; j < Myglobals.NumberOfMeshLinesY + 1; j++)
                    {
                        Shell.MeshLineYx1[i, j] = X1 + LX * (j - 1) * LEMDA;
                        Shell.MeshLineYy1[i, j] = Y1 + LY * (j - 1) * LEMDA;
                        Shell.MeshLineYz1[i, j] = Z1 + LZ * (j - 1) * LEMDA;
                    }
                    //////**********
                    X1 = Joint.XReal[PP2];
                    Y1 = Joint.YReal[PP2];
                    Z1 = Joint.ZReal[PP2];
                    X2 = Joint.XReal[PP4];
                    Y2 = Joint.YReal[PP4];
                    Z2 = Joint.ZReal[PP4];
                    BeamLength = (Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2) + Math.Pow(Z2 - Z1, 2)));
                    LX = (X2 - X1) / BeamLength;
                    LY = (Y2 - Y1) / BeamLength;
                    LZ = (Z2 - Z1) / BeamLength;
                    LEMDA = BeamLength / Myglobals.NumberOfMeshLinesY;
                    Shell.MeshLineYx2[i, 1] = X1;
                    Shell.MeshLineYy2[i, 1] = Y1;
                    Shell.MeshLineYz2[i, 1] = Z1;
                    Shell.MeshLineYx2[i, Myglobals.NumberOfMeshLinesY + 1] = X2;
                    Shell.MeshLineYy2[i, Myglobals.NumberOfMeshLinesY + 1] = Y2;
                    Shell.MeshLineYz2[i, Myglobals.NumberOfMeshLinesY + 1] = Z2;
                    for (int j = 2; j < Myglobals.NumberOfMeshLinesY + 1; j++)
                    {
                        Shell.MeshLineYx2[i, j] = X1 + LX * (j - 1) * LEMDA;
                        Shell.MeshLineYy2[i, j] = Y1 + LY * (j - 1) * LEMDA;
                        Shell.MeshLineYz2[i, j] = Z1 + LZ * (j - 1) * LEMDA;
                    }
                }
                #endregion
                #region // جدار ثلاث نقاط
                if (Shell.PointNumbers[i] == 3)
                {
                    for (int j = 1; j < Myglobals.NumberOfMeshLinesX + 2; j++)
                    {
                        Shell.MeshLineXx1[i, j] = Joint.XReal[PP1];
                        Shell.MeshLineXy1[i, j] = Joint.YReal[PP1];
                        Shell.MeshLineXz1[i, j] = Joint.ZReal[PP1];
                    }
                    X1 = Joint.XReal[PP2];
                    Y1 = Joint.YReal[PP2];
                    Z1 = Joint.ZReal[PP2];
                    X2 = Joint.XReal[PP3];
                    Y2 = Joint.YReal[PP3];
                    Z2 = Joint.ZReal[PP3];
                    BeamLength = (Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2) + Math.Pow(Z2 - Z1, 2)));
                    LX = (X2 - X1) / BeamLength;
                    LY = (Y2 - Y1) / BeamLength;
                    LZ = (Z2 - Z1) / BeamLength;
                    LEMDA = BeamLength / Myglobals.NumberOfMeshLinesX;
                    Shell.MeshLineXx2[i, 1] = X1;
                    Shell.MeshLineXy2[i, 1] = Y1;
                    Shell.MeshLineXz2[i, 1] = Z1;
                    Shell.MeshLineXx2[i, Myglobals.NumberOfMeshLinesX + 1] = X2;
                    Shell.MeshLineXy2[i, Myglobals.NumberOfMeshLinesX + 1] = Y2;
                    Shell.MeshLineXz2[i, Myglobals.NumberOfMeshLinesX + 1] = Z2;
                    for (int j = 2; j < Myglobals.NumberOfMeshLinesX + 1; j++)
                    {
                        Shell.MeshLineXx2[i, j] = X1 + LX * (j - 1) * LEMDA;
                        Shell.MeshLineXy2[i, j] = Y1 + LY * (j - 1) * LEMDA;
                        Shell.MeshLineXz2[i, j] = Z1 + LZ * (j - 1) * LEMDA;
                    }
                    //////**********الأفقيات
                    X1 = Joint.XReal[PP1];
                    Y1 = Joint.YReal[PP1];
                    Z1 = Joint.ZReal[PP1];
                    X2 = Joint.XReal[PP2];
                    Y2 = Joint.YReal[PP2];
                    Z2 = Joint.ZReal[PP2];
                    BeamLength = (Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2) + Math.Pow(Z2 - Z1, 2)));
                    LX = (X2 - X1) / BeamLength;
                    LY = (Y2 - Y1) / BeamLength;
                    LZ = (Z2 - Z1) / BeamLength;
                    LEMDA = BeamLength / Myglobals.NumberOfMeshLinesY;
                    Shell.MeshLineYx1[i, 1] = X1;
                    Shell.MeshLineYy1[i, 1] = Y1;
                    Shell.MeshLineYz1[i, 1] = Z1;
                    Shell.MeshLineYx1[i, Myglobals.NumberOfMeshLinesY + 1] = X2;
                    Shell.MeshLineYy1[i, Myglobals.NumberOfMeshLinesY + 1] = Y2;
                    Shell.MeshLineYz1[i, Myglobals.NumberOfMeshLinesY + 1] = Z2;
                    for (int j = 2; j < Myglobals.NumberOfMeshLinesY + 1; j++)
                    {
                        Shell.MeshLineYx1[i, j] = X1 + LX * (j - 1) * LEMDA;
                        Shell.MeshLineYy1[i, j] = Y1 + LY * (j - 1) * LEMDA;
                        Shell.MeshLineYz1[i, j] = Z1 + LZ * (j - 1) * LEMDA;
                    }
                    //////**********
                    X1 = Joint.XReal[PP1];
                    Y1 = Joint.YReal[PP1];
                    Z1 = Joint.ZReal[PP1];
                    X2 = Joint.XReal[PP3];
                    Y2 = Joint.YReal[PP3];
                    Z2 = Joint.ZReal[PP3];
                    BeamLength = (Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2) + Math.Pow(Z2 - Z1, 2)));
                    LX = (X2 - X1) / BeamLength;
                    LY = (Y2 - Y1) / BeamLength;
                    LZ = (Z2 - Z1) / BeamLength;
                    LEMDA = BeamLength / Myglobals.NumberOfMeshLinesY;
                    Shell.MeshLineYx2[i, 1] = X1;
                    Shell.MeshLineYy2[i, 1] = Y1;
                    Shell.MeshLineYz2[i, 1] = Z1;
                    Shell.MeshLineYx2[i, Myglobals.NumberOfMeshLinesY + 1] = X2;
                    Shell.MeshLineYy2[i, Myglobals.NumberOfMeshLinesY + 1] = Y2;
                    Shell.MeshLineYz2[i, Myglobals.NumberOfMeshLinesY + 1] = Z2;
                    for (int j = 2; j < Myglobals.NumberOfMeshLinesY + 1; j++)
                    {
                        Shell.MeshLineYx2[i, j] = X1 + LX * (j - 1) * LEMDA;
                        Shell.MeshLineYy2[i, j] = Y1 + LY * (j - 1) * LEMDA;
                        Shell.MeshLineYz2[i, j] = Z1 + LZ * (j - 1) * LEMDA;
                    }
                }
                #endregion
            endz: { };
            }
        }
        public void CalculateMeshJoints()
        {
            double X1 = 0;
            double Y1 = 0;
            double Z1 = 0;
            double X2 = 0;
            double Y2 = 0;
            double Z2 = 0;
            double X3 = 0;
            double Y3 = 0;
            double Z3 = 0;
            double X4 = 0;
            double Y4 = 0;
            double Z4 = 0;
            int i = SelectedShell;
            {
                for (int j = 1; j < Myglobals.NumberOfMeshLinesX + 2; j++)
                {
                    X1 = Shell.MeshLineXx1[i, j];
                    Y1 = Shell.MeshLineXy1[i, j];
                    Z1 = Shell.MeshLineXz1[i, j];
                    X2 = Shell.MeshLineXx2[i, j];
                    Y2 = Shell.MeshLineXy2[i, j];
                    Z2 = Shell.MeshLineXz2[i, j];
                    for (int k = 1; k < Myglobals.NumberOfMeshLinesY + 2; k++)
                    {
                        X3 = Shell.MeshLineYx1[i, k];
                        Y3 = Shell.MeshLineYy1[i, k];
                        Z3 = Shell.MeshLineYz1[i, k];
                        X4 = Shell.MeshLineYx2[i, k];
                        Y4 = Shell.MeshLineYy2[i, k];
                        Z4 = Shell.MeshLineYz2[i, k];
                        INTERSECTION = 0;
                        FindIntersectionPoint3D(X1, Y1, Z1, X2, Y2, Z2, X3, Y3, Z3, X4, Y4, Z4);
                        if (INTERSECTION == 1)
                        {
                            int tah = 0;
                            int selectedjoint = 0;
                            for (int kk = 1; kk < JointMesh.Number + 1; kk++)
                            {
                                if (JointMesh.XReal[kk] == Xr4 & JointMesh.YReal[kk] == Yr4 & JointMesh.ZReal[kk] == Zr4)
                                {
                                    tah = 1;
                                    selectedjoint = kk;
                                    break;
                                }
                            }
                            if (tah == 0)
                            {
                                JointMesh.Number = JointMesh.Number + 1;
                                JointMesh.XReal[JointMesh.Number] = Xr4;
                                JointMesh.YReal[JointMesh.Number] = Yr4;
                                JointMesh.ZReal[JointMesh.Number] = Zr4;
                                JointMesh.Floor[JointMesh.Number] = i;
                            }
                        }
                    }
                }
            }
        }
        public void CalculateMeshJoint3d()
        {
            for (int i = 1; i < JointMesh.Number + 1; i++)
            {
                double tx1 = JointMesh.XReal[i];
                double ty1 = -1 * JointMesh.YReal[i];
                double tz1 = -1 * JointMesh.ZReal[i];
                Math3DP.PointG Joint3dM = new Math3DP.PointG(tx1, ty1, tz1);
                Joint3dM.RotateX = Myglobals.tXValue;
                Joint3dM.RotateY = Myglobals.tYValue;
                Joint3dM.RotateZ = Myglobals.tZValue;
                Joint3dM.DrawPoint();
                JointMesh.X3d[i] = Myglobals.TheX3d;
                JointMesh.Y3d[i] = Myglobals.TheY3d;
            }
        }
        
        public void DrawSpecialaxesShell3d()
        {
            if (Shell.Localaxis == 1)
            {
                Graphics g = Graphics.FromImage(((MainForm)mainForm).pictureBox2.Image);
                Pen pen = new Pen(Color.Blue, 1f);
                int tx1 = 0;
                int ty1 = 0;
                int tx2 = 0;
                int ty2 = 0;
                double txR = 0;
                double tyR = 0;
                double tzR = 0;
                double txR0 = 0;
                double tyR0 = 0;
                double tzR0 = 0;
                double tx0 = 0;
                double ty0 = 0;
                double tz0 = 0;
                for (int i = 1; i < Shell.Number + 1; i++)
                {
                    if (Shell.Visible[i] == 0)
                    {
                        Point[] P1 = new Point[8];
                        /////محور 1
                        tx0 = Shell.AxisX1[i, 1];
                        ty0 = Shell.AxisY1[i, 1];
                        tz0 = Shell.AxisZ1[i, 1];
                        txR = (tx0);
                        tyR = -1 * (ty0);
                        tzR = -1 * (tz0);
                        Math3DP.PointG Point3dM1 = new Math3DP.PointG(txR, tyR, tzR);
                        Point3dM1.RotateX = Myglobals.tXValue;
                        Point3dM1.RotateY = Myglobals.tYValue;
                        Point3dM1.RotateZ = Myglobals.tZValue;
                        Point3dM1.DrawPoint();
                        tx1 = Myglobals.TheX3d;
                        ty1 = Myglobals.TheY3d;
                        txR0 = Shell.AxisX2[i, 1];
                        tyR0 = Shell.AxisY2[i, 1];
                        tzR0 = Shell.AxisZ2[i, 1];
                        txR = (txR0);
                        tyR = -1 * (tyR0);
                        tzR = -1 * (tzR0);
                        Point3dM1 = new Math3DP.PointG(txR, tyR, tzR);
                        Point3dM1.RotateX = Myglobals.tXValue;
                        Point3dM1.RotateY = Myglobals.tYValue;
                        Point3dM1.RotateZ = Myglobals.tZValue;
                        Point3dM1.DrawPoint();
                        tx2 = Myglobals.TheX3d;
                        ty2 = Myglobals.TheY3d;
                        Shell.AxisX13d[i, 1] = tx1;
                        Shell.AxisY13d[i, 1] = ty1;
                        Shell.AxisX23d[i, 1] = tx2;
                        Shell.AxisY23d[i, 1] = ty2;
                        //محور 2
                        txR0 = Shell.AxisX2[i, 2];
                        tyR0 = Shell.AxisY2[i, 2];
                        tzR0 = Shell.AxisZ2[i, 2];
                        txR = (txR0);
                        tyR = -1 * (tyR0);
                        tzR = -1 * (tzR0);
                        Point3dM1 = new Math3DP.PointG(txR, tyR, tzR);
                        Point3dM1.RotateX = Myglobals.tXValue;
                        Point3dM1.RotateY = Myglobals.tYValue;
                        Point3dM1.RotateZ = Myglobals.tZValue;
                        Point3dM1.DrawPoint();
                        tx2 = Myglobals.TheX3d;
                        ty2 = Myglobals.TheY3d;
                        Shell.AxisX13d[i, 2] = tx1;
                        Shell.AxisY13d[i, 2] = ty1;
                        Shell.AxisX23d[i, 2] = tx2;
                        Shell.AxisY23d[i, 2] = ty2;
                        //محور 3
                        txR0 = Shell.AxisX2[i, 3];
                        tyR0 = Shell.AxisY2[i, 3];
                        tzR0 = Shell.AxisZ2[i, 3];
                        txR = (txR0);
                        tyR = -1 * (tyR0);
                        tzR = -1 * (tzR0);
                        Point3dM1 = new Math3DP.PointG(txR, tyR, tzR);
                        Point3dM1.RotateX = Myglobals.tXValue;
                        Point3dM1.RotateY = Myglobals.tYValue;
                        Point3dM1.RotateZ = Myglobals.tZValue;
                        Point3dM1.DrawPoint();
                        tx2 = Myglobals.TheX3d;
                        ty2 = Myglobals.TheY3d;
                        Shell.AxisX13d[i, 3] = tx1;
                        Shell.AxisY13d[i, 3] = ty1;
                        Shell.AxisX23d[i, 3] = tx2;
                        Shell.AxisY23d[i, 3] = ty2;
                        /////////////////////////////////////////////////////////////////////////الرسم
                        /////محور 1
                        tx1 = Shell.AxisX13d[i, 1];
                        ty1 = Shell.AxisY13d[i, 1];
                        tx2 = Shell.AxisX23d[i, 1];
                        ty2 = Shell.AxisY23d[i, 1];
                        pen = new Pen(Color.Red, 1f);
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        //محور 2
                        tx1 = Shell.AxisX13d[i, 2];
                        ty1 = Shell.AxisY13d[i, 2];
                        tx2 = Shell.AxisX23d[i, 2];
                        ty2 = Shell.AxisY23d[i, 2];
                        pen = new Pen(Color.Green, 1f);
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        //محور 3
                        tx1 = Shell.AxisX13d[i, 3];
                        ty1 = Shell.AxisY13d[i, 3];
                        tx2 = Shell.AxisX23d[i, 3];
                        ty2 = Shell.AxisY23d[i, 3];
                        pen = new Pen(Color.Blue, 1f);
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    }
                }
            }
        }
        public void DrawMeshShell3d()
        {
            Graphics g = Graphics.FromImage(((MainForm)mainForm).pictureBox2.Image);
            Pen pen = new Pen(Color.White, 1f);
            int tx1 = 0;
            int ty1 = 0;
            int tx2 = 0;
            int ty2 = 0;
            for (int i = 1; i < Shell.Number + 1; i++)
            {
                if (Shell.Visible[i] == 0)
                {
                    for (int j = 1; j < Myglobals.NumberOfMeshLinesX + 2; j++)
                    {
                        double txR = Shell.MeshLineXx1[i, j];
                        double tyR = -1 * (Shell.MeshLineXy1[i, j]);
                        double tzR = -1 * (Shell.MeshLineXz1[i, j]);
                        Math3DP.PointG Point3dM = new Math3DP.PointG(txR, tyR, tzR);
                        Point3dM.RotateX = Myglobals.tXValue;
                        Point3dM.RotateY = Myglobals.tYValue;
                        Point3dM.RotateZ = Myglobals.tZValue;
                        Point3dM.DrawPoint();
                        tx1 = Myglobals.TheX3d;
                        ty1 = Myglobals.TheY3d;
                        txR = Shell.MeshLineXx2[i, j];
                        tyR = -1 * (Shell.MeshLineXy2[i, j]);
                        tzR = -1 * (Shell.MeshLineXz2[i, j]);
                        Point3dM = new Math3DP.PointG(txR, tyR, tzR);
                        Point3dM.RotateX = Myglobals.tXValue;
                        Point3dM.RotateY = Myglobals.tYValue;
                        Point3dM.RotateZ = Myglobals.tZValue;
                        Point3dM.DrawPoint();
                        tx2 = Myglobals.TheX3d;
                        ty2 = Myglobals.TheY3d;
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    }
                    for (int j = 1; j < Myglobals.NumberOfMeshLinesY + 2; j++)
                    {
                        double txR = Shell.MeshLineYx1[i, j];
                        double tyR = -1 * (Shell.MeshLineYy1[i, j]);
                        double tzR = -1 * (Shell.MeshLineYz1[i, j]);
                        Math3DP.PointG Point3dM = new Math3DP.PointG(txR, tyR, tzR);
                        Point3dM.RotateX = Myglobals.tXValue;
                        Point3dM.RotateY = Myglobals.tYValue;
                        Point3dM.RotateZ = Myglobals.tZValue;
                        Point3dM.DrawPoint();
                        tx1 = Myglobals.TheX3d;
                        ty1 = Myglobals.TheY3d;
                        txR = Shell.MeshLineYx2[i, j];
                        tyR = -1 * (Shell.MeshLineYy2[i, j]);
                        tzR = -1 * (Shell.MeshLineYz2[i, j]);
                        Point3dM = new Math3DP.PointG(txR, tyR, tzR);
                        Point3dM.RotateX = Myglobals.tXValue;
                        Point3dM.RotateY = Myglobals.tYValue;
                        Point3dM.RotateZ = Myglobals.tZValue;
                        Point3dM.DrawPoint();
                        tx2 = Myglobals.TheX3d;
                        ty2 = Myglobals.TheY3d;
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    }
                }
            }
        }



        int FaceNumber = 0;
        double[] DistanceP0 = new double[100000];
        Point[,] FaceP3D = new Point[100000, 51];
        int[] FaceType = new int[100000];
        int[] FaceShellTypeShell = new int[100000];//اذا صفر جائز
        int[] FacePNumbers = new int[100000];
        public void DrawFloor3d()
        {
            try
            {
                Graphics g = Graphics.FromImage(((MainForm)mainForm).pictureBox2.Image);
                Pen pen = new Pen(Color.Black, 1f);
                //-----------------------------------
                //رسم البلاطات على الفراغي        
                #region//البلاطات و الجدران اولا
                for (int i = 1; i < Shell.Number + 1; i++)
                {
                    if (Shell.Visible[i] == 0 & Shell.Type[i] != 4)
                    {
                        FaceNumber = FaceNumber + 1;
                        Point[] P = new Point[Shell.PointNumbers[i] + 2];
                        double XFORCG = 0;
                        double YFORCG = 0;
                        double ZFORCG = 0;
                        for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                        {
                            int P1 = Shell.JointNo[i, j];
                            XFORCG = XFORCG + Joint.XReal[P1];
                            YFORCG = YFORCG + Joint.YReal[P1];
                            ZFORCG = ZFORCG + Joint.ZReal[P1];
                            P[j].X = Joint.X3d[Shell.JointNo[i, j]];
                            P[j].Y = Joint.Y3d[Shell.JointNo[i, j]];
                            FaceP3D[FaceNumber, j] = P[j];
                        }
                        double CenterX = Math.Round((XFORCG) / Shell.PointNumbers[i], 3);
                        double CenterY = Math.Round((YFORCG) / Shell.PointNumbers[i], 3);
                        double CenterZ = Math.Round((ZFORCG) / Shell.PointNumbers[i], 3);
                        DistanceP0[FaceNumber] = -Math.Sqrt(Math.Pow(CenterX - Myglobals.EyeX, 2) + Math.Pow(CenterY - Myglobals.EyeY, 2) + Math.Pow(CenterZ - Myglobals.EyeZ, 2));
                        P[Shell.PointNumbers[i] + 1].X = P[1].X;
                        P[Shell.PointNumbers[i] + 1].Y = P[1].Y;
                        FaceP3D[FaceNumber, Shell.PointNumbers[i] + 1] = P[Shell.PointNumbers[i] + 1];
                        FaceType[FaceNumber] = 1;//وجه سفلي أساسي
                        FaceShellTypeShell[FaceNumber] = Shell.Type[i];
                        FacePNumbers[FaceNumber] = Shell.PointNumbers[i];
                    }
                }
                #endregion
                #region//الفتحات ثانيا
                for (int i = 1; i < Shell.Number + 1; i++)
                {
                    if (Shell.Visible[i] == 0 & Shell.Type[i] == 4)
                    {
                        Point[] P = new Point[Shell.PointNumbers[i] + 2];
                        for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                        {
                            P[j].X = Joint.X3d[Shell.JointNo[i, j]];
                            P[j].Y = Joint.Y3d[Shell.JointNo[i, j]];
                        }
                        P[Shell.PointNumbers[i] + 1].X = P[1].X;
                        P[Shell.PointNumbers[i] + 1].Y = P[1].Y;
                        using (Brush brush = new SolidBrush(Color.White))
                        {
                            g.FillPolygon(brush, P);
                        }
                    }
                }
                #endregion
            }
            catch { }
        }
        public void DrawCube3d()
        {
            Graphics g = Graphics.FromImage(((MainForm)mainForm).pictureBox2.Image);
            Pen pen = new Pen(Color.DarkSlateBlue, 1);
            Point[] P = new Point[51];
            double tx = 0;
            double ty = 0;
            double tz = 0;
            double tx1 = 0;
            double ty1 = 0;
            double tz1 = 0;
            int i = 0;
            int j = 0;
            for (int iai = 1; iai < bArray.Length; iai++)
            {
                i = bArray[iai];
                if (i == 0) break;
                {
                    string DESIGNATION = Section.DESIGNATION[((MainForm)mainForm).FrameElement[i].Section];
                    double D = Section.D[((MainForm)mainForm).FrameElement[i].Section];/// 100;
                    if (D == 0) D = Section.HT[((MainForm)mainForm).FrameElement[i].Section];// / 100;
                    double BF = Section.BF[((MainForm)mainForm).FrameElement[i].Section];// / 100;
                    if (BF == 0) BF = Section.B[((MainForm)mainForm).FrameElement[i].Section];// / 100;
                    double BFB = Section.BFB[((MainForm)mainForm).FrameElement[i].Section];// / 100;
                    double TF = Section.TF[((MainForm)mainForm).FrameElement[i].Section];// / 100;
                    double TFB = Section.TFB[((MainForm)mainForm).FrameElement[i].Section];// / 100;
                    double TW = Section.TW[((MainForm)mainForm).FrameElement[i].Section];// / 100;
                    double X = Section.X[((MainForm)mainForm).FrameElement[i].Section];// / 100;
                    double Y = Section.Y[((MainForm)mainForm).FrameElement[i].Section];// / 100;
                    double angelRotate = ((MainForm)mainForm).FrameElement[i].RotateAngel * Math.PI / 180;////
                    Point[] P1 = new Point[8];
                    if (DESIGNATION == "L")
                    {
                        #region//شكل L
                        double angelvertical = 0;
                        if (angelvertical == 0)
                        {
                            tx1 = Joint.XReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                            ty1 = Joint.YReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                            tz1 = Joint.ZReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                            tx = Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                            ty = Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                            tz = Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        }
                        else
                        {
                            tx = Joint.XReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                            ty = Joint.YReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                            tz = Joint.ZReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                            tx1 = Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                            ty1 = Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                            tz1 = Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        }
                        double xDiff = tx - tx1;
                        double yDiff = ty - ty1;
                        double zDiff = tz - tz1;
                        double LDiff = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));
                        double angel = Math.Atan2(yDiff, xDiff);
                        double angel1 = Math.Atan2(zDiff, LDiff);
                        //if (tx == tx1 & ty == ty1)//عمود
                        //{
                        //   angelRotate = angelRotate + Math.PI / 2;
                        //}
                        double radious = Math.Sqrt(Math.Pow((D - Y), 2) + Math.Pow(X, 2));
                        double angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2((D - Y), X);
                        double angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                        double thelength = 2 * radious * Math.Sin(angelRotate / 2);
                        double DfBL1 = thelength * Math.Sin(angel3);
                        double DfDL1 = thelength * Math.Cos(angel3);

                        radious = Math.Sqrt(Math.Pow((D - Y), 2) + Math.Pow((X - TW), 2));
                        angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2((D - Y), (X - TW));
                        angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                        thelength = 2 * radious * Math.Sin(angelRotate / 2);
                        double DfBL2 = thelength * Math.Sin(angel3);
                        double DfDL2 = thelength * Math.Cos(angel3);

                        radious = Math.Sqrt(Math.Pow((Y - TF), 2) + Math.Pow((X - TW), 2));
                        angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2((Y - TF), (X - TW));
                        angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                        thelength = 2 * radious * Math.Sin(angelRotate / 2);
                        double DfBL3 = thelength * Math.Cos(angel2);
                        double DfDL3 = thelength * Math.Sin(angel2);

                        radious = Math.Sqrt(Math.Pow((Y - TF), 2) + Math.Pow((BF - X), 2));
                        angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2((Y - TF), (BF - X));
                        angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                        thelength = 2 * radious * Math.Sin(angelRotate / 2);
                        double DfBL4 = thelength * Math.Sin(angel3);
                        double DfDL4 = thelength * Math.Cos(angel3);

                        radious = Math.Sqrt(Math.Pow(Y, 2) + Math.Pow((BF - X), 2));
                        angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2(Y, (BF - X));
                        angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                        thelength = 2 * radious * Math.Sin(angelRotate / 2);
                        double DfBL5 = thelength * Math.Sin(angel3);
                        double DfDL5 = thelength * Math.Cos(angel3);

                        radious = Math.Sqrt(Math.Pow(Y, 2) + Math.Pow(X, 2));
                        angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2(Y, X);
                        angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                        thelength = 2 * radious * Math.Sin(angelRotate / 2);
                        double DfBL6 = thelength * Math.Cos(angel2);
                        double DfDL6 = thelength * Math.Sin(angel2);

                        for (j = 1; j < 13; j++)
                        {
                            if (j > 6)
                            {
                                if (angelvertical == 0)
                                {
                                    tx = Joint.XReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                                    ty = Joint.YReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                                    tz = Joint.ZReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                                }
                                else
                                {
                                    tx = Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                                    ty = Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                                    tz = Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                                }
                            }
                            if (j == 1 || j == 7)
                            {
                                tx1 = tx - (X + DfBL1) * Math.Sin(angel) - ((D - Y - DfDL1) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty + (X + DfBL1) * Math.Cos(angel) - ((D - Y - DfDL1) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz + (D - Y - DfDL1) * Math.Cos(angel1);
                            }
                            if (j == 2 || j == 8)
                            {
                                tx1 = tx - (X - TW + DfBL2) * Math.Sin(angel) - ((D - Y - DfDL2) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty + (X - TW + DfBL2) * Math.Cos(angel) - ((D - Y - DfDL2) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz + (D - Y - DfDL2) * Math.Cos(angel1);
                            }
                            if (j == 3 || j == 9)
                            {
                                tx1 = tx - (X - TW - DfBL3) * Math.Sin(angel) + ((Y - TF + DfDL3) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty + (X - TW - DfBL3) * Math.Cos(angel) + ((Y - TF + DfDL3) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz - (Y - TF + DfDL3) * Math.Cos(angel1);
                            }
                            if (j == 4 || j == 10)
                            {
                                tx1 = tx + (BF - X + DfBL4) * Math.Sin(angel) + ((Y - TF - DfDL4) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty - (BF - X + DfBL4) * Math.Cos(angel) + ((Y - TF - DfDL4) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz - (Y - TF - DfDL4) * Math.Cos(angel1);
                            }
                            if (j == 5 || j == 11)
                            {
                                tx1 = tx + (BF - X + DfBL5) * Math.Sin(angel) + ((Y - DfDL5) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty - (BF - X + DfBL5) * Math.Cos(angel) + ((Y - DfDL5) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz - (Y - DfDL5) * Math.Cos(angel1);
                            }
                            if (j == 6 || j == 12)
                            {
                                tx1 = tx - (X - DfBL6) * Math.Sin(angel) + ((Y + DfDL6) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty + (X - DfBL6) * Math.Cos(angel) + ((Y + DfDL6) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz - (Y + DfDL6) * Math.Cos(angel1);
                            }
                            tx1 = tx1;
                            ty1 = -1 * ty1;
                            tz1 = -1 * tz1;
                            Math3DP.PointG cube3dM = new Math3DP.PointG(tx1, ty1, tz1);
                            cube3dM.RotateX = Myglobals.tXValue;
                            cube3dM.RotateY = Myglobals.tYValue;
                            cube3dM.RotateZ = Myglobals.tZValue;
                            cube3dM.DrawPoint();
                            P[j].X = Myglobals.TheX3d;
                            P[j].Y = Myglobals.TheY3d;
                        }
                        //الرسم
                        P1 = new Point[8];//الوجوه المقابلة
                        for (j = 1; j < 7; j++)
                        {
                            P1[j] = P[j];
                        }
                        P1[7] = P1[1];
                        for (j = 1; j < 6; j++)
                        {
                            g.DrawLine(pen, P1[j], P1[j + 1]);
                        }
                        g.DrawLine(pen, P1[1], P1[7]);
                        using (Brush brush = new SolidBrush(Color.FromArgb(60, Color.DarkSlateBlue)))
                        {
                            g.FillPolygon(brush, P1);
                        }
                        for (j = 1; j < 7; j++)
                        {
                            P1[j] = P[6 + j];
                        }
                        P1[7] = P1[1];
                        for (j = 1; j < 6; j++)
                        {
                            g.DrawLine(pen, P1[j], P1[j + 1]);
                        }
                        g.DrawLine(pen, P1[1], P1[7]);
                        using (Brush brush = new SolidBrush(Color.FromArgb(60, Color.DarkSlateBlue)))
                        {
                            g.FillPolygon(brush, P1);
                        }
                        P1 = new Point[6];
                        for (int k = 1; k < 7; k++)//الوجوه الجانبية
                        {
                            if (k == 1)
                            {
                                P1[1] = P[1];
                                P1[2] = P[7];
                                P1[3] = P[12];
                                P1[4] = P[6];
                            }
                            if (k == 2)
                            {
                                P1[1] = P[1];
                                P1[2] = P[7];
                                P1[3] = P[8];
                                P1[4] = P[2];
                            }
                            if (k == 3)
                            {
                                P1[1] = P[2];
                                P1[2] = P[8];
                                P1[3] = P[9];
                                P1[4] = P[3];
                            }
                            if (k == 4)
                            {
                                P1[1] = P[3];
                                P1[2] = P[9];
                                P1[3] = P[10];
                                P1[4] = P[4];
                            }
                            if (k == 5)
                            {
                                P1[1] = P[4];
                                P1[2] = P[10];
                                P1[3] = P[11];
                                P1[4] = P[5];
                            }
                            if (k == 6)
                            {
                                P1[1] = P[6];
                                P1[2] = P[12];
                                P1[3] = P[11];
                                P1[4] = P[5];
                            }
                            P1[5] = P1[1];
                            for (j = 1; j < 4; j++)
                            {
                                g.DrawLine(pen, P1[j], P1[j + 1]);
                            }
                            g.DrawLine(pen, P1[1], P1[4]);
                            using (Brush brush = new SolidBrush(Color.FromArgb(60, Color.DarkSlateBlue)))
                            {
                                g.FillPolygon(brush, P1);
                            }
                        }
                        #endregion
                    }
                    if (DESIGNATION == "R")////////////////فقط محلولة للأوجه
                    {
                        #region//شكل مستطيل
                        tx = Joint.XReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        ty = Joint.YReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        tz = Joint.ZReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        tx1 = Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        ty1 = Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        tz1 = Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        double xDiff = tx - tx1;
                        double yDiff = ty - ty1;
                        double zDiff = tz - tz1;
                        double LDiff = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));
                        double angel = Math.Atan2(yDiff, xDiff);
                        double angel1 = Math.Atan2(zDiff, LDiff);
                        // if (tx == tx1 & ty == ty1)//عمود
                        // {
                        //  angelRotate = -angelRotate + Math.PI / 2;
                        //}
                        double radious = Math.Sqrt(Math.Pow(D / 2, 2) + Math.Pow(BF / 2, 2));
                        double angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2(D / 2, BF / 2);
                        double angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                        double thelength = 2 * radious * Math.Sin(angelRotate / 2);
                        double DfBR = thelength * Math.Cos(angel2);
                        double DfDR = thelength * Math.Sin(angel2);
                        double DfBL = thelength * Math.Sin(angel3);
                        double DfDL = thelength * Math.Cos(angel3);
                        double[] XReal = new double[9];
                        double[] YReal = new double[9];
                        double[] ZReal = new double[9];
                        int i1 = 0;
                        int i2 = 0;
                        int i3 = 0;
                        int i4 = 0;
                        int jaez = 0;
                        if (tz == tz1)
                        {
                            tz = tz - D / 2;
                            jaez = 1;
                        }
                        for (j = 1; j < 9; j++)
                        {
                            if (j > 4)
                            {
                                tx = Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                                ty = Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                                tz = Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                                if (jaez == 1) tz = tz - D / 2;
                            }
                            if (j == 1 || j == 5)
                            {
                                tx1 = tx - (BF / 2 + DfBL) * Math.Sin(angel) - ((D / 2 - DfDL) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty + (BF / 2 + DfBL) * Math.Cos(angel) - ((D / 2 - DfDL) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz + (D / 2 - DfDL) * Math.Cos(angel1);
                            }
                            if (j == 2 || j == 6)
                            {
                                tx1 = tx + (BF / 2 - DfBR) * Math.Sin(angel) - ((D / 2 + DfDR) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty - (BF / 2 - DfBR) * Math.Cos(angel) - ((D / 2 + DfDR) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz + (D / 2 + DfDR) * Math.Cos(angel1);
                            }
                            if (j == 3 || j == 7)
                            {
                                tx1 = tx + (BF / 2 + DfBL) * Math.Sin(angel) + ((D / 2 - DfDL) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty - (BF / 2 + DfBL) * Math.Cos(angel) + ((D / 2 - DfDL) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz - (D / 2 - DfDL) * Math.Cos(angel1);
                            }
                            if (j == 4 || j == 8)
                            {
                                tx1 = tx - (BF / 2 - DfBR) * Math.Sin(angel) + ((D / 2 + DfDR) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty + (BF / 2 - DfBR) * Math.Cos(angel) + ((D / 2 + DfDR) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz - (D / 2 + DfDR) * Math.Cos(angel1);
                            }
                            XReal[j] = tx1;
                            YReal[j] = ty1;
                            ZReal[j] = tz1;
                            tx1 = tx1;
                            ty1 = -1 * ty1;
                            tz1 = -1 * tz1;
                            Math3DP.PointG cube3dM = new Math3DP.PointG(tx1, ty1, tz1);
                            cube3dM.RotateX = Myglobals.tXValue;
                            cube3dM.RotateY = Myglobals.tYValue;
                            cube3dM.RotateZ = Myglobals.tZValue;
                            cube3dM.DrawPoint();
                            P[j].X = Myglobals.TheX3d;
                            P[j].Y = Myglobals.TheY3d;
                        }
                        //التجهيز للرسم
                        if (BeamTypeToDraw[i] == 0)
                        {
                            for (int k = 1; k < 7; k++)
                            {
                                if (k == 1)
                                {
                                    i1 = 1;
                                    i2 = 2;
                                    i3 = 3;
                                    i4 = 4;
                                }
                                if (k == 2)
                                {
                                    i1 = 5;
                                    i2 = 6;
                                    i3 = 7;
                                    i4 = 8;
                                }
                                if (k == 3)
                                {
                                    i1 = 1;
                                    i2 = 5;
                                    i3 = 8;
                                    i4 = 4;
                                }
                                if (k == 4)
                                {
                                    i1 = 1;
                                    i2 = 5;
                                    i3 = 6;
                                    i4 = 2;
                                }
                                if (k == 5)
                                {
                                    i1 = 2;
                                    i2 = 6;
                                    i3 = 7;
                                    i4 = 3;
                                }
                                if (k == 6)
                                {
                                    i1 = 4;
                                    i2 = 8;
                                    i3 = 7;
                                    i4 = 3;
                                }
                                FaceNumber = FaceNumber + 1;
                                FaceP3D[FaceNumber, 1] = P[i1];
                                FaceP3D[FaceNumber, 2] = P[i2];
                                FaceP3D[FaceNumber, 3] = P[i3];
                                FaceP3D[FaceNumber, 4] = P[i4];
                                FaceP3D[FaceNumber, 5] = P[i1];
                                FaceType[FaceNumber] = k;
                                CalculateSurfaceDistance1(XReal[i1], YReal[i1], ZReal[i1], XReal[i2], YReal[i2], ZReal[i2], XReal[i3], YReal[i3], ZReal[i3], XReal[i4], YReal[i4], ZReal[i4]);
                                DistanceP0[FaceNumber] = -Math.Abs(Distance);
                                FacePNumbers[FaceNumber] = 4;
                                FaceShellTypeShell[FaceNumber] = 0;
                            }
                        }
                        if (BeamTypeToDraw[i] == 1)
                        {
                            i1 = 1;
                            i2 = 5;
                            i3 = 6;
                            i4 = 2;
                            int k = 4;
                            FaceNumber = FaceNumber + 1;
                            FaceP3D[FaceNumber, 1] = P[i1];
                            FaceP3D[FaceNumber, 2] = P[i2];
                            FaceP3D[FaceNumber, 3] = P[i3];
                            FaceP3D[FaceNumber, 4] = P[i4];
                            FaceP3D[FaceNumber, 5] = P[i1];
                            FaceType[FaceNumber] = k;
                            CalculateSurfaceDistance1(XReal[i1], YReal[i1], ZReal[i1], XReal[i2], YReal[i2], ZReal[i2], XReal[i3], YReal[i3], ZReal[i3], XReal[i4], YReal[i4], ZReal[i4]);
                            DistanceP0[FaceNumber] = -Math.Abs(Distance);
                            FacePNumbers[FaceNumber] = 4;
                            FaceShellTypeShell[FaceNumber] = 0;
                        }
                        #endregion
                    }
                    if (DESIGNATION == "CR")
                    {
                        #region //دائرة

                        tx = Joint.XReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        ty = Joint.YReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        tz = Joint.ZReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        tx1 = Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        ty1 = Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        tz1 = Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        double xDiff = tx - tx1;
                        double yDiff = ty - ty1;
                        double zDiff = tz - tz1;
                        double LDiff = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));
                        double angel = Math.Atan2(yDiff, xDiff);
                        double angel1 = Math.Atan2(zDiff, LDiff);

                        double alfa = Math.PI / 12;
                        double radious = D / 2;
                        int mm = 0;
                        for (j = 0; j < 50; j++)
                        {
                            if (j > 24)
                            {
                                tx = Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                                ty = Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                                tz = Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                                mm = 25;
                            }
                            double alfa1 = alfa * (j - mm);
                            tx1 = tx + (Math.Cos(alfa1) * radious) * Math.Sin(angel) - ((Math.Sin(alfa1) * radious) * Math.Sin(angel1) * Math.Cos(angel));
                            ty1 = ty - (Math.Cos(alfa1) * radious) * Math.Cos(angel) - ((Math.Sin(alfa1) * radious) * Math.Sin(angel1) * Math.Sin(angel));
                            tz1 = tz + (Math.Sin(alfa1) * radious) * Math.Cos(angel1);
                            tx1 = tx1;
                            ty1 = -1 * ty1;
                            tz1 = -1 * tz1;
                            Math3DP.PointG cube3dM = new Math3DP.PointG(tx1, ty1, tz1);
                            cube3dM.RotateX = Myglobals.tXValue;
                            cube3dM.RotateY = Myglobals.tYValue;
                            cube3dM.RotateZ = Myglobals.tZValue;
                            cube3dM.DrawPoint();
                            P[j].X = Myglobals.TheX3d;
                            P[j].Y = Myglobals.TheY3d;
                        }
                        //الرسم
                        for (j = 0; j < 24; j++)
                        {
                            g.DrawLine(pen, P[j], P[j + 1]);
                        }
                        for (j = 25; j < 49; j++)
                        {
                            g.DrawLine(pen, P[j], P[j + 1]);
                        }
                        for (j = 0; j < 24; j++)
                        {
                            g.DrawLine(pen, P[j], P[j + 25]);
                        }

                        P1 = new Point[6];
                        for (int k = 0; k < 24; k++)
                        {
                            P1[1] = P[k];
                            P1[2] = P[k + 1];
                            P1[3] = P[k + 26];
                            P1[4] = P[k + 25];
                            P1[5] = P1[1];
                            using (Brush brush = new SolidBrush(Color.FromArgb(60, Color.DarkSlateBlue)))
                            {
                                g.FillPolygon(brush, P1);
                            }
                        }
                        P1 = new Point[25];
                        for (int k = 0; k < 25; k++)
                        {
                            P1[k] = P[k];
                        }
                        using (Brush brush = new SolidBrush(Color.FromArgb(60, Color.DarkSlateBlue)))
                        {
                            g.FillPolygon(brush, P1);
                        }
                        for (int k = 0; k < 25; k++)
                        {
                            P1[k] = P[k + 25];
                        }
                        using (Brush brush = new SolidBrush(Color.FromArgb(60, Color.DarkSlateBlue)))
                        {
                            g.FillPolygon(brush, P1);
                        }

                        #endregion
                    }
                    if (DESIGNATION == "B")
                    {
                        #region//شكل تيوب
                        tx = Joint.XReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        ty = Joint.YReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        tz = Joint.ZReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        tx1 = Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        ty1 = Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        tz1 = Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        double xDiff = tx - tx1;
                        double yDiff = ty - ty1;
                        double zDiff = tz - tz1;
                        double LDiff = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));
                        double angel = Math.Atan2(yDiff, xDiff);
                        double angel1 = Math.Atan2(zDiff, LDiff);
                        /*if (tx == tx1 & ty == ty1)//عمود
                        {
                            angelRotate = -angelRotate + Math.PI / 2;
                        }*/
                        double radious = Math.Sqrt(Math.Pow(D / 2, 2) + Math.Pow(BF / 2, 2));
                        double angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2(D / 2, BF / 2);
                        double angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                        double thelength = 2 * radious * Math.Sin(angelRotate / 2);
                        double DfBR = thelength * Math.Cos(angel2);
                        double DfDR = thelength * Math.Sin(angel2);
                        double DfBL = thelength * Math.Sin(angel3);
                        double DfDL = thelength * Math.Cos(angel3);

                        for (j = 1; j < 9; j++)
                        {
                            if (j > 4)
                            {
                                tx = Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                                ty = Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                                tz = Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                            }
                            if (j == 1 || j == 5)
                            {
                                tx1 = tx - (BF / 2 + DfBL) * Math.Sin(angel) - ((D / 2 - DfDL) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty + (BF / 2 + DfBL) * Math.Cos(angel) - ((D / 2 - DfDL) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz + (D / 2 - DfDL) * Math.Cos(angel1);
                            }
                            if (j == 2 || j == 6)
                            {
                                tx1 = tx + (BF / 2 - DfBR) * Math.Sin(angel) - ((D / 2 + DfDR) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty - (BF / 2 - DfBR) * Math.Cos(angel) - ((D / 2 + DfDR) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz + (D / 2 + DfDR) * Math.Cos(angel1);
                            }
                            if (j == 3 || j == 7)
                            {
                                tx1 = tx + (BF / 2 + DfBL) * Math.Sin(angel) + ((D / 2 - DfDL) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty - (BF / 2 + DfBL) * Math.Cos(angel) + ((D / 2 - DfDL) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz - (D / 2 - DfDL) * Math.Cos(angel1);
                            }
                            if (j == 4 || j == 8)
                            {
                                tx1 = tx - (BF / 2 - DfBR) * Math.Sin(angel) + ((D / 2 + DfDR) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty + (BF / 2 - DfBR) * Math.Cos(angel) + ((D / 2 + DfDR) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz - (D / 2 + DfDR) * Math.Cos(angel1);
                            }
                            tx1 = tx1;
                            ty1 = -1 * ty1;
                            tz1 = -1 * tz1;
                            Math3DP.PointG cube3dM = new Math3DP.PointG(tx1, ty1, tz1);
                            cube3dM.RotateX = Myglobals.tXValue;
                            cube3dM.RotateY = Myglobals.tYValue;
                            cube3dM.RotateZ = Myglobals.tZValue;
                            cube3dM.DrawPoint();
                            P[j].X = Myglobals.TheX3d;
                            P[j].Y = Myglobals.TheY3d;
                        }
                        //الرسم
                        P1 = new Point[6];
                        for (int k = 1; k < 5; k++)
                        {
                            if (k == 10)
                            {
                                P1[1] = P[1];
                                P1[2] = P[2];
                                P1[3] = P[3];
                                P1[4] = P[4];
                            }
                            if (k == 20)
                            {
                                P1[1] = P[5];
                                P1[2] = P[6];
                                P1[3] = P[7];
                                P1[4] = P[8];
                            }
                            if (k == 1)
                            {
                                P1[1] = P[1];
                                P1[2] = P[5];
                                P1[3] = P[8];
                                P1[4] = P[4];
                            }
                            if (k == 2)
                            {
                                P1[1] = P[1];
                                P1[2] = P[5];
                                P1[3] = P[6];
                                P1[4] = P[2];
                            }
                            if (k == 3)
                            {
                                P1[1] = P[2];
                                P1[2] = P[6];
                                P1[3] = P[7];
                                P1[4] = P[3];
                            }
                            if (k == 4)
                            {
                                P1[1] = P[4];
                                P1[2] = P[8];
                                P1[3] = P[7];
                                P1[4] = P[3];
                            }
                            P1[5] = P1[1];
                            for (j = 1; j < 4; j++)
                            {
                                g.DrawLine(pen, P1[j], P1[j + 1]);
                            }
                            g.DrawLine(pen, P1[1], P1[4]);
                            using (Brush brush = new SolidBrush(Color.FromArgb(60, Color.DarkSlateBlue)))
                            {
                                g.FillPolygon(brush, P1);
                            }
                        }
                        #endregion
                    }
                    if (DESIGNATION == "W" || DESIGNATION == "I")
                    {
                        #region//شكل I
                        tx = Joint.XReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        ty = Joint.YReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        tz = Joint.ZReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        tx1 = Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        ty1 = Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        tz1 = Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        double xDiff = tx - tx1;
                        double yDiff = ty - ty1;
                        double zDiff = tz - tz1;
                        double LDiff = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));
                        double angel = Math.Atan2(yDiff, xDiff);
                        double angel1 = Math.Atan2(zDiff, LDiff);
                        /* if (tx == tx1 & ty == ty1)//عمود
                         {
                             angelRotate = -angelRotate + Math.PI / 2;
                         }*/
                        double radious = Math.Sqrt(Math.Pow(D / 2, 2) + Math.Pow(BF / 2, 2));
                        double angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2(D / 2, BF / 2);
                        double angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                        double thelength = 2 * radious * Math.Sin(angelRotate / 2);
                        double DfBR1 = thelength * Math.Cos(angel2);
                        double DfDR1 = thelength * Math.Sin(angel2);
                        double DfBL1 = thelength * Math.Sin(angel3);
                        double DfDL1 = thelength * Math.Cos(angel3);

                        radious = Math.Sqrt(Math.Pow(D / 2 - TF, 2) + Math.Pow(BF / 2, 2));
                        angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2(D / 2 - TF, BF / 2);
                        angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                        thelength = 2 * radious * Math.Sin(angelRotate / 2);
                        double DfBR2 = thelength * Math.Cos(angel2);
                        double DfDR2 = thelength * Math.Sin(angel2);
                        double DfBL2 = thelength * Math.Sin(angel3);
                        double DfDL2 = thelength * Math.Cos(angel3);

                        radious = Math.Sqrt(Math.Pow(D / 2 - TF, 2) + Math.Pow(TW / 2, 2));
                        angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2(D / 2 - TF, TW / 2);
                        angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                        thelength = 2 * radious * Math.Sin(angelRotate / 2);
                        double DfBR3 = thelength * Math.Cos(angel2);
                        double DfDR3 = thelength * Math.Sin(angel2);
                        double DfBL3 = thelength * Math.Sin(angel3);
                        double DfDL3 = thelength * Math.Cos(angel3);

                        radious = Math.Sqrt(Math.Pow(D / 2, 2) + Math.Pow(BFB / 2, 2));
                        angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2(D / 2, BFB / 2);
                        angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                        thelength = 2 * radious * Math.Sin(angelRotate / 2);
                        double DfBR4 = thelength * Math.Cos(angel2);
                        double DfDR4 = thelength * Math.Sin(angel2);
                        double DfBL4 = thelength * Math.Sin(angel3);
                        double DfDL4 = thelength * Math.Cos(angel3);

                        radious = Math.Sqrt(Math.Pow(D / 2 - TFB, 2) + Math.Pow(BFB / 2, 2));
                        angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2(D / 2 - TFB, BFB / 2);
                        angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                        thelength = 2 * radious * Math.Sin(angelRotate / 2);
                        double DfBR5 = thelength * Math.Cos(angel2);
                        double DfDR5 = thelength * Math.Sin(angel2);
                        double DfBL5 = thelength * Math.Sin(angel3);
                        double DfDL5 = thelength * Math.Cos(angel3);

                        radious = Math.Sqrt(Math.Pow(D / 2 - TFB, 2) + Math.Pow(TW / 2, 2));
                        angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2(D / 2 - TFB, TW / 2);
                        angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                        thelength = 2 * radious * Math.Sin(angelRotate / 2);
                        double DfBR6 = thelength * Math.Cos(angel2);
                        double DfDR6 = thelength * Math.Sin(angel2);
                        double DfBL6 = thelength * Math.Sin(angel3);
                        double DfDL6 = thelength * Math.Cos(angel3);

                        for (j = 1; j < 25; j++)
                        {
                            if (j > 12)
                            {
                                tx = Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                                ty = Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                                tz = Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                            }
                            if (j == 1 || j == 13)
                            {
                                tx1 = tx - (BF / 2 + DfBL1) * Math.Sin(angel) - ((D / 2 - DfDL1) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty + (BF / 2 + DfBL1) * Math.Cos(angel) - ((D / 2 - DfDL1) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz + (D / 2 - DfDL1) * Math.Cos(angel1);
                            }
                            if (j == 2 || j == 14)
                            {
                                tx1 = tx + (BF / 2 - DfBR1) * Math.Sin(angel) - ((D / 2 + DfDR1) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty - (BF / 2 - DfBR1) * Math.Cos(angel) - ((D / 2 + DfDR1) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz + (D / 2 + DfDR1) * Math.Cos(angel1);
                            }
                            if (j == 3 || j == 15)
                            {
                                tx1 = tx + (BF / 2 - DfBR2) * Math.Sin(angel) - ((D / 2 - TF + DfDR2) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty - (BF / 2 - DfBR2) * Math.Cos(angel) - ((D / 2 - TF + DfDR2) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz + (D / 2 - TF + DfDR2) * Math.Cos(angel1);
                            }
                            if (j == 4 || j == 16)
                            {
                                tx1 = tx + (TW / 2 - DfBR3) * Math.Sin(angel) - ((D / 2 - TF + DfDR3) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty - (TW / 2 - DfBR3) * Math.Cos(angel) - ((D / 2 - TF + DfDR3) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz + (D / 2 - TF + DfDR3) * Math.Cos(angel1);
                            }
                            if (j == 5 || j == 17)
                            {
                                tx1 = tx + (TW / 2 + DfBL6) * Math.Sin(angel) + ((D / 2 - TFB - DfDL6) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty - (TW / 2 + DfBL6) * Math.Cos(angel) + ((D / 2 - TFB - DfDL6) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz - (D / 2 - TFB - DfDL6) * Math.Cos(angel1);
                            }
                            if (j == 6 || j == 18)
                            {
                                tx1 = tx + (BFB / 2 + DfBL5) * Math.Sin(angel) + ((D / 2 - TFB - DfDL5) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty - (BFB / 2 + DfBL5) * Math.Cos(angel) + ((D / 2 - TFB - DfDL5) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz - (D / 2 - TFB - DfDL5) * Math.Cos(angel1);
                            }
                            if (j == 7 || j == 19)
                            {
                                tx1 = tx + (BFB / 2 + DfBL4) * Math.Sin(angel) + ((D / 2 - DfDL4) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty - (BFB / 2 + DfBL4) * Math.Cos(angel) + ((D / 2 - DfDL4) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz - (D / 2 - DfDL4) * Math.Cos(angel1);
                            }
                            if (j == 8 || j == 20)
                            {
                                tx1 = tx - (BFB / 2 - DfBR4) * Math.Sin(angel) + ((D / 2 + DfDR4) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty + (BFB / 2 - DfBR4) * Math.Cos(angel) + ((D / 2 + DfDR4) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz - (D / 2 + DfDR4) * Math.Cos(angel1);
                            }
                            if (j == 9 || j == 21)
                            {
                                tx1 = tx - (BFB / 2 - DfBR5) * Math.Sin(angel) + ((D / 2 - TFB + DfDR5) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty + (BFB / 2 - DfBR5) * Math.Cos(angel) + ((D / 2 - TFB + DfDR5) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz - (D / 2 - TFB + DfDR5) * Math.Cos(angel1);
                            }
                            if (j == 10 || j == 22)
                            {
                                tx1 = tx - (TW / 2 - DfBR6) * Math.Sin(angel) + ((D / 2 - TFB + DfDR6) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty + (TW / 2 - DfBR6) * Math.Cos(angel) + ((D / 2 - TFB + DfDR6) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz - (D / 2 - TFB + DfDR6) * Math.Cos(angel1);
                            }
                            if (j == 11 || j == 23)
                            {
                                tx1 = tx - (TW / 2 + DfBL3) * Math.Sin(angel) - ((D / 2 - TF - DfDL3) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty + (TW / 2 + DfBL3) * Math.Cos(angel) - ((D / 2 - TF - DfDL3) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz + (D / 2 - TF - DfDL3) * Math.Cos(angel1);
                            }
                            if (j == 12 || j == 24)
                            {
                                tx1 = tx - (BF / 2 + DfBL2) * Math.Sin(angel) - ((D / 2 - TF - DfDL2) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty + (BF / 2 + DfBL2) * Math.Cos(angel) - ((D / 2 - TF - DfDL2) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz + (D / 2 - TF - DfDL2) * Math.Cos(angel1);
                            }
                            tx1 = tx1;
                            ty1 = -1 * ty1;
                            tz1 = -1 * tz1;
                            Math3DP.PointG cube3dM = new Math3DP.PointG(tx1, ty1, tz1);
                            cube3dM.RotateX = Myglobals.tXValue;
                            cube3dM.RotateY = Myglobals.tYValue;
                            cube3dM.RotateZ = Myglobals.tZValue;
                            cube3dM.DrawPoint();
                            P[j].X = Myglobals.TheX3d;
                            P[j].Y = Myglobals.TheY3d;
                        }
                        //الرسم
                        P1 = new Point[14];//الوجوه المقابلة
                        for (j = 1; j < 13; j++)
                        {
                            P1[j] = P[j];
                        }
                        P1[13] = P1[1];
                        for (j = 1; j < 12; j++)
                        {
                            g.DrawLine(pen, P1[j], P1[j + 1]);
                        }
                        g.DrawLine(pen, P1[1], P1[13]);
                        using (Brush brush = new SolidBrush(Color.FromArgb(60, Color.DarkSlateBlue)))
                        {
                            g.FillPolygon(brush, P1);
                        }
                        for (j = 1; j < 13; j++)
                        {
                            P1[j] = P[12 + j];
                        }
                        P1[13] = P1[1];
                        for (j = 1; j < 12; j++)
                        {
                            g.DrawLine(pen, P1[j], P1[j + 1]);
                        }
                        g.DrawLine(pen, P1[1], P1[13]);
                        using (Brush brush = new SolidBrush(Color.FromArgb(60, Color.DarkSlateBlue)))
                        {
                            g.FillPolygon(brush, P1);
                        }
                        P1 = new Point[6];

                        for (int k = 1; k < 12; k++)
                        {
                            if (k == 1)
                            {
                                P1[1] = P[1];
                                P1[2] = P[13];
                                P1[3] = P[14];
                                P1[4] = P[2];
                            }
                            if (k == 2)
                            {
                                P1[1] = P[2];
                                P1[2] = P[14];
                                P1[3] = P[15];
                                P1[4] = P[3];
                            }
                            if (k == 3)
                            {
                                P1[1] = P[4];
                                P1[2] = P[16];
                                P1[3] = P[15];
                                P1[4] = P[3];
                            }
                            if (k == 4)
                            {
                                P1[1] = P[4];
                                P1[2] = P[16];
                                P1[3] = P[17];
                                P1[4] = P[5];
                            }
                            if (k == 5)
                            {
                                P1[1] = P[5];
                                P1[2] = P[17];
                                P1[3] = P[18];
                                P1[4] = P[6];
                            }
                            if (k == 6)
                            {
                                P1[1] = P[6];
                                P1[2] = P[18];
                                P1[3] = P[19];
                                P1[4] = P[7];
                            }
                            if (k == 7)
                            {
                                P1[1] = P[8];
                                P1[2] = P[20];
                                P1[3] = P[19];
                                P1[4] = P[7];
                            }
                            if (k == 8)
                            {
                                P1[1] = P[9];
                                P1[2] = P[21];
                                P1[3] = P[20];
                                P1[4] = P[8];
                            }
                            if (k == 9)
                            {
                                P1[1] = P[10];
                                P1[2] = P[22];
                                P1[3] = P[23];
                                P1[4] = P[11];
                            }
                            if (k == 10)
                            {
                                P1[1] = P[11];
                                P1[2] = P[23];
                                P1[3] = P[24];
                                P1[4] = P[12];
                            }
                            if (k == 11)
                            {
                                P1[1] = P[12];
                                P1[2] = P[24];
                                P1[3] = P[13];
                                P1[4] = P[1];
                            }
                            P1[5] = P1[1];
                            for (j = 1; j < 4; j++)
                            {
                                g.DrawLine(pen, P1[j], P1[j + 1]);
                            }
                            g.DrawLine(pen, P1[1], P1[4]);
                            using (Brush brush = new SolidBrush(Color.FromArgb(60, Color.DarkSlateBlue)))
                            {
                                g.FillPolygon(brush, P1);
                            }
                        }
                        #endregion
                    }
                    if (DESIGNATION == "T")
                    {
                        #region//شكل T
                        tx = Joint.XReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        ty = Joint.YReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        tz = Joint.ZReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        tx1 = Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        ty1 = Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        tz1 = Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        double xDiff = tx - tx1;
                        double yDiff = ty - ty1;
                        double zDiff = tz - tz1;
                        double LDiff = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));
                        double angel = Math.Atan2(yDiff, xDiff);
                        double angel1 = Math.Atan2(zDiff, LDiff);
                        /* if (tx == tx1 & ty == ty1)//عمود
                         {
                             angelRotate = -angelRotate + Math.PI / 2;
                         }*/
                        double radious = Math.Sqrt(Math.Pow(Y, 2) + Math.Pow(BF / 2, 2));
                        double angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2(Y, BF / 2);
                        double angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                        double thelength = 2 * radious * Math.Sin(angelRotate / 2);
                        double DfBR1 = thelength * Math.Cos(angel2);
                        double DfDR1 = thelength * Math.Sin(angel2);
                        double DfBL1 = thelength * Math.Sin(angel3);
                        double DfDL1 = thelength * Math.Cos(angel3);

                        radious = Math.Sqrt(Math.Pow(Y - TF, 2) + Math.Pow(BF / 2, 2));
                        angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2(Y - TF, BF / 2);
                        angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                        thelength = 2 * radious * Math.Sin(angelRotate / 2);
                        double DfBR2 = thelength * Math.Cos(angel2);
                        double DfDR2 = thelength * Math.Sin(angel2);
                        double DfBL2 = thelength * Math.Sin(angel3);
                        double DfDL2 = thelength * Math.Cos(angel3);

                        radious = Math.Sqrt(Math.Pow(Y - TF, 2) + Math.Pow(TW / 2, 2));
                        angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2(Y - TF, TW / 2);
                        angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                        thelength = 2 * radious * Math.Sin(angelRotate / 2);
                        double DfBR3 = thelength * Math.Cos(angel2);
                        double DfDR3 = thelength * Math.Sin(angel2);
                        double DfBL3 = thelength * Math.Sin(angel3);
                        double DfDL3 = thelength * Math.Cos(angel3);

                        radious = Math.Sqrt(Math.Pow(D - Y, 2) + Math.Pow(TW / 2, 2));
                        angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2(D - Y, TW / 2);
                        angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                        thelength = 2 * radious * Math.Sin(angelRotate / 2);
                        double DfBR4 = thelength * Math.Cos(angel2);
                        double DfDR4 = thelength * Math.Sin(angel2);
                        double DfBL4 = thelength * Math.Sin(angel3);
                        double DfDL4 = thelength * Math.Cos(angel3);

                        for (j = 1; j < 17; j++)
                        {
                            if (j > 8)
                            {
                                tx = Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                                ty = Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                                tz = Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                            }
                            if (j == 1 || j == 9)
                            {
                                tx1 = tx - (BF / 2 + DfBL1) * Math.Sin(angel) - ((Y - DfDL1) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty + (BF / 2 + DfBL1) * Math.Cos(angel) - ((Y - DfDL1) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz + (Y - DfDL1) * Math.Cos(angel1);
                            }
                            if (j == 2 || j == 10)
                            {
                                tx1 = tx + (BF / 2 - DfBR1) * Math.Sin(angel) - ((Y + DfDR1) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty - (BF / 2 - DfBR1) * Math.Cos(angel) - ((Y + DfDR1) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz + (Y + DfDR1) * Math.Cos(angel1);
                            }
                            if (j == 3 || j == 11)
                            {
                                tx1 = tx + (BF / 2 - DfBR2) * Math.Sin(angel) - ((Y - TF + DfDR2) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty - (BF / 2 - DfBR2) * Math.Cos(angel) - ((Y - TF + DfDR2) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz + (Y - TF + DfDR2) * Math.Cos(angel1);
                            }
                            if (j == 4 || j == 12)
                            {
                                tx1 = tx + (TW / 2 - DfBR3) * Math.Sin(angel) - ((Y - TF + DfDR3) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty - (TW / 2 - DfBR3) * Math.Cos(angel) - ((Y - TF + DfDR3) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz + (Y - TF + DfDR3) * Math.Cos(angel1);
                            }
                            if (j == 5 || j == 13)
                            {
                                tx1 = tx + (TW / 2 + DfBL4) * Math.Sin(angel) + (((D - Y) - DfDL4) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty - (TW / 2 + DfBL4) * Math.Cos(angel) + (((D - Y) - DfDL4) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz - ((D - Y) - DfDL4) * Math.Cos(angel1);
                            }
                            if (j == 6 || j == 14)
                            {
                                tx1 = tx - (TW / 2 - DfBR4) * Math.Sin(angel) + (((D - Y) + DfDR4) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty + (TW / 2 - DfBR4) * Math.Cos(angel) + (((D - Y) + DfDR4) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz - ((D - Y) + DfDR4) * Math.Cos(angel1);
                            }
                            if (j == 7 || j == 15)
                            {
                                tx1 = tx - (TW / 2 + DfBL3) * Math.Sin(angel) - ((Y - TF - DfDL3) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty + (TW / 2 + DfBL3) * Math.Cos(angel) - ((Y - TF - DfDL3) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz + (Y - TF - DfDL3) * Math.Cos(angel1);
                            }
                            if (j == 8 || j == 16)
                            {
                                tx1 = tx - (BF / 2 + DfBL2) * Math.Sin(angel) - ((Y - TF - DfDL2) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty + (BF / 2 + DfBL2) * Math.Cos(angel) - ((Y - TF - DfDL2) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz + (Y - TF - DfDL2) * Math.Cos(angel1);
                            }
                            tx1 = tx1;
                            ty1 = -1 * ty1;
                            tz1 = -1 * tz1;
                            Math3DP.PointG cube3dM = new Math3DP.PointG(tx1, ty1, tz1);
                            cube3dM.RotateX = Myglobals.tXValue;
                            cube3dM.RotateY = Myglobals.tYValue;
                            cube3dM.RotateZ = Myglobals.tZValue;
                            cube3dM.DrawPoint();
                            P[j].X = Myglobals.TheX3d;
                            P[j].Y = Myglobals.TheY3d;
                        }
                        //الرسم
                        P1 = new Point[10];//الوجوه المقابلة
                        for (j = 1; j < 9; j++)
                        {
                            P1[j] = P[j];
                        }
                        P1[9] = P1[1];
                        for (j = 1; j < 8; j++)
                        {
                            g.DrawLine(pen, P1[j], P1[j + 1]);
                        }
                        g.DrawLine(pen, P1[1], P1[9]);
                        using (Brush brush = new SolidBrush(Color.FromArgb(60, Color.DarkSlateBlue)))
                        {
                            g.FillPolygon(brush, P1);
                        }
                        for (j = 1; j < 9; j++)
                        {
                            P1[j] = P[8 + j];
                        }
                        P1[9] = P1[1];
                        for (j = 1; j < 8; j++)
                        {
                            g.DrawLine(pen, P1[j], P1[j + 1]);
                        }
                        g.DrawLine(pen, P1[1], P1[9]);
                        using (Brush brush = new SolidBrush(Color.FromArgb(60, Color.DarkSlateBlue)))
                        {
                            g.FillPolygon(brush, P1);
                        }
                        P1 = new Point[6];

                        for (int k = 1; k < 9; k++)
                        {
                            if (k == 1)
                            {
                                P1[1] = P[1];
                                P1[2] = P[9];
                                P1[3] = P[10];
                                P1[4] = P[2];
                            }
                            if (k == 2)
                            {
                                P1[1] = P[2];
                                P1[2] = P[10];
                                P1[3] = P[11];
                                P1[4] = P[3];
                            }
                            if (k == 3)
                            {
                                P1[1] = P[4];
                                P1[2] = P[12];
                                P1[3] = P[11];
                                P1[4] = P[3];
                            }
                            if (k == 4)
                            {
                                P1[1] = P[4];
                                P1[2] = P[12];
                                P1[3] = P[13];
                                P1[4] = P[5];
                            }
                            if (k == 5)
                            {
                                P1[1] = P[5];
                                P1[2] = P[13];
                                P1[3] = P[14];
                                P1[4] = P[6];
                            }
                            if (k == 6)
                            {
                                P1[1] = P[6];
                                P1[2] = P[14];
                                P1[3] = P[15];
                                P1[4] = P[7];
                            }
                            if (k == 7)
                            {
                                P1[1] = P[7];
                                P1[2] = P[15];
                                P1[3] = P[16];
                                P1[4] = P[8];
                            }
                            if (k == 8)
                            {
                                P1[1] = P[8];
                                P1[2] = P[16];
                                P1[3] = P[9];
                                P1[4] = P[1];
                            }

                            P1[5] = P1[1];
                            for (j = 1; j < 4; j++)
                            {
                                g.DrawLine(pen, P1[j], P1[j + 1]);
                            }
                            g.DrawLine(pen, P1[1], P1[4]);
                            using (Brush brush = new SolidBrush(Color.FromArgb(60, Color.DarkSlateBlue)))
                            {
                                g.FillPolygon(brush, P1);
                            }
                        }
                        #endregion
                    }
                    if (DESIGNATION == "C")
                    {
                        #region//شكل C
                        double angelvertical = 0;
                        if (angelvertical == 0)
                        {
                            tx1 = Joint.XReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                            ty1 = Joint.YReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                            tz1 = Joint.ZReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                            tx = Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                            ty = Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                            tz = Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        }
                        else
                        {
                            tx = Joint.XReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                            ty = Joint.YReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                            tz = Joint.ZReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                            tx1 = Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                            ty1 = Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                            tz1 = Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        }
                        double xDiff = tx - tx1;
                        double yDiff = ty - ty1;
                        double zDiff = tz - tz1;
                        double LDiff = Math.Sqrt(Math.Pow(xDiff, 2) + Math.Pow(yDiff, 2));
                        double angel = Math.Atan2(yDiff, xDiff);
                        double angel1 = Math.Atan2(zDiff, LDiff);
                        /* if (tx == tx1 & ty == ty1)//عمود
                         {
                             angelRotate = angelRotate + Math.PI / 2;
                         }*/
                        double radious = Math.Sqrt(Math.Pow((D / 2), 2) + Math.Pow(X, 2));
                        double angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2((D / 2), X);
                        double angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                        double thelength = 2 * radious * Math.Sin(angelRotate / 2);
                        double DfBL1 = thelength * Math.Sin(angel3);
                        double DfDL1 = thelength * Math.Cos(angel3);

                        radious = Math.Sqrt(Math.Pow((D / 2), 2) + Math.Pow((BF - X), 2));
                        angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2((D / 2), (BF - X));
                        angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                        thelength = 2 * radious * Math.Sin(angelRotate / 2);
                        double DfBL2 = thelength * Math.Cos(angel2);
                        double DfDL2 = thelength * Math.Sin(angel2);

                        radious = Math.Sqrt(Math.Pow((D / 2 - TF), 2) + Math.Pow((BF - X), 2));
                        angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2((D / 2 - TF), (BF - X));
                        angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                        thelength = 2 * radious * Math.Sin(angelRotate / 2);
                        double DfBL3 = thelength * Math.Cos(angel2);
                        double DfDL3 = thelength * Math.Sin(angel2);

                        radious = Math.Sqrt(Math.Pow((D / 2 - TF), 2) + Math.Pow((X - TW), 2));
                        angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2((D / 2 - TF), (X - TW));
                        angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                        thelength = 2 * radious * Math.Sin(angelRotate / 2);
                        double DfBL4 = thelength * Math.Sin(angel3);
                        double DfDL4 = thelength * Math.Cos(angel3);
                        radious = Math.Sqrt(Math.Pow((D / 2 - TF), 2) + Math.Pow((X - TW), 2));
                        angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2((D / 2 - TF), (X - TW));
                        angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                        thelength = 2 * radious * Math.Sin(angelRotate / 2);
                        double DfBL5 = thelength * Math.Cos(angel2);
                        double DfDL5 = thelength * Math.Sin(angel2);
                        radious = Math.Sqrt(Math.Pow((D / 2 - TF), 2) + Math.Pow((BF - X), 2));
                        angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2((D / 2 - TF), (BF - X));
                        angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                        thelength = 2 * radious * Math.Sin(angelRotate / 2);
                        double DfBL6 = thelength * Math.Sin(angel3);
                        double DfDL6 = thelength * Math.Cos(angel3);
                        radious = Math.Sqrt(Math.Pow(D / 2, 2) + Math.Pow((BF - X), 2));
                        angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2(D / 2, (BF - X));
                        angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                        thelength = 2 * radious * Math.Sin(angelRotate / 2);
                        double DfBL7 = thelength * Math.Sin(angel3);
                        double DfDL7 = thelength * Math.Cos(angel3);
                        radious = Math.Sqrt(Math.Pow(D / 2, 2) + Math.Pow(X, 2));
                        angel2 = ((Math.PI - angelRotate) / 2) - Math.Atan2(D / 2, X);
                        angel3 = ((Math.PI / 2 - angelRotate)) - angel2;
                        thelength = 2 * radious * Math.Sin(angelRotate / 2);
                        double DfBL8 = thelength * Math.Cos(angel2);
                        double DfDL8 = thelength * Math.Sin(angel2);

                        for (j = 1; j < 17; j++)
                        {
                            if (j > 8)
                            {
                                if (angelvertical == 0)
                                {
                                    tx = Joint.XReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                                    ty = Joint.YReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                                    tz = Joint.ZReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                                }
                                else
                                {
                                    tx = Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                                    ty = Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                                    tz = Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                                }
                            }
                            if (j == 1 || j == 9)
                            {
                                tx1 = tx - (X + DfBL1) * Math.Sin(angel) - ((D / 2 - DfDL1) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty + (X + DfBL1) * Math.Cos(angel) - ((D / 2 - DfDL1) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz + (D / 2 - DfDL1) * Math.Cos(angel1);
                            }

                            if (j == 2 || j == 10)
                            {
                                tx1 = tx + (BF - X - DfBL2) * Math.Sin(angel) - ((D / 2 + DfDL2) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty - (BF - X - DfBL2) * Math.Cos(angel) - ((D / 2 + DfDL2) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz + (D / 2 + DfDL2) * Math.Cos(angel1);
                            }
                            if (j == 3 || j == 11)
                            {
                                tx1 = tx + (BF - X - DfBL3) * Math.Sin(angel) - ((D / 2 - TF + DfDL3) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty - (BF - X - DfBL3) * Math.Cos(angel) - ((D / 2 - TF + DfDL3) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz + (D / 2 - TF + DfDL3) * Math.Cos(angel1);
                            }

                            if (j == 4 || j == 12)
                            {
                                tx1 = tx - (X - TW + DfBL4) * Math.Sin(angel) - ((D / 2 - TF - DfDL4) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty + (X - TW + DfBL4) * Math.Cos(angel) - ((D / 2 - TF - DfDL4) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz + (D / 2 - DfDL4 - TF) * Math.Cos(angel1);
                            }
                            if (j == 5 || j == 13)
                            {
                                tx1 = tx - (X - TW - DfBL5) * Math.Sin(angel) + ((D / 2 - TF + DfDL5) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty + (X - TW - DfBL5) * Math.Cos(angel) + ((D / 2 - TF + DfDL5) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz - (D / 2 - TF + DfDL5) * Math.Cos(angel1);
                            }
                            if (j == 6 || j == 14)
                            {
                                tx1 = tx + (BF - X + DfBL6) * Math.Sin(angel) + ((D / 2 - TF - DfDL6) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty - (BF - X + DfBL6) * Math.Cos(angel) + ((D / 2 - TF - DfDL6) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz - (D / 2 - TF - DfDL6) * Math.Cos(angel1);
                            }
                            if (j == 7 || j == 15)
                            {
                                tx1 = tx + (BF - X + DfBL7) * Math.Sin(angel) + ((D / 2 - DfDL7) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty - (BF - X + DfBL7) * Math.Cos(angel) + ((D / 2 - DfDL7) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz - (D / 2 - DfDL7) * Math.Cos(angel1);
                            }
                            if (j == 8 || j == 16)
                            {
                                tx1 = tx - (X - DfBL8) * Math.Sin(angel) + ((D / 2 + DfDL8) * Math.Sin(angel1) * Math.Cos(angel));
                                ty1 = ty + (X - DfBL8) * Math.Cos(angel) + ((D / 2 + DfDL8) * Math.Sin(angel1) * Math.Sin(angel));
                                tz1 = tz - (D / 2 + DfDL8) * Math.Cos(angel1);
                            }
                            tx1 = tx1;
                            ty1 = -1 * ty1;
                            tz1 = -1 * tz1;
                            Math3DP.PointG cube3dM = new Math3DP.PointG(tx1, ty1, tz1);
                            cube3dM.RotateX = Myglobals.tXValue;
                            cube3dM.RotateY = Myglobals.tYValue;
                            cube3dM.RotateZ = Myglobals.tZValue;
                            cube3dM.DrawPoint();
                            P[j].X = Myglobals.TheX3d;
                            P[j].Y = Myglobals.TheY3d;
                        }
                        //الرسم
                        P1 = new Point[10];//الوجوه المقابلة
                        for (j = 1; j < 9; j++)
                        {
                            P1[j] = P[j];
                        }
                        P1[9] = P1[1];
                        for (j = 1; j < 8; j++)
                        {
                            g.DrawLine(pen, P1[j], P1[j + 1]);
                        }
                        g.DrawLine(pen, P1[1], P1[9]);
                        using (Brush brush = new SolidBrush(Color.FromArgb(60, Color.DarkSlateBlue)))
                        {
                            g.FillPolygon(brush, P1);
                        }
                        for (j = 1; j < 9; j++)
                        {
                            P1[j] = P[8 + j];
                        }
                        P1[9] = P1[1];
                        for (j = 1; j < 8; j++)
                        {
                            g.DrawLine(pen, P1[j], P1[j + 1]);
                        }
                        g.DrawLine(pen, P1[1], P1[9]);
                        using (Brush brush = new SolidBrush(Color.FromArgb(60, Color.DarkSlateBlue)))
                        {
                            g.FillPolygon(brush, P1);
                        }
                        P1 = new Point[6];
                        for (int k = 1; k < 9; k++)//الوجوه الجانبية
                        {
                            if (k == 1)
                            {
                                P1[1] = P[1];
                                P1[2] = P[9];
                                P1[3] = P[10];
                                P1[4] = P[2];
                            }
                            if (k == 2)
                            {
                                P1[1] = P[2];
                                P1[2] = P[10];
                                P1[3] = P[11];
                                P1[4] = P[3];
                            }
                            if (k == 3)
                            {
                                P1[1] = P[4];
                                P1[2] = P[12];
                                P1[3] = P[11];
                                P1[4] = P[3];
                            }
                            if (k == 4)
                            {
                                P1[1] = P[4];
                                P1[2] = P[12];
                                P1[3] = P[13];
                                P1[4] = P[5];
                            }
                            if (k == 5)
                            {
                                P1[1] = P[5];
                                P1[2] = P[13];
                                P1[3] = P[14];
                                P1[4] = P[6];
                            }
                            if (k == 6)
                            {
                                P1[1] = P[6];
                                P1[2] = P[14];
                                P1[3] = P[15];
                                P1[4] = P[7];
                            }
                            if (k == 7)
                            {
                                P1[1] = P[8];
                                P1[2] = P[16];
                                P1[3] = P[15];
                                P1[4] = P[7];
                            }
                            if (k == 8)
                            {
                                P1[1] = P[1];
                                P1[2] = P[9];
                                P1[3] = P[16];
                                P1[4] = P[8];
                            }
                            P1[5] = P1[1];
                            for (j = 1; j < 4; j++)
                            {
                                g.DrawLine(pen, P1[j], P1[j + 1]);
                            }
                            g.DrawLine(pen, P1[1], P1[4]);
                            using (Brush brush = new SolidBrush(Color.FromArgb(60, Color.DarkSlateBlue)))
                            {
                                g.FillPolygon(brush, P1);
                            }
                        }
                        #endregion
                    }
                }
            }
            g.Dispose();
        }
        public void DrawCubeFloor3d()
        {
            Graphics g = Graphics.FromImage(((MainForm)mainForm).pictureBox2.Image);
            Pen pen = new Pen(Color.Black, 1f);
            int tx1 = 0;
            int ty1 = 0;
            int tx2 = 0;
            int ty2 = 0;
            int tx11 = 0;
            int ty11 = 0;
            int tx21 = 0;
            int ty21 = 0;
            //-----------------------------------
            //رسم البلاطات على الفراغي        
            for (int i = 1; i < Shell.Number + 1; i++)
            {
                if (Shell.Visible[i] == 0 & Shell.Type[i] != 4)
                {
                    #region//السطح الأول
                    double thick = 0;
                    FaceNumber = FaceNumber + 1;
                    if (Shell.Type[i] == 1) thick = Slab.Thickness[Shell.Section[i]];
                    if (Shell.Type[i] == 2) thick = Wall.Thickness[Shell.Section[i]];
                    if (Shell.Type[i] == 3) thick = Wall.Thickness[Shell.Section[i]];
                    Point[] P = new Point[Shell.PointNumbers[i] + 2];
                    double[] PrealX = new double[Shell.PointNumbers[i] + 2];
                    double[] PrealY = new double[Shell.PointNumbers[i] + 2];
                    double[] PrealZ = new double[Shell.PointNumbers[i] + 2];
                    int firstpoint = 1;
                    int secondjoint = 2;
                    double tx1D = Joint.XReal[Shell.JointNo[i, 1]];
                    double ty1D = Joint.YReal[Shell.JointNo[i, 1]];
                    double tz1D = Joint.ZReal[Shell.JointNo[i, 1]];
                    double tx2D = Joint.XReal[Shell.JointNo[i, 2]];
                    double ty2D = Joint.YReal[Shell.JointNo[i, 2]];
                    double tz2D = Joint.ZReal[Shell.JointNo[i, 2]];
                    double tx3D = 0;
                    double ty3D = 0;
                    double tz3D = 0;
                    for (int k = 1; k < Shell.PointNumbers[i] + 1; k++)
                    {
                        if (k != firstpoint & k != secondjoint)
                        {
                            tx3D = Joint.XReal[Shell.JointNo[i, k]];
                            ty3D = Joint.YReal[Shell.JointNo[i, k]];
                            tz3D = Joint.ZReal[Shell.JointNo[i, k]];
                            double kx = (tx3D - tx1D);
                            double ky = (ty3D - ty1D);
                            double kz = (tz3D - tz1D);
                            double kV = 0;
                            if (kx != 0) kV = (tx2D - tx1D) / kx;
                            if (ky != 0) kV = (ty2D - ty1D) / ky;
                            if (kz != 0) kV = (tz2D - tz1D) / kz;
                            int tah = 0;
                            if (kV * kx == (tx2D - tx1D) & kV * ky == (ty2D - ty1D) & kV * kz == (tz2D - tz1D)) tah = 1;
                            if (tah == 0) break;
                        }
                    }
                    double var2 = tx2D - tx1D;
                    double var3 = tx3D - tx1D;
                    double var5 = ty2D - ty1D;
                    double var6 = ty3D - ty1D;
                    double var8 = tz2D - tz1D;
                    double var9 = tz3D - tz1D;
                    double varA = var5 * var9 - var8 * var6;//a
                    double varB = var8 * var3 - var2 * var9;//b
                    double varC = var2 * var6 - var5 * var3;//c
                    double varD = -varA * tx1D - varB * ty1D - varC * tz1D;//d                  
                    double varT = thick / Math.Sqrt(Math.Pow(varA, 2) + Math.Pow(varB, 2) + Math.Pow(varC, 2));
                    double tx0D = 0;
                    double ty0D = 0;
                    double tz0D = 0;
                    double XFORCG = 0;
                    double YFORCG = 0;
                    double ZFORCG = 0;
                    for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                    {
                        tx1D = Joint.XReal[Shell.JointNo[i, j]];
                        ty1D = Joint.YReal[Shell.JointNo[i, j]];
                        tz1D = Joint.ZReal[Shell.JointNo[i, j]];
                        tx0D = tx1D + varA * varT;
                        ty0D = ty1D + varB * varT;
                        tz0D = tz1D + varC * varT;
                        tx1D = tx0D;
                        ty1D = -1 * ty0D;
                        tz1D = -1 * tz0D;
                        Math3DP.PointG Joint3dM = new Math3DP.PointG(tx1D, ty1D, tz1D);
                        Joint3dM.RotateX = Myglobals.tXValue;
                        Joint3dM.RotateY = Myglobals.tYValue;
                        Joint3dM.RotateZ = Myglobals.tZValue;
                        Joint3dM.DrawPoint();
                        P[j].X = Myglobals.TheX3d;
                        P[j].Y = Myglobals.TheY3d;
                        FaceP3D[FaceNumber, j] = P[j];
                        PrealX[j] = tx0D;
                        PrealY[j] = ty0D;
                        PrealZ[j] = tz0D;
                        XFORCG = XFORCG + tx0D;
                        YFORCG = YFORCG + ty0D;
                        ZFORCG = ZFORCG + tz0D;
                    }
                    P[Shell.PointNumbers[i] + 1].X = P[1].X;
                    P[Shell.PointNumbers[i] + 1].Y = P[1].Y;
                    double CenterX = Math.Round((XFORCG) / Shell.PointNumbers[i], 3);
                    double CenterY = Math.Round((YFORCG) / Shell.PointNumbers[i], 3);
                    double CenterZ = Math.Round((ZFORCG) / Shell.PointNumbers[i], 3);
                    DistanceP0[FaceNumber] = -Math.Sqrt(Math.Pow(CenterX - Myglobals.EyeX, 2) + Math.Pow(CenterY - Myglobals.EyeY, 2) + Math.Pow(CenterZ - Myglobals.EyeZ, 2));
                    P[Shell.PointNumbers[i] + 1].X = P[1].X;
                    P[Shell.PointNumbers[i] + 1].Y = P[1].Y;
                    FaceP3D[FaceNumber, Shell.PointNumbers[i] + 1] = P[Shell.PointNumbers[i] + 1];
                    FaceType[FaceNumber] = 2;//وجه علوي أساسي
                    FaceShellTypeShell[FaceNumber] = Shell.Type[i];
                    FacePNumbers[FaceNumber] = Shell.PointNumbers[i];
                    #endregion
                    #region  //الجوانب
                    for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                    {
                        XFORCG = Joint.XReal[Shell.JointNo[i, j]];
                        YFORCG = Joint.YReal[Shell.JointNo[i, j]];
                        ZFORCG = Joint.ZReal[Shell.JointNo[i, j]];
                        tx1 = Joint.X3d[Shell.JointNo[i, j]];
                        ty1 = Joint.Y3d[Shell.JointNo[i, j]];
                        tx11 = P[j].X;
                        ty11 = P[j].Y;
                        XFORCG = XFORCG + PrealX[j];
                        YFORCG = YFORCG + PrealY[j];
                        ZFORCG = ZFORCG + PrealZ[j];
                        if (j != Shell.PointNumbers[i])
                        {
                            tx2 = Joint.X3d[Shell.JointNo[i, j + 1]];
                            ty2 = Joint.Y3d[Shell.JointNo[i, j + 1]];
                            tx21 = P[j + 1].X;
                            ty21 = P[j + 1].Y;
                            XFORCG = XFORCG + PrealX[j+1] + Joint.XReal[Shell.JointNo[i, j+1]]; 
                            YFORCG = YFORCG + PrealY[j+1] + Joint.YReal[Shell.JointNo[i, j+1]]; 
                            ZFORCG = ZFORCG + PrealZ[j+1] + Joint.ZReal[Shell.JointNo[i, j+1]]; 
                        }
                        else
                        {
                            tx2 = Joint.X3d[Shell.JointNo[i, 1]];
                            ty2 = Joint.Y3d[Shell.JointNo[i, 1]];
                            tx21 = P[1].X;
                            ty21 = P[1].Y;
                            XFORCG = XFORCG + PrealX[1] + Joint.XReal[Shell.JointNo[i, 1]];
                            YFORCG = YFORCG + PrealY[1] + Joint.YReal[Shell.JointNo[i, 1]];
                            ZFORCG = ZFORCG + PrealZ[1] + Joint.ZReal[Shell.JointNo[i, 1]]; 
                        }
                        Point[] P1 = new Point[6];
                        P1[1].X = tx1;
                        P1[1].Y = ty1;
                        P1[2].X = tx2;
                        P1[2].Y = ty2;
                        P1[3].X = tx21;
                        P1[3].Y = ty21;
                        P1[4].X = tx11;
                        P1[4].Y = ty11;
                        P1[5].X = P1[1].X;
                        P1[5].Y = P1[1].Y;
                        FaceNumber = FaceNumber + 1;
                        for (int k = 1; k < 4 + 2; k++)
                        {
                            FaceP3D[FaceNumber, k] = P1[k];
                        }
                        CenterX = Math.Round((XFORCG) / 4, 3);
                        CenterY = Math.Round((YFORCG) / 4, 3);
                        CenterZ = Math.Round((ZFORCG) / 4, 3);
                        DistanceP0[FaceNumber] = -Math.Sqrt(Math.Pow(CenterX - Myglobals.EyeX, 2) + Math.Pow(CenterY - Myglobals.EyeY, 2) + Math.Pow(CenterZ - Myglobals.EyeZ, 2));
                        FaceType[FaceNumber] = 3;
                        FaceShellTypeShell[FaceNumber] = Shell.Type[i];
                        FacePNumbers[FaceNumber] = 4;
                    }
                    #endregion
                }
            }
            for (int i = 1; i < Shell.Number + 1; i++)
            {
                if (Shell.Visible[i] == 0 & Shell.Type[i] == 4)
                {
                    #region//السطح الأول
                    double thick = 0;
                    if (Shell.Type[i] == 1) thick = Slab.Thickness[Shell.Section[i]];
                    if (Shell.Type[i] == 2) thick = Wall.Thickness[Shell.Section[i]];
                    if (Shell.Type[i] == 3) thick = Wall.Thickness[Shell.Section[i]];
                    Point[] P = new Point[Shell.PointNumbers[i] + 2];
                    int firstpoint = 1;
                    int secondjoint = 2;
                    double tx1D = Joint.XReal[Shell.JointNo[i, 1]];
                    double ty1D = Joint.YReal[Shell.JointNo[i, 1]];
                    double tz1D = Joint.ZReal[Shell.JointNo[i, 1]];
                    double tx2D = Joint.XReal[Shell.JointNo[i, 2]];
                    double ty2D = Joint.YReal[Shell.JointNo[i, 2]];
                    double tz2D = Joint.ZReal[Shell.JointNo[i, 2]];
                    double tx3D = 0;
                    double ty3D = 0;
                    double tz3D = 0;
                    for (int k = 1; k < Shell.PointNumbers[i] + 1; k++)
                    {
                        if (k != firstpoint & k != secondjoint)
                        {
                            tx3D = Joint.XReal[Shell.JointNo[i, k]];
                            ty3D = Joint.YReal[Shell.JointNo[i, k]];
                            tz3D = Joint.ZReal[Shell.JointNo[i, k]];
                            double kx = (tx3D - tx1D);
                            double ky = (ty3D - ty1D);
                            double kz = (tz3D - tz1D);
                            double kV = 0;
                            if (kx != 0) kV = (tx2D - tx1D) / kx;
                            if (ky != 0) kV = (ty2D - ty1D) / ky;
                            if (kz != 0) kV = (tz2D - tz1D) / kz;
                            int tah = 0;
                            if (kV * kx == (tx2D - tx1D) & kV * ky == (ty2D - ty1D) & kV * kz == (tz2D - tz1D)) tah = 1;
                            if (tah == 0) break;
                        }
                    }
                    double var2 = tx2D - tx1D;
                    double var3 = tx3D - tx1D;
                    double var5 = ty2D - ty1D;
                    double var6 = ty3D - ty1D;
                    double var8 = tz2D - tz1D;
                    double var9 = tz3D - tz1D;
                    double varA = var5 * var9 - var8 * var6;//a
                    double varB = var8 * var3 - var2 * var9;//b
                    double varC = var2 * var6 - var5 * var3;//c
                    double varD = -varA * tx1D - varB * ty1D - varC * tz1D;//d                  
                    double varT = thick / Math.Sqrt(Math.Pow(varA, 2) + Math.Pow(varB, 2) + Math.Pow(varC, 2));
                    for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                    {
                        tx1D = Joint.XReal[Shell.JointNo[i, j]];
                        ty1D = Joint.YReal[Shell.JointNo[i, j]];
                        tz1D = Joint.ZReal[Shell.JointNo[i, j]];
                        double tx0D = tx1D + varA * varT;
                        double ty0D = ty1D + varB * varT;
                        double tz0D = tz1D + varC * varT;
                        tx1D = tx0D;
                        ty1D = -1 * ty0D;
                        tz1D = -1 * tz0D;
                        Math3DP.PointG Joint3dM = new Math3DP.PointG(tx1D, ty1D, tz1D);
                        Joint3dM.RotateX = Myglobals.tXValue;
                        Joint3dM.RotateY = Myglobals.tYValue;
                        Joint3dM.RotateZ = Myglobals.tZValue;
                        Joint3dM.DrawPoint();
                        P[j].X = Myglobals.TheX3d;
                        P[j].Y = Myglobals.TheY3d;
                    }
                    P[Shell.PointNumbers[i] + 1].X = P[1].X;
                    P[Shell.PointNumbers[i] + 1].Y = P[1].Y;
                    using (Brush brush = new SolidBrush(Color.White))
                    {
                        g.FillPolygon(brush, P);
                    }
                    #endregion
                    #region  //الجوانب
                    for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                    {
                        tx1 = Joint.X3d[Shell.JointNo[i, j]];
                        ty1 = Joint.Y3d[Shell.JointNo[i, j]];
                        tx11 = P[j].X;
                        ty11 = P[j].Y;
                        if (j != Shell.PointNumbers[i])
                        {
                            tx2 = Joint.X3d[Shell.JointNo[i, j + 1]];
                            ty2 = Joint.Y3d[Shell.JointNo[i, j + 1]];
                            tx21 = P[j + 1].X;
                            ty21 = P[j + 1].Y;
                        }
                        else
                        {
                            tx2 = Joint.X3d[Shell.JointNo[i, 1]];
                            ty2 = Joint.Y3d[Shell.JointNo[i, 1]];
                            tx21 = P[1].X;
                            ty21 = P[1].Y;
                        }
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        g.DrawLine(pen, tx11, ty11, tx21, ty21);
                        g.DrawLine(pen, tx1, ty1, tx11, ty11);
                        Point[] P1 = new Point[6];
                        P1[1].X = tx1;
                        P1[1].Y = ty1;
                        P1[2].X = tx2;
                        P1[2].Y = ty2;
                        P1[3].X = tx21;
                        P1[3].Y = ty21;
                        P1[4].X = tx11;
                        P1[4].Y = ty11;
                        P1[5].X = P1[1].X;
                        P1[5].Y = P1[1].Y;
                        using (Brush brush = new SolidBrush(Color.White))
                        {
                            g.FillPolygon(brush, P1);
                        }
                    }
                    #endregion
                }
            }
        }
        
        public void DrawFaces3d()
        {
            if (FaceNumber > 0)
            {
                Graphics g = Graphics.FromImage(((MainForm)mainForm).pictureBox2.Image);
                Pen pen = new Pen(Color.DarkSlateBlue, 1);
                int[] cArray = new int[FaceNumber + 1];
                for (int kj = 1; kj < FaceNumber + 1; kj++)
                {
                    cArray[kj] = kj;
                }
                var newList = cArray.OrderBy(x => DistanceP0[x]).ToList();

                double Maxdis = -DistanceP0[newList[0]];
                double Mindis = -DistanceP0[newList[FaceNumber - 1]];
                double Ddis = Maxdis - Mindis;
                int RMax = Color.DimGray.R;
                int RMin = Color.WhiteSmoke.R;
                int BMax = Color.DimGray.B;
                int BMin = Color.WhiteSmoke.B;
                int GMax = Color.DimGray.G;
                int GMin = Color.WhiteSmoke.G;
                int DR = RMax - RMin;
                int DB = BMax - BMin;
                int DG = GMax - GMin;

                int RMax1 = Color.DarkRed.R;
                int RMin1 = Color.Red.R;
                int BMax1 = Color.DarkRed.B;
                int BMin1 = Color.Red.B;
                int GMax1 = Color.DarkRed.G;
                int GMin1 = Color.Red.G;
                int DR1 = RMax1 - RMin1;
                int DB1 = BMax1 - BMin1;
                int DG1 = GMax1 - GMin1;
                for (int iai = 0; iai < FaceNumber + 1; iai++)
                {
                    int i = newList[iai];
                    if (i != 0)
                    {
                        Point[] P = new Point[FacePNumbers[i] + 2];
                        for (int j = 1; j < FacePNumbers[i] + 2; j++)
                        {
                            P[j] = FaceP3D[i, j];
                        }
                        if (Myglobals.DrowRealShape == 0 || Myglobals.ExtrudedShell == 0)
                        {
                            if (FaceShellTypeShell[i] == 0)//جائز
                            {
                                using (Brush brush = new SolidBrush(Color.FromArgb(80, Color.DarkSlateBlue)))
                                {
                                    g.FillPolygon(brush, P);
                                }
                            }
                            if (FaceShellTypeShell[i] == 1)
                            {
                                using (Brush brush = new SolidBrush(Color.FromArgb(90, Color.Gray)))
                                {
                                    g.FillPolygon(brush, P);
                                }
                            }
                            if (FaceShellTypeShell[i] == 2)
                            {
                                using (Brush brush = new SolidBrush(Color.FromArgb(90, Color.Red)))
                                {
                                    g.FillPolygon(brush, P);
                                }
                            }
                            if (FaceShellTypeShell[i] == 3)
                            {
                                HatchBrush hBrush = new HatchBrush(HatchStyle.Percent50, Color.Brown, Color.FromArgb(80, Color.White));
                                g.FillPolygon(hBrush, P);
                            }
                        }
                        if (Myglobals.DrowRealShape == 1 & Myglobals.ExtrudedShell == 1)
                        {
                            if (FaceShellTypeShell[i] == 0)//جائز
                            {
                                double aws = -DistanceP0[i];
                                int R = RMin + (int)(DR * (aws - Mindis) / Ddis);
                                int B = BMin + (int)(DB * (aws - Mindis) / Ddis);
                                int G = GMin + (int)(DG * (aws - Mindis) / Ddis);
                                using (Brush brush = new SolidBrush(Color.FromArgb(255, (byte)R, (byte)G, (byte)B)))
                                {
                                    g.FillPolygon(brush, P);
                                }
                            }

                            if (FaceShellTypeShell[i] == 1)
                            {
                                double aws = -DistanceP0[i];
                                int R = RMin + (int)(DR * (aws - Mindis) / Ddis);
                                int B = BMin + (int)(DB * (aws - Mindis) / Ddis);
                                int G = GMin + (int)(DG * (aws - Mindis) / Ddis);
                                using (Brush brush = new SolidBrush(Color.FromArgb(255, (byte)R, (byte)G, (byte)B)))
                                {
                                    g.FillPolygon(brush, P);
                                }
                            }
                            if (FaceShellTypeShell[i] == 2)
                            {
                                double aws = -DistanceP0[i];
                                int R = RMin1 + (int)(DR1 * (aws - Mindis) / Ddis);
                                int B = BMin1 + (int)(DB1 * (aws - Mindis) / Ddis);
                                int G = GMin1 + (int)(DG1 * (aws - Mindis) / Ddis);
                                using (Brush brush = new SolidBrush(Color.FromArgb(255, (byte)R, (byte)G, (byte)B)))
                                {
                                    g.FillPolygon(brush, P);
                                }
                            }
                            if (FaceShellTypeShell[i] == 3)
                            {
                                if (FaceType[i] == 1) g.FillPolygon(Brushes.Brown, P);
                            }
                        }
                        for (int jf = 1; jf < FacePNumbers[i]; jf++)
                        {
                            g.DrawLine(pen, P[jf], P[jf + 1]);
                        }
                        g.DrawLine(pen, P[1], P[FacePNumbers[i]]);
                    }
                }
                g.Dispose();
            }
        }
        
        private void checkintersection(double X1, double Y1, double X2, double Y2, double X3, double Y3, double X4, double Y4)
        {
            int THETAHKIK = 0;
            double x0 = 0;
            double y0 = 0;
            if (X2 == X1 & Y3 == Y4)//شاقولي و أفقي
            {
                int tah = 0;
                if (X1 < X3 & X1 < X4) tah = 1;
                if (X1 > X3 & X1 > X4) tah = 1;
                if (Y3 < Y1 & Y3 < Y2) tah = 1;
                if (Y3 > Y1 & Y3 > Y2) tah = 1;
                if (tah == 1) goto end100;
                x0 = X1;
                y0 = Y3;
                x0 = Math.Round(x0, 3);
                y0 = Math.Round(y0, 3);
                if (x0 > X3 & x0 > X4) goto end100;
                if (x0 < X3 & x0 < X4) goto end100;
                if (y0 > Y3 & y0 > Y4) goto end100;
                if (y0 < Y3 & y0 < Y4) goto end100;
                if (x0 > X1 & x0 > X2) goto end100;
                if (x0 < X1 & x0 < X2) goto end100;
                if (y0 > Y1 & y0 > Y2) goto end100;
                if (y0 < Y1 & y0 < Y2) goto end100;
                THETAHKIK = 1;
                goto end100;
            }
            if (X2 == X1 & Y3 != Y4 & X4 != X3)//شاقولي و مائل
            {
                x0 = X1;
                y0 = ((x0 - X3) / (X4 - X3)) * (Y4 - Y3) + Y3;
                int tah = 0;
                if (y0 < Y1 & y0 < Y2) tah = 1;
                if (y0 > Y1 & y0 > Y2) tah = 1;
                if (tah == 1) goto end100;
                x0 = Math.Round(x0, 3);
                y0 = Math.Round(y0, 3);
                if (x0 > X3 & x0 > X4) goto end100;
                if (x0 < X3 & x0 < X4) goto end100;
                if (y0 > Y3 & y0 > Y4) goto end100;
                if (y0 < Y3 & y0 < Y4) goto end100;
                if (x0 > X1 & x0 > X2) goto end100;
                if (x0 < X1 & x0 < X2) goto end100;
                if (y0 > Y1 & y0 > Y2) goto end100;
                if (y0 < Y1 & y0 < Y2) goto end100;
                THETAHKIK = 1;
                goto end100;
            }
            double bast = (Y3 - Y1) * (X4 - X3) * (X2 - X1);//حالة عامة
            bast = bast + X1 * (Y2 - Y1) * (X4 - X3);
            bast = bast - X3 * (Y4 - Y3) * (X2 - X1);
            double makam = (Y2 - Y1) * (X4 - X3);
            makam = makam - (Y4 - Y3) * (X2 - X1);
            if (makam != 0)
            {
                x0 = bast / makam;
                y0 = (((Y2 - Y1) / (X2 - X1)) * (x0 - X1)) + Y1;
                x0 = Math.Round(x0, 3);
                y0 = Math.Round(y0, 3);
                if (x0 > X3 & x0 > X4) goto end100;
                if (x0 < X3 & x0 < X4) goto end100;
                if (y0 > Y3 & y0 > Y4) goto end100;
                if (y0 < Y3 & y0 < Y4) goto end100;
                if (x0 > X1 & x0 > X2) goto end100;
                if (x0 < X1 & x0 < X2) goto end100;
                if (y0 > Y1 & y0 > Y2) goto end100;
                if (y0 < Y1 & y0 < Y2) goto end100;
                THETAHKIK = 1;
            }
        end100: { }
            if (forarias == 1)
            {
                if (Math.Abs(X1 - x0) < 0.008 & Math.Abs(Y1 - y0) < 0.008) THETAHKIK = 0;
                if (Math.Abs(X2 - x0) < 0.008 & Math.Abs(Y2 - y0) < 0.008) THETAHKIK = 0;
            }
            INTERSECTION = THETAHKIK;
            TheX0 = x0;
            TheY0 = y0;
        }
        private void FindArias()
        {
            #region//تعريفات
            Myglobals.AriaNo = 0;
            Myglobals.AriaPointNo = new int[GridPoint.Number2d + 2];
            Myglobals.p1X = new double[GridPoint.Number2d + 2, GridPoint.Number2d + 2];
            Myglobals.p1Y = new double[GridPoint.Number2d + 2, GridPoint.Number2d + 2];
            int AllLine = GridLine.OnX + GridLine.OnY + GridLine.OnXY;
            double X1 = 0;
            double Y1 = 0;
            double X2 = 0;
            double Y2 = 0;
            double X3 = 0;
            double Y3 = 0;
            double X4 = 0;
            double Y4 = 0;
            double x = 0;
            double y = 0;
            int StartJoint = 0;
            int LastJoint = 0;
            int PointADD = 0;
            int m = 0;
            #endregion
            for (int i = 1; i < GridPoint.Number2d + 1; i++)
            {
                int check0 = 0;
                double[] pX = new double[GridPoint.Number2d + 2];
                double[] pY = new double[GridPoint.Number2d + 2];
                int[] PointNO = new int[GridPoint.Number2d + 1];
                X1 = GridPoint.XReal[i];
                Y1 = GridPoint.YReal[i];
                m = 1;
                pX[m] = X1;
                pY[m] = Y1;
                #region//ايجاد العقد التي تقع ضمن المساحة
                PointADD = 0;
                for (int j = 1; j < GridPoint.Number2d + 1; j++)
                {
                    if (j != i)
                    {
                        X2 = GridPoint.XReal[j];
                        Y2 = GridPoint.YReal[j];
                        for (int k = 1; k < AllLine + 1; k++)
                        {
                            if (GridLine.Visible[k] == 0)
                            {
                                X3 = GridLine.X1Real[k];
                                Y3 = GridLine.Y1Real[k];
                                X4 = GridLine.X2Real[k];
                                Y4 = GridLine.Y2Real[k];
                                #region//التحقق من أن العقدتين على نفس المحور
                                x = X1;
                                y = Y1;
                                int chk1 = 0;
                                if (X3 == X4 & X3 == x)
                                {
                                    if (y >= Y3 & y <= Y4) chk1 = 1;
                                    if (y <= Y3 & y >= Y4) chk1 = 1;
                                }
                                if (Y3 == Y4 & Y3 == y)
                                {
                                    if (x >= X3 & x <= X4) chk1 = 1;
                                    if (x <= X3 & x >= X4) chk1 = 1;
                                }
                                if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk1 = 1;
                                x = X2;
                                y = Y2;
                                int chk2 = 0;
                                if (X3 == X4 & X3 == x)
                                {
                                    if (y >= Y3 & y <= Y4) chk2 = 1;
                                    if (y <= Y3 & y >= Y4) chk2 = 1;
                                }
                                if (Y3 == Y4 & Y3 == y)
                                {
                                    if (x >= X3 & x <= X4) chk2 = 1;
                                    if (x <= X3 & x >= X4) chk2 = 1;
                                }
                                if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk2 = 1;
                                if (chk1 == 1 & chk2 == 1)
                                {
                                    goto nextk;
                                }
                                #endregion
                                checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                                if (INTERSECTION == 1) goto nextj;
                            nextk: { }
                            }
                        }
                        PointADD = PointADD + 1;
                        PointNO[PointADD] = j;
                    }
                nextj: { }
                }
                #endregion
                #region //ترتيب العقد عكس عقارب الساعة
                StartJoint = i;
                double[] aTanA = new double[PointADD + 1];
                for (int j = 1; j < PointADD + 1; j++)
                {
                    aTanA[j] = Math.Atan2(GridPoint.YReal[PointNO[j]] - GridPoint.YReal[StartJoint], GridPoint.XReal[PointNO[j]] - GridPoint.XReal[StartJoint]);
                    if (Math.Abs(aTanA[j] - Convert.ToDouble((Math.PI))) < 0.001) aTanA[j] = -1 * aTanA[j];
                }
                double[] Apower = new double[PointADD + 1];
                int[] Bpower = new int[PointADD + 1];
                double[] Cpower = new double[PointADD + 1];
                int[] Dpower = new int[PointADD + 1];
                int[] Epower = new int[PointADD + 1];
                int Row = 0;
                int N = PointADD;
                int NN = PointADD;
                double MAXI = +1000000;
                int MAXId = 0;
                for (int j = 1; j < N + 1; j++)
                {
                    Apower[j] = aTanA[j];
                    Bpower[j] = j;
                    Dpower[j] = PointNO[j];
                }
                for (int j = 1; j < N + 1; j++)
                {
                    MAXI = 1000000;
                    for (int k = 1; k < NN + 1; k++)
                    {
                        if (MAXI >= Apower[k])
                        {
                            MAXI = Apower[k];
                            MAXId = Dpower[k];
                            Row = Bpower[k];
                        }
                    }
                    Cpower[j] = MAXI;
                    Epower[j] = MAXId;
                    NN = NN - 1;
                    for (int k = Row; k < NN + 1; k++)
                    {
                        Apower[k] = Apower[k + 1];
                        Bpower[k] = k;
                        Dpower[k] = Dpower[k + 1];
                    }
                }
                for (int j = 1; j < N + 1; j++)
                {
                    aTanA[j] = Cpower[j];
                    PointNO[j] = Epower[j];
                }
                #endregion
                #region// إيجاد أول عقدة ترتبط مع العقدة صفر بمحور
                for (int j = 1; j < PointADD + 1; j++)
                {
                    X2 = GridPoint.XReal[PointNO[j]];
                    Y2 = GridPoint.YReal[PointNO[j]];
                    for (int k = 1; k < AllLine + 1; k++)
                    {
                        if (GridLine.Visible[k] == 0)
                        {
                            X3 = GridLine.X1Real[k];
                            Y3 = GridLine.Y1Real[k];
                            X4 = GridLine.X2Real[k];
                            Y4 = GridLine.Y2Real[k];
                            x = X1;
                            y = Y1;
                            int chk1 = 0;
                            if (X3 == X4 & X3 == x)
                            {
                                if (y >= Y3 & y <= Y4) chk1 = 1;
                                if (y <= Y3 & y >= Y4) chk1 = 1;
                            }
                            if (Y3 == Y4 & Y3 == y)
                            {
                                if (x >= X3 & x <= X4) chk1 = 1;
                                if (x <= X3 & x >= X4) chk1 = 1;
                            }
                            if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk1 = 1;
                            x = X2;
                            y = Y2;
                            int chk2 = 0;
                            if (X3 == X4 & X3 == x)
                            {
                                if (y >= Y3 & y <= Y4) chk2 = 1;
                                if (y <= Y3 & y >= Y4) chk2 = 1;
                            }
                            if (Y3 == Y4 & Y3 == y)
                            {
                                if (x >= X3 & x <= X4) chk2 = 1;
                                if (x <= X3 & x >= X4) chk2 = 1;
                            }
                            if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk2 = 1;
                            if (chk1 == 1 & chk2 == 1)
                            {
                                LastJoint = j;
                                m = m + 1;
                                pX[m] = X2;
                                pY[m] = Y2;
                                goto Endj;
                            }
                        }
                    }
                }
            Endj: { }
                #endregion
                for (int j = LastJoint + 1; j < PointADD + 1; j++)
                {
                    X2 = GridPoint.XReal[PointNO[j]];
                    Y2 = GridPoint.YReal[PointNO[j]];
                    #region//التحقق من أن العقدتين على نفس المحور
                    for (int k = 1; k < AllLine + 1; k++)
                    {
                        if (GridLine.Visible[k] == 0)
                        {
                            X3 = GridLine.X1Real[k];
                            Y3 = GridLine.Y1Real[k];
                            X4 = GridLine.X2Real[k];
                            Y4 = GridLine.Y2Real[k];
                            x = GridPoint.XReal[PointNO[LastJoint]];
                            y = GridPoint.YReal[PointNO[LastJoint]];
                            int chk1 = 0;
                            if (X3 == X4 & X3 == x)
                            {
                                if (y >= Y3 & y <= Y4) chk1 = 1;
                                if (y <= Y3 & y >= Y4) chk1 = 1;
                            }
                            if (Y3 == Y4 & Y3 == y)
                            {
                                if (x >= X3 & x <= X4) chk1 = 1;
                                if (x <= X3 & x >= X4) chk1 = 1;
                            }
                            if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk1 = 1;
                            x = X2;
                            y = Y2;
                            int chk2 = 0;
                            if (X3 == X4 & X3 == x)
                            {
                                if (y >= Y3 & y <= Y4) chk2 = 1;
                                if (y <= Y3 & y >= Y4) chk2 = 1;
                            }
                            if (Y3 == Y4 & Y3 == y)
                            {
                                if (x >= X3 & x <= X4) chk2 = 1;
                                if (x <= X3 & x >= X4) chk2 = 1;
                            }
                            if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk2 = 1;
                            if (chk1 == 1 & chk2 == 1)
                            {
                                #region//التحقق من ان العقدة تقع على محور يتصل بالعقدة صفر لننهي المساحة
                                for (int s = 1; s < AllLine + 1; s++)
                                {
                                    if (GridLine.Visible[s] == 0)
                                    {
                                        X3 = GridLine.X1Real[s];
                                        Y3 = GridLine.Y1Real[s];
                                        X4 = GridLine.X2Real[s];
                                        Y4 = GridLine.Y2Real[s];
                                        x = X1;
                                        y = Y1;
                                        int chk10 = 0;
                                        if (X3 == X4 & X3 == x)
                                        {
                                            if (y >= Y3 & y <= Y4) chk10 = 1;
                                            if (y <= Y3 & y >= Y4) chk10 = 1;
                                        }
                                        if (Y3 == Y4 & Y3 == y)
                                        {
                                            if (x >= X3 & x <= X4) chk10 = 1;
                                            if (x <= X3 & x >= X4) chk10 = 1;
                                        }
                                        if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk10 = 1;
                                        x = X2;
                                        y = Y2;
                                        int chk20 = 0;
                                        if (X3 == X4 & X3 == x)
                                        {
                                            if (y >= Y3 & y <= Y4) chk20 = 1;
                                            if (y <= Y3 & y >= Y4) chk20 = 1;
                                        }
                                        if (Y3 == Y4 & Y3 == y)
                                        {
                                            if (x >= X3 & x <= X4) chk20 = 1;
                                            if (x <= X3 & x >= X4) chk20 = 1;
                                        }
                                        if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk20 = 1;
                                        if (chk10 == 1 & chk20 == 1)
                                        {
                                            m = m + 1;//وجدنا المساحة
                                            pX[m] = X2;
                                            pY[m] = Y2;
                                            m = m + 1;
                                            pX[m] = X1;
                                            pY[m] = Y1;
                                            check0 = 1;
                                            goto nexti;
                                        }
                                    }
                                }
                                #endregion
                                LastJoint = j;
                                m = m + 1;
                                pX[m] = X2;
                                pY[m] = Y2;
                                goto nextj;
                            }
                        }
                    }
                    goto nexti;
                    #endregion
                nextj: { }
                }
            nexti: { }
                #region//التحقق من أن المساحة غير مكررة
                if (m > 2 & check0 == 1)
                {
                    int tah = 0;
                    int s = 0;
                    for (int j = 1; j < Myglobals.AriaNo + 1; j++)
                    {
                        if (Myglobals.AriaPointNo[j] == m)
                        {
                            s = 0;
                            for (int k = 1; k < m + 1; k++)
                            {
                                x = pX[k];
                                y = pY[k];
                                for (int l = 1; l < m + 1; l++)
                                {
                                    if (x == Myglobals.p1X[j, l] & y == Myglobals.p1Y[j, l]) s = s + 1;
                                }
                            }
                            if (s >= m + 1)
                            {
                                tah = 1;
                                break;
                            }
                        }
                    }
                    if (tah == 0)
                    {
                        int tahtah1 = 0;
                        int tahtah2 = 0;
                        for (int j = 1; j < m ; j++)
                        {
                            if (pX[j] != pX[j+1]) tahtah1 = 1;
                            if (pY[j] != pY[j + 1]) tahtah2 = 1;
                        }
                        if (tahtah1 != 0 & tahtah2 != 0)
                        {
                            Myglobals.AriaNo = Myglobals.AriaNo + 1;
                            Myglobals.AriaPointNo[Myglobals.AriaNo] = m;
                            for (int j = 1; j < m + 1; j++)
                            {
                                Myglobals.p1X[Myglobals.AriaNo, j] = pX[j];
                                Myglobals.p1Y[Myglobals.AriaNo, j] = pY[j];
                            }
                        }
                    }
                }
                #endregion
                pX = new double[GridPoint.Number2d + 2];
                pY = new double[GridPoint.Number2d + 2];
            }
        }
        private void FindArias1()
        {
            #region//تعريفات
            int AllLine = GridLine.OnX + GridLine.OnY + GridLine.OnXY;
            double X1 = 0;
            double Y1 = 0;
            double X2 = 0;
            double Y2 = 0;
            double X3 = 0;
            double Y3 = 0;
            double X4 = 0;
            double Y4 = 0;
            double x = 0;
            double y = 0;
            int StartJoint = 0;
            int LastJoint = 0;
            int PointADD = 0;
            int m = 0;
            #endregion
            for (int i = 1; i < GridPoint.Number2d + 1; i++)
            {
                int check0 = 0;
                double[] pX = new double[GridPoint.Number2d + 2];
                double[] pY = new double[GridPoint.Number2d + 2];
                int[] PointNO = new int[GridPoint.Number2d + 1];
                X1 = GridPoint.XReal[i];
                Y1 = GridPoint.YReal[i];
                m = 1;
                pX[m] = X1;
                pY[m] = Y1;
                #region//ايجاد العقد التي تقع ضمن المساحة
                PointADD = 0;
                for (int j = 1; j < GridPoint.Number2d + 1; j++)
                {
                    if (j != i)
                    {
                        X2 = GridPoint.XReal[j];
                        Y2 = GridPoint.YReal[j];
                        for (int k = 1; k < AllLine + 1; k++)
                        {
                            if (GridLine.Visible[k] == 0)
                            {
                                X3 = GridLine.X1Real[k];
                                Y3 = GridLine.Y1Real[k];
                                X4 = GridLine.X2Real[k];
                                Y4 = GridLine.Y2Real[k];
                                #region//التحقق من أن العقدتين على نفس المحور
                                x = X1;
                                y = Y1;
                                int chk1 = 0;
                                if (X3 == X4 & X3 == x)
                                {
                                    if (y >= Y3 & y <= Y4) chk1 = 1;
                                    if (y <= Y3 & y >= Y4) chk1 = 1;
                                }
                                if (Y3 == Y4 & Y3 == y)
                                {
                                    if (x >= X3 & x <= X4) chk1 = 1;
                                    if (x <= X3 & x >= X4) chk1 = 1;
                                }
                                if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk1 = 1;
                                x = X2;
                                y = Y2;
                                int chk2 = 0;
                                if (X3 == X4 & X3 == x)
                                {
                                    if (y >= Y3 & y <= Y4) chk2 = 1;
                                    if (y <= Y3 & y >= Y4) chk2 = 1;
                                }
                                if (Y3 == Y4 & Y3 == y)
                                {
                                    if (x >= X3 & x <= X4) chk2 = 1;
                                    if (x <= X3 & x >= X4) chk2 = 1;
                                }
                                if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk2 = 1;
                                if (chk1 == 1 & chk2 == 1)
                                {
                                    goto nextk;
                                }
                                #endregion
                                checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                                if (INTERSECTION == 1) goto nextj;
                            nextk: { }
                            }
                        }
                        PointADD = PointADD + 1;
                        PointNO[PointADD] = j;
                    }
                nextj: { }
                }
                #endregion
                #region //ترتيب العقد مع عقارب الساعة
                StartJoint = i;
                double[] aTanA = new double[PointADD + 1];
                for (int j = 1; j < PointADD + 1; j++)
                {
                    aTanA[j] = Math.Atan2(GridPoint.YReal[PointNO[j]] - GridPoint.YReal[StartJoint], GridPoint.XReal[PointNO[j]] - GridPoint.XReal[StartJoint]);
                    if (Math.Abs(aTanA[j] - Convert.ToDouble((Math.PI))) < 0.001) aTanA[j] = -1 * aTanA[j];
                }
                double[] Apower = new double[PointADD + 1];
                int[] Bpower = new int[PointADD + 1];
                double[] Cpower = new double[PointADD + 1];
                int[] Dpower = new int[PointADD + 1];
                int[] Epower = new int[PointADD + 1];
                int Row = 0;
                int N = PointADD;
                int NN = PointADD;
                double MAXI = -1000000;
                int MAXId = 0;
                for (int j = 1; j < N + 1; j++)
                {
                    Apower[j] = aTanA[j];
                    Bpower[j] = j;
                    Dpower[j] = PointNO[j];
                }
                for (int j = 1; j < N + 1; j++)
                {
                    MAXI = -1000000;
                    for (int k = 1; k < NN + 1; k++)
                    {
                        if (MAXI <= Apower[k])
                        {
                            MAXI = Apower[k];
                            MAXId = Dpower[k];
                            Row = Bpower[k];
                        }
                    }
                    Cpower[j] = MAXI;
                    Epower[j] = MAXId;
                    NN = NN - 1;
                    for (int k = Row; k < NN + 1; k++)
                    {
                        Apower[k] = Apower[k + 1];
                        Bpower[k] = k;
                        Dpower[k] = Dpower[k + 1];
                    }
                }
                for (int j = 1; j < N + 1; j++)
                {
                    aTanA[j] = Cpower[j];
                    PointNO[j] = Epower[j];
                }
                #endregion
                #region// إيجاد أول عقدة ترتبط مع العقدة صفر بمحور
                for (int j = 1; j < PointADD + 1; j++)
                {
                    X2 = GridPoint.XReal[PointNO[j]];
                    Y2 = GridPoint.YReal[PointNO[j]];
                    for (int k = 1; k < AllLine + 1; k++)
                    {
                        if (GridLine.Visible[k] == 0)
                        {
                            X3 = GridLine.X1Real[k];
                            Y3 = GridLine.Y1Real[k];
                            X4 = GridLine.X2Real[k];
                            Y4 = GridLine.Y2Real[k];
                            x = X1;
                            y = Y1;
                            int chk1 = 0;
                            if (X3 == X4 & X3 == x)
                            {
                                if (y >= Y3 & y <= Y4) chk1 = 1;
                                if (y <= Y3 & y >= Y4) chk1 = 1;
                            }
                            if (Y3 == Y4 & Y3 == y)
                            {
                                if (x >= X3 & x <= X4) chk1 = 1;
                                if (x <= X3 & x >= X4) chk1 = 1;
                            }
                            if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk1 = 1;
                            x = X2;
                            y = Y2;
                            int chk2 = 0;
                            if (X3 == X4 & X3 == x)
                            {
                                if (y >= Y3 & y <= Y4) chk2 = 1;
                                if (y <= Y3 & y >= Y4) chk2 = 1;
                            }
                            if (Y3 == Y4 & Y3 == y)
                            {
                                if (x >= X3 & x <= X4) chk2 = 1;
                                if (x <= X3 & x >= X4) chk2 = 1;
                            }
                            if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk2 = 1;
                            if (chk1 == 1 & chk2 == 1)
                            {
                                LastJoint = j;
                                m = m + 1;
                                pX[m] = X2;
                                pY[m] = Y2;
                                goto Endj;
                            }
                        }
                    }
                }
            Endj: { }
                #endregion
                for (int j = LastJoint + 1; j < PointADD + 1; j++)
                {
                    X2 = GridPoint.XReal[PointNO[j]];
                    Y2 = GridPoint.YReal[PointNO[j]];
                    #region//التحقق من أن العقدتين على نفس المحور
                    for (int k = 1; k < AllLine + 1; k++)
                    {
                        if (GridLine.Visible[k] == 0)
                        {
                            X3 = GridLine.X1Real[k];
                            Y3 = GridLine.Y1Real[k];
                            X4 = GridLine.X2Real[k];
                            Y4 = GridLine.Y2Real[k];
                            x = GridPoint.XReal[PointNO[LastJoint]];
                            y = GridPoint.YReal[PointNO[LastJoint]];
                            int chk1 = 0;
                            if (X3 == X4 & X3 == x)
                            {
                                if (y >= Y3 & y <= Y4) chk1 = 1;
                                if (y <= Y3 & y >= Y4) chk1 = 1;
                            }
                            if (Y3 == Y4 & Y3 == y)
                            {
                                if (x >= X3 & x <= X4) chk1 = 1;
                                if (x <= X3 & x >= X4) chk1 = 1;
                            }
                            if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk1 = 1;
                            x = X2;
                            y = Y2;
                            int chk2 = 0;
                            if (X3 == X4 & X3 == x)
                            {
                                if (y >= Y3 & y <= Y4) chk2 = 1;
                                if (y <= Y3 & y >= Y4) chk2 = 1;
                            }
                            if (Y3 == Y4 & Y3 == y)
                            {
                                if (x >= X3 & x <= X4) chk2 = 1;
                                if (x <= X3 & x >= X4) chk2 = 1;
                            }
                            if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk2 = 1;
                            if (chk1 == 1 & chk2 == 1)
                            {
                                #region//التحقق من ان العقدة تقع على محور يتصل بالعقدة صفر لننهي المساحة
                                for (int s = 1; s < AllLine + 1; s++)
                                {
                                    if (GridLine.Visible[s] == 0)
                                    {
                                        X3 = GridLine.X1Real[s];
                                        Y3 = GridLine.Y1Real[s];
                                        X4 = GridLine.X2Real[s];
                                        Y4 = GridLine.Y2Real[s];
                                        x = X1;
                                        y = Y1;
                                        int chk10 = 0;
                                        if (X3 == X4 & X3 == x)
                                        {
                                            if (y >= Y3 & y <= Y4) chk10 = 1;
                                            if (y <= Y3 & y >= Y4) chk10 = 1;
                                        }
                                        if (Y3 == Y4 & Y3 == y)
                                        {
                                            if (x >= X3 & x <= X4) chk10 = 1;
                                            if (x <= X3 & x >= X4) chk10 = 1;
                                        }
                                        if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk10 = 1;
                                        x = X2;
                                        y = Y2;
                                        int chk20 = 0;
                                        if (X3 == X4 & X3 == x)
                                        {
                                            if (y >= Y3 & y <= Y4) chk20 = 1;
                                            if (y <= Y3 & y >= Y4) chk20 = 1;
                                        }
                                        if (Y3 == Y4 & Y3 == y)
                                        {
                                            if (x >= X3 & x <= X4) chk20 = 1;
                                            if (x <= X3 & x >= X4) chk20 = 1;
                                        }
                                        if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk20 = 1;
                                        if (chk10 == 1 & chk20 == 1)
                                        {
                                            m = m + 1;//وجدنا المساحة
                                            pX[m] = X2;
                                            pY[m] = Y2;
                                            m = m + 1;
                                            pX[m] = X1;
                                            pY[m] = Y1;
                                            check0 = 1;
                                            goto nexti;
                                        }
                                    }
                                }
                                #endregion
                                LastJoint = j;
                                m = m + 1;
                                pX[m] = X2;
                                pY[m] = Y2;
                                goto nextj;
                            }
                        }
                    }
                    goto nexti;
                    #endregion
                nextj: { }
                }
            nexti: { }
                #region//التحقق من أن المساحة غير مكررة
            if (m > 2 & check0 == 1)
            {
                int tah = 0;
                int s = 0;
                for (int j = 1; j < Myglobals.AriaNo + 1; j++)
                {
                    if (Myglobals.AriaPointNo[j] == m)
                    {
                        s = 0;
                        for (int k = 1; k < m + 1; k++)
                        {
                            x = pX[k];
                            y = pY[k];
                            for (int l = 1; l < m + 1; l++)
                            {
                                if (x == Myglobals.p1X[j, l] & y == Myglobals.p1Y[j, l]) s = s + 1;
                            }
                        }
                        if (s >= m + 1)
                        {
                            tah = 1;
                            break;
                        }
                    }
                }
                if (tah == 0)
                {
                    int tahtah1 = 0;
                    int tahtah2 = 0;
                    for (int j = 1; j < m; j++)
                    {
                        if (pX[j] != pX[j + 1]) tahtah1 = 1;
                        if (pY[j] != pY[j + 1]) tahtah2 = 1;
                    }
                    if (tahtah1 != 0 & tahtah2 != 0)
                    {
                        Myglobals.AriaNo = Myglobals.AriaNo + 1;
                        Myglobals.AriaPointNo[Myglobals.AriaNo] = m;
                        for (int j = 1; j < m + 1; j++)
                        {
                            Myglobals.p1X[Myglobals.AriaNo, j] = pX[j];
                            Myglobals.p1Y[Myglobals.AriaNo, j] = pY[j];
                        }
                    }
                }
            }
                #endregion
                pX = new double[GridPoint.Number2d + 2];
                pY = new double[GridPoint.Number2d + 2];
            }
        }
        public void FindAriasELEV()
        {
            int PointADD = Myglobals.elevPointNumbers[Myglobals.Selectedelev];
            #region
            int numberX = 0;
            int[] Apower = new int[PointADD + 1];
            int[] Bpower = new int[PointADD + 1];
            int[] Cpower = new int[PointADD + 1];
            int Row = 0;
            int N = PointADD;
            int NN = PointADD;
            int[] LINEX = new int[500];
            for (int j = 1; j < N + 1; j++)
            {
                LINEX[j] = GridPoint.Xelev[j];
            }
            int MAXI = +1000000;
            for (int j = 1; j < N + 1; j++)
            {
                Apower[j] = LINEX[j];
                Bpower[j] = j;
            }
            for (int j = 1; j < N + 1; j++)
            {
                MAXI = 1000000;
                for (int k = 1; k < NN + 1; k++)
                {
                    if (MAXI >= Apower[k])
                    {
                        MAXI = Apower[k];
                        Row = Bpower[k];
                    }
                }
                Cpower[j] = MAXI;
                NN = NN - 1;
                for (int k = Row; k < NN + 1; k++)
                {
                    Apower[k] = Apower[k + 1];
                    Bpower[k] = k;
                }
            }
            for (int j = 1; j < N + 1; j++)
            {
                LINEX[j] = Cpower[j];
            }
            int m = 0;
            for (int j = 1; j < N + 1; j++)
            {
                if (LINEX[j] != LINEX[j + 1])
                {
                    m = m + 1;
                    Cpower[m] = LINEX[j];
                }
            }
            for (int j = 1; j < m + 1; j++)
            {
                LINEX[j] = Cpower[j];
            }
            numberX = m;
            #endregion
            #region
            int numberY = 0;
            Apower = new int[PointADD + 1];
            Bpower = new int[PointADD + 1];
            Cpower = new int[PointADD + 1];
            Row = 0;
            N = PointADD;
            NN = PointADD;
            int[] LINEY = new int[500];
            for (int j = 1; j < N + 1; j++)
            {
                LINEY[j] = GridPoint.Yelev[j];
            }
            int MAYI = +1000000;
            for (int j = 1; j < N + 1; j++)
            {
                Apower[j] = LINEY[j];
                Bpower[j] = j;
            }
            for (int j = 1; j < N + 1; j++)
            {
                MAYI = 1000000;
                for (int k = 1; k < NN + 1; k++)
                {
                    if (MAYI >= Apower[k])
                    {
                        MAYI = Apower[k];
                        Row = Bpower[k];
                    }
                }
                Cpower[j] = MAYI;
                NN = NN - 1;
                for (int k = Row; k < NN + 1; k++)
                {
                    Apower[k] = Apower[k + 1];
                    Bpower[k] = k;
                }
            }
            for (int j = 1; j < N + 1; j++)
            {
                LINEY[j] = Cpower[j];
            }
            m = 0;
            for (int j = 1; j < N + 1; j++)
            {
                if (LINEY[j] != LINEY[j + 1])
                {
                    m = m + 1;
                    Cpower[m] = LINEY[j];
                }
            }
            for (int j = 1; j < m + 1; j++)
            {
                LINEY[j] = Cpower[j];
            }
            numberY = m;
            #endregion
            Myglobals.AriaNoELEV = 0;
            for (int i = 1; i < numberX; i++)
            {
                for (int j = 1; j < numberY; j++)
                {
                    Myglobals.AriaNoELEV = Myglobals.AriaNoELEV + 1;
                    Myglobals.AriaPointNoELEV[Myglobals.AriaNoELEV] = 5;

                    Myglobals.p1XELEV[Myglobals.AriaNoELEV, 1] = LINEX[i];
                    Myglobals.p1YELEV[Myglobals.AriaNoELEV, 1] = LINEY[j];
                    for (int k = 1; k < PointADD + 1; k++)
                    {
                        if (GridPoint.Xelev[k] == Myglobals.p1XELEV[Myglobals.AriaNoELEV, 1] & GridPoint.Yelev[k] == Myglobals.p1YELEV[Myglobals.AriaNoELEV, 1])
                        {
                            Myglobals.p1XELEVR[Myglobals.AriaNoELEV, 1] = Myglobals.elevPointX[Myglobals.Selectedelev, k];
                            Myglobals.p1YELEVR[Myglobals.AriaNoELEV, 1] = Myglobals.elevPointY[Myglobals.Selectedelev, k];
                            Myglobals.p1ZELEVR[Myglobals.AriaNoELEV, 1] = Myglobals.elevPointZ[Myglobals.Selectedelev, k];
                            break;
                        }
                    }

                    Myglobals.p1XELEV[Myglobals.AriaNoELEV, 2] = LINEX[i + 1];
                    Myglobals.p1YELEV[Myglobals.AriaNoELEV, 2] = LINEY[j];
                    for (int k = 1; k < PointADD + 1; k++)
                    {
                        if (GridPoint.Xelev[k] == Myglobals.p1XELEV[Myglobals.AriaNoELEV, 2] & GridPoint.Yelev[k] == Myglobals.p1YELEV[Myglobals.AriaNoELEV, 2])
                        {
                            Myglobals.p1XELEVR[Myglobals.AriaNoELEV, 2] = Myglobals.elevPointX[Myglobals.Selectedelev, k];
                            Myglobals.p1YELEVR[Myglobals.AriaNoELEV, 2] = Myglobals.elevPointY[Myglobals.Selectedelev, k];
                            Myglobals.p1ZELEVR[Myglobals.AriaNoELEV, 2] = Myglobals.elevPointZ[Myglobals.Selectedelev, k];
                            break;
                        }
                    }

                    Myglobals.p1XELEV[Myglobals.AriaNoELEV, 3] = LINEX[i + 1];
                    Myglobals.p1YELEV[Myglobals.AriaNoELEV, 3] = LINEY[j + 1];
                    for (int k = 1; k < PointADD + 1; k++)
                    {
                        if (GridPoint.Xelev[k] == Myglobals.p1XELEV[Myglobals.AriaNoELEV, 3] & GridPoint.Yelev[k] == Myglobals.p1YELEV[Myglobals.AriaNoELEV, 3])
                        {
                            Myglobals.p1XELEVR[Myglobals.AriaNoELEV, 3] = Myglobals.elevPointX[Myglobals.Selectedelev, k];
                            Myglobals.p1YELEVR[Myglobals.AriaNoELEV, 3] = Myglobals.elevPointY[Myglobals.Selectedelev, k];
                            Myglobals.p1ZELEVR[Myglobals.AriaNoELEV, 3] = Myglobals.elevPointZ[Myglobals.Selectedelev, k];
                            break;
                        }
                    }

                    Myglobals.p1XELEV[Myglobals.AriaNoELEV, 4] = LINEX[i];
                    Myglobals.p1YELEV[Myglobals.AriaNoELEV, 4] = LINEY[j + 1];
                    for (int k = 1; k < PointADD + 1; k++)
                    {
                        if (GridPoint.Xelev[k] == Myglobals.p1XELEV[Myglobals.AriaNoELEV, 4] & GridPoint.Yelev[k] == Myglobals.p1YELEV[Myglobals.AriaNoELEV, 4])
                        {
                            Myglobals.p1XELEVR[Myglobals.AriaNoELEV, 4] = Myglobals.elevPointX[Myglobals.Selectedelev, k];
                            Myglobals.p1YELEVR[Myglobals.AriaNoELEV, 4] = Myglobals.elevPointY[Myglobals.Selectedelev, k];
                            Myglobals.p1ZELEVR[Myglobals.AriaNoELEV, 4] = Myglobals.elevPointZ[Myglobals.Selectedelev, k];
                            break;
                        }
                    }

                    Myglobals.p1XELEV[Myglobals.AriaNoELEV, 5] = Myglobals.p1XELEV[Myglobals.AriaNoELEV, 1];
                    Myglobals.p1YELEV[Myglobals.AriaNoELEV, 5] = Myglobals.p1YELEV[Myglobals.AriaNoELEV, 1];

                    Myglobals.p1XELEVR[Myglobals.AriaNoELEV, 5] = Myglobals.p1XELEVR[Myglobals.AriaNoELEV, 1];
                    Myglobals.p1YELEVR[Myglobals.AriaNoELEV, 5] = Myglobals.p1YELEVR[Myglobals.AriaNoELEV, 1];
                    Myglobals.p1ZELEVR[Myglobals.AriaNoELEV, 5] = Myglobals.p1ZELEVR[Myglobals.AriaNoELEV, 1];
                }
            }
        }
        private void CalculateSurfaceDistance1(double tx1D, double ty1D, double tz1D, double tx2D, double ty2D, double tz2D, double tx3D, double ty3D, double tz3D, double tx4D, double ty4D, double tz4D)
        {
            double X0 = Myglobals.EyeX;
            double Y0 = Myglobals.EyeY;
            double Z0 = Myglobals.EyeZ;

            double Xcenter = (tx1D + tx2D + tx3D + tx4D) / 4;
            double Ycenter = (ty1D + ty2D + ty3D + ty4D) / 4;
            double Zcenter = (tz1D + tz2D + tz3D + tz4D) / 4;
            Distance = Math.Sqrt(Math.Pow(Xcenter - X0, 2) + Math.Pow(Ycenter - Y0, 2) + Math.Pow(Zcenter - Z0, 2));
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
            return degrees;
        }

        double Xr4;
        double Yr4;
        double Zr4;
        double Xr5;
        double Yr5;
        double Zr5;
        private void FindPointOnLine3D(double X1, double Y1, double Z1, double X2, double Y2, double Z2, double X3, double Y3, double Z3)
        {
            double BeamLength = (Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2) + Math.Pow(Z2 - Z1, 2)));
            double LX = (X2 - X1) / BeamLength;
            double LY = (Y2 - Y1) / BeamLength;
            double LZ = (Z2 - Z1) / BeamLength;
            double BAST = (X1 - X3) * LX + (Y1 - Y3) * LY + (Z1 - Z3) * LZ;
            double MAKAM = Math.Pow(LX, 2) + Math.Pow(LY, 2) + Math.Pow(LZ, 2);
            double LEMDA = -BAST / MAKAM;
            Xr4 = X1 + LX * LEMDA;
            Yr4 = Y1 + LY * LEMDA;
            Zr4 = Z1 + LZ * LEMDA;
            LEMDA = 1;
            Xr5 = X1 + LX * LEMDA;
            Yr5 = Y1 + LY * LEMDA;
            Zr5 = Z1 + LZ * LEMDA;
        }
        private void FindIntersectionPoint3D(double X1, double Y1, double Z1, double X2, double Y2, double Z2, double X3, double Y3, double Z3, double X4, double Y4, double Z4)
        {
            double DX1 = (X2 - X1) ;
            double DY1 = (Y2 - Y1) ;
            double DZ1 = (Z2 - Z1) ;
            double DX2 = (X4 - X3);
            double DY2 = (Y4 - Y3);
            double DZ2 = (Z4 - Z3);
            double T = 0;
            double S = 0;
            double BAST = 0;
            double MAKAM = 0;
            double awal = 100;
            double tani = 0;
            if (DX1 == 0) DX1 = 0.0000001;
            if (DY1 == 0) DY1 = 0.0000001;
            if (DZ1 == 0) DZ1 = 0.0000001;
            if (DX2 == 0) DX2 = 0.0000001;
            if (DY2 == 0) DY2 = 0.0000001;
            if (DZ2 == 0) DZ2 = 0.0000001;
             MAKAM = DY1 * DX2 - DX1 * DY2;
             if (Math.Abs(Math.Round(MAKAM,4)) == 0) goto nextloop;
             BAST = Y3 * DX2 + X1 * DY2 - X3 * DY2 - Y1 * DX2;
             T = BAST / MAKAM;
             S = (X1 + T * DX1 - X3) / DX2;
             awal = Z1 + T * DZ1;
             tani = Z3 + S * DZ2;
             goto endloop;
         nextloop: { }
             MAKAM = DZ1 * DX2 - DX1 * DZ2;
             if (Math.Abs(Math.Round(MAKAM, 4)) == 0) goto nextloop1;
             BAST = Z3 * DX2 + X1 * DZ2 - X3 * DZ2 - Z1 * DX2;
             T = BAST / MAKAM;
             S = (X1 + T * DX1 - X3) / DX2;
             awal = Y1 + T * DY1;
             tani = Y3 + S * DY2;
             goto endloop;
         nextloop1: { }
             MAKAM = DZ1 * DY2 - DY1 * DZ2;
             if (Math.Abs(Math.Round(MAKAM, 4)) == 0) goto endloop;
             BAST = Z3 * DY2 + Y1 * DZ2 - Y3 * DZ2 - Z1 * DY2;
             T = BAST / MAKAM;
             S = (X1 + T * DX1 - X3) / DX2;
             awal = X1 + T * DX1;
             tani = X3 + S * DX2;
         endloop: { }
         if (Math.Abs(awal - tani) < 0.001)
         {
             INTERSECTION = 1;
             Xr4 = Math.Round(X1 + T * DX1, 3);
             Yr4 = Math.Round(Y1 + T * DY1, 3);
             Zr4 = Math.Round(Z1 + T * DZ1, 3);
             if (Zr4 < Z1 & Zr4 < Z2) INTERSECTION = 0;
             if (Zr4 > Z1 & Zr4 > Z2) INTERSECTION = 0;
         }

        }

    }
}
