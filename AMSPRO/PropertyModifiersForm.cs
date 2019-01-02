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
    public partial class PropertyModifiersForm : Form
    {
        Form mainForm = Application.OpenForms["MainForm"];

        public PropertyModifiersForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Myglobals.ModifyShow_PropertyIsOpen == 0)
            {
                for (int i = 1; i < Frame.Number + 1; i++)
                {
                    if (((MainForm)mainForm).FrameElement[i].Selected == 1)
                    {
                        ((MainForm)mainForm).FrameElement[i].ModifiersArea = Convert.ToDouble(textBox1.Text);
                        ((MainForm)mainForm).FrameElement[i].ModifiersShear2 = Convert.ToDouble(textBox2.Text);
                        ((MainForm)mainForm).FrameElement[i].ModifiersShear3 = Convert.ToDouble(textBox3.Text);
                        ((MainForm)mainForm).FrameElement[i].ModifiersTorsional = Convert.ToDouble(textBox4.Text);
                        ((MainForm)mainForm).FrameElement[i].ModifiersI2 = Convert.ToDouble(textBox5.Text);
                        ((MainForm)mainForm).FrameElement[i].ModifiersI3 = Convert.ToDouble(textBox6.Text);
                        ((MainForm)mainForm).FrameElement[i].ModifiersMass = Convert.ToDouble(textBox7.Text);
                        ((MainForm)mainForm).FrameElement[i].ModifiersWeight = Convert.ToDouble(textBox8.Text);
                    }
                }
            }
            if (Myglobals.ModifyShow_PropertyIsOpen == 1)
            {
                int i = Section.Selected;
                Section.ModifiersAread[i]=Convert.ToDouble (textBox1.Text );
                Section.ModifiersShear2d[i] = Convert.ToDouble(textBox2.Text);
                Section.ModifiersShear3d[i] = Convert.ToDouble(textBox3.Text);
                Section.ModifiersTorsionald[i] = Convert.ToDouble(textBox4.Text);
                Section.ModifiersI2d[i] = Convert.ToDouble(textBox5.Text);
                Section.ModifiersI3d[i] = Convert.ToDouble(textBox6.Text);
                Section.ModifiersMassd[i] = Convert.ToDouble(textBox7.Text);
                Section.ModifiersWeightd[i] = Convert.ToDouble(textBox8.Text);
            }

            this.Close();
        }

        private void PropertyModifiersForm_Load(object sender, EventArgs e)
        {
            if (Myglobals.ModifyShow_PropertyIsOpen == 1)
            {
                int i = Section.Selected;
                textBox1.Text = Section.ModifiersAread[i].ToString();
                textBox2.Text = Section.ModifiersShear2d[i].ToString();
                textBox3.Text = Section.ModifiersShear3d[i].ToString();
                textBox4.Text = Section.ModifiersTorsionald[i].ToString();
                textBox5.Text = Section.ModifiersI2d[i].ToString();
                textBox6.Text = Section.ModifiersI3d[i].ToString();
                textBox7.Text = Section.ModifiersMassd[i].ToString();
                textBox8.Text = Section.ModifiersWeightd[i].ToString();
            }
        }
    }
}
