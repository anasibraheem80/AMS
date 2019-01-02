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
    public partial class StoryFrmAdd : Form
    {
        public StoryFrmAdd()
        {
            InitializeComponent();
        }
        Form storyFrm = Application.OpenForms["StoryFrm"];
        private void FloorFrmAdd_Load(object sender, EventArgs e)
        {
            for (int i = Myglobals.StoryNumbersTemp; i > 0; i--)
            {
                comboBox1.Items.Add(Myglobals.StoryNameTemp[i]);
            }
             comboBox1.Items.Add("Base");
            comboBox1.SelectedIndex = 0;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            double theheight = Convert.ToDouble(textBox1.Text);
            int AddedNumber = Convert.ToInt32(textBox2.Text);
            int Above = Myglobals.StoryNumbersTemp - (comboBox1.SelectedIndex);
            int m = 0;
            double[] height = new double[Myglobals.StoryNumbersTemp + AddedNumber + 1];
            double[] level = new double[Myglobals.StoryNumbersTemp + AddedNumber + 1];
            string[] name = new string[Myglobals.StoryNumbersTemp + AddedNumber + 1];
            for (int i = 1; i < Above + 1; i++)
            {
                height[i] = Myglobals.StoryHightTemp[i];
                name[i] = Myglobals.StoryNameTemp[i];
            }
            for (int i = Above + 1; i < Above + AddedNumber + 1; i++)
            {
                height[i] = theheight;
                name[i] = "Story "+i;
            }
            for (int i = Above + AddedNumber + 1; i < Myglobals.StoryNumbersTemp + AddedNumber + 1; i++)
            {
                m = i - AddedNumber;
                height[i] = Myglobals.StoryHightTemp[m];
                name[i] = Myglobals.StoryNameTemp[m];
            }
            level[1] = height[1];
            for (int i = 2; i < Myglobals.StoryNumbersTemp + AddedNumber + 1; i++)
            {
                level[i] = level[i - 1] + height[i];
            }

            Myglobals.StoryNumbersTemp = Myglobals.StoryNumbersTemp + AddedNumber;
            for (int i = 1; i < Myglobals.StoryNumbersTemp + 1; i++)
            {
                Myglobals.StoryNameTemp[i] = name[i];
                Myglobals.StoryHightTemp[i] = height[i - 1];
                Myglobals.StoryLevelTemp[i] = level[i - 1];
            }
            ((StoryFrm)storyFrm).dataGridView1.Rows.Clear();
            for (int i = Myglobals.StoryNumbersTemp; i > 0; i--)
            {
                ((StoryFrm)storyFrm).dataGridView1.Rows.Add(name[i], height[i], level[i]);
            }
            ((StoryFrm)storyFrm).dataGridView1.Rows.Add("Base", "0", "0");
            this.Close();
        }
    }
}
