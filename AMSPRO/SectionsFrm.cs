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
    public partial class SectionsFrm : Form
    {
        public SectionsFrm()
        {
            InitializeComponent();
            
        }
        Form framePropertiesFrm = Application.OpenForms["FramePropertiesFrm"];

        int[] SelectedM = new int[10000];

        string[] SectionDESCRIPTION = new string[10000];
        string[] SectionLENGTH_UNITS = new string[10000];
        string[] SectionMODEL = new string[10000];
        string[] SectionLABEL = new string[10000];
        string[] SectionEDI_STD = new string[10000];
        string[] SectionDESIGNATION = new string[10000];
        string[] SectionD = new string[10000];
        string[] SectionBF = new string[10000];
        string[] SectionTF = new string[10000];
        string[] SectionBFB = new string[10000];
        string[] SectionTFB = new string[10000];
        string[] SectionTW = new string[10000];
        string[] SectionA = new string[10000];
        string[] SectionI33 = new string[10000];
        string[] SectionZ33 = new string[10000];
        string[] SectionAS3 = new string[10000];
        string[] SectionI22 = new string[10000];
        string[] SectionZ22 = new string[10000];
        string[] SectionAS2 = new string[10000];
        string[] SectionJ = new string[10000];
        string[] SectionX = new string[10000];
        string[] SectionY = new string[10000];
        string[] SectionS33POS = new string[10000];
        string[] SectionS33NEG = new string[10000];
        string[] SectionS22POS = new string[10000];
        string[] SectionS22NEG = new string[10000];
        string[] SectionR33 = new string[10000];
        string[] SectionR22 = new string[10000];

        string[] SectionB = new string[10000];
        string[] SectionHT = new string[10000];
        string[] SectionOD = new string[10000];
        string[] SectionTDES = new string[10000];
        string DESCRIPTION = "";
        string LENGTH_UNITS = "";
        int AllSections =0;

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = System.IO.Directory.GetParent(@"./Property Libraries/").FullName;
            openFileDialog1.Filter = "xml files (*.XML)|*.XML|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    listBox1.Items.Clear(); 
                    listBox2.Items.Clear();
                    listBox1.Items.Add("ALL");
                    string strpath = openFileDialog1.FileName;
                    StreamReader SW = new StreamReader(strpath);
                    string[] lines = System.IO.File.ReadAllLines(strpath);
                    string laststring = "";
                    string f1 = "";
                    string f2 = "";
                    string ff = "";
                    int m = 0;
                    for (int i = 0; i < lines.Length; ++i)
                    {
                        string GH = Convert.ToString(SW.ReadLine());
                        int Slength = GH.Length;
                        if (GH != "" & i > 6)
                        {
                            int pFrom = GH.IndexOf("<") + "<".Length;
                            int pTo = GH.IndexOf(">");
                            String result = GH.Substring(pFrom, pTo - pFrom);

                            if (result == "DESCRIPTION")
                            {
                                pFrom = GH.IndexOf(">") + ">".Length;
                                pTo = GH.LastIndexOf("<");
                                result = GH.Substring(pFrom, pTo - pFrom);
                                DESCRIPTION = result;
                            }
                            if (result == "LENGTH_UNITS")
                            {
                                pFrom = GH.IndexOf(">") + ">".Length;
                                pTo = GH.LastIndexOf("<");
                                result = GH.Substring(pFrom, pTo - pFrom);
                                LENGTH_UNITS = result;
                            }

                            if (result == "LABEL")
                            {
                                m = m + 1;
                                pFrom = GH.IndexOf(">") + ">".Length;
                                pTo = GH.LastIndexOf("<");
                                result = GH.Substring(pFrom, pTo - pFrom);
                                listBox2.Items.Add(result);
                                SectionLABEL[m] = result;
                                SectionDESCRIPTION[m] = DESCRIPTION;
                                SectionLENGTH_UNITS[m] = LENGTH_UNITS;
                                SelectedM[m] = m;
                            }
                            if (result == "EDI_STD")
                            {
                                pFrom = GH.IndexOf(">") + ">".Length;
                                pTo = GH.LastIndexOf("<");
                                result = GH.Substring(pFrom, pTo - pFrom);
                                SectionEDI_STD[m] = result;
                            }
                            if (result == "DESIGNATION")
                            {
                                pFrom = GH.IndexOf(">") + ">".Length;
                                pTo = GH.LastIndexOf("<");
                                result = GH.Substring(pFrom, pTo - pFrom);
                                SectionDESIGNATION[m] = result;
                            }


                            f1 = GH.Substring(0, 3);
                            f2 = GH.Substring(0, 4);
                            if (f1 == "  <" & f2 != "  </")
                            {
                                result = GH.Substring(3, Slength - 4);
                                SectionMODEL[m] = result;
                            }

                            if (result == "D")
                            {
                                pFrom = GH.IndexOf(">") + ">".Length;
                                pTo = GH.LastIndexOf("<");
                                result = GH.Substring(pFrom, pTo - pFrom);
                                SectionD[m] = result;
                            }
                            if (result == "BF")
                            {
                                pFrom = GH.IndexOf(">") + ">".Length;
                                pTo = GH.LastIndexOf("<");
                                result = GH.Substring(pFrom, pTo - pFrom);
                                SectionBF[m] = result;
                                SectionBFB[m] = result;
                            }
                            if (result == "TF")
                            {
                                pFrom = GH.IndexOf(">") + ">".Length;
                                pTo = GH.LastIndexOf("<");
                                result = GH.Substring(pFrom, pTo - pFrom);
                                SectionTF[m] = result;
                                SectionTFB[m] = result;
                            }

                            if (result == "BFB")
                            {
                                pFrom = GH.IndexOf(">") + ">".Length;
                                pTo = GH.LastIndexOf("<");
                                result = GH.Substring(pFrom, pTo - pFrom);
                                SectionBFB[m] = result;
                            }
                            if (result == "TFB")
                            {
                                pFrom = GH.IndexOf(">") + ">".Length;
                                pTo = GH.LastIndexOf("<");
                                result = GH.Substring(pFrom, pTo - pFrom);
                                SectionTFB[m] = result;
                            }

                            if (result == "TW")
                            {
                                pFrom = GH.IndexOf(">") + ">".Length;
                                pTo = GH.LastIndexOf("<");
                                result = GH.Substring(pFrom, pTo - pFrom);
                                SectionTW[m] = result;
                            }
                            if (result == "A")
                            {
                                pFrom = GH.IndexOf(">") + ">".Length;
                                pTo = GH.LastIndexOf("<");
                                result = GH.Substring(pFrom, pTo - pFrom);
                                SectionA[m] = result;
                            }
                            if (result == "I33")
                            {
                                pFrom = GH.IndexOf(">") + ">".Length;
                                pTo = GH.LastIndexOf("<");
                                result = GH.Substring(pFrom, pTo - pFrom);
                                SectionI33[m] = result;
                            }
                            if (result == "Z33")
                            {
                                pFrom = GH.IndexOf(">") + ">".Length;
                                pTo = GH.LastIndexOf("<");
                                result = GH.Substring(pFrom, pTo - pFrom);
                                SectionZ33[m] = result;
                            }
                            if (result == "AS3")
                            {
                                pFrom = GH.IndexOf(">") + ">".Length;
                                pTo = GH.LastIndexOf("<");
                                result = GH.Substring(pFrom, pTo - pFrom);
                                SectionAS3[m] = result;
                            }
                            if (result == "I22")
                            {
                                pFrom = GH.IndexOf(">") + ">".Length;
                                pTo = GH.LastIndexOf("<");
                                result = GH.Substring(pFrom, pTo - pFrom);
                                SectionI22[m] = result;
                            }
                            if (result == "Z22")
                            {
                                pFrom = GH.IndexOf(">") + ">".Length;
                                pTo = GH.LastIndexOf("<");
                                result = GH.Substring(pFrom, pTo - pFrom);
                                SectionZ22[m] = result;
                            }
                            if (result == "AS2")
                            {
                                pFrom = GH.IndexOf(">") + ">".Length;
                                pTo = GH.LastIndexOf("<");
                                result = GH.Substring(pFrom, pTo - pFrom);
                                SectionAS2[m] = result;
                            }
                            if (result == "J")
                            {
                                pFrom = GH.IndexOf(">") + ">".Length;
                                pTo = GH.LastIndexOf("<");
                                result = GH.Substring(pFrom, pTo - pFrom);
                                SectionJ[m] = result;
                            }
                            if (result == "X")
                            {
                                pFrom = GH.IndexOf(">") + ">".Length;
                                pTo = GH.LastIndexOf("<");
                                result = GH.Substring(pFrom, pTo - pFrom);
                                SectionX[m] = result;
                            }
                            if (result == "Y")
                            {
                                pFrom = GH.IndexOf(">") + ">".Length;
                                pTo = GH.LastIndexOf("<");
                                result = GH.Substring(pFrom, pTo - pFrom);
                                SectionY[m] = result;
                            }
                            if (result == "S33POS")
                            {
                                pFrom = GH.IndexOf(">") + ">".Length;
                                pTo = GH.LastIndexOf("<");
                                result = GH.Substring(pFrom, pTo - pFrom);
                                SectionS33POS[m] = result;
                            }
                            if (result == "S33NEG")
                            {
                                pFrom = GH.IndexOf(">") + ">".Length;
                                pTo = GH.LastIndexOf("<");
                                result = GH.Substring(pFrom, pTo - pFrom);
                                SectionS33NEG[m] = result;
                            }
                            if (result == "S22POS")
                            {
                                pFrom = GH.IndexOf(">") + ">".Length;
                                pTo = GH.LastIndexOf("<");
                                result = GH.Substring(pFrom, pTo - pFrom);
                                SectionS22POS[m] = result;
                            }
                            if (result == "S22NEG")
                            {
                                pFrom = GH.IndexOf(">") + ">".Length;
                                pTo = GH.LastIndexOf("<");
                                result = GH.Substring(pFrom, pTo - pFrom);
                                SectionS22NEG[m] = result;
                            }
                            if (result == "R33")
                            {
                                pFrom = GH.IndexOf(">") + ">".Length;
                                pTo = GH.LastIndexOf("<");
                                result = GH.Substring(pFrom, pTo - pFrom);
                                SectionR33[m] = result;
                            }
                            if (result == "R22")
                            {
                                pFrom = GH.IndexOf(">") + ">".Length;
                                pTo = GH.LastIndexOf("<");
                                result = GH.Substring(pFrom, pTo - pFrom);
                                SectionR22[m] = result;
                            }


                            if (result == "B")////L
                            {
                                pFrom = GH.IndexOf(">") + ">".Length;
                                pTo = GH.LastIndexOf("<");
                                result = GH.Substring(pFrom, pTo - pFrom);
                                SectionB[m] = result;
                            }

                            if (result == "HT")////B
                            {
                                pFrom = GH.IndexOf(">") + ">".Length;
                                pTo = GH.LastIndexOf("<");
                                result = GH.Substring(pFrom, pTo - pFrom);
                                SectionHT[m] = result;
                            }
                            if (result == "OD")////P
                            {
                                pFrom = GH.IndexOf(">") + ">".Length;
                                pTo = GH.LastIndexOf("<");
                                result = GH.Substring(pFrom, pTo - pFrom);
                                SectionOD[m] = result;
                            }
                            if (result == "TDES")////P
                            {
                                pFrom = GH.IndexOf(">") + ">".Length;
                                pTo = GH.LastIndexOf("<");
                                result = GH.Substring(pFrom, pTo - pFrom);
                                SectionTDES[m] = result;
                            }
                            f1 = GH.Substring(0, 3);
                            f2 = GH.Substring(0, 4);
                            if (f1 == "  <" & f2 != "  </")
                            {
                                ff = GH.Substring(3, Slength - 4);
                                if (laststring != ff)
                                {
                                    laststring = ff;
                                    listBox1.Items.Add(ff);
                                }
                            }
                        }
                    }
                    AllSections = m;
                    SW.Close();
                }
                catch
                {
                    MessageBox.Show("ERROR! Please check passphrase and do not attempt to edit cipher text");
                }
            }
        }

        private void SectionsFrm_Load(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            string text = listBox1.GetItemText(listBox1.SelectedItem);
            int m = 0;
            if (text == "ALL")
            {
                for (int i = 1; i < AllSections + 1; i++)
                {
                    m = m + 1;
                    SelectedM[m] = i;
                    listBox2.Items.Add(SectionLABEL[i]);
                }
                goto endloop;
            }

            for (int i = 1; i < AllSections + 1;i++ )
            {
                if (SectionMODEL[i] == text)
                {
                    m = m + 1;
                    SelectedM[m]=i;
                    listBox2.Items.Add(SectionLABEL[i]);
                }
            }
            endloop:
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int index;
            int i = 0;
            for (int m = 0; m < listBox2.SelectedItems.Count; m++)
            {
                Section.Numberd = 1 + Section.Numberd;
                i = Section.Numberd;
                index = SelectedM[listBox2.SelectedIndices[m] + 1];
                Section.MODELd[i] = SectionMODEL[index];
                Section.DESCRIPTIONd[i] = SectionDESCRIPTION[index];
                Section.LENGTH_UNITSd[i] = SectionLENGTH_UNITS[index];
                Section.LABELd[i] = SectionLABEL[index];
                Section.DESIGNATIONd[i] = SectionDESIGNATION[index];
                Section.Dd[i] =Convert.ToDouble (SectionD[index]);
                Section.BFd[i] = Convert.ToDouble (SectionBF[index]);
                Section.TFd[i] =Convert.ToDouble ( SectionTF[index]);
                Section.BFBd[i] =Convert.ToDouble ( SectionBFB[index]);
                Section.TFBd[i] = Convert.ToDouble (SectionTFB[index]);
                Section.TWd[i] = Convert.ToDouble (SectionTW[index]);
                Section.Ad[i] =Convert.ToDouble ( SectionA[index]);
                Section.I33d[i] =Convert.ToDouble ( SectionI33[index]);
                Section.Z33d[i] = Convert.ToDouble (SectionZ33[index]);
                Section.AS3d[i] = Convert.ToDouble (SectionAS3[index]);
                Section.I22d[i] = Convert.ToDouble (SectionI22[index]);
                Section.Z22d[i] =Convert.ToDouble ( SectionZ22[index]);
                Section.AS2d[i] = Convert.ToDouble (SectionAS2[index]);
                Section.Jd[i] = Convert.ToDouble (SectionJ[index]);
                Section.Xd[i] = Convert.ToDouble (SectionX[index]);
                Section.Yd[i] = Convert.ToDouble (SectionY[index]);
                Section.S33POSd[i] = Convert.ToDouble (SectionS33POS[index]);
                Section.S33NEGd[i] = Convert.ToDouble (SectionS33NEG[index]);
                Section.S22POSd[i] = Convert.ToDouble (SectionS22POS[index]);
                Section.S22NEGd[i] = Convert.ToDouble (SectionS22NEG[index]);
                Section.R33d[i] = Convert.ToDouble (SectionR33[index]);
                Section.R22d[i] = Convert.ToDouble (SectionR22[index]);
                Section.Bd[i] = Convert.ToDouble (SectionB[index]);
                Section.HTd[i] = Convert.ToDouble(SectionHT[index]);
                Section.ODd[i] = Convert.ToDouble (SectionOD[index]);
                Section.TDESd[i] = Convert.ToDouble (SectionTDES[index]);
                ((FramePropertiesFrm)framePropertiesFrm).listBox1.Items.Add(Section.LABELd[i]);
            }
            this.Close();
        }
       

    }
}
