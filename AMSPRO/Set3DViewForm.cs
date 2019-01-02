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
    public partial class Set3DViewForm : Form
    {
        public Set3DViewForm()
        {
            InitializeComponent();
        }
        Form mainForm = Application.OpenForms["MainForm"];

        private void Set3DViewForm_Load(object sender, EventArgs e)
        {
            textBox4.Text = Myglobals.Aperture.ToString();
            textBox5.Text = Myglobals.valDY.ToString();
            textBox6.Text = Myglobals.valDX.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox5.Text = "0";
            textBox6.Text = "0";
            SetThe3D();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox5.Text = "90";
            textBox6.Text = "0";
            SetThe3D();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox5.Text = "90";
            textBox6.Text = "270";
            SetThe3D();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox5.Text = "50";
            textBox6.Text = "35";
            SetThe3D();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void SetThe3D()
        {
            double tah = Math.PI / 180;

            Myglobals.valDY = Convert.ToDouble(textBox5.Text);
            Myglobals.valDX = Convert.ToDouble (textBox6.Text) ;

            if (Myglobals.valDX >= 360)
            {
                Myglobals.valDX = Myglobals.valDX - 360;
                goto EndloopX;
            }
            if (Myglobals.valDX <= 0)
            {
                Myglobals.valDX = Myglobals.valDX + 360;
                goto EndloopX;
            }
        EndloopX: { }
            if (Myglobals.valDY >= 360)
            {
                Myglobals.valDY = Myglobals.valDY - 360;
                goto EndloopY;
            }
            if (Myglobals.valDY <= 0)
            {
                Myglobals.valDY = Myglobals.valDY + 360;
                goto EndloopY;
            }
        EndloopY: { }

            double zaweea = Myglobals.valDX;
            if (Myglobals.valDX >= 315 & Myglobals.valDX <= 360)///////////1
            {
                Myglobals.Tadweer = 1;
                zaweea = Myglobals.valDX - 360;
            }
            if (Myglobals.valDX >= 0 & Myglobals.valDX < 45)
            {
                Myglobals.Tadweer = 1;
                zaweea = Myglobals.valDX;
            }

            if (Myglobals.valDX >= 45 & Myglobals.valDX < 135)/////////////2
            {
                Myglobals.Tadweer = 2;
                zaweea = 90 - Myglobals.valDX;
            }

            if (Myglobals.valDX >= 135 & Myglobals.valDX < 225)//////////3
            {
                Myglobals.Tadweer = 3;
                zaweea = Myglobals.valDX - 180;
            }

            if (Myglobals.valDX >= 225 & Myglobals.valDX < 315)/////////4
            {
                Myglobals.Tadweer = 4;
                zaweea = 270 - Myglobals.valDX;
            }
            float addedvalue = (float)((8) * Math.Cos(tah * (Myglobals.valDY)) * Math.Sin(tah * (Myglobals.valDY)) * Math.Sin(tah * (zaweea))); 
            if (Myglobals.Tadweer == 1 || Myglobals.Tadweer == 3)
            {
                Myglobals.tXValue = +(float)Myglobals.valDY;
                Myglobals.tYValue = 0 - (float)(zaweea * Math.Sin(tah * (Myglobals.valDY))) - addedvalue; 
                Myglobals.tZValue = (float)(zaweea * Math.Cos(tah * (Myglobals.valDY))) - addedvalue; 
            }

            if (Myglobals.Tadweer == 2 || Myglobals.Tadweer == 4)
            {
                Myglobals.tXValue = -(float)Myglobals.valDY;
                Myglobals.tYValue = 0 + (float)(zaweea * Math.Sin(tah * (Myglobals.valDY))) + addedvalue; 
                Myglobals.tZValue = (float)(zaweea * Math.Cos(tah * (Myglobals.valDY))) - addedvalue; 
            }

            if (Myglobals.tXValue > 360)
            {
                Myglobals.tXValue = Myglobals.tXValue - 360;
                goto EndX;
            }
            if (Myglobals.tXValue < 0)
            {
                Myglobals.tXValue = Myglobals.tXValue + 360;
                goto EndX;
            }
        EndX: { };
            if (Myglobals.tYValue > 360)
            {
                Myglobals.tYValue = Myglobals.tYValue - 360;
                goto EndY;
            }
            if (Myglobals.tYValue < 0)
            {
                Myglobals.tYValue = Myglobals.tYValue + 360;
                goto EndY;
            }
        EndY: { };
            if (Myglobals.tZValue > 360)
            {
                Myglobals.tZValue = Myglobals.tZValue - 360;
                goto EndZ;
            }
            if (Myglobals.tZValue < 0)
            {
                Myglobals.tZValue = Myglobals.tZValue + 360;
                goto EndZ;
            }
        EndZ: { };
            Myglobals.Aperture = (float)(Convert.ToDouble(textBox4.Text));
            ((MainForm)mainForm).MakeTempFiles();
            DROWcls callmee = new DROWcls();
            callmee.Render2d();
            callmee.Render3d();
        }

        private void button15_Click(object sender, EventArgs e)
        {
                textBox6.Text = (Convert.ToDouble(textBox6.Text) + 1).ToString();
                SetThe3D();
        }

        private void button17_Click(object sender, EventArgs e)
        {
                textBox5.Text = (Convert.ToDouble(textBox5.Text) + 1).ToString();
                SetThe3D();
        }

        private void button14_Click(object sender, EventArgs e)
        {
                textBox6.Text = (Convert.ToDouble(textBox6.Text) - 1).ToString();
                SetThe3D();
        }

        private void button16_Click(object sender, EventArgs e)
        {
                textBox5.Text = (Convert.ToDouble(textBox5.Text) - 1).ToString();
                SetThe3D();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            SetThe3D();
            this.Close(); 
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox4.Text = (Convert.ToDouble(textBox4.Text) + 1).ToString();
            SetThe3D();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox4.Text = (Convert.ToDouble(textBox4.Text) - 1).ToString();
            SetThe3D();
        }
   }
}
