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
    public partial class SelectElevationForm : Form
    {
        public SelectElevationForm()
        {
            InitializeComponent();
        }
        Form mainForm = Application.OpenForms["MainForm"];
        private void SelectElevationForm_Load(object sender, EventArgs e)
        {
            int allline = GridLine.OnX + GridLine.OnY + GridLine.OnXY;
            for (int i = 1; i < Myglobals.elevNumbers + 1; i++)
            {
                listBox1.Items.Add(GridLine.Name[Myglobals.elevGridLine[i]]);
            }
            listBox1.SelectedIndex = Myglobals.Selectedelev - 1;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (Myglobals.ShowEleveWindow == 1)
            {
                 Myglobals.Selectedelev = listBox1.SelectedIndex + 1;
                ((MainForm)mainForm).tabPage3.BorderStyle = BorderStyle.FixedSingle;
                ((MainForm)mainForm).tabPage1.BorderStyle = BorderStyle.None;
                ((MainForm)mainForm).tabPage2.BorderStyle = BorderStyle.None;
                 Myglobals.SelectedPlan = "ELEV";
                DROWcls callmee = new DROWcls();
                callmee.Renderelev();
              //  callmee.FindAriasELEV();
                if (Myglobals.IfAnalysis == 1)
                {
                    string ghg = "";
                    if (Myglobals.AnyDiagram == 1)  //axial
                    {
                        ghg = "  Axial Force Diagram";
                    }
                    if (Myglobals.AnyDiagram == 2)//s22
                    {
                        ghg = "  Shear Force 2-2 Diagram";
                    }
                    if (Myglobals.AnyDiagram == 3) //s33
                    {
                        ghg = "  Shear Force 3-3 Diagram";
                    }
                    if (Myglobals.AnyDiagram == 4)//m22
                    {
                        ghg = "   Moment 2-2 Diagram";
                    }
                    if (Myglobals.AnyDiagram == 5) //m33 
                    {
                        ghg = "  Moment 3-3 Diagram";
                    }
                    if (Myglobals.AnyDiagram == 6) //torsion
                    {
                        ghg = "  Torsion";
                    }
                    ((MainForm)mainForm).tabPage3.Text = "Elevation View - " + GridLine.Name[Myglobals.Selectedelev] + ghg;
                }
                else
                {
                    ((MainForm)mainForm).tabPage3.Text = "Elevation View - " + GridLine.Name[Myglobals.elevGridLine[Myglobals.Selectedelev]];
                }
                goto endloop;
            }
            if (Myglobals.SelectedPlan == "3D")
            {
                Myglobals.ShowEleveWindow = 1;
                Myglobals.Show3DWindow = 0;
                ((MainForm)mainForm).tabControl3.Location = new Point(((MainForm)mainForm).tabControl2.Left, ((MainForm)mainForm).tabControl2.Top);
                ((MainForm)mainForm).tabControl2.Visible = false;
                ((MainForm)mainForm).tabControl3.Visible = true;
                Myglobals.Selectedelev = listBox1.SelectedIndex + 1;
                ((MainForm)mainForm).tabPage3.BorderStyle = BorderStyle.FixedSingle;
                ((MainForm)mainForm).tabPage1.BorderStyle = BorderStyle.None;
                ((MainForm)mainForm).tabPage2.BorderStyle = BorderStyle.None;
                ((MainForm)mainForm).tabPage3.Text = "Elevation View - " + GridLine.Name[Myglobals.Selectedelev];
                Myglobals.SelectedPlan = "ELEV";
                DROWcls callmee = new DROWcls();
                callmee.Renderelev();
            //    callmee.FindAriasELEV();
                goto endloop;
            }
            if (Myglobals.SelectedPlan == "Plan")
            {
                Myglobals.ShowEleveWindow = 1;
                Myglobals.ShowPlaneWindow = 0;
                ((MainForm)mainForm).tabControl3.Location = new Point(((MainForm)mainForm).tabControl1.Left, ((MainForm)mainForm).tabControl1.Top);
                ((MainForm)mainForm).tabControl1.Visible = false;
                ((MainForm)mainForm).tabControl3.Visible = true;
                Myglobals.Selectedelev = listBox1.SelectedIndex + 1;
                ((MainForm)mainForm).tabPage3.BorderStyle = BorderStyle.FixedSingle;
                ((MainForm)mainForm).tabPage1.BorderStyle = BorderStyle.None;
                ((MainForm)mainForm).tabPage2.BorderStyle = BorderStyle.None;
                ((MainForm)mainForm).tabPage3.Text = "Elevation View - " + GridLine.Name[Myglobals.Selectedelev];
                Myglobals.SelectedPlan = "ELEV";
                DROWcls callmee = new DROWcls();
                callmee.Renderelev();
            //    callmee.FindAriasELEV();
                goto endloop;
            }
        endloop: { }
        
        this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
    }
}
