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
    public partial class MainForm : Form
    {
        #region
        Label jointLBL3d = new Label();
        RectangleShape JointShape3d = new RectangleShape();
        LineShape[] SelectShape3d = new LineShape[5];
        Point drawOrigin;
        double XelevR = 0;
        double YelevR =0;
        int LineMoveX1 = 0;
        int LineMoveY1 = 0;
        int LineMoveX2 = 0;
        int LineMoveY2 = 0;
        int TempX;
        int TempY;
        double[] TempXReal = new double[3];
        double[] TempYReal = new double[3];
        double[] TempZReal = new double[3];
        double TempX12Real;
        double TempY12Real;
        double TempZ12Real;
        int[] TahkikXY = new int[3];
        int Tahkik;
        int Picture3IsPainted;
        # endregion
        public MainForm()
        {
            InitializeComponent();
            this.MouseWheel += new MouseEventHandler(pictureBox2_MouseWheel);
        }
        public ArchWallS[] ArchWall = new ArchWallS[200];
        public FrameElements[] FrameElement = new FrameElements[10000];
        double TheResultValue = 0;
        int SelectedStorytodrowallstory = 0;
        int INTERSECTION = 0;
        double TheX0 = 0;
        double TheY0 = 0;
        string SnapFromType = "";
        int SelectedGridpoint = 0;
        int SelectedGridpoint3d = 0;
        int SelectedGridpointelev = 0;
        int SelectedJoint = 0;
        int lastzoomX2d = 0;
        int lastzoomY2d = 0;
        double lastzoomX2dR = 0;
        double lastzoomY2dR = 0;
        int lastzoomXelev = 0;
        int lastzoomYelev = 0;
        double lastzoomXelevR = 0;
        double lastzoomYelevR = 0;
        int lastzoomX3d = 0;
        int lastzoomY3d = 0;
        double lastzoomX3dR = 0;
        double lastzoomY3dR = 0;
        double lastzoomZ3dR = 0;
        int timetodo = 0;
        int JointAssignments = 0;
        int BeamAssignments = 0;
        int MouseButtonsLeft = 0;
        int ExtrudedShell = 0;
        int ExtrudedFrame = 0;
        int JointShowPower = 0;
        int FrameShowPower = 0;
        int ShiftPress = 0;
        #region//Form1_Load
        private void Form1_Load(object sender, EventArgs e)
        {
           Myglobals.ShllAnalysisMeshVeiw = 0;
           Shell.Localaxis = 0;
           Myglobals.MeshType=1;
           FrameElements emp = new FrameElements();
           FrameElement[0]=emp;

            Frame.AnalisesSecNumbers = 22;
            Frame.ShowPower = 0;
            Frame.ShowPowerValue = 0;
            Frame.Assignments = 0;

            Myglobals.DrowRealShape = 0;
            Myglobals.AnyDiagram = 5;
            Myglobals.DiagramValue = 0;
            Myglobals.DrawDiagram = 0;
            Myglobals.IfAnalysis = 0;
            Myglobals.StoryName[0] = "Base";
            Myglobals.Selectedelev = 1;
            Myglobals.ShowGrid = 1;
            Myglobals.Rotate3DVeiw = 0;
            Myglobals.Show3DWindow = 1;
            Myglobals.ShowPlaneWindow = 1;
            Myglobals.ShowEleveWindow = 0;
            Myglobals.ExtrudedFrame = 1;
            Myglobals.ExtrudedShell = 1;
            Myglobals.FillObjects = 1;
            Myglobals.Toggel = 1;
            Myglobals.SelectedPlan = "Plan";
            Snap.Joints = 1;
            Snap.LineEndsandMidpoints = 1;
            Snap.GridIntersections = 1;
            Snap.LinesandFrames = 1;
            Snap.Edges = 1;
            Snap.PerpendicularProjections = 0;
            Snap.Intersections = 1;
            Snap.FineGrid = 0;
            Snap.Extensions = 0;
            Snap.Prallels = 1;
            Snap.IntelligentSnap = 0;
            Snap.ArchLayer = 0;
            Snap.FineGridValue = 0.25;
            Array.ForEach(Directory.GetFiles(@"./TempFiles/"), File.Delete);
            Myglobals.TempFile = 0;
            Myglobals.TempSelectedFile = 0;
            Joint.ShowPower = 0;

            Joint.Assignments = 0;

            comboBox1.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            Myglobals.RotateAngelDraw = 0;
            Section.SelectedToDraw = 1;
            Slab.SelectedToDraw = 1;
            Wall.SelectedToDraw = 1;
            Myglobals.FillObjects = 1;
            LoadSections();
            LoadMaterials();
            LoadSlabs();
            LoadWalls();
            Myglobals.PropertyGridchoice = 1;
            barlist = new List<Bar>();
            barlist1 = new List<Bar1>();
            barlist2 = new List<Bar2>();
            for (int i = 1; i < Section.Number + 1; i++)
            {
                Bar bar = new Bar();
                bar.barvalue = Section.LABEL[i];
                barlist.Add(bar);
                comboBox2.Items.Add(bar);
            }
            Bar1 bar1 = new Bar1();
            bar1.barvalue = "Frame";
            barlist1.Add(bar1);
            comboBox2.Items.Add(bar1);
            Bar2 bar2 = new Bar2();
            bar2.barvalue = "Continuous";
            barlist2.Add(bar2);
            comboBox2.Items.Add(bar2);
            bar2 = new Bar2();
            bar2.barvalue = "Pinned";
            barlist2.Add(bar2);
            comboBox2.Items.Add(bar2);
            Foo foo = new Foo();
            foo.bar = new Bar();
            foo.bar1 = new Bar1();
            foo.bar2 = new Bar2();
            foo.Name = "0";
            foo.bar.barvalue = Section.LABEL[1];
            foo.bar1.barvalue = "Frame";
            foo.bar2.barvalue = "Continuous";
            propertyGrid1.SelectedObject = foo;
            Myglobals.AllStories = 0;
            Myglobals.SelectedStory = 1;
            Myglobals.StoryNumbers = 4;
            Myglobals.StoryName[1] = "ground";
            for (int i = 1; i < Myglobals.StoryNumbers + 1; i++)
            {
                Myglobals.StoryHight[i] = 3;
                Myglobals.StoryName[i] = "Story"+ i;
                Myglobals.StoryLevel[i] = Myglobals.StoryLevel[i - 1] + Myglobals.StoryHight[i];
            }
            Loads.Number = 2;
            Loads.Load[1] = "Dead";
            Loads.Type[1] = "Dead";
            Loads.SelfWeight[1] = 1;
            Loads.AutoLateral[1] = "0";
            Loads.Load[2] = "Live";
            Loads.Type[2] = "Live";
            Loads.SelfWeight[2] = 0;
            Loads.AutoLateral[2] = "0";
            Myglobals.drowclick = 0;
            GridLine.VisibleAs = 1;
            Myglobals.Zoom2d = 15;
            Myglobals.Zoom3d = 15;
            Myglobals.startX2d = 100;
            Myglobals.startY2d = 500;
            Myglobals.startX3d = 300;
            Myglobals.startY3d = 350;
            Myglobals.BitampWidth3d = 700;
            Myglobals.BitampHight3d = 600;
            Myglobals.BitampWidth2d = 700;
            Myglobals.BitampHight2d = 600;

            Myglobals.Zoomelev = 15;
            Myglobals.startXelev = 100;
            Myglobals.startYelev = 500;
            Myglobals.BitampWidthelev = 700;
            Myglobals.BitampHightelev = 600;

            pictureBox1.Controls.Add(pictureBox3);
            pictureBox3.Location = new Point(0, 0);
            pictureBox3.BackColor = Color.Transparent;
            pictureBox2.Controls.Add(pictureBox4);
            pictureBox4.Location = new Point(0, 0);
            pictureBox4.BackColor = Color.Transparent;
            pictureBox5.Controls.Add(pictureBox6);
            pictureBox6.Location = new Point(0, 0);
            pictureBox6.BackColor = Color.Transparent;

            Myglobals.RotatePointX3d = 0;
            Myglobals.RotatePointY3d = 0;
            Myglobals.RotatePointZ3d = 0;
            double tah = Math.PI / 180;
            Myglobals.valDY = 50;
            Myglobals.valDX = 35;
            Myglobals.Tadweer = 1;
            float addedvalue = (float)((8) * Math.Cos(tah * (Myglobals.valDY)) * Math.Sin(tah * (Myglobals.valDY)) * Math.Sin(tah * (Myglobals.valDX))); 
            Myglobals.tXValue = (float)Myglobals.valDY;
            Myglobals.tYValue = -(float)(Myglobals.valDX * Math.Sin(tah * (Myglobals.valDY))) - addedvalue;
            Myglobals.tZValue = (float)(Myglobals.valDX * Math.Cos(tah * (Myglobals.valDY))) - addedvalue; 
            Myglobals.Aperture = 80;
            Myglobals.EyeX = Myglobals.RotatePointX3d - 500 * Math.Sin(tah * (Myglobals.valDX));
            Myglobals.EyeY = Myglobals.RotatePointY3d - 500 * Math.Cos(tah * (Myglobals.valDX));
            Myglobals.EyeZ = Myglobals.RotatePointZ3d + 500 * Math.Cos(tah * (Myglobals.valDY));

            GRIDfrm gridfrm = new GRIDfrm();
            gridfrm.ShowDialog();
            MakeTempFiles();
        }
        #endregion
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey) ShiftPress = 1;
            if (Myglobals.IfAnalysis == 0)
            {
                #region//التراجع
                if (e.Control && e.KeyCode == Keys.Z)
                {
                    if (Myglobals.TempSelectedFile - 1 > 0)
                    {
                        Myglobals.drowclick = 0;
                        Myglobals.LineMove2dVisible = 0;
                        Myglobals.LineMove2dFVisible = 0;
                        Myglobals.LineMove3dVisible = 0;
                        Myglobals.LineMove3dFVisible = 0;
                        Myglobals.LineMoveelevVisible = 0;
                        Myglobals.LineMoveelevFVisible = 0;
                        button14.Visible = false;
                        Myglobals.TempSelectedFile = Myglobals.TempSelectedFile - 1;
                        OpenTemp();
                        UnSellectAll();
                        DROWcls callmee = new DROWcls();
                        callmee.Render2d();
                        callmee.Render3d();
                        callmee.Renderelev();
                    }
                }
                #endregion
                #region//الحذف
                if (e.KeyCode == Keys.Delete)
                {
                    int ifselected = 0;
                    int NB = Frame.Number;
                    int m = 0;
                    int i = 1;
                    int thestart = 1;
                    #region//حذف العناصر الخطية
                startloopB: { }
                    m = m + 1;
                    for (i = thestart; i < NB + 1; i++)
                    {
                        if (FrameElement[i].Selected == 1)
                        {
                            ifselected = 1;
                            thestart = i;
                            int thejoint = FrameElement[i].FirstJoint;
                            for (int k = 1; k < Joint.BeamConnectionN[thejoint] + 1; k++)
                            {
                                if (Joint.Beam[thejoint, k] == i)
                                {
                                    for (int l = k; l < Joint.BeamConnectionN[thejoint]; l++)
                                    {
                                        Joint.Beam[thejoint, l] = Joint.Beam[thejoint, l + 1];
                                    }
                                    goto nextjointB;
                                }
                            }
                        nextjointB: { }
                            Joint.Beam[thejoint, Joint.BeamConnectionN[thejoint]] = 0;
                            Joint.BeamConnectionN[thejoint] = Joint.BeamConnectionN[thejoint] - 1;
                            thejoint = FrameElement[i].SecondJoint;
                            for (int k = 1; k < Joint.BeamConnectionN[thejoint] + 1; k++)
                            {
                                if (Joint.Beam[thejoint, k] == i)
                                {
                                    for (int l = k; l < Joint.BeamConnectionN[thejoint]; l++)
                                    {
                                        Joint.Beam[thejoint, l] = Joint.Beam[thejoint, l + 1];
                                    }
                                    goto nextjointB1;
                                }
                            }
                        nextjointB1: { }
                            Joint.Beam[thejoint, Joint.BeamConnectionN[thejoint]] = 0;
                            Joint.BeamConnectionN[thejoint] = Joint.BeamConnectionN[thejoint] - 1;
                            for (int k = 1; k < Joint.Number3d + 1; k++)
                            {
                                for (int l = 1; l < Joint.BeamConnectionN[k] + 1; l++)
                                {
                                    if (Joint.Beam[k, l] > i) Joint.Beam[k, l] = Joint.Beam[k, l] - 1;
                                }
                            }
                            Frame.Number = Frame.Number - 1;
                            for (int j = i; j < Frame.Number + 1; j++)
                            {
                                FrameElement[j] = FrameElement[j + 1];
                            }
                            FrameElement[Frame.Number + 1] = FrameElement[0];
                            if (m < NB + 1) goto startloopB;
                        }
                    }
                endbaem: { }
                    #endregion
                    #region//حذف البلاطات
                    int NF = Shell.Number;
                    m = 0;
                    i = 1;
                    thestart = 1;
                startloopF: { }
                    m = m + 1;
                    for (i = thestart; i < NF + 1; i++)
                    {
                        for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                        {
                            if (Shell.SelectedLine[i, j] == 1)
                            {
                                ifselected = 1;
                                thestart = i;
                                for (int p = 1; p < Shell.PointNumbers[i] + 1; p++)
                                {
                                    int thejoint = Shell.JointNo[i, p];
                                    for (int k = 1; k < Joint.FloorConnectionN[thejoint] + 1; k++)
                                    {
                                        if (Joint.Floor[thejoint, k] == i)
                                        {
                                            for (int l = k; l < Joint.FloorConnectionN[thejoint]; l++)
                                            {
                                                Joint.Floor[thejoint, l] = Joint.Floor[thejoint, l + 1];
                                            }
                                            goto nextjointF;
                                        }
                                    }
                                nextjointF: { }
                                    Joint.Floor[thejoint, Joint.FloorConnectionN[thejoint]] = 0;
                                    Joint.FloorConnectionN[thejoint] = Joint.FloorConnectionN[thejoint] - 1;
                                }
                                for (int k = 1; k < Joint.Number3d + 1; k++)
                                {
                                    for (int l = 1; l < Joint.FloorConnectionN[k] + 1; l++)
                                    {
                                        if (Joint.Floor[k, l] > i) Joint.Floor[k, l] = Joint.Floor[k, l] - 1;
                                    }
                                }
                                Shell.Number = Shell.Number - 1;
                                for (int k = i; k < Shell.Number + 1; k++)
                                {
                                    Shell.PointNumbers[k] = Shell.PointNumbers[k + 1];
                                    Shell.Type[k] = Shell.Type[k + 1];
                                    Shell.Section[k] = Shell.Section[k + 1];
                                    for (int l = 1; l < Shell.PointNumbers[k] + 1; l++)
                                    {
                                        Shell.SelectedLine[k, l] = Shell.SelectedLine[k + 1, l];
                                        Shell.JointNo[k, l] = Shell.JointNo[k + 1, l];
                                    }
                                }
                                for (int K = 1; K < Shell.PointNumbers[Shell.Number + 1] + 1; K++)
                                {
                                    Shell.SelectedLine[Shell.Number + 1, K] = 0;
                                }
                                if (m < NF + 1) goto startloopF;
                            }
                        }
                    }
                    #endregion
                    #region /حذف العقد
                    if (ifselected == 1)
                    {
                        int NJ = Joint.Number3d;
                        m = 0;
                        i = 1;
                        thestart = 1;
                    startloopJ: { }
                        m = m + 1;
                        for (i = thestart; i < NJ + 1; i++)
                        {
                            if (Joint.BeamConnectionN[i] == 0 & Joint.FloorConnectionN[i] == 0)
                            {
                                thestart = i;
                                Joint.Number3d = Joint.Number3d - 1;
                                for (int j = i; j < Joint.Number3d + 1; j++)
                                {
                                    Joint.BeamConnectionN[j] = Joint.BeamConnectionN[j + 1];
                                    Joint.FloorConnectionN[j] = Joint.FloorConnectionN[j + 1];
                                    Joint.XReal[j] = Joint.XReal[j + 1];
                                    Joint.YReal[j] = Joint.YReal[j + 1];
                                    Joint.ZReal[j] = Joint.ZReal[j + 1];
                                    Joint.FixX[j] = Joint.FixX[j + 1];
                                    Joint.FixY[j] = Joint.FixY[j + 1];
                                    Joint.FixZ[j] = Joint.FixZ[j + 1];
                                    Joint.FixRX[j] = Joint.FixRX[j + 1];
                                    Joint.FixRY[j] = Joint.FixRY[j + 1];
                                    Joint.FixRZ[j] = Joint.FixRZ[j + 1];
                                    Joint.Selected[j] = Joint.Selected[j + 1];
                                    Joint.PowerX[j] = Joint.PowerX[j + 1];
                                    Joint.PowerY[j] = Joint.PowerY[j + 1];
                                    Joint.PowerZ[j] = Joint.PowerZ[j + 1];
                                    Joint.MomentXX[j] = Joint.MomentXX[j + 1];
                                    Joint.MomentYY[j] = Joint.MomentYY[j + 1];
                                    Joint.MomentZZ[j] = Joint.MomentZZ[j + 1];
                                    for (int k = 1; k < Joint.BeamConnectionN[j] + 1; k++)
                                    {
                                        Joint.Beam[j, k] = Joint.Beam[j + 1, k];
                                    }
                                    for (int k = 1; k < Joint.FloorConnectionN[j] + 1; k++)
                                    {
                                        Joint.Floor[j, k] = Joint.Floor[j + 1, k];
                                    }
                                }
                                for (int k = 1; k < Joint.BeamConnectionN[Joint.Number3d + 1] + 1; k++)
                                {
                                    Joint.Beam[Joint.Number3d + 1, k] = 0;
                                }
                                for (int k = 1; k < Joint.FloorConnectionN[Joint.Number3d + 1] + 1; k++)
                                {
                                    Joint.Floor[Joint.Number3d + 1, k] = 0;
                                }
                                Joint.BeamConnectionN[Joint.Number3d + 1] = 1;
                                Joint.FloorConnectionN[Joint.Number3d + 1] = 1;
                                Joint.FixX[Joint.Number3d + 1] = 0;
                                Joint.FixY[Joint.Number3d + 1] = 0;
                                Joint.FixZ[Joint.Number3d + 1] = 0;
                                Joint.FixRX[Joint.Number3d + 1] = 0;
                                Joint.FixRY[Joint.Number3d + 1] = 0;
                                Joint.FixRZ[Joint.Number3d + 1] = 0;
                                Joint.Selected[Joint.Number3d + 1] = 0;
                                Joint.PowerX[Joint.Number3d + 1] = 0;
                                Joint.PowerY[Joint.Number3d + 1] = 0;
                                Joint.PowerZ[Joint.Number3d + 1] = 0;
                                Joint.MomentXX[Joint.Number3d + 1] = 0;
                                Joint.MomentYY[Joint.Number3d + 1] = 0;
                                Joint.MomentZZ[Joint.Number3d + 1] = 0;
                                for (int j = i; j < Joint.Number3d + 2; j++)
                                {
                                    for (int p = 1; p < Frame.Number + 1; p++)
                                    {
                                        if (FrameElement[p].FirstJoint == j)
                                        {
                                            FrameElement[p].FirstJoint = j - 1;
                                        }
                                        if (FrameElement[p].SecondJoint == j)
                                        {
                                            FrameElement[p].SecondJoint = j - 1;
                                        }
                                    }
                                    for (int p = 1; p < Shell.Number + 1; p++)
                                    {
                                        for (int k = 1; k < Shell.PointNumbers[p] + 1; k++)
                                        {
                                            if (Shell.JointNo[p, k] == j) Shell.JointNo[p, k] = j - 1;
                                        }
                                    }
                                }
                                if (m < NJ + 1) goto startloopJ;
                            }
                        }
                        for (i = Joint.Number3d + 1; i < NJ + 1; i++)
                        {
                            Joint.BeamConnectionN[i] = 0;
                            Joint.FloorConnectionN[i] = 0;
                        }
                        Joint.Number2d = Joint.Number3d;
                        DROWcls callmee = new DROWcls();
                        callmee.Render2d();
                        callmee.Render3d();
                        callmee.Renderelev();
                        MakeTempFiles();
                    }
                    #endregion
                }
                #endregion
            }
            #region // النتر
            if (e.KeyCode == Keys.Enter)
            {
                Myglobals.LineMove2dVisible = 0;
                Myglobals.LineMove2dFVisible = 0;
                Myglobals.LineMove3dVisible = 0;
                Myglobals.LineMove3dFVisible = 0;
                Myglobals.LineMoveelevVisible = 0;
                Myglobals.LineMoveelevFVisible = 0;
                Myglobals.drowclick = 0;
                DROWcls callmee1 = new DROWcls();
                callmee1.Render2d();
                callmee1.Render3d();
            }

            #endregion
            #region // الايسكيب
            if (e.KeyCode == Keys.Escape)
            {
                int escapeforfloor = 0;
                Myglobals.AriaSelected = 0;
                Myglobals.AriaSelectedELEV = 0;
                for (int i = 1; i < Frame.Number + 1; i++)
                {
                    FrameElement[i].Selected = 0;
                }
                for (int i = 1; i < Shell.Number + 1; i++)
                {
                    for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                    {
                        Shell.SelectedLine[i, j] = 0;
                    }
                }
                for (int add = 1; add < Joint.Number3d + 1; add++)
                {
                    Joint.Selected[add] = 0;
                }
                Myglobals.LineMove2dVisible = 0;
                Myglobals.LineMove2dFVisible = 0;
                Myglobals.LineMove3dVisible = 0;
                Myglobals.LineMove3dFVisible = 0;
                Myglobals.LineMoveelevVisible = 0;
                Myglobals.LineMoveelevFVisible = 0;
                if (toolStripButton11.Checked == true || toolStripButton3.Checked == true || toolStripButton5.Checked == true || toolStripButton10.Checked == true)
                {
                    toolStripButton1.Checked = true;
                    toolStripButton3.Checked = false;
                    toolStripButton5.Checked = false;
                    toolStripButton8.Checked = false;
                    toolStripButton9.Checked = false;
                    toolStripButton10.Checked = false;
                    toolStripButton11.Checked = false;
                    toolStripButton6.Checked = false;
                    Myglobals.LineMove2dVisible = 0;
                    Myglobals.LineMove2dFVisible = 0;
                    Myglobals.LineMove3dVisible = 0;
                    Myglobals.LineMove3dFVisible = 0;
                    Myglobals.LineMoveelevVisible = 0;
                    Myglobals.LineMoveelevFVisible = 0;
                    Myglobals.drowclick = 0;
                    propertyGrid1.Visible = false;
                    button14.Visible = false;
                }
                #region //إنهاء رسم البلاطة بالنقاط
                if (toolStripButton8.Checked == true)
                {
                    toolStripButton8.Checked = false;
                    toolStripButton1.Checked = true;
                    propertyGrid1.Visible = false;
                    button14.Visible = false;
                    if (Shell.PointNumbersTemp > 2)
                    {
                        Shell.Number = Shell.Number + 1;
                        Shell.PointNumbers[Shell.Number] = Shell.PointNumbersTemp;
                        int tah = 1;
                        for (int i = 1; i < Shell.PointNumbers[Shell.Number] + 1; i++)
                        {
                            double theX = Shell.PointXTemp[i];
                            double theY = Shell.PointYTemp[i];
                            double theZ = Shell.PointZTemp[i];
                            int tah1 = 0;
                            int selectedjoint = 0;
                            for (int J = 1; J < Joint.Number2d + 1; J++)
                            {
                                if (Joint.XReal[J] == theX & Joint.YReal[J] == theY & Joint.ZReal[J] == theZ)
                                {
                                    tah1 = 1;
                                    selectedjoint = J;
                                    break;
                                }
                            }
                            if (tah1 == 0)
                            {
                                Joint.Number2d = Joint.Number2d + 1;
                                selectedjoint = Joint.Number2d;
                                Joint.XReal[Joint.Number2d] = theX;
                                Joint.YReal[Joint.Number2d] = theY;
                                Joint.ZReal[Joint.Number2d] = theZ;
                            }
                            if (i < Shell.PointNumbers[Shell.Number])
                            {
                                if (Shell.PointZTemp[i] != Shell.PointZTemp[i + 1]) tah = 2;
                            }
                            Shell.JointNo[Shell.Number, i] = selectedjoint;
                            Shell.SelectedLine[Shell.Number, i] = 0;
                            Joint.FloorConnectionN[selectedjoint] = Joint.FloorConnectionN[selectedjoint] + 1;
                            int thecount = Joint.FloorConnectionN[selectedjoint];
                            Joint.Floor[selectedjoint, thecount] = Shell.Number;
                        }
                        Shell.Type[Shell.Number] = tah;
                        Shell.Section[Shell.Number] = Slab.SelectedToDraw;
                        int ShellType = tah;
                        DROWcls callmee = new DROWcls();
                        callmee.SelectedShell = Shell.Number;
                        callmee.CalculatePropertiesShell();
                        callmee.CalculateShellMeshLines();
                        callmee.CalculateMeshJoints();
                        #region//رسم في كل الطوابق
                        if (Myglobals.AllStories == 1)
                        {
                            for (int add = 0; add < Myglobals.StoryNumbers + 1; add++)
                            {
                                if (ShellType == 1 & add == 0) goto nextadd;//بلاطة لاترسمه في الارضي
                                for (int i = 1; i < Shell.PointNumbers[Shell.Number] + 1; i++)
                                {
                                    double theZ0 = Shell.PointZTemp[i];
                                    double delataZ = theZ0 - Myglobals.StoryLevel[SelectedStorytodrowallstory];
                                    if (add == Myglobals.StoryNumbers & Myglobals.StoryLevel[add] + delataZ > Myglobals.StoryLevel[Myglobals.StoryNumbers]) goto nextadd;
                                    if (add == 0 & Myglobals.StoryLevel[add] + delataZ < 0) goto nextadd;
                                }
                                if (add != SelectedStorytodrowallstory)
                                {
                                    Shell.Number = Shell.Number + 1;
                                    Shell.PointNumbers[Shell.Number] = Shell.PointNumbersTemp;
                                    tah = 1;
                                    for (int i = 1; i < Shell.PointNumbers[Shell.Number] + 1; i++)
                                    {
                                        double theX = Shell.PointXTemp[i];
                                        double theY = Shell.PointYTemp[i];
                                        double theZ0 = Shell.PointZTemp[i];
                                        double delataZ = theZ0 - Myglobals.StoryLevel[SelectedStorytodrowallstory];
                                        double theZ = Myglobals.StoryLevel[add] + delataZ;
                                        int tah1 = 0;
                                        int selectedjoint = 0;
                                        for (int J = 1; J < Joint.Number2d + 1; J++)
                                        {
                                            if (Joint.XReal[J] == theX & Joint.YReal[J] == theY & Joint.ZReal[J] == theZ)
                                            {
                                                tah1 = 1;
                                                selectedjoint = J;
                                                break;
                                            }
                                        }
                                        if (tah1 == 0)
                                        {
                                            Joint.Number2d = Joint.Number2d + 1;
                                            selectedjoint = Joint.Number2d;
                                            Joint.XReal[Joint.Number2d] = theX;
                                            Joint.YReal[Joint.Number2d] = theY;
                                            Joint.ZReal[Joint.Number2d] = theZ;
                                        }
                                        if (i < Shell.PointNumbers[Shell.Number])
                                        {
                                            if (Shell.PointZTemp[i] != Shell.PointZTemp[i + 1]) tah = 2;
                                        }
                                        Shell.JointNo[Shell.Number, i] = selectedjoint;
                                        Shell.SelectedLine[Shell.Number, i] = 0;
                                        Joint.FloorConnectionN[selectedjoint] = Joint.FloorConnectionN[selectedjoint] + 1;
                                        int thecount = Joint.FloorConnectionN[selectedjoint];
                                        Joint.Floor[selectedjoint, thecount] = Shell.Number;
                                    }
                                    Shell.Type[Shell.Number] = tah;
                                    Shell.Section[Shell.Number] = Slab.SelectedToDraw;
                                    callmee.SelectedShell = Shell.Number;
                                    callmee.CalculatePropertiesShell();
                                    callmee.CalculateShellMeshLines();
                                    callmee.CalculateMeshJoints();
                                }
                            nextadd: { };
                            }
                        }
                        #endregion
                        escapeforfloor = 1;
                        callmee.Render2d();
                        callmee.Render3d();
                        callmee.Renderelev();
                        MakeTempFiles();
                    }
                    Shell.PointNumbersTemp = 0;
                    Shell.LineNumberTemp = 0;
                    Myglobals.drowclick = 0;
                }
                #endregion
                if (escapeforfloor == 0)
                {
                    DROWcls callmee1 = new DROWcls();
                    callmee1.Render2d();
                    callmee1.Render3d();
                    callmee1.Renderelev();
                }
                Bitmap finalBmp = new Bitmap(Myglobals.BitampWidth2d, Myglobals.BitampHight2d);
                if (pictureBox3.Image != null)
                {
                    pictureBox3.Image.Dispose();
                    pictureBox3.Image = null;
                }
                pictureBox3.Image = finalBmp;
                finalBmp = new Bitmap(Myglobals.BitampWidth3d, Myglobals.BitampHight3d);
                if (pictureBox4.Image != null)
                {
                    pictureBox4.Image.Dispose();
                    pictureBox4.Image = null;
                }
                pictureBox4.Image = finalBmp;
            }
            #endregion
        }
        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey ) ShiftPress = 0;
        }
        #region//pictureBox2
        private void pictureBox2_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (tabPage1.BorderStyle == BorderStyle.FixedSingle)
                {
                    Myglobals.Zoom2d = Myglobals.Zoom2d + 2;
                    Myglobals.startX2d = lastzoomX2d - (int)(lastzoomX2dR * Myglobals.Zoom2d);
                    Myglobals.startY2d = lastzoomY2d + (int)(lastzoomY2dR * Myglobals.Zoom2d);
                    TempX = lastzoomX2d;
                    TempY = lastzoomY2d;
                    DROWcls callmee = new DROWcls();
                    callmee.Render2d();
                    pictureBox3Draw();
                }
                if (tabPage2.BorderStyle == BorderStyle.FixedSingle)
                {
                    lastzoomX3dR = (double)(lastzoomX3d - Myglobals.startX3d) / Myglobals.Zoom3d;
                    lastzoomY3dR = (double)(lastzoomY3d - Myglobals.startY3d) / Myglobals.Zoom3d;
                    Myglobals.Zoom3d = Myglobals.Zoom3d + 4;
                    if (Myglobals.Zoom3d > 60) Myglobals.Zoom3d = Myglobals.Zoom3d + 50;
                    Myglobals.startX3d = (int)(lastzoomX3d - lastzoomX3dR * Myglobals.Zoom3d);
                    Myglobals.startY3d = (int)(lastzoomY3d - lastzoomY3dR * Myglobals.Zoom3d);
                    TempX = lastzoomX3d;
                    TempY = lastzoomY3d;
                    DROWcls callmee = new DROWcls();
                    callmee.Render3d();
                    pictureBox4Draw();
                }
                if (tabPage3.BorderStyle == BorderStyle.FixedSingle)
                {
                    Myglobals.Zoomelev = Myglobals.Zoomelev + 2;
                    Myglobals.startXelev = lastzoomXelev - (int)(lastzoomXelevR * Myglobals.Zoomelev);
                    Myglobals.startYelev = lastzoomYelev + (int)(lastzoomYelevR * Myglobals.Zoomelev);
                    TempX = lastzoomXelev;
                    TempY = lastzoomYelev;
                    DROWcls callmee = new DROWcls();
                    callmee.Renderelev();
                    pictureBox6Draw();
                }
            }
            else
            {
                if (tabPage1.BorderStyle == BorderStyle.FixedSingle)
                {
                    if (Myglobals.Zoom2d - 2 > 0)
                    {
                        Myglobals.Zoom2d = Myglobals.Zoom2d - 2;
                        Myglobals.startX2d = lastzoomX2d - (int)(lastzoomX2dR * Myglobals.Zoom2d);
                        Myglobals.startY2d = lastzoomY2d + (int)(lastzoomY2dR * Myglobals.Zoom2d);
                        TempX = lastzoomX2d;
                        TempY = lastzoomY2d;
                        DROWcls callmee = new DROWcls();
                        callmee.Render2d();
                        pictureBox3Draw();
                    }
                }
                if (tabPage2.BorderStyle == BorderStyle.FixedSingle)
                {
                    if (Myglobals.Zoom3d - 4 > 0)
                    {
                        lastzoomX3dR = (double)(lastzoomX3d - Myglobals.startX3d) / Myglobals.Zoom3d;
                        lastzoomY3dR = (double)(lastzoomY3d - Myglobals.startY3d) / Myglobals.Zoom3d;
                        Myglobals.Zoom3d = Myglobals.Zoom3d - 4;
                        if (Myglobals.Zoom3d > 60) Myglobals.Zoom3d = Myglobals.Zoom3d - 50;
                        Myglobals.startX3d = (int)(lastzoomX3d - lastzoomX3dR * Myglobals.Zoom3d);
                        Myglobals.startY3d = (int)(lastzoomY3d - lastzoomY3dR * Myglobals.Zoom3d);
                        TempX = lastzoomX3d;
                        TempY = lastzoomY3d;
                        DROWcls callmee = new DROWcls();
                        callmee.Render3d();
                        pictureBox4Draw();
                    }
                }
                if (tabPage3.BorderStyle == BorderStyle.FixedSingle)
                {
                    if (Myglobals.Zoomelev - 2 > 0)
                    {
                        Myglobals.Zoomelev = Myglobals.Zoomelev - 2;
                        Myglobals.startXelev = lastzoomXelev - (int)(lastzoomXelevR * Myglobals.Zoomelev);
                        Myglobals.startYelev = lastzoomYelev + (int)(lastzoomYelevR * Myglobals.Zoomelev);
                        TempX = lastzoomXelev;
                        TempY = lastzoomYelev;
                        DROWcls callmee = new DROWcls();
                        callmee.Renderelev();
                        pictureBox6Draw();
                    }
                }
            }
        }
        #endregion
        #region//pictureBox3
        int ClickedOn;
        private void ClickOnElement(int X1, int Y1, int X2, int Y2, int X, int Y)
        {
            double distance = 100000;
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
        ClickedOn = 0;
            if (distance < 0.1 * Myglobals.Zoom2d)
            {
                ClickedOn = 1;
            }
        }
        
        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            tabPage1.BorderStyle = BorderStyle.FixedSingle;
            tabPage2.BorderStyle = BorderStyle.None;
            tabPage3.BorderStyle = BorderStyle.None;
            Myglobals.SelectedPlan = "Plan";
            Myglobals.xmove = Cursor.Position.X;
            Myglobals.ymove = Cursor.Position.Y;
            Myglobals.xmove1 = e.X;
            Myglobals.ymove1 = e.Y;
            int ifselected = 0;
            if (e.Button == System.Windows.Forms.MouseButtons.Left || e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                DROWcls callmee = new DROWcls();
                #region//النقر من أجل الريبليكات
                if (Myglobals.PickPoints == 2)//////الدوران
                {
                    for (int i = 0; i < Application.OpenForms.Count; i++)
                    {
                        Form n = Application.OpenForms[i];
                        if (n.Name == "ReplicateForm" || n.Name == "ExtrudeForm")
                        {
                            n.BringToFront();
                            foreach (Control control in n.Controls)
                            {
                                if (control.Name == "tabControl1")
                                {
                                    foreach (Control control1 in control.Controls)
                                    {
                                        if (control1.Name == "tabPage2")
                                        {
                                            foreach (Control control2 in control1.Controls)
                                            {
                                                if (control2.Name == "groupBox1")
                                                {
                                                    foreach (Control control3 in control2.Controls)
                                                    {
                                                        if (control3.Name == "textBox4") control3.Text = TempX12Real.ToString();
                                                        if (control3.Name == "textBox5") control3.Text = TempY12Real.ToString();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (Myglobals.PickPoints == 1)//المراي
                {
                    if (Myglobals.PickNumber == 2)
                    {
                        for (int i = 0; i < Application.OpenForms.Count; i++)
                        {
                            Form n = Application.OpenForms[i];
                            if (n.Name == "ReplicateForm")
                            {
                                n.BringToFront();
                                foreach (Control control in n.Controls)
                                {
                                    if (control.Name == "tabControl1")
                                    {
                                        foreach (Control control1 in control.Controls)
                                        {
                                            if (control1.Name == "tabPage3")
                                            {
                                                foreach (Control control2 in control1.Controls)
                                                {
                                                    if (control2.Name == "textBox10") control2.Text = TempX12Real.ToString();
                                                    if (control2.Name == "textBox11") control2.Text = TempY12Real.ToString();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        Myglobals.PickNumber = 0;
                    }
                    if (Myglobals.PickNumber == 1)
                    {
                        for (int i = 0; i < Application.OpenForms.Count; i++)
                        {
                            Form n = Application.OpenForms[i];
                            if (n.Name == "ReplicateForm")
                            {
                                n.BringToFront();
                                foreach (Control control in n.Controls)
                                {
                                    if (control.Name == "tabControl1")
                                    {
                                        foreach (Control control1 in control.Controls)
                                        {
                                            if (control1.Name == "tabPage3")
                                            {
                                                foreach (Control control2 in control1.Controls)
                                                {
                                                    if (control2.Name == "textBox8") control2.Text = TempX12Real.ToString();
                                                    if (control2.Name == "textBox9") control2.Text = TempY12Real.ToString();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        Myglobals.PickNumber = 2;
                    }
                }
                if (Myglobals.PickPoints == 3)//التباعد
                {
                    if (Myglobals.PickNumber == 2)
                    {
                        for (int i = 0; i < Application.OpenForms.Count; i++)
                        {
                            Form n = Application.OpenForms[i];
                            if (n.Name == "ReplicateForm" || n.Name == "ExtrudeForm")
                            {
                                n.BringToFront();
                                foreach (Control control in n.Controls)
                                {
                                    if (control.Name == "tabControl1")
                                    {
                                        foreach (Control control1 in control.Controls)
                                        {
                                            if (control1.Name == "tabPage1")
                                            {
                                                foreach (Control control2 in control1.Controls)
                                                {
                                                    if (control2.Name == "textBox1") control2.Text = (TempX12Real - Myglobals.PickX).ToString();
                                                    if (control2.Name == "textBox2") control2.Text = (TempY12Real - Myglobals.PickY).ToString();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        Myglobals.PickNumber = 0;
                    }
                    if (Myglobals.PickNumber == 1)
                    {
                        Myglobals.PickX = TempX12Real;
                        Myglobals.PickY = TempY12Real;
                        Myglobals.PickNumber = 2;
                    }
                }
                #endregion
                if (toolStripButton1.Checked == true)//تحديد العناصر
                {
                    #region//تحديد العقد
                    for (int i = 1; i < Joint.Number3d + 1; i++)
                    {
                        Joint.ID[i]=i;
                    }
                    int[] b = Joint.ID.Where(x => Joint.ZReal[x] == (Myglobals.StoryLevel[Myglobals.SelectedStory]) & Math.Abs(e.X - Joint.X2d[x]) < 4 & Math.Abs(e.Y - Joint.Y2d[x]) < 4).ToArray();
                    if (b.Length > 0)
                    {
                        int ij = b[0];
                        ifselected = 1;
                        if (Joint.Selected[ij] == 1)
                        {
                            Joint.Selected[ij] = 0;
                        }
                        else
                        {
                            Joint.Selected[ij] = 1;
                        }
                        if (e.Button == System.Windows.Forms.MouseButtons.Right)
                        {
                            Joint.Selected[ij] = 1;
                            Joint.SelectedforProp = ij;
                            ifselected = 0;
                            callmee.Render2d();
                            callmee.Render3d();
                            callmee.Renderelev();
                            JointInformationForm informationForm = new JointInformationForm();
                            informationForm.ShowDialog();
                        }
                        goto endloop1;
                    }
                    #endregion
                    # region//تحديد العناصر الخطية
                    for (int i = 1; i < Frame.Number + 1; i++)
                    {
                        Frame.ID[i] = i;
                    }
                    double zed = Myglobals.StoryLevel[Myglobals.SelectedStory];
                    int X = e.X;
                    int Y = e.Y;
                 //   int[] b1 = Frame.ID.Where(x => Joint.ZReal[FrameElement[x].FirstJoint] == zed
                 //       & Joint.ZReal[FrameElement[x].SecondJoint] == zed & FrameElement[x].Visible == 0).ToArray();

                    int[] b1 = Frame.ID.Where(x =>
                    (
                    ((Joint.X2d[FrameElement[x].FirstJoint] == Joint.X2d[FrameElement[x].SecondJoint] & Math.Abs(Joint.X2d[FrameElement[x].FirstJoint] - X) < 0.15 * Myglobals.Zoom2d)
                    &
                    (
                       (Joint.Y2d[FrameElement[x].FirstJoint] >= Y & Joint.Y2d[FrameElement[x].SecondJoint] <= Y)
                    || (Joint.Y2d[FrameElement[x].FirstJoint] <= Y & Joint.Y2d[FrameElement[x].SecondJoint] >= Y)))
                    
                    || ((Joint.Y2d[FrameElement[x].FirstJoint] == Joint.Y2d[FrameElement[x].SecondJoint] & Math.Abs(Joint.Y2d[FrameElement[x].FirstJoint] - Y) < 0.15 * Myglobals.Zoom2d)
                    &
                    (
                       (Joint.X2d[FrameElement[x].FirstJoint] >= X & Joint.X2d[FrameElement[x].SecondJoint] <= X)
                    || (Joint.X2d[FrameElement[x].FirstJoint] <= X & Joint.X2d[FrameElement[x].SecondJoint] >= X)))
                    
                    || (Joint.Y2d[FrameElement[x].FirstJoint] <= Y & Joint.Y2d[FrameElement[x].SecondJoint] >= Y
                    & Joint.X2d[FrameElement[x].FirstJoint] <= X & Joint.X2d[FrameElement[x].SecondJoint] >= X)
                   
                    || (Joint.Y2d[FrameElement[x].FirstJoint] <= Y & Joint.Y2d[FrameElement[x].SecondJoint] >= Y
                    & Joint.X2d[FrameElement[x].FirstJoint] >= X & Joint.X2d[FrameElement[x].SecondJoint] <= X)
                   
                    || (Joint.Y2d[FrameElement[x].FirstJoint] >= Y & Joint.Y2d[FrameElement[x].SecondJoint] <= Y
                    & Joint.X2d[FrameElement[x].FirstJoint] <= X & Joint.X2d[FrameElement[x].SecondJoint] >= X)
                   
                    || (Joint.Y2d[FrameElement[x].FirstJoint] >= Y & Joint.Y2d[FrameElement[x].SecondJoint] <= Y
                    & Joint.X2d[FrameElement[x].FirstJoint] >= X & Joint.X2d[FrameElement[x].SecondJoint] <= X)
                    )
                    & Joint.ZReal[FrameElement[x].FirstJoint] == zed
                    & Joint.ZReal[FrameElement[x].SecondJoint] == zed
                    & FrameElement[x].Visible == 0).ToArray();


                    for (int j = 0; j < b1.Length; j++)
                    {
                        int i = b1[j];
                        //جائز
                        int X1 = Joint.X2d[FrameElement[i].FirstJoint];
                        int Y1 = Joint.Y2d[FrameElement[i].FirstJoint];
                        int X2 = Joint.X2d[FrameElement[i].SecondJoint];
                        int Y2 = Joint.Y2d[FrameElement[i].SecondJoint];
                        X = e.X;
                        Y = e.Y;
                        ClickOnElement(X1, Y1, X2, Y2, X, Y);
                        if (ClickedOn == 1)
                        {
                            ifselected = 1;
                            if (FrameElement[i].Selected == 1)
                            {
                                FrameElement[i].Selected = 0;
                            }
                            else
                            {
                                FrameElement[i].Selected = 1;
                            }
                            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                            {
                                FrameElement[i].Selected = 1;
                                Frame.SelectedforProp = i;
                                ifselected = 0;
                                callmee.Render2d();
                                callmee.Render3d();
                                callmee.Renderelev();
                                BeamInformationForm beamInformationForm = new BeamInformationForm();
                                beamInformationForm.ShowDialog();
                            }
                            goto endloop1;
                        }
                    }
                    #endregion
                    # region//تحديد البلاطات بخطوطها
                    for (int i = 1; i < Shell.Number + 1; i++)
                    {
                        if (Shell.Visible[i] == 0)
                        {
                            if (Shell.Type[i] == 1 & Joint.ZReal[Shell.JointNo[i, 1]] == Myglobals.StoryLevel[Myglobals.SelectedStory])
                            {
                                for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                                {
                                    int X1 = Joint.X2d[Shell.JointNo[i, j]];
                                    int Y1 = Joint.Y2d[Shell.JointNo[i, j]];
                                    int X2 = 0;
                                    int Y2 = 0;
                                    if (j != Shell.PointNumbers[i])
                                    {
                                        X2 = Joint.X2d[Shell.JointNo[i, j + 1]];
                                        Y2 = Joint.Y2d[Shell.JointNo[i, j + 1]];
                                    }
                                    else
                                    {
                                        X2 = Joint.X2d[Shell.JointNo[i, 1]];
                                        Y2 = Joint.Y2d[Shell.JointNo[i, 1]];
                                    }
                                   // X = e.Location.X;
                                  //  Y = e.Location.Y;
                                    ClickOnElement(X1, Y1, X2, Y2, X, Y);
                                    if (ClickedOn == 1)
                                    {
                                        ifselected = 1;
                                        if (Shell.SelectedLine[i, j] == 1)
                                        {
                                            Shell.SelectedLine[i, j] = 0;
                                        }
                                        else
                                        {
                                            Shell.SelectedLine[i, j] = 1;
                                        }
                                        if (e.Button == System.Windows.Forms.MouseButtons.Right)
                                        {
                                            Shell.SelectedLine[i, j] = 1;
                                            Shell.SelectedforProp = i;
                                            ifselected = 0;
                                            callmee.Render2d();
                                            callmee.Render3d();
                                            callmee.Renderelev();
                                            SlabInformationForm slabInformationForm = new SlabInformationForm();
                                            slabInformationForm.ShowDialog();
                                        }
                                        goto endloop1;
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    # region//تحديد البلاطات بالنقر ضمنها
                    int Xtest = e.X;
                    int Ytest = e.Y;
                    for (int i = 1; i < Shell.Number + 1; i++)
                    {
                        if (Shell.Visible[i] == 0)
                        {
                            if (Shell.Type[i] == 1 & Joint.ZReal[Shell.JointNo[i, 1]] == Myglobals.StoryLevel[Myglobals.SelectedStory])
                            {

                                int N = Shell.PointNumbers[i] + 1;
                                Point[] polygon = new Point[N + 1];
                                for (int j = 1; j < N; j++)
                                {
                                    polygon[j].X = Joint.X2d[Shell.JointNo[i, j]];
                                    polygon[j].Y = Joint.Y2d[Shell.JointNo[i, j]];
                                }
                                polygon[N].X = polygon[1].X;
                                polygon[N].Y = polygon[1].Y;
                                bool result = false;
                                int nvert = N;
                                int k, l;
                                for (k = 1, l = nvert - 1; k < nvert; l = k++)
                                {
                                    if (((polygon[k].Y > Ytest) != (polygon[l].Y > Ytest)) &&
                                     (Xtest < (polygon[l].X - polygon[k].X) * (Ytest - polygon[k].Y) / (polygon[l].Y - polygon[k].Y) + polygon[k].X))
                                        result = !result;
                                }
                                if (result == true)
                                {
                                    ifselected = 1;
                                    int tah = 0;
                                    for (int j = 1; j < N; j++)
                                    {
                                        if (Shell.SelectedLine[i, j] == 1)
                                        {
                                            tah = 1;
                                            break;
                                        }
                                    }
                                    for (int j = 1; j < N; j++)
                                    {
                                        if (tah == 1)
                                        {
                                            Shell.SelectedLine[i, j] = 0;
                                        }
                                        else
                                        {
                                            Shell.SelectedLine[i, j] = 1;
                                        }
                                    }
                                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                                    {
                                        for (int j = 1; j < N; j++)
                                        {
                                            Shell.SelectedLine[i, j] = 1;
                                        }
                                        Shell.SelectedforProp = i;
                                        ifselected = 0;
                                        callmee.Render2d();
                                        callmee.Render3d();
                                        callmee.Renderelev();
                                        SlabInformationForm slabInformationForm = new SlabInformationForm();
                                        slabInformationForm.ShowDialog();
                                    }
                                    goto endloop1;
                                }
                            }
                        }
                    }
                    #endregion
                endloop1: { };
                    if (ifselected == 1)
                    {
                        callmee.Render2d();
                        callmee.Render3d();
                        callmee.Renderelev();
                    }
                }
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    #region رسم جدار
                    if (toolStripButton11.Checked == true & Myglobals.SelectedStory > 0)
                    {
                        if (Myglobals.drowclick == 0)
                        {
                            int x1 = e.X;
                            int y1 = e.Y;
                            TempXReal[1] = Math.Round((x1 - Myglobals.startX2d) / (Myglobals.Zoom2d), 3);
                            TempYReal[1] = Math.Round((Myglobals.startY2d - y1) / (Myglobals.Zoom2d), 3);
                            TempZReal[1] = Math.Round(Myglobals.StoryLevel[Myglobals.SelectedStory], 3);
                            if (Tahkik == 1)
                            {
                                TahkikXY[1] = 1;
                                x1 = TempX;
                                y1 = TempY;
                                TempXReal[1] = TempX12Real;
                                TempYReal[1] = TempY12Real;
                            }
                            double tx1 = TempXReal[1];
                            double ty1 = -1 * TempYReal[1];
                            double tz1 = -1 * TempZReal[1];
                            Math3DP.PointG Point3dM = new Math3DP.PointG(tx1, ty1, tz1);
                            Point3dM.RotateX = Myglobals.tXValue;
                            Point3dM.RotateY = Myglobals.tYValue;
                            Point3dM.RotateZ = Myglobals.tZValue;
                            Point3dM.DrawPoint();
                            LineMoveX1 = x1;
                            LineMoveY1 = y1;
                            LineMoveX2 = LineMoveX1;
                            LineMoveY2 = LineMoveY1;
                            Myglobals.LineMove2dVisible = 1;
                            Myglobals.LineMove3dVisible = 1;
                            Myglobals.LineMoveelevVisible = 1;
                            Myglobals.drowclick = 1;
                            goto sdd;
                        }
                        if (Myglobals.drowclick == 1)
                        {
                            TempZReal[2] = Myglobals.StoryLevel[Myglobals.SelectedStory];
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

                            Shell.Number = Shell.Number + 1;
                            Shell.PointNumbers[Shell.Number] = 4;
                            Shell.Type[Shell.Number] = 2;
                            double theX = TempXReal[1];
                            double theY = TempYReal[1];
                            double theZ = Myglobals.StoryLevel[Myglobals.SelectedStory];
                            int tah = 0;
                            int selectedjoint = 0;
                            for (int i = 1; i < Joint.Number2d + 1; i++)
                            {
                                if (Joint.XReal[i] == theX & Joint.YReal[i] == theY & Joint.ZReal[i] == theZ)
                                {
                                    tah = 1;
                                    selectedjoint = i;
                                    break;
                                }
                            }
                            if (tah == 0)
                            {
                                Joint.Number2d = Joint.Number2d + 1;
                                selectedjoint = Joint.Number2d;
                                Joint.XReal[Joint.Number2d] = theX;
                                Joint.YReal[Joint.Number2d] = theY;
                                Joint.ZReal[Joint.Number2d] = theZ;
                            }
                            Shell.JointNo[Shell.Number, 1] = selectedjoint;
                            Shell.SelectedLine[Shell.Number, 1] = 0;
                            Joint.FloorConnectionN[selectedjoint] = Joint.FloorConnectionN[selectedjoint] + 1;
                            int thecount = Joint.FloorConnectionN[selectedjoint];
                            Joint.Floor[selectedjoint, thecount] = Shell.Number;

                            theX = TempXReal[1];
                            theY = TempYReal[1];
                            theZ = Math.Round(Myglobals.StoryLevel[Myglobals.SelectedStory - 1], 3);
                            tah = 0;
                            selectedjoint = 0;
                            for (int i = 1; i < Joint.Number2d + 1; i++)
                            {
                                if (Joint.XReal[i] == theX & Joint.YReal[i] == theY & Joint.ZReal[i] == theZ)
                                {
                                    tah = 1;
                                    selectedjoint = i;
                                    break;
                                }
                            }
                            if (tah == 0)
                            {
                                Joint.Number2d = Joint.Number2d + 1;
                                selectedjoint = Joint.Number2d;
                                Joint.XReal[Joint.Number2d] = theX;
                                Joint.YReal[Joint.Number2d] = theY;
                                Joint.ZReal[Joint.Number2d] = theZ;
                            }
                            Shell.JointNo[Shell.Number, 4] = selectedjoint;
                            Shell.SelectedLine[Shell.Number, 4] = 0;
                            Joint.FloorConnectionN[selectedjoint] = Joint.FloorConnectionN[selectedjoint] + 1;
                            thecount = Joint.FloorConnectionN[selectedjoint];
                            Joint.Floor[selectedjoint, thecount] = Shell.Number;

                            theX = Math.Round((x2 - Myglobals.startX2d) / (Myglobals.Zoom2d), 3);
                            theY = Math.Round((Myglobals.startY2d - y2) / (Myglobals.Zoom2d), 3);
                            theZ = TempZReal[2];
                            if (TahkikXY[2] == 1)
                            {
                                theX = TempXReal[2];
                                theY = TempYReal[2];
                            }
                            tah = 0;
                            selectedjoint = 0;
                            for (int i = 1; i < Joint.Number2d + 1; i++)
                            {
                                if (Joint.XReal[i] == theX & Joint.YReal[i] == theY & Joint.ZReal[i] == theZ)
                                {
                                    tah = 1;
                                    selectedjoint = i;
                                    break;
                                }
                            }
                            if (tah == 0)
                            {
                                Joint.Number2d = Joint.Number2d + 1;
                                selectedjoint = Joint.Number2d;
                                Joint.XReal[Joint.Number2d] = theX;
                                Joint.YReal[Joint.Number2d] = theY;
                                Joint.ZReal[Joint.Number2d] = theZ;
                            }
                            Shell.JointNo[Shell.Number, 2] = selectedjoint;
                            Shell.SelectedLine[Shell.Number, 2] = 0;
                            Joint.FloorConnectionN[selectedjoint] = Joint.FloorConnectionN[selectedjoint] + 1;
                            thecount = Joint.FloorConnectionN[selectedjoint];
                            Joint.Floor[selectedjoint, thecount] = Shell.Number;

                            theX = Math.Round((x2 - Myglobals.startX2d) / (Myglobals.Zoom2d), 3);
                            theY = Math.Round((Myglobals.startY2d - y2) / (Myglobals.Zoom2d), 3);
                            theZ = Math.Round(Myglobals.StoryLevel[Myglobals.SelectedStory - 1], 3);
                            if (TahkikXY[2] == 1)
                            {
                                theX = TempXReal[2];
                                theY = TempYReal[2];
                            }
                            tah = 0;
                            selectedjoint = 0;
                            for (int i = 1; i < Joint.Number2d + 1; i++)
                            {
                                if (Joint.XReal[i] == theX & Joint.YReal[i] == theY & Joint.ZReal[i] == theZ)
                                {
                                    tah = 1;
                                    selectedjoint = i;
                                    break;
                                }
                            }
                            if (tah == 0)
                            {
                                Joint.Number2d = Joint.Number2d + 1;
                                selectedjoint = Joint.Number2d;
                                Joint.XReal[Joint.Number2d] = theX;
                                Joint.YReal[Joint.Number2d] = theY;
                                Joint.ZReal[Joint.Number2d] = theZ;
                            }
                            Shell.JointNo[Shell.Number, 3] = selectedjoint;
                            Shell.SelectedLine[Shell.Number, 3] = 0;
                            Joint.FloorConnectionN[selectedjoint] = Joint.FloorConnectionN[selectedjoint] + 1;
                            thecount = Joint.FloorConnectionN[selectedjoint];
                            Joint.Floor[selectedjoint, thecount] = Shell.Number;
                            Shell.Section[Shell.Number] = Slab.SelectedToDraw;
                            if (TahkikXY[2] == 1)
                            {
                                TahkikXY[1] = 1;
                            }
                            else
                            {
                                TahkikXY[1] = 0;
                            }
                            TahkikXY[2] = 0;
                            TempXReal[1] = theX;
                            TempYReal[1] = theY;
                            TempZReal[1] = theZ;
                            int selectedjoint1 = selectedjoint;
                            callmee.SelectedShell = Shell.Number;
                            callmee.CalculatePropertiesShell();
                            callmee.CalculateShellMeshLines();
                            callmee.CalculateMeshJoints();
                            #region//رسم في كل الطوابق
                            if (Myglobals.AllStories == 1)
                            {
                                Shell.PointNumbersTemp = 4;
                                for (int i = 1; i < 5; i++)
                                {
                                    Shell.PointXTemp[i] = Joint.XReal[Shell.JointNo[Shell.Number, i]];
                                    Shell.PointYTemp[i] = Joint.YReal[Shell.JointNo[Shell.Number, i]];
                                    Shell.PointZTemp[i] = Joint.ZReal[Shell.JointNo[Shell.Number, i]];
                                }
                                for (int add = Myglobals.SelectedStory; add < Myglobals.StoryNumbers; add++)
                                {
                                    Shell.Number = Shell.Number + 1;
                                    Shell.PointNumbers[Shell.Number] = Shell.PointNumbersTemp;
                                    tah = 1;
                                    for (int i = 1; i < Shell.PointNumbers[Shell.Number] + 1; i++)
                                    {
                                        theX = Shell.PointXTemp[i];
                                        theY = Shell.PointYTemp[i];
                                        theZ = Shell.PointZTemp[i] + Myglobals.StoryLevel[add + 1] - Myglobals.StoryLevel[Myglobals.SelectedStory];
                                        int tah1 = 0;
                                        selectedjoint = 0;
                                        for (int J = 1; J < Joint.Number2d + 1; J++)
                                        {
                                            if (Joint.XReal[J] == theX & Joint.YReal[J] == theY & Joint.ZReal[J] == theZ)
                                            {
                                                tah1 = 1;
                                                selectedjoint = J;
                                                break;
                                            }
                                        }
                                        if (tah1 == 0)
                                        {
                                            Joint.Number2d = Joint.Number2d + 1;
                                            selectedjoint = Joint.Number2d;
                                            Joint.XReal[Joint.Number2d] = theX;
                                            Joint.YReal[Joint.Number2d] = theY;
                                            Joint.ZReal[Joint.Number2d] = theZ;
                                        }
                                        if (i < Shell.PointNumbers[Shell.Number])
                                        {
                                            if (Shell.PointZTemp[i] != Shell.PointZTemp[i + 1]) tah = 2;
                                        }
                                        Shell.JointNo[Shell.Number, i] = selectedjoint;
                                        Shell.SelectedLine[Shell.Number, i] = 0;
                                        Joint.FloorConnectionN[selectedjoint] = Joint.FloorConnectionN[selectedjoint] + 1;
                                        thecount = Joint.FloorConnectionN[selectedjoint];
                                        Joint.Floor[selectedjoint, thecount] = Shell.Number;
                                    }
                                    Shell.Type[Shell.Number] = tah;
                                    Shell.Section[Shell.Number] = Slab.SelectedToDraw;
                                    callmee.SelectedShell = Shell.Number;
                                    callmee.CalculatePropertiesShell();
                                    callmee.CalculateShellMeshLines();
                                    callmee.CalculateMeshJoints();
                                }
                                for (int add = Myglobals.SelectedStory; add > 1; add--)
                                {
                                    Shell.Number = Shell.Number + 1;
                                    Shell.PointNumbers[Shell.Number] = Shell.PointNumbersTemp;
                                    tah = 1;
                                    for (int i = 1; i < Shell.PointNumbers[Shell.Number] + 1; i++)
                                    {
                                        theX = Shell.PointXTemp[i];
                                        theY = Shell.PointYTemp[i];
                                        theZ = Shell.PointZTemp[i] + Myglobals.StoryLevel[add - 1] - Myglobals.StoryLevel[Myglobals.SelectedStory];
                                        int tah1 = 0;
                                        selectedjoint = 0;
                                        for (int J = 1; J < Joint.Number2d + 1; J++)
                                        {
                                            if (Joint.XReal[J] == theX & Joint.YReal[J] == theY & Joint.ZReal[J] == theZ)
                                            {
                                                tah1 = 1;
                                                selectedjoint = J;
                                                break;
                                            }
                                        }
                                        if (tah1 == 0)
                                        {
                                            Joint.Number2d = Joint.Number2d + 1;
                                            selectedjoint = Joint.Number2d;
                                            Joint.XReal[Joint.Number2d] = theX;
                                            Joint.YReal[Joint.Number2d] = theY;
                                            Joint.ZReal[Joint.Number2d] = theZ;
                                        }
                                        if (i < Shell.PointNumbers[Shell.Number])
                                        {
                                            if (Shell.PointZTemp[i] != Shell.PointZTemp[i + 1]) tah = 2;
                                        }
                                        Shell.JointNo[Shell.Number, i] = selectedjoint;
                                        Shell.SelectedLine[Shell.Number, i] = 0;
                                        Joint.FloorConnectionN[selectedjoint] = Joint.FloorConnectionN[selectedjoint] + 1;
                                        thecount = Joint.FloorConnectionN[selectedjoint];
                                        Joint.Floor[selectedjoint, thecount] = Shell.Number;
                                    }
                                    Shell.Type[Shell.Number] = tah;
                                    Shell.Section[Shell.Number] = Slab.SelectedToDraw;
                                    callmee.SelectedShell = Shell.Number;
                                    callmee.CalculatePropertiesShell();
                                    callmee.CalculateShellMeshLines();
                                    callmee.CalculateMeshJoints();
                                }
                            }
                            #endregion
                            callmee.Render2d();
                            callmee.Render3d();
                            callmee.Renderelev();
                            MakeTempFiles();
                        }
                    sdd: { }
                    }
                    #endregion
                    #region رسم بلاطة مستطيل
                    if (toolStripButton9.Checked == true)
                    {
                        if (Myglobals.drowclick == 0)
                        {
                            int x1 = e.Location.X;
                            int y1 = e.Location.Y;
                            TempXReal[1] = Math.Round((x1 - Myglobals.startX2d) / (Myglobals.Zoom2d), 3);
                            TempYReal[1] = Math.Round((Myglobals.startY2d - y1) / (Myglobals.Zoom2d), 3);
                            TempZReal[1] = Myglobals.StoryLevel[Myglobals.SelectedStory];
                            if (Tahkik == 1)
                            {
                                TahkikXY[1] = 1;
                                x1 = TempX;
                                y1 = TempY;
                                TempXReal[1] = TempX12Real;
                                TempYReal[1] = TempY12Real;
                            }
                            double tx1 = TempXReal[1];
                            double ty1 = -1 * TempYReal[1];
                            double tz1 = -1 * TempZReal[1];
                            Math3DP.PointG Point3dM = new Math3DP.PointG(tx1, ty1, tz1);
                            Point3dM.RotateX = Myglobals.tXValue;
                            Point3dM.RotateY = Myglobals.tYValue;
                            Point3dM.RotateZ = Myglobals.tZValue;
                            Point3dM.DrawPoint();
                            // SelectShape2d[1].X1 = x1;
                            // SelectShape2d[1].Y1 = y1;
                            LineMoveX1 = x1;
                            LineMoveY1 = y1;
                            LineMoveX2 = LineMoveX1;
                            LineMoveY2 = LineMoveY1;
                            Myglobals.LineMove2dVisible = 1;
                            Shell.PointNumbersTemp = Shell.PointNumbersTemp + 1;
                            Shell.PointXTemp[Shell.PointNumbersTemp] = TempXReal[1];
                            Shell.PointYTemp[Shell.PointNumbersTemp] = TempYReal[1];
                            Shell.PointZTemp[Shell.PointNumbersTemp] = TempZReal[1];
                            Myglobals.drowclick = 1;
                        }
                    }
                    #endregion
                    #region رسم بلاطة سريع بنقرة
                    
                    if (toolStripButton10.Checked == true)
                    {
                        if (Myglobals.AriaSelected != 0)
                        {
                            if (Myglobals.AllStories == 0)
                            {
                                Shell.Number = Shell.Number + 1;
                                Shell.PointNumbers[Shell.Number] = Myglobals.AriaPointNo[Myglobals.AriaSelected] - 1;
                                for (int i = 1; i < Shell.PointNumbers[Shell.Number] + 1; i++)
                                {
                                    double theX = Myglobals.p1X[Myglobals.AriaSelected, i];
                                    double theY = Myglobals.p1Y[Myglobals.AriaSelected, i];
                                    double theZ = Myglobals.StoryLevel[Myglobals.SelectedStory];
                                    int tah1 = 0;
                                    int selectedjoint = 0;
                                    for (int J = 1; J < Joint.Number2d + 1; J++)
                                    {
                                        if (Joint.XReal[J] == theX & Joint.YReal[J] == theY & Joint.ZReal[J] == theZ)
                                        {
                                            tah1 = 1;
                                            selectedjoint = J;
                                            break;
                                        }
                                    }
                                    if (tah1 == 0)
                                    {
                                        Joint.Number2d = Joint.Number2d + 1;
                                        selectedjoint = Joint.Number2d;
                                        Joint.XReal[Joint.Number2d] = theX;
                                        Joint.YReal[Joint.Number2d] = theY;
                                        Joint.ZReal[Joint.Number2d] = theZ;
                                    }
                                    Shell.JointNo[Shell.Number, i] = selectedjoint;
                                    Shell.SelectedLine[Shell.Number, i] = 0;
                                    Joint.FloorConnectionN[selectedjoint] = Joint.FloorConnectionN[selectedjoint] + 1;
                                    int thecount = Joint.FloorConnectionN[selectedjoint];
                                    Joint.Floor[selectedjoint, thecount] = Shell.Number;
                                }
                                Shell.Type[Shell.Number] = 1;
                                Shell.Section[Shell.Number] = Slab.SelectedToDraw;
                                callmee.SelectedShell = Shell.Number;
                                callmee.CalculatePropertiesShell();
                                callmee.CalculateShellMeshLines();
                                callmee.CalculateMeshJoints();
                            }
                            #region//رسم في كل الطوابق
                            if (Myglobals.AllStories == 1)
                            {
                                for (int add = 1; add < Myglobals.StoryNumbers + 1; add++)
                                {
                                    Shell.Number = Shell.Number + 1;
                                    Shell.PointNumbers[Shell.Number] = Myglobals.AriaPointNo[Myglobals.AriaSelected] - 1;
                                    for (int i = 1; i < Shell.PointNumbers[Shell.Number] + 1; i++)
                                    {
                                        double theX = Myglobals.p1X[Myglobals.AriaSelected, i];
                                        double theY = Myglobals.p1Y[Myglobals.AriaSelected, i];
                                        double theZ = Myglobals.StoryLevel[add];
                                        int tah1 = 0;
                                        int selectedjoint = 0;
                                        for (int J = 1; J < Joint.Number2d + 1; J++)
                                        {
                                            if (Joint.XReal[J] == theX & Joint.YReal[J] == theY & Joint.ZReal[J] == theZ)
                                            {
                                                tah1 = 1;
                                                selectedjoint = J;
                                                break;
                                            }
                                        }
                                        if (tah1 == 0)
                                        {
                                            Joint.Number2d = Joint.Number2d + 1;
                                            selectedjoint = Joint.Number2d;
                                            Joint.XReal[Joint.Number2d] = theX;
                                            Joint.YReal[Joint.Number2d] = theY;
                                            Joint.ZReal[Joint.Number2d] = theZ;
                                        }
                                        Shell.JointNo[Shell.Number, i] = selectedjoint;
                                        Shell.SelectedLine[Shell.Number, i] = 0;
                                        Joint.FloorConnectionN[selectedjoint] = Joint.FloorConnectionN[selectedjoint] + 1;
                                        int thecount = Joint.FloorConnectionN[selectedjoint];
                                        Joint.Floor[selectedjoint, thecount] = Shell.Number;
                                    }
                                    Shell.Type[Shell.Number] = 1;
                                    Shell.Section[Shell.Number] = Slab.SelectedToDraw;
                                    callmee.SelectedShell = Shell.Number;
                                    callmee.CalculatePropertiesShell();
                                    callmee.CalculateShellMeshLines();
                                    callmee.CalculateMeshJoints();
                                }
                            }
                            #endregion
                            callmee.Render2d();
                            callmee.Render3d();
                            callmee.Renderelev();
                            MakeTempFiles();
                        }
                    }
                    #endregion
                    #region رسم بلاطة بالنقاط
                    if (toolStripButton8.Checked == true)//رسم بلاطة بالنقاط 
                    {
                        if (Myglobals.drowclick == 0)
                        {
                            int x1 = e.Location.X;
                            int y1 = e.Location.Y;
                            TempXReal[1] = Math.Round((x1 - Myglobals.startX2d) / (Myglobals.Zoom2d), 3);
                            TempYReal[1] = Math.Round((Myglobals.startY2d - y1) / (Myglobals.Zoom2d), 3);
                            TempZReal[1] = Myglobals.StoryLevel[Myglobals.SelectedStory];
                            if (Tahkik == 1)
                            {
                                TahkikXY[1] = 1;
                                x1 = TempX;
                                y1 = TempY;
                                TempXReal[1] = TempX12Real;
                                TempYReal[1] = TempY12Real;
                            }
                            double tx1 = TempXReal[1];
                            double ty1 = -1 * TempYReal[1];
                            double tz1 = -1 * TempZReal[1];
                            Math3DP.PointG Point3dM = new Math3DP.PointG(tx1, ty1, tz1);
                            Point3dM.RotateX = Myglobals.tXValue;
                            Point3dM.RotateY = Myglobals.tYValue;
                            Point3dM.RotateZ = Myglobals.tZValue;
                            Point3dM.DrawPoint();
                            LineMoveX1 = x1;
                            LineMoveY1 = y1;
                            LineMoveX2 = LineMoveX1;
                            LineMoveY2 = LineMoveY1;
                            Myglobals.LineMove2dVisible = 1;
                            Myglobals.LineMove3dVisible = 1;
                            Myglobals.LineMove3dFVisible = 1;
                            Myglobals.LineMoveelevVisible = 1;
                            Myglobals.LineMoveelevFVisible = 1;
                            Shell.PointNumbersTemp = Shell.PointNumbersTemp + 1;
                            Shell.PointXTemp[Shell.PointNumbersTemp] = TempXReal[1];
                            Shell.PointYTemp[Shell.PointNumbersTemp] = TempYReal[1];
                            Shell.PointZTemp[Shell.PointNumbersTemp] = TempZReal[1];
                            Myglobals.drowclick = 1;
                            SelectedStorytodrowallstory = Myglobals.SelectedStory;
                            goto sdd;
                        }
                        if (Myglobals.drowclick == 1)
                        {
                            TempZReal[2] = Myglobals.StoryLevel[Myglobals.SelectedStory];
                            int x1 = LineMoveX1;
                            int y1 = LineMoveY1;
                            int x2 = e.Location.X;
                            int y2 = e.Location.Y;
                            if (Tahkik == 1)
                            {
                                x2 = TempX;
                                y2 = TempY;
                                TahkikXY[2] = 1;
                                TempXReal[2] = TempX12Real;
                                TempYReal[2] = TempY12Real;
                            }
                            Shell.LineNumberTemp = Shell.LineNumberTemp + 1;

                            double theX = Math.Round(Convert.ToDouble(x1 - Myglobals.startX2d) / (Myglobals.Zoom2d), 3);
                            double theY = Math.Round((Myglobals.startY2d - y1) / (Myglobals.Zoom2d), 3);
                            double theZ = Myglobals.StoryLevel[Myglobals.SelectedStory];
                            if (TahkikXY[1] == 1)
                            {
                                theX = TempXReal[1];
                                theY = TempYReal[1];
                            }
                            double tx1 = theX;
                            double ty1 = -1 * theY;
                            double tz1 = -1 * theZ;
                            Math3DP.PointG Point3dM = new Math3DP.PointG(tx1, ty1, tz1);
                            Point3dM.RotateX = Myglobals.tXValue;
                            Point3dM.RotateY = Myglobals.tYValue;
                            Point3dM.RotateZ = Myglobals.tZValue;
                            Point3dM.DrawPoint();
                            theX = Math.Round(Convert.ToDouble(x2 - Myglobals.startX2d) / (Myglobals.Zoom2d), 3);
                            theY = Math.Round((Myglobals.startY2d - y2) / (Myglobals.Zoom2d), 3);
                            theZ = Myglobals.StoryLevel[Myglobals.SelectedStory];
                            if (TahkikXY[2] == 1)
                            {
                                theX = TempXReal[2];
                                theY = TempYReal[2];
                            }
                            tx1 = theX;
                            ty1 = -1 * theY;
                            tz1 = -1 * theZ;
                            Point3dM = new Math3DP.PointG(tx1, ty1, tz1);
                            Point3dM.RotateX = Myglobals.tXValue;
                            Point3dM.RotateY = Myglobals.tYValue;
                            Point3dM.RotateZ = Myglobals.tZValue;
                            Point3dM.DrawPoint();

                            Shell.PointNumbersTemp = Shell.PointNumbersTemp + 1;
                            Shell.PointXTemp[Shell.PointNumbersTemp] = theX;
                            Shell.PointYTemp[Shell.PointNumbersTemp] = theY;
                            Shell.PointZTemp[Shell.PointNumbersTemp] = theZ;

                            if (TahkikXY[2] == 1)
                            {
                                TahkikXY[1] = 1;
                            }
                            else
                            {
                                TahkikXY[1] = 0;
                            }
                            TahkikXY[2] = 0;
                            TempXReal[1] = theX;
                            TempYReal[1] = theY;
                            TempZReal[1] = theZ;
                        }
                    sdd: { }
                    }
                    #endregion
                    #region رسم أعمدة سريع
                    if (toolStripButton5.Checked == true & Myglobals.SelectedStory != 0)//رسم أعمدة سريع
                    {
                        #region//في طابق واحد
                        if (Myglobals.AllStories == 0)
                        {
                            int x1 = e.Location.X;
                            int y1 = e.Location.Y;
                            int x2 = x1;
                            int y2 = y1;
                            if (Tahkik == 1)
                            {
                                x1 = TempX;
                                y1 = TempY;
                                x2 = TempX;
                                y2 = TempY;
                            }
                            Frame.Number = Frame.Number + 1;//////////////////////
                            FrameElements emp = new FrameElements();
                            FrameElement[Frame.Number] = emp;
                            double theX = Math.Round(Convert.ToDouble(x1 - Myglobals.startX2d) / (Myglobals.Zoom2d), 3);
                            double theY = Math.Round((Myglobals.startY2d - y1) / (Myglobals.Zoom2d), 3);
                            if (Tahkik == 1)
                            {
                                theX = Math.Round(TempX12Real, 3);
                                theY = Math.Round(TempY12Real, 3);
                            }
                            double theZ = Math.Round(Myglobals.StoryLevel[Myglobals.SelectedStory], 3);

                            int tah = 0;
                            int selectedjoint = 0;
                            for (int i = 1; i < Joint.Number2d + 1; i++)
                            {
                                if (Joint.XReal[i] == theX & Joint.YReal[i] == theY & Joint.ZReal[i] == theZ)
                                {
                                    tah = 1;
                                    selectedjoint = i;
                                    break;
                                }
                            }
                            if (tah == 0)
                            {
                                Joint.Number2d = Joint.Number2d + 1;
                                selectedjoint = Joint.Number2d;
                                Joint.XReal[Joint.Number2d] = Math.Round(theX, 3);
                                Joint.YReal[Joint.Number2d] = Math.Round(theY, 3);
                                Joint.ZReal[Joint.Number2d] = Math.Round(theZ, 3);
                            }
                            FrameElement[Frame.Number].FirstJoint= selectedjoint;

                            theZ = Myglobals.StoryLevel[Myglobals.SelectedStory - 1];
                            tah = 0;
                            selectedjoint = 0;
                            for (int i = 1; i < Joint.Number2d + 1; i++)
                            {
                                if (Joint.XReal[i] == theX & Joint.YReal[i] == theY & Joint.ZReal[i] == theZ)
                                {
                                    tah = 1;
                                    selectedjoint = i;
                                    break;
                                }
                            }
                            if (tah == 0)
                            {
                                Joint.Number2d = Joint.Number2d + 1;
                                selectedjoint = Joint.Number2d;
                                Joint.XReal[Joint.Number2d] = Math.Round(theX, 3);
                                Joint.YReal[Joint.Number2d] = Math.Round(theY, 3);
                                Joint.ZReal[Joint.Number2d] = Math.Round(theZ, 3);
                            }
                            FrameElement[Frame.Number].SecondJoint= selectedjoint;
                            FrameElement[Frame.Number].Section= Section.SelectedToDraw;
                            FrameElement[Frame.Number].RotateAngel= Myglobals.RotateAngelDraw;

                            int thejoint = FrameElement[Frame.Number].FirstJoint;
                            Joint.BeamConnectionN[thejoint] = Joint.BeamConnectionN[thejoint] + 1;
                            int thecount = Joint.BeamConnectionN[thejoint];
                            Joint.Beam[thejoint, thecount] = Frame.Number;

                            thejoint = FrameElement[Frame.Number].SecondJoint;
                            Joint.BeamConnectionN[thejoint] = Joint.BeamConnectionN[thejoint] + 1;
                            thecount = Joint.BeamConnectionN[thejoint];
                            Joint.Beam[thejoint, thecount] = Frame.Number;
                            callmee.Render2d();
                            callmee.Render3d();
                            callmee.Renderelev();
                        }
                        #endregion
                        #region//في كل الطوابق
                        if (Myglobals.AllStories == 1)
                        {

                            int x1 = e.Location.X;
                            int y1 = e.Location.Y;
                            int x2 = x1;
                            int y2 = y1;
                            if (Tahkik == 1)
                            {
                                x1 = TempX;
                                y1 = TempY;
                                x2 = TempX;
                                y2 = TempY;
                            }
                            for (int add = 1; add < Myglobals.StoryNumbers + 1; add++)
                            {
                                Frame.Number = Frame.Number + 1;//////////////////////
                                FrameElements emp = new FrameElements();
                                FrameElement[Frame.Number] = emp;
                                double theX = Math.Round(Convert.ToDouble(x1 - Myglobals.startX2d) / (Myglobals.Zoom2d), 3);
                                double theY = Math.Round((Myglobals.startY2d - y1) / (Myglobals.Zoom2d), 3);
                                if (Tahkik == 1)
                                {
                                    theX = Math.Round(TempX12Real, 3);
                                    theY = Math.Round(TempY12Real, 3);
                                }
                                double theZ = Math.Round(Myglobals.StoryLevel[add], 3);
                                int tah = 0;
                                int selectedjoint = 0;
                                for (int i = 1; i < Joint.Number2d + 1; i++)
                                {
                                    if (Joint.XReal[i] == theX & Joint.YReal[i] == theY & Joint.ZReal[i] == theZ)
                                    {
                                        tah = 1;
                                        selectedjoint = i;
                                        break;
                                    }
                                }
                                if (tah == 0)
                                {
                                    Joint.Number2d = Joint.Number2d + 1;
                                    selectedjoint = Joint.Number2d;
                                    Joint.XReal[Joint.Number2d] = Math.Round(theX, 3);
                                    Joint.YReal[Joint.Number2d] = Math.Round(theY, 3);
                                    Joint.ZReal[Joint.Number2d] = Math.Round(theZ, 3);
                                }
                                FrameElement[Frame.Number].FirstJoint= selectedjoint;
                                theZ = Myglobals.StoryLevel[add - 1];
                                tah = 0;
                                selectedjoint = 0;
                                for (int i = 1; i < Joint.Number2d + 1; i++)
                                {
                                    if (Joint.XReal[i] == theX & Joint.YReal[i] == theY & Joint.ZReal[i] == theZ)
                                    {
                                        tah = 1;
                                        selectedjoint = i;
                                        break;
                                    }
                                }
                                if (tah == 0)
                                {
                                    Joint.Number2d = Joint.Number2d + 1;
                                    selectedjoint = Joint.Number2d;
                                    Joint.XReal[Joint.Number2d] = Math.Round(theX, 3);
                                    Joint.YReal[Joint.Number2d] = Math.Round(theY, 3);
                                    Joint.ZReal[Joint.Number2d] = Math.Round(theZ, 3);
                                }
                                FrameElement[Frame.Number].SecondJoint= selectedjoint;
                                FrameElement[Frame.Number].Section= Section.SelectedToDraw;
                                FrameElement[Frame.Number].RotateAngel= Myglobals.RotateAngelDraw;

                                int thejoint = FrameElement[Frame.Number].FirstJoint;
                                Joint.BeamConnectionN[thejoint] = Joint.BeamConnectionN[thejoint] + 1;
                                int thecount = Joint.BeamConnectionN[thejoint];
                                Joint.Beam[thejoint, thecount] = Frame.Number;

                                thejoint = FrameElement[Frame.Number].SecondJoint;
                                Joint.BeamConnectionN[thejoint] = Joint.BeamConnectionN[thejoint] + 1;
                                thecount = Joint.BeamConnectionN[thejoint];
                                Joint.Beam[thejoint, thecount] = Frame.Number;
                            }
                            callmee.Render2d();
                            callmee.Render3d();
                            callmee.Renderelev();
                        }
                        #endregion
                        MakeTempFiles();
                    }
                    #endregion
                    #region رسم جوائز
                    if (toolStripButton3.Checked == true)
                    {
                        if (Myglobals.drowclick == 0)
                        {
                            int x1 = e.X;
                            int y1 = e.Y;
                            TempXReal[1] = Math.Round((x1 - Myglobals.startX2d) / (Myglobals.Zoom2d), 3);
                            TempYReal[1] = Math.Round((Myglobals.startY2d - y1) / (Myglobals.Zoom2d), 3);
                            TempZReal[1] = Math.Round(Myglobals.StoryLevel[Myglobals.SelectedStory], 3);
                            if (Tahkik == 1)
                            {
                                TahkikXY[1] = 1;
                                x1 = TempX;
                                y1 = TempY;
                                TempXReal[1] = TempX12Real;
                                TempYReal[1] = TempY12Real;
                            }
                            double tx1 = TempXReal[1];
                            double ty1 = -1 * TempYReal[1];
                            double tz1 = -1 * TempZReal[1];
                            Math3DP.PointG Point3dM = new Math3DP.PointG(tx1, ty1, tz1);
                            Point3dM.RotateX = Myglobals.tXValue;
                            Point3dM.RotateY = Myglobals.tYValue;
                            Point3dM.RotateZ = Myglobals.tZValue;
                            Point3dM.DrawPoint();
                            LineMoveX1 = x1;
                            LineMoveY1 = y1;
                            LineMoveX2 = LineMoveX1;
                            LineMoveY2 = LineMoveY1;
                            Myglobals.LineMove2dVisible = 1;
                            Myglobals.LineMove3dVisible = 1;
                            Myglobals.LineMoveelevVisible = 1;
                            Myglobals.drowclick = 1;
                            SelectedStorytodrowallstory = Myglobals.SelectedStory;
                            goto sdd;
                        }
                        if (Myglobals.drowclick == 1)
                        {
                            TempZReal[2] = Myglobals.StoryLevel[Myglobals.SelectedStory];
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

                            Frame.Number = Frame.Number + 1;//////////////////////
                            FrameElements emp = new FrameElements();
                            FrameElement[Frame.Number] = emp;
                            double theX = TempXReal[1];
                            double theY = TempYReal[1];
                            double theZ = TempZReal[1];
                            int tah = 0;
                            int selectedjoint = 0;
                            for (int i = 1; i < Joint.Number2d + 1; i++)
                            {
                                if (Joint.XReal[i] == theX & Joint.YReal[i] == theY & Joint.ZReal[i] == theZ)
                                {
                                    tah = 1;
                                    selectedjoint = i;
                                    break;
                                }
                            }
                            int AddAtFirstBeam = 0;
                            int AddAtFirstJoint = 0;
                            if (tah == 0)
                            {
                                Joint.Number2d = Joint.Number2d + 1;
                                selectedjoint = Joint.Number2d;
                                Joint.XReal[Joint.Number2d] = theX;
                                Joint.YReal[Joint.Number2d] = theY;
                                Joint.ZReal[Joint.Number2d] = theZ;
                                for (int i = 1; i < Frame.Number; i++)
                                {
                                    int j = FrameElement[i].FirstJoint;
                                    double X1 = Joint.XReal[j];
                                    double Y1 = Joint.YReal[j];
                                    double Z1 = Joint.ZReal[j];
                                    j = FrameElement[i].SecondJoint;
                                    double X2 = Joint.XReal[j];
                                    double Y2 = Joint.YReal[j];
                                    double Z2 = Joint.ZReal[j];
                                    double X = theX;
                                    double Y = theY;
                                    double Z = theZ;
                                    checkinPointOnLine(X1, Y1, Z1, X2, Y2, Z2, X, Y, Z);
                                    if (INTERSECTION == 1)
                                    {
                                        AddAtFirstBeam = i;
                                        AddAtFirstJoint = selectedjoint;
                                        break;
                                    }
                                }
                            }
                            FrameElement[Frame.Number].FirstJoint= selectedjoint;

                            theX = Math.Round((x2 - Myglobals.startX2d) / (Myglobals.Zoom2d), 3);
                            theY = Math.Round((Myglobals.startY2d - y2) / (Myglobals.Zoom2d), 3);
                            theZ = TempZReal[2];
                            if (TahkikXY[2] == 1)
                            {
                                theX = TempXReal[2];
                                theY = TempYReal[2];
                            }
                            tah = 0;
                            selectedjoint = 0;
                            for (int i = 1; i < Joint.Number2d + 1; i++)
                            {
                                if (Joint.XReal[i] == theX & Joint.YReal[i] == theY & Joint.ZReal[i] == theZ)
                                {
                                    tah = 1;
                                    selectedjoint = i;
                                    break;
                                }
                            }
                            int AddAtSecoundBeam = 0;
                            int AddAtSecoundJoint = 0;
                            if (tah == 0)
                            {
                                Joint.Number2d = Joint.Number2d + 1;
                                selectedjoint = Joint.Number2d;
                                Joint.XReal[Joint.Number2d] = theX;
                                Joint.YReal[Joint.Number2d] = theY;
                                Joint.ZReal[Joint.Number2d] = theZ;
                                for (int i = 1; i < Frame.Number; i++)
                                {
                                    int j = FrameElement[i].FirstJoint;
                                    double X1 = Joint.XReal[j];
                                    double Y1 = Joint.YReal[j];
                                    double Z1 = Joint.ZReal[j];
                                    j = FrameElement[i].SecondJoint;
                                    double X2 = Joint.XReal[j];
                                    double Y2 = Joint.YReal[j];
                                    double Z2 = Joint.ZReal[j];
                                    double X = theX;
                                    double Y = theY;
                                    double Z = theZ;
                                    checkinPointOnLine(X1, Y1, Z1, X2, Y2, Z2, X, Y, Z);
                                    if (INTERSECTION == 1)
                                    {
                                        AddAtSecoundBeam = i;
                                        AddAtSecoundJoint = selectedjoint;
                                        break;
                                    }
                                }
                            }
                            FrameElement[Frame.Number].SecondJoint= selectedjoint;
                            FrameElement[Frame.Number].Section= Section.SelectedToDraw;
                            FrameElement[Frame.Number].RotateAngel= Myglobals.RotateAngelDraw;
                            int thejoint = FrameElement[Frame.Number].FirstJoint;
                            Joint.BeamConnectionN[thejoint] = Joint.BeamConnectionN[thejoint] + 1;
                            int thecount = Joint.BeamConnectionN[thejoint];
                            Joint.Beam[thejoint, thecount] = Frame.Number;
                            thejoint = FrameElement[Frame.Number].SecondJoint;
                            Joint.BeamConnectionN[thejoint] = Joint.BeamConnectionN[thejoint] + 1;
                            thecount = Joint.BeamConnectionN[thejoint];
                            Joint.Beam[thejoint, thecount] = Frame.Number;

                            if (AddAtFirstBeam != 0)
                            {
                                Frame.Number = Frame.Number + 1;////
                                FrameElements emp0 = new FrameElements();
                                emp0 = FrameElement[AddAtFirstBeam];
                                FrameElements emp1 = new FrameElements();
                                emp1.FirstJoint = AddAtFirstJoint;
                                emp1.SecondJoint = emp0.SecondJoint;
                                emp1.Section = emp0.Section;
                                FrameElement[Frame.Number] = emp1;
                                FrameElement[AddAtFirstBeam].SecondJoint = AddAtFirstJoint;
                                thejoint = AddAtFirstJoint;//---
                                Joint.BeamConnectionN[thejoint] = Joint.BeamConnectionN[thejoint] + 1;
                                thecount = Joint.BeamConnectionN[thejoint];
                                Joint.Beam[thejoint, thecount] = Frame.Number;
                                thejoint = AddAtFirstJoint;
                                Joint.BeamConnectionN[thejoint] = Joint.BeamConnectionN[thejoint] + 1;
                                thecount = Joint.BeamConnectionN[thejoint];
                                Joint.Beam[thejoint, thecount] = AddAtFirstBeam;
                                for (int i = 1; i < Joint.Number2d + 1; i++)
                                {
                                    for (int j = 1; j < Joint.BeamConnectionN[i] + 1; j++)
                                    {
                                        if (FrameElement[Joint.Beam[i, j]].FirstJoint == i || FrameElement[Joint.Beam[i, j]].SecondJoint == i)
                                        {
                                            goto nexti;
                                        }
                                        if (Joint.Beam[i, j] == AddAtFirstBeam)
                                        {
                                            Joint.Beam[i, j] = Frame.Number;
                                            goto endend1;
                                        }
                                    }
                                nexti: { }
                                }
                            }
                        endend1: { }
                            if (AddAtSecoundBeam != 0)
                            {
                                Frame.Number = Frame.Number + 1;////
                                FrameElements emp0 = new FrameElements();
                                emp0 = FrameElement[AddAtSecoundBeam];
                                FrameElements emp1 = new FrameElements();
                                emp1.FirstJoint = AddAtSecoundJoint;
                                emp1.SecondJoint = emp0.SecondJoint;
                                emp1.Section = emp0.Section;
                                FrameElement[Frame.Number] = emp1;
                                FrameElement[AddAtSecoundBeam].SecondJoint = AddAtSecoundJoint;
                                thejoint = AddAtSecoundJoint;//---
                                Joint.BeamConnectionN[thejoint] = Joint.BeamConnectionN[thejoint] + 1;
                                thecount = Joint.BeamConnectionN[thejoint];
                                Joint.Beam[thejoint, thecount] = Frame.Number;
                                thejoint = AddAtSecoundJoint;
                                Joint.BeamConnectionN[thejoint] = Joint.BeamConnectionN[thejoint] + 1;
                                thecount = Joint.BeamConnectionN[thejoint];
                                Joint.Beam[thejoint, thecount] = AddAtSecoundBeam;
                                for (int i = 1; i < Joint.Number2d + 1; i++)
                                {
                                    for (int j = 1; j < Joint.BeamConnectionN[i] + 1; j++)
                                    {
                                        if (FrameElement[Joint.Beam[i, j]].FirstJoint == i || FrameElement[Joint.Beam[i, j]].SecondJoint == i)
                                        {
                                            goto nexti;
                                        }
                                        if (Joint.Beam[i, j] == AddAtSecoundBeam)
                                        {
                                            Joint.Beam[i, j] = Frame.Number;
                                            goto endend2;
                                        }
                                    }
                                nexti: { }
                                }
                            }
                        endend2: { }
                            if (TahkikXY[2] == 1)
                            {
                                TahkikXY[1] = 1;
                            }
                            else
                            {
                                TahkikXY[1] = 0;
                            }
                            TahkikXY[2] = 0;
                            TempXReal[1] = theX;
                            TempYReal[1] = theY;
                            TempZReal[1] = theZ;
                            int selectedjoint1 = selectedjoint;
                            #region//رسم في كل الطوابق
                            if (Myglobals.AllStories == 1)
                            {
                                double theX1 = Joint.XReal[FrameElement[Frame.Number].FirstJoint];
                                double theY1 = Joint.YReal[FrameElement[Frame.Number].FirstJoint];
                                double theX2 = Joint.XReal[FrameElement[Frame.Number].SecondJoint];
                                double theY2 = Joint.YReal[FrameElement[Frame.Number].SecondJoint];
                                double theZ01 = Joint.ZReal[FrameElement[Frame.Number].FirstJoint];
                                double theZ02 = Joint.ZReal[FrameElement[Frame.Number].SecondJoint];
                                double theZ1 = 0;
                                double theZ2 = 0;
                                double delataZ1 = 0;
                                double delataZ2 = 0;
                                delataZ1 = theZ01 - Myglobals.StoryLevel[SelectedStorytodrowallstory];
                                delataZ2 = theZ02 - Myglobals.StoryLevel[SelectedStorytodrowallstory];
                                for (int add = 0; add < Myglobals.StoryNumbers + 1; add++)///من الاول حتى الاحير عدا المطلوب  
                                {
                                    if (theZ01 == theZ02 & add == 0) goto nextadd;//جائز لاترسمه في الارضي
                                    if (add == Myglobals.StoryNumbers & Myglobals.StoryLevel[add] + delataZ1 > Myglobals.StoryLevel[Myglobals.StoryNumbers]) goto nextadd;
                                    if (add == Myglobals.StoryNumbers & Myglobals.StoryLevel[add] + delataZ2 > Myglobals.StoryLevel[Myglobals.StoryNumbers]) goto nextadd;
                                    if (add == 0 & Myglobals.StoryLevel[add] + delataZ1 < 0) goto nextadd;
                                    if (add == 0 & Myglobals.StoryLevel[add] + delataZ2 < 0) goto nextadd;
                                    if (add != SelectedStorytodrowallstory)
                                    {
                                        Frame.Number = Frame.Number + 1;
                                        FrameElements emp1 = new FrameElements();
                                        FrameElement[Frame.Number] = emp1;
                                        theZ1 = Myglobals.StoryLevel[add] + delataZ1;
                                        theZ2 = Myglobals.StoryLevel[add] + delataZ2;
                                        tah = 0;
                                        selectedjoint = 0;
                                        for (int i = 1; i < Joint.Number2d + 1; i++)
                                        {
                                            if (Joint.XReal[i] == theX1 & Joint.YReal[i] == theY1 & Joint.ZReal[i] == theZ1)
                                            {
                                                tah = 1;
                                                selectedjoint = i;
                                                break;
                                            }
                                        }
                                        if (tah == 0)
                                        {
                                            Joint.Number2d = Joint.Number2d + 1;
                                            selectedjoint = Joint.Number2d;
                                            Joint.XReal[Joint.Number2d] = theX1;
                                            Joint.YReal[Joint.Number2d] = theY1;
                                            Joint.ZReal[Joint.Number2d] = theZ1;
                                        }
                                        FrameElement[Frame.Number].FirstJoint= selectedjoint;
                                        tah = 0;
                                        selectedjoint = 0;
                                        for (int i = 1; i < Joint.Number2d + 1; i++)
                                        {
                                            if (Joint.XReal[i] == theX2 & Joint.YReal[i] == theY2 & Joint.ZReal[i] == theZ2)
                                            {
                                                tah = 1;
                                                selectedjoint = i;
                                                break;
                                            }
                                        }
                                        if (tah == 0)
                                        {
                                            Joint.Number2d = Joint.Number2d + 1;
                                            selectedjoint = Joint.Number2d;
                                            Joint.XReal[Joint.Number2d] = theX2;
                                            Joint.YReal[Joint.Number2d] = theY2;
                                            Joint.ZReal[Joint.Number2d] = theZ2;
                                        }
                                        FrameElement[Frame.Number].SecondJoint= selectedjoint;
                                        FrameElement[Frame.Number].Section= Section.SelectedToDraw;
                                        FrameElement[Frame.Number].RotateAngel= Myglobals.RotateAngelDraw;
                                        thejoint = FrameElement[Frame.Number].FirstJoint;
                                        Joint.BeamConnectionN[thejoint] = Joint.BeamConnectionN[thejoint] + 1;
                                        thecount = Joint.BeamConnectionN[thejoint];
                                        Joint.Beam[thejoint, thecount] = Frame.Number;
                                        thejoint = FrameElement[Frame.Number].SecondJoint;
                                        Joint.BeamConnectionN[thejoint] = Joint.BeamConnectionN[thejoint] + 1;
                                        thecount = Joint.BeamConnectionN[thejoint];
                                        Joint.Beam[thejoint, thecount] = Frame.Number;
                                    }
                                nextadd: { };
                                }
                            }
                            #endregion
                            SelectedStorytodrowallstory = Myglobals.SelectedStory;
                            callmee.Render2d();
                            callmee.Render3d();
                            callmee.Renderelev();
                            MakeTempFiles();
                        }
                    sdd: { }
                    }
                    #endregion
                }
            }
        }
        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            lastzoomX2d = e.X;
            lastzoomY2d = e.Y;
            #region // تحريك المسقط
            if (e.Button == MouseButtons.Middle)
            {
                timetodo = timetodo + 1;
                Myglobals.startX2d = Myglobals.startX2d + (Cursor.Position.X - Myglobals.xmove);
                Myglobals.startY2d = Myglobals.startY2d + (Cursor.Position.Y - Myglobals.ymove);
                Myglobals.xmove = Cursor.Position.X;
                Myglobals.ymove = Cursor.Position.Y;
                if (timetodo > 2)
                {
                    if (Joint.Assignments == 1) JointAssignments = 1;
                    if (Frame.Assignments == 1) BeamAssignments = 1;
                    Joint.Assignments = 0;
                    Frame.Assignments = 0;
                    DROWcls callmee = new DROWcls();
                    callmee.Render2d();
                    timetodo = 0;
                    Bitmap finalBmp = new Bitmap(Myglobals.BitampWidth2d, Myglobals.BitampHight2d);
                    if (pictureBox3.Image != null)
                    {
                        pictureBox3.Image.Dispose();
                        pictureBox3.Image = null;
                    }
                    pictureBox3.Image = finalBmp;
                }
                goto ENDLOOP;
            }
            #endregion
            # region//تحديد البلاطات بالمرور على المساحات
            if (toolStripButton10.Checked == true)//رسم بلاطة سريع
            {
                int ifselected = 0;
                int Xtest = e.X;
                int Ytest = e.Y;
                for (int i = 1; i < Myglobals.AriaNo + 1; i++)
                {
                    int N = Myglobals.AriaPointNo[i];
                    Point[] polygon = new Point[N + 1];
                    for (int j = 1; j < N + 1; j++)
                    {
                        polygon[j].X = Myglobals.startX2d + Convert.ToInt32((Myglobals.p1X[i, j] * Myglobals.Zoom2d));
                        polygon[j].Y = Myglobals.startY2d - Convert.ToInt32((Myglobals.p1Y[i, j] * Myglobals.Zoom2d));
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
                    if (result == true & Myglobals.AriaSelected != i)
                    {
                        Myglobals.AriaSelected = i;
                        break;
                    }
                    if (result == false)
                    {
                        ifselected = ifselected + 1;
                    }
                }
                if (ifselected == Myglobals.AriaNo & Myglobals.AriaSelected != 0)
                {
                    Myglobals.AriaSelected = 0;
                    Bitmap finalBmp = new Bitmap(Myglobals.BitampWidth2d, Myglobals.BitampHight2d);
                    if (pictureBox3.Image != null)
                    {
                        pictureBox3.Image.Dispose();
                        pictureBox3.Image = null;
                    }
                    pictureBox3.Image = finalBmp;
                }
            }
            #endregion
            #region//التقاط الاوسناب
            TempX = e.X;
            TempY = e.Y;
            TempX12Real = Math.Round((TempX - Myglobals.startX2d) / (Myglobals.Zoom2d), 3);
            TempY12Real = Math.Round((Myglobals.startY2d - TempY) / (Myglobals.Zoom2d), 3);
            lastzoomX2dR = TempX12Real;
            lastzoomY2dR = TempY12Real;
            Tahkik = 0;
            #region//تقاطعات الشبكة
            if (Snap.GridIntersections == 1)
            {
                for (int i = 1; i < GridPoint.Number2d + 1; i++)
                {
                    if (Math.Abs(TempX - GridPoint.X2d[i]) < 4 & Math.Abs(TempY - GridPoint.Y2d[i]) < 4)
                    {
                        Tahkik = 1;
                        SelectedGridpoint = i;
                        SnapFromType = "GridIntersections";
                        lastzoomX2dR = GridPoint.XReal[i];
                        lastzoomY2dR = GridPoint.YReal[i];
                        TempX12Real = GridPoint.XReal[i];
                        TempY12Real = GridPoint.YReal[i];
                        TempX = GridPoint.X2d[i];
                        TempY = GridPoint.Y2d[i];
                        goto endloop;
                    }
                }
            }
            #endregion
            #region//العقد
            if (Snap.Joints == 1)
            {
                for (int i = 1; i < Joint.Number2d + 1; i++)
                {
                    if (Joint.ZReal[i] == Myglobals.StoryLevel[Myglobals.SelectedStory])
                    {
                        if (Math.Abs(TempX - Joint.X2d[i]) < 4 & Math.Abs(TempY - Joint.Y2d[i]) < 4)
                        {
                            Tahkik = 1;
                            SelectedJoint = i;
                            SnapFromType = "Joints";
                            lastzoomX2dR = Joint.XReal[i];
                            lastzoomY2dR = Joint.YReal[i];
                            TempX12Real = Joint.XReal[i];
                            TempY12Real = Joint.YReal[i];
                            TempZ12Real = Joint.ZReal[i];
                            TempX = Joint.X2d[i];
                            TempY = Joint.Y2d[i];
                            goto endloop;
                        }
                    }
                }
            }
            #endregion
            #region//التقاطعات
            if (Snap.Intersections == 1)
            {
                for (int i = 1; i < IntersectionPoint.Number2d + 1; i++)
                {
                    if (Math.Abs(TempX - IntersectionPoint.X2d[i]) < 4 & Math.Abs(TempY - IntersectionPoint.Y2d[i]) < 4)
                    {
                        Tahkik = 1;
                        SnapFromType = "Intersections";
                        lastzoomX2dR = IntersectionPoint.XReal[i];
                        lastzoomY2dR = IntersectionPoint.YReal[i];
                        TempX12Real = IntersectionPoint.XReal[i];
                        TempY12Real = IntersectionPoint.YReal[i];
                        TempX = IntersectionPoint.X2d[i];
                        TempY = IntersectionPoint.Y2d[i];
                        goto endloop;
                    }
                }
            }
            #endregion
            #region //نهايات ومنتصف الخطوط
            if (Snap.LineEndsandMidpoints == 1)
            {
                for (int i = 1; i < Frame.Number + 1; i++)
                {
                    if (FrameElement[i].Visible == 0)
                    {
                        if (Joint.ZReal[FrameElement[i].FirstJoint] == Myglobals.StoryLevel[Myglobals.SelectedStory] & Joint.ZReal[FrameElement[i].FirstJoint] == Joint.ZReal[FrameElement[i].SecondJoint])
                        {
                            int Xmid = (Joint.X2d[FrameElement[i].FirstJoint] + Joint.X2d[FrameElement[i].SecondJoint]) / 2;
                            int Ymid = (Joint.Y2d[FrameElement[i].FirstJoint] + Joint.Y2d[FrameElement[i].SecondJoint]) / 2;
                            if (Math.Abs(TempX - Xmid) < 4 & Math.Abs(TempY - Ymid) < 4)
                            {
                                Tahkik = 1;
                                SnapFromType = "LineEndsandMidpoints";
                                lastzoomX2dR = (Joint.XReal[FrameElement[i].FirstJoint] + Joint.XReal[FrameElement[i].SecondJoint]) / 2;
                                lastzoomY2dR = (Joint.YReal[FrameElement[i].FirstJoint] + Joint.YReal[FrameElement[i].SecondJoint]) / 2;
                                TempX12Real = (Joint.XReal[FrameElement[i].FirstJoint] + Joint.XReal[FrameElement[i].SecondJoint]) / 2;
                                TempY12Real = (Joint.YReal[FrameElement[i].FirstJoint] + Joint.YReal[FrameElement[i].SecondJoint]) / 2;
                                TempZ12Real = (Joint.ZReal[FrameElement[i].FirstJoint] + Joint.ZReal[FrameElement[i].SecondJoint]) / 2;
                                TempX = Xmid;
                                TempY = Ymid;
                                goto endloop;
                            }
                        }
                    }
                }
                for (int i = 1; i < Shell.Number + 1; i++)
                {
                    if (Shell.Visible[i] == 0)
                    {
                        if (Shell.Type[i] == 1 & Joint.ZReal[Shell.JointNo[i, 1]] == Myglobals.StoryLevel[Myglobals.SelectedStory])
                        {
                            int Xmid = 0;
                            int Ymid = 0;
                            for (int j = 1; j < Shell.PointNumbers[i]; j++)
                            {
                                Xmid = (Joint.X2d[Shell.JointNo[i, j]] + Joint.X2d[Shell.JointNo[i, j + 1]]) / 2;
                                Ymid = (Joint.Y2d[Shell.JointNo[i, j]] + Joint.Y2d[Shell.JointNo[i, j + 1]]) / 2;
                                if (Math.Abs(TempX - Xmid) < 4 & Math.Abs(TempY - Ymid) < 4)
                                {
                                    Tahkik = 1;
                                    SnapFromType = "LineEndsandMidpoints";
                                    lastzoomX2dR = (Joint.XReal[Shell.JointNo[i, j]] + Joint.XReal[Shell.JointNo[i, j + 1]]) / 2;
                                    lastzoomY2dR = (Joint.YReal[Shell.JointNo[i, j]] + Joint.YReal[Shell.JointNo[i, j + 1]]) / 2;
                                    TempX12Real = (Joint.XReal[Shell.JointNo[i, j]] + Joint.XReal[Shell.JointNo[i, j + 1]]) / 2;
                                    TempY12Real = (Joint.YReal[Shell.JointNo[i, j]] + Joint.YReal[Shell.JointNo[i, j + 1]]) / 2;
                                    TempZ12Real = (Joint.ZReal[Shell.JointNo[i, j]] + Joint.ZReal[Shell.JointNo[i, j + 1]]) / 2;
                                    TempX = Xmid;
                                    TempY = Ymid;
                                    goto endloop;
                                }
                            }
                            Xmid = (Joint.X2d[Shell.JointNo[i, Shell.PointNumbers[i]]] + Joint.X2d[Shell.JointNo[i, 1]]) / 2;
                            Ymid = (Joint.Y2d[Shell.JointNo[i, Shell.PointNumbers[i]]] + Joint.Y2d[Shell.JointNo[i, 1]]) / 2;
                            if (Math.Abs(TempX - Xmid) < 4 & Math.Abs(TempY - Ymid) < 4)
                            {
                                Tahkik = 1;
                                SnapFromType = "LineEndsandMidpoints";
                                lastzoomX2dR = (Joint.XReal[Shell.JointNo[i, Shell.PointNumbers[i]]] + Joint.XReal[Shell.JointNo[i, 1]]) / 2;
                                lastzoomY2dR = (Joint.YReal[Shell.JointNo[i, Shell.PointNumbers[i]]] + Joint.YReal[Shell.JointNo[i, 1]]) / 2;
                                TempX12Real = (Joint.XReal[Shell.JointNo[i, Shell.PointNumbers[i]]] + Joint.XReal[Shell.JointNo[i, 1]]) / 2;
                                TempY12Real = (Joint.YReal[Shell.JointNo[i, Shell.PointNumbers[i]]] + Joint.YReal[Shell.JointNo[i, 1]]) / 2;
                                TempZ12Real = (Joint.ZReal[Shell.JointNo[i, Shell.PointNumbers[i]]] + Joint.ZReal[Shell.JointNo[i, 1]]) / 2;
                                TempX = Xmid;
                                TempY = Ymid;
                                goto endloop;
                            }
                        }
                    }
                }
            }
            #endregion
            if (Snap.LinesandFrames == 1)
            {
                # region//سناب العناصر الخطية
                for (int i = 1; i < Frame.Number + 1; i++)
                {
                    //جائز
                    if (FrameElement[i].Visible == 0)
                    {
                        if (Joint.ZReal[FrameElement[i].FirstJoint] == Myglobals.StoryLevel[Myglobals.SelectedStory] & Joint.ZReal[FrameElement[i].SecondJoint] == Myglobals.StoryLevel[Myglobals.SelectedStory])
                        {
                            double XREAL = 0;
                            double YREAL = 0;
                            int X2d = 0;
                            int Y2d = 0;
                            int X1 = Joint.X2d[FrameElement[i].FirstJoint];
                            int Y1 = Joint.Y2d[FrameElement[i].FirstJoint];
                            int X2 = Joint.X2d[FrameElement[i].SecondJoint];
                            int Y2 = Joint.Y2d[FrameElement[i].SecondJoint];
                            int X = e.X;
                            int Y = e.Y;
                            double distance = 100000;
                            int Tah = 0;
                            if ((Y2 == Y1))
                            {
                                if (X >= X1 & X <= X2) Tah = 1;
                                if (X <= X1 & X >= X2) Tah = 1;
                                if (Tah == 1)
                                {
                                    distance = (Y - Y1);
                                    XREAL = Math.Round((X - Myglobals.startX2d) / (Myglobals.Zoom2d), 3);
                                    YREAL = Joint.YReal[FrameElement[i].FirstJoint];
                                    X2d = X;
                                    Y2d = Joint.Y2d[FrameElement[i].FirstJoint];
                                    goto endloop5;
                                }
                            }
                            if ((X2 == X1))
                            {
                                if (Y >= Y1 & Y <= Y2) Tah = 1;
                                if (Y <= Y1 & Y >= Y2) Tah = 1;
                                if (Tah == 1)
                                {
                                    distance = (X - X1);
                                    XREAL = Joint.XReal[FrameElement[i].FirstJoint];
                                    YREAL = Math.Round((Myglobals.startY2d - Y) / (Myglobals.Zoom2d), 3);
                                    X2d = Joint.X2d[FrameElement[i].FirstJoint];
                                    Y2d = Y;
                                    goto endloop5;
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
                                    distance = ((X2 - X1) * (Y1 - Y) - (X1 - X) * (Y2 - Y1)) / Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2));
                                    double X1R = Joint.XReal[FrameElement[i].FirstJoint];
                                    double Y1R = Joint.YReal[FrameElement[i].FirstJoint];
                                    double X2R = Joint.XReal[FrameElement[i].SecondJoint];
                                    double Y2R = Joint.YReal[FrameElement[i].SecondJoint];
                                    double Length = Math.Sqrt(Math.Pow(X1R - X2R, 2) + Math.Pow(Y1R - Y2R, 2));
                                    XREAL = Math.Round((X - Myglobals.startX2d) / (Myglobals.Zoom2d), 3);
                                    YREAL = Math.Round((Myglobals.startY2d - Y) / (Myglobals.Zoom2d), 3);
                                    double DIS = Math.Sqrt(Math.Pow(X1R - XREAL, 2) + Math.Pow(Y1R - YREAL, 2));
                                    if (DIS > Length) DIS = Length;
                                    XREAL = Math.Round(X1R + (DIS / Length * (X2R - X1R)), 3);
                                    YREAL = Math.Round(Y1R + (DIS / Length * (Y2R - Y1R)), 3);
                                    X2d = Myglobals.startX2d + Convert.ToInt32((XREAL) * Myglobals.Zoom2d);
                                    Y2d = Myglobals.startY2d - Convert.ToInt32((YREAL) * Myglobals.Zoom2d);
                                }
                            }
                        endloop5: { }
                            if (Math.Abs(distance) < 0.1 * Myglobals.Zoom2d)
                            {
                                Tahkik = 1;
                                SnapFromType = "LinesandFrames";
                                lastzoomX2dR = XREAL;
                                lastzoomY2dR = YREAL;
                                TempX12Real = XREAL;
                                TempY12Real = YREAL;
                                TempZ12Real = Myglobals.StoryLevel[Myglobals.SelectedStory];
                                TempX = X2d;
                                TempY = Y2d;
                                goto endloop;
                            }
                        }
                    }
                }
                #endregion
                # region//سناب البلاطات بخطوطها
                for (int i = 1; i < Shell.Number + 1; i++)
                {
                    if (Shell.Visible[i] == 0)
                    {
                        if (Shell.Type[i] == 1 & Joint.ZReal[Shell.JointNo[i, 1]] == Myglobals.StoryLevel[Myglobals.SelectedStory])
                        {
                            for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                            {
                                int X1 = Joint.X2d[Shell.JointNo[i, j]];
                                int Y1 = Joint.Y2d[Shell.JointNo[i, j]];
                                int X2 = 0;
                                int Y2 = 0;
                                double XREAL = 0;
                                double YREAL = 0;
                                int X2d = 0;
                                int Y2d = 0;
                                if (j != Shell.PointNumbers[i])
                                {
                                    X2 = Joint.X2d[Shell.JointNo[i, j + 1]];
                                    Y2 = Joint.Y2d[Shell.JointNo[i, j + 1]];
                                }
                                else
                                {
                                    X2 = Joint.X2d[Shell.JointNo[i, 1]];
                                    Y2 = Joint.Y2d[Shell.JointNo[i, 1]];
                                }
                                int X = e.Location.X;
                                int Y = e.Location.Y;
                                double distance = 100000;
                                int Tah = 0;
                                if ((Y2 == Y1))
                                {
                                    if (X >= X1 & X <= X2) Tah = 1;
                                    if (X <= X1 & X >= X2) Tah = 1;
                                    if (Tah == 1)
                                    {
                                        distance = (Y - Y1);
                                        XREAL = Math.Round((X - Myglobals.startX2d) / (Myglobals.Zoom2d), 3);
                                        YREAL = Joint.YReal[Shell.JointNo[i, j]];
                                        X2d = X;
                                        Y2d = Y2;
                                        goto endloop5;
                                    }
                                }
                                if ((X2 == X1))
                                {
                                    if (Y >= Y1 & Y <= Y2) Tah = 1;
                                    if (Y <= Y1 & Y >= Y2) Tah = 1;
                                    if (Tah == 1)
                                    {
                                        distance = (X - X1);
                                        XREAL = Joint.XReal[Shell.JointNo[i, j]];
                                        YREAL = Math.Round((Myglobals.startY2d - Y) / (Myglobals.Zoom2d), 3);
                                        X2d = X2;
                                        Y2d = Y;
                                        goto endloop5;
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
                                        distance = ((X2 - X1) * (Y1 - Y) - (X1 - X) * (Y2 - Y1)) / Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2));
                                        double X1R = Joint.XReal[Shell.JointNo[i, j]];
                                        double Y1R = Joint.YReal[Shell.JointNo[i, j]];
                                        double X2R = 0;
                                        double Y2R = 0;
                                        if (j != Shell.PointNumbers[i])
                                        {
                                            X2R = Joint.XReal[Shell.JointNo[i, j + 1]];
                                            Y2R = Joint.YReal[Shell.JointNo[i, j + 1]];
                                        }
                                        else
                                        {
                                            X2R = Joint.XReal[Shell.JointNo[i, 1]];
                                            Y2R = Joint.YReal[Shell.JointNo[i, 1]];
                                        }
                                        double Length = Math.Sqrt(Math.Pow(X1R - X2R, 2) + Math.Pow(Y1R - Y2R, 2));
                                        XREAL = Math.Round((X - Myglobals.startX2d) / (Myglobals.Zoom2d), 3);
                                        YREAL = Math.Round((Myglobals.startY2d - Y) / (Myglobals.Zoom2d), 3);
                                        double DIS = Math.Sqrt(Math.Pow(X1R - XREAL, 2) + Math.Pow(Y1R - YREAL, 2));
                                        if (DIS > Length) DIS = Length;
                                        XREAL = Math.Round(X1R + (DIS / Length * (X2R - X1R)), 3);
                                        YREAL = Math.Round(Y1R + (DIS / Length * (Y2R - Y1R)), 3);
                                        X2d = Myglobals.startX2d + Convert.ToInt32((XREAL) * Myglobals.Zoom2d);
                                        Y2d = Myglobals.startY2d - Convert.ToInt32((YREAL) * Myglobals.Zoom2d);
                                    }
                                }
                            endloop5: { }
                                if (Math.Abs(distance) < 0.1 * Myglobals.Zoom2d)
                                {
                                    Tahkik = 1;
                                    SnapFromType = "LinesandFrames";
                                    lastzoomX2dR = XREAL;
                                    lastzoomY2dR = YREAL;
                                    TempX12Real = XREAL;
                                    TempY12Real = YREAL;
                                    TempZ12Real = Myglobals.StoryLevel[Myglobals.SelectedStory];
                                    TempX = X2d;
                                    TempY = Y2d;
                                    goto endloop;
                                }
                            }
                        }
                    }
                }
                #endregion
            }
            if (Snap.Edges == 1)
            {
                # region//سناب البلاطات بخطوطها
                for (int i = 1; i < Shell.Number + 1; i++)
                {
                    if (Shell.Visible[i] == 0)
                    {
                        if (Shell.Type[i] == 1 & Joint.ZReal[Shell.JointNo[i, 1]] == Myglobals.StoryLevel[Myglobals.SelectedStory])
                        {
                            for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                            {
                                int X1 = Joint.X2d[Shell.JointNo[i, j]];
                                int Y1 = Joint.Y2d[Shell.JointNo[i, j]];
                                int X2 = 0;
                                int Y2 = 0;
                                double XREAL = 0;
                                double YREAL = 0;
                                int X2d = 0;
                                int Y2d = 0;
                                if (j != Shell.PointNumbers[i])
                                {
                                    X2 = Joint.X2d[Shell.JointNo[i, j + 1]];
                                    Y2 = Joint.Y2d[Shell.JointNo[i, j + 1]];
                                }
                                else
                                {
                                    X2 = Joint.X2d[Shell.JointNo[i, 1]];
                                    Y2 = Joint.Y2d[Shell.JointNo[i, 1]];
                                }
                                int X = e.Location.X;
                                int Y = e.Location.Y;
                                double distance = 100000;
                                int Tah = 0;
                                if ((Y2 == Y1))
                                {
                                    if (X >= X1 & X <= X2) Tah = 1;
                                    if (X <= X1 & X >= X2) Tah = 1;
                                    if (Tah == 1)
                                    {
                                        distance = (Y - Y1);
                                        XREAL = Math.Round((X - Myglobals.startX2d) / (Myglobals.Zoom2d), 3);
                                        YREAL = Joint.YReal[Shell.JointNo[i, j]];
                                        X2d = X;
                                        Y2d = Y2;
                                        goto endloop5;
                                    }
                                }
                                if ((X2 == X1))
                                {
                                    if (Y >= Y1 & Y <= Y2) Tah = 1;
                                    if (Y <= Y1 & Y >= Y2) Tah = 1;
                                    if (Tah == 1)
                                    {
                                        distance = (X - X1);
                                        XREAL = Joint.XReal[Shell.JointNo[i, j]];
                                        YREAL = Math.Round((Myglobals.startY2d - Y) / (Myglobals.Zoom2d), 3);
                                        X2d = X2;
                                        Y2d = Y;
                                        goto endloop5;
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
                                        distance = ((X2 - X1) * (Y1 - Y) - (X1 - X) * (Y2 - Y1)) / Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2));
                                        double X1R = Joint.XReal[Shell.JointNo[i, j]];
                                        double Y1R = Joint.YReal[Shell.JointNo[i, j]];
                                        double X2R = 0;
                                        double Y2R = 0;
                                        if (j != Shell.PointNumbers[i])
                                        {
                                            X2R = Joint.XReal[Shell.JointNo[i, j + 1]];
                                            Y2R = Joint.YReal[Shell.JointNo[i, j + 1]];
                                        }
                                        else
                                        {
                                            X2R = Joint.XReal[Shell.JointNo[i, 1]];
                                            Y2R = Joint.YReal[Shell.JointNo[i, 1]];
                                        }
                                        double Length = Math.Sqrt(Math.Pow(X1R - X2R, 2) + Math.Pow(Y1R - Y2R, 2));
                                        XREAL = Math.Round((X - Myglobals.startX2d) / (Myglobals.Zoom2d), 3);
                                        YREAL = Math.Round((Myglobals.startY2d - Y) / (Myglobals.Zoom2d), 3);
                                        double DIS = Math.Sqrt(Math.Pow(X1R - XREAL, 2) + Math.Pow(Y1R - YREAL, 2));
                                        if (DIS > Length) DIS = Length;
                                        XREAL = Math.Round(X1R + (DIS / Length * (X2R - X1R)), 3);
                                        YREAL = Math.Round(Y1R + (DIS / Length * (Y2R - Y1R)), 3);
                                        X2d = Myglobals.startX2d + Convert.ToInt32((XREAL) * Myglobals.Zoom2d);
                                        Y2d = Myglobals.startY2d - Convert.ToInt32((YREAL) * Myglobals.Zoom2d);
                                    }
                                }
                            endloop5: { }
                                if (Math.Abs(distance) < 0.1 * Myglobals.Zoom2d)
                                {
                                    Tahkik = 1;
                                    SnapFromType = "Edges";
                                    lastzoomX2dR = XREAL;
                                    lastzoomY2dR = YREAL;
                                    TempX12Real = XREAL;
                                    TempY12Real = YREAL;
                                    TempZ12Real = Myglobals.StoryLevel[Myglobals.SelectedStory];
                                    TempX = X2d;
                                    TempY = Y2d;
                                    goto endloop;
                                }
                            }
                        }
                    }
                }
                #endregion
            }
            #region//التعامد
            if (Snap.Prallels == 1)
            {
                float sweepAngle = (float)Math.Round(Angulo(TempXReal[1], TempYReal[1], TempX12Real, TempY12Real), 2);
                if (Math.Abs(sweepAngle - 90) < 0.8 || Math.Abs(sweepAngle - 270) < 0.8)
                {
                    Tahkik = 1;
                    SnapFromType = "Prallels";
                    TempX = LineMoveX1;
                    TempY = lastzoomY2d;
                    double XREAL = Math.Round((lastzoomX2d - Myglobals.startX2d) / (Myglobals.Zoom2d), 3);
                    double YREAL = Math.Round((Myglobals.startY2d - lastzoomY2d) / (Myglobals.Zoom2d), 3);
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
                    double XREAL = Math.Round((lastzoomX2d - Myglobals.startX2d) / (Myglobals.Zoom2d), 3);
                    double YREAL = Math.Round((Myglobals.startY2d - lastzoomY2d) / (Myglobals.Zoom2d), 3);
                    lastzoomX2dR = XREAL;
                    lastzoomY2dR = TempYReal[1];
                    TempX12Real = XREAL;
                    TempY12Real = TempYReal[1];
                    goto endloop;
                }
            }
            #endregion
            #region//الشبكة الناعمة
            if (Snap.FineGrid == 1)
            {
                Tahkik = 1;
                SnapFromType = "FineGrid";
                double XREAL = (TempX - Myglobals.startX2d) / (Myglobals.Zoom2d);
                double YREAL = (Myglobals.startY2d - TempY) / (Myglobals.Zoom2d);
                XREAL = Math.Round(XREAL / Snap.FineGridValue, 0) * Snap.FineGridValue;
                YREAL = Math.Round(YREAL / Snap.FineGridValue, 0) * Snap.FineGridValue;
                TempX = Myglobals.startX2d + Convert.ToInt32((XREAL) * Myglobals.Zoom2d);
                TempY = Myglobals.startY2d - Convert.ToInt32((YREAL) * Myglobals.Zoom2d);
                lastzoomX2dR = XREAL;
                lastzoomY2dR = YREAL;
                TempX12Real = XREAL;
                TempY12Real = YREAL;
                goto endloop;
            }
            #endregion
        endloop: { }
            LBLX.Visible = true;
            LBLY.Visible = true;
            LBLZ.Visible = true;
            LBLX.Text = "X= " + Convert.ToString(TempX12Real);
            LBLY.Text = "Y= " + Convert.ToString(TempY12Real);
            LBLZ.Text = "Z= " + Convert.ToString(Myglobals.StoryLevel[Myglobals.SelectedStory]);
            #endregion
            if (e.Button == MouseButtons.Middle) goto ENDLOOP;
            MouseButtonsLeft = 0;
            if (e.Button == MouseButtons.Left) MouseButtonsLeft = 1;
            timetodo = timetodo + 1;
            if (timetodo > 2)
            {
                pictureBox3Draw();
                timetodo = 0;
            }
        ENDLOOP: { }
        }
        private void pictureBox3Draw()
        {
            Bitmap finalBmp = new Bitmap(Myglobals.BitampWidth2d, Myglobals.BitampHight2d);
            if (pictureBox3.Image != null)
            {
                pictureBox3.Image.Dispose();
                pictureBox3.Image = null;
            }
            pictureBox3.Image = finalBmp;
            #region//رسم مربع التحديد
            if (toolStripButton1.Checked == true & MouseButtonsLeft == 1)
            {
                Graphics g = Graphics.FromImage(pictureBox3.Image);
                Pen pen = new Pen(Color.Black, 1f);
                float[] dashValues = { 5, 3, 5, 3 };
                pen.DashPattern = dashValues;
                Point[] polygon = new Point[5];
                polygon[0].X = Myglobals.xmove1;
                polygon[0].Y = Myglobals.ymove1;
                polygon[1].X = TempX;
                polygon[1].Y = Myglobals.ymove1;
                polygon[2].X = TempX;
                polygon[2].Y = TempY;
                polygon[3].X = Myglobals.xmove1;
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
            #region//رسم بلاطة مستطيل
            if (toolStripButton9.Checked == true & Myglobals.drowclick == 1)
            {
                Graphics g = Graphics.FromImage(pictureBox3.Image);
                Pen pen = new Pen(Color.Gray, 1f);
                float[] dashValues = { 5, 3, 5, 3 };
                pen.DashPattern = dashValues;
                Font drawFont = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
                SolidBrush drawBrush = new SolidBrush(Color.Blue);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                Point[] polygon = new Point[5];
                double thelength = 0;
                int thex = 0;
                int they = 0;
                polygon[0].X = LineMoveX1;
                polygon[0].Y = LineMoveY1;
                polygon[1].X = TempX;
                polygon[1].Y = LineMoveY1;
                polygon[2].X = TempX;
                polygon[2].Y = TempY;
                polygon[3].X = LineMoveX1;
                polygon[3].Y = TempY;
                polygon[4] = polygon[0];
                g.DrawLine(pen, polygon[0], polygon[1]);
                g.DrawLine(pen, polygon[1], polygon[2]);
                g.DrawLine(pen, polygon[2], polygon[3]);
                g.DrawLine(pen, polygon[3], polygon[4]);

                thelength = Math.Round(Math.Abs(TempXReal[1] - TempX12Real), 3);
                thex = (polygon[0].X + polygon[1].X) / 2;
                they = (polygon[0].Y + polygon[1].Y) / 2;
                g.DrawString(thelength.ToString() + " m", drawFont, drawBrush, thex - 15, they - 15);
                thex = (polygon[2].X + polygon[3].X) / 2;
                they = (polygon[2].Y + polygon[3].Y) / 2;
                g.DrawString(thelength.ToString() + " m", drawFont, drawBrush, thex - 15, they - 15);
                thelength = Math.Round(Math.Abs(TempYReal[1] - TempY12Real), 3);
                thex = (polygon[1].X + polygon[2].X) / 2;
                they = (polygon[1].Y + polygon[2].Y) / 2;
                g.DrawString(thelength.ToString() + " m", drawFont, drawBrush, thex - 15, they - 15);
                thex = (polygon[0].X + polygon[3].X) / 2;
                they = (polygon[0].Y + polygon[3].Y) / 2;
                g.DrawString(thelength.ToString() + " m", drawFont, drawBrush, thex - 15, they - 15);
                using (Brush brush = new SolidBrush(Color.FromArgb(30, Color.LightSkyBlue)))
                {
                    g.FillPolygon(brush, polygon);
                }
            }
            #endregion
            #region//رسم بلاطة بالنقاط
            if (toolStripButton8.Checked == true & Myglobals.drowclick == 1)
            {
                Graphics g = Graphics.FromImage(pictureBox3.Image);
                Pen pen = new Pen(Color.Red, 1f);
                float[] dashValues = { 5, 3, 5, 3 };
                pen = new Pen(Color.Gray, 1f);
                pen.DashPattern = dashValues;
                double thelength = 0;
                int thex = 0;
                int they = 0;
                Font drawFont = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
                SolidBrush drawBrush = new SolidBrush(Color.Blue);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                Point[] polygon = new Point[Shell.PointNumbersTemp + 2];

                for (int i = 0; i < Shell.PointNumbersTemp; i++)
                {
                    polygon[i].X = Myglobals.startX2d + Convert.ToInt32((Shell.PointXTemp[i + 1]) * Myglobals.Zoom2d);
                    polygon[i].Y = Myglobals.startY2d - Convert.ToInt32((Shell.PointYTemp[i + 1]) * Myglobals.Zoom2d);
                }
                for (int i = 0; i < Shell.PointNumbersTemp; i++)
                {
                    g.DrawString((i + 1).ToString(), drawFont, drawBrush, polygon[i].X - 15, polygon[i].Y - 15);
                }
                for (int i = 0; i < Shell.PointNumbersTemp - 1; i++)
                {
                    g.DrawLine(pen, polygon[i], polygon[i + 1]);
                    thelength = Math.Round(Math.Sqrt(Math.Pow(Shell.PointXTemp[i + 1] - Shell.PointXTemp[i + 2], 2) + Math.Pow(Shell.PointYTemp[i + 1] - Shell.PointYTemp[i + 2], 2)), 3);
                    thex = (polygon[i].X + polygon[i + 1].X) / 2;
                    they = (polygon[i].Y + polygon[i + 1].Y) / 2;
                    g.DrawString(thelength.ToString() + " m", drawFont, drawBrush, thex - 15, they - 15);
                }
                g.DrawString((Shell.PointNumbersTemp + 1).ToString(), drawFont, drawBrush, TempX - 15, TempY - 15);

                polygon[Shell.PointNumbersTemp].X = TempX;
                polygon[Shell.PointNumbersTemp].Y = TempY;
                polygon[Shell.PointNumbersTemp + 1] = polygon[0];
                g.DrawLine(pen, polygon[0], polygon[Shell.PointNumbersTemp]);
                g.DrawLine(pen, polygon[Shell.PointNumbersTemp - 1], polygon[Shell.PointNumbersTemp]);

                thelength = Math.Round(Math.Sqrt(Math.Pow(TempX12Real - Shell.PointXTemp[Shell.PointNumbersTemp], 2) + Math.Pow(TempY12Real - Shell.PointYTemp[Shell.PointNumbersTemp], 2)), 3);
                thex = (TempX + polygon[Shell.PointNumbersTemp - 1].X) / 2;
                they = (TempY + polygon[Shell.PointNumbersTemp - 1].Y) / 2;
                g.DrawString(thelength.ToString() + " m", drawFont, drawBrush, thex - 15, they - 15);

                thelength = Math.Round(Math.Sqrt(Math.Pow(TempX12Real - Shell.PointXTemp[1], 2) + Math.Pow(TempY12Real - Shell.PointYTemp[1], 2)), 3);
                thex = (TempX + polygon[0].X) / 2;
                they = (TempY + polygon[0].Y) / 2;
                g.DrawString(thelength.ToString() + " m", drawFont, drawBrush, thex - 15, they - 15);

                using (Brush brush = new SolidBrush(Color.FromArgb(30, Color.LightSkyBlue)))
                {
                    g.FillPolygon(brush, polygon);
                }
            }
            #endregion
            #region//تحديد البلاطة بالرسم السريع
            if (toolStripButton10.Checked == true & Myglobals.AriaSelected != 0)
            {
                Graphics g = Graphics.FromImage(pictureBox3.Image);
                Pen pen = new Pen(Color.Red, 2f);
                int i = Myglobals.AriaSelected;
                int N = Myglobals.AriaPointNo[i];
                Point[] polygon = new Point[N + 1];
                for (int j = 1; j < N + 1; j++)
                {
                    polygon[j].X = Myglobals.startX2d + Convert.ToInt32((Myglobals.p1X[i, j] * Myglobals.Zoom2d));
                    polygon[j].Y = Myglobals.startY2d - Convert.ToInt32((Myglobals.p1Y[i, j] * Myglobals.Zoom2d));
                }
                float[] dashValues = { 5, 3, 5, 3 };
                //pen = new Pen(Color.Black, 1f);
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
            pictureBox3Drawsub();
        }
        private void pictureBox3Drawsub()
        {
            Graphics g = Graphics.FromImage(pictureBox3.Image);
            Pen pen = new Pen(Color.Gray, 1f);
            Font drawFont = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
            SolidBrush drawBrush = new SolidBrush(Color.Blue);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            float[] dashValues = { 2, 3 };
            pen.DashPattern = dashValues;
            LineMoveX1 = Myglobals.startX2d + Convert.ToInt32((TempXReal[1]) * Myglobals.Zoom2d);
            LineMoveY1 = Myglobals.startY2d - Convert.ToInt32((TempYReal[1]) * Myglobals.Zoom2d);
            //
            if (Myglobals.LineMove2dVisible == 1)
            {
                if (toolStripButton11.Checked == true || toolStripButton3.Checked == true)
                {
                    g.DrawLine(pen, LineMoveX1, LineMoveY1, TempX, TempY);// رسم خط الجوائز
                    double thelength = Math.Round(Math.Sqrt(Math.Pow(TempXReal[1] - TempX12Real, 2) + Math.Pow(TempYReal[1] - TempY12Real, 2)), 3);
                    int thex = (TempX + LineMoveX1) / 2;
                    int they = (TempY + LineMoveY1) / 2;
                    g.DrawString(thelength.ToString() + " m", drawFont, drawBrush, thex - 15, they - 15);
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
            }
            g = Graphics.FromImage(pictureBox3.Image);//رسم نقطة تقاطع الشبكة
            pen = new Pen(Color.Red, 1f);
            if (Tahkik == 1 & SnapFromType == "GridIntersections")
            {
                g.DrawLine(pen, TempX + 5, TempY, TempX - 5, TempY);
                g.DrawLine(pen, TempX, TempY + 5, TempX, TempY - 5);
                drawBrush = new SolidBrush(Color.Black);
                g.DrawString("Grid Point " + GridPoint.Name2d[SelectedGridpoint].ToString(), drawFont, drawBrush, TempX + 10, TempY - 15);
            }
            if (Tahkik == 1 & SnapFromType == "Joints")
            {
                g.DrawLine(pen, TempX + 5, TempY + 5, TempX - 5, TempY + 5);
                g.DrawLine(pen, TempX + 5, TempY - 5, TempX - 5, TempY - 5);
                g.DrawLine(pen, TempX + 5, TempY - 5, TempX + 5, TempY + 5);
                g.DrawLine(pen, TempX - 5, TempY - 5, TempX - 5, TempY + 5);
                drawBrush = new SolidBrush(Color.Black);
                g.DrawString("Joint", drawFont, drawBrush, TempX + 10, TempY - 15);
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
        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            DROWcls callmee = new DROWcls();
            if (e.Button == MouseButtons.Middle)
            {
                if (JointAssignments == 1) Joint.Assignments = 1;
                if (BeamAssignments == 1) Frame.Assignments = 1;
                JointAssignments = 0;
                BeamAssignments = 0;
                callmee.Render2d();
                pictureBox3Draw();
            }

            # region//رسم البلاطة المستطيلة
            if (Myglobals.LineMove2dVisible == 1 & toolStripButton9.Checked == true)
            {
                Myglobals.LineMove2dVisible = 0;
                if (Myglobals.drowclick == 1)
                {
                    int x1 = e.Location.X;
                    int y1 = e.Location.Y;
                    TempXReal[1] = Math.Round((x1 - Myglobals.startX2d) / (Myglobals.Zoom2d), 3);
                    TempYReal[1] = Math.Round((Myglobals.startY2d - y1) / (Myglobals.Zoom2d), 3);
                    TempZReal[1] = Myglobals.StoryLevel[Myglobals.SelectedStory];
                    if (Tahkik == 1)
                    {
                        TahkikXY[1] = 1;
                        x1 = TempX;
                        y1 = TempY;
                        TempXReal[1] = TempX12Real;
                        TempYReal[1] = TempY12Real;
                    }
                    double tx1 = TempXReal[1];
                    double ty1 = -1 * TempYReal[1];
                    double tz1 = -1 * TempZReal[1];
                    Math3DP.PointG Point3dM = new Math3DP.PointG(tx1, ty1, tz1);
                    Point3dM.RotateX = Myglobals.tXValue;
                    Point3dM.RotateY = Myglobals.tYValue;
                    Point3dM.RotateZ = Myglobals.tZValue;
                    Point3dM.DrawPoint();
                    Shell.PointNumbersTemp = Shell.PointNumbersTemp + 1;
                    Shell.PointXTemp[Shell.PointNumbersTemp] = TempXReal[1];
                    Shell.PointYTemp[Shell.PointNumbersTemp] = Shell.PointYTemp[1];
                    Shell.PointZTemp[Shell.PointNumbersTemp] = TempZReal[1];
                    Shell.PointNumbersTemp = Shell.PointNumbersTemp + 1;
                    Shell.PointXTemp[Shell.PointNumbersTemp] = TempXReal[1];
                    Shell.PointYTemp[Shell.PointNumbersTemp] = TempYReal[1];
                    Shell.PointZTemp[Shell.PointNumbersTemp] = TempZReal[1];
                    Shell.PointNumbersTemp = Shell.PointNumbersTemp + 1;
                    Shell.PointXTemp[Shell.PointNumbersTemp] = Shell.PointXTemp[1];
                    Shell.PointYTemp[Shell.PointNumbersTemp] = TempYReal[1];
                    Shell.PointZTemp[Shell.PointNumbersTemp] = TempZReal[1];
                    toolStripButton8.Checked = false;
                    toolStripButton1.Checked = true;
                    if (Shell.PointNumbersTemp > 2)
                    {
                        Shell.Number = Shell.Number + 1;
                        Shell.PointNumbers[Shell.Number] = Shell.PointNumbersTemp;
                        int tah = 1;
                        for (int i = 1; i < Shell.PointNumbers[Shell.Number] + 1; i++)
                        {
                            double theX = Shell.PointXTemp[i];
                            double theY = Shell.PointYTemp[i];
                            double theZ = Shell.PointZTemp[i];
                            int tah1 = 0;
                            int selectedjoint = 0;
                            for (int J = 1; J < Joint.Number2d + 1; J++)
                            {
                                if (Joint.XReal[J] == theX & Joint.YReal[J] == theY & Joint.ZReal[J] == theZ)
                                {
                                    tah1 = 1;
                                    selectedjoint = J;
                                    break;
                                }
                            }
                            if (tah1 == 0)
                            {
                                Joint.Number2d = Joint.Number2d + 1;
                                selectedjoint = Joint.Number2d;
                                Joint.XReal[Joint.Number2d] = theX;
                                Joint.YReal[Joint.Number2d] = theY;
                                Joint.ZReal[Joint.Number2d] = theZ;
                            }
                            if (i < Shell.PointNumbers[Shell.Number])
                            {
                                if (Shell.PointZTemp[i] != Shell.PointZTemp[i + 1]) tah = 2;
                            }
                            Shell.JointNo[Shell.Number, i] = selectedjoint;
                            Shell.SelectedLine[Shell.Number, i] = 0;
                            Joint.FloorConnectionN[selectedjoint] = Joint.FloorConnectionN[selectedjoint] + 1;
                            int thecount = Joint.FloorConnectionN[selectedjoint];
                            Joint.Floor[selectedjoint, thecount] = Shell.Number;
                        }
                        Shell.Type[Shell.Number] = tah;
                        Shell.Section[Shell.Number] = Slab.SelectedToDraw;
                        callmee.SelectedShell = Shell.Number;
                        callmee.CalculatePropertiesShell();
                        callmee.CalculateShellMeshLines();
                        callmee.CalculateMeshJoints();
                        #region//رسم في كل الطوابق
                        if (Myglobals.AllStories == 1)
                        {
                            for (int add = 1; add < Myglobals.StoryNumbers + 1; add++)
                            {
                                if (add != Myglobals.SelectedStory)
                                {
                                    Shell.Number = Shell.Number + 1;
                                    Shell.PointNumbers[Shell.Number] = Shell.PointNumbersTemp;
                                    tah = 1;
                                    for (int i = 1; i < Shell.PointNumbers[Shell.Number] + 1; i++)
                                    {
                                        double theX = Shell.PointXTemp[i];
                                        double theY = Shell.PointYTemp[i];
                                        double theZ = Myglobals.StoryLevel[add];
                                        int tah1 = 0;
                                        int selectedjoint = 0;
                                        for (int J = 1; J < Joint.Number2d + 1; J++)
                                        {
                                            if (Joint.XReal[J] == theX & Joint.YReal[J] == theY & Joint.ZReal[J] == theZ)
                                            {
                                                tah1 = 1;
                                                selectedjoint = J;
                                                break;
                                            }
                                        }
                                        if (tah1 == 0)
                                        {
                                            Joint.Number2d = Joint.Number2d + 1;
                                            selectedjoint = Joint.Number2d;
                                            Joint.XReal[Joint.Number2d] = theX;
                                            Joint.YReal[Joint.Number2d] = theY;
                                            Joint.ZReal[Joint.Number2d] = theZ;
                                        }
                                        if (i < Shell.PointNumbers[Shell.Number])
                                        {
                                            if (Shell.PointZTemp[i] != Shell.PointZTemp[i + 1]) tah = 2;
                                        }
                                        Shell.JointNo[Shell.Number, i] = selectedjoint;
                                        Shell.SelectedLine[Shell.Number, i] = 0;
                                        Joint.FloorConnectionN[selectedjoint] = Joint.FloorConnectionN[selectedjoint] + 1;
                                        int thecount = Joint.FloorConnectionN[selectedjoint];
                                        Joint.Floor[selectedjoint, thecount] = Shell.Number;
                                    }
                                    Shell.Type[Shell.Number] = tah;
                                    Shell.Section[Shell.Number] = Slab.SelectedToDraw;
                                    callmee.SelectedShell = Shell.Number;
                                    callmee.CalculatePropertiesShell();
                                    callmee.CalculateShellMeshLines();
                                    callmee.CalculateMeshJoints();
                                }
                            }
                        }
                        #endregion
                        callmee.Render2d();
                        callmee.Render3d();
                        callmee.Renderelev();
                        MakeTempFiles();
                    }
                    Shell.PointNumbersTemp = 0;
                    Shell.LineNumberTemp = 0;
                    Myglobals.drowclick = 0;
                    toolStripButton9.Checked = false;
                }
                goto endloop;
            }
            if (toolStripButton9.Checked == true)
            {
                Shell.PointNumbersTemp = 0;
                Shell.LineNumberTemp = 0;
                Myglobals.drowclick = 0;
                toolStripButton9.Checked = false;
            }
            #endregion
            #region//تحديد العناصر مع مربع التحديد
            if (MouseButtonsLeft == 1 & toolStripButton1.Checked == true)
            {
                MouseButtonsLeft = 0;
                int thecase = 0;
                if (e.X >= Myglobals.xmove1) thecase = 1;//تحديد العناصر الواقعة ضمن المربع تماما
                if (e.X < Myglobals.xmove1) thecase = 2;//تحديد العناصر المتقاطعة 
                int MaxX = 0;
                int MaxY = 0;
                int MinX = 0;
                int MinY = 0;
                MaxX = Myglobals.xmove1;
                MinX = Myglobals.xmove1;
                if (e.X > MaxX) MaxX = e.X;
                if (e.X < MinX) MinX = e.X;
                MaxY = Myglobals.ymove1;
                MinY = Myglobals.ymove1;
                if (e.Y > MaxY) MaxY = e.Y;
                if (e.Y < MinY) MinY = e.Y;
                int ifselected = 0;
                #region//تحديد العقد
                for (int i = 1; i < Joint.Number3d + 1; i++)
                {
                    if (Joint.ZReal[i] == Myglobals.StoryLevel[Myglobals.SelectedStory])
                    {
                        int Tah = 0;
                        int X3 = Joint.X2d[i];
                        int Y3 = Joint.Y2d[i];
                        if (X3 >= MinX & X3 <= MaxX & Y3 >= MinY & Y3 <= MaxY) Tah = 1;
                        if (Tah == 1)
                        {
                            ifselected = 1;
                            Joint.Selected[i] = 1;
                        }
                    }
                }
                #endregion
                # region //تحديد الجوائز
                for (int i = 1; i < Frame.Number + 1; i++)
                {
                    if (FrameElement[i].Visible == 0)
                    {
                        //جائز
                        if (Joint.ZReal[FrameElement[i].FirstJoint] == Myglobals.StoryLevel[Myglobals.SelectedStory] & Joint.ZReal[FrameElement[i].SecondJoint] == Myglobals.StoryLevel[Myglobals.SelectedStory])
                        {
                            int Tah1 = 0;
                            int Tah2 = 0;
                            int X3 = Joint.X2d[FrameElement[i].FirstJoint];
                            int Y3 = Joint.Y2d[FrameElement[i].FirstJoint];
                            int X4 = Joint.X2d[FrameElement[i].SecondJoint];
                            int Y4 = Joint.Y2d[FrameElement[i].SecondJoint];
                            if (X3 >= MinX & X3 <= MaxX & Y3 >= MinY & Y3 <= MaxY) Tah1 = 1;
                            if (X4 >= MinX & X4 <= MaxX & Y4 >= MinY & Y4 <= MaxY) Tah2 = 1;
                            if (thecase == 1)
                            {
                                if (Tah1 == 1 & Tah2 == 1)
                                {
                                    ifselected = 1;
                                    FrameElement[i].Selected = 1;
                                }
                            }
                            if (thecase == 2)
                            {
                                if (Tah1 == 1 || Tah2 == 1)
                                {
                                    ifselected = 1;
                                    FrameElement[i].Selected = 1;
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
                                    FrameElement[i].Selected = 1;
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
                                    FrameElement[i].Selected = 1;
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
                                    FrameElement[i].Selected = 1;
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
                                    FrameElement[i].Selected = 1;
                                    goto end100;
                                }
                            }
                        end100: { }
                        }
                        //عمود
                        if (Joint.XReal[FrameElement[i].FirstJoint] == Joint.XReal[FrameElement[i].SecondJoint] & Joint.YReal[FrameElement[i].FirstJoint] == Joint.YReal[FrameElement[i].SecondJoint])
                        {
                            double MaxZ = Joint.ZReal[FrameElement[i].FirstJoint];
                            if (MaxZ < Joint.ZReal[FrameElement[i].SecondJoint]) MaxZ = Joint.ZReal[FrameElement[i].SecondJoint];
                            if (MaxZ == Myglobals.StoryLevel[Myglobals.SelectedStory])
                            {
                                int Tah1 = 0;
                                int Tah2 = 0;
                                int X3 = Joint.X2d[FrameElement[i].FirstJoint];
                                int Y3 = Joint.Y2d[FrameElement[i].FirstJoint];
                                int X4 = Joint.X2d[FrameElement[i].SecondJoint];
                                int Y4 = Joint.Y2d[FrameElement[i].SecondJoint];
                                if (X3 >= MinX & X3 <= MaxX & Y3 >= MinY & Y3 <= MaxY) Tah1 = 1;
                                if (X4 >= MinX & X4 <= MaxX & Y4 >= MinY & Y4 <= MaxY) Tah2 = 1;
                                if (thecase == 1)
                                {
                                    if (Tah1 == 1 & Tah2 == 1)
                                    {
                                        ifselected = 1;
                                        FrameElement[i].Selected = 1;
                                    }
                                }
                                if (thecase == 2)
                                {
                                    if (Tah1 == 1 || Tah2 == 1)
                                    {
                                        ifselected = 1;
                                        FrameElement[i].Selected = 1;
                                        goto end100;
                                    }
                                }
                            end100: { }
                            }
                        }
                    }
                }
                #endregion
                # region //تحديد البلاطات
                for (int i = 1; i < Shell.Number + 1; i++)
                {
                    if (Shell.Visible[i] == 0)
                    {
                        if (Shell.Type[i] == 1 & Joint.ZReal[Shell.JointNo[i, 1]] == Myglobals.StoryLevel[Myglobals.SelectedStory])
                        {
                            for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                            {
                                int Tah1 = 0;
                                int Tah2 = 0;
                                int X3 = Joint.X2d[Shell.JointNo[i, j]];
                                int Y3 = Joint.Y2d[Shell.JointNo[i, j]];
                                int X4 = 0;
                                int Y4 = 0;
                                if (j != Shell.PointNumbers[i])
                                {
                                    X4 = Joint.X2d[Shell.JointNo[i, j + 1]];
                                    Y4 = Joint.Y2d[Shell.JointNo[i, j + 1]];
                                }
                                else
                                {
                                    X4 = Joint.X2d[Shell.JointNo[i, 1]];
                                    Y4 = Joint.Y2d[Shell.JointNo[i, 1]];
                                }
                                if (X3 >= MinX & X3 <= MaxX & Y3 >= MinY & Y3 <= MaxY) Tah1 = 1;
                                if (X4 >= MinX & X4 <= MaxX & Y4 >= MinY & Y4 <= MaxY) Tah2 = 1;
                                if (thecase == 1)
                                {
                                    if (Tah1 == 1 & Tah2 == 1)
                                    {
                                        ifselected = 1;
                                        Shell.SelectedLine[i, j] = 1;
                                    }
                                }
                                if (thecase == 2)
                                {
                                    if (Tah1 == 1 || Tah2 == 1)
                                    {
                                        ifselected = 1;
                                        Shell.SelectedLine[i, j] = 1;
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
                                        Shell.SelectedLine[i, j] = 1;
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
                                        Shell.SelectedLine[i, j] = 1;
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
                                        Shell.SelectedLine[i, j] = 1;
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
                                        Shell.SelectedLine[i, j] = 1;
                                        goto end100;
                                    }
                                }
                            end100: { }
                            }
                        }
                    }
                }
                #endregion
                if (ifselected == 1)
                {
                    callmee.Render2d();
                    callmee.Render3d();
                    callmee.Renderelev();
                }
                Bitmap finalBmp = new Bitmap(Myglobals.BitampWidth2d, Myglobals.BitampHight2d);
                if (pictureBox3.Image != null)
                {
                    pictureBox3.Image.Dispose();
                    pictureBox3.Image = null;
                }
                pictureBox3.Image = finalBmp;
            }
            #endregion
        endloop: { }
        }
        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            Bitmap finalBmp = new Bitmap(Myglobals.BitampWidth2d, Myglobals.BitampHight2d);
            if (pictureBox3.Image != null)
            {
                pictureBox3.Image.Dispose();
                pictureBox3.Image = null;
            }
            pictureBox3.Image = finalBmp;
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            tabPage1.BorderStyle = BorderStyle.FixedSingle;
            tabPage2.BorderStyle = BorderStyle.None;
            tabPage3.BorderStyle = BorderStyle.None;
            Myglobals.SelectedPlan = "Plan";
        }
        #endregion
        #region//pictureBox4
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            tabPage2.BorderStyle = BorderStyle.FixedSingle;
            tabPage1.BorderStyle = BorderStyle.None;
            tabPage3.BorderStyle = BorderStyle.None;
            Myglobals.SelectedPlan = "3D";
        }
        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            tabPage2.BorderStyle = BorderStyle.FixedSingle;
            tabPage1.BorderStyle = BorderStyle.None;
            tabPage3.BorderStyle = BorderStyle.None;
            Myglobals.SelectedPlan = "3D";
            Myglobals.xmove = Cursor.Position.X;
            Myglobals.ymove = Cursor.Position.Y;
            Myglobals.xmove1 = e.X;
            Myglobals.ymove1 = e.Y;
            int ifselected = 0;
            if (Myglobals.Rotate3DVeiw == 0)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left || e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    if (toolStripButton1.Checked == true)//تحديد العناصر
                    {
                        #region//تحديد العقد
                        for (int i = 1; i < Joint.Number3d + 1; i++)
                        {
                            if (Math.Abs(e.X - Joint.X3d[i]) < 4 & Math.Abs(e.Y - Joint.Y3d[i]) < 4)
                            {
                                ifselected = 1;
                                if (Joint.Selected[i] == 1)
                                {
                                    Joint.Selected[i] = 0;
                                }
                                else
                                {
                                    Joint.Selected[i] = 1;
                                }
                                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                                {
                                    Joint.Selected[i] = 1;
                                    Joint.SelectedforProp = i;
                                    ifselected = 0;
                                    DROWcls callmee = new DROWcls();
                                    callmee.Render2d();
                                    callmee.Render3d();
                                    callmee.Renderelev();
                                    JointInformationForm informationForm = new JointInformationForm();
                                    informationForm.ShowDialog();
                                }
                                goto endloop1;
                            }
                        }
                        #endregion
                        # region//تحديد العناصر الخطية
                        for (int i = 1; i < Frame.Number + 1; i++)
                        {
                            if (FrameElement[i].Visible == 0)
                            {
                                //جائز
                                int X1 = Joint.X3d[FrameElement[i].FirstJoint];
                                int Y1 = Joint.Y3d[FrameElement[i].FirstJoint];
                                int X2 = Joint.X3d[FrameElement[i].SecondJoint];
                                int Y2 = Joint.Y3d[FrameElement[i].SecondJoint];
                                int X = e.X;
                                int Y = e.Y;
                                double distance = 100000;
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
                                if (distance < 0.1 * Myglobals.Zoom2d)
                                {
                                    ifselected = 1;
                                    if (FrameElement[i].Selected == 1)
                                    {
                                        FrameElement[i].Selected = 0;
                                    }
                                    else
                                    {
                                        FrameElement[i].Selected = 1;
                                    }
                                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                                    {
                                        FrameElement[i].Selected = 1;
                                        Frame.SelectedforProp = i;
                                        ifselected = 0;
                                        DROWcls callmee = new DROWcls();
                                        callmee.Render2d();
                                        callmee.Render3d();
                                        callmee.Renderelev();
                                        BeamInformationForm beamInformationForm = new BeamInformationForm();
                                        beamInformationForm.ShowDialog();
                                    }
                                    goto endloop1;
                                }
                            }
                        }
                        #endregion
                        # region//تحديد البلاطات بخطوطها
                        for (int i = 1; i < Shell.Number + 1; i++)
                        {
                            if (Shell.Visible[i] == 0)
                            {
                                for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                                {
                                    int X1 = Joint.X3d[Shell.JointNo[i, j]];
                                    int Y1 = Joint.Y3d[Shell.JointNo[i, j]];
                                    int X2 = 0;
                                    int Y2 = 0;
                                    if (j != Shell.PointNumbers[i])
                                    {
                                        X2 = Joint.X3d[Shell.JointNo[i, j + 1]];
                                        Y2 = Joint.Y3d[Shell.JointNo[i, j + 1]];
                                    }
                                    else
                                    {
                                        X2 = Joint.X3d[Shell.JointNo[i, 1]];
                                        Y2 = Joint.Y3d[Shell.JointNo[i, 1]];
                                    }
                                    int X = e.Location.X;
                                    int Y = e.Location.Y;
                                    double distance = 100000;
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
                                    if (distance < 0.1 * Myglobals.Zoom2d)
                                    {
                                        ifselected = 1;
                                        if (Shell.SelectedLine[i, j] == 1)
                                        {
                                            Shell.SelectedLine[i, j] = 0;
                                        }
                                        else
                                        {
                                            Shell.SelectedLine[i, j] = 1;
                                        }
                                        if (e.Button == System.Windows.Forms.MouseButtons.Right)
                                        {
                                            Shell.SelectedLine[i, j] = 1;
                                            Shell.SelectedforProp = i;
                                            ifselected = 0;
                                            DROWcls callmee = new DROWcls();
                                            callmee.Render2d();
                                            callmee.Render3d();
                                            callmee.Renderelev();
                                            SlabInformationForm slabInformationForm = new SlabInformationForm();
                                            slabInformationForm.ShowDialog();
                                        }
                                        goto endloop1;
                                    }
                                }
                            }
                        }
                        #endregion
                        # region//تحديد البلاطات بالنقر ضمنها
                        int Xtest = e.X;
                        int Ytest = e.Y;
                        #region//لبفتحات أولا
                        for (int i = 1; i < Shell.Number + 1; i++)
                        {
                            if (Shell.Visible[i] == 0 & Shell.Type[i] == 4)
                            {
                                int N = Shell.PointNumbers[i] + 1;
                                Point[] polygon = new Point[N + 1];
                                for (int j = 1; j < N; j++)
                                {
                                    polygon[j].X = Joint.X3d[Shell.JointNo[i, j]];
                                    polygon[j].Y = Joint.Y3d[Shell.JointNo[i, j]];
                                }
                                polygon[N].X = polygon[1].X;
                                polygon[N].Y = polygon[1].Y;
                                bool result = false;
                                int nvert = N;
                                int k, l;
                                for (k = 1, l = nvert - 1; k < nvert; l = k++)
                                {
                                    if (((polygon[k].Y > Ytest) != (polygon[l].Y > Ytest)) &&
                                     (Xtest < (polygon[l].X - polygon[k].X) * (Ytest - polygon[k].Y) / (polygon[l].Y - polygon[k].Y) + polygon[k].X))
                                        result = !result;
                                }
                                if (result == true)
                                {
                                    ifselected = 1;
                                    int tah = 0;
                                    for (int j = 1; j < N; j++)
                                    {
                                        if (Shell.SelectedLine[i, j] == 1)
                                        {
                                            tah = 1;
                                            break;
                                        }
                                    }
                                    for (int j = 1; j < N; j++)
                                    {
                                        if (tah == 1)
                                        {
                                            Shell.SelectedLine[i, j] = 0;
                                        }
                                        else
                                        {
                                            Shell.SelectedLine[i, j] = 1;
                                        }
                                    }
                                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                                    {
                                        for (int j = 1; j < N; j++)
                                        {
                                            Shell.SelectedLine[i, j] = 1;
                                        }
                                        Shell.SelectedforProp = i;
                                        ifselected = 0;
                                        DROWcls callmee = new DROWcls();
                                        callmee.Render2d();
                                        callmee.Render3d();
                                        callmee.Renderelev();
                                        SlabInformationForm slabInformationForm = new SlabInformationForm();
                                        slabInformationForm.ShowDialog();
                                    }
                                    goto endloop1;
                                }
                            }
                        }
                        #endregion
                        #region//الجدران و الباطات ثانيا
                        for (int i = 1; i < Shell.Number + 1; i++)
                        {
                            if (Shell.Visible[i] == 0 & Shell.Type[i] != 4)
                            {
                                int N = Shell.PointNumbers[i] + 1;
                                Point[] polygon = new Point[N + 1];
                                for (int j = 1; j < N; j++)
                                {
                                    polygon[j].X = Joint.X3d[Shell.JointNo[i, j]];
                                    polygon[j].Y = Joint.Y3d[Shell.JointNo[i, j]];
                                }
                                polygon[N].X = polygon[1].X;
                                polygon[N].Y = polygon[1].Y;
                                bool result = false;
                                int nvert = N;
                                int k, l;
                                for (k = 1, l = nvert - 1; k < nvert; l = k++)
                                {
                                    if (((polygon[k].Y > Ytest) != (polygon[l].Y > Ytest)) &&
                                     (Xtest < (polygon[l].X - polygon[k].X) * (Ytest - polygon[k].Y) / (polygon[l].Y - polygon[k].Y) + polygon[k].X))
                                        result = !result;
                                }
                                if (result == true)
                                {
                                    ifselected = 1;
                                    int tah = 0;
                                    for (int j = 1; j < N; j++)
                                    {
                                        if (Shell.SelectedLine[i, j] == 1)
                                        {
                                            tah = 1;
                                            break;
                                        }
                                    }
                                    for (int j = 1; j < N; j++)
                                    {
                                        if (tah == 1)
                                        {
                                            Shell.SelectedLine[i, j] = 0;
                                        }
                                        else
                                        {
                                            Shell.SelectedLine[i, j] = 1;
                                        }
                                    }
                                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                                    {
                                        for (int j = 1; j < N; j++)
                                        {
                                            Shell.SelectedLine[i, j] = 1;
                                        }
                                        Shell.SelectedforProp = i;
                                        ifselected = 0;
                                        DROWcls callmee = new DROWcls();
                                        callmee.Render2d();
                                        callmee.Render3d();
                                        callmee.Renderelev();
                                        SlabInformationForm slabInformationForm = new SlabInformationForm();
                                        slabInformationForm.ShowDialog();
                                    }
                                    goto endloop1;
                                }
                            }
                        }
                        #endregion

                        #endregion
                    endloop1: { };
                        if (ifselected == 1)
                        {
                            DROWcls callmee = new DROWcls();
                            callmee.Render2d();
                            callmee.Render3d();
                            callmee.Renderelev();
                        }
                    }
                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        #region رسم بلاطة
                        if (toolStripButton8.Checked == true & Tahkik == 1)
                        {
                            if (Myglobals.drowclick == 0)
                            {
                                TempXReal[1] = TempX12Real;
                                TempYReal[1] = TempY12Real;
                                TempZReal[1] = TempZ12Real;
                                TahkikXY[1] = 1;
                                int x1 = TempX;
                                int y1 = TempY;
                                Myglobals.LineMove3dVisible = 1;
                                Myglobals.LineMove3dFVisible = 1;
                                Shell.PointNumbersTemp = Shell.PointNumbersTemp + 1;
                                Shell.PointXTemp[Shell.PointNumbersTemp] = TempXReal[1];
                                Shell.PointYTemp[Shell.PointNumbersTemp] = TempYReal[1];
                                Shell.PointZTemp[Shell.PointNumbersTemp] = TempZReal[1];
                                Myglobals.drowclick = 1;
                                for (int add = 0; add < Myglobals.StoryNumbers + 1; add++)
                                {
                                    if (TempZReal[1] == Myglobals.StoryLevel[add])
                                    {
                                        SelectedStorytodrowallstory = add;
                                        break;
                                    }
                                }
                                goto sdd;
                            }
                            if (Myglobals.drowclick == 1)
                            {
                                TahkikXY[2] = 1;
                                TempXReal[2] = TempX12Real;
                                TempYReal[2] = TempY12Real;
                                TempZReal[2] = TempZ12Real;
                                int x1 = TempX;
                                int y1 = TempY;
                                int x2 = TempX;
                                int y2 = TempY;
                                Shell.LineNumberTemp = Shell.LineNumberTemp + 1;
                                double theX = TempXReal[1];
                                double theY = TempYReal[1];
                                double theZ = TempZReal[1];
                                Myglobals.LineType = 5;
                                double tx1 = theX;
                                double ty1 = -1 * theY;
                                double tz1 = -1 * theZ;
                                Math3DP.PointG Point3dM = new Math3DP.PointG(tx1, ty1, tz1);
                                Point3dM.RotateX = Myglobals.tXValue;
                                Point3dM.RotateY = Myglobals.tYValue;
                                Point3dM.RotateZ = Myglobals.tZValue;
                                Point3dM.DrawPoint();

                                theX = TempXReal[2];
                                theY = TempYReal[2];
                                theZ = TempZReal[2];
                                tx1 = theX;
                                ty1 = -1 * theY;
                                tz1 = -1 * theZ;
                                Point3dM = new Math3DP.PointG(tx1, ty1, tz1);
                                Point3dM.RotateX = Myglobals.tXValue;
                                Point3dM.RotateY = Myglobals.tYValue;
                                Point3dM.RotateZ = Myglobals.tZValue;
                                Point3dM.DrawPoint();

                                x1 = Myglobals.startX2d + Convert.ToInt32((TempXReal[1]) * Myglobals.Zoom2d);
                                y1 = Myglobals.startY2d - Convert.ToInt32((TempYReal[1]) * Myglobals.Zoom2d);
                                x2 = Myglobals.startX2d + Convert.ToInt32((TempXReal[2]) * Myglobals.Zoom2d);
                                y2 = Myglobals.startY2d - Convert.ToInt32((TempYReal[2]) * Myglobals.Zoom2d);

                                Shell.PointNumbersTemp = Shell.PointNumbersTemp + 1;
                                Shell.PointXTemp[Shell.PointNumbersTemp] = theX;
                                Shell.PointYTemp[Shell.PointNumbersTemp] = theY;
                                Shell.PointZTemp[Shell.PointNumbersTemp] = theZ;
                                TahkikXY[1] = 1;
                                TempXReal[1] = TempXReal[2];
                                TempYReal[1] = TempYReal[2];
                                TempZReal[1] = TempZReal[2];
                                TahkikXY[2] = 0;
                            }
                        sdd: { }
                        }
                        #endregion
                        #region رسم جوائز
                        if (toolStripButton3.Checked == true & Tahkik == 1)
                        {
                            if (Myglobals.drowclick == 0)
                            {
                                TempXReal[1] = Math.Round(TempX12Real, 3);
                                TempYReal[1] = Math.Round(TempY12Real, 3);
                                TempZReal[1] = Math.Round(TempZ12Real, 3);
                                TahkikXY[1] = 1;
                                int x1 = TempX;
                                int y1 = TempY;
                                Myglobals.LineMove3dVisible = 1;
                                Myglobals.LineMove2dVisible = 1;
                                Myglobals.LineMoveelevVisible = 1;
                                Myglobals.drowclick = 1;
                                for (int add = 0; add < Myglobals.StoryNumbers + 1; add++)
                                {
                                    if (TempZReal[1] == Myglobals.StoryLevel[add])
                                    {
                                        SelectedStorytodrowallstory = add;
                                        break;
                                    }
                                }
                                goto sdd;
                            }
                            if (Myglobals.drowclick == 1)
                            {
                                TahkikXY[2] = 1;
                                TempXReal[2] = Math.Round(TempX12Real, 3);
                                TempYReal[2] = Math.Round(TempY12Real, 3);
                                TempZReal[2] = Math.Round(TempZ12Real, 3);
                                int x2 = TempX;
                                int y2 = TempY;
                                Frame.Number = Frame.Number + 1;
                                FrameElements emp = new FrameElements();
                                FrameElement[Frame.Number] = emp;
                                FrameElement[Frame.Number].Selected = 0;
                                double theX = Math.Round(TempXReal[1], 3);
                                double theY = Math.Round(TempYReal[1], 3);
                                double theZ = Math.Round(TempZReal[1], 3);
                                int tah = 0;
                                int selectedjoint = 0;
                                for (int i = 1; i < Joint.Number2d + 1; i++)
                                {
                                    if (Joint.XReal[i] == theX & Joint.YReal[i] == theY & Joint.ZReal[i] == theZ)
                                    {
                                        tah = 1;
                                        selectedjoint = i;
                                        break;
                                    }
                                }
                                if (tah == 0)
                                {
                                    Joint.Number2d = Joint.Number2d + 1;
                                    selectedjoint = Joint.Number2d;
                                    Joint.XReal[Joint.Number2d] = theX;
                                    Joint.YReal[Joint.Number2d] = theY;
                                    Joint.ZReal[Joint.Number2d] = theZ;
                                }
                                FrameElement[Frame.Number].FirstJoint= selectedjoint;
                                theX = TempXReal[2];
                                theY = TempYReal[2];
                                theZ = TempZReal[2];
                                tah = 0;
                                selectedjoint = 0;
                                for (int i = 1; i < Joint.Number2d + 1; i++)
                                {
                                    if (Joint.XReal[i] == theX & Joint.YReal[i] == theY & Joint.ZReal[i] == theZ)
                                    {
                                        tah = 1;
                                        selectedjoint = i;
                                        break;
                                    }
                                }
                                if (tah == 0)
                                {
                                    Joint.Number2d = Joint.Number2d + 1;
                                    selectedjoint = Joint.Number2d;
                                    Joint.XReal[Joint.Number2d] = theX;
                                    Joint.YReal[Joint.Number2d] = theY;
                                    Joint.ZReal[Joint.Number2d] = theZ;
                                }
                                FrameElement[Frame.Number].SecondJoint= selectedjoint;
                                FrameElement[Frame.Number].Section= Section.SelectedToDraw;
                                FrameElement[Frame.Number].RotateAngel= Myglobals.RotateAngelDraw;
                                int thejoint = FrameElement[Frame.Number].FirstJoint;
                                Joint.BeamConnectionN[thejoint] = Joint.BeamConnectionN[thejoint] + 1;
                                int thecount = Joint.BeamConnectionN[thejoint];
                                Joint.Beam[thejoint, thecount] = Frame.Number;
                                thejoint = FrameElement[Frame.Number].SecondJoint;
                                Joint.BeamConnectionN[thejoint] = Joint.BeamConnectionN[thejoint] + 1;
                                thecount = Joint.BeamConnectionN[thejoint];
                                Joint.Beam[thejoint, thecount] = Frame.Number;

                                TahkikXY[1] = 1;
                                TempXReal[1] = TempXReal[2];
                                TempYReal[1] = TempYReal[2];
                                TempZReal[1] = TempZReal[2];
                                TahkikXY[2] = 0;
                                #region//رسم في كل الطوابق
                                if (Myglobals.AllStories == 1)
                                {
                                    double theX1 = Joint.XReal[FrameElement[Frame.Number].FirstJoint];
                                    double theY1 = Joint.YReal[FrameElement[Frame.Number].FirstJoint];
                                    double theX2 = Joint.XReal[FrameElement[Frame.Number].SecondJoint];
                                    double theY2 = Joint.YReal[FrameElement[Frame.Number].SecondJoint];
                                    double theZ01 = Joint.ZReal[FrameElement[Frame.Number].FirstJoint];
                                    double theZ02 = Joint.ZReal[FrameElement[Frame.Number].SecondJoint];
                                    double theZ1 = 0;
                                    double theZ2 = 0;
                                    double delataZ1 = 0;
                                    double delataZ2 = 0;
                                    delataZ1 = theZ01 - Myglobals.StoryLevel[SelectedStorytodrowallstory];
                                    delataZ2 = theZ02 - Myglobals.StoryLevel[SelectedStorytodrowallstory];
                                    for (int add = 0; add < Myglobals.StoryNumbers + 1; add++)///من الاول حتى الاحير عدا المطلوب  
                                    {
                                        if (theZ01 == theZ02 & add == 0) goto nextadd;//جائز لاترسمه في الارضي
                                        if (add == Myglobals.StoryNumbers & Myglobals.StoryLevel[add] + delataZ1 > Myglobals.StoryLevel[Myglobals.StoryNumbers]) goto nextadd;
                                        if (add == Myglobals.StoryNumbers & Myglobals.StoryLevel[add] + delataZ2 > Myglobals.StoryLevel[Myglobals.StoryNumbers]) goto nextadd;
                                        if (add == 0 & Myglobals.StoryLevel[add] + delataZ1 < 0) goto nextadd;
                                        if (add == 0 & Myglobals.StoryLevel[add] + delataZ2 < 0) goto nextadd;
                                        if (add != SelectedStorytodrowallstory)
                                        {
                                            Frame.Number = Frame.Number + 1;
                                            theZ1 = Myglobals.StoryLevel[add] + delataZ1;
                                            theZ2 = Myglobals.StoryLevel[add] + delataZ2;
                                            tah = 0;
                                            selectedjoint = 0;
                                            for (int i = 1; i < Joint.Number2d + 1; i++)
                                            {
                                                if (Joint.XReal[i] == theX1 & Joint.YReal[i] == theY1 & Joint.ZReal[i] == theZ1)
                                                {
                                                    tah = 1;
                                                    selectedjoint = i;
                                                    break;
                                                }
                                            }
                                            if (tah == 0)
                                            {
                                                Joint.Number2d = Joint.Number2d + 1;
                                                selectedjoint = Joint.Number2d;
                                                Joint.XReal[Joint.Number2d] = theX1;
                                                Joint.YReal[Joint.Number2d] = theY1;
                                                Joint.ZReal[Joint.Number2d] = theZ1;
                                            }
                                            FrameElement[Frame.Number].FirstJoint= selectedjoint;
                                            tah = 0;
                                            selectedjoint = 0;
                                            for (int i = 1; i < Joint.Number2d + 1; i++)
                                            {
                                                if (Joint.XReal[i] == theX2 & Joint.YReal[i] == theY2 & Joint.ZReal[i] == theZ2)
                                                {
                                                    tah = 1;
                                                    selectedjoint = i;
                                                    break;
                                                }
                                            }
                                            if (tah == 0)
                                            {
                                                Joint.Number2d = Joint.Number2d + 1;
                                                selectedjoint = Joint.Number2d;
                                                Joint.XReal[Joint.Number2d] = theX2;
                                                Joint.YReal[Joint.Number2d] = theY2;
                                                Joint.ZReal[Joint.Number2d] = theZ2;
                                            }
                                            FrameElement[Frame.Number].SecondJoint= selectedjoint;
                                            FrameElement[Frame.Number].Section= Section.SelectedToDraw;
                                            FrameElement[Frame.Number].RotateAngel= Myglobals.RotateAngelDraw;
                                            thejoint = FrameElement[Frame.Number].FirstJoint;
                                            Joint.BeamConnectionN[thejoint] = Joint.BeamConnectionN[thejoint] + 1;
                                            thecount = Joint.BeamConnectionN[thejoint];
                                            Joint.Beam[thejoint, thecount] = Frame.Number;
                                            thejoint = FrameElement[Frame.Number].SecondJoint;
                                            Joint.BeamConnectionN[thejoint] = Joint.BeamConnectionN[thejoint] + 1;
                                            thecount = Joint.BeamConnectionN[thejoint];
                                            Joint.Beam[thejoint, thecount] = Frame.Number;
                                        }
                                    nextadd: { }
                                    }
                                }
                                #endregion
                                for (int add = 0; add < Myglobals.StoryNumbers + 1; add++)
                                {
                                    if (TempZReal[2] == Myglobals.StoryLevel[add])
                                    {
                                        SelectedStorytodrowallstory = add;
                                        break;
                                    }
                                }
                                DROWcls callmee = new DROWcls();
                                callmee.Render2d();
                                callmee.Render3d();
                                callmee.Renderelev();
                                MakeTempFiles();
                            }
                        sdd: { }
                        }
                        #endregion
                    }
                }
            }
        }
        private void pictureBox4_MouseMove(object sender, MouseEventArgs e)
        {
            lastzoomX3d = e.X;
            lastzoomY3d = e.Y;
            LBLX.Visible = false;
            LBLY.Visible = false;
            LBLZ.Visible = false;
            #region//تحريك المسقط
            if (e.Button == MouseButtons.Middle & ShiftPress == 0)
            {
                timetodo = timetodo + 1;
                Myglobals.startX3d = Myglobals.startX3d + (Cursor.Position.X - Myglobals.xmove);
                Myglobals.startY3d = Myglobals.startY3d + (Cursor.Position.Y - Myglobals.ymove);
                Myglobals.xmove = Cursor.Position.X;
                Myglobals.ymove = Cursor.Position.Y;
                if (timetodo > 2)
                {
                    if (Joint.Assignments == 1) JointAssignments = 1;
                    if (Frame.Assignments == 1) BeamAssignments = 1;
                    if (Myglobals.ExtrudedShell == 1) ExtrudedShell = 1;
                    if (Myglobals.ExtrudedFrame == 1) ExtrudedFrame = 1;
                    if (Joint.ShowPower == 1) JointShowPower = 1;
                    if (Frame.ShowPower == 1) FrameShowPower = 1;
                    Joint.ShowPower = 0;
                    Frame.ShowPower = 0;
                    Joint.Assignments = 0;
                    Frame.Assignments = 0;
                    Myglobals.ExtrudedShell = 0;
                    Myglobals.ExtrudedFrame = 0;
                    DROWcls callmee = new DROWcls();
                    callmee.Render3d();
                    timetodo = 0;
                    Bitmap finalBmp = new Bitmap(Myglobals.BitampWidth3d, Myglobals.BitampHight3d);
                    if (pictureBox4.Image != null)
                    {
                        pictureBox4.Image.Dispose();
                        pictureBox4.Image = null;
                    }
                    pictureBox4.Image = finalBmp;
                }
                goto ENDLOOP;
            }
            #endregion
            #region//تدوير المسقط
            int tadweer = 0;
            if (e.Button == MouseButtons.Middle & ShiftPress == 1)
            {
                Myglobals.Rotate3DVeiw = 1;
                tadweer = 1;
            }
            if (e.Button == MouseButtons.Left & Myglobals.Rotate3DVeiw == 1) tadweer = 1;
            if (tadweer == 1)
            {
                int thex = e.X;
                int they = e.Y;
                int difx = (thex - Myglobals.xmove1);
                int dify = (they - Myglobals.ymove1);
                double tah = Math.PI / 180;

                Myglobals.valDY = Myglobals.valDY - dify;
                Myglobals.valDX = Myglobals.valDX + difx;

                if (Myglobals.valDX >= 360)
                {
                    Myglobals.valDX = Myglobals.valDX - 360;
                    goto EndloopX;
                }
                if (Myglobals.valDX <= 0)
                {
                    Myglobals.valDX = Myglobals.valDX + 360;
                    goto EndloopX;
                }
            EndloopX: { }
                if (Myglobals.valDY >= 360)
                {
                    Myglobals.valDY = Myglobals.valDY - 360;
                    goto EndloopY;
                }
                if (Myglobals.valDY <= 0)
                {
                    Myglobals.valDY = Myglobals.valDY + 360;
                    goto EndloopY;
                }
            EndloopY: { }

                double zaweea = Myglobals.valDX;
                if (Myglobals.valDX >= 315 & Myglobals.valDX <= 360)///////////1
                {
                    Myglobals.Tadweer = 1;
                    zaweea = Myglobals.valDX - 360;
                }
                if (Myglobals.valDX >= 0 & Myglobals.valDX < 45)
                {
                    Myglobals.Tadweer = 1;
                    zaweea = Myglobals.valDX;
                }

                if (Myglobals.valDX >= 45 & Myglobals.valDX < 135)/////////////2
                {
                    Myglobals.Tadweer = 2;
                    zaweea = 90 - Myglobals.valDX;
                }

                if (Myglobals.valDX >= 135 & Myglobals.valDX < 225)//////////3
                {
                    Myglobals.Tadweer = 3;
                    zaweea = Myglobals.valDX - 180;
                }


                if (Myglobals.valDX >= 225 & Myglobals.valDX < 315)/////////4
                {
                    Myglobals.Tadweer = 4;
                    zaweea = 270 - Myglobals.valDX;
                }
                float addedvalue = (float)((8) * Math.Cos(tah * (Myglobals.valDY)) * Math.Sin(tah * (Myglobals.valDY)) * Math.Sin(tah * (zaweea)));
                if (Myglobals.Tadweer == 1 || Myglobals.Tadweer == 3)
                {
                    Myglobals.tXValue = +(float)Myglobals.valDY;
                    Myglobals.tYValue = 0 - (float)(zaweea * Math.Sin(tah * (Myglobals.valDY))) - addedvalue;
                    Myglobals.tZValue = (float)(zaweea * Math.Cos(tah * (Myglobals.valDY))) - addedvalue;
                }

                if (Myglobals.Tadweer == 2 || Myglobals.Tadweer == 4)
                {
                    Myglobals.tXValue = -(float)Myglobals.valDY;
                    Myglobals.tYValue = 0 + (float)(zaweea * Math.Sin(tah * (Myglobals.valDY))) + addedvalue;
                    Myglobals.tZValue = (float)(zaweea * Math.Cos(tah * (Myglobals.valDY))) - addedvalue;
                }

                if (Myglobals.tXValue > 360)
                {
                    Myglobals.tXValue = Myglobals.tXValue - 360;
                    goto EndX;
                }
                if (Myglobals.tXValue < 0)
                {
                    Myglobals.tXValue = Myglobals.tXValue + 360;
                    goto EndX;
                }
            EndX: { };
                if (Myglobals.tYValue > 360)
                {
                    Myglobals.tYValue = Myglobals.tYValue - 360;
                    goto EndY;
                }
                if (Myglobals.tYValue < 0)
                {
                    Myglobals.tYValue = Myglobals.tYValue + 360;
                    goto EndY;
                }
            EndY: { };
                if (Myglobals.tZValue > 360)
                {
                    Myglobals.tZValue = Myglobals.tZValue - 360;
                    goto EndZ;
                }
                if (Myglobals.tZValue < 0)
                {
                    Myglobals.tZValue = Myglobals.tZValue + 360;
                    goto EndZ;
                }
            EndZ: { };
                Myglobals.xmove1 = e.X;
                Myglobals.ymove1 = e.Y;
                Myglobals.EyeX = Myglobals.RotatePointX3d - 500 * Math.Sin(tah * (Myglobals.valDX));
                Myglobals.EyeY = Myglobals.RotatePointY3d - 500 * Math.Cos(tah * (Myglobals.valDX));
                Myglobals.EyeZ = Myglobals.RotatePointZ3d + 500 * Math.Cos(tah * (Myglobals.valDY));

                if (Joint.Assignments == 1) JointAssignments = 1;
                if (Frame.Assignments == 1) BeamAssignments = 1;
                if (Myglobals.ExtrudedShell == 1) ExtrudedShell = 1;
                if (Myglobals.ExtrudedFrame == 1) ExtrudedFrame = 1;
                if (Joint.ShowPower == 1) JointShowPower = 1;
                if (Frame.ShowPower == 1) FrameShowPower = 1;
                Joint.ShowPower = 0;
                Frame.ShowPower = 0;
                Joint.Assignments = 0;
                Frame.Assignments = 0;
                Myglobals.ExtrudedShell = 0;
                Myglobals.ExtrudedFrame = 0;
                DROWcls callmee = new DROWcls();
                callmee.Render3d();
                Bitmap finalBmp = new Bitmap(Myglobals.BitampWidth3d, Myglobals.BitampHight3d);
                if (pictureBox4.Image != null)
                {
                    pictureBox4.Image.Dispose();
                    pictureBox4.Image = null;
                }
                pictureBox4.Image = finalBmp;
                goto ENDLOOP;
            }
            #endregion
            #region//التقاط الاوسناب
            TempX = e.X;
            TempY = e.Y;
            Tahkik = 0;
            if (Snap.GridIntersections == 1)
            {
                for (int i = 1; i < GridPoint.Number3d + 1; i++)
                {
                    if (Math.Abs(TempX - GridPoint.X3d[i]) < 4 & Math.Abs(TempY - GridPoint.Y3d[i]) < 4)
                    {
                        Tahkik = 1;
                        SelectedGridpoint3d = i;
                        SnapFromType = "GridIntersections";
                        TempX12Real = GridPoint.XReal[i];
                        TempY12Real = GridPoint.YReal[i];
                        TempZ12Real = GridPoint.ZReal[i];
                        TempX = GridPoint.X3d[i];
                        TempY = GridPoint.Y3d[i];
                        goto endloop;
                    }
                }
            }
            if (Snap.Joints == 1)
            {
                for (int i = 1; i < Joint.Number3d + 1; i++)
                {
                    if (Math.Abs(TempX - Joint.X3d[i]) < 4 & Math.Abs(TempY - Joint.Y3d[i]) < 4)
                    {
                        Tahkik = 1;
                        SelectedJoint = i;
                        SnapFromType = "Joints";
                        TempX12Real = Joint.XReal[i];
                        TempY12Real = Joint.YReal[i];
                        TempZ12Real = Joint.ZReal[i];
                        TempX = Joint.X3d[i];
                        TempY = Joint.Y3d[i];
                        goto endloop;
                    }
                }
            }

        endloop: { }
            if (Tahkik == 1)
            {
                LBLX.Visible = true;
                LBLY.Visible = true;
                LBLZ.Visible = true;
                LBLX.Text = "X= " + Convert.ToString(TempX12Real);
                LBLY.Text = "Y= " + Convert.ToString(TempY12Real);
                LBLZ.Text = "Z= " + Convert.ToString(TempZ12Real);
            }
            #endregion
            if (e.Button == MouseButtons.Middle) goto ENDLOOP;
            MouseButtonsLeft = 0;
            if (e.Button == MouseButtons.Left) MouseButtonsLeft = 1;
            timetodo = timetodo + 1;
            pictureBox4Draw();
        ENDLOOP: { }
        }
        private void pictureBox4_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                if (JointAssignments == 1) Joint.Assignments = 1;
                if (BeamAssignments == 1) Frame.Assignments = 1;
                if (ExtrudedShell == 1) Myglobals.ExtrudedShell = 1;
                if (ExtrudedFrame == 1) Myglobals.ExtrudedFrame = 1;
                if (JointShowPower == 1) Joint.ShowPower = 1;
                if (FrameShowPower == 1) Frame.ShowPower = 1;
                JointShowPower = 0;
                FrameShowPower = 0;
                ExtrudedShell = 0;
                ExtrudedFrame = 0;
                JointAssignments = 0;
                BeamAssignments = 0;
                DROWcls callmee = new DROWcls();
                callmee.Render3d();
                pictureBox4Draw();
            }
            //  if (e.Button == MouseButtons.Right)
            if (Myglobals.Rotate3DVeiw == 1)
            {
                Myglobals.Rotate3DVeiw = 0;
                if (JointShowPower == 1 || FrameShowPower == 1 || ExtrudedShell == 1 || ExtrudedFrame == 1 || JointAssignments == 1 || BeamAssignments == 1)
                {
                    if (JointAssignments == 1) Joint.Assignments = 1;
                    if (BeamAssignments == 1) Frame.Assignments = 1;
                    if (ExtrudedShell == 1) Myglobals.ExtrudedShell = 1;
                    if (ExtrudedFrame == 1) Myglobals.ExtrudedFrame = 1;
                    if (JointShowPower == 1) Joint.ShowPower = 1;
                    if (FrameShowPower == 1) Frame.ShowPower = 1;
                    JointShowPower = 0;
                    FrameShowPower = 0;
                    ExtrudedShell = 0;
                    ExtrudedFrame = 0;
                    JointAssignments = 0;
                    BeamAssignments = 0;
                    DROWcls callmee = new DROWcls();
                    callmee.Render3d();
                }
            }
            if (MouseButtonsLeft == 1 & toolStripButton1.Checked == true)
            {
                int thecase = 0;
                if (e.X >= Myglobals.xmove1) thecase = 1;//تحديد العناصر الواقعة ضمن المربع تماما
                if (e.X < Myglobals.xmove1) thecase = 2;//تحديد العناصر المتقاطعة 
                int MaxX = 0;
                int MaxY = 0;
                int MinX = 0;
                int MinY = 0;
                MaxX = Myglobals.xmove1;
                MinX = Myglobals.xmove1;
                if (e.X > MaxX) MaxX = e.X;
                if (e.X < MinX) MinX = e.X;
                MaxY = Myglobals.ymove1;
                MinY = Myglobals.ymove1;
                if (e.Y > MaxY) MaxY = e.Y;
                if (e.Y < MinY) MinY = e.Y;
                int ifselected = 0;
                #region//تحديد العقد
                for (int i = 1; i < Joint.Number3d + 1; i++)
                {
                    int Tah = 0;
                    int X3 = Joint.X3d[i];
                    int Y3 = Joint.Y3d[i];
                    if (X3 >= MinX & X3 <= MaxX & Y3 >= MinY & Y3 <= MaxY) Tah = 1;
                    if (Tah == 1)
                    {
                        ifselected = 1;
                        Joint.Selected[i] = 1;
                    }
                }
                #endregion
                #region//تحديد الجوائز
                for (int i = 1; i < Frame.Number + 1; i++)
                {
                    if (FrameElement[i].Visible == 0)
                    {
                        int Tah1 = 0;
                        int Tah2 = 0;
                        int X3 = Joint.X3d[FrameElement[i].FirstJoint];
                        int Y3 = Joint.Y3d[FrameElement[i].FirstJoint];
                        int X4 = Joint.X3d[FrameElement[i].SecondJoint];
                        int Y4 = Joint.Y3d[FrameElement[i].SecondJoint];
                        if (X3 >= MinX & X3 <= MaxX & Y3 >= MinY & Y3 <= MaxY) Tah1 = 1;
                        if (X4 >= MinX & X4 <= MaxX & Y4 >= MinY & Y4 <= MaxY) Tah2 = 1;
                        if (thecase == 1)
                        {
                            if (Tah1 == 1 & Tah2 == 1)
                            {
                                ifselected = 1;
                                FrameElement[i].Selected = 1;
                            }
                        }
                        if (thecase == 2)
                        {
                            if (Tah1 == 1 || Tah2 == 1)
                            {
                                ifselected = 1;
                                FrameElement[i].Selected = 1;
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
                                FrameElement[i].Selected = 1;
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
                                FrameElement[i].Selected = 1;
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
                                FrameElement[i].Selected = 1;
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
                                FrameElement[i].Selected = 1;
                                goto end100;
                            }
                        }
                    end100: { }
                    }
                }
                #endregion
                # region //تحديد البلاطات
                for (int i = 1; i < Shell.Number + 1; i++)
                {
                    if (Shell.Visible[i] == 0)
                    {
                        for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                        {
                            int Tah1 = 0;
                            int Tah2 = 0;
                            int X3 = Joint.X3d[Shell.JointNo[i, j]];
                            int Y3 = Joint.Y3d[Shell.JointNo[i, j]];
                            int X4 = 0;
                            int Y4 = 0;
                            if (j != Shell.PointNumbers[i])
                            {
                                X4 = Joint.X3d[Shell.JointNo[i, j + 1]];
                                Y4 = Joint.Y3d[Shell.JointNo[i, j + 1]];
                            }
                            else
                            {
                                X4 = Joint.X3d[Shell.JointNo[i, 1]];
                                Y4 = Joint.Y3d[Shell.JointNo[i, 1]];
                            }
                            if (X3 >= MinX & X3 <= MaxX & Y3 >= MinY & Y3 <= MaxY) Tah1 = 1;
                            if (X4 >= MinX & X4 <= MaxX & Y4 >= MinY & Y4 <= MaxY) Tah2 = 1;
                            if (thecase == 1)
                            {
                                if (Tah1 == 1 & Tah2 == 1)
                                {
                                    ifselected = 1;
                                    Shell.SelectedLine[i, j] = 1;
                                }
                            }
                            if (thecase == 2)
                            {
                                if (Tah1 == 1 || Tah2 == 1)
                                {
                                    ifselected = 1;
                                    Shell.SelectedLine[i, j] = 1;
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
                                    Shell.SelectedLine[i, j] = 1;
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
                                    Shell.SelectedLine[i, j] = 1;
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
                                    Shell.SelectedLine[i, j] = 1;
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
                                    Shell.SelectedLine[i, j] = 1;
                                    goto end100;
                                }
                            }
                        end100: { }
                        }
                    }
                }
                #endregion
                if (ifselected == 1)
                {
                    DROWcls callmee = new DROWcls();
                    callmee.Render2d();
                    callmee.Render3d();
                    callmee.Renderelev();
                }
                Bitmap finalBmp = new Bitmap(Myglobals.BitampWidth3d, Myglobals.BitampHight3d);
                if (pictureBox4.Image != null)
                {
                    pictureBox4.Image.Dispose();
                    pictureBox4.Image = null;
                }
                pictureBox4.Image = finalBmp;
            }
        }
        private void pictureBox4Draw()
        {
            Bitmap finalBmp = new Bitmap(Myglobals.BitampWidth3d, Myglobals.BitampHight3d);
            if (pictureBox4.Image != null)
            {
                pictureBox4.Image.Dispose();
                pictureBox4.Image = null;
            }
            pictureBox4.Image = finalBmp;
            #region//رسم مربع التحديد
            if (toolStripButton1.Checked == true & MouseButtonsLeft == 1)
            {
                Graphics g = Graphics.FromImage(pictureBox4.Image);
                Pen pen = new Pen(Color.Black, 1f);
                float[] dashValues = { 5, 3, 5, 3 };
                pen.DashPattern = dashValues;
                Point[] polygon = new Point[5];
                polygon[0].X = Myglobals.xmove1;
                polygon[0].Y = Myglobals.ymove1;
                polygon[1].X = TempX;
                polygon[1].Y = Myglobals.ymove1;
                polygon[2].X = TempX;
                polygon[2].Y = TempY;
                polygon[3].X = Myglobals.xmove1;
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
            #region//رسم بلاطة بالنقاط
            if (toolStripButton8.Checked == true & Myglobals.drowclick == 1)
            {
                Graphics g = Graphics.FromImage(pictureBox4.Image);
                Pen pen = new Pen(Color.Red, 1f);
                float[] dashValues = { 5, 3, 5, 3 };
                pen = new Pen(Color.Gray, 1f);
                pen.DashPattern = dashValues;
                double thelength = 0;
                int thex = 0;
                int they = 0;
                Font drawFont = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
                SolidBrush drawBrush = new SolidBrush(Color.Blue);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                Point[] polygon = new Point[Shell.PointNumbersTemp + 2];

                for (int i = 0; i < Shell.PointNumbersTemp; i++)
                {
                    double tx1 = Shell.PointXTemp[i + 1];
                    double ty1 = -1 * Shell.PointYTemp[i + 1];
                    double tz1 = -1 * Shell.PointZTemp[i + 1];
                    Math3DP.PointG Joint3dM = new Math3DP.PointG(tx1, ty1, tz1);
                    Joint3dM.RotateX = Myglobals.tXValue;
                    Joint3dM.RotateY = Myglobals.tYValue;
                    Joint3dM.RotateZ = Myglobals.tZValue;
                    Joint3dM.DrawPoint();
                    polygon[i].X = Myglobals.TheX3d;
                    polygon[i].Y = Myglobals.TheY3d;
                }
                for (int i = 0; i < Shell.PointNumbersTemp; i++)
                {
                    g.DrawString((i + 1).ToString(), drawFont, drawBrush, polygon[i].X - 15, polygon[i].Y - 15);
                }
                for (int i = 0; i < Shell.PointNumbersTemp - 1; i++)
                {
                    g.DrawLine(pen, polygon[i], polygon[i + 1]);
                    thelength = Math.Round(Math.Sqrt(Math.Pow(Shell.PointXTemp[i + 1] - Shell.PointXTemp[i + 2], 2) + Math.Pow(Shell.PointYTemp[i + 1] - Shell.PointYTemp[i + 2], 2) + Math.Pow(Shell.PointZTemp[i + 1] - Shell.PointZTemp[i + 2], 2)), 3);
                    thex = (polygon[i].X + polygon[i + 1].X) / 2;
                    they = (polygon[i].Y + polygon[i + 1].Y) / 2;
                    g.DrawString(thelength.ToString() + " m", drawFont, drawBrush, thex - 15, they - 15);
                }
                g.DrawString((Shell.PointNumbersTemp + 1).ToString(), drawFont, drawBrush, TempX - 15, TempY - 15);

                polygon[Shell.PointNumbersTemp].X = TempX;
                polygon[Shell.PointNumbersTemp].Y = TempY;
                polygon[Shell.PointNumbersTemp + 1] = polygon[0];
                g.DrawLine(pen, polygon[0], polygon[Shell.PointNumbersTemp]);
                g.DrawLine(pen, polygon[Shell.PointNumbersTemp - 1], polygon[Shell.PointNumbersTemp]);

                if (Tahkik == 1)
                {
                    thelength = Math.Round(Math.Sqrt(Math.Pow(TempX12Real - Shell.PointXTemp[Shell.PointNumbersTemp], 2) + Math.Pow(TempY12Real - Shell.PointYTemp[Shell.PointNumbersTemp], 2) + Math.Pow(TempZ12Real - Shell.PointZTemp[Shell.PointNumbersTemp], 2)), 3);
                    thex = (TempX + polygon[Shell.PointNumbersTemp - 1].X) / 2;
                    they = (TempY + polygon[Shell.PointNumbersTemp - 1].Y) / 2;
                    g.DrawString(thelength.ToString() + " m", drawFont, drawBrush, thex - 15, they - 15);

                    thelength = Math.Round(Math.Sqrt(Math.Pow(TempX12Real - Shell.PointXTemp[1], 2) + Math.Pow(TempY12Real - Shell.PointYTemp[1], 2) + Math.Pow(TempZ12Real - Shell.PointZTemp[1], 2)), 3);
                    thex = (TempX + polygon[0].X) / 2;
                    they = (TempY + polygon[0].Y) / 2;
                    g.DrawString(thelength.ToString() + " m", drawFont, drawBrush, thex - 15, they - 15);
                }
                using (Brush brush = new SolidBrush(Color.FromArgb(30, Color.LightSkyBlue)))
                {
                    g.FillPolygon(brush, polygon);
                }
            }
            #endregion
            #region//تحديد البلاطة بالرسم السريع
            if (toolStripButton10.Checked == true & Myglobals.AriaSelected != 0)
            {
                Graphics g = Graphics.FromImage(pictureBox4.Image);
                Pen pen = new Pen(Color.Red, 2f);
                int i = Myglobals.AriaSelected;
                int N = Myglobals.AriaPointNo[i];
                Point[] polygon = new Point[N + 1];
                for (int j = 1; j < N + 1; j++)
                {
                    polygon[j].X = Myglobals.startX2d + Convert.ToInt32((Myglobals.p1X[i, j] * Myglobals.Zoom2d));
                    polygon[j].Y = Myglobals.startY2d - Convert.ToInt32((Myglobals.p1Y[i, j] * Myglobals.Zoom2d));
                }
                float[] dashValues = { 5, 3, 5, 3 };
                //pen = new Pen(Color.Black, 1f);
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
            pictureBox4Drawsub();
        }
        private void pictureBox4Drawsub()
        {
            Graphics g = Graphics.FromImage(pictureBox4.Image);
            Pen pen = new Pen(Color.Gray, 1f);
            Font drawFont = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
            SolidBrush drawBrush = new SolidBrush(Color.Blue);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            float[] dashValues = { 2, 3 };
            pen.DashPattern = dashValues;
            double tx1 = TempXReal[1];
            double ty1 = -1 * TempYReal[1];
            double tz1 = -1 * TempZReal[1];
            Math3DP.PointG Joint3dM = new Math3DP.PointG(tx1, ty1, tz1);
            Joint3dM.RotateX = Myglobals.tXValue;
            Joint3dM.RotateY = Myglobals.tYValue;
            Joint3dM.RotateZ = Myglobals.tZValue;
            Joint3dM.DrawPoint();
            LineMoveX1 = Myglobals.TheX3d;
            LineMoveY1 = Myglobals.TheY3d;
            //
            if (Myglobals.LineMove3dVisible == 1)
            {
                if (toolStripButton11.Checked == true || toolStripButton3.Checked == true)
                {
                    g.DrawLine(pen, LineMoveX1, LineMoveY1, TempX, TempY);// رسم خط الجوائز
                    if (Tahkik == 1)
                    {
                        double thelength = Math.Round(Math.Sqrt(Math.Pow(TempXReal[1] - TempX12Real, 2) + Math.Pow(TempYReal[1] - TempY12Real, 2) + Math.Pow(TempZReal[1] - TempZ12Real, 2)), 3);
                        int thex = (TempX + LineMoveX1) / 2;
                        int they = (TempY + LineMoveY1) / 2;
                        g.DrawString(thelength.ToString() + " m", drawFont, drawBrush, thex - 15, they - 15);
                    }
                }
            }
            g = Graphics.FromImage(pictureBox4.Image);//رسم نقطة تقاطع الشبكة
            pen = new Pen(Color.Red, 1f);
            if (Tahkik == 1 & SnapFromType == "GridIntersections")
            {
                g.DrawLine(pen, TempX + 5, TempY, TempX - 5, TempY);
                g.DrawLine(pen, TempX, TempY + 5, TempX, TempY - 5);
                drawBrush = new SolidBrush(Color.Black);
                g.DrawString("Grid Point " + GridPoint.Name3d[SelectedGridpoint3d].ToString(), drawFont, drawBrush, TempX + 10, TempY - 15);
            }
            if (Tahkik == 1 & SnapFromType == "Joints")
            {
                g.DrawLine(pen, TempX + 5, TempY + 5, TempX - 5, TempY + 5);
                g.DrawLine(pen, TempX + 5, TempY - 5, TempX - 5, TempY - 5);
                g.DrawLine(pen, TempX + 5, TempY - 5, TempX + 5, TempY + 5);
                g.DrawLine(pen, TempX - 5, TempY - 5, TempX - 5, TempY + 5);
                drawBrush = new SolidBrush(Color.Black);
                g.DrawString("Joint", drawFont, drawBrush, TempX + 10, TempY - 15);
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
        }
        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            Bitmap finalBmp = new Bitmap(Myglobals.BitampWidth3d, Myglobals.BitampHight3d);
            if (pictureBox4.Image != null)
            {
                pictureBox4.Image.Dispose();
                pictureBox4.Image = null;
            }
            pictureBox4.Image = finalBmp;
        }
        #endregion
        #region//pictureBox6
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            tabPage3.BorderStyle = BorderStyle.FixedSingle;
            tabPage1.BorderStyle = BorderStyle.None;
            tabPage2.BorderStyle = BorderStyle.None;
            Myglobals.SelectedPlan = "ELEV";
        }
        private void pictureBox6_MouseDown(object sender, MouseEventArgs e)
        {
            tabPage3.BorderStyle = BorderStyle.FixedSingle;
            tabPage1.BorderStyle = BorderStyle.None;
            tabPage2.BorderStyle = BorderStyle.None;
            Myglobals.SelectedPlan = "ELEV";
            Myglobals.xmove = Cursor.Position.X;
            Myglobals.ymove = Cursor.Position.Y;
            Myglobals.xmove1 = e.X;
            Myglobals.ymove1 = e.Y;
            int ifselected = 0;
            if (e.Button == System.Windows.Forms.MouseButtons.Left || e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                double tx1D = Myglobals.elevPointX[Myglobals.Selectedelev, 1];
                double ty1D = Myglobals.elevPointY[Myglobals.Selectedelev, 1];
                double tz1D = Myglobals.elevPointZ[Myglobals.Selectedelev, 1];
                double tx2D = Myglobals.elevPointX[Myglobals.Selectedelev, 1 + Myglobals.StoryNumbers];
                double ty2D = Myglobals.elevPointY[Myglobals.Selectedelev, 1 + Myglobals.StoryNumbers];
                double tz2D = Myglobals.elevPointZ[Myglobals.Selectedelev, 1 + Myglobals.StoryNumbers];
                double tx3D = Myglobals.elevPointX[Myglobals.Selectedelev, 2 + Myglobals.StoryNumbers];
                double ty3D = Myglobals.elevPointY[Myglobals.Selectedelev, 2 + Myglobals.StoryNumbers];
                double tz3D = Myglobals.elevPointZ[Myglobals.Selectedelev, 2 + Myglobals.StoryNumbers];
                double var2 = tx2D - tx1D;
                double var3 = tx3D - tx1D;
                double var5 = ty2D - ty1D;
                double var6 = ty3D - ty1D;
                double var8 = tz2D - tz1D;
                double var9 = tz3D - tz1D;
                double varA = var5 * var9 - var8 * var6;//a
                double varB = var8 * var3 - var2 * var9;//b
                double varC = var2 * var6 - var5 * var3;//c
                double varD = -varA * tx1D - varB * ty1D - varC * tz1D;//d  
                double XReal1 = GridLine.X1Real[Myglobals.elevGridLine[Myglobals.Selectedelev]];
                double YReal1 = GridLine.Y1Real[Myglobals.elevGridLine[Myglobals.Selectedelev]];
                double XReal2 = GridLine.X2Real[Myglobals.elevGridLine[Myglobals.Selectedelev]];
                double YReal2 = GridLine.Y2Real[Myglobals.elevGridLine[Myglobals.Selectedelev]];
                double length = Math.Sqrt(Math.Pow(XReal1 - XReal2, 2) + Math.Pow(YReal1 - YReal2, 2));
                double ALFA = 0;
                ALFA = (float)Angulo(XReal1, YReal1, XReal2, YReal2);
                ALFA = ALFA * Math.PI / 180;
                if (toolStripButton1.Checked == true)//تحديد العناصر
                {
                    #region//تحديد العقد
                    for (int i = 1; i < Joint.Number3d + 1; i++)
                    {

                        int tah = 0;
                        double xx = varA * Joint.XReal[i];
                        double yy = varB * Joint.YReal[i];
                        double zz = varC * Joint.ZReal[i];
                        double sm = xx + yy + zz;
                        if (Math.Abs(sm + varD) <= 0.5) tah = 1;
                        if (tah == 1)
                        {
                            if (Math.Abs(e.X - Joint.Xelev[i]) < 4 & Math.Abs(e.Y - Joint.Yelev[i]) < 4)
                            {
                                ifselected = 1;
                                if (Joint.Selected[i] == 1)
                                {
                                    Joint.Selected[i] = 0;
                                }
                                else
                                {
                                    Joint.Selected[i] = 1;
                                }
                                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                                {
                                    Joint.Selected[i] = 1;
                                    Joint.SelectedforProp = i;
                                    ifselected = 0;
                                    DROWcls callmee = new DROWcls();
                                    callmee.Render2d();
                                    callmee.Render3d();
                                    callmee.Renderelev();
                                    JointInformationForm informationForm = new JointInformationForm();
                                    informationForm.ShowDialog();
                                }
                                goto endloop1;
                            }
                        }
                    }
                    #endregion
                    # region//تحديد العناصر الخطية
                    for (int i = 1; i < Frame.Number + 1; i++)
                    {
                        //جائز
                        if (FrameElement[i].Visible == 0)
                        {
                            int tah1 = 0;
                            int tah2 = 0;
                            int tah = 0;
                            double xx = varA * Joint.XReal[FrameElement[i].FirstJoint];
                            double yy = varB * Joint.YReal[FrameElement[i].FirstJoint];
                            double zz = varC * Joint.ZReal[FrameElement[i].FirstJoint];
                            double sm = xx + yy + zz;
                            if (Math.Abs(sm + varD) <= 0.5) tah1 = 1;
                            xx = varA * Joint.XReal[FrameElement[i].SecondJoint];
                            yy = varB * Joint.YReal[FrameElement[i].SecondJoint];
                            zz = varC * Joint.ZReal[FrameElement[i].SecondJoint];
                            sm = xx + yy + zz;
                            if (Math.Abs(sm + varD) <= 0.5) tah2 = 1;
                            if (tah1 == 1 & tah2 == 1) tah = 1;
                            if (tah == 1)
                            {
                                int X1 = Joint.Xelev[FrameElement[i].FirstJoint];
                                int Y1 = Joint.Yelev[FrameElement[i].FirstJoint];
                                int X2 = Joint.Xelev[FrameElement[i].SecondJoint];
                                int Y2 = Joint.Yelev[FrameElement[i].SecondJoint];
                                int X = e.X;
                                int Y = e.Y;
                                double distance = 100000;
                                int Tah = 0;
                                if ((Y2 == Y1))
                                {
                                    if (X >= X1 & X <= X2) Tah = 1;
                                    if (X <= X1 & X >= X2) Tah = 1;
                                }
                                if ((X2 == X1))
                                {
                                    if (Y >= Y1 & Y <= Y2) Tah = 1;
                                    if (Y <= Y1 & Y >= Y2) Tah = 1;
                                }
                                {
                                    if (X >= X1 & X <= X2 & Y >= Y1 & Y <= Y2) Tah = 1;
                                    if (X >= X2 & X <= X1 & Y >= Y2 & Y <= Y1) Tah = 1;
                                    if (X >= X2 & X <= X1 & Y >= Y1 & Y <= Y2) Tah = 1;
                                    if (X >= X1 & X <= X2 & Y >= Y2 & Y <= Y1) Tah = 1;
                                    if (Tah == 1)
                                    {
                                        distance = ((X2 - X1) * (Y1 - Y) - (X1 - X) * (Y2 - Y1)) / Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2));
                                    }
                                }
                                if (Math.Abs(distance) < 0.2 * Myglobals.Zoomelev)
                                {
                                    ifselected = 1;
                                    if (FrameElement[i].Selected == 1)
                                    {
                                        FrameElement[i].Selected = 0;
                                    }
                                    else
                                    {
                                        FrameElement[i].Selected = 1;
                                    }
                                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                                    {
                                        FrameElement[i].Selected = 1;
                                        Frame.SelectedforProp = i;
                                        ifselected = 0;
                                        DROWcls callmee = new DROWcls();
                                        callmee.Render2d();
                                        callmee.Render3d();
                                        callmee.Renderelev();
                                        if (Myglobals.DrawDiagram == 0)
                                        {
                                            BeamInformationForm beamInformationForm = new BeamInformationForm();
                                            beamInformationForm.ShowDialog();
                                        }
                                        else
                                        {
                                            FrameResultsForm frameResultsForm = new FrameResultsForm();
                                            frameResultsForm.ShowDialog();
                                        }
                                    }
                                    goto endloop1;
                                }
                            }
                        }
                    }
                    #endregion
                    # region//تحديد البلاطات بخطوطها
                    for (int i = 1; i < Shell.Number + 1; i++)
                    {
                        if (Shell.Visible[i] == 0)
                        {
                            if (Shell.Type[i] == 2 || Shell.Type[i] == 3)
                            {
                                for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                                {
                                    int tah1 = 0;
                                    int tah2 = 0;
                                    int tah = 0;
                                    double xx = varA * Joint.XReal[Shell.JointNo[i, j]];
                                    double yy = varB * Joint.YReal[Shell.JointNo[i, j]];
                                    double zz = varC * Joint.ZReal[Shell.JointNo[i, j]];
                                    double sm = xx + yy + zz;
                                    if (Math.Abs(sm + varD) <= 0.5) tah1 = 1;
                                    if (j < Shell.PointNumbers[i])
                                    {
                                        xx = varA * Joint.XReal[Shell.JointNo[i, j + 1]];
                                        yy = varB * Joint.YReal[Shell.JointNo[i, j + 1]];
                                        zz = varC * Joint.ZReal[Shell.JointNo[i, j + 1]];
                                    }
                                    else
                                    {
                                        xx = varA * Joint.XReal[Shell.JointNo[i, 1]];
                                        yy = varB * Joint.YReal[Shell.JointNo[i, 1]];
                                        zz = varC * Joint.ZReal[Shell.JointNo[i, 1]];
                                    }
                                    sm = xx + yy + zz;
                                    if (Math.Abs(sm + varD) <= 0.5) tah2 = 1;
                                    if (tah1 == 1 & tah2 == 1) tah = 1;
                                    if (tah == 0) goto nextii;
                                }
                                for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                                {
                                    int X1 = Joint.Xelev[Shell.JointNo[i, j]];
                                    int Y1 = Joint.Yelev[Shell.JointNo[i, j]];
                                    int X2 = 0;
                                    int Y2 = 0;
                                    if (j != Shell.PointNumbers[i])
                                    {
                                        X2 = Joint.Xelev[Shell.JointNo[i, j + 1]];
                                        Y2 = Joint.Yelev[Shell.JointNo[i, j + 1]];
                                    }
                                    else
                                    {
                                        X2 = Joint.Xelev[Shell.JointNo[i, 1]];
                                        Y2 = Joint.Yelev[Shell.JointNo[i, 1]];
                                    }
                                    int X = e.Location.X;
                                    int Y = e.Location.Y;
                                    double distance = 100000;
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
                                    if (distance < 0.1 * Myglobals.Zoom2d)
                                    {
                                        ifselected = 1;
                                        if (Shell.SelectedLine[i, j] == 1)
                                        {
                                            Shell.SelectedLine[i, j] = 0;
                                        }
                                        else
                                        {
                                            Shell.SelectedLine[i, j] = 1;
                                        }
                                        if (e.Button == System.Windows.Forms.MouseButtons.Right)
                                        {
                                            Shell.SelectedLine[i, j] = 1;
                                            Shell.SelectedforProp = i;
                                            ifselected = 0;
                                            DROWcls callmee = new DROWcls();
                                            callmee.Render2d();
                                            callmee.Render3d();
                                            callmee.Renderelev();
                                            SlabInformationForm slabInformationForm = new SlabInformationForm();
                                            slabInformationForm.ShowDialog();
                                        }
                                        goto endloop1;
                                    }
                                }
                            }
                        }
                    nextii: { };
                    }
                    #endregion
                    # region//تحديد البلاطات بالنقر ضمنها
                    int Xtest = e.X;
                    int Ytest = e.Y;
                    #region//الفتحات أولا
                    for (int i = 1; i < Shell.Number + 1; i++)
                    {
                        if (Shell.Visible[i] == 0)
                        {
                            if (Shell.Type[i] == 4)
                            {
                                for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                                {
                                    int tah1 = 0;
                                    int tah2 = 0;
                                    int tah = 0;
                                    double xx = varA * Joint.XReal[Shell.JointNo[i, j]];
                                    double yy = varB * Joint.YReal[Shell.JointNo[i, j]];
                                    double zz = varC * Joint.ZReal[Shell.JointNo[i, j]];
                                    double sm = xx + yy + zz;
                                    if (Math.Abs(sm + varD) <= 0.5) tah1 = 1;
                                    if (j < Shell.PointNumbers[i])
                                    {
                                        xx = varA * Joint.XReal[Shell.JointNo[i, j + 1]];
                                        yy = varB * Joint.YReal[Shell.JointNo[i, j + 1]];
                                        zz = varC * Joint.ZReal[Shell.JointNo[i, j + 1]];
                                    }
                                    else
                                    {
                                        xx = varA * Joint.XReal[Shell.JointNo[i, 1]];
                                        yy = varB * Joint.YReal[Shell.JointNo[i, 1]];
                                        zz = varC * Joint.ZReal[Shell.JointNo[i, 1]];
                                    }
                                    sm = xx + yy + zz;
                                    if (Math.Abs(sm + varD) <= 0.5) tah2 = 1;
                                    if (tah1 == 1 & tah2 == 1) tah = 1;
                                    if (tah == 0) goto nexti;
                                }
                                int N = Shell.PointNumbers[i] + 1;
                                Point[] polygon = new Point[N + 1];
                                for (int j = 1; j < N; j++)
                                {
                                    polygon[j].X = Joint.Xelev[Shell.JointNo[i, j]];
                                    polygon[j].Y = Joint.Yelev[Shell.JointNo[i, j]];
                                }
                                polygon[N].X = polygon[1].X;
                                polygon[N].Y = polygon[1].Y;
                                bool result = false;
                                int nvert = N;
                                int k, l;
                                for (k = 1, l = nvert - 1; k < nvert; l = k++)
                                {
                                    if (((polygon[k].Y > Ytest) != (polygon[l].Y > Ytest)) &&
                                     (Xtest < (polygon[l].X - polygon[k].X) * (Ytest - polygon[k].Y) / (polygon[l].Y - polygon[k].Y) + polygon[k].X))
                                        result = !result;
                                }
                                if (result == true)
                                {
                                    ifselected = 1;
                                    int tah = 0;
                                    for (int j = 1; j < N; j++)
                                    {
                                        if (Shell.SelectedLine[i, j] == 1)
                                        {
                                            tah = 1;
                                            break;
                                        }
                                    }
                                    for (int j = 1; j < N; j++)
                                    {
                                        if (tah == 1)
                                        {
                                            Shell.SelectedLine[i, j] = 0;
                                        }
                                        else
                                        {
                                            Shell.SelectedLine[i, j] = 1;
                                        }
                                    }
                                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                                    {
                                        for (int j = 1; j < N; j++)
                                        {
                                            Shell.SelectedLine[i, j] = 1;
                                        }
                                        Shell.SelectedforProp = i;
                                        ifselected = 0;
                                        DROWcls callmee = new DROWcls();
                                        callmee.Render2d();
                                        callmee.Render3d();
                                        callmee.Renderelev();
                                        SlabInformationForm slabInformationForm = new SlabInformationForm();
                                        slabInformationForm.ShowDialog();
                                    }
                                    goto endloop1;
                                }
                            }
                        }
                    nexti: { };
                    }
                    #endregion
                    #region//الجدران ثانيا
                    for (int i = 1; i < Shell.Number + 1; i++)
                    {
                        if (Shell.Visible[i] == 0)
                        {
                            if (Shell.Type[i] == 2 || Shell.Type[i] == 3)
                            {
                                for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                                {
                                    int tah1 = 0;
                                    int tah2 = 0;
                                    int tah = 0;
                                    double xx = varA * Joint.XReal[Shell.JointNo[i, j]];
                                    double yy = varB * Joint.YReal[Shell.JointNo[i, j]];
                                    double zz = varC * Joint.ZReal[Shell.JointNo[i, j]];
                                    double sm = xx + yy + zz;
                                    if (Math.Abs(sm + varD) <= 0.5) tah1 = 1;
                                    if (j < Shell.PointNumbers[i])
                                    {
                                        xx = varA * Joint.XReal[Shell.JointNo[i, j + 1]];
                                        yy = varB * Joint.YReal[Shell.JointNo[i, j + 1]];
                                        zz = varC * Joint.ZReal[Shell.JointNo[i, j + 1]];
                                    }
                                    else
                                    {
                                        xx = varA * Joint.XReal[Shell.JointNo[i, 1]];
                                        yy = varB * Joint.YReal[Shell.JointNo[i, 1]];
                                        zz = varC * Joint.ZReal[Shell.JointNo[i, 1]];
                                    }
                                    sm = xx + yy + zz;
                                    if (Math.Abs(sm + varD) <= 0.5) tah2 = 1;
                                    if (tah1 == 1 & tah2 == 1) tah = 1;
                                    if (tah == 0) goto nexti;
                                }
                                int N = Shell.PointNumbers[i] + 1;
                                Point[] polygon = new Point[N + 1];
                                for (int j = 1; j < N; j++)
                                {
                                    polygon[j].X = Joint.Xelev[Shell.JointNo[i, j]];
                                    polygon[j].Y = Joint.Yelev[Shell.JointNo[i, j]];
                                }
                                polygon[N].X = polygon[1].X;
                                polygon[N].Y = polygon[1].Y;
                                bool result = false;
                                int nvert = N;
                                int k, l;
                                for (k = 1, l = nvert - 1; k < nvert; l = k++)
                                {
                                    if (((polygon[k].Y > Ytest) != (polygon[l].Y > Ytest)) &&
                                     (Xtest < (polygon[l].X - polygon[k].X) * (Ytest - polygon[k].Y) / (polygon[l].Y - polygon[k].Y) + polygon[k].X))
                                        result = !result;
                                }
                                if (result == true)
                                {
                                    ifselected = 1;
                                    int tah = 0;
                                    for (int j = 1; j < N; j++)
                                    {
                                        if (Shell.SelectedLine[i, j] == 1)
                                        {
                                            tah = 1;
                                            break;
                                        }
                                    }
                                    for (int j = 1; j < N; j++)
                                    {
                                        if (tah == 1)
                                        {
                                            Shell.SelectedLine[i, j] = 0;
                                        }
                                        else
                                        {
                                            Shell.SelectedLine[i, j] = 1;
                                        }
                                    }
                                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                                    {
                                        for (int j = 1; j < N; j++)
                                        {
                                            Shell.SelectedLine[i, j] = 1;
                                        }
                                        Shell.SelectedforProp = i;
                                        ifselected = 0;
                                        DROWcls callmee = new DROWcls();
                                        callmee.Render2d();
                                        callmee.Render3d();
                                        callmee.Renderelev();
                                        SlabInformationForm slabInformationForm = new SlabInformationForm();
                                        slabInformationForm.ShowDialog();
                                    }
                                    goto endloop1;
                                }
                            }
                        }
                    nexti: { };
                    }
                    #endregion

                    #endregion
                endloop1: { };
                    if (ifselected == 1)
                    {
                        DROWcls callmee = new DROWcls();
                        callmee.Render2d();
                        callmee.Render3d();
                        callmee.Renderelev();
                    }
                }
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    #region رسم جوائز
                    if (toolStripButton3.Checked == true)
                    {
                        if (Myglobals.drowclick == 0)
                        {
                            TempXReal[1] = Math.Round(TempX12Real, 3);
                            TempYReal[1] = Math.Round(TempY12Real, 3);
                            TempZReal[1] = Math.Round(TempZ12Real, 3);
                            TahkikXY[1] = 1;
                            Myglobals.LineMove3dVisible = 1;
                            Myglobals.LineMove2dVisible = 1;
                            Myglobals.LineMoveelevVisible = 1;
                            Myglobals.drowclick = 1;
                            LineMoveX1 = TempX;
                            LineMoveY1 = TempY;
                            LineMoveX2 = LineMoveX1;
                            LineMoveY2 = LineMoveY1;
                            XelevR = (TempX - Myglobals.startXelev) / (Myglobals.Zoomelev);
                            YelevR = (Myglobals.startYelev - TempY) / (Myglobals.Zoomelev);
                            goto sdd;
                        }
                        if (Myglobals.drowclick == 1)
                        {
                            TahkikXY[2] = 1;
                            TempXReal[2] = Math.Round(TempX12Real, 3);
                            TempYReal[2] = Math.Round(TempY12Real, 3);
                            TempZReal[2] = Math.Round(TempZ12Real, 3);
                            int x2 = TempX;
                            int y2 = TempY;
                            LineMoveX1 = TempX;
                            LineMoveY1 = TempY;
                            LineMoveX2 = LineMoveX1;
                            LineMoveY2 = LineMoveY1;
                            XelevR = (TempX - Myglobals.startXelev) / (Myglobals.Zoomelev);
                            YelevR = (Myglobals.startYelev - TempY) / (Myglobals.Zoomelev);
                            Frame.Number = Frame.Number + 1;
                            FrameElements emp = new FrameElements();
                            FrameElement[Frame.Number] = emp;
                            FrameElement[Frame.Number].Selected = 0;
                            double theX = Math.Round(TempXReal[1], 3);
                            double theY = Math.Round(TempYReal[1], 3);
                            double theZ = Math.Round(TempZReal[1], 3);
                            int tah = 0;
                            int selectedjoint = 0;
                            for (int i = 1; i < Joint.Number2d + 1; i++)
                            {
                                if (Joint.XReal[i] == theX & Joint.YReal[i] == theY & Joint.ZReal[i] == theZ)
                                {
                                    tah = 1;
                                    selectedjoint = i;
                                    break;
                                }
                            }
                            if (tah == 0)
                            {
                                Joint.Number2d = Joint.Number2d + 1;
                                selectedjoint = Joint.Number2d;
                                Joint.XReal[Joint.Number2d] = theX;
                                Joint.YReal[Joint.Number2d] = theY;
                                Joint.ZReal[Joint.Number2d] = theZ;
                            }
                            FrameElement[Frame.Number].FirstJoint= selectedjoint;
                            theX = TempXReal[2];
                            theY = TempYReal[2];
                            theZ = TempZReal[2];
                            tah = 0;
                            selectedjoint = 0;
                            for (int i = 1; i < Joint.Number2d + 1; i++)
                            {
                                if (Joint.XReal[i] == theX & Joint.YReal[i] == theY & Joint.ZReal[i] == theZ)
                                {
                                    tah = 1;
                                    selectedjoint = i;
                                    break;
                                }
                            }
                            if (tah == 0)
                            {
                                Joint.Number2d = Joint.Number2d + 1;
                                selectedjoint = Joint.Number2d;
                                Joint.XReal[Joint.Number2d] = theX;
                                Joint.YReal[Joint.Number2d] = theY;
                                Joint.ZReal[Joint.Number2d] = theZ;
                            }
                            FrameElement[Frame.Number].SecondJoint= selectedjoint;
                            FrameElement[Frame.Number].Section= Section.SelectedToDraw;
                            FrameElement[Frame.Number].RotateAngel= Myglobals.RotateAngelDraw;
                            int thejoint = FrameElement[Frame.Number].FirstJoint;
                            Joint.BeamConnectionN[thejoint] = Joint.BeamConnectionN[thejoint] + 1;
                            int thecount = Joint.BeamConnectionN[thejoint];
                            Joint.Beam[thejoint, thecount] = Frame.Number;
                            thejoint = FrameElement[Frame.Number].SecondJoint;
                            Joint.BeamConnectionN[thejoint] = Joint.BeamConnectionN[thejoint] + 1;
                            thecount = Joint.BeamConnectionN[thejoint];
                            Joint.Beam[thejoint, thecount] = Frame.Number;
                            TahkikXY[1] = 1;
                            TempXReal[1] = TempXReal[2];
                            TempYReal[1] = TempYReal[2];
                            TempZReal[1] = TempZReal[2];
                            TahkikXY[2] = 0;
                            #region//رسم في كل الطوابق
                            if (Myglobals.AllStories == 1)
                            {
                                double theX1 = Joint.XReal[FrameElement[Frame.Number].FirstJoint];
                                double theY1 = Joint.YReal[FrameElement[Frame.Number].FirstJoint];
                                double theX2 = Joint.XReal[FrameElement[Frame.Number].SecondJoint];
                                double theY2 = Joint.YReal[FrameElement[Frame.Number].SecondJoint];
                                double theZ01 = Joint.ZReal[FrameElement[Frame.Number].FirstJoint];
                                double theZ02 = Joint.ZReal[FrameElement[Frame.Number].SecondJoint];
                                double theZ1 = 0;
                                double theZ2 = 0;
                                double delataZ1 = 0;
                                double delataZ2 = 0;
                                delataZ1 = theZ01 - Myglobals.StoryLevel[SelectedStorytodrowallstory];
                                delataZ2 = theZ02 - Myglobals.StoryLevel[SelectedStorytodrowallstory];
                                for (int add = 0; add < Myglobals.StoryNumbers + 1; add++)///من الاول حتى الاحير عدا المطلوب  
                                {
                                    if (theZ01 == theZ02 & add == 0) goto nextadd;//جائز لاترسمه في الارضي
                                    if (add == Myglobals.StoryNumbers & Myglobals.StoryLevel[add] + delataZ1 > Myglobals.StoryLevel[Myglobals.StoryNumbers]) goto nextadd;
                                    if (add == Myglobals.StoryNumbers & Myglobals.StoryLevel[add] + delataZ2 > Myglobals.StoryLevel[Myglobals.StoryNumbers]) goto nextadd;
                                    if (add == 0 & Myglobals.StoryLevel[add] + delataZ1 < 0) goto nextadd;
                                    if (add == 0 & Myglobals.StoryLevel[add] + delataZ2 < 0) goto nextadd;
                                    if (add != SelectedStorytodrowallstory)
                                    {
                                        Frame.Number = Frame.Number + 1;
                                        theZ1 = Myglobals.StoryLevel[add] + delataZ1;
                                        theZ2 = Myglobals.StoryLevel[add] + delataZ2;
                                        tah = 0;
                                        selectedjoint = 0;
                                        for (int i = 1; i < Joint.Number2d + 1; i++)
                                        {
                                            if (Joint.XReal[i] == theX1 & Joint.YReal[i] == theY1 & Joint.ZReal[i] == theZ1)
                                            {
                                                tah = 1;
                                                selectedjoint = i;
                                                break;
                                            }
                                        }
                                        if (tah == 0)
                                        {
                                            Joint.Number2d = Joint.Number2d + 1;
                                            selectedjoint = Joint.Number2d;
                                            Joint.XReal[Joint.Number2d] = theX1;
                                            Joint.YReal[Joint.Number2d] = theY1;
                                            Joint.ZReal[Joint.Number2d] = theZ1;
                                        }
                                        FrameElement[Frame.Number].FirstJoint= selectedjoint;
                                        tah = 0;
                                        selectedjoint = 0;
                                        for (int i = 1; i < Joint.Number2d + 1; i++)
                                        {
                                            if (Joint.XReal[i] == theX2 & Joint.YReal[i] == theY2 & Joint.ZReal[i] == theZ2)
                                            {
                                                tah = 1;
                                                selectedjoint = i;
                                                break;
                                            }
                                        }
                                        if (tah == 0)
                                        {
                                            Joint.Number2d = Joint.Number2d + 1;
                                            selectedjoint = Joint.Number2d;
                                            Joint.XReal[Joint.Number2d] = theX2;
                                            Joint.YReal[Joint.Number2d] = theY2;
                                            Joint.ZReal[Joint.Number2d] = theZ2;
                                        }
                                        FrameElement[Frame.Number].SecondJoint= selectedjoint;
                                        FrameElement[Frame.Number].Section= Section.SelectedToDraw;
                                        FrameElement[Frame.Number].RotateAngel= Myglobals.RotateAngelDraw;
                                        thejoint = FrameElement[Frame.Number].FirstJoint;
                                        Joint.BeamConnectionN[thejoint] = Joint.BeamConnectionN[thejoint] + 1;
                                        thecount = Joint.BeamConnectionN[thejoint];
                                        Joint.Beam[thejoint, thecount] = Frame.Number;
                                        thejoint = FrameElement[Frame.Number].SecondJoint;
                                        Joint.BeamConnectionN[thejoint] = Joint.BeamConnectionN[thejoint] + 1;
                                        thecount = Joint.BeamConnectionN[thejoint];
                                        Joint.Beam[thejoint, thecount] = Frame.Number;
                                    }
                                nextadd: { }
                                }
                            }
                            #endregion
                            for (int add = 0; add < Myglobals.StoryNumbers + 1; add++)
                            {
                                if (TempZReal[2] == Myglobals.StoryLevel[add])
                                {
                                    SelectedStorytodrowallstory = add;
                                    break;
                                }
                            }
                            DROWcls callmee = new DROWcls();
                            callmee.Render2d();
                            callmee.Render3d();
                            callmee.Renderelev();
                            MakeTempFiles();
                        }
                    sdd: { }
                    }
                    #endregion
                    #region رسم بلاطة مستطيل
                    if (toolStripButton9.Checked == true)
                    {
                        if (Myglobals.drowclick == 0)
                        {
                            int x1 = e.Location.X;
                            int y1 = e.Location.Y;
                            TempXReal[1] = Math.Round(TempX12Real, 3);
                            TempYReal[1] = Math.Round(TempY12Real, 3);
                            TempZReal[1] = Math.Round(TempZ12Real, 3);
                            if (Tahkik == 1)
                            {
                                TahkikXY[1] = 1;
                                x1 = TempX;
                                y1 = TempY;
                                TempXReal[1] = Math.Round(TempX12Real, 3);
                                TempYReal[1] = Math.Round(TempY12Real, 3);
                                TempZReal[1] = Math.Round(TempZ12Real, 3);
                            }
                            double tx1 = TempXReal[1];
                            double ty1 = -1 * TempYReal[1];
                            double tz1 = -1 * TempZReal[1];
                            Math3DP.PointG Point3dM = new Math3DP.PointG(tx1, ty1, tz1);
                            Point3dM.RotateX = Myglobals.tXValue;
                            Point3dM.RotateY = Myglobals.tYValue;
                            Point3dM.RotateZ = Myglobals.tZValue;
                            Point3dM.DrawPoint();
                            // SelectShape2d[1].X1 = x1;
                            // SelectShape2d[1].Y1 = y1;
                            LineMoveX1 = x1;
                            LineMoveY1 = y1;
                            LineMoveX2 = LineMoveX1;
                            LineMoveY2 = LineMoveY1;
                            Myglobals.LineMoveelevVisible = 1;
                            Shell.PointNumbersTemp = Shell.PointNumbersTemp + 1;
                            Shell.PointXTemp[Shell.PointNumbersTemp] = TempXReal[1];
                            Shell.PointYTemp[Shell.PointNumbersTemp] = TempYReal[1];
                            Shell.PointZTemp[Shell.PointNumbersTemp] = TempZReal[1];
                            Myglobals.drowclick = 1;
                        }
                    }
                    #endregion
                    #region رسم بلاطة سريع بنقرة
                    if (toolStripButton10.Checked == true)
                    {
                        if (Myglobals.AriaSelectedELEV != 0)
                        {
                            // if (Myglobals.AllStories == 0)
                            {
                                Shell.Number = Shell.Number + 1;
                                Shell.PointNumbers[Shell.Number] = Myglobals.AriaPointNoELEV[Myglobals.AriaSelectedELEV] - 1;
                                for (int i = 1; i < Shell.PointNumbers[Shell.Number] + 1; i++)
                                {
                                    double theX = Myglobals.p1XELEVR[Myglobals.AriaSelectedELEV, i];
                                    double theY = Myglobals.p1YELEVR[Myglobals.AriaSelectedELEV, i];
                                    double theZ = Myglobals.p1ZELEVR[Myglobals.AriaSelectedELEV, i];
                                    int tah1 = 0;
                                    int selectedjoint = 0;
                                    for (int J = 1; J < Joint.Number2d + 1; J++)
                                    {
                                        if (Joint.XReal[J] == theX & Joint.YReal[J] == theY & Joint.ZReal[J] == theZ)
                                        {
                                            tah1 = 1;
                                            selectedjoint = J;
                                            break;
                                        }
                                    }
                                    if (tah1 == 0)
                                    {
                                        Joint.Number2d = Joint.Number2d + 1;
                                        selectedjoint = Joint.Number2d;
                                        Joint.XReal[Joint.Number2d] = theX;
                                        Joint.YReal[Joint.Number2d] = theY;
                                        Joint.ZReal[Joint.Number2d] = theZ;
                                    }
                                    Shell.JointNo[Shell.Number, i] = selectedjoint;
                                    Shell.SelectedLine[Shell.Number, i] = 0;
                                    Joint.FloorConnectionN[selectedjoint] = Joint.FloorConnectionN[selectedjoint] + 1;
                                    int thecount = Joint.FloorConnectionN[selectedjoint];
                                    Joint.Floor[selectedjoint, thecount] = Shell.Number;
                                }
                                Shell.Type[Shell.Number] = 3;
                                Shell.Section[Shell.Number] = Slab.SelectedToDraw;
                            }
                            DROWcls callmee = new DROWcls();
                            callmee.Render2d();
                            callmee.Render3d();
                            callmee.Renderelev();
                            MakeTempFiles();
                        }
                    }
                    #endregion
                    #region رسم بلاطة
                    if (toolStripButton8.Checked == true)
                    {
                        if (Myglobals.drowclick == 0)
                        {
                            TempXReal[1] = TempX12Real;
                            TempYReal[1] = TempY12Real;
                            TempZReal[1] = TempZ12Real;
                            TahkikXY[1] = 1;
                            int x1 = TempX;
                            int y1 = TempY;
                            Myglobals.LineMoveelevVisible = 1;
                            Myglobals.LineMoveelevFVisible = 1;
                            Shell.PointNumbersTemp = Shell.PointNumbersTemp + 1;
                            Shell.PointXTemp[Shell.PointNumbersTemp] = TempXReal[1];
                            Shell.PointYTemp[Shell.PointNumbersTemp] = TempYReal[1];
                            Shell.PointZTemp[Shell.PointNumbersTemp] = TempZReal[1];
                            Myglobals.drowclick = 1;
                            for (int add = 0; add < Myglobals.StoryNumbers + 1; add++)
                            {
                                if (TempZReal[1] == Myglobals.StoryLevel[add])
                                {
                                    SelectedStorytodrowallstory = add;
                                    break;
                                }
                            }
                            goto sdd;
                        }
                        if (Myglobals.drowclick == 1)
                        {
                            TahkikXY[2] = 1;
                            TempXReal[2] = TempX12Real;
                            TempYReal[2] = TempY12Real;
                            TempZReal[2] = TempZ12Real;
                            int x1 = TempX;
                            int y1 = TempY;
                            int x2 = TempX;
                            int y2 = TempY;
                            Shell.LineNumberTemp = Shell.LineNumberTemp + 1;
                            double theX = TempXReal[1];
                            double theY = TempYReal[1];
                            double theZ = TempZReal[1];
                            Myglobals.LineType = 5;
                            double tx1 = theX;
                            double ty1 = -1 * theY;
                            double tz1 = -1 * theZ;
                            Math3DP.PointG Point3dM = new Math3DP.PointG(tx1, ty1, tz1);
                            Point3dM.RotateX = Myglobals.tXValue;
                            Point3dM.RotateY = Myglobals.tYValue;
                            Point3dM.RotateZ = Myglobals.tZValue;
                            Point3dM.DrawPoint();

                            theX = TempXReal[2];
                            theY = TempYReal[2];
                            theZ = TempZReal[2];
                            tx1 = theX;
                            ty1 = -1 * theY;
                            tz1 = -1 * theZ;
                            Point3dM = new Math3DP.PointG(tx1, ty1, tz1);
                            Point3dM.RotateX = Myglobals.tXValue;
                            Point3dM.RotateY = Myglobals.tYValue;
                            Point3dM.RotateZ = Myglobals.tZValue;
                            Point3dM.DrawPoint();

                            x1 = Myglobals.startX2d + Convert.ToInt32((TempXReal[1]) * Myglobals.Zoom2d);
                            y1 = Myglobals.startY2d - Convert.ToInt32((TempYReal[1]) * Myglobals.Zoom2d);
                            x2 = Myglobals.startX2d + Convert.ToInt32((TempXReal[2]) * Myglobals.Zoom2d);
                            y2 = Myglobals.startY2d - Convert.ToInt32((TempYReal[2]) * Myglobals.Zoom2d);

                            Shell.PointNumbersTemp = Shell.PointNumbersTemp + 1;
                            Shell.PointXTemp[Shell.PointNumbersTemp] = theX;
                            Shell.PointYTemp[Shell.PointNumbersTemp] = theY;
                            Shell.PointZTemp[Shell.PointNumbersTemp] = theZ;
                            TahkikXY[1] = 1;
                            TempXReal[1] = TempXReal[2];
                            TempYReal[1] = TempYReal[2];
                            TempZReal[1] = TempZReal[2];
                            TahkikXY[2] = 0;
                        }
                    sdd: { }
                    }
                    #endregion
                }
            }
        }
        private void pictureBox6_MouseMove(object sender, MouseEventArgs e)
        {
            lastzoomXelev = e.X;
            lastzoomYelev = e.Y;
            #region // تحريك المسقط
            if (e.Button == MouseButtons.Middle)
            {
                timetodo = timetodo + 1;
                Myglobals.startXelev = Myglobals.startXelev + (Cursor.Position.X - Myglobals.xmove);
                Myglobals.startYelev = Myglobals.startYelev + (Cursor.Position.Y - Myglobals.ymove);
                Myglobals.xmove = Cursor.Position.X;
                Myglobals.ymove = Cursor.Position.Y;
                if (timetodo > 2)
                {
                    if (Joint.Assignments == 1) JointAssignments = 1;
                    if (Frame.Assignments == 1) BeamAssignments = 1;
                    Joint.Assignments = 0;
                    Frame.Assignments = 0;
                    DROWcls callmee = new DROWcls();
                    callmee.Renderelev();
                    timetodo = 0;
                    Bitmap finalBmp = new Bitmap(Myglobals.BitampWidthelev, Myglobals.BitampHightelev);
                    if (pictureBox6.Image != null)
                    {
                        pictureBox6.Image.Dispose();
                        pictureBox6.Image = null;
                    }
                    pictureBox6.Image = finalBmp;
                }
                goto ENDLOOP;
            }
            #endregion
            # region//تحديد البلاطات بالمرور على المساحات
            if (toolStripButton10.Checked == true)//رسم بلاطة سريع
            {
                int ifselected = 0;
                int Xtest = e.X;
                int Ytest = e.Y;
                for (int i = 1; i < Myglobals.AriaNoELEV + 1; i++)
                {
                    int N = Myglobals.AriaPointNoELEV[i];
                    Point[] polygon = new Point[N + 1];
                    for (int j = 1; j < N + 1; j++)
                    {
                        //polygon[j].X = Myglobals.startX2d + Convert.ToInt32((Myglobals.p1X[i, j] * Myglobals.Zoom2d));
                        //  polygon[j].Y = Myglobals.startY2d - Convert.ToInt32((Myglobals.p1Y[i, j] * Myglobals.Zoom2d));
                        polygon[j].X = Convert.ToInt32((Myglobals.p1XELEV[i, j]));
                        polygon[j].Y = Convert.ToInt32((Myglobals.p1YELEV[i, j]));
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
                    if (result == true & Myglobals.AriaSelectedELEV != i)
                    {
                        Myglobals.AriaSelectedELEV = i;
                        break;
                    }
                    if (result == false)
                    {
                        ifselected = ifselected + 1;
                    }
                }
                if (ifselected == Myglobals.AriaNoELEV & Myglobals.AriaSelectedELEV != 0)
                {
                    Myglobals.AriaSelectedELEV = 0;
                    Bitmap finalBmp = new Bitmap(Myglobals.BitampWidthelev, Myglobals.BitampWidthelev);
                    if (pictureBox6.Image != null)
                    {
                        pictureBox6.Image.Dispose();
                        pictureBox6.Image = null;
                    }
                    pictureBox6.Image = finalBmp;
                }
            }
            #endregion
            #region//التقاط الاوسناب
            TempX = e.X;
            TempY = e.Y;
            double tx1D = Myglobals.elevPointX[Myglobals.Selectedelev, 1];
            double ty1D = Myglobals.elevPointY[Myglobals.Selectedelev, 1];
            double tz1D = Myglobals.elevPointZ[Myglobals.Selectedelev, 1];
            double tx2D = Myglobals.elevPointX[Myglobals.Selectedelev, 1 + Myglobals.StoryNumbers];
            double ty2D = Myglobals.elevPointY[Myglobals.Selectedelev, 1 + Myglobals.StoryNumbers];
            double tz2D = Myglobals.elevPointZ[Myglobals.Selectedelev, 1 + Myglobals.StoryNumbers];
            double tx3D = Myglobals.elevPointX[Myglobals.Selectedelev, 2 + Myglobals.StoryNumbers];
            double ty3D = Myglobals.elevPointY[Myglobals.Selectedelev, 2 + Myglobals.StoryNumbers];
            double tz3D = Myglobals.elevPointZ[Myglobals.Selectedelev, 2 + Myglobals.StoryNumbers];
            double var2 = tx2D - tx1D;
            double var3 = tx3D - tx1D;
            double var5 = ty2D - ty1D;
            double var6 = ty3D - ty1D;
            double var8 = tz2D - tz1D;
            double var9 = tz3D - tz1D;
            double varA = var5 * var9 - var8 * var6;//a
            double varB = var8 * var3 - var2 * var9;//b
            double varC = var2 * var6 - var5 * var3;//c
            double varD = -varA * tx1D - varB * ty1D - varC * tz1D;//d  
            double theX = (TempX - Myglobals.startXelev) / (Myglobals.Zoomelev);
            double theY = (Myglobals.startYelev - TempY) / (Myglobals.Zoomelev);
            double XReal1 = GridLine.X1Real[Myglobals.elevGridLine[Myglobals.Selectedelev]];
            double YReal1 = GridLine.Y1Real[Myglobals.elevGridLine[Myglobals.Selectedelev]];
            double XReal2 = GridLine.X2Real[Myglobals.elevGridLine[Myglobals.Selectedelev]];
            double YReal2 = GridLine.Y2Real[Myglobals.elevGridLine[Myglobals.Selectedelev]];
            double length = Math.Sqrt(Math.Pow(XReal1 - XReal2, 2) + Math.Pow(YReal1 - YReal2, 2));
            double ALFA = 0;
            ALFA = (float)Angulo(XReal1, YReal1, XReal2, YReal2);
            ALFA = ALFA * Math.PI / 180;
            TempX12Real = Math.Round(XReal1 + theX * Math.Cos(ALFA), 3);
            TempY12Real = Math.Round(YReal1 + theX * Math.Sin(ALFA), 3);
            TempZ12Real = Math.Round(theY, 3);
            Tahkik = 0;
            if (Snap.GridIntersections == 1 & Myglobals.IfAnalysis == 0)
            {
                #region //نقاط الشبكة
                for (int i = 1; i < Myglobals.elevPointNumbers[Myglobals.Selectedelev] + 1; i++)
                {
                    if (Math.Abs(TempX - GridPoint.Xelev[i]) < 4 & Math.Abs(TempY - GridPoint.Yelev[i]) < 4)
                    {
                        Tahkik = 1;
                        SelectedGridpointelev = i;
                        SnapFromType = "GridIntersections";
                        TempX12Real = Myglobals.elevPointX[Myglobals.Selectedelev, i];
                        TempY12Real = Myglobals.elevPointY[Myglobals.Selectedelev, i];
                        TempZ12Real = Myglobals.elevPointZ[Myglobals.Selectedelev, i];
                        TempX = GridPoint.Xelev[i];
                        TempY = GridPoint.Yelev[i];
                        goto endloop;
                    }
                }
                #endregion
            }
            if (Snap.Joints == 1 & Myglobals.IfAnalysis == 0)
            {
                #region//العقدة
                for (int i = 1; i < Joint.Number2d + 1; i++)
                {
                    int tah = 0;
                    double xx = varA * Joint.XReal[i];
                    double yy = varB * Joint.YReal[i];
                    double zz = varC * Joint.ZReal[i];
                    double sm = xx + yy + zz;
                    if (Math.Abs(sm + varD) <= 0.5) tah = 1;
                    if (Math.Abs(TempX - Joint.Xelev[i]) < 4 & Math.Abs(TempY - Joint.Yelev[i]) < 4 & tah == 1)
                    {
                        Tahkik = 1;
                        SelectedJoint = i;
                        SnapFromType = "Joints";
                        TempX12Real = Joint.XReal[i];
                        TempY12Real = Joint.YReal[i];
                        TempZ12Real = Joint.ZReal[i];
                        TempX = Joint.Xelev[i];
                        TempY = Joint.Yelev[i];
                        goto endloop;
                    }
                }
                #endregion
            }
            if (Snap.LineEndsandMidpoints == 1 & Myglobals.IfAnalysis == 0)
            {
                #region //منتصف القطعة
                for (int i = 1; i < Frame.Number + 1; i++)
                {
                    if (FrameElement[i].Visible == 0)
                    {
                        int tah1 = 0;
                        int tah2 = 0;
                        int tah = 0;
                        double xx = varA * Joint.XReal[FrameElement[i].FirstJoint];
                        double yy = varB * Joint.YReal[FrameElement[i].FirstJoint];
                        double zz = varC * Joint.ZReal[FrameElement[i].FirstJoint];
                        double sm = xx + yy + zz;
                        if (Math.Abs(sm + varD) <= 0.5) tah1 = 1;
                        xx = varA * Joint.XReal[FrameElement[i].SecondJoint];
                        yy = varB * Joint.YReal[FrameElement[i].SecondJoint];
                        zz = varC * Joint.ZReal[FrameElement[i].SecondJoint];
                        sm = xx + yy + zz;
                        if (Math.Abs(sm + varD) <= 0.5) tah2 = 1;
                        if (tah1 == 1 & tah2 == 1) tah = 1;
                        if (tah == 1)
                        {
                            int Xmid = (Joint.Xelev[FrameElement[i].FirstJoint] + Joint.Xelev[FrameElement[i].SecondJoint]) / 2;
                            int Ymid = (Joint.Yelev[FrameElement[i].FirstJoint] + Joint.Yelev[FrameElement[i].SecondJoint]) / 2;
                            if (Math.Abs(TempX - Xmid) < 4 & Math.Abs(TempY - Ymid) < 4)
                            {
                                Tahkik = 1;
                                SnapFromType = "LineEndsandMidpoints";
                                TempX12Real = (Joint.XReal[FrameElement[i].FirstJoint] + Joint.XReal[FrameElement[i].SecondJoint]) / 2;
                                TempY12Real = (Joint.YReal[FrameElement[i].FirstJoint] + Joint.YReal[FrameElement[i].SecondJoint]) / 2;
                                TempZ12Real = (Joint.ZReal[FrameElement[i].FirstJoint] + Joint.ZReal[FrameElement[i].SecondJoint]) / 2;
                                TempX = Xmid;
                                TempY = Ymid;
                                goto endloop;
                            }
                        }
                    }
                }
                #endregion
            }
            if (Snap.LinesandFrames == 1)
            {
                # region//سناب العناصر الخطية
                for (int i = 1; i < Frame.Number + 1; i++)
                {
                    //جائز
                    if (FrameElement[i].Visible == 0)
                    {
                        int tah1 = 0;
                        int tah2 = 0;
                        int tah = 0;
                        double xx = varA * Joint.XReal[FrameElement[i].FirstJoint];
                        double yy = varB * Joint.YReal[FrameElement[i].FirstJoint];
                        double zz = varC * Joint.ZReal[FrameElement[i].FirstJoint];
                        double sm = xx + yy + zz;
                        if (Math.Abs(sm + varD) <= 0.5) tah1 = 1;
                        xx = varA * Joint.XReal[FrameElement[i].SecondJoint];
                        yy = varB * Joint.YReal[FrameElement[i].SecondJoint];
                        zz = varC * Joint.ZReal[FrameElement[i].SecondJoint];
                        sm = xx + yy + zz;
                        if (Math.Abs(sm + varD) <= 0.5) tah2 = 1;
                        if (tah1 == 1 & tah2 == 1) tah = 1;
                        if (tah == 1)
                        {
                            double XREAL = 0;
                            double YREAL = 0;
                            double ZREAL = 0;
                            double X1R = Joint.XReal[FrameElement[i].FirstJoint];
                            double Y1R = Joint.YReal[FrameElement[i].FirstJoint];
                            double Z1R = Joint.ZReal[FrameElement[i].FirstJoint];
                            double X2R = Joint.XReal[FrameElement[i].SecondJoint];
                            double Y2R = Joint.YReal[FrameElement[i].SecondJoint];
                            double Z2R = Joint.ZReal[FrameElement[i].SecondJoint];
                            double Length = Math.Sqrt(Math.Pow(X1R - X2R, 2) + Math.Pow(Y1R - Y2R, 2) + Math.Pow(Z1R - Z2R, 2));

                            int X2d = 0;
                            int Y2d = 0;
                            int X1 = Joint.Xelev[FrameElement[i].FirstJoint];
                            int Y1 = Joint.Yelev[FrameElement[i].FirstJoint];
                            int X2 = Joint.Xelev[FrameElement[i].SecondJoint];
                            int Y2 = Joint.Yelev[FrameElement[i].SecondJoint];
                            int X = e.X;
                            int Y = e.Y;
                            double distance = 100000;
                            int Tah = 0;
                            if ((Y2 == Y1))
                            {
                                if (X >= X1 & X <= X2) Tah = 1;
                                if (X <= X1 & X >= X2) Tah = 1;
                            }
                            if ((X2 == X1))
                            {
                                if (Y >= Y1 & Y <= Y2) Tah = 1;
                                if (Y <= Y1 & Y >= Y2) Tah = 1;
                            }
                            // if (X2 != X1)
                            {
                                if (X >= X1 & X <= X2 & Y >= Y1 & Y <= Y2) Tah = 1;
                                if (X >= X2 & X <= X1 & Y >= Y2 & Y <= Y1) Tah = 1;
                                if (X >= X2 & X <= X1 & Y >= Y1 & Y <= Y2) Tah = 1;
                                if (X >= X1 & X <= X2 & Y >= Y2 & Y <= Y1) Tah = 1;
                                if (Tah == 1)
                                {
                                    distance = ((X2 - X1) * (Y1 - Y) - (X1 - X) * (Y2 - Y1)) / Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2));
                                    int MouseXint = X - Myglobals.startXelev;
                                    int MouseYint = Myglobals.startYelev - Y;
                                    XREAL = tx1D + Math.Round((MouseXint * Math.Cos(ALFA)) / (Myglobals.Zoomelev), 3);
                                    YREAL = ty1D + Math.Round((MouseXint * Math.Sin(ALFA)) / (Myglobals.Zoomelev), 3);
                                    ZREAL = tz1D + Math.Round((MouseYint) / (Myglobals.Zoomelev), 3);
                                    double DIS = Math.Sqrt(Math.Pow(X1R - XREAL, 2) + Math.Pow(Y1R - YREAL, 2) + Math.Pow(Z1R - ZREAL, 2));
                                    if (DIS > Length) DIS = Length;
                                    XREAL = Math.Round(X1R + (DIS / Length * (X2R - X1R)), 3);
                                    YREAL = Math.Round(Y1R + (DIS / Length * (Y2R - Y1R)), 3);
                                    ZREAL = Math.Round(Z1R + (DIS / Length * (Z2R - Z1R)), 3);
                                    double DIS1 = Math.Sqrt(Math.Pow(XREAL - tx1D, 2) + Math.Pow(YREAL - ty1D, 2));
                                    double ALFA1 = (double)Math.Round(Angulo(tx1D, ty1D, XREAL, YREAL), 2);
                                    ALFA1 = ALFA1 * Math.PI / 180;
                                    double shara = Math.Cos(ALFA1) / Math.Abs(Math.Cos(ALFA1));
                                    DIS1 = DIS1 * shara;
                                    X2d = Myglobals.startXelev + Convert.ToInt32((DIS1) * Myglobals.Zoomelev);
                                    Y2d = Myglobals.startYelev - Convert.ToInt32((ZREAL) * Myglobals.Zoomelev);
                                }
                            }
                        endloop5: { }
                            if (Math.Abs(distance) < 0.2 * Myglobals.Zoomelev)
                            {
                                Tahkik = 1;
                                SnapFromType = "LinesandFrames";
                                TempX12Real = XREAL;
                                TempY12Real = YREAL;
                                TempZ12Real = ZREAL;
                                TempX = X2d;
                                TempY = Y2d;
                                if (Myglobals.IfAnalysis == 1 & Myglobals.DrawDiagram == 1)
                                {
                                    double ThePowerValue = 0;
                                    double ThePowerValue1 = 0;
                                    double ThePowerValue2 = 0;
                                    double TheDistanceValue1 = 0;
                                    double TheDistanceValue2 = 0;
                                    double TheDistanceValue = Math.Sqrt(Math.Pow(X1R - XREAL, 2) + Math.Pow(Y1R - YREAL, 2) + Math.Pow(Z1R - ZREAL, 2));
                                    //TheDistanceValue = TheDistanceValue ;
                                    double plus = Length / Frame.AnalisesSecNumbers;
                                    for (int j = 0; j < Frame.AnalisesSecNumbers; j++)
                                    {
                                        TheDistanceValue1 = j * plus;
                                        TheDistanceValue2 = (j + 1) * plus;
                                        if (j == Frame.AnalisesSecNumbers - 1)
                                        {
                                            TheDistanceValue2 = Length;
                                            //TheDistanceValue = 1;
                                        }
                                        if (TheDistanceValue >= TheDistanceValue1 & TheDistanceValue <= TheDistanceValue2)
                                        {
                                            //double txR = X1R + TheDistanceValue1 * (X2R - X1R);
                                            // double tyR = Y1R + TheDistanceValue1 * (Y2R - Y1R);
                                            // double tzR = Z1R + TheDistanceValue1 * (Z2R - Z1R);
                                            if (Myglobals.AnyDiagram == 1)//axial
                                            {
                                                ThePowerValue1 = Frame.ResultValue1[i, j];
                                                ThePowerValue2 = Frame.ResultValue1[i, j + 1];
                                            }
                                            if (Myglobals.AnyDiagram == 2)//s22
                                            {
                                                ThePowerValue1 = Frame.ResultValue2[i, j];
                                                ThePowerValue2 = Frame.ResultValue2[i, j + 1];
                                            }
                                            if (Myglobals.AnyDiagram == 3) //s33
                                            {
                                                ThePowerValue1 = Frame.ResultValue4[i, j];
                                                ThePowerValue2 = Frame.ResultValue4[i, j + 1];
                                            }
                                            if (Myglobals.AnyDiagram == 4)//m22
                                            {
                                                ThePowerValue1 = Frame.ResultValue5[i, j];
                                                ThePowerValue2 = Frame.ResultValue5[i, j + 1];
                                            }
                                            if (Myglobals.AnyDiagram == 5)//m33
                                            {
                                                ThePowerValue1 = Frame.ResultValue3[i, j];
                                                ThePowerValue2 = Frame.ResultValue3[i, j + 1];
                                            }
                                            if (Myglobals.AnyDiagram == 6)//torsion
                                            {
                                                ThePowerValue1 = Frame.ResultValue6[i, j];
                                                ThePowerValue2 = Frame.ResultValue6[i, j + 1];
                                            }
                                            double farkV12 = ThePowerValue2 - ThePowerValue1;
                                            double farkD12 = TheDistanceValue2 - TheDistanceValue1;
                                            double farkD = TheDistanceValue - TheDistanceValue1;
                                            double farkV = farkV12 * farkD / farkD12;
                                            ThePowerValue = ThePowerValue1 + farkV;
                                            TheResultValue = Math.Round(ThePowerValue, 2);
                                            break;
                                        }
                                    }
                                }
                                goto endloop;
                            }
                        }
                    }
                }
                #endregion
            }
            if (Snap.Prallels == 1 & Myglobals.IfAnalysis == 0)
            {
                #region//التعامد
                // float sweepAngle = (float)Math.Round(Angulo(TempXReal[1], TempYReal[1], TempX12Real, TempY12Real), 2);
                float sweepAngle = (float)Math.Round(Angulo(LineMoveX1, LineMoveY1, TempX, TempY), 2);

                if (Math.Abs(sweepAngle - 90) < 0.8 || Math.Abs(sweepAngle - 270) < 0.8)
                {
                    Tahkik = 1;
                    SnapFromType = "Prallels";
                    TempX = LineMoveX1;
                    TempY = lastzoomYelev;
                    int MouseXint = lastzoomXelev - Myglobals.startXelev;
                    int MouseYint = Myglobals.startYelev - lastzoomYelev;
                    double XREAL = tx1D + Math.Round((MouseXint * Math.Cos(ALFA)) / (Myglobals.Zoomelev), 3);
                    double YREAL = ty1D + Math.Round((MouseXint * Math.Sin(ALFA)) / (Myglobals.Zoomelev), 3);
                    double ZREAL = tz1D + Math.Round((MouseYint) / (Myglobals.Zoomelev), 3);
                    TempX12Real = TempXReal[1];
                    TempY12Real = TempYReal[1];
                    TempZ12Real = ZREAL;
                    goto endloop;
                }
                if (Math.Abs(sweepAngle - 180) < 0.8 || Math.Abs(sweepAngle - 0) < 0.8 || Math.Abs(sweepAngle - 360) < 0.8)
                {
                    Tahkik = 1;
                    SnapFromType = "Prallels";
                    TempX = lastzoomXelev;
                    TempY = LineMoveY1;
                    int MouseXint = lastzoomXelev - Myglobals.startXelev;
                    int MouseYint = Myglobals.startYelev - lastzoomYelev;
                    double XREAL = tx1D + Math.Round((MouseXint * Math.Cos(ALFA)) / (Myglobals.Zoomelev), 3);
                    double YREAL = ty1D + Math.Round((MouseXint * Math.Sin(ALFA)) / (Myglobals.Zoomelev), 3);
                    double ZREAL = tz1D + Math.Round((MouseYint) / (Myglobals.Zoomelev), 3);
                    TempX12Real = XREAL;
                    TempY12Real = YREAL;
                    TempZ12Real = TempZReal[1];
                    goto endloop;
                }

                #endregion
            }
        endloop: { }
            lastzoomXelevR = (TempX - Myglobals.startXelev) / (Myglobals.Zoomelev);
            lastzoomYelevR = (Myglobals.startYelev - TempY) / (Myglobals.Zoomelev);
            LBLX.Visible = true;
            LBLY.Visible = true;
            LBLZ.Visible = true;
            LBLX.Text = "X= " + Convert.ToString(TempX12Real);
            LBLY.Text = "Y= " + Convert.ToString(TempY12Real);
            LBLZ.Text = "Z= " + Convert.ToString(TempZ12Real);
            #endregion
            if (e.Button == MouseButtons.Middle) goto ENDLOOP;
            MouseButtonsLeft = 0;
            if (e.Button == MouseButtons.Left) MouseButtonsLeft = 1;
            timetodo = timetodo + 1;
            if (timetodo > 2)
            {
                pictureBox6Draw();
                timetodo = 0;
            }
        ENDLOOP: { }
        }
        private void pictureBox6_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                if (JointAssignments == 1) Joint.Assignments = 1;
                if (BeamAssignments == 1) Frame.Assignments = 1;
                JointAssignments = 0;
                BeamAssignments = 0;
                DROWcls callmee = new DROWcls();
                callmee.Renderelev();
                pictureBox6Draw();
            }
            # region//رسم البلاطة المستطيلة
            if (Myglobals.LineMoveelevVisible == 1 & toolStripButton9.Checked == true)
            {
                Myglobals.LineMoveelevVisible = 0;
                if (Myglobals.drowclick == 1)
                {
                    int x1 = e.Location.X;
                    int y1 = e.Location.Y;
                    TempXReal[1] = TempX12Real;
                    TempYReal[1] = TempY12Real;
                    TempZReal[1] = TempZ12Real;
                    if (Tahkik == 1)
                    {
                        TahkikXY[1] = 1;
                        x1 = TempX;
                        y1 = TempY;
                        TempXReal[1] = TempX12Real;
                        TempYReal[1] = TempY12Real;
                        TempZReal[1] = TempZ12Real;
                    }
                    double tx1 = TempXReal[1];
                    double ty1 = -1 * TempYReal[1];
                    double tz1 = -1 * TempZReal[1];
                    Math3DP.PointG Point3dM = new Math3DP.PointG(tx1, ty1, tz1);
                    Point3dM.RotateX = Myglobals.tXValue;
                    Point3dM.RotateY = Myglobals.tYValue;
                    Point3dM.RotateZ = Myglobals.tZValue;
                    Point3dM.DrawPoint();
                    Shell.PointNumbersTemp = Shell.PointNumbersTemp + 1;
                    Shell.PointXTemp[Shell.PointNumbersTemp] = TempXReal[1];
                    Shell.PointYTemp[Shell.PointNumbersTemp] = TempYReal[1];
                    Shell.PointZTemp[Shell.PointNumbersTemp] = Shell.PointZTemp[1];
                    Shell.PointNumbersTemp = Shell.PointNumbersTemp + 1;
                    Shell.PointXTemp[Shell.PointNumbersTemp] = TempXReal[1];
                    Shell.PointYTemp[Shell.PointNumbersTemp] = TempYReal[1];
                    Shell.PointZTemp[Shell.PointNumbersTemp] = TempZReal[1];
                    Shell.PointNumbersTemp = Shell.PointNumbersTemp + 1;
                    Shell.PointXTemp[Shell.PointNumbersTemp] = Shell.PointXTemp[1];
                    Shell.PointYTemp[Shell.PointNumbersTemp] = Shell.PointYTemp[1];
                    Shell.PointZTemp[Shell.PointNumbersTemp] = TempZReal[1];
                    toolStripButton8.Checked = false;
                    toolStripButton1.Checked = true;
                    if (Shell.PointNumbersTemp > 2)
                    {
                        Shell.Number = Shell.Number + 1;
                        Shell.PointNumbers[Shell.Number] = Shell.PointNumbersTemp;
                        int tah = 1;
                        for (int i = 1; i < Shell.PointNumbers[Shell.Number] + 1; i++)
                        {
                            double theX = Shell.PointXTemp[i];
                            double theY = Shell.PointYTemp[i];
                            double theZ = Shell.PointZTemp[i];
                            int tah1 = 0;
                            int selectedjoint = 0;
                            for (int J = 1; J < Joint.Number2d + 1; J++)
                            {
                                if (Joint.XReal[J] == theX & Joint.YReal[J] == theY & Joint.ZReal[J] == theZ)
                                {
                                    tah1 = 1;
                                    selectedjoint = J;
                                    break;
                                }
                            }
                            if (tah1 == 0)
                            {
                                Joint.Number2d = Joint.Number2d + 1;
                                selectedjoint = Joint.Number2d;
                                Joint.XReal[Joint.Number2d] = theX;
                                Joint.YReal[Joint.Number2d] = theY;
                                Joint.ZReal[Joint.Number2d] = theZ;
                            }
                            if (i < Shell.PointNumbers[Shell.Number])
                            {
                                if (Shell.PointZTemp[i] != Shell.PointZTemp[i + 1]) tah = 2;
                            }
                            Shell.JointNo[Shell.Number, i] = selectedjoint;
                            Shell.SelectedLine[Shell.Number, i] = 0;
                            Joint.FloorConnectionN[selectedjoint] = Joint.FloorConnectionN[selectedjoint] + 1;
                            int thecount = Joint.FloorConnectionN[selectedjoint];
                            Joint.Floor[selectedjoint, thecount] = Shell.Number;
                        }
                        Shell.Type[Shell.Number] = 4;// tah;
                        Shell.Section[Shell.Number] = Slab.SelectedToDraw;
                        DROWcls callmee = new DROWcls();
                        callmee.Render2d();
                        callmee.Render3d();
                        callmee.Renderelev();
                        MakeTempFiles();
                    }
                    Shell.PointNumbersTemp = 0;
                    Shell.LineNumberTemp = 0;
                    Myglobals.drowclick = 0;
                    toolStripButton9.Checked = false;
                }
                goto endloop;
            }
            if (toolStripButton9.Checked == true)
            {
                Shell.PointNumbersTemp = 0;
                Shell.LineNumberTemp = 0;
                Myglobals.drowclick = 0;
                toolStripButton9.Checked = false;
            }
            #endregion
            #region//تحديد العناصر مع مربع التحديد
            if (MouseButtonsLeft == 1 & toolStripButton1.Checked == true)
            {
                double tx1D = Myglobals.elevPointX[Myglobals.Selectedelev, 1];
                double ty1D = Myglobals.elevPointY[Myglobals.Selectedelev, 1];
                double tz1D = Myglobals.elevPointZ[Myglobals.Selectedelev, 1];
                double tx2D = Myglobals.elevPointX[Myglobals.Selectedelev, 1 + Myglobals.StoryNumbers];
                double ty2D = Myglobals.elevPointY[Myglobals.Selectedelev, 1 + Myglobals.StoryNumbers];
                double tz2D = Myglobals.elevPointZ[Myglobals.Selectedelev, 1 + Myglobals.StoryNumbers];
                double tx3D = Myglobals.elevPointX[Myglobals.Selectedelev, 2 + Myglobals.StoryNumbers];
                double ty3D = Myglobals.elevPointY[Myglobals.Selectedelev, 2 + Myglobals.StoryNumbers];
                double tz3D = Myglobals.elevPointZ[Myglobals.Selectedelev, 2 + Myglobals.StoryNumbers];
                double var2 = tx2D - tx1D;
                double var3 = tx3D - tx1D;
                double var5 = ty2D - ty1D;
                double var6 = ty3D - ty1D;
                double var8 = tz2D - tz1D;
                double var9 = tz3D - tz1D;
                double varA = var5 * var9 - var8 * var6;//a
                double varB = var8 * var3 - var2 * var9;//b
                double varC = var2 * var6 - var5 * var3;//c
                double varD = -varA * tx1D - varB * ty1D - varC * tz1D;//d  
                double XReal1 = GridLine.X1Real[Myglobals.elevGridLine[Myglobals.Selectedelev]];
                double YReal1 = GridLine.Y1Real[Myglobals.elevGridLine[Myglobals.Selectedelev]];
                double XReal2 = GridLine.X2Real[Myglobals.elevGridLine[Myglobals.Selectedelev]];
                double YReal2 = GridLine.Y2Real[Myglobals.elevGridLine[Myglobals.Selectedelev]];
                double length = Math.Sqrt(Math.Pow(XReal1 - XReal2, 2) + Math.Pow(YReal1 - YReal2, 2));
                double ALFA = 0;
                ALFA = (float)Angulo(XReal1, YReal1, XReal2, YReal2);
                ALFA = ALFA * Math.PI / 180;
                MouseButtonsLeft = 0;
                int thecase = 0;
                if (e.X >= Myglobals.xmove1) thecase = 1;//تحديد العناصر الواقعة ضمن المربع تماما
                if (e.X < Myglobals.xmove1) thecase = 2;//تحديد العناصر المتقاطعة 
                int MaxX = 0;
                int MaxY = 0;
                int MinX = 0;
                int MinY = 0;
                MaxX = Myglobals.xmove1;
                MinX = Myglobals.xmove1;
                if (e.X > MaxX) MaxX = e.X;
                if (e.X < MinX) MinX = e.X;
                MaxY = Myglobals.ymove1;
                MinY = Myglobals.ymove1;
                if (e.Y > MaxY) MaxY = e.Y;
                if (e.Y < MinY) MinY = e.Y;
                int ifselected = 0;
                #region//تحديد العقد
                for (int i = 1; i < Joint.Number3d + 1; i++)
                {
                    int tah = 0;
                    double xx = varA * Joint.XReal[i];
                    double yy = varB * Joint.YReal[i];
                    double zz = varC * Joint.ZReal[i];
                    double sm = xx + yy + zz;
                    if (Math.Abs(sm + varD) <= 0.5) tah = 1;
                    if (tah == 1)
                    {
                        int Tah1 = 0;
                        int X3 = Joint.Xelev[i];
                        int Y3 = Joint.Yelev[i];
                        if (X3 >= MinX & X3 <= MaxX & Y3 >= MinY & Y3 <= MaxY) Tah1 = 1;
                        if (Tah1 == 1)
                        {
                            ifselected = 1;
                            Joint.Selected[i] = 1;
                        }
                    }
                }
                #endregion
                # region //تحديد الجوائز
                for (int i = 1; i < Frame.Number + 1; i++)
                {
                    if (FrameElement[i].Visible == 0)
                    {
                        int tah1 = 0;
                        int tah2 = 0;
                        int tah = 0;
                        double xx = varA * Joint.XReal[FrameElement[i].FirstJoint];
                        double yy = varB * Joint.YReal[FrameElement[i].FirstJoint];
                        double zz = varC * Joint.ZReal[FrameElement[i].FirstJoint];
                        double sm = xx + yy + zz;
                        if (Math.Abs(sm + varD) <= 0.5) tah1 = 1;
                        xx = varA * Joint.XReal[FrameElement[i].SecondJoint];
                        yy = varB * Joint.YReal[FrameElement[i].SecondJoint];
                        zz = varC * Joint.ZReal[FrameElement[i].SecondJoint];
                        sm = xx + yy + zz;
                        if (Math.Abs(sm + varD) <= 0.5) tah2 = 1;
                        if (tah1 == 1 & tah2 == 1) tah = 1;
                        if (tah == 1)
                        {
                            int Tah1 = 0;
                            int Tah2 = 0;
                            int X3 = Joint.Xelev[FrameElement[i].FirstJoint];
                            int Y3 = Joint.Yelev[FrameElement[i].FirstJoint];
                            int X4 = Joint.Xelev[FrameElement[i].SecondJoint];
                            int Y4 = Joint.Yelev[FrameElement[i].SecondJoint];
                            if (X3 >= MinX & X3 <= MaxX & Y3 >= MinY & Y3 <= MaxY) Tah1 = 1;
                            if (X4 >= MinX & X4 <= MaxX & Y4 >= MinY & Y4 <= MaxY) Tah2 = 1;
                            if (thecase == 1)
                            {
                                if (Tah1 == 1 & Tah2 == 1)
                                {
                                    ifselected = 1;
                                    FrameElement[i].Selected = 1;
                                }
                            }
                            if (thecase == 2)
                            {
                                if (Tah1 == 1 || Tah2 == 1)
                                {
                                    ifselected = 1;
                                    FrameElement[i].Selected = 1;
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
                                    FrameElement[i].Selected = 1;
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
                                    FrameElement[i].Selected = 1;
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
                                    FrameElement[i].Selected = 1;
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
                                    FrameElement[i].Selected = 1;
                                    goto end100;
                                }
                            }
                        end100: { }
                        }
                    }
                }
                #endregion
                # region //تحديد البلاطات
                for (int i = 1; i < Shell.Number + 1; i++)
                {
                    if (Shell.Visible[i] == 0)
                    {
                        if (Shell.Type[i] == 2 & Shell.Type[i] == 3)
                        {
                            for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                            {
                                int tah1 = 0;
                                int tah2 = 0;
                                int tah = 0;
                                double xx = varA * Joint.XReal[Shell.JointNo[i, j]];
                                double yy = varB * Joint.YReal[Shell.JointNo[i, j]];
                                double zz = varC * Joint.ZReal[Shell.JointNo[i, j]];
                                double sm = xx + yy + zz;
                                if (Math.Abs(sm + varD) <= 0.5) tah1 = 1;
                                if (j < Shell.PointNumbers[i])
                                {
                                    xx = varA * Joint.XReal[Shell.JointNo[i, j + 1]];
                                    yy = varB * Joint.YReal[Shell.JointNo[i, j + 1]];
                                    zz = varC * Joint.ZReal[Shell.JointNo[i, j + 1]];
                                }
                                else
                                {
                                    xx = varA * Joint.XReal[Shell.JointNo[i, 1]];
                                    yy = varB * Joint.YReal[Shell.JointNo[i, 1]];
                                    zz = varC * Joint.ZReal[Shell.JointNo[i, 1]];
                                }
                                sm = xx + yy + zz;
                                if (Math.Abs(sm + varD) <= 0.5) tah2 = 1;
                                if (tah1 == 1 & tah2 == 1) tah = 1;
                                if (tah == 0) goto nexti;
                            }
                        }
                        for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                        {
                            int Tah1 = 0;
                            int Tah2 = 0;
                            int X3 = Joint.Xelev[Shell.JointNo[i, j]];
                            int Y3 = Joint.Yelev[Shell.JointNo[i, j]];
                            int X4 = 0;
                            int Y4 = 0;
                            if (j != Shell.PointNumbers[i])
                            {
                                X4 = Joint.Xelev[Shell.JointNo[i, j + 1]];
                                Y4 = Joint.Yelev[Shell.JointNo[i, j + 1]];
                            }
                            else
                            {
                                X4 = Joint.Xelev[Shell.JointNo[i, 1]];
                                Y4 = Joint.Yelev[Shell.JointNo[i, 1]];
                            }
                            if (X3 >= MinX & X3 <= MaxX & Y3 >= MinY & Y3 <= MaxY) Tah1 = 1;
                            if (X4 >= MinX & X4 <= MaxX & Y4 >= MinY & Y4 <= MaxY) Tah2 = 1;
                            if (thecase == 1)
                            {
                                if (Tah1 == 1 & Tah2 == 1)
                                {
                                    ifselected = 1;
                                    Shell.SelectedLine[i, j] = 1;
                                }
                            }
                            if (thecase == 2)
                            {
                                if (Tah1 == 1 || Tah2 == 1)
                                {
                                    ifselected = 1;
                                    Shell.SelectedLine[i, j] = 1;
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
                                    Shell.SelectedLine[i, j] = 1;
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
                                    Shell.SelectedLine[i, j] = 1;
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
                                    Shell.SelectedLine[i, j] = 1;
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
                                    Shell.SelectedLine[i, j] = 1;
                                    goto end100;
                                }
                            }
                        end100: { }
                        }
                    }
                nexti: { };
                }
                #endregion
                if (ifselected == 1)
                {
                    DROWcls callmee = new DROWcls();
                    callmee.Render2d();
                    callmee.Render3d();
                    callmee.Renderelev();
                }
                Bitmap finalBmp = new Bitmap(Myglobals.BitampWidthelev, Myglobals.BitampHightelev);
                if (pictureBox6.Image != null)
                {
                    pictureBox6.Image.Dispose();
                    pictureBox6.Image = null;
                }
                pictureBox6.Image = finalBmp;
            }
            #endregion
        endloop: { }
        }
        private void pictureBox6Draw()
        {
            Bitmap finalBmp = new Bitmap(Myglobals.BitampWidthelev, Myglobals.BitampHightelev);
            if (pictureBox6.Image != null)
            {
                pictureBox6.Image.Dispose();
                pictureBox6.Image = null;
            }
            pictureBox6.Image = finalBmp;
            #region//رسم مربع التحديد
            if (toolStripButton1.Checked == true & MouseButtonsLeft == 1)
            {
                Graphics g = Graphics.FromImage(pictureBox6.Image);
                Pen pen = new Pen(Color.Black, 1f);
                float[] dashValues = { 5, 3, 5, 3 };
                pen.DashPattern = dashValues;
                Point[] polygon = new Point[5];
                polygon[0].X = Myglobals.xmove1;
                polygon[0].Y = Myglobals.ymove1;
                polygon[1].X = TempX;
                polygon[1].Y = Myglobals.ymove1;
                polygon[2].X = TempX;
                polygon[2].Y = TempY;
                polygon[3].X = Myglobals.xmove1;
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
            #region//رسم بلاطة مستطيل
            if (toolStripButton9.Checked == true & Myglobals.drowclick == 1)
            {
                Graphics g = Graphics.FromImage(pictureBox6.Image);
                Pen pen = new Pen(Color.Gray, 1f);
                float[] dashValues = { 5, 3, 5, 3 };
                pen.DashPattern = dashValues;
                Font drawFont = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
                SolidBrush drawBrush = new SolidBrush(Color.Blue);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                Point[] polygon = new Point[5];
                double thelength = 0;
                int thex = 0;
                int they = 0;
                polygon[0].X = LineMoveX1;
                polygon[0].Y = LineMoveY1;
                polygon[1].X = TempX;
                polygon[1].Y = LineMoveY1;
                polygon[2].X = TempX;
                polygon[2].Y = TempY;
                polygon[3].X = LineMoveX1;
                polygon[3].Y = TempY;
                polygon[4] = polygon[0];
                g.DrawLine(pen, polygon[0], polygon[1]);
                g.DrawLine(pen, polygon[1], polygon[2]);
                g.DrawLine(pen, polygon[2], polygon[3]);
                g.DrawLine(pen, polygon[3], polygon[4]);

                thelength = Math.Sqrt(Math.Pow(TempXReal[1] - TempX12Real, 2) + Math.Pow(TempYReal[1] - TempY12Real, 2));
                // thelength = Math.Round(Math.Abs(TempXReal[1] - TempX12Real), 3);
                thex = (polygon[0].X + polygon[1].X) / 2;
                they = (polygon[0].Y + polygon[1].Y) / 2;
                g.DrawString(thelength.ToString() + " m", drawFont, drawBrush, thex - 15, they - 15);
                thex = (polygon[2].X + polygon[3].X) / 2;
                they = (polygon[2].Y + polygon[3].Y) / 2;
                g.DrawString(thelength.ToString() + " m", drawFont, drawBrush, thex - 15, they - 15);
                // thelength = Math.Round(Math.Abs(TempYReal[1] - TempY12Real), 3);
                thelength = Math.Round(Math.Abs(TempZReal[1] - TempZ12Real), 3);
                thex = (polygon[1].X + polygon[2].X) / 2;
                they = (polygon[1].Y + polygon[2].Y) / 2;
                g.DrawString(thelength.ToString() + " m", drawFont, drawBrush, thex - 15, they - 15);
                thex = (polygon[0].X + polygon[3].X) / 2;
                they = (polygon[0].Y + polygon[3].Y) / 2;
                g.DrawString(thelength.ToString() + " m", drawFont, drawBrush, thex - 15, they - 15);
                using (Brush brush = new SolidBrush(Color.FromArgb(30, Color.LightSkyBlue)))
                {
                    g.FillPolygon(brush, polygon);
                }
            }
            #endregion
            #region//رسم بلاطة بالنقاط
            if (toolStripButton8.Checked == true & Myglobals.drowclick == 1)
            {
                Graphics g = Graphics.FromImage(pictureBox6.Image);
                Pen pen = new Pen(Color.Red, 1f);
                float[] dashValues = { 5, 3, 5, 3 };
                pen = new Pen(Color.Gray, 1f);
                pen.DashPattern = dashValues;
                double thelength = 0;
                int thex = 0;
                int they = 0;
                Font drawFont = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
                SolidBrush drawBrush = new SolidBrush(Color.Blue);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                Point[] polygon = new Point[Shell.PointNumbersTemp + 2];
                for (int i = 0; i < Shell.PointNumbersTemp; i++)
                {
                    double tx1D = Myglobals.elevPointX[Myglobals.Selectedelev, 1];
                    double ty1D = Myglobals.elevPointY[Myglobals.Selectedelev, 1];
                    double llength = Math.Sqrt(Math.Pow(Shell.PointXTemp[i + 1] - tx1D, 2) + Math.Pow(Shell.PointYTemp[i + 1] - ty1D, 2));
                    double ALFA1 = (double)Math.Round(Angulo(tx1D, ty1D, Shell.PointXTemp[i + 1], Shell.PointYTemp[i + 1]), 2);
                    ALFA1 = ALFA1 * Math.PI / 180;
                    double shara = Math.Cos(ALFA1) / Math.Abs(Math.Cos(ALFA1));
                    llength = llength * shara;
                    LineMoveX1 = Myglobals.startXelev + Convert.ToInt32((llength) * Myglobals.Zoomelev);
                    LineMoveY1 = Myglobals.startYelev - Convert.ToInt32((Shell.PointZTemp[i + 1]) * Myglobals.Zoomelev);
                    polygon[i].X = LineMoveX1;
                    polygon[i].Y = LineMoveY1;
                }
                for (int i = 0; i < Shell.PointNumbersTemp; i++)
                {
                    g.DrawString((i + 1).ToString(), drawFont, drawBrush, polygon[i].X - 15, polygon[i].Y - 15);
                }
                for (int i = 0; i < Shell.PointNumbersTemp - 1; i++)
                {
                    g.DrawLine(pen, polygon[i], polygon[i + 1]);
                    thelength = Math.Round(Math.Sqrt(Math.Pow(Shell.PointXTemp[i + 1] - Shell.PointXTemp[i + 2], 2) + Math.Pow(Shell.PointYTemp[i + 1] - Shell.PointYTemp[i + 2], 2) + Math.Pow(Shell.PointZTemp[i + 1] - Shell.PointZTemp[i + 2], 2)), 3);
                    thex = (polygon[i].X + polygon[i + 1].X) / 2;
                    they = (polygon[i].Y + polygon[i + 1].Y) / 2;
                    g.DrawString(thelength.ToString() + " m", drawFont, drawBrush, thex - 15, they - 15);
                }
                g.DrawString((Shell.PointNumbersTemp + 1).ToString(), drawFont, drawBrush, TempX - 15, TempY - 15);

                polygon[Shell.PointNumbersTemp].X = TempX;
                polygon[Shell.PointNumbersTemp].Y = TempY;
                polygon[Shell.PointNumbersTemp + 1] = polygon[0];
                g.DrawLine(pen, polygon[0], polygon[Shell.PointNumbersTemp]);
                g.DrawLine(pen, polygon[Shell.PointNumbersTemp - 1], polygon[Shell.PointNumbersTemp]);

                thelength = Math.Round(Math.Sqrt(Math.Pow(TempX12Real - Shell.PointXTemp[Shell.PointNumbersTemp], 2) + Math.Pow(TempY12Real - Shell.PointYTemp[Shell.PointNumbersTemp], 2) + Math.Pow(TempZ12Real - Shell.PointZTemp[Shell.PointNumbersTemp], 2)), 3);
                thex = (TempX + polygon[Shell.PointNumbersTemp - 1].X) / 2;
                they = (TempY + polygon[Shell.PointNumbersTemp - 1].Y) / 2;
                g.DrawString(thelength.ToString() + " m", drawFont, drawBrush, thex - 15, they - 15);

                thelength = Math.Round(Math.Sqrt(Math.Pow(TempX12Real - Shell.PointXTemp[1], 2) + Math.Pow(TempY12Real - Shell.PointYTemp[1], 2) + Math.Pow(TempZ12Real - Shell.PointZTemp[1], 2)), 3);
                thex = (TempX + polygon[0].X) / 2;
                they = (TempY + polygon[0].Y) / 2;
                g.DrawString(thelength.ToString() + " m", drawFont, drawBrush, thex - 15, they - 15);

                using (Brush brush = new SolidBrush(Color.FromArgb(30, Color.LightSkyBlue)))
                {
                    g.FillPolygon(brush, polygon);
                }
            }
            #endregion
            #region//تحديد البلاطة بالرسم السريع
            if (toolStripButton10.Checked == true & Myglobals.AriaSelectedELEV != 0)
            {
                Graphics g = Graphics.FromImage(pictureBox6.Image);
                Pen pen = new Pen(Color.Red, 2f);
                int i = Myglobals.AriaSelectedELEV;
                int N = Myglobals.AriaPointNoELEV[i];
                Point[] polygon = new Point[N + 1];
                for (int j = 1; j < N + 1; j++)
                {
                    //polygon[j].X = Myglobals.startX2d + Convert.ToInt32((Myglobals.p1X[i, j] * Myglobals.Zoom2d));
                    // polygon[j].Y = Myglobals.startY2d - Convert.ToInt32((Myglobals.p1Y[i, j] * Myglobals.Zoom2d));
                    polygon[j].X = Convert.ToInt32((Myglobals.p1XELEV[i, j]));
                    polygon[j].Y = Convert.ToInt32((Myglobals.p1YELEV[i, j]));
                }
                float[] dashValues = { 5, 3, 5, 3 };
                //pen = new Pen(Color.Black, 1f);
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
            pictureBox6Drawsub();
        }
        private void pictureBox6Drawsub()
        {
            Graphics g = Graphics.FromImage(pictureBox6.Image);
            Pen pen = new Pen(Color.Gray, 1f);
            Font drawFont = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
            SolidBrush drawBrush = new SolidBrush(Color.Blue);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            float[] dashValues = { 2, 3 };
            pen.DashPattern = dashValues;
            double tx1D = Myglobals.elevPointX[Myglobals.Selectedelev, 1];
            double ty1D = Myglobals.elevPointY[Myglobals.Selectedelev, 1];
            double llength = Math.Sqrt(Math.Pow(TempXReal[1] - tx1D, 2) + Math.Pow(TempYReal[1] - ty1D, 2));
            double ALFA1 = (double)Math.Round(Angulo(tx1D, ty1D, TempXReal[1], TempYReal[1]), 2);
            ALFA1 = ALFA1 * Math.PI / 180;
            double shara = Math.Cos(ALFA1) / Math.Abs(Math.Cos(ALFA1));
            llength = llength * shara;
            LineMoveX1 = Myglobals.startXelev + Convert.ToInt32((llength) * Myglobals.Zoomelev);
            LineMoveY1 = Myglobals.startYelev - Convert.ToInt32((TempZReal[1]) * Myglobals.Zoomelev);
            //LineMoveX1 = Myglobals.startXelev + Convert.ToInt32((XelevR) * Myglobals.Zoomelev);
            // LineMoveY1 = Myglobals.startYelev - Convert.ToInt32((YelevR) * Myglobals.Zoomelev);
            if (Myglobals.LineMoveelevVisible == 1)
            {
                if (toolStripButton11.Checked == true || toolStripButton3.Checked == true)
                {
                    g.DrawLine(pen, LineMoveX1, LineMoveY1, TempX, TempY);// رسم خط الجوائز
                    double thelength = Math.Round(Math.Sqrt(Math.Pow(TempXReal[1] - TempX12Real, 2) + Math.Pow(TempYReal[1] - TempY12Real, 2) + Math.Pow(TempZReal[1] - TempZ12Real, 2)), 3);
                    int thex = (TempX + LineMoveX1) / 2;
                    int they = (TempY + LineMoveY1) / 2;
                    g.DrawString(thelength.ToString() + " m", drawFont, drawBrush, thex - 15, they - 15);
                    int width = 60;
                    int height = 60;
                    int startAngle = 0;
                    double llength1 = Math.Sqrt(Math.Pow(TempX12Real, 2) + Math.Pow(TempY12Real, 2));
                    if (TempY12Real < 0 & TempX12Real >= 0) llength1 = -llength1;
                    if (TempX12Real < 0 & TempY12Real >= 0) llength1 = -llength1;
                    float sweepAngle = (float)Math.Round(Angulo(LineMoveX1, LineMoveY1, TempX, TempY), 2);
                    thex = LineMoveX1;
                    they = LineMoveY1;
                    pen = new Pen(Color.Gray, 1f);
                    if (sweepAngle <= 180)
                    {
                        g.DrawString(sweepAngle.ToString() + " ْ", drawFont, drawBrush, thex + 32, they - 15);
                        g.DrawArc(pen, LineMoveX1 - 30, LineMoveY1 - 30, width, height, startAngle, sweepAngle);
                    }
                    else
                    {
                        g.DrawString(Math.Round((360 - sweepAngle), 2).ToString() + " ْ", drawFont, drawBrush, thex + 32, they - 15);
                        g.DrawArc(pen, LineMoveX1 - 30, LineMoveY1 - 30, width, height, startAngle, -360 + sweepAngle);
                    }
                    g.DrawLine(pen, LineMoveX1, LineMoveY1, LineMoveX1 + 30, LineMoveY1);
                    pen = new Pen(Color.LightGreen, 1f);
                    pen.DashPattern = dashValues;
                    if (Math.Abs(sweepAngle - 90) < 0.8)
                    {
                        g.DrawLine(pen, LineMoveX1, LineMoveY1, LineMoveX1, LineMoveY1 + 1000);
                    }
                    if (Math.Abs(sweepAngle - 180) < 0.8)
                    {
                        g.DrawLine(pen, LineMoveX1, LineMoveY1, LineMoveX1 - 1000, LineMoveY1);
                    }
                    if (Math.Abs(sweepAngle - 270) < 0.8)
                    {
                        g.DrawLine(pen, LineMoveX1, LineMoveY1, LineMoveX1, LineMoveY1 - 1000);
                    }

                    if (Math.Abs(sweepAngle - 0) < 0.8 || Math.Abs(sweepAngle - 360) < 0.8)
                    {
                        g.DrawLine(pen, LineMoveX1, LineMoveY1, LineMoveX1 + 1000, LineMoveY1);
                    }
                }
            }
            g = Graphics.FromImage(pictureBox6.Image);//رسم نقطة تقاطع الشبكة
            pen = new Pen(Color.Red, 1f);
            if (Tahkik == 1 & SnapFromType == "GridIntersections")
            {
                g.DrawLine(pen, TempX + 5, TempY, TempX - 5, TempY);
                g.DrawLine(pen, TempX, TempY + 5, TempX, TempY - 5);
                drawBrush = new SolidBrush(Color.Black);
                g.DrawString("Grid Point " + Myglobals.elevPointName[Myglobals.Selectedelev, SelectedGridpointelev].ToString(), drawFont, drawBrush, TempX + 10, TempY - 15);
            }
            if (Tahkik == 1 & SnapFromType == "Joints")
            {
                g.DrawLine(pen, TempX + 5, TempY + 5, TempX - 5, TempY + 5);
                g.DrawLine(pen, TempX + 5, TempY - 5, TempX - 5, TempY - 5);
                g.DrawLine(pen, TempX + 5, TempY - 5, TempX + 5, TempY + 5);
                g.DrawLine(pen, TempX - 5, TempY - 5, TempX - 5, TempY + 5);
                drawBrush = new SolidBrush(Color.Black);
                g.DrawString("Joint", drawFont, drawBrush, TempX + 10, TempY - 15);
            }
            if (Tahkik == 1 & SnapFromType == "LineEndsandMidpoints")
            {
                g.DrawLine(pen, TempX + 5, TempY + 5, TempX - 5, TempY + 5);
                g.DrawLine(pen, TempX - 5, TempY + 5, TempX, TempY - 5);
                g.DrawLine(pen, TempX + 5, TempY + 5, TempX, TempY - 5);
                drawBrush = new SolidBrush(Color.Black);
                g.DrawString("Mid Point", drawFont, drawBrush, TempX + 10, TempY - 15);
            }
            if (Tahkik == 1 & SnapFromType == "LinesandFrames" & Myglobals.IfAnalysis == 0)
            {
                g.DrawLine(pen, TempX + 5, TempY + 5, TempX - 5, TempY + 5);
                g.DrawLine(pen, TempX + 5, TempY - 5, TempX - 5, TempY - 5);
                g.DrawLine(pen, TempX + 5, TempY + 5, TempX - 5, TempY - 5);
                g.DrawLine(pen, TempX - 5, TempY + 5, TempX + 5, TempY - 5);
                drawBrush = new SolidBrush(Color.Black);
                g.DrawString("Line/Edge", drawFont, drawBrush, TempX + 10, TempY - 15);
            }
            if (Tahkik == 1 & SnapFromType == "LinesandFrames" & Myglobals.IfAnalysis == 1 & Myglobals.DrawDiagram == 1)
            {
                drawBrush = new SolidBrush(Color.Black);
                g.DrawString(TheResultValue.ToString(), drawFont, drawBrush, TempX + 10, TempY - 15);
                pen = new Pen(Color.Red, 3f);
                g.DrawEllipse(pen, TempX - 10, TempY - 10, 20, 20);
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
            if (Tahkik == 1 & SnapFromType == "Prallels" & Myglobals.LineMoveelevVisible == 1)
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
        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            Bitmap finalBmp = new Bitmap(Myglobals.BitampWidthelev, Myglobals.BitampHightelev);
            if (pictureBox6.Image != null)
            {
                pictureBox6.Image.Dispose();
                pictureBox6.Image = null;
            }
            pictureBox6.Image = finalBmp;
        }
        #endregion
        
        #region//saveopen
        private void Openmenu_Click(object sender, EventArgs e)
        {
            OpenReal();
        }
        private void Savemenu_Click(object sender, EventArgs e)
        {
            SaveReal();
        }
        private void OpenReal()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "e:\\";
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
                    for (int i = 1; i < GridLine.OnX + GridLine.OnY + GridLine.OnXY + 1; i++)
                    {
                        GridLine.Name[i] = Convert.ToString(SW.ReadLine());
                        GridLine.X1Real[i] = Convert.ToDouble(SW.ReadLine());
                        GridLine.Y1Real[i] = Convert.ToDouble(SW.ReadLine());
                        GridLine.X2Real[i] = Convert.ToDouble(SW.ReadLine());
                        GridLine.Y2Real[i] = Convert.ToDouble(SW.ReadLine());
                        GridLine.Visible[i] = Convert.ToInt32(SW.ReadLine());
                        GridLine.Distance[i] = Convert.ToDouble(SW.ReadLine());
                    }
                    Myglobals.StoryNumbers = Convert.ToInt32(SW.ReadLine());
                    for (int i = 1; i < Myglobals.StoryNumbers + 1; i++)
                    {
                        Myglobals.StoryName[i] = Convert.ToString(SW.ReadLine());
                        Myglobals.StoryHight[i] = Convert.ToDouble(SW.ReadLine());
                        Myglobals.StoryLevel[i] = Convert.ToDouble(SW.ReadLine());
                    }
                    Joint.Number3d = Convert.ToInt32(SW.ReadLine());
                    Joint.Number2d = Joint.Number3d;
                    for (int i = 1; i < Joint.Number3d + 1; i++)
                    {
                        Joint.XReal[i] = Convert.ToDouble(SW.ReadLine());
                        Joint.YReal[i] = Convert.ToDouble(SW.ReadLine());
                        Joint.ZReal[i] = Convert.ToDouble(SW.ReadLine());
                        Joint.FixX[i] = Convert.ToInt32(SW.ReadLine());
                        Joint.FixY[i] = Convert.ToInt32(SW.ReadLine());
                        Joint.FixZ[i] = Convert.ToInt32(SW.ReadLine());
                        Joint.FixRX[i] = Convert.ToInt32(SW.ReadLine());
                        Joint.FixRY[i] = Convert.ToInt32(SW.ReadLine());
                        Joint.FixRZ[i] = Convert.ToInt32(SW.ReadLine());
                        Joint.PowerX[i] = Convert.ToDouble(SW.ReadLine());
                        Joint.PowerY[i] = Convert.ToDouble(SW.ReadLine());
                        Joint.PowerZ[i] = Convert.ToDouble(SW.ReadLine());
                        Joint.MomentXX[i] = Convert.ToDouble(SW.ReadLine());
                        Joint.MomentYY[i] = Convert.ToDouble(SW.ReadLine());
                        Joint.MomentZZ[i] = Convert.ToDouble(SW.ReadLine());
                    }
                    Frame.Number = Convert.ToInt32(SW.ReadLine());
                    for (int i = 1; i < Frame.Number + 1; i++)
                    {
                        FrameElements emp = new FrameElements();
                        FrameElement[i] = emp;
                        FrameElement[i].FirstJoint = Convert.ToInt32(SW.ReadLine());
                        FrameElement[i].SecondJoint = Convert.ToInt32(SW.ReadLine());
                        FrameElement[i].Section = Convert.ToInt32(SW.ReadLine());
                        FrameElement[i].RotateAngel = Convert.ToDouble(SW.ReadLine());
                        FrameElement[i].LoadDNumber = Convert.ToInt32(SW.ReadLine());
                        for (int j = 1; j < FrameElement[i].LoadDNumber + 1; j++)
                        {
                            FrameElement[i].LoadDValue1[j ] = Convert.ToDouble(SW.ReadLine());
                            FrameElement[i].LoadDValue2[j ] = Convert.ToDouble(SW.ReadLine());
                            FrameElement[i].LoadDValue3[j ] = Convert.ToDouble(SW.ReadLine());
                            FrameElement[i].LoadDValue4[j ] = Convert.ToDouble(SW.ReadLine());
                            FrameElement[i].LoadDDistance1[j ] = Convert.ToDouble(SW.ReadLine());
                            FrameElement[i].LoadDDistance2[j ] = Convert.ToDouble(SW.ReadLine());
                            FrameElement[i].LoadDDistance3[j ] = Convert.ToDouble(SW.ReadLine());
                            FrameElement[i].LoadDDistance4[j ] = Convert.ToDouble(SW.ReadLine());
                            FrameElement[i].LoadDType[j] = Convert.ToInt32(SW.ReadLine());
                            FrameElement[i].LoadDUniform[j] = Convert.ToDouble(SW.ReadLine());
                            FrameElement[i].LoadDDirection[j] = Convert.ToInt32(SW.ReadLine());
                            FrameElement[i].LoadDPattern[j] = Convert.ToInt32(SW.ReadLine());
                        }
                        FrameElement[i].LoadPNumber = Convert.ToInt32(SW.ReadLine());
                        for (int j = 1; j <  FrameElement[i].LoadPNumber + 1; j++)
                        {
                            FrameElement[i].LoadPValue[j] = Convert.ToDouble(SW.ReadLine());
                            FrameElement[i].LoadPDistance[j] = Convert.ToDouble(SW.ReadLine());
                            FrameElement[i].LoadPType[j] = Convert.ToInt32(SW.ReadLine());
                            FrameElement[i].LoadPDirection[j] = Convert.ToInt32(SW.ReadLine());
                            FrameElement[i].LoadPPattern[j] = Convert.ToInt32(SW.ReadLine());
                        }
                    }
                    Shell.Number = Convert.ToInt32(SW.ReadLine());
                    for (int i = 1; i < Shell.Number + 1; i++)
                    {
                        Shell.PointNumbers[i] = Convert.ToInt32(SW.ReadLine());
                        Shell.Type[i] = Convert.ToInt32(SW.ReadLine());
                        Shell.Section[i] = Convert.ToInt32(SW.ReadLine());
                        for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                        {
                            Shell.JointNo[i, j] = Convert.ToInt32(SW.ReadLine());
                        }

                        Shell.LoadNumber[i] = Convert.ToInt32(SW.ReadLine());
                        for (int j = 1; j < Shell.LoadNumber[i] + 1; j++)
                        {
                            Shell.LoadPattern[i, j] = Convert.ToInt32(SW.ReadLine());
                            Shell.LoadType[i, j] = Convert.ToInt32(SW.ReadLine());
                            Shell.LoadDirection[i, j] = Convert.ToInt32(SW.ReadLine());
                            Shell.LoadUniform[i, j] = Convert.ToDouble(SW.ReadLine());
                        }
                    }
                    for (int i = 1; i < Joint.Number3d + 1; i++)
                    {
                        Joint.BeamConnectionN[i] = Convert.ToInt32(SW.ReadLine());
                        for (int j = 1; j < Joint.BeamConnectionN[i] + 1; j++)
                        {
                            Joint.Beam[i, j] = Convert.ToInt32(SW.ReadLine());
                        }
                        Joint.FloorConnectionN[i] = Convert.ToInt32(SW.ReadLine());
                        for (int j = 1; j < Joint.FloorConnectionN[i] + 1; j++)
                        {
                            Joint.Floor[i, j] = Convert.ToInt32(SW.ReadLine());
                        }
                    }
                    Loads.Number = Convert.ToInt32(SW.ReadLine());
                    for (int i = 1; i < Loads.Number + 1; i++)
                    {
                        Loads.Load[i] = Convert.ToString(SW.ReadLine());
                        Loads.Type[i] = Convert.ToString(SW.ReadLine());
                        Loads.SelfWeight[i] = Convert.ToDouble(SW.ReadLine());
                        Loads.AutoLateral[i] = Convert.ToString(SW.ReadLine());
                    }
                    LoadsCombination.Number = Convert.ToInt32(SW.ReadLine());
                    for (int i = 1; i < LoadsCombination.Number + 1; i++)
                    {
                        LoadsCombination.Name[i] = SW.ReadLine();
                        LoadsCombination.Type[i] = SW.ReadLine();
                        LoadsCombination.NumberRow[i] = Convert.ToInt32(SW.ReadLine());
                        for (int j = 1; j < LoadsCombination.NumberRow[i] + 1; j++)
                        {
                            LoadsCombination.LoadName[i, j] = SW.ReadLine();
                            LoadsCombination.ScaleFactor[i, j] = Convert.ToDouble(SW.ReadLine());
                        }
                    }
                   
                    Section.Number = Convert.ToInt32(SW.ReadLine());////////////المقاطع
                    for (int add = 1; add < Section.Number + 1; add++)
                    {
                        (Section.DESCRIPTION[add]) = Convert.ToString(SW.ReadLine());
                        (Section.LENGTH_UNITS[add]) = Convert.ToString(SW.ReadLine());
                        (Section.MODEL[add]) = Convert.ToString(SW.ReadLine());
                        (Section.LABEL[add]) = Convert.ToString(SW.ReadLine());
                        (Section.EDI_STD[add]) = Convert.ToString(SW.ReadLine());
                        (Section.DESIGNATION[add]) = Convert.ToString(SW.ReadLine());
                        (Section.D[add]) = Convert.ToDouble(SW.ReadLine());
                        (Section.B[add]) = Convert.ToDouble(SW.ReadLine());
                        (Section.BF[add]) = Convert.ToDouble(SW.ReadLine());
                        (Section.TF[add]) = Convert.ToDouble(SW.ReadLine());
                        (Section.BFB[add]) = Convert.ToDouble(SW.ReadLine());
                        (Section.TFB[add]) = Convert.ToDouble(SW.ReadLine());
                        (Section.TW[add]) = Convert.ToDouble(SW.ReadLine());
                        (Section.A[add]) = Convert.ToDouble(SW.ReadLine());
                        (Section.I33[add]) = Convert.ToDouble(SW.ReadLine());
                        (Section.Z33[add]) = Convert.ToDouble(SW.ReadLine());
                        (Section.AS3[add]) = Convert.ToDouble(SW.ReadLine());
                        (Section.I22[add]) = Convert.ToDouble(SW.ReadLine());
                        (Section.Z22[add]) = Convert.ToDouble(SW.ReadLine());
                        (Section.AS2[add]) = Convert.ToDouble(SW.ReadLine());
                        (Section.J[add]) = Convert.ToDouble(SW.ReadLine());
                        (Section.X[add]) = Convert.ToDouble(SW.ReadLine());
                        (Section.Y[add]) = Convert.ToDouble(SW.ReadLine());
                        (Section.S33POS[add]) = Convert.ToDouble(SW.ReadLine());
                        (Section.S33NEG[add]) = Convert.ToDouble(SW.ReadLine());
                        (Section.S22POS[add]) = Convert.ToDouble(SW.ReadLine());
                        (Section.S22NEG[add]) = Convert.ToDouble(SW.ReadLine());
                        (Section.R33[add]) = Convert.ToDouble(SW.ReadLine());
                        (Section.R22[add]) = Convert.ToDouble(SW.ReadLine());
                        (Section.HT[add]) = Convert.ToDouble(SW.ReadLine());
                        (Section.OD[add]) = Convert.ToDouble(SW.ReadLine());
                        (Section.TDES[add]) = Convert.ToDouble(SW.ReadLine());
                        (Section.Material[add]) = Convert.ToInt32(SW.ReadLine());
                    }
                    Material.Number = Convert.ToInt32(SW.ReadLine());//المواد
                    for (int add = 1; add < Material.Number + 1; add++)
                    {
                        (Material.Name[add]) = SW.ReadLine();
                        (Material.MType[add]) = Convert.ToInt32(SW.ReadLine());
                        (Material.Summetry[add]) = Convert.ToInt32(SW.ReadLine());
                        (Material.WperV[add]) = Convert.ToDouble(SW.ReadLine());
                        (Material.MperV[add]) = Convert.ToDouble(SW.ReadLine());
                        (Material.Elastisity[add]) = Convert.ToDouble(SW.ReadLine());
                        (Material.Poisson[add]) = Convert.ToDouble(SW.ReadLine());
                        (Material.Thermal[add]) = Convert.ToDouble(SW.ReadLine());
                        (Material.ShearM[add]) = Convert.ToDouble(SW.ReadLine());
                        (Material.fc[add]) = Convert.ToDouble(SW.ReadLine());
                        (Material.LweightCon[add]) = Convert.ToInt32(SW.ReadLine());
                        (Material.MinYeildFy[add]) = Convert.ToDouble(SW.ReadLine());
                        (Material.MinTensileFu[add]) = Convert.ToDouble(SW.ReadLine());
                        (Material.EffYeildFye[add]) = Convert.ToDouble(SW.ReadLine());
                        (Material.EffTensileFue[add]) = Convert.ToDouble(SW.ReadLine());
                    }
                    Slab.Number = Convert.ToInt32(SW.ReadLine());//البلاطات
                    for (int add = 1; add < Slab.Number + 1; add++)
                    {
                        Slab.Name[add] = SW.ReadLine();
                        Slab.Material[add] = Convert.ToInt32(SW.ReadLine());
                        Slab.MType[add] = Convert.ToInt32(SW.ReadLine());
                        Slab.OneWay[add] = Convert.ToInt32(SW.ReadLine());
                        Slab.ProType[add] = Convert.ToInt32(SW.ReadLine());
                        Slab.Thickness[add] = Convert.ToDouble(SW.ReadLine());
                        Slab.OverallDepth[add] = Convert.ToDouble(SW.ReadLine());
                        Slab.SlabThickness[add] = Convert.ToDouble(SW.ReadLine());
                        Slab.StemWidthatTop[add] = Convert.ToDouble(SW.ReadLine());
                        Slab.StemWidthatBottom[add] = Convert.ToDouble(SW.ReadLine());
                        Slab.RibSpacing[add] = Convert.ToDouble(SW.ReadLine());
                        Slab.RibDirection[add] = Convert.ToInt32(SW.ReadLine());
                        Slab.RibSpacing1[add] = Convert.ToDouble(SW.ReadLine());
                        Slab.RibSpacing2[add] = Convert.ToDouble(SW.ReadLine());
                    }
                    Wall.Number = Convert.ToInt32(SW.ReadLine());//الجدران
                    for (int add = 1; add < Wall.Number + 1; add++)
                    {
                        Wall.Name[add] = SW.ReadLine();
                        Wall.Material[add] = Convert.ToInt32(SW.ReadLine());
                        Wall.MType[add] = Convert.ToInt32(SW.ReadLine());
                        Wall.ProType[add] = Convert.ToInt32(SW.ReadLine());
                        Wall.Thickness[add] = Convert.ToDouble(SW.ReadLine());
                    }
                    SW.Close();
                    Array.ForEach(Directory.GetFiles(@"./TempFiles/"), File.Delete);
                    Myglobals.TempFile = 0;
                    Myglobals.TempSelectedFile = 0;
                    MakeTempFiles();
                    UnSellectAll();
                    DROWcls callmee = new DROWcls();
                    callmee.CalculateGridPointReal();
                    callmee.Render2d();
                    callmee.Render3d();
                    callmee.Renderelev();
                }
                catch
                {
                    MessageBox.Show("ERROR! Please check passphrase and do not attempt to edit cipher text");
                }
            }
        }
        private void SaveReal()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = "e:\\";
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
                    for (int i = 1; i < GridLine.OnX + GridLine.OnY + GridLine.OnXY + 1; i++)
                    {
                        SW.WriteLine(GridLine.Name[i]);
                        SW.WriteLine(GridLine.X1Real[i]);
                        SW.WriteLine(GridLine.Y1Real[i]);
                        SW.WriteLine(GridLine.X2Real[i]);
                        SW.WriteLine(GridLine.Y2Real[i]);
                        SW.WriteLine(GridLine.Visible[i]);
                        SW.WriteLine(GridLine.Distance[i]);
                    }
                    SW.WriteLine(Myglobals.StoryNumbers);
                    for (int i = 1; i < Myglobals.StoryNumbers + 1; i++)
                    {
                        SW.WriteLine(Myglobals.StoryName[i]);
                        SW.WriteLine(Myglobals.StoryHight[i]);
                        SW.WriteLine(Myglobals.StoryLevel[i]);
                    }
                    SW.WriteLine(Joint.Number3d);
                    Joint.Number2d = Joint.Number3d;
                    for (int i = 1; i < Joint.Number3d + 1; i++)
                    {
                        SW.WriteLine(Joint.XReal[i]);
                        SW.WriteLine(Joint.YReal[i]);
                        SW.WriteLine(Joint.ZReal[i]);
                        SW.WriteLine(Joint.FixX[i]);
                        SW.WriteLine(Joint.FixY[i]);
                        SW.WriteLine(Joint.FixZ[i]);
                        SW.WriteLine(Joint.FixRX[i]);
                        SW.WriteLine(Joint.FixRY[i]);
                        SW.WriteLine(Joint.FixRZ[i]);
                        SW.WriteLine(Joint.PowerX[i]);
                        SW.WriteLine(Joint.PowerY[i]);
                        SW.WriteLine(Joint.PowerZ[i]);
                        SW.WriteLine(Joint.MomentXX[i]);
                        SW.WriteLine(Joint.MomentYY[i]);
                        SW.WriteLine(Joint.MomentZZ[i]);
                    }
                    SW.WriteLine(Frame.Number);
                    for (int i = 1; i < Frame.Number + 1; i++)
                    {
                        SW.WriteLine(FrameElement[i].FirstJoint);
                        SW.WriteLine(FrameElement[i].SecondJoint);
                        SW.WriteLine(FrameElement[i].Section);
                        SW.WriteLine(FrameElement[i].RotateAngel);
                        SW.WriteLine(FrameElement[i].LoadDNumber);
                        for (int j = 1; j < FrameElement[i].LoadDNumber + 1; j++)
                        {
                            SW.WriteLine(FrameElement[i].LoadDValue1[j ]);
                            SW.WriteLine(FrameElement[i].LoadDValue2[j ]);
                            SW.WriteLine(FrameElement[i].LoadDValue3[j ]);
                            SW.WriteLine(FrameElement[i].LoadDValue4[j ]);
                            SW.WriteLine(FrameElement[i].LoadDDistance1[j ]);
                            SW.WriteLine(FrameElement[i].LoadDDistance2[j ]);
                            SW.WriteLine(FrameElement[i].LoadDDistance3[j ]);
                            SW.WriteLine(FrameElement[i].LoadDDistance4[j ]);
                            SW.WriteLine( FrameElement[i].LoadDType[j]);
                            SW.WriteLine( FrameElement[i].LoadDUniform[j]);
                            SW.WriteLine( FrameElement[i].LoadDDirection[j]);
                            SW.WriteLine( FrameElement[i].LoadDPattern[j]);
                        }
                        SW.WriteLine(FrameElement[i].LoadPNumber);
                        for (int j = 1; j < FrameElement[i].LoadPNumber + 1; j++)
                        {
                            SW.WriteLine(FrameElement[i].LoadPValue[j]);
                            SW.WriteLine(FrameElement[i].LoadPDistance[j]);
                            SW.WriteLine(FrameElement[i].LoadPType[j]);
                            SW.WriteLine(FrameElement[i].LoadPDirection[j]);
                            SW.WriteLine(FrameElement[i].LoadPPattern[j]);
                        }
                    }
                    SW.WriteLine(Shell.Number);
                    for (int i = 1; i < Shell.Number + 1; i++)
                    {
                        SW.WriteLine(Shell.PointNumbers[i]);
                        SW.WriteLine(Shell.Type[i]);
                        SW.WriteLine(Shell.Section[i]);
                        for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                        {
                            SW.WriteLine(Shell.JointNo[i, j]);
                        }

                        SW.WriteLine(Shell.LoadNumber[i]);
                        for (int j = 1; j < Shell.LoadNumber[i] + 1; j++)
                        {
                            SW.WriteLine(Shell.LoadPattern[i, j]);
                            SW.WriteLine(Shell.LoadType[i, j]);
                            SW.WriteLine(Shell.LoadDirection[i, j]);
                            SW.WriteLine(Shell.LoadUniform[i, j]);
                        }
                    }
                    for (int i = 1; i < Joint.Number3d + 1; i++)
                    {
                        SW.WriteLine(Joint.BeamConnectionN[i]);
                        for (int j = 1; j < Joint.BeamConnectionN[i] + 1; j++)
                        {
                            SW.WriteLine(Joint.Beam[i, j]);
                        }
                        SW.WriteLine(Joint.FloorConnectionN[i]);
                        for (int j = 1; j < Joint.FloorConnectionN[i] + 1; j++)
                        {
                            SW.WriteLine(Joint.Floor[i, j]);
                        }
                    }
                    SW.WriteLine(Loads.Number);
                    for (int i = 1; i < Loads.Number + 1; i++)
                    {
                        SW.WriteLine(Loads.Load[i]);
                        SW.WriteLine(Loads.Type[i]);
                        SW.WriteLine(Loads.SelfWeight[i]);
                        SW.WriteLine(Loads.AutoLateral[i]);
                    }
                    SW.WriteLine(LoadsCombination.Number);
                    for (int i = 1; i < LoadsCombination.Number + 1; i++)
                    {
                        SW.WriteLine(LoadsCombination.Name[i]);
                        SW.WriteLine(LoadsCombination.Type[i]);
                        SW.WriteLine(LoadsCombination.NumberRow[i]);
                        for (int j = 1; j < LoadsCombination.NumberRow[i] + 1; j++)
                        {
                            SW.WriteLine(LoadsCombination.LoadName[i, j]);
                            SW.WriteLine(LoadsCombination.ScaleFactor[i, j]);
                        }
                    }
                    SW.WriteLine(Section.Number);////////////المقاطع
                    for (int add = 1; add < Section.Number + 1; add++)
                    {
                        SW.WriteLine(Section.DESCRIPTION[add]) ;
                        SW.WriteLine(Section.LENGTH_UNITS[add]) ;
                        SW.WriteLine(Section.MODEL[add]) ;
                        SW.WriteLine(Section.LABEL[add]) ;
                        SW.WriteLine(Section.EDI_STD[add]) ;
                        SW.WriteLine(Section.DESIGNATION[add]) ;
                        SW.WriteLine(Section.D[add]) ;
                        SW.WriteLine(Section.B[add]) ;
                        SW.WriteLine(Section.BF[add]) ;
                        SW.WriteLine(Section.TF[add]) ;
                        SW.WriteLine(Section.BFB[add]);
                        SW.WriteLine(Section.TFB[add]) ;
                        SW.WriteLine(Section.TW[add]) ;
                        SW.WriteLine(Section.A[add]) ;
                        SW.WriteLine(Section.I33[add]) ;
                        SW.WriteLine(Section.Z33[add]) ;
                        SW.WriteLine(Section.AS3[add]);
                        SW.WriteLine(Section.I22[add]) ;
                        SW.WriteLine(Section.Z22[add]) ;
                        SW.WriteLine(Section.AS2[add]) ;
                        SW.WriteLine(Section.J[add]) ;
                        SW.WriteLine(Section.X[add]) ;
                        SW.WriteLine(Section.Y[add]) ;
                        SW.WriteLine(Section.S33POS[add]) ;
                        SW.WriteLine(Section.S33NEG[add]) ;
                        SW.WriteLine(Section.S22POS[add]) ;
                        SW.WriteLine(Section.S22NEG[add]) ;
                        SW.WriteLine(Section.R33[add]) ;
                        SW.WriteLine(Section.R22[add]) ;
                        SW.WriteLine(Section.HT[add]) ;
                        SW.WriteLine(Section.OD[add]) ;
                        SW.WriteLine(Section.TDES[add]) ;
                        SW.WriteLine(Section.Material[add]) ;
                    }
                    SW.WriteLine(Material.Number) ;//المواد
                    for (int add = 1; add < Material.Number + 1; add++)
                    {
                        SW.WriteLine(Material.Name[add]) ;
                        SW.WriteLine(Material.MType[add]) ;
                        SW.WriteLine(Material.Summetry[add]) ;
                        SW.WriteLine(Material.WperV[add]) ;
                        SW.WriteLine(Material.MperV[add]) ;
                        SW.WriteLine(Material.Elastisity[add]) ;
                        SW.WriteLine(Material.Poisson[add]) ;
                        SW.WriteLine(Material.Thermal[add]) ;
                        SW.WriteLine(Material.ShearM[add]) ;
                        SW.WriteLine(Material.fc[add]) ;
                        SW.WriteLine(Material.LweightCon[add]) ;
                        SW.WriteLine(Material.MinYeildFy[add]) ;
                        SW.WriteLine(Material.MinTensileFu[add]) ;
                        SW.WriteLine(Material.EffYeildFye[add]) ;
                        SW.WriteLine(Material.EffTensileFue[add]) ;
                    }
                    SW.WriteLine(Slab.Number );//البلاطات
                    for (int add = 1; add < Slab.Number + 1; add++)
                    {
                        SW.WriteLine(Slab.Name[add] );
                        SW.WriteLine(Slab.Material[add] );
                        SW.WriteLine(Slab.MType[add] );
                        SW.WriteLine(Slab.OneWay[add] );
                        SW.WriteLine(Slab.ProType[add] );
                        SW.WriteLine(Slab.Thickness[add] );
                        SW.WriteLine(Slab.OverallDepth[add] );
                        SW.WriteLine(Slab.SlabThickness[add] );
                        SW.WriteLine(Slab.StemWidthatTop[add] );
                        SW.WriteLine(Slab.StemWidthatBottom[add] );
                        SW.WriteLine(Slab.RibSpacing[add] );
                        SW.WriteLine(Slab.RibDirection[add] );
                        SW.WriteLine(Slab.RibSpacing1[add] );
                        SW.WriteLine(Slab.RibSpacing2[add] );
                    }
                    SW.WriteLine(Wall.Number);//الجدران
                    for (int add = 1; add < Wall.Number + 1; add++)
                    {
                        SW.WriteLine(Wall.Name[add]);
                        SW.WriteLine(Wall.Material[add] );
                        SW.WriteLine(Wall.MType[add] );
                        SW.WriteLine(Wall.ProType[add] );
                        SW.WriteLine(Wall.Thickness[add] );
                    }
                    SW.Close();
                }
                catch
                {
                    MessageBox.Show("ERROR! Please check passphrase and do not attempt to edit cipher text");
                }
            }
        }
        private void OpenTemp()
        {
            Joint.BeamConnectionN = new int[10000];
            Joint.FloorConnectionN = new int[10000];
            Joint.Beam = new int[10000, 10000];
            Joint.Floor = new int[10000, 10000];
            string strpath = Myglobals.TempFileName[Myglobals.TempSelectedFile];
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
            for (int i = 1; i < GridLine.OnX + GridLine.OnY + GridLine.OnXY + 1; i++)
            {
                GridLine.Name[i] = Convert.ToString(SW.ReadLine());
                GridLine.X1Real[i] = Convert.ToDouble(SW.ReadLine());
                GridLine.Y1Real[i] = Convert.ToDouble(SW.ReadLine());
                GridLine.X2Real[i] = Convert.ToDouble(SW.ReadLine());
                GridLine.Y2Real[i] = Convert.ToDouble(SW.ReadLine());
                GridLine.Visible[i] = Convert.ToInt32(SW.ReadLine());
                GridLine.Distance[i] = Convert.ToDouble(SW.ReadLine());
            }
            Myglobals.StoryNumbers = Convert.ToInt32(SW.ReadLine());
            for (int i = 1; i < Myglobals.StoryNumbers + 1; i++)
            {
                Myglobals.StoryName[i] = Convert.ToString(SW.ReadLine());
                Myglobals.StoryHight[i] = Convert.ToDouble(SW.ReadLine());
                Myglobals.StoryLevel[i] = Convert.ToDouble(SW.ReadLine());
            }
            Joint.Number3d = Convert.ToInt32(SW.ReadLine());
            Joint.Number2d = Joint.Number3d;
            for (int i = 1; i < Joint.Number3d + 1; i++)
            {
                Joint.XReal[i] = Convert.ToDouble(SW.ReadLine());
                Joint.YReal[i] = Convert.ToDouble(SW.ReadLine());
                Joint.ZReal[i] = Convert.ToDouble(SW.ReadLine());
                Joint.FixX[i] = Convert.ToInt32(SW.ReadLine());
                Joint.FixY[i] = Convert.ToInt32(SW.ReadLine());
                Joint.FixZ[i] = Convert.ToInt32(SW.ReadLine());
                Joint.FixRX[i] = Convert.ToInt32(SW.ReadLine());
                Joint.FixRY[i] = Convert.ToInt32(SW.ReadLine());
                Joint.FixRZ[i] = Convert.ToInt32(SW.ReadLine());
                Joint.PowerX[i] = Convert.ToDouble(SW.ReadLine());
                Joint.PowerY[i] = Convert.ToDouble(SW.ReadLine());
                Joint.PowerZ[i] = Convert.ToDouble(SW.ReadLine());
                Joint.MomentXX[i] = Convert.ToDouble(SW.ReadLine());
                Joint.MomentYY[i] = Convert.ToDouble(SW.ReadLine());
                Joint.MomentZZ[i] = Convert.ToDouble(SW.ReadLine());
            }
            Frame.Number = Convert.ToInt32(SW.ReadLine());
            for (int i = 1; i < Frame.Number + 1; i++)
            {
                FrameElements emp = new FrameElements();
                FrameElement[i] = emp;
                FrameElement[i].FirstJoint = Convert.ToInt32(SW.ReadLine());
                FrameElement[i].SecondJoint = Convert.ToInt32(SW.ReadLine());
                FrameElement[i].Section = Convert.ToInt32(SW.ReadLine());
                FrameElement[i].RotateAngel = Convert.ToDouble(SW.ReadLine());
                FrameElement[i].LoadDNumber = Convert.ToInt32(SW.ReadLine());
                for (int j = 1; j < FrameElement[i].LoadDNumber + 1; j++)
                {
                    FrameElement[i].LoadDValue1[j ] = Convert.ToDouble(SW.ReadLine());
                    FrameElement[i].LoadDValue2[j ] = Convert.ToDouble(SW.ReadLine());
                    FrameElement[i].LoadDValue3[j ] = Convert.ToDouble(SW.ReadLine());
                    FrameElement[i].LoadDValue4[j ] = Convert.ToDouble(SW.ReadLine());
                    FrameElement[i].LoadDDistance1[j ] = Convert.ToDouble(SW.ReadLine());
                    FrameElement[i].LoadDDistance2[j ] = Convert.ToDouble(SW.ReadLine());
                    FrameElement[i].LoadDDistance3[j ] = Convert.ToDouble(SW.ReadLine());
                    FrameElement[i].LoadDDistance4[j ] = Convert.ToDouble(SW.ReadLine());
                    FrameElement[i].LoadDType[j] = Convert.ToInt32(SW.ReadLine());
                    FrameElement[i].LoadDUniform[j] = Convert.ToDouble(SW.ReadLine());
                    FrameElement[i].LoadDDirection[j] = Convert.ToInt32(SW.ReadLine());
                    FrameElement[i].LoadDPattern[j] = Convert.ToInt32(SW.ReadLine());
                }
                FrameElement[i].LoadPNumber = Convert.ToInt32(SW.ReadLine());
                for (int j = 1; j < FrameElement[i].LoadPNumber + 1; j++)
                {
                    FrameElement[i].LoadPValue[j] = Convert.ToDouble(SW.ReadLine());
                    FrameElement[i].LoadPDistance[j] = Convert.ToDouble(SW.ReadLine());
                    FrameElement[i].LoadPType[j] = Convert.ToInt32(SW.ReadLine());
                    FrameElement[i].LoadPDirection[j] = Convert.ToInt32(SW.ReadLine());
                    FrameElement[i].LoadPPattern[j] = Convert.ToInt32(SW.ReadLine());
                }
            }
            Shell.Number = Convert.ToInt32(SW.ReadLine());
            for (int i = 1; i < Shell.Number + 1; i++)
            {
                Shell.PointNumbers[i] = Convert.ToInt32(SW.ReadLine());
                Shell.Type[i] = Convert.ToInt32(SW.ReadLine());
                Shell.Section[i] = Convert.ToInt32(SW.ReadLine());
                for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                {
                    Shell.JointNo[i, j] = Convert.ToInt32(SW.ReadLine());
                }
                Shell.LoadNumber[i] = Convert.ToInt32(SW.ReadLine());
                for (int j = 1; j < Shell.LoadNumber[i] + 1; j++)
                {
                    Shell.LoadPattern[i, j] = Convert.ToInt32(SW.ReadLine());
                    Shell.LoadType[i, j] = Convert.ToInt32(SW.ReadLine());
                    Shell.LoadDirection[i, j] = Convert.ToInt32(SW.ReadLine());
                    Shell.LoadUniform[i, j] = Convert.ToDouble(SW.ReadLine());
                }
            }
            for (int i = 1; i < Joint.Number3d + 1; i++)
            {
                Joint.BeamConnectionN[i] = Convert.ToInt32(SW.ReadLine());
                for (int j = 1; j < Joint.BeamConnectionN[i] + 1; j++)
                {
                    Joint.Beam[i, j] = Convert.ToInt32(SW.ReadLine());
                }
                Joint.FloorConnectionN[i] = Convert.ToInt32(SW.ReadLine());
                for (int j = 1; j < Joint.FloorConnectionN[i] + 1; j++)
                {
                    Joint.Floor[i, j] = Convert.ToInt32(SW.ReadLine());
                }
            }
            SW.Close();
        }
        private void SaveTemp()
        {
            string strpath = Myglobals.TempFileName[Myglobals.TempSelectedFile];
            StreamWriter SW = new StreamWriter(strpath);
            SW.WriteLine(GridLine.OnX);
            SW.WriteLine(GridLine.OnY);
            SW.WriteLine(GridLine.OnXY);
            for (int i = 1; i < GridLine.OnX + GridLine.OnY + GridLine.OnXY + 1; i++)
            {
                SW.WriteLine(GridLine.Name[i]);
                SW.WriteLine(GridLine.X1Real[i]);
                SW.WriteLine(GridLine.Y1Real[i]);
                SW.WriteLine(GridLine.X2Real[i]);
                SW.WriteLine(GridLine.Y2Real[i]);
                SW.WriteLine(GridLine.Visible[i]);
                SW.WriteLine(GridLine.Distance[i]);
            }
            SW.WriteLine(Myglobals.StoryNumbers);
            for (int i = 1; i < Myglobals.StoryNumbers + 1; i++)
            {
                SW.WriteLine(Myglobals.StoryName[i]);
                SW.WriteLine(Myglobals.StoryHight[i]);
                SW.WriteLine(Myglobals.StoryLevel[i]);
            }
            SW.WriteLine(Joint.Number3d);
            Joint.Number2d = Joint.Number3d;
            for (int i = 1; i < Joint.Number3d + 1; i++)
            {
                SW.WriteLine(Joint.XReal[i]);
                SW.WriteLine(Joint.YReal[i]);
                SW.WriteLine(Joint.ZReal[i]);
                SW.WriteLine(Joint.FixX[i]);
                SW.WriteLine(Joint.FixY[i]);
                SW.WriteLine(Joint.FixZ[i]);
                SW.WriteLine(Joint.FixRX[i]);
                SW.WriteLine(Joint.FixRY[i]);
                SW.WriteLine(Joint.FixRZ[i]);
                SW.WriteLine(Joint.PowerX[i]);
                SW.WriteLine(Joint.PowerY[i]);
                SW.WriteLine(Joint.PowerZ[i]);
                SW.WriteLine(Joint.MomentXX[i]);
                SW.WriteLine(Joint.MomentYY[i]);
                SW.WriteLine(Joint.MomentZZ[i]);
            }
            SW.WriteLine(Frame.Number);
            for (int i = 1; i < Frame.Number + 1; i++)
            {
                SW.WriteLine(FrameElement[i].FirstJoint);
                SW.WriteLine(FrameElement[i].SecondJoint);
                SW.WriteLine(FrameElement[i].Section);
                SW.WriteLine(FrameElement[i].RotateAngel);
                SW.WriteLine(FrameElement[i].LoadDNumber);
                for (int j = 1; j < FrameElement[i].LoadDNumber + 1; j++)
                {
                    SW.WriteLine(FrameElement[i].LoadDValue1[j ]);
                    SW.WriteLine(FrameElement[i].LoadDValue2[j ]);
                    SW.WriteLine(FrameElement[i].LoadDValue3[j ]);
                    SW.WriteLine(FrameElement[i].LoadDValue4[j ]);
                    SW.WriteLine(FrameElement[i].LoadDDistance1[j ]);
                    SW.WriteLine(FrameElement[i].LoadDDistance2[j ]);
                    SW.WriteLine(FrameElement[i].LoadDDistance3[j ]);
                    SW.WriteLine(FrameElement[i].LoadDDistance4[j ]);
                    SW.WriteLine(FrameElement[i].LoadDType[j]);
                    SW.WriteLine(FrameElement[i].LoadDUniform[j]);
                    SW.WriteLine(FrameElement[i].LoadDDirection[j]);
                    SW.WriteLine(FrameElement[i].LoadDPattern[j]);
                }
                SW.WriteLine(FrameElement[i].LoadPNumber);
                for (int j = 1; j < FrameElement[i].LoadPNumber + 1; j++)
                {
                    SW.WriteLine(FrameElement[i].LoadPValue[j]);
                    SW.WriteLine(FrameElement[i].LoadPDistance[j]);
                    SW.WriteLine(FrameElement[i].LoadPType[j]);
                    SW.WriteLine(FrameElement[i].LoadPDirection[j]);
                    SW.WriteLine(FrameElement[i].LoadPPattern[j]);
                }
            }
            SW.WriteLine(Shell.Number);
            for (int i = 1; i < Shell.Number + 1; i++)
            {
                SW.WriteLine(Shell.PointNumbers[i]);
                SW.WriteLine(Shell.Type[i]);
                SW.WriteLine(Shell.Section[i]);
                for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                {
                    SW.WriteLine(Shell.JointNo[i, j]);
                }
                SW.WriteLine(Shell.LoadNumber[i]);
                for (int j = 1; j < Shell.LoadNumber[i] + 1; j++)
                {
                    SW.WriteLine(Shell.LoadPattern[i, j]);
                    SW.WriteLine(Shell.LoadType[i, j]);
                    SW.WriteLine(Shell.LoadDirection[i, j]);
                    SW.WriteLine(Shell.LoadUniform[i, j]);
                }
            }
            for (int i = 1; i < Joint.Number3d + 1; i++)
            {
                SW.WriteLine(Joint.BeamConnectionN[i]);
                for (int j = 1; j < Joint.BeamConnectionN[i] + 1; j++)
                {
                    SW.WriteLine(Joint.Beam[i, j]);
                }
                SW.WriteLine(Joint.FloorConnectionN[i]);
                for (int j = 1; j < Joint.FloorConnectionN[i] + 1; j++)
                {
                    SW.WriteLine(Joint.Floor[i, j]);
                }
            }
            SW.Close();
        }
        public void MakeTempFiles()
        {
            int M = Myglobals.TempFile;
            for (int i = Myglobals.TempSelectedFile + 1; i < M + 1; i++)
            {
                if ((System.IO.File.Exists(Myglobals.TempFileName[i])))
                {
                    System.IO.File.Delete(Myglobals.TempFileName[i]);
                    Myglobals.TempFile = Myglobals.TempFile - 1;
                }
            }
            Myglobals.TempFile = Myglobals.TempFile + 1;
            Myglobals.TempFileName[Myglobals.TempFile] = Convert.ToString(Directory.GetParent(@"./TempFiles/File" + Myglobals.TempFile + "/"));
            Myglobals.TempSelectedFile = Myglobals.TempFile;
            SaveTemp();
        }
        #endregion
        #region//toolStripButton
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            toolStripButton3.Checked = false;
            toolStripButton1.Checked = false;
            toolStripButton5.Checked = false;
            toolStripButton8.Checked = false;
            toolStripButton9.Checked = false;
            toolStripButton10.Checked = false;
            toolStripButton11.Checked = false;
            toolStripButton6.Checked = true;
            propertyGrid1.Visible = true;
            button14.Visible = true;
            Myglobals.LineMove2dVisible = 0;
            Myglobals.LineMove2dFVisible = 0;
            Myglobals.LineMove3dVisible = 0;
            Myglobals.LineMove3dFVisible = 0;
            Myglobals.LineMoveelevVisible = 0;
            Myglobals.LineMoveelevFVisible = 0;
        }
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            BeamDesignerForm theform = new BeamDesignerForm();
            theform.ShowDialog();
        }
        
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (Myglobals.IfAnalysis == 1)
            {
                Form3 theform = new Form3();
                theform.ShowDialog();
            }
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            SectionDrawForm theform = new SectionDrawForm();
            theform.ShowDialog();
        }
        private void architecturalWallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArchWallFormALL theform = new ArchWallFormALL();
            theform.ShowDialog();
        }
        private void toolStripButton27_Click(object sender, EventArgs e)
        {
            AnalysisModel callmee = new AnalysisModel();
            callmee.AnalysisRun();
            callmee.AnalysisResults();
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
                tabPage3.Text = "Elevation View - " + GridLine.Name[Myglobals.Selectedelev] + ghg;
            }
            else
            {
                tabPage3.Text = "Elevation View - " + GridLine.Name[Myglobals.elevGridLine[Myglobals.Selectedelev]];
            }
            toolStripButton3.Enabled = false;
            toolStripButton5.Enabled = false;
            toolStripButton8.Enabled = false;
            toolStripButton9.Enabled = false;
            toolStripButton10.Enabled = false;
            toolStripButton11.Enabled = false;
            toolStripButton26.Image = Properties.Resources._2;
        }
        private void toolStripButton46_Click(object sender, EventArgs e)
        {
            if (Myglobals.IfAnalysis == 1)
            {
                FramesDiagramChoiceForm framesDiagramChoiceForm = new FramesDiagramChoiceForm();
                framesDiagramChoiceForm.ShowDialog();
            }
        }
        private void toolStripButton28_Click(object sender, EventArgs e)
        {
            int alllines = 9;
            GridLine.Name = new string[alllines];
            GridLine.XOrY = new int[alllines];
            GridLine.Visible = new int[alllines];
            GridLine.X1Real = new double[alllines];
            GridLine.X2Real = new double[alllines];
            GridLine.Y1Real = new double[alllines];
            GridLine.Y2Real = new double[alllines];
            GridLine.Distance = new double[alllines];


            GridLine.OnX = 4;
            GridLine.OnY = 4;
            //GridLine.OnXY = 1;

            GridLine.X1Real[1] = 0;
            GridLine.Y1Real[1] = 0;
            GridLine.X2Real[1] = 0;
            GridLine.Y2Real[1] = 24;
            GridLine.Name[1] = "A";

            GridLine.X1Real[2] = 8;
            GridLine.Y1Real[2] = 0;
            GridLine.X2Real[2] = 8;
            GridLine.Y2Real[2] = 24;
            GridLine.Name[2] = "B";

            GridLine.X1Real[3] = 16;
            GridLine.Y1Real[3] = 0;
            GridLine.X2Real[3] = 16;
            GridLine.Y2Real[3] = 24;
            GridLine.Name[3] = "C";

            GridLine.X1Real[4] = 24;
            GridLine.Y1Real[4] = 0;
            GridLine.X2Real[4] = 24;
            GridLine.Y2Real[4] = 24;
            GridLine.Name[4] = "D";

            GridLine.X1Real[5] = 0;
            GridLine.Y1Real[5] = 0;
            GridLine.X2Real[5] = 24;
            GridLine.Y2Real[5] = 0;
            GridLine.Name[5] = "1";

            GridLine.X1Real[6] = 0;
            GridLine.Y1Real[6] = 8;
            GridLine.X2Real[6] = 24;
            GridLine.Y2Real[6] = 8;
            GridLine.Name[6] = "2";

            GridLine.X1Real[7] = 0;
            GridLine.Y1Real[7] = 16;
            GridLine.X2Real[7] = 24;
            GridLine.Y2Real[7] = 16;
            GridLine.Name[7] = "3";

            GridLine.X1Real[8] = 0;
            GridLine.Y1Real[8] = 24;
            GridLine.X2Real[8] = 24;
            GridLine.Y2Real[8] = 24;
            GridLine.Name[8] = "4";

            //GridLine.X1Real[9] = 1;
            //GridLine.Y1Real[9] = 1;
            //GridLine.X2Real[9] = 8;
            //GridLine.Y2Real[9] = 8;

            DROWcls callmee = new DROWcls();
            callmee.CalculateGridPointReal();
            callmee.Render2d();
            callmee.Render3d();
            callmee.Renderelev();
        }
        private void toolStripButton41_Click(object sender, EventArgs e)
        {
            Myglobals.DrawDiagram = 0;
            DROWcls callmee = new DROWcls();
            callmee.Renderelev();
        }
        private void toolStripButton39_Click(object sender, EventArgs e)
        {
            //elevation
            if (Myglobals.elevNumbers > 0)
            {
                SelectElevationForm selectElevationForm = new SelectElevationForm();
                selectElevationForm.ShowDialog();
            }
        }
        private void toolStripButton45_Click(object sender, EventArgs e)
        {
            Myglobals.Rotate3DVeiw = 1;
        }
        private void endLengthOffsetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EndLengthOffsetsForm endLengthOffsetsForm = new EndLengthOffsetsForm();
            endLengthOffsetsForm.ShowDialog();
        }
        private void localAxesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrameLocalAxesForm frameLocalAxesForm = new FrameLocalAxesForm();
            frameLocalAxesForm.ShowDialog();
        }
        private void toolStripButton30_Click(object sender, EventArgs e)
        {
            double MaxX = -1000000;
            double MaxY = -1000000;
            double MinX = 1000000;
            double MinY = 1000000;
            double X = 0;
            double Y = 0;
            if (Myglobals.SelectedPlan == "Plan")
            {
                for (int i = 1; i < Joint.Number2d + 1; i++)
                {
                    X = Joint.X2d[i];
                    Y = Joint.Y2d[i];
                    if (X > MaxX) MaxX = X;
                    if (Y > MaxY) MaxY = Y;
                    if (X < MinX) MinX = X;
                    if (Y < MinY) MinY = Y;
                }
                for (int i = 1; i < GridPoint.Number2d + 1; i++)
                {
                    X = GridPoint.X2d[i];
                    Y = GridPoint.Y2d[i];
                    if (X > MaxX) MaxX = X;
                    if (Y > MaxY) MaxY = Y;
                    if (X < MinX) MinX = X;
                    if (Y < MinY) MinY = Y;
                }
                double lengthX = (MaxX - MinX) / Myglobals.Zoom2d;
                double lengthY = (MaxY - MinY) / Myglobals.Zoom2d;
                double WIDTH = pictureBox3.Width - 150;
                double HEIGHT = pictureBox3.Height - 150;
                double ZoomX = WIDTH / lengthX;
                double ZoomY = HEIGHT / lengthY;
                Myglobals.Zoom2d = (int)ZoomX;
                if (ZoomY < ZoomX) Myglobals.Zoom2d = (int)ZoomY;
                Myglobals.startX2d = 100;
                Myglobals.startY2d = 500;
            }
            if (Myglobals.SelectedPlan == "3D")
            {
                for (int i = 1; i < Joint.Number3d + 1; i++)
                {
                    X = Joint.X3d[i];
                    Y = Joint.Y3d[i];
                    if (X > MaxX) MaxX = X;
                    if (Y > MaxY) MaxY = Y;
                    if (X < MinX) MinX = X;
                    if (Y < MinY) MinY = Y;
                }
                for (int i = 1; i < GridPoint.Number3d + 1; i++)
                {
                    X = GridPoint.X3d[i];
                    Y = GridPoint.Y3d[i];
                    if (X > MaxX) MaxX = X;
                    if (Y > MaxY) MaxY = Y;
                    if (X < MinX) MinX = X;
                    if (Y < MinY) MinY = Y;
                }
                double lengthX = (MaxX - MinX) / Myglobals.Zoom3d;
                double lengthY = (MaxY - MinY) / Myglobals.Zoom3d;
                double WIDTH = pictureBox4.Width - 100;
                double HEIGHT = pictureBox4.Height - 100;
                double ZoomX = WIDTH / lengthX;
                double ZoomY = HEIGHT / lengthY;
                Myglobals.Zoom3d = (int)ZoomX;
                if (ZoomY < ZoomX) Myglobals.Zoom3d = (int)ZoomY;
                WIDTH = WIDTH + 100;
                HEIGHT = HEIGHT + 100;
                Myglobals.startX3d = (int)(WIDTH / 2);
                Myglobals.startY3d = (int)(HEIGHT / 2);
            }
            DROWcls callmee = new DROWcls();
            callmee.Render2d();
            callmee.Render3d();
            callmee.Renderelev();
        }
        private void toolStripButton43_Click(object sender, EventArgs e)
        {
            if (Frame.ShowPower == 1)
            {
                Frame.ShowPower = 0;
            }
            else
            {
                Frame.ShowPower = 1;
            }
            DROWcls callmee = new DROWcls();
            callmee.Render2d();
            callmee.Render3d();
            callmee.Renderelev();
        }
        private void toolStripButton42_Click(object sender, EventArgs e)
        {
            if (Joint.ShowPower == 1)
            {
                Joint.ShowPower = 0;
            }
            else
            {
                Joint.ShowPower = 1;
            }
            DROWcls callmee = new DROWcls();
            callmee.Render2d();
            callmee.Render3d();
           callmee.Renderelev();
        }
        private void showSelectedObjectOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < Frame.Number + 1; i++)
            {
                FrameElement[i].Visible = 1;
            }
            for (int i = 1; i < Shell.Number + 1; i++)
            {
                Shell.Visible[i] = 1;
            }

            for (int i = 1; i < Frame.Number + 1; i++)
            {
                if (FrameElement[i].Selected == 1)
                {
                    FrameElement[i].Visible = 0;
                }
            }

            for (int i = 1; i < Shell.Number + 1; i++)
            {
                for (int j = 1; j < Shell.PointNumbers[i]; j++)
                {
                    if (Shell.SelectedLine[i, j] == 1)
                    {
                        Shell.Visible[i] = 0;
                        break;
                    }
                }
            }
            DROWcls callmee = new DROWcls();
            callmee.Render2d();
            callmee.Render3d();
            callmee.Renderelev();
        }
        private void makeSelectedObjectInvisibleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < Frame.Number + 1; i++)
            {
                if (FrameElement[i].Selected == 1)
                {
                    FrameElement[i].Visible = 1;
                }
            }

            for (int i = 1; i < Shell.Number + 1; i++)
            {
                for (int j = 1; j < Shell.PointNumbers[i]; j++)
                {
                    if (Shell.SelectedLine[i, j] == 1)
                    {
                        Shell.Visible[i] = 1;
                        break;
                    }
                }
            }
            DROWcls callmee = new DROWcls();
            callmee.Render2d();
            callmee.Render3d();
            callmee.Renderelev();
        }
        private void showAllObjectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < Frame.Number + 1; i++)
            {
                FrameElement[i].Visible = 0;
            }
            for (int i = 1; i < Shell.Number + 1; i++)
            {
                Shell.Visible[i] = 0;
            }
            DROWcls callmee = new DROWcls();
            callmee.Render2d();
            callmee.Render3d();
            callmee.Renderelev();
        }
        private void invertVisibilityOfObjectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < Frame.Number + 1; i++)
            {
                if (FrameElement[i].Visible == 1)
                {
                    FrameElement[i].Visible = 0;
                }
                else
                {
                    FrameElement[i].Visible = 1;
                }
            }
            for (int i = 1; i < Shell.Number + 1; i++)
            {
                if (Shell.Visible[i] == 1)
                {
                    Shell.Visible[i] = 0;
                }
                else
                {
                    Shell.Visible[i] = 1;
                }
            }
            DROWcls callmee = new DROWcls();
            callmee.Render2d();
            callmee.Render3d();
            callmee.Renderelev();
        }
        private void set3DViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Set3DViewForm set3DViewForm = new Set3DViewForm();
            set3DViewForm.ShowDialog();
        }
        private void toolStripButton26_Click(object sender, EventArgs e)
        {
            Myglobals.IfAnalysis = 0;
            Myglobals.DrawDiagram = 0;
            DROWcls callmee = new DROWcls();
            callmee.CalculateGridPointReal();
            callmee.Render2d();
            callmee.Render3d();
            callmee.Renderelev();
            toolStripButton3.Enabled = true;
            toolStripButton5.Enabled = true;
            toolStripButton8.Enabled = true;
            toolStripButton9.Enabled = true;
            toolStripButton10.Enabled = true;
            toolStripButton11.Enabled = true;
            toolStripButton26.Image = Properties.Resources._3;
        }
        private void toolStripButton38_Click(object sender, EventArgs e)
        {
            if (Myglobals.ShowPlaneWindow == 1) goto endloop;
            if (Myglobals.SelectedPlan == "3D")
            {
                Myglobals.ShowPlaneWindow = 1;
                Myglobals.Show3DWindow = 0;
                tabControl1.Location = new Point(tabControl2.Left, tabControl2.Top);
                tabControl2.Visible = false;
                tabControl1.Visible = true;
                goto endloop;
            }
            if (Myglobals.SelectedPlan == "ELEV")
            {
                Myglobals.ShowPlaneWindow = 1;
                Myglobals.ShowEleveWindow = 0;
                tabControl1.Location = new Point(tabControl3.Left, tabControl3.Top);
                tabControl3.Visible = false;
                tabControl1.Visible = true;
                goto endloop;
            }
        endloop: { }
        }
        private void toolStripButton37_Click(object sender, EventArgs e)
        {
            if (Myglobals.Show3DWindow == 1) goto endloop;
            if (Myglobals.SelectedPlan == "Plan")
            {
                Myglobals.Show3DWindow = 1;
                Myglobals.ShowPlaneWindow = 0;
                tabControl2.Location = new Point(tabControl1.Left, tabControl1.Top);
                tabControl1.Visible = false;
                tabControl2.Visible = true;
                goto endloop;
            }
            if (Myglobals.SelectedPlan == "ELEV")
            {
                Myglobals.Show3DWindow = 1;
                Myglobals.ShowEleveWindow = 0;
                tabControl2.Location = new Point(tabControl3.Left, tabControl3.Top);
                tabControl3.Visible = false;
                tabControl2.Visible = true;
                goto endloop;
            }
        endloop: { }

        }
        private void toolStripButton36_Click(object sender, EventArgs e)
        {
            if (Myglobals.Toggel == 1)
            {
                Myglobals.Toggel = 0;
            }
            else
            {
                Myglobals.Toggel = 1;
            }
            DROWcls callmee = new DROWcls();
            callmee.Render3d();
        }
        private void toolStripButton40_Click(object sender, EventArgs e)
        {
            SetVeiwOptionsForm setVeiwOptionsForm = new SetVeiwOptionsForm();
            setVeiwOptionsForm.ShowDialog();
        }
        private void useExtrudedViewForFrameObjectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (useExtrudedViewForFrameObjectsToolStripMenuItem.Checked == true)
            {
                Myglobals.ExtrudedFrame = 1;
            }
            else
            {
                Myglobals.ExtrudedFrame = 0;
            }
            DROWcls callmee = new DROWcls();
            callmee.Render3d();
        }
        private void useExtrudedViewForShellObjectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (useExtrudedViewForShellObjectsToolStripMenuItem.Checked == true)
            {
                Myglobals.ExtrudedShell = 1;
            }
            else
            {
                Myglobals.ExtrudedShell = 0;
            }
            DROWcls callmee = new DROWcls();
            callmee.Render3d();
        }
        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            if (Myglobals.ExtrudedFrame == 1 || Myglobals.ExtrudedShell == 1)
            {
                Myglobals.ExtrudedFrame = 0;
                Myglobals.ExtrudedShell = 0;
                useExtrudedViewForShellObjectsToolStripMenuItem.Checked = false;
                useExtrudedViewForFrameObjectsToolStripMenuItem.Checked = false;
            }
            else
            {
                Myglobals.ExtrudedFrame = 1;
                Myglobals.ExtrudedShell = 1;
                useExtrudedViewForShellObjectsToolStripMenuItem.Checked = true;
                useExtrudedViewForFrameObjectsToolStripMenuItem.Checked = true;
            }
            DROWcls callmee = new DROWcls();
            callmee.Render3d();
        }
        private void snapOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SnapOptionsForm snapOptionsForm = new SnapOptionsForm();
            snapOptionsForm.ShowDialog();
        }
        private void toolStripButton24_Click(object sender, EventArgs e)
        {
            if (Myglobals.TempSelectedFile - 1 > 0 & Myglobals.IfAnalysis == 0)
            {
                Myglobals.TempSelectedFile = Myglobals.TempSelectedFile - 1;
                OpenTemp();
                DROWcls callmee = new DROWcls();
                callmee.Render2d();
                callmee.Render3d();
                callmee.Renderelev();
            }
        }
        private void toolStripButton25_Click(object sender, EventArgs e)
        {
            if (Myglobals.TempSelectedFile + 1 <= Myglobals.TempFile & Myglobals.IfAnalysis == 0)
            {
                Myglobals.TempSelectedFile = Myglobals.TempSelectedFile + 1;
                OpenTemp();
                DROWcls callmee = new DROWcls();
                callmee.Render2d();
                callmee.Render3d();
                callmee.Renderelev();
            }
        }
        private void toolStripButton22_Click(object sender, EventArgs e)
        {
            OpenReal();
        }
        private void toolStripButton23_Click(object sender, EventArgs e)
        {
            SaveReal();
        }
        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            toolStripButton3.Checked = false;
            toolStripButton1.Checked = false;
            toolStripButton5.Checked = false;
            toolStripButton8.Checked = false;
            toolStripButton9.Checked = false;
            toolStripButton10.Checked = false;
            toolStripButton11.Checked = true;
            toolStripButton6.Checked = false;
            propertyGrid1.Visible = true;
            button14.Visible = true;
            Myglobals.LineMove2dVisible = 0;
            Myglobals.LineMove2dFVisible = 0;
            Myglobals.LineMove3dVisible = 0;
            Myglobals.LineMove3dFVisible = 0;
            Myglobals.LineMoveelevVisible = 0;
            Myglobals.LineMoveelevFVisible = 0;
        }
        private void sectionPropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrameSectionAssignForm frameSectionAssignForm = new FrameSectionAssignForm();
            frameSectionAssignForm.ShowDialog();
        }
        private void slabSectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShellSectionAssignmentForm shellSectionAssignmentForm = new ShellSectionAssignmentForm();
            shellSectionAssignmentForm.ShowDialog();
        }
        private void objectTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectByObjectForm selectByForm = new SelectByObjectForm();
            selectByForm.ShowDialog();
        }
        private void materialPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectByMaterialForm selectByMaterialForm = new SelectByMaterialForm();
            selectByMaterialForm.ShowDialog();
        }
        private void frameSectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectByFrameForm selectByFrameForm = new SelectByFrameForm();
            selectByFrameForm.ShowDialog();
        }
        private void slabSectionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SelectBySlabForm selectBySlabForm = new SelectBySlabForm();
            selectBySlabForm.ShowDialog();
        }
        private void wallSectiopnsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectByWallForm selectByWallForm = new SelectByWallForm();
            selectByWallForm.ShowDialog();
        }
        private void storiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectByStoriesForm selectByStoriesForm = new SelectByStoriesForm();
            selectByStoriesForm.ShowDialog();
        }
        private void destributedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrameLoadAssignmentForm frameLoadAssignmentForm = new FrameLoadAssignmentForm();
            frameLoadAssignmentForm.ShowDialog();
        }
        private void pointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrameLoadAssignmentPForm frameLoadAssignmentPForm = new FrameLoadAssignmentPForm();
            frameLoadAssignmentPForm.ShowDialog();
        }
        private void uniformToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShellLoadAssignmentForm shellLoadAssignmentForm = new ShellLoadAssignmentForm();
            shellLoadAssignmentForm.ShowDialog();
        }
        private void tabControl1_Enter(object sender, EventArgs e)
        {
            tabPage1.BorderStyle = BorderStyle.FixedSingle;
            tabPage2.BorderStyle = BorderStyle.None;
            Myglobals.SelectedPlan = "Plan";
        }
        private void tabControl2_Enter(object sender, EventArgs e)
        {
            tabPage2.BorderStyle = BorderStyle.FixedSingle;
            tabPage1.BorderStyle = BorderStyle.None;
            Myglobals.SelectedPlan = "3D";
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            toolStripButton1.Checked = true;
            toolStripButton3.Checked = false;
            toolStripButton5.Checked = false;
            toolStripButton8.Checked = false;
            toolStripButton9.Checked = false;
            toolStripButton10.Checked = false;
            toolStripButton11.Checked = false;
            toolStripButton6.Checked = false;
            propertyGrid1.Visible = false;
            button14.Visible = false;
            Myglobals.LineMove2dVisible = 0;
            Myglobals.LineMove2dFVisible = 0;
            Myglobals.LineMove3dVisible = 0;
            Myglobals.LineMove3dFVisible = 0;
            Myglobals.LineMoveelevVisible = 0;
            Myglobals.LineMoveelevFVisible = 0;
            Myglobals.drowclick = 0;
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (Myglobals.PropertyGridchoice != 1)
            {
                Myglobals.PropertyGridchoice = 1;
                barlist = new List<Bar>();
                barlist1 = new List<Bar1>();
                barlist2 = new List<Bar2>();
                for (int i = 1; i < Section.Number + 1; i++)
                {
                    Bar bar = new Bar();
                    bar.barvalue = Section.LABEL[i];
                    barlist.Add(bar);
                    comboBox2.Items.Add(bar);
                }
                Bar1 bar1 = new Bar1();
                bar1.barvalue = "Frame";
                barlist1.Add(bar1);
                comboBox2.Items.Add(bar1);
                Bar2 bar2 = new Bar2();
                bar2.barvalue = "Continuous";
                barlist2.Add(bar2);
                comboBox2.Items.Add(bar2);
                bar2 = new Bar2();
                bar2.barvalue = "Pinned";
                barlist2.Add(bar2);
                comboBox2.Items.Add(bar2);
                Foo foo = new Foo();
                foo.bar = new Bar();
                foo.bar1 = new Bar1();
                foo.bar2 = new Bar2();
                foo.Name = "0";
                foo.bar.barvalue = Section.LABEL[1];
                foo.bar1.barvalue = "Frame";
                foo.bar2.barvalue = "Continuous";
                propertyGrid1.SelectedObject = foo;

                Section.SelectedToDraw = 1;
            }
            toolStripButton3.Checked = true;
            toolStripButton1.Checked = false;
            toolStripButton5.Checked = false;
            toolStripButton8.Checked = false;
            toolStripButton9.Checked = false;
            toolStripButton10.Checked = false;
            toolStripButton11.Checked = false;
            toolStripButton6.Checked = false;
            propertyGrid1.Visible = true;
            button14.Visible = true;
            Myglobals.LineMove2dVisible = 0;
            Myglobals.LineMove2dFVisible = 0;
            Myglobals.LineMove3dVisible = 0;
            Myglobals.LineMove3dFVisible = 0;
            Myglobals.LineMoveelevVisible = 0;
            Myglobals.LineMoveelevFVisible = 0;
        }
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (Myglobals.PropertyGridchoice != 1)
            {
                Myglobals.PropertyGridchoice = 1;
                barlist = new List<Bar>();
                barlist1 = new List<Bar1>();
                barlist2 = new List<Bar2>();
                for (int i = 1; i < Section.Number + 1; i++)
                {
                    Bar bar = new Bar();
                    bar.barvalue = Section.LABEL[i];
                    barlist.Add(bar);
                    comboBox2.Items.Add(bar);
                }
                Bar1 bar1 = new Bar1();
                bar1.barvalue = "Frame";
                barlist1.Add(bar1);
                comboBox2.Items.Add(bar1);
                Bar2 bar2 = new Bar2();
                bar2.barvalue = "Continuous";
                barlist2.Add(bar2);
                comboBox2.Items.Add(bar2);
                bar2 = new Bar2();
                bar2.barvalue = "Pinned";
                barlist2.Add(bar2);
                comboBox2.Items.Add(bar2);
                Foo foo = new Foo();
                foo.bar = new Bar();
                foo.bar1 = new Bar1();
                foo.bar2 = new Bar2();
                foo.Name = "0";
                foo.bar.barvalue = Section.LABEL[1];
                foo.bar1.barvalue = "Frame";
                foo.bar2.barvalue = "Continuous";
                propertyGrid1.SelectedObject = foo;

                Section.SelectedToDraw = 1;
            }
            toolStripButton5.Checked = true;
            toolStripButton1.Checked = false;
            toolStripButton3.Checked = false;
            toolStripButton8.Checked = false;
            toolStripButton9.Checked = false;
            toolStripButton10.Checked = false;
            toolStripButton11.Checked = false;
            toolStripButton6.Checked = false;
            propertyGrid1.Visible = true;
            button14.Visible = true;
        }
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (Myglobals.PropertyGridchoice != 2)
            {
                Myglobals.PropertyGridchoice = 2;
                barlist = new List<Bar>();
                barlist1 = new List<Bar1>();
                barlist2 = new List<Bar2>();
                for (int i = 1; i < Slab.Number + 1; i++)
                {
                    Bar bar = new Bar();
                    bar.barvalue = Slab.Name[i];
                    barlist.Add(bar);
                    comboBox2.Items.Add(bar);
                }
                FooSlab foo = new FooSlab();
                foo.bar = new Bar();
                foo.Name = "0";
                foo.bar.barvalue = Slab.Name[1];
                propertyGrid1.SelectedObject = foo;
                Slab.SelectedToDraw = 1;
            }
            toolStripButton1.Checked = false;
            toolStripButton5.Checked = false;
            toolStripButton3.Checked = false;
            toolStripButton9.Checked = false;
            toolStripButton10.Checked = false;
            toolStripButton11.Checked = false;
            toolStripButton6.Checked = false;
            propertyGrid1.Visible = true;
            button14.Visible = true;
        }
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            if (Myglobals.PropertyGridchoice != 2)
            {
                Myglobals.PropertyGridchoice = 2;
                barlist = new List<Bar>();
                barlist1 = new List<Bar1>();
                barlist2 = new List<Bar2>();
                for (int i = 1; i < Slab.Number + 1; i++)
                {
                    Bar bar = new Bar();
                    bar.barvalue = Slab.Name[i];
                    barlist.Add(bar);
                    comboBox2.Items.Add(bar);
                }
                FooSlab foo = new FooSlab();
                foo.bar = new Bar();
                foo.Name = "0";
                foo.bar.barvalue = Slab.Name[1];
                propertyGrid1.SelectedObject = foo;

                Slab.SelectedToDraw = 1;
            }
            toolStripButton1.Checked = false;
            toolStripButton3.Checked = false;
            toolStripButton8.Checked = false;
            toolStripButton5.Checked = false;
            toolStripButton10.Checked = false;
            toolStripButton11.Checked = false;
            toolStripButton6.Checked = false;
        }
        private void toolStripButton32_Click(object sender, EventArgs e)
        {
            if (tabPage1.BorderStyle == BorderStyle.FixedSingle)
            {
                Myglobals.Zoom2d = Myglobals.Zoom2d + 2;
                DROWcls callmee = new DROWcls();
                callmee.Render2d();
            }
            if (tabPage2.BorderStyle == BorderStyle.FixedSingle)
            {
                Myglobals.Zoom3d = Myglobals.Zoom3d + 2;
                DROWcls callmee = new DROWcls();
                callmee.Render3d();
            }
            if (tabPage3.BorderStyle == BorderStyle.FixedSingle)
            {
                Myglobals.Zoomelev= Myglobals.Zoomelev + 2;
                DROWcls callmee = new DROWcls();
                callmee.Renderelev();
            }
        }
        private void toolStripButton33_Click(object sender, EventArgs e)
        {
            if (tabPage1.BorderStyle == BorderStyle.FixedSingle)
            {
                if (Myglobals.Zoom2d - 2 > 0)
                {
                    Myglobals.Zoom2d = Myglobals.Zoom2d - 2;
                    DROWcls callmee = new DROWcls();
                    callmee.Render2d();
                }
            }
            if (tabPage2.BorderStyle == BorderStyle.FixedSingle)
            {
                if (Myglobals.Zoom3d - 2 > 0)
                {
                    Myglobals.Zoom3d = Myglobals.Zoom3d - 2;
                    DROWcls callmee = new DROWcls();
                    callmee.Render3d();
                }
            }
            if (tabPage3.BorderStyle == BorderStyle.FixedSingle)
            {
                if (Myglobals.Zoomelev - 2 > 0)
                {
                    Myglobals.Zoomelev = Myglobals.Zoomelev - 2;
                    DROWcls callmee = new DROWcls();
                    callmee.Renderelev();
                }
            }
        }
        private void toolStripButton34_Click(object sender, EventArgs e)
        {
            if  (Myglobals.SelectedPlan == "Plan")
            {
                if (Myglobals.SelectedStory < Myglobals.StoryNumbers)
                {
                    Myglobals.SelectedStory = Myglobals.SelectedStory + 1;
                    tabPage1.Text = "Plan View" + "  " + "Story" + Myglobals.SelectedStory + " -Z=  " + Myglobals.StoryLevel[Myglobals.SelectedStory] + " m";
                    DROWcls callmee = new DROWcls();
                    callmee.Render2d();
                    callmee.Render3d();
                    callmee.Renderelev();
                }
            }
            if (Myglobals.SelectedPlan == "ELEV")
            {
                if (Myglobals.Selectedelev < Myglobals.elevNumbers)
                {
                    Myglobals.Selectedelev = Myglobals.Selectedelev + 1;
                    DROWcls callmee = new DROWcls();
                    callmee.Renderelev();
                   // callmee.FindAriasELEV();
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
                       tabPage3.Text = "Elevation View - " + GridLine.Name[Myglobals.Selectedelev] + ghg;
                    }
                    else
                    {
                        tabPage3.Text = "Elevation View - " + GridLine.Name[Myglobals.elevGridLine[Myglobals.Selectedelev]];
                    }
                }
            }
        }
        private void toolStripButton35_Click(object sender, EventArgs e)
        {
            if (Myglobals.SelectedPlan == "Plan")
            {
                if (Myglobals.SelectedStory > 0)
                {
                    Myglobals.SelectedStory = Myglobals.SelectedStory - 1;
                    if (Myglobals.SelectedStory != 0)
                    {
                        tabPage1.Text = "Plan View" + "  " + "Story" + Myglobals.SelectedStory + " -Z=  " + Myglobals.StoryLevel[Myglobals.SelectedStory] + " m";
                    }
                    else
                    {
                        tabPage1.Text = "Plan View" + "  " + "Base" + " -Z= 0  m";
                    }
                    DROWcls callmee = new DROWcls();
                    callmee.Render2d();
                    callmee.Render3d();
                    callmee.Renderelev();
                }
            }
            if (Myglobals.SelectedPlan == "ELEV")
            {
                if (Myglobals.Selectedelev > 1)
                {
                    Myglobals.Selectedelev = Myglobals.Selectedelev - 1;
                    DROWcls callmee = new DROWcls();
                    callmee.Renderelev();
                   // callmee.FindAriasELEV();
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
                        tabPage3.Text = "Elevation View - " + GridLine.Name[Myglobals.Selectedelev] + ghg;
                    }
                    else
                    {
                        tabPage3.Text = "Elevation View - " + GridLine.Name[Myglobals.elevGridLine[Myglobals.Selectedelev]];
                    }
                }
            }
        }
        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            UnSellectAll();
            DROWcls callmee = new DROWcls();
            callmee.Render2d();
            callmee.Render3d();
            callmee.Renderelev();
        }
        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < Frame.Number + 1; i++)
            {
                FrameElement[i].Selected = 1;
            }
            for (int i = 1; i < Shell.Number + 1; i++)
            {
                for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                {
                    Shell.SelectedLine[i, j] = 1;
                }
            }
            DROWcls callmee = new DROWcls();
            callmee.Render2d();
            callmee.Render3d();
            callmee.Renderelev();
        }
        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            if (Myglobals.PropertyGridchoice != 2)
            {
                Myglobals.PropertyGridchoice = 2;
                barlist = new List<Bar>();
                barlist1 = new List<Bar1>();
                barlist2 = new List<Bar2>();
                for (int i = 1; i < Slab.Number + 1; i++)
                {
                    Bar bar = new Bar();
                    bar.barvalue = Slab.Name[i];
                    barlist.Add(bar);
                    comboBox2.Items.Add(bar);
                }
                FooSlab foo = new FooSlab();
                foo.bar = new Bar();
                foo.Name = "0";
                foo.bar.barvalue = Slab.Name[1];
                propertyGrid1.SelectedObject = foo;

                Slab.SelectedToDraw = 1;
            }
            toolStripButton10.Checked = true;
            toolStripButton1.Checked = false;
            toolStripButton3.Checked = false;
            toolStripButton5.Checked = false;
            toolStripButton8.Checked = false;
            toolStripButton9.Checked = false;
            toolStripButton11.Checked = false;
            toolStripButton6.Checked = false;
            Myglobals.LineMove2dVisible = 0;
            Myglobals.LineMove2dFVisible = 0;
            Myglobals.LineMove3dVisible = 0;
            Myglobals.LineMove3dFVisible = 0;
            Myglobals.LineMoveelevVisible = 0;
            Myglobals.LineMoveelevFVisible = 0;
        }
        private void extrudeFrameToSheelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool found = false;
            try
            {
                for (int i = 0; i < Application.OpenForms.Count; i++)
                {
                    Form n = Application.OpenForms[i];
                    if (n.Name == "ExtrudeForm")
                    {
                        n.BringToFront();
                        found = true;
                        Myglobals.ExtrudeType = "Frame";
                    }
                }
                if (!found)
                {
                    ExtrudeForm extrudeForm = new ExtrudeForm();
                    extrudeForm.Name = "ExtrudeForm";
                    extrudeForm.TopMost = true;
                    Myglobals.ExtrudeType = "Frame";
                    extrudeForm.Show();
                }
            }
            catch
            {
            }
        }
        private void extrudeJointsToFramesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool found = false;
            try
            {
                for (int i = 0; i < Application.OpenForms.Count; i++)
                {
                    Form n = Application.OpenForms[i];
                    if (n.Name == "ExtrudeForm")
                    {
                        n.BringToFront();
                        found = true;
                        Myglobals.ExtrudeType = "Joint";
                    }
                }
                if (!found)
                {
                    ExtrudeForm extrudeForm = new ExtrudeForm();
                    extrudeForm.Name = "ExtrudeForm";
                    extrudeForm.TopMost = true;
                    Myglobals.ExtrudeType = "Joint";
                    extrudeForm.Show();
                }
            }
            catch
            {
            }
        }
        private void setGridSystemVisibilityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Myglobals.ShowGrid == 1)
            {
                Myglobals.ShowGrid = 0;
            }
            else
            {
                Myglobals.ShowGrid = 1;
            }
            DROWcls callmee = new DROWcls();
            callmee.Render2d();
            callmee.Render3d();
            callmee.Renderelev();
        }
        private void replicateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool found = false;
            try
            {
                for (int i = 0; i < Application.OpenForms.Count; i++)
                {
                    Form n = Application.OpenForms[i];
                    if (n.Name == "ReplicateForm")
                    {
                        n.BringToFront();
                        found = true;
                    }
                }
                if (!found)
                {
                    ReplicateForm replicateForm = new ReplicateForm();
                    replicateForm.Name = "ReplicateForm";
                    replicateForm.TopMost = true;
                    replicateForm.Show();
                }
            }
            catch
            {
            }
        }
        #endregion
        #region//Voids
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
        private void DistanceCalc(int X, int Y, int X1, int Y1, int X2, int Y2)
        {
            double distance = 0;
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
                x0 = Math.Round(x0, 3);
                y0 = Math.Round(y0, 3);
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
                x0 = Math.Round(x0, 3);
                y0 = Math.Round(y0, 3);
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
                x0 = Math.Round(x0, 3);
                y0 = Math.Round(y0, 3);
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
        private void checkinPointOnLine(double X1, double Y1, double Z1, double X2, double Y2, double Z2, double X, double Y, double Z)
        {
            int THETAHKIK = 0;
            double VAL1 = 0;
            double VAL2 = 0;
            double VAL3 = 0;
            double tollerance = 0.003;
            double VAL = 0;
            int chkX = 0;
            int chkY = 0;
            int chkZ = 0;
            int chk = 0;
            if ((X >= X1) & (X <= X2)) chkX = 1;
            if ((X <= X1) & (X >= X2)) chkX = 1;
            if ((Y >= Y1) & (Y <= Y2)) chkY = 1;
            if ((Y <= Y1) & (Y >= Y2)) chkY = 1;
            if ((Z >= Z1) & (Z <= Z2)) chkZ = 1;
            if ((Z <= Z1) & (Z >= Z2)) chkZ = 1;
            if (chkX == 1 & chkY == 1 & chkZ == 1) chk = 1;
            if ((X == X1) & (Y == Y1) & (Z == Z1)) chk = 0;
            if ((X == X2) & (Y == Y2) & (Z == Z2)) chk = 0;
            if (chk == 1)
            {
                if (X2 != X1)
                {
                    VAL1 = (X - X1) / (X2 - X1);
                    VAL = VAL1;
                }
                if (Y2 != Y1)
                {
                    VAL2 = (Y - Y1) / (Y2 - Y1);
                    VAL = VAL2;
                }
                if (Z2 != Z1)
                {
                    VAL3 = (Z - Z1) / (Z2 - Z1);
                    VAL = VAL3;
                }
                if (X2 == X1 & X == X1) VAL1 = VAL;
                if (Y2 == Y1 & Y == Y1) VAL2 = VAL;
                if (Z2 == Z1 & Z == Z1) VAL3 = VAL;
                VAL1 = Math.Round(VAL1, 3);
                VAL2 = Math.Round(VAL2, 3);
                VAL3 = Math.Round(VAL3, 3);
                if (Math.Abs(VAL1 - VAL2) <= tollerance & Math.Abs(VAL2 - VAL3) <= tollerance)
                {
                    THETAHKIK = 1;
                }
            }
            INTERSECTION = THETAHKIK;
        }
       

        private void UnSellectAll()
        {
            for (int i = 1; i < Frame.Number + 1; i++)
            {
                FrameElement[i].Selected = 0;
            }
            for (int i = 1; i < Shell.Number + 1; i++)
            {
                for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                {
                    Shell.SelectedLine[i, j] = 0;
                }
            }
            for (int add = 1; add < Joint.Number3d + 1; add++)
            {
                Joint.Selected[add] = 0;
            }
        }
        private void PreviousSellect()
        {
            for (int i = 1; i < Frame.Number + 1; i++)
            {
                FrameElement[i].SelectedPriv = FrameElement[i].Selected;
            }
            for (int i = 1; i < Shell.Number + 1; i++)
            {
                for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                {
                    Shell.SelectedLinePriv[i, j] = Shell.SelectedLine[i, j];
                }
            }
            for (int add = 1; add < Joint.Number3d + 1; add++)
            {
                Joint.SelectedPriv[add] = Joint.Selected[add];
            }
        }
        private void LoadSections()
        {
            try
            {
                string strpath = Convert.ToString(Directory.GetParent(@"./Property Libraries/MySections/"));
                StreamReader SW = new StreamReader(strpath);
                Section.Number = Convert.ToInt32(SW.ReadLine());
                for (int add = 1; add < Section.Number + 1; add++)
                {
                    (Section.DESCRIPTION[add]) = Convert.ToString(SW.ReadLine());
                    (Section.LENGTH_UNITS[add]) = Convert.ToString(SW.ReadLine());
                    (Section.MODEL[add]) = Convert.ToString(SW.ReadLine());
                    (Section.LABEL[add]) = Convert.ToString(SW.ReadLine());
                    (Section.EDI_STD[add]) = Convert.ToString(SW.ReadLine());
                    (Section.DESIGNATION[add]) = Convert.ToString(SW.ReadLine());
                    (Section.D[add]) = Convert.ToDouble(SW.ReadLine());
                    (Section.B[add]) = Convert.ToDouble(SW.ReadLine());
                    (Section.BF[add]) = Convert.ToDouble(SW.ReadLine());
                    (Section.TF[add]) = Convert.ToDouble(SW.ReadLine());
                    (Section.BFB[add]) = Convert.ToDouble(SW.ReadLine());
                    (Section.TFB[add]) = Convert.ToDouble(SW.ReadLine());
                    (Section.TW[add]) = Convert.ToDouble(SW.ReadLine());
                    (Section.A[add]) = Convert.ToDouble(SW.ReadLine());
                    (Section.I33[add]) = Convert.ToDouble(SW.ReadLine());
                    (Section.Z33[add]) = Convert.ToDouble(SW.ReadLine());
                    (Section.AS3[add]) = Convert.ToDouble(SW.ReadLine());
                    (Section.I22[add]) = Convert.ToDouble(SW.ReadLine());
                    (Section.Z22[add]) = Convert.ToDouble(SW.ReadLine());
                    (Section.AS2[add]) = Convert.ToDouble(SW.ReadLine());
                    (Section.J[add]) = Convert.ToDouble(SW.ReadLine());
                    (Section.X[add]) = Convert.ToDouble(SW.ReadLine());
                    (Section.Y[add]) = Convert.ToDouble(SW.ReadLine());
                    (Section.S33POS[add]) = Convert.ToDouble(SW.ReadLine());
                    (Section.S33NEG[add]) = Convert.ToDouble(SW.ReadLine());
                    (Section.S22POS[add]) = Convert.ToDouble(SW.ReadLine());
                    (Section.S22NEG[add]) = Convert.ToDouble(SW.ReadLine());
                    (Section.R33[add]) = Convert.ToDouble(SW.ReadLine());
                    (Section.R22[add]) = Convert.ToDouble(SW.ReadLine());
                    (Section.HT[add]) = Convert.ToDouble(SW.ReadLine());
                    (Section.OD[add]) = Convert.ToDouble(SW.ReadLine());
                    (Section.TDES[add]) = Convert.ToDouble(SW.ReadLine());
                    (Section.Material[add]) = Convert.ToInt32(SW.ReadLine());

                    (Section.ModifiersArea[add]) = Convert.ToDouble(SW.ReadLine());
                    (Section.ModifiersShear2[add]) = Convert.ToDouble(SW.ReadLine());
                    (Section.ModifiersShear3[add]) = Convert.ToDouble(SW.ReadLine());
                    (Section.ModifiersTorsional[add]) = Convert.ToDouble(SW.ReadLine());
                    (Section.ModifiersI2[add]) = Convert.ToDouble(SW.ReadLine());
                    (Section.ModifiersI3[add]) = Convert.ToDouble(SW.ReadLine());
                    (Section.ModifiersMass[add]) = Convert.ToDouble(SW.ReadLine());
                    (Section.ModifiersWeight[add]) = Convert.ToDouble(SW.ReadLine());

                   
                    Section.DesignType[add] = Convert.ToInt32(SW.ReadLine());
                    Section.RebarMaterial1[add] = Convert.ToInt32(SW.ReadLine());
                    Section.RebarMaterial2[add] = Convert.ToInt32(SW.ReadLine());
                    Section.CoverTop[add] = Convert.ToDouble(SW.ReadLine());
                    Section.CoverBottom[add] = Convert.ToDouble(SW.ReadLine());
                    Section.ReinTopI[add] = Convert.ToDouble(SW.ReadLine());
                    Section.ReinTopJ[add] = Convert.ToDouble(SW.ReadLine());
                    Section.ReinBottomI[add] = Convert.ToDouble(SW.ReadLine());
                    Section.ReinBottomJ[add] = Convert.ToDouble(SW.ReadLine());
                    



                }
                SW.Close();
            }
            catch
            {
                MessageBox.Show("ERROR! Please check passphrase and do not attempt to edit cipher text");
            }
        }
        private void LoadMaterials()
        {
            try
            {
                string strpath = Convert.ToString(Directory.GetParent(@"./Property Libraries/MyMaterials/"));
                StreamReader SW = new StreamReader(strpath);
                Material.Number = Convert.ToInt32(SW.ReadLine());
                for (int add = 1; add < Material.Number + 1; add++)
                {
                    (Material.Name[add]) = SW.ReadLine();
                    (Material.MType[add]) = Convert.ToInt32(SW.ReadLine());
                    (Material.Summetry[add]) = Convert.ToInt32(SW.ReadLine());
                    (Material.WperV[add]) = Convert.ToDouble(SW.ReadLine());
                    (Material.MperV[add]) = Convert.ToDouble(SW.ReadLine());
                    (Material.Elastisity[add]) = Convert.ToDouble(SW.ReadLine());
                    (Material.Poisson[add]) = Convert.ToDouble(SW.ReadLine());
                    (Material.Thermal[add]) = Convert.ToDouble(SW.ReadLine());
                    (Material.ShearM[add]) = Convert.ToDouble(SW.ReadLine());
                    (Material.fc[add]) = Convert.ToDouble(SW.ReadLine());
                    (Material.LweightCon[add]) = Convert.ToInt32(SW.ReadLine());
                    (Material.MinYeildFy[add]) = Convert.ToDouble(SW.ReadLine());
                    (Material.MinTensileFu[add]) = Convert.ToDouble(SW.ReadLine());
                    (Material.EffYeildFye[add]) = Convert.ToDouble(SW.ReadLine());
                    (Material.EffTensileFue[add]) = Convert.ToDouble(SW.ReadLine());
                }
                SW.Close();
            }
            catch
            {
                MessageBox.Show("ERROR! Please check passphrase and do not attempt to edit cipher text");
            }
        }
        private void LoadSlabs()
        {
            try
            {
                string strpath = Convert.ToString(Directory.GetParent(@"./Property Libraries/MySlabs/"));
                StreamReader SW = new StreamReader(strpath);
                Slab.Number = Convert.ToInt32(SW.ReadLine());
                for (int add = 1; add < Slab.Number + 1; add++)
                {
                    Slab.Name[add] = SW.ReadLine();
                    Slab.Material[add] = Convert.ToInt32(SW.ReadLine());
                    Slab.MType[add] = Convert.ToInt32(SW.ReadLine());
                    Slab.OneWay[add] = Convert.ToInt32(SW.ReadLine());
                    Slab.ProType[add] = Convert.ToInt32(SW.ReadLine());
                    Slab.Thickness[add] = Convert.ToDouble(SW.ReadLine());
                    Slab.OverallDepth[add] = Convert.ToDouble(SW.ReadLine());
                    Slab.SlabThickness[add] = Convert.ToDouble(SW.ReadLine());
                    Slab.StemWidthatTop[add] = Convert.ToDouble(SW.ReadLine());
                    Slab.StemWidthatBottom[add] = Convert.ToDouble(SW.ReadLine());
                    Slab.RibSpacing[add] = Convert.ToDouble(SW.ReadLine());
                    Slab.RibDirection[add] = Convert.ToInt32(SW.ReadLine());
                    Slab.RibSpacing1[add] = Convert.ToDouble(SW.ReadLine());
                    Slab.RibSpacing2[add] = Convert.ToDouble(SW.ReadLine());
                }
                SW.Close();
            }
            catch
            {
                MessageBox.Show("ERROR! Please check passphrase and do not attempt to edit cipher text");
            }
        }
        private void LoadWalls()
        {
            try
            {
                string strpath = Convert.ToString(Directory.GetParent(@"./Property Libraries/MyWalls/"));
                StreamReader SW = new StreamReader(strpath);
                Wall.Number = Convert.ToInt32(SW.ReadLine());
                for (int add = 1; add < Wall.Number + 1; add++)
                {
                    Wall.Name[add] = SW.ReadLine();
                    Wall.Material[add] = Convert.ToInt32(SW.ReadLine());
                    Wall.MType[add] = Convert.ToInt32(SW.ReadLine());
                    Wall.ProType[add] = Convert.ToInt32(SW.ReadLine());
                    Wall.Thickness[add] = Convert.ToDouble(SW.ReadLine());
                }
                SW.Close();
            }
            catch
            {
                MessageBox.Show("ERROR! Please check passphrase and do not attempt to edit cipher text");
            }
        }
        #endregion
        #region///propertyGrid1
        public static List<Bar> barlist;
        public static List<Bar1> barlist1;
        public static List<Bar2> barlist2;
        public class Foo
        {
            Bar mybar;
            Bar1 mybar1;
            Bar2 mybar2;

            [Category("Properties of Objects")]
            [DisplayName("Type of Line")]
            [TypeConverter(typeof(BarConverter1))]
            public Bar1 bar1
            {
                get { return mybar1; }
                set { mybar1 = value; }
            }
            [Category("Properties of Objects")]
            [DisplayName("Property")]
            [TypeConverter(typeof(BarConverter))]
            public Bar bar
            {
                get { return mybar; }
                set { mybar = value; }
            }
            [Category("Properties of Objects")]
            [DisplayName("Moment Releases")]
            [TypeConverter(typeof(BarConverter2))]
            public Bar2 bar2
            {
                get { return mybar2; }
                set { mybar2 = value; }
            }

            private string _name;
            [Category("Properties of Objects")]
            [DisplayName("Local Axis2 angele")]
            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

        }
        public class FooSlab
        {
            Bar mybar;
            [Category("Properties of Objects")]
            [DisplayName("Property")]
            [TypeConverter(typeof(BarConverter))]
            public Bar bar
            {
                get { return mybar; }
                set { mybar = value; }
            }
            private string _name;
            [Category("Properties of Objects")]
            [DisplayName("Local Axis")]
            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }
        }
        public class Bar
        {
            public String barvalue;
            public override String ToString()
            {
                return barvalue;
            }
        }
        public class Bar1
        {
            public String barvalue;
            public override String ToString()
            {
                return barvalue;
            }
        }
        public class Bar2
        {
            public String barvalue;
            public override String ToString()
            {
                return barvalue;
            }
        }
        class BarConverter : TypeConverter
        {
            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
            public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                return new StandardValuesCollection(barlist);
            }
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string))
                {
                    return true;
                }
                return base.CanConvertFrom(context, sourceType);
            }
            public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
            {
                if (value is string)
                {
                    foreach (Bar b in barlist)
                    {
                        if (b.barvalue == (string)value)
                        {
                            return b;
                        }
                    }
                }
                return base.ConvertFrom(context, culture, value);
            }
        }
        class BarConverter1 : TypeConverter
        {
            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
            public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                return new StandardValuesCollection(barlist1);
            }
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string))
                {
                    return true;
                }
                return base.CanConvertFrom(context, sourceType);
            }
            public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
            {
                if (value is string)
                {
                    foreach (Bar1 b in barlist1)
                    {
                        if (b.barvalue == (string)value)
                        {
                            return b;
                        }
                    }
                }
                return base.ConvertFrom(context, culture, value);
            }
        }
        class BarConverter2 : TypeConverter
        {
            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
            public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                return new StandardValuesCollection(barlist2);
            }
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string))
                {
                    return true;
                }
                return base.CanConvertFrom(context, sourceType);
            }
            public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
            {
                if (value is string)
                {
                    foreach (Bar2 b in barlist2)
                    {
                        if (b.barvalue == (string)value)
                        {
                            return b;
                        }
                    }
                }
                return base.ConvertFrom(context, culture, value);
            }
        }
        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            GridItem gi = propertyGrid1.SelectedGridItem;
            while (gi.Parent != null)
            {
                gi = gi.Parent;
            }
            if (Myglobals.PropertyGridchoice == 1)
            {
                Section.SelectedToDraw = 1;
                Myglobals.RotateAngelDraw = Convert.ToDouble(gi.GridItems[3].Value.ToString());
                string thename = gi.GridItems[1].Value.ToString();
                for (int i = 1; i < Section.Number + 1; i++)
                {
                    if (Section.LABEL[i] == thename)
                    {
                        Section.SelectedToDraw = i;
                        break;
                    }
                }
            }
            if (Myglobals.PropertyGridchoice == 2)
            {
                Section.SelectedToDraw = 1;
                Myglobals.RotateAngelDraw = Convert.ToDouble(gi.GridItems[1].Value.ToString());
                string thename = gi.GridItems[0].Value.ToString();
                for (int i = 1; i < Slab.Number + 1; i++)
                {
                    if (Slab.Name[i] == thename)
                    {
                        Slab.SelectedToDraw = i;
                        break;
                    }
                }
            }
        }
        #endregion
        #region //buttons
        private void timer1_Tick(object sender, EventArgs e)
        {
            timetodo = timetodo + 1;
        }
        private void button14_Click(object sender, EventArgs e)
        {
            propertyGrid1.Visible = false;
            button14.Visible = false;
        }
        private void button13_Click(object sender, EventArgs e)
        {
            LineFrm lineFrm = new LineFrm();
            lineFrm.ShowDialog();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Myglobals.AllStories = comboBox1.SelectedIndex;
        }
        #endregion
        #region //MenuItem
        private void propertyModifiersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Myglobals.ModifyShow_PropertyIsOpen = 0;
            PropertyModifiersForm propertyModifiersForm = new PropertyModifiersForm();
            propertyModifiersForm.ShowDialog();
        }

        private void releasesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FramReleasesForm framReleasesForm = new FramReleasesForm();
            framReleasesForm.ShowDialog();
        }

        private void framSectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FramePropertiesFrm framePropertiesFrm = new FramePropertiesFrm();
            framePropertiesFrm.ShowDialog();
        }
        private void restraintsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JointRestraintsForm jointRestraintsForm = new JointRestraintsForm();
            jointRestraintsForm.ShowDialog();
        }
        private void forceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JointLoadAssignmentForm jointLoadAssignmentForm = new JointLoadAssignmentForm();
            jointLoadAssignmentForm.ShowDialog();
        }
        private void loadPatternsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DefineLoadPatternsForm defineLoadPatternsForm = new DefineLoadPatternsForm();
            defineLoadPatternsForm.ShowDialog();
        }
        private void gridSystemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GRIDfrm gridfrm = new GRIDfrm();
            gridfrm.ShowDialog();
        }
        private void addStoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StoryFrm floorFrm = new StoryFrm();
            floorFrm.ShowDialog();
        }
        private void loadCombinationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadCombinationForm loadCombinationForm = new LoadCombinationForm();
            loadCombinationForm.ShowDialog();
        }
        private void materialProperityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DefineMaterialsForm defineMaterialsForm = new DefineMaterialsForm();
            defineMaterialsForm.ShowDialog();
        }
        private void slabSectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SlabProperteisForm slabProperteisForm = new SlabProperteisForm();
            slabProperteisForm.ShowDialog();
        }
        private void iFCFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IFCclass callmee = new IFCclass();
            callmee.ImportIFC();
            DROWcls callmee1 = new DROWcls();
            callmee1.CalculateGridPointReal();
            callmee1.Render2d();
            callmee1.Render3d();
            tabPage1.Text = "Plan View" + "  " + "Story" + Myglobals.SelectedStory + " -Z=  " + Myglobals.StoryLevel[Myglobals.SelectedStory] + " m";
        }
        private void wallSectionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WallPropertiesForm wallPropertiesForm = new WallPropertiesForm();
            wallPropertiesForm.ShowDialog();
        }
        #endregion

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                for (int i = 0; i < Application.OpenForms.Count; i++)
                {
                    Form n = Application.OpenForms[i];
                    if (n.Name == "ReplicateForm")
                    {
                        n.Visible = false;
                    }
                }
            }
            if (WindowState == FormWindowState.Maximized)
            {
                for (int i = 0; i < Application.OpenForms.Count; i++)
                {
                    Form n = Application.OpenForms[i];
                    if (n.Name == "ReplicateForm")
                    {
                        n.Visible = true;
                    }
                }
            }
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Myglobals.DrowRealShape = comboBox3.SelectedIndex;
                DROWcls callmee = new DROWcls();
                callmee.Render3d();
            }
            catch
            { }
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {

        }
    }
}
