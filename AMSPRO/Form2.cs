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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
 

        private void button2_Click(object sender, EventArgs e)
        {
            int sh = 0;  // x section reletive distance 
            if (textBox1.Text != "") sh = Convert.ToInt32(textBox1.Text);
            MSFlex2.RowCount = Globals.ElementNumberALL;
            for (int k = 0; k < Globals.ElementNumberALL; k++)
            {
                //print result in table
                MSFlex2.Rows[k].Cells[0].Value = k + 1;
                MSFlex2.Rows[k].Cells[1].Value = Frame.ResultValue1[k+1, sh];
                MSFlex2.Rows[k].Cells[2].Value = Frame.ResultValue2[k+1, sh];
                MSFlex2.Rows[k].Cells[5].Value = Frame.ResultValue3[k+1, sh];
                MSFlex2.Rows[k].Cells[3].Value = Frame.ResultValue4[k+1, sh];
                MSFlex2.Rows[k].Cells[4].Value = Frame.ResultValue5[k+1, sh];
                MSFlex2.Rows[k].Cells[6].Value = Frame.ResultValue6[k+1, sh];
                MSFlex2.Rows[k].Cells[7].Value = Frame.ResultValue7[k + 1, sh];
                MSFlex2.Rows[k].Cells[8].Value = Frame.ResultValue8[k + 1, sh];
            }

            /*
            MSFlex1.RowCount = Globals.NodeNumberALL;
            int i = 0;
            int j = 0;
            for (i = 0; i < Globals.NodeNumberALL; i++)
            {
                MSFlex1.Rows[i].Cells[0].Value = i + 1;
                for (j = 1; j < 7; j++)
                {
                    MSFlex1.Rows[i].Cells[j].Value = Math.Round(Globals.UDispAll[j - 1, i], 9);
                }
            }
            */
        }
    }
}
