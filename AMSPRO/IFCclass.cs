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
    class IFCclass
    {
        Form mainForm = Application.OpenForms["MainForm"];
        double TheX = 0;
        double TheY = 0;
        public void ImportIFC()
        {
            try
            {
            int m = 0;
            int StoryNumbers = 0;
            int DirectionNumbers = 0;
            int PointNumbers = 0;
            int SectionNumbers = 0;
            int MaterialNumbers = 0;
            int BeamNumbers = 0;
            int SlabNumbers = 0;
            int WallNumbers = 0;
            int PowerJointNumber = 0;
            string TheElement = "";
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = System.IO.Directory.GetParent(@"./").FullName;
            openFileDialog1.Filter = "ifc files (*.IFC)|*.IFC|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                string strpath = openFileDialog1.FileName;
                StreamReader SW = new StreamReader(strpath);
                string[] lines = System.IO.File.ReadAllLines(strpath);
                int Nlines = lines.Length;
                #region//تعريفات
                for (int i = 0; i < Nlines; ++i)
                {
                    string GH = Convert.ToString(SW.ReadLine());
                    if (GH != "")
                    {
                        int Slength = GH.Length;
                        int pFrom = 0;
                        int pTo = 1;
                        string result = GH.Substring(pFrom, pTo - pFrom);
                        if (result == "#")
                        {
                            pFrom = GH.IndexOf("#") + "#".Length;
                            pTo = GH.IndexOf("=");
                            if (pTo == -1) goto EndResult0;
                            result = GH.Substring(pFrom, pTo - pFrom);
                            pFrom = GH.IndexOf("=") + "=".Length;
                            pTo = GH.IndexOf("(");
                            result = GH.Substring(pFrom, pTo - pFrom);
                            if (result == "IFCBEAM") BeamNumbers = BeamNumbers + 1;
                            if (result == "IFCCOLUMN") BeamNumbers = BeamNumbers + 1;
                            if (result == "IFCSLAB") SlabNumbers = SlabNumbers + 1;
                            if (result == "IFCWALLSTANDARDCASE") WallNumbers = WallNumbers + 1;
                            if (result == "IFCSTRUCTURALPROFILEPROPERTIES") SectionNumbers = SectionNumbers + 1;
                            if (result == "IFCMATERIAL") MaterialNumbers = MaterialNumbers + 1;
                            if (result == "IFCDIRECTION") DirectionNumbers = DirectionNumbers + 1;
                            if (result == "IFCCARTESIANPOINT") PointNumbers = PointNumbers + 1;
                            if (result == "IFCBUILDINGSTOREY") StoryNumbers = StoryNumbers + 1;
                            if (result == "IFCSTRUCTURALLOADSINGLEFORCE") PowerJointNumber = PowerJointNumber + 1;                          
                        }
                    }
                EndResult0: { }
                }
                SW.Close();
                string[] StoryName = new string[StoryNumbers+1];
                string[] StoryLevel = new string[StoryNumbers+1];
                string[] DirectionId = new string[DirectionNumbers+1];
                string[] DirectionType = new string[DirectionNumbers + 1];
                string[] DirectionX = new string[DirectionNumbers + 1];
                string[] DirectionY = new string[DirectionNumbers + 1];
                string[] DirectionZ = new string[DirectionNumbers + 1];
                string[] PointId = new string[PointNumbers + 1];
                string[] PointType = new string[PointNumbers + 1];
                string[] PointX = new string[PointNumbers + 1];
                string[] PointY = new string[PointNumbers + 1];
                string[] PointZ = new string[PointNumbers + 1];
                string[] SectionName = new string[SectionNumbers + 1];
                string[] SectionID = new string[SectionNumbers + 1];
                string[] SectionAria = new string[SectionNumbers + 1];
                string[] SectionJ = new string[SectionNumbers + 1];
                string[] SectionI33 = new string[SectionNumbers + 1];
                string[] SectionI22 = new string[SectionNumbers + 1];
                string[] SectionAS2 = new string[SectionNumbers + 1];
                string[] SectionAS3 = new string[SectionNumbers + 1];
                string[] SectionS33P = new string[SectionNumbers + 1];
                string[] SectionS33N = new string[SectionNumbers + 1];
                string[] SectionS22P = new string[SectionNumbers + 1];
                string[] SectionS22N = new string[SectionNumbers + 1];
                string[] SectionMaterial = new string[SectionNumbers + 1];
                string[] SectionMaterialExtend = new string[SectionNumbers + 1];
                string[] SectionKind = new string[SectionNumbers + 1];
                string[] SectionD = new string[SectionNumbers + 1];
                string[] SectionBF = new string[SectionNumbers + 1];
                string[] SectionTF = new string[SectionNumbers + 1];
                string[] SectionBFB = new string[SectionNumbers + 1];
                string[] SectionTFB = new string[SectionNumbers + 1];
                string[] SectionTW = new string[SectionNumbers + 1];
                string[] MterialID = new string[MaterialNumbers + 1];
                string[] MterialName = new string[MaterialNumbers + 1];
                string[] MterialMassDensity = new string[MaterialNumbers + 1];
                string[] MterialPoissonRatio = new string[MaterialNumbers + 1];
                string[] MterialShearModulus = new string[MaterialNumbers + 1];
                string[] MterialThermalExpansionCoefficient = new string[MaterialNumbers + 1];
                string[] MterialUltimateStress = new string[MaterialNumbers + 1];
                string[] MterialYieldStress = new string[MaterialNumbers + 1];
                string[] MterialYoungModulus = new string[MaterialNumbers + 1];
                string[] MterialCompressiveStrength = new string[MaterialNumbers + 1];
                string[] BeamLength = new string[BeamNumbers + 1];
                string[] BeamSection = new string[BeamNumbers + 1];
                string[] BeamNode = new string[BeamNumbers + 1];
                string[] BeamDirection1 = new string[BeamNumbers + 1];
                string[] BeamDirection2 = new string[BeamNumbers + 1];
                string[] SlabThick = new string[SlabNumbers + 1];
                string[] SlabMaterial = new string[SlabNumbers + 1];
                string[] SlabMaterialExtend = new string[SlabNumbers + 1];
                int[] SlabPointNumbers = new int[SlabNumbers + 1];
                string[,] SlabNode = new string[SlabNumbers + 1, 10000];
                string[] SlabInsertionNode = new string[SlabNumbers + 1];
                string[] SlabDirection1 = new string[SlabNumbers + 1];
                string[] SlabDirection2 = new string[SlabNumbers + 1];
                string[] WallThick = new string[WallNumbers + 1];
                string[] WallHight = new string[WallNumbers + 1];
                string[] WallMaterial = new string[WallNumbers + 1];
                string[] WallMaterialExtend = new string[WallNumbers + 1];
                int[] WallPointNumbers = new int[WallNumbers + 1];
                string[,] WallNode = new string[WallNumbers + 1, 6];
                string[] WallInsertionNode = new string[WallNumbers + 1];
                string[] WallDirection1 = new string[WallNumbers + 1];
                string[] WallDirection2 = new string[WallNumbers + 1];
                string[] LoadPX = new string[PowerJointNumber + 1];
                string[] LoadPY = new string[PowerJointNumber + 1];
                string[] LoadPZ = new string[PowerJointNumber + 1];
                string[] LoadMX = new string[PowerJointNumber + 1];
                string[] LoadMY = new string[PowerJointNumber + 1];
                string[] LoadMZ = new string[PowerJointNumber + 1];
                string[] LoadNode = new string[PowerJointNumber + 1];
                #endregion
                #region//جلب العناصر
                m = 0;
                 StoryNumbers = 0;
                 DirectionNumbers = 0;
                 PointNumbers = 0;
                 SectionNumbers = 0;
                 MaterialNumbers = 0;
                 BeamNumbers = 0;
                 SlabNumbers = 0;
                 WallNumbers = 0;
                 PowerJointNumber = 0;
                 SW = new StreamReader(strpath);
                 lines = System.IO.File.ReadAllLines(strpath);
                 Nlines = lines.Length;
                 string[] LCount = new string[Nlines + 1];
                 string[] LLbel = new string[Nlines + 1];
                for (int i = 0; i < Nlines; ++i)
                {
                    string GH = Convert.ToString(SW.ReadLine());
                    if (GH != "")
                    {
                        int Slength = GH.Length;
                        int pFrom = 0;
                        int pTo = 1;
                        string result = GH.Substring(pFrom, pTo - pFrom);
                        string result1 = "";
                        if (result == "#")
                        {
                            pFrom = GH.IndexOf("#") + "#".Length;
                            pTo = GH.IndexOf("=");
                            if (pTo == -1) goto EndResult;
                            result = GH.Substring(pFrom, pTo - pFrom);
                            m = m + 1;
                            LCount[m] = result;//ترتيب الخاصية
                            pFrom = GH.IndexOf("=") + "=".Length;
                            pTo = GH.IndexOf("(");
                            result = GH.Substring(pFrom, pTo - pFrom);
                            LLbel[m] = result;//اسم الخاصية
                            if (result == "IFCBEAM")
                            {
                                TheElement = result;
                                BeamNumbers = BeamNumbers + 1;
                            }
                            if (result == "IFCCOLUMN")
                            {
                                TheElement = result;
                                BeamNumbers = BeamNumbers + 1;
                            }
                            if (result == "IFCSLAB")
                            {
                                SlabNumbers = SlabNumbers + 1;
                                TheElement = result;
                            }
                            if (result == "IFCWALLSTANDARDCASE")
                            {
                                WallNumbers = WallNumbers + 1;
                                TheElement = result;
                            }
                            if (result == "IFCSTRUCTURALLOADSINGLEFORCE")
                            {
                                PowerJointNumber = PowerJointNumber + 1;
                                TheElement = result;
                            }
                            #region  //جلب المقاطع
                            if (result == "IFCSTRUCTURALPROFILEPROPERTIES")
                            {
                                SectionNumbers = SectionNumbers + 1;
                                pFrom = GH.IndexOf("('") + "('".Length;
                                pTo = GH.IndexOf("',");
                                result = GH.Substring(pFrom, pTo - pFrom);
                                SectionName[SectionNumbers] = result;//اسم المقطع
                                //--------------------------------------------جلب خصائص المقطع
                                pFrom = GH.IndexOf(",") + ",".Length;
                                pTo = GH.Length;
                                string GH1 = GH.Substring(pFrom, pTo - pFrom);
                                int GH1Length = GH1.Length;
                                string theword = "";
                                int countwords = 0;

                                for (int k = 0; k < GH1Length; k++)
                                {
                                    result = GH1.Substring(k, 1);
                                    if (result != ",")
                                    {
                                        theword = theword + result;
                                    }
                                    else
                                    {
                                        countwords = countwords + 1;
                                        if (countwords == 6) SectionAria[SectionNumbers] = theword;
                                        if (countwords == 7) SectionJ[SectionNumbers] = theword;
                                        if (countwords == 9) SectionI33[SectionNumbers] = theword;
                                        if (countwords == 10) SectionI22[SectionNumbers] = theword;
                                        if (countwords == 14) SectionAS2[SectionNumbers] = theword;
                                        if (countwords == 15) SectionAS3[SectionNumbers] = theword;
                                        if (countwords == 16) SectionS33P[SectionNumbers] = theword;
                                        if (countwords == 17) SectionS33N[SectionNumbers] = theword;
                                        if (countwords == 18) SectionS22P[SectionNumbers] = theword;
                                        if (countwords == 19) SectionS22N[SectionNumbers] = theword;
                                        theword = "";
                                    }
                                }
                            }
                            if (TheElement == "IFCBEAM" || TheElement == "IFCCOLUMN")
                            {
                                if (result == "IFCEXTENDEDMATERIALPROPERTIES")//مادة المقطع
                                {
                                    pFrom = GH.IndexOf(",'") + ",'".Length;
                                    pTo = GH.IndexOf("')");
                                    result1 = GH.Substring(pFrom, pTo - pFrom);
                                    SectionMaterial[SectionNumbers] = result1;
                                }
                                if (result == "IFCRELASSOCIATESMATERIAL")//مادة المقطع
                                {
                                    pFrom = GH.IndexOf("),#") + "),#".Length;
                                    pTo = GH.IndexOf(");");
                                    result1 = GH.Substring(pFrom, pTo - pFrom);
                                    SectionMaterialExtend[SectionNumbers] = result1;
                                }
                                #region//مقطع L
                                if (result == "IFCLSHAPEPROFILEDEF")
                                {
                                    SectionID[SectionNumbers] = LCount[m];
                                    SectionKind[SectionNumbers] = "L";
                                    pFrom = GH.IndexOf("',") + "',".Length;
                                    pTo = GH.Length;
                                    string GH1 = GH.Substring(pFrom, pTo - pFrom);
                                    int GH1Length = GH1.Length;
                                    string theword = "";
                                    int countwords = 0;
                                    for (int k = 0; k < GH1Length; k++)
                                    {
                                        result = GH1.Substring(k, 1);
                                        if (result != ",")
                                        {
                                            theword = theword + result;
                                        }
                                        else
                                        {
                                            countwords = countwords + 1;
                                            if (countwords == 2) SectionD[SectionNumbers] = theword;
                                            if (countwords == 3)
                                            {
                                                SectionBFB[SectionNumbers] = theword;
                                                SectionBF[SectionNumbers] = theword;
                                            }
                                            if (countwords == 4) SectionTW[SectionNumbers] = theword;
                                            if (countwords == 5 & theword =="$")
                                            {
                                                SectionTFB[SectionNumbers] = SectionTW[SectionNumbers];
                                                SectionTF[SectionNumbers] = SectionTW[SectionNumbers];
                                            }
                                            if (countwords == 5 & theword != "$")
                                            {
                                                SectionTFB[SectionNumbers] = theword;
                                                SectionTF[SectionNumbers] = theword;
                                            }
                                            theword = "";
                                        }
                                    }
                                }
                                #endregion
                                #region//مقطع T
                                if (result == "IFCTSHAPEPROFILEDEF")
                                {
                                    SectionID[SectionNumbers] = LCount[m];
                                    SectionKind[SectionNumbers] = "T";
                                    pFrom = GH.IndexOf("',") + "',".Length;
                                    pTo = GH.Length;
                                    string GH1 = GH.Substring(pFrom, pTo - pFrom);
                                    int GH1Length = GH1.Length;
                                    string theword = "";
                                    int countwords = 0;
                                    for (int k = 0; k < GH1Length; k++)
                                    {
                                        result = GH1.Substring(k, 1);
                                        if (result != ",")
                                        {
                                            theword = theword + result;
                                        }
                                        else
                                        {
                                            countwords = countwords + 1;
                                            if (countwords == 2) SectionD[SectionNumbers] = theword;
                                            if (countwords == 3)
                                            {
                                                SectionBFB[SectionNumbers] = theword;
                                                SectionBF[SectionNumbers] = theword;
                                            }
                                            if (countwords == 4) SectionTW[SectionNumbers] = theword;
                                            if (countwords == 5)
                                            {
                                                SectionTFB[SectionNumbers] = theword;
                                                SectionTF[SectionNumbers] = theword;
                                            }
                                            theword = "";
                                        }
                                    }
                                }
                                #endregion
                                #region//مقطع C
                                if (result == "IFCUSHAPEPROFILEDEF")
                                {
                                    SectionID[SectionNumbers] = LCount[m];
                                    SectionKind[SectionNumbers] = "C";
                                    pFrom = GH.IndexOf("',") + "',".Length;
                                    pTo = GH.Length;
                                    string GH1 = GH.Substring(pFrom, pTo - pFrom);
                                    int GH1Length = GH1.Length;
                                    string theword = "";
                                    int countwords = 0;
                                    for (int k = 0; k < GH1Length; k++)
                                    {
                                        result = GH1.Substring(k, 1);
                                        if (result != ",")
                                        {
                                            theword = theword + result;
                                        }
                                        else
                                        {
                                            countwords = countwords + 1;
                                            if (countwords == 2) SectionD[SectionNumbers] = theword;
                                            if (countwords == 3)
                                            {
                                                SectionBFB[SectionNumbers] = theword;
                                                SectionBF[SectionNumbers] = theword;
                                            }
                                            if (countwords == 4) SectionTW[SectionNumbers] = theword;
                                            if (countwords == 5)
                                            {
                                                SectionTFB[SectionNumbers] = theword;
                                                SectionTF[SectionNumbers] = theword;
                                            }
                                            theword = "";
                                        }
                                    }
                                }
                                #endregion
                                #region//مقطع اي بسيط
                                if (result == "IFCISHAPEPROFILEDEF")
                                {
                                    SectionID[SectionNumbers] = LCount[m];
                                    SectionKind[SectionNumbers] = "I";
                                    pFrom = GH.IndexOf("',") + "',".Length;
                                    pTo = GH.Length;
                                    string GH1 = GH.Substring(pFrom, pTo - pFrom);
                                    int GH1Length = GH1.Length;
                                    string theword = "";
                                    int countwords = 0;
                                    for (int k = 0; k < GH1Length; k++)
                                    {
                                        result = GH1.Substring(k, 1);
                                        if (result != ",")
                                        {
                                            theword = theword + result;
                                        }
                                        else
                                        {
                                            countwords = countwords + 1;
                                            if (countwords == 2)
                                            {
                                                SectionBFB[SectionNumbers] = theword;
                                                SectionBF[SectionNumbers] = theword;
                                            }
                                            if (countwords == 3) SectionD[SectionNumbers] = theword;
                                            if (countwords == 4) SectionTW[SectionNumbers] = theword;
                                            if (countwords == 5)
                                            {
                                                SectionTFB[SectionNumbers] = theword;
                                                SectionTF[SectionNumbers] = theword;
                                            }
                                            theword = "";
                                        }
                                    }
                                }
                                #endregion
                                #region//  مقطع اي معقد
                                if (result == "IFCASYMMETRICISHAPEPROFILEDEF")
                                {
                                    SectionID[SectionNumbers] = LCount[m];
                                    SectionKind[SectionNumbers] = "I";
                                    pFrom = GH.IndexOf("',") + "',".Length;
                                    pTo = GH.Length;
                                    string GH1 = GH.Substring(pFrom, pTo - pFrom);
                                    int GH1Length = GH1.Length;
                                    string theword = "";
                                    int countwords = 0;
                                    for (int k = 0; k < GH1Length; k++)
                                    {
                                        result = GH1.Substring(k, 1);
                                        if (result != ",")
                                        {
                                            theword = theword + result;
                                        }
                                        else
                                        {
                                            countwords = countwords + 1;
                                            if (countwords == 2) SectionBFB[SectionNumbers] = theword;
                                            if (countwords == 3) SectionD[SectionNumbers] = theword;
                                            if (countwords == 4) SectionTW[SectionNumbers] = theword;
                                            if (countwords == 5) SectionTFB[SectionNumbers] = theword;
                                            if (countwords == 6) SectionBF[SectionNumbers] = theword;
                                            if (countwords == 7) SectionTF[SectionNumbers] = theword;
                                            theword = "";
                                        }
                                    }
                                }
                                #endregion
                                #region//مقطع مستطيل
                                if (result == "IFCRECTANGLEPROFILEDEF")
                                {
                                    SectionID[SectionNumbers] = LCount[m];
                                    SectionKind[SectionNumbers] = "B";
                                    pFrom = GH.IndexOf("',") + "',".Length;
                                    pTo = GH.Length;
                                    string GH1 = GH.Substring(pFrom, pTo - pFrom);
                                    int GH1Length = GH1.Length;
                                    string theword = "";
                                    int countwords = 0;
                                    for (int k = 0; k < GH1Length; k++)
                                    {
                                        result = GH1.Substring(k, 1);
                                        if (result != "," & result != ")")
                                        {
                                            theword = theword + result;
                                        }
                                        else
                                        {
                                            countwords = countwords + 1;
                                            if (countwords == 2) SectionBF[SectionNumbers] = theword;
                                            if (countwords == 3) SectionD[SectionNumbers] = theword;
                                            theword = "";
                                        }
                                    }
                                }
                                #endregion
                            }
                            #endregion
                            #region//جلب المواد
                            if (result == "IFCMATERIAL")
                            {
                                MaterialNumbers = MaterialNumbers + 1;
                                MterialID[MaterialNumbers] = LCount[m];
                                pFrom = GH.IndexOf("('") + "('".Length;
                                pTo = GH.IndexOf("')");
                                MterialName[MaterialNumbers] = GH.Substring(pFrom, pTo - pFrom);
                                result1 = "";
                            startone: { }
                                int tah = 0;
                                i = i + 1;
                                GH = Convert.ToString(SW.ReadLine());
                                pFrom = GH.IndexOf("('") + "('".Length;
                                pTo = GH.IndexOf("',");
                                if (pTo != -1) result1 = GH.Substring(pFrom, pTo - pFrom);
                                if (result1 == "MassDensity")
                                {
                                    tah = 1;
                                    m = m + 1;
                                    pFrom = GH.IndexOf("#") + "#".Length;
                                    pTo = GH.IndexOf("=");
                                    result1 = GH.Substring(pFrom, pTo - pFrom);
                                    LCount[m] = result1;//ترتيب الخاصية
                                    pFrom = GH.IndexOf("RE(") + "RE(".Length;
                                    pTo = GH.IndexOf("),");
                                    MterialMassDensity[MaterialNumbers] = GH.Substring(pFrom, pTo - pFrom);
                                }
                                if (result1 == "PoissonRatio")
                                {
                                    tah = 1;
                                    m = m + 1;
                                    pFrom = GH.IndexOf("#") + "#".Length;
                                    pTo = GH.IndexOf("=");
                                    result1 = GH.Substring(pFrom, pTo - pFrom);
                                    LCount[m] = result1;//ترتيب الخاصية
                                    pFrom = GH.IndexOf("RE(") + "RE(".Length;
                                    pTo = GH.IndexOf("),");
                                    MterialPoissonRatio[MaterialNumbers] = GH.Substring(pFrom, pTo - pFrom);
                                }
                                if (result1 == "ShearModulus")
                                {
                                    tah = 1;
                                    m = m + 1;
                                    pFrom = GH.IndexOf("#") + "#".Length;
                                    pTo = GH.IndexOf("=");
                                    result1 = GH.Substring(pFrom, pTo - pFrom);
                                    LCount[m] = result1;//ترتيب الخاصية
                                    pFrom = GH.IndexOf("RE(") + "RE(".Length;
                                    pTo = GH.IndexOf("),");
                                    MterialShearModulus[MaterialNumbers] = GH.Substring(pFrom, pTo - pFrom);
                                }
                                if (result1 == "ThermalExpansionCoefficient")
                                {
                                    tah = 1;
                                    m = m + 1;
                                    pFrom = GH.IndexOf("#") + "#".Length;
                                    pTo = GH.IndexOf("=");
                                    result1 = GH.Substring(pFrom, pTo - pFrom);
                                    LCount[m] = result1;//ترتيب الخاصية
                                    pFrom = GH.IndexOf("RE(") + "RE(".Length;
                                    pTo = GH.IndexOf("),");
                                    MterialThermalExpansionCoefficient[MaterialNumbers] = GH.Substring(pFrom, pTo - pFrom);
                                }
                                if (result1 == "UltimateStress")
                                {
                                    tah = 1;
                                    m = m + 1;
                                    pFrom = GH.IndexOf("#") + "#".Length;
                                    pTo = GH.IndexOf("=");
                                    result1 = GH.Substring(pFrom, pTo - pFrom);
                                    LCount[m] = result1;//ترتيب الخاصية
                                    pFrom = GH.IndexOf("RE(") + "RE(".Length;
                                    pTo = GH.IndexOf("),");
                                    MterialUltimateStress[MaterialNumbers] = GH.Substring(pFrom, pTo - pFrom);
                                }
                                if (result1 == "YieldStress")
                                {
                                    tah = 1;
                                    m = m + 1;
                                    pFrom = GH.IndexOf("#") + "#".Length;
                                    pTo = GH.IndexOf("=");
                                    result1 = GH.Substring(pFrom, pTo - pFrom);
                                    LCount[m] = result1;//ترتيب الخاصية
                                    pFrom = GH.IndexOf("RE(") + "RE(".Length;
                                    pTo = GH.IndexOf("),");
                                    MterialYieldStress[MaterialNumbers] = GH.Substring(pFrom, pTo - pFrom);
                                }
                                if (result1 == "CompressiveStrength")
                                {
                                    tah = 1;
                                    m = m + 1;
                                    pFrom = GH.IndexOf("#") + "#".Length;
                                    pTo = GH.IndexOf("=");
                                    result1 = GH.Substring(pFrom, pTo - pFrom);
                                    LCount[m] = result1;//ترتيب الخاصية
                                    pFrom = GH.IndexOf("RE(") + "RE(".Length;
                                    pTo = GH.IndexOf("),");
                                    MterialCompressiveStrength[MaterialNumbers] = GH.Substring(pFrom, pTo - pFrom);
                                }
                                if (result1 == "YoungModulus")
                                {
                                    tah = 1;
                                    m = m + 1;
                                    pFrom = GH.IndexOf("#") + "#".Length;
                                    pTo = GH.IndexOf("=");
                                    result1 = GH.Substring(pFrom, pTo - pFrom);
                                    LCount[m] = result1;//ترتيب الخاصية
                                    pFrom = GH.IndexOf("RE(") + "RE(".Length;
                                    pTo = GH.IndexOf("),");
                                    MterialYoungModulus[MaterialNumbers] = GH.Substring(pFrom, pTo - pFrom);
                                    goto ENDMATERIAL;
                                }
                                if (tah == 1) goto startone;
                            ENDMATERIAL: { }
                            }
                            #endregion
                            #region//جلب الاتجاهات
                            if (result == "IFCDIRECTION")
                            {
                                DirectionNumbers = DirectionNumbers + 1;
                                DirectionId[DirectionNumbers] = LCount[m];
                                pFrom = GH.IndexOf("((") + "((".Length;
                                pTo = GH.Length;
                                string GH1 = GH.Substring(pFrom, pTo - pFrom);
                                int GH1Length = GH1.Length;
                                string theword = "";
                                int countwords = 0;
                                for (int k = 0; k < GH1Length; k++)
                                {
                                    result = GH1.Substring(k, 1);
                                    if (result != "," & result != ")")
                                    {
                                        theword = theword + result;
                                    }
                                    else
                                    {
                                        countwords = countwords + 1;
                                        if (countwords == 1) DirectionX[DirectionNumbers] = theword;
                                        if (countwords == 2) DirectionY[DirectionNumbers] = theword;
                                        if (countwords == 3) DirectionZ[DirectionNumbers] = theword;
                                        theword = "";
                                    }
                                }
                                DirectionType[DirectionNumbers] = (countwords - 1).ToString();
                            }
                            #endregion
                            #region//جلب النقاط
                            if (result == "IFCCARTESIANPOINT")
                            {
                                PointNumbers = PointNumbers + 1;
                                PointId[PointNumbers] = LCount[m];
                                pFrom = GH.IndexOf("((") + "((".Length;
                                pTo = GH.Length;
                                string GH1 = GH.Substring(pFrom, pTo - pFrom);
                                int GH1Length = GH1.Length;
                                string theword = "";
                                int countwords = 0;
                                for (int k = 0; k < GH1Length; k++)
                                {
                                    result = GH1.Substring(k, 1);
                                    if (result != "," & result != ")")
                                    {
                                        theword = theword + result;
                                    }
                                    else
                                    {
                                        countwords = countwords + 1;
                                        if (countwords == 1) PointX[PointNumbers] = theword;
                                        if (countwords == 2) PointY[PointNumbers] = theword;
                                        if (countwords == 3) PointZ[PointNumbers] = theword;
                                        theword = "";
                                    }
                                }
                                PointType[PointNumbers] = (countwords - 1).ToString();
                            }
                            #endregion
                            #region//جلب  العناصر الخطية
                            if (TheElement == "IFCBEAM" || TheElement == "IFCCOLUMN")
                            {
                                if (result == "IFCAXIS2PLACEMENT3D")//الموقع و التجاه
                                {
                                    pFrom = GH.IndexOf("(") + "(".Length;
                                    pTo = GH.Length;
                                    string GH1 = GH.Substring(pFrom, pTo - pFrom);
                                    int GH1Length = GH1.Length;
                                    string theword = "";
                                    int countwords = 0;
                                    for (int k = 0; k < GH1Length; k++)
                                    {
                                        result = GH1.Substring(k, 1);
                                        if (result != "," & result != ")")
                                        {
                                            theword = theword + result;
                                        }
                                        else
                                        {
                                            countwords = countwords + 1;
                                            if (countwords == 1) BeamNode[BeamNumbers] = theword;
                                            if (countwords == 2) BeamDirection1[BeamNumbers] = theword;
                                            if (countwords == 3) BeamDirection2[BeamNumbers] = theword;
                                            theword = "";
                                        }
                                    }
                                }
                                if (result == "IFCEXTRUDEDAREASOLID")//الطول
                                {
                                    pFrom = GH.IndexOf("(") + "(".Length;
                                    pTo = GH.Length;
                                    string GH1 = GH.Substring(pFrom, pTo - pFrom);
                                    int GH1Length = GH1.Length;
                                    string theword = "";
                                    int countwords = 0;
                                    for (int k = 0; k < GH1Length; k++)
                                    {
                                        result = GH1.Substring(k, 1);
                                        if (result != "," & result != ")")
                                        {
                                            theword = theword + result;
                                        }
                                        else
                                        {
                                            countwords = countwords + 1;
                                            if (countwords == 1)
                                            {
                                                if (theword != "#" + (Convert.ToInt32(LCount[m]) + 1).ToString())
                                                {
                                                    BeamSection[BeamNumbers] = theword;
                                                }
                                            }
                                            if (countwords == 4) BeamLength[BeamNumbers] = theword;
                                            theword = "";
                                        }
                                    }
                                }
                                if (result == "IFCDERIVEDPROFILEDEF")//المقطع
                                {
                                    pFrom = GH.IndexOf("(") + "(".Length;
                                    pTo = GH.Length;
                                    string GH1 = GH.Substring(pFrom, pTo - pFrom);
                                    int GH1Length = GH1.Length;
                                    string theword = "";
                                    int countwords = 0;
                                    for (int k = 0; k < GH1Length; k++)
                                    {
                                        result = GH1.Substring(k, 1);
                                        if (result != "," & result != ")")
                                        {
                                            theword = theword + result;
                                        }
                                        else
                                        {
                                            countwords = countwords + 1;
                                            if (countwords == 3) BeamSection[BeamNumbers] = theword;
                                            theword = "";
                                        }
                                    }
                                }
                            }
                            #endregion
                            #region//جلب  البلاطات
                            if (TheElement == "IFCSLAB")
                            {
                                if (result == "IFCMATERIALLAYER")//سماكة البلاطة ومادتها 
                                {
                                    pFrom = GH.IndexOf("(") + "(".Length;
                                    pTo = GH.Length;
                                    string GH1 = GH.Substring(pFrom, pTo - pFrom);
                                    int GH1Length = GH1.Length;
                                    string theword = "";
                                    int countwords = 0;
                                    for (int k = 0; k < GH1Length; k++)
                                    {
                                        result = GH1.Substring(k, 1);
                                        if (result != "," & result != ")")
                                        {
                                            theword = theword + result;
                                        }
                                        else
                                        {
                                            countwords = countwords + 1;
                                            if (countwords == 1) SlabMaterialExtend[SlabNumbers] = theword;
                                            if (countwords == 2) SlabThick[SlabNumbers] = theword;
                                            theword = "";
                                        }
                                    }
                                }
                                if (result == "IFCPOLYLINE")//نقاط العنصر المساحي   
                                {
                                    pFrom = GH.IndexOf("((") + "((".Length;
                                    pTo = GH.Length;
                                    string GH1 = GH.Substring(pFrom, pTo - pFrom);
                                    int GH1Length = GH1.Length;
                                    string theword = "";
                                    int countwords = 0;
                                    for (int k = 0; k < GH1Length; k++)
                                    {
                                        result = GH1.Substring(k, 1);
                                        if (result != "," & result != ")")
                                        {
                                            theword = theword + result;
                                        }
                                        else
                                        {
                                            if (theword != "")
                                            {
                                                countwords = countwords + 1;
                                                SlabPointNumbers[SlabNumbers] = countwords;
                                                SlabNode[SlabNumbers, countwords] = theword;
                                                theword = "";
                                            }
                                        }
                                    }
                                }
                                if (result == "IFCAXIS2PLACEMENT3D")//الموقع و التجاه
                                {
                                    pFrom = GH.IndexOf("(") + "(".Length;
                                    pTo = GH.Length;
                                    string GH1 = GH.Substring(pFrom, pTo - pFrom);
                                    int GH1Length = GH1.Length;
                                    string theword = "";
                                    int countwords = 0;
                                    for (int k = 0; k < GH1Length; k++)
                                    {
                                        result = GH1.Substring(k, 1);
                                        if (result != "," & result != ")")
                                        {
                                            theword = theword + result;
                                        }
                                        else
                                        {
                                            countwords = countwords + 1;
                                            if (countwords == 1) SlabInsertionNode[SlabNumbers] = theword;
                                            if (countwords == 2) SlabDirection1[SlabNumbers] = theword;
                                            if (countwords == 3) SlabDirection2[SlabNumbers] = theword;
                                            theword = "";
                                        }
                                    }
                                }
                            }
                            #endregion
                            #region//جلب الجدران  
                            if (TheElement == "IFCWALLSTANDARDCASE")
                            {
                                if (result == "IFCMATERIALLAYER")//سماكة الجدار ومادته 
                                {
                                    pFrom = GH.IndexOf("(") + "(".Length;
                                    pTo = GH.Length;
                                    string GH1 = GH.Substring(pFrom, pTo - pFrom);
                                    int GH1Length = GH1.Length;
                                    string theword = "";
                                    int countwords = 0;
                                    for (int k = 0; k < GH1Length; k++)
                                    {
                                        result = GH1.Substring(k, 1);
                                        if (result != "," & result != ")")
                                        {
                                            theword = theword + result;
                                        }
                                        else
                                        {
                                            countwords = countwords + 1;
                                            if (countwords == 1) WallMaterialExtend[WallNumbers] = theword;
                                            if (countwords == 2) WallThick[WallNumbers] = theword;
                                            theword = "";
                                        }
                                    }
                                }
                                if (result == "IFCEXTRUDEDAREASOLID")//الارتفاع
                                {
                                    pFrom = GH.IndexOf("(") + "(".Length;
                                    pTo = GH.Length;
                                    string GH1 = GH.Substring(pFrom, pTo - pFrom);
                                    int GH1Length = GH1.Length;
                                    string theword = "";
                                    int countwords = 0;
                                    for (int k = 0; k < GH1Length; k++)
                                    {
                                        result = GH1.Substring(k, 1);
                                        if (result != "," & result != ")")
                                        {
                                            theword = theword + result;
                                        }
                                        else
                                        {
                                            countwords = countwords + 1;
                                            if (countwords == 4) WallHight[WallNumbers] = theword;
                                            theword = "";
                                        }
                                    }
                                }
                                if (result == "IFCPOLYLINE")//نقاط العنصر المساحي   
                                {
                                    pFrom = GH.IndexOf("((") + "((".Length;
                                    pTo = GH.Length;
                                    string GH1 = GH.Substring(pFrom, pTo - pFrom);
                                    int GH1Length = GH1.Length;
                                    string theword = "";
                                    int countwords = 0;
                                    for (int k = 0; k < GH1Length; k++)
                                    {
                                        result = GH1.Substring(k, 1);
                                        if (result != "," & result != ")")
                                        {
                                            theword = theword + result;
                                        }
                                        else
                                        {
                                            if (theword != "")
                                            {
                                                countwords = countwords + 1;
                                                WallPointNumbers[WallNumbers] = countwords;
                                                WallNode[WallNumbers, countwords] = theword;
                                                theword = "";
                                            }
                                        }
                                    }
                                }
                                if (result == "IFCAXIS2PLACEMENT3D")//الموقع و التجاه
                                {
                                    pFrom = GH.IndexOf("(") + "(".Length;
                                    pTo = GH.Length;
                                    string GH1 = GH.Substring(pFrom, pTo - pFrom);
                                    int GH1Length = GH1.Length;
                                    string theword = "";
                                    int countwords = 0;
                                    for (int k = 0; k < GH1Length; k++)
                                    {
                                        result = GH1.Substring(k, 1);
                                        if (result != "," & result != ")")
                                        {
                                            theword = theword + result;
                                        }
                                        else
                                        {
                                            countwords = countwords + 1;
                                            if (countwords == 1) WallInsertionNode[WallNumbers] = theword;
                                            if (countwords == 2) WallDirection1[WallNumbers] = theword;
                                            if (countwords == 3) WallDirection2[WallNumbers] = theword;
                                            theword = "";
                                        }
                                    }
                                }
                            }
                            #endregion
                            #region//جلب  الطوابق
                            if (result == "IFCBUILDINGSTOREY")
                            {
                                StoryNumbers = StoryNumbers + 1;
                                pFrom = GH.IndexOf("',") + "',".Length;
                                pTo = GH.Length;
                                string GH1 = GH.Substring(pFrom, pTo - pFrom);
                                int GH1Length = GH1.Length;
                                string theword = "";
                                int countwords = 0;
                                for (int k = 0; k < GH1Length; k++)
                                {
                                    result = GH1.Substring(k, 1);
                                    if (result != "," & result != ")")
                                    {
                                        theword = theword + result;
                                    }
                                    else
                                    {
                                        countwords = countwords + 1;
                                        if (countwords == 9) StoryLevel[StoryNumbers] = theword;
                                        theword = "";
                                    }
                                }
                            }
                            #endregion
                            #region//جلب  القوى العقدة
                            if (TheElement == "IFCSTRUCTURALLOADSINGLEFORCE")
                            {
                                if (result == "IFCSTRUCTURALLOADSINGLEFORCE")
                                {
                                    pFrom = GH.IndexOf("(") + "(".Length;
                                    pTo = GH.Length;
                                    string GH1 = GH.Substring(pFrom, pTo - pFrom);
                                    int GH1Length = GH1.Length;
                                    string theword = "";
                                    int countwords = 0;
                                    for (int k = 0; k < GH1Length; k++)
                                    {
                                        result = GH1.Substring(k, 1);
                                        if (result != "," & result != ")")
                                        {
                                            theword = theword + result;
                                        }
                                        else
                                        {
                                            countwords = countwords + 1;
                                            if (countwords == 2) LoadPX[PowerJointNumber] = theword;
                                            if (countwords == 3) LoadPY[PowerJointNumber] = theword;
                                            if (countwords == 4) LoadPZ[PowerJointNumber] = theword;
                                            if (countwords == 5) LoadMX[PowerJointNumber] = theword;
                                            if (countwords == 6) LoadMY[PowerJointNumber] = theword;
                                            if (countwords == 7) LoadMZ[PowerJointNumber] = theword;
                                            theword = "";
                                        }
                                    }
                                }
                                if (result == "IFCVERTEXPOINT")
                                {
                                    pFrom = GH.IndexOf("(") + "(".Length;
                                    pTo = GH.IndexOf(")");
                                    result = GH.Substring(pFrom, pTo - pFrom);
                                    LoadNode[PowerJointNumber] = result;
                                }
                            }
                            #endregion
                        EndResult: { }
                        }
                    }
                }
                SW.Close();
                #endregion
              
                #region//تخزين العناصر
                #region// تخزين البلاطات
                Joint.Number2d = 0;
                for (int i = 1; i < SlabNumbers + 1; i++)//بلاطات
                {
                    for (int j = 1; j < MaterialNumbers + 1; j++)
                    {
                        if (SlabMaterialExtend[i] == "#"+MterialID[j])
                        {
                            SlabMaterialExtend[i] = j.ToString();
                            break;
                        }
                    }
                }
                m = 0;
                for (int j = 1; j < SlabNumbers + 1; j++)
                {
                    int selectedslab = 0;
                    for (int i = 1; i < Slab.Number + 1; i++)
                    {
                        if (Slab.Material[i] == Convert.ToInt32(SlabMaterialExtend[j]) & Slab.Thickness[i] == Convert.ToDouble(SlabThick[j]) / 1000)
                        {
                            selectedslab = i;
                            goto endslab;
                        }
                    }
                    m = m + 1;
                    Slab.Number = m;
                    selectedslab = Slab.Number;
                    Slab.Name[m] = "Slab " + m;
                    Slab.Material[m] = Convert.ToInt32(SlabMaterialExtend[j]);
                    Slab.Thickness[m] = Convert.ToDouble(SlabThick[j]) / 1000;
                    Slab.SlabThickness[m] = Convert.ToDouble(SlabThick[j]) / 1000;
                    Slab.MType[m] = 0;
                    Slab.OneWay[m] = 0;
                    Slab.ProType[m] = 0;
                    Slab.OverallDepth[m] = 0;
                    Slab.StemWidthatTop[m] = 0;
                    Slab.StemWidthatBottom[m] = 0;
                    Slab.RibSpacing[m] = 0;
                    Slab.RibDirection[m] = 0;
                    Slab.RibSpacing1[m] = 0;
                    Slab.RibSpacing2[m] = 0;
                endslab: { }
                Shell.Section[j] = selectedslab;
                }
                Shell.Number = SlabNumbers;
                for (int i = 1; i < Shell.Number + 1; i++)
                {
                    double DX = 0;
                    double DY = 0;
                    double DZ = 0;
                    double XI = 0;
                    double YI = 0;
                    double ZI = 0;
                    for (int j = 1; j < DirectionNumbers + 1; j++)
                    {
                        if (SlabDirection2[i] == "#" + DirectionId[j])
                        {
                            DX = Convert.ToDouble(DirectionX[j]);
                            DY = Convert.ToDouble(DirectionY[j]);
                            DZ = Convert.ToDouble(DirectionZ[j]);
                        }
                    }
                    for (int j = 1; j < PointNumbers + 1; j++)
                    {
                        if (SlabInsertionNode[i] == "#" + PointId[j])
                        {
                            XI = Convert.ToDouble(PointX[j]) / 1000;
                            YI = Convert.ToDouble(PointY[j]) / 1000;
                            ZI = Convert.ToDouble(PointZ[j]) / 1000;
                            break;
                        }
                    }
                    Shell.PointNumbers[i] = SlabPointNumbers[i] - 1;
                    Shell.Type[i] = 1;
                    for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                    {
                        Shell.SelectedLine[i, j] = 0;
                        for (int k = 1; k < PointNumbers + 1; k++)
                        {
                            int selectedjoint = 0;
                            if (SlabNode[i, j] == "#" + PointId[k])
                            {
                                double x = Convert.ToDouble(PointX[k]) / 1000 + XI;
                                double y = Convert.ToDouble(PointY[k]) / 1000 + YI;
                                double z = ZI;
                                double angle = Math.Atan(DY / DX);
                                if (DX < 0 )
                                {
                                     angle = Math.PI + Math.Atan(DY / DX);
                                }
                                RotatePoint1(x, y, XI, YI, angle);
                                x = Math.Round(TheX, 3);
                                y = Math.Round(TheY, 3);
                                for (int l = 1; l < Joint.Number2d + 1; l++)
                                {
                                    if (Joint.XReal[l] == x & Joint.YReal[l] == y & Joint.ZReal[l] == z)
                                    {
                                        selectedjoint = l;
                                        goto Endjoint;
                                    }
                                }
                                Joint.Number2d = Joint.Number2d + 1;
                                selectedjoint = Joint.Number2d;
                                Joint.XReal[selectedjoint] = x;
                                Joint.YReal[selectedjoint] = y;
                                Joint.ZReal[selectedjoint] = z;
                            Endjoint: { }
                                Shell.JointNo[i, j] = selectedjoint;
                                int thejoint = selectedjoint;
                                Joint.FloorConnectionN[thejoint] = Joint.FloorConnectionN[thejoint] + 1;
                                int thecount = Joint.FloorConnectionN[thejoint];
                                Joint.Floor[thejoint, thecount] = i;
                                break;
                            }
                        }
                    }
                }
                #endregion
                #region// تخزين الجدران
                for (int i = 1; i < WallNumbers + 1; i++)
                {
                    for (int j = 1; j < MaterialNumbers + 1; j++)
                    {
                        if (WallMaterialExtend[i] == "#" + MterialID[j])
                        {
                            WallMaterialExtend[i] = j.ToString();
                            break;
                        }
                    }
                }
                m = 0;
                for (int j = 1; j < WallNumbers + 1; j++)
                {
                    int selectedWall = 0;
                    for (int i = 1; i < Wall.Number + 1; i++)
                    {
                        if (Wall.Material[i] == Convert.ToInt32(WallMaterialExtend[j]) & Wall.Thickness[i] == Convert.ToDouble(WallThick[j]) / 1000)
                        {
                            selectedWall = i;
                            goto endslab;
                        }
                    }
                    m = m + 1;
                    Wall.Number = m;
                    selectedWall = Wall.Number;
                    Wall.Name[m] = "Wall " + m;
                    Wall.Material[m] = Convert.ToInt32(WallMaterialExtend[j]);
                    Wall.Thickness[m] = Convert.ToDouble(WallThick[j]) / 1000;
                    Wall.MType[m] = 0;
                    Wall.ProType[m] = 0;
                endslab: { }
                    Shell.Section[SlabNumbers + j] = selectedWall;
                }
                Shell.Number = SlabNumbers + WallNumbers;
                m = 0;
                for (int i = SlabNumbers + 1; i < Shell.Number + 1; i++)
                {
                    m = m + 1;
                    double DX = 0;
                    double DY = 0;
                    double DZ = 0;
                    double XI = 0;
                    double YI = 0;
                    double ZI = 0;
                    for (int j = 1; j < DirectionNumbers + 1; j++)
                    {
                        if (WallDirection2[m] == "#" + DirectionId[j])
                        {
                            DX = Convert.ToDouble(DirectionX[j]);
                            DY = Convert.ToDouble(DirectionY[j]);
                            DZ = Convert.ToDouble(DirectionZ[j]);
                        }
                    }
                    for (int j = 1; j < PointNumbers + 1; j++)
                    {
                        if (WallInsertionNode[m] == "#" + PointId[j])
                        {
                            XI = Convert.ToDouble(PointX[j]) / 1000;
                            YI = Convert.ToDouble(PointY[j]) / 1000;
                            ZI = Convert.ToDouble(PointZ[j]) / 1000;
                            break;
                        }
                    }
                    Shell.Type[i] = 2;
                    // Shell.PointNumbers[i] = WallPointNumbers[i];
                    Shell.PointNumbers[i] = 2;
                    for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                    {
                        Shell.SelectedLine[i, j] = 0;
                        for (int k = 1; k < PointNumbers + 1; k++)
                        {
                            int selectedjoint = 0;
                            if (WallNode[m, j] == "#" + PointId[k])
                            {
                                double x = Convert.ToDouble(PointX[k]) / 1000 + XI;
                                double y = Convert.ToDouble(PointY[k]) / 1000 + YI;
                                double z = ZI;
                                double angle = Math.Atan(DY / DX);
                                if (DX < 0)
                                {
                                    angle = Math.PI + Math.Atan(DY / DX);
                                }
                                RotatePoint1(x, y, XI, YI, angle);
                                x = Math.Round(TheX, 3);
                                y = Math.Round(TheY, 3);
                                for (int l = 1; l < Joint.Number2d + 1; l++)
                                {
                                    if (Joint.XReal[l] == x & Joint.YReal[l] == y & Joint.ZReal[l] == z)
                                    {
                                        selectedjoint = l;
                                        goto Endjoint;
                                    }
                                }
                                Joint.Number2d = Joint.Number2d + 1;
                                selectedjoint = Joint.Number2d;
                                Joint.XReal[selectedjoint] = x;
                                Joint.YReal[selectedjoint] = y;
                                Joint.ZReal[selectedjoint] = z;
                            Endjoint: { }
                                Shell.JointNo[i, j] = selectedjoint;
                                int thejoint = selectedjoint;
                                Joint.FloorConnectionN[thejoint] = Joint.FloorConnectionN[thejoint] + 1;
                                int thecount = Joint.FloorConnectionN[thejoint];
                                Joint.Floor[thejoint, thecount] = i;
                                break;
                            }
                        }
                    }
                    Shell.PointNumbers[i] = 4;
                    for (int j = 3; j < 4 + 1; j++)
                    {
                        int selectedjoint = 0;
                        int Firstjoint = 0;
                        if (j == 3) Firstjoint = Shell.JointNo[i, 2];
                        if (j == 4) Firstjoint = Shell.JointNo[i, 1];
                        double X = Joint.XReal[Firstjoint];
                        double Y = Joint.YReal[Firstjoint];
                        double Z = Joint.ZReal[Firstjoint] + Math.Round(Convert.ToDouble(WallHight[m]) / 1000, 3);
                        for (int l = 1; l < Joint.Number2d + 1; l++)
                        {
                            if (Joint.XReal[l] == X & Joint.YReal[l] == Y & Joint.ZReal[l] == Z)
                            {
                                selectedjoint = l;
                                goto Endjoint;
                            }
                        }
                        Joint.Number2d = Joint.Number2d + 1;
                        selectedjoint = Joint.Number2d;
                        Joint.XReal[selectedjoint] = X;
                        Joint.YReal[selectedjoint] = Y;
                        Joint.ZReal[selectedjoint] = Z;
                    Endjoint: { }
                    Shell.JointNo[i, j] = selectedjoint;
                        int thejoint = selectedjoint;
                        Joint.FloorConnectionN[thejoint] = Joint.FloorConnectionN[thejoint] + 1;
                        int thecount = Joint.FloorConnectionN[thejoint];
                        Joint.Floor[thejoint, thecount] = i;
                    }
                }
                #endregion
                #region// تخزين الطوابق
                double[] Apower = new double[StoryNumbers + 1];
                int[] Bpower = new int[StoryNumbers + 1];
                double[] Cpower = new double[StoryNumbers + 1];
                int[] Dpower = new int[StoryNumbers + 1];
                int Row = 0;
                int N = StoryNumbers;
                int NN = StoryNumbers;
                double MAXI = +1000000;
                for (int j = 1; j < N + 1; j++)
                {
                    Apower[j] = Convert.ToDouble(StoryLevel[j]);
                    Bpower[j] = j;
                }
                for (int j = 1; j < N + 1; j++)
                {
                    MAXI = 1000000;
                    for (int k = 1; k < NN + 1; k++)
                    {
                        if (MAXI >= Apower[k])
                        {
                            MAXI = Apower[k];
                            Row = Bpower[k];
                        }
                    }
                    Cpower[j] = MAXI;
                    NN = NN - 1;
                    for (int k = Row; k < NN + 1; k++)
                    {
                        Apower[k] = Apower[k + 1];
                        Bpower[k] = k;
                    }
                }
                Myglobals.SelectedStory = 1;
                Myglobals.StoryNumbers = StoryNumbers - 1;
                for (int j = 1; j < Myglobals.StoryNumbers + 1; j++)
                {
                    Myglobals.StoryLevel[j] = Cpower[j + 1] / 1000;
                    Myglobals.StoryHight[j] = Myglobals.StoryLevel[j] - Myglobals.StoryLevel[j - 1];
                    Myglobals.StoryName[j] = "Story " + j;
                }
                Myglobals.StoryName[0] = "Base";
                #endregion
                #region//تخزين المقاطع
                for (int i = 1; i < SectionNumbers + 1; i++)
                {
                    if (SectionMaterial[i] == null)
                    {
                        for (int j = 1; j < MaterialNumbers + 1; j++)
                        {
                            if (SectionMaterialExtend[i] == MterialID[j])
                            {
                                SectionMaterial[i] = MterialName[j];
                            }
                        }
                    }
                }
                for (int i = 1; i < SectionNumbers + 1; i++)
                {
                    for (int j = 1; j < MaterialNumbers + 1; j++)
                    {
                        if (SectionMaterial[i] == MterialName[j])
                        {
                            SectionMaterial[i] = j.ToString();
                            break;
                        }
                    }
                }
                Section.Number = SectionNumbers;
                for (int i = 1; i < Section.Number + 1; i++)
                {
                    try
                    {
                        Section.DESCRIPTION[i] = "IFCFILE";
                        Section.LENGTH_UNITS[i] = "IFCFILE";
                        Section.MODEL[i] = "IFCFILE";
                        Section.LABEL[i] = SectionName[i];
                        Section.EDI_STD[i] = "IFCFILE";
                        Section.DESIGNATION[i] = SectionKind[i];
                        Section.A[i] = Convert.ToDouble(SectionAria[i]);
                        Section.J[i] = Convert.ToDouble(SectionJ[i]);
                        Section.I33[i] = Convert.ToDouble(SectionI33[i]);
                        Section.I22[i] = Convert.ToDouble(SectionI22[i]);
                        Section.AS2[i] = Convert.ToDouble(SectionAS2[i]);
                        Section.AS3[i] = Convert.ToDouble(SectionAS3[i]);
                        Section.S33POS[i] = Convert.ToDouble(SectionS33P[i]);
                        Section.S33NEG[i] = Convert.ToDouble(SectionS33N[i]);
                        Section.S22POS[i] = Convert.ToDouble(SectionS22P[i]);
                        Section.S22NEG[i] = Convert.ToDouble(SectionS22N[i]);
                        Section.Material[i] = Convert.ToInt32(SectionMaterial[i]);
                        Section.D[i] = Convert.ToDouble(SectionD[i]) / 10;
                        Section.BF[i] = Convert.ToDouble(SectionBF[i]) / 10;
                        Section.TF[i] = Convert.ToDouble(SectionTF[i]) / 10;
                        Section.BFB[i] = Convert.ToDouble(SectionBFB[i]) / 10;
                        Section.TFB[i] = Convert.ToDouble(SectionTFB[i]) / 10;
                        Section.TW[i] = Convert.ToDouble(SectionTW[i]) / 10;
                        //
                        Section.B[i] = Convert.ToDouble(SectionBF[i])/10;
                        Section.HT[i] = Convert.ToDouble(SectionD[i])/10;
                        Section.X[i] = Convert.ToDouble(SectionBF[i])/10 / 2;
                        Section.Y[i] = Convert.ToDouble(SectionD[i])/10 / 2;
                        Section.Z33[i] = 0;
                        Section.Z22[i] = 0;
                        Section.R33[i] = 0;
                        Section.R22[i] = 0;
                        Section.R33[i] = 0;
                        Section.OD[i] = 0;
                        Section.TDES[i] = 0;
                    }
                    catch { }
                }
                #endregion
                #region //تخزين المواد
                Material.Number = MaterialNumbers;
                for (int i = 1; i < Material.Number + 1; i++)
                {
                    Material.Name[i] = MterialName[i];
                    if (MterialCompressiveStrength[i] != null)
                    {
                        Material.MType[i] = 1;
                        Material.fc[i] = Convert.ToDouble(MterialCompressiveStrength[i]);
                        Material.LweightCon[i] = 0;
                        //
                        Material.MinYeildFy[i] = 0;
                        Material.MinTensileFu[i] = 0;
                        Material.EffYeildFye[i] = 0;
                        Material.EffTensileFue[i] = 0;
                    }
                    else
                    {
                        Material.fc[i] = 0;
                        Material.LweightCon[i] = 0;
                        //
                        Material.MType[i] = 0;
                        Material.MinYeildFy[i] = Convert.ToDouble(MterialYieldStress[i]);
                        Material.MinTensileFu[i] = Convert.ToDouble(MterialUltimateStress[i]);
                        Material.EffYeildFye[i] = Convert.ToDouble(MterialYieldStress[i]) * 1.1;
                        Material.EffTensileFue[i] = Convert.ToDouble(MterialUltimateStress[i]) * 1.1;
                    }
                    Material.Summetry[i] = 0;
                    Material.WperV[i] = Convert.ToDouble(MterialMassDensity[i]) * 0.981;
                    Material.MperV[i] = Convert.ToDouble(MterialMassDensity[i]);
                    Material.Elastisity[i] = Convert.ToDouble(MterialYoungModulus[i]);
                    Material.Poisson[i] = Convert.ToDouble(MterialPoissonRatio[i]);
                    Material.Thermal[i] = Convert.ToDouble(MterialThermalExpansionCoefficient[i]);
                    Material.ShearM[i] = Convert.ToDouble(MterialShearModulus[i]);
                }
                #endregion
                #region//تخزين الجوائز
                Frame.Number = BeamNumbers;
                for (int i = 1; i < Frame.Number + 1; i++)
                {
                    for (int j = 1; j < SectionNumbers + 1; j++)
                    {
                        if (BeamSection[i] == "#" + SectionID[j])
                        {
                            ((MainForm)mainForm).FrameElement[i].Section = j;
                        }
                    }
                }
                for (int i = 1; i < Frame.Number + 1; i++)
                {
                    ((MainForm)mainForm).FrameElement[i].Selected = 0;
                    ((MainForm)mainForm).FrameElement[i].RotateAngel = 0;
                    int selectedjoint = 0;
                    for (int j = 1; j < PointNumbers + 1; j++)
                    {
                        if (BeamNode[i] == "#" + PointId[j])
                        {
                            double x =Math.Round(Convert.ToDouble(PointX[j]) / 1000,3);
                            double y = Math.Round(Convert.ToDouble(PointY[j]) / 1000,3);
                            double z = Math.Round(Convert.ToDouble(PointZ[j]) / 1000, 3);
                            for (int k = 1; k < Joint.Number2d + 1; k++)
                            {
                                if (Joint.XReal[k] == x & Joint.YReal[k] == y & Joint.ZReal[k] == z)
                                {
                                    selectedjoint = k;
                                    goto Endjoint;
                                }
                            }
                            Joint.Number2d = Joint.Number2d + 1;
                            selectedjoint = Joint.Number2d;
                            Joint.XReal[selectedjoint] = x;
                            Joint.YReal[selectedjoint] = y;
                            Joint.ZReal[selectedjoint] = z;
                        Endjoint: { }
                            ((MainForm)mainForm).FrameElement[i].FirstJoint = selectedjoint;
                            int thejoint = selectedjoint;
                            Joint.BeamConnectionN[thejoint] = Joint.BeamConnectionN[thejoint] + 1;
                            int thecount = Joint.BeamConnectionN[thejoint];
                            Joint.Beam[thejoint, thecount] = i;
                            break;
                        }
                    }
                    double length = Convert.ToDouble(BeamLength[i]) / 1000;
                    double DX = 0;
                    double DY = 0;
                    double DZ = 0;
                    for (int j = 1; j < DirectionNumbers + 1; j++)
                    {
                        if (BeamDirection1[i] == "#" + DirectionId[j])
                        {
                            DX = Convert.ToDouble(DirectionX[j]);
                            DY = Convert.ToDouble(DirectionY[j]);
                            DZ = Convert.ToDouble(DirectionZ[j]);
                        }
                    }
                    double X = Math.Round(DX * length, 3) + Joint.XReal[selectedjoint];
                    double Y = Math.Round(DY * length, 3) + Joint.YReal[selectedjoint];
                    double Z = Math.Round(DZ * length, 3) + Joint.ZReal[selectedjoint];
                    for (int k = 1; k < Joint.Number2d + 1; k++)
                    {
                        if (Joint.XReal[k] == X & Joint.YReal[k] == Y & Joint.ZReal[k] == Z)
                        {
                            selectedjoint = k;
                            goto Endjoint1;
                        }
                    }
                    Joint.Number2d = Joint.Number2d + 1;
                    selectedjoint = Joint.Number2d;
                    Joint.XReal[selectedjoint] = X;
                    Joint.YReal[selectedjoint] = Y;
                    Joint.ZReal[selectedjoint] = Z;
                Endjoint1: { }
                    ((MainForm)mainForm).FrameElement[i].SecondJoint = selectedjoint;
                    int thejoint1 = selectedjoint;
                    Joint.BeamConnectionN[thejoint1] = Joint.BeamConnectionN[thejoint1] + 1;
                    int thecount1 = Joint.BeamConnectionN[thejoint1];
                    Joint.Beam[thejoint1, thecount1] = i;
                }
                Joint.Number3d = Joint.Number2d;
                #endregion
                    for (int i = 1; i < PowerJointNumber + 1; i++)
                    {
                        int selectedjoint = 0;
                        for (int j = 1; j < PointNumbers + 1; j++)
                        {
                            if (LoadNode[i] == "#" + PointId[j])
                            {
                                double x = Math.Round(Convert.ToDouble(PointX[j]) / 1000, 3);
                                double y = Math.Round(Convert.ToDouble(PointY[j]) / 1000, 3);
                                double z = Math.Round(Convert.ToDouble(PointZ[j]) / 1000, 3);
                                for (int k = 1; k < Joint.Number2d + 1; k++)
                                {
                                    if (Joint.XReal[k] == x & Joint.YReal[k] == y & Joint.ZReal[k] == z)
                                    {
                                        selectedjoint = k;
                                        goto Endjoint;
                                    }
                                }
                            }
                        Endjoint: { };
                        }
                }
                #endregion
                DROWcls callmee = new DROWcls();
                callmee.Render2d();
                callmee.Render3d();
                ((MainForm)mainForm).MakeTempFiles();
            }
        }
            catch{};
        }
        public void RotatePoint1(double XR, double YR, double XC, double YC, double angleInDegrees)
        {
            double angleInRadians = angleInDegrees;//* (Math.PI / 180);
            double cosTheta = Math.Cos(angleInRadians);
            double sinTheta = Math.Sin(angleInRadians);
            TheX = (cosTheta * (XR - XC) - sinTheta * (YR - YC) + XC);
            TheY = (sinTheta * (XR - XC) + cosTheta * (YR - YC) + YC);
        }
    }
}
