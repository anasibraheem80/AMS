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
    public partial class LoadCombinationForm : Form
    {
        public LoadCombinationForm()
        {
            InitializeComponent();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            Myglobals.EditOrNew = 1;
            LoadsCombination.Selected = 0;
            LoadCombinationDataForm loadCombinationDataForm = new LoadCombinationDataForm();
            loadCombinationDataForm.ShowDialog();
        }
        private void LoadCombinationForm_Load(object sender, EventArgs e)
        {
            if (LoadsCombination.Number > 0)
            {
                LoadsCombination.Numbertemp = LoadsCombination.Number;
                for (int i = 1; i < LoadsCombination.Number + 1; i++)
                {
                    listBox1.Items.Add(LoadsCombination.Name[i]);
                    LoadsCombination.Nametemp[i] = LoadsCombination.Name[i];
                    LoadsCombination.Typetemp[i] = LoadsCombination.Type[i];
                    LoadsCombination.NumberRowtemp[i] = LoadsCombination.NumberRow[i];
                    for (int j = 1; j < LoadsCombination.NumberRowtemp[i] + 1; j++)
                    {
                        LoadsCombination.LoadNametemp[i, j] = LoadsCombination.LoadName[i, j];
                        LoadsCombination.ScaleFactortemp[i, j] = LoadsCombination.ScaleFactor[i, j];
                    }
                }
                listBox1.SetSelected(0, true);
            }
            LoadsCombination.Selected = 1;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoadsCombination.Number = LoadsCombination.Numbertemp;
            for (int i = 1; i < LoadsCombination.Number + 1; i++)
            {
                LoadsCombination.Name[i] = LoadsCombination.Nametemp[i];
                LoadsCombination.Type[i] = LoadsCombination.Typetemp[i];
                LoadsCombination.NumberRow[i] = LoadsCombination.NumberRowtemp[i];
                for (int j = 1; j < LoadsCombination.NumberRow[i] + 1; j++)
                {
                    LoadsCombination.LoadName[i, j] = LoadsCombination.LoadNametemp[i, j];
                    LoadsCombination.ScaleFactor[i, j] = LoadsCombination.ScaleFactortemp[i, j];
                }
            }
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex + 1;
            LoadsCombination.Selected = i;
            Myglobals.EditOrNew = 3;
            LoadCombinationDataForm loadCombinationDataForm = new LoadCombinationDataForm();
            loadCombinationDataForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex + 1;
            LoadsCombination.Selected = i;
            Myglobals.EditOrNew = 2;
            LoadCombinationDataForm loadCombinationDataForm = new LoadCombinationDataForm();
            loadCombinationDataForm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (LoadsCombination.Numbertemp > 1)
            {
                LoadsCombination.Numbertemp = LoadsCombination.Numbertemp - 1;
                int m = listBox1.SelectedIndex + 1;
                for (int i = m; i < LoadsCombination.Numbertemp + 1; i++)
                {
                    LoadsCombination.Nametemp[i] = LoadsCombination.Nametemp[i+1];
                    LoadsCombination.Typetemp[i] = LoadsCombination.Typetemp[i+1];
                    LoadsCombination.NumberRowtemp[i] = LoadsCombination.NumberRowtemp[i+1];
                    for (int j = 1; j < LoadsCombination.NumberRowtemp[i] + 1; j++)
                    {
                        LoadsCombination.LoadNametemp[i, j] = LoadsCombination.LoadNametemp[i+1, j];
                        LoadsCombination.ScaleFactortemp[i, j] = LoadsCombination.ScaleFactortemp[i+1, j];
                    }
                }
                LoadsCombination.Selected = listBox1.SelectedIndex;
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                if (LoadsCombination.Selected < listBox1.Items.Count)
                {
                    listBox1.SetSelected(LoadsCombination.Selected, true);
                }
                if (LoadsCombination.Selected == listBox1.Items.Count)
                {
                    listBox1.SetSelected(LoadsCombination.Selected - 1, true);
                }
            }
        }
    }
}
