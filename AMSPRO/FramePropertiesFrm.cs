using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.PowerPacks;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Media.Media3D;
using System.Collections;

namespace AMSPRO
{
    public partial class FramePropertiesFrm : Form
    {
        public FramePropertiesFrm()
        {
            InitializeComponent();
        }
        Form mainForm = Application.OpenForms["MainForm"];
        Form beamDesignerForm = Application.OpenForms["BeamDesignerForm"];
        private void FramePropertiesFrm_Load(object sender, EventArgs e)
        {
            Section.Numberd = Section.Number;
            for (int i = 1; i < Section.Number + 1; i++)
            {
                listBox1.Items.Add(Section.LABEL[i]);
                Section.MODELd[i] = Section.MODEL[i];
                Section.DESCRIPTIONd[i] = Section.DESCRIPTION[i];
                Section.LENGTH_UNITSd[i] = Section.LENGTH_UNITS[i];
                Section.LABELd[i] = Section.LABEL[i];
                Section.DESIGNATIONd[i] = Section.DESIGNATION[i];
                Section.Dd[i] = Section.D[i];
                Section.BFd[i] = Section.BF[i];
                Section.TFd[i] = Section.TF[i];
                Section.BFBd[i] = Section.BFB[i];
                Section.TFBd[i] = Section.TFB[i];
                Section.TWd[i] = Section.TW[i];
                Section.Ad[i] = Section.A[i];
                Section.I33d[i] = Section.I33[i];
                Section.Z33d[i] = Section.Z33[i];
                Section.AS3d[i] = Section.AS3[i];
                Section.I22d[i] = Section.I22[i];
                Section.Z22d[i] = Section.Z22[i];
                Section.AS2d[i] = Section.AS2[i];
                Section.Jd[i] = Section.J[i];
                Section.Xd[i] = Section.X[i];
                Section.Yd[i] = Section.Y[i];
                Section.S33POSd[i] = Section.S33POS[i];
                Section.S33NEGd[i] = Section.S33NEG[i];
                Section.S22POSd[i] = Section.S22POS[i];
                Section.S22NEGd[i] = Section.S22NEG[i];
                Section.R33d[i] = Section.R33[i];
                Section.R22d[i] = Section.R22[i];
                Section.Bd[i] = Section.B[i];
                Section.HTd[i] = Section.HT[i];
                Section.ODd[i] = Section.OD[i];
                Section.TDESd[i] = Section.TDES[i];
                Section.Materiald[i] = Section.Material[i];

                Section.ModifiersAread[i] = Section.ModifiersArea[i];
                Section.ModifiersShear2d[i] = Section.ModifiersShear2[i];
                Section.ModifiersShear3d[i] = Section.ModifiersShear3[i];
                Section.ModifiersTorsionald[i] = Section.ModifiersTorsional[i];
                Section.ModifiersI2d[i] = Section.ModifiersI2[i];
                Section.ModifiersI3d[i] = Section.ModifiersI3[i];
                Section.ModifiersMassd[i] = Section.ModifiersMass[i];
                Section.ModifiersWeightd[i] = Section.ModifiersWeight[i];

                Section.DesignTyped[i] = Section.DesignType[i];
                Section.RebarMaterial1d[i] = Section.RebarMaterial1[i];
                Section.RebarMaterial2d[i] = Section.RebarMaterial2[i];
                Section.CoverTopd[i] = Section.CoverTop[i];
                Section.CoverBottomd[i] = Section.CoverBottom[i];
                Section.ReinTopId[i] = Section.ReinTopI[i];
                Section.ReinTopJd[i] = Section.ReinTopJ[i];
                Section.ReinBottomId[i] = Section.ReinBottomI[i];
                Section.ReinBottomJd[i] = Section.ReinBottomJ[i];
            }
            listBox1.SetSelected(0, true);
            Section.Selected = 1;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SectionsFrm sectionsFrm = new SectionsFrm();
            sectionsFrm.ShowDialog();
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyNameTxt.Text = listBox1.Text;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex + 1;
            Section.Selected = i;
            Myglobals.EditOrNew = 2;
            Modify_Show_PropertyForm modify_Show_PropertyForm = new Modify_Show_PropertyForm();
            modify_Show_PropertyForm.ShowDialog();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (Section.Numberd > 1)
            {
                Section.Numberd = Section.Numberd - 1;
                int m = listBox1.SelectedIndex+1;
                for (int i = m; i < Section.Numberd + 1; i++)
                {
                    Section.MODELd[i] = Section.MODELd[i + 1];
                    Section.DESCRIPTIONd[i] = Section.DESCRIPTIONd[i + 1];
                    Section.LENGTH_UNITSd[i] = Section.LENGTH_UNITSd[i + 1];
                    Section.LABELd[i] = Section.LABELd[i + 1];
                    Section.DESIGNATIONd[i] = Section.DESIGNATIONd[i + 1];
                    Section.Dd[i] = Section.Dd[i + 1];
                    Section.BFd[i] = Section.BFd[i + 1];
                    Section.TFd[i] = Section.TFd[i + 1];
                    Section.BFBd[i] = Section.BFBd[i + 1];
                    Section.TFBd[i] = Section.TFBd[i + 1];
                    Section.TWd[i] = Section.TWd[i + 1];
                    Section.Ad[i] = Section.Ad[i + 1];
                    Section.I33d[i] = Section.I33d[i + 1];
                    Section.Z33d[i] = Section.Z33d[i + 1];
                    Section.AS3d[i] = Section.AS3d[i + 1];
                    Section.I22d[i] = Section.I22d[i + 1];
                    Section.Z22d[i] = Section.Z22d[i + 1];
                    Section.AS2d[i] = Section.AS2d[i + 1];
                    Section.Jd[i] = Section.Jd[i + 1];
                    Section.Xd[i] = Section.Xd[i + 1];
                    Section.Yd[i] = Section.Yd[i + 1];
                    Section.S33POSd[i] = Section.S33POSd[i + 1];
                    Section.S33NEGd[i] = Section.S33NEGd[i + 1];
                    Section.S22POSd[i] = Section.S22POSd[i + 1];
                    Section.S22NEGd[i] = Section.S22NEGd[i + 1];
                    Section.R33d[i] = Section.R33d[i + 1];
                    Section.R22d[i] = Section.R22d[i + 1];
                    Section.Bd[i] = Section.Bd[i + 1];
                    Section.HTd[i] = Section.HTd[i + 1];
                    Section.ODd[i] = Section.ODd[i + 1];
                    Section.TDESd[i] = Section.TDESd[i + 1];
                    Section.Materiald[i] = Section.Materiald[i+1];

                    Section.ModifiersAread[i] = Section.ModifiersAread[i+1];
                    Section.ModifiersShear2d[i] = Section.ModifiersShear2d[i+1];
                    Section.ModifiersShear3d[i] = Section.ModifiersShear3d[i+1];
                    Section.ModifiersTorsionald[i] = Section.ModifiersTorsionald[i+1];
                    Section.ModifiersI2d[i] = Section.ModifiersI2d[i+1];
                    Section.ModifiersI3d[i] = Section.ModifiersI3d[i+1];
                    Section.ModifiersMassd[i] = Section.ModifiersMassd[i+1];
                    Section.ModifiersWeightd[i] = Section.ModifiersWeightd[i+1];

                    Section.DesignTyped[i] = Section.DesignTyped[i+1];
                    Section.RebarMaterial1d[i] = Section.RebarMaterial1d[i+1];
                    Section.RebarMaterial2d[i] = Section.RebarMaterial2d[i+1];
                    Section.CoverTopd[i] = Section.CoverTopd[i+1];
                    Section.CoverBottomd[i] = Section.CoverBottomd[i+1];
                    Section.ReinTopId[i] = Section.ReinTopId[i+1];
                    Section.ReinTopJd[i] = Section.ReinTopJd[i+1];
                    Section.ReinBottomId[i] = Section.ReinBottomId[i+1];
                    Section.ReinBottomJd[i] = Section.ReinBottomJd[i+1];
                }
                Section.Selected = listBox1.SelectedIndex ;
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                if (Section.Selected < listBox1.Items.Count)
                {
                    listBox1.SetSelected(Section.Selected , true);
                }
                if (Section.Selected == listBox1.Items.Count)
                {
                    listBox1.SetSelected(Section.Selected - 1, true);
                }
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            int i = 0;
            Section.Number = Section.Numberd;
            for ( i = 1; i < Section.Number + 1; i++)
            {
                Section.MODEL[i] = Section.MODELd[i];
                Section.DESCRIPTION[i] = Section.DESCRIPTIONd[i];
                Section.LENGTH_UNITS[i] = Section.LENGTH_UNITSd[i];
                Section.LABEL[i] = Section.LABELd[i];
                Section.DESIGNATION[i] = Section.DESIGNATIONd[i];
                Section.D[i] = Section.Dd[i];
                Section.BF[i] = Section.BFd[i];
                Section.TF[i] = Section.TFd[i];
                Section.BFB[i] = Section.BFBd[i];
                Section.TFB[i] = Section.TFBd[i];
                Section.TW[i] = Section.TWd[i];
                Section.A[i] = Section.Ad[i];
                Section.I33[i] = Section.I33d[i];
                Section.Z33[i] = Section.Z33d[i];
                Section.AS3[i] = Section.AS3d[i];
                Section.I22[i] = Section.I22d[i];
                Section.Z22[i] = Section.Z22d[i];
                Section.AS2[i] = Section.AS2d[i];
                Section.J[i] = Section.Jd[i];
                Section.X[i] = Section.Xd[i];
                Section.Y[i] = Section.Yd[i];
                Section.S33POS[i] = Section.S33POSd[i];
                Section.S33NEG[i] = Section.S33NEGd[i];
                Section.S22POS[i] = Section.S22POSd[i];
                Section.S22NEG[i] = Section.S22NEGd[i];
                Section.R33[i] = Section.R33d[i];
                Section.R22[i] = Section.R22d[i];
                Section.B[i] = Section.Bd[i];
                Section.HT[i] = Section.HTd[i];
                Section.OD[i] = Section.ODd[i];
                Section.TDES[i] = Section.TDESd[i];
                Section.Material[i] = Section.Materiald[i];
                Section.ModifiersArea[i] = Section.ModifiersAread[i];
                Section.ModifiersShear2[i] = Section.ModifiersShear2d[i];
                Section.ModifiersShear3[i] = Section.ModifiersShear3d[i];
                Section.ModifiersTorsional[i] = Section.ModifiersTorsionald[i];
                Section.ModifiersI2[i] = Section.ModifiersI2d[i];
                Section.ModifiersI3[i] = Section.ModifiersI3d[i];
                Section.ModifiersMass[i] = Section.ModifiersMassd[i];
                Section.ModifiersWeight[i] = Section.ModifiersWeightd[i];
            }
            if (Myglobals.PropertyGridchoice == 1)
            {
               // DataGridViewComboBoxCell cbCell = (DataGridViewComboBoxCell)((MainForm)mainForm).PropertyGrid.Rows[1].Cells[1];
               // cbCell.Items.Clear();
              //  for (int i = 1; i < Section.Number + 1; i++)
              //  {
              //      cbCell.Items.Add(Section.LABEL[i]);
              //  }
               // cbCell.Value = cbCell.Items[0];
                Section.SelectedToDraw = 1;
            }
            if (Myglobals.FrameDesignerIsOpen == 1)
            {
                i = listBox1.SelectedIndex + 1;
                ((BeamDesignerForm)beamDesignerForm).SpanSection[Myglobals.FrameDesignerAnySpan] = i;
                ((BeamDesignerForm)beamDesignerForm).FillInTable();
                ((BeamDesignerForm)beamDesignerForm).DrawDiagram();
                ((BeamDesignerForm)beamDesignerForm).LastSection = i;
                Myglobals.FrameDesignerIsOpen = 0;
            }
            if (Myglobals.FrameDesignerIsOpen == 2)
            {
                i = listBox1.SelectedIndex + 1;
                for (int j = 1; j < ((BeamDesignerForm)beamDesignerForm).Spans + 1; j++)
                 {
                     ((BeamDesignerForm)beamDesignerForm).SpanSection[j] = i;
                 }
                ((BeamDesignerForm)beamDesignerForm).FillInTable();
                ((BeamDesignerForm)beamDesignerForm).DrawDiagram();
                ((BeamDesignerForm)beamDesignerForm).LastSection = i;
                Myglobals.FrameDesignerIsOpen = 0;
            }

            this.Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = Convert.ToString(Directory.GetParent(@"./Property Libraries/"));
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string strpath = saveFileDialog1.FileName + "";
                    StreamWriter SW = new StreamWriter(strpath);
                    SW.WriteLine(Section.Numberd);
                    for (int add = 1; add < Section.Numberd + 1; add++)
                    {
                        SW.WriteLine(Section.DESCRIPTIONd[add]);
                        SW.WriteLine(Section.LENGTH_UNITSd[add]);
                        SW.WriteLine(Section.MODELd[add]);
                        SW.WriteLine(Section.LABELd[add]);
                        SW.WriteLine(Section.EDI_STDd[add]);
                        SW.WriteLine(Section.DESIGNATIONd[add]);
                        SW.WriteLine(Section.Dd[add]);
                        SW.WriteLine(Section.Bd[add]);
                        SW.WriteLine(Section.BFd[add]);
                        SW.WriteLine(Section.TFd[add]);
                        SW.WriteLine(Section.BFBd[add]);
                        SW.WriteLine(Section.TFBd[add]);
                        SW.WriteLine(Section.TWd[add]);
                        SW.WriteLine(Section.Ad[add]);
                        SW.WriteLine(Section.I33d[add]);
                        SW.WriteLine(Section.Z33d[add]);
                        SW.WriteLine(Section.AS3d[add]);
                        SW.WriteLine(Section.I22d[add]);
                        SW.WriteLine(Section.Z22d[add]);
                        SW.WriteLine(Section.AS2d[add]);
                        SW.WriteLine(Section.Jd[add]);
                        SW.WriteLine(Section.Xd[add]);
                        SW.WriteLine(Section.Yd[add]);
                        SW.WriteLine(Section.S33POSd[add]);
                        SW.WriteLine(Section.S33NEGd[add]);
                        SW.WriteLine(Section.S22POSd[add]);
                        SW.WriteLine(Section.S22NEGd[add]);
                        SW.WriteLine(Section.R33d[add]);
                        SW.WriteLine(Section.R22d[add]);
                        SW.WriteLine(Section.HTd[add]);
                        SW.WriteLine(Section.ODd[add]);
                        SW.WriteLine(Section.TDESd[add]);
                        SW.WriteLine(Section.Materiald[add]);
                    }
                    SW.Close();
                }
                catch
                {
                    MessageBox.Show("ERROR! Please check passphrase and do not attempt to edit cipher text");
                }
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                string strpath = Convert.ToString(Directory.GetParent(@"./Property Libraries/MySections/"));
                StreamWriter SW = new StreamWriter(strpath);
                SW.WriteLine(Section.Numberd);
                for (int add = 1; add < Section.Numberd + 1; add++)
                {
                    SW.WriteLine(Section.DESCRIPTIONd[add]);
                    SW.WriteLine(Section.LENGTH_UNITSd[add]);
                    SW.WriteLine(Section.MODELd[add]);
                    SW.WriteLine(Section.LABELd[add]);
                    SW.WriteLine(Section.EDI_STDd[add]);
                    SW.WriteLine(Section.DESIGNATIONd[add]);
                    SW.WriteLine(Section.Dd[add]);
                    SW.WriteLine(Section.Bd[add]);
                    SW.WriteLine(Section.BFd[add]);
                    SW.WriteLine(Section.TFd[add]);
                    SW.WriteLine(Section.BFBd[add]);
                    SW.WriteLine(Section.TFBd[add]);
                    SW.WriteLine(Section.TWd[add]);
                    SW.WriteLine(Section.Ad[add]);
                    SW.WriteLine(Section.I33d[add]);
                    SW.WriteLine(Section.Z33d[add]);
                    SW.WriteLine(Section.AS3d[add]);
                    SW.WriteLine(Section.I22d[add]);
                    SW.WriteLine(Section.Z22d[add]);
                    SW.WriteLine(Section.AS2d[add]);
                    SW.WriteLine(Section.Jd[add]);
                    SW.WriteLine(Section.Xd[add]);
                    SW.WriteLine(Section.Yd[add]);
                    SW.WriteLine(Section.S33POSd[add]);
                    SW.WriteLine(Section.S33NEGd[add]);
                    SW.WriteLine(Section.S22POSd[add]);
                    SW.WriteLine(Section.S22NEGd[add]);
                    SW.WriteLine(Section.R33d[add]);
                    SW.WriteLine(Section.R22d[add]);
                    SW.WriteLine(Section.HTd[add]);
                    SW.WriteLine(Section.ODd[add]);
                    SW.WriteLine(Section.TDESd[add]);
                    SW.WriteLine(Section.Materiald[add]);

                    SW.WriteLine(Section.ModifiersAread[add]);
                    SW.WriteLine(Section.ModifiersShear2d[add]);
                    SW.WriteLine(Section.ModifiersShear3d[add]);
                    SW.WriteLine(Section.ModifiersTorsionald[add]);
                    SW.WriteLine(Section.ModifiersI2d[add]);
                    SW.WriteLine(Section.ModifiersI3d[add]);
                    SW.WriteLine(Section.ModifiersMassd[add]);
                    SW.WriteLine(Section.ModifiersWeightd[add]);

                    SW.WriteLine(Section.DesignTyped[add]);
                    SW.WriteLine(Section.RebarMaterial1d[add]);
                    SW.WriteLine(Section.RebarMaterial2d[add]);
                    SW.WriteLine(Section.CoverTopd[add]);
                    SW.WriteLine(Section.CoverBottomd[add]);
                    SW.WriteLine(Section.ReinTopId[add]);
                    SW.WriteLine(Section.ReinTopJd[add]);
                    SW.WriteLine(Section.ReinBottomId[add]);
                    SW.WriteLine(Section.ReinBottomJd[add]);
                }
                SW.Close();
            }
            catch
            {
                MessageBox.Show("ERROR! Please check passphrase and do not attempt to edit cipher text");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Myglobals.EditOrNew = 1;
            Section.Selected = 0;
            Modify_Show_PropertyForm modify_Show_PropertyForm = new Modify_Show_PropertyForm();
            modify_Show_PropertyForm.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex + 1;
            Section.Selected = i;
            Myglobals.EditOrNew = 3;
            Modify_Show_PropertyForm modify_Show_PropertyForm = new Modify_Show_PropertyForm();
            modify_Show_PropertyForm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}
