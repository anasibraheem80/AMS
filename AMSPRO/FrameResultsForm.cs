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
    public partial class FrameResultsForm : Form
    {
        public FrameResultsForm()
        {
            InitializeComponent();
        }
        Form mainForm = Application.OpenForms["MainForm"];
        int xmove = 0;
        int ymove = 0;
        int[] anyJ = new int[9];
        double[] MAXpowerj = new double[9];

        
        double disancValue = 0;
        double BeamLength =0;
        double plus =0;
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FrameResultsForm_Load(object sender, EventArgs e)
        {
            pictureBox1.Controls.Add(pictureBox11);
            pictureBox11.Location = new Point(0, 0);
            pictureBox11.BackColor = Color.Transparent;
            pictureBox2.Controls.Add(pictureBox22);
            pictureBox22.Location = new Point(0, 0);
            pictureBox22.BackColor = Color.Transparent;
            pictureBox3.Controls.Add(pictureBox33);
            pictureBox33.Location = new Point(0, 0);
            pictureBox33.BackColor = Color.Transparent;
            pictureBox4.Controls.Add(pictureBox44);
            pictureBox44.Location = new Point(0, 0);
            pictureBox44.BackColor = Color.Transparent;
            pictureBox5.Controls.Add(pictureBox55);
            pictureBox55.Location = new Point(0, 0);
            pictureBox55.BackColor = Color.Transparent;
            pictureBox6.Controls.Add(pictureBox66);
            pictureBox66.Location = new Point(0, 0);
            pictureBox66.BackColor = Color.Transparent;
            pictureBox7.Controls.Add(pictureBox77);
            pictureBox77.Location = new Point(0, 0);
            pictureBox77.BackColor = Color.Transparent;
            pictureBox8.Controls.Add(pictureBox88);
            pictureBox88.Location = new Point(0, 0);
            pictureBox88.BackColor = Color.Transparent;
            int i=Frame.SelectedforProp ;
            double ThePowerValue = 0;
            int TheDistanceValue = 0;
            Bitmap finalBmp1 = new Bitmap(350, 100);
            Bitmap finalBmp2= new Bitmap(350, 100);
            Bitmap finalBmp3 = new Bitmap(350, 100);
            Bitmap finalBmp4 = new Bitmap(350, 100);
            Bitmap finalBmp5 = new Bitmap(350, 100);
            Bitmap finalBmp6 = new Bitmap(350, 100);
            Bitmap finalBmp7 = new Bitmap(350, 100);
            Bitmap finalBmp8= new Bitmap(350, 100);
            pictureBox1.Image = finalBmp1;
            pictureBox2.Image = finalBmp2;
            pictureBox3.Image = finalBmp3;
            pictureBox4.Image = finalBmp4;
            pictureBox5.Image = finalBmp5;
            pictureBox6.Image = finalBmp6;
            pictureBox7.Image = finalBmp7;
            pictureBox8.Image = finalBmp8;
            double tx1R = Joint.XReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
            double ty1R = Joint.YReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
            double tz1R = Joint.ZReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
            double tx2R = Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
            double ty2R = Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
            double tz2R = Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
            BeamLength = (Math.Sqrt(Math.Pow(tx1R - tx2R, 2) + Math.Pow(ty1R - ty2R, 2) + Math.Pow(tz1R - tz2R, 2)));
            textBox1.Text = Math.Round (BeamLength,2).ToString ();
            plus = BeamLength / Frame.AnalisesSecNumbers;
            int AnyDiagram =0;/////////////////////////////
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            #region//حساب مكان القيم
            for (AnyDiagram = 1; AnyDiagram < 9; AnyDiagram++)
            {
                if (AnyDiagram == 1) g = Graphics.FromImage(pictureBox1.Image);
                if (AnyDiagram == 2) g = Graphics.FromImage(pictureBox2.Image);
                if (AnyDiagram == 3) g = Graphics.FromImage(pictureBox3.Image);
                if (AnyDiagram == 4) g = Graphics.FromImage(pictureBox4.Image);
                if (AnyDiagram == 5) g = Graphics.FromImage(pictureBox5.Image);
                if (AnyDiagram == 6) g = Graphics.FromImage(pictureBox6.Image);
                if (AnyDiagram == 7) g = Graphics.FromImage(pictureBox7.Image);
                if (AnyDiagram == 8) g = Graphics.FromImage(pictureBox8.Image);
                Pen pen = new Pen(Color.Green, 1f);
                Font drawFont = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
                SolidBrush drawBrush = new SolidBrush(Color.Black);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                int tx1 = 10;
                int ty1 = 40;
                int tx2 = 290;
                int ty2 = 40;
                int tx = 0;
                int ty = 0;
                int tx0 = 0;
                int ty0 = 0;
                int intlength = 280;
                int intpower = 37;
                g.DrawLine(pen, tx1, ty1, tx2, ty2);
                pen = new Pen(Color.Red, 1f);
                double MAXpower = -1000000000;
                for (int j = 0; j < Frame.AnalisesSecNumbers + 1; j++)
                {
                    if (AnyDiagram == 1) ThePowerValue = Frame.ResultValue1[i, j];//axial
                    if (AnyDiagram == 2) ThePowerValue = Frame.ResultValue2[i, j];//s22
                    if (AnyDiagram == 3) ThePowerValue = Frame.ResultValue4[i, j];//s33
                    if (AnyDiagram == 4) ThePowerValue = Frame.ResultValue5[i, j];//m22
                    if (AnyDiagram == 5) ThePowerValue = Frame.ResultValue3[i, j];//m33
                    if (AnyDiagram == 6) ThePowerValue = Frame.ResultValue6[i, j];//torsion
                    if (AnyDiagram == 7) ThePowerValue = Frame.ResultValue7[i, j];//u1
                    if (AnyDiagram == 8) ThePowerValue = Frame.ResultValue8[i, j];//u2
                    if (Math.Abs(ThePowerValue) > MAXpower)
                    {
                        MAXpowerj[AnyDiagram] = Math.Abs(ThePowerValue);
                        anyJ[AnyDiagram] = j;
                        MAXpower = Math.Abs(ThePowerValue);
                    }
                }
                if (AnyDiagram == 1) label1.Text = Math.Round (MAXpower,2).ToString ();//axial
                if (AnyDiagram == 2) label2.Text = Math.Round (MAXpower,2).ToString();//s22
                if (AnyDiagram == 3) label3.Text = Math.Round (MAXpower,2).ToString();//s33
                if (AnyDiagram == 4) label4.Text = Math.Round (MAXpower,2).ToString();//m22
                if (AnyDiagram == 5) label5.Text = Math.Round (MAXpower,2).ToString();//m33
                if (AnyDiagram == 6) label6.Text = Math.Round(MAXpower, 2).ToString();//torsion
                if (AnyDiagram == 7) label7.Text = Math.Round(MAXpower, 2).ToString();//u1
                if (AnyDiagram == 8) label8.Text = Math.Round(MAXpower, 2).ToString();//u2
                if (MAXpower > 0)
                {
                    pen = new Pen(Color.Black, 1f);
                    for (int j = 0; j < Frame.AnalisesSecNumbers + 1; j++)
                    {
                        if (AnyDiagram == 1) ThePowerValue = Frame.ResultValue1[i, j];//axial
                        if (AnyDiagram == 2) ThePowerValue = Frame.ResultValue2[i, j];//s22
                        if (AnyDiagram == 3) ThePowerValue = Frame.ResultValue4[i, j];//s33
                        if (AnyDiagram == 4) ThePowerValue = Frame.ResultValue5[i, j];//m22
                        if (AnyDiagram == 5) ThePowerValue = Frame.ResultValue3[i, j];//m33
                        if (AnyDiagram == 6) ThePowerValue = Frame.ResultValue6[i, j];//torsion
                        if (AnyDiagram == 7) ThePowerValue = Frame.ResultValue7[i, j];//u1
                        if (AnyDiagram == 8) ThePowerValue = Frame.ResultValue8[i, j];//u2
                        TheDistanceValue = Convert.ToInt32(j * plus / BeamLength * intlength);
                        if (j == Frame.AnalisesSecNumbers) TheDistanceValue = intlength;
                        tx = tx1 + TheDistanceValue;
                        ty = ty1 + Convert.ToInt32(ThePowerValue / MAXpower * intpower);
                        g.DrawLine(pen, tx, ty, tx, ty1);
                        if (j > 0) g.DrawLine(pen, tx, ty, tx0, ty0);
                        tx0 = tx;
                        ty0 = ty;
                    }
                }
            }
            #endregion
            MaxValues();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            MaxValues();
        }

        private void movemouse()
        {
            int i = Frame.SelectedforProp;
            int TheDistanceValue = 0;
            Bitmap finalBmp1 = new Bitmap(350, 100);
            Bitmap finalBmp2 = new Bitmap(350, 100);
            Bitmap finalBmp3 = new Bitmap(350, 100);
            Bitmap finalBmp4 = new Bitmap(350, 100);
            Bitmap finalBmp5 = new Bitmap(350, 100);
            Bitmap finalBmp6 = new Bitmap(350, 100);
            Bitmap finalBmp7 = new Bitmap(350, 100);
            Bitmap finalBmp8 = new Bitmap(350, 100);

            if (pictureBox11.Image != null) pictureBox11.Image = null;
            if (pictureBox22.Image != null) pictureBox22.Image = null;
            if (pictureBox33.Image != null) pictureBox33.Image = null;
            if (pictureBox44.Image != null) pictureBox44.Image = null;
            if (pictureBox55.Image != null) pictureBox55.Image = null;
            if (pictureBox66.Image != null) pictureBox66.Image = null;
            if (pictureBox77.Image != null) pictureBox77.Image = null;
            if (pictureBox88.Image != null) pictureBox88.Image = null;
            pictureBox11.Image = finalBmp1;
            pictureBox22.Image = finalBmp2;
            pictureBox33.Image = finalBmp3;
            pictureBox44.Image = finalBmp4;
            pictureBox55.Image = finalBmp5;
            pictureBox66.Image = finalBmp6;
            pictureBox77.Image = finalBmp7;
            pictureBox88.Image = finalBmp8;
            int AnyDiagram = 0;/////////////////////////////
            Graphics g = Graphics.FromImage(pictureBox11.Image);
            #region//حساب مكان القيم
            for (AnyDiagram = 1; AnyDiagram < 9; AnyDiagram++)
            {
                if (AnyDiagram == 1) g = Graphics.FromImage(pictureBox11.Image);
                if (AnyDiagram == 2) g = Graphics.FromImage(pictureBox22.Image);
                if (AnyDiagram == 3) g = Graphics.FromImage(pictureBox33.Image);
                if (AnyDiagram == 4) g = Graphics.FromImage(pictureBox44.Image);
                if (AnyDiagram == 5) g = Graphics.FromImage(pictureBox55.Image);
                if (AnyDiagram == 6) g = Graphics.FromImage(pictureBox66.Image);
                if (AnyDiagram == 7) g = Graphics.FromImage(pictureBox77.Image);
                if (AnyDiagram == 8) g = Graphics.FromImage(pictureBox88.Image);
                Pen pen = new Pen(Color.Red, 1f);
                Font drawFont = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
                SolidBrush drawBrush = new SolidBrush(Color.Black);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                int tx1 = 10;
                int ty1 = 40;
                int tx = 0;
                int ty = 0;
                int intlength = 280;
                pen = new Pen(Color.Red, 1f);
                TheDistanceValue = Convert.ToInt32(anyJ[AnyDiagram] * plus / BeamLength * intlength);
                if (anyJ[AnyDiagram] == Frame.AnalisesSecNumbers) TheDistanceValue = intlength;
                if (xmove < tx1) xmove = tx1;
                if (xmove > tx1 + intlength) xmove = tx1 + intlength;
                disancValue = (Convert.ToDouble (xmove - tx1) / Convert.ToDouble (intlength)) * BeamLength;
                textBox2.Text = Math.Round(disancValue, 2).ToString();  
                tx = xmove;
                g.DrawLine(pen, tx, ty1 - 100, tx, ty1 + 100);
                double ThePowerValue = 0;
                double ThePowerValue1 = 0;
                double ThePowerValue2 = 0;
                double TheDistanceValue1 = 0;
                double TheDistanceValue2 = 0;
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
                        if (AnyDiagram == 1)//axial
                        {
                            ThePowerValue1 = Frame.ResultValue1[i, j];
                            ThePowerValue2 = Frame.ResultValue1[i, j + 1];
                        }
                        if (AnyDiagram == 2)//s22
                        {
                            ThePowerValue1 = Frame.ResultValue2[i, j];
                            ThePowerValue2 = Frame.ResultValue2[i, j + 1];
                        }
                        if (AnyDiagram == 3) //s33
                        {
                            ThePowerValue1 = Frame.ResultValue4[i, j];
                            ThePowerValue2 = Frame.ResultValue4[i, j + 1];
                        }
                        if (AnyDiagram == 4)//m22
                        {
                            ThePowerValue1 = Frame.ResultValue5[i, j];
                            ThePowerValue2 = Frame.ResultValue5[i, j + 1];
                        }
                        if (AnyDiagram == 5)//m33
                        {
                            ThePowerValue1 = Frame.ResultValue3[i, j];
                            ThePowerValue2 = Frame.ResultValue3[i, j + 1];
                        }
                        if (AnyDiagram == 6)//torsion
                        {
                            ThePowerValue1 = Frame.ResultValue6[i, j];
                            ThePowerValue2 = Frame.ResultValue6[i, j + 1];
                        }
                        if (AnyDiagram == 7)//u1
                        {
                            ThePowerValue1 = Frame.ResultValue7[i, j];
                            ThePowerValue2 = Frame.ResultValue7[i, j + 1];
                        }
                        if (AnyDiagram ==8)//u2
                        {
                            ThePowerValue1 = Frame.ResultValue8[i, j];
                            ThePowerValue2 = Frame.ResultValue8[i, j + 1];
                        }
                        double farkV12 = ThePowerValue2 - ThePowerValue1;
                        double farkD12 = TheDistanceValue2 - TheDistanceValue1;
                        double farkD = disancValue - TheDistanceValue1;
                        double farkV = farkV12 * farkD / farkD12;
                        ThePowerValue = ThePowerValue1 + farkV;
                        break;
                    }
                }
                if (AnyDiagram == 1) label1.Text = Math.Round(ThePowerValue, 0).ToString();//axial
                if (AnyDiagram == 2) label2.Text = Math.Round(ThePowerValue, 0).ToString();//s22
                if (AnyDiagram == 3) label3.Text = Math.Round(ThePowerValue, 0).ToString();//s33
                if (AnyDiagram == 4) label4.Text = Math.Round(ThePowerValue, 0).ToString();//m22
                if (AnyDiagram == 5) label5.Text = Math.Round(ThePowerValue, 0).ToString();//m33
                if (AnyDiagram == 6) label6.Text = Math.Round(ThePowerValue, 0).ToString();//torsion
                if (AnyDiagram == 7) label7.Text = Math.Round(ThePowerValue, 6).ToString();//u1
                if (AnyDiagram == 8) label8.Text = Math.Round(ThePowerValue, 6).ToString();//u2
            }
            #endregion
        }
        private void MaxValues()
        {
            int i = Frame.SelectedforProp;
            int TheDistanceValue = 0;
            Bitmap finalBmp1 = new Bitmap(350, 100);
            Bitmap finalBmp2 = new Bitmap(350, 100);
            Bitmap finalBmp3 = new Bitmap(350, 100);
            Bitmap finalBmp4 = new Bitmap(350, 100);
            Bitmap finalBmp5 = new Bitmap(350, 100);
            Bitmap finalBmp6 = new Bitmap(350, 100);
            Bitmap finalBmp7 = new Bitmap(350, 100);
            Bitmap finalBmp8 = new Bitmap(350, 100);
            if (pictureBox11.Image != null) pictureBox11.Image = null;
            if (pictureBox22.Image != null) pictureBox22.Image = null;
            if (pictureBox33.Image != null) pictureBox33.Image = null;
            if (pictureBox44.Image != null) pictureBox44.Image = null;
            if (pictureBox55.Image != null) pictureBox55.Image = null;
            if (pictureBox66.Image != null) pictureBox66.Image = null;
            if (pictureBox77.Image != null) pictureBox77.Image = null;
            if (pictureBox88.Image != null) pictureBox88.Image = null;

            pictureBox11.Image = finalBmp1;
            pictureBox22.Image = finalBmp2;
            pictureBox33.Image = finalBmp3;
            pictureBox44.Image = finalBmp4;
            pictureBox55.Image = finalBmp5;
            pictureBox66.Image = finalBmp6;
            pictureBox77.Image = finalBmp7;
            pictureBox88.Image = finalBmp8;

            double tx1R = Joint.XReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
            double ty1R = Joint.YReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
            double tz1R = Joint.ZReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
            double tx2R = Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
            double ty2R = Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
            double tz2R = Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
            double BeamLength = (Math.Sqrt(Math.Pow(tx1R - tx2R, 2) + Math.Pow(ty1R - ty2R, 2) + Math.Pow(tz1R - tz2R, 2)));
            double plus = BeamLength / Frame.AnalisesSecNumbers;
            int AnyDiagram = 0;/////////////////////////////
            Graphics g = Graphics.FromImage(pictureBox11.Image);
            #region//حساب مكان القيم
            for (AnyDiagram = 1; AnyDiagram < 9; AnyDiagram++)
            {
                if (AnyDiagram == 1) g = Graphics.FromImage(pictureBox11.Image);
                if (AnyDiagram == 2) g = Graphics.FromImage(pictureBox22.Image);
                if (AnyDiagram == 3) g = Graphics.FromImage(pictureBox33.Image);
                if (AnyDiagram == 4) g = Graphics.FromImage(pictureBox44.Image);
                if (AnyDiagram == 5) g = Graphics.FromImage(pictureBox55.Image);
                if (AnyDiagram == 6) g = Graphics.FromImage(pictureBox66.Image);
                if (AnyDiagram == 7) g = Graphics.FromImage(pictureBox77.Image);
                if (AnyDiagram == 8) g = Graphics.FromImage(pictureBox88.Image);
                Pen pen = new Pen(Color.Red, 1f);
                Font drawFont = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
                SolidBrush drawBrush = new SolidBrush(Color.Black);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                int tx1 = 10;
                int ty1 = 40;
                int tx = 0;
                int intlength = 280;
                pen = new Pen(Color.Red, 1f);
                TheDistanceValue = Convert.ToInt32(anyJ[AnyDiagram] * plus / BeamLength * intlength);
                if (anyJ[AnyDiagram] == Frame.AnalisesSecNumbers) TheDistanceValue = intlength;
                tx = tx1 + TheDistanceValue;
                g.DrawLine(pen, tx, ty1 - 100, tx, ty1 + 100);
            }
            #endregion
        }

        private void pictureBox11_MouseMove(object sender, MouseEventArgs e)
        {
            xmove = e.X;
            ymove = e.Y;
            movemouse();
        }

        private void pictureBox22_MouseMove(object sender, MouseEventArgs e)
        {
            xmove = e.X;
            ymove = e.Y;
            movemouse();
        }

        private void pictureBox33_MouseMove(object sender, MouseEventArgs e)
        {
            xmove = e.X;
            ymove = e.Y;
            movemouse();
        }

        private void pictureBox44_MouseMove(object sender, MouseEventArgs e)
        {
            xmove = e.X;
            ymove = e.Y;
            movemouse();
        }

        private void pictureBox55_MouseMove(object sender, MouseEventArgs e)
        {
            xmove = e.X;
            ymove = e.Y;
            movemouse();
        }

        private void pictureBox66_MouseMove(object sender, MouseEventArgs e)
        {
            xmove = e.X;
            ymove = e.Y;
            movemouse();
        }

        private void pictureBox77_MouseMove(object sender, MouseEventArgs e)
        {
            xmove = e.X;
            ymove = e.Y;
            movemouse();
        }

        private void pictureBox88_MouseMove(object sender, MouseEventArgs e)
        {
            xmove = e.X;
            ymove = e.Y;
            movemouse();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrameResultsGroupForm theform = new FrameResultsGroupForm();
            theform.ShowDialog();
        }
   
    
    
    }
}
