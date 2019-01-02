using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;

namespace AMSPRO
{
    public partial class SectionDrawForm : Form
    {
        public SectionDrawForm()
        {
            InitializeComponent();
            this.MouseWheel += new MouseEventHandler(pictureBox2_MouseWheel);
        }
        #region/////////////////////////////تعريف المتحولات
        string AnyKindtoEditIs = "";

        public int ClipsNumber = 0;
        public int SelectedClip = 0;

        int EditType = 0;
        int ClickToEdit = 0;
        int RecEdgeToEdit = 0;
        int GridIntersections = 1;
        int LineEnds = 0;
        int TempFile = 0;
        int TempSelectedFile = 0;
        string[] TempFileName = new string[1000];
        int[] kx = new int[5];
        int[] ky = new int[5];
        double[] Tx = new double[5];
        double[] Ty = new double[5];
        public int WallType = 0;
        public int SelectedType = 0;
        public int SelectedBar = 0;
        public int SelecteLinedBar = 0;
        public int SelecteLinedBar1 = 0;
        public int SelecteLinedBar2 = 0;
        public int SelecteLinedBar3 = 0;
        public int SelecteRecLinedBar = 0;
        int INTERSECTION = 0;
        double TheX0 = 0;
        double TheY0 = 0;
        double distance = 0;
        int DrawType = 0;
        int LineMove2dVisible = 0;
        int[] TahkikXY = new int[3];
        int drowclick = 0;
        int xmove1 = 0;
        int ymove1 = 0;
        double Zoom2d = 0.6;
        int startX2d = 100;
        int startY2d = 400;
        public Bars[] Bar = new Bars[2000];
        public LineBars[] LineBar = new LineBars[2000];
        public Clips[] Clip = new Clips[2000];
        public RecLineBars[] RecLineBar = new RecLineBars[2000];
        public RecShapes[] RecShape = new RecShapes[500];
        public CircleShapes[] CircleShape = new CircleShapes[500];
        public CircleLineBars[] CircleLineBar = new CircleLineBars[500];
        public FlangedWalls[] FlangedWallShape = new FlangedWalls[500];
        public TeeShapes[] TeeShape = new TeeShapes[500];

        public int SelectedTeeShape = 0;
        public int FlangedWallShapeNumber = 0;
        public int TeeShapeNumber = 0;
        public int SelectedFlangedWallShape = 0;
        public int SelectedCircleLineBar = 0;
        public int CircleBarsNumber = 0;
        public int RecShapeNumber = 0;
        public int CircleShapeNumber = 0;
        public int BarsNumber = 0;
        public int LineBarsNumber = 0;
        public int RecBarsNumber = 0;
        public int SelectedInED = 0;
        public int SelectedRecBar = 0;

        public int SelectedRecBar1 = 0;
        public int SelectedRecBar2 = 0;
        public int SelectedRecBar3 = 0;

        public int SelectedCorner = 0;
        public int SelectedRecShape = 0;
        public int SelectedCircleShape = 0;
        int lastzoomX2d = 0;
        int lastzoomY2d = 0;
        int timetodo = 0;
        int TempX;
        int TempY;
        int xmove = 0;
        int ymove = 0;
        double TempX12Real;
        double TempY12Real;
        double lastzoomX2dR = 0;
        double lastzoomY2dR = 0;
        int Tahkik = 0;
        string SnapFromType = "";
        int SelectedJoint = 0;
        int LineMoveX1 = 0;
        int LineMoveY1 = 0;
        int LineMoveX2 = 0;
        int LineMoveY2 = 0;
        int BitampWidth2d = 1000;
        int BitampHight2d = 1000;
        double[] TempXReal = new double[3];
        double[] TempYReal = new double[3];
        int GridNumberSX = 0;
        int GridNumberSY = 0;
        int GridPointNumber = 0;
        double[] TX1Rx = new double[500];
        double[] TY1Rx = new double[500];
        double[] TX2Rx = new double[500];
        double[] TY2Rx = new double[500];
        double[] TX1Ry = new double[500];
        double[] TY1Ry = new double[500];
        double[] TX2Ry = new double[500];
        double[] TY2Ry = new double[500];
        double[] GridXR = new double[10000];
        double[] GridYR = new double[10000];
        int[] GridX2D = new int[10000];
        int[] GridY2D = new int[10000];

        int[] GridFiberX1D = new int[10000];
        int[] GridFiberY1D = new int[10000];
        int[] GridFiberX2D = new int[10000];
        int[] GridFiberY2D = new int[10000];

        meshclass CallMesh = new meshclass();
        int[,] AriaPointXintF = new int[1000, 100];
        int[,] AriaPointYintF = new int[1000, 100];

        int MouseButtonsLeft = 0;
        #endregion
        #region//إجرائيات الحفظ و الفتح
        private void Savemenu_Click(object sender, EventArgs e)
        {
            SaveVoid();
        }
        private void Openmenu_Click(object sender, EventArgs e)
        {
            OpenVoid();
        }
        private void SaveVoid()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = "e:\\";
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // try
                {
                    string strpath = saveFileDialog1.FileName + ".txt";
                    StreamWriter SW = new StreamWriter(strpath);
                    SW.WriteLine(BarsNumber);
                    for (int i = 1; i < BarsNumber + 1; i++)
                    {
                        SW.WriteLine(Bar[i].XR);
                        SW.WriteLine(Bar[i].YR);
                        SW.WriteLine(Bar[i].DR);
                        SW.WriteLine(Bar[i].InLine);
                        SW.WriteLine(Bar[i].InED);
                        SW.WriteLine(Bar[i].InREC);
                        SW.WriteLine(Bar[i].InCircle);
                        SW.WriteLine(Bar[i].Type);
                        SW.WriteLine(Bar[i].Corner);
                    }
                    SW.WriteLine(LineBarsNumber);
                    for (int i = 1; i < LineBarsNumber + 1; i++)
                    {
                        SW.WriteLine(LineBar[i].X1R);
                        SW.WriteLine(LineBar[i].Y1R);
                        SW.WriteLine(LineBar[i].X2R);
                        SW.WriteLine(LineBar[i].Y2R);
                        SW.WriteLine(LineBar[i].DR);
                        SW.WriteLine(LineBar[i].Distance);
                        SW.WriteLine(LineBar[i].BarsNumbers);
                    }
                    SW.WriteLine(RecBarsNumber);
                    for (int i = 1; i < RecBarsNumber + 1; i++)
                    {
                        SW.WriteLine(RecLineBar[i].InRecShape);
                        SW.WriteLine(RecLineBar[i].InFlangedWallShape);
                        SW.WriteLine(RecLineBar[i].InTeeShape);
                        SW.WriteLine(RecLineBar[i].Width);
                        SW.WriteLine(RecLineBar[i].Height);
                        SW.WriteLine(RecLineBar[i].CenterX);
                        SW.WriteLine(RecLineBar[i].CenterY);
                        SW.WriteLine(RecLineBar[i].TIEDR);
                        for (int j = 1; j < 5; j++)
                        {
                            SW.WriteLine(RecLineBar[i].EDX1R[j]);
                            SW.WriteLine(RecLineBar[i].EDY1R[j]);
                            SW.WriteLine(RecLineBar[i].EDX2R[j]);
                            SW.WriteLine(RecLineBar[i].EDY2R[j]);
                            SW.WriteLine(RecLineBar[i].EDBarsNumbers[j]);
                            SW.WriteLine(RecLineBar[i].EDDR[j]);
                            SW.WriteLine(RecLineBar[i].EDDistance[j]);
                            SW.WriteLine(RecLineBar[i].CORDR[j]);
                        }
                    }
                    SW.WriteLine(RecShapeNumber);
                    for (int i = 1; i < RecShapeNumber + 1; i++)
                    {
                        SW.WriteLine(RecShape[i].ApplyedToRecBars);
                        SW.WriteLine(RecShape[i].HasReinforcment);
                        SW.WriteLine(RecShape[i].Width);
                        SW.WriteLine(RecShape[i].Height);
                        SW.WriteLine(RecShape[i].CenterX);
                        SW.WriteLine(RecShape[i].CenterY);
                        for (int j = 1; j < 5; j++)
                        {
                            SW.WriteLine(RecShape[i].EDX1R[j]);
                            SW.WriteLine(RecShape[i].EDY1R[j]);
                            SW.WriteLine(RecShape[i].EDX2R[j]);
                            SW.WriteLine(RecShape[i].EDY2R[j]);
                            SW.WriteLine(RecShape[i].EDCoverR[j]);
                        }
                    }

                    SW.WriteLine(CircleBarsNumber);
                    for (int i = 1; i < CircleBarsNumber + 1; i++)
                    {
                        SW.WriteLine(CircleLineBar[i].InCircleShape);
                        SW.WriteLine(CircleLineBar[i].Diameter);
                        SW.WriteLine(CircleLineBar[i].CenterX);
                        SW.WriteLine(CircleLineBar[i].CenterY);
                        SW.WriteLine(CircleLineBar[i].BarsNumbers);
                        SW.WriteLine(CircleLineBar[i].DR);
                        SW.WriteLine(CircleLineBar[i].TIEDR);
                        for (int j = 1; j < 33; j++)
                        {
                            SW.WriteLine(CircleLineBar[i].PointXR[j]);
                            SW.WriteLine(CircleLineBar[i].PointYR[j]);

                        }
                    }
                    SW.WriteLine(CircleShapeNumber);
                    for (int i = 1; i < CircleShapeNumber + 1; i++)
                    {
                        SW.WriteLine(CircleShape[i].ApplyedToCircleBars);
                        SW.WriteLine(CircleShape[i].HasReinforcment);
                        SW.WriteLine(CircleShape[i].Diameter);
                        SW.WriteLine(CircleShape[i].CenterX);
                        SW.WriteLine(CircleShape[i].CenterY);
                        SW.WriteLine(CircleShape[i].CoverR);
                        for (int j = 1; j < 33; j++)
                        {
                            SW.WriteLine(CircleShape[i].PointXR[j]);
                            SW.WriteLine(CircleShape[i].PointYR[j]);
                        }
                    }
                    SW.WriteLine(FlangedWallShapeNumber);
                    for (int i = 1; i < FlangedWallShapeNumber + 1; i++)
                    {
                        SW.WriteLine(FlangedWallShape[i].HasReinforcment);
                        for (int j = 1; j < 4; j++)
                        {
                            SW.WriteLine(FlangedWallShape[i].ApplyedToRecBars[j]);
                        }
                        SW.WriteLine(FlangedWallShape[i].CenterX);
                        SW.WriteLine(FlangedWallShape[i].CenterY);
                        SW.WriteLine(FlangedWallShape[i].Length);
                        SW.WriteLine(FlangedWallShape[i].StemWidth);
                        SW.WriteLine(FlangedWallShape[i].LeftFlangWidth);
                        SW.WriteLine(FlangedWallShape[i].LeftFlangLength);
                        SW.WriteLine(FlangedWallShape[i].RightFlangWidth);
                        SW.WriteLine(FlangedWallShape[i].RightFlangLength);
                        SW.WriteLine(FlangedWallShape[i].LeftFlangEccen);
                        SW.WriteLine(FlangedWallShape[i].RightFlangEccen);
                        SW.WriteLine(FlangedWallShape[i].ED1CoverR);
                        SW.WriteLine(FlangedWallShape[i].ED2CoverR);
                        SW.WriteLine(FlangedWallShape[i].ED3CoverR);
                        SW.WriteLine(FlangedWallShape[i].ED4CoverR);
                        SW.WriteLine(FlangedWallShape[i].ED1LCoverR);
                        SW.WriteLine(FlangedWallShape[i].ED2LCoverR);
                        SW.WriteLine(FlangedWallShape[i].ED3LCoverR);
                        SW.WriteLine(FlangedWallShape[i].ED4LCoverR);
                        SW.WriteLine(FlangedWallShape[i].ED1RCoverR);
                        SW.WriteLine(FlangedWallShape[i].ED2RCoverR);
                        SW.WriteLine(FlangedWallShape[i].ED3RCoverR);
                        SW.WriteLine(FlangedWallShape[i].ED4RCoverR);
                        for (int j = 1; j < 13; j++)
                        {
                            SW.WriteLine(FlangedWallShape[i].PointXReal[j]);
                            SW.WriteLine(FlangedWallShape[i].PointYReal[j]);
                        }
                    }
                    SW.WriteLine(TeeShapeNumber);
                    for (int i = 1; i < TeeShapeNumber + 1; i++)
                    {
                        SW.WriteLine(TeeShape[i].HasReinforcment);
                        for (int j = 1; j < 3; j++)
                        {
                            SW.WriteLine(TeeShape[i].ApplyedToRecBars[j]);
                        }
                        SW.WriteLine(TeeShape[i].CenterX);
                        SW.WriteLine(TeeShape[i].CenterY);
                        SW.WriteLine(TeeShape[i].Height );
                        SW.WriteLine(TeeShape[i].FlangWidth );
                        SW.WriteLine(TeeShape[i].FlangThickness );
                        SW.WriteLine(TeeShape[i].WebThickness);
                        SW.WriteLine(TeeShape[i].ED1CoverR);
                        SW.WriteLine(TeeShape[i].ED2CoverR);
                        SW.WriteLine(TeeShape[i].ED3CoverR);
                        SW.WriteLine(TeeShape[i].ED4CoverR);
                        SW.WriteLine(TeeShape[i].ED1cCoverR);
                        SW.WriteLine(TeeShape[i].ED2cCoverR);
                        SW.WriteLine(TeeShape[i].ED3cCoverR);
                        SW.WriteLine(TeeShape[i].ED4cCoverR);
                        for (int j = 1; j < 9; j++)
                        {
                            SW.WriteLine(TeeShape[i].PointXReal[j]);
                            SW.WriteLine(TeeShape[i].PointYReal[j]);
                        }
                    }
                    SW.WriteLine(ClipsNumber);
                    for (int i = 1; i < ClipsNumber + 1; i++)
                    {
                        SW.WriteLine(Clip[i].X1R);
                        SW.WriteLine(Clip[i].Y1R);
                        SW.WriteLine(Clip[i].X2R);
                        SW.WriteLine(Clip[i].Y2R);
                        SW.WriteLine(Clip[i].DR);
                        SW.WriteLine(Clip[i].DR1);
                        SW.WriteLine(Clip[i].DR2);
                        SW.WriteLine(Clip[i].Type);
                    }
                    SW.Close();
                }
                // catch
                {
                    //    MessageBox.Show("ERROR! Please check passphrase and do not attempt to edit cipher text");
                }
            }
        }
        private void SaveTemp()
        {
            string strpath = TempFileName[TempSelectedFile];
            StreamWriter SW = new StreamWriter(strpath);
            {
                SW.WriteLine(BarsNumber);
                for (int i = 1; i < BarsNumber + 1; i++)
                {
                    SW.WriteLine(Bar[i].XR);
                    SW.WriteLine(Bar[i].YR);
                    SW.WriteLine(Bar[i].DR);
                    SW.WriteLine(Bar[i].InLine);
                    SW.WriteLine(Bar[i].InED);
                    SW.WriteLine(Bar[i].InREC);
                    SW.WriteLine(Bar[i].InCircle);
                    SW.WriteLine(Bar[i].Type);
                    SW.WriteLine(Bar[i].Corner);
                }
                SW.WriteLine(LineBarsNumber);
                for (int i = 1; i < LineBarsNumber + 1; i++)
                {
                    SW.WriteLine(LineBar[i].X1R);
                    SW.WriteLine(LineBar[i].Y1R);
                    SW.WriteLine(LineBar[i].X2R);
                    SW.WriteLine(LineBar[i].Y2R);
                    SW.WriteLine(LineBar[i].DR);
                    SW.WriteLine(LineBar[i].Distance);
                    SW.WriteLine(LineBar[i].BarsNumbers);
                }
                SW.WriteLine(RecBarsNumber);
                for (int i = 1; i < RecBarsNumber + 1; i++)
                {
                    SW.WriteLine(RecLineBar[i].InRecShape);
                    SW.WriteLine(RecLineBar[i].InFlangedWallShape);
                    SW.WriteLine(RecLineBar[i].InTeeShape);
                    SW.WriteLine(RecLineBar[i].Width);
                    SW.WriteLine(RecLineBar[i].Height);
                    SW.WriteLine(RecLineBar[i].CenterX);
                    SW.WriteLine(RecLineBar[i].CenterY);
                    SW.WriteLine(RecLineBar[i].TIEDR);
                    for (int j = 1; j < 5; j++)
                    {
                        SW.WriteLine(RecLineBar[i].EDX1R[j]);
                        SW.WriteLine(RecLineBar[i].EDY1R[j]);
                        SW.WriteLine(RecLineBar[i].EDX2R[j]);
                        SW.WriteLine(RecLineBar[i].EDY2R[j]);
                        SW.WriteLine(RecLineBar[i].EDBarsNumbers[j]);
                        SW.WriteLine(RecLineBar[i].EDDR[j]);
                        SW.WriteLine(RecLineBar[i].EDDistance[j]);
                        SW.WriteLine(RecLineBar[i].CORDR[j]);
                    }
                }
                SW.WriteLine(RecShapeNumber);
                for (int i = 1; i < RecShapeNumber + 1; i++)
                {
                    SW.WriteLine(RecShape[i].ApplyedToRecBars);
                    SW.WriteLine(RecShape[i].HasReinforcment);
                    SW.WriteLine(RecShape[i].Width);
                    SW.WriteLine(RecShape[i].Height);
                    SW.WriteLine(RecShape[i].CenterX);
                    SW.WriteLine(RecShape[i].CenterY);
                    for (int j = 1; j < 5; j++)
                    {
                        SW.WriteLine(RecShape[i].EDX1R[j]);
                        SW.WriteLine(RecShape[i].EDY1R[j]);
                        SW.WriteLine(RecShape[i].EDX2R[j]);
                        SW.WriteLine(RecShape[i].EDY2R[j]);
                        SW.WriteLine(RecShape[i].EDCoverR[j]);
                    }
                }

                SW.WriteLine(CircleBarsNumber);
                for (int i = 1; i < CircleBarsNumber + 1; i++)
                {
                    SW.WriteLine(CircleLineBar[i].InCircleShape);
                    SW.WriteLine(CircleLineBar[i].Diameter);
                    SW.WriteLine(CircleLineBar[i].CenterX);
                    SW.WriteLine(CircleLineBar[i].CenterY);
                    SW.WriteLine(CircleLineBar[i].BarsNumbers);
                    SW.WriteLine(CircleLineBar[i].DR);
                    SW.WriteLine(CircleLineBar[i].TIEDR);
                    for (int j = 1; j < 33; j++)
                    {
                        SW.WriteLine(CircleLineBar[i].PointXR[j]);
                        SW.WriteLine(CircleLineBar[i].PointYR[j]);

                    }
                }
                SW.WriteLine(CircleShapeNumber);
                for (int i = 1; i < CircleShapeNumber + 1; i++)
                {
                    SW.WriteLine(CircleShape[i].ApplyedToCircleBars);
                    SW.WriteLine(CircleShape[i].HasReinforcment);
                    SW.WriteLine(CircleShape[i].Diameter);
                    SW.WriteLine(CircleShape[i].CenterX);
                    SW.WriteLine(CircleShape[i].CenterY);
                    SW.WriteLine(CircleShape[i].CoverR);
                    for (int j = 1; j < 33; j++)
                    {
                        SW.WriteLine(CircleShape[i].PointXR[j]);
                        SW.WriteLine(CircleShape[i].PointYR[j]);
                    }
                }
                SW.WriteLine(FlangedWallShapeNumber);
                for (int i = 1; i < FlangedWallShapeNumber + 1; i++)
                {
                    SW.WriteLine(FlangedWallShape[i].HasReinforcment);
                    for (int j = 1; j < 4; j++)
                    {
                        SW.WriteLine(FlangedWallShape[i].ApplyedToRecBars[j]);
                    }
                    SW.WriteLine(FlangedWallShape[i].CenterX);
                    SW.WriteLine(FlangedWallShape[i].CenterY);
                    SW.WriteLine(FlangedWallShape[i].Length);
                    SW.WriteLine(FlangedWallShape[i].StemWidth);
                    SW.WriteLine(FlangedWallShape[i].LeftFlangWidth);
                    SW.WriteLine(FlangedWallShape[i].LeftFlangLength);
                    SW.WriteLine(FlangedWallShape[i].RightFlangWidth);
                    SW.WriteLine(FlangedWallShape[i].RightFlangLength);
                    SW.WriteLine(FlangedWallShape[i].LeftFlangEccen);
                    SW.WriteLine(FlangedWallShape[i].RightFlangEccen);
                    SW.WriteLine(FlangedWallShape[i].ED1CoverR);
                    SW.WriteLine(FlangedWallShape[i].ED2CoverR);
                    SW.WriteLine(FlangedWallShape[i].ED3CoverR);
                    SW.WriteLine(FlangedWallShape[i].ED4CoverR);
                    SW.WriteLine(FlangedWallShape[i].ED1LCoverR);
                    SW.WriteLine(FlangedWallShape[i].ED2LCoverR);
                    SW.WriteLine(FlangedWallShape[i].ED3LCoverR);
                    SW.WriteLine(FlangedWallShape[i].ED4LCoverR);
                    SW.WriteLine(FlangedWallShape[i].ED1RCoverR);
                    SW.WriteLine(FlangedWallShape[i].ED2RCoverR);
                    SW.WriteLine(FlangedWallShape[i].ED3RCoverR);
                    SW.WriteLine(FlangedWallShape[i].ED4RCoverR);
                    for (int j = 1; j < 13; j++)
                    {
                        SW.WriteLine(FlangedWallShape[i].PointXReal[j]);
                        SW.WriteLine(FlangedWallShape[i].PointYReal[j]);
                    }
                }
                SW.WriteLine(TeeShapeNumber);
                for (int i = 1; i < TeeShapeNumber + 1; i++)
                {
                    SW.WriteLine(TeeShape[i].HasReinforcment);
                    for (int j = 1; j < 3; j++)
                    {
                        SW.WriteLine(TeeShape[i].ApplyedToRecBars[j]);
                    }
                    SW.WriteLine(TeeShape[i].CenterX);
                    SW.WriteLine(TeeShape[i].CenterY);
                    SW.WriteLine(TeeShape[i].Height);
                    SW.WriteLine(TeeShape[i].FlangWidth);
                    SW.WriteLine(TeeShape[i].FlangThickness);
                    SW.WriteLine(TeeShape[i].WebThickness);
                    SW.WriteLine(TeeShape[i].ED1CoverR);
                    SW.WriteLine(TeeShape[i].ED2CoverR);
                    SW.WriteLine(TeeShape[i].ED3CoverR);
                    SW.WriteLine(TeeShape[i].ED4CoverR);
                    SW.WriteLine(TeeShape[i].ED1cCoverR);
                    SW.WriteLine(TeeShape[i].ED2cCoverR);
                    SW.WriteLine(TeeShape[i].ED3cCoverR);
                    SW.WriteLine(TeeShape[i].ED4cCoverR);
                    for (int j = 1; j < 9; j++)
                    {
                        SW.WriteLine(TeeShape[i].PointXReal[j]);
                        SW.WriteLine(TeeShape[i].PointYReal[j]);
                    }
                }
                SW.WriteLine(ClipsNumber);
                for (int i = 1; i < ClipsNumber + 1; i++)
                {
                    SW.WriteLine(Clip[i].X1R);
                    SW.WriteLine(Clip[i].Y1R);
                    SW.WriteLine(Clip[i].X2R);
                    SW.WriteLine(Clip[i].Y2R);
                    SW.WriteLine(Clip[i].DR);
                    SW.WriteLine(Clip[i].DR1);
                    SW.WriteLine(Clip[i].DR2);
                    SW.WriteLine(Clip[i].Type);
                }
                SW.Close();
            }
        }
        private void OpenVoid()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "e:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //  try
                {
                    string strpath = openFileDialog1.FileName;
                    StreamReader SW = new StreamReader(strpath);
                    BarsNumber = Convert.ToInt32(SW.ReadLine());
                    for (int i = 1; i < BarsNumber + 1; i++)
                    {
                        Bars emp = new Bars();
                        emp.XR = Convert.ToDouble(SW.ReadLine());
                        emp.YR = Convert.ToDouble(SW.ReadLine());
                        emp.DR = Convert.ToDouble(SW.ReadLine());
                        emp.InLine = Convert.ToInt32(SW.ReadLine());
                        emp.InED = Convert.ToInt32(SW.ReadLine());
                        emp.InREC = Convert.ToInt32(SW.ReadLine());
                        emp.InCircle = Convert.ToInt32(SW.ReadLine());
                        emp.Type = Convert.ToInt32(SW.ReadLine());
                        emp.Corner = Convert.ToInt32(SW.ReadLine());
                        Bar[i] = emp;
                    }
                    LineBarsNumber = Convert.ToInt32(SW.ReadLine());
                    for (int i = 1; i < LineBarsNumber + 1; i++)
                    {
                        LineBars emp = new LineBars();
                        emp.X1R = Convert.ToDouble(SW.ReadLine());
                        emp.Y1R = Convert.ToDouble(SW.ReadLine());
                        emp.X2R = Convert.ToDouble(SW.ReadLine());
                        emp.Y2R = Convert.ToDouble(SW.ReadLine());
                        emp.DR = Convert.ToDouble(SW.ReadLine());
                        emp.Distance = Convert.ToDouble(SW.ReadLine());
                        emp.BarsNumbers = Convert.ToInt32(SW.ReadLine());
                        LineBar[i] = emp;
                    }
                    RecBarsNumber = Convert.ToInt32(SW.ReadLine());
                    for (int i = 1; i < RecBarsNumber + 1; i++)
                    {
                        RecLineBars emp = new RecLineBars();
                        emp.InRecShape = Convert.ToInt32(SW.ReadLine());
                        emp.InFlangedWallShape = Convert.ToInt32(SW.ReadLine());
                        emp.InTeeShape = Convert.ToInt32(SW.ReadLine());
                        emp.Width = Convert.ToDouble(SW.ReadLine());
                        emp.Height = Convert.ToDouble(SW.ReadLine());
                        emp.CenterX = Convert.ToDouble(SW.ReadLine());
                        emp.CenterY = Convert.ToDouble(SW.ReadLine());
                        emp.TIEDR = Convert.ToInt32(SW.ReadLine());
                        for (int j = 1; j < 5; j++)
                        {
                            emp.EDX1R[j] = Convert.ToDouble(SW.ReadLine());
                            emp.EDY1R[j] = Convert.ToDouble(SW.ReadLine());
                            emp.EDX2R[j] = Convert.ToDouble(SW.ReadLine());
                            emp.EDY2R[j] = Convert.ToDouble(SW.ReadLine());
                            emp.EDBarsNumbers[j] = Convert.ToInt32(SW.ReadLine());
                            emp.EDDR[j] = Convert.ToInt32(SW.ReadLine());
                            emp.EDDistance[j] = Convert.ToDouble(SW.ReadLine());
                            emp.CORDR[j] = Convert.ToInt32(SW.ReadLine());
                        }
                        RecLineBar[i] = emp;
                    }
                    RecShapeNumber = Convert.ToInt32(SW.ReadLine());
                    for (int i = 1; i < RecShapeNumber + 1; i++)
                    {
                        RecShapes emp = new RecShapes();
                        emp.ApplyedToRecBars = Convert.ToInt32(SW.ReadLine());
                        emp.HasReinforcment = Convert.ToInt32(SW.ReadLine());
                        emp.Width = Convert.ToDouble(SW.ReadLine());
                        emp.Height = Convert.ToDouble(SW.ReadLine());
                        emp.CenterX = Convert.ToDouble(SW.ReadLine());
                        emp.CenterY = Convert.ToDouble(SW.ReadLine());
                        for (int j = 1; j < 5; j++)
                        {
                            emp.EDX1R[j] = Convert.ToDouble(SW.ReadLine());
                            emp.EDY1R[j] = Convert.ToDouble(SW.ReadLine());
                            emp.EDX2R[j] = Convert.ToDouble(SW.ReadLine());
                            emp.EDY2R[j] = Convert.ToDouble(SW.ReadLine());
                            emp.EDCoverR[j] = Convert.ToDouble(SW.ReadLine());
                        }
                        RecShape[i] = emp;
                    }
                    CircleBarsNumber = Convert.ToInt32(SW.ReadLine());
                    for (int i = 1; i < CircleBarsNumber + 1; i++)
                    {
                        CircleLineBars emp = new CircleLineBars();
                        emp.InCircleShape = Convert.ToInt32(SW.ReadLine());
                        emp.Diameter = Convert.ToDouble(SW.ReadLine());
                        emp.CenterX = Convert.ToDouble(SW.ReadLine());
                        emp.CenterY = Convert.ToDouble(SW.ReadLine());
                        emp.BarsNumbers = Convert.ToInt32(SW.ReadLine());
                        emp.DR = Convert.ToInt32(SW.ReadLine());
                        emp.TIEDR = Convert.ToInt32(SW.ReadLine());
                        for (int j = 1; j < 33; j++)
                        {
                            emp.PointXR[j] = Convert.ToDouble(SW.ReadLine());
                            emp.PointYR[j] = Convert.ToDouble(SW.ReadLine());
                        }
                        CircleLineBar[i] = emp;
                    }
                    CircleShapeNumber = Convert.ToInt32(SW.ReadLine());
                    for (int i = 1; i < CircleShapeNumber + 1; i++)
                    {
                        CircleShapes emp = new CircleShapes();
                        emp.ApplyedToCircleBars = Convert.ToInt32(SW.ReadLine());
                        emp.HasReinforcment = Convert.ToInt32(SW.ReadLine());
                        emp.Diameter = Convert.ToDouble(SW.ReadLine());
                        emp.CenterX = Convert.ToDouble(SW.ReadLine());
                        emp.CenterY = Convert.ToDouble(SW.ReadLine());
                        emp.CoverR = Convert.ToDouble(SW.ReadLine());
                        for (int j = 1; j < 33; j++)
                        {
                            emp.PointXR[j] = Convert.ToDouble(SW.ReadLine());
                            emp.PointYR[j] = Convert.ToDouble(SW.ReadLine());
                        }
                        CircleShape[i] = emp;
                    }
                    FlangedWallShapeNumber = Convert.ToInt32(SW.ReadLine());
                    for (int i = 1; i < FlangedWallShapeNumber + 1; i++)
                    {
                        FlangedWalls emp = new FlangedWalls();
                        emp.HasReinforcment = Convert.ToInt32(SW.ReadLine());
                        for (int j = 1; j < 4; j++)
                        {
                            emp.ApplyedToRecBars[j] = Convert.ToInt32(SW.ReadLine());
                        }
                        emp.CenterX = Convert.ToDouble(SW.ReadLine());
                        emp.CenterY = Convert.ToDouble(SW.ReadLine());
                        emp.Length = Convert.ToDouble(SW.ReadLine());
                        emp.StemWidth = Convert.ToDouble(SW.ReadLine());
                        emp.LeftFlangWidth = Convert.ToDouble(SW.ReadLine());
                        emp.LeftFlangLength = Convert.ToDouble(SW.ReadLine());
                        emp.RightFlangWidth = Convert.ToDouble(SW.ReadLine());
                        emp.RightFlangLength = Convert.ToDouble(SW.ReadLine());
                        emp.LeftFlangEccen = Convert.ToDouble(SW.ReadLine());
                        emp.RightFlangEccen = Convert.ToDouble(SW.ReadLine());
                        emp.ED1CoverR = Convert.ToDouble(SW.ReadLine());
                        emp.ED2CoverR = Convert.ToDouble(SW.ReadLine());
                        emp.ED3CoverR = Convert.ToDouble(SW.ReadLine());
                        emp.ED4CoverR = Convert.ToDouble(SW.ReadLine());
                        emp.ED1LCoverR = Convert.ToDouble(SW.ReadLine());
                        emp.ED2LCoverR = Convert.ToDouble(SW.ReadLine());
                        emp.ED3LCoverR = Convert.ToDouble(SW.ReadLine());
                        emp.ED4LCoverR = Convert.ToDouble(SW.ReadLine());
                        emp.ED1RCoverR = Convert.ToDouble(SW.ReadLine());
                        emp.ED2RCoverR = Convert.ToDouble(SW.ReadLine());
                        emp.ED3RCoverR = Convert.ToDouble(SW.ReadLine());
                        emp.ED4RCoverR = Convert.ToDouble(SW.ReadLine());
                        for (int j = 1; j < 13; j++)
                        {
                            emp.PointXReal[j] = Convert.ToDouble(SW.ReadLine());
                            emp.PointYReal[j] = Convert.ToDouble(SW.ReadLine());
                        }
                        FlangedWallShape[i] = emp;
                    }

                    TeeShapeNumber  = Convert.ToInt32(SW.ReadLine());
                    for (int i = 1; i < TeeShapeNumber + 1; i++)
                    {
                        TeeShape[i].HasReinforcment =Convert.ToInt32(SW.ReadLine());
                        for (int j = 1; j < 3; j++)
                        {
                             TeeShape[i].ApplyedToRecBars[j] = Convert.ToInt32(SW.ReadLine());
                        }
                        TeeShape[i].CenterX = Convert.ToDouble(SW.ReadLine());
                        TeeShape[i].CenterY = Convert.ToDouble(SW.ReadLine());
                        TeeShape[i].Height= Convert.ToDouble(SW.ReadLine());
                        TeeShape[i].FlangWidth= Convert.ToDouble(SW.ReadLine());
                        TeeShape[i].FlangThickness= Convert.ToDouble(SW.ReadLine());
                        TeeShape[i].WebThickness= Convert.ToDouble(SW.ReadLine());
                        TeeShape[i].ED1CoverR= Convert.ToDouble(SW.ReadLine());
                        TeeShape[i].ED2CoverR= Convert.ToDouble(SW.ReadLine());
                        TeeShape[i].ED3CoverR= Convert.ToDouble(SW.ReadLine());
                        TeeShape[i].ED4CoverR= Convert.ToDouble(SW.ReadLine());
                        TeeShape[i].ED1cCoverR= Convert.ToDouble(SW.ReadLine());
                        TeeShape[i].ED2cCoverR= Convert.ToDouble(SW.ReadLine());
                        TeeShape[i].ED3cCoverR= Convert.ToDouble(SW.ReadLine());
                        TeeShape[i].ED4cCoverR= Convert.ToDouble(SW.ReadLine());
                        for (int j = 1; j < 9; j++)
                        {
                            TeeShape[i].PointXReal[j]= Convert.ToDouble(SW.ReadLine());
                            TeeShape[i].PointYReal[j]= Convert.ToDouble(SW.ReadLine());
                        }
                    }

                    ClipsNumber = Convert.ToInt32(SW.ReadLine());
                    for (int i = 1; i < ClipsNumber + 1; i++)
                    {
                        Clips emp = new Clips();
                        emp.X1R = Convert.ToDouble(SW.ReadLine());
                        emp.Y1R = Convert.ToDouble(SW.ReadLine());
                        emp.X2R = Convert.ToDouble(SW.ReadLine());
                        emp.Y2R = Convert.ToDouble(SW.ReadLine());
                        emp.DR = Convert.ToInt32(SW.ReadLine());
                        emp.DR1 = Convert.ToInt32(SW.ReadLine());
                        emp.DR2 = Convert.ToInt32(SW.ReadLine());
                        emp.Type = Convert.ToInt32(SW.ReadLine());
                        Clip[i] = emp;
                    }
                    SW.Close();
                    Render2d();
                }
                //   catch
                {
                    //  MessageBox.Show("ERROR! Please check passphrase and do not attempt to edit cipher text");
                }
            }
            MakeTempFiles();
        }
        private void OpenTemp()
        {
            string strpath = TempFileName[TempSelectedFile];
            StreamReader SW = new StreamReader(strpath);
            {
                BarsNumber = Convert.ToInt32(SW.ReadLine());
                for (int i = 1; i < BarsNumber + 1; i++)
                {
                    Bars emp = new Bars();
                    emp.XR = Convert.ToDouble(SW.ReadLine());
                    emp.YR = Convert.ToDouble(SW.ReadLine());
                    emp.DR = Convert.ToDouble(SW.ReadLine());
                    emp.InLine = Convert.ToInt32(SW.ReadLine());
                    emp.InED = Convert.ToInt32(SW.ReadLine());
                    emp.InREC = Convert.ToInt32(SW.ReadLine());
                    emp.InCircle = Convert.ToInt32(SW.ReadLine());
                    emp.Type = Convert.ToInt32(SW.ReadLine());
                    emp.Corner = Convert.ToInt32(SW.ReadLine());
                    Bar[i] = emp;
                }
                LineBarsNumber = Convert.ToInt32(SW.ReadLine());
                for (int i = 1; i < LineBarsNumber + 1; i++)
                {
                    LineBars emp = new LineBars();
                    emp.X1R = Convert.ToDouble(SW.ReadLine());
                    emp.Y1R = Convert.ToDouble(SW.ReadLine());
                    emp.X2R = Convert.ToDouble(SW.ReadLine());
                    emp.Y2R = Convert.ToDouble(SW.ReadLine());
                    emp.DR = Convert.ToDouble(SW.ReadLine());
                    emp.Distance = Convert.ToDouble(SW.ReadLine());
                    emp.BarsNumbers = Convert.ToInt32(SW.ReadLine());
                    LineBar[i] = emp;
                }
                RecBarsNumber = Convert.ToInt32(SW.ReadLine());
                for (int i = 1; i < RecBarsNumber + 1; i++)
                {
                    RecLineBars emp = new RecLineBars();
                    emp.InRecShape = Convert.ToInt32(SW.ReadLine());
                    emp.InFlangedWallShape = Convert.ToInt32(SW.ReadLine());
                    emp.InTeeShape = Convert.ToInt32(SW.ReadLine());
                    emp.Width = Convert.ToDouble(SW.ReadLine());
                    emp.Height = Convert.ToDouble(SW.ReadLine());
                    emp.CenterX = Convert.ToDouble(SW.ReadLine());
                    emp.CenterY = Convert.ToDouble(SW.ReadLine());
                    emp.TIEDR = Convert.ToInt32(SW.ReadLine());
                    for (int j = 1; j < 5; j++)
                    {
                        emp.EDX1R[j] = Convert.ToDouble(SW.ReadLine());
                        emp.EDY1R[j] = Convert.ToDouble(SW.ReadLine());
                        emp.EDX2R[j] = Convert.ToDouble(SW.ReadLine());
                        emp.EDY2R[j] = Convert.ToDouble(SW.ReadLine());
                        emp.EDBarsNumbers[j] = Convert.ToInt32(SW.ReadLine());
                        emp.EDDR[j] = Convert.ToInt32(SW.ReadLine());
                        emp.EDDistance[j] = Convert.ToDouble(SW.ReadLine());
                        emp.CORDR[j] = Convert.ToInt32(SW.ReadLine());
                    }
                    RecLineBar[i] = emp;
                }
                RecShapeNumber = Convert.ToInt32(SW.ReadLine());
                for (int i = 1; i < RecShapeNumber + 1; i++)
                {
                    RecShapes emp = new RecShapes();
                    emp.ApplyedToRecBars = Convert.ToInt32(SW.ReadLine());
                    emp.HasReinforcment = Convert.ToInt32(SW.ReadLine());
                    emp.Width = Convert.ToDouble(SW.ReadLine());
                    emp.Height = Convert.ToDouble(SW.ReadLine());
                    emp.CenterX = Convert.ToDouble(SW.ReadLine());
                    emp.CenterY = Convert.ToDouble(SW.ReadLine());
                    for (int j = 1; j < 5; j++)
                    {
                        emp.EDX1R[j] = Convert.ToDouble(SW.ReadLine());
                        emp.EDY1R[j] = Convert.ToDouble(SW.ReadLine());
                        emp.EDX2R[j] = Convert.ToDouble(SW.ReadLine());
                        emp.EDY2R[j] = Convert.ToDouble(SW.ReadLine());
                        emp.EDCoverR[j] = Convert.ToDouble(SW.ReadLine());
                    }
                    RecShape[i] = emp;
                }
                CircleBarsNumber = Convert.ToInt32(SW.ReadLine());
                for (int i = 1; i < CircleBarsNumber + 1; i++)
                {
                    CircleLineBars emp = new CircleLineBars();
                    emp.InCircleShape = Convert.ToInt32(SW.ReadLine());
                    emp.Diameter = Convert.ToDouble(SW.ReadLine());
                    emp.CenterX = Convert.ToDouble(SW.ReadLine());
                    emp.CenterY = Convert.ToDouble(SW.ReadLine());
                    emp.BarsNumbers = Convert.ToInt32(SW.ReadLine());
                    emp.DR = Convert.ToInt32(SW.ReadLine());
                    emp.TIEDR = Convert.ToInt32(SW.ReadLine());
                    for (int j = 1; j < 33; j++)
                    {
                        emp.PointXR[j] = Convert.ToDouble(SW.ReadLine());
                        emp.PointYR[j] = Convert.ToDouble(SW.ReadLine());
                    }
                    CircleLineBar[i] = emp;
                }
                CircleShapeNumber = Convert.ToInt32(SW.ReadLine());
                for (int i = 1; i < CircleShapeNumber + 1; i++)
                {
                    CircleShapes emp = new CircleShapes();
                    emp.ApplyedToCircleBars = Convert.ToInt32(SW.ReadLine());
                    emp.HasReinforcment = Convert.ToInt32(SW.ReadLine());
                    emp.Diameter = Convert.ToDouble(SW.ReadLine());
                    emp.CenterX = Convert.ToDouble(SW.ReadLine());
                    emp.CenterY = Convert.ToDouble(SW.ReadLine());
                    emp.CoverR = Convert.ToDouble(SW.ReadLine());
                    for (int j = 1; j < 33; j++)
                    {
                        emp.PointXR[j] = Convert.ToDouble(SW.ReadLine());
                        emp.PointYR[j] = Convert.ToDouble(SW.ReadLine());
                    }
                    CircleShape[i] = emp;
                }
                FlangedWallShapeNumber = Convert.ToInt32(SW.ReadLine());
                for (int i = 1; i < FlangedWallShapeNumber + 1; i++)
                {
                    FlangedWalls emp = new FlangedWalls();
                    emp.HasReinforcment = Convert.ToInt32(SW.ReadLine());
                    for (int j = 1; j < 4; j++)
                    {
                        emp.ApplyedToRecBars[j] = Convert.ToInt32(SW.ReadLine());
                    }
                    emp.CenterX = Convert.ToDouble(SW.ReadLine());
                    emp.CenterY = Convert.ToDouble(SW.ReadLine());
                    emp.Length = Convert.ToDouble(SW.ReadLine());
                    emp.StemWidth = Convert.ToDouble(SW.ReadLine());
                    emp.LeftFlangWidth = Convert.ToDouble(SW.ReadLine());
                    emp.LeftFlangLength = Convert.ToDouble(SW.ReadLine());
                    emp.RightFlangWidth = Convert.ToDouble(SW.ReadLine());
                    emp.RightFlangLength = Convert.ToDouble(SW.ReadLine());
                    emp.LeftFlangEccen = Convert.ToDouble(SW.ReadLine());
                    emp.RightFlangEccen = Convert.ToDouble(SW.ReadLine());
                    emp.ED1CoverR = Convert.ToDouble(SW.ReadLine());
                    emp.ED2CoverR = Convert.ToDouble(SW.ReadLine());
                    emp.ED3CoverR = Convert.ToDouble(SW.ReadLine());
                    emp.ED4CoverR = Convert.ToDouble(SW.ReadLine());
                    emp.ED1LCoverR = Convert.ToDouble(SW.ReadLine());
                    emp.ED2LCoverR = Convert.ToDouble(SW.ReadLine());
                    emp.ED3LCoverR = Convert.ToDouble(SW.ReadLine());
                    emp.ED4LCoverR = Convert.ToDouble(SW.ReadLine());
                    emp.ED1RCoverR = Convert.ToDouble(SW.ReadLine());
                    emp.ED2RCoverR = Convert.ToDouble(SW.ReadLine());
                    emp.ED3RCoverR = Convert.ToDouble(SW.ReadLine());
                    emp.ED4RCoverR = Convert.ToDouble(SW.ReadLine());
                    for (int j = 1; j < 13; j++)
                    {
                        emp.PointXReal[j] = Convert.ToDouble(SW.ReadLine());
                        emp.PointYReal[j] = Convert.ToDouble(SW.ReadLine());
                    }
                    FlangedWallShape[i] = emp;
                }
                TeeShapeNumber = Convert.ToInt32(SW.ReadLine());
                for (int i = 1; i < TeeShapeNumber + 1; i++)
                {
                    TeeShape[i].HasReinforcment = Convert.ToInt32(SW.ReadLine());
                    for (int j = 1; j < 3; j++)
                    {
                        TeeShape[i].ApplyedToRecBars[j] = Convert.ToInt32(SW.ReadLine());
                    }
                    TeeShape[i].CenterX = Convert.ToDouble(SW.ReadLine());
                    TeeShape[i].CenterY = Convert.ToDouble(SW.ReadLine());
                    TeeShape[i].Height = Convert.ToDouble(SW.ReadLine());
                    TeeShape[i].FlangWidth = Convert.ToDouble(SW.ReadLine());
                    TeeShape[i].FlangThickness = Convert.ToDouble(SW.ReadLine());
                    TeeShape[i].WebThickness = Convert.ToDouble(SW.ReadLine());
                    TeeShape[i].ED1CoverR = Convert.ToDouble(SW.ReadLine());
                    TeeShape[i].ED2CoverR = Convert.ToDouble(SW.ReadLine());
                    TeeShape[i].ED3CoverR = Convert.ToDouble(SW.ReadLine());
                    TeeShape[i].ED4CoverR = Convert.ToDouble(SW.ReadLine());
                    TeeShape[i].ED1cCoverR = Convert.ToDouble(SW.ReadLine());
                    TeeShape[i].ED2cCoverR = Convert.ToDouble(SW.ReadLine());
                    TeeShape[i].ED3cCoverR = Convert.ToDouble(SW.ReadLine());
                    TeeShape[i].ED4cCoverR = Convert.ToDouble(SW.ReadLine());
                    for (int j = 1; j < 9; j++)
                    {
                        TeeShape[i].PointXReal[j] = Convert.ToDouble(SW.ReadLine());
                        TeeShape[i].PointYReal[j] = Convert.ToDouble(SW.ReadLine());
                    }
                }
                ClipsNumber = Convert.ToInt32(SW.ReadLine());
                for (int i = 1; i < ClipsNumber + 1; i++)
                {
                    Clips emp = new Clips();
                    emp.X1R = Convert.ToDouble(SW.ReadLine());
                    emp.Y1R = Convert.ToDouble(SW.ReadLine());
                    emp.X2R = Convert.ToDouble(SW.ReadLine());
                    emp.Y2R = Convert.ToDouble(SW.ReadLine());
                    emp.DR = Convert.ToInt32(SW.ReadLine());
                    emp.DR1 = Convert.ToInt32(SW.ReadLine());
                    emp.DR2 = Convert.ToInt32(SW.ReadLine());
                    emp.Type = Convert.ToInt32(SW.ReadLine());
                    Clip[i] = emp;
                }
                SW.Close();
            }
        }
        public void MakeTempFiles()
        {
            int M = TempFile;
            for (int i = TempSelectedFile + 1; i < M + 1; i++)
            {
                if ((System.IO.File.Exists(TempFileName[i])))
                {
                    System.IO.File.Delete(TempFileName[i]);
                    TempFile = TempFile - 1;
                }
            }
            TempFile = TempFile + 1;
            TempFileName[TempFile] = Convert.ToString(Directory.GetParent(@"./TempFilesSection/File" + TempFile + "/"));
            TempSelectedFile = TempFile;
            SaveTemp();
        }
        #endregion
        private void SectionDrawForm_Load(object sender, EventArgs e)
        {
            kx[1] = 1;
            ky[1] = -1;
            kx[2] = -1;
            ky[2] = -1;
            kx[3] = -1;
            ky[3] = 1;
            kx[4] = 1;
            ky[4] = 1;
            Array.ForEach(Directory.GetFiles(@"./TempFilesSection/"), File.Delete);
            RecShapes em = new RecShapes();
            RecShape[0] = em;
            RecLineBars em1 = new RecLineBars();
            RecLineBar[0] = em1;
            CircleShapes em2 = new CircleShapes();
            CircleShape[0] = em2;
            CircleLineBars em3 = new CircleLineBars();
            CircleLineBar[0] = em3;
            FlangedWalls em4 = new FlangedWalls();
            FlangedWallShape[0] = em4;

            pictureBox1.Controls.Add(pictureBox2);
            pictureBox2.Location = new Point(0, 0);
            pictureBox2.BackColor = Color.Transparent;
            Render2d();
            MakeTempFiles();
        }
        private void SectionDrawForm_KeyDown(object sender, KeyEventArgs e)
        {
            #region//الحذف
            if (e.KeyCode == Keys.Delete)
            {
                int ifselected = 0;
                #region   //قضبان منفردة
            startloop: { }
                for (int i = 1; i < BarsNumber + 1; i++)
                {
                    if (Bar[i].Selected == 1)
                    {
                        BarsNumber = BarsNumber - 1;
                        ifselected = 1;
                        for (int j = i; j < BarsNumber + 1; j++)
                        {
                            Bar[j] = Bar[j + 1];
                        }
                        if (i <= BarsNumber) goto startloop;
                    }
                }
                #endregion
                #region   //شناغل
            startloops: { }
                for (int i = 1; i < ClipsNumber + 1; i++)
                {
                    if (Clip[i].Selected == 1)
                    {
                        ClipsNumber = ClipsNumber - 1;
                        ifselected = 1;
                        for (int j = i; j < ClipsNumber + 1; j++)
                        {
                            Clip[j] = Clip[j + 1];
                        }
                        if (i <= ClipsNumber) goto startloops;
                    }
                }
                #endregion

                #region //خط قضبان
            startloop1: { }
                for (int i = 1; i < LineBarsNumber + 1; i++)
                {
                    if (LineBar[i].Selected == 1)
                    {
                    startloop2: { }
                        for (int k = 1; k < BarsNumber + 1; k++)
                        {
                            if (Bar[k].InLine == i)
                            {
                                BarsNumber = BarsNumber - 1;
                                for (int j = k; j < BarsNumber + 1; j++)
                                {
                                    Bar[j] = Bar[j + 1];
                                }
                                if (k <= BarsNumber) goto startloop2;
                            }
                        }
                        for (int k = 1; k < BarsNumber + 1; k++)
                        {
                            if (Bar[k].InLine > i)
                            {
                                Bar[k].InLine = Bar[k].InLine - 1;
                            }
                        }
                        LineBarsNumber = LineBarsNumber - 1;
                        ifselected = 1;
                        for (int j = i; j < LineBarsNumber + 1; j++)
                        {
                            LineBar[j] = LineBar[j + 1];
                        }
                        if (i <= LineBarsNumber) goto startloop1;
                    }
                }
                #endregion
                #region //مستطيل قضبان
            startloop10: { }
                for (int i = 1; i < RecBarsNumber + 1; i++)
                {
                    if (RecLineBar[i].EDSelected[1] == 1)
                    {
                    startloop20: { }
                        for (int k = 1; k < BarsNumber + 1; k++)
                        {
                            if (Bar[k].InREC == i)
                            {
                                BarsNumber = BarsNumber - 1;
                                for (int j = k; j < BarsNumber + 1; j++)
                                {
                                    Bar[j] = Bar[j + 1];
                                }
                                if (k <= BarsNumber) goto startloop20;
                            }
                        }
                        for (int k = 1; k < BarsNumber + 1; k++)
                        {
                            if (Bar[k].InREC > i)
                            {
                                Bar[k].InREC = Bar[k].InREC - 1;
                            }
                        }
                        RecBarsNumber = RecBarsNumber - 1;
                        ifselected = 1;
                        for (int j = i; j < RecBarsNumber + 1; j++)
                        {
                            RecLineBar[j] = RecLineBar[j + 1];
                        }
                        for (int j = 1; j < RecShapeNumber + 1; j++)//مضاف
                        {
                            if (RecShape[j].ApplyedToRecBars > i)
                            {
                                RecShape[j].ApplyedToRecBars = RecShape[j].ApplyedToRecBars - 1;
                            }
                        }
                        for (int j = 1; j < TeeShapeNumber + 1; j++)//مضاف
                        {
                            if (TeeShape[j].ApplyedToRecBars[1] > i)
                            {
                                TeeShape[j].ApplyedToRecBars[1] = TeeShape[j].ApplyedToRecBars[1] - 1;
                                TeeShape[j].ApplyedToRecBars[2] = TeeShape[j].ApplyedToRecBars[2] - 1;
                            }
                        }
                        for (int j = 1; j < FlangedWallShapeNumber + 1; j++)//مضاف
                        {
                            if (FlangedWallShape[j].ApplyedToRecBars[1] > i)
                            {
                                FlangedWallShape[j].ApplyedToRecBars[1] = FlangedWallShape[j].ApplyedToRecBars[1] - 1;
                                FlangedWallShape[j].ApplyedToRecBars[2] = FlangedWallShape[j].ApplyedToRecBars[2] - 1;
                                FlangedWallShape[j].ApplyedToRecBars[3] = FlangedWallShape[j].ApplyedToRecBars[3] - 1;
                            }
                        }
                        if (i <= RecBarsNumber) goto startloop10;
                    }
                }
                #endregion
                #region//مستطيل شكل
            startloop100: { }
                for (int i = 1; i < RecShapeNumber + 1; i++)
                {
                    if (RecShape[i].EDSelected[1] == 1)
                    {
                        RecShapeNumber = RecShapeNumber - 1;
                        ifselected = 1;
                        for (int j = i; j < RecShapeNumber + 1; j++)
                        {
                            RecShape[j] = RecShape[j + 1];
                        }
                        for (int j = 1; j < RecBarsNumber + 1; j++)//مضاف
                        {
                            if (RecLineBar[j].InRecShape > i)
                            {
                                RecLineBar[j].InRecShape = RecLineBar[j].InRecShape - 1;
                            }
                        }
                        if (i <= RecShapeNumber) goto startloop100;
                    }
                }
                #endregion
                #region//تيه شكل
            startloop2001: { }
                for (int i = 1; i < TeeShapeNumber + 1; i++)
                {
                    if (TeeShape[i].Selected == 1)
                    {
                        TeeShapeNumber = TeeShapeNumber - 1;
                        ifselected = 1;
                        for (int j = i; j < TeeShapeNumber + 1; j++)
                        {
                            TeeShape[j] = TeeShape[j + 1];
                        }
                        for (int j = 1; j < RecBarsNumber + 1; j++)//مضاف
                        {
                            if (RecLineBar[j].InTeeShape > i)
                            {
                                RecLineBar[j].InTeeShape = RecLineBar[j].InTeeShape - 1;
                            }
                        }
                        if (i <= TeeShapeNumber) goto startloop2001;
                    }
                }
                #endregion
                #region//جدار شكل
            startloop2000: { }
                for (int i = 1; i < FlangedWallShapeNumber + 1; i++)
                {
                    if (FlangedWallShape[i].Selected == 1)
                    {
                        FlangedWallShapeNumber = FlangedWallShapeNumber - 1;
                        ifselected = 1;
                        for (int j = i; j < FlangedWallShapeNumber + 1; j++)
                        {
                            FlangedWallShape[j] = FlangedWallShape[j + 1];
                        }
                        for (int j = 1; j < RecBarsNumber + 1; j++)//مضاف
                        {
                            if (RecLineBar[j].InFlangedWallShape > i)
                            {
                                RecLineBar[j].InFlangedWallShape = RecLineBar[j].InFlangedWallShape - 1;
                            }
                        }
                        if (i <= FlangedWallShapeNumber) goto startloop2000;
                    }
                }
                #endregion
                #region //دائرة قضبان
            startloop110: { }
                for (int i = 1; i < CircleBarsNumber + 1; i++)
                {
                    if (CircleLineBar[i].Selected == 1)
                    {
                    startloop120: { }
                        for (int k = 1; k < BarsNumber + 1; k++)
                        {
                            if (Bar[k].InCircle == i)
                            {
                                BarsNumber = BarsNumber - 1;
                                for (int j = k; j < BarsNumber + 1; j++)
                                {
                                    Bar[j] = Bar[j + 1];
                                }
                                if (k <= BarsNumber) goto startloop120;
                            }
                        }
                        for (int k = 1; k < BarsNumber + 1; k++)
                        {
                            if (Bar[k].InCircle > i)
                            {
                                Bar[k].InCircle = Bar[k].InCircle - 1;
                            }
                        }
                        CircleBarsNumber = CircleBarsNumber - 1;
                        ifselected = 1;
                        for (int j = i; j < CircleBarsNumber + 1; j++)
                        {
                            CircleLineBar[j] = CircleLineBar[j + 1];
                        }
                        for (int j = 1; j < CircleShapeNumber + 1; j++)//مضاف
                        {
                            if (CircleShape[j].ApplyedToCircleBars > i)
                            {
                                CircleShape[j].ApplyedToCircleBars = CircleShape[j].ApplyedToCircleBars - 1;
                            }
                        }
                        if (i <= CircleBarsNumber) goto startloop110;
                    }
                }
                #endregion
                #region//دائرة شكل
            startloop1000: { }
                for (int i = 1; i < CircleShapeNumber + 1; i++)
                {
                    if (CircleShape[i].Selected == 1)
                    {
                        CircleShapeNumber = CircleShapeNumber - 1;
                        ifselected = 1;
                        for (int j = i; j < CircleShapeNumber + 1; j++)
                        {
                            CircleShape[j] = CircleShape[j + 1];
                        }
                        for (int j = 1; j < CircleBarsNumber + 1; j++)//مضاف
                        {
                            if (CircleLineBar[j].InCircleShape > i)
                            {
                                CircleLineBar[j].InCircleShape = CircleLineBar[j].InCircleShape - 1;
                            }
                        }
                        if (i <= CircleShapeNumber) goto startloop1000;
                    }
                }
                #endregion
                if (ifselected == 1)
                {
                    MakeTempFiles();
                    Render2d();
                }
            }
            #endregion
            #region//التراجع
            if (e.Control && e.KeyCode == Keys.Z)
            {
                if (TempSelectedFile - 1 > 0)
                {
                    drowclick = 0;
                    LineMove2dVisible = 0;
                    TempSelectedFile = TempSelectedFile - 1;
                    OpenTemp();
                    UnSellectAll();
                    Render2d();
                }
            }
            #endregion
            #region // الايسكيب
            if (e.KeyCode == Keys.Escape)
            {
                UnSellectAll();
                if (EditType == 1)
                {
                    if (AnyKindtoEditIs == "RecShape") DrawForEditRecShape();
                    if (AnyKindtoEditIs == "RecLineBar") DrawForEditRecLineBar();
                    if (AnyKindtoEditIs == "CircleShape") DrawForEditCircleShape();
                    if (AnyKindtoEditIs == "CircleLineBar") DrawForEditCircleLineBar();
                    if (AnyKindtoEditIs == "FlangedWallShape") DrawForEditFlangedWallShape();
                    AnyKindtoEditIs = "";
                    EditType = 0;
                }
                toolStripButton14.Checked = false;
                toolStripButton1.Checked = true;
                // Render2d();
            }
            #endregion
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #region//pictureBox2
        private void pictureBox2_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                Zoom2d = Math.Round(Zoom2d + 0.05, 2);
                startX2d = lastzoomX2d - (int)(lastzoomX2dR * Zoom2d);
                startY2d = lastzoomY2d + (int)(lastzoomY2dR * Zoom2d);
                TempX = lastzoomX2d;
                TempY = lastzoomY2d;
                Render2d();
                pictureBox2Draw();
                if (EditType == 1)
                {
                    if (AnyKindtoEditIs == "RecShape") DrawForEditRecShape();
                    if (AnyKindtoEditIs == "RecLineBar") DrawForEditRecLineBar();
                    if (AnyKindtoEditIs == "CircleShape") DrawForEditCircleShape();
                    if (AnyKindtoEditIs == "CircleLineBar") DrawForEditCircleLineBar();
                    if (AnyKindtoEditIs == "FlangedWallShape") DrawForEditFlangedWallShape();
                }
            }
            else
            {
                if (Math.Round(Zoom2d - 0.05, 2) > 0.01)
                {
                    Zoom2d = Math.Round(Zoom2d - 0.05, 2);
                    startX2d = lastzoomX2d - (int)(lastzoomX2dR * Zoom2d);
                    startY2d = lastzoomY2d + (int)(lastzoomY2dR * Zoom2d);
                    TempX = lastzoomX2d;
                    TempY = lastzoomY2d;
                    Render2d();
                    pictureBox2Draw();
                    if (EditType == 1)
                    {
                        if (AnyKindtoEditIs == "RecShape") DrawForEditRecShape();
                        if (AnyKindtoEditIs == "RecLineBar") DrawForEditRecLineBar();
                        if (AnyKindtoEditIs == "CircleShape") DrawForEditCircleShape();
                        if (AnyKindtoEditIs == "CircleLineBar") DrawForEditCircleLineBar();
                        if (AnyKindtoEditIs == "FlangedWallShape") DrawForEditFlangedWallShape();
                    }
                }
            }
        }
        int AriaSelected = 0;
        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            lastzoomX2d = e.X;
            lastzoomY2d = e.Y;
            if (e.Button == MouseButtons.Left)
            {
                if (ClickToEdit == 1)
                {
                    if (AnyKindtoEditIs == "RecShape") DrawForEditRecShape();
                    if (AnyKindtoEditIs == "RecLineBar") DrawForEditRecLineBar();
                    if (AnyKindtoEditIs == "CircleShape") DrawForEditCircleShape();
                    if (AnyKindtoEditIs == "CircleLineBar") DrawForEditCircleLineBar();
                    if (AnyKindtoEditIs == "FlangedWallShape") DrawForEditFlangedWallShape();
                    if (AnyKindtoEditIs == "TeeShape") DrawForEditTeeShape();
                }
            }
            #region // تحريك المسقط
            if (e.Button == MouseButtons.Middle)
            {
                timetodo = timetodo + 1;
                startX2d = startX2d + (Cursor.Position.X - xmove);
                startY2d = startY2d + (Cursor.Position.Y - ymove);
                xmove = Cursor.Position.X;
                ymove = Cursor.Position.Y;
                if (timetodo > 2)
                {
                    Render2d();
                    timetodo = 0;
                    Bitmap finalBmp = new Bitmap(BitampWidth2d, BitampHight2d);
                    if (pictureBox2.Image != null)
                    {
                        pictureBox2.Image.Dispose();
                        pictureBox2.Image = null;
                    }
                    pictureBox2.Image = finalBmp;
                    if (EditType == 1)
                    {
                        if (AnyKindtoEditIs == "RecShape") DrawForEditRecShape();
                        if (AnyKindtoEditIs == "RecLineBar") DrawForEditRecLineBar();
                        if (AnyKindtoEditIs == "CircleShape") DrawForEditCircleShape();
                        if (AnyKindtoEditIs == "CircleLineBar") DrawForEditCircleLineBar();
                        if (AnyKindtoEditIs == "FlangedWallShape") DrawForEditFlangedWallShape();
                    }
                }
                goto ENDLOOP;
            }
            #endregion
            #region//التقاط الاوسناب
            TempX = e.X;
            TempY = e.Y;
            TempX12Real = Math.Round((TempX - startX2d) / (Zoom2d), 0);
            TempY12Real = Math.Round((startY2d - TempY) / (Zoom2d), 0);
            lastzoomX2dR = TempX12Real;
            lastzoomY2dR = TempY12Real;
            Tahkik = 0;
            if (Tahkik == 0)
            {
                #region//تقاطعات الشبكة
                if (GridIntersections == 1)
                {
                    for (int i = 1; i < GridPointNumber + 1; i++)
                    {
                        if (Math.Abs(TempX - GridX2D[i]) < 4 & Math.Abs(TempY - GridY2D[i]) < 4)
                        {
                            Tahkik = 1;
                            SnapFromType = "GridIntersection";
                            lastzoomX2dR = GridXR[i];
                            lastzoomY2dR = GridYR[i];
                            TempX12Real = lastzoomX2dR;
                            TempY12Real = lastzoomY2dR;
                            TempX = GridX2D[i];
                            TempY = GridY2D[i];
                            goto endloop;
                        }
                    }
                }
                #endregion
                #region//عقد القضبان
                if (GridIntersections == 1)
                {
                    for (int i = 1; i < BarsNumber + 1; i++)
                    {
                        if (Math.Abs(TempX - Bar[i].X2D) < 4 & Math.Abs(TempY - Bar[i].Y2D) < 4)
                        {
                            Tahkik = 1;
                            SnapFromType = "Point";
                            lastzoomX2dR = Bar[i].XR;
                            lastzoomY2dR = Bar[i].YR;
                            TempX12Real = lastzoomX2dR;
                            TempY12Real = lastzoomY2dR;
                            TempX = Bar[i].X2D;
                            TempY = Bar[i].Y2D;
                            goto endloop;
                        }
                    }
                }
                #endregion
                #region//نهايات خطوط القضبان
                if (GridIntersections == 1 || LineEnds == 1)
                {
                    for (int i = 1; i < LineBarsNumber + 1; i++)
                    {
                        if (Math.Abs(TempX - LineBar[i].X12D) < 4 & Math.Abs(TempY - LineBar[i].Y12D) < 4)
                        {
                            Tahkik = 1;
                            if (GridIntersections == 1)
                            {
                                SnapFromType = "Point";
                            }
                            else
                            {
                                SnapFromType = "End Point";
                            }
                            lastzoomX2dR = LineBar[i].X1R;
                            lastzoomY2dR = LineBar[i].Y1R;
                            TempX12Real = lastzoomX2dR;
                            TempY12Real = lastzoomY2dR;
                            TempX = LineBar[i].X12D;
                            TempY = LineBar[i].Y12D;
                            goto endloop;
                        }
                        if (Math.Abs(TempX - LineBar[i].X22D) < 4 & Math.Abs(TempY - LineBar[i].Y22D) < 4)
                        {
                            Tahkik = 1;
                            SelectedJoint = i;
                            if (GridIntersections == 1)
                            {
                                SnapFromType = "Point";
                            }
                            else
                            {
                                SnapFromType = "End Point";
                            }
                            lastzoomX2dR = LineBar[i].X2R;
                            lastzoomY2dR = LineBar[i].Y2R;
                            TempX12Real = lastzoomX2dR;
                            TempY12Real = lastzoomY2dR;
                            TempX = LineBar[i].X22D;
                            TempY = LineBar[i].Y22D;
                            goto endloop;
                        }
                    }
                }
                #endregion
                #region//عقد التسليح الدائري
                if (GridIntersections == 1 || LineEnds == 1)
                {
                    for (int i = 1; i < CircleBarsNumber + 1; i++)
                    {
                        for (int j = 1; j < 32 + 1; j++)
                        {
                            if (Math.Abs(TempX - CircleLineBar[i].PointX2D[j]) < 4 & Math.Abs(TempY - CircleLineBar[i].PointY2D[j]) < 4)
                            {
                                Tahkik = 1;
                                if (GridIntersections == 1)
                                {
                                    SnapFromType = "Point";
                                }
                                else
                                {
                                    SnapFromType = "End Point";
                                }
                                lastzoomX2dR = CircleLineBar[i].PointXR[j];
                                lastzoomY2dR = CircleLineBar[i].PointYR[j];
                                TempX12Real = lastzoomX2dR;
                                TempY12Real = lastzoomY2dR;
                                TempX = CircleLineBar[i].PointX2D[j];
                                TempY = CircleLineBar[i].PointY2D[j];
                                goto endloop;
                            }
                        }
                    }
                }
                #endregion
                #region//عقد الشكل الدائري
                if (GridIntersections == 1 || LineEnds == 1)
                {
                    for (int i = 1; i < CircleShapeNumber + 1; i++)
                    {
                        for (int j = 1; j < 32 + 1; j++)
                        {
                            if (Math.Abs(TempX - CircleShape[i].PointX2D[j]) < 4 & Math.Abs(TempY - CircleShape[i].PointY2D[j]) < 4)
                            {
                                Tahkik = 1;
                                if (GridIntersections == 1)
                                {
                                    SnapFromType = "Point";
                                }
                                else
                                {
                                    SnapFromType = "End Point";
                                }
                                lastzoomX2dR = CircleShape[i].PointXR[j];
                                lastzoomY2dR = CircleShape[i].PointYR[j];
                                TempX12Real = lastzoomX2dR;
                                TempY12Real = lastzoomY2dR;
                                TempX = CircleShape[i].PointX2D[j];
                                TempY = CircleShape[i].PointY2D[j];
                                goto endloop;
                            }
                        }
                    }
                }
                #endregion
                #region//عقد التسليح مربع
                if (GridIntersections == 1 || LineEnds == 1)
                {
                    for (int i = 1; i < RecBarsNumber + 1; i++)
                    {
                        for (int j = 1; j < 4 + 1; j++)
                        {
                            int xx = 0;
                            int yy = 0;
                            double xxR = 0;
                            double yyR = 0;
                            xx = RecLineBar[i].EDX12D[j];
                            yy = RecLineBar[i].EDY12D[j];
                            xxR = RecLineBar[i].EDX1R[j];
                            yyR = RecLineBar[i].EDY1R[j];
                            if (Math.Abs(TempX - xx) < 4 & Math.Abs(TempY - yy) < 4)
                            {
                                Tahkik = 1;
                                if (GridIntersections == 1)
                                {
                                    SnapFromType = "Point";
                                }
                                else
                                {
                                    SnapFromType = "End Point";
                                }
                                lastzoomX2dR = xxR;
                                lastzoomY2dR = yyR;
                                TempX12Real = lastzoomX2dR;
                                TempY12Real = lastzoomY2dR;
                                TempX = xx;
                                TempY = yy;
                                goto endloop;
                            }
                        }
                    }
                }
                #endregion
                #region//عقد شكل مربع
                if (GridIntersections == 1 || LineEnds == 1)
                {
                    for (int i = 1; i < RecShapeNumber + 1; i++)
                    {
                        for (int j = 1; j < 4 + 1; j++)
                        {
                            int xx = RecShape[i].EDX12D[j];
                            int yy = RecShape[i].EDY12D[j];
                            double xxR = RecShape[i].EDX1R[j];
                            double yyR = RecShape[i].EDY1R[j];
                            if (Math.Abs(TempX - xx) < 4 & Math.Abs(TempY - yy) < 4)
                            {
                                Tahkik = 1;
                                if (GridIntersections == 1)
                                {
                                    SnapFromType = "Point";
                                }
                                else
                                {
                                    SnapFromType = "End Point";
                                }
                                lastzoomX2dR = xxR;
                                lastzoomY2dR = yyR;
                                TempX12Real = lastzoomX2dR;
                                TempY12Real = lastzoomY2dR;
                                TempX = xx;
                                TempY = yy;
                                goto endloop;
                            }
                        }
                    }
                }
                #endregion
                #region//عقد الشكل جدار
                if (GridIntersections == 1 || LineEnds == 1)
                {
                    for (int i = 1; i < FlangedWallShapeNumber + 1; i++)
                    {
                        for (int j = 1; j < 12 + 1; j++)
                        {
                            if (Math.Abs(TempX - FlangedWallShape[i].PointX2D[j]) < 4 & Math.Abs(TempY - FlangedWallShape[i].PointY2D[j]) < 4)
                            {
                                Tahkik = 1;
                                if (GridIntersections == 1)
                                {
                                    SnapFromType = "Point";
                                }
                                else
                                {
                                    SnapFromType = "End Point";
                                }
                                lastzoomX2dR = FlangedWallShape[i].PointXReal[j];
                                lastzoomY2dR = FlangedWallShape[i].PointYReal[j];
                                TempX12Real = lastzoomX2dR;
                                TempY12Real = lastzoomY2dR;
                                TempX = FlangedWallShape[i].PointX2D[j];
                                TempY = FlangedWallShape[i].PointY2D[j];
                                goto endloop;
                            }
                        }
                    }
                }
                #endregion
                #region//التعامد
                // if (Snap.Prallels == 1)
                {
                    float sweepAngle = (float)Math.Round(Angulo(TempXReal[1], TempYReal[1], TempX12Real, TempY12Real), 2);
                    if (Math.Abs(sweepAngle - 90) < 0.8 || Math.Abs(sweepAngle - 270) < 0.8)
                    {
                        Tahkik = 1;
                        SnapFromType = "Prallels";
                        TempX = LineMoveX1;
                        TempY = lastzoomY2d;
                        double XREAL = Math.Round((lastzoomX2d - startX2d) / (Zoom2d), 0);
                        double YREAL = Math.Round((startY2d - lastzoomY2d) / (Zoom2d), 0);
                        lastzoomX2dR = TempXReal[1];
                        lastzoomY2dR = YREAL;
                        TempX12Real = TempXReal[1];
                        TempY12Real = YREAL;
                        goto endloop;
                    }
                    if (Math.Abs(sweepAngle - 180) < 0.8 || Math.Abs(sweepAngle - 0) < 0.8 || Math.Abs(sweepAngle - 360) < 0.8)
                    {
                        Tahkik = 1;
                        SnapFromType = "Prallels";
                        TempX = lastzoomX2d;
                        TempY = LineMoveY1;
                        double XREAL = Math.Round((lastzoomX2d - startX2d) / (Zoom2d), 0);
                        double YREAL = Math.Round((startY2d - lastzoomY2d) / (Zoom2d), 0);
                        lastzoomX2dR = XREAL;
                        lastzoomY2dR = TempYReal[1];
                        TempX12Real = XREAL;
                        TempY12Real = TempYReal[1];
                        goto endloop;
                    }
                }
                #endregion
            }
        endloop: { }
            LBLX.Visible = true;
            LBLY.Visible = true;
            LBLX.Text = "X= " + Convert.ToString(Math.Round(TempX12Real, 0));
            LBLY.Text = "Y= " + Convert.ToString(Math.Round(TempY12Real, 0));
            #endregion
            # region//تحديد البلاطات بالمرور على المساحات
            int ifselected = 0;
            int Xtest = e.X;
            int Ytest = e.Y;
            for (int i = 1; i < CallMesh.AriaNoF + 1; i++)
            {
                int N = CallMesh.AriaPointNoF[i];
                Point[] polygon = new Point[N + 1];
                for (int j = 1; j < N + 1; j++)
                {
                    polygon[j].X = startX2d + Convert.ToInt32((CallMesh.AriaPointXF[i, j] * Zoom2d));
                    polygon[j].Y = startY2d - Convert.ToInt32((CallMesh.AriaPointYF[i, j] * Zoom2d));
                }
                bool result = false;
                int nvert = N;
                int k, l;
                for (k = 1, l = nvert - 1; k < nvert; l = k++)
                {
                    if (((polygon[k].Y > Ytest) != (polygon[l].Y > Ytest)) &&
                     (Xtest < (polygon[l].X - polygon[k].X) * (Ytest - polygon[k].Y) / (polygon[l].Y - polygon[k].Y) + polygon[k].X))
                        result = !result;
                }
                if (result == true & AriaSelected != i)
                {
                    AriaSelected = i;
                    break;
                }
                if (result == false)
                {
                    ifselected = ifselected + 1;
                }
            }
            if (ifselected == CallMesh.AriaNoF & AriaSelected != 0)
            {
                AriaSelected = 0;
                Bitmap finalBmp = new Bitmap(BitampWidth2d, BitampHight2d);
                if (pictureBox2.Image != null)
                {
                    pictureBox2.Image.Dispose();
                    pictureBox2.Image = null;
                }
                pictureBox2.Image = finalBmp;
            }
            #endregion
            if (e.Button == MouseButtons.Middle) goto ENDLOOP;
            MouseButtonsLeft = 0;
            if (e.Button == MouseButtons.Left) MouseButtonsLeft = 1;
            timetodo = timetodo + 1;
            if (timetodo > 2)
            {
                if (EditType == 0)
                {
                    pictureBox2Draw();
                }
                else
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        if (AnyKindtoEditIs == "RecShape") DrawForEditRecShape();
                        if (AnyKindtoEditIs == "RecLineBar") DrawForEditRecLineBar();
                        if (AnyKindtoEditIs == "CircleShape") DrawForEditCircleShape();
                        if (AnyKindtoEditIs == "CircleLineBar") DrawForEditCircleLineBar();
                        if (AnyKindtoEditIs == "FlangedWallShape") DrawForEditFlangedWallShape();
                        if (AnyKindtoEditIs == "TeeShape") DrawForEditTeeShape();
                        pictureBox2Drawsub();
                    }
                }
                timetodo = 0;
            }
        ENDLOOP: { }
        }
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            xmove = Cursor.Position.X;
            ymove = Cursor.Position.Y;
            xmove1 = e.X;
            ymove1 = e.Y;
            int ifselected = 0;
            if (e.Button == System.Windows.Forms.MouseButtons.Left || e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                SelectedType = 0;
                SelecteLinedBar = 0;
                SelectedBar = 0;
                SelectedInED = 0;
                SelectedRecBar = 0;
                SelectedRecBar1 = 0;
                SelectedRecBar2 = 0;
                SelectedRecBar3 = 0;
                SelectedCorner = 0;
                SelectedRecShape = 0;
                SelectedCircleLineBar = 0;
                SelectedFlangedWallShape = 0;
                SelectedCircleShape = 0;
                RecEdgeToEdit = 0;
                if (EditType == 1)
                {
                    if (AnyKindtoEditIs == "RecShape") DrawForEditRecShape();
                    if (AnyKindtoEditIs == "RecLineBar") DrawForEditRecLineBar();
                    if (AnyKindtoEditIs == "CircleShape") DrawForEditCircleShape();
                    if (AnyKindtoEditIs == "CircleLineBar") DrawForEditCircleLineBar();
                    if (AnyKindtoEditIs == "FlangedWallShape") DrawForEditFlangedWallShape();
                    if (AnyKindtoEditIs == "TeeShape") DrawForEditTeeShape();
                    AnyKindtoEditIs = "";
                }
                if (toolStripButton1.Checked == true || toolStripButton14.Checked == true)
                {
                    #region//تحديد العقد
                    for (int i = 1; i < BarsNumber + 1; i++)
                    {
                        if (Math.Abs(TempX - Bar[i].X2D) < 4 & Math.Abs(TempY - Bar[i].Y2D) < 4)
                        {
                            if (Bar[i].Type == 1)
                            {
                                ifselected = 1;
                                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                                {
                                    if (Bar[i].Selected == 1)
                                    {
                                        Bar[i].Selected = 0;
                                    }
                                    else
                                    {
                                        Bar[i].Selected = 1;
                                    }
                                }
                                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                                {
                                    SelectedBar = i;
                                    SelecteLinedBar = Bar[i].InLine;
                                    SelectedType = Bar[i].Type;
                                    SelectedInED = Bar[i].InED;
                                    SelectedRecBar = Bar[i].InREC;
                                    SelectedCorner = Bar[i].Corner;
                                    Bar[i].Selected = 1;
                                    ifselected = 0;
                                    Render2d();
                                    SectionBarsPropertyForm sectionBarsPropertyForm = new SectionBarsPropertyForm();
                                    sectionBarsPropertyForm.ShowDialog();
                                }
                                goto endloop1;
                            }
                            if (Bar[i].Type == 2)
                            {
                                ifselected = 1;
                                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                                {
                                    if (LineBar[Bar[i].InLine].Selected == 1)
                                    {
                                        LineBar[Bar[i].InLine].Selected = 0;
                                    }
                                    else
                                    {
                                        LineBar[Bar[i].InLine].Selected = 1;
                                    }
                                }
                                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                                {
                                    SelectedBar = i;
                                    SelecteLinedBar = Bar[i].InLine;
                                    SelectedType = Bar[i].Type;
                                    SelectedInED = Bar[i].InED;
                                    SelectedRecBar = Bar[i].InREC;
                                    SelectedCorner = Bar[i].Corner;
                                    LineBar[Bar[i].InLine].Selected = 1;
                                    ifselected = 0;
                                    Render2d();
                                    SectionBarsPropertyForm sectionBarsPropertyForm = new SectionBarsPropertyForm();
                                    sectionBarsPropertyForm.ShowDialog();
                                }
                                goto endloop1;
                            }
                            if (Bar[i].Type == 3 || Bar[i].Type == 4)
                            {
                                ifselected = 1;
                                SelectedInED = Bar[i].InED;
                                SelectedRecBar = Bar[i].InREC;
                                SelectedRecShape = RecLineBar[SelectedRecBar].InRecShape;
                                SelectedFlangedWallShape = RecLineBar[SelectedRecBar].InFlangedWallShape;
                                SelectedTeeShape = RecLineBar[SelectedRecBar].InTeeShape;
                                if (SelectedFlangedWallShape != 0)
                                {
                                    SelectedRecBar1 = FlangedWallShape[SelectedFlangedWallShape].ApplyedToRecBars[1];
                                    SelectedRecBar2 = FlangedWallShape[SelectedFlangedWallShape].ApplyedToRecBars[2];
                                    SelectedRecBar3 = FlangedWallShape[SelectedFlangedWallShape].ApplyedToRecBars[3];
                                }
                                if (SelectedTeeShape != 0)
                                {
                                    SelectedRecBar1 = TeeShape[SelectedTeeShape].ApplyedToRecBars[1];
                                    SelectedRecBar2 = TeeShape[SelectedTeeShape].ApplyedToRecBars[2];
                                }

                                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                                {
                                    if (RecLineBar[SelectedRecBar].EDSelected[1] == 1)
                                    {
                                        RecLineBar[SelectedRecBar].EDSelected[1] = 0;
                                        RecLineBar[SelectedRecBar].EDSelected[2] = 0;
                                        RecLineBar[SelectedRecBar].EDSelected[3] = 0;
                                        RecLineBar[SelectedRecBar].EDSelected[4] = 0;
                                        if (SelectedRecShape != 0)
                                        {
                                            for (int add = 1; add < 5; add++)
                                            {
                                                RecShape[SelectedRecShape].EDSelected[add] = 0;
                                            }
                                        }
                                        if (SelectedTeeShape != 0)
                                        {
                                            TeeShape[SelectedTeeShape].Selected = 0;
                                            RecLineBar[SelectedRecBar1].EDSelected[1] = 0;
                                            RecLineBar[SelectedRecBar1].EDSelected[2] = 0;
                                            RecLineBar[SelectedRecBar1].EDSelected[3] = 0;
                                            RecLineBar[SelectedRecBar1].EDSelected[4] = 0;
                                            RecLineBar[SelectedRecBar2].EDSelected[1] = 0;
                                            RecLineBar[SelectedRecBar2].EDSelected[2] = 0;
                                            RecLineBar[SelectedRecBar2].EDSelected[3] = 0;
                                            RecLineBar[SelectedRecBar2].EDSelected[4] = 0;
                                        }
                                        if (SelectedFlangedWallShape != 0)
                                        {
                                            FlangedWallShape[SelectedFlangedWallShape].Selected = 0;
                                            RecLineBar[SelectedRecBar1].EDSelected[1] = 0;
                                            RecLineBar[SelectedRecBar1].EDSelected[2] = 0;
                                            RecLineBar[SelectedRecBar1].EDSelected[3] = 0;
                                            RecLineBar[SelectedRecBar1].EDSelected[4] = 0;
                                            RecLineBar[SelectedRecBar2].EDSelected[1] = 0;
                                            RecLineBar[SelectedRecBar2].EDSelected[2] = 0;
                                            RecLineBar[SelectedRecBar2].EDSelected[3] = 0;
                                            RecLineBar[SelectedRecBar2].EDSelected[4] = 0;
                                            RecLineBar[SelectedRecBar3].EDSelected[1] = 0;
                                            RecLineBar[SelectedRecBar3].EDSelected[2] = 0;
                                            RecLineBar[SelectedRecBar3].EDSelected[3] = 0;
                                            RecLineBar[SelectedRecBar3].EDSelected[4] = 0;
                                        }
                                    }
                                    else
                                    {
                                        RecLineBar[SelectedRecBar].EDSelected[1] = 1;
                                        RecLineBar[SelectedRecBar].EDSelected[2] = 1;
                                        RecLineBar[SelectedRecBar].EDSelected[3] = 1;
                                        RecLineBar[SelectedRecBar].EDSelected[4] = 1;
                                        if (SelectedRecShape != 0)
                                        {
                                            for (int add = 1; add < 5; add++)
                                            {
                                                RecShape[SelectedRecShape].EDSelected[add] = 1;
                                            }
                                        }
                                        if (SelectedTeeShape != 0)
                                        {
                                            TeeShape[SelectedTeeShape].Selected = 1;
                                            RecLineBar[SelectedRecBar1].EDSelected[1] = 1;
                                            RecLineBar[SelectedRecBar1].EDSelected[2] = 1;
                                            RecLineBar[SelectedRecBar1].EDSelected[3] = 1;
                                            RecLineBar[SelectedRecBar1].EDSelected[4] = 1;
                                            RecLineBar[SelectedRecBar2].EDSelected[1] = 1;
                                            RecLineBar[SelectedRecBar2].EDSelected[2] = 1;
                                            RecLineBar[SelectedRecBar2].EDSelected[3] = 1;
                                            RecLineBar[SelectedRecBar2].EDSelected[4] = 1;
                                        }
                                        if (SelectedFlangedWallShape != 0)
                                        {
                                            FlangedWallShape[SelectedFlangedWallShape].Selected = 1;
                                            RecLineBar[SelectedRecBar1].EDSelected[1] = 1;
                                            RecLineBar[SelectedRecBar1].EDSelected[2] = 1;
                                            RecLineBar[SelectedRecBar1].EDSelected[3] = 1;
                                            RecLineBar[SelectedRecBar1].EDSelected[4] = 1;
                                            RecLineBar[SelectedRecBar2].EDSelected[1] = 1;
                                            RecLineBar[SelectedRecBar2].EDSelected[2] = 1;
                                            RecLineBar[SelectedRecBar2].EDSelected[3] = 1;
                                            RecLineBar[SelectedRecBar2].EDSelected[4] = 1;
                                            RecLineBar[SelectedRecBar3].EDSelected[1] = 1;
                                            RecLineBar[SelectedRecBar3].EDSelected[2] = 1;
                                            RecLineBar[SelectedRecBar3].EDSelected[3] = 1;
                                            RecLineBar[SelectedRecBar3].EDSelected[4] = 1;
                                        }
                                    }
                                }
                                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                                {
                                    SelectedBar = i;
                                    SelecteLinedBar = Bar[i].InLine;
                                    SelectedType = Bar[i].Type;
                                    SelectedInED = Bar[i].InED;
                                    SelectedRecBar = Bar[i].InREC;
                                    SelectedCorner = Bar[i].Corner;
                                    SelectedRecShape = RecLineBar[SelectedRecBar].InRecShape;
                                    SelectedFlangedWallShape = RecLineBar[SelectedRecBar].InFlangedWallShape;
                                    ifselected = 0;
                                    RecLineBar[SelectedRecBar].EDSelected[1] = 1;
                                    RecLineBar[SelectedRecBar].EDSelected[2] = 1;
                                    RecLineBar[SelectedRecBar].EDSelected[3] = 1;
                                    RecLineBar[SelectedRecBar].EDSelected[4] = 1;
                                    if (SelectedRecShape != 0)
                                    {
                                        for (int add = 1; add < 5; add++)
                                        {
                                            RecShape[SelectedRecShape].EDSelected[add] = 1;
                                        }
                                    }
                                    if (SelectedTeeShape != 0)
                                    {
                                        TeeShape[SelectedTeeShape].Selected = 1;
                                        RecLineBar[SelectedRecBar1].EDSelected[1] = 1;
                                        RecLineBar[SelectedRecBar1].EDSelected[2] = 1;
                                        RecLineBar[SelectedRecBar1].EDSelected[3] = 1;
                                        RecLineBar[SelectedRecBar1].EDSelected[4] = 1;
                                        RecLineBar[SelectedRecBar2].EDSelected[1] = 1;
                                        RecLineBar[SelectedRecBar2].EDSelected[2] = 1;
                                        RecLineBar[SelectedRecBar2].EDSelected[3] = 1;
                                        RecLineBar[SelectedRecBar2].EDSelected[4] = 1;
                                    }
                                    if (SelectedFlangedWallShape != 0)
                                    {
                                        FlangedWallShape[SelectedFlangedWallShape].Selected = 1;
                                        RecLineBar[SelectedRecBar1].EDSelected[1] = 1;
                                        RecLineBar[SelectedRecBar1].EDSelected[2] = 1;
                                        RecLineBar[SelectedRecBar1].EDSelected[3] = 1;
                                        RecLineBar[SelectedRecBar1].EDSelected[4] = 1;
                                        RecLineBar[SelectedRecBar2].EDSelected[1] = 1;
                                        RecLineBar[SelectedRecBar2].EDSelected[2] = 1;
                                        RecLineBar[SelectedRecBar2].EDSelected[3] = 1;
                                        RecLineBar[SelectedRecBar2].EDSelected[4] = 1;
                                        RecLineBar[SelectedRecBar3].EDSelected[1] = 1;
                                        RecLineBar[SelectedRecBar3].EDSelected[2] = 1;
                                        RecLineBar[SelectedRecBar3].EDSelected[3] = 1;
                                        RecLineBar[SelectedRecBar3].EDSelected[4] = 1;
                                    }
                                    Render2d();
                                    if (RecLineBar[SelectedRecBar].InRecShape != 0) SelectedType = 5;

                                    if (RecLineBar[SelectedRecBar].InFlangedWallShape == 0 & RecLineBar[SelectedRecBar].InTeeShape == 0)
                                    {
                                        SectionBarsPropertyForm sectionBarsPropertyForm = new SectionBarsPropertyForm();
                                        sectionBarsPropertyForm.ShowDialog();
                                    }
                                    if (RecLineBar[SelectedRecBar].InFlangedWallShape != 0)
                                    {
                                        SectionWallForm sectionWallForm = new SectionWallForm();
                                        sectionWallForm.ShowDialog();
                                    }
                                    if (RecLineBar[SelectedRecBar].InTeeShape != 0)
                                    {
                                        SectionTeeForm sectionTeeForm = new SectionTeeForm();
                                        sectionTeeForm.ShowDialog();
                                    }
                                }
                                goto endloop1;
                            }
                            if (Bar[i].Type == 5)
                            {
                                ifselected = 1;
                                SelectedCircleLineBar = Bar[i].InCircle;
                                SelectedCircleShape = CircleLineBar[SelectedCircleLineBar].InCircleShape;
                                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                                {
                                    if (CircleLineBar[SelectedCircleLineBar].Selected == 1)
                                    {
                                        CircleLineBar[SelectedCircleLineBar].Selected = 0;
                                        CircleShape[SelectedCircleShape].Selected = 0;
                                    }
                                    else
                                    {
                                        CircleLineBar[SelectedCircleLineBar].Selected = 1;
                                        CircleShape[SelectedCircleShape].Selected = 1;
                                    }
                                }
                                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                                {
                                    SelectedType = 7;
                                    SelectedCircleLineBar = Bar[i].InCircle;
                                    CircleLineBar[SelectedCircleLineBar].Selected = 1;
                                    CircleShape[SelectedCircleShape].Selected = 1;
                                    ifselected = 0;
                                    Render2d();
                                    SectionBarsPropertyForm sectionBarsPropertyForm = new SectionBarsPropertyForm();
                                    sectionBarsPropertyForm.ShowDialog();
                                }
                                goto endloop1;
                            }
                        }
                    }
                    #endregion
                    # region//تحديد العناصر الخطية
                    for (int i = 1; i < LineBarsNumber + 1; i++)
                    {
                        int X1 = LineBar[i].X12D;
                        int Y1 = LineBar[i].Y12D;
                        int X2 = LineBar[i].X22D;
                        int Y2 = LineBar[i].Y22D;
                        int X = e.X;
                        int Y = e.Y;
                        DistanceCalc(X, Y, X1, Y1, X2, Y2);
                        if (distance < 7 * Zoom2d)
                        {
                            ifselected = 1;
                            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                            {
                                if (LineBar[i].Selected == 1)
                                {
                                    LineBar[i].Selected = 0;
                                }
                                else
                                {
                                    LineBar[i].Selected = 1;
                                }
                            }
                            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                            {
                                LineBar[i].Selected = 1;
                                SelecteLinedBar = i;
                                SelectedType = 2;
                                ifselected = 0;
                                Render2d();
                                SectionBarsPropertyForm sectionBarsPropertyForm = new SectionBarsPropertyForm();
                                sectionBarsPropertyForm.ShowDialog();
                            }
                            goto endloop1;
                        }
                    }
                    #endregion
                    # region//تحديد الشناغل
                    for (int i = 1; i < ClipsNumber + 1; i++)
                    {
                        int X1 = Clip[i].X12D;
                        int Y1 = Clip[i].Y12D;
                        int X2 = Clip[i].X22D;
                        int Y2 = Clip[i].Y22D;
                        int X = e.X;
                        int Y = e.Y;
                        DistanceCalc(X, Y, X1, Y1, X2, Y2);
                        if (distance < 7 * Zoom2d)
                        {
                            ifselected = 1;
                            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                            {
                                if (Clip[i].Selected == 1)
                                {
                                    Clip[i].Selected = 0;
                                }
                                else
                                {
                                    Clip[i].Selected = 1;
                                }
                            }
                            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                            {
                                Clip[i].Selected = 1;
                                SelectedClip = i;
                                ifselected = 0;
                                Render2d();
                                SectionRebarTieForm sectionRebarTieForm = new SectionRebarTieForm();
                                sectionRebarTieForm.ShowDialog();
                            }
                            goto endloop1;
                        }
                    }
                    #endregion
                    # region//تحديد تسليح المستطيلات
                    for (int i = 1; i < RecBarsNumber + 1; i++)
                    {
                        for (int j = 1; j < 5; j++)
                        {
                            {
                                int X1 = RecLineBar[i].EDX12D[j];
                                int Y1 = RecLineBar[i].EDY12D[j];
                                int X2 = RecLineBar[i].EDX22D[j];
                                int Y2 = RecLineBar[i].EDY22D[j];
                                int X = e.X;
                                int Y = e.Y;
                                DistanceCalc(X, Y, X1, Y1, X2, Y2);
                                if (distance < 7 * Zoom2d)
                                {
                                    ifselected = 1;
                                    SelectedRecShape = RecLineBar[i].InRecShape;
                                    SelectedFlangedWallShape = RecLineBar[i].InFlangedWallShape;
                                    SelectedTeeShape = RecLineBar[i].InTeeShape;
                                    SelectedRecBar = i;
                                    if (SelectedFlangedWallShape != 0)
                                    {
                                        SelectedRecBar1 = FlangedWallShape[SelectedFlangedWallShape].ApplyedToRecBars[1];
                                        SelectedRecBar2 = FlangedWallShape[SelectedFlangedWallShape].ApplyedToRecBars[2];
                                        SelectedRecBar3 = FlangedWallShape[SelectedFlangedWallShape].ApplyedToRecBars[3];
                                    }
                                    if (SelectedTeeShape != 0)
                                    {
                                        SelectedRecBar1 = TeeShape[SelectedTeeShape].ApplyedToRecBars[1];
                                        SelectedRecBar2 = TeeShape[SelectedTeeShape].ApplyedToRecBars[2];
                                    }
                                    if (EditType == 1 & e.Button == MouseButtons.Left)//////////////////////////تعديل
                                    {
                                        int XX1 = (X1 + X2) / 2;
                                        int YY1 = (Y1 + Y2) / 2 + 3;
                                        int XX2 = (X1 + X2) / 2;
                                        int YY2 = (Y1 + Y2) / 2 - 3;
                                        DistanceCalc(X, Y, XX1, YY1, XX2, YY2);
                                        AnyKindtoEditIs = "RecLineBar";
                                        if (distance < 7 * Zoom2d)
                                        {
                                            RecEdgeToEdit = j;
                                            ClickToEdit = 1;
                                        }
                                        DrawForEditRecLineBar();
                                        goto endloop2;
                                    }
                                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                                    {
                                        if (RecLineBar[i].EDSelected[1] == 1)
                                        {
                                            RecLineBar[i].EDSelected[1] = 0;
                                            RecLineBar[i].EDSelected[2] = 0;
                                            RecLineBar[i].EDSelected[3] = 0;
                                            RecLineBar[i].EDSelected[4] = 0;
                                            if (SelectedRecShape != 0)
                                            {
                                                for (int add = 1; add < 5; add++)
                                                {
                                                    RecShape[SelectedRecShape].EDSelected[add] = 0;
                                                }
                                            }
                                            if (SelectedTeeShape != 0)
                                            {
                                                TeeShape[SelectedTeeShape].Selected = 0;
                                                RecLineBar[SelectedRecBar1].EDSelected[1] = 0;
                                                RecLineBar[SelectedRecBar1].EDSelected[2] = 0;
                                                RecLineBar[SelectedRecBar1].EDSelected[3] = 0;
                                                RecLineBar[SelectedRecBar1].EDSelected[4] = 0;
                                                RecLineBar[SelectedRecBar2].EDSelected[1] = 0;
                                                RecLineBar[SelectedRecBar2].EDSelected[2] = 0;
                                                RecLineBar[SelectedRecBar2].EDSelected[3] = 0;
                                                RecLineBar[SelectedRecBar2].EDSelected[4] = 0;
                                            }
                                            if (SelectedFlangedWallShape != 0)
                                            {
                                                FlangedWallShape[SelectedFlangedWallShape].Selected = 0;
                                                RecLineBar[SelectedRecBar1].EDSelected[1] = 0;
                                                RecLineBar[SelectedRecBar1].EDSelected[2] = 0;
                                                RecLineBar[SelectedRecBar1].EDSelected[3] = 0;
                                                RecLineBar[SelectedRecBar1].EDSelected[4] = 0;
                                                RecLineBar[SelectedRecBar2].EDSelected[1] = 0;
                                                RecLineBar[SelectedRecBar2].EDSelected[2] = 0;
                                                RecLineBar[SelectedRecBar2].EDSelected[3] = 0;
                                                RecLineBar[SelectedRecBar2].EDSelected[4] = 0;
                                                RecLineBar[SelectedRecBar3].EDSelected[1] = 0;
                                                RecLineBar[SelectedRecBar3].EDSelected[2] = 0;
                                                RecLineBar[SelectedRecBar3].EDSelected[3] = 0;
                                                RecLineBar[SelectedRecBar3].EDSelected[4] = 0;
                                            }
                                        }
                                        else
                                        {
                                            RecLineBar[i].EDSelected[1] = 1;
                                            RecLineBar[i].EDSelected[2] = 1;
                                            RecLineBar[i].EDSelected[3] = 1;
                                            RecLineBar[i].EDSelected[4] = 1;
                                            if (SelectedRecShape != 0)
                                            {
                                                for (int add = 1; add < 5; add++)
                                                {
                                                    RecShape[SelectedRecShape].EDSelected[add] = 1;
                                                }
                                            }
                                            if (SelectedTeeShape != 0)
                                            {
                                                TeeShape[SelectedTeeShape].Selected = 1;
                                                RecLineBar[SelectedRecBar1].EDSelected[1] = 1;
                                                RecLineBar[SelectedRecBar1].EDSelected[2] = 1;
                                                RecLineBar[SelectedRecBar1].EDSelected[3] = 1;
                                                RecLineBar[SelectedRecBar1].EDSelected[4] = 1;
                                                RecLineBar[SelectedRecBar2].EDSelected[1] = 1;
                                                RecLineBar[SelectedRecBar2].EDSelected[2] = 1;
                                                RecLineBar[SelectedRecBar2].EDSelected[3] = 1;
                                                RecLineBar[SelectedRecBar2].EDSelected[4] = 1;
                                            }
                                            if (SelectedFlangedWallShape != 0)
                                            {
                                                FlangedWallShape[SelectedFlangedWallShape].Selected = 1;
                                                RecLineBar[SelectedRecBar1].EDSelected[1] = 1;
                                                RecLineBar[SelectedRecBar1].EDSelected[2] = 1;
                                                RecLineBar[SelectedRecBar1].EDSelected[3] = 1;
                                                RecLineBar[SelectedRecBar1].EDSelected[4] = 1;
                                                RecLineBar[SelectedRecBar2].EDSelected[1] = 1;
                                                RecLineBar[SelectedRecBar2].EDSelected[2] = 1;
                                                RecLineBar[SelectedRecBar2].EDSelected[3] = 1;
                                                RecLineBar[SelectedRecBar2].EDSelected[4] = 1;
                                                RecLineBar[SelectedRecBar3].EDSelected[1] = 1;
                                                RecLineBar[SelectedRecBar3].EDSelected[2] = 1;
                                                RecLineBar[SelectedRecBar3].EDSelected[3] = 1;
                                                RecLineBar[SelectedRecBar3].EDSelected[4] = 1;
                                            }
                                        }
                                    }
                                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                                    {
                                        RecLineBar[i].EDSelected[1] = 1;
                                        RecLineBar[i].EDSelected[2] = 1;
                                        RecLineBar[i].EDSelected[3] = 1;
                                        RecLineBar[i].EDSelected[4] = 1;
                                        if (SelectedRecShape != 0)
                                        {
                                            for (int add = 1; add < 5; add++)
                                            {
                                                RecShape[SelectedRecShape].EDSelected[add] = 1;
                                            }
                                        }
                                        if (SelectedTeeShape != 0)
                                        {
                                            TeeShape[SelectedTeeShape].Selected = 1;
                                            RecLineBar[SelectedRecBar1].EDSelected[1] = 1;
                                            RecLineBar[SelectedRecBar1].EDSelected[2] = 1;
                                            RecLineBar[SelectedRecBar1].EDSelected[3] = 1;
                                            RecLineBar[SelectedRecBar1].EDSelected[4] = 1;
                                            RecLineBar[SelectedRecBar2].EDSelected[1] = 1;
                                            RecLineBar[SelectedRecBar2].EDSelected[2] = 1;
                                            RecLineBar[SelectedRecBar2].EDSelected[3] = 1;
                                            RecLineBar[SelectedRecBar2].EDSelected[4] = 1;
                                        }
                                        if (SelectedFlangedWallShape != 0)
                                        {
                                            FlangedWallShape[SelectedFlangedWallShape].Selected = 1;
                                            RecLineBar[SelectedRecBar1].EDSelected[1] = 1;
                                            RecLineBar[SelectedRecBar1].EDSelected[2] = 1;
                                            RecLineBar[SelectedRecBar1].EDSelected[3] = 1;
                                            RecLineBar[SelectedRecBar1].EDSelected[4] = 1;
                                            RecLineBar[SelectedRecBar2].EDSelected[1] = 1;
                                            RecLineBar[SelectedRecBar2].EDSelected[2] = 1;
                                            RecLineBar[SelectedRecBar2].EDSelected[3] = 1;
                                            RecLineBar[SelectedRecBar2].EDSelected[4] = 1;
                                            RecLineBar[SelectedRecBar3].EDSelected[1] = 1;
                                            RecLineBar[SelectedRecBar3].EDSelected[2] = 1;
                                            RecLineBar[SelectedRecBar3].EDSelected[3] = 1;
                                            RecLineBar[SelectedRecBar3].EDSelected[4] = 1;
                                        }
                                        SelecteLinedBar = 0;
                                        ifselected = 0;
                                        SelectedType = 3;
                                        SelectedRecBar = i;
                                        Render2d();
                                        if (RecLineBar[i].InRecShape != 0)
                                        {
                                            SelectedType = 5;
                                            SelectedRecShape = RecLineBar[i].InRecShape;
                                        }

                                        if (SelectedFlangedWallShape == 0 & SelectedTeeShape == 0)
                                        {
                                            SectionBarsPropertyForm sectionBarsPropertyForm = new SectionBarsPropertyForm();
                                            sectionBarsPropertyForm.ShowDialog();
                                        }
                                        if (SelectedTeeShape != 0)
                                        {
                                            SectionTeeForm sectionTeeForm = new SectionTeeForm();
                                            sectionTeeForm.ShowDialog();
                                        }
                                        if (SelectedFlangedWallShape != 0)
                                        {
                                            SectionWallForm sectionWallForm = new SectionWallForm();
                                            sectionWallForm.ShowDialog();
                                        }
                                    }
                                    goto endloop1;
                                }
                            }
                        }
                    }
                    #endregion
                    # region//تحديد تسليح الدوائر
                    for (int i = 1; i < CircleBarsNumber + 1; i++)
                    {
                        for (int j = 1; j < 33; j++)
                        {
                            {
                                int X1 = 0;
                                int Y1 = 0;
                                int X2 = 0;
                                int Y2 = 0;
                                if (j < 32)
                                {
                                    X1 = CircleLineBar[i].PointX2D[j];
                                    Y1 = CircleLineBar[i].PointY2D[j];
                                    X2 = CircleLineBar[i].PointX2D[j + 1];
                                    Y2 = CircleLineBar[i].PointY2D[j + 1];
                                }
                                else
                                {
                                    X1 = CircleLineBar[i].PointX2D[1];
                                    Y1 = CircleLineBar[i].PointY2D[1];
                                    X2 = CircleLineBar[i].PointX2D[32];
                                    Y2 = CircleLineBar[i].PointY2D[32];
                                }
                                int X = e.X;
                                int Y = e.Y;
                                DistanceCalc(X, Y, X1, Y1, X2, Y2);
                                if (distance < 10 * Zoom2d)
                                {
                                    ifselected = 1;
                                    SelectedCircleLineBar = i;
                                    SelectedCircleShape = CircleLineBar[i].InCircleShape;
                                    #region////////تعديل
                                    if (EditType == 1 & e.Button == MouseButtons.Left)
                                    {
                                        AnyKindtoEditIs = "CircleLineBar";
                                        int Diameter = Convert.ToInt32(CircleLineBar[SelectedCircleLineBar].Diameter * Zoom2d / 2);
                                        int kkx1 = 0;
                                        int kky1 = 0;
                                        int kkx2 = 0;
                                        int kky2 = 0;
                                        for (int s = 1; s < 5; s++)
                                        {
                                            if (s == 1)
                                            {
                                                kkx1 = -1;
                                                kky1 = -1;
                                                kkx2 = 1;
                                                kky2 = -1;
                                            }
                                            if (s == 2)
                                            {
                                                kkx1 = 1;
                                                kky1 = -1;
                                                kkx2 = 1;
                                                kky2 = 1;
                                            }
                                            if (s == 3)
                                            {
                                                kkx1 = 1;
                                                kky1 = 1;
                                                kkx2 = -1;
                                                kky2 = 1;
                                            }
                                            if (s == 4)
                                            {
                                                kkx1 = -1;
                                                kky1 = 1;
                                                kkx2 = -1;
                                                kky2 = -1;
                                            }
                                            X1 = CircleLineBar[SelectedCircleLineBar].CenterX2D + kkx1 * Diameter;
                                            Y1 = CircleLineBar[SelectedCircleLineBar].CenterY2D + kky1 * Diameter;
                                            X2 = CircleLineBar[SelectedCircleLineBar].CenterX2D + kkx2 * Diameter;
                                            Y2 = CircleLineBar[SelectedCircleLineBar].CenterY2D + kky2 * Diameter;
                                            int XX1 = (X1 + X2) / 2;
                                            int YY1 = (Y1 + Y2) / 2 + 5;
                                            int XX2 = (X1 + X2) / 2;
                                            int YY2 = (Y1 + Y2) / 2 - 5;
                                            DistanceCalc(X, Y, XX1, YY1, XX2, YY2);
                                            RecEdgeToEdit = 0;
                                            if (distance < 10 * Zoom2d)
                                            {
                                                RecEdgeToEdit = j;
                                                ClickToEdit = 1;
                                                goto endchec;
                                            }
                                        }
                                    endchec: { };
                                        DrawForEditCircleLineBar();
                                        goto endloop2;
                                    }
                                    #endregion
                                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                                    {
                                        if (CircleLineBar[i].Selected == 1)
                                        {
                                            CircleLineBar[i].Selected = 0;
                                            CircleShape[SelectedCircleShape].Selected = 0;
                                        }
                                        else
                                        {
                                            CircleLineBar[i].Selected = 1;
                                            CircleShape[SelectedCircleShape].Selected = 1;
                                        }
                                    }
                                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                                    {
                                        CircleLineBar[i].Selected = 1;
                                        CircleShape[SelectedCircleShape].Selected = 1;
                                        SelectedCircleLineBar = i;
                                        SelectedCircleShape = CircleLineBar[i].InCircleShape;
                                        SelectedType = 6;
                                        Render2d();
                                        if (CircleLineBar[i].InCircleShape != 0)
                                        {
                                            SelectedType = 7;
                                            SelectedCircleShape = CircleLineBar[i].InCircleShape;
                                        }
                                        SectionBarsPropertyForm sectionBarsPropertyForm = new SectionBarsPropertyForm();
                                        sectionBarsPropertyForm.ShowDialog();
                                    }
                                    goto endloop1;
                                }
                            }
                        }
                    }
                    #endregion
                    # region//تحديد أشكال المستطيلات
                    for (int i = 1; i < RecShapeNumber + 1; i++)
                    {
                        for (int j = 1; j < 5; j++)
                        {
                            {
                                int X1 = RecShape[i].EDX12D[j];
                                int Y1 = RecShape[i].EDY12D[j];
                                int X2 = RecShape[i].EDX22D[j];
                                int Y2 = RecShape[i].EDY22D[j];
                                int X = e.X;
                                int Y = e.Y;
                                DistanceCalc(X, Y, X1, Y1, X2, Y2);
                                if (distance < 7 * Zoom2d)
                                {
                                    SelecteLinedBar = RecShape[i].ApplyedToRecBars;
                                    SelectedRecShape = i;
                                    if (EditType == 1 & e.Button == MouseButtons.Left)//////////////////////////تعديل
                                    {
                                        int XX1 = (X1 + X2) / 2;
                                        int YY1 = (Y1 + Y2) / 2 + 3;
                                        int XX2 = (X1 + X2) / 2;
                                        int YY2 = (Y1 + Y2) / 2 - 3;
                                        DistanceCalc(X, Y, XX1, YY1, XX2, YY2);
                                        AnyKindtoEditIs = "RecShape";
                                        if (distance < 7 * Zoom2d)
                                        {
                                            RecEdgeToEdit = j;
                                            ClickToEdit = 1;
                                        }
                                        DrawForEditRecShape();
                                        goto endloop2;
                                    }
                                    ifselected = 1;
                                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                                    {
                                        if (RecShape[i].EDSelected[1] == 1)
                                        {
                                            RecLineBar[SelecteLinedBar].EDSelected[1] = 0;
                                            RecLineBar[SelecteLinedBar].EDSelected[2] = 0;
                                            RecLineBar[SelecteLinedBar].EDSelected[3] = 0;
                                            RecLineBar[SelecteLinedBar].EDSelected[4] = 0;
                                            for (int add = 1; add < 5; add++)
                                            {
                                                RecShape[i].EDSelected[add] = 0;
                                            }
                                        }
                                        else
                                        {
                                            RecLineBar[SelecteLinedBar].EDSelected[1] = 1;
                                            RecLineBar[SelecteLinedBar].EDSelected[2] = 1;
                                            RecLineBar[SelecteLinedBar].EDSelected[3] = 1;
                                            RecLineBar[SelecteLinedBar].EDSelected[4] = 1;
                                            for (int add = 1; add < 5; add++)
                                            {
                                                RecShape[i].EDSelected[add] = 1;
                                            }
                                        }
                                    }
                                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                                    {
                                        RecLineBar[SelecteLinedBar].EDSelected[1] = 1;
                                        RecLineBar[SelecteLinedBar].EDSelected[2] = 1;
                                        RecLineBar[SelecteLinedBar].EDSelected[3] = 1;
                                        RecLineBar[SelecteLinedBar].EDSelected[4] = 1;
                                        for (int add = 1; add < 5; add++)
                                        {
                                            RecShape[i].EDSelected[add] = 1;
                                        }
                                        SelecteLinedBar = 0;
                                        SelectedBar = 0;
                                        SelectedInED = 0;
                                        SelectedCorner = 0;
                                        ifselected = 0;
                                        SelectedType = 5;
                                        SelectedRecShape = i;
                                        SelectedRecBar = RecShape[i].ApplyedToRecBars;
                                        Render2d();
                                        SectionBarsPropertyForm sectionBarsPropertyForm = new SectionBarsPropertyForm();
                                        sectionBarsPropertyForm.ShowDialog();
                                    }
                                    goto endloop1;
                                }
                            }
                        }
                    }
                    #endregion
                    # region//تحديد أشكال الدوائر
                    for (int i = 1; i < CircleShapeNumber + 1; i++)
                    {
                        for (int j = 1; j < 33; j++)
                        {
                            {
                                int X1 = 0;
                                int Y1 = 0;
                                int X2 = 0;
                                int Y2 = 0;
                                if (j < 32)
                                {
                                    X1 = CircleShape[i].PointX2D[j];
                                    Y1 = CircleShape[i].PointY2D[j];
                                    X2 = CircleShape[i].PointX2D[j + 1];
                                    Y2 = CircleShape[i].PointY2D[j + 1];
                                }
                                else
                                {
                                    X1 = CircleShape[i].PointX2D[1];
                                    Y1 = CircleShape[i].PointY2D[1];
                                    X2 = CircleShape[i].PointX2D[32];
                                    Y2 = CircleShape[i].PointY2D[32];
                                }
                                int X = e.X;
                                int Y = e.Y;
                                DistanceCalc(X, Y, X1, Y1, X2, Y2);
                                if (distance < 10 * Zoom2d)
                                {
                                    ifselected = 1;
                                    SelectedCircleShape = i;
                                    SelecteLinedBar = CircleShape[i].ApplyedToCircleBars;
                                    #region////////تعديل
                                    if (EditType == 1 & e.Button == MouseButtons.Left)
                                    {
                                        AnyKindtoEditIs = "CircleShape";
                                        int Diameter = Convert.ToInt32(CircleShape[SelectedCircleShape].Diameter * Zoom2d / 2);
                                        int kkx1 = 0;
                                        int kky1 = 0;
                                        int kkx2 = 0;
                                        int kky2 = 0;
                                        for (int s = 1; s < 5; s++)
                                        {
                                            if (s == 1)
                                            {
                                                kkx1 = -1;
                                                kky1 = -1;
                                                kkx2 = 1;
                                                kky2 = -1;
                                            }
                                            if (s == 2)
                                            {
                                                kkx1 = 1;
                                                kky1 = -1;
                                                kkx2 = 1;
                                                kky2 = 1;
                                            }
                                            if (s == 3)
                                            {
                                                kkx1 = 1;
                                                kky1 = 1;
                                                kkx2 = -1;
                                                kky2 = 1;
                                            }
                                            if (s == 4)
                                            {
                                                kkx1 = -1;
                                                kky1 = 1;
                                                kkx2 = -1;
                                                kky2 = -1;
                                            }
                                            X1 = CircleShape[SelectedCircleShape].CenterX2D + kkx1 * Diameter;
                                            Y1 = CircleShape[SelectedCircleShape].CenterY2D + kky1 * Diameter;
                                            X2 = CircleShape[SelectedCircleShape].CenterX2D + kkx2 * Diameter;
                                            Y2 = CircleShape[SelectedCircleShape].CenterY2D + kky2 * Diameter;
                                            int XX1 = (X1 + X2) / 2;
                                            int YY1 = (Y1 + Y2) / 2 + 5;
                                            int XX2 = (X1 + X2) / 2;
                                            int YY2 = (Y1 + Y2) / 2 - 5;
                                            DistanceCalc(X, Y, XX1, YY1, XX2, YY2);
                                            RecEdgeToEdit = 0;
                                            if (distance < 10 * Zoom2d)
                                            {
                                                RecEdgeToEdit = j;
                                                ClickToEdit = 1;
                                                goto endchec;
                                            }
                                        }
                                    endchec: { };
                                        DrawForEditCircleShape();
                                        goto endloop2;
                                    }
                                    #endregion
                                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                                    {
                                        if (CircleShape[i].Selected == 1)
                                        {
                                            CircleShape[i].Selected = 0;
                                            CircleLineBar[SelecteLinedBar].Selected = 0;
                                        }
                                        else
                                        {
                                            CircleShape[i].Selected = 1;
                                            CircleLineBar[SelecteLinedBar].Selected = 1;
                                        }
                                    }
                                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                                    {
                                        CircleShape[i].Selected = 1;
                                        CircleLineBar[SelecteLinedBar].Selected = 1;
                                        SelectedCircleShape = i;
                                        SelectedCircleLineBar = CircleShape[i].ApplyedToCircleBars;
                                        SelectedType = 7;
                                        Render2d();
                                        SectionBarsPropertyForm sectionBarsPropertyForm = new SectionBarsPropertyForm();
                                        sectionBarsPropertyForm.ShowDialog();
                                    }
                                    goto endloop1;
                                }
                            }
                        }
                    }
                    #endregion
                    # region//تحديد أشكال الجدران
                    for (int i = 1; i < FlangedWallShapeNumber + 1; i++)
                    {
                        int X = e.X;
                        int Y = e.Y;
                        if (EditType == 1 & e.Button == MouseButtons.Left)//////////////////////////تعديل
                        {
                            int X1 = 0;
                            int Y1 = 0;
                            int X2 = 0;
                            int Y2 = 0;
                            int XX1 = 0;
                            int YY1 = 0;
                            int XX2 = 0;
                            int YY2 = 0;
                            int maxX = FlangedWallShape[i].PointX2D[1];
                            int minY = FlangedWallShape[i].PointY2D[1];
                            if (FlangedWallShape[i].PointY2D[6] < minY) minY = FlangedWallShape[i].PointY2D[6];
                            int maxY = FlangedWallShape[i].PointY2D[7];
                            if (FlangedWallShape[i].PointY2D[12] > maxY) maxY = FlangedWallShape[i].PointY2D[12];
                            for (int s = 1; s < 5; s++)
                            {
                                if (s == 1)
                                {
                                    X1 = FlangedWallShape[i].PointX2D[1];////
                                    Y1 = minY;
                                    X2 = FlangedWallShape[i].PointX2D[6];
                                    Y2 = minY;
                                }
                                if (s == 2)
                                {
                                    X1 = FlangedWallShape[i].PointX2D[6];/////
                                    Y1 = minY;
                                    X2 = FlangedWallShape[i].PointX2D[6];
                                    Y2 = maxY;
                                }
                                if (s == 3)
                                {
                                    X1 = FlangedWallShape[i].PointX2D[6];//////
                                    Y1 = maxY;
                                    X2 = FlangedWallShape[i].PointX2D[12];
                                    Y2 = maxY;
                                }
                                if (s == 4)
                                {
                                    X1 = FlangedWallShape[i].PointX2D[12];//////
                                    Y1 = maxY;
                                    X2 = FlangedWallShape[i].PointX2D[12];
                                    Y2 = minY;
                                }
                                XX1 = (X1 + X2) / 2;
                                YY1 = (Y1 + Y2) / 2 + 3;
                                XX2 = (X1 + X2) / 2;
                                YY2 = (Y1 + Y2) / 2 - 3;
                                DistanceCalc(X, Y, XX1, YY1, XX2, YY2);
                                if (distance < 12 * Zoom2d)
                                {
                                    ifselected = 1;
                                    SelectedFlangedWallShape = i;
                                    SelecteLinedBar = FlangedWallShape[i].ApplyedToRecBars[1];
                                    SelecteLinedBar1 = FlangedWallShape[i].ApplyedToRecBars[1];
                                    SelecteLinedBar2 = FlangedWallShape[i].ApplyedToRecBars[2];
                                    SelecteLinedBar3 = FlangedWallShape[i].ApplyedToRecBars[3];
                                    RecEdgeToEdit = s;
                                    ClickToEdit = 1;
                                    AnyKindtoEditIs = "FlangedWallShape";
                                    DrawForEditFlangedWallShape();
                                    goto endloop2;
                                }
                            }
                        }
                        for (int j = 1; j < 13; j++)
                        {
                            {
                                int X1 = 0;
                                int Y1 = 0;
                                int X2 = 0;
                                int Y2 = 0;
                                if (j < 12)
                                {
                                    X1 = FlangedWallShape[i].PointX2D[j];
                                    Y1 = FlangedWallShape[i].PointY2D[j];
                                    X2 = FlangedWallShape[i].PointX2D[j + 1];
                                    Y2 = FlangedWallShape[i].PointY2D[j + 1];
                                }
                                else
                                {
                                    X1 = FlangedWallShape[i].PointX2D[1];
                                    Y1 = FlangedWallShape[i].PointY2D[1];
                                    X2 = FlangedWallShape[i].PointX2D[12];
                                    Y2 = FlangedWallShape[i].PointY2D[12];
                                }

                                DistanceCalc(X, Y, X1, Y1, X2, Y2);
                                if (distance < 12 * Zoom2d)
                                {
                                    ifselected = 1;
                                    SelectedFlangedWallShape = i;
                                    SelecteLinedBar = FlangedWallShape[i].ApplyedToRecBars[1];
                                    SelecteLinedBar1 = FlangedWallShape[i].ApplyedToRecBars[1];
                                    SelecteLinedBar2 = FlangedWallShape[i].ApplyedToRecBars[2];
                                    SelecteLinedBar3 = FlangedWallShape[i].ApplyedToRecBars[3];
                                    if (EditType == 1 & e.Button == MouseButtons.Left)//////////////////////////تعديل
                                    {
                                        AnyKindtoEditIs = "FlangedWallShape";
                                        DrawForEditFlangedWallShape();
                                        goto endloop2;
                                    }
                                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                                    {
                                        if (FlangedWallShape[i].Selected == 1)
                                        {
                                            FlangedWallShape[i].Selected = 0;
                                            RecLineBar[SelecteLinedBar1].EDSelected[1] = 0;
                                            RecLineBar[SelecteLinedBar1].EDSelected[2] = 0;
                                            RecLineBar[SelecteLinedBar1].EDSelected[3] = 0;
                                            RecLineBar[SelecteLinedBar1].EDSelected[4] = 0;
                                            RecLineBar[SelecteLinedBar2].EDSelected[1] = 0;
                                            RecLineBar[SelecteLinedBar2].EDSelected[2] = 0;
                                            RecLineBar[SelecteLinedBar2].EDSelected[3] = 0;
                                            RecLineBar[SelecteLinedBar2].EDSelected[4] = 0;
                                            RecLineBar[SelecteLinedBar3].EDSelected[1] = 0;
                                            RecLineBar[SelecteLinedBar3].EDSelected[2] = 0;
                                            RecLineBar[SelecteLinedBar3].EDSelected[3] = 0;
                                            RecLineBar[SelecteLinedBar3].EDSelected[4] = 0;
                                        }
                                        else
                                        {
                                            FlangedWallShape[i].Selected = 1;
                                            RecLineBar[SelecteLinedBar1].EDSelected[1] = 1;
                                            RecLineBar[SelecteLinedBar1].EDSelected[2] = 1;
                                            RecLineBar[SelecteLinedBar1].EDSelected[3] = 1;
                                            RecLineBar[SelecteLinedBar1].EDSelected[4] = 1;
                                            RecLineBar[SelecteLinedBar2].EDSelected[1] = 1;
                                            RecLineBar[SelecteLinedBar2].EDSelected[2] = 1;
                                            RecLineBar[SelecteLinedBar2].EDSelected[3] = 1;
                                            RecLineBar[SelecteLinedBar2].EDSelected[4] = 1;
                                            RecLineBar[SelecteLinedBar3].EDSelected[1] = 1;
                                            RecLineBar[SelecteLinedBar3].EDSelected[2] = 1;
                                            RecLineBar[SelecteLinedBar3].EDSelected[3] = 1;
                                            RecLineBar[SelecteLinedBar3].EDSelected[4] = 1;
                                        }
                                    }
                                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                                    {
                                        FlangedWallShape[i].Selected = 1;
                                        RecLineBar[SelecteLinedBar1].EDSelected[1] = 1;
                                        RecLineBar[SelecteLinedBar1].EDSelected[2] = 1;
                                        RecLineBar[SelecteLinedBar1].EDSelected[3] = 1;
                                        RecLineBar[SelecteLinedBar1].EDSelected[4] = 1;
                                        RecLineBar[SelecteLinedBar2].EDSelected[1] = 1;
                                        RecLineBar[SelecteLinedBar2].EDSelected[2] = 1;
                                        RecLineBar[SelecteLinedBar2].EDSelected[3] = 1;
                                        RecLineBar[SelecteLinedBar2].EDSelected[4] = 1;
                                        RecLineBar[SelecteLinedBar3].EDSelected[1] = 1;
                                        RecLineBar[SelecteLinedBar3].EDSelected[2] = 1;
                                        RecLineBar[SelecteLinedBar3].EDSelected[3] = 1;
                                        RecLineBar[SelecteLinedBar3].EDSelected[4] = 1;
                                        SelectedFlangedWallShape = i;
                                        SelectedRecBar = FlangedWallShape[i].ApplyedToRecBars[1];
                                        SelectedRecBar1 = FlangedWallShape[i].ApplyedToRecBars[1];
                                        SelectedRecBar2 = FlangedWallShape[i].ApplyedToRecBars[2];
                                        SelectedRecBar3 = FlangedWallShape[i].ApplyedToRecBars[3];
                                        SelectedType = 10;
                                        Render2d();
                                        SectionWallForm sectionWallForm = new SectionWallForm();
                                        sectionWallForm.ShowDialog();
                                    }
                                    goto endloop1;
                                }
                            }
                        }
                    }
                    #endregion
                    # region//تحديد أشكال تيه
                    for (int i = 1; i < TeeShapeNumber + 1; i++)
                    {
                        int X = e.X;
                        int Y = e.Y;
                        #region//////////////////////////تعديل
                        if (EditType == 1 & e.Button == MouseButtons.Left)
                        {
                            int X1 = 0;
                            int Y1 = 0;
                            int X2 = 0;
                            int Y2 = 0;
                            int XX1 = 0;
                            int YY1 = 0;
                            int XX2 = 0;
                            int YY2 = 0;
                            for (int s = 1; s < 5; s++)
                            {
                                if (s == 1)
                                {
                                    X1 = TeeShape[i].PointX2D[1];
                                    Y1 = TeeShape[i].PointY2D[1];
                                    X2 = TeeShape[i].PointX2D[2];
                                    Y2 = TeeShape[i].PointY2D[2];
                                }
                                if (s == 2)
                                {
                                    X1 = TeeShape[i].PointX2D[2];
                                    Y1 = TeeShape[i].PointY2D[2];
                                    X2 = TeeShape[i].PointX2D[2];
                                    Y2 = TeeShape[i].PointY2D[5];
                                }
                                if (s == 3)
                                {
                                    X1 = TeeShape[i].PointX2D[2];
                                    Y1 = TeeShape[i].PointY2D[5];
                                    X2 = TeeShape[i].PointX2D[1];
                                    Y2 = TeeShape[i].PointY2D[5];
                                }
                                if (s == 4)
                                {
                                    X1 = TeeShape[i].PointX2D[1];
                                    Y1 = TeeShape[i].PointY2D[5];
                                    X2 = TeeShape[i].PointX2D[1];
                                    Y2 = TeeShape[i].PointY2D[1];
                                }
                                XX1 = (X1 + X2) / 2;
                                YY1 = (Y1 + Y2) / 2 + 3;
                                XX2 = (X1 + X2) / 2;
                                YY2 = (Y1 + Y2) / 2 - 3;
                                DistanceCalc(X, Y, XX1, YY1, XX2, YY2);
                                if (distance < 12 * Zoom2d)
                                {
                                    ifselected = 1;
                                    SelectedFlangedWallShape = i;
                                    SelecteLinedBar = TeeShape[i].ApplyedToRecBars[1];
                                    SelecteLinedBar1 = TeeShape[i].ApplyedToRecBars[1];
                                    SelecteLinedBar2 = TeeShape[i].ApplyedToRecBars[2];
                                    RecEdgeToEdit = s;
                                    ClickToEdit = 1;
                                    AnyKindtoEditIs = "TeeShape";
                                    DrawForEditTeeShape();
                                    goto endloop2;
                                }
                            }
                        }
                        #endregion
                        for (int j = 1; j < 9; j++)
                        {
                            {
                                int X1 = 0;
                                int Y1 = 0;
                                int X2 = 0;
                                int Y2 = 0;
                                if (j < 8)
                                {
                                    X1 = TeeShape[i].PointX2D[j];
                                    Y1 = TeeShape[i].PointY2D[j];
                                    X2 = TeeShape[i].PointX2D[j + 1];
                                    Y2 = TeeShape[i].PointY2D[j + 1];
                                }
                                else
                                {
                                    X1 = TeeShape[i].PointX2D[1];
                                    Y1 = TeeShape[i].PointY2D[1];
                                    X2 = TeeShape[i].PointX2D[8];
                                    Y2 = TeeShape[i].PointY2D[8];
                                }

                                DistanceCalc(X, Y, X1, Y1, X2, Y2);
                                if (distance < 12 * Zoom2d)
                                {
                                    ifselected = 1;
                                    SelectedTeeShape = i;
                                    SelecteLinedBar = TeeShape[i].ApplyedToRecBars[1];
                                    SelecteLinedBar1 = TeeShape[i].ApplyedToRecBars[1];
                                    SelecteLinedBar2 = TeeShape[i].ApplyedToRecBars[2];
                                    if (EditType == 1 & e.Button == MouseButtons.Left)//////////////////////////تعديل
                                    {
                                        AnyKindtoEditIs = "TeeShape";
                                        DrawForEditTeeShape();
                                        goto endloop2;
                                    }
                                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                                    {
                                        if (TeeShape[i].Selected == 1)
                                        {
                                            TeeShape[i].Selected = 0;
                                            RecLineBar[SelecteLinedBar1].EDSelected[1] = 0;
                                            RecLineBar[SelecteLinedBar1].EDSelected[2] = 0;
                                            RecLineBar[SelecteLinedBar1].EDSelected[3] = 0;
                                            RecLineBar[SelecteLinedBar1].EDSelected[4] = 0;
                                            RecLineBar[SelecteLinedBar2].EDSelected[1] = 0;
                                            RecLineBar[SelecteLinedBar2].EDSelected[2] = 0;
                                            RecLineBar[SelecteLinedBar2].EDSelected[3] = 0;
                                            RecLineBar[SelecteLinedBar2].EDSelected[4] = 0;
                                        }
                                        else
                                        {
                                            TeeShape[i].Selected = 1;
                                            RecLineBar[SelecteLinedBar1].EDSelected[1] = 1;
                                            RecLineBar[SelecteLinedBar1].EDSelected[2] = 1;
                                            RecLineBar[SelecteLinedBar1].EDSelected[3] = 1;
                                            RecLineBar[SelecteLinedBar1].EDSelected[4] = 1;
                                            RecLineBar[SelecteLinedBar2].EDSelected[1] = 1;
                                            RecLineBar[SelecteLinedBar2].EDSelected[2] = 1;
                                            RecLineBar[SelecteLinedBar2].EDSelected[3] = 1;
                                            RecLineBar[SelecteLinedBar2].EDSelected[4] = 1;
                                        }
                                    }
                                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                                    {
                                        TeeShape[i].Selected = 1;
                                        RecLineBar[SelecteLinedBar1].EDSelected[1] = 1;
                                        RecLineBar[SelecteLinedBar1].EDSelected[2] = 1;
                                        RecLineBar[SelecteLinedBar1].EDSelected[3] = 1;
                                        RecLineBar[SelecteLinedBar1].EDSelected[4] = 1;
                                        RecLineBar[SelecteLinedBar2].EDSelected[1] = 1;
                                        RecLineBar[SelecteLinedBar2].EDSelected[2] = 1;
                                        RecLineBar[SelecteLinedBar2].EDSelected[3] = 1;
                                        RecLineBar[SelecteLinedBar2].EDSelected[4] = 1;
                                        SelectedFlangedWallShape = i;
                                        SelectedRecBar = TeeShape[i].ApplyedToRecBars[1];
                                        SelectedRecBar1 = TeeShape[i].ApplyedToRecBars[1];
                                        SelectedRecBar2 = TeeShape[i].ApplyedToRecBars[2];
                                        SelectedType = 70;
                                        Render2d();
                                        SectionTeeForm sectionTeeForm = new SectionTeeForm();
                                        sectionTeeForm.ShowDialog();
                                    }
                                    goto endloop1;
                                }
                            }
                        }
                    }
                    #endregion
                }
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    #region//رسم تسليح
                    #region////رسم قضيب
                    if (DrawType == 1)
                    {
                        // if (drowclick == 0)
                        {
                            int x1 = e.X;
                            int y1 = e.Y;
                            TempXReal[1] = Math.Round((x1 - startX2d) / (Zoom2d), 3);
                            TempYReal[1] = Math.Round((startY2d - y1) / (Zoom2d), 3);
                            if (Tahkik == 1)
                            {
                                TahkikXY[1] = 1;
                                x1 = TempX;
                                y1 = TempY;
                                TempXReal[1] = TempX12Real;
                                TempYReal[1] = TempY12Real;
                            }
                            LineMoveX1 = x1;
                            LineMoveY1 = y1;
                            LineMoveX2 = LineMoveX1;
                            LineMoveY2 = LineMoveY1;
                            //drowclick = 1;
                            BarsNumber = BarsNumber + 1;
                            Bars emp = new Bars();
                            emp.XR = Math.Round(TempXReal[1], 3);
                            emp.YR = Math.Round(TempYReal[1], 3);
                            emp.DR = 16;
                            emp.Selected = 0;
                            emp.InLine = 0;
                            emp.Type = 1;
                            emp.InED = 0;
                            emp.InREC = 0;
                            emp.Corner = 0;
                            emp.InCircle = 0;
                            Bar[BarsNumber] = emp;
                            Render2d();
                            MakeTempFiles();
                            //goto sdd;
                        }
                    }
                    #endregion
                    #region رسم خط قضبان
                    if (DrawType == 2)
                    {
                        if (drowclick == 0)
                        {
                            int x1 = e.X;
                            int y1 = e.Y;
                            TempXReal[1] = Math.Round((x1 - startX2d) / (Zoom2d), 3);
                            TempYReal[1] = Math.Round((startY2d - y1) / (Zoom2d), 3);
                            if (Tahkik == 1)
                            {
                                TahkikXY[1] = 1;
                                x1 = TempX;
                                y1 = TempY;
                                TempXReal[1] = TempX12Real;
                                TempYReal[1] = TempY12Real;
                            }
                            LineMoveX1 = x1;
                            LineMoveY1 = y1;
                            LineMoveX2 = LineMoveX1;
                            LineMoveY2 = LineMoveY1;
                            drowclick = 1;
                            LineMove2dVisible = 1;
                            goto sdd;
                        }
                        if (drowclick == 1)
                        {
                            int x2 = e.X;
                            int y2 = e.Y;
                            if (Tahkik == 1)
                            {
                                x2 = TempX;
                                y2 = TempY;
                                TahkikXY[2] = 1;
                                TempXReal[2] = TempX12Real;
                                TempYReal[2] = TempY12Real;
                            }
                            double theX = Math.Round((x2 - startX2d) / (Zoom2d), 3);
                            double theY = Math.Round((startY2d - y2) / (Zoom2d), 3);
                            if (TahkikXY[2] == 1)
                            {
                                theX = TempXReal[2];
                                theY = TempYReal[2];
                            }
                            if (TahkikXY[2] == 1)
                            {
                                TahkikXY[1] = 1;
                            }
                            else
                            {
                                TahkikXY[1] = 0;
                            }
                            TahkikXY[2] = 0;

                            double DX = theX - TempXReal[1];
                            double DY = theY - TempYReal[1];
                            double length = Math.Sqrt(Math.Pow(DX, 2) + Math.Pow(DY, 2));
                            int NBAR = 5;
                            int diameter = 25;
                            double DIS = Math.Round(length / (NBAR + 1), 3);

                            LineBarsNumber = LineBarsNumber + 1;
                            LineBars emp1 = new LineBars();
                            emp1.X1R = TempXReal[1];
                            emp1.Y1R = TempYReal[1];
                            emp1.X2R = theX;
                            emp1.Y2R = theY;
                            emp1.DR = diameter;
                            emp1.Selected = 0;
                            emp1.BarsNumbers = NBAR;
                            emp1.Distance = DIS;
                            emp1.HasEndBars = 0;
                            LineBar[LineBarsNumber] = emp1;

                            for (int i = 1; i < NBAR + 1; i++)
                            {
                                BarsNumber = BarsNumber + 1;
                                Bars emp = new Bars();
                                emp.XR = Math.Round(TempXReal[1] + (i * DIS) * DX / length, 3);
                                emp.YR = Math.Round(TempYReal[1] + (i * DIS) * DY / length, 3);
                                emp.DR = diameter;
                                emp.Selected = 0;
                                emp.InLine = LineBarsNumber;
                                emp.Type = 2;
                                emp.InED = 0;
                                emp.InREC = 0;
                                emp.Corner = 0;
                                emp.InCircle = 0;
                                Bar[BarsNumber] = emp;
                            }
                            TempXReal[1] = theX;
                            TempYReal[1] = theY;
                            DrawType = 0;
                            LineMove2dVisible = 0;
                            drowclick = 0;
                            Render2d();
                            MakeTempFiles();
                            toolStripButton1.Checked = true;
                        }
                    sdd: { }
                    }
                    #endregion
                    #region رسم شنغل
                    if (DrawType == 20)
                    {
                        if (drowclick == 0)
                        {
                            int x1 = e.X;
                            int y1 = e.Y;
                            TempXReal[1] = Math.Round((x1 - startX2d) / (Zoom2d), 3);
                            TempYReal[1] = Math.Round((startY2d - y1) / (Zoom2d), 3);
                            if (Tahkik == 1)
                            {
                                TahkikXY[1] = 1;
                                x1 = TempX;
                                y1 = TempY;
                                TempXReal[1] = TempX12Real;
                                TempYReal[1] = TempY12Real;
                            }
                            LineMoveX1 = x1;
                            LineMoveY1 = y1;
                            LineMoveX2 = LineMoveX1;
                            LineMoveY2 = LineMoveY1;
                            drowclick = 1;
                            LineMove2dVisible = 1;
                            goto sdd;
                        }
                        if (drowclick == 1)
                        {
                            int x2 = e.X;
                            int y2 = e.Y;
                            if (Tahkik == 1)
                            {
                                x2 = TempX;
                                y2 = TempY;
                                TahkikXY[2] = 1;
                                TempXReal[2] = TempX12Real;
                                TempYReal[2] = TempY12Real;
                            }
                            double theX = Math.Round((x2 - startX2d) / (Zoom2d), 3);
                            double theY = Math.Round((startY2d - y2) / (Zoom2d), 3);
                            if (TahkikXY[2] == 1)
                            {
                                theX = TempXReal[2];
                                theY = TempYReal[2];
                            }
                            if (TahkikXY[2] == 1)
                            {
                                TahkikXY[1] = 1;
                            }
                            else
                            {
                                TahkikXY[1] = 0;
                            }
                            TahkikXY[2] = 0;

                            double DX = theX - TempXReal[1];
                            double DY = theY - TempYReal[1];
                            double length = Math.Sqrt(Math.Pow(DX, 2) + Math.Pow(DY, 2));
                            int diameter = 8;
                            int diameter1 = 25;
                            int diameter2 = 25;
                            ClipsNumber = ClipsNumber + 1;
                            Clips emp1 = new Clips();
                            // emp1.X1R = TempXReal[1];
                            // emp1.Y1R = TempYReal[1];
                            // emp1.X2R = theX;
                            //  emp1.Y2R = theY;

                            float Diameter = diameter1;
                            float Diameter1 = Diameter / 2;
                            float sweepAngle = (float)Math.Round(Angulo(TempXReal[1], TempYReal[1], theX, theY), 2);
                            double angel1 = ((360 - sweepAngle)) * (Math.PI / 180);
                            double angel2 = (90 - (360 - sweepAngle)) * (Math.PI / 180);

                            emp1.X1R = TempXReal[1] + (diameter1 / 2) * Math.Sin(angel1);
                            emp1.Y1R = TempYReal[1] + (diameter1 / 2) * Math.Cos(angel1);
                            emp1.X2R = theX + (diameter2 / 2) * Math.Sin(angel1); ;
                            emp1.Y2R = theY + (diameter2 / 2) * Math.Cos(angel1);

                            emp1.DR = diameter;
                            emp1.DR1 = diameter1;
                            emp1.DR2 = diameter2;
                            emp1.Selected = 0;
                            emp1.Dir1 = 0;
                            emp1.Dir2 = 0;
                            Clip[ClipsNumber] = emp1;
                            TempXReal[1] = theX;
                            TempYReal[1] = theY;
                            DrawType = 0;
                            LineMove2dVisible = 0;
                            drowclick = 0;
                            Render2d();
                            MakeTempFiles();
                            toolStripButton1.Checked = true;
                        }
                    sdd: { }
                    }
                    #endregion
                    #region////رسم مستطيل
                    if (DrawType == 3 || DrawType == 30)
                    {
                        // if (drowclick == 0)
                        {
                            int x1 = e.X;
                            int y1 = e.Y;
                            TempXReal[1] = Math.Round((x1 - startX2d) / (Zoom2d), 3);
                            TempYReal[1] = Math.Round((startY2d - y1) / (Zoom2d), 3);
                            if (Tahkik == 1)
                            {
                                TahkikXY[1] = 1;
                                x1 = TempX;
                                y1 = TempY;
                                TempXReal[1] = TempX12Real;
                                TempYReal[1] = TempY12Real;
                            }
                            LineMoveX1 = x1;
                            LineMoveY1 = y1;
                            LineMoveX2 = LineMoveX1;
                            LineMoveY2 = LineMoveY1;
                            RecBarsNumber = RecBarsNumber + 1;
                            int LX = 600;
                            int LY = 600;
                            int diameter = 25;
                            int diameter1 = 12;
                            int EDBarN = 3;
                            RecLineBars emp = new RecLineBars();
                            emp.Type = 0;
                            if (DrawType == 30)
                            {
                                diameter = 0;
                                EDBarN = 0;
                                emp.Type = 1;
                            }
                            emp.Width = LX;
                            emp.Height = LY;
                            emp.TIEDR = diameter1;
                            emp.CenterX = Math.Round(TempXReal[1], 0);
                            emp.CenterY = Math.Round(TempYReal[1], 0);
                            emp.EDX1R[1] = Math.Round(TempXReal[1], 0) - LX / 2;
                            emp.EDY1R[1] = Math.Round(TempYReal[1], 0) + LY / 2;
                            emp.EDX2R[1] = Math.Round(TempXReal[1], 0) + LX / 2;
                            emp.EDY2R[1] = Math.Round(TempYReal[1], 0) + LY / 2;
                            emp.EDBarsNumbers[1] = EDBarN;
                            emp.EDDR[1] = diameter;
                            emp.EDSelected[1] = 0;
                            double DX = emp.EDX2R[1] - emp.EDX1R[1];
                            double DY = emp.EDY2R[1] - emp.EDY1R[1];
                            double length = Math.Sqrt(Math.Pow(DX, 2) + Math.Pow(DY, 2));
                            int NBAR = emp.EDBarsNumbers[1];

                            double DIS = Math.Round((length - 2 * diameter1) / (NBAR + 1), 3);
                            if (DrawType == 30) DIS = 1000000;
                            emp.EDDistance[1] = DIS;
                            if (DrawType == 3)
                            {
                                for (int i = 1; i < NBAR + 1; i++)
                                {
                                    BarsNumber = BarsNumber + 1;
                                    Bars emp1 = new Bars();
                                    emp1.XR = Math.Round(emp.EDX1R[1] + diameter1 + (i * DIS), 3);
                                    emp1.YR = Math.Round(emp.EDY1R[1] - diameter1 - (diameter / 2), 3);
                                    emp1.DR = diameter;
                                    emp1.Selected = 0;
                                    emp1.InLine = 0;
                                    emp1.Type = 3;
                                    emp1.InED = 1;
                                    emp1.InREC = RecBarsNumber;
                                    emp1.InCircle = 0;
                                    Bar[BarsNumber] = emp1;
                                }
                            }
                            emp.EDX1R[2] = emp.EDX2R[1];
                            emp.EDY1R[2] = emp.EDY2R[1];
                            emp.EDX2R[2] = Math.Round(TempXReal[1], 0) + LX / 2;
                            emp.EDY2R[2] = Math.Round(TempYReal[1], 0) - LY / 2;
                            emp.EDBarsNumbers[2] = EDBarN;
                            emp.EDDR[2] = diameter;
                            emp.EDSelected[2] = 0;
                            DX = emp.EDX2R[2] - emp.EDX1R[2];
                            DY = emp.EDY2R[2] - emp.EDY1R[2];
                            length = Math.Sqrt(Math.Pow(DX, 2) + Math.Pow(DY, 2));
                            NBAR = emp.EDBarsNumbers[2];
                            DIS = Math.Round((length - 2 * diameter1) / (NBAR + 1), 3);
                            if (DrawType == 30) DIS = 1000000;
                            emp.EDDistance[2] = DIS;
                            if (DrawType == 3)
                            {
                                for (int i = 1; i < NBAR + 1; i++)
                                {
                                    BarsNumber = BarsNumber + 1;
                                    Bars emp1 = new Bars();
                                    emp1.XR = Math.Round(emp.EDX1R[2] - diameter1 - (diameter / 2), 3);
                                    emp1.YR = Math.Round(emp.EDY1R[2] - (i * DIS) - diameter1, 3);
                                    emp1.DR = diameter;
                                    emp1.Selected = 0;
                                    emp1.InLine = 0;
                                    emp1.InED = 2;
                                    emp1.InREC = RecBarsNumber;
                                    emp1.Type = 3;
                                    emp1.Corner = 0;
                                    emp1.InCircle = 0;
                                    Bar[BarsNumber] = emp1;
                                }
                            }
                            emp.EDX1R[3] = emp.EDX2R[2];
                            emp.EDY1R[3] = emp.EDY2R[2];
                            emp.EDX2R[3] = Math.Round(TempXReal[1], 0) - LX / 2;
                            emp.EDY2R[3] = Math.Round(TempYReal[1], 0) - LY / 2;
                            emp.EDBarsNumbers[3] = EDBarN;
                            emp.EDDR[3] = diameter;
                            emp.EDSelected[3] = 0;
                            DX = emp.EDX2R[3] - emp.EDX1R[3];
                            DY = emp.EDY2R[3] - emp.EDY1R[3];
                            length = Math.Sqrt(Math.Pow(DX, 2) + Math.Pow(DY, 2));
                            NBAR = emp.EDBarsNumbers[3];
                            DIS = Math.Round((length - 2 * diameter1) / (NBAR + 1), 3);
                            if (DrawType == 30) DIS = 1000000;
                            emp.EDDistance[3] = DIS;
                            if (DrawType == 3)
                            {
                                for (int i = 1; i < NBAR + 1; i++)
                                {
                                    BarsNumber = BarsNumber + 1;
                                    Bars emp1 = new Bars();
                                    emp1.XR = Math.Round(emp.EDX1R[3] - diameter1 - (i * DIS), 3);
                                    emp1.YR = Math.Round(emp.EDY1R[3] + diameter1 + (diameter / 2), 3);
                                    emp1.DR = diameter;
                                    emp1.Selected = 0;
                                    emp1.InLine = 0;
                                    emp1.Type = 3;
                                    emp1.InED = 3;
                                    emp1.Corner = 0;
                                    emp1.InCircle = 0;
                                    emp1.InREC = RecBarsNumber;
                                    Bar[BarsNumber] = emp1;
                                }
                            }
                            emp.EDX1R[4] = emp.EDX2R[3];
                            emp.EDY1R[4] = emp.EDY2R[3];
                            emp.EDX2R[4] = emp.EDX1R[1];
                            emp.EDY2R[4] = emp.EDY1R[1];
                            emp.EDBarsNumbers[4] = EDBarN;
                            emp.EDDR[4] = diameter;
                            emp.EDSelected[4] = 0;
                            DX = emp.EDX2R[4] - emp.EDX1R[4];
                            DY = emp.EDY2R[4] - emp.EDY1R[4];
                            length = Math.Sqrt(Math.Pow(DX, 2) + Math.Pow(DY, 2));
                            NBAR = emp.EDBarsNumbers[4];
                            DIS = Math.Round((length - 2 * diameter1) / (NBAR + 1), 3);
                            if (DrawType == 30) DIS = 1000000;
                            emp.EDDistance[4] = DIS;
                            if (DrawType == 3)
                            {
                                for (int i = 1; i < NBAR + 1; i++)
                                {
                                    BarsNumber = BarsNumber + 1;
                                    Bars emp1 = new Bars();
                                    emp1.XR = Math.Round(emp.EDX1R[4] + diameter1 + (diameter / 2), 3);
                                    emp1.YR = Math.Round(emp.EDY1R[4] + diameter1 + (i * DIS), 3);
                                    emp1.DR = diameter;
                                    emp1.Selected = 0;
                                    emp1.InLine = 0;
                                    emp1.Type = 3;
                                    emp1.InED = 4;
                                    emp1.Corner = 0;
                                    emp1.InCircle = 0;
                                    emp1.InREC = RecBarsNumber;
                                    Bar[BarsNumber] = emp1;
                                }
                                for (int i = 1; i < 5; i++)
                                {
                                    BarsNumber = BarsNumber + 1;
                                    Bars emp1 = new Bars();
                                    emp1.XR = Math.Round(emp.EDX1R[i] + kx[i] * diameter1 + kx[i] * (diameter / 2), 3);
                                    emp1.YR = Math.Round(emp.EDY1R[i] + ky[i] * diameter1 + ky[i] * (diameter / 2), 3);
                                    emp1.DR = diameter;
                                    emp1.Selected = 0;
                                    emp1.InLine = 0;
                                    emp1.Type = 4;
                                    emp1.InED = 0;
                                    emp1.Corner = i;
                                    emp1.InREC = RecBarsNumber;
                                    emp1.InCircle = 0;
                                    Bar[BarsNumber] = emp1;
                                    emp.CORDR[i] = diameter;
                                }
                            }
                            emp.InRecShape = 0;
                            RecLineBar[RecBarsNumber] = emp;
                            DrawType = 0;
                            LineMove2dVisible = 0;
                            drowclick = 0;
                            Render2d();
                            MakeTempFiles();
                            toolStripButton1.Checked = true;
                        }
                    }
                    #endregion
                    #region////رسم دائرة
                    if (DrawType == 4)
                    {
                        int x1 = e.X;
                        int y1 = e.Y;
                        TempXReal[1] = Math.Round((x1 - startX2d) / (Zoom2d), 3);
                        TempYReal[1] = Math.Round((startY2d - y1) / (Zoom2d), 3);
                        if (Tahkik == 1)
                        {
                            TahkikXY[1] = 1;
                            x1 = TempX;
                            y1 = TempY;
                            TempXReal[1] = TempX12Real;
                            TempYReal[1] = TempY12Real;
                        }
                        LineMoveX1 = x1;
                        LineMoveY1 = y1;
                        LineMoveX2 = LineMoveX1;
                        LineMoveY2 = LineMoveY1;
                        CircleBarsNumber = CircleBarsNumber + 1;
                        int Diameter = 1000;
                        int DR = 25;
                        int TIEDR = 12;
                        int BarN = 8;
                        CircleLineBars emp = new CircleLineBars();
                        emp.BarsNumbers = BarN;
                        emp.Diameter = Diameter;
                        emp.DR = DR;
                        emp.TIEDR = TIEDR;
                        emp.CenterX = Math.Round(TempXReal[1], 3);
                        emp.CenterY = Math.Round(TempYReal[1], 3);
                        emp.Selected = 0;
                        int pointN = 32;
                        double zaweaa = 2 * Math.PI / pointN;
                        double Diameter1 = Diameter / 2;// +(DR / 2 + TIEDR);
                        for (int i = 1; i < pointN + 1; i++)
                        {
                            emp.PointXR[i] = Math.Round(TempXReal[1] + Math.Cos(i * zaweaa) * Diameter1, 3);
                            emp.PointYR[i] = Math.Round(TempYReal[1] + Math.Sin(i * zaweaa) * Diameter1, 3);
                        }
                        zaweaa = 2 * Math.PI / BarN;
                        Diameter1 = Diameter / 2 - (DR / 2 + TIEDR);
                        for (int i = 1; i < BarN + 1; i++)
                        {
                            BarsNumber = BarsNumber + 1;
                            Bars emp1 = new Bars();
                            emp1.XR = Math.Round(TempXReal[1] + Math.Cos(i * zaweaa) * Diameter1, 3);
                            emp1.YR = Math.Round(TempYReal[1] + Math.Sin(i * zaweaa) * Diameter1, 3);
                            emp1.DR = DR;
                            emp1.Selected = 0;
                            emp1.InLine = 0;
                            emp1.Type = 5;
                            emp1.InED = 0;
                            emp1.InREC = 0;
                            emp1.InCircle = CircleBarsNumber;
                            Bar[BarsNumber] = emp1;
                        }
                        emp.InCircleShape = 0;
                        CircleLineBar[CircleBarsNumber] = emp;
                        DrawType = 0;
                        LineMove2dVisible = 0;
                        drowclick = 0;
                        Render2d();
                        MakeTempFiles();
                        toolStripButton1.Checked = true;
                    }
                    #endregion
                    #endregion
                    #region////رسم شكل مستطيل
                    if (DrawType == 5)
                    {
                        int x1 = e.X;
                        int y1 = e.Y;
                        TempXReal[1] = Math.Round((x1 - startX2d) / (Zoom2d), 3);
                        TempYReal[1] = Math.Round((startY2d - y1) / (Zoom2d), 3);
                        if (Tahkik == 1)
                        {
                            TahkikXY[1] = 1;
                            x1 = TempX;
                            y1 = TempY;
                            TempXReal[1] = TempX12Real;
                            TempYReal[1] = TempY12Real;
                        }
                        LineMoveX1 = x1;
                        LineMoveY1 = y1;
                        LineMoveX2 = LineMoveX1;
                        LineMoveY2 = LineMoveY1;
                        RecShapeNumber = RecShapeNumber + 1;
                        int LX = 650;
                        int LY = 650;
                        if (WallType == 1)
                        {
                            LX = 3650;
                            LY = 300;
                        }
                        RecShapes emp = new RecShapes();
                        emp.Width = LX;
                        emp.Height = LY;
                        //emp.IsWall = WallType;
                        emp.CenterX = Math.Round(TempXReal[1], 3);
                        emp.CenterY = Math.Round(TempYReal[1], 3);
                        emp.EDX1R[1] = Math.Round(TempXReal[1], 3) - LX / 2;
                        emp.EDY1R[1] = Math.Round(TempYReal[1], 3) + LY / 2;
                        emp.EDX2R[1] = Math.Round(TempXReal[1], 3) + LX / 2;
                        emp.EDY2R[1] = Math.Round(TempYReal[1], 3) + LY / 2;
                        emp.EDSelected[1] = 0;
                        emp.EDX1R[2] = emp.EDX2R[1];
                        emp.EDY1R[2] = emp.EDY2R[1];
                        emp.EDX2R[2] = Math.Round(TempXReal[1], 3) + LX / 2;
                        emp.EDY2R[2] = Math.Round(TempYReal[1], 3) - LY / 2;
                        emp.EDSelected[2] = 0;
                        emp.EDX1R[3] = emp.EDX2R[2];
                        emp.EDY1R[3] = emp.EDY2R[2];
                        emp.EDX2R[3] = Math.Round(TempXReal[1], 3) - LX / 2;
                        emp.EDY2R[3] = Math.Round(TempYReal[1], 3) - LY / 2;
                        emp.EDSelected[3] = 0;
                        emp.EDX1R[4] = emp.EDX2R[3];
                        emp.EDY1R[4] = emp.EDY2R[3];
                        emp.EDX2R[4] = emp.EDX1R[1];
                        emp.EDY2R[4] = emp.EDY1R[1];
                        emp.EDSelected[4] = 0;
                        emp.ApplyedToRecBars = 0;
                        emp.HasReinforcment = 0;
                        emp.EDCoverR[1] = 40;
                        emp.EDCoverR[2] = 40;
                        emp.EDCoverR[3] = 40;
                        emp.EDCoverR[4] = 40;
                        RecShape[RecShapeNumber] = emp;
                        DrawType = 0;
                        LineMove2dVisible = 0;
                        drowclick = 0;
                        Render2d();
                        MakeTempFiles();
                        toolStripButton1.Checked = true;
                    }
                    #endregion
                    #region////رسم شكل دائرة
                    if (DrawType == 6)
                    {
                        int x1 = e.X;
                        int y1 = e.Y;
                        TempXReal[1] = Math.Round((x1 - startX2d) / (Zoom2d), 3);
                        TempYReal[1] = Math.Round((startY2d - y1) / (Zoom2d), 3);
                        if (Tahkik == 1)
                        {
                            TahkikXY[1] = 1;
                            x1 = TempX;
                            y1 = TempY;
                            TempXReal[1] = TempX12Real;
                            TempYReal[1] = TempY12Real;
                        }
                        LineMoveX1 = x1;
                        LineMoveY1 = y1;
                        LineMoveX2 = LineMoveX1;
                        LineMoveY2 = LineMoveY1;
                        CircleShapeNumber = CircleShapeNumber + 1;
                        int Diameter = 1000;
                        int CoverR = 40;
                        CircleShapes emp = new CircleShapes();
                        emp.Diameter = Diameter;
                        emp.CoverR = CoverR;
                        emp.CenterX = Math.Round(TempXReal[1], 3);
                        emp.CenterY = Math.Round(TempYReal[1], 3);
                        emp.Selected = 0;
                        int pointN = 32;
                        double zaweaa = 2 * Math.PI / pointN;
                        double Diameter1 = Diameter / 2;
                        for (int i = 1; i < pointN + 1; i++)
                        {
                            emp.PointXR[i] = Math.Round(TempXReal[1] + Math.Cos(i * zaweaa) * Diameter1, 3);
                            emp.PointYR[i] = Math.Round(TempYReal[1] + Math.Sin(i * zaweaa) * Diameter1, 3);
                        }
                        emp.ApplyedToCircleBars = 0;
                        emp.HasReinforcment = 0;
                        CircleShape[CircleShapeNumber] = emp;
                        DrawType = 0;
                        LineMove2dVisible = 0;
                        drowclick = 0;
                        Render2d();
                        MakeTempFiles();
                        toolStripButton1.Checked = true;
                    }
                    #endregion
                    #region////رسم شكل جدار
                    if (DrawType == 10)
                    {
                        int x1 = e.X;
                        int y1 = e.Y;
                        TempXReal[1] = Math.Round((x1 - startX2d) / (Zoom2d), 3);
                        TempYReal[1] = Math.Round((startY2d - y1) / (Zoom2d), 3);
                        if (Tahkik == 1)
                        {
                            TahkikXY[1] = 1;
                            x1 = TempX;
                            y1 = TempY;
                            TempXReal[1] = TempX12Real;
                            TempYReal[1] = TempY12Real;
                        }
                        LineMoveX1 = x1;
                        LineMoveY1 = y1;
                        LineMoveX2 = LineMoveX1;
                        LineMoveY2 = LineMoveY1;
                        FlangedWallShapeNumber = FlangedWallShapeNumber + 1;

                        double CenterX = Math.Round(TempXReal[1], 3);
                        double CenterY = Math.Round(TempYReal[1], 3);
                        double Length = 3650;
                        double StemWidth = 300;
                        double LeftFlangWidth = 600;
                        double LeftFlangLength = 600;
                        double RightFlangWidth = 600;
                        double RightFlangLength = 600;
                        double LeftFlangEccen = 0;
                        double RightFlangEccen = 0;

                        double ED1CoverR = 40;
                        double ED2CoverR = 40;
                        double ED3CoverR = 40;
                        double ED4CoverR = 40;
                        double ED1LCoverR = 40;
                        double ED2LCoverR = 40;
                        double ED3LCoverR = 40;
                        double ED4LCoverR = 40;
                        double ED1RCoverR = 40;
                        double ED2RCoverR = 40;
                        double ED3RCoverR = 40;
                        double ED4RCoverR = 40;

                        double[] PointXReal = new double[13];
                        double[] PointYReal = new double[13];
                        FlangedWalls emp = new FlangedWalls();
                        emp.StemSurrounded=0;
                        emp.LeftFlangSurrounded=1;
                        emp.RightFlangSurrounded=1;
                        emp.ED1CoverR = ED1CoverR;
                        emp.ED2CoverR = ED2CoverR;
                        emp.ED3CoverR = ED3CoverR;
                        emp.ED4CoverR = ED4CoverR;
                        emp.ED1LCoverR = ED1LCoverR;
                        emp.ED2LCoverR = ED2LCoverR;
                        emp.ED3LCoverR = ED3LCoverR;
                        emp.ED4LCoverR = ED4LCoverR;
                        emp.ED1RCoverR = ED1RCoverR;
                        emp.ED2RCoverR = ED2RCoverR;
                        emp.ED3RCoverR = ED3RCoverR;
                        emp.ED4RCoverR = ED4RCoverR;
                        emp.HasReinforcment = 0;
                        emp.CenterX = CenterX;
                        emp.CenterY = CenterY;
                        emp.Length = Length;
                        emp.StemWidth = StemWidth;
                        emp.LeftFlangWidth = LeftFlangWidth;
                        emp.LeftFlangLength = LeftFlangLength;
                        emp.RightFlangWidth = RightFlangWidth;
                        emp.RightFlangLength = RightFlangLength;
                        emp.LeftFlangEccen = LeftFlangEccen;
                        emp.RightFlangEccen = RightFlangEccen;
                        emp.Selected = 0;
                        emp.PointXReal[1] = CenterX - Length / 2;
                        emp.PointXReal[2] = CenterX - Length / 2 + LeftFlangLength;
                        emp.PointXReal[3] = CenterX - Length / 2 + LeftFlangLength;
                        emp.PointXReal[4] = CenterX + Length / 2 - RightFlangLength;
                        emp.PointXReal[5] = CenterX + Length / 2 - RightFlangLength;
                        emp.PointXReal[6] = CenterX + Length / 2;
                        emp.PointXReal[7] = CenterX + Length / 2;
                        emp.PointXReal[8] = CenterX + Length / 2 - RightFlangLength;
                        emp.PointXReal[9] = CenterX + Length / 2 - RightFlangLength;
                        emp.PointXReal[10] = CenterX - Length / 2 + LeftFlangLength;
                        emp.PointXReal[11] = CenterX - Length / 2 + LeftFlangLength;
                        emp.PointXReal[12] = CenterX - Length / 2;

                        emp.PointYReal[1] = CenterY + LeftFlangWidth / 2;
                        emp.PointYReal[2] = CenterY + LeftFlangWidth / 2;
                        emp.PointYReal[3] = CenterY + StemWidth / 2;
                        emp.PointYReal[4] = CenterY + StemWidth / 2;
                        emp.PointYReal[5] = CenterY + RightFlangWidth / 2;
                        emp.PointYReal[6] = CenterY + RightFlangWidth / 2;
                        emp.PointYReal[7] = CenterY - RightFlangWidth / 2;
                        emp.PointYReal[8] = CenterY - RightFlangWidth / 2;
                        emp.PointYReal[9] = CenterY - StemWidth / 2;
                        emp.PointYReal[10] = CenterY - StemWidth / 2;
                        emp.PointYReal[11] = CenterY - LeftFlangWidth / 2;
                        emp.PointYReal[12] = CenterY - LeftFlangWidth / 2;

                        FlangedWallShape[FlangedWallShapeNumber] = emp;
                        DrawType = 0;
                        LineMove2dVisible = 0;
                        drowclick = 0;
                        Render2d();
                        MakeTempFiles();
                        toolStripButton1.Checked = true;
                    }
                    #endregion
                    #region////رسم تيه
                    if (DrawType == 7)
                    {
                        int x1 = e.X;
                        int y1 = e.Y;
                        TempXReal[1] = Math.Round((x1 - startX2d) / (Zoom2d), 3);
                        TempYReal[1] = Math.Round((startY2d - y1) / (Zoom2d), 3);
                        if (Tahkik == 1)
                        {
                            TahkikXY[1] = 1;
                            x1 = TempX;
                            y1 = TempY;
                            TempXReal[1] = TempX12Real;
                            TempYReal[1] = TempY12Real;
                        }
                        LineMoveX1 = x1;
                        LineMoveY1 = y1;
                        LineMoveX2 = LineMoveX1;
                        LineMoveY2 = LineMoveY1;
                        TeeShapeNumber = TeeShapeNumber + 1;

                        double CenterX = Math.Round(TempXReal[1], 3);
                        double CenterY = Math.Round(TempYReal[1], 3);
                        double Height = 750;
                        double WebThickness = 200;
                        double FlangWidth = 750;
                        double FlangThickness = 200;
  
                        double ED1CoverR = 25;
                        double ED2CoverR = 25;
                        double ED3CoverR = 25;
                        double ED4CoverR = 25;
                        double ED1cCoverR = 25;
                        double ED2cCoverR = 25;
                        double ED3cCoverR = 25;
                        double ED4cCoverR = 25;

                        double[] PointXReal = new double[13];
                        double[] PointYReal = new double[13];
                        TeeShapes emp = new TeeShapes();
                        emp.ED1CoverR = ED1CoverR;
                        emp.ED2CoverR = ED2CoverR;
                        emp.ED3CoverR = ED3CoverR;
                        emp.ED4CoverR = ED4CoverR;
                        emp.ED1cCoverR = ED1cCoverR;
                        emp.ED2cCoverR = ED2cCoverR;
                        emp.ED3cCoverR = ED3cCoverR;
                        emp.ED4cCoverR = ED4cCoverR;
                        emp.HasReinforcment = 0;
                        emp.CenterX = CenterX;
                        emp.CenterY = CenterY;
                        emp.Height = Height;
                        emp.WebThickness = WebThickness;
                        emp.FlangWidth = FlangWidth;
                        emp.FlangThickness = FlangThickness;
                        emp.Selected = 0;
                        emp.PointXReal[1] = CenterX - FlangWidth / 2;
                        emp.PointXReal[2] = CenterX + FlangWidth / 2;
                        emp.PointXReal[3] = CenterX + FlangWidth / 2;
                        emp.PointXReal[4] = CenterX + WebThickness / 2;
                        emp.PointXReal[5] = CenterX + WebThickness / 2;
                        emp.PointXReal[6] = CenterX - WebThickness / 2;
                        emp.PointXReal[7] = CenterX - WebThickness / 2;
                        emp.PointXReal[8] = CenterX - FlangWidth / 2;
                        double WebHeight = (Height - FlangThickness);
                        double aria1 = FlangWidth * FlangThickness;
                        double aria2 = WebHeight * WebThickness;
                        double aria = aria1 + aria2;
                        double way = ((aria1 * FlangThickness / 2) + aria2 * (FlangThickness + (WebHeight / 2))) / aria;
                        double way1 = way - FlangThickness;
                        double way2 = Height - way; 
                        emp.PointYReal[1] = CenterY + way;
                        emp.PointYReal[2] = CenterY + way;
                        emp.PointYReal[3] = CenterY + way1;
                        emp.PointYReal[4] = CenterY + way1;
                        emp.PointYReal[5] = CenterY - way2;
                        emp.PointYReal[6] = CenterY - way2;
                        emp.PointYReal[7] = CenterY + way1;
                        emp.PointYReal[8] = CenterY + way1;
                        TeeShape[TeeShapeNumber] = emp;
                        DrawType = 0;
                        LineMove2dVisible = 0;
                        drowclick = 0;
                        Render2d();
                        MakeTempFiles();
                        toolStripButton1.Checked = true;
                    }
                    #endregion             
                }
            }
        endloop1: { };
            if (ifselected == 1) Render2d();
        endloop2: { };
        }
        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            if (RecEdgeToEdit != 0)
            {
                if (AnyKindtoEditIs == "RecShape") EditRecShape();
                if (AnyKindtoEditIs == "RecLineBar") EditRecLineBar();
                if (AnyKindtoEditIs == "FlangedWallShape") EditFlangedWallShape();
                if (AnyKindtoEditIs == "TeeShape") EditTeeShape();
                if (RecEdgeToEdit == 7 || RecEdgeToEdit == 8 || RecEdgeToEdit == 9 || RecEdgeToEdit == 15 || RecEdgeToEdit == 16 || RecEdgeToEdit == 17 || RecEdgeToEdit == 23 || RecEdgeToEdit == 25 || RecEdgeToEdit == 25 || RecEdgeToEdit == 31 || RecEdgeToEdit == 32 || RecEdgeToEdit == 1)
                {
                    if (AnyKindtoEditIs == "CircleShape") EditCircleShape();
                    if (AnyKindtoEditIs == "CircleLineBar") EditCircleLineBar();
                }
            }
            #region//تحديد العناصر مع مربع التحديد
            if (MouseButtonsLeft == 1 & toolStripButton1.Checked == true)
            {
                MouseButtonsLeft = 0;
                int thecase = 0;
                if (e.X >= xmove1) thecase = 1;//تحديد العناصر الواقعة ضمن المربع تماما
                if (e.X < xmove1) thecase = 2;//تحديد العناصر المتقاطعة 
                int MaxX = 0;
                int MaxY = 0;
                int MinX = 0;
                int MinY = 0;
                MaxX = xmove1;
                MinX = xmove1;
                if (e.X > MaxX) MaxX = e.X;
                if (e.X < MinX) MinX = e.X;
                MaxY = ymove1;
                MinY = ymove1;
                if (e.Y > MaxY) MaxY = e.Y;
                if (e.Y < MinY) MinY = e.Y;
                int ifselected = 0;
                # region //تحديد القضبان
                for (int i = 1; i < BarsNumber + 1; i++)
                {
                    if (Bar[i].Type == 1)
                    {
                        {
                            {
                                int Tah1 = 0;
                                int Tah2 = 0;
                                int X3 = Bar[i].X2D;
                                int Y3 = Bar[i].Y2D;
                                int X4 = Bar[i].X2D;
                                int Y4 = Bar[i].Y2D;
                                if (X3 >= MinX & X3 <= MaxX & Y3 >= MinY & Y3 <= MaxY) Tah1 = 1;
                                if (X4 >= MinX & X4 <= MaxX & Y4 >= MinY & Y4 <= MaxY) Tah2 = 1;
                                if (thecase == 1)
                                {
                                    if (Tah1 == 1 & Tah2 == 1)
                                    {
                                        ifselected = 1;
                                        Bar[i].Selected = 1;
                                    }
                                }
                                if (thecase == 2)
                                {
                                    if (Tah1 == 1 || Tah2 == 1)
                                    {
                                        ifselected = 1;
                                        Bar[i].Selected = 1;
                                        goto end100;
                                    }
                                }
                            end100: { }
                            }
                        }
                    }
                }
                #endregion
                # region //تحديد خطوط القضبان
                for (int i = 1; i < LineBarsNumber + 1; i++)
                {
                    {
                        {
                            int Tah1 = 0;
                            int Tah2 = 0;
                            int X3 = LineBar[i].X12D;
                            int Y3 = LineBar[i].Y12D;
                            int X4 = LineBar[i].X22D;
                            int Y4 = LineBar[i].Y22D;
                            if (X3 >= MinX & X3 <= MaxX & Y3 >= MinY & Y3 <= MaxY) Tah1 = 1;
                            if (X4 >= MinX & X4 <= MaxX & Y4 >= MinY & Y4 <= MaxY) Tah2 = 1;
                            if (thecase == 1)
                            {
                                if (Tah1 == 1 & Tah2 == 1)
                                {
                                    ifselected = 1;
                                    LineBar[i].Selected = 1;
                                }
                            }
                            if (thecase == 2)
                            {
                                if (Tah1 == 1 || Tah2 == 1)
                                {
                                    ifselected = 1;
                                    LineBar[i].Selected = 1;
                                    goto end100;
                                }
                                INTERSECTION = 0;
                                int X1 = MinX;
                                int Y1 = MinY;
                                int X2 = MaxX;
                                int Y2 = MinY;
                                checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                                if (INTERSECTION == 1)
                                {
                                    ifselected = 1;
                                    LineBar[i].Selected = 1;
                                    goto end100;
                                }
                                X1 = MinX;
                                Y1 = MinY;
                                X2 = MinX;
                                Y2 = MaxY;
                                checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                                if (INTERSECTION == 1)
                                {
                                    ifselected = 1;
                                    LineBar[i].Selected = 1;
                                    goto end100;
                                }
                                X1 = MinX;
                                Y1 = MaxY;
                                X2 = MaxX;
                                Y2 = MaxY;
                                checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                                if (INTERSECTION == 1)
                                {
                                    ifselected = 1;
                                    LineBar[i].Selected = 1;
                                    goto end100;
                                }
                                X1 = MaxX;
                                Y1 = MaxY;
                                X2 = MaxX;
                                Y2 = MinY;
                                checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                                if (INTERSECTION == 1)
                                {
                                    ifselected = 1;
                                    LineBar[i].Selected = 1;
                                    goto end100;
                                }
                            }
                        end100: { }
                        }
                    }
                }
                #endregion
                # region //تحديد الشناغل
                for (int i = 1; i < ClipsNumber + 1; i++)
                {
                    int Tah1 = 0;
                    int Tah2 = 0;
                    int X3 = Clip[i].X12D;
                    int Y3 = Clip[i].Y12D;
                    int X4 = Clip[i].X22D;
                    int Y4 = Clip[i].Y22D;
                    if (X3 >= MinX & X3 <= MaxX & Y3 >= MinY & Y3 <= MaxY) Tah1 = 1;
                    if (X4 >= MinX & X4 <= MaxX & Y4 >= MinY & Y4 <= MaxY) Tah2 = 1;
                    if (thecase == 1)
                    {
                        if (Tah1 == 1 & Tah2 == 1)
                        {
                            ifselected = 1;
                            Clip[i].Selected = 1;
                        }
                    }
                    if (thecase == 2)
                    {
                        if (Tah1 == 1 || Tah2 == 1)
                        {
                            ifselected = 1;
                            Clip[i].Selected = 1;
                            goto end100;
                        }
                        INTERSECTION = 0;
                        int X1 = MinX;
                        int Y1 = MinY;
                        int X2 = MaxX;
                        int Y2 = MinY;
                        checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                        if (INTERSECTION == 1)
                        {
                            ifselected = 1;
                            Clip[i].Selected = 1;
                            goto end100;
                        }
                        X1 = MinX;
                        Y1 = MinY;
                        X2 = MinX;
                        Y2 = MaxY;
                        checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                        if (INTERSECTION == 1)
                        {
                            ifselected = 1;
                            Clip[i].Selected = 1;
                            goto end100;
                        }
                        X1 = MinX;
                        Y1 = MaxY;
                        X2 = MaxX;
                        Y2 = MaxY;
                        checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                        if (INTERSECTION == 1)
                        {
                            ifselected = 1;
                            Clip[i].Selected = 1;
                            goto end100;
                        }
                        X1 = MaxX;
                        Y1 = MaxY;
                        X2 = MaxX;
                        Y2 = MinY;
                        checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                        if (INTERSECTION == 1)
                        {
                            ifselected = 1;
                            Clip[i].Selected = 1;
                            goto end100;
                        }
                    }
                end100: { }
                }
                #endregion
                # region //تحديد خطوط تسليح المستطيلات
                for (int i = 1; i < RecBarsNumber + 1; i++)
                {
                    int X3 = 0;
                    int Y3 = 0;
                    int X4 = 0;
                    int Y4 = 0;
                    for (int j = 1; j < 5; j++)
                    {
                        X3 = RecLineBar[i].EDX12D[j];
                        Y3 = RecLineBar[i].EDY12D[j];
                        X4 = RecLineBar[i].EDX22D[j];
                        Y4 = RecLineBar[i].EDY22D[j];
                        int Tah1 = 0;
                        int Tah2 = 0;
                        if (X3 >= MinX & X3 <= MaxX & Y3 >= MinY & Y3 <= MaxY) Tah1 = 1;
                        if (X4 >= MinX & X4 <= MaxX & Y4 >= MinY & Y4 <= MaxY) Tah2 = 1;
                        if (thecase == 1)
                        {
                            if (Tah1 == 1 & Tah2 == 1)
                            {
                                ifselected = 1;
                                RecLineBar[i].EDSelected[1] = 1;
                                RecLineBar[i].EDSelected[2] = 1;
                                RecLineBar[i].EDSelected[3] = 1;
                                RecLineBar[i].EDSelected[4] = 1;
                                SelectedRecShape = RecLineBar[i].InRecShape;
                                for (int add = 1; add < 5; add++)
                                {
                                    RecShape[SelectedRecShape].EDSelected[add] = 1;
                                }
                                SelectedFlangedWallShape = RecLineBar[i].InFlangedWallShape;
                                SelectedRecBar1 = FlangedWallShape[SelectedFlangedWallShape].ApplyedToRecBars[1];
                                SelectedRecBar2 = FlangedWallShape[SelectedFlangedWallShape].ApplyedToRecBars[2];
                                SelectedRecBar3 = FlangedWallShape[SelectedFlangedWallShape].ApplyedToRecBars[3];
                                if (SelectedFlangedWallShape != 0)
                                {
                                    FlangedWallShape[SelectedFlangedWallShape].Selected = 1;
                                    RecLineBar[SelectedRecBar1].EDSelected[1] = 1;
                                    RecLineBar[SelectedRecBar1].EDSelected[2] = 1;
                                    RecLineBar[SelectedRecBar1].EDSelected[3] = 1;
                                    RecLineBar[SelectedRecBar1].EDSelected[4] = 1;
                                    RecLineBar[SelectedRecBar2].EDSelected[1] = 1;
                                    RecLineBar[SelectedRecBar2].EDSelected[2] = 1;
                                    RecLineBar[SelectedRecBar2].EDSelected[3] = 1;
                                    RecLineBar[SelectedRecBar2].EDSelected[4] = 1;
                                    RecLineBar[SelectedRecBar3].EDSelected[1] = 1;
                                    RecLineBar[SelectedRecBar3].EDSelected[2] = 1;
                                    RecLineBar[SelectedRecBar3].EDSelected[3] = 1;
                                    RecLineBar[SelectedRecBar3].EDSelected[4] = 1;
                                }
                            }
                        }
                        if (thecase == 2)
                        {
                            if (Tah1 == 1 || Tah2 == 1)
                            {
                                ifselected = 1;
                                RecLineBar[i].EDSelected[1] = 1;
                                RecLineBar[i].EDSelected[2] = 1;
                                RecLineBar[i].EDSelected[3] = 1;
                                RecLineBar[i].EDSelected[4] = 1;
                                SelectedRecShape = RecLineBar[i].InRecShape;
                                for (int add = 1; add < 5; add++)
                                {
                                    RecShape[SelectedRecShape].EDSelected[add] = 1;
                                }
                                SelectedFlangedWallShape = RecLineBar[i].InFlangedWallShape;
                                SelectedRecBar1 = FlangedWallShape[SelectedFlangedWallShape].ApplyedToRecBars[1];
                                SelectedRecBar2 = FlangedWallShape[SelectedFlangedWallShape].ApplyedToRecBars[2];
                                SelectedRecBar3 = FlangedWallShape[SelectedFlangedWallShape].ApplyedToRecBars[3];
                                if (SelectedFlangedWallShape != 0)
                                {
                                    FlangedWallShape[SelectedFlangedWallShape].Selected = 1;
                                    RecLineBar[SelectedRecBar1].EDSelected[1] = 1;
                                    RecLineBar[SelectedRecBar1].EDSelected[2] = 1;
                                    RecLineBar[SelectedRecBar1].EDSelected[3] = 1;
                                    RecLineBar[SelectedRecBar1].EDSelected[4] = 1;
                                    RecLineBar[SelectedRecBar2].EDSelected[1] = 1;
                                    RecLineBar[SelectedRecBar2].EDSelected[2] = 1;
                                    RecLineBar[SelectedRecBar2].EDSelected[3] = 1;
                                    RecLineBar[SelectedRecBar2].EDSelected[4] = 1;
                                    RecLineBar[SelectedRecBar3].EDSelected[1] = 1;
                                    RecLineBar[SelectedRecBar3].EDSelected[2] = 1;
                                    RecLineBar[SelectedRecBar3].EDSelected[3] = 1;
                                    RecLineBar[SelectedRecBar3].EDSelected[4] = 1;
                                }
                                goto end100;
                            }
                            INTERSECTION = 0;
                            int X1 = MinX;
                            int Y1 = MinY;
                            int X2 = MaxX;
                            int Y2 = MinY;
                            checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                            if (INTERSECTION == 1)
                            {
                                ifselected = 1;
                                RecLineBar[i].EDSelected[1] = 1;
                                RecLineBar[i].EDSelected[2] = 1;
                                RecLineBar[i].EDSelected[3] = 1;
                                RecLineBar[i].EDSelected[4] = 1;
                                SelectedRecShape = RecLineBar[i].InRecShape;
                                for (int add = 1; add < 5; add++)
                                {
                                    RecShape[SelectedRecShape].EDSelected[add] = 1;
                                }
                                SelectedFlangedWallShape = RecLineBar[i].InFlangedWallShape;
                                SelectedRecBar1 = FlangedWallShape[SelectedFlangedWallShape].ApplyedToRecBars[1];
                                SelectedRecBar2 = FlangedWallShape[SelectedFlangedWallShape].ApplyedToRecBars[2];
                                SelectedRecBar3 = FlangedWallShape[SelectedFlangedWallShape].ApplyedToRecBars[3];
                                if (SelectedFlangedWallShape != 0)
                                {
                                    FlangedWallShape[SelectedFlangedWallShape].Selected = 1;
                                    RecLineBar[SelectedRecBar1].EDSelected[1] = 1;
                                    RecLineBar[SelectedRecBar1].EDSelected[2] = 1;
                                    RecLineBar[SelectedRecBar1].EDSelected[3] = 1;
                                    RecLineBar[SelectedRecBar1].EDSelected[4] = 1;
                                    RecLineBar[SelectedRecBar2].EDSelected[1] = 1;
                                    RecLineBar[SelectedRecBar2].EDSelected[2] = 1;
                                    RecLineBar[SelectedRecBar2].EDSelected[3] = 1;
                                    RecLineBar[SelectedRecBar2].EDSelected[4] = 1;
                                    RecLineBar[SelectedRecBar3].EDSelected[1] = 1;
                                    RecLineBar[SelectedRecBar3].EDSelected[2] = 1;
                                    RecLineBar[SelectedRecBar3].EDSelected[3] = 1;
                                    RecLineBar[SelectedRecBar3].EDSelected[4] = 1;
                                }
                                goto end100;
                            }
                            X1 = MinX;
                            Y1 = MinY;
                            X2 = MinX;
                            Y2 = MaxY;
                            checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                            if (INTERSECTION == 1)
                            {
                                ifselected = 1;
                                RecLineBar[i].EDSelected[1] = 1;
                                RecLineBar[i].EDSelected[2] = 1;
                                RecLineBar[i].EDSelected[3] = 1;
                                RecLineBar[i].EDSelected[4] = 1;
                                SelectedRecShape = RecLineBar[i].InRecShape;
                                for (int add = 1; add < 5; add++)
                                {
                                    RecShape[SelectedRecShape].EDSelected[add] = 1;
                                }
                                SelectedFlangedWallShape = RecLineBar[i].InFlangedWallShape;
                                SelectedRecBar1 = FlangedWallShape[SelectedFlangedWallShape].ApplyedToRecBars[1];
                                SelectedRecBar2 = FlangedWallShape[SelectedFlangedWallShape].ApplyedToRecBars[2];
                                SelectedRecBar3 = FlangedWallShape[SelectedFlangedWallShape].ApplyedToRecBars[3];
                                if (SelectedFlangedWallShape != 0)
                                {
                                    FlangedWallShape[SelectedFlangedWallShape].Selected = 1;
                                    RecLineBar[SelectedRecBar1].EDSelected[1] = 1;
                                    RecLineBar[SelectedRecBar1].EDSelected[2] = 1;
                                    RecLineBar[SelectedRecBar1].EDSelected[3] = 1;
                                    RecLineBar[SelectedRecBar1].EDSelected[4] = 1;
                                    RecLineBar[SelectedRecBar2].EDSelected[1] = 1;
                                    RecLineBar[SelectedRecBar2].EDSelected[2] = 1;
                                    RecLineBar[SelectedRecBar2].EDSelected[3] = 1;
                                    RecLineBar[SelectedRecBar2].EDSelected[4] = 1;
                                    RecLineBar[SelectedRecBar3].EDSelected[1] = 1;
                                    RecLineBar[SelectedRecBar3].EDSelected[2] = 1;
                                    RecLineBar[SelectedRecBar3].EDSelected[3] = 1;
                                    RecLineBar[SelectedRecBar3].EDSelected[4] = 1;
                                }
                                goto end100;
                            }
                            X1 = MinX;
                            Y1 = MaxY;
                            X2 = MaxX;
                            Y2 = MaxY;
                            checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                            if (INTERSECTION == 1)
                            {
                                ifselected = 1;
                                RecLineBar[i].EDSelected[1] = 1;
                                RecLineBar[i].EDSelected[2] = 1;
                                RecLineBar[i].EDSelected[3] = 1;
                                RecLineBar[i].EDSelected[4] = 1;
                                SelectedRecShape = RecLineBar[i].InRecShape;
                                for (int add = 1; add < 5; add++)
                                {
                                    RecShape[SelectedRecShape].EDSelected[add] = 1;
                                }
                                SelectedFlangedWallShape = RecLineBar[i].InFlangedWallShape;
                                SelectedRecBar1 = FlangedWallShape[SelectedFlangedWallShape].ApplyedToRecBars[1];
                                SelectedRecBar2 = FlangedWallShape[SelectedFlangedWallShape].ApplyedToRecBars[2];
                                SelectedRecBar3 = FlangedWallShape[SelectedFlangedWallShape].ApplyedToRecBars[3];
                                if (SelectedFlangedWallShape != 0)
                                {
                                    FlangedWallShape[SelectedFlangedWallShape].Selected = 1;
                                    RecLineBar[SelectedRecBar1].EDSelected[1] = 1;
                                    RecLineBar[SelectedRecBar1].EDSelected[2] = 1;
                                    RecLineBar[SelectedRecBar1].EDSelected[3] = 1;
                                    RecLineBar[SelectedRecBar1].EDSelected[4] = 1;
                                    RecLineBar[SelectedRecBar2].EDSelected[1] = 1;
                                    RecLineBar[SelectedRecBar2].EDSelected[2] = 1;
                                    RecLineBar[SelectedRecBar2].EDSelected[3] = 1;
                                    RecLineBar[SelectedRecBar2].EDSelected[4] = 1;
                                    RecLineBar[SelectedRecBar3].EDSelected[1] = 1;
                                    RecLineBar[SelectedRecBar3].EDSelected[2] = 1;
                                    RecLineBar[SelectedRecBar3].EDSelected[3] = 1;
                                    RecLineBar[SelectedRecBar3].EDSelected[4] = 1;
                                }
                                goto end100;
                            }
                            X1 = MaxX;
                            Y1 = MaxY;
                            X2 = MaxX;
                            Y2 = MinY;
                            checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                            if (INTERSECTION == 1)
                            {
                                ifselected = 1;
                                RecLineBar[i].EDSelected[1] = 1;
                                RecLineBar[i].EDSelected[2] = 1;
                                RecLineBar[i].EDSelected[3] = 1;
                                RecLineBar[i].EDSelected[4] = 1;
                                SelectedRecShape = RecLineBar[i].InRecShape;
                                for (int add = 1; add < 5; add++)
                                {
                                    RecShape[SelectedRecShape].EDSelected[add] = 1;
                                }
                                SelectedFlangedWallShape = RecLineBar[i].InFlangedWallShape;
                                SelectedRecBar1 = FlangedWallShape[SelectedFlangedWallShape].ApplyedToRecBars[1];
                                SelectedRecBar2 = FlangedWallShape[SelectedFlangedWallShape].ApplyedToRecBars[2];
                                SelectedRecBar3 = FlangedWallShape[SelectedFlangedWallShape].ApplyedToRecBars[3];
                                if (SelectedFlangedWallShape != 0)
                                {
                                    FlangedWallShape[SelectedFlangedWallShape].Selected = 1;
                                    RecLineBar[SelectedRecBar1].EDSelected[1] = 1;
                                    RecLineBar[SelectedRecBar1].EDSelected[2] = 1;
                                    RecLineBar[SelectedRecBar1].EDSelected[3] = 1;
                                    RecLineBar[SelectedRecBar1].EDSelected[4] = 1;
                                    RecLineBar[SelectedRecBar2].EDSelected[1] = 1;
                                    RecLineBar[SelectedRecBar2].EDSelected[2] = 1;
                                    RecLineBar[SelectedRecBar2].EDSelected[3] = 1;
                                    RecLineBar[SelectedRecBar2].EDSelected[4] = 1;
                                    RecLineBar[SelectedRecBar3].EDSelected[1] = 1;
                                    RecLineBar[SelectedRecBar3].EDSelected[2] = 1;
                                    RecLineBar[SelectedRecBar3].EDSelected[3] = 1;
                                    RecLineBar[SelectedRecBar3].EDSelected[4] = 1;
                                }
                                goto end100;
                            }
                        }
                    }
                end100: { }
                }
                #endregion
                # region //تحديد خطوط أشكال المستطيلات
                for (int i = 1; i < RecShapeNumber + 1; i++)
                {
                    int X3 = 0;
                    int Y3 = 0;
                    int X4 = 0;
                    int Y4 = 0;
                    for (int j = 1; j < 5; j++)
                    {
                        X3 = RecShape[i].EDX12D[j];
                        Y3 = RecShape[i].EDY12D[j];
                        X4 = RecShape[i].EDX22D[j];
                        Y4 = RecShape[i].EDY22D[j];
                        int Tah1 = 0;
                        int Tah2 = 0;
                        if (X3 >= MinX & X3 <= MaxX & Y3 >= MinY & Y3 <= MaxY) Tah1 = 1;
                        if (X4 >= MinX & X4 <= MaxX & Y4 >= MinY & Y4 <= MaxY) Tah2 = 1;
                        if (thecase == 1)
                        {
                            if (Tah1 == 1 & Tah2 == 1)
                            {
                                ifselected = 1;
                                SelecteLinedBar = RecShape[i].ApplyedToRecBars;
                                RecLineBar[SelecteLinedBar].EDSelected[1] = 1;
                                RecLineBar[SelecteLinedBar].EDSelected[2] = 1;
                                RecLineBar[SelecteLinedBar].EDSelected[3] = 1;
                                RecLineBar[SelecteLinedBar].EDSelected[4] = 1;
                                for (int add = 1; add < 5; add++)
                                {
                                    RecShape[i].EDSelected[add] = 1;
                                }
                            }
                        }
                        if (thecase == 2)
                        {
                            if (Tah1 == 1 || Tah2 == 1)
                            {
                                ifselected = 1;
                                SelecteLinedBar = RecShape[i].ApplyedToRecBars;
                                RecLineBar[SelecteLinedBar].EDSelected[1] = 1;
                                RecLineBar[SelecteLinedBar].EDSelected[2] = 1;
                                RecLineBar[SelecteLinedBar].EDSelected[3] = 1;
                                RecLineBar[SelecteLinedBar].EDSelected[4] = 1;
                                for (int add = 1; add < 5; add++)
                                {
                                    RecShape[i].EDSelected[add] = 1;
                                }
                                goto end100;
                            }
                            INTERSECTION = 0;
                            int X1 = MinX;
                            int Y1 = MinY;
                            int X2 = MaxX;
                            int Y2 = MinY;
                            checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                            if (INTERSECTION == 1)
                            {
                                ifselected = 1;
                                SelecteLinedBar = RecShape[i].ApplyedToRecBars;
                                RecLineBar[SelecteLinedBar].EDSelected[1] = 1;
                                RecLineBar[SelecteLinedBar].EDSelected[2] = 1;
                                RecLineBar[SelecteLinedBar].EDSelected[3] = 1;
                                RecLineBar[SelecteLinedBar].EDSelected[4] = 1;
                                for (int add = 1; add < 5; add++)
                                {
                                    RecShape[i].EDSelected[add] = 1;
                                }
                                goto end100;
                            }
                            X1 = MinX;
                            Y1 = MinY;
                            X2 = MinX;
                            Y2 = MaxY;
                            checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                            if (INTERSECTION == 1)
                            {
                                ifselected = 1;
                                SelecteLinedBar = RecShape[i].ApplyedToRecBars;
                                RecLineBar[SelecteLinedBar].EDSelected[1] = 1;
                                RecLineBar[SelecteLinedBar].EDSelected[2] = 1;
                                RecLineBar[SelecteLinedBar].EDSelected[3] = 1;
                                RecLineBar[SelecteLinedBar].EDSelected[4] = 1;
                                for (int add = 1; add < 5; add++)
                                {
                                    RecShape[i].EDSelected[add] = 1;
                                }
                                goto end100;
                            }
                            X1 = MinX;
                            Y1 = MaxY;
                            X2 = MaxX;
                            Y2 = MaxY;
                            checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                            if (INTERSECTION == 1)
                            {
                                ifselected = 1;
                                SelecteLinedBar = RecShape[i].ApplyedToRecBars;
                                RecLineBar[SelecteLinedBar].EDSelected[1] = 1;
                                RecLineBar[SelecteLinedBar].EDSelected[2] = 1;
                                RecLineBar[SelecteLinedBar].EDSelected[3] = 1;
                                RecLineBar[SelecteLinedBar].EDSelected[4] = 1;
                                for (int add = 1; add < 5; add++)
                                {
                                    RecShape[i].EDSelected[add] = 1;
                                }
                                goto end100;
                            }
                            X1 = MaxX;
                            Y1 = MaxY;
                            X2 = MaxX;
                            Y2 = MinY;
                            checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                            if (INTERSECTION == 1)
                            {
                                ifselected = 1;
                                SelecteLinedBar = RecShape[i].ApplyedToRecBars;
                                RecLineBar[SelecteLinedBar].EDSelected[1] = 1;
                                RecLineBar[SelecteLinedBar].EDSelected[2] = 1;
                                RecLineBar[SelecteLinedBar].EDSelected[3] = 1;
                                RecLineBar[SelecteLinedBar].EDSelected[4] = 1;
                                for (int add = 1; add < 5; add++)
                                {
                                    RecShape[i].EDSelected[add] = 1;
                                }
                                goto end100;
                            }
                        }
                    }
                end100: { }
                }
                #endregion
                # region //تحديد خطوط أشكال جدران القص
                for (int i = 1; i < FlangedWallShapeNumber + 1; i++)
                {
                    int X3 = 0;
                    int Y3 = 0;
                    int X4 = 0;
                    int Y4 = 0;
                    for (int j = 1; j < 13; j++)
                    {
                        if (j < 12)
                        {
                            X3 = FlangedWallShape[i].PointX2D[j];
                            Y3 = FlangedWallShape[i].PointY2D[j];
                            X4 = FlangedWallShape[i].PointX2D[j + 1];
                            Y4 = FlangedWallShape[i].PointY2D[j + 1];
                        }
                        else
                        {
                            X3 = FlangedWallShape[i].PointX2D[1];
                            Y3 = FlangedWallShape[i].PointY2D[1];
                            X4 = FlangedWallShape[i].PointX2D[12];
                            Y4 = FlangedWallShape[i].PointY2D[12];
                        }
                        int Tah1 = 0;
                        int Tah2 = 0;
                        if (X3 >= MinX & X3 <= MaxX & Y3 >= MinY & Y3 <= MaxY) Tah1 = 1;
                        if (X4 >= MinX & X4 <= MaxX & Y4 >= MinY & Y4 <= MaxY) Tah2 = 1;
                        if (thecase == 1)
                        {
                            if (Tah1 == 1 & Tah2 == 1)
                            {
                                ifselected = 1;
                                FlangedWallShape[i].Selected = 1;
                                SelectedRecBar1 = FlangedWallShape[i].ApplyedToRecBars[1];
                                SelectedRecBar2 = FlangedWallShape[i].ApplyedToRecBars[2];
                                SelectedRecBar3 = FlangedWallShape[i].ApplyedToRecBars[3];
                                RecLineBar[SelectedRecBar1].EDSelected[1] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[2] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[3] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[4] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[1] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[2] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[3] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[4] = 1;
                                RecLineBar[SelectedRecBar3].EDSelected[1] = 1;
                                RecLineBar[SelectedRecBar3].EDSelected[2] = 1;
                                RecLineBar[SelectedRecBar3].EDSelected[3] = 1;
                                RecLineBar[SelectedRecBar3].EDSelected[4] = 1;
                            }
                        }
                        if (thecase == 2)
                        {
                            if (Tah1 == 1 || Tah2 == 1)
                            {
                                ifselected = 1;
                                FlangedWallShape[i].Selected = 1;
                                SelectedRecBar1 = FlangedWallShape[i].ApplyedToRecBars[1];
                                SelectedRecBar2 = FlangedWallShape[i].ApplyedToRecBars[2];
                                SelectedRecBar3 = FlangedWallShape[i].ApplyedToRecBars[3];
                                RecLineBar[SelectedRecBar1].EDSelected[1] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[2] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[3] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[4] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[1] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[2] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[3] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[4] = 1;
                                RecLineBar[SelectedRecBar3].EDSelected[1] = 1;
                                RecLineBar[SelectedRecBar3].EDSelected[2] = 1;
                                RecLineBar[SelectedRecBar3].EDSelected[3] = 1;
                                RecLineBar[SelectedRecBar3].EDSelected[4] = 1;
                                goto end100;
                            }
                            INTERSECTION = 0;
                            int X1 = MinX;
                            int Y1 = MinY;
                            int X2 = MaxX;
                            int Y2 = MinY;
                            checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                            if (INTERSECTION == 1)
                            {
                                ifselected = 1;
                                FlangedWallShape[i].Selected = 1;
                                SelectedRecBar1 = FlangedWallShape[i].ApplyedToRecBars[1];
                                SelectedRecBar2 = FlangedWallShape[i].ApplyedToRecBars[2];
                                SelectedRecBar3 = FlangedWallShape[i].ApplyedToRecBars[3];
                                RecLineBar[SelectedRecBar1].EDSelected[1] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[2] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[3] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[4] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[1] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[2] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[3] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[4] = 1;
                                RecLineBar[SelectedRecBar3].EDSelected[1] = 1;
                                RecLineBar[SelectedRecBar3].EDSelected[2] = 1;
                                RecLineBar[SelectedRecBar3].EDSelected[3] = 1;
                                RecLineBar[SelectedRecBar3].EDSelected[4] = 1;
                                goto end100;
                            }
                            X1 = MinX;
                            Y1 = MinY;
                            X2 = MinX;
                            Y2 = MaxY;
                            checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                            if (INTERSECTION == 1)
                            {
                                ifselected = 1;
                                FlangedWallShape[i].Selected = 1;
                                SelectedRecBar1 = FlangedWallShape[i].ApplyedToRecBars[1];
                                SelectedRecBar2 = FlangedWallShape[i].ApplyedToRecBars[2];
                                SelectedRecBar3 = FlangedWallShape[i].ApplyedToRecBars[3];
                                RecLineBar[SelectedRecBar1].EDSelected[1] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[2] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[3] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[4] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[1] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[2] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[3] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[4] = 1;
                                RecLineBar[SelectedRecBar3].EDSelected[1] = 1;
                                RecLineBar[SelectedRecBar3].EDSelected[2] = 1;
                                RecLineBar[SelectedRecBar3].EDSelected[3] = 1;
                                RecLineBar[SelectedRecBar3].EDSelected[4] = 1;
                                goto end100;
                            }
                            X1 = MinX;
                            Y1 = MaxY;
                            X2 = MaxX;
                            Y2 = MaxY;
                            checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                            if (INTERSECTION == 1)
                            {
                                ifselected = 1;
                                FlangedWallShape[i].Selected = 1;
                                SelectedRecBar1 = FlangedWallShape[i].ApplyedToRecBars[1];
                                SelectedRecBar2 = FlangedWallShape[i].ApplyedToRecBars[2];
                                SelectedRecBar3 = FlangedWallShape[i].ApplyedToRecBars[3];
                                RecLineBar[SelectedRecBar1].EDSelected[1] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[2] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[3] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[4] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[1] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[2] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[3] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[4] = 1;
                                RecLineBar[SelectedRecBar3].EDSelected[1] = 1;
                                RecLineBar[SelectedRecBar3].EDSelected[2] = 1;
                                RecLineBar[SelectedRecBar3].EDSelected[3] = 1;
                                RecLineBar[SelectedRecBar3].EDSelected[4] = 1;
                                goto end100;
                            }
                            X1 = MaxX;
                            Y1 = MaxY;
                            X2 = MaxX;
                            Y2 = MinY;
                            checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                            if (INTERSECTION == 1)
                            {
                                ifselected = 1;
                                FlangedWallShape[i].Selected = 1;
                                SelectedRecBar1 = FlangedWallShape[i].ApplyedToRecBars[1];
                                SelectedRecBar2 = FlangedWallShape[i].ApplyedToRecBars[2];
                                SelectedRecBar3 = FlangedWallShape[i].ApplyedToRecBars[3];
                                RecLineBar[SelectedRecBar1].EDSelected[1] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[2] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[3] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[4] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[1] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[2] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[3] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[4] = 1;
                                RecLineBar[SelectedRecBar3].EDSelected[1] = 1;
                                RecLineBar[SelectedRecBar3].EDSelected[2] = 1;
                                RecLineBar[SelectedRecBar3].EDSelected[3] = 1;
                                RecLineBar[SelectedRecBar3].EDSelected[4] = 1;
                                goto end100;
                            }
                        }
                    }
                end100: { }
                }
                #endregion
                # region //تحديد خطوط أشكال تيه 
                for (int i = 1; i < TeeShapeNumber + 1; i++)
                {
                    int X3 = 0;
                    int Y3 = 0;
                    int X4 = 0;
                    int Y4 = 0;
                    for (int j = 1; j < 9; j++)
                    {
                        if (j < 8)
                        {
                            X3 = TeeShape[i].PointX2D[j];
                            Y3 = TeeShape[i].PointY2D[j];
                            X4 = TeeShape[i].PointX2D[j + 1];
                            Y4 = TeeShape[i].PointY2D[j + 1];
                        }
                        else
                        {
                            X3 = TeeShape[i].PointX2D[1];
                            Y3 = TeeShape[i].PointY2D[1];
                            X4 = TeeShape[i].PointX2D[8];
                            Y4 = TeeShape[i].PointY2D[8];
                        }
                        int Tah1 = 0;
                        int Tah2 = 0;
                        if (X3 >= MinX & X3 <= MaxX & Y3 >= MinY & Y3 <= MaxY) Tah1 = 1;
                        if (X4 >= MinX & X4 <= MaxX & Y4 >= MinY & Y4 <= MaxY) Tah2 = 1;
                        if (thecase == 1)
                        {
                            if (Tah1 == 1 & Tah2 == 1)
                            {
                                ifselected = 1;
                                TeeShape[i].Selected = 1;
                                SelectedRecBar1 = TeeShape[i].ApplyedToRecBars[1];
                                SelectedRecBar2 = TeeShape[i].ApplyedToRecBars[2];
                                RecLineBar[SelectedRecBar1].EDSelected[1] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[2] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[3] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[4] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[1] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[2] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[3] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[4] = 1;
                            }
                        }
                        if (thecase == 2)
                        {
                            if (Tah1 == 1 || Tah2 == 1)
                            {
                                ifselected = 1;
                                TeeShape[i].Selected = 1;
                                SelectedRecBar1 = TeeShape[i].ApplyedToRecBars[1];
                                SelectedRecBar2 = TeeShape[i].ApplyedToRecBars[2];
                                RecLineBar[SelectedRecBar1].EDSelected[1] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[2] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[3] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[4] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[1] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[2] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[3] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[4] = 1;
                                goto end1001;
                            }
                            INTERSECTION = 0;
                            int X1 = MinX;
                            int Y1 = MinY;
                            int X2 = MaxX;
                            int Y2 = MinY;
                            checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                            if (INTERSECTION == 1)
                            {
                                ifselected = 1;
                                TeeShape[i].Selected = 1;
                                SelectedRecBar1 = TeeShape[i].ApplyedToRecBars[1];
                                SelectedRecBar2 = TeeShape[i].ApplyedToRecBars[2];
                                RecLineBar[SelectedRecBar1].EDSelected[1] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[2] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[3] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[4] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[1] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[2] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[3] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[4] = 1;
                                RecLineBar[SelectedRecBar3].EDSelected[1] = 1;
                                RecLineBar[SelectedRecBar3].EDSelected[2] = 1;
                                RecLineBar[SelectedRecBar3].EDSelected[3] = 1;
                                RecLineBar[SelectedRecBar3].EDSelected[4] = 1;
                                goto end1001;
                            }
                            X1 = MinX;
                            Y1 = MinY;
                            X2 = MinX;
                            Y2 = MaxY;
                            checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                            if (INTERSECTION == 1)
                            {
                                ifselected = 1;
                                TeeShape[i].Selected = 1;
                                SelectedRecBar1 = TeeShape[i].ApplyedToRecBars[1];
                                SelectedRecBar2 = TeeShape[i].ApplyedToRecBars[2];
                                RecLineBar[SelectedRecBar1].EDSelected[1] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[2] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[3] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[4] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[1] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[2] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[3] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[4] = 1;
                                goto end1001;
                            }
                            X1 = MinX;
                            Y1 = MaxY;
                            X2 = MaxX;
                            Y2 = MaxY;
                            checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                            if (INTERSECTION == 1)
                            {
                                ifselected = 1;
                                TeeShape[i].Selected = 1;
                                SelectedRecBar1 = TeeShape[i].ApplyedToRecBars[1];
                                SelectedRecBar2 = TeeShape[i].ApplyedToRecBars[2];
                                RecLineBar[SelectedRecBar1].EDSelected[1] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[2] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[3] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[4] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[1] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[2] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[3] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[4] = 1;
                                goto end1001;
                            }
                            X1 = MaxX;
                            Y1 = MaxY;
                            X2 = MaxX;
                            Y2 = MinY;
                            checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                            if (INTERSECTION == 1)
                            {
                                ifselected = 1;
                                TeeShape[i].Selected = 1;
                                SelectedRecBar1 = TeeShape[i].ApplyedToRecBars[1];
                                SelectedRecBar2 = TeeShape[i].ApplyedToRecBars[2];
                                RecLineBar[SelectedRecBar1].EDSelected[1] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[2] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[3] = 1;
                                RecLineBar[SelectedRecBar1].EDSelected[4] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[1] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[2] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[3] = 1;
                                RecLineBar[SelectedRecBar2].EDSelected[4] = 1;
                                goto end1001;
                            }
                        }
                    }
                end1001: { }
                }
                #endregion
                # region //تحديد خطوط تسليح الدوائر
                for (int i = 1; i < CircleBarsNumber + 1; i++)
                {
                    int X3 = 0;
                    int Y3 = 0;
                    int X4 = 0;
                    int Y4 = 0;
                    for (int j = 1; j < 33; j++)
                    {
                        if (j < 32)
                        {
                            X3 = CircleLineBar[i].PointX2D[j];
                            Y3 = CircleLineBar[i].PointY2D[j];
                            X4 = CircleLineBar[i].PointX2D[j + 1];
                            Y4 = CircleLineBar[i].PointY2D[j + 1];
                        }
                        else
                        {
                            X3 = CircleLineBar[i].PointX2D[1];
                            Y3 = CircleLineBar[i].PointY2D[1];
                            X4 = CircleLineBar[i].PointX2D[32];
                            Y4 = CircleLineBar[i].PointY2D[32];
                        }
                        int Tah1 = 0;
                        int Tah2 = 0;
                        if (X3 >= MinX & X3 <= MaxX & Y3 >= MinY & Y3 <= MaxY) Tah1 = 1;
                        if (X4 >= MinX & X4 <= MaxX & Y4 >= MinY & Y4 <= MaxY) Tah2 = 1;
                        if (thecase == 1)
                        {
                            if (Tah1 == 1 & Tah2 == 1)
                            {
                                ifselected = 1;
                                CircleLineBar[i].Selected = 1;
                                // SelectedRecShape = RecLineBar[i].InRecShape;
                                // RecShape[SelectedRecShape].ED1Selected = 1;
                                // RecShape[SelectedRecShape].ED2Selected = 1;
                                // RecShape[SelectedRecShape].ED3Selected = 1;
                                //  RecShape[SelectedRecShape].ED4Selected = 1;
                            }
                        }
                        if (thecase == 2)
                        {
                            if (Tah1 == 1 || Tah2 == 1)
                            {
                                ifselected = 1;
                                CircleLineBar[i].Selected = 1;
                                // SelectedRecShape = RecLineBar[i].InRecShape;
                                // RecShape[SelectedRecShape].ED1Selected = 1;
                                // RecShape[SelectedRecShape].ED2Selected = 1;
                                // RecShape[SelectedRecShape].ED3Selected = 1;
                                //  RecShape[SelectedRecShape].ED4Selected = 1;
                                goto end100;
                            }
                            INTERSECTION = 0;
                            int X1 = MinX;
                            int Y1 = MinY;
                            int X2 = MaxX;
                            int Y2 = MinY;
                            checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                            if (INTERSECTION == 1)
                            {
                                ifselected = 1;
                                CircleLineBar[i].Selected = 1;
                                // SelectedRecShape = RecLineBar[i].InRecShape;
                                // RecShape[SelectedRecShape].ED1Selected = 1;
                                // RecShape[SelectedRecShape].ED2Selected = 1;
                                // RecShape[SelectedRecShape].ED3Selected = 1;
                                //  RecShape[SelectedRecShape].ED4Selected = 1;
                                goto end100;
                            }
                            X1 = MinX;
                            Y1 = MinY;
                            X2 = MinX;
                            Y2 = MaxY;
                            checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                            if (INTERSECTION == 1)
                            {
                                ifselected = 1;
                                CircleLineBar[i].Selected = 1;
                                // SelectedRecShape = RecLineBar[i].InRecShape;
                                // RecShape[SelectedRecShape].ED1Selected = 1;
                                // RecShape[SelectedRecShape].ED2Selected = 1;
                                // RecShape[SelectedRecShape].ED3Selected = 1;
                                //  RecShape[SelectedRecShape].ED4Selected = 1;
                                goto end100;
                            }
                            X1 = MinX;
                            Y1 = MaxY;
                            X2 = MaxX;
                            Y2 = MaxY;
                            checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                            if (INTERSECTION == 1)
                            {
                                ifselected = 1;
                                CircleLineBar[i].Selected = 1;
                                // SelectedRecShape = RecLineBar[i].InRecShape;
                                // RecShape[SelectedRecShape].ED1Selected = 1;
                                // RecShape[SelectedRecShape].ED2Selected = 1;
                                // RecShape[SelectedRecShape].ED3Selected = 1;
                                //  RecShape[SelectedRecShape].ED4Selected = 1;
                                goto end100;
                            }
                            X1 = MaxX;
                            Y1 = MaxY;
                            X2 = MaxX;
                            Y2 = MinY;
                            checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                            if (INTERSECTION == 1)
                            {
                                ifselected = 1;
                                CircleLineBar[i].Selected = 1;
                                // SelectedRecShape = RecLineBar[i].InRecShape;
                                // RecShape[SelectedRecShape].ED1Selected = 1;
                                // RecShape[SelectedRecShape].ED2Selected = 1;
                                // RecShape[SelectedRecShape].ED3Selected = 1;
                                //  RecShape[SelectedRecShape].ED4Selected = 1;
                                goto end100;
                            }
                        }
                    }
                end100: { }
                }
                #endregion
                # region //تحديد خطوط أشكال الدوائر
                for (int i = 1; i < CircleShapeNumber + 1; i++)
                {
                    int X3 = 0;
                    int Y3 = 0;
                    int X4 = 0;
                    int Y4 = 0;
                    for (int j = 1; j < 33; j++)
                    {
                        if (j < 32)
                        {
                            X3 = CircleShape[i].PointX2D[j];
                            Y3 = CircleShape[i].PointY2D[j];
                            X4 = CircleShape[i].PointX2D[j + 1];
                            Y4 = CircleShape[i].PointY2D[j + 1];
                        }
                        else
                        {
                            X3 = CircleShape[i].PointX2D[1];
                            Y3 = CircleShape[i].PointY2D[1];
                            X4 = CircleShape[i].PointX2D[32];
                            Y4 = CircleShape[i].PointY2D[32];
                        }
                        int Tah1 = 0;
                        int Tah2 = 0;
                        if (X3 >= MinX & X3 <= MaxX & Y3 >= MinY & Y3 <= MaxY) Tah1 = 1;
                        if (X4 >= MinX & X4 <= MaxX & Y4 >= MinY & Y4 <= MaxY) Tah2 = 1;
                        if (thecase == 1)
                        {
                            if (Tah1 == 1 & Tah2 == 1)
                            {
                                ifselected = 1;
                                CircleShape[i].Selected = 1;
                            }
                        }
                        if (thecase == 2)
                        {
                            if (Tah1 == 1 || Tah2 == 1)
                            {
                                ifselected = 1;
                                CircleShape[i].Selected = 1;
                                goto end100;
                            }
                            INTERSECTION = 0;
                            int X1 = MinX;
                            int Y1 = MinY;
                            int X2 = MaxX;
                            int Y2 = MinY;
                            checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                            if (INTERSECTION == 1)
                            {
                                ifselected = 1;
                                CircleShape[i].Selected = 1;
                                goto end100;
                            }
                            X1 = MinX;
                            Y1 = MinY;
                            X2 = MinX;
                            Y2 = MaxY;
                            checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                            if (INTERSECTION == 1)
                            {
                                ifselected = 1;
                                CircleShape[i].Selected = 1;
                                goto end100;
                            }
                            X1 = MinX;
                            Y1 = MaxY;
                            X2 = MaxX;
                            Y2 = MaxY;
                            checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                            if (INTERSECTION == 1)
                            {
                                ifselected = 1;
                                CircleShape[i].Selected = 1;
                                goto end100;
                            }
                            X1 = MaxX;
                            Y1 = MaxY;
                            X2 = MaxX;
                            Y2 = MinY;
                            checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                            if (INTERSECTION == 1)
                            {
                                ifselected = 1;
                                CircleShape[i].Selected = 1;
                                goto end100;
                            }
                        }
                    }
                end100: { }
                }
                #endregion
                if (ifselected == 1)
                {
                    Render2d();
                }
                Bitmap finalBmp = new Bitmap(BitampWidth2d, BitampHight2d);
                if (pictureBox2.Image != null)
                {
                    pictureBox2.Image.Dispose();
                    pictureBox2.Image = null;
                }
                pictureBox2.Image = finalBmp;
            }
            #endregion
        }
        private void pictureBox2Draw()
        {
            Bitmap finalBmp = new Bitmap(BitampWidth2d, BitampHight2d);
            if (pictureBox2.Image != null)
            {
                pictureBox2.Image.Dispose();
                pictureBox2.Image = null;
            }
            pictureBox2.Image = finalBmp;
            #region//رسم مربع التحديد
            if (MouseButtonsLeft == 1 & toolStripButton1.Checked == true)
            {
                Graphics g = Graphics.FromImage(pictureBox2.Image);
                Pen pen = new Pen(Color.Black, 1f);
                float[] dashValues = { 5, 3, 5, 3 };
                pen.DashPattern = dashValues;
                Point[] polygon = new Point[5];
                polygon[0].X = xmove1;
                polygon[0].Y = ymove1;
                polygon[1].X = TempX;
                polygon[1].Y = ymove1;
                polygon[2].X = TempX;
                polygon[2].Y = TempY;
                polygon[3].X = xmove1;
                polygon[3].Y = TempY;
                polygon[4] = polygon[0];
                g.DrawLine(pen, polygon[0], polygon[1]);
                g.DrawLine(pen, polygon[1], polygon[2]);
                g.DrawLine(pen, polygon[2], polygon[3]);
                g.DrawLine(pen, polygon[3], polygon[4]);
                using (Brush brush = new SolidBrush(Color.FromArgb(30, Color.Orange)))
                {
                    g.FillPolygon(brush, polygon);
                }
            }
            #endregion
            #region//تحديد البلاطة بالرسم السريع
            if (AriaSelected != 0)
            {
                Graphics g = Graphics.FromImage(pictureBox2.Image);
                Pen pen = new Pen(Color.Red, 2f);
                int i = AriaSelected;
                int N = CallMesh.AriaPointNoF[i];
                Point[] polygon = new Point[N + 1];
                for (int j = 1; j < N + 1; j++)
                {
                    polygon[j].X = startX2d + Convert.ToInt32((CallMesh.AriaPointXF[i, j] * Zoom2d));
                    polygon[j].Y = startY2d - Convert.ToInt32((CallMesh.AriaPointYF[i, j] * Zoom2d));
                }
                float[] dashValues = { 5, 3, 5, 3 };
                pen.DashPattern = dashValues;
                for (int j = 1; j < N; j++)
                {
                    g.DrawLine(pen, polygon[j], polygon[j + 1]);
                }
                using (Brush brush = new SolidBrush(Color.FromArgb(30, Color.LightSkyBlue)))
                {
                    g.FillPolygon(brush, polygon);
                }
            }
            #endregion
            pictureBox2Drawsub();
        }
        private void pictureBox2Drawsub()
        {
            Graphics g = Graphics.FromImage(pictureBox2.Image);
            Pen pen = new Pen(Color.Gray, 1f);
            Font drawFont = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
            SolidBrush drawBrush = new SolidBrush(Color.Blue);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            float[] dashValues = { 2, 3 };
            pen.DashPattern = dashValues;
            LineMoveX1 = startX2d + Convert.ToInt32((TempXReal[1]) * Zoom2d);
            LineMoveY1 = startY2d - Convert.ToInt32((TempYReal[1]) * Zoom2d);
            //
            if (LineMove2dVisible == 1)
            {
                g.DrawLine(pen, LineMoveX1, LineMoveY1, TempX, TempY);// رسم خط الجوائز
                double thelength = Math.Round(Math.Sqrt(Math.Pow(TempXReal[1] - TempX12Real, 2) + Math.Pow(TempYReal[1] - TempY12Real, 2)), 0);
                int thex = (TempX + LineMoveX1) / 2;
                int they = (TempY + LineMoveY1) / 2;
                g.DrawString(thelength.ToString() + " mm", drawFont, drawBrush, thex - 15, they - 15);
                int width = 60;
                int height = 60;
                int startAngle = 0;
                float sweepAngle = (float)Math.Round(Angulo(TempXReal[1], TempYReal[1], TempX12Real, TempY12Real), 2);
                thex = LineMoveX1;
                they = LineMoveY1;
                pen = new Pen(Color.Gray, 1f);
                if (sweepAngle <= 180)
                {
                    g.DrawString(sweepAngle.ToString() + " ْ", drawFont, drawBrush, thex + 32, they - 15);
                    g.DrawArc(pen, LineMoveX1 - 30, LineMoveY1 - 30, width, height, startAngle, -sweepAngle);
                }
                else
                {
                    g.DrawString(Math.Round((360 - sweepAngle), 2).ToString() + " ْ", drawFont, drawBrush, thex + 32, they - 15);
                    g.DrawArc(pen, LineMoveX1 - 30, LineMoveY1 - 30, width, height, startAngle, (360 - sweepAngle));
                }
                g.DrawLine(pen, LineMoveX1, LineMoveY1, LineMoveX1 + 30, LineMoveY1);
                pen = new Pen(Color.LightGreen, 1f);
                pen.DashPattern = dashValues;
                if (Math.Abs(sweepAngle - 90) < 0.8)
                {
                    g.DrawLine(pen, LineMoveX1, LineMoveY1, LineMoveX1, LineMoveY1 - 1000);
                }
                if (Math.Abs(sweepAngle - 180) < 0.8)
                {
                    g.DrawLine(pen, LineMoveX1, LineMoveY1, LineMoveX1 - 1000, LineMoveY1);
                }
                if (Math.Abs(sweepAngle - 270) < 0.8)
                {
                    g.DrawLine(pen, LineMoveX1, LineMoveY1, LineMoveX1, LineMoveY1 + 1000);
                }

                if (Math.Abs(sweepAngle - 0) < 0.8 || Math.Abs(sweepAngle - 360) < 0.8)
                {
                    g.DrawLine(pen, LineMoveX1, LineMoveY1, LineMoveX1 + 1000, LineMoveY1);
                }
            }
            g = Graphics.FromImage(pictureBox2.Image);//رسم نقطة تقاطع الشبكة
            pen = new Pen(Color.Red, 1f);
            if (Tahkik == 1 & SnapFromType == "GridIntersection")
            {
                g.DrawLine(pen, TempX + 5, TempY, TempX - 5, TempY);
                g.DrawLine(pen, TempX, TempY + 5, TempX, TempY - 5);
                drawBrush = new SolidBrush(Color.Black);
                g.DrawString("Grid Point " + "", drawFont, drawBrush, TempX + 10, TempY - 15);
            }
            if (Tahkik == 1 & SnapFromType == "Point")
            {
                g.DrawLine(pen, TempX + 5, TempY + 5, TempX - 5, TempY + 5);
                g.DrawLine(pen, TempX + 5, TempY - 5, TempX - 5, TempY - 5);
                g.DrawLine(pen, TempX + 5, TempY - 5, TempX + 5, TempY + 5);
                g.DrawLine(pen, TempX - 5, TempY - 5, TempX - 5, TempY + 5);
                drawBrush = new SolidBrush(Color.Black);
                string textName = "Point";
                var textPosition = new Point(TempX + 10, TempY - 15);
                var size = g.MeasureString(textName, drawFont);
                var rect = new RectangleF(textPosition.X, textPosition.Y, size.Width, size.Height);
                g.FillRectangle(Brushes.LemonChiffon, rect);
                g.DrawString(textName, drawFont, drawBrush, textPosition);
            }
            if (Tahkik == 1 & SnapFromType == "End Point")
            {
                g.DrawLine(pen, TempX + 5, TempY + 5, TempX - 5, TempY + 5);
                g.DrawLine(pen, TempX + 5, TempY - 5, TempX - 5, TempY - 5);
                g.DrawLine(pen, TempX + 5, TempY - 5, TempX + 5, TempY + 5);
                g.DrawLine(pen, TempX - 5, TempY - 5, TempX - 5, TempY + 5);
                drawBrush = new SolidBrush(Color.Black);
                string textName = "End Point";
                var textPosition = new Point(TempX + 10, TempY - 15);
                var size = g.MeasureString(textName, drawFont);
                var rect = new RectangleF(textPosition.X, textPosition.Y, size.Width, size.Height);
                g.FillRectangle(Brushes.LemonChiffon, rect);
                g.DrawString(textName, drawFont, drawBrush, textPosition);
            }
            if (Tahkik == 1 & SnapFromType == "LineEndsandMidpoints")
            {
                g.DrawLine(pen, TempX + 5, TempY + 5, TempX - 5, TempY + 5);
                g.DrawLine(pen, TempX - 5, TempY + 5, TempX, TempY - 5);
                g.DrawLine(pen, TempX + 5, TempY + 5, TempX, TempY - 5);
                drawBrush = new SolidBrush(Color.Black);
                g.DrawString("Mid Point", drawFont, drawBrush, TempX + 10, TempY - 15);
            }
            if (Tahkik == 1 & SnapFromType == "LinesandFrames")
            {
                g.DrawLine(pen, TempX + 5, TempY + 5, TempX - 5, TempY + 5);
                g.DrawLine(pen, TempX + 5, TempY - 5, TempX - 5, TempY - 5);
                g.DrawLine(pen, TempX + 5, TempY + 5, TempX - 5, TempY - 5);
                g.DrawLine(pen, TempX - 5, TempY + 5, TempX + 5, TempY - 5);
                drawBrush = new SolidBrush(Color.Black);
                g.DrawString("Line/Edge", drawFont, drawBrush, TempX + 10, TempY - 15);
            }
            if (Tahkik == 1 & SnapFromType == "Edges")
            {
                g.DrawLine(pen, TempX + 5, TempY + 5, TempX - 5, TempY + 5);
                g.DrawLine(pen, TempX + 5, TempY - 5, TempX - 5, TempY - 5);
                g.DrawLine(pen, TempX + 5, TempY + 5, TempX - 5, TempY - 5);
                g.DrawLine(pen, TempX - 5, TempY + 5, TempX + 5, TempY - 5);
                drawBrush = new SolidBrush(Color.Black);
                g.DrawString("Area Edge", drawFont, drawBrush, TempX + 10, TempY - 15);
            }
            if (Tahkik == 1 & SnapFromType == "Intersections")
            {
                g.DrawLine(pen, TempX + 5, TempY + 5, TempX - 5, TempY - 5);
                g.DrawLine(pen, TempX - 5, TempY + 5, TempX + 5, TempY - 5);
                drawBrush = new SolidBrush(Color.Black);
                g.DrawString("Intersection", drawFont, drawBrush, TempX + 10, TempY - 15);
            }
            if (Tahkik == 1 & SnapFromType == "Prallels" & Myglobals.LineMove2dVisible == 1)
            {
                g.DrawLine(pen, TempX + 5, TempY + 5, TempX - 5, TempY - 5);
                g.DrawLine(pen, TempX - 5, TempY + 5, TempX + 5, TempY - 5);
                drawBrush = new SolidBrush(Color.Black);
                g.DrawString("Prallels", drawFont, drawBrush, TempX + 10, TempY - 15);
            }
            if (Tahkik == 1 & SnapFromType == "FineGrid")
            {
                g.DrawLine(pen, TempX + 5, TempY, TempX - 5, TempY);
                g.DrawLine(pen, TempX, TempY + 5, TempX, TempY - 5);
                drawBrush = new SolidBrush(Color.Black);
                g.DrawString("FineGrid", drawFont, drawBrush, TempX + 10, TempY - 15);
            }
        }
        #endregion
        #region //اجرائيات التعديل بالمقابض
        private void DrawForEditTeeShape()
        {
            Bitmap finalBmp = new Bitmap(BitampWidth2d, BitampHight2d);
            if (pictureBox2.Image != null)
            {
                pictureBox2.Image.Dispose();
                pictureBox2.Image = null;
            }
            pictureBox2.Image = finalBmp;
            if (SelectedTeeShape != 0)
            {
                Graphics g = Graphics.FromImage(pictureBox2.Image);
                Pen pen = new Pen(Color.Navy, 3f);
                Pen pen1 = new Pen(Color.Blue, 5);
                Font drawFont = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
                SolidBrush drawBrush = new SolidBrush(Color.Blue);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                float[] dashValues = { 3, 3 };
                pen.DashPattern = dashValues;
                int thex = (TempX + LineMoveX1) / 2;
                int they = (TempY + LineMoveY1) / 2;
                int tx1 = 0;
                int ty1 = 0;
                int tx2 = 0;
                int ty2 = 0;
                int tx = 0;
                int ty = 0;
                #region// RecEdgeToEdit == 0
                if (RecEdgeToEdit == 0)
                {
                    tx1 = TeeShape[SelectedTeeShape].PointX2D[1];
                    ty1 = TeeShape[SelectedTeeShape].PointY2D[1];
                    tx2 = TeeShape[SelectedTeeShape].PointX2D[2];
                    ty2 = TeeShape[SelectedTeeShape].PointY2D[2];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = TeeShape[SelectedTeeShape].PointX2D[2];
                    ty1 = TeeShape[SelectedTeeShape].PointY2D[2];
                    tx2 = TeeShape[SelectedTeeShape].PointX2D[2];
                    ty2 = TeeShape[SelectedTeeShape].PointY2D[5];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = TeeShape[SelectedTeeShape].PointX2D[2];
                    ty1 = TeeShape[SelectedTeeShape].PointY2D[5];
                    tx2 = TeeShape[SelectedTeeShape].PointX2D[1];
                    ty2 = TeeShape[SelectedTeeShape].PointY2D[5];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = TeeShape[SelectedTeeShape].PointX2D[1];
                    ty1 = TeeShape[SelectedTeeShape].PointY2D[5];
                    tx2 = TeeShape[SelectedTeeShape].PointX2D[1];
                    ty2 = TeeShape[SelectedTeeShape].PointY2D[1];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                }
                #endregion
                #region//RecEdgeToEdit == 1
                if (RecEdgeToEdit == 1)
                {
                    tx1 = TeeShape[SelectedFlangedWallShape].PointX2D[1];
                    ty1 = TempY;
                    tx2 = TeeShape[SelectedFlangedWallShape].PointX2D[2];
                    ty2 = TempY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = TeeShape[SelectedFlangedWallShape].PointX2D[2];
                    ty1 = TempY;
                    tx2 = TeeShape[SelectedFlangedWallShape].PointX2D[2];
                    ty2 = TeeShape[SelectedFlangedWallShape].PointY2D[5];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = TeeShape[SelectedFlangedWallShape].PointX2D[2];
                    ty1 = TeeShape[SelectedFlangedWallShape].PointY2D[5];
                    tx2 = TeeShape[SelectedFlangedWallShape].PointX2D[1];
                    ty2 = TeeShape[SelectedFlangedWallShape].PointY2D[5];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = TeeShape[SelectedFlangedWallShape].PointX2D[1];
                    ty1 = TeeShape[SelectedFlangedWallShape].PointY2D[5];
                    tx2 = TeeShape[SelectedFlangedWallShape].PointX2D[1];
                    ty2 = TempY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    Tx[1] = TeeShape[SelectedFlangedWallShape].PointXReal[1];
                    Ty[1] = Math.Round((startY2d - TempY) / Zoom2d, 3);
                    Tx[2] = TeeShape[SelectedFlangedWallShape].PointXReal[2];
                    Ty[2] = Math.Round((startY2d - TempY) / Zoom2d, 3);
                    Tx[3] = TeeShape[SelectedFlangedWallShape].PointXReal[2];
                    Ty[3] = TeeShape[SelectedFlangedWallShape].PointYReal[5];
                    Tx[4] = TeeShape[SelectedFlangedWallShape].PointXReal[1];
                    Ty[4] = TeeShape[SelectedFlangedWallShape].PointYReal[5];
                }
                #endregion
                #region //RecEdgeToEdit == 3
                if (RecEdgeToEdit == 3)
                {
                    tx1 = TeeShape[SelectedTeeShape].PointX2D[1];
                    ty1 = TeeShape[SelectedTeeShape].PointY2D[1];
                    tx2 = TeeShape[SelectedTeeShape].PointX2D[2];
                    ty2 = TeeShape[SelectedTeeShape].PointY2D[1];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = TeeShape[SelectedTeeShape].PointX2D[2];
                    ty1 = TeeShape[SelectedTeeShape].PointY2D[2];
                    tx2 = TeeShape[SelectedTeeShape].PointX2D[2];
                    ty2 = TempY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = TeeShape[SelectedTeeShape].PointX2D[2];
                    ty1 = TempY;
                    tx2 = TeeShape[SelectedTeeShape].PointX2D[1];
                    ty2 = TempY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = TeeShape[SelectedTeeShape].PointX2D[1];
                    ty1 = TempY;
                    tx2 = TeeShape[SelectedTeeShape].PointX2D[1];
                    ty2 = TeeShape[SelectedTeeShape].PointY2D[1];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    Tx[1] = TeeShape[SelectedTeeShape].PointXReal[1];
                    Ty[1] = TeeShape[SelectedTeeShape].PointYReal[1];
                    Tx[2] = TeeShape[SelectedTeeShape].PointXReal[2];
                    Ty[2] = TeeShape[SelectedTeeShape].PointYReal[2];
                    Tx[3] = TeeShape[SelectedTeeShape].PointXReal[2];
                    Ty[3] = Math.Round((startY2d - TempY) / Zoom2d, 3);
                    Tx[4] = TeeShape[SelectedTeeShape].PointXReal[1];
                    Ty[4] = Math.Round((startY2d - TempY) / Zoom2d, 3);
                }
                #endregion
                #region//RecEdgeToEdit == 2
                if (RecEdgeToEdit == 2)
                {
                    tx1 = TeeShape[SelectedTeeShape].PointX2D[1];
                    ty1 = TeeShape[SelectedTeeShape].PointY2D[1];
                    tx2 = TempX;
                    ty2 = TeeShape[SelectedTeeShape].PointY2D[1];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = TempX;
                    ty1 = TeeShape[SelectedTeeShape].PointY2D[2];
                    tx2 = TempX;
                    ty2 = TeeShape[SelectedTeeShape].PointY2D[5];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = TempX;
                    ty1 = TeeShape[SelectedTeeShape].PointY2D[5];
                    tx2 = TeeShape[SelectedTeeShape].PointX2D[1];
                    ty2 = TeeShape[SelectedTeeShape].PointY2D[5];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = TeeShape[SelectedTeeShape].PointX2D[1];
                    ty1 = TeeShape[SelectedTeeShape].PointY2D[1];
                    tx2 = TeeShape[SelectedTeeShape].PointX2D[1];
                    ty2 = TeeShape[SelectedTeeShape].PointY2D[5];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    Tx[1] = TeeShape[SelectedTeeShape].PointXReal[1];
                    Ty[1] = TeeShape[SelectedTeeShape].PointYReal[1];
                    Tx[2] = Math.Round((TempX - startX2d) / Zoom2d, 3);
                    Ty[2] = TeeShape[SelectedTeeShape].PointYReal[1];
                    Tx[3] = Math.Round((TempX - startX2d) / Zoom2d, 3);
                    Ty[3] = TeeShape[SelectedTeeShape].PointYReal[5];
                    Tx[4] = TeeShape[SelectedTeeShape].PointXReal[1];
                    Ty[4] = TeeShape[SelectedTeeShape].PointYReal[5];
                }
                #endregion
                #region//RecEdgeToEdit == 4
                if (RecEdgeToEdit == 4)
                {
                    tx1 = TempX;
                    ty1 = TeeShape[SelectedTeeShape].PointY2D[1];
                    tx2 = TeeShape[SelectedTeeShape].PointX2D[2];
                    ty2 = TeeShape[SelectedTeeShape].PointY2D[1];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = TeeShape[SelectedTeeShape].PointX2D[2];
                    ty1 = TeeShape[SelectedTeeShape].PointY2D[2];
                    tx2 = TeeShape[SelectedTeeShape].PointX2D[2];
                    ty2 = TeeShape[SelectedTeeShape].PointY2D[5];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = TeeShape[SelectedTeeShape].PointX2D[2];
                    ty1 = TeeShape[SelectedTeeShape].PointY2D[5];
                    tx2 = TempX;
                    ty2 = TeeShape[SelectedTeeShape].PointY2D[5];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = TempX;
                    ty1 = TeeShape[SelectedTeeShape].PointY2D[1];
                    tx2 = TempX;
                    ty2 = TeeShape[SelectedTeeShape].PointY2D[5];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    Tx[1] = Math.Round((TempX - startX2d) / Zoom2d, 3);
                    Ty[1] = TeeShape[SelectedTeeShape].PointYReal[1];
                    Tx[2] = TeeShape[SelectedTeeShape].PointXReal[2];
                    Ty[2] = TeeShape[SelectedTeeShape].PointYReal[2];
                    Tx[3] = TeeShape[SelectedTeeShape].PointXReal[2];
                    Ty[3] = TeeShape[SelectedTeeShape].PointYReal[5];
                    Tx[4] = Math.Round((TempX - startX2d) / Zoom2d, 3);
                    Ty[4] = TeeShape[SelectedTeeShape].PointYReal[5];
                }
                #endregion
            }
        }
        private void EditTeeShape()
        {
            int L = SelectedTeeShape;
            if (RecEdgeToEdit == 2 || RecEdgeToEdit == 4)
            {
                TeeShape[L].FlangWidth =Math.Round ( Math.Abs(Tx[2] - Tx[1]),3);
            }
            if (RecEdgeToEdit == 1 || RecEdgeToEdit == 3)
            {
                TeeShape[L].Height = Math.Round(Math.Abs(Ty[1] - Ty[4]), 3);
            }
            double Height = TeeShape[L].Height;
            double FlangWidth = TeeShape[L].FlangWidth;
            double WebThickness = TeeShape[L].WebThickness;
            double FlangThickness = TeeShape[L].FlangThickness;
            double WebHeight = (Height - FlangThickness);
            double aria1 = FlangWidth * FlangThickness;
            double aria2 = WebHeight * WebThickness;
            double aria = aria1 + aria2;
            double way = ((aria1 * FlangThickness / 2) + aria2 * (FlangThickness + (WebHeight / 2))) / aria;
            double way1 = way - FlangThickness;
            double way2 = Height - way;
            double CenterX = (Tx[2] + Tx[1]) / 2;
            double CenterY = Ty[1] - way;
            TeeShape[L].CenterX= Math.Round (CenterX,3);
            TeeShape[L].CenterY = Math.Round(CenterY, 3);
            TeeShape[L].PointXReal[1] = CenterX - FlangWidth / 2;
            TeeShape[L].PointXReal[2] = CenterX + FlangWidth / 2;
            TeeShape[L].PointXReal[3] = CenterX + FlangWidth / 2;
            TeeShape[L].PointXReal[4] = CenterX + WebThickness / 2;
            TeeShape[L].PointXReal[5] = CenterX + WebThickness / 2;
            TeeShape[L].PointXReal[6] = CenterX - WebThickness / 2;
            TeeShape[L].PointXReal[7] = CenterX - WebThickness / 2;
            TeeShape[L].PointXReal[8] = CenterX - FlangWidth / 2;
            TeeShape[L].PointYReal[1] = CenterY + way;
            TeeShape[L].PointYReal[2] = CenterY + way;
            TeeShape[L].PointYReal[3] = CenterY + way1;
            TeeShape[L].PointYReal[4] = CenterY + way1;
            TeeShape[L].PointYReal[5] = CenterY - way2;
            TeeShape[L].PointYReal[6] = CenterY - way2;
            TeeShape[L].PointYReal[7] = CenterY + way1;
            TeeShape[L].PointYReal[8] = CenterY + way1;
            TeeShapes emp0 = new TeeShapes();
            emp0 = TeeShape[L];

            #region//يوجد تسليح
            if (TeeShape[L].HasReinforcment == 1)
            {
                SelectedRecBar1 = TeeShape[L].ApplyedToRecBars[1];
                SelectedRecBar2 = TeeShape[L].ApplyedToRecBars[2];
                RecLineBars emp0R1 = new RecLineBars();
                emp0R1 = RecLineBar[SelectedRecBar1];
                RecLineBars emp0R2 = new RecLineBars();
                emp0R2 = RecLineBar[SelectedRecBar2];
                double X1 = 0;
                double Y1 = 0;
                double length = 0;
                int NBAR = 0;
                int diameter = 0;
                int diameter1 = 0;
                double DIS = 0;
                double TheDistance = 0;
                int TheBars = 0;
                #region//تحميل مستطيلات التسليح
                #region//حذف وترتيب
            startloop: { }
                for (int k = 1; k < BarsNumber + 1; k++)
                {
                    if (Bar[k].InREC == SelectedRecBar1)
                    {
                        BarsNumber = BarsNumber - 1;
                        for (int l = k; l < BarsNumber + 1; l++)
                        {
                            Bar[l] = Bar[l + 1];
                        }
                        if (k <= BarsNumber) goto startloop;
                    }
                }
                for (int k = 1; k < BarsNumber + 1; k++)
                {
                    if (Bar[k].InREC > SelectedRecBar1)
                    {
                        Bar[k].InREC = Bar[k].InREC - 1;
                    }
                }
                for (int k = SelectedRecBar1; k < RecBarsNumber; k++)
                {
                    RecLineBar[k] = RecLineBar[k + 1];
                }
                for (int k = SelectedTeeShape; k < TeeShapeNumber; k++)//مضاف
                {
                    TeeShape[k] = TeeShape[k + 1];
                }
                for (int k = 1; k < RecBarsNumber; k++)//مضاف
                {
                    if (RecLineBar[k].InTeeShape > SelectedTeeShape)
                    {
                        RecLineBar[k].InTeeShape = RecLineBar[k].InTeeShape - 1;
                    }
                }
                for (int k = 1; k < RecShapeNumber + 1; k++)//مضاف///////////////////////
                {
                    if (RecShape[k].ApplyedToRecBars > SelectedRecBar1)
                    {
                        RecShape[k].ApplyedToRecBars = RecShape[k].ApplyedToRecBars - 1;
                    }
                }
                for (int k = 1; k < TeeShapeNumber; k++)//مضاف
                {
                    if (TeeShape[k].ApplyedToRecBars[1] > SelectedRecBar1)
                    {
                        TeeShape[k].ApplyedToRecBars[1] = TeeShape[k].ApplyedToRecBars[1] - 1;
                        TeeShape[k].ApplyedToRecBars[2] = TeeShape[k].ApplyedToRecBars[2] - 1;
                    }
                }
                for (int k = 1; k < FlangedWallShapeNumber+1; k++)//مضاف
                {
                    if (FlangedWallShape[k].ApplyedToRecBars[1] > SelectedRecBar1)
                    {
                        FlangedWallShape[k].ApplyedToRecBars[1] = FlangedWallShape[k].ApplyedToRecBars[1] - 1;
                        FlangedWallShape[k].ApplyedToRecBars[2] = FlangedWallShape[k].ApplyedToRecBars[2] - 1;
                        FlangedWallShape[k].ApplyedToRecBars[3] = FlangedWallShape[k].ApplyedToRecBars[3] - 1;
                    }
                }
                #endregion
                if (SelectedRecBar2 > SelectedRecBar1) SelectedRecBar2 = SelectedRecBar2 - 1;
                SelectedTeeShape = TeeShapeNumber;
                SelectedRecBar1 = RecBarsNumber;
                #region//اسوارة التسليح
                RecLineBars emp = new RecLineBars();
                emp.InTeeShape = SelectedTeeShape;
                emp.InRecShape = 0;
                emp.InFlangedWallShape = 0;
                emp.Width = emp0.PointXReal[2] - emp0.PointXReal[1] - emp0.ED2CoverR - emp0.ED4CoverR;
                emp.Height = emp0.PointYReal[2] - emp0.PointYReal[3] - emp0.ED1CoverR - emp0.ED3CoverR;
                emp.CenterX = emp0.PointXReal[1] + emp0.ED4CoverR + emp.Width / 2;
                emp.CenterY = emp0.PointYReal[1] - emp0.ED1CoverR - emp.Height / 2;
                emp.TIEDR = emp0R1.TIEDR;
                emp.CORDR[1] = emp0R1.CORDR[1];
                emp.CORDR[2] = emp0R1.CORDR[2];
                emp.CORDR[3] = emp0R1.CORDR[3];
                emp.CORDR[4] = emp0R1.CORDR[4];

                emp.EDDR[1] = emp0R1.EDDR[1];
                emp.EDDistance[1] = emp0R1.EDDistance[1];
                diameter1 = emp.TIEDR;
                length = emp.Width;
                TheDistance = emp.EDDistance[1];
                TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                emp.EDBarsNumbers[1] = TheBars;
                emp.EDX1R[1] = emp.CenterX - emp.Width / 2;
                emp.EDY1R[1] = emp.CenterY + emp.Height / 2;
                emp.EDX2R[1] = emp.CenterX + emp.Width / 2;
                emp.EDY2R[1] = emp.CenterY + emp.Height / 2;

                emp.EDDR[2] = emp0R1.EDDR[2];
                emp.EDDistance[2] = emp0R1.EDDistance[2];
                diameter1 = emp.TIEDR;
                length = emp.Height;
                TheDistance = emp.EDDistance[2];
                TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                emp.EDBarsNumbers[2] = TheBars;
                emp.EDX1R[2] = emp.CenterX + emp.Width / 2;
                emp.EDY1R[2] = emp.CenterY + emp.Height / 2;
                emp.EDX2R[2] = emp.CenterX + emp.Width / 2;
                emp.EDY2R[2] = emp.CenterY - emp.Height / 2;

                emp.EDDR[3] = emp0R1.EDDR[3];
                emp.EDDistance[3] = emp0R1.EDDistance[3];
                diameter1 = emp.TIEDR;
                length = emp.Width;
                TheDistance = emp.EDDistance[3];
                TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                emp.EDBarsNumbers[3] = TheBars;
                emp.EDX1R[3] = emp.CenterX + emp.Width / 2;
                emp.EDY1R[3] = emp.CenterY - emp.Height / 2;
                emp.EDX2R[3] = emp.CenterX - emp.Width / 2;
                emp.EDY2R[3] = emp.CenterY - emp.Height / 2;

                emp.EDDR[4] = emp0R1.EDDR[4];
                emp.EDDistance[4] = emp0R1.EDDistance[4];
                diameter1 = emp.TIEDR;
                length = emp.Height;
                TheDistance = emp.EDDistance[4];
                TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                emp.EDBarsNumbers[4] = TheBars;
                emp.EDX1R[4] = emp.CenterX - emp.Width / 2;
                emp.EDY1R[4] = emp.CenterY - emp.Height / 2;
                emp.EDX2R[4] = emp.CenterX - emp.Width / 2;
                emp.EDY2R[4] = emp.CenterY + emp.Height / 2;
                #endregion
                #region//القضبان الداخلية
                for (int s = 1; s < 5; s++)
                {
                    int kl1 = 0;
                    int kl2 = 0;
                    if (s == 1 || s == 3)
                    {
                        length = emp.Width;
                        kl1 = 1;
                        kl2 = 0;
                    }
                    if (s == 2 || s == 4)
                    {
                        length = emp.Height;
                        kl1 = 0;
                        kl2 = 1;
                    }
                    NBAR = emp.EDBarsNumbers[s];
                    diameter = emp.EDDR[s];
                    DIS = Math.Round((length - 2 * diameter1) / (NBAR + 1), 3);
                    X1 = emp.EDX1R[s];
                    Y1 = emp.EDY1R[s];
                    for (int k = 1; k < NBAR + 1; k++)
                    {
                        BarsNumber = BarsNumber + 1;
                        Bars emp1 = new Bars();
                        emp1.XR = Math.Round(X1 + kl1 * kx[s] * (k * DIS) + kx[s] * diameter1 + kl2 * ky[s] * (diameter / 2), 3);
                        emp1.YR = Math.Round(Y1 + kl2 * ky[s] * (k * DIS) + ky[s] * diameter1 + kl1 * ky[s] * (diameter / 2), 3);
                        emp1.DR = diameter;
                        emp1.Selected = 0;
                        emp1.InLine = 0;
                        emp1.Type = 3;
                        emp1.InED = s;
                        emp1.InREC = RecBarsNumber;
                        emp1.Corner = 0;
                        emp1.InCircle = 0;
                        Bar[BarsNumber] = emp1;
                    }
                }
                #endregion
                #region//قضبان الزوايا
                for (int k = 1; k < 5; k++)
                {
                    BarsNumber = BarsNumber + 1;
                    Bars emp1 = new Bars();
                    diameter = emp.CORDR[k];
                    if (k == 1)
                    {
                        emp1.XR = Math.Round(emp0.PointXReal[1] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED4CoverR, 3);
                        emp1.YR = Math.Round(emp0.PointYReal[1] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED1CoverR, 3);
                    }
                    if (k == 2)
                    {
                        emp1.XR = Math.Round(emp0.PointXReal[2] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED2CoverR, 3);
                        emp1.YR = Math.Round(emp0.PointYReal[2] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED1CoverR, 3);
                    }
                    if (k == 3)
                    {
                        emp1.XR = Math.Round(emp0.PointXReal[3] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED2CoverR, 3);
                        emp1.YR = Math.Round(emp0.PointYReal[3] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED3CoverR, 3);
                    }
                    if (k == 4)
                    {
                        emp1.XR = Math.Round(emp0.PointXReal[8] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED4CoverR, 3);
                        emp1.YR = Math.Round(emp0.PointYReal[8] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED3CoverR, 3);
                    }
                    emp1.DR = diameter;
                    emp1.Selected = 0;
                    emp1.InLine = 0;
                    emp1.Type = 4;
                    emp1.InED = 0;
                    emp1.InCircle = 0;
                    emp1.Corner = k;
                    emp1.InREC = RecBarsNumber;
                    Bar[BarsNumber] = emp1;
                }
                #endregion
                RecLineBar[RecBarsNumber] = emp;
                #endregion
                #region//تحميل مستطيلات التسليح
                #region//حذف وترتيب
            startloopL: { }
                for (int k = 1; k < BarsNumber + 1; k++)
                {
                    if (Bar[k].InREC == SelectedRecBar2)
                    {
                        BarsNumber = BarsNumber - 1;
                        for (int l = k; l < BarsNumber + 1; l++)
                        {
                            Bar[l] = Bar[l + 1];
                        }
                        if (k <= BarsNumber) goto startloopL;
                    }
                }
                for (int k = 1; k < BarsNumber + 1; k++)
                {
                    if (Bar[k].InREC > SelectedRecBar2)
                    {
                        Bar[k].InREC = Bar[k].InREC - 1;
                    }
                }
                for (int k = SelectedRecBar2; k < RecBarsNumber; k++)
                {
                    RecLineBar[k] = RecLineBar[k + 1];
                }
                for (int k = SelectedTeeShape; k < TeeShapeNumber; k++)//مضاف
                {
                    TeeShape[k] = TeeShape[k + 1];
                }
                for (int k = 1; k < RecBarsNumber; k++)//مضاف
                {
                    if (RecLineBar[k].InTeeShape > SelectedTeeShape)
                    {
                        RecLineBar[k].InTeeShape = RecLineBar[k].InTeeShape - 1;
                    }
                }
                for (int k = 1; k < RecShapeNumber + 1; k++)//مضاف///////////////////////
                {
                    if (RecShape[k].ApplyedToRecBars > SelectedRecBar2)
                    {
                        RecShape[k].ApplyedToRecBars = RecShape[k].ApplyedToRecBars - 1;
                    }
                }
                for (int k = 1; k < TeeShapeNumber; k++)//مضاف
                {
                    if (TeeShape[k].ApplyedToRecBars[2] > SelectedRecBar2)
                    {
                        TeeShape[k].ApplyedToRecBars[2] = TeeShape[k].ApplyedToRecBars[2] - 1;
                        TeeShape[k].ApplyedToRecBars[1] = TeeShape[k].ApplyedToRecBars[1] - 1;
                    }
                }
                for (int k = 1; k < FlangedWallShapeNumber+1; k++)//مضاف
                {
                    if (FlangedWallShape[k].ApplyedToRecBars[2] > SelectedRecBar2)
                    {
                        FlangedWallShape[k].ApplyedToRecBars[2] = FlangedWallShape[k].ApplyedToRecBars[2] - 1;
                        FlangedWallShape[k].ApplyedToRecBars[1] = FlangedWallShape[k].ApplyedToRecBars[1] - 1;
                        FlangedWallShape[k].ApplyedToRecBars[3] = FlangedWallShape[k].ApplyedToRecBars[3] - 1;
                    }
                }
                #endregion
                SelectedTeeShape = TeeShapeNumber;
                SelectedRecBar2 = RecBarsNumber;
                #region//اسوارة التسليح
                emp = new RecLineBars();
                emp.InTeeShape = SelectedTeeShape;
                emp.InFlangedWallShape = 0;
                emp.InRecShape = 0;
                emp.Width = emp0.PointXReal[4] - emp0.PointXReal[7] - emp0.ED2cCoverR - emp0.ED4cCoverR;
                emp.Height = emp0.PointYReal[1] - emp0.PointYReal[5] - emp0.ED1cCoverR - emp0.ED3cCoverR;
                emp.CenterX = emp0.PointXReal[7] + emp0.ED4cCoverR + emp.Width / 2;
                emp.CenterY = emp0.PointYReal[1] - emp0.ED1cCoverR - emp.Height / 2;
                emp.TIEDR = emp0R2.TIEDR;
                emp.CORDR[1] = emp0R2.CORDR[1];
                emp.CORDR[2] = emp0R2.CORDR[2];
                emp.CORDR[3] = emp0R2.CORDR[3];
                emp.CORDR[4] = emp0R2.CORDR[4];

                emp.EDDR[1] = emp0R2.EDDR[1];
                emp.EDDistance[1] = emp0R2.EDDistance[1];
                diameter1 = emp.TIEDR;
                length = emp.Width;
                TheDistance = emp.EDDistance[1];
                TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                emp.EDBarsNumbers[1] = TheBars;
                emp.EDX1R[1] = emp.CenterX - emp.Width / 2;
                emp.EDY1R[1] = emp.CenterY + emp.Height / 2;
                emp.EDX2R[1] = emp.CenterX + emp.Width / 2;
                emp.EDY2R[1] = emp.CenterY + emp.Height / 2;

                emp.EDDR[2] = emp0R2.EDDR[2];
                emp.EDDistance[2] = emp0R2.EDDistance[2];
                diameter1 = emp.TIEDR;
                length = emp.Height;
                TheDistance = emp.EDDistance[2];
                TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                emp.EDBarsNumbers[2] = TheBars;
                emp.EDX1R[2] = emp.CenterX + emp.Width / 2;
                emp.EDY1R[2] = emp.CenterY + emp.Height / 2;
                emp.EDX2R[2] = emp.CenterX + emp.Width / 2;
                emp.EDY2R[2] = emp.CenterY - emp.Height / 2;

                emp.EDDR[3] = emp0R2.EDDR[3];
                emp.EDDistance[3] = emp0R2.EDDistance[3];
                diameter1 = emp.TIEDR;
                length = emp.Width;
                TheDistance = emp.EDDistance[3];
                TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                emp.EDBarsNumbers[3] = TheBars;
                emp.EDX1R[3] = emp.CenterX + emp.Width / 2;
                emp.EDY1R[3] = emp.CenterY - emp.Height / 2;
                emp.EDX2R[3] = emp.CenterX - emp.Width / 2;
                emp.EDY2R[3] = emp.CenterY - emp.Height / 2;

                emp.EDDR[4] = emp0R2.EDDR[4];
                emp.EDDistance[4] = emp0R2.EDDistance[4];
                diameter1 = emp.TIEDR;
                length = emp.Height;
                TheDistance = emp.EDDistance[4];
                TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                emp.EDBarsNumbers[4] = TheBars;
                emp.EDX1R[4] = emp.CenterX - emp.Width / 2;
                emp.EDY1R[4] = emp.CenterY - emp.Height / 2;
                emp.EDX2R[4] = emp.CenterX - emp.Width / 2;
                emp.EDY2R[4] = emp.CenterY + emp.Height / 2;
                #endregion
                #region//القضبان الداخلية
                diameter1 = emp.TIEDR;
                for (int s = 1; s < 5; s++)
                {
                    int kl1 = 0;
                    int kl2 = 0;
                    if (s == 1 || s == 3)
                    {
                        length = emp.Width;
                        kl1 = 1;
                        kl2 = 0;
                    }
                    if (s == 2 || s == 4)
                    {
                        length = emp.Height;
                        kl1 = 0;
                        kl2 = 1;
                    }
                    NBAR = emp.EDBarsNumbers[s];
                    diameter = emp.EDDR[s];
                    DIS = Math.Round((length - 2 * diameter1) / (NBAR + 1), 3);
                    X1 = emp.EDX1R[s];
                    Y1 = emp.EDY1R[s];
                    for (int k = 1; k < NBAR + 1; k++)
                    {
                        BarsNumber = BarsNumber + 1;
                        Bars emp1 = new Bars();
                        emp1.XR = Math.Round(X1 + kl1 * kx[s] * (k * DIS) + kx[s] * diameter1 + kl2 * ky[s] * (diameter / 2), 3);
                        emp1.YR = Math.Round(Y1 + kl2 * ky[s] * (k * DIS) + ky[s] * diameter1 + kl1 * ky[s] * (diameter / 2), 3);
                        emp1.DR = diameter;
                        emp1.Selected = 0;
                        emp1.InLine = 0;
                        emp1.Type = 3;
                        emp1.InED = s;
                        emp1.InREC = RecBarsNumber;
                        emp1.Corner = 0;
                        emp1.InCircle = 0;
                        Bar[BarsNumber] = emp1;
                    }
                }
                #endregion
                #region//قضبان الزوايا
                for (int k = 1; k < 5; k++)
                {
                    BarsNumber = BarsNumber + 1;
                    Bars emp1 = new Bars();
                    diameter = emp.CORDR[k];
                    if (k == 1)
                    {
                        emp1.XR = Math.Round(emp0.PointXReal[7] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED4cCoverR, 3);
                        emp1.YR = Math.Round(emp0.PointYReal[1] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED1cCoverR, 3);
                    }
                    if (k == 2)
                    {
                        emp1.XR = Math.Round(emp0.PointXReal[4] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED2cCoverR, 3);
                        emp1.YR = Math.Round(emp0.PointYReal[2] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED1cCoverR, 3);
                    }
                    if (k == 3)
                    {
                        emp1.XR = Math.Round(emp0.PointXReal[5] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED2cCoverR, 3);
                        emp1.YR = Math.Round(emp0.PointYReal[5] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED3cCoverR, 3);
                    }
                    if (k == 4)
                    {
                        emp1.XR = Math.Round(emp0.PointXReal[6] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED4cCoverR, 3);
                        emp1.YR = Math.Round(emp0.PointYReal[6] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED3cCoverR, 3);
                    }
                    emp1.DR = diameter;
                    emp1.Selected = 0;
                    emp1.InLine = 0;
                    emp1.Type = 4;
                    emp1.InED = 0;
                    emp1.InCircle = 0;
                    emp1.Corner = k;
                    emp1.InREC = RecBarsNumber;
                    Bar[BarsNumber] = emp1;
                }
                #endregion
                RecLineBar[RecBarsNumber] = emp;
                #endregion
                emp0.ApplyedToRecBars[1] = RecBarsNumber - 1;
                emp0.ApplyedToRecBars[2] = RecBarsNumber;
             }
            #endregion
           
            TeeShape[SelectedTeeShape] = emp0;
            RecEdgeToEdit = 0;
            ClickToEdit = 0;
            MakeTempFiles();
            Render2d();
            DrawForEditTeeShape();
        }

        private void DrawForEditFlangedWallShape()
        {
            Bitmap finalBmp = new Bitmap(BitampWidth2d, BitampHight2d);
            if (pictureBox2.Image != null)
            {
                pictureBox2.Image.Dispose();
                pictureBox2.Image = null;
            }
            pictureBox2.Image = finalBmp;
            if (SelectedFlangedWallShape != 0)
            {
                Graphics g = Graphics.FromImage(pictureBox2.Image);
                Pen pen = new Pen(Color.Navy, 3f);
                Pen pen1 = new Pen(Color.Blue, 5);
                Font drawFont = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
                SolidBrush drawBrush = new SolidBrush(Color.Blue);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                float[] dashValues = { 3, 3 };
                pen.DashPattern = dashValues;
                int thex = (TempX + LineMoveX1) / 2;
                int they = (TempY + LineMoveY1) / 2;
                int tx1 = 0;
                int ty1 = 0;
                int tx2 = 0;
                int ty2 = 0;
                int tx = 0;
                int ty = 0;
                int maxX = FlangedWallShape[SelectedFlangedWallShape].PointX2D[1];
                int minY = FlangedWallShape[SelectedFlangedWallShape].PointY2D[1];
                if (FlangedWallShape[SelectedFlangedWallShape].PointY2D[6] < minY) minY = FlangedWallShape[SelectedFlangedWallShape].PointY2D[6];
                int maxY = FlangedWallShape[SelectedFlangedWallShape].PointY2D[7];
                if (FlangedWallShape[SelectedFlangedWallShape].PointY2D[12] > maxY) maxY = FlangedWallShape[SelectedFlangedWallShape].PointY2D[12];
                #region// RecEdgeToEdit == 0
                if (RecEdgeToEdit == 0)
                {
                    tx1 = FlangedWallShape[SelectedFlangedWallShape].PointX2D[1];////
                    ty1 = minY;
                    tx2 = FlangedWallShape[SelectedFlangedWallShape].PointX2D[6];
                    ty2 = minY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = FlangedWallShape[SelectedFlangedWallShape].PointX2D[6];/////
                    ty1 = minY;
                    tx2 = FlangedWallShape[SelectedFlangedWallShape].PointX2D[6];
                    ty2 = maxY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = FlangedWallShape[SelectedFlangedWallShape].PointX2D[6];//////
                    ty1 = maxY;
                    tx2 = FlangedWallShape[SelectedFlangedWallShape].PointX2D[12];
                    ty2 = maxY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = FlangedWallShape[SelectedFlangedWallShape].PointX2D[12];//////
                    ty1 = maxY;
                    tx2 = FlangedWallShape[SelectedFlangedWallShape].PointX2D[12];
                    ty2 = minY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                }
                #endregion
                #region//RecEdgeToEdit == 1
                if (RecEdgeToEdit == 1)
                {
                    tx1 = FlangedWallShape[SelectedFlangedWallShape].PointX2D[1];
                    ty1 = TempY;
                    tx2 = FlangedWallShape[SelectedFlangedWallShape].PointX2D[6];
                    ty2 = TempY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = FlangedWallShape[SelectedFlangedWallShape].PointX2D[7];
                    ty1 = TempY;
                    tx2 = FlangedWallShape[SelectedFlangedWallShape].PointX2D[7];
                    ty2 = maxY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = FlangedWallShape[SelectedFlangedWallShape].PointX2D[7];
                    ty1 = maxY;
                    tx2 = FlangedWallShape[SelectedFlangedWallShape].PointX2D[12];
                    ty2 = maxY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = FlangedWallShape[SelectedFlangedWallShape].PointX2D[12];
                    ty1 = maxY;
                    tx2 = FlangedWallShape[SelectedFlangedWallShape].PointX2D[12];
                    ty2 = TempY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    Tx[1] = FlangedWallShape[SelectedFlangedWallShape].PointXReal[1];
                    Ty[1] = Math.Round((startY2d - TempY) / Zoom2d, 3);
                    Tx[2] = FlangedWallShape[SelectedFlangedWallShape].PointXReal[6];
                    Ty[2] = Math.Round((startY2d - TempY) / Zoom2d, 3);
                    Tx[3] = FlangedWallShape[SelectedFlangedWallShape].PointXReal[7];
                    Ty[3] = maxY;
                    Tx[4] = FlangedWallShape[SelectedFlangedWallShape].PointXReal[12];
                    Ty[4] = maxY;
                }
                #endregion
                #region //RecEdgeToEdit == 3
                if (RecEdgeToEdit == 3)
                {
                    tx1 = FlangedWallShape[SelectedFlangedWallShape].PointX2D[1];
                    ty1 = minY;
                    tx2 = FlangedWallShape[SelectedFlangedWallShape].PointX2D[6];
                    ty2 = minY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = FlangedWallShape[SelectedFlangedWallShape].PointX2D[6];
                    ty1 = minY;
                    tx2 = FlangedWallShape[SelectedFlangedWallShape].PointX2D[6];
                    ty2 = TempY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = FlangedWallShape[SelectedFlangedWallShape].PointX2D[6];
                    ty1 = TempY;
                    tx2 = FlangedWallShape[SelectedFlangedWallShape].PointX2D[1];
                    ty2 = TempY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = FlangedWallShape[SelectedFlangedWallShape].PointX2D[1];
                    ty1 = TempY;
                    tx2 = FlangedWallShape[SelectedFlangedWallShape].PointX2D[1];
                    ty2 = minY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    Tx[1] = FlangedWallShape[SelectedFlangedWallShape].PointXReal[1];
                    Ty[1] = minY;
                    Tx[2] = FlangedWallShape[SelectedFlangedWallShape].PointXReal[6];
                    Ty[2] = minY;
                    Tx[3] = FlangedWallShape[SelectedFlangedWallShape].PointXReal[7];
                    Ty[3] = Math.Round((startY2d - TempY) / Zoom2d, 3);
                    Tx[4] = FlangedWallShape[SelectedFlangedWallShape].PointXReal[12];
                    Ty[4] = Math.Round((startY2d - TempY) / Zoom2d, 3);
                }
                #endregion
                #region//RecEdgeToEdit == 2
                if (RecEdgeToEdit == 2)
                {
                    tx1 = FlangedWallShape[SelectedFlangedWallShape].PointX2D[1];
                    ty1 = minY;
                    tx2 = TempX;
                    ty2 = minY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = TempX;
                    ty1 = minY;
                    tx2 = TempX;
                    ty2 = maxY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = TempX;
                    ty1 = maxY;
                    tx2 = FlangedWallShape[SelectedFlangedWallShape].PointX2D[1];
                    ty2 = maxY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = FlangedWallShape[SelectedFlangedWallShape].PointX2D[1];
                    ty1 = maxY;
                    tx2 = FlangedWallShape[SelectedFlangedWallShape].PointX2D[1];
                    ty2 = minY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    Tx[1] = FlangedWallShape[SelectedFlangedWallShape].PointXReal[1];
                    Ty[1] = minY;
                    Tx[2] = Math.Round((TempX - startX2d) / Zoom2d, 3);
                    Ty[2] = minY;
                    Tx[3] = Math.Round((TempX - startX2d) / Zoom2d, 3);
                    Ty[3] = maxY;
                    Tx[4] = FlangedWallShape[SelectedFlangedWallShape].PointXReal[12];
                    Ty[4] = maxY;
                }
                #endregion
                #region//RecEdgeToEdit == 4
                if (RecEdgeToEdit == 4)
                {
                    tx1 = TempX;
                    ty1 = minY;
                    tx2 = FlangedWallShape[SelectedFlangedWallShape].PointX2D[6];
                    ty2 = minY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = FlangedWallShape[SelectedFlangedWallShape].PointX2D[6];
                    ty1 = minY;
                    tx2 = FlangedWallShape[SelectedFlangedWallShape].PointX2D[6];
                    ty2 = maxY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = FlangedWallShape[SelectedFlangedWallShape].PointX2D[6];
                    ty1 = maxY;
                    tx2 = TempX;
                    ty2 = maxY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = TempX;
                    ty1 = maxY;
                    tx2 = TempX;
                    ty2 = minY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    Tx[1] = Math.Round((TempX - startX2d) / Zoom2d, 3);
                    Ty[1] = minY;
                    Tx[2] = FlangedWallShape[SelectedFlangedWallShape].PointXReal[6];
                    Ty[2] = minY;
                    Tx[3] = FlangedWallShape[SelectedFlangedWallShape].PointXReal[7];
                    Ty[3] = maxY;
                    Tx[4] = Math.Round((TempX - startX2d) / Zoom2d, 3);
                    Ty[4] = maxY;
                }
                #endregion
            }
        }
        private void EditFlangedWallShape()
        {
            int L = SelectedFlangedWallShape;
            if (RecEdgeToEdit == 2 || RecEdgeToEdit == 4)
            {
                FlangedWallShape[L].Length = Math.Abs(Tx[2] - Tx[1]);
            }
            //FlangedWallShape[SelectedFlangedWallShape].StemWidth = Math.Abs(Ty[2] - Ty[3]);
            if (RecEdgeToEdit == 1)
            {
                if (Ty[1] < FlangedWallShape[L].PointYReal[3]) Ty[1] = FlangedWallShape[L].PointYReal[3];
                FlangedWallShape[L].LeftFlangWidth = Math.Abs(Ty[1] - FlangedWallShape[L].PointYReal[12]);
                FlangedWallShape[L].RightFlangWidth = Math.Abs(Ty[1] - FlangedWallShape[L].PointYReal[7]);
                double width = FlangedWallShape[L].LeftFlangWidth;
                double widthstem = FlangedWallShape[L].StemWidth;
                double kima = (width - widthstem) / 2;
                double kima1 = FlangedWallShape[L].PointYReal[10] - FlangedWallShape[L].PointYReal[11];
                double LeftEccen = kima - kima1;
                FlangedWallShape[L].LeftFlangEccen = LeftEccen;

                width = FlangedWallShape[L].RightFlangWidth;
                kima = (width - widthstem) / 2;
                kima1 = FlangedWallShape[L].PointYReal[9] - FlangedWallShape[L].PointYReal[8];
                double RightEccen = kima - kima1;
                FlangedWallShape[L].RightFlangEccen = RightEccen;
            }
            if (RecEdgeToEdit == 3)
            {
                if (Ty[3] > FlangedWallShape[L].PointYReal[9]) Ty[3] = FlangedWallShape[L].PointYReal[9];
                FlangedWallShape[L].LeftFlangWidth = Math.Abs(Ty[3] - FlangedWallShape[L].PointYReal[1]);
                FlangedWallShape[L].RightFlangWidth = Math.Abs(Ty[3] - FlangedWallShape[L].PointYReal[6]);
                double width = FlangedWallShape[L].LeftFlangWidth;
                double widthstem = FlangedWallShape[L].StemWidth;
                double kima = (width - widthstem) / 2;
                double kima1 = FlangedWallShape[L].PointYReal[2] - FlangedWallShape[L].PointYReal[3];
                FlangedWallShape[L].LeftFlangEccen = -kima + kima1;
                kima1 = FlangedWallShape[L].PointYReal[5] - FlangedWallShape[L].PointYReal[4];
                FlangedWallShape[L].RightFlangEccen = -kima + kima1;
            }

            FlangedWallShape[L].CenterX = (Tx[2] + Tx[1]) / 2;
            // FlangedWallShape[L].CenterY = (Ty[2] + Ty[1]) / 2;
            double CenterX = FlangedWallShape[L].CenterX;
            double CenterY = FlangedWallShape[L].CenterY;
            double Length = FlangedWallShape[L].Length;
            double LeftFlangLength = FlangedWallShape[L].LeftFlangLength;
            double RightFlangLength = FlangedWallShape[L].RightFlangLength;
            double LeftFlangWidth = FlangedWallShape[L].LeftFlangWidth;
            double RightFlangWidth = FlangedWallShape[L].RightFlangWidth;
            double StemWidth = FlangedWallShape[L].StemWidth;
            double LeftFlangEccen = FlangedWallShape[L].LeftFlangEccen;
            double RightFlangEccen = FlangedWallShape[L].RightFlangEccen;

            FlangedWallShape[L].PointXReal[1] = CenterX - Length / 2;
            FlangedWallShape[L].PointXReal[2] = CenterX - Length / 2 + LeftFlangLength;
            FlangedWallShape[L].PointXReal[3] = CenterX - Length / 2 + LeftFlangLength;
            FlangedWallShape[L].PointXReal[4] = CenterX + Length / 2 - RightFlangLength;
            FlangedWallShape[L].PointXReal[5] = CenterX + Length / 2 - RightFlangLength;
            FlangedWallShape[L].PointXReal[6] = CenterX + Length / 2;
            FlangedWallShape[L].PointXReal[7] = CenterX + Length / 2;
            FlangedWallShape[L].PointXReal[8] = CenterX + Length / 2 - RightFlangLength;
            FlangedWallShape[L].PointXReal[9] = CenterX + Length / 2 - RightFlangLength;
            FlangedWallShape[L].PointXReal[10] = CenterX - Length / 2 + LeftFlangLength;
            FlangedWallShape[L].PointXReal[11] = CenterX - Length / 2 + LeftFlangLength;
            FlangedWallShape[L].PointXReal[12] = CenterX - Length / 2;

            FlangedWallShape[L].PointYReal[1] = CenterY + LeftFlangWidth / 2 + LeftFlangEccen;
            FlangedWallShape[L].PointYReal[2] = CenterY + LeftFlangWidth / 2 + LeftFlangEccen;
            FlangedWallShape[L].PointYReal[3] = CenterY + StemWidth / 2;
            FlangedWallShape[L].PointYReal[4] = CenterY + StemWidth / 2;
            FlangedWallShape[L].PointYReal[5] = CenterY + RightFlangWidth / 2 + RightFlangEccen;
            FlangedWallShape[L].PointYReal[6] = CenterY + RightFlangWidth / 2 + RightFlangEccen;
            FlangedWallShape[L].PointYReal[7] = CenterY - RightFlangWidth / 2 + RightFlangEccen;
            FlangedWallShape[L].PointYReal[8] = CenterY - RightFlangWidth / 2 + RightFlangEccen;
            FlangedWallShape[L].PointYReal[9] = CenterY - StemWidth / 2;
            FlangedWallShape[L].PointYReal[10] = CenterY - StemWidth / 2;
            FlangedWallShape[L].PointYReal[11] = CenterY - LeftFlangWidth / 2 + LeftFlangEccen;
            FlangedWallShape[L].PointYReal[12] = CenterY - LeftFlangWidth / 2 + LeftFlangEccen;
            FlangedWalls emp0 = new FlangedWalls();
            emp0 = FlangedWallShape[L];
            #region//يوجد تسليح
            if (FlangedWallShape[L].HasReinforcment == 1)
            {
                SelectedRecBar1 = FlangedWallShape[L].ApplyedToRecBars[1];
                SelectedRecBar2 = FlangedWallShape[L].ApplyedToRecBars[2];
                SelectedRecBar3 = FlangedWallShape[L].ApplyedToRecBars[3];
                RecLineBars emp0R1 = new RecLineBars();
                emp0R1 = RecLineBar[SelectedRecBar1];
                RecLineBars emp0R2 = new RecLineBars();
                emp0R2 = RecLineBar[SelectedRecBar2];
                RecLineBars emp0R3 = new RecLineBars();
                emp0R3 = RecLineBar[SelectedRecBar3];
                double X1 = 0;
                double Y1 = 0;
                double length = 0;
                int NBAR = 0;
                int diameter = 0;
                int diameter1 = 0;
                double DIS = 0;
                double TheDistance = 0;
                int TheBars = 0;
                #region//تحميل مستطيلات التسليح
                #region//حذف وترتيب
            startloop: { }
                for (int k = 1; k < BarsNumber + 1; k++)
                {
                    if (Bar[k].InREC == SelectedRecBar1)
                    {
                        BarsNumber = BarsNumber - 1;
                        for (int l = k; l < BarsNumber + 1; l++)
                        {
                            Bar[l] = Bar[l + 1];
                        }
                        if (k <= BarsNumber) goto startloop;
                    }
                }
                for (int k = 1; k < BarsNumber + 1; k++)
                {
                    if (Bar[k].InREC > SelectedRecBar1)
                    {
                        Bar[k].InREC = Bar[k].InREC - 1;
                    }
                }
                for (int k = SelectedRecBar1; k < RecBarsNumber; k++)
                {
                    RecLineBar[k] = RecLineBar[k + 1];
                }
                for (int k = SelectedFlangedWallShape; k < FlangedWallShapeNumber; k++)//مضاف
                {
                    FlangedWallShape[k] = FlangedWallShape[k + 1];
                }
                for (int k = 1; k < RecBarsNumber; k++)//مضاف
                {
                    if (RecLineBar[k].InFlangedWallShape > SelectedFlangedWallShape)
                    {
                        RecLineBar[k].InFlangedWallShape = RecLineBar[k].InFlangedWallShape - 1;
                    }
                }
                for (int k = 1; k < RecShapeNumber + 1; k++)//مضاف///////////////////////
                {
                    if (RecShape[k].ApplyedToRecBars > SelectedRecBar1)
                    {
                        RecShape[k].ApplyedToRecBars = RecShape[k].ApplyedToRecBars - 1;
                    }
                }
                for (int k = 1; k < FlangedWallShapeNumber; k++)//مضاف
                {
                    if (FlangedWallShape[k].ApplyedToRecBars[1] > SelectedRecBar1)
                    {
                        FlangedWallShape[k].ApplyedToRecBars[1] = FlangedWallShape[k].ApplyedToRecBars[1] - 1;
                        FlangedWallShape[k].ApplyedToRecBars[2] = FlangedWallShape[k].ApplyedToRecBars[2] - 1;
                        FlangedWallShape[k].ApplyedToRecBars[3] = FlangedWallShape[k].ApplyedToRecBars[3] - 1;
                    }
                }
                #endregion
                if (SelectedRecBar2 > SelectedRecBar1) SelectedRecBar2 = SelectedRecBar2 - 1;
                if (SelectedRecBar3 > SelectedRecBar1) SelectedRecBar3 = SelectedRecBar3 - 1;
                SelectedFlangedWallShape = FlangedWallShapeNumber;
                SelectedRecBar1 = RecBarsNumber;
                #region//اسوارة التسليح
                RecLineBars emp = new RecLineBars();
                emp.InFlangedWallShape = SelectedFlangedWallShape;
                emp.InRecShape = 0;
                emp.Width = emp0.PointXReal[6] - emp0.PointXReal[1] - emp0.ED2CoverR - emp0.ED4CoverR;
                emp.Height = emp0.PointYReal[3] - emp0.PointYReal[10] - emp0.ED1CoverR - emp0.ED3CoverR;
                emp.CenterX = emp0.PointXReal[1] + emp0.ED4CoverR + emp.Width / 2;
                emp.CenterY = emp0.PointYReal[3] - emp0.ED1CoverR - emp.Height / 2;
                emp.TIEDR = emp0R1.TIEDR;
                emp.CORDR[1] = emp0R1.CORDR[1];
                emp.CORDR[2] = emp0R1.CORDR[2];
                emp.CORDR[3] = emp0R1.CORDR[3];
                emp.CORDR[4] = emp0R1.CORDR[4];

                emp.EDDR[1] = emp0R1.EDDR[1];
                emp.EDDistance[1] = emp0R1.EDDistance[1];
                diameter1 = emp.TIEDR;
                length = emp.Width;
                TheDistance = emp.EDDistance[1];
                TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                emp.EDBarsNumbers[1] = TheBars;
                emp.EDX1R[1] = emp.CenterX - emp.Width / 2;
                emp.EDY1R[1] = emp.CenterY + emp.Height / 2;
                emp.EDX2R[1] = emp.CenterX + emp.Width / 2;
                emp.EDY2R[1] = emp.CenterY + emp.Height / 2;

                emp.EDDR[2] = emp0R1.EDDR[2];
                emp.EDDistance[2] = emp0R1.EDDistance[2];
                diameter1 = emp.TIEDR;
                length = emp.Height;
                TheDistance = emp.EDDistance[2];
                TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                emp.EDBarsNumbers[2] = TheBars;
                emp.EDX1R[2] = emp.CenterX + emp.Width / 2;
                emp.EDY1R[2] = emp.CenterY + emp.Height / 2;
                emp.EDX2R[2] = emp.CenterX + emp.Width / 2;
                emp.EDY2R[2] = emp.CenterY - emp.Height / 2;

                emp.EDDR[3] = emp0R1.EDDR[3];
                emp.EDDistance[3] = emp0R1.EDDistance[3];
                diameter1 = emp.TIEDR;
                length = emp.Width;
                TheDistance = emp.EDDistance[3];
                TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                emp.EDBarsNumbers[3] = TheBars;
                emp.EDX1R[3] = emp.CenterX + emp.Width / 2;
                emp.EDY1R[3] = emp.CenterY - emp.Height / 2;
                emp.EDX2R[3] = emp.CenterX - emp.Width / 2;
                emp.EDY2R[3] = emp.CenterY - emp.Height / 2;

                emp.EDDR[4] = emp0R1.EDDR[4];
                emp.EDDistance[4] = emp0R1.EDDistance[4];
                diameter1 = emp.TIEDR;
                length = emp.Height;
                TheDistance = emp.EDDistance[4];
                TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                emp.EDBarsNumbers[4] = TheBars;
                emp.EDX1R[4] = emp.CenterX - emp.Width / 2;
                emp.EDY1R[4] = emp.CenterY - emp.Height / 2;
                emp.EDX2R[4] = emp.CenterX - emp.Width / 2;
                emp.EDY2R[4] = emp.CenterY + emp.Height / 2;
                #endregion
                #region//القضبان الداخلية
                for (int s = 1; s < 5; s++)
                {
                    int kl1 = 0;
                    int kl2 = 0;
                    if (s == 1 || s == 3)
                    {
                        length = emp.Width;
                        kl1 = 1;
                        kl2 = 0;
                    }
                    if (s == 2 || s == 4)
                    {
                        length = emp.Height;
                        kl1 = 0;
                        kl2 = 1;
                    }
                    NBAR = emp.EDBarsNumbers[s];
                    diameter = emp.EDDR[s];
                    DIS = Math.Round((length - 2 * diameter1) / (NBAR + 1), 3);
                    X1 = emp.EDX1R[s];
                    Y1 = emp.EDY1R[s];
                    for (int k = 1; k < NBAR + 1; k++)
                    {
                        BarsNumber = BarsNumber + 1;
                        Bars emp1 = new Bars();
                        emp1.XR = Math.Round(X1 + kl1 * kx[s] * (k * DIS) + kx[s] * diameter1 + kl2 * ky[s] * (diameter / 2), 3);
                        emp1.YR = Math.Round(Y1 + kl2 * ky[s] * (k * DIS) + ky[s] * diameter1 + kl1 * ky[s] * (diameter / 2), 3);
                        emp1.DR = diameter;
                        emp1.Selected = 0;
                        emp1.InLine = 0;
                        emp1.Type = 3;
                        emp1.InED = s;
                        emp1.InREC = RecBarsNumber;
                        emp1.Corner = 0;
                        emp1.InCircle = 0;
                        Bar[BarsNumber] = emp1;
                    }
                }
                #endregion
                #region//قضبان الزوايا
                for (int k = 1; k < 5; k++)
                {
                    BarsNumber = BarsNumber + 1;
                    Bars emp1 = new Bars();
                    diameter = emp.CORDR[k];
                    if (k == 1)
                    {
                        emp1.XR = Math.Round(emp0.PointXReal[1] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED4CoverR, 3);
                        emp1.YR = Math.Round(emp0.PointYReal[3] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED1CoverR, 3);
                    }
                    if (k == 2)
                    {
                        emp1.XR = Math.Round(emp0.PointXReal[6] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED2CoverR, 3);
                        emp1.YR = Math.Round(emp0.PointYReal[4] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED1CoverR, 3);
                    }
                    if (k == 3)
                    {
                        emp1.XR = Math.Round(emp0.PointXReal[7] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED2CoverR, 3);
                        emp1.YR = Math.Round(emp0.PointYReal[9] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED3CoverR, 3);
                    }
                    if (k == 4)
                    {
                        emp1.XR = Math.Round(emp0.PointXReal[12] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED4CoverR, 3);
                        emp1.YR = Math.Round(emp0.PointYReal[10] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED3CoverR, 3);
                    }
                    emp1.DR = diameter;
                    emp1.Selected = 0;
                    emp1.InLine = 0;
                    emp1.Type = 4;
                    emp1.InED = 0;
                    emp1.InCircle = 0;
                    emp1.Corner = k;
                    emp1.InREC = RecBarsNumber;
                    Bar[BarsNumber] = emp1;
                }
                #endregion
                RecLineBar[RecBarsNumber] = emp;
                #endregion
                #region//تحميل مستطيلات التسليح
                #region//حذف وترتيب
            startloopL: { }
                for (int k = 1; k < BarsNumber + 1; k++)
                {
                    if (Bar[k].InREC == SelectedRecBar2)
                    {
                        BarsNumber = BarsNumber - 1;
                        for (int l = k; l < BarsNumber + 1; l++)
                        {
                            Bar[l] = Bar[l + 1];
                        }
                        if (k <= BarsNumber) goto startloopL;
                    }
                }
                for (int k = 1; k < BarsNumber + 1; k++)
                {
                    if (Bar[k].InREC > SelectedRecBar2)
                    {
                        Bar[k].InREC = Bar[k].InREC - 1;
                    }
                }
                for (int k = SelectedRecBar2; k < RecBarsNumber; k++)
                {
                    RecLineBar[k] = RecLineBar[k + 1];
                }
                for (int k = SelectedFlangedWallShape; k < FlangedWallShapeNumber; k++)//مضاف
                {
                    FlangedWallShape[k] = FlangedWallShape[k + 1];
                }
                for (int k = 1; k < RecBarsNumber; k++)//مضاف
                {
                    if (RecLineBar[k].InFlangedWallShape > SelectedFlangedWallShape)
                    {
                        RecLineBar[k].InFlangedWallShape = RecLineBar[k].InFlangedWallShape - 1;
                    }
                }
                for (int k = 1; k < RecShapeNumber + 1; k++)//مضاف///////////////////////
                {
                    if (RecShape[k].ApplyedToRecBars > SelectedRecBar2)
                    {
                        RecShape[k].ApplyedToRecBars = RecShape[k].ApplyedToRecBars - 1;
                    }
                }
                for (int k = 1; k < FlangedWallShapeNumber; k++)//مضاف
                {
                    if (FlangedWallShape[k].ApplyedToRecBars[2] > SelectedRecBar2)
                    {
                        FlangedWallShape[k].ApplyedToRecBars[2] = FlangedWallShape[k].ApplyedToRecBars[2] - 1;
                        FlangedWallShape[k].ApplyedToRecBars[1] = FlangedWallShape[k].ApplyedToRecBars[1] - 1;
                        FlangedWallShape[k].ApplyedToRecBars[3] = FlangedWallShape[k].ApplyedToRecBars[3] - 1;
                    }
                }
                #endregion
                if (SelectedRecBar3 > SelectedRecBar2) SelectedRecBar3 = SelectedRecBar3 - 1;
                SelectedFlangedWallShape = FlangedWallShapeNumber;
                SelectedRecBar2 = RecBarsNumber;
                #region//اسوارة التسليح
                emp = new RecLineBars();
                emp.InFlangedWallShape = SelectedFlangedWallShape;
                emp.InRecShape = 0;
                emp.Width = emp0.PointXReal[2] - emp0.PointXReal[1] - emp0.ED2LCoverR - emp0.ED4LCoverR;
                emp.Height = emp0.PointYReal[1] - emp0.PointYReal[12] - emp0.ED1LCoverR - emp0.ED3LCoverR;
                emp.CenterX = emp0.PointXReal[1] + emp0.ED4LCoverR + emp.Width / 2;
                emp.CenterY = emp0.PointYReal[1] - emp0.ED1LCoverR - emp.Height / 2;
                emp.TIEDR = emp0R2.TIEDR;
                emp.CORDR[1] = emp0R2.CORDR[1];
                emp.CORDR[2] = emp0R2.CORDR[2];
                emp.CORDR[3] = emp0R2.CORDR[3];
                emp.CORDR[4] = emp0R2.CORDR[4];

                emp.EDDR[1] = emp0R2.EDDR[1];
                emp.EDDistance[1] = emp0R2.EDDistance[1];
                diameter1 = emp.TIEDR;
                length = emp.Width;
                TheDistance = emp.EDDistance[1];
                TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                emp.EDBarsNumbers[1] = TheBars;
                emp.EDX1R[1] = emp.CenterX - emp.Width / 2;
                emp.EDY1R[1] = emp.CenterY + emp.Height / 2;
                emp.EDX2R[1] = emp.CenterX + emp.Width / 2;
                emp.EDY2R[1] = emp.CenterY + emp.Height / 2;

                emp.EDDR[2] = emp0R2.EDDR[2];
                emp.EDDistance[2] = emp0R2.EDDistance[2];
                diameter1 = emp.TIEDR;
                length = emp.Height;
                TheDistance = emp.EDDistance[2];
                TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                emp.EDBarsNumbers[2] = TheBars;
                emp.EDX1R[2] = emp.CenterX + emp.Width / 2;
                emp.EDY1R[2] = emp.CenterY + emp.Height / 2;
                emp.EDX2R[2] = emp.CenterX + emp.Width / 2;
                emp.EDY2R[2] = emp.CenterY - emp.Height / 2;

                emp.EDDR[3] = emp0R2.EDDR[3];
                emp.EDDistance[3] = emp0R2.EDDistance[3];
                diameter1 = emp.TIEDR;
                length = emp.Width;
                TheDistance = emp.EDDistance[3];
                TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                emp.EDBarsNumbers[3] = TheBars;
                emp.EDX1R[3] = emp.CenterX + emp.Width / 2;
                emp.EDY1R[3] = emp.CenterY - emp.Height / 2;
                emp.EDX2R[3] = emp.CenterX - emp.Width / 2;
                emp.EDY2R[3] = emp.CenterY - emp.Height / 2;

                emp.EDDR[4] = emp0R2.EDDR[4];
                emp.EDDistance[4] = emp0R2.EDDistance[4];
                diameter1 = emp.TIEDR;
                length = emp.Height;
                TheDistance = emp.EDDistance[4];
                TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                emp.EDBarsNumbers[4] = TheBars;
                emp.EDX1R[4] = emp.CenterX - emp.Width / 2;
                emp.EDY1R[4] = emp.CenterY - emp.Height / 2;
                emp.EDX2R[4] = emp.CenterX - emp.Width / 2;
                emp.EDY2R[4] = emp.CenterY + emp.Height / 2;
                #endregion
                #region//القضبان الداخلية
                diameter1 = emp.TIEDR;
                for (int s = 1; s < 5; s++)
                {
                    int kl1 = 0;
                    int kl2 = 0;
                    if (s == 1 || s == 3)
                    {
                        length = emp.Width;
                        kl1 = 1;
                        kl2 = 0;
                    }
                    if (s == 2 || s == 4)
                    {
                        length = emp.Height;
                        kl1 = 0;
                        kl2 = 1;
                    }
                    NBAR = emp.EDBarsNumbers[s];
                    diameter = emp.EDDR[s];
                    DIS = Math.Round((length - 2 * diameter1) / (NBAR + 1), 3);
                    X1 = emp.EDX1R[s];
                    Y1 = emp.EDY1R[s];
                    for (int k = 1; k < NBAR + 1; k++)
                    {
                        BarsNumber = BarsNumber + 1;
                        Bars emp1 = new Bars();
                        emp1.XR = Math.Round(X1 + kl1 * kx[s] * (k * DIS) + kx[s] * diameter1 + kl2 * ky[s] * (diameter / 2), 3);
                        emp1.YR = Math.Round(Y1 + kl2 * ky[s] * (k * DIS) + ky[s] * diameter1 + kl1 * ky[s] * (diameter / 2), 3);
                        emp1.DR = diameter;
                        emp1.Selected = 0;
                        emp1.InLine = 0;
                        emp1.Type = 3;
                        emp1.InED = s;
                        emp1.InREC = RecBarsNumber;
                        emp1.Corner = 0;
                        emp1.InCircle = 0;
                        Bar[BarsNumber] = emp1;
                    }
                }
                #endregion
                #region//قضبان الزوايا
                for (int k = 1; k < 5; k++)
                {
                    BarsNumber = BarsNumber + 1;
                    Bars emp1 = new Bars();
                    diameter = emp.CORDR[k];
                    if (k == 1)
                    {
                        emp1.XR = Math.Round(emp0.PointXReal[1] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED4LCoverR, 3);
                        emp1.YR = Math.Round(emp0.PointYReal[1] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED1LCoverR, 3);
                    }
                    if (k == 2)
                    {
                        emp1.XR = Math.Round(emp0.PointXReal[2] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED2LCoverR, 3);
                        emp1.YR = Math.Round(emp0.PointYReal[2] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED1LCoverR, 3);
                    }
                    if (k == 3)
                    {
                        emp1.XR = Math.Round(emp0.PointXReal[11] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED2LCoverR, 3);
                        emp1.YR = Math.Round(emp0.PointYReal[11] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED3LCoverR, 3);
                    }
                    if (k == 4)
                    {
                        emp1.XR = Math.Round(emp0.PointXReal[12] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED4LCoverR, 3);
                        emp1.YR = Math.Round(emp0.PointYReal[12] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED3LCoverR, 3);
                    }
                    emp1.DR = diameter;
                    emp1.Selected = 0;
                    emp1.InLine = 0;
                    emp1.Type = 4;
                    emp1.InED = 0;
                    emp1.InCircle = 0;
                    emp1.Corner = k;
                    emp1.InREC = RecBarsNumber;
                    Bar[BarsNumber] = emp1;
                }
                #endregion
                RecLineBar[RecBarsNumber] = emp;
                #endregion
                #region//تحميل مستطيلات التسليح
                #region//حذف و ترتيب
            startloopR: { }
                for (int k = 1; k < BarsNumber + 1; k++)
                {
                    if (Bar[k].InREC == SelectedRecBar3)
                    {
                        BarsNumber = BarsNumber - 1;
                        for (int l = k; l < BarsNumber + 1; l++)
                        {
                            Bar[l] = Bar[l + 1];
                        }
                        if (k <= BarsNumber) goto startloopR;
                    }
                }
                for (int k = 1; k < BarsNumber + 1; k++)
                {
                    if (Bar[k].InREC > SelectedRecBar3)
                    {
                        Bar[k].InREC = Bar[k].InREC - 1;
                    }
                }
                for (int k = SelectedRecBar3; k < RecBarsNumber; k++)
                {
                    RecLineBar[k] = RecLineBar[k + 1];
                }
                for (int k = SelectedFlangedWallShape; k < FlangedWallShapeNumber; k++)//مضاف
                {
                    FlangedWallShape[k] = FlangedWallShape[k + 1];
                }
                for (int k = 1; k < RecBarsNumber; k++)//مضاف
                {
                    if (RecLineBar[k].InFlangedWallShape > SelectedFlangedWallShape)
                    {
                        RecLineBar[k].InFlangedWallShape = RecLineBar[k].InFlangedWallShape - 1;
                    }
                }
                for (int k = 1; k < RecShapeNumber + 1; k++)//مضاف///////////////////////
                {
                    if (RecShape[k].ApplyedToRecBars > SelectedRecBar3)
                    {
                        RecShape[k].ApplyedToRecBars = RecShape[k].ApplyedToRecBars - 1;
                    }
                }
                for (int k = 1; k < FlangedWallShapeNumber; k++)//مضاف
                {
                    if (FlangedWallShape[k].ApplyedToRecBars[3] > SelectedRecBar3)
                    {
                        FlangedWallShape[k].ApplyedToRecBars[2] = FlangedWallShape[k].ApplyedToRecBars[2] - 1;
                        FlangedWallShape[k].ApplyedToRecBars[1] = FlangedWallShape[k].ApplyedToRecBars[1] - 1;
                        FlangedWallShape[k].ApplyedToRecBars[3] = FlangedWallShape[k].ApplyedToRecBars[3] - 1;
                    }
                }
                #endregion
                SelectedFlangedWallShape = FlangedWallShapeNumber;
                SelectedRecBar3 = RecBarsNumber;
                #region//أسوارة التسليح
                emp = new RecLineBars();
                emp.InFlangedWallShape = SelectedFlangedWallShape;
                emp.InRecShape = 0;
                emp.Width = emp0.PointXReal[6] - emp0.PointXReal[5] - emp0.ED2RCoverR - emp0.ED4RCoverR;
                emp.Height = emp0.PointYReal[6] - emp0.PointYReal[7] - emp0.ED1RCoverR - emp0.ED3RCoverR;
                emp.CenterX = emp0.PointXReal[5] + emp0.ED4RCoverR + emp.Width / 2;
                emp.CenterY = emp0.PointYReal[5] - emp0.ED1RCoverR - emp.Height / 2;
                emp.TIEDR = emp0R3.TIEDR;
                emp.CORDR[1] = emp0R3.CORDR[1];
                emp.CORDR[2] = emp0R3.CORDR[2];
                emp.CORDR[3] = emp0R3.CORDR[3];
                emp.CORDR[4] = emp0R3.CORDR[4];

                emp.EDDR[1] = emp0R3.EDDR[1];
                emp.EDDistance[1] = emp0R3.EDDistance[1];
                diameter1 = emp.TIEDR;
                length = emp.Width;
                TheDistance = emp.EDDistance[1];
                TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                emp.EDBarsNumbers[1] = TheBars;
                emp.EDX1R[1] = emp.CenterX - emp.Width / 2;
                emp.EDY1R[1] = emp.CenterY + emp.Height / 2;
                emp.EDX2R[1] = emp.CenterX + emp.Width / 2;
                emp.EDY2R[1] = emp.CenterY + emp.Height / 2;

                emp.EDDR[2] = emp0R3.EDDR[2];
                emp.EDDistance[2] = emp0R3.EDDistance[2];
                diameter1 = emp.TIEDR;
                length = emp.Height;
                TheDistance = emp.EDDistance[2];
                TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                emp.EDBarsNumbers[2] = TheBars;
                emp.EDX1R[2] = emp.CenterX + emp.Width / 2;
                emp.EDY1R[2] = emp.CenterY + emp.Height / 2;
                emp.EDX2R[2] = emp.CenterX + emp.Width / 2;
                emp.EDY2R[2] = emp.CenterY - emp.Height / 2;

                emp.EDDR[3] = emp0R3.EDDR[3];
                emp.EDDistance[3] = emp0R3.EDDistance[3];
                diameter1 = emp.TIEDR;
                length = emp.Width;
                TheDistance = emp.EDDistance[3];
                TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                emp.EDBarsNumbers[3] = TheBars;
                emp.EDX1R[3] = emp.CenterX + emp.Width / 2;
                emp.EDY1R[3] = emp.CenterY - emp.Height / 2;
                emp.EDX2R[3] = emp.CenterX - emp.Width / 2;
                emp.EDY2R[3] = emp.CenterY - emp.Height / 2;

                emp.EDDR[4] = emp0R3.EDDR[4];
                emp.EDDistance[4] = emp0R3.EDDistance[4];
                diameter1 = emp.TIEDR;
                length = emp.Height;
                TheDistance = emp.EDDistance[4];
                TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                emp.EDBarsNumbers[4] = TheBars;
                emp.EDX1R[4] = emp.CenterX - emp.Width / 2;
                emp.EDY1R[4] = emp.CenterY - emp.Height / 2;
                emp.EDX2R[4] = emp.CenterX - emp.Width / 2;
                emp.EDY2R[4] = emp.CenterY + emp.Height / 2;
                #endregion
                #region//القضبان الداخلية
                diameter1 = emp.TIEDR;
                for (int s = 1; s < 5; s++)
                {
                    int kl1 = 0;
                    int kl2 = 0;
                    if (s == 1 || s == 3)
                    {
                        length = emp.Width;
                        kl1 = 1;
                        kl2 = 0;
                    }
                    if (s == 2 || s == 4)
                    {
                        length = emp.Height;
                        kl1 = 0;
                        kl2 = 1;
                    }
                    NBAR = emp.EDBarsNumbers[s];
                    diameter = emp.EDDR[s];
                    DIS = Math.Round((length - 2 * diameter1) / (NBAR + 1), 3);
                    X1 = emp.EDX1R[s];
                    Y1 = emp.EDY1R[s];
                    for (int k = 1; k < NBAR + 1; k++)
                    {
                        BarsNumber = BarsNumber + 1;
                        Bars emp1 = new Bars();
                        emp1.XR = Math.Round(X1 + kl1 * kx[s] * (k * DIS) + kx[s] * diameter1 + kl2 * ky[s] * (diameter / 2), 3);
                        emp1.YR = Math.Round(Y1 + kl2 * ky[s] * (k * DIS) + ky[s] * diameter1 + kl1 * ky[s] * (diameter / 2), 3);
                        emp1.DR = diameter;
                        emp1.Selected = 0;
                        emp1.InLine = 0;
                        emp1.Type = 3;
                        emp1.InED = s;
                        emp1.InREC = RecBarsNumber;
                        emp1.Corner = 0;
                        emp1.InCircle = 0;
                        Bar[BarsNumber] = emp1;
                    }
                }
                #endregion
                #region//قضبان الزوايا
                for (int k = 1; k < 5; k++)
                {
                    BarsNumber = BarsNumber + 1;
                    Bars emp1 = new Bars();
                    diameter = emp.CORDR[k];
                    if (k == 1)
                    {
                        emp1.XR = Math.Round(emp0.PointXReal[5] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED4RCoverR, 3);
                        emp1.YR = Math.Round(emp0.PointYReal[5] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED1RCoverR, 3);
                    }
                    if (k == 2)
                    {
                        emp1.XR = Math.Round(emp0.PointXReal[6] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED2RCoverR, 3);
                        emp1.YR = Math.Round(emp0.PointYReal[6] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED1RCoverR, 3);
                    }
                    if (k == 3)
                    {
                        emp1.XR = Math.Round(emp0.PointXReal[7] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED2RCoverR, 3);
                        emp1.YR = Math.Round(emp0.PointYReal[7] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED3RCoverR, 3);
                    }
                    if (k == 4)
                    {
                        emp1.XR = Math.Round(emp0.PointXReal[8] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.ED4RCoverR, 3);
                        emp1.YR = Math.Round(emp0.PointYReal[8] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.ED3RCoverR, 3);
                    }
                    emp1.DR = diameter;
                    emp1.Selected = 0;
                    emp1.InLine = 0;
                    emp1.Type = 4;
                    emp1.InED = 0;
                    emp1.InCircle = 0;
                    emp1.Corner = k;
                    emp1.InREC = RecBarsNumber;
                    Bar[BarsNumber] = emp1;
                }
                #endregion
                RecLineBar[RecBarsNumber] = emp;
                #endregion
                emp0.ApplyedToRecBars[1] = RecBarsNumber - 2;
                emp0.ApplyedToRecBars[2] = RecBarsNumber - 1;
                emp0.ApplyedToRecBars[3] = RecBarsNumber;
            }
            #endregion
            FlangedWallShape[SelectedFlangedWallShape] = emp0;
            RecEdgeToEdit = 0;
            ClickToEdit = 0;
            MakeTempFiles();
            Render2d();
            DrawForEditFlangedWallShape();
        }
        private void DrawForEditCircleShape()
        {
            Bitmap finalBmp = new Bitmap(BitampWidth2d, BitampHight2d);
            if (pictureBox2.Image != null)
            {
                pictureBox2.Image.Dispose();
                pictureBox2.Image = null;
            }
            pictureBox2.Image = finalBmp;
            if (SelectedCircleShape != 0)
            {
                Graphics g = Graphics.FromImage(pictureBox2.Image);
                Pen pen = new Pen(Color.Navy, 3f);
                Pen pen1 = new Pen(Color.Blue, 5);
                Font drawFont = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
                SolidBrush drawBrush = new SolidBrush(Color.Blue);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                float[] dashValues = { 3, 3 };
                pen.DashPattern = dashValues;
                int thex = (TempX + LineMoveX1) / 2;
                int they = (TempY + LineMoveY1) / 2;
                int tx1 = 0;
                int ty1 = 0;
                int tx2 = 0;
                int ty2 = 0;
                int tx = 0;
                int ty = 0;
                int kkx1 = 0;
                int kky1 = 0;
                int kkx2 = 0;
                int kky2 = 0;
                int Diameter = Convert.ToInt32(CircleShape[SelectedCircleShape].Diameter * Zoom2d / 2);
                double DiameterR = CircleShape[SelectedCircleShape].Diameter / 2;
                #region///////0
                if (RecEdgeToEdit == 0)
                {
                    for (int i = 1; i < 5; i++)
                    {
                        if (i == 1)
                        {
                            kkx1 = -1;
                            kky1 = -1;
                            kkx2 = 1;
                            kky2 = -1;
                        }
                        if (i == 2)
                        {
                            kkx1 = 1;
                            kky1 = -1;
                            kkx2 = 1;
                            kky2 = 1;
                        }
                        if (i == 3)
                        {
                            kkx1 = 1;
                            kky1 = 1;
                            kkx2 = -1;
                            kky2 = 1;
                        }
                        if (i == 4)
                        {
                            kkx1 = -1;
                            kky1 = 1;
                            kkx2 = -1;
                            kky2 = -1;
                        }
                        tx1 = CircleShape[SelectedCircleShape].CenterX2D + kkx1 * Diameter;
                        ty1 = CircleShape[SelectedCircleShape].CenterY2D + kky1 * Diameter;
                        tx2 = CircleShape[SelectedCircleShape].CenterX2D + kkx2 * Diameter;
                        ty2 = CircleShape[SelectedCircleShape].CenterY2D + kky2 * Diameter;
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        tx = ((tx1 + tx2) / 2) - 3;
                        ty = ((ty1 + ty2) / 2) - 3;
                        g.DrawRectangle(pen1, tx, ty, 6, 6);
                    }
                }
                #endregion
                #region///////1
                if (RecEdgeToEdit == 8 || RecEdgeToEdit == 7 || RecEdgeToEdit == 9)
                {
                    kkx1 = -1;
                    kky1 = -1;
                    kkx2 = 1;
                    kky2 = -1;
                    tx1 = CircleShape[SelectedCircleShape].CenterX2D + kkx1 * Diameter;
                    ty1 = TempY;
                    tx2 = CircleShape[SelectedCircleShape].CenterX2D + kkx2 * Diameter;
                    ty2 = TempY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    kkx1 = 1;
                    kky1 = -1;
                    kkx2 = 1;
                    kky2 = 1;
                    tx1 = CircleShape[SelectedCircleShape].CenterX2D + kkx1 * Diameter;
                    ty1 = TempY;
                    tx2 = CircleShape[SelectedCircleShape].CenterX2D + kkx2 * Diameter;
                    ty2 = CircleShape[SelectedCircleShape].CenterY2D + kky2 * Diameter;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    kkx1 = 1;
                    kky1 = 1;
                    kkx2 = -1;
                    kky2 = 1;
                    tx1 = CircleShape[SelectedCircleShape].CenterX2D + kkx1 * Diameter;
                    ty1 = CircleShape[SelectedCircleShape].CenterY2D + kky1 * Diameter;
                    tx2 = CircleShape[SelectedCircleShape].CenterX2D + kkx2 * Diameter;
                    ty2 = CircleShape[SelectedCircleShape].CenterY2D + kky2 * Diameter;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    kkx1 = -1;
                    kky1 = 1;
                    kkx2 = -1;
                    kky2 = -1;
                    tx1 = CircleShape[SelectedCircleShape].CenterX2D + kkx1 * Diameter;
                    ty1 = CircleShape[SelectedCircleShape].CenterY2D + kky1 * Diameter;
                    tx2 = CircleShape[SelectedCircleShape].CenterX2D + kkx2 * Diameter;
                    ty2 = TempY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    Tx[1] = CircleShape[SelectedCircleShape].CenterX - DiameterR;
                    Ty[1] = Math.Round((startY2d - TempY) / Zoom2d, 3);
                    Tx[2] = CircleShape[SelectedCircleShape].CenterX + DiameterR;
                    Ty[2] = Math.Round((startY2d - TempY) / Zoom2d, 3);
                    Tx[3] = CircleShape[SelectedCircleShape].CenterX + DiameterR;
                    Ty[3] = CircleShape[SelectedCircleShape].CenterY - DiameterR;
                    Tx[4] = CircleShape[SelectedCircleShape].CenterX - DiameterR;
                    Ty[4] = CircleShape[SelectedCircleShape].CenterY - DiameterR;
                }
                #endregion
                #region///////3
                if (RecEdgeToEdit == 24 || RecEdgeToEdit == 25 || RecEdgeToEdit == 23)
                {
                    kkx1 = -1;
                    kky1 = -1;
                    kkx2 = 1;
                    kky2 = -1;
                    tx1 = CircleShape[SelectedCircleShape].CenterX2D + kkx1 * Diameter;
                    ty1 = CircleShape[SelectedCircleShape].CenterY2D + kky1 * Diameter;
                    tx2 = CircleShape[SelectedCircleShape].CenterX2D + kkx2 * Diameter;
                    ty2 = CircleShape[SelectedCircleShape].CenterY2D + kky2 * Diameter;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    kkx1 = 1;
                    kky1 = -1;
                    kkx2 = 1;
                    kky2 = 1;
                    tx1 = CircleShape[SelectedCircleShape].CenterX2D + kkx1 * Diameter;
                    ty1 = CircleShape[SelectedCircleShape].CenterY2D + kky1 * Diameter;
                    tx2 = CircleShape[SelectedCircleShape].CenterX2D + kkx2 * Diameter;
                    ty2 = TempY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    kkx1 = 1;
                    kky1 = 1;
                    kkx2 = -1;
                    kky2 = 1;
                    tx1 = CircleShape[SelectedCircleShape].CenterX2D + kkx1 * Diameter;
                    ty1 = TempY;
                    tx2 = CircleShape[SelectedCircleShape].CenterX2D + kkx2 * Diameter;
                    ty2 = TempY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    kkx1 = -1;
                    kky1 = 1;
                    kkx2 = -1;
                    kky2 = -1;
                    tx1 = CircleShape[SelectedCircleShape].CenterX2D + kkx1 * Diameter;
                    ty1 = TempY;
                    tx2 = CircleShape[SelectedCircleShape].CenterX2D + kkx2 * Diameter;
                    ty2 = CircleShape[SelectedCircleShape].CenterY2D + kky2 * Diameter;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    Tx[1] = CircleShape[SelectedCircleShape].CenterX - DiameterR;
                    Ty[1] = CircleShape[SelectedCircleShape].CenterY + DiameterR;
                    Tx[2] = CircleShape[SelectedCircleShape].CenterX + DiameterR;
                    Ty[2] = CircleShape[SelectedCircleShape].CenterY + DiameterR;
                    Tx[3] = CircleShape[SelectedCircleShape].CenterX + DiameterR;
                    Ty[3] = Math.Round((startY2d - TempY) / Zoom2d, 3);
                    Tx[4] = CircleShape[SelectedCircleShape].CenterX - DiameterR;
                    Ty[4] = Math.Round((startY2d - TempY) / Zoom2d, 3);
                }
                #endregion
                #region///////2
                if (RecEdgeToEdit == 32 || RecEdgeToEdit == 1 || RecEdgeToEdit == 31)
                {
                    kkx1 = -1;
                    kky1 = -1;
                    kkx2 = 1;
                    kky2 = -1;
                    tx1 = CircleShape[SelectedCircleShape].CenterX2D + kkx1 * Diameter;
                    ty1 = CircleShape[SelectedCircleShape].CenterY2D + kky1 * Diameter;
                    tx2 = TempX;
                    ty2 = CircleShape[SelectedCircleShape].CenterY2D + kky2 * Diameter;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    kkx1 = 1;
                    kky1 = -1;
                    kkx2 = 1;
                    kky2 = 1;
                    tx1 = TempX;
                    ty1 = CircleShape[SelectedCircleShape].CenterY2D + kky1 * Diameter;
                    tx2 = TempX;
                    ty2 = CircleShape[SelectedCircleShape].CenterY2D + kky2 * Diameter;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    kkx1 = 1;
                    kky1 = 1;
                    kkx2 = -1;
                    kky2 = 1;
                    tx1 = TempX;
                    ty1 = CircleShape[SelectedCircleShape].CenterY2D + kky1 * Diameter;
                    tx2 = CircleShape[SelectedCircleShape].CenterX2D + kkx2 * Diameter;
                    ty2 = CircleShape[SelectedCircleShape].CenterY2D + kky2 * Diameter;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    kkx1 = -1;
                    kky1 = 1;
                    kkx2 = -1;
                    kky2 = -1;
                    tx1 = CircleShape[SelectedCircleShape].CenterX2D + kkx1 * Diameter;
                    ty1 = CircleShape[SelectedCircleShape].CenterY2D + kky1 * Diameter;
                    tx2 = CircleShape[SelectedCircleShape].CenterX2D + kkx2 * Diameter;
                    ty2 = CircleShape[SelectedCircleShape].CenterY2D + kky2 * Diameter;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    Tx[1] = CircleShape[SelectedCircleShape].CenterX - DiameterR;
                    Ty[1] = CircleShape[SelectedCircleShape].CenterY + DiameterR;
                    Tx[2] = Math.Round((TempX - startX2d) / Zoom2d, 3);
                    Ty[2] = CircleShape[SelectedCircleShape].CenterY + DiameterR;
                    Tx[3] = Math.Round((TempX - startX2d) / Zoom2d, 3);
                    Ty[3] = CircleShape[SelectedCircleShape].CenterY - DiameterR;
                    Tx[4] = CircleShape[SelectedCircleShape].CenterX - DiameterR;
                    Ty[4] = CircleShape[SelectedCircleShape].CenterY - DiameterR;
                }
                #endregion
                #region///////4
                if (RecEdgeToEdit == 16 || RecEdgeToEdit == 17 || RecEdgeToEdit == 15)
                {
                    kkx1 = -1;
                    kky1 = -1;
                    kkx2 = 1;
                    kky2 = -1;
                    tx1 = TempX;
                    ty1 = CircleShape[SelectedCircleShape].CenterY2D + kky1 * Diameter;
                    tx2 = CircleShape[SelectedCircleShape].CenterX2D + kkx2 * Diameter;
                    ty2 = CircleShape[SelectedCircleShape].CenterY2D + kky2 * Diameter;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    kkx1 = 1;
                    kky1 = -1;
                    kkx2 = 1;
                    kky2 = 1;
                    tx1 = CircleShape[SelectedCircleShape].CenterX2D + kkx1 * Diameter;
                    ty1 = CircleShape[SelectedCircleShape].CenterY2D + kky1 * Diameter;
                    tx2 = CircleShape[SelectedCircleShape].CenterX2D + kkx2 * Diameter;
                    ty2 = CircleShape[SelectedCircleShape].CenterY2D + kky2 * Diameter;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    kkx1 = 1;
                    kky1 = 1;
                    kkx2 = -1;
                    kky2 = 1;
                    tx1 = CircleShape[SelectedCircleShape].CenterX2D + kkx1 * Diameter;
                    ty1 = CircleShape[SelectedCircleShape].CenterY2D + kky1 * Diameter;
                    tx2 = TempX;
                    ty2 = CircleShape[SelectedCircleShape].CenterY2D + kky2 * Diameter;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    kkx1 = -1;
                    kky1 = 1;
                    kkx2 = -1;
                    kky2 = -1;
                    tx1 = TempX;
                    ty1 = CircleShape[SelectedCircleShape].CenterY2D + kky1 * Diameter;
                    tx2 = TempX;
                    ty2 = CircleShape[SelectedCircleShape].CenterY2D + kky2 * Diameter;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    Tx[1] = Math.Round((TempX - startX2d) / Zoom2d, 3);
                    Ty[1] = CircleShape[SelectedCircleShape].CenterY + DiameterR;
                    Tx[2] = CircleShape[SelectedCircleShape].CenterX + DiameterR;
                    Ty[2] = CircleShape[SelectedCircleShape].CenterY + DiameterR;
                    Tx[3] = CircleShape[SelectedCircleShape].CenterX + DiameterR;
                    Ty[3] = CircleShape[SelectedCircleShape].CenterY - DiameterR;
                    Tx[4] = Math.Round((TempX - startX2d) / Zoom2d, 3);
                    Ty[4] = CircleShape[SelectedCircleShape].CenterY - DiameterR;
                }
                #endregion
            }
        }
        private void EditCircleShape()
        {
            if (RecEdgeToEdit == 8 || RecEdgeToEdit == 7 || RecEdgeToEdit == 9)
            {
                CircleShape[SelectedCircleShape].Diameter = Math.Abs(Ty[4] - Ty[1]);
            }
            if (RecEdgeToEdit == 24 || RecEdgeToEdit == 25 || RecEdgeToEdit == 23)
            {
                CircleShape[SelectedCircleShape].Diameter = Math.Abs(Ty[4] - Ty[1]);
            }
            if (RecEdgeToEdit == 32 || RecEdgeToEdit == 1 || RecEdgeToEdit == 31)
            {
                CircleShape[SelectedCircleShape].Diameter = Math.Abs(Tx[2] - Tx[1]);
            }
            if (RecEdgeToEdit == 16 || RecEdgeToEdit == 15 || RecEdgeToEdit == 17)
            {
                CircleShape[SelectedCircleShape].Diameter = Math.Abs(Tx[2] - Tx[1]);
            }
            CircleShape[SelectedCircleShape].CenterX = (Tx[2] + Tx[1]) / 2;
            CircleShape[SelectedCircleShape].CenterY = (Ty[2] + Ty[3]) / 2;
            double Diameter = CircleShape[SelectedCircleShape].Diameter;
            double CenterX = CircleShape[SelectedCircleShape].CenterX;
            double CenterY = CircleShape[SelectedCircleShape].CenterY;
            int pointN = 32;
            double zaweaa = 2 * Math.PI / pointN;
            double Diameter1 = Diameter / 2;
            for (int s = 1; s < pointN + 1; s++)
            {
                CircleShape[SelectedCircleShape].PointXR[s] = Math.Round(CenterX + Math.Cos(s * zaweaa) * Diameter1, 3);
                CircleShape[SelectedCircleShape].PointYR[s] = Math.Round(CenterY + Math.Sin(s * zaweaa) * Diameter1, 3);
            }
            CircleShapes emp0 = new CircleShapes();
            emp0 = CircleShape[SelectedCircleShape];
            #region//يوجد تسليح
            if (CircleShape[SelectedCircleShape].HasReinforcment == 1)
            {
                SelectedCircleLineBar = CircleShape[SelectedCircleShape].ApplyedToCircleBars;
                CircleLineBars emp0R = new CircleLineBars();
                emp0R = CircleLineBar[SelectedCircleLineBar];
                #region///الحذف و الترتيب
            startloop: { }
                for (int k = 1; k < BarsNumber + 1; k++)
                {
                    if (Bar[k].InCircle == SelectedCircleLineBar)
                    {
                        BarsNumber = BarsNumber - 1;
                        for (int l = k; l < BarsNumber + 1; l++)
                        {
                            Bar[l] = Bar[l + 1];
                        }
                        if (k <= BarsNumber) goto startloop;
                    }
                }
                for (int k = 1; k < BarsNumber + 1; k++)
                {
                    if (Bar[k].InCircle > SelectedCircleLineBar)
                    {
                        Bar[k].InCircle = Bar[k].InCircle - 1;
                    }
                }
                for (int k = SelectedCircleLineBar; k < CircleBarsNumber; k++)
                {
                    CircleLineBar[k] = CircleLineBar[k + 1];
                }
                for (int k = SelectedCircleShape; k < CircleShapeNumber; k++)//مضاف
                {
                    CircleShape[k] = CircleShape[k + 1];
                }
                for (int k = 1; k < CircleBarsNumber; k++)//مضاف
                {
                    if (CircleLineBar[k].InCircleShape > SelectedCircleShape)
                    {
                        CircleLineBar[k].InCircleShape = CircleLineBar[k].InCircleShape - 1;
                    }
                }
                for (int k = 1; k < CircleShapeNumber; k++)//مضاف
                {
                    if (CircleShape[k].ApplyedToCircleBars > SelectedCircleLineBar)
                    {
                        CircleShape[k].ApplyedToCircleBars = CircleShape[k].ApplyedToCircleBars - 1;
                    }
                }
                #endregion
                SelectedCircleShape = CircleShapeNumber;
                SelectedCircleLineBar = CircleBarsNumber;
                CircleLineBars emp = new CircleLineBars();
                Diameter = emp0.Diameter - 2 * emp0.CoverR;
                emp.InCircleShape = SelectedCircleShape;
                emp.Diameter = Diameter;
                emp.CenterX = emp0.CenterX;
                emp.CenterY = emp0.CenterY;
                emp.TIEDR = emp0R.TIEDR;
                emp.DR = emp0R.DR;
                emp.BarsNumbers = emp0R.BarsNumbers;
                pointN = 32;
                zaweaa = 2 * Math.PI / pointN;
                Diameter1 = Diameter / 2;// + (emp.DR / 2 + emp.TIEDR);
                for (int s = 1; s < pointN + 1; s++)
                {
                    emp.PointXR[s] = Math.Round(CenterX + Math.Cos(s * zaweaa) * Diameter1, 3);
                    emp.PointYR[s] = Math.Round(CenterY + Math.Sin(s * zaweaa) * Diameter1, 3);
                }
                zaweaa = 2 * Math.PI / emp.BarsNumbers;
                Diameter1 = Diameter / 2 - (emp.DR / 2 + emp.TIEDR);
                for (int s = 1; s < emp.BarsNumbers + 1; s++)
                {
                    BarsNumber = BarsNumber + 1;
                    Bars emp1 = new Bars();
                    emp1.XR = Math.Round(CenterX + Math.Cos(s * zaweaa) * Diameter1, 3);
                    emp1.YR = Math.Round(CenterY + Math.Sin(s * zaweaa) * Diameter1, 3);
                    emp1.DR = emp.DR;
                    emp1.Selected = 0;
                    emp1.InLine = 0;
                    emp1.Type = 5;
                    emp1.InED = 0;
                    emp1.InREC = 0;
                    emp1.InCircle = CircleBarsNumber;
                    Bar[BarsNumber] = emp1;
                }
                CircleLineBar[CircleBarsNumber] = emp;
                emp0.ApplyedToCircleBars = SelectedCircleLineBar;
            }
            #endregion
            CircleShape[SelectedCircleShape] = emp0;
            RecEdgeToEdit = 0;
            ClickToEdit = 0;
            MakeTempFiles();
            Render2d();
            DrawForEditCircleShape();
        }
        private void DrawForEditCircleLineBar()
        {
            Bitmap finalBmp = new Bitmap(BitampWidth2d, BitampHight2d);
            if (pictureBox2.Image != null)
            {
                pictureBox2.Image.Dispose();
                pictureBox2.Image = null;
            }
            pictureBox2.Image = finalBmp;
            if (SelectedCircleLineBar != 0)
            {
                Graphics g = Graphics.FromImage(pictureBox2.Image);
                Pen pen = new Pen(Color.Navy, 3f);
                Pen pen1 = new Pen(Color.Blue, 5);
                Font drawFont = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
                SolidBrush drawBrush = new SolidBrush(Color.Blue);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                float[] dashValues = { 3, 3 };
                pen.DashPattern = dashValues;
                int thex = (TempX + LineMoveX1) / 2;
                int they = (TempY + LineMoveY1) / 2;
                int tx1 = 0;
                int ty1 = 0;
                int tx2 = 0;
                int ty2 = 0;
                int tx = 0;
                int ty = 0;
                int kkx1 = 0;
                int kky1 = 0;
                int kkx2 = 0;
                int kky2 = 0;
                int Diameter = Convert.ToInt32(CircleLineBar[SelectedCircleLineBar].Diameter * Zoom2d / 2);
                double DiameterR = CircleLineBar[SelectedCircleLineBar].Diameter / 2;
                #region///////0
                if (RecEdgeToEdit == 0)
                {
                    for (int i = 1; i < 5; i++)
                    {
                        if (i == 1)
                        {
                            kkx1 = -1;
                            kky1 = -1;
                            kkx2 = 1;
                            kky2 = -1;
                        }
                        if (i == 2)
                        {
                            kkx1 = 1;
                            kky1 = -1;
                            kkx2 = 1;
                            kky2 = 1;
                        }
                        if (i == 3)
                        {
                            kkx1 = 1;
                            kky1 = 1;
                            kkx2 = -1;
                            kky2 = 1;
                        }
                        if (i == 4)
                        {
                            kkx1 = -1;
                            kky1 = 1;
                            kkx2 = -1;
                            kky2 = -1;
                        }
                        tx1 = CircleLineBar[SelectedCircleLineBar].CenterX2D + kkx1 * Diameter;
                        ty1 = CircleLineBar[SelectedCircleLineBar].CenterY2D + kky1 * Diameter;
                        tx2 = CircleLineBar[SelectedCircleLineBar].CenterX2D + kkx2 * Diameter;
                        ty2 = CircleLineBar[SelectedCircleLineBar].CenterY2D + kky2 * Diameter;
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        tx = ((tx1 + tx2) / 2) - 3;
                        ty = ((ty1 + ty2) / 2) - 3;
                        g.DrawRectangle(pen1, tx, ty, 6, 6);
                    }
                }
                #endregion
                #region///////1
                if (RecEdgeToEdit == 8 || RecEdgeToEdit == 7 || RecEdgeToEdit == 9)
                {
                    kkx1 = -1;
                    kky1 = -1;
                    kkx2 = 1;
                    kky2 = -1;
                    tx1 = CircleLineBar[SelectedCircleLineBar].CenterX2D + kkx1 * Diameter;
                    ty1 = TempY;
                    tx2 = CircleLineBar[SelectedCircleLineBar].CenterX2D + kkx2 * Diameter;
                    ty2 = TempY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    kkx1 = 1;
                    kky1 = -1;
                    kkx2 = 1;
                    kky2 = 1;
                    tx1 = CircleLineBar[SelectedCircleLineBar].CenterX2D + kkx1 * Diameter;
                    ty1 = TempY;
                    tx2 = CircleLineBar[SelectedCircleLineBar].CenterX2D + kkx2 * Diameter;
                    ty2 = CircleLineBar[SelectedCircleLineBar].CenterY2D + kky2 * Diameter;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    kkx1 = 1;
                    kky1 = 1;
                    kkx2 = -1;
                    kky2 = 1;
                    tx1 = CircleLineBar[SelectedCircleLineBar].CenterX2D + kkx1 * Diameter;
                    ty1 = CircleLineBar[SelectedCircleLineBar].CenterY2D + kky1 * Diameter;
                    tx2 = CircleLineBar[SelectedCircleLineBar].CenterX2D + kkx2 * Diameter;
                    ty2 = CircleLineBar[SelectedCircleLineBar].CenterY2D + kky2 * Diameter;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    kkx1 = -1;
                    kky1 = 1;
                    kkx2 = -1;
                    kky2 = -1;
                    tx1 = CircleLineBar[SelectedCircleLineBar].CenterX2D + kkx1 * Diameter;
                    ty1 = CircleLineBar[SelectedCircleLineBar].CenterY2D + kky1 * Diameter;
                    tx2 = CircleLineBar[SelectedCircleLineBar].CenterX2D + kkx2 * Diameter;
                    ty2 = TempY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    Tx[1] = CircleLineBar[SelectedCircleLineBar].CenterX - DiameterR;
                    Ty[1] = Math.Round((startY2d - TempY) / Zoom2d, 3);
                    Tx[2] = CircleLineBar[SelectedCircleLineBar].CenterX + DiameterR;
                    Ty[2] = Math.Round((startY2d - TempY) / Zoom2d, 3);
                    Tx[3] = CircleLineBar[SelectedCircleLineBar].CenterX + DiameterR;
                    Ty[3] = CircleLineBar[SelectedCircleLineBar].CenterY - DiameterR;
                    Tx[4] = CircleLineBar[SelectedCircleLineBar].CenterX - DiameterR;
                    Ty[4] = CircleLineBar[SelectedCircleLineBar].CenterY - DiameterR;
                }
                #endregion
                #region///////3
                if (RecEdgeToEdit == 24 || RecEdgeToEdit == 25 || RecEdgeToEdit == 23)
                {
                    kkx1 = -1;
                    kky1 = -1;
                    kkx2 = 1;
                    kky2 = -1;
                    tx1 = CircleLineBar[SelectedCircleLineBar].CenterX2D + kkx1 * Diameter;
                    ty1 = CircleLineBar[SelectedCircleLineBar].CenterY2D + kky1 * Diameter;
                    tx2 = CircleLineBar[SelectedCircleLineBar].CenterX2D + kkx2 * Diameter;
                    ty2 = CircleLineBar[SelectedCircleLineBar].CenterY2D + kky2 * Diameter;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    kkx1 = 1;
                    kky1 = -1;
                    kkx2 = 1;
                    kky2 = 1;
                    tx1 = CircleLineBar[SelectedCircleLineBar].CenterX2D + kkx1 * Diameter;
                    ty1 = CircleLineBar[SelectedCircleLineBar].CenterY2D + kky1 * Diameter;
                    tx2 = CircleLineBar[SelectedCircleLineBar].CenterX2D + kkx2 * Diameter;
                    ty2 = TempY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    kkx1 = 1;
                    kky1 = 1;
                    kkx2 = -1;
                    kky2 = 1;
                    tx1 = CircleLineBar[SelectedCircleLineBar].CenterX2D + kkx1 * Diameter;
                    ty1 = TempY;
                    tx2 = CircleLineBar[SelectedCircleLineBar].CenterX2D + kkx2 * Diameter;
                    ty2 = TempY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    kkx1 = -1;
                    kky1 = 1;
                    kkx2 = -1;
                    kky2 = -1;
                    tx1 = CircleLineBar[SelectedCircleLineBar].CenterX2D + kkx1 * Diameter;
                    ty1 = TempY;
                    tx2 = CircleLineBar[SelectedCircleLineBar].CenterX2D + kkx2 * Diameter;
                    ty2 = CircleLineBar[SelectedCircleLineBar].CenterY2D + kky2 * Diameter;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    Tx[1] = CircleLineBar[SelectedCircleLineBar].CenterX - DiameterR;
                    Ty[1] = CircleLineBar[SelectedCircleLineBar].CenterY + DiameterR;
                    Tx[2] = CircleLineBar[SelectedCircleLineBar].CenterX + DiameterR;
                    Ty[2] = CircleLineBar[SelectedCircleLineBar].CenterY + DiameterR;
                    Tx[3] = CircleLineBar[SelectedCircleLineBar].CenterX + DiameterR;
                    Ty[3] = Math.Round((startY2d - TempY) / Zoom2d, 3);
                    Tx[4] = CircleLineBar[SelectedCircleLineBar].CenterX - DiameterR;
                    Ty[4] = Math.Round((startY2d - TempY) / Zoom2d, 3);
                }
                #endregion
                #region///////2
                if (RecEdgeToEdit == 32 || RecEdgeToEdit == 1 || RecEdgeToEdit == 31)
                {
                    kkx1 = -1;
                    kky1 = -1;
                    kkx2 = 1;
                    kky2 = -1;
                    tx1 = CircleLineBar[SelectedCircleLineBar].CenterX2D + kkx1 * Diameter;
                    ty1 = CircleLineBar[SelectedCircleLineBar].CenterY2D + kky1 * Diameter;
                    tx2 = TempX;
                    ty2 = CircleLineBar[SelectedCircleLineBar].CenterY2D + kky2 * Diameter;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    kkx1 = 1;
                    kky1 = -1;
                    kkx2 = 1;
                    kky2 = 1;
                    tx1 = TempX;
                    ty1 = CircleLineBar[SelectedCircleLineBar].CenterY2D + kky1 * Diameter;
                    tx2 = TempX;
                    ty2 = CircleLineBar[SelectedCircleLineBar].CenterY2D + kky2 * Diameter;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    kkx1 = 1;
                    kky1 = 1;
                    kkx2 = -1;
                    kky2 = 1;
                    tx1 = TempX;
                    ty1 = CircleLineBar[SelectedCircleLineBar].CenterY2D + kky1 * Diameter;
                    tx2 = CircleLineBar[SelectedCircleLineBar].CenterX2D + kkx2 * Diameter;
                    ty2 = CircleLineBar[SelectedCircleLineBar].CenterY2D + kky2 * Diameter;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    kkx1 = -1;
                    kky1 = 1;
                    kkx2 = -1;
                    kky2 = -1;
                    tx1 = CircleLineBar[SelectedCircleLineBar].CenterX2D + kkx1 * Diameter;
                    ty1 = CircleLineBar[SelectedCircleLineBar].CenterY2D + kky1 * Diameter;
                    tx2 = CircleLineBar[SelectedCircleLineBar].CenterX2D + kkx2 * Diameter;
                    ty2 = CircleLineBar[SelectedCircleLineBar].CenterY2D + kky2 * Diameter;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    Tx[1] = CircleLineBar[SelectedCircleLineBar].CenterX - DiameterR;
                    Ty[1] = CircleLineBar[SelectedCircleLineBar].CenterY + DiameterR;
                    Tx[2] = Math.Round((TempX - startX2d) / Zoom2d, 3);
                    Ty[2] = CircleLineBar[SelectedCircleLineBar].CenterY + DiameterR;
                    Tx[3] = Math.Round((TempX - startX2d) / Zoom2d, 3);
                    Ty[3] = CircleLineBar[SelectedCircleLineBar].CenterY - DiameterR;
                    Tx[4] = CircleLineBar[SelectedCircleLineBar].CenterX - DiameterR;
                    Ty[4] = CircleLineBar[SelectedCircleLineBar].CenterY - DiameterR;
                }
                #endregion
                #region///////4
                if (RecEdgeToEdit == 16 || RecEdgeToEdit == 17 || RecEdgeToEdit == 15)
                {
                    kkx1 = -1;
                    kky1 = -1;
                    kkx2 = 1;
                    kky2 = -1;
                    tx1 = TempX;
                    ty1 = CircleLineBar[SelectedCircleLineBar].CenterY2D + kky1 * Diameter;
                    tx2 = CircleLineBar[SelectedCircleLineBar].CenterX2D + kkx2 * Diameter;
                    ty2 = CircleLineBar[SelectedCircleLineBar].CenterY2D + kky2 * Diameter;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    kkx1 = 1;
                    kky1 = -1;
                    kkx2 = 1;
                    kky2 = 1;
                    tx1 = CircleLineBar[SelectedCircleLineBar].CenterX2D + kkx1 * Diameter;
                    ty1 = CircleLineBar[SelectedCircleLineBar].CenterY2D + kky1 * Diameter;
                    tx2 = CircleLineBar[SelectedCircleLineBar].CenterX2D + kkx2 * Diameter;
                    ty2 = CircleLineBar[SelectedCircleLineBar].CenterY2D + kky2 * Diameter;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    kkx1 = 1;
                    kky1 = 1;
                    kkx2 = -1;
                    kky2 = 1;
                    tx1 = CircleLineBar[SelectedCircleLineBar].CenterX2D + kkx1 * Diameter;
                    ty1 = CircleLineBar[SelectedCircleLineBar].CenterY2D + kky1 * Diameter;
                    tx2 = TempX;
                    ty2 = CircleLineBar[SelectedCircleLineBar].CenterY2D + kky2 * Diameter;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    kkx1 = -1;
                    kky1 = 1;
                    kkx2 = -1;
                    kky2 = -1;
                    tx1 = TempX;
                    ty1 = CircleLineBar[SelectedCircleLineBar].CenterY2D + kky1 * Diameter;
                    tx2 = TempX;
                    ty2 = CircleLineBar[SelectedCircleLineBar].CenterY2D + kky2 * Diameter;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    Tx[1] = Math.Round((TempX - startX2d) / Zoom2d, 3);
                    Ty[1] = CircleLineBar[SelectedCircleLineBar].CenterY + DiameterR;
                    Tx[2] = CircleLineBar[SelectedCircleLineBar].CenterX + DiameterR;
                    Ty[2] = CircleLineBar[SelectedCircleLineBar].CenterY + DiameterR;
                    Tx[3] = CircleLineBar[SelectedCircleLineBar].CenterX + DiameterR;
                    Ty[3] = CircleLineBar[SelectedCircleLineBar].CenterY - DiameterR;
                    Tx[4] = Math.Round((TempX - startX2d) / Zoom2d, 3);
                    Ty[4] = CircleLineBar[SelectedCircleLineBar].CenterY - DiameterR;
                }
                #endregion
            }
        }
        private void EditCircleLineBar()
        {
            if (RecEdgeToEdit == 8 || RecEdgeToEdit == 7 || RecEdgeToEdit == 9)
            {
                CircleLineBar[SelectedCircleLineBar].Diameter = Math.Abs(Ty[4] - Ty[1]);
                if (CircleLineBar[SelectedCircleLineBar].InCircleShape != 0)
                {
                    CircleLineBar[SelectedCircleLineBar].Diameter = Math.Abs(Ty[1] - CircleShape[SelectedCircleShape].CenterY) * 2;
                }
            }
            if (RecEdgeToEdit == 24 || RecEdgeToEdit == 25 || RecEdgeToEdit == 23)
            {
                CircleLineBar[SelectedCircleLineBar].Diameter = Math.Abs(Ty[4] - Ty[1]);
                if (CircleLineBar[SelectedCircleLineBar].InCircleShape != 0)
                {
                    CircleLineBar[SelectedCircleLineBar].Diameter = Math.Abs(Ty[4] - CircleShape[SelectedCircleShape].CenterY) * 2;
                }
            }
            if (RecEdgeToEdit == 32 || RecEdgeToEdit == 1 || RecEdgeToEdit == 31)
            {
                CircleLineBar[SelectedCircleLineBar].Diameter = Math.Abs(Tx[2] - Tx[1]);
                if (CircleLineBar[SelectedCircleLineBar].InCircleShape != 0)
                {
                    CircleLineBar[SelectedCircleLineBar].Diameter = Math.Abs(Tx[2] - CircleShape[SelectedCircleShape].CenterX) * 2;
                }
            }
            if (RecEdgeToEdit == 16 || RecEdgeToEdit == 15 || RecEdgeToEdit == 17)
            {
                CircleLineBar[SelectedCircleLineBar].Diameter = Math.Abs(Tx[2] - Tx[1]);
                if (CircleLineBar[SelectedCircleLineBar].InCircleShape != 0)
                {
                    CircleLineBar[SelectedCircleLineBar].Diameter = Math.Abs(Tx[1] - CircleShape[SelectedCircleShape].CenterX) * 2;
                }
            }
            CircleLineBar[SelectedCircleLineBar].CenterX = (Tx[2] + Tx[1]) / 2;
            CircleLineBar[SelectedCircleLineBar].CenterY = (Ty[2] + Ty[3]) / 2;
            double Diameter = CircleLineBar[SelectedCircleLineBar].Diameter;
            if (CircleLineBar[SelectedCircleLineBar].InCircleShape != 0)
            {
                SelectedCircleShape = CircleLineBar[SelectedCircleLineBar].InCircleShape;
                CircleLineBar[SelectedCircleLineBar].CenterX = CircleShape[SelectedCircleShape].CenterX;
                CircleLineBar[SelectedCircleLineBar].CenterY = CircleShape[SelectedCircleShape].CenterY;
                CircleShape[SelectedCircleShape].CoverR = (CircleShape[SelectedCircleShape].Diameter - CircleLineBar[SelectedCircleLineBar].Diameter) / 2;
            }
            double CenterX = CircleLineBar[SelectedCircleLineBar].CenterX;
            double CenterY = CircleLineBar[SelectedCircleLineBar].CenterY;
            int pointN = 32;
            double zaweaa = 2 * Math.PI / pointN;
            double Diameter1 = Diameter / 2;
            for (int s = 1; s < pointN + 1; s++)
            {
                CircleLineBar[SelectedCircleLineBar].PointXR[s] = Math.Round(CenterX + Math.Cos(s * zaweaa) * Diameter1, 3);
                CircleLineBar[SelectedCircleLineBar].PointYR[s] = Math.Round(CenterY + Math.Sin(s * zaweaa) * Diameter1, 3);
            }
            int BarsNumbers = CircleLineBar[SelectedCircleLineBar].BarsNumbers;
            int DR = CircleLineBar[SelectedCircleLineBar].DR;
            int TIEDR = CircleLineBar[SelectedCircleLineBar].TIEDR;
            zaweaa = 2 * Math.PI / BarsNumbers;
            Diameter1 = Diameter / 2 - (DR / 2 + TIEDR);
            int m = 0;
            for (int s = 1; s < BarsNumber + 1; s++)
            {
                if (Bar[s].InCircle == SelectedCircleLineBar)
                {
                    m = m + 1;
                    Bar[s].XR = Math.Round(CenterX + Math.Cos(m * zaweaa) * Diameter1, 3);
                    Bar[s].YR = Math.Round(CenterY + Math.Sin(m * zaweaa) * Diameter1, 3);
                }
            }

            //       CircleShapes emp0 = new CircleShapes();
            //     emp0 = CircleShape[SelectedCircleShape];

            //   CircleShape[SelectedCircleShape] = emp0;
            RecEdgeToEdit = 0;
            ClickToEdit = 0;
            MakeTempFiles();
            Render2d();
            DrawForEditCircleLineBar();
        }
        private void DrawForEditRecShape()
        {
            Bitmap finalBmp = new Bitmap(BitampWidth2d, BitampHight2d);
            if (pictureBox2.Image != null)
            {
                pictureBox2.Image.Dispose();
                pictureBox2.Image = null;
            }
            pictureBox2.Image = finalBmp;
            if (SelectedRecShape != 0)
            {
                Graphics g = Graphics.FromImage(pictureBox2.Image);
                Pen pen = new Pen(Color.Navy, 3f);
                Pen pen1 = new Pen(Color.Blue, 5);
                Font drawFont = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
                SolidBrush drawBrush = new SolidBrush(Color.Blue);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                float[] dashValues = { 3, 3 };
                pen.DashPattern = dashValues;
                int thex = (TempX + LineMoveX1) / 2;
                int they = (TempY + LineMoveY1) / 2;
                int tx1 = 0;
                int ty1 = 0;
                int tx2 = 0;
                int ty2 = 0;
                int tx = 0;
                int ty = 0;
                if (RecEdgeToEdit == 0)
                {
                    for (int i = 1; i < 5; i++)
                    {
                        tx1 = RecShape[SelectedRecShape].EDX12D[i];
                        ty1 = RecShape[SelectedRecShape].EDY12D[i];
                        tx2 = RecShape[SelectedRecShape].EDX22D[i];
                        ty2 = RecShape[SelectedRecShape].EDY22D[i];
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        tx = ((tx1 + tx2) / 2) - 3;
                        ty = ((ty1 + ty2) / 2) - 3;
                        g.DrawRectangle(pen1, tx, ty, 6, 6);
                    }
                }
                if (RecEdgeToEdit == 1)
                {
                    tx1 = RecShape[SelectedRecShape].EDX12D[1];
                    ty1 = TempY;
                    tx2 = RecShape[SelectedRecShape].EDX22D[1];
                    ty2 = TempY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = RecShape[SelectedRecShape].EDX12D[2];
                    ty1 = TempY;
                    tx2 = RecShape[SelectedRecShape].EDX22D[2];
                    ty2 = RecShape[SelectedRecShape].EDY22D[2];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = RecShape[SelectedRecShape].EDX12D[3];
                    ty1 = RecShape[SelectedRecShape].EDY12D[3];
                    tx2 = RecShape[SelectedRecShape].EDX22D[3];
                    ty2 = RecShape[SelectedRecShape].EDY22D[3];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = RecShape[SelectedRecShape].EDX12D[4];
                    ty1 = RecShape[SelectedRecShape].EDY12D[4];
                    tx2 = RecShape[SelectedRecShape].EDX22D[4];
                    ty2 = TempY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    Tx[1] = RecShape[SelectedRecShape].EDX1R[1];
                    Ty[1] = Math.Round((startY2d - TempY) / Zoom2d, 3);
                    Tx[2] = RecShape[SelectedRecShape].EDX1R[2];
                    Ty[2] = Math.Round((startY2d - TempY) / Zoom2d, 3);
                    Tx[3] = RecShape[SelectedRecShape].EDX1R[3];
                    Ty[3] = RecShape[SelectedRecShape].EDY1R[3];
                    Tx[4] = RecShape[SelectedRecShape].EDX1R[4];
                    Ty[4] = RecShape[SelectedRecShape].EDY1R[4];
                }
                if (RecEdgeToEdit == 3)
                {
                    tx1 = RecShape[SelectedRecShape].EDX12D[1];
                    ty1 = RecShape[SelectedRecShape].EDY12D[1];
                    tx2 = RecShape[SelectedRecShape].EDX22D[1];
                    ty2 = RecShape[SelectedRecShape].EDY22D[1];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = RecShape[SelectedRecShape].EDX12D[2];
                    ty1 = RecShape[SelectedRecShape].EDY12D[2];
                    tx2 = RecShape[SelectedRecShape].EDX22D[2];
                    ty2 = TempY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = RecShape[SelectedRecShape].EDX12D[3];
                    ty1 = TempY;
                    tx2 = RecShape[SelectedRecShape].EDX22D[3];
                    ty2 = TempY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = RecShape[SelectedRecShape].EDX12D[4];
                    ty1 = TempY;
                    tx2 = RecShape[SelectedRecShape].EDX22D[4];
                    ty2 = RecShape[SelectedRecShape].EDY22D[4];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    Tx[1] = RecShape[SelectedRecShape].EDX1R[1];
                    Ty[1] = RecShape[SelectedRecShape].EDY1R[1];
                    Tx[2] = RecShape[SelectedRecShape].EDX1R[2];
                    Ty[2] = RecShape[SelectedRecShape].EDY1R[2];
                    Tx[3] = RecShape[SelectedRecShape].EDX1R[3];
                    Ty[3] = Math.Round((startY2d - TempY) / Zoom2d, 3);
                    Tx[4] = RecShape[SelectedRecShape].EDX1R[4];
                    Ty[4] = Math.Round((startY2d - TempY) / Zoom2d, 3);
                }
                if (RecEdgeToEdit == 2)
                {
                    tx1 = RecShape[SelectedRecShape].EDX12D[1];
                    ty1 = RecShape[SelectedRecShape].EDY12D[1];
                    tx2 = TempX;
                    ty2 = RecShape[SelectedRecShape].EDY22D[1];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = TempX;
                    ty1 = RecShape[SelectedRecShape].EDY12D[2];
                    tx2 = TempX;
                    ty2 = RecShape[SelectedRecShape].EDY22D[2];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = TempX;
                    ty1 = RecShape[SelectedRecShape].EDY12D[3];
                    tx2 = RecShape[SelectedRecShape].EDX22D[3];
                    ty2 = RecShape[SelectedRecShape].EDY22D[3];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = RecShape[SelectedRecShape].EDX12D[4];
                    ty1 = RecShape[SelectedRecShape].EDY12D[4];
                    tx2 = RecShape[SelectedRecShape].EDX22D[4];
                    ty2 = RecShape[SelectedRecShape].EDY22D[4];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    Tx[1] = RecShape[SelectedRecShape].EDX1R[1];
                    Ty[1] = RecShape[SelectedRecShape].EDY1R[1];
                    Tx[2] = Math.Round((TempX - startX2d) / Zoom2d, 3);
                    Ty[2] = RecShape[SelectedRecShape].EDY1R[2];
                    Tx[3] = Math.Round((TempX - startX2d) / Zoom2d, 3);
                    Ty[3] = RecShape[SelectedRecShape].EDY1R[3];
                    Tx[4] = RecShape[SelectedRecShape].EDX1R[4];
                    Ty[4] = RecShape[SelectedRecShape].EDY1R[4];
                }
                if (RecEdgeToEdit == 4)
                {
                    tx1 = TempX;
                    ty1 = RecShape[SelectedRecShape].EDY12D[1];
                    tx2 = RecShape[SelectedRecShape].EDX22D[1];
                    ty2 = RecShape[SelectedRecShape].EDY22D[1];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = RecShape[SelectedRecShape].EDX12D[2];
                    ty1 = RecShape[SelectedRecShape].EDY12D[2];
                    tx2 = RecShape[SelectedRecShape].EDX22D[2];
                    ty2 = RecShape[SelectedRecShape].EDY22D[2];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = RecShape[SelectedRecShape].EDX12D[3];
                    ty1 = RecShape[SelectedRecShape].EDY12D[3];
                    tx2 = TempX;
                    ty2 = RecShape[SelectedRecShape].EDY22D[3];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = TempX;
                    ty1 = RecShape[SelectedRecShape].EDY12D[4];
                    tx2 = TempX;
                    ty2 = RecShape[SelectedRecShape].EDY22D[4];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    Tx[1] = Math.Round((TempX - startX2d) / Zoom2d, 3);
                    Ty[1] = RecShape[SelectedRecShape].EDY1R[1];
                    Tx[2] = RecShape[SelectedRecShape].EDX1R[2];
                    Ty[2] = RecShape[SelectedRecShape].EDY1R[2];
                    Tx[3] = RecShape[SelectedRecShape].EDX1R[3];
                    Ty[3] = RecShape[SelectedRecShape].EDY1R[3];
                    Tx[4] = Math.Round((TempX - startX2d) / Zoom2d, 3);
                    Ty[4] = RecShape[SelectedRecShape].EDY1R[4];
                }
            }
        }
        private void EditRecShape()
        {
            RecShape[SelectedRecShape].Width = Math.Abs(Tx[2] - Tx[1]);
            RecShape[SelectedRecShape].Height = Math.Abs(Ty[2] - Ty[3]);
            RecShape[SelectedRecShape].CenterX = (Tx[2] + Tx[1]) / 2;
            RecShape[SelectedRecShape].CenterY = (Ty[2] + Ty[3]) / 2;
            RecShape[SelectedRecShape].EDX1R[1] = RecShape[SelectedRecShape].CenterX - RecShape[SelectedRecShape].Width / 2;
            RecShape[SelectedRecShape].EDY1R[1] = RecShape[SelectedRecShape].CenterY + RecShape[SelectedRecShape].Height / 2;
            RecShape[SelectedRecShape].EDX2R[1] = RecShape[SelectedRecShape].CenterX + RecShape[SelectedRecShape].Width / 2;
            RecShape[SelectedRecShape].EDY2R[1] = RecShape[SelectedRecShape].CenterY + RecShape[SelectedRecShape].Height / 2;
            RecShape[SelectedRecShape].EDX1R[2] = RecShape[SelectedRecShape].CenterX + RecShape[SelectedRecShape].Width / 2;
            RecShape[SelectedRecShape].EDY1R[2] = RecShape[SelectedRecShape].CenterY + RecShape[SelectedRecShape].Height / 2;
            RecShape[SelectedRecShape].EDX2R[2] = RecShape[SelectedRecShape].CenterX + RecShape[SelectedRecShape].Width / 2;
            RecShape[SelectedRecShape].EDY2R[2] = RecShape[SelectedRecShape].CenterY - RecShape[SelectedRecShape].Height / 2;
            RecShape[SelectedRecShape].EDX1R[3] = RecShape[SelectedRecShape].CenterX + RecShape[SelectedRecShape].Width / 2;
            RecShape[SelectedRecShape].EDY1R[3] = RecShape[SelectedRecShape].CenterY - RecShape[SelectedRecShape].Height / 2;
            RecShape[SelectedRecShape].EDX2R[3] = RecShape[SelectedRecShape].CenterX - RecShape[SelectedRecShape].Width / 2;
            RecShape[SelectedRecShape].EDY2R[3] = RecShape[SelectedRecShape].CenterY - RecShape[SelectedRecShape].Height / 2;
            RecShape[SelectedRecShape].EDX1R[4] = RecShape[SelectedRecShape].CenterX - RecShape[SelectedRecShape].Width / 2;
            RecShape[SelectedRecShape].EDY1R[4] = RecShape[SelectedRecShape].CenterY - RecShape[SelectedRecShape].Height / 2;
            RecShape[SelectedRecShape].EDX2R[4] = RecShape[SelectedRecShape].CenterX - RecShape[SelectedRecShape].Width / 2;
            RecShape[SelectedRecShape].EDY2R[4] = RecShape[SelectedRecShape].CenterY + RecShape[SelectedRecShape].Height / 2;
            RecShapes emp0 = new RecShapes();
            emp0 = RecShape[SelectedRecShape];
            #region//يوجد تسليح
            if (RecShape[SelectedRecShape].HasReinforcment == 1)
            {
                SelectedRecBar = RecShape[SelectedRecShape].ApplyedToRecBars;
                RecLineBars emp0R = new RecLineBars();
                emp0R = RecLineBar[SelectedRecBar];
                double X1 = 0;
                double Y1 = 0;
                double length = 0;
                int NBAR = 0;
                int diameter = 0;
                double DIS = 0;
                int TheBars = 0;
                double TheDistance = 0;
                int diameter1 = 0;
                #region//الحذف و الترتيب
            startloop: { }
                for (int k = 1; k < BarsNumber + 1; k++)
                {
                    if (Bar[k].InREC == SelectedRecBar)
                    {
                        BarsNumber = BarsNumber - 1;
                        for (int l = k; l < BarsNumber + 1; l++)
                        {
                            Bar[l] = Bar[l + 1];
                        }
                        if (k <= BarsNumber) goto startloop;
                    }
                }
                for (int k = 1; k < BarsNumber + 1; k++)
                {
                    if (Bar[k].InREC > SelectedRecBar)
                    {
                        Bar[k].InREC = Bar[k].InREC - 1;
                    }
                }
                for (int k = SelectedRecBar; k < RecBarsNumber; k++)
                {
                    RecLineBar[k] = RecLineBar[k + 1];
                }
                for (int k = SelectedRecShape; k < RecShapeNumber; k++)//مضاف
                {
                    RecShape[k] = RecShape[k + 1];
                }
                for (int k = 1; k < RecBarsNumber; k++)//مضاف
                {
                    if (RecLineBar[k].InRecShape > SelectedRecShape)
                    {
                        RecLineBar[k].InRecShape = RecLineBar[k].InRecShape - 1;
                    }
                }
                for (int k = 1; k < RecShapeNumber; k++)//مضاف
                {
                    if (RecShape[k].ApplyedToRecBars > SelectedRecBar)
                    {
                        RecShape[k].ApplyedToRecBars = RecShape[k].ApplyedToRecBars - 1;
                    }
                }
                for (int k = 1; k < FlangedWallShapeNumber + 1; k++)//مضاف
                {
                    if (FlangedWallShape[k].ApplyedToRecBars[1] > SelectedRecBar)
                    {
                        FlangedWallShape[k].ApplyedToRecBars[1] = FlangedWallShape[k].ApplyedToRecBars[1] - 1;
                        FlangedWallShape[k].ApplyedToRecBars[2] = FlangedWallShape[k].ApplyedToRecBars[2] - 1;
                        FlangedWallShape[k].ApplyedToRecBars[3] = FlangedWallShape[k].ApplyedToRecBars[3] - 1;
                    }
                }
                #endregion
                SelectedRecShape = RecShapeNumber;
                SelectedRecBar = RecBarsNumber;
                RecLineBars emp = new RecLineBars();
                emp.InRecShape = SelectedRecShape;
                emp.Width = emp0.Width - emp0.EDCoverR[2] - emp0.EDCoverR[4];
                emp.Height = emp0.Height - emp0.EDCoverR[1] - emp0.EDCoverR[3];
                emp.CenterX = emp0.EDX1R[1] + emp0.EDCoverR[4] + emp.Width / 2;
                emp.CenterY = emp0.EDY1R[1] - emp0.EDCoverR[1] - emp.Height / 2;
                emp.TIEDR = emp0R.TIEDR;
                emp.CORDR[1] = emp0R.CORDR[1];
                emp.CORDR[2] = emp0R.CORDR[2];
                emp.CORDR[3] = emp0R.CORDR[3];
                emp.CORDR[4] = emp0R.CORDR[4];
                diameter1 = emp.TIEDR;
                length = emp.Width;
                TheDistance = emp0R.EDDistance[1];
                TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                emp.EDDR[1] = emp0R.EDDR[1];
                emp.EDDistance[1] = emp0R.EDDistance[1];
                emp.EDBarsNumbers[1] = TheBars;
                emp.EDX1R[1] = emp.CenterX - emp.Width / 2;
                emp.EDY1R[1] = emp.CenterY + emp.Height / 2;
                emp.EDX2R[1] = emp.CenterX + emp.Width / 2;
                emp.EDY2R[1] = emp.CenterY + emp.Height / 2;
                length = emp.Height;
                TheDistance = emp0R.EDDistance[2];
                TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                emp.EDDR[2] = emp0R.EDDR[2];
                emp.EDDistance[2] = emp0R.EDDistance[2];
                emp.EDBarsNumbers[2] = TheBars;
                emp.EDX1R[2] = emp.CenterX + emp.Width / 2;
                emp.EDY1R[2] = emp.CenterY + emp.Height / 2;
                emp.EDX2R[2] = emp.CenterX + emp.Width / 2;
                emp.EDY2R[2] = emp.CenterY - emp.Height / 2;
                length = emp.Width;
                TheDistance = emp0R.EDDistance[3];
                TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                emp.EDDR[3] = emp0R.EDDR[3];
                emp.EDDistance[3] = emp0R.EDDistance[3];
                emp.EDBarsNumbers[3] = TheBars;
                emp.EDX1R[3] = emp.CenterX + emp.Width / 2;
                emp.EDY1R[3] = emp.CenterY - emp.Height / 2;
                emp.EDX2R[3] = emp.CenterX - emp.Width / 2;
                emp.EDY2R[3] = emp.CenterY - emp.Height / 2;
                length = emp.Height;
                TheDistance = emp0R.EDDistance[4];
                TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                emp.EDDR[4] = emp0R.EDDR[4];
                emp.EDDistance[4] = emp0R.EDDistance[4];
                emp.EDBarsNumbers[4] = TheBars;
                emp.EDX1R[4] = emp.CenterX - emp.Width / 2;
                emp.EDY1R[4] = emp.CenterY - emp.Height / 2;
                emp.EDX2R[4] = emp.CenterX - emp.Width / 2;
                emp.EDY2R[4] = emp.CenterY + emp.Height / 2;

                for (int s = 1; s < 5; s++)
                {
                    int kl1 = 0;
                    int kl2 = 0;
                    if (s == 1 || s == 3)
                    {
                        length = emp.Width;
                        kl1 = 1;
                        kl2 = 0;
                    }
                    if (s == 2 || s == 4)
                    {
                        length = emp.Height;
                        kl1 = 0;
                        kl2 = 1;
                    }
                    NBAR = emp.EDBarsNumbers[s];
                    diameter = emp.EDDR[s];
                    DIS = Math.Round((length - 2 * diameter1) / (NBAR + 1), 3);
                    X1 = emp.EDX1R[s];
                    Y1 = emp.EDY1R[s];
                    for (int k = 1; k < NBAR + 1; k++)
                    {
                        BarsNumber = BarsNumber + 1;
                        Bars emp1 = new Bars();
                        emp1.XR = Math.Round(X1 + kl1 * kx[s] * (k * DIS) + kx[s] * diameter1 + kl2 * ky[s] * (diameter / 2), 3);
                        emp1.YR = Math.Round(Y1 + kl2 * ky[s] * (k * DIS) + ky[s] * diameter1 + kl1 * ky[s] * (diameter / 2), 3);
                        emp1.DR = diameter;
                        emp1.Selected = 0;
                        emp1.InLine = 0;
                        emp1.Type = 3;
                        emp1.InED = s;
                        emp1.InREC = RecBarsNumber;
                        emp1.Corner = 0;
                        emp1.InCircle = 0;
                        Bar[BarsNumber] = emp1;
                    }
                }
                for (int k = 1; k < 5; k++)
                {
                    BarsNumber = BarsNumber + 1;
                    Bars emp1 = new Bars();
                    diameter = emp.CORDR[k];
                    if (k == 1)
                    {
                        emp1.XR = Math.Round(emp0.EDX1R[1] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.EDCoverR[4], 3);
                        emp1.YR = Math.Round(emp0.EDY1R[1] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.EDCoverR[1], 3);
                    }
                    if (k == 2)
                    {
                        emp1.XR = Math.Round(emp0.EDX2R[1] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.EDCoverR[2], 3);
                        emp1.YR = Math.Round(emp0.EDY2R[1] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.EDCoverR[1], 3);
                    }
                    if (k == 3)
                    {
                        emp1.XR = Math.Round(emp0.EDX1R[3] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.EDCoverR[2], 3);
                        emp1.YR = Math.Round(emp0.EDY1R[3] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.EDCoverR[3], 3);
                    }
                    if (k == 4)
                    {
                        emp1.XR = Math.Round(emp0.EDX2R[3] + kx[k] * (diameter / 2) + kx[k] * diameter1 + kx[k] * emp0.EDCoverR[4], 3);
                        emp1.YR = Math.Round(emp0.EDY2R[3] + ky[k] * (diameter / 2) + ky[k] * diameter1 + ky[k] * emp0.EDCoverR[3], 3);
                    }
                    emp1.DR = diameter;
                    emp1.Selected = 0;
                    emp1.InLine = 0;
                    emp1.Type = 4;
                    emp1.InED = 0;
                    emp1.InCircle = 0;
                    emp1.Corner = k;
                    emp1.InREC = RecBarsNumber;
                    Bar[BarsNumber] = emp1;
                }
                RecLineBar[RecBarsNumber] = emp;
                emp0.ApplyedToRecBars = SelectedRecBar;
            }
            #endregion
            RecShape[SelectedRecShape] = emp0;
            RecEdgeToEdit = 0;
            ClickToEdit = 0;
            MakeTempFiles();
            Render2d();
        }
        private void DrawForEditRecLineBar()
        {
            Bitmap finalBmp = new Bitmap(BitampWidth2d, BitampHight2d);
            if (pictureBox2.Image != null)
            {
                pictureBox2.Image.Dispose();
                pictureBox2.Image = null;
            }
            pictureBox2.Image = finalBmp;
            if (SelectedRecBar != 0)
            {
                Graphics g = Graphics.FromImage(pictureBox2.Image);
                Pen pen = new Pen(Color.Navy, 3f);
                Pen pen1 = new Pen(Color.Blue, 5);
                Font drawFont = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
                SolidBrush drawBrush = new SolidBrush(Color.Blue);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                float[] dashValues = { 3, 3 };
                pen.DashPattern = dashValues;
                int thex = (TempX + LineMoveX1) / 2;
                int they = (TempY + LineMoveY1) / 2;
                int tx1 = 0;
                int ty1 = 0;
                int tx2 = 0;
                int ty2 = 0;
                int tx = 0;
                int ty = 0;
                if (RecEdgeToEdit == 0)
                {
                    for (int i = 1; i < 5; i++)
                    {
                        tx1 = RecLineBar[SelectedRecBar].EDX12D[i];
                        ty1 = RecLineBar[SelectedRecBar].EDY12D[i];
                        tx2 = RecLineBar[SelectedRecBar].EDX22D[i];
                        ty2 = RecLineBar[SelectedRecBar].EDY22D[i];
                        g.DrawLine(pen, tx1, ty1, tx2, ty2);
                        tx = ((tx1 + tx2) / 2) - 3;
                        ty = ((ty1 + ty2) / 2) - 3;
                        g.DrawRectangle(pen1, tx, ty, 6, 6);
                    }
                }
                if (RecEdgeToEdit == 1)
                {
                    tx1 = RecLineBar[SelectedRecBar].EDX12D[1];
                    ty1 = TempY;
                    tx2 = RecLineBar[SelectedRecBar].EDX22D[1];
                    ty2 = TempY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = RecLineBar[SelectedRecBar].EDX12D[2];
                    ty1 = TempY;
                    tx2 = RecLineBar[SelectedRecBar].EDX22D[2];
                    ty2 = RecLineBar[SelectedRecBar].EDY22D[2];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = RecLineBar[SelectedRecBar].EDX12D[3];
                    ty1 = RecLineBar[SelectedRecBar].EDY12D[3];
                    tx2 = RecLineBar[SelectedRecBar].EDX22D[3];
                    ty2 = RecLineBar[SelectedRecBar].EDY22D[3];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = RecLineBar[SelectedRecBar].EDX12D[4];
                    ty1 = RecLineBar[SelectedRecBar].EDY12D[4];
                    tx2 = RecLineBar[SelectedRecBar].EDX22D[4];
                    ty2 = TempY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    Tx[1] = RecLineBar[SelectedRecBar].EDX1R[1];
                    Ty[1] = Math.Round((startY2d - TempY) / Zoom2d, 3);
                    Tx[2] = RecLineBar[SelectedRecBar].EDX1R[2];
                    Ty[2] = Math.Round((startY2d - TempY) / Zoom2d, 3);
                    Tx[3] = RecLineBar[SelectedRecBar].EDX1R[3];
                    Ty[3] = RecLineBar[SelectedRecBar].EDY1R[3];
                    Tx[4] = RecLineBar[SelectedRecBar].EDX1R[4];
                    Ty[4] = RecLineBar[SelectedRecBar].EDY1R[4];
                }
                if (RecEdgeToEdit == 3)
                {
                    tx1 = RecLineBar[SelectedRecBar].EDX12D[1];
                    ty1 = RecLineBar[SelectedRecBar].EDY12D[1];
                    tx2 = RecLineBar[SelectedRecBar].EDX22D[1];
                    ty2 = RecLineBar[SelectedRecBar].EDY22D[1];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = RecLineBar[SelectedRecBar].EDX12D[2];
                    ty1 = RecLineBar[SelectedRecBar].EDY12D[2];
                    tx2 = RecLineBar[SelectedRecBar].EDX22D[2];
                    ty2 = TempY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = RecLineBar[SelectedRecBar].EDX12D[3];
                    ty1 = TempY;
                    tx2 = RecLineBar[SelectedRecBar].EDX22D[3];
                    ty2 = TempY;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = RecLineBar[SelectedRecBar].EDX12D[4];
                    ty1 = TempY;
                    tx2 = RecLineBar[SelectedRecBar].EDX22D[4];
                    ty2 = RecLineBar[SelectedRecBar].EDY22D[4];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    Tx[1] = RecLineBar[SelectedRecBar].EDX1R[1];
                    Ty[1] = RecLineBar[SelectedRecBar].EDY1R[1];
                    Tx[2] = RecLineBar[SelectedRecBar].EDX1R[2];
                    Ty[2] = RecLineBar[SelectedRecBar].EDY1R[2];
                    Tx[3] = RecLineBar[SelectedRecBar].EDX1R[3];
                    Ty[3] = Math.Round((startY2d - TempY) / Zoom2d, 3);
                    Tx[4] = RecLineBar[SelectedRecBar].EDX1R[4];
                    Ty[4] = Math.Round((startY2d - TempY) / Zoom2d, 3);
                }
                if (RecEdgeToEdit == 2)
                {
                    tx1 = RecLineBar[SelectedRecBar].EDX12D[1];
                    ty1 = RecLineBar[SelectedRecBar].EDY12D[1];
                    tx2 = TempX;
                    ty2 = RecLineBar[SelectedRecBar].EDY22D[1];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = TempX;
                    ty1 = RecLineBar[SelectedRecBar].EDY12D[2];
                    tx2 = TempX;
                    ty2 = RecLineBar[SelectedRecBar].EDY22D[2];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = TempX;
                    ty1 = RecLineBar[SelectedRecBar].EDY12D[3];
                    tx2 = RecLineBar[SelectedRecBar].EDX22D[3];
                    ty2 = RecLineBar[SelectedRecBar].EDY22D[3];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = RecLineBar[SelectedRecBar].EDX12D[4];
                    ty1 = RecLineBar[SelectedRecBar].EDY12D[4];
                    tx2 = RecLineBar[SelectedRecBar].EDX22D[4];
                    ty2 = RecLineBar[SelectedRecBar].EDY22D[4];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    Tx[1] = RecLineBar[SelectedRecBar].EDX1R[1];
                    Ty[1] = RecLineBar[SelectedRecBar].EDY1R[1];
                    Tx[2] = Math.Round((TempX - startX2d) / Zoom2d, 3);
                    Ty[2] = RecLineBar[SelectedRecBar].EDY1R[2];
                    Tx[3] = Math.Round((TempX - startX2d) / Zoom2d, 3);
                    Ty[3] = RecLineBar[SelectedRecBar].EDY1R[3];
                    Tx[4] = RecLineBar[SelectedRecBar].EDX1R[4];
                    Ty[4] = RecLineBar[SelectedRecBar].EDY1R[4];
                }
                if (RecEdgeToEdit == 4)
                {
                    tx1 = TempX;
                    ty1 = RecLineBar[SelectedRecBar].EDY12D[1];
                    tx2 = RecLineBar[SelectedRecBar].EDX22D[1];
                    ty2 = RecLineBar[SelectedRecBar].EDY22D[1];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = RecLineBar[SelectedRecBar].EDX12D[2];
                    ty1 = RecLineBar[SelectedRecBar].EDY12D[2];
                    tx2 = RecLineBar[SelectedRecBar].EDX22D[2];
                    ty2 = RecLineBar[SelectedRecBar].EDY22D[2];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = RecLineBar[SelectedRecBar].EDX12D[3];
                    ty1 = RecLineBar[SelectedRecBar].EDY12D[3];
                    tx2 = TempX;
                    ty2 = RecLineBar[SelectedRecBar].EDY22D[3];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    tx1 = TempX;
                    ty1 = RecLineBar[SelectedRecBar].EDY12D[4];
                    tx2 = TempX;
                    ty2 = RecLineBar[SelectedRecBar].EDY22D[4];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    tx = ((tx1 + tx2) / 2) - 3;
                    ty = ((ty1 + ty2) / 2) - 3;
                    g.DrawRectangle(pen1, tx, ty, 6, 6);
                    Tx[1] = Math.Round((TempX - startX2d) / Zoom2d, 3);
                    Ty[1] = RecLineBar[SelectedRecBar].EDY1R[1];
                    Tx[2] = RecLineBar[SelectedRecBar].EDX1R[2];
                    Ty[2] = RecLineBar[SelectedRecBar].EDY1R[2];
                    Tx[3] = RecLineBar[SelectedRecBar].EDX1R[3];
                    Ty[3] = RecLineBar[SelectedRecBar].EDY1R[3];
                    Tx[4] = Math.Round((TempX - startX2d) / Zoom2d, 3);
                    Ty[4] = RecLineBar[SelectedRecBar].EDY1R[4];
                }
            }
        }
        private void EditRecLineBar()
        {
            double X1 = 0;
            double Y1 = 0;
            double length = 0;
            int NBAR = 0;
            int diameter = 0;
            double DIS = 0;
            int TheBars = 0;
            double TheDistance = 0;
            int diameter1 = 0;
            int hala = 0;
            #region//تحديد اذا كان ينتمي لجدار قص
            for (int k = 1; k < FlangedWallShapeNumber + 1; k++)//مضاف
            {
                if (FlangedWallShape[k].ApplyedToRecBars[1] == SelectedRecBar)
                {
                    hala = 1;
                    SelectedRecBar1 = FlangedWallShape[k].ApplyedToRecBars[1];
                    SelectedRecBar2 = FlangedWallShape[k].ApplyedToRecBars[2];
                    SelectedRecBar3 = FlangedWallShape[k].ApplyedToRecBars[3];
                    break;
                }
                if (FlangedWallShape[k].ApplyedToRecBars[2] == SelectedRecBar)
                {
                    hala = 2;
                    SelectedRecBar1 = FlangedWallShape[k].ApplyedToRecBars[1];
                    SelectedRecBar2 = FlangedWallShape[k].ApplyedToRecBars[2];
                    SelectedRecBar3 = FlangedWallShape[k].ApplyedToRecBars[3];
                    break;
                }
                if (FlangedWallShape[k].ApplyedToRecBars[3] == SelectedRecBar)
                {
                    hala = 3;
                    SelectedRecBar1 = FlangedWallShape[k].ApplyedToRecBars[1];
                    SelectedRecBar2 = FlangedWallShape[k].ApplyedToRecBars[2];
                    SelectedRecBar3 = FlangedWallShape[k].ApplyedToRecBars[3];
                    break;
                }
            }
            #endregion
            #region//لا ينتمي إلى جدار قص
            if (hala == 0)
            {
                RecLineBars emp0R = new RecLineBars();
                emp0R = RecLineBar[SelectedRecBar];
                int thetype = RecLineBar[SelectedRecBar].Type;
                RecShapes emp0 = new RecShapes();
                if (SelectedRecShape != 0) emp0 = RecShape[SelectedRecShape];
                #region//الحذف و الترتيب
            startloop: { }
                for (int k = 1; k < BarsNumber + 1; k++)
                {
                    if (Bar[k].InREC == SelectedRecBar)
                    {
                        BarsNumber = BarsNumber - 1;
                        for (int l = k; l < BarsNumber + 1; l++)
                        {
                            Bar[l] = Bar[l + 1];
                        }
                        if (k <= BarsNumber) goto startloop;
                    }
                }
                for (int k = 1; k < BarsNumber + 1; k++)
                {
                    if (Bar[k].InREC > SelectedRecBar)
                    {
                        Bar[k].InREC = Bar[k].InREC - 1;
                    }
                }
                for (int k = SelectedRecBar; k < RecBarsNumber; k++)
                {
                    RecLineBar[k] = RecLineBar[k + 1];
                }
                #region///////////////////////////////////////////// ينتمي الى مستطيل
                if (SelectedRecShape != 0)
                {
                    for (int k = SelectedRecShape; k < RecShapeNumber; k++)//مضاف****************
                    {
                        RecShape[k] = RecShape[k + 1];
                    }
                    for (int k = 1; k < RecBarsNumber; k++)//مضاف****************
                    {
                        if (RecLineBar[k].InRecShape > SelectedRecShape)
                        {
                            RecLineBar[k].InRecShape = RecLineBar[k].InRecShape - 1;
                        }
                    }
                }
                #endregion
                #region/////////////////////////////////////////////ينتمي الى جدار
                if (SelectedFlangedWallShape != 0)
                {
                    for (int k = SelectedFlangedWallShape; k < FlangedWallShapeNumber; k++)//مضاف****************
                    {
                        FlangedWallShape[k] = FlangedWallShape[k + 1];
                    }
                    for (int k = 1; k < RecBarsNumber; k++)//مضاف****************
                    {
                        if (RecLineBar[k].InFlangedWallShape > SelectedFlangedWallShape)
                        {
                            RecLineBar[k].InFlangedWallShape = RecLineBar[k].InFlangedWallShape - 1;
                        }
                    }
                }
                #endregion
                for (int k = 1; k < RecShapeNumber + 1; k++)//مضاف
                {
                    if (RecShape[k].ApplyedToRecBars > SelectedRecBar)
                    {
                        RecShape[k].ApplyedToRecBars = RecShape[k].ApplyedToRecBars - 1;
                    }
                }
                for (int k = 1; k < FlangedWallShapeNumber + 1; k++)//مضاف
                {
                    if (FlangedWallShape[k].ApplyedToRecBars[1] > SelectedRecBar)
                    {
                        FlangedWallShape[k].ApplyedToRecBars[1] = FlangedWallShape[k].ApplyedToRecBars[1] - 1;
                        FlangedWallShape[k].ApplyedToRecBars[2] = FlangedWallShape[k].ApplyedToRecBars[2] - 1;
                        FlangedWallShape[k].ApplyedToRecBars[3] = FlangedWallShape[k].ApplyedToRecBars[3] - 1;
                    }
                }
                #endregion
                SelectedRecBar = RecBarsNumber;
                if (SelectedRecShape != 0) SelectedRecShape = RecShapeNumber;
                #region//أبعاد ومواصفات أسوارة التسليح
                RecLineBars emp = new RecLineBars();
                emp.InRecShape = SelectedRecShape;
                emp.Type = thetype;
                emp.InFlangedWallShape = SelectedFlangedWallShape;
                emp.Width = Math.Abs(Tx[2] - Tx[1]);
                emp.Height = Math.Abs(Ty[2] - Ty[3]);
                emp.CenterX = (Tx[2] + Tx[1]) / 2;
                emp.CenterY = (Ty[2] + Ty[3]) / 2;
                emp.EDX1R[1] = emp.CenterX - emp.Width / 2;
                emp.EDY1R[1] = emp.CenterY + emp.Height / 2;
                emp.EDX2R[1] = emp.CenterX + emp.Width / 2;
                emp.EDY2R[1] = emp.CenterY + emp.Height / 2;
                emp.EDX1R[2] = emp.CenterX + emp.Width / 2;
                emp.EDY1R[2] = emp.CenterY + emp.Height / 2;
                emp.EDX2R[2] = emp.CenterX + emp.Width / 2;
                emp.EDY2R[2] = emp.CenterY - emp.Height / 2;
                emp.EDX1R[3] = emp.CenterX + emp.Width / 2;
                emp.EDY1R[3] = emp.CenterY - emp.Height / 2;
                emp.EDX2R[3] = emp.CenterX - emp.Width / 2;
                emp.EDY2R[3] = emp.CenterY - emp.Height / 2;
                emp.EDX1R[4] = emp.CenterX - emp.Width / 2;
                emp.EDY1R[4] = emp.CenterY - emp.Height / 2;
                emp.EDX2R[4] = emp.CenterX - emp.Width / 2;
                emp.EDY2R[4] = emp.CenterY + emp.Height / 2;
                emp.TIEDR = emp0R.TIEDR;
                diameter1 = emp.TIEDR;
                #endregion
                if (thetype == 0)
                {
                    #region//القضبان الداخلية
                    length = emp.Width;
                    TheDistance = emp0R.EDDistance[1];
                    TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                    emp.EDDR[1] = emp0R.EDDR[1];
                    emp.EDDistance[1] = emp0R.EDDistance[1];
                    emp.EDBarsNumbers[1] = TheBars;
                    length = emp.Height;
                    TheDistance = emp0R.EDDistance[2];
                    TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                    emp.EDDR[2] = emp0R.EDDR[2];
                    emp.EDDistance[2] = emp0R.EDDistance[2];
                    emp.EDBarsNumbers[2] = TheBars;
                    length = emp.Width;
                    TheDistance = emp0R.EDDistance[3];
                    TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                    emp.EDDR[3] = emp0R.EDDR[3];
                    emp.EDDistance[3] = emp0R.EDDistance[3];
                    emp.EDBarsNumbers[3] = TheBars;
                    length = emp.Height;
                    TheDistance = emp0R.EDDistance[4];
                    TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                    emp.EDDR[4] = emp0R.EDDR[4];
                    emp.EDDistance[4] = emp0R.EDDistance[4];
                    emp.EDBarsNumbers[4] = TheBars;
                    for (int s = 1; s < 5; s++)
                    {
                        int kl1 = 0;
                        int kl2 = 0;
                        if (s == 1 || s == 3)
                        {
                            length = emp.Width;
                            kl1 = 1;
                            kl2 = 0;
                        }
                        if (s == 2 || s == 4)
                        {
                            length = emp.Height;
                            kl1 = 0;
                            kl2 = 1;
                        }
                        NBAR = emp.EDBarsNumbers[s];
                        diameter = emp.EDDR[s];
                        DIS = Math.Round((length - 2 * diameter1) / (NBAR + 1), 3);
                        X1 = emp.EDX1R[s];
                        Y1 = emp.EDY1R[s];
                        for (int k = 1; k < NBAR + 1; k++)
                        {
                            BarsNumber = BarsNumber + 1;
                            Bars emp1 = new Bars();
                            emp1.XR = Math.Round(X1 + kl1 * kx[s] * (k * DIS) + kx[s] * diameter1 + kl2 * ky[s] * (diameter / 2), 3);
                            emp1.YR = Math.Round(Y1 + kl2 * ky[s] * (k * DIS) + ky[s] * diameter1 + kl1 * ky[s] * (diameter / 2), 3);
                            emp1.DR = diameter;
                            emp1.Selected = 0;
                            emp1.InLine = 0;
                            emp1.Type = 3;
                            emp1.InED = s;
                            emp1.InREC = RecBarsNumber;
                            emp1.Corner = 0;
                            emp1.InCircle = 0;
                            Bar[BarsNumber] = emp1;
                        }
                    }
                    #endregion
                    #region//قضبان الزوايا
                    emp.CORDR[1] = emp0R.CORDR[1];
                    emp.CORDR[2] = emp0R.CORDR[2];
                    emp.CORDR[3] = emp0R.CORDR[3];
                    emp.CORDR[4] = emp0R.CORDR[4];
                    for (int k = 1; k < 5; k++)
                    {
                        BarsNumber = BarsNumber + 1;
                        Bars emp1 = new Bars();
                        diameter = emp.CORDR[k];
                        if (k == 1)
                        {
                            emp1.XR = Math.Round(emp.EDX1R[1] + kx[k] * (diameter / 2) + kx[k] * diameter1, 3);
                            emp1.YR = Math.Round(emp.EDY1R[1] + ky[k] * (diameter / 2) + ky[k] * diameter1, 3);
                        }
                        if (k == 2)
                        {
                            emp1.XR = Math.Round(emp.EDX2R[1] + kx[k] * (diameter / 2) + kx[k] * diameter1, 3);
                            emp1.YR = Math.Round(emp.EDY2R[1] + ky[k] * (diameter / 2) + ky[k] * diameter1, 3);
                        }
                        if (k == 3)
                        {
                            emp1.XR = Math.Round(emp.EDX1R[3] + kx[k] * (diameter / 2) + kx[k] * diameter1, 3);
                            emp1.YR = Math.Round(emp.EDY1R[3] + ky[k] * (diameter / 2) + ky[k] * diameter1, 3);
                        }
                        if (k == 4)
                        {
                            emp1.XR = Math.Round(emp.EDX2R[3] + kx[k] * (diameter / 2) + kx[k] * diameter1, 3);
                            emp1.YR = Math.Round(emp.EDY2R[3] + ky[k] * (diameter / 2) + ky[k] * diameter1, 3);
                        }
                        emp1.DR = diameter;
                        emp1.Selected = 0;
                        emp1.InLine = 0;
                        emp1.Type = 4;
                        emp1.InED = 0;
                        emp1.InCircle = 0;
                        emp1.Corner = k;
                        emp1.InREC = RecBarsNumber;
                        Bar[BarsNumber] = emp1;
                    }
                    #endregion
                }
                if (thetype == 1)
                {
                    emp.EDDistance[1] = 1000000;
                    emp.EDDistance[2] = 1000000;
                    emp.EDDistance[3] = 1000000;
                    emp.EDDistance[4] = 1000000;
                }
                RecLineBar[RecBarsNumber] = emp;
                if (SelectedRecShape != 0)
                {
                    emp0.EDCoverR[1] = emp0.EDY1R[1] - emp.EDY1R[1];
                    emp0.EDCoverR[2] = emp0.EDX1R[2] - emp.EDX1R[2];
                    emp0.EDCoverR[3] = emp.EDY1R[3] - emp0.EDY1R[3];
                    emp0.EDCoverR[4] = emp.EDX1R[4] - emp0.EDX1R[4];
                    emp0.ApplyedToRecBars = SelectedRecBar;
                    RecShape[SelectedRecShape] = emp0;
                }
            }
            #endregion
            #region// ينتمي إلى جدار قص
            if (hala != 0)
            {
                double ED1CoverR = 0;
                double ED2CoverR = 0;
                double ED3CoverR = 0;
                double ED4CoverR = 0;
                FlangedWalls emp0 = new FlangedWalls();
                emp0 = FlangedWallShape[SelectedFlangedWallShape];
                RecLineBars emp0R = new RecLineBars();
                emp0R = RecLineBar[SelectedRecBar];
                RecLineBars emp0R1 = new RecLineBars();
                emp0R1 = RecLineBar[SelectedRecBar1];
                RecLineBars emp0R2 = new RecLineBars();
                emp0R2 = RecLineBar[SelectedRecBar2];
                RecLineBars emp0R3 = new RecLineBars();
                emp0R3 = RecLineBar[SelectedRecBar3];
                #region//تحميل مستطيلات التسليح
                #region//حذف و ترتيب
            startloop: { }
                for (int k = 1; k < BarsNumber + 1; k++)
                {
                    if (Bar[k].InREC == SelectedRecBar1)
                    {
                        BarsNumber = BarsNumber - 1;
                        for (int l = k; l < BarsNumber + 1; l++)
                        {
                            Bar[l] = Bar[l + 1];
                        }
                        if (k <= BarsNumber) goto startloop;
                    }
                }
                for (int k = 1; k < BarsNumber + 1; k++)
                {
                    if (Bar[k].InREC > SelectedRecBar1)
                    {
                        Bar[k].InREC = Bar[k].InREC - 1;
                    }
                }
                for (int k = SelectedRecBar1; k < RecBarsNumber; k++)
                {
                    RecLineBar[k] = RecLineBar[k + 1];
                }
                for (int k = SelectedFlangedWallShape; k < FlangedWallShapeNumber; k++)//مضاف
                {
                    FlangedWallShape[k] = FlangedWallShape[k + 1];
                }
                for (int k = 1; k < RecBarsNumber; k++)//مضاف
                {
                    if (RecLineBar[k].InFlangedWallShape > SelectedFlangedWallShape)
                    {
                        RecLineBar[k].InFlangedWallShape = RecLineBar[k].InFlangedWallShape - 1;
                    }
                }
                for (int k = 1; k < RecShapeNumber + 1; k++)//مضاف///////////////////////
                {
                    if (RecShape[k].ApplyedToRecBars > SelectedRecBar1)
                    {
                        RecShape[k].ApplyedToRecBars = RecShape[k].ApplyedToRecBars - 1;
                    }
                }
                for (int k = 1; k < FlangedWallShapeNumber; k++)//مضاف
                {
                    if (FlangedWallShape[k].ApplyedToRecBars[1] > SelectedRecBar1)
                    {
                        FlangedWallShape[k].ApplyedToRecBars[1] = FlangedWallShape[k].ApplyedToRecBars[1] - 1;
                        FlangedWallShape[k].ApplyedToRecBars[2] = FlangedWallShape[k].ApplyedToRecBars[2] - 1;
                        FlangedWallShape[k].ApplyedToRecBars[3] = FlangedWallShape[k].ApplyedToRecBars[3] - 1;
                    }
                }
                #endregion
                if (SelectedRecBar2 > SelectedRecBar1) SelectedRecBar2 = SelectedRecBar2 - 1;
                if (SelectedRecBar3 > SelectedRecBar1) SelectedRecBar3 = SelectedRecBar3 - 1;
                SelectedFlangedWallShape = FlangedWallShapeNumber;
                SelectedRecBar1 = RecBarsNumber;
                RecLineBars emp = new RecLineBars();
                emp = emp0R1;
                if (hala == 1)
                {
                    #region//أبعاد ومواصفات أسوارة التسليح
                    emp.Width = Math.Abs(Tx[2] - Tx[1]);
                    emp.Height = Math.Abs(Ty[2] - Ty[3]);
                    emp.CenterX = (Tx[2] + Tx[1]) / 2;
                    emp.CenterY = (Ty[2] + Ty[3]) / 2;
                    emp.EDX1R[1] = emp.CenterX - emp.Width / 2;
                    emp.EDY1R[1] = emp.CenterY + emp.Height / 2;
                    emp.EDX2R[1] = emp.CenterX + emp.Width / 2;
                    emp.EDY2R[1] = emp.CenterY + emp.Height / 2;
                    emp.EDX1R[2] = emp.CenterX + emp.Width / 2;
                    emp.EDY1R[2] = emp.CenterY + emp.Height / 2;
                    emp.EDX2R[2] = emp.CenterX + emp.Width / 2;
                    emp.EDY2R[2] = emp.CenterY - emp.Height / 2;
                    emp.EDX1R[3] = emp.CenterX + emp.Width / 2;
                    emp.EDY1R[3] = emp.CenterY - emp.Height / 2;
                    emp.EDX2R[3] = emp.CenterX - emp.Width / 2;
                    emp.EDY2R[3] = emp.CenterY - emp.Height / 2;
                    emp.EDX1R[4] = emp.CenterX - emp.Width / 2;
                    emp.EDY1R[4] = emp.CenterY - emp.Height / 2;
                    emp.EDX2R[4] = emp.CenterX - emp.Width / 2;
                    emp.EDY2R[4] = emp.CenterY + emp.Height / 2;
                    diameter1 = emp.TIEDR;
                    length = emp.Width;
                    TheDistance = emp.EDDistance[1];
                    TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                    emp.EDBarsNumbers[1] = TheBars;
                    emp.EDX1R[1] = emp.CenterX - emp.Width / 2;
                    emp.EDY1R[1] = emp.CenterY + emp.Height / 2;
                    emp.EDX2R[1] = emp.CenterX + emp.Width / 2;
                    emp.EDY2R[1] = emp.CenterY + emp.Height / 2;
                    length = emp.Height;
                    TheDistance = emp.EDDistance[2];
                    TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                    emp.EDBarsNumbers[2] = TheBars;
                    emp.EDX1R[2] = emp.CenterX + emp.Width / 2;
                    emp.EDY1R[2] = emp.CenterY + emp.Height / 2;
                    emp.EDX2R[2] = emp.CenterX + emp.Width / 2;
                    emp.EDY2R[2] = emp.CenterY - emp.Height / 2;
                    length = emp.Width;
                    TheDistance = emp.EDDistance[3];
                    TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                    emp.EDBarsNumbers[3] = TheBars;
                    emp.EDX1R[3] = emp.CenterX + emp.Width / 2;
                    emp.EDY1R[3] = emp.CenterY - emp.Height / 2;
                    emp.EDX2R[3] = emp.CenterX - emp.Width / 2;
                    emp.EDY2R[3] = emp.CenterY - emp.Height / 2;
                    length = emp.Height;
                    TheDistance = emp.EDDistance[4];
                    TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                    emp.EDBarsNumbers[4] = TheBars;
                    emp.EDX1R[4] = emp.CenterX - emp.Width / 2;
                    emp.EDY1R[4] = emp.CenterY - emp.Height / 2;
                    emp.EDX2R[4] = emp.CenterX - emp.Width / 2;
                    emp.EDY2R[4] = emp.CenterY + emp.Height / 2;

                    ED1CoverR = emp0.PointYReal[3] - emp.EDY1R[1];
                    ED2CoverR = emp0.PointXReal[6] - emp.EDX1R[2];
                    ED3CoverR = emp.EDY1R[3] - emp0.PointYReal[9];
                    ED4CoverR = emp.EDX1R[1] - emp0.PointXReal[1];
                    #endregion
                }
                diameter1 = emp.TIEDR;
                #region//القضبان الداخلية
                for (int s = 1; s < 5; s++)
                {
                    int kl1 = 0;
                    int kl2 = 0;
                    if (s == 1 || s == 3)
                    {
                        length = emp.Width;
                        kl1 = 1;
                        kl2 = 0;
                    }
                    if (s == 2 || s == 4)
                    {
                        length = emp.Height;
                        kl1 = 0;
                        kl2 = 1;
                    }
                    NBAR = emp.EDBarsNumbers[s];
                    diameter = emp.EDDR[s];
                    DIS = Math.Round((length - 2 * diameter1) / (NBAR + 1), 3);
                    X1 = emp.EDX1R[s];
                    Y1 = emp.EDY1R[s];
                    for (int k = 1; k < NBAR + 1; k++)
                    {
                        BarsNumber = BarsNumber + 1;
                        Bars emp1 = new Bars();
                        emp1.XR = Math.Round(X1 + kl1 * kx[s] * (k * DIS) + kx[s] * diameter1 + kl2 * ky[s] * (diameter / 2), 3);
                        emp1.YR = Math.Round(Y1 + kl2 * ky[s] * (k * DIS) + ky[s] * diameter1 + kl1 * ky[s] * (diameter / 2), 3);
                        emp1.DR = diameter;
                        emp1.Selected = 0;
                        emp1.InLine = 0;
                        emp1.Type = 3;
                        emp1.InED = s;
                        emp1.InREC = RecBarsNumber;
                        emp1.Corner = 0;
                        emp1.InCircle = 0;
                        Bar[BarsNumber] = emp1;
                    }
                }
                #endregion
                #region//قضبان الزاوية
                for (int k = 1; k < 5; k++)
                {
                    BarsNumber = BarsNumber + 1;
                    Bars emp1 = new Bars();
                    diameter = emp.CORDR[k];
                    X1 = emp.EDX1R[k];
                    Y1 = emp.EDY1R[k];
                    if (k == 1)
                    {
                        emp1.XR = Math.Round(X1 + kx[k] * (diameter / 2) + kx[k] * diameter1, 3);
                        emp1.YR = Math.Round(Y1 + ky[k] * (diameter / 2) + ky[k] * diameter1, 3);
                    }
                    if (k == 2)
                    {
                        emp1.XR = Math.Round(X1 + kx[k] * (diameter / 2) + kx[k] * diameter1, 3);
                        emp1.YR = Math.Round(Y1 + ky[k] * (diameter / 2) + ky[k] * diameter1, 3);
                    }
                    if (k == 3)
                    {
                        emp1.XR = Math.Round(X1 + kx[k] * (diameter / 2) + kx[k] * diameter1, 3);
                        emp1.YR = Math.Round(Y1 + ky[k] * (diameter / 2) + ky[k] * diameter1, 3);
                    }
                    if (k == 4)
                    {
                        emp1.XR = Math.Round(X1 + kx[k] * (diameter / 2) + kx[k] * diameter1, 3);
                        emp1.YR = Math.Round(Y1 + ky[k] * (diameter / 2) + ky[k] * diameter1, 3);
                    }
                    emp1.DR = diameter;
                    emp1.Selected = 0;
                    emp1.InLine = 0;
                    emp1.Type = 4;
                    emp1.InED = 0;
                    emp1.InCircle = 0;
                    emp1.Corner = k;
                    emp1.InREC = RecBarsNumber;
                    Bar[BarsNumber] = emp1;
                }
                #endregion
                emp.InFlangedWallShape = SelectedFlangedWallShape;
                emp.InRecShape = 0;
                RecLineBar[RecBarsNumber] = emp;
                #endregion
                #region//تحميل مستطيلات التسليح
                #region//حذف و ترتيب
            startloopL: { }
                for (int k = 1; k < BarsNumber + 1; k++)
                {
                    if (Bar[k].InREC == SelectedRecBar2)
                    {
                        BarsNumber = BarsNumber - 1;
                        for (int l = k; l < BarsNumber + 1; l++)
                        {
                            Bar[l] = Bar[l + 1];
                        }
                        if (k <= BarsNumber) goto startloopL;
                    }
                }
                for (int k = 1; k < BarsNumber + 1; k++)
                {
                    if (Bar[k].InREC > SelectedRecBar2)
                    {
                        Bar[k].InREC = Bar[k].InREC - 1;
                    }
                }
                for (int k = SelectedRecBar2; k < RecBarsNumber; k++)
                {
                    RecLineBar[k] = RecLineBar[k + 1];
                }
                for (int k = SelectedFlangedWallShape; k < FlangedWallShapeNumber; k++)//مضاف
                {
                    FlangedWallShape[k] = FlangedWallShape[k + 1];
                }
                for (int k = 1; k < RecBarsNumber; k++)//مضاف
                {
                    if (RecLineBar[k].InFlangedWallShape > SelectedFlangedWallShape)
                    {
                        RecLineBar[k].InFlangedWallShape = RecLineBar[k].InFlangedWallShape - 1;
                    }
                }
                for (int k = 1; k < RecShapeNumber + 1; k++)//مضاف///////////////////////
                {
                    if (RecShape[k].ApplyedToRecBars > SelectedRecBar2)
                    {
                        RecShape[k].ApplyedToRecBars = RecShape[k].ApplyedToRecBars - 1;
                    }
                }
                for (int k = 1; k < FlangedWallShapeNumber; k++)//مضاف
                {
                    if (FlangedWallShape[k].ApplyedToRecBars[2] > SelectedRecBar2)
                    {
                        FlangedWallShape[k].ApplyedToRecBars[2] = FlangedWallShape[k].ApplyedToRecBars[2] - 1;
                        FlangedWallShape[k].ApplyedToRecBars[1] = FlangedWallShape[k].ApplyedToRecBars[1] - 1;
                        FlangedWallShape[k].ApplyedToRecBars[3] = FlangedWallShape[k].ApplyedToRecBars[3] - 1;
                    }
                }
                #endregion
                if (SelectedRecBar3 > SelectedRecBar2) SelectedRecBar3 = SelectedRecBar3 - 1;
                SelectedFlangedWallShape = FlangedWallShapeNumber;
                SelectedRecBar2 = RecBarsNumber;
                emp = new RecLineBars();
                emp = emp0R2;
                if (hala == 2)
                {
                    #region//أبعاد ومواصفات أسوارة التسليح
                    emp.Width = Math.Abs(Tx[2] - Tx[1]);
                    emp.Height = Math.Abs(Ty[2] - Ty[3]);
                    emp.CenterX = (Tx[2] + Tx[1]) / 2;
                    emp.CenterY = (Ty[2] + Ty[3]) / 2;
                    emp.EDX1R[1] = emp.CenterX - emp.Width / 2;
                    emp.EDY1R[1] = emp.CenterY + emp.Height / 2;
                    emp.EDX2R[1] = emp.CenterX + emp.Width / 2;
                    emp.EDY2R[1] = emp.CenterY + emp.Height / 2;
                    emp.EDX1R[2] = emp.CenterX + emp.Width / 2;
                    emp.EDY1R[2] = emp.CenterY + emp.Height / 2;
                    emp.EDX2R[2] = emp.CenterX + emp.Width / 2;
                    emp.EDY2R[2] = emp.CenterY - emp.Height / 2;
                    emp.EDX1R[3] = emp.CenterX + emp.Width / 2;
                    emp.EDY1R[3] = emp.CenterY - emp.Height / 2;
                    emp.EDX2R[3] = emp.CenterX - emp.Width / 2;
                    emp.EDY2R[3] = emp.CenterY - emp.Height / 2;
                    emp.EDX1R[4] = emp.CenterX - emp.Width / 2;
                    emp.EDY1R[4] = emp.CenterY - emp.Height / 2;
                    emp.EDX2R[4] = emp.CenterX - emp.Width / 2;
                    emp.EDY2R[4] = emp.CenterY + emp.Height / 2;
                    diameter1 = emp.TIEDR;
                    length = emp.Width;
                    TheDistance = emp.EDDistance[1];
                    TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                    emp.EDBarsNumbers[1] = TheBars;
                    emp.EDX1R[1] = emp.CenterX - emp.Width / 2;
                    emp.EDY1R[1] = emp.CenterY + emp.Height / 2;
                    emp.EDX2R[1] = emp.CenterX + emp.Width / 2;
                    emp.EDY2R[1] = emp.CenterY + emp.Height / 2;
                    length = emp.Height;
                    TheDistance = emp.EDDistance[2];
                    TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                    emp.EDBarsNumbers[2] = TheBars;
                    emp.EDX1R[2] = emp.CenterX + emp.Width / 2;
                    emp.EDY1R[2] = emp.CenterY + emp.Height / 2;
                    emp.EDX2R[2] = emp.CenterX + emp.Width / 2;
                    emp.EDY2R[2] = emp.CenterY - emp.Height / 2;
                    length = emp.Width;
                    TheDistance = emp.EDDistance[3];
                    TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                    emp.EDBarsNumbers[3] = TheBars;
                    emp.EDX1R[3] = emp.CenterX + emp.Width / 2;
                    emp.EDY1R[3] = emp.CenterY - emp.Height / 2;
                    emp.EDX2R[3] = emp.CenterX - emp.Width / 2;
                    emp.EDY2R[3] = emp.CenterY - emp.Height / 2;
                    length = emp.Height;
                    TheDistance = emp.EDDistance[4];
                    TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                    emp.EDBarsNumbers[4] = TheBars;
                    emp.EDX1R[4] = emp.CenterX - emp.Width / 2;
                    emp.EDY1R[4] = emp.CenterY - emp.Height / 2;
                    emp.EDX2R[4] = emp.CenterX - emp.Width / 2;
                    emp.EDY2R[4] = emp.CenterY + emp.Height / 2;

                    ED1CoverR = emp0.PointYReal[1] - emp.EDY1R[1];
                    ED2CoverR = emp0.PointXReal[2] - emp.EDX1R[2];
                    ED3CoverR = emp.EDY1R[3] - emp0.PointYReal[11];
                    ED4CoverR = emp.EDX1R[4] - emp0.PointXReal[12];
                    #endregion
                }
                diameter1 = emp.TIEDR;
                #region//القضبان الداخلية
                for (int s = 1; s < 5; s++)
                {
                    int kl1 = 0;
                    int kl2 = 0;
                    if (s == 1 || s == 3)
                    {
                        length = emp.Width;
                        kl1 = 1;
                        kl2 = 0;
                    }
                    if (s == 2 || s == 4)
                    {
                        length = emp.Height;
                        kl1 = 0;
                        kl2 = 1;
                    }
                    NBAR = emp.EDBarsNumbers[s];
                    diameter = emp.EDDR[s];
                    DIS = Math.Round((length - 2 * diameter1) / (NBAR + 1), 3);
                    X1 = emp.EDX1R[s];
                    Y1 = emp.EDY1R[s];
                    for (int k = 1; k < NBAR + 1; k++)
                    {
                        BarsNumber = BarsNumber + 1;
                        Bars emp1 = new Bars();
                        emp1.XR = Math.Round(X1 + kl1 * kx[s] * (k * DIS) + kx[s] * diameter1 + kl2 * ky[s] * (diameter / 2), 3);
                        emp1.YR = Math.Round(Y1 + kl2 * ky[s] * (k * DIS) + ky[s] * diameter1 + kl1 * ky[s] * (diameter / 2), 3);
                        emp1.DR = diameter;
                        emp1.Selected = 0;
                        emp1.InLine = 0;
                        emp1.Type = 3;
                        emp1.InED = s;
                        emp1.InREC = RecBarsNumber;
                        emp1.Corner = 0;
                        emp1.InCircle = 0;
                        Bar[BarsNumber] = emp1;
                    }
                }
                #endregion
                #region//قضبان الزاوية
                for (int k = 1; k < 5; k++)
                {
                    BarsNumber = BarsNumber + 1;
                    Bars emp1 = new Bars();
                    diameter = emp.CORDR[k];
                    X1 = emp.EDX1R[k];
                    Y1 = emp.EDY1R[k];
                    if (k == 1)
                    {
                        emp1.XR = Math.Round(X1 + kx[k] * (diameter / 2) + kx[k] * diameter1, 3);
                        emp1.YR = Math.Round(Y1 + ky[k] * (diameter / 2) + ky[k] * diameter1, 3);
                    }
                    if (k == 2)
                    {
                        emp1.XR = Math.Round(X1 + kx[k] * (diameter / 2) + kx[k] * diameter1, 3);
                        emp1.YR = Math.Round(Y1 + ky[k] * (diameter / 2) + ky[k] * diameter1, 3);
                    }
                    if (k == 3)
                    {
                        emp1.XR = Math.Round(X1 + kx[k] * (diameter / 2) + kx[k] * diameter1, 3);
                        emp1.YR = Math.Round(Y1 + ky[k] * (diameter / 2) + ky[k] * diameter1, 3);
                    }
                    if (k == 4)
                    {
                        emp1.XR = Math.Round(X1 + kx[k] * (diameter / 2) + kx[k] * diameter1, 3);
                        emp1.YR = Math.Round(Y1 + ky[k] * (diameter / 2) + ky[k] * diameter1, 3);
                    }
                    emp1.DR = diameter;
                    emp1.Selected = 0;
                    emp1.InLine = 0;
                    emp1.Type = 4;
                    emp1.InED = 0;
                    emp1.InCircle = 0;
                    emp1.Corner = k;
                    emp1.InREC = RecBarsNumber;
                    Bar[BarsNumber] = emp1;
                }
                #endregion
                emp.InFlangedWallShape = SelectedFlangedWallShape;
                emp.InRecShape = 0;
                RecLineBar[RecBarsNumber] = emp;
                #endregion
                #region//تحميل مستطيلات التسليح
                #region//حذف و ترتيب
            startloopR: { }
                for (int k = 1; k < BarsNumber + 1; k++)
                {
                    if (Bar[k].InREC == SelectedRecBar3)
                    {
                        BarsNumber = BarsNumber - 1;
                        for (int l = k; l < BarsNumber + 1; l++)
                        {
                            Bar[l] = Bar[l + 1];
                        }
                        if (k <= BarsNumber) goto startloopR;
                    }
                }
                for (int k = 1; k < BarsNumber + 1; k++)
                {
                    if (Bar[k].InREC > SelectedRecBar3)
                    {
                        Bar[k].InREC = Bar[k].InREC - 1;
                    }
                }
                for (int k = SelectedRecBar3; k < RecBarsNumber; k++)
                {
                    RecLineBar[k] = RecLineBar[k + 1];
                }
                for (int k = SelectedFlangedWallShape; k < FlangedWallShapeNumber; k++)//مضاف
                {
                    FlangedWallShape[k] = FlangedWallShape[k + 1];
                }
                for (int k = 1; k < RecBarsNumber; k++)//مضاف
                {
                    if (RecLineBar[k].InFlangedWallShape > SelectedFlangedWallShape)
                    {
                        RecLineBar[k].InFlangedWallShape = RecLineBar[k].InFlangedWallShape - 1;
                    }
                }
                for (int k = 1; k < RecShapeNumber + 1; k++)//مضاف///////////////////////
                {
                    if (RecShape[k].ApplyedToRecBars > SelectedRecBar3)
                    {
                        RecShape[k].ApplyedToRecBars = RecShape[k].ApplyedToRecBars - 1;
                    }
                }
                for (int k = 1; k < FlangedWallShapeNumber; k++)//مضاف
                {
                    if (FlangedWallShape[k].ApplyedToRecBars[3] > SelectedRecBar3)
                    {
                        FlangedWallShape[k].ApplyedToRecBars[2] = FlangedWallShape[k].ApplyedToRecBars[2] - 1;
                        FlangedWallShape[k].ApplyedToRecBars[1] = FlangedWallShape[k].ApplyedToRecBars[1] - 1;
                        FlangedWallShape[k].ApplyedToRecBars[3] = FlangedWallShape[k].ApplyedToRecBars[3] - 1;
                    }
                }
                #endregion
                SelectedFlangedWallShape = FlangedWallShapeNumber;
                SelectedRecBar3 = RecBarsNumber;
                emp = new RecLineBars();
                emp = emp0R3;
                if (hala == 3)
                {
                    #region//أبعاد ومواصفات أسوارة التسليح
                    emp.Width = Math.Abs(Tx[2] - Tx[1]);
                    emp.Height = Math.Abs(Ty[2] - Ty[3]);
                    emp.CenterX = (Tx[2] + Tx[1]) / 2;
                    emp.CenterY = (Ty[2] + Ty[3]) / 2;
                    emp.EDX1R[1] = emp.CenterX - emp.Width / 2;
                    emp.EDY1R[1] = emp.CenterY + emp.Height / 2;
                    emp.EDX2R[1] = emp.CenterX + emp.Width / 2;
                    emp.EDY2R[1] = emp.CenterY + emp.Height / 2;
                    emp.EDX1R[2] = emp.CenterX + emp.Width / 2;
                    emp.EDY1R[2] = emp.CenterY + emp.Height / 2;
                    emp.EDX2R[2] = emp.CenterX + emp.Width / 2;
                    emp.EDY2R[2] = emp.CenterY - emp.Height / 2;
                    emp.EDX1R[3] = emp.CenterX + emp.Width / 2;
                    emp.EDY1R[3] = emp.CenterY - emp.Height / 2;
                    emp.EDX2R[3] = emp.CenterX - emp.Width / 2;
                    emp.EDY2R[3] = emp.CenterY - emp.Height / 2;
                    emp.EDX1R[4] = emp.CenterX - emp.Width / 2;
                    emp.EDY1R[4] = emp.CenterY - emp.Height / 2;
                    emp.EDX2R[4] = emp.CenterX - emp.Width / 2;
                    emp.EDY2R[4] = emp.CenterY + emp.Height / 2;
                    diameter1 = emp.TIEDR;
                    length = emp.Width;
                    TheDistance = emp.EDDistance[1];
                    TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                    emp.EDBarsNumbers[1] = TheBars;
                    emp.EDX1R[1] = emp.CenterX - emp.Width / 2;
                    emp.EDY1R[1] = emp.CenterY + emp.Height / 2;
                    emp.EDX2R[1] = emp.CenterX + emp.Width / 2;
                    emp.EDY2R[1] = emp.CenterY + emp.Height / 2;
                    length = emp.Height;
                    TheDistance = emp.EDDistance[2];
                    TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                    emp.EDBarsNumbers[2] = TheBars;
                    emp.EDX1R[2] = emp.CenterX + emp.Width / 2;
                    emp.EDY1R[2] = emp.CenterY + emp.Height / 2;
                    emp.EDX2R[2] = emp.CenterX + emp.Width / 2;
                    emp.EDY2R[2] = emp.CenterY - emp.Height / 2;
                    length = emp.Width;
                    TheDistance = emp.EDDistance[3];
                    TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                    emp.EDBarsNumbers[3] = TheBars;
                    emp.EDX1R[3] = emp.CenterX + emp.Width / 2;
                    emp.EDY1R[3] = emp.CenterY - emp.Height / 2;
                    emp.EDX2R[3] = emp.CenterX - emp.Width / 2;
                    emp.EDY2R[3] = emp.CenterY - emp.Height / 2;
                    length = emp.Height;
                    TheDistance = emp.EDDistance[4];
                    TheBars = Convert.ToInt32(Math.Ceiling((length - 2 * diameter1) / TheDistance)) - 1;
                    emp.EDBarsNumbers[4] = TheBars;
                    emp.EDX1R[4] = emp.CenterX - emp.Width / 2;
                    emp.EDY1R[4] = emp.CenterY - emp.Height / 2;
                    emp.EDX2R[4] = emp.CenterX - emp.Width / 2;
                    emp.EDY2R[4] = emp.CenterY + emp.Height / 2;

                    ED1CoverR = emp0.PointYReal[5] - emp.EDY1R[1];
                    ED2CoverR = emp0.PointXReal[6] - emp.EDX1R[2];
                    ED3CoverR = emp.EDY1R[3] - emp0.PointYReal[7];
                    ED4CoverR = emp.EDX1R[4] - emp0.PointXReal[8];
                    #endregion
                }
                diameter1 = emp.TIEDR;
                #region//القضبان الداخلية
                for (int s = 1; s < 5; s++)
                {
                    int kl1 = 0;
                    int kl2 = 0;
                    if (s == 1 || s == 3)
                    {
                        length = emp.Width;
                        kl1 = 1;
                        kl2 = 0;
                    }
                    if (s == 2 || s == 4)
                    {
                        length = emp.Height;
                        kl1 = 0;
                        kl2 = 1;
                    }
                    NBAR = emp.EDBarsNumbers[s];
                    diameter = emp.EDDR[s];
                    DIS = Math.Round((length - 2 * diameter1) / (NBAR + 1), 3);
                    X1 = emp.EDX1R[s];
                    Y1 = emp.EDY1R[s];
                    for (int k = 1; k < NBAR + 1; k++)
                    {
                        BarsNumber = BarsNumber + 1;
                        Bars emp1 = new Bars();
                        emp1.XR = Math.Round(X1 + kl1 * kx[s] * (k * DIS) + kx[s] * diameter1 + kl2 * ky[s] * (diameter / 2), 3);
                        emp1.YR = Math.Round(Y1 + kl2 * ky[s] * (k * DIS) + ky[s] * diameter1 + kl1 * ky[s] * (diameter / 2), 3);
                        emp1.DR = diameter;
                        emp1.Selected = 0;
                        emp1.InLine = 0;
                        emp1.Type = 3;
                        emp1.InED = s;
                        emp1.InREC = RecBarsNumber;
                        emp1.Corner = 0;
                        emp1.InCircle = 0;
                        Bar[BarsNumber] = emp1;
                    }
                }
                #endregion
                #region//قضبان الزاوية
                for (int k = 1; k < 5; k++)
                {
                    BarsNumber = BarsNumber + 1;
                    Bars emp1 = new Bars();
                    diameter = emp.CORDR[k];
                    X1 = emp.EDX1R[k];
                    Y1 = emp.EDY1R[k];
                    if (k == 1)
                    {
                        emp1.XR = Math.Round(X1 + kx[k] * (diameter / 2) + kx[k] * diameter1, 3);
                        emp1.YR = Math.Round(Y1 + ky[k] * (diameter / 2) + ky[k] * diameter1, 3);
                    }
                    if (k == 2)
                    {
                        emp1.XR = Math.Round(X1 + kx[k] * (diameter / 2) + kx[k] * diameter1, 3);
                        emp1.YR = Math.Round(Y1 + ky[k] * (diameter / 2) + ky[k] * diameter1, 3);
                    }
                    if (k == 3)
                    {
                        emp1.XR = Math.Round(X1 + kx[k] * (diameter / 2) + kx[k] * diameter1, 3);
                        emp1.YR = Math.Round(Y1 + ky[k] * (diameter / 2) + ky[k] * diameter1, 3);
                    }
                    if (k == 4)
                    {
                        emp1.XR = Math.Round(X1 + kx[k] * (diameter / 2) + kx[k] * diameter1, 3);
                        emp1.YR = Math.Round(Y1 + ky[k] * (diameter / 2) + ky[k] * diameter1, 3);
                    }
                    emp1.DR = diameter;
                    emp1.Selected = 0;
                    emp1.InLine = 0;
                    emp1.Type = 4;
                    emp1.InED = 0;
                    emp1.InCircle = 0;
                    emp1.Corner = k;
                    emp1.InREC = RecBarsNumber;
                    Bar[BarsNumber] = emp1;
                }
                #endregion
                emp.InFlangedWallShape = SelectedFlangedWallShape;
                emp.InRecShape = 0;
                RecLineBar[RecBarsNumber] = emp;
                #endregion
                #region//تعبئة الجدار
                emp0.ApplyedToRecBars[1] = RecBarsNumber - 2;
                emp0.ApplyedToRecBars[2] = RecBarsNumber - 1;
                emp0.ApplyedToRecBars[3] = RecBarsNumber;
                emp0.HasReinforcment = 1;
                if (hala == 1)
                {
                    emp0.ED1CoverR = ED1CoverR;
                    emp0.ED2CoverR = ED2CoverR;
                    emp0.ED3CoverR = ED3CoverR;
                    emp0.ED4CoverR = ED4CoverR;
                }
                if (hala == 2)
                {
                    emp0.ED1LCoverR = ED1CoverR;
                    emp0.ED2LCoverR = ED2CoverR;
                    emp0.ED3LCoverR = ED3CoverR;
                    emp0.ED4LCoverR = ED4CoverR;
                }
                if (hala == 3)
                {
                    emp0.ED1RCoverR = ED1CoverR;
                    emp0.ED2RCoverR = ED2CoverR;
                    emp0.ED3RCoverR = ED3CoverR;
                    emp0.ED4RCoverR = ED4CoverR;
                }
                FlangedWallShape[SelectedFlangedWallShape] = emp0;
                #endregion
            }
            #endregion
            RecEdgeToEdit = 0;
            ClickToEdit = 0;
            MakeTempFiles();
            Render2d();
        }
        #endregion
        #region//اجرائيات الريندر
        public void Render2d()
        {
            PlaneAria2d();
            CalculateLineJoint2d();
            CalculateRECLineJoint2d();
            CalculateRECShapeJoint2d();
            CalculateCircleLineJoint2d();
            CalculateCIRCLEShapeJoint2d();
            CalculateClipJoint2d();
            CalculateJoint2d();
            FlangedWallsJoint2d();
            CalculateTeeShapeJoint2d();
            CalculateIntersectionPoint();
            CalculateJointFiber2d();
            CalculateGridFiberLines2d();
            DrowBars();
        }
        private void PlaneAria2d()
        {
            Bitmap finalBmp = new Bitmap(BitampWidth2d, BitampHight2d);
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
            pictureBox1.Image = finalBmp;
        }
        private void CalculateJoint2d()
        {
            for (int i = 1; i < BarsNumber + 1; i++)
            {
                Bar[i].X2D = startX2d + Convert.ToInt32((Bar[i].XR) * Zoom2d);
                Bar[i].Y2D = startY2d - Convert.ToInt32((Bar[i].YR) * Zoom2d);
                Bar[i].D2D = Convert.ToInt32((Bar[i].DR) * Zoom2d);
            }
        }
        private void CalculateLineJoint2d()
        {
            for (int i = 1; i < LineBarsNumber + 1; i++)
            {
                LineBar[i].X12D = startX2d + Convert.ToInt32((LineBar[i].X1R) * Zoom2d);
                LineBar[i].Y12D = startY2d - Convert.ToInt32((LineBar[i].Y1R) * Zoom2d);
                LineBar[i].X22D = startX2d + Convert.ToInt32((LineBar[i].X2R) * Zoom2d);
                LineBar[i].Y22D = startY2d - Convert.ToInt32((LineBar[i].Y2R) * Zoom2d);
            }
        }
        private void CalculateClipJoint2d()
        {
            for (int i = 1; i < ClipsNumber + 1; i++)
            {
                Clip[i].X12D = startX2d + Convert.ToInt32((Clip[i].X1R) * Zoom2d);
                Clip[i].Y12D = startY2d - Convert.ToInt32((Clip[i].Y1R) * Zoom2d);
                Clip[i].X22D = startX2d + Convert.ToInt32((Clip[i].X2R) * Zoom2d);
                Clip[i].Y22D = startY2d - Convert.ToInt32((Clip[i].Y2R) * Zoom2d);
            }
        }
        private void CalculateRECLineJoint2d()
        {
            for (int i = 1; i < RecBarsNumber + 1; i++)
            {
                RecLineBar[i].CenterX2D = startX2d + Convert.ToInt32((RecLineBar[i].CenterX) * Zoom2d);
                RecLineBar[i].CenterY2D = startY2d - Convert.ToInt32((RecLineBar[i].CenterY) * Zoom2d);
                for (int j = 1; j < 5; j++)
                {
                    RecLineBar[i].EDX12D[j] = startX2d + Convert.ToInt32((RecLineBar[i].EDX1R[j]) * Zoom2d);
                    RecLineBar[i].EDY12D[j] = startY2d - Convert.ToInt32((RecLineBar[i].EDY1R[j]) * Zoom2d);
                    RecLineBar[i].EDX22D[j] = startX2d + Convert.ToInt32((RecLineBar[i].EDX2R[j]) * Zoom2d);
                    RecLineBar[i].EDY22D[j] = startY2d - Convert.ToInt32((RecLineBar[i].EDY2R[j]) * Zoom2d);
                }
            }
        }
        private void CalculateCircleLineJoint2d()
        {
            for (int i = 1; i < CircleBarsNumber + 1; i++)
            {
                CircleLineBar[i].CenterX2D = startX2d + Convert.ToInt32((CircleLineBar[i].CenterX) * Zoom2d);
                CircleLineBar[i].CenterY2D = startY2d - Convert.ToInt32((CircleLineBar[i].CenterY) * Zoom2d);
                for (int j = 1; j < 32 + 1; j++)
                {
                    CircleLineBar[i].PointX2D[j] = startX2d + Convert.ToInt32((CircleLineBar[i].PointXR[j]) * Zoom2d);
                    CircleLineBar[i].PointY2D[j] = startY2d - Convert.ToInt32((CircleLineBar[i].PointYR[j]) * Zoom2d);
                }
            }
        }
        public void CalculateIntersectionPoint()
        {
            #region
            int n = GridNumberSX;
            int n1 = GridNumberSY;
            GridPointNumber = 0;
            for (int i = 1; i < n + 1; i++)
            {
                double X1 = TX1Rx[i];
                double Y1 = TY1Rx[i];
                double X2 = TX2Rx[i];
                double Y2 = TY2Rx[i];
                for (int j = 1; j < n1 + 1; j++)
                {
                    double X3 = TX1Ry[j];
                    double Y3 = TY1Ry[j];
                    double X4 = TX2Ry[j];
                    double Y4 = TY2Ry[j];
                    checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                    if (INTERSECTION == 0) goto Nextj;
                    GridPointNumber = GridPointNumber + 1;
                    GridXR[GridPointNumber] = TheX0;
                    GridYR[GridPointNumber] = TheY0;
                    GridX2D[GridPointNumber] = startX2d + Convert.ToInt32(GridXR[GridPointNumber] * Zoom2d);
                    GridY2D[GridPointNumber] = startY2d - Convert.ToInt32(GridYR[GridPointNumber] * Zoom2d);
                Nextj: { }
                }
            }
            #endregion
        }
        private void CalculateRECShapeJoint2d()
        {
            for (int i = 1; i < RecShapeNumber + 1; i++)
            {
                RecShape[i].CenterX2D = startX2d + Convert.ToInt32((RecShape[i].CenterX) * Zoom2d);
                RecShape[i].CenterY2D = startY2d - Convert.ToInt32((RecShape[i].CenterY) * Zoom2d);
                for (int j = 1; j < 5; j++)
                {
                    RecShape[i].EDX12D[j] = startX2d + Convert.ToInt32((RecShape[i].EDX1R[j]) * Zoom2d);
                    RecShape[i].EDY12D[j] = startY2d - Convert.ToInt32((RecShape[i].EDY1R[j]) * Zoom2d);
                    RecShape[i].EDX22D[j] = startX2d + Convert.ToInt32((RecShape[i].EDX2R[j]) * Zoom2d);
                    RecShape[i].EDY22D[j] = startY2d - Convert.ToInt32((RecShape[i].EDY2R[j]) * Zoom2d);
                }
            }
        }
        private void CalculateCIRCLEShapeJoint2d()
        {
            for (int i = 1; i < CircleShapeNumber + 1; i++)
            {
                CircleShape[i].CenterX2D = startX2d + Convert.ToInt32((CircleShape[i].CenterX) * Zoom2d);
                CircleShape[i].CenterY2D = startY2d - Convert.ToInt32((CircleShape[i].CenterY) * Zoom2d);
                for (int j = 1; j < 32 + 1; j++)
                {
                    CircleShape[i].PointX2D[j] = startX2d + Convert.ToInt32((CircleShape[i].PointXR[j]) * Zoom2d);
                    CircleShape[i].PointY2D[j] = startY2d - Convert.ToInt32((CircleShape[i].PointYR[j]) * Zoom2d);
                }
            }
        }
        private void FlangedWallsJoint2d()
        {
            for (int i = 1; i < FlangedWallShapeNumber + 1; i++)
            {
                FlangedWallShape[i].CenterX2D = startX2d + Convert.ToInt32((FlangedWallShape[i].CenterX) * Zoom2d);
                FlangedWallShape[i].CenterY2D = startY2d - Convert.ToInt32((FlangedWallShape[i].CenterY) * Zoom2d);
                for (int j = 1; j < 13; j++)
                {
                    FlangedWallShape[i].PointX2D[j] = startX2d + Convert.ToInt32((FlangedWallShape[i].PointXReal[j]) * Zoom2d);
                    FlangedWallShape[i].PointY2D[j] = startY2d - Convert.ToInt32((FlangedWallShape[i].PointYReal[j]) * Zoom2d);
                }
            }
        }
        private void CalculateTeeShapeJoint2d()
        {
            for (int i = 1; i < TeeShapeNumber + 1; i++)
            {
                TeeShape[i].CenterX2D = startX2d + Convert.ToInt32((TeeShape[i].CenterX) * Zoom2d);
                TeeShape[i].CenterY2D = startY2d - Convert.ToInt32((TeeShape[i].CenterY) * Zoom2d);
                for (int j = 1; j < 9; j++)
                {
                    TeeShape[i].PointX2D[j] = startX2d + Convert.ToInt32((TeeShape[i].PointXReal[j]) * Zoom2d);
                    TeeShape[i].PointY2D[j] = startY2d - Convert.ToInt32((TeeShape[i].PointYReal[j]) * Zoom2d);
                 }
            }
        }

        public int LineNO1 = 5;
        public int LineNO2 = 5;
        public double Angel = 0;
        public void CalculateGridFiberLines()
        {
            CallMesh.AllLine = 0;
            double AngelRad = Angel * Math.PI / 180;
            int m = 0;
            double dis1 = 200;
            double dis2 = 200;
            double startxx = 0;
            double startyy = 0;
            double endxx = 1000;
            double endyy = 1000;
            double[] EDXRnew = new double[33];
            double[] EDYRnew = new double[33];
            double[] LineX1 = new double[10000];
            double[] LineY1 = new double[10000];
            double[] LineX2 = new double[10000];
            double[] LineY2 = new double[10000];
            int[] LineType = new int[10000];
            int[] LineShape = new int[10000];
            int ShapeNumbers = 0;
            #region// خطوط الشبكة
            double MaxX = -1000000;
            double MaxY = -1000000;
            double MinX = 1000000;
            double MINY = 1000000;
            for (int j = 1; j < RecShapeNumber + 1; j++)
            {
                for (int i = 1; i < 5; i++)
                {
                    EDXRnew[i] = RecShape[j].EDX1R[i] * Math.Cos(AngelRad) + RecShape[j].EDY1R[i] * Math.Sin(AngelRad);
                    EDYRnew[i] = -RecShape[j].EDX1R[i] * Math.Sin(AngelRad) + RecShape[j].EDY1R[i] * Math.Cos(AngelRad);
                    if (EDXRnew[i] > MaxX) MaxX = EDXRnew[i];
                    if (EDYRnew[i] > MaxY) MaxY = EDYRnew[i];
                    if (EDXRnew[i] < MinX) MinX = EDXRnew[i];
                    if (EDYRnew[i] < MINY) MINY = EDYRnew[i];
                }
            }
            for (int j = 1; j < CircleShapeNumber + 1; j++)
            {
                for (int i = 1; i < 33; i++)
                {
                    EDXRnew[i] = CircleShape[j].PointXR[i] * Math.Cos(AngelRad) + CircleShape[j].PointYR[i] * Math.Sin(AngelRad);
                    EDYRnew[i] = -CircleShape[j].PointXR[i] * Math.Sin(AngelRad) + CircleShape[j].PointYR[i] * Math.Cos(AngelRad);
                    if (EDXRnew[i] > MaxX) MaxX = EDXRnew[i];
                    if (EDYRnew[i] > MaxY) MaxY = EDYRnew[i];
                    if (EDXRnew[i] < MinX) MinX = EDXRnew[i];
                    if (EDYRnew[i] < MINY) MINY = EDYRnew[i];
                }
            }
            for (int j = 1; j < FlangedWallShapeNumber + 1; j++)
            {
                for (int i = 1; i < 13; i++)
                {
                    EDXRnew[i] = FlangedWallShape[j].PointXReal[i] * Math.Cos(AngelRad) + FlangedWallShape[j].PointYReal[i] * Math.Sin(AngelRad);
                    EDYRnew[i] = -FlangedWallShape[j].PointXReal[i] * Math.Sin(AngelRad) + FlangedWallShape[j].PointYReal[i] * Math.Cos(AngelRad);
                    if (EDXRnew[i] > MaxX) MaxX = EDXRnew[i];
                    if (EDYRnew[i] > MaxY) MaxY = EDYRnew[i];
                    if (EDXRnew[i] < MinX) MinX = EDXRnew[i];
                    if (EDYRnew[i] < MINY) MINY = EDYRnew[i];
                }
            }
            for (int j = 1; j < TeeShapeNumber + 1; j++)
            {
                for (int i = 1; i < 9; i++)
                {
                    EDXRnew[i] = TeeShape[j].PointXReal[i] * Math.Cos(AngelRad) + TeeShape[j].PointYReal[i] * Math.Sin(AngelRad);
                    EDYRnew[i] = -TeeShape[j].PointXReal[i] * Math.Sin(AngelRad) + TeeShape[j].PointYReal[i] * Math.Cos(AngelRad);
                    if (EDXRnew[i] > MaxX) MaxX = EDXRnew[i];
                    if (EDYRnew[i] > MaxY) MaxY = EDYRnew[i];
                    if (EDXRnew[i] < MinX) MinX = EDXRnew[i];
                    if (EDYRnew[i] < MINY) MINY = EDYRnew[i];
                }
            }
            double Width = MaxX - MinX;
            double Height = MaxY - MINY;
            startxx = MinX;
            startyy = MINY;
            endxx = MaxX;
            endyy = MaxY;
            dis1 = Width / LineNO1;
            dis2 = Height / LineNO2;
            for (int i = 1; i < LineNO1 + 2; i++)
            {
                m = m + 1;
                LineX1[m] = startxx + dis1 * (i - 1);
                LineY1[m] = startyy;
                LineX2[m] = startxx + dis1 * (i - 1);
                LineY2[m] = endyy;
            }
            for (int i = 1; i < LineNO2 + 2; i++)
            {
                m = m + 1;
                LineX1[m] = startxx;
                LineY1[m] = startyy + dis2 * (i - 1);
                LineX2[m] = endxx;
                LineY2[m] = startyy + dis2 * (i - 1);
            }
            for (int i = 1; i < m + 1; i++)///ارجاع
            {
                CallMesh.LineX1Real[i] = LineX1[i] * Math.Cos(-AngelRad) + LineY1[i] * Math.Sin(-AngelRad);
                CallMesh.LineY1Real[i] = -LineX1[i] * Math.Sin(-AngelRad) + LineY1[i] * Math.Cos(-AngelRad);
                CallMesh.LineX2Real[i] = LineX2[i] * Math.Cos(-AngelRad) + LineY2[i] * Math.Sin(-AngelRad);
                CallMesh.LineY2Real[i] = -LineX2[i] * Math.Sin(-AngelRad) + LineY2[i] * Math.Cos(-AngelRad);
            }
            #endregion
            #region//الشكل المستطيل
            for (int j = 1; j < RecShapeNumber + 1; j++)
            {
                ShapeNumbers = ShapeNumbers + 1;
                CallMesh.ShapeType[ShapeNumbers] = 1;
                for (int i = 1; i < 4 + 1; i++)
                {
                    m = m + 1;
                    CallMesh.ShapePointX[ShapeNumbers, i] = RecShape[j].EDX1R[i];
                    CallMesh.ShapePointY[ShapeNumbers, i] = RecShape[j].EDY1R[i];
                    CallMesh.LineX1Real[m] = RecShape[j].EDX1R[i];
                    CallMesh.LineY1Real[m] = RecShape[j].EDY1R[i];
                    CallMesh.LineX2Real[m] = RecShape[j].EDX2R[i];
                    CallMesh.LineY2Real[m] = RecShape[j].EDY2R[i];
                }
            }
            #endregion
            #region//الشكل الدائري
            for (int j = 1; j < CircleShapeNumber + 1; j++)
            {
                ShapeNumbers = ShapeNumbers + 1;
                CallMesh.ShapeType[ShapeNumbers] = 2;
                for (int i = 1; i < 32 + 1; i++)
                {
                    m = m + 1;
                    CallMesh.ShapePointX[ShapeNumbers, i] = CircleShape[j].PointXR[i];
                    CallMesh.ShapePointY[ShapeNumbers, i] = CircleShape[j].PointYR[i];
                    CallMesh.LineX1Real[m] = CircleShape[j].PointXR[i];
                    CallMesh.LineY1Real[m] = CircleShape[j].PointYR[i];
                    if (i != 32)
                    {
                        CallMesh.LineX2Real[m] = CircleShape[j].PointXR[i + 1];
                        CallMesh.LineY2Real[m] = CircleShape[j].PointYR[i + 1];
                    }
                    else
                    {
                        CallMesh.LineX2Real[m] = CircleShape[j].PointXR[1];
                        CallMesh.LineY2Real[m] = CircleShape[j].PointYR[1];
                    }
                }
            }
            #endregion
            #region//الشكل جدار قص
            for (int j = 1; j < FlangedWallShapeNumber + 1; j++)
            {
                ShapeNumbers = ShapeNumbers + 1;
                CallMesh.ShapeType[ShapeNumbers] = 3;
                for (int i = 1; i < 12 + 1; i++)
                {
                    m = m + 1;
                    CallMesh.ShapePointX[ShapeNumbers, i] = FlangedWallShape[j].PointXReal[i];
                    CallMesh.ShapePointY[ShapeNumbers, i] = FlangedWallShape[j].PointYReal[i];
                    CallMesh.LineX1Real[m] = FlangedWallShape[1].PointXReal[i];
                    CallMesh.LineY1Real[m] = FlangedWallShape[1].PointYReal[i];
                    if (i != 12)
                    {
                        CallMesh.LineX2Real[m] = FlangedWallShape[j].PointXReal[i + 1];
                        CallMesh.LineY2Real[m] = FlangedWallShape[j].PointYReal[i + 1];
                    }
                    else
                    {
                        CallMesh.LineX2Real[m] = FlangedWallShape[j].PointXReal[1];
                        CallMesh.LineY2Real[m] = FlangedWallShape[j].PointYReal[1];
                    }
                }
            }
            #endregion
            #region//الشكل تيه
            for (int j = 1; j < TeeShapeNumber + 1; j++)
            {
                ShapeNumbers = ShapeNumbers + 1;
                CallMesh.ShapeType[ShapeNumbers] = 4;
                for (int i = 1; i < 8 + 1; i++)
                {
                    m = m + 1;
                    CallMesh.ShapePointX[ShapeNumbers, i] = TeeShape[j].PointXReal[i];
                    CallMesh.ShapePointY[ShapeNumbers, i] = TeeShape[j].PointYReal[i];
                    CallMesh.LineX1Real[m] = TeeShape[1].PointXReal[i];
                    CallMesh.LineY1Real[m] = TeeShape[1].PointYReal[i];
                    if (i != 8)
                    {
                        CallMesh.LineX2Real[m] = TeeShape[j].PointXReal[i + 1];
                        CallMesh.LineY2Real[m] = TeeShape[j].PointYReal[i + 1];
                    }
                    else
                    {
                        CallMesh.LineX2Real[m] = TeeShape[j].PointXReal[1];
                        CallMesh.LineY2Real[m] = TeeShape[j].PointYReal[1];
                    }
                }
            }
            #endregion
            int TieNumbers = 0;
            #region//الأساور المستطيلة
            if (RecBarsNumber != 0)
            {
                int mm = 0;
                int theWall = 0;
                for (int j = 1; j < RecBarsNumber + 1; j++)
                {
                    int tah = 0;
                    if (RecLineBar[j].InFlangedWallShape != 0 & mm == 0)
                    {
                        theWall = RecLineBar[j].InFlangedWallShape;
                    }
                     if (theWall!=0)  mm = mm + 1;
                        
                        if (FlangedWallShape[theWall].StemSurrounded == 0 & mm==1) tah = 1;
                        if (FlangedWallShape[theWall].LeftFlangSurrounded == 0 & mm == 2) tah = 1;
                        if (FlangedWallShape[theWall].RightFlangSurrounded == 0 & mm == 3) tah = 1;
                        if (mm == 3)
                        {
                            mm = 0;
                            theWall = 0;
                        }

                    if (tah == 0)
                    {
                        TieNumbers = TieNumbers + 1;
                        CallMesh.TieType[TieNumbers] = 1;
                        for (int i = 1; i < 4 + 1; i++)
                        {
                            m = m + 1;
                            double valval = RecLineBar[j].TIEDR / 2;
                            if (i == 1)
                            {
                                CallMesh.TiePointX[TieNumbers, i] = RecLineBar[j].EDX1R[i] + valval;
                                CallMesh.TiePointY[TieNumbers, i] = RecLineBar[j].EDY1R[i] - valval;
                                CallMesh.LineX1Real[m] = RecLineBar[j].EDX1R[i] + valval;
                                CallMesh.LineY1Real[m] = RecLineBar[j].EDY1R[i] - valval;
                                CallMesh.LineX2Real[m] = RecLineBar[j].EDX2R[i] - valval;
                                CallMesh.LineY2Real[m] = RecLineBar[j].EDY2R[i] - valval;
                            }
                            if (i == 2)
                            {
                                CallMesh.TiePointX[TieNumbers, i] = RecLineBar[j].EDX1R[i] - valval;
                                CallMesh.TiePointY[TieNumbers, i] = RecLineBar[j].EDY1R[i] - valval;
                                CallMesh.LineX1Real[m] = RecLineBar[j].EDX1R[i] - valval;
                                CallMesh.LineY1Real[m] = RecLineBar[j].EDY1R[i] - valval;
                                CallMesh.LineX2Real[m] = RecLineBar[j].EDX2R[i] - valval;
                                CallMesh.LineY2Real[m] = RecLineBar[j].EDY2R[i] + valval;
                            }
                            if (i == 3)
                            {
                                CallMesh.TiePointX[TieNumbers, i] = RecLineBar[j].EDX1R[i] - valval;
                                CallMesh.TiePointY[TieNumbers, i] = RecLineBar[j].EDY1R[i] + valval;
                                CallMesh.LineX1Real[m] = RecLineBar[j].EDX1R[i] - valval;
                                CallMesh.LineY1Real[m] = RecLineBar[j].EDY1R[i] + valval;
                                CallMesh.LineX2Real[m] = RecLineBar[j].EDX2R[i] + valval;
                                CallMesh.LineY2Real[m] = RecLineBar[j].EDY2R[i] + valval;
                            }
                            if (i == 4)
                            {
                                CallMesh.TiePointX[TieNumbers, i] = RecLineBar[j].EDX1R[i] + valval;
                                CallMesh.TiePointY[TieNumbers, i] = RecLineBar[j].EDY1R[i] + valval;
                                CallMesh.LineX1Real[m] = RecLineBar[j].EDX1R[i] + valval;
                                CallMesh.LineY1Real[m] = RecLineBar[j].EDY1R[i] + valval;
                                CallMesh.LineX2Real[m] = RecLineBar[j].EDX2R[i] + valval;
                                CallMesh.LineY2Real[m] = RecLineBar[j].EDY2R[i] - valval;
                            }
                        }
                    }
                }
            }
            #endregion
            #region//الأساور الدائرية
            if (CircleBarsNumber != 0)
            {
                double zaweaa = 2 * Math.PI / 32;
                for (int j = 1; j < CircleBarsNumber + 1; j++)
                {
                    TieNumbers = TieNumbers + 1;
                    CallMesh.TieType[TieNumbers] = 2;
                    double Diameter1 = CircleLineBar[j].Diameter / 2 - (CircleLineBar[j].TIEDR / 2);
                    for (int i = 1; i < 32 + 1; i++)
                    {
                        m = m + 1;
                        double x1 = Math.Round(CircleLineBar[j].CenterX + Math.Cos(i * zaweaa) * Diameter1, 3);
                        double y1 = Math.Round(CircleLineBar[j].CenterY + Math.Sin(i * zaweaa) * Diameter1, 3);
                        double x2 = 0;
                        double y2 = 0;
                        if (i != 32)
                        {
                            x2 = Math.Round(CircleLineBar[j].CenterX + Math.Cos((i + 1) * zaweaa) * Diameter1, 3);
                            y2 = Math.Round(CircleLineBar[j].CenterY + Math.Sin((i + 1) * zaweaa) * Diameter1, 3);
                        }
                        else
                        {
                            x2 = Math.Round(CircleLineBar[j].CenterX + Math.Cos(1 * zaweaa) * Diameter1, 3);
                            y2 = Math.Round(CircleLineBar[j].CenterY + Math.Sin(1 * zaweaa) * Diameter1, 3);
                        }
                        CallMesh.TiePointX[TieNumbers, i] = x1;
                        CallMesh.TiePointY[TieNumbers, i] = y1;
                        CallMesh.LineX1Real[m] = x1;
                        CallMesh.LineY1Real[m] = y1;
                        CallMesh.LineX2Real[m] = x2;
                        CallMesh.LineY2Real[m] = y2;
                    }
                }
            }
            #endregion
            CallMesh.AllLine = m;
            CallMesh.ShapeNumbers = ShapeNumbers;
            CallMesh.TieNumbers = TieNumbers;
            CallMesh.DoMeshPlease();
        }
        private void CalculateGridFiberLines2d()
        {
            for (int i = 1; i < CallMesh.AllLine + 1; i++)
            {
                GridFiberX1D[i] = startX2d + Convert.ToInt32((CallMesh.LineX1Real[i]) * Zoom2d);
                GridFiberX2D[i] = startX2d + Convert.ToInt32((CallMesh.LineX2Real[i]) * Zoom2d);
                GridFiberY1D[i] = startY2d - Convert.ToInt32((CallMesh.LineY1Real[i]) * Zoom2d);
                GridFiberY2D[i] = startY2d - Convert.ToInt32((CallMesh.LineY2Real[i]) * Zoom2d);
            }
        }
        private void CalculateJointFiber2d()
        {
            for (int i = 1; i < CallMesh.AriaNoF + 1; i++)
            {
                for (int j = 1; j < CallMesh.AriaPointNoF[i] + 1; j++)
                {
                    AriaPointXintF[i, j] = startX2d + Convert.ToInt32((CallMesh.AriaPointXF[i, j]) * Zoom2d);
                    AriaPointYintF[i, j] = startY2d - Convert.ToInt32((CallMesh.AriaPointYF[i, j]) * Zoom2d);
                }
            }
        }
        #endregion
        #region//الرسم
        private void DrowBars()
        {
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            Font drawFont = new Font("Tahoma", 14, FontStyle.Bold, GraphicsUnit.Pixel);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            // HatchBrush hBrush = new HatchBrush(HatchStyle.DottedGrid ,Color.Red,Color.FromArgb(255, 128, 255, 255));
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            int TheWidth = pictureBox2.Width;
            int TheHeight = pictureBox2.Height;
            int tx1 = 0;
            int ty1 = 0;
            int tx2 = 0;
            int ty2 = 0;
            #region//رسم الشبكة
            double Dis1R = 50;
            double Dis2R = 500;
            int Dis12D = Convert.ToInt32(Dis1R * Zoom2d);
            int Dis22D = 10 * Dis12D;
            int GridNumber1XL = Convert.ToInt32(startX2d / (Dis1R * Zoom2d)) + 1;
            int GridNumber1XR = Convert.ToInt32((TheWidth - startX2d) / (Dis1R * Zoom2d)) + 1;
            if (GridNumber1XL < 0) GridNumber1XL = 0;
            if (GridNumber1XR < 0) GridNumber1XR = 0;
            int GridNumber1YL = Convert.ToInt32(startY2d / (Dis1R * Zoom2d)) + 1;
            int GridNumber1YR = Convert.ToInt32((TheHeight - startY2d) / (Dis1R * Zoom2d)) + 1;
            if (GridNumber1YL < 0) GridNumber1YL = 0;
            if (GridNumber1YR < 0) GridNumber1YR = 0;

            Pen pen = new Pen(Color.LightGray, 1f);
            for (int i = 0; i < GridNumber1XL + 1; i++)
            {
                int thedis = startX2d - Convert.ToInt32(i * Dis1R * Zoom2d);
                if (thedis >= 0 & thedis <= TheWidth)
                {
                    tx1 = thedis;
                    ty1 = 0;
                    tx2 = tx1;
                    ty2 = TheHeight;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                }
            }
            for (int i = 0; i < GridNumber1XR + 1; i++)
            {
                int thedis = startX2d + Convert.ToInt32(i * Dis1R * Zoom2d);
                if (thedis >= 0 & thedis <= TheWidth)
                {
                    tx1 = thedis;
                    ty1 = 0;
                    tx2 = tx1;
                    ty2 = TheHeight;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                }
            }
            for (int i = 0; i < GridNumber1YL + 1; i++)
            {
                int thedis = startY2d - Convert.ToInt32(i * Dis1R * Zoom2d);
                if (thedis >= 0 & thedis <= TheHeight)
                {
                    tx1 = 0;
                    ty1 = thedis;
                    tx2 = TheWidth;
                    ty2 = ty1;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                }
            }
            for (int i = 0; i < GridNumber1YR + 1; i++)
            {
                int thedis = startY2d + Convert.ToInt32(i * Dis1R * Zoom2d);
                if (thedis >= 0 & thedis <= TheHeight)
                {
                    tx1 = 0;
                    ty1 = thedis;
                    tx2 = TheWidth;
                    ty2 = ty1;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                }
            }
            //////////////////////////////////////////////////////////////////////
            int GridNumber2XL = Convert.ToInt32(startX2d / (Dis2R * Zoom2d)) + 1;
            int GridNumber2XR = Convert.ToInt32((TheWidth - startX2d) / (Dis2R * Zoom2d)) + 1;
            if (GridNumber2XL < 0) GridNumber2XL = 0;
            if (GridNumber2XR < 0) GridNumber2XR = 0;
            int GridNumber2YL = Convert.ToInt32(startY2d / (Dis2R * Zoom2d)) + 1;
            int GridNumber2YR = Convert.ToInt32((TheHeight - startY2d) / (Dis2R * Zoom2d)) + 1;
            if (GridNumber2YL < 0) GridNumber2YL = 0;
            if (GridNumber2YR < 0) GridNumber2YR = 0;
            int m = 0;
            pen = new Pen(Color.Gray, 1f);
            int WW = 1000000;// Convert.ToInt32((TheWidth) / Zoom2d);
            int HH = 1000000;// Convert.ToInt32((TheHeight) / Zoom2d);

            for (int i = 0; i < GridNumber2XL + 1; i++)
            {
                int thedis = startX2d - Convert.ToInt32(i * Dis2R * Zoom2d);
                if (thedis >= 0 & thedis <= TheWidth)
                {
                    m = m + 1;
                    tx1 = thedis;
                    ty1 = 0;
                    tx2 = tx1;
                    ty2 = TheHeight;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    TX1Rx[m] = -i * Dis2R;
                    TY1Rx[m] = HH;
                    TX2Rx[m] = TX1Rx[m];
                    TY2Rx[m] = -TY1Rx[m];
                }
            }
            for (int i = 0; i < GridNumber2XR + 1; i++)
            {
                int thedis = startX2d + Convert.ToInt32(i * Dis2R * Zoom2d);
                if (thedis >= 0 & thedis <= TheWidth)
                {
                    m = m + 1;
                    tx1 = thedis;
                    ty1 = 0;
                    tx2 = tx1;
                    ty2 = TheHeight;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    TX1Rx[m] = i * Dis2R;
                    TY1Rx[m] = HH;
                    TX2Rx[m] = TX1Rx[m];
                    TY2Rx[m] = -TY1Rx[m];
                }
            }
            GridNumberSX = m;
            m = 0;
            for (int i = 0; i < GridNumber2YL + 1; i++)
            {
                int thedis = startY2d - Convert.ToInt32(i * Dis2R * Zoom2d);
                if (thedis >= 0 & thedis <= TheHeight)
                {
                    m = m + 1;
                    tx1 = 0;
                    ty1 = thedis;
                    tx2 = TheWidth;
                    ty2 = ty1;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    TX1Ry[m] = WW;
                    TY1Ry[m] = i * Dis2R;
                    TX2Ry[m] = -TX1Ry[m];
                    TY2Ry[m] = TY1Ry[m];
                }
            }
            for (int i = 0; i < GridNumber2YR + 1; i++)
            {
                int thedis = startY2d + Convert.ToInt32(i * Dis2R * Zoom2d);
                if (thedis >= 0 & thedis <= TheHeight)
                {
                    m = m + 1;
                    tx1 = 0;
                    ty1 = thedis;
                    tx2 = TheWidth;
                    ty2 = ty1;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    TX1Ry[m] = WW;
                    TY1Ry[m] = -i * Dis2R;
                    TX2Ry[m] = -TX1Ry[m];
                    TY2Ry[m] = TY1Ry[m];
                }
            }
            GridNumberSY = m;
            ///////////////////////////////////////////////////////////////////////////////////
            #endregion
          
            
            #region//رسم مستطيلات الأشكال
            for (int i = 1; i < RecShapeNumber + 1; i++)
            {
                Point[] P = new Point[6];
                pen = new Pen(Color.Black, 1);
                if (RecShape[i].EDSelected[1] == 1)
                {
                    pen = new Pen(Color.Navy, 2);
                    float[] dashValues = { 5, 3, 5, 3 };
                    pen.DashPattern = dashValues;
                }
                tx1 = RecShape[i].EDX12D[1];
                ty1 = RecShape[i].EDY12D[1];
                tx2 = RecShape[i].EDX22D[1];
                ty2 = RecShape[i].EDY22D[1];
                g.DrawLine(pen, tx1, ty1, tx2, ty2);
                P[1].X = tx1;
                P[1].Y = ty1;

                pen = new Pen(Color.Black, 1);
                if (RecShape[i].EDSelected[2] == 1)
                {
                    pen = new Pen(Color.Navy, 2);
                    float[] dashValues = { 5, 3, 5, 3 };
                    pen.DashPattern = dashValues;
                }
                tx1 = RecShape[i].EDX12D[2];
                ty1 = RecShape[i].EDY12D[2];
                tx2 = RecShape[i].EDX22D[2];
                ty2 = RecShape[i].EDY22D[2];
                g.DrawLine(pen, tx1, ty1, tx2, ty2);
                P[2].X = tx1;
                P[2].Y = ty1;

                pen = new Pen(Color.Black, 1);
                if (RecShape[i].EDSelected[3] == 1)
                {
                    pen = new Pen(Color.Navy, 2);
                    float[] dashValues = { 5, 3, 5, 3 };
                    pen.DashPattern = dashValues;
                }
                tx1 = RecShape[i].EDX12D[3];
                ty1 = RecShape[i].EDY12D[3];
                tx2 = RecShape[i].EDX22D[3];
                ty2 = RecShape[i].EDY22D[3];
                g.DrawLine(pen, tx1, ty1, tx2, ty2);
                P[3].X = tx1;
                P[3].Y = ty1;

                pen = new Pen(Color.Black, 1);
                if (RecShape[i].EDSelected[4] == 1)
                {
                    pen = new Pen(Color.Navy, 2);
                    float[] dashValues = { 5, 3, 5, 3 };
                    pen.DashPattern = dashValues;
                }
                tx1 = RecShape[i].EDX12D[4];
                ty1 = RecShape[i].EDY12D[4];
                tx2 = RecShape[i].EDX22D[4];
                ty2 = RecShape[i].EDY22D[4];
                g.DrawLine(pen, tx1, ty1, tx2, ty2);
                P[4].X = tx1;
                P[4].Y = ty1;
                P[5].X = P[1].X;
                P[5].Y = P[1].Y;
                HatchBrush hBrush = new HatchBrush(HatchStyle.Percent25, Color.Gray, Color.FromArgb(30, Color.White));
                g.FillPolygon(hBrush, P);
                pen = new Pen(Color.Blue, 1);
                tx1 = Convert.ToInt32(RecShape[i].CenterX2D + 30 * Zoom2d);
                ty1 = RecShape[i].CenterY2D;
                tx2 = Convert.ToInt32(RecShape[i].CenterX2D - 30 * Zoom2d);
                ty2 = ty1;
                g.DrawLine(pen, tx1, ty1, tx2, ty2);
                tx1 = RecShape[i].CenterX2D; ;
                ty1 = Convert.ToInt32(RecShape[i].CenterY2D + 30 * Zoom2d);
                tx2 = tx1;
                ty2 = Convert.ToInt32(RecShape[i].CenterY2D - 30 * Zoom2d);
                g.DrawLine(pen, tx1, ty1, tx2, ty2);
            }
            #endregion
            #region//رسم جدران الأشكال
            for (int i = 1; i < FlangedWallShapeNumber + 1; i++)
            {
                Point[] P = new Point[14];
                pen = new Pen(Color.Black, 1);
                if (FlangedWallShape[i].Selected == 1)
                {
                    pen = new Pen(Color.Navy, 2);
                    float[] dashValues = { 5, 3, 5, 3 };
                    pen.DashPattern = dashValues;
                }

                for (int j = 1; j < 13; j++)
                {
                    if (j < 12)
                    {
                        tx1 = FlangedWallShape[i].PointX2D[j];
                        ty1 = FlangedWallShape[i].PointY2D[j];
                        tx2 = FlangedWallShape[i].PointX2D[j + 1];
                        ty2 = FlangedWallShape[i].PointY2D[j + 1];
                    }
                    if (j == 12)
                    {
                        tx1 = FlangedWallShape[i].PointX2D[12];
                        ty1 = FlangedWallShape[i].PointY2D[12];
                        tx2 = FlangedWallShape[i].PointX2D[1];
                        ty2 = FlangedWallShape[i].PointY2D[1];
                    }
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    P[j].X = tx1;
                    P[j].Y = ty1;
                }
                P[13].X = P[1].X;
                P[13].Y = P[1].Y;
                HatchBrush hBrush = new HatchBrush(HatchStyle.Percent25, Color.Gray, Color.FromArgb(30, Color.White));
                g.FillPolygon(hBrush, P);
                pen = new Pen(Color.Blue, 1);
                tx1 = Convert.ToInt32(FlangedWallShape[i].CenterX2D + 30 * Zoom2d);
                ty1 = FlangedWallShape[i].CenterY2D;
                tx2 = Convert.ToInt32(FlangedWallShape[i].CenterX2D - 30 * Zoom2d);
                ty2 = ty1;
                g.DrawLine(pen, tx1, ty1, tx2, ty2);
                tx1 = FlangedWallShape[i].CenterX2D; ;
                ty1 = Convert.ToInt32(FlangedWallShape[i].CenterY2D + 30 * Zoom2d);
                tx2 = tx1;
                ty2 = Convert.ToInt32(FlangedWallShape[i].CenterY2D - 30 * Zoom2d);
                g.DrawLine(pen, tx1, ty1, tx2, ty2);
            }
            #endregion
            #region//رسم دوائر أشكال
            for (int i = 1; i < CircleShapeNumber + 1; i++)
            {
                Point[] P = new Point[34];
                double Diameter = CircleShape[i].Diameter;
                pen = new Pen(Color.Black, 1);
                if (CircleShape[i].Selected == 1)
                {
                    pen = new Pen(Color.Navy, 3);
                    float[] dashValues = { 2, 1, 2, 1 };
                    pen.DashPattern = dashValues;
                }
                for (int j = 1; j < 31 + 1; j++)
                {
                    tx1 = CircleShape[i].PointX2D[j];
                    ty1 = CircleShape[i].PointY2D[j];
                    tx2 = CircleShape[i].PointX2D[j + 1];
                    ty2 = CircleShape[i].PointY2D[j + 1];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    P[j].X = tx1;
                    P[j].Y = ty1;
                }
                tx1 = CircleShape[i].PointX2D[32];
                ty1 = CircleShape[i].PointY2D[32];
                tx2 = CircleShape[i].PointX2D[1];
                ty2 = CircleShape[i].PointY2D[1];
                g.DrawLine(pen, tx1, ty1, tx2, ty2);
                P[32].X = tx1;
                P[32].Y = ty1;
                P[33].X = tx2;
                P[33].Y = ty2;
                HatchBrush hBrush = new HatchBrush(HatchStyle.Percent25, Color.Gray, Color.FromArgb(30, Color.White));
                g.FillPolygon(hBrush, P);

                pen = new Pen(Color.Blue, 1);
                tx1 = Convert.ToInt32(CircleShape[i].CenterX2D + 30 * Zoom2d);
                ty1 = CircleShape[i].CenterY2D;
                tx2 = Convert.ToInt32(CircleShape[i].CenterX2D - 30 * Zoom2d);
                ty2 = ty1;
                g.DrawLine(pen, tx1, ty1, tx2, ty2);
                tx1 = CircleShape[i].CenterX2D; ;
                ty1 = Convert.ToInt32(CircleShape[i].CenterY2D + 30 * Zoom2d);
                tx2 = tx1;
                ty2 = Convert.ToInt32(CircleShape[i].CenterY2D - 30 * Zoom2d);
                g.DrawLine(pen, tx1, ty1, tx2, ty2);
            }
            #endregion
            #region//رسم تيه الأشكال
            for (int i = 1; i < TeeShapeNumber + 1; i++)
            {
                Point[] P = new Point[10];
                pen = new Pen(Color.Black, 1);
                if (TeeShape[i].Selected == 1)
                {
                    pen = new Pen(Color.Navy, 2);
                    float[] dashValues = { 5, 3, 5, 3 };
                    pen.DashPattern = dashValues;
                }

                for (int j = 1; j < 9; j++)
                {
                    if (j < 8)
                    {
                        tx1 = TeeShape[i].PointX2D[j];
                        ty1 = TeeShape[i].PointY2D[j];
                        tx2 = TeeShape[i].PointX2D[j + 1];
                        ty2 = TeeShape[i].PointY2D[j + 1];
                    }
                    if (j == 8)
                    {
                        tx1 = TeeShape[i].PointX2D[8];
                        ty1 = TeeShape[i].PointY2D[8];
                        tx2 = TeeShape[i].PointX2D[1];
                        ty2 = TeeShape[i].PointY2D[1];
                    }
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    P[j].X = tx1;
                    P[j].Y = ty1;
                }
                P[9].X = P[1].X;
                P[9].Y = P[1].Y;
                HatchBrush hBrush = new HatchBrush(HatchStyle.Percent25, Color.Gray, Color.FromArgb(30, Color.White));
                g.FillPolygon(hBrush, P);
                pen = new Pen(Color.Blue, 1);
                tx1 = Convert.ToInt32(TeeShape[i].CenterX2D + 30 * Zoom2d);
                ty1 = TeeShape[i].CenterY2D;
                tx2 = Convert.ToInt32(TeeShape[i].CenterX2D - 30 * Zoom2d);
                ty2 = ty1;
                g.DrawLine(pen, tx1, ty1, tx2, ty2);
                tx1 = TeeShape[i].CenterX2D; ;
                ty1 = Convert.ToInt32(TeeShape[i].CenterY2D + 30 * Zoom2d);
                tx2 = tx1;
                ty2 = Convert.ToInt32(TeeShape[i].CenterY2D - 30 * Zoom2d);
                g.DrawLine(pen, tx1, ty1, tx2, ty2);
            }
            #endregion
            #region//رسم مستطيلات التسليح
            for (int i = 1; i < RecBarsNumber + 1; i++)
            {
                int aswaraD = Convert.ToInt32(RecLineBar[i].TIEDR * Zoom2d);
                int aswaraD2 = aswaraD / 2;
                int RX1 = 0;
                int RX2 = 0;
                int RX3 = 0;
                int RX4 = 0;
                int RY1 = 0;
                int RY2 = 0;
                int RY3 = 0;
                int RY4 = 0;
                pen = new Pen(Color.Black, 1);
                if (RecLineBar[i].EDSelected[1] == 1)
                {
                    pen = new Pen(Color.Navy, 2);
                    float[] dashValues = { 5, 3, 5, 3 };
                    pen.DashPattern = dashValues;
                }
                for (int j = 1; j < 5; j++)
                {

                    tx1 = RecLineBar[i].EDX12D[j];
                    ty1 = RecLineBar[i].EDY12D[j];
                    tx2 = RecLineBar[i].EDX22D[j];
                    ty2 = RecLineBar[i].EDY22D[j];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                    if (j == 1)
                    {
                        RX1 = tx1 + aswaraD2;
                        RY1 = ty1 + aswaraD2;
                    }
                    if (j == 2)
                    {
                        RX2 = tx1 - aswaraD2;
                        RY2 = ty1 + aswaraD2;
                    }
                    if (j == 3)
                    {
                        RX3 = tx1 - aswaraD2;
                        RY3 = ty1 - aswaraD2;
                    }
                    if (j == 4)
                    {
                        RX4 = tx1 + aswaraD2;
                        RY4 = ty1 + aswaraD2;
                    }
                }
                // Construct a new RectangleF.
                RectangleF myRectangleF = new RectangleF(RX1, RY1, RX2 - RX1, RY3 - RY2);
                // Call the Round method.
                Rectangle roundedRectangle = Rectangle.Round(myRectangleF);
                // Draw the rounded rectangle in red.
                //Pen redPen = new Pen(Color.Red, 4);
                //  g.DrawRectangle(redPen, roundedRectangle);
                // Call the Truncate method.
                Rectangle truncatedRectangle = Rectangle.Truncate(myRectangleF);
                // Draw the truncated rectangle in white.
                Pen whitePen = new Pen(Color.Gray, aswaraD);
                g.DrawRectangle(whitePen, roundedRectangle);

                pen = new Pen(Color.Blue, 1);
                tx1 = Convert.ToInt32(RecLineBar[i].CenterX2D + 30 * Zoom2d);
                ty1 = RecLineBar[i].CenterY2D;
                tx2 = Convert.ToInt32(RecLineBar[i].CenterX2D - 30 * Zoom2d);
                ty2 = ty1;
                g.DrawLine(pen, tx1, ty1, tx2, ty2);
                tx1 = RecLineBar[i].CenterX2D; ;
                ty1 = Convert.ToInt32(RecLineBar[i].CenterY2D + 30 * Zoom2d);
                tx2 = tx1;
                ty2 = Convert.ToInt32(RecLineBar[i].CenterY2D - 30 * Zoom2d);
                g.DrawLine(pen, tx1, ty1, tx2, ty2);
            }
            #endregion
            #region//رسم دوائر التسليح
            drawBrush = new SolidBrush(Color.Black);
            for (int i = 1; i < CircleBarsNumber + 1; i++)
            {
                double Diameter = CircleLineBar[i].Diameter;
                int aswaraD = Convert.ToInt32(CircleLineBar[i].TIEDR * Zoom2d);
                int DR1 = CircleLineBar[i].TIEDR;
                int DR = CircleLineBar[i].DR;
                pen = new Pen(Color.Black, 1);
                if (CircleLineBar[i].Selected == 1)
                {
                    pen = new Pen(Color.Navy, 3);
                    float[] dashValues = { 2, 1, 2, 1 };
                    pen.DashPattern = dashValues;
                }
                for (int j = 1; j < 31 + 1; j++)
                {
                    tx1 = CircleLineBar[i].PointX2D[j];
                    ty1 = CircleLineBar[i].PointY2D[j];
                    tx2 = CircleLineBar[i].PointX2D[j + 1];
                    ty2 = CircleLineBar[i].PointY2D[j + 1];
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                }
                tx1 = CircleLineBar[i].PointX2D[32];
                ty1 = CircleLineBar[i].PointY2D[32];
                tx2 = CircleLineBar[i].PointX2D[1];
                ty2 = CircleLineBar[i].PointY2D[1];
                g.DrawLine(pen, tx1, ty1, tx2, ty2);

                Diameter = Diameter - DR1;
                tx1 = startX2d + Convert.ToInt32((CircleLineBar[i].CenterX - Diameter / 2) * Zoom2d);
                ty1 = startY2d - Convert.ToInt32((CircleLineBar[i].CenterY + Diameter / 2) * Zoom2d);

                tx2 = Convert.ToInt32((Diameter) * Zoom2d);
                ty2 = Convert.ToInt32((Diameter) * Zoom2d);
                pen = new Pen(Color.Gray, aswaraD);
                g.DrawEllipse(pen, tx1, ty1, tx2, ty2);

                pen = new Pen(Color.Blue, 1);
                tx1 = Convert.ToInt32(CircleLineBar[i].CenterX2D + 30 * Zoom2d);
                ty1 = CircleLineBar[i].CenterY2D;
                tx2 = Convert.ToInt32(CircleLineBar[i].CenterX2D - 30 * Zoom2d);
                ty2 = ty1;
                g.DrawLine(pen, tx1, ty1, tx2, ty2);
                tx1 = CircleLineBar[i].CenterX2D; ;
                ty1 = Convert.ToInt32(CircleLineBar[i].CenterY2D + 30 * Zoom2d);
                tx2 = tx1;
                ty2 = Convert.ToInt32(CircleLineBar[i].CenterY2D - 30 * Zoom2d);
                g.DrawLine(pen, tx1, ty1, tx2, ty2);
            }
            #endregion
            #region//رسم الفايبر
            pen = new Pen(Color.Black, 1);
            drawBrush = new SolidBrush(Color.Red);
            for (int i = 1; i < CallMesh.AriaNoF + 1; i++)
            {
                int N = CallMesh.AriaPointNoF[i];
                Point[] P = new Point[N];
                for (int j = 1; j < N + 1; j++)
                {
                    P[j - 1].X = AriaPointXintF[i, j];
                    P[j - 1].Y = AriaPointYintF[i, j];
                }
                HatchBrush hBrush = new HatchBrush(HatchStyle.Percent25, Color.Red, Color.FromArgb(30, Color.White));
                if (CallMesh.AriaTypeF[i] == 1)
                {
                    hBrush = new HatchBrush(HatchStyle.Percent25, Color.Yellow, Color.FromArgb(30, Color.White));
                }
                else
                {
                    hBrush = new HatchBrush(HatchStyle.DiagonalCross, Color.Blue, Color.FromArgb(30, Color.White));
                }
                g.FillPolygon(hBrush, P);
                for (int j = 1; j < N; j++)
                {
                    tx1 = P[j - 1].X;
                    ty1 = P[j - 1].Y;
                    tx2 = P[j].X;
                    ty2 = P[j].Y;
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                }
                int cw = Convert.ToInt32(10 * Zoom2d);
                tx1 = startX2d + Convert.ToInt32((CallMesh.AriaCenterXF[i]) * Zoom2d);
                ty1 = startY2d - Convert.ToInt32((CallMesh.AriaCenterYF[i]) * Zoom2d);
                g.FillEllipse(drawBrush, tx1 - cw / 2, ty1 - cw / 2, cw, cw);
            }
            /*
                  pen = new Pen(Color.Black, 1);
                   for (int i = 1; i < CallMesh.AllLine + 1; i++)
                   {
                       tx1 = GridFiberX1D[i];
                       ty1 = GridFiberY1D[i];
                       tx2 = GridFiberX2D[i];
                       ty2 = GridFiberY2D[i];
                       g.DrawLine(pen, tx1, ty1, tx2, ty2);
                  }
               */

            /*
            drawBrush = new SolidBrush(Color.Black);
            for (int i = 1; i < CallMesh.AriaNoF + 1; i++)
            {
               tx1 = startX2d + Convert.ToInt32((CallMesh.AriaCenterXF[i]) * Zoom2d);
               ty1 = startY2d - Convert.ToInt32((CallMesh.AriaCenterYF[i]) * Zoom2d);
               g.DrawString(i.ToString(), drawFont, drawBrush, tx1 + 5, ty1 - 10);
            }
            */

            /*
            drawBrush = new SolidBrush(Color.Black);
            for (int i = 1; i < CallMesh.PointNumber2d + 1; i++)
            {
                tx1 = startX2d + Convert.ToInt32((CallMesh.PointXReal[i]) * Zoom2d);
                ty1 = startY2d - Convert.ToInt32((CallMesh.PointYReal[i]) * Zoom2d);
                g.DrawString(i.ToString(), drawFont, drawBrush, tx1 + 5, ty1 - 10);
            }
            */

            #endregion
            #region//رسم مستقيمات قضبان التسليح
            for (int i = 1; i < LineBarsNumber + 1; i++)
            {
                pen = new Pen(Color.Black, 1f);
                if (LineBar[i].Selected == 1)
                {
                    pen = new Pen(Color.Black, 2f);
                    float[] dashValues = { 5, 3, 5, 3 };
                    pen.DashPattern = dashValues;
                }
                tx1 = LineBar[i].X12D;
                ty1 = LineBar[i].Y12D;
                tx2 = LineBar[i].X22D;
                ty2 = LineBar[i].Y22D;
                g.DrawLine(pen, tx1, ty1, tx2, ty2);
            }
            #endregion
            #region//رسم الشناغل
            for (int i = 1; i < ClipsNumber + 1; i++)
            {
                int thick = Convert.ToInt32(Clip[i].DR * Zoom2d);
                pen = new Pen(Color.Gray, thick);
                int tx10 = Clip[i].X12D;
                int ty10 = Clip[i].Y12D;
                int tx20 = Clip[i].X22D;
                int ty20 = Clip[i].Y22D;
                g.DrawLine(pen, tx10, ty10, tx20, ty20);
                if (Clip[i].Selected == 1)
                {
                    Pen pen1 = new Pen(Color.Navy, 3);
                    float[] dashValues = { 5, 3, 5, 3 };
                    pen1.DashPattern = dashValues;
                    g.DrawLine(pen1, tx10, ty10, tx20, ty20);
                }
                float Diameter = Convert.ToInt32(Clip[i].DR1 * Zoom2d);
                float Diameter1 = Diameter / 2;
                float sweepAngle = (float)Math.Round(Angulo(tx10, ty10, tx20, ty20), 2);
                float startAngle = 90 + sweepAngle;

                double angel = (90 - sweepAngle) * (Math.PI / 180);
                double angel1 = (360 - sweepAngle) * (Math.PI / 180);
                double angel2 = (90 - (360 - sweepAngle)) * (Math.PI / 180);

                if (Clip[i].Dir1 == 0)
                {
                    tx2 = Convert.ToInt32(tx20 - (Diameter1 + Diameter1 * Math.Cos(angel)));
                    ty2 = Convert.ToInt32(ty20 - (Diameter1 - Diameter1 * Math.Sin(angel)));
                    g.DrawArc(pen, tx2, ty2, Diameter, Diameter, startAngle + 5, -205);
                    tx1 = Convert.ToInt32(tx20 + (Diameter * Math.Sin(angel1)));
                    ty1 = Convert.ToInt32(ty20 + (Diameter * Math.Cos(angel1)));
                    tx2 = Convert.ToInt32(tx1 - (Diameter * Math.Cos(angel1)));
                    ty2 = Convert.ToInt32(ty1 + (Diameter * Math.Sin(angel1)));
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                }
                if (Clip[i].Dir2 == 0)
                {
                    tx1 = Convert.ToInt32(tx10 - (Diameter1 + Diameter1 * Math.Cos(angel)));
                    ty1 = Convert.ToInt32(ty10 - (Diameter1 - Diameter1 * Math.Sin(angel)));
                    g.DrawArc(pen, tx1, ty1, Diameter, Diameter, startAngle + 185, -205);
                    tx1 = Convert.ToInt32(tx10 + (Diameter * Math.Sin(angel1)));
                    ty1 = Convert.ToInt32(ty10 + (Diameter * Math.Cos(angel1)));
                    tx2 = Convert.ToInt32(tx1 + (Diameter * Math.Cos(angel1)));
                    ty2 = Convert.ToInt32(ty1 - (Diameter * Math.Sin(angel1)));
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                }
                if (Clip[i].Dir1 == 1)
                {
                    tx2 = Convert.ToInt32(tx20 - (Diameter1 + Diameter1 * Math.Cos(angel2)));
                    ty2 = Convert.ToInt32(ty20 - (Diameter1 + Diameter1 * Math.Sin(angel2)));
                    g.DrawArc(pen, tx2, ty2, Diameter, Diameter, startAngle + 5, -205);
                    tx1 = Convert.ToInt32(tx20 - (Diameter * Math.Cos(angel2)));
                    ty1 = Convert.ToInt32(ty20 - (Diameter * Math.Sin(angel2)));
                    tx2 = Convert.ToInt32(tx1 - (Diameter * Math.Cos(angel1)));
                    ty2 = Convert.ToInt32(ty1 + (Diameter * Math.Sin(angel1)));
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                }
                if (Clip[i].Dir2 == 1)
                {
                    tx1 = Convert.ToInt32(tx10 - (Diameter1 + Diameter1 * Math.Cos(angel2)));
                    ty1 = Convert.ToInt32(ty10 - (Diameter1 + Diameter1 * Math.Sin(angel2)));
                    g.DrawArc(pen, tx1, ty1, Diameter, Diameter, startAngle + 185, -205);
                    tx1 = Convert.ToInt32(tx10 - (Diameter * Math.Cos(angel2)));
                    ty1 = Convert.ToInt32(ty10 - (Diameter * Math.Sin(angel2)));
                    tx2 = Convert.ToInt32(tx1 + (Diameter * Math.Cos(angel1)));
                    ty2 = Convert.ToInt32(ty1 - (Diameter * Math.Sin(angel1)));
                    g.DrawLine(pen, tx1, ty1, tx2, ty2);
                }
            }
            #endregion
            #region//رسم القضبان
            drawBrush = new SolidBrush(Color.Black);
            for (int i = 1; i < BarsNumber + 1; i++)///////////****************************************************
            {
                pen = new Pen(Color.Red, 1f);
                tx1 = Bar[i].X2D;
                ty1 = Bar[i].Y2D;
                g.FillEllipse(drawBrush, tx1 - Bar[i].D2D / 2, ty1 - Bar[i].D2D / 2, Bar[i].D2D, Bar[i].D2D);
                int hh = Convert.ToInt32((Bar[i].D2D));
                g.DrawLine(pen, tx1 - hh, ty1, tx1 + hh, ty1);
                g.DrawLine(pen, tx1, ty1 - hh, tx1, ty1 + hh);
                if (Bar[i].Selected == 1)
                {
                    pen = new Pen(Color.MidnightBlue, 2f);
                    g.DrawLine(pen, tx1 - hh, ty1 - hh, tx1 + hh, ty1 + hh);
                    g.DrawLine(pen, tx1 + hh, ty1 - hh, tx1 - hh, ty1 + hh);
                }
            }
            #endregion
            #region//رسم المحاور العامة
            pen = new Pen(Color.Green, 1f);
            drawBrush = new SolidBrush(Color.Green);
            tx1 = startX2d;
            ty1 = startY2d;
            tx2 = tx1;
            ty2 = ty1 - 100;
            g.DrawLine(pen, tx1, ty1, tx2, ty2);
            g.DrawString("Y", drawFont, drawBrush, tx2 - 7, ty2 - 27);
            tx1 = tx2;
            ty1 = ty2;
            tx2 = tx1 - 7;
            ty2 = ty1 + 15;
            g.DrawLine(pen, tx1, ty1, tx2, ty2);
            tx2 = tx1 + 7;
            ty2 = ty1 + 15;
            g.DrawLine(pen, tx1, ty1, tx2, ty2);

            tx1 = startX2d;
            ty1 = startY2d;
            tx2 = tx1 + 100;
            ty2 = ty1;
            g.DrawLine(pen, tx1, ty1, tx2, ty2);
            g.DrawString("X", drawFont, drawBrush, tx2 + 10, ty2 - 7);
            tx1 = tx2;
            ty1 = ty2;
            tx2 = tx1 - 15;
            ty2 = ty1 - 7;
            g.DrawLine(pen, tx1, ty1, tx2, ty2);
            tx2 = tx1 - 15;
            ty2 = ty1 + 7;
            g.DrawLine(pen, tx1, ty1, tx2, ty2);
            #endregion
            drawFont.Dispose();
            drawBrush.Dispose();
            g.Dispose();
        }
        #endregion
        public void UnSellectAll()
        {
            SelectedType = 0;
            SelecteLinedBar = 0;
            SelectedBar = 0;
            SelectedInED = 0;
            SelectedRecBar = 0;
            SelectedRecBar1 = 0;
            SelectedRecBar2 = 0;
            SelectedRecBar3 = 0;
            SelectedCorner = 0;
            SelectedRecShape = 0;
            SelectedCircleLineBar = 0;
            SelectedFlangedWallShape = 0;
            SelectedTeeShape = 0;
            # region //تحديد القضبان
            for (int i = 1; i < BarsNumber + 1; i++)
            {
                Bar[i].Selected = 0;
            }
            for (int i = 1; i < LineBarsNumber + 1; i++)
            {
                LineBar[i].Selected = 0;
            }
            for (int i = 1; i < ClipsNumber + 1; i++)
            {
                Clip[i].Selected = 0;
            }
            for (int i = 1; i < RecBarsNumber + 1; i++)
            {
                RecLineBar[i].EDSelected[1] = 0;
                RecLineBar[i].EDSelected[2] = 0;
                RecLineBar[i].EDSelected[3] = 0;
                RecLineBar[i].EDSelected[4] = 0;
            }
            for (int i = 1; i < RecShapeNumber + 1; i++)
            {
                for (int add = 1; add < 5; add++)
                {
                    RecShape[i].EDSelected[add] = 0;
                }
            }
            for (int i = 1; i < CircleBarsNumber + 1; i++)
            {
                CircleLineBar[i].Selected = 0;
            }
            for (int i = 1; i < CircleShapeNumber + 1; i++)
            {
                CircleShape[i].Selected = 0;
            }
            for (int i = 1; i < TeeShapeNumber + 1; i++)
            {
                TeeShape[i].Selected = 0;
            }
            for (int i = 1; i < FlangedWallShapeNumber + 1; i++)
            {
                FlangedWallShape[i].Selected = 0;
            }

            #endregion
            // toolStripButton1.Checked = true;
            DrawType = 0;
            LineMove2dVisible = 0;
            drowclick = 0;
            Render2d();

        }
        #region//اجرائيات المسافة و الزاوية و التقاطع
        private void DistanceCalc(int X, int Y, int X1, int Y1, int X2, int Y2)
        {
            distance = 1000000;
            int Tah = 0;
            if ((Y2 == Y1))
            {
                if (X >= X1 & X <= X2) Tah = 1;
                if (X <= X1 & X >= X2) Tah = 1;
                if (Tah == 1)
                {
                    distance = Math.Abs(Y - Y1);
                    goto endloop;
                }
            }
            if ((X2 == X1))
            {
                if (Y >= Y1 & Y <= Y2) Tah = 1;
                if (Y <= Y1 & Y >= Y2) Tah = 1;
                if (Tah == 1)
                {
                    distance = Math.Abs(X - X1);
                    goto endloop;
                }
            }
            if (X2 != X1)
            {
                if (X >= X1 & X <= X2 & Y >= Y1 & Y <= Y2) Tah = 1;
                if (X >= X2 & X <= X1 & Y >= Y2 & Y <= Y1) Tah = 1;
                if (X >= X2 & X <= X1 & Y >= Y1 & Y <= Y2) Tah = 1;
                if (X >= X1 & X <= X2 & Y >= Y2 & Y <= Y1) Tah = 1;
                if (Tah == 1)
                {
                    distance = Math.Abs((X2 - X1) * (Y1 - Y) - (X1 - X) * (Y2 - Y1)) / Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2));
                }
            }
        endloop: { }
        }
        private double Angulo(double x1, double y1, double x2, double y2)
        {
            double degrees;
            if (x2 - x1 == 0)
            {
                if (y2 > y1)
                    degrees = 90;
                else
                    degrees = 270;
            }
            else
            {
                double riseoverrun = (double)(y2 - y1) / (double)(x2 - x1);
                double radians = Math.Atan(riseoverrun);
                degrees = radians * ((double)180 / Math.PI);
                if ((x2 - x1) < 0 || (y2 - y1) < 0)
                    degrees += 180;
                if ((x2 - x1) > 0 && (y2 - y1) < 0)
                    degrees -= 180;
                if (degrees < 0)
                    degrees += 360;
            }
            return degrees;
        }
        private void checkintersection(double X1, double Y1, double X2, double Y2, double X3, double Y3, double X4, double Y4)
        {
            int THETAHKIK = 0;
            double x0 = 0;
            double y0 = 0;
            if (X2 == X1 & Y3 == Y4)//شاقولي و أفقي
            {
                int tah = 0;
                if (X1 < X3 & X1 < X4) tah = 1;
                if (X1 > X3 & X1 > X4) tah = 1;
                if (Y3 < Y1 & Y3 < Y2) tah = 1;
                if (Y3 > Y1 & Y3 > Y2) tah = 1;
                if (tah == 1) goto end100;
                x0 = X1;
                y0 = Y3;
                x0 = Math.Round(x0, 0);
                y0 = Math.Round(y0, 0);
                if (x0 > X3 & x0 > X4) goto end100;
                if (x0 < X3 & x0 < X4) goto end100;
                if (y0 > Y3 & y0 > Y4) goto end100;
                if (y0 < Y3 & y0 < Y4) goto end100;
                if (x0 > X1 & x0 > X2) goto end100;
                if (x0 < X1 & x0 < X2) goto end100;
                if (y0 > Y1 & y0 > Y2) goto end100;
                if (y0 < Y1 & y0 < Y2) goto end100;
                THETAHKIK = 1;
                goto end100;
            }
            if (X2 == X1 & Y3 != Y4 & X4 != X3)//شاقولي و مائل
            {
                x0 = X1;
                y0 = ((x0 - X3) / (X4 - X3)) * (Y4 - Y3) + Y3;
                int tah = 0;
                if (y0 < Y1 & y0 < Y2) tah = 1;
                if (y0 > Y1 & y0 > Y2) tah = 1;
                if (tah == 1) goto end100;
                x0 = Math.Round(x0, 0);
                y0 = Math.Round(y0, 0);
                if (x0 > X3 & x0 > X4) goto end100;
                if (x0 < X3 & x0 < X4) goto end100;
                if (y0 > Y3 & y0 > Y4) goto end100;
                if (y0 < Y3 & y0 < Y4) goto end100;
                if (x0 > X1 & x0 > X2) goto end100;
                if (x0 < X1 & x0 < X2) goto end100;
                if (y0 > Y1 & y0 > Y2) goto end100;
                if (y0 < Y1 & y0 < Y2) goto end100;
                THETAHKIK = 1;
                goto end100;
            }
            double bast = (Y3 - Y1) * (X4 - X3) * (X2 - X1);//حالة عامة
            bast = bast + X1 * (Y2 - Y1) * (X4 - X3);
            bast = bast - X3 * (Y4 - Y3) * (X2 - X1);
            double makam = (Y2 - Y1) * (X4 - X3);
            makam = makam - (Y4 - Y3) * (X2 - X1);
            if (makam != 0)
            {
                x0 = bast / makam;
                y0 = (((Y2 - Y1) / (X2 - X1)) * (x0 - X1)) + Y1;
                x0 = Math.Round(x0, 0);
                y0 = Math.Round(y0, 0);
                if (x0 > X3 & x0 > X4) goto end100;
                if (x0 < X3 & x0 < X4) goto end100;
                if (y0 > Y3 & y0 > Y4) goto end100;
                if (y0 < Y3 & y0 < Y4) goto end100;
                if (x0 > X1 & x0 > X2) goto end100;
                if (x0 < X1 & x0 < X2) goto end100;
                if (y0 > Y1 & y0 > Y2) goto end100;
                if (y0 < Y1 & y0 < Y2) goto end100;
                THETAHKIK = 1;
            }
        end100: { }
            INTERSECTION = THETAHKIK;
            TheX0 = x0;
            TheY0 = y0;
        }
        #endregion
        #region///menue strip
        private void toolStripButton32_Click(object sender, EventArgs e)
        {
            Zoom2d = Math.Round(Zoom2d + 0.05, 2);
            startX2d = lastzoomX2d - (int)(lastzoomX2dR * Zoom2d);
            startY2d = lastzoomY2d + (int)(lastzoomY2dR * Zoom2d);
            TempX = lastzoomX2d;
            TempY = lastzoomY2d;
            Render2d();
            pictureBox2Draw();
        }
        private void toolStripButton33_Click(object sender, EventArgs e)
        {
            if (Math.Round(Zoom2d - 0.05, 2) > 0.01)
            {
                Zoom2d = Math.Round(Zoom2d - 0.05, 2);
                startX2d = lastzoomX2d - (int)(lastzoomX2dR * Zoom2d);
                startY2d = lastzoomY2d + (int)(lastzoomY2dR * Zoom2d);
                TempX = lastzoomX2d;
                TempY = lastzoomY2d;
                Render2d();
                pictureBox2Draw();
            }
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            OpenVoid();
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            SaveVoid();
        }
        private void toolStripButton24_Click(object sender, EventArgs e)
        {
            if (TempSelectedFile - 1 > 0)
            {
                TempSelectedFile = TempSelectedFile - 1;
                OpenTemp();
                Render2d();
            }
        }
        private void toolStripButton25_Click(object sender, EventArgs e)
        {
            if (TempSelectedFile + 1 <= TempFile)
            {
                TempSelectedFile = TempSelectedFile + 1;
                OpenTemp();
                Render2d();
            }
        }
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (toolStripButton5.Checked == true)
            {
                GridIntersections = 1;
            }
            else
            {
                GridIntersections = 0;
            }
        }
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (toolStripButton6.Checked == true)
            {
                LineEnds = 1;
            }
            else
            {
                LineEnds = 0;
            }
        }
        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            DrawType = 0;
            LineMove2dVisible = 0;
            drowclick = 0;
            EditType = 1;
            UnSellectAll();
            Render2d();
            toolStripButton1.Checked = false;
        }
        private void drawStraightWallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton14.Checked = false;
            EditType = 0;
            toolStripButton1.Checked = false;
            WallType = 1;
            DrawType = 5;
        }
        private void drawFlangedWallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton14.Checked = false;
            EditType = 0;
            toolStripButton1.Checked = false;
            WallType = 2;
            DrawType = 10;
        }
        private void dToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton14.Checked = false;
            EditType = 0;
            toolStripButton1.Checked = false;
            DrawType = 1;
        }
        private void fToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton14.Checked = false;
            EditType = 0;
            toolStripButton1.Checked = false;
            DrawType = 2;
        }
        private void gToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton14.Checked = false;
            EditType = 0;
            toolStripButton1.Checked = false;
            DrawType = 3;
        }
        private void drawCircleRebarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton14.Checked = false;
            EditType = 0;
            toolStripButton14.Checked = false;
            DrawType = 4;
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DrawType = 0;
            LineMove2dVisible = 0;
            drowclick = 0;
            EditType = 0;
            toolStripButton14.Checked = false;
        }
        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            EditType = 0;
            # region //تحديد القضبان
            for (int i = 1; i < BarsNumber + 1; i++)
            {
                Bar[i].Selected = 0;
            }
            for (int i = 1; i < LineBarsNumber + 1; i++)
            {
                LineBar[i].Selected = 0;
            }
            for (int i = 1; i < RecBarsNumber + 1; i++)
            {
                RecLineBar[i].EDSelected[1] = 0;
                RecLineBar[i].EDSelected[2] = 0;
                RecLineBar[i].EDSelected[3] = 0;
                RecLineBar[i].EDSelected[4] = 0;
            }
            for (int i = 1; i < RecShapeNumber + 1; i++)
            {
                for (int add = 1; add < 5; add++)
                {
                    RecShape[i].EDSelected[add] = 0;
                }
            }
            for (int i = 1; i < CircleBarsNumber + 1; i++)
            {
                CircleLineBar[i].Selected = 0;
            }
            for (int i = 1; i < CircleShapeNumber + 1; i++)
            {
                CircleShape[i].Selected = 0;
            }

            for (int i = 1; i < FlangedWallShapeNumber + 1; i++)
            {
                FlangedWallShape[i].Selected = 0;
            }
            #endregion
            Render2d();
        }
        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            # region //تحديد القضبان
            for (int i = 1; i < BarsNumber + 1; i++)
            {
                Bar[i].Selected = 1;
            }
            for (int i = 1; i < LineBarsNumber + 1; i++)
            {
                LineBar[i].Selected = 1;
            }
            #endregion
            Render2d();
        }
        private void toolStripButton30_Click(object sender, EventArgs e)
        {
            startX2d = 100;
            startY2d = 400;
            Render2d();
        }
        private void drawRectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton14.Checked = false;
            EditType = 0;
            toolStripButton1.Checked = false;
            WallType = 0;
            DrawType = 5;
        }
        private void drawCircleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton14.Checked = false;
            EditType = 0;
            toolStripButton1.Checked = false;
            DrawType = 6;
        }
        private void drawRectangleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripButton14.Checked = false;
            EditType = 0;
            toolStripButton1.Checked = false;
            DrawType = 30;
        }
        private void drawRebarTieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton14.Checked = false;
            EditType = 0;
            toolStripButton1.Checked = false;
            DrawType = 20;
        }
        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            SectionDesignerForm theform = new SectionDesignerForm();
            theform.ShowDialog();
        }
        private void fiberGridToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SectionFiberForm TheForm = new SectionFiberForm();
            TheForm.ShowDialog();
        }
        private void drawTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton14.Checked = false;
            EditType = 0;
            toolStripButton1.Checked = false;
            WallType = 0;
            DrawType = 7;
        }
        #endregion
        private void button3_Click(object sender, EventArgs e)
        {
            CalculateGridFiberLines();
            Render2d();
        }
    }
}
