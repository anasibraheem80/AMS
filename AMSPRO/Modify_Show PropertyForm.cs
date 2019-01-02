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
    public partial class Modify_Show_PropertyForm : Form
    {
        public Modify_Show_PropertyForm()
        {
            InitializeComponent();
        }
        Form framePropertiesFrm = Application.OpenForms["FramePropertiesFrm"];
        private void Modify_Show_PropertyForm_Load(object sender, EventArgs e)
        {
            for (int j = 1; j < Material.Number + 1; j++)
            {
                comboBox2.Items.Add(Material.Name[j]);
            }
            comboBox2.SelectedIndex = 0;
            int i = Section.Selected;
            MODELtxt.Text = Section.MODELd[i];
            DESCRIPTIONtxt.Text = Section.DESCRIPTIONd[i];
            LENGTH_UNITStxt.Text = Section.LENGTH_UNITSd[i];
            LABELtxt.Text = Section.LABELd[i];
            DESIGNATIONtxt.Text = Section.DESIGNATIONd[i];
            Dtxt.Text = Section.Dd[i].ToString();
            BFtxt.Text = Section.BFd[i].ToString();
            TFtxt.Text = Section.TFd[i].ToString();
            BFBtxt.Text = Section.BFBd[i].ToString();
            TFBtxt.Text = Section.TFBd[i].ToString();
            TWtxt.Text = Section.TWd[i].ToString();
            Atxt.Text = Section.Ad[i].ToString();
            I33txt.Text = Section.I33d[i].ToString();
            Z33txt.Text = Section.Z33d[i].ToString();
            AS3txt.Text = Section.AS3d[i].ToString();
            I22txt.Text = Section.I22d[i].ToString();
            Z22txt.Text = Section.Z22d[i].ToString();
            AS2txt.Text = Section.AS2d[i].ToString();
            Jtxt.Text = Section.Jd[i].ToString();
            Xtxt.Text = Section.Xd[i].ToString();
            Ytxt.Text = Section.Yd[i].ToString();
            S33POStxt.Text = Section.S33POSd[i].ToString();
            S33NEGtxt.Text = Section.S33NEGd[i].ToString();
            S22POStxt.Text = Section.S22POSd[i].ToString();
            S22NEGtxt.Text = Section.S22NEGd[i].ToString();
            R33txt.Text = Section.R33d[i].ToString();
            R22txt.Text = Section.R22d[i].ToString();
            Btxt.Text = Section.Bd[i].ToString();
            HTtxt.Text = Section.HTd[i].ToString();
            ODtxt.Text = Section.ODd[i].ToString();
            TDEStxt.Text = Section.TDESd[i].ToString();
            if (Section.Materiald[i] > 0) comboBox2.SelectedIndex = Section.Materiald[i] - 1;
            if (Section.DESIGNATIONd[i] == "L")
            {
                comboBox1.SelectedIndex = 3;
            }

            if (Section.DESIGNATIONd[i] == "W" || Section.DESIGNATIONd[i] == "I")
            {
                comboBox1.SelectedIndex = 0;
            }

            if (Section.DESIGNATIONd[i] == "R")
            {
                comboBox1.SelectedIndex = 14;
            }
            if (Section.DESIGNATIONd[i] == "B")
            {
                comboBox1.SelectedIndex = 6;
            }
            if (Section.DESIGNATIONd[i] == "CR")
            {
                comboBox1.SelectedIndex = 15;
            }
            if (Myglobals.EditOrNew == 1)
            {
                comboBox1.SelectedIndex = 14;
                DESCRIPTIONtxt.Text = "User Defined";
                LABELtxt.Text = "Conc";
                Dtxt.Text = "80";
                Btxt.Text = "50";
                i = Section.Numberd + 1;
                Section.Selected = i;
                Section.ModifiersAread[i] = 1;
                Section.ModifiersShear2d[i] = 1;
                Section.ModifiersShear3d[i] = 1;
                Section.ModifiersTorsionald[i] = 1;
                Section.ModifiersI2d[i] = 1;
                Section.ModifiersI3d[i] = 1;
                Section.ModifiersMassd[i] = 1;
                Section.ModifiersWeightd[i] = 1;
                Section.DesignTyped[i] = 2;
                Section.RebarMaterial1d[i] = 1;
                Section.RebarMaterial2d[i] = 1;
                Section.CoverTopd[i] = 60;
                Section.CoverBottomd[i] = 60;
                Section.ReinTopId[i] = 300;
                Section.ReinTopJd[i] = 300;
                Section.ReinBottomId[i] = 300;
                Section.ReinBottomJd[i] = 300;

            }
            if (Myglobals.EditOrNew == 3)
            {
                int j = Section.Selected;
                i = Section.Numberd + 1;
                Section.Selected = i;
                Section.ModifiersAread[i] = Section.ModifiersAread[j];
                Section.ModifiersShear2d[i] = Section.ModifiersShear2d[j];
                Section.ModifiersShear3d[i] = Section.ModifiersShear3d[j];
                Section.ModifiersTorsionald[i] = Section.ModifiersTorsionald[j];
                Section.ModifiersI2d[i] = Section.ModifiersI2d[j];
                Section.ModifiersI3d[i] = Section.ModifiersI3d[j];
                Section.ModifiersMassd[i] = Section.ModifiersMassd[j];
                Section.ModifiersWeightd[i] = Section.ModifiersWeightd[j];

                Section.DesignTyped[i] = Section.DesignTyped[j];
                Section.RebarMaterial1d[i] = Section.RebarMaterial1d[j];
                Section.RebarMaterial2d[i] = Section.RebarMaterial2d[j];
                Section.CoverTopd[i] = Section.CoverTopd[j];
                Section.CoverBottomd[i] = Section.CoverBottomd[j];
                Section.ReinTopId[i] = Section.ReinTopId[j];
                Section.ReinTopJd[i] = Section.ReinTopJd[j];
                Section.ReinBottomId[i] = Section.ReinBottomId[j];
                Section.ReinBottomJd[i] = Section.ReinBottomJd[j];
            }
            DrawSection();
        }
        private void DrawSection()
        {
            Bitmap finalBmp = new Bitmap(300, 300);
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
            pictureBox1.Image = finalBmp;
            int startX = 125;
            int startY = 225;
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            Pen pen = new Pen(Color.Black, 1f);
            try
            {
                #region//مقطع L
                if (DESIGNATIONtxt.Text == "L")
                {
                    double D = Convert.ToDouble(Dtxt.Text);
                    if (D > 0)
                    {
                        Point[] P = new Point[8];
                        double B = Convert.ToDouble(Btxt.Text);
                        double TF = Convert.ToDouble(TFtxt.Text);
                        double TW = Convert.ToDouble(TWtxt.Text);

                        double MaxW = D;
                        if (B > MaxW) MaxW = B;
                        double thezoom = Convert.ToDouble(200 / MaxW);
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

                        double X = Convert.ToDouble(Xtxt.Text);
                        double Y = Convert.ToDouble(Ytxt.Text);
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
                if (DESIGNATIONtxt.Text == "I" || DESIGNATIONtxt.Text == "W")
                {
                    double D = Convert.ToDouble(Dtxt.Text);
                    if (D > 0)
                    {
                        Point[] P = new Point[14];
                        double BF = Convert.ToDouble(BFtxt.Text);
                        double TF = Convert.ToDouble(TFtxt.Text);
                        double BFB = Convert.ToDouble(BFBtxt.Text);
                        double TFB = Convert.ToDouble(TFBtxt.Text);
                        double TW = Convert.ToDouble(TWtxt.Text);

                        double MaxW = D;
                        if (BF > MaxW) MaxW = BF;
                        if (BFB > MaxW) MaxW = BFB;
                        double thezoom = Convert.ToDouble(200 / MaxW);
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
                        double X = Convert.ToDouble(Xtxt.Text);
                        double Y = Convert.ToDouble(Ytxt.Text);
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
                if (DESIGNATIONtxt.Text == "T")
                {
                    double D = Convert.ToDouble(Dtxt.Text);
                    if (D > 0)
                    {
                        Point[] P = new Point[10];
                        double BF = Convert.ToDouble(BFtxt.Text);
                        double TF = Convert.ToDouble(TFtxt.Text);
                        double TW = Convert.ToDouble(TWtxt.Text);
                        double Y = Convert.ToDouble(Ytxt.Text);

                        double MaxW = D;
                        if (BF > MaxW) MaxW = BF;
                        double thezoom = Convert.ToDouble(200 / MaxW);
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
                if (DESIGNATIONtxt.Text == "R")
                {
                    double D = Convert.ToDouble(Dtxt.Text);
                    double HT = Convert.ToDouble(HTtxt.Text);
                    if (D == 0) D = HT;
                    if (D > 0)
                    {
                        Point[] P = new Point[6];
                        double B = Convert.ToDouble(Btxt.Text);
                        double TF = Convert.ToDouble(TFtxt.Text);
                        double TW = Convert.ToDouble(TWtxt.Text);
                        double MaxW = D;
                        if (B > MaxW) MaxW = B;
                        double thezoom = Convert.ToDouble(200 / MaxW);
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
                        double X = Convert.ToDouble(Xtxt.Text);
                        double Y = Convert.ToDouble(Ytxt.Text);
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
                if (DESIGNATIONtxt.Text == "C")
                {
                    double D = Convert.ToDouble(Dtxt.Text);
                    if (D > 0)
                    {
                        Point[] P = new Point[10];
                        double BF = Convert.ToDouble(BFtxt.Text);
                        double TF = Convert.ToDouble(TFtxt.Text);
                        double TW = Convert.ToDouble(TWtxt.Text);

                        double MaxW = D;
                        if (BF > MaxW) MaxW = BF;
                        double thezoom = Convert.ToDouble(200 / MaxW);
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
                        double X = Convert.ToDouble(Xtxt.Text);
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
                if (DESIGNATIONtxt.Text == "B")
                {
                    double D = Convert.ToDouble(Dtxt.Text);
                    double HT = Convert.ToDouble(HTtxt.Text);
                    if (D == 0) D = HT;
                    if (D > 0)
                    {
                        Point[] P = new Point[6];
                        double B = Convert.ToDouble(Btxt.Text);
                        double TF = Convert.ToDouble(TFtxt.Text);
                        double TW = Convert.ToDouble(TWtxt.Text);

                        double MaxW = D;
                        if (B > MaxW) MaxW = B;
                        double thezoom = Convert.ToDouble(200 / MaxW);
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
                        double X = Convert.ToDouble(Xtxt.Text);
                        double Y = Convert.ToDouble(Ytxt.Text);
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
                if (DESIGNATIONtxt.Text == "CR")
                {
                    startX = 125;
                    startY = 125;
                    double D = Convert.ToDouble(Dtxt.Text);
                    if (D > 0)
                    {
                        Point[] P = new Point[25];

                        double MaxW = D;
                        double alfa = Math.PI / 12;
                        double thezoom = Convert.ToDouble(200 / MaxW);
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

                        double X = Convert.ToDouble(Xtxt.Text);
                        double Y = Convert.ToDouble(Ytxt.Text);
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

            catch { };
        
        }
        private void CalcSection()
        {
            double D = 0, BF = 0, TF = 0, BFB = 0, TFB = 0, TW = 0, B = 0, HT = 0;
            double A = 0, I33 = 0, Z33 = 0, AS3 = 0, I22 = 0, Z22 = 0, AS2 = 0, J = 0, X = 0, Y = 0;
            double S33POS = 0, S33NEG = 0, S22POS = 0, S22NEG = 0, R33 = 0, R22 = 0, CW = 0, OD = 0, TDES = 0;
           
            
            double A1 = 0, A2 = 0, A3 = 0;
            double X1 = 0, X2 = 0, X3 = 0;
            double Y1 = 0, Y2 = 0, Y3 = 0;
            double I331 = 0, I332 = 0, I333 = 0;
            double I221 = 0, I222 = 0, I223 = 0;
            D = Convert.ToDouble(Dtxt.Text);
            BF = Convert.ToDouble(BFtxt.Text);
            TF = Convert.ToDouble(TFtxt.Text);
            BFB = Convert.ToDouble(BFBtxt.Text);
            TFB = Convert.ToDouble(TFBtxt.Text);
            TW = Convert.ToDouble(TWtxt.Text);
            B = Convert.ToDouble(Btxt.Text);
            HT = Convert.ToDouble(HTtxt.Text);

            #region//مقطع L
            if (DESIGNATIONtxt.Text == "L")
            {

            }
            #endregion
            #region//مقطع I
            if (DESIGNATIONtxt.Text == "I" || DESIGNATIONtxt.Text == "W")
            {
                A1 = BF * TF;
                A2 = (D - (TF + TFB)) * TW;
                A3 = BFB * TFB;
                A = A1 + A2 + A3;
                Y1 = D - (TF / 2);
                Y2 = (D - (TF + TFB)) / 2 + TFB;
                Y3 = TFB / 2;
                Y = (Y1 * A1 + Y2 * A2 + Y3 * A3) / A;
                X = 0;

                I331 = (Math.Pow(TF, 3) * BF) / 12 + A1 * Math.Pow((Y1 - Y), 2);
                I332 = (Math.Pow((D - (TF + TFB)), 3) * TW) / 12 + A2 * Math.Pow((Y2 - Y), 2);
                I333 = (Math.Pow(TFB, 3) * BFB) / 12 + A3 * Math.Pow((Y3 - Y), 2);
                I33 = I331 + I332 + I333;
                I221 = (Math.Pow(BF, 3) * TF) / 12;
                I222 = (Math.Pow(TW, 3) * (D - (TF + TFB))) / 12;
                I223 = (Math.Pow(BFB, 3) * TFB) / 12;
                I22 = I221 + I222 + I223;


                //The shear area for forces in the section local axis direction.
                double val1 = 0;
                double val2 = 0;
                double valZ = 0;
                double BB1 = 0, BB2 = 0, BB3 = 0, BB4 = 0;
                double FST = 0;
                double SCT = 0;
                double S21 = 0, S22 = 0, S23 = 0, S24 = 0;
                double AM1 = 0, AM2 = 0, AM3 = 0, AM4 = 0;
                val1 = D - Y - TF;
                val2 = D - Y;
                BB1 = BF;
                valZ = val1;
                FST = (Math.Pow(valZ, 5) / 5) - (2 * Math.Pow(val1, 2) * Math.Pow(valZ, 3) / 3) + (Math.Pow(val1, 4) * valZ);
                valZ = val2;
                SCT = (Math.Pow(valZ, 5) / 5) - (2 * Math.Pow(val1, 2) * Math.Pow(valZ, 3) / 3) + (Math.Pow(val1, 4) * valZ);
                if ((SCT - FST) != 0)
                {
                    S21 = ((Math.Pow(BB1, 2) / 4) * (SCT - FST));
                }
                val1 = -Y + TFB;
                val2 = D - Y - TF;
                BB2 = TW;
                valZ = val1;
                FST = (Math.Pow(valZ, 5) / 5) - (2 * Math.Pow(val1, 2) * Math.Pow(valZ, 3) / 3) + (Math.Pow(val1, 4) * valZ);
                valZ = val2;
                SCT = (Math.Pow(valZ, 5) / 5) - (2 * Math.Pow(val1, 2) * Math.Pow(valZ, 3) / 3) + (Math.Pow(val1, 4) * valZ);
                if ((SCT - FST) != 0)
                {
                    S22 = ((Math.Pow(BB2, 2) / 4) * (SCT - FST));
                }
                val1 = -Y;
                val2 = -Y + TFB;
                BB3 = BFB;
                valZ = val1;
                FST = (Math.Pow(valZ, 5) / 5) - (2 * Math.Pow(val1, 2) * Math.Pow(valZ, 3) / 3) + (Math.Pow(val1, 4) * valZ);
                valZ = val2;
                SCT = (Math.Pow(valZ, 5) / 5) - (2 * Math.Pow(val1, 2) * Math.Pow(valZ, 3) / 3) + (Math.Pow(val1, 4) * valZ);
                if ((SCT - FST) != 0)
                {
                    S23 = ((Math.Pow(BB3, 2) / 4) * (SCT - FST));
                }
                AM1 = Math.Pow(I331, 2) / (S21 / BB1);
                AM2 = Math.Pow(I332, 2) / (S22 / BB2);
                AM3 = Math.Pow(I333, 2) / (S23 / BB3);
                AS3 = AM1 + AM2 + AM3;
                // AS3 = Math.Pow(I33, 2)/((S21 / BB1) + (S22 / BB2) + (S23 / BB3));
                AS3 = A2;
                AS2 = (A1 + A3);//لم تحل

                //The section modulus for bending about the local axis
                S33POS = (Math.Pow(TF, 2) * BF) / 6 + A1 * (Y1 - Y);
                S33POS = S33POS + (Math.Pow((D - (TF + TFB)), 2) * TW) / 6 + A2 * (Y2 - Y);
                S33POS = S33POS + (Math.Pow(TFB, 2) * BFB) / 6 + A3 * (Y3 - Y);
                S33NEG = (Math.Pow(D, 2) * B) / 6;
                S22POS = (Math.Pow(B, 2) * D) / 6;
                S22NEG = (Math.Pow(B, 2) * D) / 6;

                //Radius of gyration
                R33 = Math.Sqrt(I33 / A);
                R22 = Math.Sqrt(I22 / A);
                double alfa = 1 / (1 + (Math.Pow(BF / BFB, 3) * (TF / TFB)));
                double D1 = D - TF / 2 - TFB / 2;
                double D2 = D - TF- TFB;
                //The plastic modulus for bending about the local axis
                //Z33 = BF * TF * (Y1 - Y) + BFB * TFB * (Y - Y3) + (Math.Pow(D2, 2) * TW) / 4;
                Z33 = BF * TF * (D - Y-(TF/2)) + BFB * TFB * (Y - (TFB/2)) + (TW * Math.Pow((Y - TFB), 2) / 2) + (TW * Math.Pow((D - TF-Y), 2) / 2);
                //غلط
                
                Z22 = (Math.Pow(BF, 2) * TF) / 4 + (Math.Pow(BFB, 2) * TFB) / 4 + (Math.Pow(TW, 2) * D2) / 4;
                //The torsional constant
                J = ((BF * Math.Pow(TF, 3)) + (BFB * Math.Pow(TFB, 3)) + (D1 * Math.Pow(TW, 3))) / 3;
                CW = Math.Pow(D1, 2) * Math.Pow(BF, 3)*TF*alfa/12;
            }
            #endregion
            #region//مقطع T
            if (DESIGNATIONtxt.Text == "T")
            {
            }
            #endregion
            #region//مقطع مستطيل
            if (DESIGNATIONtxt.Text == "R")
            {
                A = D * B;
                I33 = (Math.Pow(D, 3) * B) / 12;
                I22 = (Math.Pow(B, 3) * D) / 12;
                X = B / 2;
                Y = D / 2;
                //The shear area for forces in the section local axis direction.
                AS3 = A * 5 / 6;
                AS2 = A * 5 / 6;
                //The section modulus for bending about the local axis
                S33POS = (Math.Pow(D, 2) * B) / 6;
                S33NEG = (Math.Pow(D, 2) * B) / 6;
                S22POS = (Math.Pow(B, 2) * D) / 6;
                S22NEG = (Math.Pow(B, 2) * D) / 6;
                //Radius of gyration
                R33 = Math.Sqrt((Math.Pow(D / 2, 2)) / 3);
                R22 = Math.Sqrt((Math.Pow(B / 2, 2)) / 3);
                //The plastic modulus for bending about the local axis
                Z33 = (Math.Pow(D, 2) * B) / 4;
                Z22 = (Math.Pow(B, 2) * D) / 4;
                //The torsional constant
                double D4 = Math.Pow(D, 4);
                double B4 = Math.Pow(B, 4);
                double D3 = Math.Pow(D, 3);
                double B3 = Math.Pow(B, 3);
                double K1 = B4 / (12 * D4);
                double K2 = D4 / (12 * B4);
                double K13 = ((double)1 / 3);
                if (D >= B)
                {
                    J = D * B3 * (K13 - 0.21 * (B / D) * (1 - K1));
                }
                else
                {
                    J = B * D3 * (K13 - 0.21 * (D / B) * (1 - K2));
                }
                //=A1*A2^3*(1/3-0.21*(A2/A1*(1-(A2^4/(12*A1^4)))))
                //OD = 0;
                //TDES = 0;
            }
            #endregion
            #region//مقطع C
            if (DESIGNATIONtxt.Text == "C")
            {

            }
            #endregion
            #region//مقطع بوكس
            if (DESIGNATIONtxt.Text == "B")
            {

            }
            #endregion
            #region//مقطع دائرة
            if (DESIGNATIONtxt.Text == "CR")
            {
                A = Math.PI * Math.Pow(D, 2) / 4;
                I33 = Math.PI * Math.Pow(D, 4) / 64;
                I22 = Math.PI * Math.Pow(D, 4) / 64;
                X = D / 2;
                Y = D / 2;
                //The shear area for forces in the section local axis direction.
                AS3 = A * 9 / 10;
                AS2 = A * 9 / 10;
                //The section modulus for bending about the local axis
                S33POS = Math.PI * (Math.Pow(D, 3)) / 32;
                S33NEG = Math.PI * (Math.Pow(D, 3)) / 32;
                S22POS = Math.PI * (Math.Pow(D, 3)) / 32;
                S22NEG = Math.PI * (Math.Pow(D, 3)) / 32;
                //Radius of gyration
                R33 = D / 4;
                R22 = D / 4;
                //The plastic modulus for bending about the local axis
                Z33 = 4 * (Math.Pow(D / 2, 3)) / 3;
                Z22 = 4 * (Math.Pow(D / 2, 3)) / 3;
                //The torsional constant
                J = Math.PI * (Math.Pow(D, 4)) / 32;
            }
            #endregion
            Atxt.Text = Math.Round(A, 8).ToString();
            I33txt.Text = Math.Round(I33, 8).ToString();
            Z33txt.Text = Math.Round(Z33, 8).ToString();
            AS3txt.Text = Math.Round(AS3, 8).ToString();
            I22txt.Text = Math.Round(I22, 8).ToString();
            Z22txt.Text = Math.Round(Z22, 8).ToString();
            AS2txt.Text = Math.Round(AS2, 8).ToString();
            Jtxt.Text = Math.Round(J, 8).ToString();
            Xtxt.Text = Math.Round(X, 8).ToString();
            Ytxt.Text = Math.Round(Y, 8).ToString();
            S33POStxt.Text = Math.Round(S33POS, 8).ToString();
            S33NEGtxt.Text = Math.Round(S33NEG, 8).ToString();
            S22POStxt.Text = Math.Round(S22POS, 8).ToString();
            S22NEGtxt.Text = Math.Round(S22NEG, 8).ToString();
            R33txt.Text = Math.Round(R33, 8).ToString();
            R22txt.Text = Math.Round(R22, 8).ToString();
            //ODtxt.Text = Math.Round(OD, 3).ToString();
            //TDEStxt.Text = Math.Round(TDES, 3).ToString();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
/*
Steel I/Wide Flang  0
Steel Channel  1
Steel Tee  2
Steel Angel  3
Steel Double Angel  4
Steel Double Channel  5
Steel Tube  6
Steel Pipe  7
Filled Steel Tube  8
Filled Steel Pipe  9
BU I Cover Plate  10
Joist Section  11
Steel Plate  12
Steel Rod  13
Concrete Rectangular  14
Concrete Circle  15 
Concrete Encasement Rectangular  16
Concrete Encasement Circle  17
Precast I  18
Concrete Tee  19
Concrete L  20
Concrete Cross  21
Concrete Box  22
Concrete Pipe  23
Cold Forrmed C  24
Cold Forrmed Z  25
Cold Forrmed Hat  26
Buckling Restrained Brace  27
General  28
*/
            if (comboBox1.SelectedIndex == 0)//Steel I/Wide Flang  0
            {
                DESIGNATIONtxt.Text = "W";
                Dtxt.Visible = true;
                Btxt.Visible = false;
                BFtxt.Visible = true;
                BFBtxt.Visible = true;
                TFtxt.Visible = true;
                TFBtxt.Visible = true;
                TWtxt.Visible = true;
                HTtxt.Visible = false;

                Dlbl.Visible = true;
                Blbl.Visible = false;
                BFlbl.Visible = true;
                BFBlbl.Visible = true;
                TFlbl.Visible = true;
                TFBlbl.Visible = true;
                TWlbl.Visible = true;
                HTlbl.Visible = false;

                Dlbl.Text = "Total Depth";
                BFlbl.Text = "Top Flange Width";
                BFBlbl.Text = "Bottom Flange Width";
                TFlbl.Text = "Top Flange Thickness";
                TFBlbl.Text = "Bottom Flange Thickness";
                TWlbl.Text = "Web Thickness";
            }


            if (comboBox1.SelectedIndex == 3)//Steel Angel  3
            {
                DESIGNATIONtxt.Text = "L";
                Dtxt.Visible = true;
                BFlbl.Visible = true;
                BFtxt.Visible = false;
                BFBtxt.Visible = false;
                TFtxt.Visible = true;
                TFBtxt.Visible = false;
                TWtxt.Visible = true;
                HTtxt.Visible = false;

                Dlbl.Visible = true;
                BFlbl.Visible = true;
                BFlbl.Visible = false;
                BFBlbl.Visible = false;
                TFlbl.Visible = true;
                TFBlbl.Visible = false;
                TWlbl.Visible = true;
                HTlbl.Visible = false;

                Dlbl.Text = "Total Depth";
                Blbl.Text = "Top Width";
                TFlbl.Text = "Horizontal Leg Thickness";
                TWlbl.Text = "Vertical Leg Thickness";
            }

            if (comboBox1.SelectedIndex == 6)//Steel Tube  6
            {
                DESIGNATIONtxt.Text = "B";
                Dtxt.Visible = false;
                Btxt.Visible = true;
                BFtxt.Visible = false;
                BFBtxt.Visible = false;
                TFtxt.Visible = true;
                TFBtxt.Visible = false;
                TWtxt.Visible = true;
                HTtxt.Visible = true;

                Dlbl.Visible = false;
                Blbl.Visible = true;
                BFlbl.Visible = false;
                BFBlbl.Visible = false;
                TFlbl.Visible = true;
                TFBlbl.Visible = false;
                TWlbl.Visible = true;
                HTlbl.Visible = true;
                HTlbl.Text = "Depth";
                Blbl.Text = "Width";
                TFlbl.Text = "Flang Thickness";
                TWlbl.Text = "Web Thickness";
            }
            if (comboBox1.SelectedIndex == 14)// Concrete Rectangular 
            {
                DESIGNATIONtxt.Text = "R";
                Dtxt.Visible = true;
                Btxt.Visible = true;
                BFtxt.Visible = false;
                BFBtxt.Visible = false;
                TFtxt.Visible = false;
                TFBtxt.Visible = false;
                TWtxt.Visible = false;
                HTtxt.Visible = false;

                Dlbl.Visible = true;
                Blbl.Visible = true;
                BFlbl.Visible = false;
                BFBlbl.Visible = false;
                TFlbl.Visible = false;
                TFBlbl.Visible = false;
                TWlbl.Visible = false;
                HTlbl.Visible = false;
                Dlbl.Text = "Depth";
                Blbl.Text = "Width";
            }

            if (comboBox1.SelectedIndex == 15)//Concrete Circle 
            {
                DESIGNATIONtxt.Text = "CR";
                Dtxt.Visible = true;
                Btxt.Visible = false;
                BFtxt.Visible = false;
                BFBtxt.Visible = false;
                TFtxt.Visible = false;
                TFBtxt.Visible = false;
                TWtxt.Visible = false;
                HTtxt.Visible = false;

                Dlbl.Visible = true;
                Blbl.Visible = false;
                BFlbl.Visible = false;
                BFBlbl.Visible = false;
                TFlbl.Visible = false;
                TFBlbl.Visible = false;
                TWlbl.Visible = false;
                HTlbl.Visible = false;
                Dlbl.Text = "Diameter";
            }
            DrawSection();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int i = Section.Selected;
            if (Myglobals.EditOrNew == 1 || Myglobals.EditOrNew == 3)
            {
                Section.Numberd = Section.Numberd + 1;
                Section.Selected = Section.Numberd;
                i = Section.Numberd;
            }
                Section.MODELd[i] = MODELtxt.Text;
                Section.DESCRIPTIONd[i] = DESCRIPTIONtxt.Text;
                Section.LENGTH_UNITSd[i] = LENGTH_UNITStxt.Text;
                Section.LABELd[i] = LABELtxt.Text;
                Section.DESIGNATIONd[i] = DESIGNATIONtxt.Text;
                Section.Dd[i] = Convert.ToDouble(Dtxt.Text);
                Section.BFd[i] = Convert.ToDouble(BFtxt.Text);
                Section.TFd[i] = Convert.ToDouble(TFtxt.Text);
                Section.BFBd[i] = Convert.ToDouble(BFBtxt.Text);
                Section.TFBd[i] = Convert.ToDouble(TFBtxt.Text);
                Section.TWd[i] = Convert.ToDouble(TWtxt.Text);
                Section.Ad[i] = Convert.ToDouble(Atxt.Text);
                Section.I33d[i] = Convert.ToDouble(I33txt.Text);
                Section.Z33d[i] = Convert.ToDouble(Z33txt.Text);
                Section.AS3d[i] = Convert.ToDouble(AS3txt.Text);
                Section.I22d[i] = Convert.ToDouble(I22txt.Text);
                Section.Z22d[i] = Convert.ToDouble(Z22txt.Text);
                Section.AS2d[i] = Convert.ToDouble(AS2txt.Text);
                Section.Jd[i] = Convert.ToDouble(Jtxt.Text);
                Section.Xd[i] = Convert.ToDouble(Xtxt.Text);
                Section.Yd[i] = Convert.ToDouble(Ytxt.Text);
                Section.S33POSd[i] = Convert.ToDouble(S33POStxt.Text);
                Section.S33NEGd[i] = Convert.ToDouble(S33NEGtxt.Text);
                Section.S22POSd[i] = Convert.ToDouble(S22POStxt.Text);
                Section.S22NEGd[i] = Convert.ToDouble(S22NEGtxt.Text);
                Section.R33d[i] = Convert.ToDouble(R33txt.Text);
                Section.R22d[i] = Convert.ToDouble(R22txt.Text);
                Section.Bd[i] = Convert.ToDouble(Btxt.Text);
                Section.HTd[i] = Convert.ToDouble(HTtxt.Text);
                Section.ODd[i] = Convert.ToDouble(ODtxt.Text);
                Section.TDESd[i] = Convert.ToDouble(TDEStxt.Text);
                Section.Materiald[i] = comboBox2.SelectedIndex+1;
                if (DESIGNATIONtxt.Text == "R")
                {
                    Section.BFd[i] = 0;
                    Section.BFBd[i] = 0;
                    Section.TFd[i] = 0;
                    Section.TFBd[i] = 0;
                    Section.TWd[i] = 0;
                    Section.HTd[i] = 0;
                }
                if (DESIGNATIONtxt.Text == "B")
                {
                    Section.BFBd[i] = 0;
                    Section.TFBd[i] = 0;
                }
                if (DESIGNATIONtxt.Text == "W")
                {
                    Section.Bd[i] = 0;
                    Section.HTd[i] = 0;
                }

                if (DESIGNATIONtxt.Text == "L")
                {
                    Section.BFd[i] = 0;
                    Section.BFBd[i] = 0;
                    Section.TFBd[i] = 0;
                    Section.HTd[i] = 0;
                }
                if (DESIGNATIONtxt.Text == "CR")
                {
                    Section.Bd[i] = 0;
                    Section.BFd[i] = 0;
                    Section.BFBd[i] = 0;
                    Section.TFd[i] = 0;
                    Section.TFBd[i] = 0;
                    Section.TWd[i] = 0;
                    Section.HTd[i] = 0;
                }
                if (Myglobals.EditOrNew == 2)//تعديل
                {
                    ((FramePropertiesFrm)framePropertiesFrm).listBox1.Items.Insert(((FramePropertiesFrm)framePropertiesFrm).listBox1.SelectedIndex, Section.LABELd[i]);
                    ((FramePropertiesFrm)framePropertiesFrm).listBox1.Items.Remove(((FramePropertiesFrm)framePropertiesFrm).listBox1.SelectedItem);
                    ((FramePropertiesFrm)framePropertiesFrm).listBox1.SetSelected(Section.Selected-1, true);
                }
                if (Myglobals.EditOrNew == 1 || Myglobals.EditOrNew == 3)//جديد
                {
                    ((FramePropertiesFrm)framePropertiesFrm).listBox1.Items.Add(Section.LABELd[i]);
                    ((FramePropertiesFrm)framePropertiesFrm).listBox1.SetSelected(Section.Selected - 1, true);
                }
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Dtxt_Leave(object sender, EventArgs e)
        {
            CalcSection();
            DrawSection();
        }
        private void Btxt_Leave(object sender, EventArgs e)
        {
            CalcSection();
            DrawSection();
        }
        private void BFtxt_Leave(object sender, EventArgs e)
        {
            CalcSection();
            DrawSection();
        }
        private void TFtxt_Leave(object sender, EventArgs e)
        {
            CalcSection();
            DrawSection();
        }
        private void BFBtxt_Leave(object sender, EventArgs e)
        {
            CalcSection();
            DrawSection();
        }
        private void TFBtxt_Leave(object sender, EventArgs e)
        {
            CalcSection();
            DrawSection();
        }
        private void TWtxt_Leave(object sender, EventArgs e)
        {
            CalcSection();
            DrawSection();
        }
        private void HTtxt_Leave(object sender, EventArgs e)
        {
            CalcSection();
            DrawSection();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Myglobals.ModifyShow_PropertyIsOpen = 1;
            PropertyModifiersForm propertyModifiersForm = new PropertyModifiersForm();
            propertyModifiersForm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrameSectionReinforcementDataForm theform = new FrameSectionReinforcementDataForm();
            theform.ShowDialog();
        }
    }
}
