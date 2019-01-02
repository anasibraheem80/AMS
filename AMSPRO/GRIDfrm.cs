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
    public partial class GRIDfrm : Form
    {
        public GRIDfrm()
        {
            InitializeComponent();
        }
        Form mainForm = Application.OpenForms["MainForm"];
        private void button1_Click(object sender, EventArgs e)
        {
            #region //فحص القيم الفارغة في الجداول
            int rows = 0;
            int rows1 = 0;
            int rows2 = 0;
            int add = 1;
            int add1 = 1;
            rows = MSFlexGridX.RowCount - 1;
            for (add = 0; add < rows - 1; add++)
            {
                if (MSFlexGridX.Rows[add].Cells[1].Value == null)
                {
                    MessageBox.Show("يوجد قيم فارغة");
                    goto endloop;
                }
            }
            if (radioButton2.Checked == true)//كتباعدات 
            {
                GridLine.VisibleAs = 2;
                if (rows > 0) MSFlexGridX.Rows[rows - 1].Cells[1].Value = 0;
                for (add = 0; add < rows - 1; add++)
                {
                    if (Convert.ToDouble(MSFlexGridX.Rows[add].Cells[1].Value) == 0)
                    {
                        MessageBox.Show("يوجد قيم مكررة");
                        goto endloop;
                    }
                }
            }
            if (radioButton1.Checked == true)//احداثيات 
            {
                GridLine.VisibleAs = 1;
                for (add = 0; add < rows; add++)
                {
                    for (add1 = add + 1; add1 < rows; add1++)
                    {
                        if (Convert.ToDouble(MSFlexGridX.Rows[add].Cells[1].Value) == Convert.ToDouble(MSFlexGridX.Rows[add1].Cells[1].Value))
                        {
                            MessageBox.Show("يوجد قيم مكررة");
                            goto endloop;
                        }
                    }
                }
            }
            rows1 = MSFlexGridY.RowCount - 1;
            for (add = 0; add < rows1 - 1; add++)
            {
                if (MSFlexGridY.Rows[add].Cells[1].Value == null)
                {
                    MessageBox.Show("يوجد قيم فارغة");
                    goto endloop;
                }
            }
            if (radioButton2.Checked == true)//تباعدات
            {
                if (rows1 > 0) MSFlexGridY.Rows[rows1 - 1].Cells[1].Value = 0;
                for (add = 0; add < rows1 - 1; add++)
                {
                    if (Convert.ToDouble(MSFlexGridY.Rows[add].Cells[1].Value) == 0)
                    {
                        MessageBox.Show("يوجد قيم مكررة");
                        goto endloop;
                    }
                }
            }
            if (radioButton1.Checked == true)//احداثيات 
            {
                for (add = 0; add < rows1; add++)
                {
                    for (add1 = add + 1; add1 < rows1; add1++)
                    {
                        if (Convert.ToDouble(MSFlexGridY.Rows[add].Cells[1].Value) == Convert.ToDouble(MSFlexGridY.Rows[add1].Cells[1].Value))
                        {
                            MessageBox.Show("يوجد قيم مكررة");
                            goto endloop;
                        }
                    }
                }
            }
            rows2 = MSFlexGridXY.RowCount - 1;
            for (add = 0; add < rows2; add++)
            {
                if (MSFlexGridXY.Rows[add].Cells[1].Value == null || MSFlexGridXY.Rows[add].Cells[2].Value == null || MSFlexGridXY.Rows[add].Cells[3].Value == null || MSFlexGridXY.Rows[add].Cells[4].Value == null)
                {
                    MessageBox.Show("يوجد قيم فارغة");
                    goto endloop;
                }
                for (add1 = 0; add1 < rows2; add1++)
                {
                    if (add != add1)
                    {
                        if (Convert.ToDouble(MSFlexGridXY.Rows[add].Cells[1].Value) == Convert.ToDouble(MSFlexGridXY.Rows[add1].Cells[1].Value) & Convert.ToDouble(MSFlexGridXY.Rows[add].Cells[2].Value) == Convert.ToDouble(MSFlexGridXY.Rows[add1].Cells[2].Value) & Convert.ToDouble(MSFlexGridXY.Rows[add].Cells[3].Value) == Convert.ToDouble(MSFlexGridXY.Rows[add1].Cells[3].Value) & Convert.ToDouble(MSFlexGridXY.Rows[add].Cells[4].Value) == Convert.ToDouble(MSFlexGridXY.Rows[add1].Cells[4].Value))
                        {
                            MessageBox.Show("يوجد قيم مكررة");
                            goto endloop;
                        }
                    }
                }
            }
            GridLine.OnX = rows;
            GridLine.OnY = rows1;
            GridLine.OnXY = rows2;
            #endregion
            //===================================================
            #region //حساب إحداثيات نقاط الخطوط
            double MAXY = Myglobals.startY;
            double MINY = Myglobals.startY;
           // double MAXY = -1000000;
           // double MINY = 1000000;
            if (radioButton2.Checked == true)//تباعدات
            {
                if (GridLine.OnY > 0)
                {
                    for (add = 0; add < GridLine.OnY ; add++)
                    {
                        if (MSFlexGridY.Rows[add].Cells[2].Value == null) MSFlexGridY.Rows[add].Cells[2].Value = true;
                        if (MSFlexGridY.Rows[add].Cells[2].Value.ToString() == "True")
                        {
                            MAXY = MAXY + Convert.ToDouble(MSFlexGridY.Rows[add].Cells[1].Value);
                        }
                    }
                }
            }
            else//احداثيات
            {
                MAXY = -1000000;
                MINY = 1000000;
                if (GridLine.OnY > 0)
                {
                    for (add = 0; add < GridLine.OnY ; add++)
                    {
                        if (MSFlexGridY.Rows[add].Cells[2].Value == null) MSFlexGridY.Rows[add].Cells[2].Value = true;
                        if (MSFlexGridY.Rows[add].Cells[2].Value.ToString() == "True")
                        {
                            if (MAXY < Convert.ToDouble(MSFlexGridY.Rows[add].Cells[1].Value))
                            {
                                MAXY = Convert.ToDouble(MSFlexGridY.Rows[add].Cells[1].Value);
                            }
                            if (MINY > Convert.ToDouble(MSFlexGridY.Rows[add].Cells[1].Value))
                            {
                                MINY = Convert.ToDouble(MSFlexGridY.Rows[add].Cells[1].Value);
                            }
                        }
                    }
                }
            }
            double MAXY2 = -1000000;
            double MINY2 = 1000000;
            if (GridLine.OnXY > 0)
            {
                for (add = 0; add < GridLine.OnXY; add++)
                {
                    if (MSFlexGridXY.Rows[add].Cells[5].Value == null) MSFlexGridXY.Rows[add].Cells[5].Value = true;
                    if (MSFlexGridXY.Rows[add].Cells[5].Value.ToString() == "True")
                    {
                        if (MAXY2 < Convert.ToDouble(MSFlexGridXY.Rows[add].Cells[4].Value))
                        {
                            MAXY2 = Convert.ToDouble(MSFlexGridXY.Rows[add].Cells[4].Value);
                        }
                        if (MAXY2 < Convert.ToDouble(MSFlexGridXY.Rows[add].Cells[2].Value))
                        {
                            MAXY2 = Convert.ToDouble(MSFlexGridXY.Rows[add].Cells[2].Value);
                        }
                        if (MINY2 > Convert.ToDouble(MSFlexGridXY.Rows[add].Cells[4].Value))
                        {
                            MINY2 = Convert.ToDouble(MSFlexGridXY.Rows[add].Cells[4].Value);
                        }
                        if (MINY2 > Convert.ToDouble(MSFlexGridXY.Rows[add].Cells[2].Value))
                        {
                            MINY2 = Convert.ToDouble(MSFlexGridXY.Rows[add].Cells[2].Value);
                        }
                    }
                }
            }
           // if (MAXY2 > MAXY) MAXY = MAXY2;
           // if (MINY2 < MINY) MINY = MINY2;
            double MAXX = Myglobals.startX;
            double MINX = Myglobals.startX;
            if (radioButton2.Checked == true)//تباعدات
            {
                if (GridLine.OnX > 0)
                {
                    for (add = 0; add < GridLine.OnX ; add++)
                    {
                        if (MSFlexGridX.Rows[add].Cells[2].Value == null) MSFlexGridX.Rows[add].Cells[2].Value = true;
                        if (MSFlexGridX.Rows[add].Cells[2].Value.ToString() == "True")
                        {
                            MAXX = MAXX + Convert.ToDouble(MSFlexGridX.Rows[add].Cells[1].Value);
                        }
                    }
                }
            }
            else// احداثيات
            {
                MAXX = -1000000;
                MINX = 1000000;
                for (add = 0; add < GridLine.OnX ; add++)
                {
                    if (MSFlexGridX.Rows[add].Cells[2].Value == null) MSFlexGridX.Rows[add].Cells[2].Value = true;
                    if (MSFlexGridX.Rows[add].Cells[2].Value.ToString() == "True")
                    {
                        if (MAXX < Convert.ToDouble(MSFlexGridX.Rows[add].Cells[1].Value))
                        {
                            MAXX = Convert.ToDouble(MSFlexGridX.Rows[add].Cells[1].Value);
                        }
                        if (MINX > Convert.ToDouble(MSFlexGridX.Rows[add].Cells[1].Value))
                        {
                            MINX = Convert.ToDouble(MSFlexGridX.Rows[add].Cells[1].Value);
                        }
                    }
                }
            }
            double MAXX2 = -1000000;
            double MINX2 = 1000000;
            if (GridLine.OnXY > 0)
            {
                for (add = 0; add < GridLine.OnXY ; add++)
                {
                    if (MSFlexGridXY.Rows[add].Cells[5].Value == null) MSFlexGridXY.Rows[add].Cells[5].Value = true;
                    if (MSFlexGridXY.Rows[add].Cells[5].Value.ToString() == "True")
                    {
                        if (MAXX2 < Convert.ToDouble(MSFlexGridXY.Rows[add].Cells[3].Value))
                        {
                            MAXX2 = Convert.ToDouble(MSFlexGridXY.Rows[add].Cells[3].Value);
                        }
                        if (MAXX2 < Convert.ToDouble(MSFlexGridXY.Rows[add].Cells[1].Value))
                        {
                            MAXX2 = Convert.ToDouble(MSFlexGridXY.Rows[add].Cells[1].Value);
                        }
                        if (MINX2 > Convert.ToDouble(MSFlexGridXY.Rows[add].Cells[3].Value))
                        {
                            MINX2 = Convert.ToDouble(MSFlexGridXY.Rows[add].Cells[3].Value);
                        }
                        if (MINX2 > Convert.ToDouble(MSFlexGridXY.Rows[add].Cells[1].Value))
                        {
                            MINX2 = Convert.ToDouble(MSFlexGridXY.Rows[add].Cells[1].Value);
                        }
                    }
                }
            }
           // if (MAXX2 > MAXX) MAXX = MAXX2;
           // if (MINX2 < MINX) MINX = MINX2;
            int alllines = GridLine.OnX + GridLine.OnY + GridLine.OnXY + 1;
            GridLine.Name = new string[alllines];
            GridLine.XOrY = new int[alllines];
            GridLine.Visible = new int[alllines];
            GridLine.X1Real = new double[alllines];
            GridLine.X2Real = new double[alllines];
            GridLine.Y1Real = new double[alllines];
            GridLine.Y2Real = new double[alllines];
            GridLine.Distance = new double[alllines];
            int m = 0;
            if (GridLine.OnX > 0)
            {
                double MAJX = Myglobals.startX;
                for (add = 0; add < GridLine.OnX; add++)
                {
                    if (MSFlexGridX.Rows[add].Cells[0].Value != null)
                    {
                        GridLine.Name[add + 1] = Convert.ToString(MSFlexGridX.Rows[add].Cells[0].Value);
                    }
                    else
                    {
                        GridLine.Name[add + 1] = "";
                    }
                    GridLine.XOrY[add + 1] = 1;
                    if (MSFlexGridX.Rows[add].Cells[2].Value == null) MSFlexGridX.Rows[add].Cells[2].Value = true;
                    if (MSFlexGridX.Rows[add].Cells[2].Value.ToString() == "True")
                    {
                        GridLine.Visible[add + 1] = 0;
                    }
                    else
                    {
                        GridLine.Visible[add + 1] = 1;
                    }
                    if (radioButton2.Checked == true)//تباعدات
                    {
                        GridLine.Distance[add + 1] = Convert.ToDouble(MSFlexGridX.Rows[add].Cells[1].Value);
                        GridLine.X1Real[add + 1] = MAJX;
                        GridLine.X2Real[add + 1] = MAJX;
                        GridLine.Y1Real[add + 1] = MINY;
                        GridLine.Y2Real[add + 1] = MAXY;
                        MAJX = Convert.ToDouble(MSFlexGridX.Rows[add].Cells[1].Value) + MAJX;
                    }
                    else//احداثيات
                    {
                        if (add == GridLine.OnX - 1)
                        {
                            GridLine.Distance[add + 1] = 0;
                        }
                        else
                        {
                            GridLine.Distance[add + 1] = Convert.ToDouble(MSFlexGridX.Rows[add + 1].Cells[1].Value) - Convert.ToDouble(MSFlexGridX.Rows[add].Cells[1].Value);
                        }
                        MAJX = Convert.ToDouble(MSFlexGridX.Rows[add].Cells[1].Value);
                        GridLine.X1Real[add + 1] = MAJX;
                        GridLine.X2Real[add + 1] = MAJX;
                        GridLine.Y1Real[add + 1] = MINY;
                        GridLine.Y2Real[add + 1] = MAXY;
                    }
                }//NEXT add
            }
            if (GridLine.OnY > 0)
            {
                m = 0;
                double MAJY = Myglobals.startY;
                for (add = GridLine.OnX; add < GridLine.OnX + GridLine.OnY; add++)
                {
                    if (MSFlexGridY.Rows[m].Cells[0].Value != null)
                    {
                        GridLine.Name[add + 1] = Convert.ToString(MSFlexGridY.Rows[m].Cells[0].Value);
                    }
                    else
                    {
                        GridLine.Name[add + 1] = "";
                    }
                    GridLine.XOrY[add + 1] = 2;
                    if (MSFlexGridY.Rows[m].Cells[2].Value == null) MSFlexGridY.Rows[m].Cells[2].Value = true;
                    if (MSFlexGridY.Rows[m].Cells[2].Value.ToString() == "True")
                    {
                        GridLine.Visible[add + 1] = 0;
                    }
                    else
                    {
                        GridLine.Visible[add + 1] = 1;
                    }
                    if (radioButton2.Checked == true)//تباعدات
                    {
                        GridLine.Distance[add + 1] = Convert.ToDouble(MSFlexGridY.Rows[m].Cells[1].Value);
                        GridLine.X1Real[add + 1] = MINX;
                        GridLine.X2Real[add + 1] = MAXX;
                        GridLine.Y1Real[add + 1] = MAJY;
                        GridLine.Y2Real[add + 1] = MAJY;
                        MAJY = Convert.ToDouble(MSFlexGridY.Rows[m].Cells[1].Value) + MAJY;
                    }
                    else
                    {
                        if (add == GridLine.OnX + GridLine.OnY - 1)
                        {
                            GridLine.Distance[add + 1] = 0;
                        }
                        else
                        {
                            GridLine.Distance[add + 1] = Convert.ToDouble(MSFlexGridY.Rows[m + 1].Cells[1].Value) - Convert.ToDouble(MSFlexGridY.Rows[m].Cells[1].Value);
                        }
                        MAJY = Convert.ToDouble(MSFlexGridY.Rows[m].Cells[1].Value);
                        GridLine.X1Real[add + 1] = MINX;
                        GridLine.X2Real[add + 1] = MAXX;
                        GridLine.Y1Real[add + 1] = MAJY;
                        GridLine.Y2Real[add + 1] = MAJY;
                    }
                    m = m + 1;
                }
            }
            if (GridLine.OnXY > 0)
            {
                m = 0;
                for (add = GridLine.OnX + GridLine.OnY; add < GridLine.OnX + GridLine.OnY + GridLine.OnXY; add++)
                {
                    if (MSFlexGridXY.Rows[m].Cells[0].Value != null)
                    {
                        GridLine.Name[add + 1] = Convert.ToString(MSFlexGridXY.Rows[m].Cells[0].Value);
                    }
                    else
                    {
                        GridLine.Name[add + 1] = "";
                    }
                    GridLine.XOrY[add + 1] = 3;
                    if (MSFlexGridXY.Rows[m].Cells[5].Value == null) MSFlexGridXY.Rows[m].Cells[5].Value = true;
                    if (MSFlexGridXY.Rows[m].Cells[5].Value.ToString() == "True")
                    {
                        GridLine.Visible[add + 1] = 0;
                    }
                    else
                    {
                        GridLine.Visible[add + 1] = 1;
                    }
                    GridLine.X1Real[add + 1] = Convert.ToDouble(MSFlexGridXY.Rows[m].Cells[1].Value);
                    GridLine.X2Real[add + 1] = Convert.ToDouble(MSFlexGridXY.Rows[m].Cells[3].Value);
                    GridLine.Y1Real[add + 1] = Convert.ToDouble(MSFlexGridXY.Rows[m].Cells[2].Value);
                    GridLine.Y2Real[add + 1] = Convert.ToDouble(MSFlexGridXY.Rows[m].Cells[4].Value);
                    GridLine.Distance[add + 1] = 1;
                    m = m + 1;
                }
            }
            for (add = 1; add < GridLine.OnX + GridLine.OnY + GridLine.OnXY + 1; add++)
            {
                GridLine.X1Real[add] = Math.Round(GridLine.X1Real[add], 3);
                GridLine.X2Real[add] = Math.Round(GridLine.X2Real[add], 3);
                GridLine.Y1Real[add] = Math.Round(GridLine.Y1Real[add], 3);
                GridLine.Y2Real[add] = Math.Round(GridLine.Y2Real[add], 3);
            }

            #endregion
            DROWcls callmee = new DROWcls();
            callmee.CalculateGridPointReal();
            callmee.Render2d();
            callmee.Render3d();
            callmee.Renderelev();
            ((MainForm)mainForm).tabPage1.Text = "Plan View" + "  " + "Story" + Myglobals.SelectedStory + " -Z=  " + Myglobals.StoryLevel[Myglobals.SelectedStory] + " m";
            this.Close();
        endloop: { }
        }
        private void GRIDfrm_Load(object sender, EventArgs e)
        {
            try
            {
                int alllines = GridLine.OnX + GridLine.OnY + GridLine.OnXY + 1;
                GridLine.Number2d = alllines;
                for (int add = 0; add < GridLine.OnX; add++)
                {
                    string awal = GridLine.Name[add + 1];
                    double thani = GridLine.X1Real[add + 1];
                    int talet = GridLine.Visible[add + 1];
                    if (talet == 0)
                    {
                        MSFlexGridX.Rows.Add(awal, thani, true);
                    }
                    else
                    {
                        MSFlexGridX.Rows.Add(awal, thani, false);
                    }

                }
                for (int add = GridLine.OnX; add < GridLine.OnX + GridLine.OnY; add++)
                {
                    string awal = GridLine.Name[add + 1];
                    double thani = GridLine.Y1Real[add + 1];
                    int talet = GridLine.Visible[add + 1];
                    if (talet == 0)
                    {
                        MSFlexGridY.Rows.Add(awal, thani, true);
                    }
                    else
                    {
                        MSFlexGridY.Rows.Add(awal, thani, false);
                    }
                }
                for (int add = GridLine.OnX + GridLine.OnY; add < GridLine.OnX + GridLine.OnY + GridLine.OnXY; add++)
                {
                    string awal = GridLine.Name[add + 1];
                    double thani = GridLine.X1Real[add + 1];
                    double thaleth = GridLine.Y1Real[add + 1];
                    double rabea = GridLine.X2Real[add + 1];
                    double khams = GridLine.Y2Real[add + 1];
                    int sades = GridLine.Visible[add + 1];
                    if (sades == 0)
                    {
                        MSFlexGridXY.Rows.Add(awal, thani, thaleth, rabea, khams, true);
                    }
                    else
                    {
                        MSFlexGridXY.Rows.Add(awal, thani, thaleth, rabea, khams, false);
                    }
                }
            }
            catch (Exception ex) { }
        }
        private void MSFlexGridX_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["Column3"].Value = true;
        }
        private void MSFlexGridY_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["Column30"].Value = true;
        }
        private void MSFlexGridXY_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["Column6"].Value = true;
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                //من تباعدات إلى إحداثيات
                MSFlexGridX.Columns[1].HeaderText = "X Ordinate";
                int therow = MSFlexGridX.RowCount - 1;
                double[] thevalue = new double[therow + 1];
                for (int add = 1; add < therow + 1; add++)
                {
                    thevalue[add] = Convert.ToDouble(MSFlexGridX.Rows[add - 1].Cells[1].Value);
                }
                double thesum = 0;
                thesum = Myglobals.startX;
                for (int add = 1; add < therow + 1; add++)
                {
                    MSFlexGridX.Rows[add - 1].Cells[1].Value = thesum;
                    thesum = thesum + thevalue[add];
                }
                MSFlexGridY.Columns[1].HeaderText = "Y Ordinate";
                int therow1 = MSFlexGridY.RowCount - 1;
                double[] thevalue1 = new double[therow1 + 1];
                for (int add = 1; add < therow1 + 1; add++)
                {
                    thevalue1[add] = Convert.ToDouble(MSFlexGridY.Rows[add - 1].Cells[1].Value);
                }
                double thesum1 = 0;
                thesum1 = Myglobals.startY;
                for (int add = 1; add < therow1 + 1; add++)
                {
                    MSFlexGridY.Rows[add - 1].Cells[1].Value = thesum1;
                    thesum1 = thesum1 + thevalue1[add];
                }
            }
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                //من إحداثيات إلى تباعدات
                MSFlexGridX.Columns[1].HeaderText = "X Spacing";
                Myglobals.startX = Convert.ToDouble(MSFlexGridX.Rows[0].Cells[1].Value);
                int therow = MSFlexGridX.RowCount - 1;
                double[] thevalue = new double[therow + 1];
                for (int add = 1; add < therow + 1; add++)
                {
                    thevalue[add] = Convert.ToDouble(MSFlexGridX.Rows[add - 1].Cells[1].Value);
                }
                for (int add = 1; add < therow; add++)
                {
                    MSFlexGridX.Rows[add - 1].Cells[1].Value = thevalue[add + 1] - thevalue[add];
                }
                if (therow > 0)
                {
                    MSFlexGridX.Rows[therow - 1].Cells[1].Value = 0;
                }
                MSFlexGridY.Columns[1].HeaderText = "Y Spacing";
                Myglobals.startY = Convert.ToDouble(MSFlexGridY.Rows[0].Cells[1].Value);
                int therow1 = MSFlexGridY.RowCount - 1;
                double[] thevalue1 = new double[therow1 + 1];
                for (int add = 1; add < therow1 + 1; add++)
                {
                    thevalue1[add] = Convert.ToDouble(MSFlexGridY.Rows[add - 1].Cells[1].Value);
                }
                for (int add = 1; add < therow1; add++)
                {
                    MSFlexGridY.Rows[add - 1].Cells[1].Value = thevalue1[add + 1] - thevalue1[add];
                }
                if (therow1 > 0)
                {
                    MSFlexGridY.Rows[therow1 - 1].Cells[1].Value = 0;
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string strpath = openFileDialog1.FileName;
                    StreamReader SW = new StreamReader(strpath);
                    GridLine.OnX = Convert.ToInt32(SW.ReadLine());
                    GridLine.OnY = Convert.ToInt32(SW.ReadLine());
                    GridLine.OnXY = Convert.ToInt32(SW.ReadLine());
                    int alllines = GridLine.OnX + GridLine.OnY + GridLine.OnXY + 1;
                    GridLine.Name = new string[alllines];
                    GridLine.XOrY = new int[alllines];
                    GridLine.Visible = new int[alllines];
                    GridLine.X1Real = new double[alllines];
                    GridLine.X2Real = new double[alllines];
                    GridLine.Y1Real = new double[alllines];
                    GridLine.Y2Real = new double[alllines];
                    GridLine.Distance = new double[alllines];
                    for (int add = 0; add < GridLine.OnX + GridLine.OnY + GridLine.OnXY; add++)
                    {
                        GridLine.Name[add + 1] = Convert.ToString(SW.ReadLine());
                        GridLine.X1Real[add + 1] = Convert.ToDouble(SW.ReadLine());
                        GridLine.Y1Real[add + 1] = Convert.ToDouble(SW.ReadLine());
                        GridLine.X2Real[add + 1] = Convert.ToDouble(SW.ReadLine());
                        GridLine.Y2Real[add + 1] = Convert.ToDouble(SW.ReadLine());
                        GridLine.Visible[add + 1] = Convert.ToInt32(SW.ReadLine());
                    }
                    SW.Close();
                }
                catch
                {
                    MessageBox.Show("ERROR! Please check passphrase and do not attempt to edit cipher text");
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = "c:\\";
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string strpath = saveFileDialog1.FileName + ".txt";
                    StreamWriter SW = new StreamWriter(strpath);
                    SW.WriteLine(GridLine.OnX);
                    SW.WriteLine(GridLine.OnY);
                    SW.WriteLine(GridLine.OnXY);
                    for (int add = 0; add < GridLine.OnX + GridLine.OnY + GridLine.OnXY; add++)
                    {
                        SW.WriteLine(GridLine.Name[add + 1]);
                        SW.WriteLine(GridLine.X1Real[add + 1]);
                        SW.WriteLine(GridLine.Y1Real[add + 1]);
                        SW.WriteLine(GridLine.X2Real[add + 1]);
                        SW.WriteLine(GridLine.Y2Real[add + 1]);
                        SW.WriteLine(GridLine.Visible[add + 1]);
                    }
                    SW.Close();
                }
                catch
                {
                    MessageBox.Show("ERROR! Please check passphrase and do not attempt to edit cipher text");
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
