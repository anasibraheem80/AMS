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
    public partial class FramReleasesForm : Form
    {
        public FramReleasesForm()
        {
            InitializeComponent();
        }
        Form mainForm = Application.OpenForms["MainForm"];
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox1.Enabled = true;
                textBox1.Text = "0";
                checkBox13.Checked = false;
            }
            else
            {
                textBox1.Enabled =false ;
                textBox1.Text = "";
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                textBox2.Enabled = true;
                textBox2.Text = "0";
                checkBox13.Checked = false;
            }
            else
            {
                textBox2.Enabled = false;
                textBox2.Text = "";
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                textBox3.Enabled = true;
                textBox3.Text = "0";
                checkBox13.Checked = false;
            }
            else
            {
                textBox3.Enabled = false;
                textBox3.Text = "";
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                textBox4.Enabled = true;
                textBox4.Text = "0";
                checkBox13.Checked = false;
            }
            else
            {
                textBox4.Enabled = false;
                textBox4.Text = "";
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                textBox5.Enabled = true;
                textBox5.Text = "0";
                checkBox13.Checked = false;
            }
            else
            {
                textBox5.Enabled = false;
                textBox5.Text = "";
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked == true)
            {
                textBox6.Enabled = true;
                textBox6.Text = "0";
                checkBox13.Checked = false;
            }
            else
            {
                textBox6.Enabled = false;
                textBox6.Text = "";
            }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked == true)
            {
                textBox7.Enabled = true;
                textBox7.Text = "0";
                checkBox13.Checked = false;
            }
            else
            {
                textBox7.Enabled = false;
                textBox7.Text = "";
            }
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked == true)
            {
                textBox8.Enabled = true;
                textBox8.Text = "0";
                checkBox13.Checked = false;
            }
            else
            {
                textBox8.Enabled = false;
                textBox8.Text = "";
            }
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox9.Checked == true)
            {
                textBox9.Enabled = true;
                textBox9.Text = "0";
                checkBox13.Checked = false;
            }
            else
            {
                textBox9.Enabled = false;
                textBox9.Text = "";
            }
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox10.Checked == true)
            {
                textBox10.Enabled = true;
                textBox10.Text = "0";
                checkBox13.Checked = false;
            }
            else
            {
                textBox10.Enabled = false;
                textBox10.Text = "";
            }
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox13.Checked == true)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
                checkBox6.Checked = false;
                checkBox7.Checked = false;
                checkBox8.Checked = false;
                checkBox9.Checked = false;
                checkBox10.Checked = false;
                checkBox11.Checked = false;
                checkBox12.Checked = false;
                textBox1.Enabled = false;
                textBox1.Text = "";
                textBox2.Enabled = false;
                textBox2.Text = "";
                textBox3.Enabled = false;
                textBox3.Text = "";
                textBox4.Enabled = false;
                textBox4.Text = "";
                textBox5.Enabled = false;
                textBox5.Text = "";
                textBox6.Enabled = false;
                textBox6.Text = "";
                textBox7.Enabled = false;
                textBox7.Text = "";
                textBox8.Enabled = false;
                textBox8.Text = "";
                textBox9.Enabled = false;
                textBox9.Text = "";
                textBox10.Enabled = false;
                textBox10.Text = "";
                textBox11.Enabled = false;
                textBox11.Text = "";
                textBox12.Enabled = false;
                textBox12.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < Frame.Number + 1; i++)
            {
                if (((MainForm)mainForm).FrameElement[i].Selected == 1)
                {
                    ((MainForm)mainForm).FrameElement[i].ReleaseAxialI = Convert.ToDouble(textBox1.Text);
                    ((MainForm)mainForm).FrameElement[i].ReleaseShear2I = Convert.ToDouble(textBox3.Text);
                    ((MainForm)mainForm).FrameElement[i].ReleaseShear3I = Convert.ToDouble(textBox5.Text);
                    ((MainForm)mainForm).FrameElement[i].ReleaseTorsionI = Convert.ToDouble(textBox7.Text);
                    ((MainForm)mainForm).FrameElement[i].ReleaseMoment22I = Convert.ToDouble(textBox9.Text);
                    ((MainForm)mainForm).FrameElement[i].ReleaseMoment33I = Convert.ToDouble(textBox11.Text);
                    ((MainForm)mainForm).FrameElement[i].ReleaseAxialJ = Convert.ToDouble(textBox2.Text);
                    ((MainForm)mainForm).FrameElement[i].ReleaseShear2J = Convert.ToDouble(textBox4.Text);
                    ((MainForm)mainForm).FrameElement[i].ReleaseShear3J = Convert.ToDouble(textBox6.Text);
                    ((MainForm)mainForm).FrameElement[i].ReleaseTorsionJ = Convert.ToDouble(textBox8.Text);
                    ((MainForm)mainForm).FrameElement[i].ReleaseMoment22J = Convert.ToDouble(textBox10.Text);
                    ((MainForm)mainForm).FrameElement[i].ReleaseMoment33J = Convert.ToDouble(textBox12.Text);
                }
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox11_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox11.Checked == true)
            {
                textBox11.Enabled = true;
                textBox11.Text = "0";
                checkBox13.Checked = false;
            }
            else
            {
                textBox11.Enabled = false;
                textBox11.Text = "";
            }
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox12.Checked == true)
            {
                textBox12.Enabled = true;
                textBox12.Text = "0";
                checkBox13.Checked = false;
            }
            else
            {
                textBox12.Enabled = false;
                textBox12.Text = "";
            }
        }
    }
}
