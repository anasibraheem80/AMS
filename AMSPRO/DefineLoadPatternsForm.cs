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
    public partial class DefineLoadPatternsForm : Form
    {
        public DefineLoadPatternsForm()
        {
            InitializeComponent();
        }
        int loadNumbers = 0;
        int toedit = 0;

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBox1.Text);
            listBox2.Items.Add(comboBox1.Text);
            listBox3.Items.Add(textBox2.Text);
            listBox4.Items.Add(comboBox2.Text);

            listBox1.SelectedIndex = loadNumbers;
            listBox2.SelectedIndex = loadNumbers;
            listBox3.SelectedIndex = loadNumbers;
            listBox4.SelectedIndex = loadNumbers;
            loadNumbers = loadNumbers + 1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Loads.Number = loadNumbers;
            for (int i = 1; i < Loads.Number + 1; i++)
            {
                Loads.Load[i] = listBox1.Items[i - 1].ToString();
                Loads.Type[i] = listBox2.Items[i - 1].ToString();
                Loads.SelfWeight[i] = Convert.ToDouble(listBox3.Items[i - 1].ToString());
                Loads.AutoLateral[i] = listBox4.Items[i - 1].ToString();
            }
            this.Close();
        }

        private void DefineLoadPatternsForm_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex  = 0;
            if (Loads.Number > 0)
            {
                toedit = 1;
                for (int i = 1; i < Loads.Number + 1; i++)
                {
                    listBox1.Items.Add(Loads.Load[i]);
                    listBox2.Items.Add(Loads.Type[i]);
                    listBox3.Items.Add(Loads.SelfWeight[i]);
                    listBox4.Items.Add(Loads.AutoLateral[i]);
                }
                toedit = 0;
                listBox1.SetSelected(0, true);
                listBox2.SetSelected(0, true);
                listBox3.SetSelected(0, true);
                listBox4.SetSelected(0, true);
                loadNumbers = Loads.Number;
                
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toedit == 0)
            {
                listBox2.SelectedIndex = listBox1.SelectedIndex;
                listBox3.SelectedIndex = listBox1.SelectedIndex;
                listBox4.SelectedIndex = listBox1.SelectedIndex;

                textBox1.Text = listBox1.Items[listBox1.SelectedIndex].ToString();
                comboBox1.Text = listBox2.Items[listBox1.SelectedIndex].ToString();
                textBox2.Text = listBox3.Items[listBox1.SelectedIndex].ToString();
                comboBox2.Text = listBox4.Items[listBox1.SelectedIndex].ToString();
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toedit == 0)
            {
                listBox1.SelectedIndex = listBox2.SelectedIndex;
                listBox3.SelectedIndex = listBox2.SelectedIndex;
                listBox4.SelectedIndex = listBox2.SelectedIndex;

                textBox1.Text = listBox1.Items[listBox1.SelectedIndex].ToString();
                comboBox1.Text = listBox2.Items[listBox1.SelectedIndex].ToString();
                textBox2.Text = listBox3.Items[listBox1.SelectedIndex].ToString();
                comboBox2.Text = listBox4.Items[listBox1.SelectedIndex].ToString();
            }
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toedit == 0)
            {
                listBox1.SelectedIndex = listBox3.SelectedIndex;
                listBox2.SelectedIndex = listBox3.SelectedIndex;
                listBox4.SelectedIndex = listBox3.SelectedIndex;

                textBox1.Text = listBox1.Items[listBox1.SelectedIndex].ToString();
                comboBox1.Text = listBox2.Items[listBox1.SelectedIndex].ToString();
                textBox2.Text = listBox3.Items[listBox1.SelectedIndex].ToString();
                comboBox2.Text = listBox4.Items[listBox1.SelectedIndex].ToString();
            }
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toedit == 0)
            {
                listBox1.SelectedIndex = listBox4.SelectedIndex;
                listBox3.SelectedIndex = listBox4.SelectedIndex;
                listBox2.SelectedIndex = listBox4.SelectedIndex;

                textBox1.Text = listBox1.Items[listBox1.SelectedIndex].ToString();
                comboBox1.Text = listBox2.Items[listBox1.SelectedIndex].ToString();
                textBox2.Text = listBox3.Items[listBox1.SelectedIndex].ToString();
                comboBox2.Text = listBox4.Items[listBox1.SelectedIndex].ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            toedit = 1;
            int theselected = listBox1.SelectedIndex;
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            listBox2.Items.RemoveAt(listBox2.SelectedIndex);
            listBox3.Items.RemoveAt(listBox3.SelectedIndex);
            listBox4.Items.RemoveAt(listBox4.SelectedIndex);
            loadNumbers = loadNumbers - 1;
            toedit = 0;
            if (theselected < listBox1.Items.Count)
            {
                listBox1.SetSelected(theselected, true);
                listBox2.SetSelected(theselected, true);
                listBox3.SetSelected(theselected, true);
                listBox4.SetSelected(theselected, true);
            }
            if (theselected == listBox1.Items.Count)
            {
                listBox1.SetSelected(theselected - 1, true);
                listBox2.SetSelected(theselected - 1, true);
                listBox3.SetSelected(theselected - 1, true);
                listBox4.SetSelected(theselected - 1, true);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            toedit = 1;
            int theselected = listBox1.SelectedIndex;
            listBox1.Items.Insert(listBox1.SelectedIndex, textBox1.Text);
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            listBox1.SetSelected(theselected, true);
            listBox2.Items.Insert(listBox2.SelectedIndex, comboBox1.Text);
            listBox2.Items.RemoveAt(listBox2.SelectedIndex);
            listBox2.SetSelected(theselected, true);
            listBox3.Items.Insert(listBox3.SelectedIndex, textBox2.Text);
            listBox3.Items.RemoveAt(listBox3.SelectedIndex);
            listBox3.SetSelected(theselected, true);
            listBox4.Items.Insert(listBox4.SelectedIndex, comboBox2.Text);
            listBox4.Items.RemoveAt(listBox4.SelectedIndex);
            listBox4.SetSelected(theselected, true);
            toedit = 0;
        }

        private void listBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
