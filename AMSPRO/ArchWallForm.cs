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
    public partial class ArchWallForm : Form
    {
        public ArchWallForm()
        {
            InitializeComponent();
        }
        Form mainForm = Application.OpenForms["MainForm"];
        Form archWallFormALL = Application.OpenForms["ArchWallFormALL"];


        private void ArchWallForm_Load(object sender, EventArgs e)
        {
            int i = Section.Selected;
            if (Myglobals.EditOrNew == 2 || Myglobals.EditOrNew == 3)
            {
                for (int j = 1; j < ((MainForm)mainForm).ArchWall[i].LayersNumber + 1; j++)
                {
                    string Function = ((MainForm)mainForm).ArchWall[i].Function[j].ToString();
                    string Material = ((MainForm)mainForm).ArchWall[i].Material[j].ToString();
                    string Thickness = ((MainForm)mainForm).ArchWall[i].Thickness[j].ToString();
                    MSFlexGridX.Rows.Add(Function, Material, Thickness, false);
                }
                textBox2.Text = ((MainForm)mainForm).ArchWall[i].Name;
                label4.Text = ((MainForm)mainForm).ArchWall[i].AllThickness.ToString();
                label5.Text = ((MainForm)mainForm).ArchWall[i].Weight.ToString();
                CALC();
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            int i = Section.Selected;
            if (Myglobals.EditOrNew == 1 || Myglobals.EditOrNew == 3)
            {
                Myglobals.ArchWallNumber = Myglobals.ArchWallNumber + 1;
                i = Myglobals.ArchWallNumber;
                Section.Selected = Myglobals.ArchWallNumber;
            }
            int LayersNumber = MSFlexGridX.RowCount ;
            ArchWallS EMP = new ArchWallS();
            EMP.Name = textBox2.Text;
            for (int j = 1; j < LayersNumber + 1; j++)
            {
                EMP.Function[j] = Convert.ToString(MSFlexGridX.Rows[j - 1].Cells[0].Value);
                EMP.Material[j] = Convert.ToString(MSFlexGridX.Rows[j - 1].Cells[1].Value);
                EMP.Thickness[j] = Convert.ToDouble(MSFlexGridX.Rows[j - 1].Cells[2].Value);
            }
            EMP.AllThickness = Convert.ToDouble(label4.Text);
            EMP.Weight = Convert.ToDouble(label5.Text);
            EMP.LayersNumber = LayersNumber;
            if (Myglobals.EditOrNew == 2)//تعديل
            {
                ((ArchWallFormALL)archWallFormALL).listBox1.Items.Insert(((ArchWallFormALL)archWallFormALL).listBox1.SelectedIndex, EMP.Name);
                ((ArchWallFormALL)archWallFormALL).listBox1.Items.Remove(((ArchWallFormALL)archWallFormALL).listBox1.SelectedItem);
                ((ArchWallFormALL)archWallFormALL).listBox1.SetSelected(Section.Selected - 1, true);
            }
            if (Myglobals.EditOrNew == 1 || Myglobals.EditOrNew == 3)//جديد
            {
                ((ArchWallFormALL)archWallFormALL).listBox1.Items.Add(EMP.Name);
                ((ArchWallFormALL)archWallFormALL).listBox1.SetSelected(Section.Selected - 1, true);
            }
            ((MainForm)mainForm).ArchWall[i] = EMP;
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int LayersNumber = MSFlexGridX.RowCount+1;
            MSFlexGridX.Rows.Add("Structure [" + LayersNumber + "]", "Concrete", "0.2", false);
            CALC();
        }
        private void button2_Click(object sender, EventArgs e)
        {
           // int LayersNumber = MSFlexGridX.RowCount;
          //  int Number = MSFlexGridX.SelectedCells[0].RowIndex + 1;
            if (MSFlexGridX.SelectedRows.Count > 0)
            {
                MSFlexGridX.Rows.RemoveAt(MSFlexGridX.SelectedRows[0].Index);
            }
            CALC();
           // MSFlexGridX.Rows.Add("Structure [" + Number + "]", "Concrete", "0.2", false);
        }
        
        private void MSFlexGridX_KeyUp(object sender, KeyEventArgs e)
        {
            CALC();
        }
        private void CALC()
        {
            int LayersNumber = MSFlexGridX.RowCount;
            double AllThickness = 0;
            double AllWeight = 0;
            for (int i = 1; i < LayersNumber + 1; i++)
            {
                AllThickness = AllThickness + Convert.ToDouble(MSFlexGridX.Rows[i - 1].Cells[2].Value);
                AllWeight = AllWeight + Convert.ToDouble(MSFlexGridX.Rows[i - 1].Cells[2].Value) * 2.5;
            }
            label4.Text = AllThickness.ToString();
            label5.Text = AllWeight.ToString();
            Draw();
        }
        private void Draw()
        {
            Bitmap finalBmp = new Bitmap(300, 300);
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
            pictureBox1.Image = finalBmp;
            int startX = 125;
            int startY = 225;
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            Pen pen = new Pen(Color.Black, 2f);

            int LayersNumber = MSFlexGridX.RowCount;
            double AllThickness = 0;
            for (int i = 1; i < LayersNumber + 1; i++)
            {
                AllThickness = AllThickness + Convert.ToDouble(MSFlexGridX.Rows[i - 1].Cells[2].Value);
            }
            #region//مقطع مستطيل
            double B = 1;
                double HT = AllThickness;
                if (HT > 0)
                {
                    Point[] P = new Point[6];
                    double MaxW = B;
                    if (HT > MaxW) MaxW = HT;
                    double thezoom = Convert.ToDouble(200 / MaxW);
                    startX = Convert.ToInt32(startX - (B) / 2 * thezoom);
                    P[1].X = Convert.ToInt32(startX);
                    P[1].Y = Convert.ToInt32(startY);
                    P[2].X = Convert.ToInt32(startX);
                    P[2].Y = Convert.ToInt32(startY - HT * thezoom);
                    P[3].X = Convert.ToInt32(startX + B * thezoom);
                    P[3].Y = Convert.ToInt32(startY - HT * thezoom);
                    P[4].X = Convert.ToInt32(startX + B * thezoom);
                    P[4].Y = Convert.ToInt32(startY);
                    P[5] = P[1];
                    g.DrawLine(pen, P[1], P[2]);
                    g.DrawLine(pen, P[2], P[3]);
                    g.DrawLine(pen, P[3], P[4]);
                    g.DrawLine(pen, P[4], P[1]);
                    pen = new Pen(Color.Black, 1f);
                    AllThickness = 0;
                    for (int i = 1; i < LayersNumber + 1; i++)
                    {
                        AllThickness = AllThickness + Convert.ToDouble(MSFlexGridX.Rows[i - 1].Cells[2].Value);
                        HT = AllThickness;
                        P[2].Y = Convert.ToInt32(startY - HT * thezoom);
                        P[3].X = Convert.ToInt32(startX + B * thezoom);
                        P[3].Y = Convert.ToInt32(startY - HT * thezoom);
                        g.DrawLine(pen, P[2], P[3]);
                    }
                    g.Dispose();
            }
            #endregion
        }

        private void MSFlexGridX_RowLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void MSFlexGridX_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            CALC();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataGridView dgv = MSFlexGridX;
            try
            {
                int totalRows = dgv.Rows.Count;
                // get index of the row for the selected cell
                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (rowIndex == 0)
                    return;
                // get index of the column for the selected cell
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                dgv.Rows.Remove(selectedRow);
                dgv.Rows.Insert(rowIndex - 1, selectedRow);
                dgv.ClearSelection();
                dgv.Rows[rowIndex - 1].Cells[colIndex].Selected = true;
                CALC();
            }
            catch { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataGridView dgv = MSFlexGridX;
            try
            {
                int totalRows = dgv.Rows.Count;
                // get index of the row for the selected cell
                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (rowIndex == totalRows - 1)
                    return;
                // get index of the column for the selected cell
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                dgv.Rows.Remove(selectedRow);
                dgv.Rows.Insert(rowIndex + 1, selectedRow);
                dgv.ClearSelection();
                dgv.Rows[rowIndex + 1].Cells[colIndex].Selected = true;
                CALC();
            }
            catch { }
        }
    }
}
