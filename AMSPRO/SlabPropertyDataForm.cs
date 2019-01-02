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
    public partial class SlabPropertyDataForm : Form
    {
        public SlabPropertyDataForm()
        {
            InitializeComponent();
        }
        Form slabProperteisForm = Application.OpenForms["SlabProperteisForm"];
        private void SlabPropertyDataForm_Load(object sender, EventArgs e)
        {
            for (int j = 1; j < Material.Number + 1; j++)
            {
                Materialtxt.Items.Add(Material.Name[j]);
            }
            Materialtxt.SelectedIndex = 0;
            MTypetxt.SelectedIndex = 0;
            ProTypetxt.SelectedIndex = 0;
            RibDirectiontxt.SelectedIndex = 0;
            int i = Slab.Selected;
            Nametxt.Text = Slab.Named[i];
            if (Slab.Materiald[i] > 0) Materialtxt.SelectedIndex = Slab.Materiald[i] - 1;
            if (Slab.MTyped[i] > 0) MTypetxt.SelectedIndex = Slab.MTyped[i] - 1;
            if (Slab.OneWayd[i] == 0) OneWaytxt.Checked = false;
            if (Slab.ProTyped[i] > 0) ProTypetxt.SelectedIndex = Slab.ProTyped[i]-1;
            Thicknesstxt.Text = Slab.Thicknessd[i].ToString();
            OverallDepthtxt.Text = Slab.OverallDepthd[i].ToString();
            SlabThicknesstxt.Text = Slab.SlabThicknessd[i].ToString();
            StemWidthatToptxt.Text = Slab.StemWidthatTopd[i].ToString();
            StemWidthatBottomtxt.Text = Slab.StemWidthatBottomd[i].ToString();
            RibSpacingdtxt.Text = Slab.RibSpacingd[i].ToString();
            if (Slab.RibDirectiond[i] > 0) RibDirectiontxt.SelectedIndex = Slab.RibDirectiond[i] - 1;
            RibSpacing1txt.Text = Slab.RibSpacing1d[i].ToString();
            RibSpacing2txt.Text = Slab.RibSpacing2d[i].ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int i = Slab.Selected;
            if (Myglobals.EditOrNew == 1 || Myglobals.EditOrNew == 3)
            {
                Slab.Numberd = Slab.Numberd + 1;
                Slab.Selected = Slab.Numberd;
                i = Slab.Numberd;
            }
            Slab.Named[i] = Nametxt.Text;
            Slab.Materiald[i] = Materialtxt.SelectedIndex + 1;
            Slab.MTyped[i] = MTypetxt.SelectedIndex + 1;
            Slab.OneWayd[i] = 0;
            if (OneWaytxt.Checked == true) Slab.OneWayd[i] = 1;
            Slab.ProTyped[i] = ProTypetxt.SelectedIndex + 1;
            Slab.Thicknessd[i] = Convert.ToDouble(  Thicknesstxt.Text);
            Slab.OverallDepthd[i] =Convert.ToDouble( OverallDepthtxt.Text);
            Slab.SlabThicknessd[i] = Convert.ToDouble(SlabThicknesstxt.Text);
            Slab.StemWidthatTopd[i] =Convert.ToDouble( StemWidthatToptxt.Text);
            Slab.StemWidthatBottomd[i] =Convert.ToDouble( StemWidthatBottomtxt.Text);
            Slab.RibSpacingd[i] =Convert.ToDouble( RibSpacingdtxt.Text);
            Slab.RibDirectiond[i] = RibDirectiontxt.SelectedIndex + 1;
            Slab.RibSpacing1d[i] =Convert.ToDouble( RibSpacing1txt.Text);
            Slab.RibSpacing2d[i] = Convert.ToDouble(RibSpacing2txt.Text);

            if (Myglobals.EditOrNew == 2)//تعديل
            {
                ((SlabProperteisForm)slabProperteisForm).listBox1.Items.Insert(((SlabProperteisForm)slabProperteisForm).listBox1.SelectedIndex, Slab.Named[i]);
                ((SlabProperteisForm)slabProperteisForm).listBox1.Items.Remove(((SlabProperteisForm)slabProperteisForm).listBox1.SelectedItem);
                ((SlabProperteisForm)slabProperteisForm).listBox1.SetSelected(Slab.Selected - 1, true);
            }
            if (Myglobals.EditOrNew == 1 || Myglobals.EditOrNew == 3)//جديد
            {
                ((SlabProperteisForm)slabProperteisForm).listBox1.Items.Add(Slab.Named[i]);
                ((SlabProperteisForm)slabProperteisForm).listBox1.SetSelected(Slab.Selected - 1, true);
            }

            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MTypetxt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MTypetxt.SelectedIndex == 2)
            {
                OneWaytxt.Visible = true;
            }
            else
            {
                OneWaytxt.Visible = false;
            }

        }

        private void ProTypetxt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ProTypetxt.SelectedIndex == 0 || ProTypetxt.SelectedIndex == 1)
            {
                Thicknesstxt.Visible = true;
                Thicknesslbl.Visible = true;
                OverallDepthtxt.Visible = false;
                OverallDepthlbl.Visible = false;
                SlabThicknesstxt.Visible = false;
                SlabThicknesslbl.Visible = false;
                StemWidthatToptxt.Visible = false;
                StemWidthatToplbl.Visible = false;
                StemWidthatBottomtxt.Visible = false;
                StemWidthatBottomlbl.Visible = false;
                RibSpacingdtxt.Visible = false;
                RibSpacingdlbl.Visible = false;
                RibDirectiontxt.Visible = false;
                RibDirectionlbl.Visible = false;
                RibSpacing1txt.Visible = false;
                RibSpacing1lbl.Visible = false;
                RibSpacing2txt.Visible = false;
                RibSpacing2lbl.Visible = false;
            }
            if (ProTypetxt.SelectedIndex == 2 )
            {
                Thicknesstxt.Visible = false;
                Thicknesslbl.Visible = false;
                OverallDepthtxt.Visible = true;
                OverallDepthlbl.Visible = true;
                SlabThicknesstxt.Visible = true;
                SlabThicknesslbl.Visible = true;
                StemWidthatToptxt.Visible = true;
                StemWidthatToplbl.Visible = true;
                StemWidthatBottomtxt.Visible = true;
                StemWidthatBottomlbl.Visible = true;
                RibSpacingdtxt.Visible = true;
                RibSpacingdlbl.Visible = true;
                RibDirectiontxt.Visible = true;
                RibDirectionlbl.Visible = true;
                RibSpacing1txt.Visible = false;
                RibSpacing1lbl.Visible = false;
                RibSpacing2txt.Visible = false;
                RibSpacing2lbl.Visible = false;
            }
            if (ProTypetxt.SelectedIndex == 3)
            {
                Thicknesstxt.Visible = false;
                Thicknesslbl.Visible = false;
                OverallDepthtxt.Visible = true;
                OverallDepthlbl.Visible = true;
                SlabThicknesstxt.Visible = true;
                SlabThicknesslbl.Visible = true;
                StemWidthatToptxt.Visible = true;
                StemWidthatToplbl.Visible = true;
                StemWidthatBottomtxt.Visible = true;
                StemWidthatBottomlbl.Visible = true;
                RibSpacingdtxt.Visible = false;
                RibSpacingdlbl.Visible = false;
                RibDirectiontxt.Visible = false;
                RibDirectionlbl.Visible = false;
                RibSpacing1txt.Visible = true;
                RibSpacing1lbl.Visible = true;
                RibSpacing2txt.Visible = true;
                RibSpacing2lbl.Visible = true;
            }
        }
    }
}
