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
    public partial class MterialPropertyDataForm : Form
    {
        public MterialPropertyDataForm()
        {
            InitializeComponent();
        }
        Form defineMaterialsForm = Application.OpenForms["DefineMaterialsForm"];
        private void button1_Click(object sender, EventArgs e)
        {
            if (MaterialTypecmb.SelectedIndex == 0)
            {
                fctxt.Text ="0" ;
            }
            if (MaterialTypecmb.SelectedIndex == 1)
            {
                MinYeildFytxt.Text = "0";
                MinTensileFutxt.Text = "0";
                EffYeildFyetxt.Text = "0";
                EffTensileFuetxt.Text = "0";
            }
            int i = Material.Selected;
            if (Myglobals.EditOrNew == 1 || Myglobals.EditOrNew == 3)
            {
                Material.Numberd = Material.Numberd + 1;
                i = Material.Numberd;
                Material.Selected = Material.Numberd;
            }
            Material.Named[i] = Nametxt.Text;
            Material.MTyped[i] = MaterialTypecmb.SelectedIndex;
            Material.Summetryd[i] = DirectionalSummetryTypecmb.SelectedIndex;
            Material.WperVd[i] = Convert.ToDouble(WperVtxt.Text);
            Material.MperVd[i] = Convert.ToDouble(MperVtxt.Text);
            Material.Elastisityd[i] = Convert.ToDouble(Elastisitytxt.Text);
            Material.Poissond[i] = Convert.ToDouble(Poissontxt.Text);
            Material.Thermald[i] = Convert.ToDouble(Thermaltxt.Text);
            Material.ShearMd[i] = Convert.ToDouble(ShearMtxt.Text);
            Material.fcd[i] = Convert.ToDouble(fctxt.Text);
            Material.LweightCond[i] = 0;
            if (LweightConchc.Checked == true) Material.LweightCond[i] = 1;
            Material.MinYeildFyd[i] = Convert.ToDouble(MinYeildFytxt.Text);
            Material.MinTensileFud[i] = Convert.ToDouble(MinTensileFutxt.Text);
            Material.EffYeildFyed[i] = Convert.ToDouble(EffYeildFyetxt.Text);
            Material.EffTensileFued[i] = Convert.ToDouble(EffTensileFuetxt.Text);
            if (Myglobals.EditOrNew == 2)//تعديل
            {
                ((DefineMaterialsForm)defineMaterialsForm).listBox1.Items.Insert(((DefineMaterialsForm)defineMaterialsForm).listBox1.SelectedIndex, Material.Named[i]);
                ((DefineMaterialsForm)defineMaterialsForm).listBox1.Items.Remove(((DefineMaterialsForm)defineMaterialsForm).listBox1.SelectedItem);
                ((DefineMaterialsForm)defineMaterialsForm).listBox1.SetSelected(Material.Selected - 1, true);
            }
            if (Myglobals.EditOrNew == 1 || Myglobals.EditOrNew == 3)//جديد
            {
                ((DefineMaterialsForm)defineMaterialsForm).listBox1.Items.Add(Material.Named[i]);
                ((DefineMaterialsForm)defineMaterialsForm).listBox1.SetSelected(Material.Selected - 1, true);
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void MterialPropertyDataForm_Load(object sender, EventArgs e)
        {
            int i = Material.Selected ;
            MaterialTypecmb.SelectedIndex =0;
            DirectionalSummetryTypecmb.SelectedIndex = 0;
            if (Myglobals.EditOrNew == 2 || Myglobals.EditOrNew == 3)
            {
                Nametxt.Text = Material.Named[i];
                MaterialTypecmb.SelectedIndex = Material.MTyped[i];
                DirectionalSummetryTypecmb.SelectedIndex = Material.Summetryd[i];
                WperVtxt.Text = (Material.WperVd[i]).ToString();
                MperVtxt.Text = (Material.MperVd[i]).ToString();
                //Elastisitytxt.Text = (Material.Elastisityd[i]).ToString();
                Elastisitytxt.Text = (2E+11).ToString();

                Poissontxt.Text = (Material.Poissond[i]).ToString();
                Thermaltxt.Text = (Material.Thermald[i]).ToString();
                //ShearMtxt.Text = (Material.ShearMd[i]).ToString();
                ShearMtxt.Text = (76920000000).ToString();

                fctxt.Text = (Material.fcd[i]).ToString();
                if (Material.LweightCond[i] == 1) LweightConchc.Checked = true;
                MinYeildFytxt.Text = (Material.MinYeildFyd[i]).ToString();
                MinTensileFutxt.Text = (Material.MinTensileFud[i]).ToString();
                EffYeildFyetxt.Text = (Material.EffYeildFyed[i]).ToString();
                EffTensileFuetxt.Text = (Material.EffTensileFued[i]).ToString();
            }
        }

        private void MaterialTypecmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MaterialTypecmb.SelectedIndex == 0)
            {
                groupBox2.Visible = false;
                groupBox3.Visible = true;
            }
            if (MaterialTypecmb.SelectedIndex == 1)
            {
                groupBox2.Visible = true;
                groupBox3.Visible = false;
            }
        }
    }
}
