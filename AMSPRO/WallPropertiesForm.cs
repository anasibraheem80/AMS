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
    public partial class WallPropertiesForm : Form
    {
        public WallPropertiesForm()
        {
            InitializeComponent();
        }
        private void WallPropertiesForm_Load(object sender, EventArgs e)
        {
            Wall.Numberd = Wall.Number;
            for (int i = 1; i < Wall.Number + 1; i++)
            {
                listBox1.Items.Add(Wall.Name[i]);
                Wall.Named[i] = Wall.Name[i];
                Wall.Materiald[i] = Wall.Material[i];
                Wall.MTyped[i] = Wall.MType[i];
                Wall.ProTyped[i] = Wall.ProType[i];
                Wall.Thicknessd[i] = Wall.Thickness[i];
            }
            listBox1.SetSelected(0, true);
            Wall.Selected = 1;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            Myglobals.EditOrNew = 1;
            Wall.Selected = 0;
            WallPropertiesDataForm wallPropertiesDataForm = new WallPropertiesDataForm();
            wallPropertiesDataForm.ShowDialog();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex + 1;
            Wall.Selected = i;
            Myglobals.EditOrNew = 3;
            WallPropertiesDataForm wallPropertiesDataForm = new WallPropertiesDataForm();
            wallPropertiesDataForm.ShowDialog();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex + 1;
            Wall.Selected = i;
            Myglobals.EditOrNew = 2;
            WallPropertiesDataForm wallPropertiesDataForm = new WallPropertiesDataForm();
            wallPropertiesDataForm.ShowDialog();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (Wall.Numberd > 1)
            {
                Wall.Numberd = Wall.Numberd - 1;
                int m = listBox1.SelectedIndex + 1;
                for (int i = m; i < Wall.Numberd + 1; i++)
                {
                    Wall.Named[i] = Wall.Named[i + 1];
                    Wall.Materiald[i] = Wall.Materiald[i + 1];
                    Wall.MTyped[i] = Wall.MTyped[i + 1];
                    Wall.ProTyped[i] = Wall.ProTyped[i + 1];
                    Wall.Thicknessd[i] = Wall.Thicknessd[i + 1];
                }
                Wall.Selected = listBox1.SelectedIndex;
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                if (Wall.Selected < listBox1.Items.Count)
                {
                    listBox1.SetSelected(Wall.Selected, true);
                }
                if (Wall.Selected == listBox1.Items.Count)
                {
                    listBox1.SetSelected(Wall.Selected - 1, true);
                }
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Wall.Number = Wall.Numberd;
            for (int i = 1; i < Wall.Number + 1; i++)
            {
                Wall.Name[i] = Wall.Named[i];
                Wall.Material[i] = Wall.Materiald[i];
                Wall.MType[i] = Wall.MTyped[i];

                Wall.ProType[i] = Wall.ProTyped[i];
                Wall.Thickness[i] = Wall.Thicknessd[i];
            }
            if (Myglobals.PropertyGridchoice == 3)
            {
                // DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)((MainForm)mainForm).PropertyGrid.Rows[0].Cells[1];
                // cbCell.Items.Clear();
                // for (int i = 1; i < Slab.Number + 1; i++)
                //{
                //    cbCell.Items.Add(Slab.Name[i]);
                //}
                //cbCell.Value = cbCell.Items[0];
                Wall.SelectedToDraw = 1;
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
                string strpath = Convert.ToString(Directory.GetParent(@"./Property Libraries/MyWalls/"));
                StreamWriter SW = new StreamWriter(strpath);
                SW.WriteLine(Wall.Numberd);
                for (int add = 1; add < Wall.Numberd + 1; add++)
                {
                    SW.WriteLine(Wall.Named[add]);
                    SW.WriteLine(Wall.Materiald[add]);
                    SW.WriteLine(Wall.MTyped[add]);
                    SW.WriteLine(Wall.ProTyped[add]);
                    SW.WriteLine(Wall.Thicknessd[add]);
                }
                SW.Close();
            }
            catch
            {
                MessageBox.Show("ERROR! Please check passphrase and do not attempt to edit cipher text");
            }
        }
    }
}
