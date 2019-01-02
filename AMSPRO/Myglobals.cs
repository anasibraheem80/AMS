using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace AMSPRO
{
    public class FrameElements
    {

        public int AnalisesSecNumbers;//
        public int Localaxis;//
        public int Assignments;//
        public int ShowPower;//
        public int ShowPowerValue;//
        public int SelectedforProp;//
        public int ID;//
        public double[] ResultValue1 = new double[40];
        public double[] ResultValue2 = new double[40];
        public double[] ResultValue3 = new double[40];
        public double[] ResultValue4 = new double[40];
        public double[] ResultValue5 = new double[40];
        public double[] ResultValue6 = new double[40];
        public double[] ResultValue7 = new double[40];
        public double[] ResultValue8 = new double[40];
        public double[] AxisX1 = new double[4];
        public double[] AxisY1 = new double[4];
        public double[] AxisZ1 = new double[4];
        public double[] AxisX2 = new double[4];
        public double[] AxisY2 = new double[4];
        public double[] AxisZ2 = new double[4];

        public int[] AxisX13d = new int[4];
        public int[] AxisY13d = new int[4];
        public int[] AxisX23d = new int[4];
        public int[] AxisY23d = new int[4];

        public int FirstJoint;
        public int SecondJoint;
        public int Selected;
        public int SelectedPriv;
        public int Section;
        public double RotateAngel;
        public int Visible;
        public int LoadDNumber;
        public double[] LoadDValue1 = new double[100];
        public double[] LoadDValue2 = new double[100];
        public double[] LoadDValue3 = new double[100];
        public double[] LoadDValue4 = new double[100];
        public double[] LoadDDistance1 = new double[100];
        public double[] LoadDDistance2 = new double[100];
        public double[] LoadDDistance3 = new double[100];
        public double[] LoadDDistance4 = new double[100];
        public int[] LoadDType = new int[100];
        public double[] LoadDUniform = new double[100];
        public int[] LoadDDirection = new int[100];
        public int[] LoadDPattern = new int[100];

        public int LoadPNumber;
        public double[] LoadPValue = new double[100];
        public double[] LoadPDistance = new double[100];
        public int[] LoadPType = new int[100];
        public int[] LoadPDirection = new int[100];
        public int[] LoadPPattern = new int[100];

        public double ModifiersArea;
        public double ModifiersShear2;
        public double ModifiersShear3;
        public double ModifiersTorsional;
        public double ModifiersI2;
        public double ModifiersI3;
        public double ModifiersMass;
        public double ModifiersWeight;

        public double ReleaseAxialI;
        public double ReleaseShear2I;
        public double ReleaseShear3I;
        public double ReleaseTorsionI;
        public double ReleaseMoment22I;
        public double ReleaseMoment33I;
        public double ReleaseAxialJ;
        public double ReleaseShear2J;
        public double ReleaseShear3J;
        public double ReleaseTorsionJ;
        public double ReleaseMoment22J;
        public double ReleaseMoment33J;
        public FrameElements()
        {
        }
    }
    class Frame
    {
        public static int Number;
        public static int[] ID = new int[10000];
        public static int AnalisesSecNumbers;
        public static int Localaxis;
        public static int Assignments;
        public static int ShowPower;
        public static int ShowPowerValue;
        public static int SelectedforProp;
        public static double[,] ResultValue1 = new double[10000, 40];
        public static double[,] ResultValue2 = new double[10000, 40];
        public static double[,] ResultValue3 = new double[10000, 40];
        public static double[,] ResultValue4 = new double[10000, 40];
        public static double[,] ResultValue5 = new double[10000, 40];
        public static double[,] ResultValue6 = new double[10000, 40];
        public static double[,] ResultValue7 = new double[10000, 40];
        public static double[,] ResultValue8 = new double[10000, 40];
    }
    
    class IntersectionPoint
    {
        public static int Number3d;
        public static int[] X3d = new int[10000];
        public static int[] Y3d = new int[10000];
        public static string[] Name3d = new string[10000];

        public static int Number2d;
        public static int[] X2d = new int[10000];
        public static int[] Y2d = new int[10000];
        public static string[] Name2d = new string[10000];

        public static double[] XReal = new double[10000];
        public static double[] YReal = new double[10000];
        public static double[] ZReal = new double[10000];
    }
    class Snap
    {
        public static int Joints;
        public static int LineEndsandMidpoints;
        public static int GridIntersections;
        public static int LinesandFrames;
        public static int Edges;
        public static int PerpendicularProjections;
        public static int Intersections;
        public static int FineGrid;
        public static int Extensions;
        public static int Prallels;
        public static int IntelligentSnap;
        public static int ArchLayer;
        public static double FineGridValue;
    }
    class Loads
    {
        public static int Number;
        public static string[] Load= new string[100];
        public static string[] Type= new string[100];
        public static double[] SelfWeight = new double[100];
        public static string[] AutoLateral= new string[100];
    }
    class LoadsCombination
    {
        public static int Number;
        public static int Selected;
        public static string[] Name = new string[100];
        public static int[] NumberRow = new int[100];
        public static string[] Type = new string[100];
        public static string[,] LoadName = new string[100, 100];
        public static double[,] ScaleFactor = new double[100, 100];

        public static int Numbertemp;
        public static string[] Nametemp = new string[100];
        public static int[] NumberRowtemp = new int[100];
        public static string[] Typetemp = new string[100];
        public static string[,] LoadNametemp = new string[100, 100];
        public static double[,] ScaleFactortemp = new double[100, 100];
    }
    class Material
    {
        public static int Number;
        public static string[] Name = new string[100];
        public static int Numberd;
        public static string[] Named = new string[100];
        public static int Selected;

        public static int[] MType = new int[100];//MaterialType
        public static int[] Summetry = new int[100];//DirectionalSummetryType
        public static double[] WperV = new double[100];//WeightperUnitVolume
        public static double[] MperV = new double[100];//MassperUnitVolume
        public static double[] Elastisity = new double[100];//ModulusofElastisity
        public static double[] Poisson = new double[100];//PoissonRatio
        public static double[] Thermal = new double[100];//ThermalExpantion
        public static double[] ShearM = new double[100];//ShearModulus
        public static double[] fc = new double[100];//SpecifiedConcreteCompressiveStrenghtfc
        public static int[] LweightCon = new int[100];//LightweightConcrete
        public static double[] MinYeildFy = new double[100];//MinimumYeildStessFy
        public static double[] MinTensileFu = new double[100];//MinimumTensileStrengthFu
        public static double[] EffYeildFye = new double[100];//EffectiveYeildStessFye
        public static double[] EffTensileFue = new double[100];//EffectiveTensileStrengthFue

        public static int[] MTyped = new int[100];//MaterialType
        public static int[] Summetryd = new int[100];//DirectionalSummetryType
        public static double[] WperVd = new double[100];//WeightperUnitVolume
        public static double[] MperVd = new double[100];//MassperUnitVolume
        public static double[] Elastisityd = new double[100];//ModulusofElastisity
        public static double[] Poissond = new double[100];//PoissonRatio
        public static double[] Thermald = new double[100];//ThermalExpantion
        public static double[] ShearMd = new double[100];//ShearModulus
        public static double[] fcd = new double[100];//SpecifiedConcreteCompressiveStrenghtfc
        public static int[] LweightCond = new int[100];//LightweightConcrete
        public static double[] MinYeildFyd = new double[100];//MinimumYeildStessFy
        public static double[] MinTensileFud = new double[100];//MinimumTensileStrengthFu
        public static double[] EffYeildFyed = new double[100];//EffectiveYeildStessFye
        public static double[] EffTensileFued = new double[100];//EffectiveTensileStrengthFue
    }
    class Slab
    {
        public static int SelectedToDraw;
        public static int Number;
        public static string[] Name = new string[1000];
        public static int[] Material = new int[1000];
        public static int[] MType = new int[1000];
        public static int[] OneWay = new int[1000];
        public static int[] ProType = new int[1000];
        public static double[] Thickness = new double[1000];
        public static double[] OverallDepth = new double[1000];
        public static double[] SlabThickness = new double[1000];
        public static double[] StemWidthatTop = new double[1000];
        public static double[] StemWidthatBottom = new double[1000];
        public static double[] RibSpacing = new double[1000];
        public static int[] RibDirection= new int[1000];
        public static double[] RibSpacing1 = new double[1000];
        public static double[] RibSpacing2 = new double[1000];

        public static int Numberd;
        public static string[] Named = new string[1000];
        public static int Selected;
        public static int[] Materiald = new int[1000];
        public static int[] MTyped = new int[1000];
        public static int[] OneWayd = new int[1000];
        public static int[] ProTyped = new int[1000];
        public static double[] Thicknessd = new double[1000];
        public static double[] OverallDepthd = new double[1000];
        public static double[] SlabThicknessd = new double[1000];
        public static double[] StemWidthatTopd = new double[1000];
        public static double[] StemWidthatBottomd = new double[1000];
        public static double[] RibSpacingd = new double[1000];
        public static int[] RibDirectiond = new int[1000];
        public static double[] RibSpacing1d = new double[1000];
        public static double[] RibSpacing2d = new double[1000];
    }
    class Wall
    {
        public static int Number;
        public static int SelectedToDraw;
        public static int Selected;
        public static string[] Name = new string[1000];
        public static int[] Material = new int[1000];
        public static int[] MType = new int[1000];
        public static int[] ProType = new int[1000];
        public static double[] Thickness = new double[1000];

        public static int Numberd;
        public static string[] Named = new string[1000];
        public static int[] Materiald = new int[1000];
        public static int[] MTyped = new int[1000];
        public static int[] ProTyped = new int[1000];
        public static double[] Thicknessd = new double[1000];
    }
    class Section
    {
        public static int Number;
        public static int Selected;
        public static int SelectedToDraw;
        public static string[] DESCRIPTION = new string[10000];
        public static string[] LENGTH_UNITS = new string[10000];
        public static string[] MODEL = new string[10000];
        public static string[] LABEL = new string[10000];
        public static string[] EDI_STD = new string[10000];
        public static string[] DESIGNATION = new string[10000];
        public static double[] D = new double[10000];
        public static double[] BF = new double[10000];
        public static double[] TF = new double[10000];
        public static double[] BFB = new double[10000];
        public static double[] TFB = new double[10000];
        public static double[] TW = new double[10000];
        public static double[] A = new double[10000];
        public static double[] I33 = new double[10000];
        public static double[] Z33 = new double[10000];
        public static double[] AS3 = new double[10000];
        public static double[] I22 = new double[10000];
        public static double[] Z22 = new double[10000];
        public static double[] AS2 = new double[10000];
        public static double[] J = new double[10000];
        public static double[] X = new double[10000];
        public static double[] Y = new double[10000];
        public static double[] S33POS = new double[10000];
        public static double[] S33NEG = new double[10000];
        public static double[] S22POS = new double[10000];
        public static double[] S22NEG = new double[10000];
        public static double[] R33 = new double[10000];
        public static double[] R22 = new double[10000];
        public static double[] B = new double[10000];
        public static double[] HT = new double[10000];
        public static double[] OD = new double[10000];
        public static double[] TDES = new double[10000];
        public static int[] Material = new int[10000];

        public static int Numberd;
        public static string[] DESCRIPTIONd= new string[10000];
        public static string[] LENGTH_UNITSd= new string[10000];
        public static string[] MODELd= new string[10000];
        public static string[] LABELd= new string[10000];
        public static string[] EDI_STDd= new string[10000];
        public static string[] DESIGNATIONd= new string[10000];
        public static double[] Dd = new double[10000];
        public static double[] BFd = new double[10000];
        public static double[] TFd = new double[10000];
        public static double[] BFBd = new double[10000];
        public static double[] TFBd = new double[10000];
        public static double[] TWd = new double[10000];
        public static double[] Ad = new double[10000];
        public static double[] I33d = new double[10000];
        public static double[] Z33d = new double[10000];
        public static double[] AS3d = new double[10000];
        public static double[] I22d = new double[10000];
        public static double[] Z22d = new double[10000];
        public static double[] AS2d = new double[10000];
        public static double[] Jd = new double[10000];
        public static double[] Xd = new double[10000];
        public static double[] Yd = new double[10000];
        public static double[] S33POSd = new double[10000];
        public static double[] S33NEGd = new double[10000];
        public static double[] S22POSd = new double[10000];
        public static double[] S22NEGd = new double[10000];
        public static double[] R33d = new double[10000];
        public static double[] R22d = new double[10000];
        public static double[] Bd = new double[10000];
        public static double[] HTd = new double[10000];
        public static double[] ODd = new double[10000];
        public static double[] TDESd = new double[10000];
        public static int[] Materiald = new int[10000];


        public static double[] ModifiersAread = new double[10000];
        public static double[] ModifiersShear2d = new double[10000];
        public static double[] ModifiersShear3d = new double[10000];
        public static double[] ModifiersTorsionald = new double[10000];
        public static double[] ModifiersI2d = new double[10000];
        public static double[] ModifiersI3d = new double[10000];
        public static double[] ModifiersMassd = new double[10000];
        public static double[] ModifiersWeightd = new double[10000];

        public static double[] ModifiersArea = new double[10000];
        public static double[] ModifiersShear2 = new double[10000];
        public static double[] ModifiersShear3 = new double[10000];
        public static double[] ModifiersTorsional = new double[10000];
        public static double[] ModifiersI2 = new double[10000];
        public static double[] ModifiersI3 = new double[10000];
        public static double[] ModifiersMass = new double[10000];
        public static double[] ModifiersWeight = new double[10000];

        public static int[] DesignType = new int[10000];
        public static int[] RebarMaterial1 = new int[10000];
        public static int[] RebarMaterial2 = new int[10000];
        public static double[] CoverTop = new double[10000];
        public static double[] CoverBottom = new double[10000];
        public static double[] ReinTopI = new double[10000];
        public static double[] ReinTopJ = new double[10000];
        public static double[] ReinBottomI = new double[10000];
        public static double[] ReinBottomJ = new double[10000];

        public static int[] DesignTyped = new int[10000];
        public static int[] RebarMaterial1d = new int[10000];
        public static int[] RebarMaterial2d = new int[10000];
        public static double[] CoverTopd = new double[10000];
        public static double[] CoverBottomd = new double[10000];
        public static double[] ReinTopId = new double[10000];
        public static double[] ReinTopJd = new double[10000];
        public static double[] ReinBottomId = new double[10000];
        public static double[] ReinBottomJd = new double[10000];

    }
    class GridPoint
    {
        public static int Number3d;
        public static int[] X3d= new int[10000];
        public static int[] Y3d = new int[10000];
        public static string[] Name3d = new string[10000];

        public static int Number2d;
        public static int[] X2d = new int[10000];
        public static int[] Y2d = new int[10000];
        public static string[] Name2d = new string[10000];

        public static int Numberelev;
        public static int[] Xelev = new int[10000];
        public static int[] Yelev = new int[10000];
        public static string[] Nameelev = new string[10000];

        public static double[] XReal = new double[10000];
        public static double[] YReal = new double[10000];
        public static double[] ZReal = new double[10000];
    }
    class Joint
    {
        public static int[] ID = new int[100000];
        public static int Number3d;
        public static int[] X3d = new int[100000];
        public static int[] Y3d = new int[100000];
        public static string[] Name3d = new string[100000];
        public static int Number2d;
        public static int[] X2d = new int[100000];
        public static int[] Y2d = new int[100000];
        public static string[] Name2d = new string[100000];
        public static int Numberelev;
        public static int[] Xelev = new int[100000];
        public static int[] Yelev = new int[100000];
        public static string[] Nameelev = new string[100000];

        public static double[] XReal = new double[100000];
        public static double[] YReal = new double[100000];
        public static double[] ZReal = new double[100000];
        public static int[] FixX = new int[100000];
        public static int[] FixY = new int[100000];
        public static int[] FixZ = new int[100000];
        public static int[] FixRX = new int[100000];
        public static int[] FixRY = new int[100000];
        public static int[] FixRZ = new int[100000];
        public static int[] Selected = new int[100000];
        public static int[] SelectedPriv = new int[100000];

        public static double[] PowerX = new double[100000];
        public static double[] PowerY = new double[100000];
        public static double[] PowerZ = new double[100000];
        public static double[] MomentXX = new double[100000];
        public static double[] MomentYY = new double[100000];
        public static double[] MomentZZ = new double[100000];

        public static int[] BeamConnectionN = new int[100000];
        public static int[] FloorConnectionN = new int[100000];
        public static int[,] Beam = new int[100000, 1000];
        public static int[,] Floor = new int[100000, 1000];
        public static int Assignments;
        public static int ShowPower;
        public static int SelectedforProp;
    }
    class JointMesh
    {
        public static int[] ID = new int[100000];
        public static int Number;
        public static double[] XReal = new double[100000];
        public static double[] YReal = new double[100000];
        public static double[] ZReal = new double[100000];
        public static int[] Floor = new int[100000];
        public static int[] X3d = new int[100000];
        public static int[] Y3d = new int[100000];
        public static int[] X2d = new int[100000];
        public static int[] Y2d = new int[100000];
        public static int[] Xelev = new int[100000];
        public static int[] Yelev = new int[100000];
    }
    class GridLine
    {
        public static int Number2d;
        public static int[] X12d = new int[10000];
        public static int[] X22d = new int[10000];
        public static int[] Y12d = new int[10000];
        public static int[] Y22d = new int[10000];
        public static int[] X1elev = new int[10000];
        public static int[] X2elev = new int[10000];
        public static int[] Y1elev = new int[10000];
        public static int[] Y2elev = new int[10000];
        public static int VisibleAs;
        public static int OnX;
        public static int OnY;
        public static int OnXY;
        public static int[] Visible;
        public static int[] XOrY;
        public static string[] Name;
        public static double[] Distance;
        public static double[] X1Real;
        public static double[] X2Real;
        public static double[] Y1Real;
        public static double[] Y2Real;

        public static int Number3d;
        public static int[] X13d = new int[10000];
        public static int[] X23d = new int[10000];
        public static int[] Y13d = new int[10000];
        public static int[] Y23d = new int[10000];
    }
    class Shell
    {
        public static int Selected;
        public static int Number;//عدد البلاطات
        public static int Localaxis;
        public static int[] PointNumbers = new int[10000];
        public static int[,] JointNo = new int[10000, 1000];
        public static int[] Type = new int[10000];
        public static int[,] SelectedLine = new int[10000, 1000];
        public static int[,] SelectedLinePriv = new int[10000, 1000];
        public static int[] Section = new int[10000];
        public static int PointNumbersTemp;
        public static double[] PointXTemp = new double[10000];
        public static double[] PointYTemp = new double[10000];
        public static double[] PointZTemp = new double[10000];
        public static int LineNumberTemp;
        public static int[] Visible = new int[10000];
        public static int[] LoadNumber = new int[10000];
        public static int[,] LoadType = new int[10000, 100];
        public static double[,] LoadUniform = new double[10000, 100];
        public static int[,] LoadDirection = new int[10000, 100];
        public static int[,] LoadPattern = new int[10000, 100];
        public static int SelectedforProp;

        public static double[,] AxisX1 = new double[10000, 4];
        public static double[,] AxisX2 = new double[10000, 4];
        public static double[,] AxisY1 = new double[10000, 4];
        public static double[,] AxisY2 = new double[10000, 4];
        public static double[,] AxisZ1 = new double[10000, 4];
        public static double[,] AxisZ2 = new double[10000, 4];

        public static int[,] AxisX13d = new int[10000, 4];
        public static int[,] AxisX23d = new int[10000, 4];
        public static int[,] AxisY13d = new int[10000, 4];
        public static int[,] AxisY23d = new int[10000, 4];

        public static double[] CenterX = new double[10000];
        public static double[] CenterY = new double[10000];
        public static double[] CenterZ = new double[10000];

        public static double[] Aria = new double[10000];
        public static double[] Perimeter = new double[10000];

        public static double[,] EnvelpoX = new double[10000, 5];
        public static double[,] EnvelpoY = new double[10000, 5];
        public static double[,] EnvelpoZ = new double[10000, 5];

        public static int MeshType;
        public static int NumberOfMeshLinesX;
        public static int NumberOfMeshLinesY;
        public static double[,] MeshLineXx1 = new double[10000, 100];
        public static double[,] MeshLineXy1 = new double[10000, 100];
        public static double[,] MeshLineXz1 = new double[10000, 100];
        public static double[,] MeshLineXx2 = new double[10000, 100];
        public static double[,] MeshLineXy2 = new double[10000, 100];
        public static double[,] MeshLineXz2 = new double[10000, 100];

        public static double[,] MeshLineYx1 = new double[10000, 100];
        public static double[,] MeshLineYy1 = new double[10000, 100];
        public static double[,] MeshLineYz1 = new double[10000, 100];
        public static double[,] MeshLineYx2 = new double[10000, 100];
        public static double[,] MeshLineYy2 = new double[10000, 100];
        public static double[,] MeshLineYz2 = new double[10000, 100];
    }
    class Myglobals
    {
        public static int MeshType;
        public static int NumberOfMeshLinesX;
        public static int NumberOfMeshLinesY;
        public static int ShllAnalysisMeshVeiw;
        public static double ShellCenterX;
        public static double ShellCenterY;
        public static double ShellCenterZ;
        public static double ShellPointX;
        public static double ShellPointY;
        public static double ShelltXValue;
        public static double ShelltYValue;
        public static double ShelltZValue;

        public static int ModifyShow_PropertyIsOpen;
        public static int FrameDesignerIsOpen;
        public static int FrameDesignerAnySpan;

        public static int ArchWallNumber;
        public static int BarsNumber;
        public static int LineBarsNumber;
        public static double EyeX;
        public static double EyeY;
        public static double EyeZ;

        public static int DrowRealShape;

        public static int AnyDiagram;
        public static int DrawDiagram;
        public static int DiagramValue;


        public static int IfAnalysis;
        public static string ExtrudeType;
        public static int PickPoints;
        public static int PickNumber;
        public static double PickX;
        public static double PickY;

        public static int ShowGrid;
        public static double Rotate3DVeiw;
        public static double CameraX;
        public static double CameraY;
        public static double CameraZ;

        public static int Tadweer;

        public static double valDY;
        public static double valDX;
        public static int Show3DWindow;
        public static int ShowPlaneWindow;
        public static int ShowEleveWindow;

        public static int Toggel;
        public static int ExtrudedFrame;
        public static int ExtrudedShell;

        public static string SelectedPlan;
        public static string SelectBy;
        public static int POWERTYPE;
        public static int TempFile;
        public static string[] TempFileName= new string[10000];
        public static int TempSelectedFile;
        public static int ShowJointPower;
        public static double RotateAngelDraw;
        public static int PropertyGridchoice;

        public static int   AriaNo ;
        public static int[]  AriaPointNo ;
        public static int AriaSelected;
        public static double[,] p1X;
        public static double[,] p1Y;

        public static int AriaNoELEV;
        public static int[] AriaPointNoELEV  = new int[10000];
        public static int AriaSelectedELEV;
        public static int[,] p1XELEV = new int[10000, 6];
        public static int[,] p1YELEV = new int[10000, 6];
        public static double[,] p1XELEVR = new double[10000, 6];
        public static double[,] p1YELEVR = new double[10000, 6];
        public static double[,] p1ZELEVR = new double[10000, 6];

        public static int EditOrNew;
        public static int AllStories;
        public static int FillObjects;
        public static int CenterX3d;
        public static int CenterY3d;
        public static int xmove;
        public static int ymove;
        public static int drowclick;
        public static int xmove1;
        public static int ymove1;
        public static double startX;
        public static double startY;
        public static int startX2d;
        public static int startY2d;
        public static double Zoom2d;

        public static int startX3d;
        public static int startY3d;
        public static int startZ3d;
        public static int Zoom3d;

        public static int startXelev;
        public static int startYelev;
        public static double Zoomelev;

        public static int TheX3d;
        public static int TheY3d;

        public static double TheX2d;
        public static double TheY2d;
        public static double TheZ2d;

        public static int TheX13d;
        public static int TheY13d;
        public static int TheX23d;
        public static int TheY23d;

        public static int LineMove2dVisible ;
        public static int LineMove2dFVisible ;
        public static int LineMove3dVisible;
        public static int LineMove3dFVisible;
        public static int LineMoveelevVisible;
        public static int LineMoveelevFVisible;

        public static int LineType;
        public static int BitampWidth3d;
        public static int BitampHight3d;

        public static int BitampWidth2d;
        public static int BitampHight2d;

        public static int BitampWidthelev;
        public static int BitampHightelev;

        public static double RotatePointX3d;
        public static double RotatePointY3d;
        public static double RotatePointZ3d;

        public static float tQXValue;
        public static float tQYValue;
        public static float tXValue;
        public static float tYValue;
        public static float tZValue;
        public static float Aperture;
        public static float Quarter;

        public static double[] OutAriaX = new double[5];
        public static double[] OutAriaY= new double[5];
        public static double[] OutAriaZ= new double[5];
        public static double[] OutAriaX3d = new double[5];
        public static double[] OutAriaY3d = new double[5];
        public static int AriaPointNumber;
        public static int SelectedPoint;
        ////--------------------------------------------------------------------الطوابق
        public static int SelectedStory;
        public static int StoryNumbers;
        public static string[] StoryName = new string[500];
        public static double[] StoryHight = new double[500];
        public static double[] StoryLevel = new double[500];
        public static int StoryNumbersTemp;
        public static string[] StoryNameTemp;
        public static double[] StoryHightTemp;
        public static double[] StoryLevelTemp;
        //----------------------------------------------------------------------
        ////--------------------------------------------------------------------الليفلات
        public static int Selectedelev;
        public static int elevNumbers;
        public static int[] elevPointNumbers = new int[500];
        public static int[] elevGridLine = new int[500];
        public static double[,] elevPointX = new double[500, 10000];
        public static double[,] elevPointY = new double[500, 10000];
        public static double[,] elevPointZ = new double[500, 10000];
        public static double[,] elevPointReal = new double[500, 10000];
        public static string[,] elevPointName = new string[500, 10000];
        //----------------------------------------------------------------------
    }
    public class Bars
    {
        public double XR;
        public double YR;
        public int X2D;
        public int Y2D;
        public double DR;
        public int D2D;
        public int Selected;
        public int InLine;
        public int InED;
        public int InREC;
        public int InCircle;
        public int Type;
        public int Corner;
        public Bars()
        {
        }
    }
    public class LineBars
    {
        public int HasEndBars;
        public double X1R;
        public double Y1R;
        public double X2R;
        public double Y2R;
        public double DR;
        public double Distance;
        public int BarsNumbers;

        public int X12D;
        public int Y12D;
        public int X22D;
        public int Y22D;
        public int Selected;

        public LineBars()
        {
        }
    }
    public class Clips
    {
        public double X1R;
        public double Y1R;
        public double X2R;
        public double Y2R;
        public double DR;
        public double DR1;
        public double DR2;
        public int Type;
        public int X12D;
        public int Y12D;
        public int X22D;
        public int Y22D;
        public int Dir1;
        public int Dir2;
        public int Selected;
        public Clips()
        {
        }
    }
    public class RecLineBars
    {
        public int Type;
        public int InRecShape;
        public int InFlangedWallShape;
        public int InTeeShape;
        public double Width;
        public double Height;
        public double CenterX;
        public double CenterY;
        public int CenterX2D;
        public int CenterY2D;
        public double[] EDX1R = new double[5];
        public double[] EDY1R = new double[5];
        public double[] EDX2R = new double[5];
        public double[] EDY2R = new double[5];
        public int[] EDX12D = new int[5];
        public int[] EDY12D = new int[5];
        public int[] EDX22D = new int[5];
        public int[] EDY22D = new int[5];
        public int[] EDBarsNumbers = new int[5];
        public int[] EDDR = new int[5];
        public int[] EDSelected = new int[5];
        public double[] EDDistance = new double[5];
        public int[] CORDR = new int[5];
        public int TIEDR;
        public int TIED2D;
        public RecLineBars()
        {
        }
    }
    public class CircleLineBars
    {
        public int InCircleShape;
        public double Diameter;
        public double CenterX;
        public double CenterY;
        public int CenterX2D;
        public int CenterY2D;

        public double[] PointXR = new double[34];
        public double[] PointYR = new double[34];
        public int[] PointX2D = new int[34];
        public int[] PointY2D = new int[34];
        public double[] Angle = new double[34];
        public int BarsNumbers;
        public int DR;
        public int D2D;
        public int Selected;
        public int TIEDR;
        public int TIED2D;
        public CircleLineBars()
        {
        }
    }
    public class RecShapes
    {
        public int ApplyedToRecBars;
        public double Width;
        public double Height;
        public double CenterX;
        public double CenterY;
        public int CenterX2D;
        public int CenterY2D;
        public int HasReinforcment;

        public double[] EDCoverR = new double[5];

        public double[] EDX1R = new double[5];
        public double[] EDY1R = new double[5];
        public double[] EDX2R = new double[5];
        public double[] EDY2R = new double[5];

        public int[] EDX12D = new int[5];
        public int[] EDY12D = new int[5];
        public int[] EDX22D = new int[5];
        public int[] EDY22D = new int[5];
        public int[] EDSelected = new int[5];
    }
    public class CircleShapes
    {
        public int ApplyedToCircleBars;
        public double Diameter;
        public double CenterX;
        public double CenterY;
        public int CenterX2D;
        public int CenterY2D;
        public double[] PointXR = new double[34];
        public double[] PointYR = new double[34];
        public int[] PointX2D = new int[34];
        public int[] PointY2D = new int[34];
        public double[] Angle = new double[34];
        public int Selected;
        public double CoverR;
        public int HasReinforcment;
    }
    public class FlangedWalls
    {
        public int[] ApplyedToRecBars = new int [4] ;
        public int HasReinforcment;
        public double CenterX;
        public double CenterY;
        public int CenterX2D;
        public int CenterY2D;
        public double Length;
        public double StemWidth;
        public double LeftFlangWidth;
        public double LeftFlangLength;
        public double RightFlangWidth;
        public double RightFlangLength;
        public double LeftFlangEccen;
        public double RightFlangEccen;
        public int StemSurrounded;
        public int LeftFlangSurrounded;
        public int RightFlangSurrounded;
        public  double[] PointXReal = new double[13];
        public  double[] PointYReal = new double[13];
        public int[] PointX2D = new int[13];
        public int[] PointY2D = new int[13];
        public int Selected;

        public double ED1CoverR;
        public double ED2CoverR;
        public double ED3CoverR;
        public double ED4CoverR;

        public double ED1LCoverR;
        public double ED2LCoverR;
        public double ED3LCoverR;
        public double ED4LCoverR;

        public double ED1RCoverR;
        public double ED2RCoverR;
        public double ED3RCoverR;
        public double ED4RCoverR;
    }
    public class TeeShapes
    {
        public int[] ApplyedToRecBars = new int[4];
        public int HasReinforcment;
        public double CenterX;
        public double CenterY;
        public int CenterX2D;
        public int CenterY2D;
        public double Height;
        public double WebThickness;
        public double FlangWidth;
        public double FlangThickness;
        public double[] PointXReal = new double[9];
        public double[] PointYReal = new double[9];
        public int[] PointX2D = new int[9];
        public int[] PointY2D = new int[9];
        public int Selected;

        public double ED1CoverR;
        public double ED2CoverR;
        public double ED3CoverR;
        public double ED4CoverR;

        public double ED1cCoverR;
        public double ED2cCoverR;
        public double ED3cCoverR;
        public double ED4cCoverR;
    }  
    public class ArchWallS
    {
        public string[] Function = new string[100];
        public string[] Material = new string[100];
        public double[] Thickness = new double[100];
        public int LayersNumber;
        public double AllThickness;
        public double TotalThickness;
        public string Name;
        public double Weight;
        public double Height;
        public int Selected;
    }

    public class ReinforcementBars
    {
        public int Kind;
        public int PointNumbers;
        public int Selected;
        public int Level;
        public double[] PointXR = new double[11];
        public double[] PointYR = new double[11];
        public double[] PointZR = new double[11];

        public int[] PointX2d = new int[11];
        public int[] PointY2d = new int[11];
        public int[] PointZ2d = new int[11];

        public double[] Angel = new double[3];
        public double[] PartLength = new double[11];

        public int Diameter ;
        public int Number;
    }
}
