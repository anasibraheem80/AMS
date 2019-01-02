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
    public partial class WallPropertiesDataForm : Form
    {
        public WallPropertiesDataForm()
        {
            InitializeComponent();
        }
        Form wallProperteisForm = Application.OpenForms["WallPropertiesForm"];
        private void WallPropertiesDataForm_Load(object sender, EventArgs e)
        {
            for (int j = 1; j < Material.Number + 1; j++)
            {
                Materialtxt.Items.Add(Material.Name[j]);
            }
            Materialtxt.SelectedIndex = 0;
            MTypetxt.SelectedIndex = 0;
            ProTypetxt.SelectedIndex = 0;
            int i = Wall.Selected;
            Nametxt.Text = Wall.Named[i];
            if (Wall.Materiald[i] > 0) Materialtxt.SelectedIndex = Wall.Materiald[i] - 1;
            if (Wall.MTyped[i] > 0) MTypetxt.SelectedIndex = Wall.MTyped[i] - 1;
            if (Wall.ProTyped[i] > 0) ProTypetxt.SelectedIndex = Wall.ProTyped[i] - 1;
            Thicknesstxt.Text = Wall.Thicknessd[i].ToString();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            int i = Wall.Selected;
            if (Myglobals.EditOrNew == 1 || Myglobals.EditOrNew == 3)
            {
                Wall.Numberd = Wall.Numberd + 1;
                Wall.Selected = Wall.Numberd;
                i = Wall.Numberd;
            }
            Wall.Named[i] = Nametxt.Text;
            Wall.Materiald[i] = Materialtxt.SelectedIndex + 1;
            Wall.MTyped[i] = MTypetxt.SelectedIndex + 1;
            Wall.ProTyped[i] = ProTypetxt.SelectedIndex + 1;
            Wall.Thicknessd[i] = Convert.ToDouble(Thicknesstxt.Text);
            if (Myglobals.EditOrNew == 2)//تعديل
            {
                ((WallPropertiesForm)wallProperteisForm).listBox1.Items.Insert(((WallPropertiesForm)wallProperteisForm).listBox1.SelectedIndex, Wall.Named[i]);
                ((WallPropertiesForm)wallProperteisForm).listBox1.Items.Remove(((WallPropertiesForm)wallProperteisForm).listBox1.SelectedItem);
                ((WallPropertiesForm)wallProperteisForm).listBox1.SetSelected(Wall.Selected - 1, true);
            }
            if (Myglobals.EditOrNew == 1 || Myglobals.EditOrNew == 3)//جديد
            {
                ((WallPropertiesForm)wallProperteisForm).listBox1.Items.Add(Wall.Named[i]);
                ((WallPropertiesForm)wallProperteisForm).listBox1.SetSelected(Wall.Selected - 1, true);
            }
            this.Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
