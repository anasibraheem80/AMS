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
    public partial class ShellLoadAssignmentForm : Form
    {
        public ShellLoadAssignmentForm()
        {
            InitializeComponent();
        }
        int ifapplay = 0;
        Form mainForm = Application.OpenForms["MainForm"];
        private void ShellLoadAssignmentForm_Load(object sender, EventArgs e)
        {
            if (Loads.Number > 0)
            {
                for (int i = 1; i < Loads.Number + 1; i++)
                {
                    comboBox1.Items.Add(Loads.Load[i]);
                }
                comboBox1.SelectedIndex = 0;
            }
            comboBox2.SelectedIndex = 5;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (ifapplay == 0)
            {
                int TheType = 0;
                double Uniform = 0;
                int Direction = 0;
                int Pattern = 0;
                if (textBox9.Text != "") Uniform = Convert.ToDouble(textBox9.Text);
                if (comboBox1.SelectedIndex >= 0) Pattern = comboBox1.SelectedIndex;
                if (comboBox2.SelectedIndex >= 0) Direction = comboBox2.SelectedIndex;
                for (int i = 1; i < Shell.Number + 1; i++)
                {
                    int selected = 0;
                    for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                    {
                        if (Shell.SelectedLine[i, j] == 1)
                        {
                            selected = 1;
                            break;
                        }
                    }
                    if (selected == 1)
                    {
                        if (radioButton1.Checked == true)//إضافة
                        {
                            Shell.LoadNumber[i] = Shell.LoadNumber[i] + 1;
                            int n = Shell.LoadNumber[i];
                            Shell.LoadType[i, n] = TheType;
                            Shell.LoadUniform[i, n] = Uniform;
                            Shell.LoadDirection[i, n] = Direction;
                            Shell.LoadPattern[i, n] = Pattern;
                        }
                        if (radioButton2.Checked == true)//تبديل
                        {
                            Shell.LoadNumber[i] = 1;
                            int n = Shell.LoadNumber[i];
                            Shell.LoadType[i, n] = TheType;
                            Shell.LoadUniform[i, n] = Uniform;
                            Shell.LoadDirection[i, n] = Direction;
                            Shell.LoadPattern[i, n] = Pattern;
                        }
                        if (radioButton3.Checked == true)//حذف
                        {
                            Shell.LoadNumber[i] = 0;
                            int n = Shell.LoadNumber[i];
                            Shell.LoadType[i, n] = TheType;
                            Shell.LoadUniform[i, n] = Uniform;
                            Shell.LoadDirection[i, n] = Direction;
                            Shell.LoadPattern[i, n] = Pattern;
                        }
                    }
                }
                ((MainForm)mainForm).MakeTempFiles();
                DROWcls callmee = new DROWcls();
                callmee.Render2d();
                callmee.Render3d();
                this.Close();
            }
        }
    }
}
