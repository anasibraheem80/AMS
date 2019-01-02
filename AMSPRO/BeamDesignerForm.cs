using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.VisualBasic.PowerPacks;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Media.Media3D;
using System.Collections;

namespace AMSPRO
{
    public partial class BeamDesignerForm : Form
    {
        public BeamDesignerForm()
        {
            InitializeComponent();
            this.MouseWheel += new MouseEventHandler(pictureBox4_MouseWheel);
        }
        #region//تعريفات
        Form beamDesignerBarsForm = Application.OpenForms["BeamDesignerBarsForm"];
        // ((BeamDesignerBarsForm)beamDesignerBarsForm)

        int IfDoubleClick = 0;
        double Minval = 0;
        double Maxval = 0;
        int TempFile = 0;
        int TempSelectedFile = 0;
        int RedLineX2d = 0;
        double RedLineXR = 0;
        string[] TempFileName = new string[1000];
        
        public int ReinforcementBarNumber;
        public ReinforcementBars[] ReinforcementBar = new ReinforcementBars[1000];
        double MAXExtendS = -1000000;
        public  string  KindForBars = "";
        public int LastSection ;

        public double[,] SpanShearDis = new double[100, 4];
        public int[,] SpanShearDisX12d = new int[100, 4];
        public int[,] SpanShearDisY12d = new int[100, 4];
        public int[,] SpanShearDisX22d = new int[100, 4];
        public int[,] SpanShearDisY22d = new int[100, 4];
        public int[,] SpanShearDisSelected = new int[100, 4];

        public int[] BarsSpUpGR = new int[100];
        public int[,] BarsSpUpNo = new int[100, 100];
        public int[,] BarsSpUpD = new int[100, 100];
        public int[,] BarsSpUpKi = new int[100, 100];

        public int[] BarsSpDnGR = new int[100];
        public int[,] BarsSpDnNo = new int[100, 100];
        public int[,] BarsSpDnD = new int[100, 100];
        public int[,] BarsSpDnKi = new int[100, 100];

        public int[] BarsSuUpGR = new int[100];
        public int[,] BarsSuUpNo = new int[100, 100];
        public int[,] BarsSuUpD = new int[100, 100];
        public int[,] BarsSuUpKi = new int[100, 100];

        public int[] BarsSuDnGR = new int[100];
        public int[,] BarsSuDnNo = new int[100, 100];
        public int[,] BarsSuDnD = new int[100, 100];
        public int[,] BarsSuDnKi = new int[100, 100];

        public int[] ShearBarsSuLGR = new int[100];
        public int[,] ShearBarsSuLNo = new int[100, 100];
        public int[,] ShearBarsSuLD = new int[100, 100];
        public int[,] ShearBarsSuLDis = new int[100, 100];
        public int[,] ShearBarsSuLKi = new int[100, 100];

        public int[] ShearBarsSuRGR = new int[100];
        public int[,] ShearBarsSuRNo = new int[100, 100];
        public int[,] ShearBarsSuRD = new int[100, 100];
        public int[,] ShearBarsSuRDis = new int[100, 100];
        public int[,] ShearBarsSuRKi = new int[100, 100];

        public int[] ShearBarsSpGR = new int[100];
        public int[,] ShearBarsSpNo = new int[100, 100];
        public int[,] ShearBarsSpD = new int[100, 100];
        public int[,] ShearBarsSpDis = new int[100, 100];
        public int[,] ShearBarsSpKi = new int[100, 100];

        public int Spans;
        double[] BeamLength = new double[100];
        double[] SpanHeight = new double[100];
        public int[] SpanSection = new int[100];

        public int SelectedSpan = 0;
        public double AlValue;
        double[] SupportWeidth = new double[100];
        int[,] ShapePointX = new int[100, 5];
        int[,] ShapePointY = new int[100, 5];
        int[,] recMomentUpSuX = new int[100, 5];
        int[,] recMomentUpSuY = new int[100, 5];
        int[,] recMomentDnSuX = new int[100, 5];
        int[,] recMomentDnSuY = new int[100, 5];
        double[] MomentUpSu = new double[100];
        int[] BarsNoUpSu = new int[100];
        int[] BarsDUpSu = new int[100];
        double[] MomentDnSu = new double[100];
        int[] BarsNoDnSu = new int[100];
        int[] BarsDDnSu = new int[100];

        int[,] recMomentUpSpX = new int[100, 5];
        int[,] recMomentUpSpY = new int[100, 5];
        int[,] recMomentDnSpX = new int[100, 5];
        int[,] recMomentDnSpY = new int[100, 5];
        double[] MomentUpSp = new double[100];
        int[] BarsNoUpSp = new int[100];
        int[] BarsDUpSp = new int[100];
        double[] MomentDnSp = new double[100];
        int[] BarsNoDnSp = new int[100];
        int[] BarsDDnSp = new int[100];
        int[,] recShearSpX = new int[100, 5];
        int[,] recShearSpY = new int[100, 5];
        double[] ShearSp = new double[100];
        int[,] recShearSuLX = new int[100, 5];
        int[,] recShearSuLY = new int[100, 5];
        int[,] recShearSuRX = new int[100, 5];
        int[,] recShearSuRY = new int[100, 5];
        double[] ShearSuL = new double[100];
        double[] ShearSuR = new double[100];
        double BeamLengthALL = 0;
        int intlengthALL = 0;
        int BmpWidth;
        int BmpHeight;
        bool result;
        double Xtest;
        double Ytest;
        string hala = "";
        int BmpWidth2d;
        int BmpHeight2d;
        int INTERSECTION = 0;
        double TheX0 = 0;
        double TheY0 = 0;
        int[] TahkikXY = new int[3];
        int xmove1 = 0;
        int ymove1 = 0;
        double Zoom2d = 50;
        int startX2d = 150;
        int startY2d = 250;
        int lastzoomX2d = 0;
        int lastzoomY2d = 0;
        int timetodo = 0;
        int TempX;
        int TempY;
        int xmove = 0;
        int ymove = 0;
        double TempX12Real;
        double TempY12Real;
        double lastzoomX2dR = 0;
        double lastzoomY2dR = 0;
        double[] TempXReal = new double[3];
        double[] TempYReal = new double[3];
        int GridNumberSX = 0;
        int GridNumberSY = 0;
        double[] TX1Rx = new double[500];
        double[] TY1Rx = new double[500];
        double[] TX2Rx = new double[500];
        double[] TY2Rx = new double[500];
        double[] TX1Ry = new double[500];
        double[] TY1Ry = new double[500];
        double[] TX2Ry = new double[500];
        double[] TY2Ry = new double[500];
        double[] GridXR = new double[10000];
        double[] GridYR = new double[10000];
        int[] GridX2D = new int[10000];
        int[] GridY2D = new int[10000];
        int MouseButtonsLeft = 0;
        #endregion
        #region//الفورم
        private void BeamDesignerForm_Load(object sender, EventArgs e)
        {
            LastSection = 1;
            BmpWidth = this.Width - 70;
            BmpHeight = this.Height - 380;
            pictureBox1.Width = BmpWidth;
            pictureBox1.Height = BmpHeight;
            pictureBox2.Width = pictureBox1.Width;
            pictureBox2.Height = pictureBox1.Height;
            pictureBox1.Controls.Add(pictureBox2);
            pictureBox2.Location = new Point(0, 0);
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Controls.Add(textBox1);

            BmpWidth2d = this.Width - 70;
            BmpHeight2d = this.Height - 200;
            pictureBox3.Width = BmpWidth2d;
            pictureBox3.Height = BmpHeight2d;
            pictureBox4.Width = pictureBox3.Width;
            pictureBox4.Height = pictureBox3.Height;
            pictureBox3.Controls.Add(pictureBox4);
            pictureBox4.Location = new Point(0, 0);
            pictureBox4.BackColor = Color.Transparent;

            //DrawDiagram();
            /////////////////////////////
        }
        private void BeamDesignerForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                int n = 0;
                int ReinforcementBarNumber1 = ReinforcementBarNumber;
            startloop: { };
                for (int i = 1; i < ReinforcementBarNumber + 1; i++)
                {
                    if (ReinforcementBar[i].Selected == 1)
                    {
                        ReinforcementBarNumber = ReinforcementBarNumber - 1;
                        for (int j = i; j < ReinforcementBarNumber + 1; j++)
                        {
                            ReinforcementBar[j] = ReinforcementBar[j + 1];
                        }
                        break;
                    }
                }
                n = n + 1;
                if (n <= ReinforcementBarNumber1) goto startloop;
                AutoReinforcement1();

