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
    public partial class SetVeiwOptionsForm : Form
    {
        public SetVeiwOptionsForm()
        {
            InitializeComponent();
        }
        Form mainForm = Application.OpenForms["MainForm"];
        private void button1_Click(object sender, EventArgs e)
        {
            Joint.Assignments = 0;
            if (JointAssignmentsChk.Checked == true) Joint.Assignments = 1;
            Frame.Assignments = 0;
            if (BeamAssignmentsChk.Checked == true) Frame.Assignments = 1;
            Frame.Localaxis = 0;
            if (BeamLocalaxesChc.Checked == true) Frame.Localaxis = 1;
            Shell.Localaxis = 0;
            if (ShellLocalaxisChc.Checked == true) Shell.Localaxis = 1;
            Myglobals.ShllAnalysisMeshVeiw = 0;
            if (ShllAnalysisMeshVeiwchc.Checked == true) Myglobals.ShllAnalysisMeshVeiw = 1;
            DROWcls callmee = new DROWcls();
            callmee.Render2d();
            callmee.Render3d();
            this.Close();
        }
        private void SetVeiwOptionsForm_Load(object sender, EventArgs e)
        {
            if (Joint.Assignments == 1) JointAssignmentsChk.Checked = true;
            if (Frame.Assignments == 1) BeamAssignmentsChk.Checked = true;
            if (Frame.Localaxis == 1) BeamLocalaxesChc.Checked = true;
            if (Myglobals.ShllAnalysisMeshVeiw == 1) ShllAnalysisMeshVeiwchc.Checked = true;
            if (Shell.Localaxis == 1) ShellLocalaxisChc.Checked = true;
        }
    }
}
