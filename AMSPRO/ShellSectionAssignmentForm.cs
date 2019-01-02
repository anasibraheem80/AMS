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
    public partial class ShellSectionAssignmentForm : Form
    {
        public ShellSectionAssignmentForm()
        {
            InitializeComponent();
        }

        private void ShellSectionAssignmentForm_Load(object sender, EventArgs e)
        {
            for (int i = 1; i < Slab.Number + 1; i++)
            {
                listBox1.Items.Add(Slab.Name[i]);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex + 1;
            int ifselected = 0;
            for (int j = 1; j < Shell.Number + 1; j++)
            {
                for (int k = 1; k < Shell.PointNumbers[j]+1; k++)
                {
                    if (Shell.SelectedLine[j,k]==1)
                    {
                        Shell.Section[j] = i;
                        ifselected = 1;
                        break;
                    }
                }
            }
            if (ifselected == 1)
            {
                DROWcls callmee = new DROWcls();
                callmee.Render2d();
                callmee.Render3d();
            }
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SlabProperteisForm slabProperteisForm = new SlabProperteisForm();
            slabProperteisForm.ShowDialog();
        }
    }
}
