using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace AMSPRO
{
    public partial class SlabProperteisForm : Form
    {
        public SlabProperteisForm()
        {
            InitializeComponent();
        }
        Form mainForm = Application.OpenForms["MainForm"];
        private void button7_Click(object sender, EventArgs e)
        {
            Myglobals.EditOrNew = 1;
            Slab.Selected = 0;
            SlabPropertyDataForm slabPropertyDataForm = new SlabPropertyDataForm();
            slabPropertyDataForm.ShowDialog();
        }
        private void SlabProperteisForm_Load(object sender, EventArgs e)
        {
            Slab.Numberd = Slab.Number;
            for (int i = 1; i < Slab.Number + 1; i++)
            {
                listBox1.Items.Add(Slab.Name[i]);
                Slab.Named[i]=Slab.Name[i];
                Slab.Materiald[i] = Slab.Material[i];
                Slab.MTyped[i] = Slab.MType[i];
                Slab.OneWayd[i] = Slab.OneWay[i];
                Slab.ProTyped[i] = Slab.ProType[i];
                Slab.Thicknessd[i] = Slab.Thickness[i];
                Slab.OverallDepthd[i] = Slab.OverallDepth[i];
                Slab.SlabThicknessd[i] = Slab.SlabThickness[i];
                Slab.StemWidthatTopd[i] = Slab.StemWidthatTop[i];
                Slab.StemWidthatBottomd[i] = Slab.StemWidthatBottom[i];
                Slab.RibSpacingd[i] = Slab.RibSpacing[i];
                Slab.RibDirectiond[i] = Slab.RibDirection[i];
                Slab.RibSpacing1d[i] = Slab.RibSpacing1[i];
                Slab.RibSpacing2d[i] = Slab.RibSpacing2[i];
            }
            listBox1.SetSelected(0, true);
            Slab.Selected = 1;
        }
        private void button8_Click(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex + 1;
            Slab.Selected = i;
            Myglobals.EditOrNew = 3;
            SlabPropertyDataForm slabPropertyDataForm = new SlabPropertyDataForm();
            slabPropertyDataForm.ShowDialog();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex + 1;
            Slab.Selected = i;
            Myglobals.EditOrNew = 2;
            SlabPropertyDataForm slabPropertyDataForm = new SlabPropertyDataForm();
            slabPropertyDataForm.ShowDialog();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (Slab.Numberd > 1)
            {
                Slab.Numberd = Slab.Numberd - 1;
                int m = listBox1.SelectedIndex + 1;
                for (int i = m; i < Slab.Numberd + 1; i++)
                {
                    Slab.Named[i] = Slab.Named[i+1];
                    Slab.Materiald[i] = Slab.Materiald[i+1];
                    Slab.MTyped[i] = Slab.MTyped[i+1];
                    Slab.OneWayd[i] = Slab.OneWayd[i+1];
                    Slab.ProTyped[i] = Slab.ProTyped[i+1];
                    Slab.Thicknessd[i] = Slab.Thicknessd[i+1];
                    Slab.OverallDepthd[i] = Slab.OverallDepthd[i+1];
                    Slab.SlabThicknessd[i] = Slab.SlabThicknessd[i+1];
                    Slab.StemWidthatTopd[i] = Slab.StemWidthatTopd[i+1];
                    Slab.StemWidthatBottomd[i] = Slab.StemWidthatBottomd[i+1];
                    Slab.RibSpacingd[i] = Slab.RibSpacingd[i+1];
                    Slab.RibDirectiond[i] = Slab.RibDirectiond[i+1];
                    Slab.RibSpacing1d[i] = Slab.RibSpacing1d[i+1];
                    Slab.RibSpacing2d[i] = Slab.RibSpacing2d[i+1];
                }
                Slab.Selected = listBox1.SelectedIndex;
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                if (Slab.Selected < listBox1.Items.Count)
                {
                    listBox1.SetSelected(Slab.Selected, true);
                }
                if (Slab.Selected == listBox1.Items.Count)
                {
                    listBox1.SetSelected(Slab.Selected - 1, true);
                }
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                string strpath = Convert.ToString(Directory.GetParent(@"./Property Libraries/MySlabs/"));
                StreamWriter SW = new StreamWriter(strpath);
                SW.WriteLine(Slab.Numberd);
                for (int add = 1; add < Slab.Numberd + 1; add++)
                {
                    SW.WriteLine(Slab.Named[add]);
                    SW.WriteLine(Slab.Materiald[add]);
                    SW.WriteLine(Slab.MTyped[add]);
                    SW.WriteLine(Slab.OneWayd[add]);
                    SW.WriteLine(Slab.ProTyped[add]);
                    SW.WriteLine(Slab.Thicknessd[add]);
                    SW.WriteLine(Slab.OverallDepthd[add]);
                    SW.WriteLine(Slab.SlabThicknessd[add]);
                    SW.WriteLine(Slab.StemWidthatTopd[add]);
                    SW.WriteLine(Slab.StemWidthatBottomd[add]);
                    SW.WriteLine(Slab.RibSpacingd[add]);
                    SW.WriteLine(Slab.RibDirectiond[add]);
                    SW.WriteLine(Slab.RibSpacing1d[add]);
                    SW.WriteLine(Slab.RibSpacing2d[add]);
                }
                SW.Close();
            }
            catch
            {
                MessageBox.Show("ERROR! Please check passphrase and do not attempt to edit cipher text");
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Slab.Number = Slab.Numberd;
            for (int i = 1; i < Slab.Number + 1; i++)
            {
                Slab.Name[i] = Slab.Named[i];
                Slab.Material[i] = Slab.Materiald[i];
                Slab.MType[i] = Slab.MTyped[i];
                Slab.OneWay[i] = Slab.OneWayd[i];
                Slab.ProType[i] = Slab.ProTyped[i];
                Slab.Thickness[i] = Slab.Thicknessd[i];
                Slab.OverallDepth[i] = Slab.OverallDepthd[i];
                Slab.SlabThickness[i] = Slab.SlabThicknessd[i];
                Slab.StemWidthatTop[i] = Slab.StemWidthatTopd[i];
                Slab.StemWidthatBottom[i] = Slab.StemWidthatBottomd[i];
                Slab.RibSpacing[i] = Slab.RibSpacingd[i];
                Slab.RibDirection[i] = Slab.RibDirectiond[i];
                Slab.RibSpacing1[i] = Slab.RibSpacing1d[i];
                Slab.RibSpacing2[i] = Slab.RibSpacing2d[i];
            }
            if (Myglobals.PropertyGridchoice == 2)
            {
               // DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)((MainForm)mainForm).PropertyGrid.Rows[0].Cells[1];
               // cbCell.Items.Clear();
               // for (int i = 1; i < Slab.Number + 1; i++)
                //{
                //    cbCell.Items.Add(Slab.Name[i]);
                //}
                //cbCell.Value = cbCell.Items[0];
                Slab.SelectedToDraw = 1;
            }
            this.Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
