using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMSPRO
{
    class Globals
    {
        public static int NodeNumberALL;         //--------Number of all Nodes 
        //public static int NodeNumber;          //--------Node Number 
        public static double[] NodeX;
        public static double[] NodeY;
        public static double[] NodeZ;
        public static int[] NodeFixX;
        public static int[] NodeFixY;
        public static int[] NodeFixZ;
        public static int[] NodeFixRX;
        public static int[] NodeFixRY;
        public static int[] NodeFixRZ;


        public static int ElementNumberALL;       //--------Number of all Elements 
        //  public static int ElementNumber;      //--------Element Number
        // public static int ElementType;         //--------Element Type
        public static int[] ElementInode;         //--------Start Node Number
        public static int[] Elementjnode;         //--------End Node Number
        public static double[] ElementA;          //--------Section Area
        public static double[] ElementE;          //--------  Elastic modulus

        public static double[] ElementG;          //--------  Shear modulus
        public static double[] ElementI33;
        public static double[] ElementI22;
        //public static double[] ElementIP;
        public static double[] ElementAS33;
        public static double[] ElementAS22;
        public static double[] ElementJ;
        public static int[] ElementType;         //--------Start Node Number

        ///-------------حجز مصفوفه الاحداثيات

        public static double[,] COORMatrix;       //============COOR : coordinate  matrix
        public static int[,] ICONMatrix;          //============ICON : connection matrix
        public static int[,] IDMatrix;            //============ID : Restrain matrix
        public static int[,] IDModifiedMatrix;            //============IDModified : Modified Restrain matrix
        public static int NEq;                     //--------Number of all Equations 

        public static int[,] LMMatrix;       //============LM : Location  matrix

        //============the direction cosines of the axial axis of the element

        public static double[] ElementL;
        public static double[] ElementN1;
        public static double[] ElementN2;
        public static double[] ElementN3;

        public static double[, ,] KLineElementMatrix;
        public static double[, ,] BTrussElementMatrix;
        public static double[, ,] AFrameElementMatrix;
        public static double[, ,] BFrameElementMatrix;
        public static double[, ,] KLFrameElementMatrix;
        public static double[, ,] TFrameElementMatrix;

        public static int[] MAXA;
        public static int[] ColH;

        public static int[] mjmatrix;
        public static int Nwk = 0;
        public static double[] Smvector;
        public static double[,] GSMvector;



        public static double[,] PNode; //=========== nodale load from external load and equivalent element loads

        public static double[] RLoad;
        public static double[] VLoad;
        public static double[] UaxialDisp;
        public static double[,] UDispAll;
        public static double[] UaxialDispAll1;
        public static double[] elementd;    // total exial displacement of truss element


        public static double[] Ngforce; // nodal: global nodal forces
        public static double[,] EGforce; // nodal: global nodal forces for each element from nodal displacement + external global loads
        public static double[,] EGforceEX; // nodal: global nodal forces for each element from external global loads
        public static double[,] ELforceEX; // nodal: Local nodal forces for each element from external Local loads
        public static double[,] ELforce; // nodal: Local nodal forces for each element from nodal displacement + external global loads


        ////////////for interaction surface
        public static double[, ,] ELforceEXP; // nodal: Local nodal forces for each element from external Local point loads
        public static double[, ,] ELforceEXPD; // nodal: Local nodal forces for each element from external Local distributed loads
        public static double[,] PaL;
        public static double[,] Mom2;
        public static double[,] Mom3;
        public static double[] angle;
        public static double[] angle1;
        public static int curvNo = 0;
        public static int angleNo = 0;
        public static int pointNO = 0;
        public static int secType = 0;
        public static int StressType = 1;
        public static double[] StressV;
        public static double[] StrainV;
        public static double[] StressVc;
        public static double[] StrainVc;

        public static double[] StressVs;
        public static double[] StrainVs;

        public static double ET = 0;

        public static double UNfac = 0;
        public static double UNfacL = 0;
        public static double UNfacF = 0;
        public static double UNfacSeg = 0;
        public static int selectchart = 0;

        //====================للحذف لاحقا
        public static double fc01 = 0;
        public static double fybar1 = 0;
        public static double secH1 = 0;
        public static double secB1 = 0;
        public static double secC1 = 0;
        public static double secD1 = 0;
        public static double ES1 = 0;
        public static int ASLn1 = 0;
        public static double[,] ASbar1;
        public static double dbar1 = 0;
        public static int Secsegment = 0;
        public static int codeNo = 0;


    }
}
