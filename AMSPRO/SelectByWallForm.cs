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
    public partial class SelectByWallForm : Form
    {
        public SelectByWallForm()
        {
            InitializeComponent();
        }

        private void SelectByWallForm_Load(object sender, EventArgs e)
        {
            for (int i = 1; i < Wall.Number + 1; i++)
            {
                listBox1.Items.Add(Wall.Name[i]);
            }
            listBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
