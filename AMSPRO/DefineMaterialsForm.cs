using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

//using Microsoft.VisualBasic.PowerPacks;

//using System.Collections;


namespace AMSPRO
{
    public partial class DefineMaterialsForm : Form
    {
        public DefineMaterialsForm()
        {
            InitializeComponent();
        }
        private void DefineMaterialsForm_Load(object sender, EventArgs e)
        {
            Material.Numberd = Material.Number;
            for (int i = 1; i < Material.Number + 1; i++)
            {
                listBox1.Items.Add(Material.Name[i]);  
                Material.Named[i] = Material.Name[i];
                Material.MTyped[i] = Material.MType[i];
                Material.Summetryd[i] = Material.Summetry[i];
                Material.WperVd[i] = Material.WperV[i];
                Material.MperVd[i]= Material.MperV[i];
                Material.Elastisityd[i] = Material.Elastisity[i];
                Material.Poissond[i] = Material.Poisson[i];
                Material.Thermald[i] = Material.Thermal[i];
                Material.ShearMd[i]= Material.ShearM[i];
                Material.fcd[i] = Material.fc[i];
                Material.LweightCond[i] = Material.LweightCon[i];
                Material.MinYeildFyd[i] = Material.MinYeildFy[i];
                Material.MinTensileFud[i] = Material.MinTensileFu[i];
                Material.EffYeildFyed[i] = Material.EffYeildFye[i];
                Material.EffTensileFued[i] = Material.EffTensileFue[i];
            }
            listBox1.SetSelected(0, true);
            Material.Selected = 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Myglobals.EditOrNew = 2;
            Material.Selected = listBox1.SelectedIndex + 1;
            MterialPropertyDataForm mterialPropertyDataForm = new MterialPropertyDataForm();
            mterialPropertyDataForm.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Myglobals.EditOrNew = 1;
            MterialPropertyDataForm mterialPropertyDataForm = new MterialPropertyDataForm();
            mterialPropertyDataForm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Material.Number = Material.Numberd;
            for (int i = 1; i < Material.Number + 1; i++)
            {
                Material.Name[i] = Material.Named[i];
                Material.MType[i] = Material.MTyped[i];
                Material.Summetry[i] = Material.Summetryd[i];
                Material.WperV[i] = Material.WperVd[i];
                Material.MperV[i] = Material.MperVd[i];
                Material.Elastisity[i] = Material.Elastisityd[i];
                Material.Poisson[i] = Material.Poissond[i];
                Material.Thermal[i] = Material.Thermald[i];
                Material.ShearM[i] = Material.ShearMd[i];
                Material.fc[i] = Material.fcd[i];
                Material.LweightCon[i] = Material.LweightCond[i];
                Material.MinYeildFy[i] = Material.MinYeildFyd[i];
                Material.MinTensileFu[i] = Material.MinTensileFud[i];
                Material.EffYeildFye[i] = Material.EffYeildFyed[i];
                Material.EffTensileFue[i] = Material.EffTensileFued[i];
            }
            
            this.Close(); 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                string strpath = Convert.ToString(Directory.GetParent(@"./Property Libraries/MyMaterials/"));
                StreamWriter SW = new StreamWriter(strpath);
                SW.WriteLine(Material.Numberd);
                for (int add = 1; add < Material.Numberd + 1; add++)
                {
                    SW.WriteLine(Material.Named[add]);
                    SW.WriteLine(Material.MTyped[add]);
                    SW.WriteLine(Material.Summetryd[add]);
                    SW.WriteLine(Material.WperVd[add]);
                    SW.WriteLine(Material.MperVd[add]);
                    SW.WriteLine(Material.Elastisityd[add]);
                    SW.WriteLine(Material.Poissond[add]);
                    SW.WriteLine(Material.Thermald[add]);
                    SW.WriteLine(Material.ShearMd[add]);
                    SW.WriteLine(Material.fcd[add]);
                    SW.WriteLine(Material.LweightCond[add]);
                    SW.WriteLine(Material.MinYeildFyd[add]);
                    SW.WriteLine(Material.MinTensileFud[add]);
                    SW.WriteLine(Material.EffYeildFyed[add]);
                    SW.WriteLine(Material.EffTensileFued[add]);
                }
                SW.Close();
            }
            catch
            {
                MessageBox.Show("ERROR! Please check passphrase and do not attempt to edit cipher text");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int m = listBox1.SelectedIndex + 1;
            if (m > 4)
            {
                Material.Numberd = Material.Numberd - 1;
                for (int i = m; i < Material.Numberd + 1; i++)
                {
                    Material.Named[i] = Material.Named[i+1];
                    Material.MTyped[i] = Material.MTyped[i + 1];
                    Material.Summetryd[i] = Material.Summetryd[i + 1];
                    Material.WperVd[i] = Material.WperVd[i + 1];
                    Material.MperVd[i] = Material.MperVd[i + 1];
                    Material.Elastisityd[i] = Material.Elastisityd[i + 1];
                    Material.Poissond[i] = Material.Poissond[i + 1];
                    Material.Thermald[i] = Material.Thermald[i + 1];
                    Material.ShearMd[i] = Material.ShearMd[i + 1];
                    Material.fcd[i] = Material.fcd[i + 1];
                    Material.LweightCond[i] = Material.LweightCond[i + 1];
                    Material.MinYeildFyd[i] = Material.MinYeildFyd[i + 1];
                    Material.MinTensileFud[i] = Material.MinTensileFud[i + 1];
                    Material.EffYeildFyed[i] = Material.EffYeildFyed[i + 1];
                    Material.EffTensileFued[i] = Material.EffTensileFued[i + 1];
                }
                Material.Selected = listBox1.SelectedIndex;
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                if (Material.Selected < listBox1.Items.Count)
                {
                    listBox1.SetSelected(Material.Selected, true);
                }
                if (Material.Selected == listBox1.Items.Count)
                {
                    listBox1.SetSelected(Material.Selected - 1, true);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > 3)
            {
                button3.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex + 1;
            Material.Selected = i;
            Myglobals.EditOrNew = 3;
            MterialPropertyDataForm mterialPropertyDataForm = new MterialPropertyDataForm();
            mterialPropertyDataForm.ShowDialog();
        }
    }
}
