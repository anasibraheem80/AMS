using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.PowerPacks;


namespace AMSPRO
{
    public partial class FramesDiagramChoiceForm : Form
    {
        public FramesDiagramChoiceForm()
        {
            InitializeComponent();
        }
        Form mainForm = Application.OpenForms["MainForm"];
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ghg = "";
            Myglobals.DrawDiagram = 1;
            if (radioButton1.Checked == true)  //axial
            {
                Myglobals.AnyDiagram = 1;
                ghg = "  Axial Force Diagram";
            }
            if (radioButton2.Checked == true)//s22
            {
                Myglobals.AnyDiagram = 2;
                ghg = "  Shear Force 2-2 Diagram";
            }
            if (radioButton3.Checked == true) //s33
            {
                Myglobals.AnyDiagram = 3;
                ghg = "  Shear Force 3-3 Diagram"; 
            }
            if (radioButton4.Checked == true)//m22
            {
                Myglobals.AnyDiagram =4;
                ghg = "   Moment 2-2 Diagram"; 
            }
            if (radioButton5.Checked == true) //m33 
            {
                Myglobals.AnyDiagram = 5;
                ghg = "  Moment 3-3 Diagram";
            }
            if (radioButton6.Checked == true) //torsion
            {
                Myglobals.AnyDiagram = 6;
                ghg = "  Torsion"; 
            }
            ((MainForm)mainForm).tabPage3.Text = "Elevation View - " + GridLine.Name[Myglobals.Selectedelev] + ghg;
            Myglobals.DiagramValue = 0;
            if (checkBox2.Checked == true) Myglobals.DiagramValue = 1;//
            DROWcls callmee = new DROWcls();
            callmee.Renderelev();
            this.Close();
         }
        private void FramesDiagramChoiceForm_Load(object sender, EventArgs e)
        {
            if (Myglobals.AnyDiagram == 1) radioButton1.Checked = true;//axial
            if (Myglobals.AnyDiagram == 2) radioButton2.Checked = true;//s22
            if (Myglobals.AnyDiagram == 3) radioButton3.Checked = true;//s33
            if (Myglobals.AnyDiagram == 4) radioButton4.Checked = true;//m22
            if (Myglobals.AnyDiagram == 5) radioButton5.Checked = true;//m33
            if (Myglobals.AnyDiagram == 6) radioButton6.Checked = true;//torsion
            if (Myglobals.DiagramValue == 1) checkBox2.Checked = true;//
        
        }
    }
}
