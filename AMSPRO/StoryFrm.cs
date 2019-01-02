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
    public partial class StoryFrm : Form
    {
        public StoryFrm()
        {
            InitializeComponent();
        }
        Form mainForm = Application.OpenForms["MainForm"];
        private void button1_Click(object sender, EventArgs e)
        {
            Myglobals.StoryNumbers = dataGridView1.RowCount - 1;
            int m = 0;
            for (int i = Myglobals.StoryNumbers; i > 0; i--)
            {
                if (dataGridView1.Rows[m].Cells[0].Value == null) dataGridView1.Rows[m].Cells[0].Value = "";
                Myglobals.StoryName[i] = dataGridView1.Rows[m].Cells[0].Value.ToString();
                Myglobals.StoryHight[i] = Convert.ToDouble(dataGridView1.Rows[m].Cells[1].Value);
                m = m + 1;
            }
            Myglobals.StoryLevel[1] = Myglobals.StoryHight[1];
            for (int i = 2; i < Myglobals.StoryNumbers + 1; i++)
            {
                Myglobals.StoryLevel[i] = Myglobals.StoryLevel[i - 1] + Myglobals.StoryHight[i];
            }
            ((MainForm)mainForm).MakeTempFiles();
            DROWcls callmee = new DROWcls();
            callmee.CalculateGridPointReal();
            callmee.Render2d();
            callmee.Render3d();
            Myglobals.SelectedStory = Myglobals.StoryNumbers;
            ((MainForm)mainForm).tabPage1.Text = "Plan View" + "  " + "Story" + Myglobals.SelectedStory + " -Z=  " + Myglobals.StoryLevel[Myglobals.SelectedStory] + " m";
            this.Close();
        }
        private void FloorFrm_Load(object sender, EventArgs e)
        {
            for (int i = Myglobals.StoryNumbers; i > 0; i--)
            {
                dataGridView1.Rows.Add(Myglobals.StoryName[i], Myglobals.StoryHight[i], Myglobals.StoryLevel[i]);
            }
            dataGridView1.Rows.Add("Base", "0", "0");
            Myglobals.StoryNumbersTemp = Myglobals.StoryNumbers;
            Myglobals.StoryNameTemp = new string[200];
            Myglobals.StoryHightTemp = new double[200];
            Myglobals.StoryLevelTemp = new double[200];
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show();
                contextMenuStrip1.Left = Cursor.Position.X;
                contextMenuStrip1.Top = Cursor.Position.Y;
            }
        }
        private void addStoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Myglobals.StoryNumbersTemp = dataGridView1.RowCount - 1;
            int m = 0;
            for (int i = Myglobals.StoryNumbersTemp; i > 0; i--)
            {
                if (dataGridView1.Rows[m].Cells[0].Value == null) dataGridView1.Rows[m].Cells[0].Value = "";
                Myglobals.StoryNameTemp[i] = dataGridView1.Rows[m].Cells[0].Value.ToString();
                Myglobals.StoryHightTemp[i] = Convert.ToDouble(dataGridView1.Rows[m].Cells[1].Value);
                m = m + 1;
            }
            Myglobals.StoryLevelTemp[1] = Myglobals.StoryHightTemp[1];
            for (int i = 2; i < Myglobals.StoryNumbersTemp + 1; i++)
            {
                Myglobals.StoryLevelTemp[i] = Myglobals.StoryLevelTemp[i - 1] + Myglobals.StoryHightTemp[i];
            }
            StoryFrmAdd floorFrmAdd = new StoryFrmAdd();
            floorFrmAdd.ShowDialog();
        }

        private void deleteStoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
            {
                if (item.Index != dataGridView1.RowCount-1)
                {
                    dataGridView1.Rows.RemoveAt(item.Index);
                }
            }
        }
    }
}
