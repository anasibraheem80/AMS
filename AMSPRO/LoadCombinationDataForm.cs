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
    public partial class LoadCombinationDataForm : Form
    {
        public LoadCombinationDataForm()
        {
            InitializeComponent();
        }
        Form loadCombinationForm = Application.OpenForms["LoadCombinationForm"];
        int thenumber = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add("", "1");
            DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)dataGridView1.Rows[thenumber].Cells[0];
            for (int i = 1; i < Loads.Number + 1; i++)
            {
                cbCell.Items.Add(Loads.Load[i]);
            }
            cbCell.Value = cbCell.Items[0];
            thenumber = thenumber + 1;
        }

        private void LoadCombinationDataForm_Load(object sender, EventArgs e)
        {
            int i = LoadsCombination.Selected;
            //if (i > 0)
            {
                textBox1.Text = LoadsCombination.Nametemp[i];
                comboBox1.Text = LoadsCombination.Typetemp[i];
                thenumber = LoadsCombination.NumberRowtemp[i];

                for (int j = 1; j < thenumber + 1; j++)
                {
                    dataGridView1.Rows.Add("", LoadsCombination.ScaleFactortemp[i, j]);
                    DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)dataGridView1.Rows[j-1].Cells[0];
                    for (int k = 1; k < Loads.Number + 1; k++)
                    {
                        cbCell.Items.Add(Loads.Load[k]);
                    }
                    cbCell.Value = LoadsCombination.LoadNametemp[i, j];
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int i = LoadsCombination.Selected;
            if (Myglobals.EditOrNew == 1 || Myglobals.EditOrNew == 3)
            {
                LoadsCombination.Numbertemp = LoadsCombination.Numbertemp + 1;
                LoadsCombination.Selected = LoadsCombination.Numbertemp;
                i = LoadsCombination.Numbertemp;
            }
            
            LoadsCombination.Nametemp[i] = textBox1.Text;
            LoadsCombination.Typetemp[i] = comboBox1.Text;
            LoadsCombination.NumberRowtemp[i] = thenumber;
            for (int j = 1; j < thenumber + 1; j++)
            {
                LoadsCombination.LoadNametemp[i, j] = Convert.ToString(dataGridView1.Rows[j - 1].Cells[0].Value);
                LoadsCombination.ScaleFactortemp[i, j] = Convert.ToDouble(dataGridView1.Rows[j - 1].Cells[1].Value);
            }

            if (Myglobals.EditOrNew == 2)//تعديل
            {
                ((LoadCombinationForm)loadCombinationForm).listBox1.Items.Insert(((LoadCombinationForm)loadCombinationForm).listBox1.SelectedIndex, LoadsCombination.Nametemp[i]);
                ((LoadCombinationForm)loadCombinationForm).listBox1.Items.Remove(((LoadCombinationForm)loadCombinationForm).listBox1.SelectedItem);
                ((LoadCombinationForm)loadCombinationForm).listBox1.SetSelected(LoadsCombination.Selected - 1, true);
            }
            if (Myglobals.EditOrNew == 1 || Myglobals.EditOrNew == 3)//جديد
            {
                ((LoadCombinationForm)loadCombinationForm).listBox1.Items.Add(LoadsCombination.Nametemp[i]);
                ((LoadCombinationForm)loadCombinationForm).listBox1.SetSelected(LoadsCombination.Selected - 1, true);
            }

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentCell.RowIndex);
            }
        }
    }
}
