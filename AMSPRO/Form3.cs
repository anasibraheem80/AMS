using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Windows.Media;

namespace AMSPRO
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        Form mainForm = Application.OpenForms["MainForm"];
        private void Form3_Load(object sender, EventArgs e)
        {
            int i = 0;
            MSFlexJionts.RowCount = Joint.Number3d + 1;
            for (i = 0; i < Joint.Number3d; i++)
            {
                MSFlexJionts.Rows[i].Cells[0].Value = i + 1;
                MSFlexJionts.Rows[i].Cells[1].Value = Joint.XReal[i + 1];
                MSFlexJionts.Rows[i].Cells[2].Value = Joint.YReal[i + 1];
                MSFlexJionts.Rows[i].Cells[3].Value = Joint.ZReal[i + 1];
                MSFlexJionts.Rows[i].Cells[4].Value = Joint.FixX[i + 1];
                MSFlexJionts.Rows[i].Cells[5].Value = Joint.FixY[i + 1];
                MSFlexJionts.Rows[i].Cells[6].Value = Joint.FixZ[i + 1];
                MSFlexJionts.Rows[i].Cells[7].Value = Joint.FixRX[i + 1];
                MSFlexJionts.Rows[i].Cells[8].Value = Joint.FixRY[i + 1];
                MSFlexJionts.Rows[i].Cells[9].Value = Joint.FixRZ[i + 1];
            }
            /////-------------------------input Elements  in Table
            MSFlexElemnts.RowCount = Frame.Number + 1;
            for (i = 0; i < Frame.Number; i++)
            {
                MSFlexElemnts.Rows[i].Cells[0].Value = i + 1;
                MSFlexElemnts.Rows[i].Cells[1].Value = ((MainForm)mainForm).FrameElement[i+1].FirstJoint;
                MSFlexElemnts.Rows[i].Cells[2].Value = ((MainForm)mainForm).FrameElement[i+1].SecondJoint;
                MSFlexElemnts.Rows[i].Cells[3].Value = Section.A[((MainForm)mainForm).FrameElement[i+1].Section];// *6.45 * Math.Pow(10, -4);
                MSFlexElemnts.Rows[i].Cells[4].Value = Material.Elastisity[Section.Material[((MainForm)mainForm).FrameElement[i+1].Section]];
                MSFlexElemnts.Rows[i].Cells[5].Value = Material.ShearM[Section.Material[((MainForm)mainForm).FrameElement[i+1].Section]];
                MSFlexElemnts.Rows[i].Cells[6].Value = Section.I33[((MainForm)mainForm).FrameElement[i+1].Section];
                MSFlexElemnts.Rows[i].Cells[7].Value = Section.I22[((MainForm)mainForm).FrameElement[i+1].Section];
                MSFlexElemnts.Rows[i].Cells[8].Value = Section.AS2[((MainForm)mainForm).FrameElement[i+1].Section];
                MSFlexElemnts.Rows[i].Cells[9].Value = Section.AS3[((MainForm)mainForm).FrameElement[i+1].Section];
                MSFlexElemnts.Rows[i].Cells[10].Value = Section.J[((MainForm)mainForm).FrameElement[i+1].Section];
                MSFlexElemnts.Rows[i].Cells[11].Value = 1; // must be option
            }
            for (i = 0; i < Globals.NodeNumberALL; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    MSFlexJionts.Rows[i].Cells[j + 10].Value = Globals.PNode[j, i];
                }
            }
        }
        private void button6_Click_1(object sender, EventArgs e)
        {
            Form2 theform = new Form2();
            theform.ShowDialog();
        }
    }
}








