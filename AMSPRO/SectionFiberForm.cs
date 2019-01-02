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
    public partial class SectionFiberForm : Form
    {
        public SectionFiberForm()
        {
            InitializeComponent();
        }
        Form sectionDrawForm = Application.OpenForms["SectionDrawForm"];
        private void button1_Click(object sender, EventArgs e)
        {
            ((SectionDrawForm)sectionDrawForm).LineNO1 = Convert.ToInt32(textBox1.Text) ;
            ((SectionDrawForm)sectionDrawForm).LineNO2 = Convert.ToInt32(textBox2.Text);
            ((SectionDrawForm)sectionDrawForm).Angel = Convert.ToDouble (textBox3.Text);
            ((SectionDrawForm)sectionDrawForm).CalculateGridFiberLines();
            ((SectionDrawForm)sectionDrawForm).Render2d();
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