                for (int i = 1; i < Spans + 1; i++)
                {
                    for (int j = 1; j < 3 + 1; j++)
                    {
                        if (SpanShearDisSelected[i, j] == 1)
                        {
                            if (j == 1) ShearBarsSuRGR[i] = 0;
                            if (j == 2) ShearBarsSpGR[i] = 0;
                            if (j == 3) ShearBarsSuLGR[i + 1] = 0;
                            SpanShearDisSelected[i, j] = 0;
                        }
                    }
                }
                Render2d();
                MakeTempFiles();
            }
            if (e.Control && e.KeyCode == Keys.Z)
            {
                if (TempSelectedFile - 1 > 0)
                {
                    TempSelectedFile = TempSelectedFile - 1;
                    OpenTemp();
                    Render2d();
                }
            }
            if (e.Control && e.KeyCode == Keys.A)
            {
                for (int i = 1; i < ReinforcementBarNumber + 1; i++)
                {
                    ReinforcementBar[i].Selected = 1;
                }
                for (int i = 1; i < Spans + 1; i++)
                {
                    for (int j = 1; j < 3 + 1; j++)
                    {
                        SpanShearDisSelected[i, j] = 1;
                    }
                }
                Render2d();
            }
            if (e.KeyCode == Keys.Escape)
            {
                for (int i = 1; i < ReinforcementBarNumber + 1; i++)
                {
                    ReinforcementBar[i].Selected = 0;
                }
                for (int i = 1; i < Spans + 1; i++)
                {
                    for (int j = 1; j < 3 + 1; j++)
                    {
                        SpanShearDisSelected[i, j] = 0;
                    }
                }
                Render2d();
            }
        }
        #endregion
        #region//الجزء الأول
        public void DrawDiagram()
        {
            #region//تجهيز الصورة
            Bitmap finalBmp1 = new Bitmap(BmpWidth, BmpHeight);
            if (pictureBox1.Image != null) pictureBox1.Image = null;
            pictureBox1.Image = finalBmp1;
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            Pen pen = new Pen(Color.Black, 2f);
            Pen penRec = new Pen(Color.LightGray, 1f);
            Font drawFont = new Font("Tahoma", 10, FontStyle.Regular, GraphicsUnit.Pixel);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            SolidBrush drawBrush1 = new SolidBrush(Color.DarkViolet);
            SolidBrush drawBrush2 = new SolidBrush(Color.DeepSkyBlue);
            SolidBrush drawBrush3 = new SolidBrush(Color.DeepSkyBlue);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            #endregion
            int tx0 = 10;
            int ty0 = pictureBox1.Height / 3;
            intlengthALL = pictureBox1.Width - 2 * tx0;
            string ghg = "";
            int aa1 = 0;
            BeamLengthALL = 0;
            for (int i = 1; i < Spans + 1; i++)
            {
                BeamLengthALL = BeamLengthALL + BeamLength[i];
            }
            #region//رسم أول خط
            int tx1 = 10;
            int ty1 = ty0;
            int tx2 = pictureBox1.Width - tx1;
            int ty2 = ty1;
            int BeamLengthALLInt = pictureBox1.Width - 2 * tx1;
            int intpower = ty1 - 5;
            g.DrawLine(pen, tx1, ty1, tx2, ty2);
            #endregion
            #region
            double LengthAdd = 0;
            int LengthAddInt = 0;
            double Nesba = BeamLengthALLInt / BeamLengthALL;
            for (int i = 1; i < Spans + 2; i++)
            {
                pen = new Pen(Color.Black, 2f);
                tx1 = tx0 + LengthAddInt;
                ty1 = ty0;
                tx2 = tx1;
                ty2 = ty1 + 20;
                g.DrawLine(pen, tx1, ty1, tx2, ty2);
                pen = new Pen(Color.Black, 1f);
                tx1 = tx1 + 5;
                ty1 = ty0 + 100;
                tx2 = tx1 - 10;
                ty2 = ty1 + 10;
                g.DrawLine(pen, tx1, ty1, tx2, ty2);
                tx1 = tx0 + LengthAddInt;
                ty1 = ty0+105+5;
                tx2 = tx1;
                ty2 = ty0 +105-5;
                g.DrawLine(pen, tx1, ty1, tx2, ty2);
                ty1 = ty0;
                int wid = 20;
                int hei = 12;
                int adX = wid / 2;
                int adY = 50;
                if (i == 1) adX = 0;
                if (i == Spans + 1) adX = wid;
                //-----------------------------------------------------------------------
                g.DrawRectangle(penRec, tx1 - adX, ty1 - adY, wid, hei);//فوق المسند
                recMomentUpSuX[i, 1] = tx1 - adX;
                recMomentUpSuX[i, 2] = recMomentUpSuX[i, 1] + wid;
                recMomentUpSuX[i, 3] = recMomentUpSuX[i, 2];
                recMomentUpSuX[i, 4] = recMomentUpSuX[i, 1];
                recMomentUpSuY[i, 1] = ty1 - adY;
                recMomentUpSuY[i, 2] = recMomentUpSuY[i, 1];
                recMomentUpSuY[i, 3] = recMomentUpSuY[i, 1] + hei;
                recMomentUpSuY[i, 4] = recMomentUpSuY[i, 3];
                ghg = MomentUpSu[i].ToString();
                aa1 = Convert.ToInt32(g.MeasureString(ghg, Font).Width / 2);
                if (i == 1) aa1 = 0;
                if (i == Spans + 1) aa1 = aa1* 2;
                g.DrawString(ghg, drawFont, drawBrush, tx1 - aa1, ty1 - adY);
                //
                ghg = "";
                for (int j = 1; j < BarsSuUpGR[i] + 1; j++)
                {
                    if (j != BarsSuUpGR[i])
                    {
                        ghg = ghg + BarsSuUpNo[i, j] + " D " + BarsSuUpD[i, j] + " + ";
                    }
                    else
                    {
                        ghg = ghg + BarsSuUpNo[i, j] + " D " + BarsSuUpD[i, j];
                    }
                }
                aa1 = Convert.ToInt32(g.MeasureString(ghg, Font).Width / 2);
                if (i == 1) aa1 = 0;
                if (i == Spans + 1) aa1 = aa1 * 2;
                g.DrawString(ghg, drawFont, drawBrush1, tx1 - aa1, ty1 - adY + 12);
                //------------------------------------------------------------------------
                g.DrawRectangle(penRec, tx1 - adX, ty1 + adY, wid, hei);//تحت المسند
                recMomentDnSuX[i, 1] = tx1 - adX;
                recMomentDnSuX[i, 2] = recMomentDnSuX[i, 1] + wid;
                recMomentDnSuX[i, 3] = recMomentDnSuX[i, 2];
                recMomentDnSuX[i, 4] = recMomentDnSuX[i, 1];
                recMomentDnSuY[i, 1] = ty1 + adY;
                recMomentDnSuY[i, 2] = recMomentDnSuY[i, 1];
                recMomentDnSuY[i, 3] = recMomentDnSuY[i, 1] + hei;
                recMomentDnSuY[i, 4] = recMomentDnSuY[i, 3];
                ghg = MomentDnSu[i].ToString();
                aa1 = Convert.ToInt32(g.MeasureString(ghg, Font).Width / 2);
                if (i == 1) aa1 = 0;
                if (i == Spans + 1) aa1 = aa1 * 2;
                g.DrawString(ghg, drawFont, drawBrush, tx1 - aa1, ty1 + adY);
                //
                ghg = "";
                for (int j = 1; j < BarsSuDnGR[i] + 1; j++)
                {
                    if (j != BarsSuDnGR[i])
                    {
                        ghg = ghg + BarsSuDnNo[i, j] + " D " + BarsSuDnD[i, j] + " + ";
                    }
                    else
                    {
                        ghg = ghg + BarsSuDnNo[i, j] + " D " + BarsSuDnD[i, j];
                    }
                }
                aa1 = Convert.ToInt32(g.MeasureString(ghg, Font).Width / 2);
                if (i == 1) aa1 = 0;
                if (i == Spans + 1) aa1 = aa1 * 2;
                g.DrawString(ghg, drawFont, drawBrush1, tx1 - aa1, ty1 + adY + 12);
//------------------------------------------------------------------------------------------------------- 
                if (i == 1 || i == Spans + 1)
                {
                    g.DrawRectangle(penRec, tx1 - adX, ty1 - adY - 60, wid, hei);//قص يسار يمين مسند
                    if (i == 1)
                    {
                        recShearSuRX[i, 1] = tx1 - adX;
                        recShearSuRX[i, 2] = recShearSuRX[i, 1] + wid;
                        recShearSuRX[i, 3] = recShearSuRX[i, 2];
                        recShearSuRX[i, 4] = recShearSuRX[i, 1];
                        recShearSuRY[i, 1] = ty1 - adY - 60;
                        recShearSuRY[i, 2] = recShearSuRY[i, 1];
                        recShearSuRY[i, 3] = recShearSuRY[i, 1] + hei;
                        recShearSuRY[i, 4] = recShearSuRY[i, 3];
                        ghg = ShearSuR[i].ToString();
                        aa1 = Convert.ToInt32(g.MeasureString(ghg, Font).Width / 2);
                        g.DrawString(ghg, drawFont, drawBrush, tx1 - adX + wid / 2 - aa1, ty1 - adY - 60);
                        ghg = "";
                        for (int j = 1; j < ShearBarsSuRGR[i] + 1; j++)
                        {
                            if (j != ShearBarsSuRGR[i])
                            {
                                ghg = ghg + ShearBarsSuRNo[i, j] + " C " + ShearBarsSuRD[i, j] + " / " + ShearBarsSuRDis[i, j] + " + ";
                            }
                            else
                            {
                                ghg = ghg + ShearBarsSuRNo[i, j] + " C " + ShearBarsSuRD[i, j] + " / " + ShearBarsSuRDis[i, j];
                            }
                        }
                        aa1 = Convert.ToInt32(g.MeasureString(ghg, Font).Width / 2);
                        if (i == 1) aa1 = 0;
                        if (i == Spans + 1) aa1 = aa1 * 2;
                        g.DrawString(ghg, drawFont, drawBrush2, tx1 - adX + wid / 2 - aa1, ty1 - adY -45);
                    }
                    if (i == Spans + 1)
                    {
                        recShearSuLX[i, 1] = tx1 - adX;
                        recShearSuLX[i, 2] = recShearSuLX[i, 1] + wid;
                        recShearSuLX[i, 3] = recShearSuLX[i, 2];
                        recShearSuLX[i, 4] = recShearSuLX[i, 1];
                        recShearSuLY[i, 1] = ty1 - adY - 60;
                        recShearSuLY[i, 2] = recShearSuLY[i, 1];
                        recShearSuLY[i, 3] = recShearSuLY[i, 1] + hei;
                        recShearSuLY[i, 4] = recShearSuLY[i, 3];
                        ghg = ShearSuL[i].ToString();
                        aa1 = Convert.ToInt32(g.MeasureString(ghg, Font).Width / 2);
                        g.DrawString(ghg, drawFont, drawBrush, tx1 - adX + wid / 2 - aa1, ty1 - adY - 60);
                        ghg = "";
                        for (int j = 1; j < ShearBarsSuLGR[i] + 1; j++)
                        {
                            if (j != ShearBarsSuLGR[i])
                            {
                                ghg = ghg + ShearBarsSuLNo[i, j] + " C " + ShearBarsSuLD[i, j] + " / " + ShearBarsSuLDis[i, j] + " + ";
                            }
                            else
                            {
                                ghg = ghg + ShearBarsSuLNo[i, j] + " C " + ShearBarsSuLD[i, j] + " / " + ShearBarsSuLDis[i, j];
                            }
                        }
                        aa1 = Convert.ToInt32(g.MeasureString(ghg, Font).Width / 2);
                        if (i == 1) aa1 = 0;
                        if (i == Spans + 1) aa1 = aa1 * 2;
                        g.DrawString(ghg, drawFont, drawBrush2, tx1 - adX + wid / 2 - aa1, ty1 - adY - 45);
                    }
                }
                if (i != 1 & i != Spans + 1)
                {
                    g.DrawRectangle(penRec, tx1 - wid - 2, ty1 - adY - 60, wid, hei);//قص يسار مسند
                    recShearSuLX[i, 1] = tx1 - wid - 2;
                    recShearSuLX[i, 2] = recShearSuLX[i, 1] + wid;
                    recShearSuLX[i, 3] = recShearSuLX[i, 2];
                    recShearSuLX[i, 4] = recShearSuLX[i, 1];
                    recShearSuLY[i, 1] = ty1 - adY - 60;
                    recShearSuLY[i, 2] = recShearSuLY[i, 1];
                    recShearSuLY[i, 3] = recShearSuLY[i, 1] + hei;
                    recShearSuLY[i, 4] = recShearSuLY[i, 3];
                    ghg = ShearSuL[i].ToString();
                    aa1 = Convert.ToInt32(g.MeasureString(ghg, Font).Width / 2);
                    g.DrawString(ghg, drawFont, drawBrush, tx1 - wid - 2 + wid / 2 - aa1, ty1 - adY - 60);
                    ghg = "";
                    for (int j = 1; j < ShearBarsSuLGR[i] + 1; j++)
                    {
                        if (j != ShearBarsSuLGR[i])
                        {
                            ghg = ghg + ShearBarsSuLNo[i, j] + " C " + ShearBarsSuLD[i, j] + " / " + ShearBarsSuLDis[i, j] + " + ";
                        }
                        else
                        {
                            ghg = ghg + ShearBarsSuLNo[i, j] + " C " + ShearBarsSuLD[i, j] + " / " + ShearBarsSuLDis[i, j];
                        }
                    }
                    aa1 = Convert.ToInt32(g.MeasureString(ghg, Font).Width);
                    g.DrawString(ghg, drawFont, drawBrush2, tx1 - aa1 - 2, ty1 - adY - 45);
                    g.DrawRectangle(penRec, tx1 + 2, ty1 - adY - 60, wid, hei);//قص يمين مسند
                    recShearSuRX[i, 1] = tx1 + 2;
                    recShearSuRX[i, 2] = recShearSuRX[i, 1] + wid;
                    recShearSuRX[i, 3] = recShearSuRX[i, 2];
                    recShearSuRX[i, 4] = recShearSuRX[i, 1];
                    recShearSuRY[i, 1] = ty1 - adY - 60;
                    recShearSuRY[i, 2] = recShearSuRY[i, 1];
                    recShearSuRY[i, 3] = recShearSuRY[i, 1] + hei;
                    recShearSuRY[i, 4] = recShearSuRY[i, 3];
                    ghg = ShearSuR[i].ToString();
                    aa1 = Convert.ToInt32(g.MeasureString(ghg, Font).Width / 2);
                    g.DrawString(ghg, drawFont, drawBrush, tx1 + 2 + wid / 2 - aa1, ty1 - adY - 60);
                    ghg = "";
                    for (int j = 1; j < ShearBarsSuRGR[i] + 1; j++)
                    {
                        if (j != ShearBarsSuRGR[i])
                        {
                            ghg = ghg + ShearBarsSuRNo[i, j] + " C " + ShearBarsSuRD[i, j] + " / " + ShearBarsSuRDis[i, j] + " + ";
                        }
                        else
                        {
                            ghg = ghg + ShearBarsSuRNo[i, j] + " C " + ShearBarsSuRD[i, j] + " / " + ShearBarsSuRDis[i, j];
                        }
                    }
                    aa1 = Convert.ToInt32(g.MeasureString(ghg, Font).Width / 2);
                    if (i == 1) aa1 = 0;
                    if (i == Spans + 1) aa1 = aa1 * 2;
                    g.DrawString(ghg, drawFont, drawBrush2, tx1 + 2, ty1 - adY - 45);
                }
                ghg = "-" + i + "-";
                aa1 = Convert.ToInt32(g.MeasureString(ghg, Font).Width / 2);
                adX = aa1;
                if (i == 1) adX = 0;
                if (i == Spans + 1) adX = wid;
                g.DrawString(ghg, drawFont, drawBrush, tx1 - adX, ty1 + 30);

                if (i < Spans + 1)
                {
                    tx1 = tx0 + LengthAddInt;
                    ty1 = ty0 + 105;
                    LengthAdd = LengthAdd + BeamLength[i];
                    LengthAddInt = Convert.ToInt32(LengthAdd * Nesba);
                    tx2 = tx0 + LengthAddInt;
                    ty2 = ty1;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    ghg = BeamLength[i].ToString() + " m";
                    aa1 = Convert.ToInt32(g.MeasureString(ghg, Font).Width / 2);
                    int tx3 = (tx1 + tx2) / 2 - aa1;
                    int ty3 = ty1 - 15;
                    g.DrawString(ghg, drawFont, drawBrush, tx3, ty3);
                    ghg = "Span" + i;
                    aa1 = Convert.ToInt32(g.MeasureString(ghg, Font).Width / 2);
                    tx3 = (tx1 + tx2) / 2 - aa1;
                    ty3 = ty1 + 5;
                    g.DrawString(ghg, drawFont, drawBrush, tx3, ty3);
                    ghg = Section.LABEL[SpanSection[i]];
                    aa1 = Convert.ToInt32(g.MeasureString(ghg, Font).Width / 2);
                    tx3 = (tx1 + tx2) / 2 - aa1;
                    ty3 = ty1 + 20;
                    g.DrawString(ghg, drawFont, drawBrush, tx3, ty3);

                    adX = wid / 2;
                    adY = 50;
                    tx3 = (tx1 + tx2) / 2;
                    ty3 = ty0;
                    //-----------------------------------------------------------------------------
                    g.DrawRectangle(penRec, tx3 - adX, ty3 - adY, wid, hei);//فوق الفتحة
                    recMomentUpSpX[i, 1] = tx3 - adX;
                    recMomentUpSpX[i, 2] = recMomentUpSpX[i, 1] + wid;
                    recMomentUpSpX[i, 3] = recMomentUpSpX[i, 2];
                    recMomentUpSpX[i, 4] = recMomentUpSpX[i, 1];
                    recMomentUpSpY[i, 1] = ty3 - adY;
                    recMomentUpSpY[i, 2] = recMomentUpSpY[i, 1];
                    recMomentUpSpY[i, 3] = recMomentUpSpY[i, 1] + hei;
                    recMomentUpSpY[i, 4] = recMomentUpSpY[i, 3];
                    ghg = MomentUpSp[i].ToString();
                    aa1 = Convert.ToInt32(g.MeasureString(ghg, Font).Width / 2);
                    g.DrawString(ghg, drawFont, drawBrush, tx3 - aa1, ty3 - adY);
                    //
                    ghg = "";
                    for (int j = 1; j < BarsSpUpGR[i] + 1; j++)
                    {
                        if (j != BarsSpUpGR[i])
                        {
                            ghg = ghg + BarsSpUpNo[i, j] + " D " + BarsSpUpD[i, j] + " + ";
                        }
                        else
                        {
                            ghg = ghg + BarsSpUpNo[i, j] + " D " + BarsSpUpD[i, j];
                        }
                    }
                    aa1 = Convert.ToInt32(g.MeasureString(ghg, Font).Width / 2);
                    g.DrawString(ghg, drawFont, drawBrush1, tx3 - aa1, ty3 - adY + 12);
                    //------------------------------------------------------------------------------
                    g.DrawRectangle(penRec, tx3 - adX, ty3 + adY, wid, hei);//تحت الفتحة
                    recMomentDnSpX[i, 1] = tx3 - adX;
                    recMomentDnSpX[i, 2] = recMomentDnSpX[i, 1] + wid;
                    recMomentDnSpX[i, 3] = recMomentDnSpX[i, 2];
                    recMomentDnSpX[i, 4] = recMomentDnSpX[i, 1];
                    recMomentDnSpY[i, 1] = ty3 + adY;
                    recMomentDnSpY[i, 2] = recMomentDnSpY[i, 1];
                    recMomentDnSpY[i, 3] = recMomentDnSpY[i, 1] + hei;
                    recMomentDnSpY[i, 4] = recMomentDnSpY[i, 3];
                    ghg = MomentDnSp[i].ToString();
                    aa1 = Convert.ToInt32(g.MeasureString(ghg, Font).Width / 2);
                    g.DrawString(ghg, drawFont, drawBrush, tx3 - aa1, ty3 + adY);
                    //
                    ghg = "";
                    for (int j = 1; j < BarsSpDnGR[i] + 1; j++)
                    {
                        if (j != BarsSpDnGR[i])
                        {
                            ghg = ghg + BarsSpDnNo[i, j] + " D " + BarsSpDnD[i, j] + " + ";
                        }
                        else
                        {
                            ghg = ghg + BarsSpDnNo[i, j] + " D " + BarsSpDnD[i, j];
                        }
                    }
                    aa1 = Convert.ToInt32(g.MeasureString(ghg, Font).Width / 2);
                    g.DrawString(ghg, drawFont, drawBrush1, tx3 - aa1, ty3 + adY + 12);
                    //------------------------------------------------------------------------------
                    g.DrawRectangle(penRec, tx3 - adX, ty3 - adY - 60, wid, hei);//قص وسط الفتجة
                    recShearSpX[i, 1] = tx3 - adX;
                    recShearSpX[i, 2] = recShearSpX[i, 1] + wid;
                    recShearSpX[i, 3] = recShearSpX[i, 2];
                    recShearSpX[i, 4] = recShearSpX[i, 1];
                    recShearSpY[i, 1] = ty3 - adY - 60;
                    recShearSpY[i, 2] = recShearSpY[i, 1];
                    recShearSpY[i, 3] = recShearSpY[i, 1] + hei;
                    recShearSpY[i, 4] = recShearSpY[i, 3];
                    ghg = ShearSp[i].ToString();
                    aa1 = Convert.ToInt32(g.MeasureString(ghg, Font).Width / 2);
                    g.DrawString(ghg, drawFont, drawBrush, tx3 - aa1, ty3 - adY - 60);
                    ghg = "";
                    for (int j = 1; j < ShearBarsSpGR[i] + 1; j++)
                    {
                        if (j != ShearBarsSpGR[i])
                        {
                            ghg = ghg + ShearBarsSpNo[i, j] + " C " + ShearBarsSpD[i, j] + " / " + ShearBarsSpDis[i, j] + " + ";
                        }
                        else
                        {
                            ghg = ghg + ShearBarsSpNo[i, j] + " C " + ShearBarsSpD[i, j] + " / " + ShearBarsSpDis[i, j];
                        }
                    }
                    aa1 = Convert.ToInt32(g.MeasureString(ghg, Font).Width / 2);
                    g.DrawString(ghg, drawFont, drawBrush2, tx3 - aa1, ty3 - adY - 45);
                }
            }
            #endregion
        }
        private void FindkAnyRec()
        {
            textBox1.Visible = false;
            int checkin = 0;
            int x1 = 0;
            int y1 = 0;
            double thevalue=0;
            for (int i = 1; i < Spans + 1; i++)
            {
                SelectedSpan = i;
                for (int j = 1; j < 4 + 1; j++)
                {
                    ShapePointX[i, j] = recMomentUpSpX[i, j];
                    ShapePointY[i, j] = recMomentUpSpY[i, j];
                }
                CheckifInShape();
                if (result == true)
                {
                    hala = "MomentUpSp";
                    checkin = 1;
                    x1 = recMomentUpSpX[i, 1];
                    y1 = recMomentUpSpY[i, 1];
                    thevalue = MomentUpSp[i];
                    goto endloop;
                }

                for (int j = 1; j < 4 + 1; j++)
                {
                    ShapePointX[i, j] = recMomentDnSpX[i, j];
                    ShapePointY[i, j] = recMomentDnSpY[i, j];
                }
                CheckifInShape();
                if (result == true)
                {
                    hala = "MomentDnSp";
                    checkin = 1;
                    x1 = recMomentDnSpX[i, 1];
                    y1 = recMomentDnSpY[i, 1];
                    thevalue = MomentDnSp[i];
                    goto endloop;
                }

                for (int j = 1; j < 4 + 1; j++)
                {
                    ShapePointX[i, j] = recShearSpX[i, j];
                    ShapePointY[i, j] = recShearSpY[i, j];
                }
                CheckifInShape();
                if (result == true)
                {
                    hala = "ShearSp";
                    x1 = recShearSpX[i, 1];
                    y1 = recShearSpY[i, 1];
                    thevalue = ShearSp[i];
                    checkin = 1;
                    goto endloop;
                }
            }

            for (int i = 1; i < Spans + 2; i++)
            {
                SelectedSpan = i;
                for (int j = 1; j < 4 + 1; j++)
                {
                    ShapePointX[i, j] = recMomentUpSuX[i, j];
                    ShapePointY[i, j] = recMomentUpSuY[i, j];
                }
                CheckifInShape();
                if (result == true)
                {
                    hala = "MomentUpSu";
                    x1 = recMomentUpSuX[i, 1];
                    y1 = recMomentUpSuY[i, 1];
                    thevalue = MomentUpSu[i];
                    checkin = 1;
                    goto endloop;
                }

                for (int j = 1; j < 4 + 1; j++)
                {
                    ShapePointX[i, j] = recMomentDnSuX[i, j];
                    ShapePointY[i, j] = recMomentDnSuY[i, j];
                }
                CheckifInShape();
                if (result == true)
                {
                    hala = "MomentDnSu";
                    x1 = recMomentDnSuX[i, 1];
                    y1 = recMomentDnSuY[i, 1];
                    thevalue = MomentDnSu[i];
                    checkin = 1;
                    goto endloop;
                }

                for (int j = 1; j < 4 + 1; j++)
                {
                    ShapePointX[i, j] = recShearSuLX[i, j];
                    ShapePointY[i, j] = recShearSuLY[i, j];
                }
                CheckifInShape();
                if (result == true)
                {
                    hala = "ShearSuL";
                    x1 = recShearSuLX[i, 1];
                    y1 = recShearSuLY[i, 1];
                    thevalue = ShearSuL[i];
                    checkin = 1;
                    goto endloop;
                }

                for (int j = 1; j < 4 + 1; j++)
                {
                    ShapePointX[i, j] = recShearSuRX[i, j];
                    ShapePointY[i, j] = recShearSuRY[i, j];
                }
                CheckifInShape();
                if (result == true)
                {
                    hala = "ShearSuR";
                    x1 = recShearSuRX[i, 1];
                    y1 = recShearSuRY[i, 1];
                    thevalue = ShearSuR[i];
                    checkin = 1;
                    goto endloop;
                }
            }
        endloop: { }

        if (checkin == 1)
        {
            KindForBars = "";
            AlValue = thevalue;
            if (hala == "MomentUpSu") KindForBars = "SuMomentUp";
            if (hala == "MomentDnSu") KindForBars = "SuMomentDn";
            if (hala == "MomentUpSp") KindForBars = "SpMomentUp";
            if (hala == "MomentDnSp") KindForBars = "SpMomentDn";
            if (hala == "ShearSp") KindForBars = "SpShear";
            if (hala == "ShearSuR") KindForBars = "SuRShear";
            if (hala == "ShearSuL") KindForBars = "SuLShear";
            BeamDesignerBarsForm theform = new BeamDesignerBarsForm();
            theform.ShowDialog();
        }
        }
        private void CheckifInShape()
        {
            int i = SelectedSpan;
            int N = 0;
            N = 4 + 1;
            double[] polygonX = new double[N + 1];
            double[] polygonY = new double[N + 1];
            for (int j = 1; j < N; j++)
            {
                polygonX[j] = ShapePointX[i, j];
                polygonY[j] = ShapePointY[i, j];
            }
            polygonX[N] = polygonX[1];
            polygonY[N] = polygonY[1];
            result = false;
            int nvert = N;
            int k, l;
            int round = 0;
            for (k = 1, l = nvert - 1; k < nvert; l = k++)
            {
                double awal = Math.Round((polygonX[l] - polygonX[k]) * (Ytest - polygonY[k]) / (polygonY[l] - polygonY[k]) + polygonX[k], round);
                if (((polygonY[k] > Ytest) != (polygonY[l] > Ytest)) && (Xtest < awal)) result = !result;
            }
        }
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            Xtest = e.X;
            Ytest = e.Y;
            FindkAnyRec();
        }
        public void FillInTable()
        {
            SpansData.RowCount = Spans + 1;
            for (int i = 1; i < Spans + 1; i++)
            {
                SpansData.Rows[i - 1].Cells[0].Value = i;
                SpansData.Rows[i - 1].Cells[1].Value = BeamLength[i];
                SpansData.Rows[i - 1].Cells[2].Value = Section.LABEL[SpanSection[i]];
                SpanHeight[i] = Section.D[SpanSection[i]];
                
                SpansData.Rows[i - 1].Cells[3].Value = MomentUpSp[i];
                string gg = "";
                for (int j = 1; j < BarsSpUpGR[i] + 1; j++)
                {
                    if (j != BarsSpUpGR[i])
                    {
                        gg = gg + BarsSpUpNo[i, j] + " D " + BarsSpUpD[i, j] + " + ";
                    }
                    else
                    {
                        gg = gg + BarsSpUpNo[i, j] + " D " + BarsSpUpD[i, j];
                    }
                }
                SpansData.Rows[i - 1].Cells[4].Value = gg;
                SpansData.Rows[i - 1].Cells[5].Value = MomentDnSp[i];
                gg = "";
                for (int j = 1; j < BarsSpDnGR[i] + 1; j++)
                {
                    if (j != BarsSpDnGR[i])
                    {
                        gg = gg + BarsSpDnNo[i, j] + " D " + BarsSpDnD[i, j] + " + ";
                    }
                    else
                    {
                        gg = gg + BarsSpDnNo[i, j] + " D " + BarsSpDnD[i, j];
                    }
                }
                SpansData.Rows[i - 1].Cells[6].Value = gg;
                SpansData.Rows[i - 1].Cells[7].Value = ShearSp[i];
                gg = "";
                for (int j = 1; j < ShearBarsSpGR[i] + 1; j++)
                {
                    if (j != ShearBarsSpGR[i])
                    {
                        gg = gg + ShearBarsSpNo[i, j] + " C " + ShearBarsSpD[i, j] + " / " + ShearBarsSpDis[i, j] + " + ";
                    }
                    else
                    {
                        gg = gg + ShearBarsSpNo[i, j] + " C " + ShearBarsSpD[i, j] + " / " + ShearBarsSpDis[i, j];
                    }
                }
                SpansData.Rows[i - 1].Cells[8].Value = gg;
            }
            SupportData.RowCount = Spans + 2;
            for (int i = 1; i < Spans + 2; i++)
            {
                SupportData.Rows[i - 1].Cells[0].Value = i;
                SupportData.Rows[i - 1].Cells[1].Value = SupportWeidth[i];
                SupportData.Rows[i - 1].Cells[2].Value = MomentUpSu[i];
                string gg = "";
                for (int j = 1; j < BarsSuUpGR[i] + 1; j++)
                {
                    if (j != BarsSuUpGR[i])
                    {
                        gg = gg + BarsSuUpNo[i, j] + " D " + BarsSuUpD[i, j] + " + ";
                    }
                    else
                    {
                        gg = gg + BarsSuUpNo[i, j] + " D " + BarsSuUpD[i, j];
                    }
                }
                SupportData.Rows[i - 1].Cells[3].Value = gg;
                SupportData.Rows[i - 1].Cells[4].Value = MomentDnSu[i];
                gg = "";
                for (int j = 1; j < BarsSuDnGR[i] + 1; j++)
                {
                    if (j != BarsSuDnGR[i])
                    {
                        gg = gg + BarsSuDnNo[i, j] + " D " + BarsSuDnD[i, j] + " + ";
                    }
                    else
                    {
                        gg = gg + BarsSuDnNo[i, j] + " D " + BarsSuDnD[i, j];
                    }
                }
                SupportData.Rows[i - 1].Cells[5].Value = gg;
                SupportData.Rows[i - 1].Cells[6].Value = ShearSuL[i];
                gg = "";
                for (int j = 1; j < ShearBarsSuLGR[i] + 1; j++)
                {
                    if (j != ShearBarsSuLGR[i])
                    {
                        gg = gg + ShearBarsSuLNo[i, j] + " C " + ShearBarsSuLD[i, j] + " / " + ShearBarsSuLDis[i, j] + " + ";
                    }
                    else
                    {
                        gg = gg + ShearBarsSuLNo[i, j] + " C " + ShearBarsSuLD[i, j] + " / " + ShearBarsSuLDis[i, j];
                    }
                }
                SupportData.Rows[i - 1].Cells[7].Value = gg;
                SupportData.Rows[i - 1].Cells[8].Value = ShearSuR[i];
                gg = "";
                for (int j = 1; j < ShearBarsSuRGR[i] + 1; j++)
                {
                    if (j != ShearBarsSuRGR[i])
                    {
                        gg = gg + ShearBarsSuRNo[i, j] + " C " + ShearBarsSuRD[i, j] + " + " + " / " + ShearBarsSuRDis[i, j];
                    }
                    else
                    {
                        gg = gg + ShearBarsSuRNo[i, j] + " C " + ShearBarsSuRD[i, j] + " / " + ShearBarsSuRDis[i, j];
                    }
                }
                SupportData.Rows[i - 1].Cells[9].Value = gg;
            }
            FillColours();
        }
        private void FillFromTable()
        {
            int Spans0 = Spans;
            Spans = SpansData.RowCount - 1;
            int i = 0;
            for ( i = 1; i < Spans + 1; i++)
            {
                if (SpanSection[i] == 0) 
                {
                    SpanSection[i] = LastSection;
                    SpansData.Rows[i - 1].Cells[2].Value = Section.LABEL[SpanSection[i]];
                    SpanHeight[i] = Section.D[SpanSection[i]];
                }
                SpansData.Rows[i - 1].Cells[0].Value = i;
                BeamLength[i] = Convert.ToDouble(SpansData.Rows[i - 1].Cells[1].Value);
                MomentUpSp[i] = Convert.ToDouble(SpansData.Rows[i - 1].Cells[3].Value);
                MomentDnSp[i] = Convert.ToDouble(SpansData.Rows[i - 1].Cells[5].Value);
                ShearSp[i] = Convert.ToDouble(SpansData.Rows[i - 1].Cells[7].Value);
            }
            SupportData.RowCount = Spans + 2;
            for ( i = 1; i < Spans + 2; i++)
            {
                SupportData.Rows[i - 1].Cells[0].Value = i;
                SupportWeidth[i] = Convert.ToDouble(SupportData.Rows[i - 1].Cells[1].Value);
                MomentUpSu[i] = Convert.ToDouble(SupportData.Rows[i - 1].Cells[2].Value);
                MomentDnSu[i] = Convert.ToDouble(SupportData.Rows[i - 1].Cells[4].Value);
                ShearSuL[i] = Convert.ToDouble(SupportData.Rows[i - 1].Cells[6].Value);
                ShearSuR[i] = Convert.ToDouble(SupportData.Rows[i - 1].Cells[8].Value);
            }
            FillColours();
        }
        private void FillColours()
        {
            for (int i = 1; i < Spans + 1; i++)
            {
                SpansData.Rows[i - 1].Cells[3].Style.BackColor = Color.WhiteSmoke;
                SpansData.Rows[i - 1].Cells[4].Style.BackColor = Color.WhiteSmoke;
                SpansData.Rows[i - 1].Cells[5].Style.BackColor = Color.LightGray;
                SpansData.Rows[i - 1].Cells[6].Style.BackColor = Color.LightGray;
                SpansData.Rows[i - 1].Cells[7].Style.BackColor = Color.WhiteSmoke;
                SpansData.Rows[i - 1].Cells[8].Style.BackColor = Color.WhiteSmoke;

            }
            for (int i = 1; i < Spans + 2; i++)
            {
                SupportData.Rows[i - 1].Cells[2].Style.BackColor = Color.WhiteSmoke;
                SupportData.Rows[i - 1].Cells[3].Style.BackColor = Color.WhiteSmoke;
                SupportData.Rows[i - 1].Cells[4].Style.BackColor = Color.LightGray;
                SupportData.Rows[i - 1].Cells[5].Style.BackColor = Color.LightGray;
                SupportData.Rows[i - 1].Cells[6].Style.BackColor = Color.WhiteSmoke;
                SupportData.Rows[i - 1].Cells[7].Style.BackColor = Color.WhiteSmoke;
                SupportData.Rows[i - 1].Cells[8].Style.BackColor = Color.LightGray;
                SupportData.Rows[i - 1].Cells[9].Style.BackColor = Color.LightGray;
            }
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int i = SelectedSpan;
                double thevalue = Convert.ToDouble(textBox1.Text);  
                if (hala == "MomentUpSp")
                {
                    MomentUpSp[i] = thevalue;
                    goto endloop;
                }
                if (hala == "MomentDnSp")
                {
                    MomentDnSp[i] = thevalue;
                    goto endloop;
                }
                if (hala == "ShearSp")
                {
                    ShearSp[i] = thevalue;
                    goto endloop;
                }
                if (hala == "MomentUpSu")
                {
                    MomentUpSu[i] = thevalue;
                    goto endloop;
                }
                if (hala == "MomentDnSu")
                {
                    MomentDnSu[i] = thevalue;
                    goto endloop;
                }
                if (hala == "ShearSuL")
                {
                    ShearSuL[i] = thevalue;
                    goto endloop;
                }
                if (hala == "ShearSuR")
                {
                    ShearSuR[i] = thevalue;
                    goto endloop;
                }
            endloop: { };
            textBox1.Visible = false; 
            DrawDiagram();
            FillInTable();
            }
        }
        private void SpansData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            FillFromTable();
            DrawDiagram();
        }
        private void SpansData_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int j = SpansData.CurrentCell.ColumnIndex;
            int i = SpansData.CurrentCell.RowIndex;
            if (i < Spans)
            {
                SelectedSpan = i + 1;
                KindForBars = "";
                if (j == 4 || j == 6 || j == 8)
                {
                    AlValue = Convert.ToDouble(SpansData.Rows[i].Cells[j - 1].Value);
                    if (j == 4) KindForBars = "SpMomentUp";
                    if (j == 6) KindForBars = "SpMomentDn";
                    if (j == 8) KindForBars = "SpShear";
                    BeamDesignerBarsForm theform = new BeamDesignerBarsForm();
                    theform.ShowDialog();
                }
                if (j == 2)
                {
                    Myglobals.FrameDesignerIsOpen = 1;
                    Myglobals.FrameDesignerAnySpan = SelectedSpan;
                    FramePropertiesFrm framePropertiesFrm = new FramePropertiesFrm();
                    framePropertiesFrm.ShowDialog();
                }
                groupBox1.Text = "Span" + SelectedSpan + " Section";
                DrawSection();
            }
        }
        private void SupportData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (Spans > 0)
            {
                FillFromTable();
                DrawDiagram();
            }
        }
        private void SupportData_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int j = SupportData.CurrentCell.ColumnIndex;
            int i = SupportData.CurrentCell.RowIndex;
            if (i < Spans+1)
            {
                SelectedSpan = i + 1;
                KindForBars = "";
                if (j == 3 || j == 5 || j == 7 || j == 9)
                {
                    AlValue = Convert.ToDouble(SupportData.Rows[i].Cells[j - 1].Value);
                    if (j == 3) KindForBars = "SuMomentUp";
                    if (j == 5) KindForBars = "SuMomentDn";
                    if (j == 7) KindForBars = "SuLShear";
                    if (j == 9) KindForBars = "SuRShear";
                    BeamDesignerBarsForm theform = new BeamDesignerBarsForm();
                    theform.ShowDialog();
                }
            }
        }
        #endregion
        #region//الجزء الثاني
        private void pictureBox4_MouseWheel(object sender, MouseEventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                if (e.Delta > 0)
                {
                    Zoom2d = Math.Round(Zoom2d + 6, 2);
                    startX2d = lastzoomX2d - (int)(lastzoomX2dR * Zoom2d);
                    startY2d = lastzoomY2d + (int)(lastzoomY2dR * Zoom2d);
                    TempX = lastzoomX2d;
                    TempY = lastzoomY2d;
                    Render2d();
                }
                else
                {
                    if (Math.Round(Zoom2d - 6, 2) > 1)
                    {
                        Zoom2d = Math.Round(Zoom2d - 6, 2);
                        startX2d = lastzoomX2d - (int)(lastzoomX2dR * Zoom2d);
                        startY2d = lastzoomY2d + (int)(lastzoomY2dR * Zoom2d);
                        TempX = lastzoomX2d;
                        TempY = lastzoomY2d;
                        Render2d();
                    }
                }
            }
        }
        public void Render2d()
        {
            PlaneAria2d();
            DrowBars();
           // AutoReinforcement();
            DrawReinforcement();
            DrowRedline();
        }
        #region//الرسم
        private void PlaneAria2d()
        {
            Bitmap finalBmp = new Bitmap(BmpWidth2d, BmpHeight2d);
            if (pictureBox3.Image != null)
            {
                pictureBox3.Image.Dispose();
                pictureBox3.Image = null;
            }
            pictureBox3.Image = finalBmp;
        }
        private void DrowBars()
        {
            Graphics g = Graphics.FromImage(pictureBox3.Image);
            Font drawFont = new Font("Tahoma", 10, FontStyle.Bold, GraphicsUnit.Pixel);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            int TheWidth = pictureBox4.Width;
            int TheHeight = pictureBox4.Height;
            int tx1 = 0;
            int ty1 = 0;
            int tx2 = 0;
            int ty2 = 0;
            int tx3 = 0;
            int ty3 = 0;
            int tx4 = 0;
            int ty4 = 0;
            string ghg = "";
            int aa1 =0;
            Pen pen = new Pen(Color.LightGray, 1f);
            Pen pen1 = new Pen(Color.Gray, 1f);
            #region//رسم الشبكة
            double Dis1R = 1;
            double Dis2R = 10;
            int Dis12D = Convert.ToInt32(Dis1R * Zoom2d);
            int Dis22D = 10 * Dis12D;
            int GridNumber1XL = Convert.ToInt32(startX2d / (Dis1R * Zoom2d)) + 1;
            int GridNumber1XR = Convert.ToInt32((TheWidth - startX2d) / (Dis1R * Zoom2d)) + 1;
            if (GridNumber1XL < 0) GridNumber1XL = 0;
            if (GridNumber1XR < 0) GridNumber1XR = 0;
            int GridNumber1YL = Convert.ToInt32(startY2d / (Dis1R * Zoom2d)) + 1;
            int GridNumber1YR = Convert.ToInt32((TheHeight - startY2d) / (Dis1R * Zoom2d)) + 1;
            if (GridNumber1YL < 0) GridNumber1YL = 0;
            if (GridNumber1YR < 0) GridNumber1YR = 0;

           
            for (int i = 0; i < GridNumber1XL + 1; i++)
            {
                int thedis = startX2d - Convert.ToInt32(i * Dis1R * Zoom2d);
                if (thedis >= 0 & thedis <= TheWidth)
                {
                    tx1 = thedis;
                    ty1 = 0;
                    tx2 = tx1;
                    ty2 = TheHeight;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                }
            }
            for (int i = 0; i < GridNumber1XR + 1; i++)
            {
                int thedis = startX2d + Convert.ToInt32(i * Dis1R * Zoom2d);
                if (thedis >= 0 & thedis <= TheWidth)
                {
                    tx1 = thedis;
                    ty1 = 0;
                    tx2 = tx1;
                    ty2 = TheHeight;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                }
            }
            for (int i = 0; i < GridNumber1YL + 1; i++)
            {
                int thedis = startY2d - Convert.ToInt32(i * Dis1R * Zoom2d);
                if (thedis >= 0 & thedis <= TheHeight)
                {
                    tx1 = 0;
                    ty1 = thedis;
                    tx2 = TheWidth;
                    ty2 = ty1;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                }
            }
            for (int i = 0; i < GridNumber1YR + 1; i++)
            {
                int thedis = startY2d + Convert.ToInt32(i * Dis1R * Zoom2d);
                if (thedis >= 0 & thedis <= TheHeight)
                {
                    tx1 = 0;
                    ty1 = thedis;
                    tx2 = TheWidth;
                    ty2 = ty1;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                }
            }
            //////////////////////////////////////////////////////////////////////
            int GridNumber2XL = Convert.ToInt32(startX2d / (Dis2R * Zoom2d)) + 1;
            int GridNumber2XR = Convert.ToInt32((TheWidth - startX2d) / (Dis2R * Zoom2d)) + 1;
            if (GridNumber2XL < 0) GridNumber2XL = 0;
            if (GridNumber2XR < 0) GridNumber2XR = 0;
            int GridNumber2YL = Convert.ToInt32(startY2d / (Dis2R * Zoom2d)) + 1;
            int GridNumber2YR = Convert.ToInt32((TheHeight - startY2d) / (Dis2R * Zoom2d)) + 1;
            if (GridNumber2YL < 0) GridNumber2YL = 0;
            if (GridNumber2YR < 0) GridNumber2YR = 0;
            int m = 0;
            pen = new Pen(Color.LightGray, 1f);
            int WW = 1000000;
            int HH = 1000000;
            for (int i = 0; i < GridNumber2XL + 1; i++)
            {
                int thedis = startX2d - Convert.ToInt32(i * Dis2R * Zoom2d);
                if (thedis >= 0 & thedis <= TheWidth)
                {
                    m = m + 1;
                    tx1 = thedis;
                    ty1 = 0;
                    tx2 = tx1;
                    ty2 = TheHeight;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    TX1Rx[m] = -i * Dis2R;
                    TY1Rx[m] = HH;
                    TX2Rx[m] = TX1Rx[m];
                    TY2Rx[m] = -TY1Rx[m];
                }
            }
            for (int i = 0; i < GridNumber2XR + 1; i++)
            {
                int thedis = startX2d + Convert.ToInt32(i * Dis2R * Zoom2d);
                if (thedis >= 0 & thedis <= TheWidth)
                {
                    m = m + 1;
                    tx1 = thedis;
                    ty1 = 0;
                    tx2 = tx1;
                    ty2 = TheHeight;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    TX1Rx[m] = i * Dis2R;
                    TY1Rx[m] = HH;
                    TX2Rx[m] = TX1Rx[m];
                    TY2Rx[m] = -TY1Rx[m];
                }
            }
            GridNumberSX = m;
            m = 0;
            for (int i = 0; i < GridNumber2YL + 1; i++)
            {
                int thedis = startY2d - Convert.ToInt32(i * Dis2R * Zoom2d);
                if (thedis >= 0 & thedis <= TheHeight)
                {
                    m = m + 1;
                    tx1 = 0;
                    ty1 = thedis;
                    tx2 = TheWidth;
                    ty2 = ty1;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    TX1Ry[m] = WW;
                    TY1Ry[m] = i * Dis2R;
                    TX2Ry[m] = -TX1Ry[m];
                    TY2Ry[m] = TY1Ry[m];
                }
            }
            for (int i = 0; i < GridNumber2YR + 1; i++)
            {
                int thedis = startY2d + Convert.ToInt32(i * Dis2R * Zoom2d);
                if (thedis >= 0 & thedis <= TheHeight)
                {
                    m = m + 1;
                    tx1 = 0;
                    ty1 = thedis;
                    tx2 = TheWidth;
                    ty2 = ty1;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    TX1Ry[m] = WW;
                    TY1Ry[m] = -i * Dis2R;
                    TX2Ry[m] = -TX1Ry[m];
                    TY2Ry[m] = TY1Ry[m];
                }
            }
            GridNumberSY = m;
            ///////////////////////////////////////////////////////////////////////////////////
            #endregion
            pen = new Pen(Color.Black, 1f);
            double BeamSum = 0;
            MAXExtendS = -1000000;
            for (int i = 1; i < Spans + 1; i++)
            {
                if (SpanHeight[i] > MAXExtendS) MAXExtendS = SpanHeight[i];
            }
            MAXExtendS = MAXExtendS + 0.5;
            BeamSum = -SupportWeidth[1] / 2;
            Minval = BeamSum;
            #region//رسم الشكل الخارجي
            for (int i = 1; i < Spans + 1; i++)
            {
                tx1 = startX2d + Convert.ToInt32(BeamSum * Zoom2d);
                ty1 = startY2d;
                BeamSum = BeamSum + BeamLength[i];
                if (i == 1) BeamSum = BeamSum + SupportWeidth[i] / 2;
                if (i == Spans) BeamSum = BeamSum + SupportWeidth[i+1] / 2;
                tx2 = startX2d + Convert.ToInt32(BeamSum * Zoom2d);
                ty2 = startY2d;
                g.DrawLine(pen, tx1, ty1, tx2, ty2);
                double supportW = SupportWeidth[i] / 2;
                if (i == 1)
                {
                    supportW = SupportWeidth[i];
                    tx3 = tx1;
                    ty3 = ty1;
                    tx4 = tx3;
                    ty4 = startY2d + Convert.ToInt32(MAXExtendS * Zoom2d);
                    g.DrawLine(pen, tx3, ty3, tx4, ty4);
                }
                tx1 = tx1 + Convert.ToInt32(supportW * Zoom2d);
                ty1 = startY2d + Convert.ToInt32(SpanHeight[i] * Zoom2d);
                supportW = SupportWeidth[i + 1] / 2;
                if (i == Spans)
                {
                    supportW = SupportWeidth[i + 1];
                    tx3 = tx2;
                    ty3 = ty2;
                    tx4 = tx3;
                    ty4 = startY2d + Convert.ToInt32(MAXExtendS * Zoom2d);
                    g.DrawLine(pen, tx3, ty3, tx4, ty4);
                }
                tx2 = tx2 - Convert.ToInt32(supportW * Zoom2d);
                ty2 = startY2d + Convert.ToInt32(SpanHeight[i] * Zoom2d);
                g.DrawLine(pen, tx1, ty1, tx2, ty2);
                tx3 = tx1;
                ty3 = ty2;
                tx4 = tx3;
                ty4 = startY2d + Convert.ToInt32(MAXExtendS * Zoom2d);
                g.DrawLine(pen, tx3, ty3, tx4, ty4);
                tx3 = tx2;
                ty3 = ty2;
                tx4 = tx3;
                ty4 = startY2d + Convert.ToInt32(MAXExtendS * Zoom2d);
                g.DrawLine(pen, tx3, ty3, tx4, ty4);
            }
            #endregion
            Maxval = BeamSum;
            #region//رسم الأبعاد
            BeamSum = 0;
            MAXExtendS = MAXExtendS + 1;
            pen = new Pen(Color.Green, 1f);
            for (int i = 1; i < Spans + 1; i++)
            {
                tx1 = startX2d + Convert.ToInt32(BeamSum * Zoom2d);
                ty1 = startY2d + Convert.ToInt32(MAXExtendS * Zoom2d);
                BeamSum = BeamSum + BeamLength[i];
                tx2 = startX2d + Convert.ToInt32(BeamSum * Zoom2d);
                ty2 = ty1;
                g.DrawLine(pen, tx1, ty1, tx2, ty2);
                ghg = BeamLength[i].ToString() + " m";
                aa1 = Convert.ToInt32(g.MeasureString(ghg, Font).Width / 2);
                tx3 = (tx1 + tx2) / 2 - aa1;
                ty3 = ty1 -11;
                g.DrawString(ghg, drawFont, drawBrush, tx3, ty3);
                tx3 = tx1 - 5;
                ty3 = ty1 + 5;
                tx4 = tx1 + 5;
                ty4 = ty1 - 5;
                g.DrawLine(pen, tx3, ty3, tx4, ty4);
                tx3 = tx1;
                ty3 = ty1 + 5;
                tx4 = tx1;
                ty4 = ty1 - 5;
                g.DrawLine(pen, tx3, ty3, tx4, ty4);
                if (i == Spans)
                {
                    tx3 = tx2 - 5;
                    ty3 = ty1 + 5;
                    tx4 = tx2 + 5;
                    ty4 = ty1 - 5;
                    g.DrawLine(pen, tx3, ty3, tx4, ty4);
                    tx3 = tx2;
                    ty3 = ty1 + 5;
                    tx4 = tx2;
                    ty4 = ty1 - 5;
                    g.DrawLine(pen, tx3, ty3, tx4, ty4);
                    tx3 = tx2;//خط المحور
                    ty3 = ty1;
                    tx4 = tx3;
                    ty4 = startY2d + Convert.ToInt32(-0.5 * Zoom2d);
                    g.DrawLine(pen1, tx3, ty3, tx4, ty4);
                    g.DrawEllipse(pen1, tx4 - 15, ty4 - 30, 30, 30);
                }
                tx3 = tx1;//خط المحور
                ty3 = ty1 ;
                tx4 = tx3;
                ty4 = startY2d + Convert.ToInt32(-0.5 * Zoom2d);
                g.DrawLine(pen1, tx3, ty3, tx4, ty4);
                g.DrawEllipse(pen1, tx4 - 15, ty4 - 30, 30, 30);
            }
            MAXExtendS = MAXExtendS + 1;
            tx1 = startX2d + Convert.ToInt32(0 * Zoom2d);
            ty1 = startY2d + Convert.ToInt32(MAXExtendS * Zoom2d);
            tx2 = startX2d + Convert.ToInt32(BeamSum * Zoom2d);
            ty2 = ty1;
            g.DrawLine(pen, tx1, ty1, tx2, ty2);
            ghg = BeamSum.ToString() + " m";
            aa1 = Convert.ToInt32(g.MeasureString(ghg, Font).Width / 2);
            tx3 = (tx1 + tx2) / 2 - aa1;
            ty3 = ty1 - 11;
            g.DrawString(ghg, drawFont, drawBrush, tx3, ty3);

            tx3 = tx1 - 5;
            ty3 = ty1 + 5;
            tx4 = tx1 + 5;
            ty4 = ty1 - 5;
            g.DrawLine(pen, tx3, ty3, tx4, ty4);
            tx3 = tx1;
            ty3 = ty1 + 5;
            tx4 = tx1;
            ty4 = ty1 - 5;
            g.DrawLine(pen, tx3, ty3, tx4, ty4);
            tx3 = tx2 - 5;
            ty3 = ty1 + 5;
            tx4 = tx2 + 5;
            ty4 = ty1 - 5;
            g.DrawLine(pen, tx3, ty3, tx4, ty4);
            tx3 = tx2;
            ty3 = ty1 + 5;
            tx4 = tx2;
            ty4 = ty1 - 5;
            g.DrawLine(pen, tx3, ty3, tx4, ty4);
            #endregion
            #region//رسم الأبعاد
            BeamSum = -SupportWeidth[1] / 2;
            MAXExtendS = MAXExtendS - 0.5;
            pen = new Pen(Color.Green, 1f);
            for (int i = 1; i < Spans + 1; i++)
            {
                tx1 = startX2d + Convert.ToInt32(BeamSum * Zoom2d);
                ty1 = startY2d + Convert.ToInt32(MAXExtendS * Zoom2d);
                BeamSum = BeamSum + SupportWeidth[i];
                tx2 = startX2d + Convert.ToInt32(BeamSum * Zoom2d);
                ty2 = ty1;
                g.DrawLine(pen, tx1, ty1, tx2, ty2);
                ghg = (SupportWeidth[i]).ToString() + " m";
                aa1 = Convert.ToInt32(g.MeasureString(ghg, Font).Width / 2);
                tx3 = (tx1 + tx2) / 2 - aa1;
                ty3 = ty1 - 11;
                g.DrawString(ghg, drawFont, drawBrush, tx3, ty3);
                tx3 = tx1 - 5;
                ty3 = ty1 + 5;
                tx4 = tx1 + 5;
                ty4 = ty1 - 5;
                g.DrawLine(pen, tx3, ty3, tx4, ty4);
                tx3 = tx1;
                ty3 = ty1 + 5;
                tx4 = tx1;
                ty4 = ty1 - 5;
                g.DrawLine(pen, tx3, ty3, tx4, ty4);

                tx1 = startX2d + Convert.ToInt32(BeamSum * Zoom2d);
                ty1 = startY2d + Convert.ToInt32(MAXExtendS * Zoom2d);
                BeamSum = BeamSum + BeamLength[i]- SupportWeidth[i] / 2- SupportWeidth[i+1] / 2;
                tx2 = startX2d + Convert.ToInt32(BeamSum * Zoom2d);
                ty2 = ty1;
                g.DrawLine(pen, tx1, ty1, tx2, ty2);
                ghg = (BeamLength[i] - SupportWeidth[i] / 2 - SupportWeidth[i + 1] / 2).ToString() + " m";
                aa1 = Convert.ToInt32(g.MeasureString(ghg, Font).Width / 2);
                tx3 = (tx1 + tx2) / 2 - aa1;
                ty3 = ty1 - 11;
                g.DrawString(ghg, drawFont, drawBrush, tx3, ty3);
                tx3 = tx1 - 5;
                ty3 = ty1 + 5;
                tx4 = tx1 + 5;
                ty4 = ty1 - 5;
                g.DrawLine(pen, tx3, ty3, tx4, ty4);
                tx3 = tx1;
                ty3 = ty1 + 5;
                tx4 = tx1;
                ty4 = ty1 - 5;
                g.DrawLine(pen, tx3, ty3, tx4, ty4);

                if (i == Spans)
                {
                    tx3 = tx2 - 5;
                    ty3 = ty1 + 5;
                    tx4 = tx2 + 5;
                    ty4 = ty1 - 5;
                    g.DrawLine(pen, tx3, ty3, tx4, ty4);
                    tx3 = tx2;
                    ty3 = ty1 + 5;
                    tx4 = tx2;
                    ty4 = ty1 - 5;
                    g.DrawLine(pen, tx3, ty3, tx4, ty4);
                    tx1 = startX2d + Convert.ToInt32(BeamSum * Zoom2d);
                    ty1 = startY2d + Convert.ToInt32(MAXExtendS * Zoom2d);
                    BeamSum = BeamSum + SupportWeidth[i + 1];
                    tx2 = startX2d + Convert.ToInt32(BeamSum * Zoom2d);
                    ty2 = ty1;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    ghg = (SupportWeidth[i + 1]).ToString() + " m";
                    aa1 = Convert.ToInt32(g.MeasureString(ghg, Font).Width / 2);
                    tx3 = (tx1 + tx2) / 2 - aa1;
                    ty3 = ty1 - 11;
                    g.DrawString(ghg, drawFont, drawBrush, tx3, ty3);
                    tx3 = tx2 - 5;
                    ty3 = ty1 + 5;
                    tx4 = tx2 + 5;
                    ty4 = ty1 - 5;
                    g.DrawLine(pen, tx3, ty3, tx4, ty4);
                    tx3 = tx2;
                    ty3 = ty1 + 5;
                    tx4 = tx2;
                    ty4 = ty1 - 5;
                    g.DrawLine(pen, tx3, ty3, tx4, ty4);
                }
            }
            #endregion
        }
        #endregion
        private void button3_Click(object sender, EventArgs e)
        {
            Spans = 4;
            int i = 1;
            BeamLength[i] = 4.25;
            MomentUpSu[i] = 1010;
            MomentDnSu[i] = 1020;
            MomentUpSp[i] = 1030;
            MomentDnSp[i] = 1040;
            ShearSp[i] = 1050;
            ShearSuL[i] = 0;
            ShearSuR[i] = 1070;
            SupportWeidth[i] = 0.4;
            SpanHeight[i] = 0.6;

            i = 2;
            BeamLength[i] = 5;
            MomentUpSu[i] = 2010;
            MomentDnSu[i] = 2020;
            MomentUpSp[i] = 2030;
            MomentDnSp[i] = 2040;
            ShearSp[i] = 2050;
            ShearSuL[i] = 2060;
            ShearSuR[i] = 2070;
            SupportWeidth[i] = 0.4;
            SpanHeight[i] = 0.9;

            i = 3;
            BeamLength[i] = 3.88;
            MomentUpSu[i] = 3010;
            MomentDnSu[i] = 3020;
            MomentUpSp[i] = 3030;
            MomentDnSp[i] = 3040;
            ShearSp[i] = 3050;
            ShearSuL[i] = 3060;
            ShearSuR[i] = 3070;
            SupportWeidth[i] = 0.4;
            SpanHeight[i] = 0.6;

            i = 4;
            BeamLength[i] = 4.25;
            MomentUpSu[i] = 4010;
            MomentDnSu[i] = 4020;
            MomentUpSp[i] = 4030;
            MomentDnSp[i] = 4040;
            ShearSp[i] = 4050;
            ShearSuL[i] = 4060;
            ShearSuR[i] = 4070;
            SupportWeidth[i] = 0.4;
            SpanHeight[i] = 1.2;

            i = 5;
            MomentUpSu[i] = 5010;
            MomentDnSu[i] = 5020;
            ShearSuL[i] = 5060;
            ShearSuR[i] = 0;
            SupportWeidth[i] = 0.4;
            FillInTable();
            DrawDiagram();
        }
        private void pictureBox4_MouseMove(object sender, MouseEventArgs e)
        {
            lastzoomX2d = e.X;
            lastzoomY2d = e.Y;
            #region // تحريك المسقط
            if (e.Button == MouseButtons.Middle)
            {
                timetodo = timetodo + 1;
                startX2d = startX2d + (Cursor.Position.X - xmove);
                startY2d = startY2d + (Cursor.Position.Y - ymove);
                xmove = Cursor.Position.X;
                ymove = Cursor.Position.Y;
                if (timetodo > 2)
                {
                    Render2d();
                    timetodo = 0;
                }
                goto ENDLOOP;
            }
            #endregion
            #region//التقاط الاوسناب
            TempX = e.X;
            TempY = e.Y;
            TempX12Real = Math.Round((TempX - startX2d) / (Zoom2d), 3);
            TempY12Real = Math.Round((startY2d - TempY) / (Zoom2d), 3);
            lastzoomX2dR = TempX12Real;
            lastzoomY2dR = TempY12Real;
            LBLX.Visible = true;
            LBLY.Visible = true;
            LBLX.Text = "X= " + Convert.ToString(Math.Round(TempX12Real, 3));
            LBLY.Text = "Y= " + Convert.ToString(Math.Round(TempY12Real, 3));
            #endregion
            if (e.Button == MouseButtons.Middle) goto ENDLOOP;
            MouseButtonsLeft = 0;
            if (e.Button == MouseButtons.Left) MouseButtonsLeft = 1;
            timetodo = timetodo + 1;
            if (timetodo > 2)
            {
                    if (e.Button == MouseButtons.Left)
                    {
                        if (TempY < startY2d)
                        {
                            RedLineXR = (TempX - startX2d) / (Zoom2d);
                            if (RedLineXR < Minval) RedLineXR = Minval;
                            if (RedLineXR > Maxval) RedLineXR = Maxval;
                            DrowRedline();
                        }
                        else
                        {
                            pictureBox4Draw();
                        }
                    }
                timetodo = 0;
            }
        ENDLOOP: { }
        }
        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            xmove = Cursor.Position.X;
            ymove = Cursor.Position.Y;
            xmove1 = e.X;
            ymove1 = e.Y;
            int ifselected = 0;
            int ifselected1 = 0;
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                if (TempY < startY2d)
                {
                    RedLineXR = (TempX - startX2d) / (Zoom2d);
                    if (RedLineXR < Minval) RedLineXR = Minval;
                    if (RedLineXR > Maxval) RedLineXR = Maxval;
                    DrowRedline();
                }
                # region//تحديد العناصر الخطية
                for (int i = 1; i < ReinforcementBarNumber + 1; i++)
                {
                    int X1 = ReinforcementBar[i].PointX2d[1];
                    int Y1 = ReinforcementBar[i].PointY2d[1];
                    int X2 = ReinforcementBar[i].PointX2d[2];
                    int Y2 = ReinforcementBar[i].PointY2d[2];
                    int X = e.X;
                    int Y = e.Y;
                    double distance = 100000;
                    int Tah = 0;
                    if ((Y2 == Y1))
                    {
                        if (X >= X1 & X <= X2) Tah = 1;
                        if (X <= X1 & X >= X2) Tah = 1;
                        if (Tah == 1)
                        {
                            distance = Math.Abs(Y - Y1);
                            goto endloop;
                        }
                    }
                    if ((X2 == X1))
                    {
                        if (Y >= Y1 & Y <= Y2) Tah = 1;
                        if (Y <= Y1 & Y >= Y2) Tah = 1;
                        if (Tah == 1)
                        {
                            distance = Math.Abs(X - X1);
                            goto endloop;
                        }
                    }
                    if (X2 != X1)
                    {
                        if (X >= X1 & X <= X2 & Y >= Y1 & Y <= Y2) Tah = 1;
                        if (X >= X2 & X <= X1 & Y >= Y2 & Y <= Y1) Tah = 1;
                        if (X >= X2 & X <= X1 & Y >= Y1 & Y <= Y2) Tah = 1;
                        if (X >= X1 & X <= X2 & Y >= Y2 & Y <= Y1) Tah = 1;
                        if (Tah == 1)
                        {
                            distance = Math.Abs((X2 - X1) * (Y1 - Y) - (X1 - X) * (Y2 - Y1)) / Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2));
                        }
                    }
                endloop: { }
                    if (distance < 0.1 * Zoom2d)
                    {
                        ifselected = 1;
                        if (ReinforcementBar[i].Selected == 1)
                        {
                            ReinforcementBar[i].Selected = 0;
                        }
                        else
                        {
                            ReinforcementBar[i].Selected = 1;
                        }
                        if (e.Button == System.Windows.Forms.MouseButtons.Right)
                        {
                        }
                        goto endloop1;
                    }
                }
                #endregion
                # region//تحديد الأساور خط
                for (int i = 1; i < Spans + 1; i++)
                {
                    for (int j = 1; j < 3 + 1; j++)
                    {
                        int X1 = SpanShearDisX12d[i, j];
                        int Y1 = SpanShearDisY12d[i, j];
                        int X2 = SpanShearDisX22d[i, j];
                        int Y2 = SpanShearDisY22d[i, j];
                        int X = e.X;
                        int Y = e.Y;
                        double distance = 100000;
                        int Tah = 0;
                        if ((Y2 == Y1))
                        {
                            if (X >= X1 & X <= X2) Tah = 1;
                            if (X <= X1 & X >= X2) Tah = 1;
                            if (Tah == 1)
                            {
                                distance = Math.Abs(Y - Y1);
                                goto endloop2;
                            }
                        }
                        if ((X2 == X1))
                        {
                            if (Y >= Y1 & Y <= Y2) Tah = 1;
                            if (Y <= Y1 & Y >= Y2) Tah = 1;
                            if (Tah == 1)
                            {
                                distance = Math.Abs(X - X1);
                                goto endloop2;
                            }
                        }
                        if (X2 != X1)
                        {
                            if (X >= X1 & X <= X2 & Y >= Y1 & Y <= Y2) Tah = 1;
                            if (X >= X2 & X <= X1 & Y >= Y2 & Y <= Y1) Tah = 1;
                            if (X >= X2 & X <= X1 & Y >= Y1 & Y <= Y2) Tah = 1;
                            if (X >= X1 & X <= X2 & Y >= Y2 & Y <= Y1) Tah = 1;
                            if (Tah == 1)
                            {
                                distance = Math.Abs((X2 - X1) * (Y1 - Y) - (X1 - X) * (Y2 - Y1)) / Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2));
                            }
                        }
                    endloop2: { }
                        if (distance < 0.1 * Zoom2d)
                        {
                            ifselected = 1;
                            ifselected1 = 1;
                            if (SpanShearDisSelected[i, j] == 1)
                            {
                                SpanShearDisSelected[i, j] = 0;
                            }
                            else
                            {
                                SpanShearDisSelected[i, j] = 1;
                            }
                            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                            {
                                for (int ii = 1; ii < ReinforcementBarNumber + 1; ii++)
                                {
                                    ReinforcementBar[ii].Selected = 0;
                                }
                                for (int ii = 1; ii < Spans + 1; ii++)
                                {
                                    for (int ji = 1; ji < 3 + 1; ji++)
                                    {
                                        SpanShearDisSelected[ii, ji] = 0;
                                    }
                                }
                                SpanShearDisSelected[i, j] = 1;
                                SelectedSpan = i;
                                if (j == 1)
                                {
                                    KindForBars = "SuRShear";
                                    AlValue = ShearSuR[SelectedSpan];
                                }
                                if (j == 2)
                                {
                                    KindForBars = "SpShear";
                                    AlValue = ShearSp[SelectedSpan];
                                }
                                if (j == 3)
                                {
                                    KindForBars = "SuLShear";
                                    SelectedSpan = SelectedSpan + 1;
                                    AlValue = ShearSuL[SelectedSpan];
                                }
                            }
                            goto endloop1;
                        }
                    }
                }
                #endregion
            endloop1: { }
                if (ifselected == 1)
                {
                    Render2d();
                    if (e.Button == MouseButtons.Right & ifselected1 == 1)
                    {
                        BeamDesignerBarsForm theform = new BeamDesignerBarsForm();
                        theform.ShowDialog();
                    }
                }
                if (ifselected == 0)
                {
                    for (int i = 1; i < ReinforcementBarNumber + 1; i++)
                    {
                        ReinforcementBar[i].Selected = 0;
                    }
                    for (int i = 1; i < Spans + 1; i++)
                    {
                        for (int j = 1; j < 3 + 1; j++)
                        {
                            SpanShearDisSelected[i, j] = 0;
                        }
                    }
                    Render2d();
                }
            }
        }
        private void pictureBox4_MouseUp(object sender, MouseEventArgs e)
        {
            #region//تحديد العناصر مع مربع التحديد
            if (MouseButtonsLeft == 1 & toolStripButton1.Checked == true)
            {
                MouseButtonsLeft = 0;
                int thecase = 0;
                if (e.X >= xmove1) thecase = 1;//تحديد العناصر الواقعة ضمن المربع تماما
                if (e.X < xmove1) thecase = 2;//تحديد العناصر المتقاطعة 
                int MaxX = 0;
                int MaxY = 0;
                int MinX = 0;
                int MinY = 0;
                MaxX = xmove1;
                MinX = xmove1;
                if (e.X > MaxX) MaxX = e.X;
                if (e.X < MinX) MinX = e.X;
                MaxY = ymove1;
                MinY = ymove1;
                if (e.Y > MaxY) MaxY = e.Y;
                if (e.Y < MinY) MinY = e.Y;
                int ifselected = 0;
                # region //تحديد خطوط القضبان
                for (int i = 1; i < ReinforcementBarNumber + 1; i++)
                {
                    int Tah1 = 0;
                    int Tah2 = 0;
                    int X3 = ReinforcementBar[i].PointX2d[1];
                    int Y3 = ReinforcementBar[i].PointY2d[1];
                    int X4 = ReinforcementBar[i].PointX2d[2];
                    int Y4 = ReinforcementBar[i].PointY2d[2];
                    if (X3 >= MinX & X3 <= MaxX & Y3 >= MinY & Y3 <= MaxY) Tah1 = 1;
                    if (X4 >= MinX & X4 <= MaxX & Y4 >= MinY & Y4 <= MaxY) Tah2 = 1;
                    if (thecase == 1)
                    {
                        if (Tah1 == 1 & Tah2 == 1)
                        {
                            ifselected = 1;
                            ReinforcementBar[i].Selected = 1;
                        }
                    }
                    if (thecase == 2)
                    {
                        if (Tah1 == 1 || Tah2 == 1)
                        {
                            ifselected = 1;
                            ReinforcementBar[i].Selected = 1;
                            goto end100;
                        }
                        INTERSECTION = 0;
                        int X1 = MinX;
                        int Y1 = MinY;
                        int X2 = MaxX;
                        int Y2 = MinY;
                        checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                        if (INTERSECTION == 1)
                        {
                            ifselected = 1;
                            ReinforcementBar[i].Selected = 1;
                            goto end100;
                        }
                        X1 = MinX;
                        Y1 = MinY;
                        X2 = MinX;
                        Y2 = MaxY;
                        checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                        if (INTERSECTION == 1)
                        {
                            ifselected = 1;
                            ReinforcementBar[i].Selected = 1;
                            goto end100;
                        }
                        X1 = MinX;
                        Y1 = MaxY;
                        X2 = MaxX;
                        Y2 = MaxY;
                        checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                        if (INTERSECTION == 1)
                        {
                            ifselected = 1;
                            ReinforcementBar[i].Selected = 1;
                            goto end100;
                        }
                        X1 = MaxX;
                        Y1 = MaxY;
                        X2 = MaxX;
                        Y2 = MinY;
                        checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                        if (INTERSECTION == 1)
                        {
                            ifselected = 1;
                            ReinforcementBar[i].Selected = 1;
                            goto end100;
                        }
                    }
                end100: { }
                }
                #endregion

                # region //تحديد خطوط الأساور
                for (int i = 1; i < Spans + 1; i++)
                {
                    for (int j = 1; j < 3 + 1; j++)
                    {
                        int Tah1 = 0;
                        int Tah2 = 0;
                        int X3 = SpanShearDisX12d[i,j];
                        int Y3 = SpanShearDisY12d[i, j];
                        int X4 = SpanShearDisX22d[i, j];
                        int Y4 = SpanShearDisY22d[i, j];
                        if (X3 >= MinX & X3 <= MaxX & Y3 >= MinY & Y3 <= MaxY) Tah1 = 1;
                        if (X4 >= MinX & X4 <= MaxX & Y4 >= MinY & Y4 <= MaxY) Tah2 = 1;
                        if (thecase == 1)
                        {
                            if (Tah1 == 1 & Tah2 == 1)
                            {
                                ifselected = 1;
                                SpanShearDisSelected[i, j] = 1;
                            }
                        }
                        if (thecase == 2)
                        {
                            if (Tah1 == 1 || Tah2 == 1)
                            {
                                ifselected = 1;
                                SpanShearDisSelected[i, j] = 1;
                                goto end100;
                            }
                            INTERSECTION = 0;
                            int X1 = MinX;
                            int Y1 = MinY;
                            int X2 = MaxX;
                            int Y2 = MinY;
                            checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                            if (INTERSECTION == 1)
                            {
                                ifselected = 1;
                                SpanShearDisSelected[i, j] = 1;
                                goto end100;
                            }
                            X1 = MinX;
                            Y1 = MinY;
                            X2 = MinX;
                            Y2 = MaxY;
                            checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                            if (INTERSECTION == 1)
                            {
                                ifselected = 1;
                                SpanShearDisSelected[i, j] = 1;
                                goto end100;
                            }
                            X1 = MinX;
                            Y1 = MaxY;
                            X2 = MaxX;
                            Y2 = MaxY;
                            checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                            if (INTERSECTION == 1)
                            {
                                ifselected = 1;
                                SpanShearDisSelected[i, j] = 1;
                                goto end100;
                            }
                            X1 = MaxX;
                            Y1 = MaxY;
                            X2 = MaxX;
                            Y2 = MinY;
                            checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                            if (INTERSECTION == 1)
                            {
                                ifselected = 1;
                                SpanShearDisSelected[i, j] = 1;
                                goto end100;
                            }
                        }
                    end100: { }
                    }
                }
                #endregion
                if (ifselected == 1)
                {
                    Render2d();
                }
                DrowRedline();
            }
            #endregion
        }
        private void pictureBox4Draw()
        {
            Bitmap finalBmp = new Bitmap(BmpWidth2d, BmpHeight2d);
            if (pictureBox4.Image != null)
            {
                pictureBox4.Image.Dispose();
                pictureBox4.Image = null;
            }
            pictureBox4.Image = finalBmp;
            #region//رسم مربع التحديد
            if (MouseButtonsLeft == 1 & toolStripButton1.Checked == true)
            {
                Graphics g = Graphics.FromImage(pictureBox4.Image);
                Pen pen = new Pen(Color.Black, 1f);
                Font drawFont = new Font("Tahoma", 10, FontStyle.Bold, GraphicsUnit.Pixel);
                SolidBrush drawBrush = new SolidBrush(Color.Black);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                float[] dashValues = { 5, 3, 5, 3 };
                pen.DashPattern = dashValues;
                Point[] polygon = new Point[5];
                polygon[0].X = xmove1;
                polygon[0].Y = ymove1;
                polygon[1].X = TempX;
                polygon[1].Y = ymove1;
                polygon[2].X = TempX;
                polygon[2].Y = TempY;
                polygon[3].X = xmove1;
                polygon[3].Y = TempY;
                polygon[4] = polygon[0];
                g.DrawLine(pen, polygon[0], polygon[1]);
                g.DrawLine(pen, polygon[1], polygon[2]);
                g.DrawLine(pen, polygon[2], polygon[3]);
                g.DrawLine(pen, polygon[3], polygon[4]);
                using (Brush brush = new SolidBrush(Color.FromArgb(30, Color.Orange)))
                {
                    g.FillPolygon(brush, polygon);
                }
                Pen pen1 = new Pen(Color.Red , 1f);
                int TheWidth = pictureBox4.Width;
                int TheHeight = pictureBox4.Height;
                int tx1 = Convert.ToInt32(startX2d + RedLineXR * Zoom2d);
                int ty1 = 0;
                int tx2 = tx1;
                int ty2 = TheHeight;
                g.DrawLine(pen1, tx1, ty1, tx2, ty2);
                pen = new Pen(Color.Black, 1f);
                tx1 = Convert.ToInt32(startX2d + Minval * Zoom2d);
               // ty1 = startY2d - 50;
                ty1 = startY2d + Convert.ToInt32(-1 * Zoom2d);
                tx2 = Convert.ToInt32(startX2d + RedLineXR * Zoom2d);
                ty2 = ty1;
                g.DrawLine(pen, tx1, ty1, tx2, ty2);
                string ghg = Math.Round((RedLineXR - Minval), 3).ToString() + " m";
                int aa1 = Convert.ToInt32(g.MeasureString(ghg, Font).Width / 2);
                int tx3 = (tx1 + tx2) / 2 - aa1;
                int ty3 = ty1 - 11;
                g.DrawString(ghg, drawFont, drawBrush, tx3, ty3);
            }
            #endregion
        }
        private void DrowRedline()
        {
            Bitmap finalBmp = new Bitmap(BmpWidth2d, BmpHeight2d);
            if (pictureBox4.Image != null)
            {
                pictureBox4.Image.Dispose();
                pictureBox4.Image = null;
            }
            pictureBox4.Image = finalBmp;

            Graphics g = Graphics.FromImage(pictureBox4.Image);
            Font drawFont = new Font("Tahoma", 10, FontStyle.Bold, GraphicsUnit.Pixel);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            Pen pen = new Pen(Color.Red, 1f);
            int TheWidth = pictureBox4.Width;
            int TheHeight = pictureBox4.Height;
            int tx1 = Convert.ToInt32(startX2d + RedLineXR * Zoom2d);
            int ty1 = 0;
            int tx2 = tx1;
            int ty2 = TheHeight;
            g.DrawLine(pen, tx1, ty1, tx2, ty2);

            pen = new Pen(Color.Black, 1f);
            tx1 = Convert.ToInt32(startX2d + Minval * Zoom2d);
            // ty1 = startY2d-50;
            ty1 = startY2d + Convert.ToInt32(-1 * Zoom2d);
            tx2 = Convert.ToInt32(startX2d + RedLineXR * Zoom2d);
            ty2 = ty1;
            g.DrawLine(pen, tx1, ty1, tx2, ty2);
            string ghg = Math.Round((RedLineXR - Minval), 3).ToString() + " m";
            int aa1 = Convert.ToInt32(g.MeasureString(ghg, Font).Width / 2);
            int tx3 = (tx1 + tx2) / 2 - aa1;
            int ty3 = ty1 - 11;
            g.DrawString(ghg, drawFont, drawBrush, tx3, ty3);
        }
        #endregion
        #region//saveopen
        private void OpenReal()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "e:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string strpath = openFileDialog1.FileName;
                    StreamReader SW = new StreamReader(strpath);

                    Spans = Convert.ToInt32(SW.ReadLine());
                    for (int i = 1; i < Spans + 1; i++)
                    {
                        BeamLength[i] = Convert.ToDouble(SW.ReadLine());
                        SpanSection[i] = Convert.ToInt32(SW.ReadLine());
                        MomentUpSp[i] = Convert.ToDouble(SW.ReadLine());
                        BarsSpUpGR[i] = Convert.ToInt32(SW.ReadLine());
                        for (int j = 1; j < 3 + 1; j++)
                        {
                            SpanShearDis[i, j] = Convert.ToDouble(SW.ReadLine());
                        }
                        for (int j = 1; j < BarsSpUpGR[i] + 1; j++)
                        {
                            BarsSpUpNo[i, j] = Convert.ToInt32(SW.ReadLine());
                            BarsSpUpD[i, j] = Convert.ToInt32(SW.ReadLine());
                            BarsSpUpKi[i, j] = Convert.ToInt32(SW.ReadLine());
                        }
                        MomentDnSp[i] = Convert.ToDouble(SW.ReadLine());
                        BarsSpDnGR[i] = Convert.ToInt32(SW.ReadLine());
                        for (int j = 1; j < BarsSpDnGR[i] + 1; j++)
                        {
                            BarsSpDnNo[i, j] = Convert.ToInt32(SW.ReadLine());
                            BarsSpDnD[i, j] = Convert.ToInt32(SW.ReadLine());
                            BarsSpDnKi[i, j] = Convert.ToInt32(SW.ReadLine());
                        }
                        ShearSp[i] = Convert.ToDouble(SW.ReadLine());
                        ShearBarsSpGR[i] = Convert.ToInt32(SW.ReadLine());
                        for (int j = 1; j < ShearBarsSpGR[i] + 1; j++)
                        {
                            ShearBarsSpNo[i, j] = Convert.ToInt32(SW.ReadLine());
                            ShearBarsSpD[i, j] = Convert.ToInt32(SW.ReadLine());
                            ShearBarsSpKi[i, j] = Convert.ToInt32(SW.ReadLine());
                            ShearBarsSpDis[i, j] = Convert.ToInt32(SW.ReadLine());
                        }
                    }

                    for (int i = 1; i < Spans + 2; i++)
                    {
                        SupportWeidth[i] = Convert.ToDouble(SW.ReadLine());
                        MomentUpSu[i] = Convert.ToDouble(SW.ReadLine());
                        BarsSuUpGR[i] = Convert.ToInt32(SW.ReadLine());
                        for (int j = 1; j < BarsSuUpGR[i] + 1; j++)
                        {
                            BarsSuUpNo[i, j] = Convert.ToInt32(SW.ReadLine());
                            BarsSuUpD[i, j] = Convert.ToInt32(SW.ReadLine());
                            BarsSuUpKi[i, j] = Convert.ToInt32(SW.ReadLine());
                        }
                        MomentDnSu[i] = Convert.ToDouble(SW.ReadLine());
                        BarsSuDnGR[i] = Convert.ToInt32(SW.ReadLine());
                        for (int j = 1; j < BarsSuDnGR[i] + 1; j++)
                        {
                            BarsSuDnNo[i, j] = Convert.ToInt32(SW.ReadLine());
                            BarsSuDnD[i, j] = Convert.ToInt32(SW.ReadLine());
                            BarsSuDnKi[i, j] = Convert.ToInt32(SW.ReadLine());
                        }
                        ShearSuL[i] = Convert.ToDouble(SW.ReadLine());
                        ShearBarsSuLGR[i] = Convert.ToInt32(SW.ReadLine());
                        for (int j = 1; j < ShearBarsSuLGR[i] + 1; j++)
                        {
                            ShearBarsSuLNo[i, j] = Convert.ToInt32(SW.ReadLine());
                            ShearBarsSuLD[i, j] = Convert.ToInt32(SW.ReadLine());
                            ShearBarsSuLKi[i, j] = Convert.ToInt32(SW.ReadLine());
                            ShearBarsSuLDis[i, j] = Convert.ToInt32(SW.ReadLine());
                        }
                        ShearSuR[i] = Convert.ToDouble(SW.ReadLine());
                        ShearBarsSuRGR[i] = Convert.ToInt32(SW.ReadLine());
                        for (int j = 1; j < ShearBarsSuRGR[i] + 1; j++)
                        {
                            ShearBarsSuRNo[i, j] = Convert.ToInt32(SW.ReadLine());
                            ShearBarsSuRD[i, j] = Convert.ToInt32(SW.ReadLine());
                            ShearBarsSuRKi[i, j] = Convert.ToInt32(SW.ReadLine());
                            ShearBarsSuRDis[i, j] = Convert.ToInt32(SW.ReadLine());
                        }
                    }
                    ReinforcementBarNumber = Convert.ToInt32(SW.ReadLine());
                    for (int i = 1; i < ReinforcementBarNumber + 1; i++)
                    {
                        ReinforcementBars emp = new ReinforcementBars();
                        ReinforcementBar[i]= emp ;
                        ReinforcementBar[i].Kind = Convert.ToInt32(SW.ReadLine());
                        ReinforcementBar[i].PointNumbers = Convert.ToInt32(SW.ReadLine());
                        for (int j = 1; j < ReinforcementBar[i].PointNumbers + 1; j++)
                        {
                            ReinforcementBar[i].PointXR[j] = Convert.ToDouble(SW.ReadLine());
                            ReinforcementBar[i].PointYR[j] = Convert.ToDouble(SW.ReadLine());
                            ReinforcementBar[i].PointZR[j] = Convert.ToDouble(SW.ReadLine());
                            ReinforcementBar[i].PointX2d[j] = Convert.ToInt32(SW.ReadLine());
                            ReinforcementBar[i].PointY2d[j] = Convert.ToInt32(SW.ReadLine());
                            ReinforcementBar[i].PointZ2d[j] = Convert.ToInt32(SW.ReadLine());
                        }
                        ReinforcementBar[i].Diameter = Convert.ToInt32(SW.ReadLine());
                        ReinforcementBar[i].Number = Convert.ToInt32(SW.ReadLine());
                    }
                    SW.Close();
                    FillInTable();
                    DrawDiagram();
                    Render2d();
                }
                catch
                {
                    MessageBox.Show("ERROR! Please check passphrase and do not attempt to edit cipher text");
                }
                MakeTempFiles();
            }
        }
        private void SaveReal()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = "e:\\";
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string strpath = saveFileDialog1.FileName + ".txt";
                    StreamWriter SW = new StreamWriter(strpath);
                    SW.WriteLine(Spans);
                    for (int i = 1; i < Spans + 1; i++)
                    {
                        SW.WriteLine(BeamLength[i]);
                        SW.WriteLine(SpanSection[i]);
                        SW.WriteLine(MomentUpSp[i]);
                        SW.WriteLine(BarsSpUpGR[i]);
                        for (int j = 1; j < 3 + 1; j++)
                        {
                            SW.WriteLine(SpanShearDis[i, j]);
                        }
                        for (int j = 1; j < BarsSpUpGR[i] + 1; j++)
                        {
                            SW.WriteLine(BarsSpUpNo[i, j]);
                            SW.WriteLine(BarsSpUpD[i, j]);
                            SW.WriteLine(BarsSpUpKi[i, j]);
                        }
                        SW.WriteLine(MomentDnSp[i]);
                        SW.WriteLine(BarsSpDnGR[i]);
                        for (int j = 1; j < BarsSpDnGR[i] + 1; j++)
                        {
                            SW.WriteLine(BarsSpDnNo[i, j]);
                            SW.WriteLine(BarsSpDnD[i, j]);
                            SW.WriteLine(BarsSpDnKi[i, j]);
                        }
                        SW.WriteLine(ShearSp[i]);
                        SW.WriteLine(ShearBarsSpGR[i]);
                        for (int j = 1; j < ShearBarsSpGR[i] + 1; j++)
                        {
                            SW.WriteLine(ShearBarsSpNo[i, j]);
                            SW.WriteLine(ShearBarsSpD[i, j]);
                            SW.WriteLine(ShearBarsSpKi[i, j]);
                            SW.WriteLine(ShearBarsSpDis[i, j]);
                        }
                    }

                    for (int i = 1; i < Spans + 2; i++)
                    {
                        SW.WriteLine(SupportWeidth[i]);
                        SW.WriteLine(MomentUpSu[i]);
                        SW.WriteLine(BarsSuUpGR[i]);
                        for (int j = 1; j < BarsSuUpGR[i] + 1; j++)
                        {
                            SW.WriteLine(BarsSuUpNo[i, j]);
                            SW.WriteLine(BarsSuUpD[i, j]);
                            SW.WriteLine(BarsSuUpKi[i, j]);
                        }
                        SW.WriteLine(MomentDnSu[i]);
                        SW.WriteLine(BarsSuDnGR[i]);
                        for (int j = 1; j < BarsSuDnGR[i] + 1; j++)
                        {
                            SW.WriteLine(BarsSuDnNo[i, j]);
                            SW.WriteLine(BarsSuDnD[i, j]);
                            SW.WriteLine(BarsSuDnKi[i, j]);
                        }
                        SW.WriteLine(ShearSuL[i]);
                        SW.WriteLine(ShearBarsSuLGR[i]);
                        for (int j = 1; j < ShearBarsSuLGR[i] + 1; j++)
                        {
                            SW.WriteLine(ShearBarsSuLNo[i, j]);
                            SW.WriteLine(ShearBarsSuLD[i, j]);
                            SW.WriteLine(ShearBarsSuLKi[i, j]);
                            SW.WriteLine(ShearBarsSuLDis[i, j]);
                        }
                        SW.WriteLine(ShearSuR[i]);
                        SW.WriteLine(ShearBarsSuRGR[i]);
                        for (int j = 1; j < ShearBarsSuRGR[i] + 1; j++)
                        {
                            SW.WriteLine(ShearBarsSuRNo[i, j]);
                            SW.WriteLine(ShearBarsSuRD[i, j]);
                            SW.WriteLine(ShearBarsSuRKi[i, j]);
                            SW.WriteLine(ShearBarsSuRDis[i, j]);
                        }
                    }
                    SW.WriteLine(ReinforcementBarNumber);
                    for (int i = 1; i < ReinforcementBarNumber + 1; i++)
                    {
                        SW.WriteLine(ReinforcementBar[i].Kind);
                        SW.WriteLine(ReinforcementBar[i].PointNumbers);

                        for (int j = 1; j < ReinforcementBar[i].PointNumbers + 1; j++)
                        {
                            SW.WriteLine(ReinforcementBar[i].PointXR[j]);
                            SW.WriteLine(ReinforcementBar[i].PointYR[j]);
                            SW.WriteLine(ReinforcementBar[i].PointZR[j]);
                            SW.WriteLine(ReinforcementBar[i].PointX2d[j]);
                            SW.WriteLine(ReinforcementBar[i].PointY2d[j]);
                            SW.WriteLine(ReinforcementBar[i].PointZ2d[j]);
                        }
                        SW.WriteLine(ReinforcementBar[i].Diameter);
                        SW.WriteLine(ReinforcementBar[i].Number);
                    }
                    SW.Close();
                }
                catch
                {
                    MessageBox.Show("ERROR! Please check passphrase and do not attempt to edit cipher text");
                }
            }
        }
        private void SaveTemp()
        {
            string strpath = TempFileName[TempSelectedFile];
            StreamWriter SW = new StreamWriter(strpath);
            SW.WriteLine(ReinforcementBarNumber);
            for (int i = 1; i < ReinforcementBarNumber + 1; i++)
            {
                SW.WriteLine(ReinforcementBar[i].Kind);
                SW.WriteLine(ReinforcementBar[i].PointNumbers);

                for (int j = 1; j < ReinforcementBar[i].PointNumbers + 1; j++)
                {
                    SW.WriteLine(ReinforcementBar[i].PointXR[j]);
                    SW.WriteLine(ReinforcementBar[i].PointYR[j]);
                    SW.WriteLine(ReinforcementBar[i].PointZR[j]);
                    SW.WriteLine(ReinforcementBar[i].PointX2d[j]);
                    SW.WriteLine(ReinforcementBar[i].PointY2d[j]);
                    SW.WriteLine(ReinforcementBar[i].PointZ2d[j]);
                }
                SW.WriteLine(ReinforcementBar[i].Diameter);
                SW.WriteLine(ReinforcementBar[i].Number);
            }
            for (int i = 1; i < Spans + 1; i++)
            {
                for (int j = 1; j < 3 + 1; j++)
                {
                    SW.WriteLine(SpanShearDis[i, j]);
                }
                SW.WriteLine(ShearBarsSpGR[i]);
                for (int j = 1; j < ShearBarsSpGR[i] + 1; j++)
                {
                    SW.WriteLine(ShearBarsSpNo[i, j]);
                    SW.WriteLine(ShearBarsSpD[i, j]);
                    SW.WriteLine(ShearBarsSpKi[i, j]);
                    SW.WriteLine(ShearBarsSpDis[i, j]);
                }
            }
            for (int i = 1; i < Spans + 2; i++)
            {
                SW.WriteLine(ShearBarsSuLGR[i]);
                for (int j = 1; j < ShearBarsSuLGR[i] + 1; j++)
                {
                    SW.WriteLine(ShearBarsSuLNo[i, j]);
                    SW.WriteLine(ShearBarsSuLD[i, j]);
                    SW.WriteLine(ShearBarsSuLKi[i, j]);
                    SW.WriteLine(ShearBarsSuLDis[i, j]);
                }
                SW.WriteLine(ShearBarsSuRGR[i]);
                for (int j = 1; j < ShearBarsSuRGR[i] + 1; j++)
                {
                    SW.WriteLine(ShearBarsSuRNo[i, j]);
                    SW.WriteLine(ShearBarsSuRD[i, j]);
                    SW.WriteLine(ShearBarsSuRKi[i, j]);
                    SW.WriteLine(ShearBarsSuRDis[i, j]);
                }
            }
            SW.Close();
        }
        private void OpenTemp()
        {
            string strpath = TempFileName[TempSelectedFile];
            StreamReader SW = new StreamReader(strpath);
            {
                ReinforcementBarNumber = Convert.ToInt32(SW.ReadLine());
                for (int i = 1; i < ReinforcementBarNumber + 1; i++)
                {
                    ReinforcementBars emp = new ReinforcementBars();
                    ReinforcementBar[i] = emp;
                    ReinforcementBar[i].Kind = Convert.ToInt32(SW.ReadLine());
                    ReinforcementBar[i].PointNumbers = Convert.ToInt32(SW.ReadLine());
                    for (int j = 1; j < ReinforcementBar[i].PointNumbers + 1; j++)
                    {
                        ReinforcementBar[i].PointXR[j] = Convert.ToDouble(SW.ReadLine());
                        ReinforcementBar[i].PointYR[j] = Convert.ToDouble(SW.ReadLine());
                        ReinforcementBar[i].PointZR[j] = Convert.ToDouble(SW.ReadLine());
                        ReinforcementBar[i].PointX2d[j] = Convert.ToInt32(SW.ReadLine());
                        ReinforcementBar[i].PointY2d[j] = Convert.ToInt32(SW.ReadLine());
                        ReinforcementBar[i].PointZ2d[j] = Convert.ToInt32(SW.ReadLine());
                    }
                    ReinforcementBar[i].Diameter = Convert.ToInt32(SW.ReadLine());
                    ReinforcementBar[i].Number = Convert.ToInt32(SW.ReadLine());
                }

                for (int i = 1; i < Spans + 1; i++)
                {
                    for (int j = 1; j < 3 + 1; j++)
                    {
                        SpanShearDis[i, j] = Convert.ToDouble(SW.ReadLine());
                    }
                    ShearBarsSpGR[i] = Convert.ToInt32(SW.ReadLine());
                    for (int j = 1; j < ShearBarsSpGR[i] + 1; j++)
                    {
                        ShearBarsSpNo[i, j] = Convert.ToInt32(SW.ReadLine());
                        ShearBarsSpD[i, j] = Convert.ToInt32(SW.ReadLine());
                        ShearBarsSpKi[i, j] = Convert.ToInt32(SW.ReadLine());
                        ShearBarsSpDis[i, j] = Convert.ToInt32(SW.ReadLine());
                    }
                }
                for (int i = 1; i < Spans + 2; i++)
                {
                    ShearBarsSuLGR[i] = Convert.ToInt32(SW.ReadLine());
                    for (int j = 1; j < ShearBarsSuLGR[i] + 1; j++)
                    {
                        ShearBarsSuLNo[i, j] = Convert.ToInt32(SW.ReadLine());
                        ShearBarsSuLD[i, j] = Convert.ToInt32(SW.ReadLine());
                        ShearBarsSuLKi[i, j] = Convert.ToInt32(SW.ReadLine());
                        ShearBarsSuLDis[i, j] = Convert.ToInt32(SW.ReadLine());
                    }
                    ShearBarsSuRGR[i] = Convert.ToInt32(SW.ReadLine());
                    for (int j = 1; j < ShearBarsSuRGR[i] + 1; j++)
                    {
                        ShearBarsSuRNo[i, j] = Convert.ToInt32(SW.ReadLine());
                        ShearBarsSuRD[i, j] = Convert.ToInt32(SW.ReadLine());
                        ShearBarsSuRKi[i, j] = Convert.ToInt32(SW.ReadLine());
                        ShearBarsSuRDis[i, j] = Convert.ToInt32(SW.ReadLine());
                    }
                }
                SW.Close();
            }
        }
        public void MakeTempFiles()
        {
            int M = TempFile;
            for (int i = TempSelectedFile + 1; i < M + 1; i++)
            {
                if ((System.IO.File.Exists(TempFileName[i])))
                {
                    System.IO.File.Delete(TempFileName[i]);
                    TempFile = TempFile - 1;
                }
            }
            TempFile = TempFile + 1;
            TempFileName[TempFile] = Convert.ToString(Directory.GetParent(@"./TempFilesSection/File" + TempFile + "/"));
            TempSelectedFile = TempFile;
            SaveTemp();
        }
        #endregion
        #region//اجرائيات
        private void DrawSection()
        {
            int TheSection = SpanSection[SelectedSpan];
            Bitmap finalBmp = new Bitmap(300, 300);
            if (pictureBox5.Image != null)
            {
                pictureBox5.Image.Dispose();
                pictureBox5.Image = null;
            }
            pictureBox5.Image = finalBmp;
            int startX = 100;
            int startY = 175;
            int ValueForZoom = 150;
            Graphics g = Graphics.FromImage(pictureBox5.Image);
            Pen pen = new Pen(Color.Black, 1f);
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            #region//مقطع L
            if (Section.DESIGNATION[TheSection] == "L")
            {
                double D = Section.D[TheSection];
                if (D > 0)
                {
                    Point[] P = new Point[8];
                    double B = Section.B[TheSection];
                    double TF = Section.TF[TheSection];
                    double TW = Section.TW[TheSection];
                    label1.Text = "D= " + Section.D[TheSection];
                    label1.Visible = true;
                    label2.Text = "B= " + Section.B[TheSection];
                    label2.Visible = true;
                    label3.Text = "TF= " + Section.TF[TheSection];
                    label3.Visible = true;
                    label4.Text = "TW= " + Section.TW[TheSection];
                    label4.Visible = true;
                    double MaxW = D;
                    if (B > MaxW) MaxW = B;
                    double thezoom = Convert.ToDouble(ValueForZoom / MaxW);
                    startX = Convert.ToInt32(startX - (B) / 2 * thezoom);

                    P[1].X = Convert.ToInt32(startX);
                    P[1].Y = Convert.ToInt32(startY);

                    P[2].X = Convert.ToInt32(startX);
                    P[2].Y = Convert.ToInt32(startY - D * thezoom);

                    P[3].X = Convert.ToInt32(startX + TW * thezoom);
                    P[3].Y = Convert.ToInt32(startY - D * thezoom);

                    P[4].X = Convert.ToInt32(startX + TW * thezoom);
                    P[4].Y = Convert.ToInt32(startY - TF * thezoom);

                    P[5].X = Convert.ToInt32(startX + B * thezoom);
                    P[5].Y = Convert.ToInt32(startY - TF * thezoom);

                    P[6].X = Convert.ToInt32(startX + B * thezoom);
                    P[6].Y = Convert.ToInt32(startY);
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

                    double X = Section.X[TheSection];
                    double Y = Section.Y[TheSection];
                    pen = new Pen(Color.Red, 2f);
                    P[1].X = Convert.ToInt32(startX + X * thezoom);
                    P[1].Y = Convert.ToInt32(startY - Y * thezoom);

                    P[2].X = P[1].X - 40;
                    P[2].Y = P[1].Y;
                    P[3].X = P[1].X;
                    P[3].Y = P[1].Y - 40;

                    g.DrawLine(pen, P[1], P[2]);
                    g.DrawLine(pen, P[1], P[3]);

                    g.Dispose();
                }
            }
            #endregion
            #region//مقطع I
            if (Section.DESIGNATION[TheSection] == "I" || Section.DESIGNATION[TheSection] == "W")
            {
                double D = Section.D[TheSection];
                if (D > 0)
                {
                    Point[] P = new Point[14];
                    double BF = Section.BF[TheSection];
                    double TF = Section.TF[TheSection];
                    double BFB = Section.BFB[TheSection];
                    double TFB = Section.TFB[TheSection];
                    double TW = Section.TW[TheSection];
                    label1.Text = "D= " + Section.D[TheSection];
                    label1.Visible = true;
                    label2.Text = "BF= " + Section.BF[TheSection];
                    label2.Visible = true;
                    label3.Text = "TF= " + Section.TF[TheSection];
                    label3.Visible = true;
                    label4.Text = "BFB= " + Section.BFB[TheSection];
                    label4.Visible = true;
                    label5.Text = "TFB= " + Section.TFB[TheSection];
                    label5.Visible = true;
                    label6.Text = "TW= " + Section.TW[TheSection];
                    label6.Visible = true;

                    double MaxW = D;
                    if (BF > MaxW) MaxW = BF;
                    if (BFB > MaxW) MaxW = BFB;
                    double thezoom = Convert.ToDouble(ValueForZoom / MaxW);
                    double MinB = BF;
                    //startX = Convert.ToInt32(startX - (BFB) / 2 * thezoom);

                    P[1].X = Convert.ToInt32(startX - (BFB) / 2 * thezoom);
                    P[1].Y = Convert.ToInt32(startY);

                    P[2].X = P[1].X;
                    P[2].Y = Convert.ToInt32(startY - TFB * thezoom);

                    P[3].X = Convert.ToInt32(startX - (TW) / 2 * thezoom);
                    P[3].Y = P[2].Y;

                    P[4].X = P[3].X;
                    P[4].Y = Convert.ToInt32(startY - (D - TF) * thezoom);

                    P[5].X = Convert.ToInt32(startX - (BF) / 2 * thezoom);
                    P[5].Y = P[4].Y;

                    P[6].X = P[5].X;
                    P[6].Y = Convert.ToInt32(startY - D * thezoom);

                    P[7].X = Convert.ToInt32(startX + (BF) / 2 * thezoom);
                    P[7].Y = P[6].Y;

                    P[8].X = P[7].X;
                    P[8].Y = P[5].Y;

                    P[9].X = Convert.ToInt32(startX + (TW) / 2 * thezoom);
                    P[9].Y = P[5].Y;

                    P[10].X = P[9].X;
                    P[10].Y = P[2].Y;

                    P[11].X = Convert.ToInt32(startX + (BFB) / 2 * thezoom);
                    P[11].Y = P[2].Y;

                    P[12].X = P[11].X;
                    P[12].Y = P[1].Y;

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

                    using (Brush brush = new SolidBrush(Color.FromArgb(80, Color.Gray)))
                    {
                        g.FillPolygon(brush, P);
                    }
                    double X = Section.X[TheSection];
                    double Y = Section.Y[TheSection];
                    pen = new Pen(Color.Red, 2f);
                    P[1].X = Convert.ToInt32(startX + X * thezoom);
                    P[1].Y = Convert.ToInt32(startY - Y * thezoom);

                    P[2].X = P[1].X - 40;
                    P[2].Y = P[1].Y;
                    P[3].X = P[1].X;
                    P[3].Y = P[1].Y - 40;

                    g.DrawLine(pen, P[1], P[2]);
                    g.DrawLine(pen, P[1], P[3]);
                    g.Dispose();
                }
            }
            #endregion
            #region//مقطع T
            if (Section.DESIGNATION[TheSection] == "T")
            {
                double D = Section.D[TheSection];
                if (D > 0)
                {
                    Point[] P = new Point[10];
                    double BF = Section.BF[TheSection];
                    double TF = Section.TF[TheSection];
                    double TW = Section.TW[TheSection];
                    double Y = Section.Y[TheSection];

                    label1.Text = "D= " + Section.D[TheSection];
                    label1.Visible = true;
                    label2.Text = "BF= " + Section.BF[TheSection];
                    label2.Visible = true;
                    label3.Text = "TF= " + Section.TF[TheSection];
                    label3.Visible = true;
                    label4.Text = "TW= " + Section.TW[TheSection];
                    label4.Visible = true;

                    double MaxW = D;
                    if (BF > MaxW) MaxW = BF;
                    double thezoom = Convert.ToDouble(ValueForZoom / MaxW);
                    startX = Convert.ToInt32(startX - BF / 2 * thezoom);
                    startY = Convert.ToInt32(startY - (D) * thezoom);
                    P[1].X = Convert.ToInt32(startX);
                    P[1].Y = Convert.ToInt32(startY);

                    P[2].X = startX + Convert.ToInt32(BF * thezoom);
                    P[2].Y = P[1].Y;

                    P[3].X = P[2].X;
                    P[3].Y = P[2].Y + Convert.ToInt32(TF * thezoom);

                    P[4].X = P[3].X - Convert.ToInt32((BF / 2 - TW / 2) * thezoom);
                    P[4].Y = P[3].Y;

                    P[5].X = P[4].X;
                    P[5].Y = startY + Convert.ToInt32((D) * thezoom);

                    P[6].X = P[5].X - Convert.ToInt32(TW * thezoom);
                    P[6].Y = P[5].Y;

                    P[7].X = P[6].X;
                    P[7].Y = P[4].Y;

                    P[8].X = P[1].X;
                    P[8].Y = P[7].Y;

                    P[9] = P[1];
                    g.DrawLine(pen, P[1], P[2]);
                    g.DrawLine(pen, P[2], P[3]);
                    g.DrawLine(pen, P[3], P[4]);
                    g.DrawLine(pen, P[4], P[5]);
                    g.DrawLine(pen, P[5], P[6]);
                    g.DrawLine(pen, P[6], P[7]);
                    g.DrawLine(pen, P[7], P[8]);
                    g.DrawLine(pen, P[8], P[1]);


                    using (Brush brush = new SolidBrush(Color.FromArgb(80, Color.Gray)))
                    {
                        g.FillPolygon(brush, P);
                    }
                    double X = (BF) / 2;

                    pen = new Pen(Color.Red, 2f);
                    P[1].X = Convert.ToInt32(startX + X * thezoom);
                    P[1].Y = Convert.ToInt32(startY + Y * thezoom);

                    P[2].X = P[1].X - 40;
                    P[2].Y = P[1].Y;
                    P[3].X = P[1].X;
                    P[3].Y = P[1].Y - 40;

                    g.DrawLine(pen, P[1], P[2]);
                    g.DrawLine(pen, P[1], P[3]);
                    g.Dispose();
                }
            }
            #endregion
            #region//مقطع مستطيل
            if (Section.DESIGNATION[TheSection] == "R")
            {
                double D = Section.D[TheSection];
                double HT = Section.HT[TheSection];
                if (D == 0) D = HT;
                if (D > 0)
                {
                    Point[] P = new Point[6];
                    double B = Section.B[TheSection];
                    label1.Text = "D= " + Section.D[TheSection];
                    label1.Visible = true;
                    label2.Text = "HT= " + Section.HT[TheSection];
                    label2.Visible = true;
                    label3.Text = "B= " + Section.B[TheSection];
                    label3.Visible = true;

                    double MaxW = D;
                    if (B > MaxW) MaxW = B;
                    double thezoom = Convert.ToDouble(ValueForZoom / MaxW);
                    startX = Convert.ToInt32(startX - (B) / 2 * thezoom);
                    P[1].X = Convert.ToInt32(startX);
                    P[1].Y = Convert.ToInt32(startY);
                    P[2].X = Convert.ToInt32(startX);
                    P[2].Y = Convert.ToInt32(startY - D * thezoom);
                    P[3].X = Convert.ToInt32(startX + B * thezoom);
                    P[3].Y = Convert.ToInt32(startY - D * thezoom);
                    P[4].X = Convert.ToInt32(startX + B * thezoom);
                    P[4].Y = Convert.ToInt32(startY);
                    P[5] = P[1];
                    g.DrawLine(pen, P[1], P[2]);
                    g.DrawLine(pen, P[2], P[3]);
                    g.DrawLine(pen, P[3], P[4]);
                    g.DrawLine(pen, P[4], P[1]);
                    using (Brush brush = new SolidBrush(Color.FromArgb(80, Color.Gray)))
                    {
                        g.FillPolygon(brush, P);
                    }
                    double X = Section.X[TheSection];
                    double Y = Section.Y[TheSection];
                    pen = new Pen(Color.Red, 2f);
                    P[1].X = Convert.ToInt32(startX + B / 2 * thezoom);
                    P[1].Y = Convert.ToInt32(startY - D / 2 * thezoom);
                    P[2].X = P[1].X - 40;
                    P[2].Y = P[1].Y;
                    P[3].X = P[1].X;
                    P[3].Y = P[1].Y - 40;
                    g.DrawLine(pen, P[1], P[2]);
                    g.DrawLine(pen, P[1], P[3]);
                    g.Dispose();
                }
            }
            #endregion
            #region//مقطع C
            if (Section.DESIGNATION[TheSection] == "C")
            {
                double D = Section.D[TheSection];
                if (D > 0)
                {
                    Point[] P = new Point[10];
                    double BF = Section.BF[TheSection];
                    double TF = Section.TF[TheSection];
                    double TW = Section.TW[TheSection];
                    label1.Text = "D= " + Section.D[TheSection];
                    label1.Visible = true;
                    label2.Text = "BF= " + Section.BF[TheSection];
                    label2.Visible = true;
                    label3.Text = "TF= " + Section.TF[TheSection];
                    label3.Visible = true;
                    label4.Text = "TW= " + Section.TW[TheSection];
                    label4.Visible = true;

                    double MaxW = D;
                    if (BF > MaxW) MaxW = BF;
                    double thezoom = Convert.ToDouble(ValueForZoom / MaxW);
                    startX = Convert.ToInt32(startX - BF / 2 * thezoom);
                    startY = Convert.ToInt32(startY - (D) * thezoom);
                    P[1].X = Convert.ToInt32(startX);
                    P[1].Y = Convert.ToInt32(startY);

                    P[2].X = startX + Convert.ToInt32(BF * thezoom);
                    P[2].Y = P[1].Y;

                    P[3].X = P[2].X;
                    P[3].Y = P[2].Y + Convert.ToInt32(TF * thezoom);

                    P[4].X = P[3].X - Convert.ToInt32((BF - TW) * thezoom);
                    P[4].Y = P[3].Y;

                    P[5].X = P[4].X;
                    P[5].Y = P[1].Y + Convert.ToInt32((D - TF) * thezoom);

                    P[6].X = P[5].X + Convert.ToInt32((BF - TW) * thezoom);
                    P[6].Y = P[5].Y;

                    P[7].X = P[6].X;
                    P[7].Y = P[6].Y + Convert.ToInt32((TF) * thezoom); ;

                    P[8].X = P[1].X;
                    P[8].Y = P[7].Y;

                    P[9] = P[1];
                    g.DrawLine(pen, P[1], P[2]);
                    g.DrawLine(pen, P[2], P[3]);
                    g.DrawLine(pen, P[3], P[4]);
                    g.DrawLine(pen, P[4], P[5]);
                    g.DrawLine(pen, P[5], P[6]);
                    g.DrawLine(pen, P[6], P[7]);
                    g.DrawLine(pen, P[7], P[8]);
                    g.DrawLine(pen, P[8], P[1]);


                    using (Brush brush = new SolidBrush(Color.FromArgb(80, Color.Gray)))
                    {
                        g.FillPolygon(brush, P);
                    }
                    double X = Section.X[TheSection];
                    double Y = D / 2;
                    pen = new Pen(Color.Red, 2f);
                    P[1].X = Convert.ToInt32(startX + X * thezoom);
                    P[1].Y = Convert.ToInt32(startY + Y * thezoom);

                    P[2].X = P[1].X - 40;
                    P[2].Y = P[1].Y;
                    P[3].X = P[1].X;
                    P[3].Y = P[1].Y - 40;

                    g.DrawLine(pen, P[1], P[2]);
                    g.DrawLine(pen, P[1], P[3]);
                    g.Dispose();
                }
            }
            #endregion
            #region//مقطع بوكس
            if (Section.DESIGNATION[TheSection] == "B")
            {
                double D = Section.D[TheSection];
                double HT = Section.HT[TheSection];
                if (D == 0) D = HT;
                if (D > 0)
                {
                    Point[] P = new Point[6];
                    double B = Section.B[TheSection];
                    double TF = Section.TF[TheSection];
                    double TW = Section.TW[TheSection];
                    label1.Text = "D= " + Section.D[TheSection];
                    label1.Visible = true;
                    label2.Text = "HT= " + Section.HT[TheSection];
                    label2.Visible = true;
                    label3.Text = "B= " + Section.B[TheSection];
                    label3.Visible = true;
                    label4.Text = "TF= " + Section.TF[TheSection];
                    label4.Visible = true;
                    label5.Text = "TW= " + Section.TW[TheSection];
                    label5.Visible = true;
                    double MaxW = D;
                    if (B > MaxW) MaxW = B;
                    double thezoom = Convert.ToDouble(ValueForZoom / MaxW);
                    startX = Convert.ToInt32(startX - (B) / 2 * thezoom);

                    P[1].X = Convert.ToInt32(startX);
                    P[1].Y = Convert.ToInt32(startY);
                    P[2].X = Convert.ToInt32(startX);
                    P[2].Y = Convert.ToInt32(startY - D * thezoom);
                    P[3].X = Convert.ToInt32(startX + B * thezoom);
                    P[3].Y = Convert.ToInt32(startY - D * thezoom);
                    P[4].X = Convert.ToInt32(startX + B * thezoom);
                    P[4].Y = Convert.ToInt32(startY);
                    P[5] = P[1];
                    using (Brush brush = new SolidBrush(Color.FromArgb(80, Color.Gray)))
                    {
                        g.FillPolygon(brush, P);
                    }
                    g.DrawLine(pen, P[1], P[2]);
                    g.DrawLine(pen, P[2], P[3]);
                    g.DrawLine(pen, P[3], P[4]);
                    g.DrawLine(pen, P[4], P[1]);
                    P[1].X = Convert.ToInt32(startX + TW * thezoom);
                    P[1].Y = Convert.ToInt32(startY - (D - TF) * thezoom);
                    P[2].X = Convert.ToInt32(startX + (B - TW) * thezoom);
                    P[2].Y = Convert.ToInt32(startY - (D - TF) * thezoom);
                    P[3].X = Convert.ToInt32(startX + (B - TW) * thezoom);
                    P[3].Y = Convert.ToInt32(startY - (TF) * thezoom);
                    P[4].X = Convert.ToInt32(startX + (TW) * thezoom);
                    P[4].Y = Convert.ToInt32(startY - (TF) * thezoom);
                    P[5] = P[1];
                    using (Brush brush = new SolidBrush(Color.AliceBlue))
                    {
                        g.FillPolygon(brush, P);
                    }
                    g.DrawLine(pen, P[1], P[2]);
                    g.DrawLine(pen, P[2], P[3]);
                    g.DrawLine(pen, P[3], P[4]);
                    g.DrawLine(pen, P[4], P[1]);
                    double X = Section.X[TheSection];
                    double Y = Section.Y[TheSection];
                    pen = new Pen(Color.Red, 2f);
                    P[1].X = Convert.ToInt32(startX + B / 2 * thezoom);
                    P[1].Y = Convert.ToInt32(startY - D / 2 * thezoom);

                    P[2].X = P[1].X - 40;
                    P[2].Y = P[1].Y;
                    P[3].X = P[1].X;
                    P[3].Y = P[1].Y - 40;

                    g.DrawLine(pen, P[1], P[2]);
                    g.DrawLine(pen, P[1], P[3]);

                    g.Dispose();
                }
            }
            #endregion
            #region//مقطع دائرة
            if (Section.DESIGNATION[TheSection] == "CR")
            {
                startX = 125;
                startY = 125;
                double D = Section.D[TheSection];
                if (D > 0)
                {
                    Point[] P = new Point[25];
                    label1.Text = "D= " + Section.D[TheSection];
                    label1.Visible = true;
                    double MaxW = D;
                    double alfa = Math.PI / 12;
                    double thezoom = Convert.ToDouble(ValueForZoom / MaxW);
                    for (int i = 0; i < 25; i++)
                    {
                        double alfa1 = alfa * (i);
                        P[i].X = Convert.ToInt32(startX + (Math.Cos(alfa1) * D / 2) * thezoom);
                        P[i].Y = Convert.ToInt32(startY - (Math.Sin(alfa1) * D / 2) * thezoom);
                    }
                    for (int i = 0; i < 24; i++)
                    {
                        g.DrawLine(pen, P[i], P[i + 1]);
                    }
                    using (Brush brush = new SolidBrush(Color.FromArgb(80, Color.Gray)))
                    {
                        g.FillPolygon(brush, P);
                    }

                    double X = Section.X[TheSection];
                    double Y = Section.Y[TheSection];
                    pen = new Pen(Color.Red, 2f);
                    P[1].X = Convert.ToInt32(startX);
                    P[1].Y = Convert.ToInt32(startY);
                    P[2].X = P[1].X - 40;
                    P[2].Y = P[1].Y;
                    P[3].X = P[1].X;
                    P[3].Y = P[1].Y - 40;
                    g.DrawLine(pen, P[1], P[2]);
                    g.DrawLine(pen, P[1], P[3]);
                    g.Dispose();
                }
            }
            #endregion
        }
        private void DrawReinforcement()
        {
            Graphics g = Graphics.FromImage(pictureBox3.Image);
            //int Fsize = Convert.ToInt32 (0.2 * Zoom2d);
            int Fsize = 10;
            Font drawFont = new Font("Tahoma", Fsize, FontStyle.Bold, GraphicsUnit.Pixel);
            SolidBrush drawBrush = new SolidBrush(Color.Blue);
            SolidBrush drawBrush1 = new SolidBrush(Color.Brown);
            Pen pen = new Pen(Color.Blue, 1f);
            //تسليح القص
            double thestartx0 = -SupportWeidth[1]/2;
            double thestartx = 0;
            double theDis0 = 0;
            double theDis = 0;
            int theD = 0;
            int theNo = 0;
            double theExtend = 0;
            int thei = 0;
            for (int i = 1; i < Spans + 1; i++)
            {
                int tx1 = 0;
                int ty1 = 0;
                int tx2 = 0;
                int ty2 = 0;
                thestartx0 = thestartx0 + SupportWeidth[i];
                for (int j = 1; j < 3 + 1; j++)
                {
                    pen = new Pen(Color.Brown, 1f);
                    theExtend = SpanShearDis[i, j];
                    thestartx = thestartx0;
                    if (j == 1) thei = ShearBarsSuRGR[i];
                    if (j == 2) thei = ShearBarsSpGR[i];
                    if (j == 3) thei = ShearBarsSuLGR[i + 1];
                    if (SpanShearDisSelected[i, j] == 1) pen = new Pen(Color.Red, 2f);
                    string gg = "";
                    for (int k = 1; k < thei + 1; k++)
                    {
                        if (j == 1)
                        {
                            theDis0 = ShearBarsSuRDis[i, k];
                            theD = ShearBarsSuRD[i, k];
                            theNo = ShearBarsSuRNo[i, k];
                        }
                        if (j == 2)
                        {
                            theDis0 = ShearBarsSpDis[i, k];
                            theD = ShearBarsSpD[i, k];
                            theNo = ShearBarsSpNo[i, k];
                        }
                        if (j == 3)
                        {
                            theDis0 = ShearBarsSuLDis[i + 1, k];
                            theD = ShearBarsSuLD[i+1, k];
                            theNo = ShearBarsSuLNo[i+1, k];
                        }
                        if (k != thei)
                        {
                            gg = gg + theNo + "C" + theD + "/" + theDis0 + " + ";
                        }
                        else
                        {
                            gg = gg + theNo + "C" + theD + "/" + theDis0;
                        }
                        theDis = theDis0 / 100;
                        if (theDis > 0)
                        {
                            int thenum = Convert.ToInt32(Math.Ceiling(theExtend / theDis));
                            theDis = theExtend / thenum;
                            int st = 0;
                            if (j == 1) thenum = thenum-1;
                            if (j == 3) st = 1;
                            for (int l = st; l < thenum + 1; l++)
                            {
                                tx1 = startX2d + Convert.ToInt32((thestartx + l * theDis) * Zoom2d);
                                ty1 = startY2d + Convert.ToInt32((0.025) * Zoom2d);
                                tx2 = tx1;
                                ty2 = startY2d + Convert.ToInt32((SpanHeight[i] - 0.025) * Zoom2d);
                                g.DrawLine(pen, tx1, ty1, tx2, ty2);
                            }
                        }
                    }
                    if (j == 1) pen = new Pen(Color.Violet, 2f);
                    if (j == 2) pen = new Pen(Color.LimeGreen, 2f);
                    if (j == 3) pen = new Pen(Color.Violet, 2f);
                    if (SpanShearDisSelected[i, j] == 1) pen = new Pen(Color.Red , 2f);
                    tx1 = startX2d + Convert.ToInt32(thestartx0 * Zoom2d);
                    ty1 = startY2d + Convert.ToInt32((MAXExtendS - 1.2) * Zoom2d);
                    thestartx0 = thestartx0 + SpanShearDis[i, j];
                    tx2 = startX2d + Convert.ToInt32(thestartx0 * Zoom2d);
                    ty2 = ty1;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    SpanShearDisX12d[i, j] = tx1;
                    SpanShearDisY12d[i, j] = ty1;
                    SpanShearDisX22d[i, j] = tx2;
                    SpanShearDisY22d[i, j] = ty2;
                    int aa1 = Convert.ToInt32(g.MeasureString(gg, Font).Width / 2);
                    g.DrawString(gg, drawFont, drawBrush1, (tx1 + tx2) / 2 - aa1, ty1 - 14);
                    gg = Math.Round (SpanShearDis[i, j],3) +"m";
                    aa1 = Convert.ToInt32(g.MeasureString(gg, Font).Width / 2);
                    g.DrawString(gg, drawFont, drawBrush1, (tx1 + tx2) / 2 - aa1, ty1 + 1);
                }
            }
            pen = new Pen(Color.Blue, 1f);
            for (int i = 1; i < ReinforcementBarNumber + 1; i++)
            {
                ReinforcementBar[i].PointX2d[1] = startX2d + Convert.ToInt32(ReinforcementBar[i].PointXR[1] * Zoom2d);
                ReinforcementBar[i].PointX2d[2] = startX2d + Convert.ToInt32(ReinforcementBar[i].PointXR[2] * Zoom2d);
                ReinforcementBar[i].PointY2d[1] = startY2d + Convert.ToInt32(ReinforcementBar[i].PointYR[1] * Zoom2d);
                ReinforcementBar[i].PointY2d[2] = startY2d + Convert.ToInt32(ReinforcementBar[i].PointYR[2] * Zoom2d);
                ReinforcementBar[i].PointZ2d[1] = startY2d + Convert.ToInt32(ReinforcementBar[i].PointZR[1] * Zoom2d);
                ReinforcementBar[i].PointZ2d[2] = startY2d + Convert.ToInt32(ReinforcementBar[i].PointZR[2] * Zoom2d);
                string theword = "";
                theword = (ReinforcementBar[i].Number + "D" + ReinforcementBar[i].Diameter);
                int shara = 1;
                if (ReinforcementBar[i].Kind == 1)
                {
                    pen = new Pen(Color.Blue, 1f);
                    drawBrush = new SolidBrush(Color.Blue);
                }
                if (ReinforcementBar[i].Kind == 2)
                {
                    pen = new Pen(Color.Green, 1f);
                    drawBrush = new SolidBrush(Color.Green);
                    shara = -1;
                }
                if (ReinforcementBar[i].Selected == 1) pen = new Pen(Color.Red, 2f);
                for (int j = 1; j < ReinforcementBar[i].PointNumbers; j++)
                {
                    int tx1 = ReinforcementBar[i].PointX2d[j];
                    int ty1 = ReinforcementBar[i].PointY2d[j];
                    int tx2 = ReinforcementBar[i].PointX2d[j + 1];
                    int ty2 = ReinforcementBar[i].PointY2d[j + 1];
                    int tz1 = ReinforcementBar[i].PointZ2d[j];
                    int tz2 = ReinforcementBar[i].PointZ2d[j + 1];
                    int extd = Convert.ToInt32(0.05 * Zoom2d);
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    g.DrawLine(pen, tx1, ty1, tx1 - extd, ty1 + shara * extd);
                    g.DrawLine(pen, tx2, ty2, tx2 + extd, ty2 + shara * extd);
                    g.DrawLine(pen, tx1, tz1, tx2, tz2);
                    g.DrawLine(pen, tx1, tz1, tx1 - extd, tz1 +shara* extd);
                    g.DrawLine(pen, tx2, tz2, tx2 + extd, tz2 +shara* extd);
                    double thelength = Math.Round(ReinforcementBar[i].PointXR[2] - ReinforcementBar[i].PointXR[1], 3);
                    string ghg = "(" + i + ") " + theword + " , L= " + thelength + " m";
                    int aa1 = Convert.ToInt32(g.MeasureString(ghg, Font).Width / 2);
                    g.DrawString(ghg, drawFont, drawBrush, (tx1 + tx2) / 2 - aa1, ty1 - 12);
                }
            }
        }
        private void AutoReinforcement()
        {
            double BeamSum = 0;
            ReinforcementBarNumber = 0;
            double MAXExtendS1 = MAXExtendS + 1;
            #region//حسابات للقص
            for (int i = 1; i < Spans + 1; i++)
            {
                SpanShearDis[i, 1] = (BeamLength[i] - SupportWeidth[i]/2 - SupportWeidth[i + 1]/2)/3;
                SpanShearDis[i, 2] = SpanShearDis[i, 1];
                SpanShearDis[i, 3] = SpanShearDis[i, 1];
            }
            #endregion
            #region //قضبان علوية
            #region//فوق المساند
            BeamSum = 0;
            for (int i = 1; i < Spans + 2; i++)
            {
                for (int ik = 1; ik < BarsSuUpGR[i] + 1; ik++)
                {
                    ReinforcementBarNumber = ReinforcementBarNumber + 1;
                    ReinforcementBars emp = new ReinforcementBars();
                    emp.PointNumbers = 2;
                    double thelength = BeamLength[i];
                    if (i > 1 & i < Spans + 1)
                    {
                        if (BeamLength[i + 1] > BeamLength[i]) thelength = BeamLength[i + 1];
                        emp.PointXR[1] = BeamSum - thelength / 3;
                        emp.PointXR[2] = BeamSum + thelength / 3;
                    }
                    if (i == 1)
                    {
                        thelength = BeamLength[i];
                        emp.PointXR[1] = BeamSum;
                        emp.PointXR[2] = BeamSum + thelength / 3;
                    }
                    if (i == Spans + 1)
                    {
                        thelength = BeamLength[i - 1];
                        emp.PointXR[1] = BeamSum + BeamLength[i] - thelength / 3;
                        emp.PointXR[2] = BeamSum + BeamLength[i];
                    }
                    MAXExtendS1 = MAXExtendS + 1 + 0.3;
                    for (int j = 1; j < ReinforcementBarNumber; j++)
                    {
                        if (ReinforcementBar[j].PointXR[2] >= emp.PointXR[1] & ReinforcementBar[j].Kind == 1)
                        {
                            if (ReinforcementBar[j].PointYR[1] == MAXExtendS1)
                            {
                                MAXExtendS1 = ReinforcementBar[j].PointYR[1] + 0.3;
                            }
                        }
                    }
                    emp.PointYR[1] = MAXExtendS1;
                    emp.PointYR[2] = emp.PointYR[1];
                    emp.PointZR[1] = 0.025;
                    emp.PointZR[2] = emp.PointZR[1];
                    emp.Kind = 1;
                    emp.Number = BarsSuUpNo[i, ik];
                    emp.Diameter = BarsSuUpD[i, ik];
                    ReinforcementBar[ReinforcementBarNumber] = emp;
                }
                BeamSum = BeamSum + BeamLength[i];
            }
            #endregion
            #region//فوق الفتحات
            BeamSum = 0;
            for (int i = 1; i < Spans + 1; i++)
            {
                for (int ik = 1; ik < BarsSpUpGR[i] + 1; ik++)
                {
                    ReinforcementBarNumber = ReinforcementBarNumber + 1;
                    ReinforcementBars emp = new ReinforcementBars();
                    emp.PointNumbers = 2;
                    double thelength = BeamLength[i];
                    emp.PointXR[1] = BeamSum + 0.4;
                    emp.PointXR[2] = BeamSum + BeamLength[i] - 0.4;
                    MAXExtendS1 = MAXExtendS + 1 + 0.3;
                    for (int j = 1; j < ReinforcementBarNumber; j++)
                    {
                        if (ReinforcementBar[j].PointXR[2] >= emp.PointXR[1] & ReinforcementBar[j].Kind == 1)
                        {
                            if (ReinforcementBar[j].PointYR[1] == MAXExtendS1)
                            {
                                MAXExtendS1 = ReinforcementBar[j].PointYR[1] + 0.3;
                            }
                        }
                    }
                    emp.PointYR[1] = MAXExtendS1;
                    emp.PointYR[2] = emp.PointYR[1];
                    emp.PointZR[1] =  0.025;
                    emp.PointZR[2] = emp.PointZR[1];
                    emp.Kind = 1;
                    emp.Number = BarsSpUpNo[i, ik];
                    emp.Diameter = BarsSpUpD[i, ik];
                    ReinforcementBar[ReinforcementBarNumber] = emp;
                }
                BeamSum = BeamSum + BeamLength[i];
            }
            #endregion
            double themaxy = -1000000;
            for (int i = 1; i < ReinforcementBarNumber + 1; i++)
            {
                if (ReinforcementBar[i].PointYR[1] > themaxy & ReinforcementBar[i].Kind == 1)
                {
                    themaxy = ReinforcementBar[i].PointYR[1];
                }
            }
            #endregion
            //قضبان سفلية
            BeamSum = 0;
            for (int i = 1; i < Spans + 1; i++)
            {
                for (int ik = 1; ik < BarsSpDnGR[i] + 1; ik++)
                {
                    ReinforcementBarNumber = ReinforcementBarNumber + 1;
                    ReinforcementBars emp = new ReinforcementBars();
                    emp.PointNumbers = 2;
                    emp.PointXR[1] = BeamSum - 0.4;
                    if (i == 1) emp.PointXR[1] = BeamSum;
                    emp.PointXR[2] = BeamSum + BeamLength[i] + 0.4;
                    if (i == Spans) emp.PointXR[2] = BeamSum + BeamLength[i];
                    MAXExtendS1 = themaxy  + 1;

                    for (int j = 1; j < ReinforcementBarNumber; j++)
                    {
                        if (ReinforcementBar[j].PointXR[2] >= emp.PointXR[1] & ReinforcementBar[j].Kind == 2)
                        {
                            if (ReinforcementBar[j].PointYR[1] == MAXExtendS1)
                            {
                                MAXExtendS1 = ReinforcementBar[j].PointYR[1] + 0.3;
                            }
                        }
                    }
                    emp.PointYR[1] = MAXExtendS1;
                    emp.PointYR[2] = emp.PointYR[1];
                    emp.PointZR[1] = SpanHeight[i]-0.025;
                    emp.PointZR[2] = emp.PointZR[1];
                    emp.Kind = 2;
                    emp.Number = BarsSpDnNo[i, ik];
                    emp.Diameter = BarsSpDnD[i, ik];
                    ReinforcementBar[ReinforcementBarNumber] = emp;
                }
                BeamSum = BeamSum + BeamLength[i];
            }
        }
        private void AutoReinforcement1()
        {
            double MAXExtendS1 = MAXExtendS + 1;
            double[] MAXExtendS2 = new double[1000];
            //قضبان علوية
            for (int i = 1; i < ReinforcementBarNumber + 1; i++)
            {
                if (ReinforcementBar[i].Kind == 1)
                {
                    int n = 0;
                    for (int j = 1; j < i; j++)
                    {
                        if (ReinforcementBar[j].Kind == ReinforcementBar[i].Kind)
                        {
                            int theok = 0;
                            if (ReinforcementBar[i].PointXR[1] >= ReinforcementBar[j].PointXR[1] & ReinforcementBar[i].PointXR[1] <= ReinforcementBar[j].PointXR[2]) theok = 1;
                            if (ReinforcementBar[i].PointXR[2] >= ReinforcementBar[j].PointXR[1] & ReinforcementBar[i].PointXR[2] <= ReinforcementBar[j].PointXR[2]) theok = 1;
                            if (ReinforcementBar[j].PointXR[1] >= ReinforcementBar[i].PointXR[1] & ReinforcementBar[j].PointXR[1] <= ReinforcementBar[i].PointXR[2]) theok = 1;
                            if (ReinforcementBar[j].PointXR[2] >= ReinforcementBar[i].PointXR[1] & ReinforcementBar[j].PointXR[2] <= ReinforcementBar[i].PointXR[2]) theok = 1;
                            if (theok == 1 & ReinforcementBar[j].Kind == 1)
                            {
                                n = n + 1;
                                MAXExtendS2[n] = ReinforcementBar[j].PointYR[1];
                            }
                        }
                    }
                    MAXExtendS1 = MAXExtendS + 1 + 0.3;
                    int l = 0;
                startloop: { };
                    for (int k = 1; k < n + 1; k++)
                    {
                        if (MAXExtendS1 == MAXExtendS2[k])
                        {
                            MAXExtendS1 = MAXExtendS1 + 0.3;
                        }
                    }
                    l = l + 1;
                    if (l <= n) goto startloop;
                    ReinforcementBar[i].PointYR[1] = MAXExtendS1;
                    ReinforcementBar[i].PointYR[2] = ReinforcementBar[i].PointYR[1];
                }
            }
            double themaxy = -1000000;
            for (int i = 1; i < ReinforcementBarNumber + 1; i++)
            {
                if (ReinforcementBar[i].PointYR[1] > themaxy & ReinforcementBar[i].Kind == 1)
                {
                    themaxy = ReinforcementBar[i].PointYR[1];
                }
            }
            if (themaxy == -1000000) themaxy = MAXExtendS + 1;
            MAXExtendS2 = new double[100];
            //قضبان سفلية
            for (int i = 1; i < ReinforcementBarNumber + 1; i++)
            {
                if (ReinforcementBar[i].Kind == 2)
                {
                    int n = 0;
                    for (int j = 1; j < i; j++)
                    {
                        int theok = 0;
                        if (ReinforcementBar[i].PointXR[1] >= ReinforcementBar[j].PointXR[1] & ReinforcementBar[i].PointXR[1] <= ReinforcementBar[j].PointXR[2]) theok = 1;
                        if (ReinforcementBar[i].PointXR[2] >= ReinforcementBar[j].PointXR[1] & ReinforcementBar[i].PointXR[2] <= ReinforcementBar[j].PointXR[2]) theok = 1;
                        if (ReinforcementBar[j].PointXR[1] >= ReinforcementBar[i].PointXR[1] & ReinforcementBar[j].PointXR[1] <= ReinforcementBar[i].PointXR[2]) theok = 1;
                        if (ReinforcementBar[j].PointXR[2] >= ReinforcementBar[i].PointXR[1] & ReinforcementBar[j].PointXR[2] <= ReinforcementBar[i].PointXR[2]) theok = 1;
                        if (theok == 1 & ReinforcementBar[j].Kind == 2)
                        {
                            n = n + 1;
                            MAXExtendS2[n] = ReinforcementBar[j].PointYR[1];
                        }
                    }
                    MAXExtendS1 = themaxy + 1;
                    int l = 0;
                startloop1: { };
                    for (int k = 1; k < n + 1; k++)
                    {
                        if (MAXExtendS1 == MAXExtendS2[k])
                        {
                            MAXExtendS1 = MAXExtendS1 + 0.3;
                        }
                    }
                    l = l + 1;
                    if (l <= n) goto startloop1;
                    ReinforcementBar[i].PointYR[1] = MAXExtendS1;
                    ReinforcementBar[i].PointYR[2] = ReinforcementBar[i].PointYR[1];
                }
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
                x0 = Math.Round(x0, 0);
                y0 = Math.Round(y0, 0);
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
                x0 = Math.Round(x0, 0);
                y0 = Math.Round(y0, 0);
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
                x0 = Math.Round(x0, 0);
                y0 = Math.Round(y0, 0);
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
            INTERSECTION = THETAHKIK;
            TheX0 = x0;
            TheY0 = y0;
        }
        private void damg()
        {
            double[] thex1 = new double[1000];
            double[] thex2 = new double[1000];
            int[] IsSelected = new int[1000];
            int[] theDBar = new int[1000];
            int[] theNBar = new int[1000];
            int[] theKind = new int[1000];
            int selectedbar1 = 0;
            int selectedbar2 = 0;
            int thahkik = 0;
            int m = 0;
            for (int i = 1; i < ReinforcementBarNumber + 1; i++)
            {
                if (m > 0) goto endloop;
                if (ReinforcementBar[i].Selected == 1)
                {
                    if (IsSelected[i] == 0)
                    {
                        IsSelected[i] = 1;
                        m = m + 1;
                        selectedbar1 = i;
                        theDBar[m] = ReinforcementBar[i].Diameter;
                        theNBar[m] = ReinforcementBar[i].Number;
                        thex1[m] = ReinforcementBar[i].PointXR[1];
                        thex2[m] = ReinforcementBar[i].PointXR[2];
                        theKind[m] = ReinforcementBar[i].Kind;
                        for (int j = 1; j < ReinforcementBarNumber + 1; j++)
                        {
                            if (selectedbar2 != 0) goto endloop;
                            if (ReinforcementBar[j].Selected == 1 & IsSelected[j] == 0 & ReinforcementBar[j].Kind == theKind[m] & theDBar[m] == ReinforcementBar[j].Diameter)
                            {
                                IsSelected[j] = 1;
                                selectedbar2 = j;
                                thahkik = 1;
                                int theok = 0;
                                if (ReinforcementBar[j].PointXR[1] >= thex1[m] & ReinforcementBar[j].PointXR[2] <= thex2[m])
                                {
                                    theok = 1;
                                }
                                if (ReinforcementBar[j].PointXR[1] <= thex1[m] & ReinforcementBar[j].PointXR[2] >= thex2[m])
                                {
                                    theok = 1;
                                    thex1[m] = ReinforcementBar[j].PointXR[1];
                                    thex2[m] = ReinforcementBar[j].PointXR[2];
                                }
                                if (theok == 1)
                                {
                                    theNBar[m] = theNBar[m] + ReinforcementBar[j].Number;
                                }
                                else
                                {
                                    if (thex1[m] > ReinforcementBar[j].PointXR[1]) thex1[m] = ReinforcementBar[j].PointXR[1];
                                    if (thex2[m] < ReinforcementBar[j].PointXR[2]) thex2[m] = ReinforcementBar[j].PointXR[2];
                                    if (ReinforcementBar[j].Number > theNBar[m] & ReinforcementBar[j].Number != theNBar[m])
                                    {
                                        int number1 = ReinforcementBar[j].Number - theNBar[m];
                                        m = m + 1;
                                        thex1[m] = ReinforcementBar[j].PointXR[1];
                                        thex2[m] = ReinforcementBar[j].PointXR[2];
                                        theNBar[m] = number1;
                                        theDBar[m] = ReinforcementBar[j].Diameter;
                                        theKind[m] = ReinforcementBar[j].Kind;
                                    }
                                    if (ReinforcementBar[j].Number < theNBar[m] & ReinforcementBar[j].Number != theNBar[m])
                                    {
                                        int number1 = theNBar[m] - ReinforcementBar[j].Number;
                                        theNBar[m] = ReinforcementBar[j].Number;
                                        m = m + 1;
                                        thex1[m] = ReinforcementBar[i].PointXR[1];
                                        thex2[m] = ReinforcementBar[i].PointXR[2];
                                        theNBar[m] = number1;
                                        theDBar[m] = ReinforcementBar[j].Diameter;
                                        theKind[m] = ReinforcementBar[j].Kind;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        endloop: { };

            if (thahkik == 1)
            {
                for (int i = 1; i < ReinforcementBarNumber + 1; i++)
                {
                    ReinforcementBar[i].Selected = 0;
                }

                for (int i = 1; i < ReinforcementBarNumber + 1; i++)
                {
                    if (i == selectedbar1 || i == selectedbar2)
                    {
                        ReinforcementBar[i].Selected = 1;
                    }
                }

                //الحذف
                int n = 0;
            startloop: { };
                for (int i = 1; i < ReinforcementBarNumber + 1; i++)
                {
                    if (ReinforcementBar[i].Selected == 1)
                    {
                        ReinforcementBarNumber = ReinforcementBarNumber - 1;
                        for (int j = i; j < ReinforcementBarNumber + 1; j++)
                        {
                            ReinforcementBar[j] = ReinforcementBar[j + 1];
                        }
                        break;
                    }
                }
                n = n + 1;
                if (n <= ReinforcementBarNumber) goto startloop;

                //الاضافة
                for (int i = 1; i < m + 1; i++)
                {
                    ReinforcementBarNumber = ReinforcementBarNumber + 1;
                    ReinforcementBars emp = new ReinforcementBars();
                    emp.PointNumbers = 2;
                    emp.PointXR[1] = thex1[i];
                    emp.PointXR[2] = thex2[i];
                    emp.Kind = theKind[i];
                    emp.Number = theNBar[i];
                    emp.Diameter = theDBar[i];
                    ReinforcementBar[ReinforcementBarNumber] = emp;
                }
                AutoReinforcement1();
                Render2d();
                MakeTempFiles();
            }
        }
        #endregion
        #region//bottons
        private void button5_Click(object sender, EventArgs e)
        {
            Myglobals.FrameDesignerIsOpen = 2;
            FramePropertiesFrm framePropertiesFrm = new FramePropertiesFrm();
            framePropertiesFrm.ShowDialog();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            AutoReinforcement();
            Render2d();
            MakeTempFiles();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            int hala = 0;
            hala = comboBox1.SelectedIndex+1;
            for (int i = 1; i < ReinforcementBarNumber + 1; i++)
            {
                if (ReinforcementBar[i].Selected == 1)
                {
                    if (hala == 1)
                    {
                        ReinforcementBar[i].PointXR[1] = ReinforcementBar[i].PointXR[1] -0.1;
                        ReinforcementBar[i].PointXR[2] = ReinforcementBar[i].PointXR[2] + 0.1;
                    }
                    if (hala == 2)
                    {
                        ReinforcementBar[i].PointXR[1] = ReinforcementBar[i].PointXR[1] - 0.1;
                    }
                    if (hala == 3)
                    {
                        ReinforcementBar[i].PointXR[2] = ReinforcementBar[i].PointXR[2] + 0.1;
                    }
                }
            }
            AutoReinforcement1();
            Render2d();
            MakeTempFiles();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            int hala = 0;
            hala = comboBox1.SelectedIndex + 1;
            for (int i = 1; i < ReinforcementBarNumber + 1; i++)
            {
                if (ReinforcementBar[i].Selected == 1)
                {
                    if (hala == 1)
                    {
                        ReinforcementBar[i].PointXR[1] = ReinforcementBar[i].PointXR[1] + 0.1;
                        ReinforcementBar[i].PointXR[2] = ReinforcementBar[i].PointXR[2] - 0.1;
                    }
                    if (hala == 2)
                    {
                        ReinforcementBar[i].PointXR[1] = ReinforcementBar[i].PointXR[1] + 0.1;
                    }
                    if (hala == 3)
                    {
                        ReinforcementBar[i].PointXR[2] = ReinforcementBar[i].PointXR[2] - 0.1;
                    }
                }
            }
            AutoReinforcement1();
            Render2d();
            MakeTempFiles();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < ReinforcementBarNumber + 1; i++)
            {
                if (ReinforcementBar[i].Selected == 1)
                {
                        ReinforcementBar[i].PointXR[1] = ReinforcementBar[i].PointXR[1] + 0.1;
                        ReinforcementBar[i].PointXR[2] = ReinforcementBar[i].PointXR[2] + 0.1;
                }
            }
            AutoReinforcement1();
            Render2d();
            MakeTempFiles();
        }
        private void button10_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < ReinforcementBarNumber + 1; i++)
            {
                if (ReinforcementBar[i].Selected == 1)
                {
                    ReinforcementBar[i].PointXR[1] = ReinforcementBar[i].PointXR[1] - 0.1;
                    ReinforcementBar[i].PointXR[2] = ReinforcementBar[i].PointXR[2] - 0.1;
                }
            }
            AutoReinforcement1();
            Render2d();
            MakeTempFiles();
        }
        private void button11_Click(object sender, EventArgs e)
        {
            int ReinforcementBarNumber1 = ReinforcementBarNumber;
            for (int i = 1; i < ReinforcementBarNumber1 + 1; i++)
            {
                if (ReinforcementBar[i].Selected == 1)
                {
                    ReinforcementBarNumber = ReinforcementBarNumber + 1;
                    ReinforcementBars emp = new ReinforcementBars();
                    emp.PointNumbers = 2;
                    emp.PointXR[1] = ReinforcementBar[i].PointXR[1];
                    emp.PointXR[2] = ReinforcementBar[i].PointXR[2];
                    emp.Kind = ReinforcementBar[i].Kind;
                    double klk = Convert.ToDouble (ReinforcementBar[i].Number) / 2;
                    emp.Number = Convert.ToInt32(Math.Ceiling(klk));
                    emp.Diameter = ReinforcementBar[i].Diameter;
                    ReinforcementBar[i].Number = emp.Number;
                    ReinforcementBar[ReinforcementBarNumber] = emp;
                }
            }
            AutoReinforcement1();
            Render2d();
            MakeTempFiles();
        }
        private void button12_Click(object sender, EventArgs e)
        {
            damg();
        }
        private void button13_Click(object sender, EventArgs e)
        {
            int n = 0;
            int ReinforcementBarNumber1 = ReinforcementBarNumber;
        startloop: { };
            for (int i = 1; i < ReinforcementBarNumber + 1; i++)
            {
                if (ReinforcementBar[i].Selected == 1)
                {
                    ReinforcementBarNumber = ReinforcementBarNumber - 1;
                    for (int j = i; j < ReinforcementBarNumber + 1; j++)
                    {
                        ReinforcementBar[j] = ReinforcementBar[j + 1];
                    }
                    break;
                }
            }
            n = n + 1;
            if (n <= ReinforcementBarNumber1) goto startloop;
            AutoReinforcement1();
            for (int i = 1; i < Spans + 1; i++)
            {
                for (int j = 1; j < 3 + 1; j++)
                {
                    if (SpanShearDisSelected[i, j] == 1)
                    {
                        if (j == 1) ShearBarsSuRGR[i] = 0;
                        if (j == 2) ShearBarsSpGR[i] = 0;
                        if (j == 3) ShearBarsSuLGR[i + 1] = 0;
                        SpanShearDisSelected[i, j] = 0;
                    }
                }
            }
            Render2d();
            MakeTempFiles();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            int ReinforcementBarNumber1 = ReinforcementBarNumber;
            for (int i = 1; i < ReinforcementBarNumber1 + 1; i++)
            {
                if (ReinforcementBar[i].Selected == 1)
                {
                    if (ReinforcementBar[i].PointXR[1] < RedLineXR & ReinforcementBar[i].PointXR[2] > RedLineXR)
                    {
                        double val = ReinforcementBar[i].PointXR[2];
                        ReinforcementBar[i].PointXR[1] = ReinforcementBar[i].PointXR[1];
                        ReinforcementBar[i].PointXR[2] = RedLineXR + 0.4;
                        ReinforcementBarNumber = ReinforcementBarNumber + 1;
                        ReinforcementBars emp = new ReinforcementBars();
                        emp.PointNumbers = 2;
                        emp.PointXR[1] = RedLineXR - 0.4;
                        emp.PointXR[2] = val;
                        emp.Kind = ReinforcementBar[i].Kind;
                        emp.Number = ReinforcementBar[i].Number;
                        emp.Diameter = ReinforcementBar[i].Diameter;
                        ReinforcementBar[ReinforcementBarNumber] = emp;
                    }
                }
            }
            for (int i = 1; i < ReinforcementBarNumber1 + 1; i++)
            {
                ReinforcementBar[i].Selected = 0;
            }
            AutoReinforcement1();
            Render2d();
            MakeTempFiles();
        }
        #endregion
        #region//toolStripButton
        private void toolStripButton32_Click(object sender, EventArgs e)
        {
            Zoom2d = Math.Round(Zoom2d + 6, 2);
            startX2d = lastzoomX2d - (int)(lastzoomX2dR * Zoom2d);
            startY2d = lastzoomY2d + (int)(lastzoomY2dR * Zoom2d);
            TempX = lastzoomX2d;
            TempY = lastzoomY2d;
            Render2d();
        }
        private void toolStripButton33_Click(object sender, EventArgs e)
        {
            if (Math.Round(Zoom2d - 6, 2) > 1)
            {
                Zoom2d = Math.Round(Zoom2d - 6, 2);
                startX2d = lastzoomX2d - (int)(lastzoomX2dR * Zoom2d);
                startY2d = lastzoomY2d + (int)(lastzoomY2dR * Zoom2d);
                TempX = lastzoomX2d;
                TempY = lastzoomY2d;
                Render2d();
            }
        }
        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < ReinforcementBarNumber + 1; i++)
            {
                ReinforcementBar[i].Selected = 1;
            }
            Render2d();
        }
        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < ReinforcementBarNumber + 1; i++)
            {
                ReinforcementBar[i].Selected = 0;
            }
            Render2d();
        }
        private void toolStripButton24_Click(object sender, EventArgs e)
        {
            if (TempSelectedFile - 1 > 0)
            {
                TempSelectedFile = TempSelectedFile - 1;
                OpenTemp();
                Render2d();
            }
        }
        private void toolStripButton25_Click(object sender, EventArgs e)
        {
            if (TempSelectedFile + 1 <= TempFile)
            {
                TempSelectedFile = TempSelectedFile + 1;
                OpenTemp();
                Render2d();
            }
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            OpenReal();
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            SaveReal();
        }
        #endregion
    }
}
