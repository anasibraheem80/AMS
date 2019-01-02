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
    public partial class ArchWallFormALL : Form
    {
        public ArchWallFormALL()
        {
            InitializeComponent();
        }
        Form mainForm = Application.OpenForms["MainForm"];
        private void button7_Click(object sender, EventArgs e)
        {
            Myglobals.EditOrNew = 1;
            Section.Selected = 0;
            ArchWallForm theform = new ArchWallForm();
            theform.ShowDialog();
        }

        private void ArchWallFormALL_Load(object sender, EventArgs e)
        {
            if (Myglobals.ArchWallNumber > 0)
            {
                for (int i = 1; i < Myglobals.ArchWallNumber + 1; i++)
                {
                    listBox1.Items.Add(((MainForm)mainForm).ArchWall[i].Name);
                }
                listBox1.SetSelected(0, true);
                ((MainForm)mainForm).ArchWall[1].Selected = 1;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
            this.Close(); 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex + 1;
            Section.Selected = i;
            Myglobals.EditOrNew = 3;
            if (i > 0)
            {
                ArchWallForm theform = new ArchWallForm();
                theform.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex + 1;
            Section.Selected = i;
            Myglobals.EditOrNew = 2;
            if (i > 0)
            {
                ArchWallForm theform = new ArchWallForm();
                theform.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int m = listBox1.SelectedIndex + 1;
            if (m > 0)
            {
                Myglobals.ArchWallNumber = Myglobals.ArchWallNumber - 1;
                for (int i = m; i < Myglobals.ArchWallNumber + 1; i++)
                {
                    ((MainForm)mainForm).ArchWall[i] = ((MainForm)mainForm).ArchWall[i];
                }
                Section.Selected = listBox1.SelectedIndex;
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                if (Material.Selected < listBox1.Items.Count)
                {
                    listBox1.SetSelected(Section.Selected, true);
                }
                if (Section.Selected == listBox1.Items.Count)
                {
                    listBox1.SetSelected(Section.Selected - 1, true);
                }
            }
        }
    }
}
