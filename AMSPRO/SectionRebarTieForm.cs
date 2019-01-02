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
    public partial class SectionRebarTieForm : Form
    {
        public SectionRebarTieForm()
        {
            InitializeComponent();
        }
        Form sectionDrawForm = Application.OpenForms["SectionDrawForm"];
        private void SectionRebarTieForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int SelectedClip = ((SectionDrawForm)sectionDrawForm).SelectedClip;
            Clips emp0 = new Clips();
            emp0 = ((SectionDrawForm)sectionDrawForm).Clip[SelectedClip];
            emp0.Dir1 = 1;


            ((SectionDrawForm)sectionDrawForm).Clip[SelectedClip] = emp0;
            ((SectionDrawForm)sectionDrawForm).MakeTempFiles();
            ((SectionDrawForm)sectionDrawForm).Render2d();

            this.Close();
        }
    }
}
