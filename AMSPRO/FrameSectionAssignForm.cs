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
    public partial class FrameSectionAssignForm : Form
    {
        public FrameSectionAssignForm()
        {
            InitializeComponent();
        }
        Form mainForm = Application.OpenForms["MainForm"];
        private void FrameSectionAssignForm_Load(object sender, EventArgs e)
        {
            for (int i = 1; i < Section.Number + 1; i++)
            {
                listBox1.Items.Add(Section.LABEL[i]);
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            FramePropertiesFrm framePropertiesFrm = new FramePropertiesFrm();
            framePropertiesFrm.ShowDialog();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex + 1;
            int ifselected = 0;
            for (int j = 1; j < Frame.Number + 1; j++)
            {
                if (((MainForm)mainForm).FrameElement[j].Selected == 1)
                {
                    ((MainForm)mainForm).FrameElement[j].Section = i;
                    ifselected = 1;
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
    }
}
