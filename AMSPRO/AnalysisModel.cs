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
    class AnalysisModel
    {
        double[,] c;
        Form mainForm = Application.OpenForms["MainForm"];

        public void AnalysisRun()
        {
            Okai();
            StiffnessMatrix();
            JointLoad();
            FramepointLoad();
            FramedistributedLoad();
            calcaluteglobalequivalentNodalLoadforgeneralsolution();
            calculateNodalGlobalDisplacement();
            calculateelementNodalLocalforces();
            Myglobals.IfAnalysis = 1;
        }

        private void Okai()
        {
            /////-------------------------input Nodes in Table
            int i = 0;
            Globals.NodeNumberALL = Joint.Number3d;
            Globals.NodeX = new double[Globals.NodeNumberALL];
            Globals.NodeY = new double[Globals.NodeNumberALL];
            Globals.NodeZ = new double[Globals.NodeNumberALL];
            Globals.NodeFixX = new int[Globals.NodeNumberALL];
            Globals.NodeFixY = new int[Globals.NodeNumberALL];
            Globals.NodeFixZ = new int[Globals.NodeNumberALL];
            Globals.NodeFixRX = new int[Globals.NodeNumberALL];
            Globals.NodeFixRY = new int[Globals.NodeNumberALL];
            Globals.NodeFixRZ = new int[Globals.NodeNumberALL];
            for (i = 0; i < Globals.NodeNumberALL; i++)
            {
                Globals.NodeX[i] = Joint.XReal[i + 1];
                Globals.NodeY[i] = Joint.YReal[i + 1];
                Globals.NodeZ[i] = Joint.ZReal[i + 1];
                Globals.NodeFixX[i] = Joint.FixX[i + 1];
                Globals.NodeFixY[i] = Joint.FixY[i + 1];
                Globals.NodeFixZ[i] = Joint.FixZ[i + 1];
                Globals.NodeFixRX[i] = Joint.FixRX[i + 1];
                Globals.NodeFixRY[i] = Joint.FixRY[i + 1];
                Globals.NodeFixRZ[i] = Joint.FixRZ[i + 1];
            }
            /////-------------------------input Elements  in Table
            Globals.ElementNumberALL = Frame.Number;
            Globals.ElementInode = new int[Globals.ElementNumberALL];
            Globals.Elementjnode = new int[Globals.ElementNumberALL];
            Globals.ElementA = new double[Globals.ElementNumberALL];
            Globals.ElementE = new double[Globals.ElementNumberALL];
            Globals.ElementG = new double[Globals.ElementNumberALL];
            Globals.ElementI33 = new double[Globals.ElementNumberALL];
            Globals.ElementI22 = new double[Globals.ElementNumberALL];
            Globals.ElementAS22 = new double[Globals.ElementNumberALL];
            Globals.ElementAS33 = new double[Globals.ElementNumberALL];
            Globals.ElementJ = new double[Globals.ElementNumberALL];
            Globals.ElementType = new int[Globals.ElementNumberALL];
            for (i = 0; i < Globals.ElementNumberALL; i++)
            {
                Globals.ElementInode[i] = ((MainForm)mainForm).FrameElement[i + 1].FirstJoint;
                Globals.Elementjnode[i] = ((MainForm)mainForm).FrameElement[i + 1].SecondJoint;
                Globals.ElementA[i] = Section.A[((MainForm)mainForm).FrameElement[i + 1].Section];
                Globals.ElementE[i] = Material.Elastisity[Section.Material[((MainForm)mainForm).FrameElement[i + 1].Section]];
                Globals.ElementG[i] = Material.ShearM[Section.Material[((MainForm)mainForm).FrameElement[i + 1].Section]];
                Globals.ElementI33[i] = Section.I33[((MainForm)mainForm).FrameElement[i + 1].Section];
                Globals.ElementI22[i] = Section.I22[((MainForm)mainForm).FrameElement[i + 1].Section];
                Globals.ElementAS22[i] = Section.AS2[((MainForm)mainForm).FrameElement[i + 1].Section];
                Globals.ElementAS33[i] = Section.AS3[((MainForm)mainForm).FrameElement[i + 1].Section];
                Globals.ElementJ[i] = Section.J[((MainForm)mainForm).FrameElement[i + 1].Section];
                Globals.ElementType[i] = 1;
            }
        }

        private void StiffnessMatrix()
        {
            int i = 0;
            int j = 0;
            int k = 0;
            //============COOR : coordinate  matrix
            int COORcolumnNo = Globals.NodeNumberALL;
            Globals.COORMatrix = new double[3, COORcolumnNo];
            for (i = 0; i < COORcolumnNo; i++)
            {
                Globals.COORMatrix[0, i] = Globals.NodeX[i];
                Globals.COORMatrix[1, i] = Globals.NodeY[i];
                Globals.COORMatrix[2, i] = Globals.NodeZ[i];
            }
            //============ICON : connection matrix
            int ICONcolumnNo = Globals.ElementNumberALL;
            Globals.ICONMatrix = new int[2, ICONcolumnNo];
            for (i = 0; i < ICONcolumnNo; i++)
            {
                Globals.ICONMatrix[0, i] = Globals.ElementInode[i];
                Globals.ICONMatrix[1, i] = Globals.Elementjnode[i];
            }
            //============ID : Restrain matrix
            int IDcolumnNo = Globals.NodeNumberALL;
            Globals.IDMatrix = new int[6, IDcolumnNo];
            for (i = 0; i < IDcolumnNo; i++)
            {
                Globals.IDMatrix[0, i] = Globals.NodeFixX[i];
                Globals.IDMatrix[1, i] = Globals.NodeFixY[i];
                Globals.IDMatrix[2, i] = Globals.NodeFixZ[i];
                Globals.IDMatrix[3, i] = Globals.NodeFixRX[i];
                Globals.IDMatrix[4, i] = Globals.NodeFixRY[i];
                Globals.IDMatrix[5, i] = Globals.NodeFixRZ[i];
            }
            //============IDModified : Modified Restrain matrix           
            Globals.IDModifiedMatrix = new int[6, IDcolumnNo];//عدد درجات الحرية لكل العقد????
            int m = 0;
            for (j = 0; j < IDcolumnNo; j++)
            {
                for (i = 0; i < 6; i++)
                {
                    if (Globals.IDMatrix[i, j] == 1)
                    {
                        Globals.IDModifiedMatrix[i, j] = 0;//عدد درجات الحرية
                    }
                    else
                    {
                        Globals.IDModifiedMatrix[i, j] = m + 1;
                        m = Globals.IDModifiedMatrix[i, j];
                    }
                }
            }
            //============  NEq : number of equations
            Globals.NEq = 0;
            for (j = 0; j < IDcolumnNo; j++)
            {
                for (i = 0; i < 6; i++)
                {
                    if (Globals.IDMatrix[i, j] == 0)
                    {
                        Globals.NEq = Globals.NEq + 1;
                    }
                }
            }
            //============LM : Location  matrix                      
            Globals.LMMatrix = new int[Globals.ElementNumberALL, 12];
            int IconElemwntNO = 0;     //----------Number of Element = Number of ICON column = Number of LM line
            int IDfristcolumnNo = 0;    //--------First column at the ID matrix = Number of the I Node of the element
            int IDsecondtcolumnNo = 0;  //--------Second column at the ID matrix  = Number of the J Node of the element
            for (i = 0; i < Globals.ElementNumberALL; i++)
            {
                IconElemwntNO = i;
                IDfristcolumnNo = Globals.ElementInode[IconElemwntNO];
                IDsecondtcolumnNo = Globals.Elementjnode[IconElemwntNO];
                for (j = 0; j < 6; j++)
                {
                    Globals.LMMatrix[i, j] = Globals.IDModifiedMatrix[j, IDfristcolumnNo - 1];
                }
                for (j = 6; j < 12; j++)
                {
                    Globals.LMMatrix[i, j] = Globals.IDModifiedMatrix[j - 6, IDsecondtcolumnNo - 1];
                }
            }
            //============the direction cosines of the axial axis of the element
            Globals.ElementL = new double[Globals.ElementNumberALL];
            Globals.ElementN1 = new double[Globals.ElementNumberALL];
            Globals.ElementN2 = new double[Globals.ElementNumberALL];
            Globals.ElementN3 = new double[Globals.ElementNumberALL];
            m = 0;
            int n = 0;
            double d = 0;
            double[] alfa = new double[Globals.ElementNumberALL];
            for (i = 0; i < Globals.ElementNumberALL; i++)
            {
                m = Globals.ElementInode[i];
                n = Globals.Elementjnode[i];
                d = Math.Pow((Globals.NodeX[m - 1] - Globals.NodeX[n - 1]), 2) + Math.Pow((Globals.NodeY[m - 1] - Globals.NodeY[n - 1]), 2) + Math.Pow((Globals.NodeZ[m - 1] - Globals.NodeZ[n - 1]), 2);
                Globals.ElementL[i] = (Math.Sqrt(d));
                Globals.ElementN1[i] = (Globals.NodeX[n - 1] - Globals.NodeX[m - 1]) / Globals.ElementL[i];
                Globals.ElementN2[i] = (Globals.NodeY[n - 1] - Globals.NodeY[m - 1]) / Globals.ElementL[i];
                Globals.ElementN3[i] = (Globals.NodeZ[n - 1] - Globals.NodeZ[m - 1]) / Globals.ElementL[i];
            }
            //============Stiffness Matrix  of the Line element/////////////////////////////////////
            Globals.BTrussElementMatrix = new double[Globals.ElementNumberALL, 1, 12];
            Globals.AFrameElementMatrix = new double[Globals.ElementNumberALL, 12, 6];
            Globals.BFrameElementMatrix = new double[Globals.ElementNumberALL, 6, 6];
            Globals.KLFrameElementMatrix = new double[Globals.ElementNumberALL, 12, 12];
            Globals.TFrameElementMatrix = new double[Globals.ElementNumberALL, 12, 12];
            Globals.KLineElementMatrix = new double[Globals.ElementNumberALL, 12, 12];

            double N1 = 0;
            double N2 = 0;
            double N3 = 0;
            double N4 = 0;
            double N5 = 0;
            double N6 = 0;

            double L1 = 0;
            double L2 = 0;
            double L3 = 0;
            double X2 = 0;
            double Y2 = 0;
            double Z2 = 0;
            double X3 = 0;
            double Y3 = 0;
            double Z3 = 0;

            for (i = 0; i < Globals.ElementNumberALL; i++)
            {
                // Globals.ElementIP[i] = Globals.ElementI33[i] + Globals.ElementI22[i];
            }

            double Ksec22 = 0;
            double Ksec33 = 0;

            for (k = 0; k < Globals.ElementNumberALL; k++)
            {
                if (Globals.ElementType[k] == 0) //====Truss element
                {
                    Globals.BTrussElementMatrix[k, 0, 0] = -1 * Globals.ElementN1[k] / Globals.ElementL[k];
                    Globals.BTrussElementMatrix[k, 0, 1] = -1 * Globals.ElementN2[k] / Globals.ElementL[k];
                    Globals.BTrussElementMatrix[k, 0, 2] = -1 * Globals.ElementN3[k] / Globals.ElementL[k];
                    Globals.BTrussElementMatrix[k, 0, 3] = 0;
                    Globals.BTrussElementMatrix[k, 0, 4] = 0;
                    Globals.BTrussElementMatrix[k, 0, 5] = 0;
                    Globals.BTrussElementMatrix[k, 0, 6] = Globals.ElementN1[k] / Globals.ElementL[k];
                    Globals.BTrussElementMatrix[k, 0, 7] = Globals.ElementN2[k] / Globals.ElementL[k];
                    Globals.BTrussElementMatrix[k, 0, 8] = Globals.ElementN3[k] / Globals.ElementL[k];
                    Globals.BTrussElementMatrix[k, 0, 9] = 0;
                    Globals.BTrussElementMatrix[k, 0, 10] = 0;
                    Globals.BTrussElementMatrix[k, 0, 11] = 0;
                }

                if (Globals.ElementType[k] == 1) //====Frame element
                {
                    //==============storage of A matrix for each frame 
                    Globals.AFrameElementMatrix[k, 0, 0] = -1;
                    Globals.AFrameElementMatrix[k, 1, 1] = -1;
                    Globals.AFrameElementMatrix[k, 2, 2] = -1;
                    Globals.AFrameElementMatrix[k, 3, 3] = -1;
                    Globals.AFrameElementMatrix[k, 4, 2] = Globals.ElementL[k];
                    Globals.AFrameElementMatrix[k, 4, 4] = -1;
                    Globals.AFrameElementMatrix[k, 5, 1] = -Globals.ElementL[k];
                    Globals.AFrameElementMatrix[k, 5, 5] = -1;
                    Globals.AFrameElementMatrix[k, 6, 0] = 1;
                    Globals.AFrameElementMatrix[k, 7, 1] = 1;
                    Globals.AFrameElementMatrix[k, 8, 2] = 1;
                    Globals.AFrameElementMatrix[k, 9, 3] = 1;
                    Globals.AFrameElementMatrix[k, 10, 4] = 1;
                    Globals.AFrameElementMatrix[k, 11, 5] = 1;

                    //==============storage of B matrix for each frame 

                    Ksec22 = Globals.ElementAS22[k] / Globals.ElementA[k];  ////=== coefficient of shear stress
                    Ksec33 = Globals.ElementAS33[k] / Globals.ElementA[k];

                    //Ksec22 = 1;  ////=== coefficient of shear stress
                    // Ksec33 =1;

                    Globals.BFrameElementMatrix[k, 0, 0] = Globals.ElementL[k] / (Globals.ElementE[k] * Globals.ElementA[k]);
                    Globals.BFrameElementMatrix[k, 1, 1] = Math.Pow(Globals.ElementL[k], 3) / (3 * Globals.ElementE[k] * Globals.ElementI33[k]) + Ksec22 * Globals.ElementL[k] / (Globals.ElementG[k] * Globals.ElementA[k]);
                    Globals.BFrameElementMatrix[k, 1, 5] = Math.Pow(Globals.ElementL[k], 2) / (2 * Globals.ElementE[k] * Globals.ElementI33[k]);
                    Globals.BFrameElementMatrix[k, 2, 2] = Math.Pow(Globals.ElementL[k], 3) / (3 * Globals.ElementE[k] * Globals.ElementI22[k]) + Ksec33 * Globals.ElementL[k] / (Globals.ElementG[k] * Globals.ElementA[k]); ;
                    Globals.BFrameElementMatrix[k, 2, 4] = -Math.Pow(Globals.ElementL[k], 2) / (2 * Globals.ElementE[k] * Globals.ElementI22[k]);
                    Globals.BFrameElementMatrix[k, 3, 3] = Globals.ElementL[k] / (Globals.ElementG[k] * Globals.ElementJ[k]);
                    Globals.BFrameElementMatrix[k, 4, 2] = -Math.Pow(Globals.ElementL[k], 2) / (2 * Globals.ElementE[k] * Globals.ElementI22[k]);
                    Globals.BFrameElementMatrix[k, 4, 4] = Globals.ElementL[k] / (Globals.ElementE[k] * Globals.ElementI22[k]);
                    Globals.BFrameElementMatrix[k, 5, 1] = Math.Pow(Globals.ElementL[k], 2) / (2 * Globals.ElementE[k] * Globals.ElementI33[k]);
                    Globals.BFrameElementMatrix[k, 5, 5] = Globals.ElementL[k] / (Globals.ElementE[k] * Globals.ElementI33[k]);
                }
            }

            for (k = 0; k < Globals.ElementNumberALL; k++)
            {
                if (Globals.ElementType[k] == 0) //====Truss element
                {
                    int row1 = 12;
                    int col1 = 1;
                    int row2 = 1;
                    int col2 = 12;
                    double[,] a = new double[row1, col1];
                    double[,] b = new double[row2, col2];
                    for (i = 0; i < 12; i++)
                    {
                        a[i, 0] = Globals.BTrussElementMatrix[k, 0, i];
                        b[0, i] = Globals.BTrussElementMatrix[k, 0, i];
                    }
                    MatrixMultiplication(row1, col1, row2, col2, a, b);
                    for (i = 0; i < 12; i++)
                    {
                        for (j = 0; j < 12; j++)
                        {
                            Globals.KLineElementMatrix[k, i, j] = Globals.ElementA[k] * Globals.ElementE[k] * Globals.ElementL[k] * c[i, j];
                        }
                    }
                }

                if (Globals.ElementType[k] == 1) //====Frame element
                {
                    // int row1 = 12;
                    // int col1 = 6;
                    // int row2 = 6;
                    //int col2 = 6;
                    double[,] a = new double[12, 6];
                    double[,] bele = new double[6, 6];
                    double[,] b = new double[6, 6];
                    double[,] aT = new double[6, 12];
                    double[,] TT = new double[12, 12];
                    double[,] KFR = new double[12, 12];
                    double[,] TFR = new double[12, 12];
                    for (i = 0; i < 12; i++)
                    {
                        for (j = 0; j < 6; j++)
                        {
                            a[i, j] = Globals.AFrameElementMatrix[k, i, j];
                            aT[j, i] = Globals.AFrameElementMatrix[k, i, j];
                        }
                    }

                    for (i = 0; i < 6; i++)
                    {
                        for (j = 0; j < 6; j++)
                        {
                            bele[i, j] = Globals.BFrameElementMatrix[k, i, j];
                        }
                    }

                    int num_rows = 6;
                    double[,] matrix = new double[num_rows, num_rows];
                    for (int row = 0; row < num_rows; row++)
                    {
                        for (int col = 0; col < num_rows; col++)
                        {
                            matrix[row, col] = bele[row, col];
                        }
                    }

                    // Find the inverse.
                    double[,] inverse = InvertMatrix(matrix);
                    b = inverse;
                    MatrixMultiplication(12, 6, 6, 6, a, b);
                    MatrixMultiplication(12, 6, 6, 12, c, aT);
                    for (i = 0; i < 12; i++)
                    {
                        for (j = 0; j < 12; j++)
                        {
                            Globals.KLFrameElementMatrix[k, i, j] = c[i, j];
                        }
                    }
                    ////////////////////////////TRANSFER  MATRIX
                    N1 = ((MainForm)mainForm).FrameElement[k + 1].AxisX2[1] - ((MainForm)mainForm).FrameElement[k + 1].AxisX1[1];
                    N2 = ((MainForm)mainForm).FrameElement[k + 1].AxisY2[1] - ((MainForm)mainForm).FrameElement[k + 1].AxisY1[1];
                    N3 = ((MainForm)mainForm).FrameElement[k + 1].AxisZ2[1] - ((MainForm)mainForm).FrameElement[k + 1].AxisZ1[1];
                    L1 = Math.Sqrt(Math.Pow(N1, 2) + Math.Pow(N2, 2) + Math.Pow(N3, 2));
                    N4 = ((MainForm)mainForm).FrameElement[k + 1].AxisX2[3] - ((MainForm)mainForm).FrameElement[k + 1].AxisX1[3];
                    N5 = (((MainForm)mainForm).FrameElement[k + 1].AxisY2[3] - ((MainForm)mainForm).FrameElement[k + 1].AxisY1[3]);
                    N6 = ((MainForm)mainForm).FrameElement[k + 1].AxisZ2[3] - ((MainForm)mainForm).FrameElement[k + 1].AxisZ1[3];

                    X2 = N5 * N3 - N6 * N2;
                    Y2 = -(N4 * N3 - N1 * N6);
                    Z2 = N4 * N2 - N1 * N5;
                    L2 = Math.Sqrt(Math.Pow(X2, 2) + Math.Pow(Y2, 2) + Math.Pow(Z2, 2));

                    X3 = N2 * Z2 / L2 - N3 * Y2 / L2;
                    Y3 = -(N1 * Z2 / L2 - N3 * X2 / L2);
                    Z3 = N1 * Y2 / L2 - N2 * X2 / L2;
                    L3 = Math.Sqrt(Math.Pow(X3, 2) + Math.Pow(Y3, 2) + Math.Pow(Z3, 2));


                    Globals.TFrameElementMatrix[k, 0, 0] = N1 / L1;
                    Globals.TFrameElementMatrix[k, 0, 1] = N2 / L1;
                    Globals.TFrameElementMatrix[k, 0, 2] = N3 / L1;

                    Globals.TFrameElementMatrix[k, 1, 0] = X2 / L2;
                    Globals.TFrameElementMatrix[k, 1, 1] = Y2 / L2;
                    Globals.TFrameElementMatrix[k, 1, 2] = Z2 / L2;

                    Globals.TFrameElementMatrix[k, 2, 0] = X3 / L3;
                    Globals.TFrameElementMatrix[k, 2, 1] = Y3 / L3;
                    Globals.TFrameElementMatrix[k, 2, 2] = Z3 / L3;

                    Globals.TFrameElementMatrix[k, 3, 3] = Globals.TFrameElementMatrix[k, 0, 0];
                    Globals.TFrameElementMatrix[k, 3, 4] = Globals.TFrameElementMatrix[k, 0, 1];
                    Globals.TFrameElementMatrix[k, 3, 5] = Globals.TFrameElementMatrix[k, 0, 2];

                    Globals.TFrameElementMatrix[k, 4, 3] = Globals.TFrameElementMatrix[k, 1, 0];
                    Globals.TFrameElementMatrix[k, 4, 4] = Globals.TFrameElementMatrix[k, 1, 1];
                    Globals.TFrameElementMatrix[k, 4, 5] = Globals.TFrameElementMatrix[k, 1, 2];


                    Globals.TFrameElementMatrix[k, 5, 3] = Globals.TFrameElementMatrix[k, 2, 0];
                    Globals.TFrameElementMatrix[k, 5, 4] = Globals.TFrameElementMatrix[k, 2, 1];
                    Globals.TFrameElementMatrix[k, 5, 5] = Globals.TFrameElementMatrix[k, 2, 2];

                    Globals.TFrameElementMatrix[k, 6, 6] = Globals.TFrameElementMatrix[k, 0, 0];
                    Globals.TFrameElementMatrix[k, 6, 7] = Globals.TFrameElementMatrix[k, 0, 1];
                    Globals.TFrameElementMatrix[k, 6, 8] = Globals.TFrameElementMatrix[k, 0, 2];

                    Globals.TFrameElementMatrix[k, 7, 6] = Globals.TFrameElementMatrix[k, 1, 0];
                    Globals.TFrameElementMatrix[k, 7, 7] = Globals.TFrameElementMatrix[k, 1, 1];
                    Globals.TFrameElementMatrix[k, 7, 8] = Globals.TFrameElementMatrix[k, 1, 2];

                    Globals.TFrameElementMatrix[k, 8, 6] = Globals.TFrameElementMatrix[k, 2, 0];
                    Globals.TFrameElementMatrix[k, 8, 7] = Globals.TFrameElementMatrix[k, 2, 1];
                    Globals.TFrameElementMatrix[k, 8, 8] = Globals.TFrameElementMatrix[k, 2, 2];

                    Globals.TFrameElementMatrix[k, 9, 9] = Globals.TFrameElementMatrix[k, 0, 0];
                    Globals.TFrameElementMatrix[k, 9, 10] = Globals.TFrameElementMatrix[k, 0, 1];
                    Globals.TFrameElementMatrix[k, 9, 11] = Globals.TFrameElementMatrix[k, 0, 2];

                    Globals.TFrameElementMatrix[k, 10, 9] = Globals.TFrameElementMatrix[k, 1, 0];
                    Globals.TFrameElementMatrix[k, 10, 10] = Globals.TFrameElementMatrix[k, 1, 1];
                    Globals.TFrameElementMatrix[k, 10, 11] = Globals.TFrameElementMatrix[k, 1, 2];

                    Globals.TFrameElementMatrix[k, 11, 9] = Globals.TFrameElementMatrix[k, 2, 0];
                    Globals.TFrameElementMatrix[k, 11, 10] = Globals.TFrameElementMatrix[k, 2, 1];
                    Globals.TFrameElementMatrix[k, 11, 11] = Globals.TFrameElementMatrix[k, 2, 2];

                    for (i = 0; i < 12; i++)
                    {
                        for (j = 0; j < 12; j++)
                        {
                            TFR[i, j] = Globals.TFrameElementMatrix[k, i, j];
                            TT[j, i] = Globals.TFrameElementMatrix[k, i, j];
                        }
                    }

                    for (i = 0; i < 12; i++)
                    {
                        for (j = 0; j < 12; j++)
                        {
                            KFR[i, j] = Globals.KLFrameElementMatrix[k, i, j];
                        }
                    }

                    MatrixMultiplication(12, 12, 12, 12, TT, KFR);
                    MatrixMultiplication(12, 12, 12, 12, c, TFR);

                    for (i = 0; i < 12; i++)
                    {
                        for (j = 0; j < 12; j++)
                        {
                            Globals.KLineElementMatrix[k, i, j] = c[i, j];
                        }
                    }

                }

            }

            /////=========MAXA===Locations of diagonal elements of Stiffness Matrix in S vector (S: stiffness matrix invert to column vector)
            int[] minDof;
            if (Globals.NEq == 0)
            {
                goto End121;
            }

            minDof = new int[Globals.NEq];
            minDof[0] = 1;

            //--------Search for minDof in LM(6,NEq) matrix for each Dof of reduced Global stiffness matrix (NEq,NEq) that we donot storage it as Square matrix
            int DOF;

            int Lms;

            for (DOF = 2; DOF < Globals.NEq + 1; DOF++)    //====number of diagonal elements=NEq
            {
                minDof[DOF - 1] = DOF;
            }

            for (DOF = 2; DOF < Globals.NEq + 1; DOF++)    //====number of diagonal elements=NEq
            {
                //--------Search for minDof in LM(6,NEq) matrix for each Dof of reduced Global stiffness matrix (NEq,NEq) that we donot storage it as Square matrix
                for (j = 0; j < Globals.ElementNumberALL; j++)    //---number of LM rows     
                {
                    for (k = 0; k < 12; k++)  //---number of LM columns
                    {
                        if (Globals.LMMatrix[j, k] == DOF) //---for each location Wherever Dof of reduced Global stiffness matrix exists  
                        {
                            for (Lms = 0; Lms < 12; Lms++)   // shearching for minDof in the row that has DOF
                            {

                                if (Globals.LMMatrix[j, Lms] > 0 && Globals.LMMatrix[j, Lms] < DOF)
                                {
                                    //  if (minDof[DOF - 1] > 0)
                                    //  {
                                    if (Globals.LMMatrix[j, Lms] < minDof[DOF - 1])
                                    {
                                        minDof[DOF - 1] = Globals.LMMatrix[j, Lms];
                                    }
                                    // }

                                    //  if (minDof[DOF - 1] == 0)
                                    //{
                                    //    minDof[DOF - 1] = Globals.LMMatrix[j, Lms];
                                    // }


                                }
                            }
                            goto End100;
                        }
                    }
                End100: { }
                }
            }
            Globals.MAXA = new int[Globals.NEq];
            for (i = 0; i < Globals.NEq; i++)    //====number of diagonal elements=NEq
            {
                if (i == 0)                    //=====position of first diagonal element in Stiffness matrix is Located in first position in S Vector
                {
                    Globals.MAXA[0] = 0;
                    goto End10;
                }
                Globals.MAXA[i] = (Globals.MAXA[i - 1] + (i + 1) - minDof[i] + 1);
            End10:
                {
                }
            }
            //==================calculate columns height
            Globals.ColH = new int[Globals.NEq];
            for (i = 0; i < Globals.NEq; i++)    //====number of diagonal elements=NEq
            {
                if (i == 0)                    //=====position of first diagonal element in Stiffness matrix is Located in first position in S Vector
                {
                    Globals.ColH[0] = 1;
                    goto End20;
                }
                Globals.ColH[i] = Globals.MAXA[i] - Globals.MAXA[i - 1];
            End20:
                {
                }
            }
            //==================calculate Nwk: the number of stiffenss matrix elemnts (Vector Stiffness matrix: S)
            Globals.Nwk = 0;

            for (i = 0; i < Globals.NEq; i++)    //====number of diagonal elements=NEq
            {
                Globals.Nwk = Globals.Nwk + Globals.ColH[i];
            }
            //=========Assmbel Stiffness matrix as S vector 
            Globals.Smvector = new double[Globals.Nwk];
            int diage = 0;
            int POsnondiage = 0;
            //string POsnondiage1G ;
            // string POsnondiage1L ;
            int POsnondiage1S;
            for (j = 0; j < Globals.Nwk; j++)    //====number of diagonal elements=NEq
            {
                Globals.Smvector[j] = 0;
            }
            for (i = 0; i < Globals.ElementNumberALL; i++)
            {
                for (Lms = 0; Lms < 12; Lms++)
                {
                    if (Globals.LMMatrix[i, Lms] > 0)
                    {
                        diage = Globals.LMMatrix[i, Lms];    //====number of diagonal elements
                        Globals.Smvector[Globals.MAXA[diage - 1]] = Globals.Smvector[Globals.MAXA[diage - 1]] + Globals.KLineElementMatrix[i, Lms, Lms];
                        for (k = 0; k < 12; k++)
                        {
                            if (Globals.LMMatrix[i, k] > 0 && Globals.LMMatrix[i, k] > Globals.LMMatrix[i, Lms])
                            {
                                POsnondiage = Globals.LMMatrix[i, k];
                                //  POsnondiage1G = diage.ToString()+ POsnondiage.ToString();
                                //    POsnondiage1L =  Lms.ToString() +  k.ToString();
                                POsnondiage1S = Globals.MAXA[POsnondiage - 1] - (POsnondiage - diage);
                                Globals.Smvector[POsnondiage1S] = Globals.Smvector[POsnondiage1S] + Globals.KLineElementMatrix[i, Lms, k];
                            }
                        }
                    }
                }
            }


            //=============================Active column solution--------making the stiffness matrix tringular

            //==============================if diagonal elemnts of stiffness matrix equal zero make it very large number (10e15)

            for (j = 0; j < Globals.NEq; j++)
            {
                if (Globals.Smvector[Globals.MAXA[j]] == 0)
                {
                    Globals.Smvector[Globals.MAXA[j]] = 1e10;
                }
            }


            //--------(mj matrix)-----calculate the row number of the nonzero element in coulmn J of stiffness matrix
            Globals.mjmatrix = new int[Globals.NEq];
            Globals.mjmatrix[0] = 0;
            for (j = 1; j < Globals.NEq; j++)
            {
                Globals.mjmatrix[j] = j - (Globals.ColH[j] - 1);
            }

            double[,] gg;
            gg = new double[Globals.NEq, 1];
            double[,] dd;
            dd = new double[Globals.NEq, 1];
            double[,] LL;
            LL = new double[Globals.NEq, 1];
            int ii = 0;
            double sum = 0;
            int maxm = 0;
            int rmm = 0;

            if (Globals.Smvector[0] == 0)
            {
                Globals.Smvector[0] = 1e10;
            }
            dd[0, 0] = Globals.Smvector[0];

            for (j = 1; j < Globals.NEq; j++)
            {
                if (j == 1)
                {
                    gg[Globals.mjmatrix[j], 0] = Globals.Smvector[Globals.MAXA[j] - (Globals.ColH[j] - Globals.mjmatrix[j]) - Globals.mjmatrix[j] + 1];   ///=====gmj,j
                    LL[Globals.mjmatrix[j], 0] = gg[Globals.mjmatrix[j], 0] / dd[0, 0];
                    Globals.Smvector[Globals.MAXA[j] - (Globals.ColH[j] - Globals.mjmatrix[j]) - Globals.mjmatrix[j] + 1] = LL[Globals.mjmatrix[j], 0];
                    dd[j, 0] = 0;
                    for (rmm = Globals.mjmatrix[j]; rmm < j; rmm++)
                    {
                        sum = sum + LL[rmm, 0] * gg[rmm, 0];
                    }
                    dd[j, 0] = Globals.Smvector[Globals.MAXA[j]] - sum;
                    Globals.Smvector[Globals.MAXA[j]] = Globals.Smvector[Globals.MAXA[j]] - sum;
                    goto End30;
                }
                for (k = Globals.mjmatrix[j - 1]; k < Globals.ColH[j - 1] + Globals.mjmatrix[j - 1]; k++)
                {
                    LL[k, 0] = Globals.Smvector[Globals.MAXA[j - 1] - (Globals.ColH[j - 1] - k) - Globals.mjmatrix[j - 1] + 1];
                }
                for (k = Globals.mjmatrix[j]; k < Globals.ColH[j] + Globals.mjmatrix[j]; k++)
                {
                    gg[k, 0] = Globals.Smvector[Globals.MAXA[j] - (Globals.ColH[j] - k) - Globals.mjmatrix[j] + 1];

                }
                for (ii = Globals.mjmatrix[j] + 1; ii < j; ii++)
                {
                    maxm = Globals.mjmatrix[j];
                    if (Globals.mjmatrix[ii] > maxm)
                    {
                        maxm = Globals.mjmatrix[ii];
                    }

                    sum = 0;
                    for (rmm = maxm; rmm < ii; rmm++)
                    {
                        // sum = sum +LL[rmm, 0] * gg[rmm, 0];
                        sum = sum + Globals.Smvector[Globals.MAXA[ii] - (Globals.ColH[ii] - rmm) - Globals.mjmatrix[ii] + 1] * gg[rmm, 0];
                    }
                    gg[ii, 0] = gg[ii, 0] - sum;
                }

                for (i = Globals.mjmatrix[j]; i < j; i++)
                {
                    LL[i, 0] = gg[i, 0] / dd[i, 0];
                    Globals.Smvector[Globals.MAXA[j] - (Globals.ColH[j] - i) - Globals.mjmatrix[j] + 1] = LL[i, 0];
                }
                sum = 0;
                dd[j, 0] = Globals.Smvector[Globals.MAXA[j]];
                for (rmm = Globals.mjmatrix[j]; rmm < j; rmm++)
                {
                    sum = sum + LL[rmm, 0] * gg[rmm, 0];

                }
                dd[j, 0] = dd[j, 0] - sum;
                if (dd[j, 0] == 0)
                {
                    dd[j, 0] = 1e10;
                }

                Globals.Smvector[Globals.MAXA[j]] = dd[j, 0];

            End30:
                { }
            }


        End121: { }

        }
       
        
        
        private void JointLoad()
        {
            double[,] a = new double[Joint.Number3d, 6];
            Globals.PNode = new double[6, Globals.NodeNumberALL];
            int i = 0;
            for (i = 0; i < Joint.Number3d; i++)
            {
                a[i, 0] = Joint.PowerX[i + 1];
                a[i, 1] = Joint.PowerY[i + 1];
                a[i, 2] = Joint.PowerZ[i + 1];
                a[i, 3] = Joint.MomentXX[i + 1];
                a[i, 4] = Joint.MomentYY[i + 1];
                a[i, 5] = Joint.MomentZZ[i + 1];
            }
            int add = 0;
            for (add = 0; add < Globals.NodeNumberALL; add++)
            {
                for (i = 0; i < 6; i++)
                {

                    Globals.PNode[i, add] = Globals.PNode[i, add] + a[add, i];
                }
            }
        }
        private void FramepointLoad()
        {
            Globals.ELforceEX = new double[Globals.ElementNumberALL, 12];  //==== equivalent Nodal Load in Local coordinate from all pointed loads applied on the element
            Globals.ELforceEXP = new double[Globals.ElementNumberALL, 10, 12];  //for each element: equivalent nodal forces from each nodal point in Local coordinate

            double[] Lele = new double[Frame.Number + 1];
            double p1 = 0;
            double p2 = 0;
            double m1 = 0;
            double m2 = 0;
            double a = 0;
            double b = 0;
            double dist = 0;
            int LDir = 0;
            //=====calclate the equivalent Nodal Load in Local coordinate form Local Loads(input)
            for (int i = 1; i < Frame.Number + 1; i++)
            {

                Lele[i] = (Math.Sqrt(Math.Pow((Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint] - Joint.XReal[((MainForm)mainForm).FrameElement[i].FirstJoint]), 2) + Math.Pow((Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint] - Joint.YReal[((MainForm)mainForm).FrameElement[i].FirstJoint]), 2) + Math.Pow((Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint] - Joint.ZReal[((MainForm)mainForm).FrameElement[i].FirstJoint]), 2)));
                for (int j = 1; j < ((MainForm)mainForm).FrameElement[i].LoadPNumber + 1; j++)
                {
                    dist = ((MainForm)mainForm).FrameElement[i].LoadPDistance[j];
                    a = dist * Lele[i];
                    b = (1 - dist) * Lele[i];
                    LDir = ((MainForm)mainForm).FrameElement[i].LoadPDirection[j];
                    if (LDir == 0)   //======point force along X',1 axis or point moment about X',1 axis
                    {
                        if (((MainForm)mainForm).FrameElement[i].LoadPType[j] == 1)   //===point force along X',1 axis
                        {
                            p1 = ((MainForm)mainForm).FrameElement[i].LoadPValue[j] * b / Lele[i];
                            p2 = ((MainForm)mainForm).FrameElement[i].LoadPValue[j] * a / Lele[i];

                            Globals.ELforceEX[i - 1, 0] = Globals.ELforceEX[i - 1, 0] + p1; //px',1  //  assmbel nodal local joint loads from all point load (later we transfer thir to Global coor to use thir in general solution)
                            Globals.ELforceEX[i - 1, 6] = Globals.ELforceEX[i - 1, 6] + p2;

                            Globals.ELforceEXP[i - 1, j, 0] = p1;  //   storage nodal local joint loads for each point load ( we use it later in calculating M,S.... diagrams)
                            Globals.ELforceEXP[i - 1, j, 6] = p2;
                        }

                        if (((MainForm)mainForm).FrameElement[i].LoadPType[j] == 2)  //===point Moment about  X',1 axis
                        {

                            m1 = ((MainForm)mainForm).FrameElement[i].LoadPValue[j] * b / Lele[i];
                            m2 = ((MainForm)mainForm).FrameElement[i].LoadPValue[j] * a / Lele[i];

                            Globals.ELforceEX[i - 1, 3] = Globals.ELforceEX[i - 1, 3] + m1; // Mx',1 
                            Globals.ELforceEX[i - 1, 9] = Globals.ELforceEX[i - 1, 9] + m2;

                            Globals.ELforceEXP[i - 1, j, 3] = m1;
                            Globals.ELforceEXP[i - 1, j, 9] = m2;

                        }

                    }

                    if (LDir == 1)  //===point force along Y',2 axis or point moment about Y',2 axis
                    {

                        if (((MainForm)mainForm).FrameElement[i].LoadPType[j] == 1)  //===point force along Y',2 axis
                        {

                            p1 = ((MainForm)mainForm).FrameElement[i].LoadPValue[j] * Math.Pow(b, 2) * (Lele[i] + 2 * a) / Math.Pow(Lele[i], 3);
                            p2 = ((MainForm)mainForm).FrameElement[i].LoadPValue[j] * Math.Pow(a, 2) * (Lele[i] + 2 * b) / Math.Pow(Lele[i], 3);
                            m1 = ((MainForm)mainForm).FrameElement[i].LoadPValue[j] * Math.Pow(b, 2) * a / Math.Pow(Lele[i], 2);
                            m2 = -((MainForm)mainForm).FrameElement[i].LoadPValue[j] * Math.Pow(a, 2) * b / Math.Pow(Lele[i], 2);

                            Globals.ELforceEX[i - 1, 1] = Globals.ELforceEX[i - 1, 1] + p1;//py',2
                            Globals.ELforceEX[i - 1, 7] = Globals.ELforceEX[i - 1, 7] + p2;
                            Globals.ELforceEX[i - 1, 5] = Globals.ELforceEX[i - 1, 5] + m1; //Mz',3
                            Globals.ELforceEX[i - 1, 11] = Globals.ELforceEX[i - 1, 11] + m2;


                            Globals.ELforceEXP[i - 1, j, 1] = p1;
                            Globals.ELforceEXP[i - 1, j, 7] = p2;
                            Globals.ELforceEXP[i - 1, j, 5] = m1;
                            Globals.ELforceEXP[i - 1, j, 11] = m2;

                        }



                        if (((MainForm)mainForm).FrameElement[i].LoadPType[j] == 2)  //===point Moment about  Y',2 axis
                        {
                            p1 = -((MainForm)mainForm).FrameElement[i].LoadPValue[j] * (Math.Pow(a, 2) + Math.Pow(b, 2) - 4 * a * b - Math.Pow(Lele[i], 2)) / Math.Pow(Lele[i], 3);
                            p2 = ((MainForm)mainForm).FrameElement[i].LoadPValue[j] * (Math.Pow(a, 2) + Math.Pow(b, 2) - 4 * a * b - Math.Pow(Lele[i], 2)) / Math.Pow(Lele[i], 3);
                            m1 = -((MainForm)mainForm).FrameElement[i].LoadPValue[j] * b * (2 * a - b) / Math.Pow(Lele[i], 2);
                            m2 = -((MainForm)mainForm).FrameElement[i].LoadPValue[j] * a * (2 * b - a) / Math.Pow(Lele[i], 2);

                            Globals.ELforceEX[i - 1, 2] = Globals.ELforceEX[i - 1, 2] + p1;//pz',3
                            Globals.ELforceEX[i - 1, 8] = Globals.ELforceEX[i - 1, 8] + p2;
                            Globals.ELforceEX[i - 1, 4] = Globals.ELforceEX[i - 1, 4] + m1;//My',2
                            Globals.ELforceEX[i - 1, 10] = Globals.ELforceEX[i - 1, 10] + m2;

                            Globals.ELforceEXP[i - 1, j, 2] = p1;
                            Globals.ELforceEXP[i - 1, j, 8] = p2;
                            Globals.ELforceEXP[i - 1, j, 4] = m1;
                            Globals.ELforceEXP[i - 1, j, 10] = m2;

                        }

                    }


                    if (LDir == 2)   //===point force along Z',3 axis or point moment about Z',3  axis
                    {

                        if (((MainForm)mainForm).FrameElement[i].LoadPType[j] == 1) //===point force along -Z',3 axis
                        {
                            p1 = ((MainForm)mainForm).FrameElement[i].LoadPValue[j] * Math.Pow(b, 2) * (Lele[i] + 2 * a) / Math.Pow(Lele[i], 3);
                            p2 = ((MainForm)mainForm).FrameElement[i].LoadPValue[j] * Math.Pow(a, 2) * (Lele[i] + 2 * b) / Math.Pow(Lele[i], 3);
                            m1 = -((MainForm)mainForm).FrameElement[i].LoadPValue[j] * Math.Pow(b, 2) * a / Math.Pow(Lele[i], 2);
                            m2 = ((MainForm)mainForm).FrameElement[i].LoadPValue[j] * Math.Pow(a, 2) * b / Math.Pow(Lele[i], 2);

                            Globals.ELforceEX[i - 1, 2] = Globals.ELforceEX[i - 1, 2] + p1; //PZ',3
                            Globals.ELforceEX[i - 1, 8] = Globals.ELforceEX[i - 1, 8] + p2;
                            Globals.ELforceEX[i - 1, 4] = Globals.ELforceEX[i - 1, 4] + m1; //MY',2
                            Globals.ELforceEX[i - 1, 10] = Globals.ELforceEX[i - 1, 10] + m2;

                            Globals.ELforceEXP[i - 1, j, 2] = p1;
                            Globals.ELforceEXP[i - 1, j, 8] = p2;
                            Globals.ELforceEXP[i - 1, j, 4] = m1;
                            Globals.ELforceEXP[i - 1, j, 10] = m2;

                        }

                        if (((MainForm)mainForm).FrameElement[i].LoadPType[j] == 2) //===point Moment about  Z',3 axis 
                        {
                            p1 = ((MainForm)mainForm).FrameElement[i].LoadPValue[j] * (Math.Pow(a, 2) + Math.Pow(b, 2) - 4 * a * b - Math.Pow(Lele[i], 2)) / Math.Pow(Lele[i], 3);
                            p2 = -((MainForm)mainForm).FrameElement[i].LoadPValue[j] * (Math.Pow(a, 2) + Math.Pow(b, 2) - 4 * a * b - Math.Pow(Lele[i], 2)) / Math.Pow(Lele[i], 3);
                            m1 = -((MainForm)mainForm).FrameElement[i].LoadPValue[j] * b * (2 * a - b) / Math.Pow(Lele[i], 2);
                            m2 = -((MainForm)mainForm).FrameElement[i].LoadPValue[j] * a * (2 * b - a) / Math.Pow(Lele[i], 2);

                            Globals.ELforceEX[i - 1, 1] = Globals.ELforceEX[i - 1, 1] + p1; //Py',2
                            Globals.ELforceEX[i - 1, 7] = Globals.ELforceEX[i - 1, 7] + p2;
                            Globals.ELforceEX[i - 1, 5] = Globals.ELforceEX[i - 1, 5] + m1; //Mz',3
                            Globals.ELforceEX[i - 1, 11] = Globals.ELforceEX[i - 1, 11] + m2;

                            Globals.ELforceEXP[i - 1, j, 1] = p1;
                            Globals.ELforceEXP[i - 1, j, 7] = p2;
                            Globals.ELforceEXP[i - 1, j, 5] = m1;
                            Globals.ELforceEXP[i - 1, j, 11] = m2;

                            // Globals.PNodeY[((MainForm)mainForm).FrameElement[i].FirstJoint - 1] = Globals.PNodeY[((MainForm)mainForm).FrameElement[i].FirstJoint - 1] + p1;
                            // Globals.PNodeY[((MainForm)mainForm).FrameElement[i].SecondJoint - 1] = Globals.PNodeY[((MainForm)mainForm).FrameElement[i].SecondJoint - 1] + p2;
                            // Globals.MNodeMZ[((MainForm)mainForm).FrameElement[i].FirstJoint - 1] = Globals.MNodeMZ[((MainForm)mainForm).FrameElement[i].FirstJoint - 1] + m1;
                            // Globals.MNodeMZ[((MainForm)mainForm).FrameElement[i].SecondJoint - 1] = Globals.MNodeMZ[((MainForm)mainForm).FrameElement[i].SecondJoint - 1] + m2;

                        }
                    }


                }

            }
        }
        private void FramedistributedLoad()
        {
            Globals.ELforceEXPD = new double[Globals.ElementNumberALL, 10, 12];  //for each element: equivalent nodal forces from each nodal point in Local coordinate
            double[] Lele = new double[Frame.Number + 1];
            double p1 = 0;
            double p2 = 0;
            double m1 = 0;
            double m2 = 0;
            double x1 = 0;
            double x2 = 0;
            double x3 = 0;
            double x4 = 0;
            double x5 = 0;
            double dist1 = 0;
            double dist2 = 0;
            double dist3 = 0;
            double dist4 = 0;
            double pow = 0;
            double pow1 = 0;
            double pow2 = 0;
            double pow3 = 0;
            double pow4 = 0;
            double w = 0;
            double w1 = 0;
            int LDir = 0;
            double xx1 = 0;
            double xx2 = 0;
            double xx3 = 0;
            double ww1 = 0;
            double ww2 = 0;
            double ww3 = 0;
            p1 = 0;
            p2 = 0;
            m1 = 0;
            m2 = 0;

            for (int i = 1; i < Frame.Number + 1; i++)
            {

                Lele[i] = Globals.ElementL[i - 1];
                x5 = Lele[i];
                for (int j = 1; j < ((MainForm)mainForm).FrameElement[i].LoadDNumber + 1; j++)
                {

                    //jd = 4 * j;
                    dist1 = ((MainForm)mainForm).FrameElement[i].LoadDDistance1[j ];
                    dist2 = ((MainForm)mainForm).FrameElement[i].LoadDDistance2[j ];
                    dist3 = ((MainForm)mainForm).FrameElement[i].LoadDDistance3[j ];
                    dist4 = ((MainForm)mainForm).FrameElement[i].LoadDDistance4[j ];
                    x1 = dist1 * Lele[i];
                    x2 = dist2 * Lele[i];
                    x3 = dist3 * Lele[i];
                    x4 = dist4 * Lele[i];
                    LDir = ((MainForm)mainForm).FrameElement[i].LoadDDirection[j];
                    pow1 = ((MainForm)mainForm).FrameElement[i].LoadDValue1[j ];
                    pow2 = ((MainForm)mainForm).FrameElement[i].LoadDValue2[j ];
                    pow3 = ((MainForm)mainForm).FrameElement[i].LoadDValue3[j ];
                    pow4 = ((MainForm)mainForm).FrameElement[i].LoadDValue4[j ];
                    pow = ((MainForm)mainForm).FrameElement[i].LoadDUniform[j];


                    if (LDir == 0) //=======distributed force or moment along 1 
                    {

                        if (((MainForm)mainForm).FrameElement[i].LoadDType[j] == 1)   //===distributed force along X',1 axis
                        {

                            if (pow != 0)  //====uniform force
                            {
                                xx1 = (0 + x5) / 2 - Math.Sqrt(0.6) * (x5 - 0) / 2;
                                xx2 = (0 + x5) / 2;
                                xx3 = (0 + x5) / 2 + Math.Sqrt(0.6) * (x5 - 0) / 2;
                                ww1 = 5 * (x5 - 0) / 18;
                                ww2 = 8 * (x5 - 0) / 18;
                                ww3 = 5 * (x5 - 0) / 18;

                                p1 = pow * ((1 - xx1 / x5) * ww1 + (1 - xx2 / x5) * ww2 + (1 - xx3 / x5) * ww3);
                                p2 = pow * ((xx1 / x5) * ww1 + (xx2 / x5) * ww2 + (xx3 / x5) * ww3);
                                Globals.ELforceEX[i - 1, 0] = Globals.ELforceEX[i - 1, 0] + p1;//p1
                                Globals.ELforceEX[i - 1, 6] = Globals.ELforceEX[i - 1, 6] + p2;
                                Globals.ELforceEXPD[i - 1, j, 0] = p1;
                                Globals.ELforceEXPD[i - 1, j, 6] = p2;
                            }

                            //==== first part from pow1 to pow2       
                            if ((x1 - x2) == 0)
                            {
                                goto End240;
                            }

                            w = pow2 - pow1;
                            w1 = pow1 * x2 - pow2 * x1;
                            xx1 = (x1 + x2) / 2 - Math.Sqrt(0.6) * (x2 - x1) / 2;
                            xx2 = (x1 + x2) / 2;
                            xx3 = (x1 + x2) / 2 + Math.Sqrt(0.6) * (x2 - x1) / 2;
                            ww1 = 5 * (x2 - x1) / 18;
                            ww2 = 8 * (x2 - x1) / 18;
                            ww3 = 5 * (x2 - x1) / 18;

                            p1 = (1 - xx1 / x5) * ww1 * (xx1 * w + w1) / (x2 - x1) + (1 - xx2 / x5) * ww2 * (xx2 * w + w1) / (x2 - x1) + (1 - xx3 / x5) * ww3 * (xx3 * w + w1) / (x2 - x1);
                            p2 = (xx1 / x5) * ww1 * (xx1 * w + w1) / (x2 - x1) + (xx2 / x5) * ww2 * (xx2 * w + w1) / (x2 - x1) + (xx3 / x5) * ww3 * (xx3 * w + w1) / (x2 - x1);
                            Globals.ELforceEX[i - 1, 0] = Globals.ELforceEX[i - 1, 0] + p1;//p1
                            Globals.ELforceEX[i - 1, 6] = Globals.ELforceEX[i - 1, 6] + p2;
                            Globals.ELforceEXPD[i - 1, j, 0] = Globals.ELforceEXPD[i - 1, j, 0] + p1;
                            Globals.ELforceEXPD[i - 1, j, 6] = Globals.ELforceEXPD[i - 1, j, 6] + p2;


                        End240: { }

                            if ((x3 - x2) == 0)
                            {
                                goto End250;
                            }

                            //==== second part from pow2 to pow3

                            w = pow3 - pow2;
                            w1 = pow2 * x3 - pow3 * x2;

                            xx1 = (x3 + x2) / 2 - Math.Sqrt(0.6) * (x3 - x2) / 2;
                            xx2 = (x3 + x2) / 2;
                            xx3 = (x3 + x2) / 2 + Math.Sqrt(0.6) * (x3 - x2) / 2;
                            ww1 = 5 * (x3 - x2) / 18;
                            ww2 = 8 * (x3 - x2) / 18;
                            ww3 = 5 * (x3 - x2) / 18;
                            p1 = ((1 - xx1 / x5) * ww1 * (xx1 * w + w1) + (1 - xx2 / x5) * ww2 * (xx2 * w + w1) + (1 - xx3 / x5) * ww3 * (xx3 * w + w1)) / (x3 - x2);
                            p2 = ((xx1 / x5) * ww1 * (xx1 * w + w1) + (xx2 / x5) * ww2 * (xx2 * w + w1) + (xx3 / x5) * ww3 * (xx3 * w + w1)) / (x3 - x2);

                            Globals.ELforceEX[i - 1, 0] = Globals.ELforceEX[i - 1, 0] + p1;//p1
                            Globals.ELforceEX[i - 1, 6] = Globals.ELforceEX[i - 1, 6] + p2;
                            Globals.ELforceEXPD[i - 1, j, 0] = Globals.ELforceEXPD[i - 1, j, 0] + p1;
                            Globals.ELforceEXPD[i - 1, j, 6] = Globals.ELforceEXPD[i - 1, j, 6] + p2;

                        End250: { }

                            if ((x4 - x3) == 0)
                            {
                                goto End260;
                            }
                            //==== third part from pow2 to pow3
                            w = pow4 - pow3;
                            w1 = pow3 * x4 - pow4 * x3;

                            xx1 = (x3 + x4) / 2 - Math.Sqrt(0.6) * (x4 - x3) / 2;
                            xx2 = (x3 + x4) / 2;
                            xx3 = (x3 + x4) / 2 + Math.Sqrt(0.6) * (x4 - x3) / 2;
                            ww1 = 5 * (x4 - x3) / 18;
                            ww2 = 8 * (x4 - x3) / 18;
                            ww3 = 5 * (x4 - x3) / 18;
                            p1 = ((1 - xx1 / x5) * ww1 * (xx1 * w + w1) + (1 - xx2 / x5) * ww2 * (xx2 * w + w1) + (1 - xx3 / x5) * ww3 * (xx3 * w + w1)) / (x4 - x3);
                            p2 = ((xx1 / x5) * ww1 * (xx1 * w + w1) + (xx2 / x5) * ww2 * (xx2 * w + w1) + (xx3 / x5) * ww3 * (xx3 * w + w1)) / (x4 - x3);


                            Globals.ELforceEX[i - 1, 0] = Globals.ELforceEX[i - 1, 0] + p1;//p1
                            Globals.ELforceEX[i - 1, 6] = Globals.ELforceEX[i - 1, 6] + p2;
                            Globals.ELforceEXPD[i - 1, j, 0] = Globals.ELforceEXPD[i - 1, j, 0] + p1;
                            Globals.ELforceEXPD[i - 1, j, 6] = Globals.ELforceEXPD[i - 1, j, 6] + p2;

                        End260: { }
                        }

                        if (((MainForm)mainForm).FrameElement[i].LoadDType[j] == 2)  //===distributed Moment about  X',1 axis
                        {


                            if (pow != 0)  //====uniform moment
                            {
                                xx1 = (0 + x5) / 2 - Math.Sqrt(0.6) * (x5 - 0) / 2;
                                xx2 = (0 + x5) / 2;
                                xx3 = (0 + x5) / 2 + Math.Sqrt(0.6) * (x5 - 0) / 2;
                                ww1 = 5 * (x5 - 0) / 18;
                                ww2 = 8 * (x5 - 0) / 18;
                                ww3 = 5 * (x5 - 0) / 18;

                                m1 = pow * ((1 - xx1 / x5) * ww1 + (1 - xx2 / x5) * ww2 + (1 - xx3 / x5) * ww3);
                                m2 = pow * ((xx1 / x5) * ww1 + (xx2 / x5) * ww2 + (xx3 / x5) * ww3);
                                Globals.ELforceEX[i - 1, 3] = Globals.ELforceEX[i - 1, 3] + m1;//p1
                                Globals.ELforceEX[i - 1, 9] = Globals.ELforceEX[i - 1, 9] + m2;
                                Globals.ELforceEXPD[i - 1, j, 3] = m1;
                                Globals.ELforceEXPD[i - 1, j, 9] = m2;
                            }

                            //==== first part from pow1 to pow2       
                            if ((x1 - x2) == 0)
                            {
                                goto End340;
                            }

                            w = pow2 - pow1;
                            w1 = pow1 * x2 - pow2 * x1;
                            xx1 = (x1 + x2) / 2 - Math.Sqrt(0.6) * (x2 - x1) / 2;
                            xx2 = (x1 + x2) / 2;
                            xx3 = (x1 + x2) / 2 + Math.Sqrt(0.6) * (x2 - x1) / 2;
                            ww1 = 5 * (x2 - x1) / 18;
                            ww2 = 8 * (x2 - x1) / 18;
                            ww3 = 5 * (x2 - x1) / 18;

                            m1 = (1 - xx1 / x5) * ww1 * (xx1 * w + w1) / (x2 - x1) + (1 - xx2 / x5) * ww2 * (xx2 * w + w1) / (x2 - x1) + (1 - xx3 / x5) * ww3 * (xx3 * w + w1) / (x2 - x1);
                            m2 = (xx1 / x5) * ww1 * (xx1 * w + w1) / (x2 - x1) + (xx2 / x5) * ww2 * (xx2 * w + w1) / (x2 - x1) + (xx3 / x5) * ww3 * (xx3 * w + w1) / (x2 - x1);
                            Globals.ELforceEX[i - 1, 3] = Globals.ELforceEX[i - 1, 3] + m1;//p1
                            Globals.ELforceEX[i - 1, 9] = Globals.ELforceEX[i - 1, 9] + m2;
                            Globals.ELforceEXPD[i - 1, j, 3] = Globals.ELforceEXPD[i - 1, j, 3] + m1;
                            Globals.ELforceEXPD[i - 1, j, 9] = Globals.ELforceEXPD[i - 1, j, 9] + m2;


                        End340: { }

                            if ((x3 - x2) == 0)
                            {
                                goto End350;
                            }

                            //==== second part from pow2 to pow3

                            w = pow3 - pow2;
                            w1 = pow2 * x3 - pow3 * x2;

                            xx1 = (x3 + x2) / 2 - Math.Sqrt(0.6) * (x3 - x2) / 2;
                            xx2 = (x3 + x2) / 2;
                            xx3 = (x3 + x2) / 2 + Math.Sqrt(0.6) * (x3 - x2) / 2;
                            ww1 = 5 * (x3 - x2) / 18;
                            ww2 = 8 * (x3 - x2) / 18;
                            ww3 = 5 * (x3 - x2) / 18;
                            m1 = ((1 - xx1 / x5) * ww1 * (xx1 * w + w1) + (1 - xx2 / x5) * ww2 * (xx2 * w + w1) + (1 - xx3 / x5) * ww3 * (xx3 * w + w1)) / (x3 - x2);
                            m2 = ((xx1 / x5) * ww1 * (xx1 * w + w1) + (xx2 / x5) * ww2 * (xx2 * w + w1) + (xx3 / x5) * ww3 * (xx3 * w + w1)) / (x3 - x2);

                            Globals.ELforceEX[i - 1, 3] = Globals.ELforceEX[i - 1, 3] + m1;//p1
                            Globals.ELforceEX[i - 1, 9] = Globals.ELforceEX[i - 1, 9] + m2;
                            Globals.ELforceEXPD[i - 1, j, 3] = Globals.ELforceEXPD[i - 1, j, 3] + m1;
                            Globals.ELforceEXPD[i - 1, j, 9] = Globals.ELforceEXPD[i - 1, j, 9] + m2;

                        End350: { }

                            if ((x4 - x3) == 0)
                            {
                                goto End360;
                            }
                            //==== third part from pow2 to pow3
                            w = pow4 - pow3;
                            w1 = pow3 * x4 - pow4 * x3;

                            xx1 = (x3 + x4) / 2 - Math.Sqrt(0.6) * (x4 - x3) / 2;
                            xx2 = (x3 + x4) / 2;
                            xx3 = (x3 + x4) / 2 + Math.Sqrt(0.6) * (x4 - x3) / 2;
                            ww1 = 5 * (x4 - x3) / 18;
                            ww2 = 8 * (x4 - x3) / 18;
                            ww3 = 5 * (x4 - x3) / 18;
                            m1 = ((1 - xx1 / x5) * ww1 * (xx1 * w + w1) + (1 - xx2 / x5) * ww2 * (xx2 * w + w1) + (1 - xx3 / x5) * ww3 * (xx3 * w + w1)) / (x4 - x3);
                            m2 = ((xx1 / x5) * ww1 * (xx1 * w + w1) + (xx2 / x5) * ww2 * (xx2 * w + w1) + (xx3 / x5) * ww3 * (xx3 * w + w1)) / (x4 - x3);


                            Globals.ELforceEX[i - 1, 3] = Globals.ELforceEX[i - 1, 3] + m1;//p1
                            Globals.ELforceEX[i - 1, 9] = Globals.ELforceEX[i - 1, 9] + m2;
                            Globals.ELforceEXPD[i - 1, j, 3] = Globals.ELforceEXPD[i - 1, j, 3] + m1;
                            Globals.ELforceEXPD[i - 1, j, 9] = Globals.ELforceEXPD[i - 1, j, 9] + m2;

                        End360: { }

                        }


                    }

                    //======================================


                    if (LDir == 1) //=======distributed force or moment along 2 
                    {

                        if (((MainForm)mainForm).FrameElement[i].LoadDType[j] == 1) //=======distributed force along 2 
                        {
                            if (pow != 0)  //====uniform load
                            {
                                p1 = pow * Lele[i] / 2;
                                p2 = p1;
                                m1 = pow * Math.Pow(Lele[i], 2) / 12;
                                m2 = -m1;

                                Globals.ELforceEX[i - 1, 1] = Globals.ELforceEX[i - 1, 1] + p1;//py',2
                                Globals.ELforceEX[i - 1, 7] = Globals.ELforceEX[i - 1, 7] + p2;
                                Globals.ELforceEX[i - 1, 5] = Globals.ELforceEX[i - 1, 5] + m1; //Mz',3
                                Globals.ELforceEX[i - 1, 11] = Globals.ELforceEX[i - 1, 11] + m2;

                                Globals.ELforceEXPD[i - 1, j, 1] = p1;
                                Globals.ELforceEXPD[i - 1, j, 7] = p2;
                                Globals.ELforceEXPD[i - 1, j, 5] = m1;
                                Globals.ELforceEXPD[i - 1, j, 11] = m2;
                            }


                            //==== first part from pow1 to pow2       
                            if ((x1 - x2) == 0)
                            {
                                goto End10;
                            }

                            w = pow2 - pow1;
                            w1 = pow1 * x2 - pow2 * x1;

                            p1 = (-2 / Math.Pow(Lele[i], 3)) * ((Math.Pow(x2, 5) - Math.Pow(x1, 5)) * w / 5 + (Math.Pow(x2, 4) - Math.Pow(x1, 4)) * w1 / 4);
                            p1 = p1 + (3 / Math.Pow(Lele[i], 2)) * ((Math.Pow(x2, 4) - Math.Pow(x1, 4)) * w / 4 + (Math.Pow(x2, 3) - Math.Pow(x1, 3)) * w1 / 3);
                            p1 = p1 - ((Math.Pow(x2, 2) - Math.Pow(x1, 2)) * w / 2 + (x2 - x1) * w1);
                            p1 = -p1 / (x2 - x1);

                            p2 = (2 / Math.Pow(Lele[i], 3)) * ((Math.Pow(x2, 5) - Math.Pow(x1, 5)) * w / 5 + (Math.Pow(x2, 4) - Math.Pow(x1, 4)) * w1 / 4);
                            p2 = p2 + (-3 / Math.Pow(Lele[i], 2)) * ((Math.Pow(x2, 4) - Math.Pow(x1, 4)) * w / 4 + (Math.Pow(x2, 3) - Math.Pow(x1, 3)) * w1 / 3);
                            p2 = -p2 / (x2 - x1);

                            m1 = (-1 / Math.Pow(Lele[i], 2)) * ((Math.Pow(x2, 5) - Math.Pow(x1, 5)) * w / 5 + (Math.Pow(x2, 4) - Math.Pow(x1, 4)) * w1 / 4);
                            m1 = m1 + (2 / Lele[i]) * ((Math.Pow(x2, 4) - Math.Pow(x1, 4)) * w / 4 + (Math.Pow(x2, 3) - Math.Pow(x1, 3)) * w1 / 3);
                            m1 = m1 - ((Math.Pow(x2, 3) - Math.Pow(x1, 3)) * w / 3 + (Math.Pow(x2, 2) - Math.Pow(x1, 2)) * w1 / 2);
                            m1 = -m1 / (x2 - x1);

                            m2 = (-1 / Math.Pow(Lele[i], 2)) * ((Math.Pow(x2, 5) - Math.Pow(x1, 5)) * w / 5 + (Math.Pow(x2, 4) - Math.Pow(x1, 4)) * w1 / 4);
                            m2 = m2 + (1 / Lele[i]) * ((Math.Pow(x2, 4) - Math.Pow(x1, 4)) * w / 4 + (Math.Pow(x2, 3) - Math.Pow(x1, 3)) * w1 / 3);
                            m2 = -m2 / (x2 - x1);

                            Globals.ELforceEX[i - 1, 1] = Globals.ELforceEX[i - 1, 1] + p1;//py',2
                            Globals.ELforceEX[i - 1, 7] = Globals.ELforceEX[i - 1, 7] + p2;
                            Globals.ELforceEX[i - 1, 5] = Globals.ELforceEX[i - 1, 5] + m1; //Mz',3
                            Globals.ELforceEX[i - 1, 11] = Globals.ELforceEX[i - 1, 11] + m2;

                            Globals.ELforceEXPD[i - 1, j, 1] = Globals.ELforceEXPD[i - 1, j, 1] + p1;
                            Globals.ELforceEXPD[i - 1, j, 7] = Globals.ELforceEXPD[i - 1, j, 7] + p2;
                            Globals.ELforceEXPD[i - 1, j, 5] = Globals.ELforceEXPD[i - 1, j, 5] + m1;
                            Globals.ELforceEXPD[i - 1, j, 11] = Globals.ELforceEXPD[i - 1, j, 11] + m2;

                        End10: { }


                            if ((x3 - x2) == 0)
                            {
                                goto End20;
                            }

                            //==== second part from pow2 to pow3

                            w = pow3 - pow2;
                            w1 = pow2 * x3 - pow3 * x2;

                            p1 = (-2 / Math.Pow(Lele[i], 3)) * ((Math.Pow(x3, 5) - Math.Pow(x2, 5)) * w / 5 + (Math.Pow(x3, 4) - Math.Pow(x2, 4)) * w1 / 4);
                            p1 = p1 + (3 / Math.Pow(Lele[i], 2)) * ((Math.Pow(x3, 4) - Math.Pow(x2, 4)) * w / 4 + (Math.Pow(x3, 3) - Math.Pow(x2, 3)) * w1 / 3);
                            p1 = p1 - ((Math.Pow(x3, 2) - Math.Pow(x2, 2)) * w / 2 + (x3 - x2) * w1);
                            p1 = -p1 / (x3 - x2);

                            p2 = (2 / Math.Pow(Lele[i], 3)) * ((Math.Pow(x3, 5) - Math.Pow(x2, 5)) * w / 5 + (Math.Pow(x3, 4) - Math.Pow(x2, 4)) * w1 / 4);
                            p2 = p2 + (-3 / Math.Pow(Lele[i], 2)) * ((Math.Pow(x3, 4) - Math.Pow(x2, 4)) * w / 4 + (Math.Pow(x3, 3) - Math.Pow(x2, 3)) * w1 / 3);
                            p2 = -p2 / (x3 - x2);

                            m1 = (-1 / Math.Pow(Lele[i], 2)) * ((Math.Pow(x3, 5) - Math.Pow(x2, 5)) * w / 5 + (Math.Pow(x3, 4) - Math.Pow(x2, 4)) * w1 / 4);
                            m1 = m1 + (2 / Lele[i]) * ((Math.Pow(x3, 4) - Math.Pow(x2, 4)) * w / 4 + (Math.Pow(x3, 3) - Math.Pow(x2, 3)) * w1 / 3);
                            m1 = m1 - ((Math.Pow(x3, 3) - Math.Pow(x2, 3)) * w / 3 + (Math.Pow(x3, 2) - Math.Pow(x2, 2)) * w1 / 2);
                            m1 = -m1 / (x3 - x2);

                            m2 = (-1 / Math.Pow(Lele[i], 2)) * ((Math.Pow(x3, 5) - Math.Pow(x2, 5)) * w / 5 + (Math.Pow(x3, 4) - Math.Pow(x2, 4)) * w1 / 4);
                            m2 = m2 + (1 / Lele[i]) * ((Math.Pow(x3, 4) - Math.Pow(x2, 4)) * w / 4 + (Math.Pow(x3, 3) - Math.Pow(x2, 3)) * w1 / 3);
                            m2 = -m2 / (x3 - x2);


                            Globals.ELforceEX[i - 1, 1] = Globals.ELforceEX[i - 1, 1] + p1;//py',2
                            Globals.ELforceEX[i - 1, 7] = Globals.ELforceEX[i - 1, 7] + p2;
                            Globals.ELforceEX[i - 1, 5] = Globals.ELforceEX[i - 1, 5] + m1; //Mz',3
                            Globals.ELforceEX[i - 1, 11] = Globals.ELforceEX[i - 1, 11] + m2;


                            Globals.ELforceEXPD[i - 1, j, 1] = Globals.ELforceEXPD[i - 1, j, 1] + p1;
                            Globals.ELforceEXPD[i - 1, j, 7] = Globals.ELforceEXPD[i - 1, j, 7] + p2;
                            Globals.ELforceEXPD[i - 1, j, 5] = Globals.ELforceEXPD[i - 1, j, 5] + m1;
                            Globals.ELforceEXPD[i - 1, j, 11] = Globals.ELforceEXPD[i - 1, j, 11] + m2;


                        End20: { }

                            if ((x4 - x3) == 0)
                            {
                                goto End30;
                            }
                            //==== third part from pow2 to pow3
                            w = pow4 - pow3;
                            w1 = pow3 * x4 - pow4 * x3;

                            p1 = (-2 / Math.Pow(Lele[i], 3)) * ((Math.Pow(x4, 5) - Math.Pow(x3, 5)) * w / 5 + (Math.Pow(x4, 4) - Math.Pow(x3, 4)) * w1 / 4);
                            p1 = p1 + (3 / Math.Pow(Lele[i], 2)) * ((Math.Pow(x4, 4) - Math.Pow(x3, 4)) * w / 4 + (Math.Pow(x4, 3) - Math.Pow(x3, 3)) * w1 / 3);
                            p1 = p1 - ((Math.Pow(x4, 2) - Math.Pow(x3, 2)) * w / 2 + (x4 - x3) * w1);
                            p1 = -p1 / (x4 - x3);

                            p2 = (2 / Math.Pow(Lele[i], 3)) * ((Math.Pow(x4, 5) - Math.Pow(x3, 5)) * w / 5 + (Math.Pow(x4, 4) - Math.Pow(x3, 4)) * w1 / 4);
                            p2 = p2 + (-3 / Math.Pow(Lele[i], 2)) * ((Math.Pow(x4, 4) - Math.Pow(x3, 4)) * w / 4 + (Math.Pow(x4, 3) - Math.Pow(x3, 3)) * w1 / 3);
                            p2 = -p2 / (x4 - x3);

                            m1 = (-1 / Math.Pow(Lele[i], 2)) * ((Math.Pow(x4, 5) - Math.Pow(x3, 5)) * w / 5 + (Math.Pow(x4, 4) - Math.Pow(x3, 4)) * w1 / 4);
                            m1 = m1 + (2 / Lele[i]) * ((Math.Pow(x4, 4) - Math.Pow(x3, 4)) * w / 4 + (Math.Pow(x4, 3) - Math.Pow(x3, 3)) * w1 / 3);
                            m1 = m1 - ((Math.Pow(x4, 3) - Math.Pow(x3, 3)) * w / 3 + (Math.Pow(x4, 2) - Math.Pow(x3, 2)) * w1 / 2);
                            m1 = -m1 / (x4 - x3);

                            m2 = (-1 / Math.Pow(Lele[i], 2)) * ((Math.Pow(x4, 5) - Math.Pow(x3, 5)) * w / 5 + (Math.Pow(x4, 4) - Math.Pow(x3, 4)) * w1 / 4);
                            m2 = m2 + (1 / Lele[i]) * ((Math.Pow(x4, 4) - Math.Pow(x3, 4)) * w / 4 + (Math.Pow(x4, 3) - Math.Pow(x3, 3)) * w1 / 3);
                            m2 = -m2 / (x4 - x3);

                            Globals.ELforceEX[i - 1, 1] = Globals.ELforceEX[i - 1, 1] + p1;//py',2
                            Globals.ELforceEX[i - 1, 7] = Globals.ELforceEX[i - 1, 7] + p2;
                            Globals.ELforceEX[i - 1, 5] = Globals.ELforceEX[i - 1, 5] + m1; //Mz',3
                            Globals.ELforceEX[i - 1, 11] = Globals.ELforceEX[i - 1, 11] + m2;


                            Globals.ELforceEXPD[i - 1, j, 1] = Globals.ELforceEXPD[i - 1, j, 1] + p1;
                            Globals.ELforceEXPD[i - 1, j, 7] = Globals.ELforceEXPD[i - 1, j, 7] + p2;
                            Globals.ELforceEXPD[i - 1, j, 5] = Globals.ELforceEXPD[i - 1, j, 5] + m1;
                            Globals.ELforceEXPD[i - 1, j, 11] = Globals.ELforceEXPD[i - 1, j, 11] + m2;
                        End30: { }

                        }
                        ////==========================

                        if (((MainForm)mainForm).FrameElement[i].LoadDType[j] == 2) //=======distributed moment about 2
                        {
                            if (pow != 0)  //====uniform moment
                            {
                                xx1 = (0 + x5) / 2 - Math.Sqrt(0.6) * (x5 - 0) / 2;
                                xx2 = (0 + x5) / 2;
                                xx3 = (0 + x5) / 2 + Math.Sqrt(0.6) * (x5 - 0) / 2;
                                ww1 = 5 * (x5 - 0) / 18;
                                ww2 = 8 * (x5 - 0) / 18;
                                ww3 = 5 * (x5 - 0) / 18;


                                p1 = (-6 * xx1 / Math.Pow(x5, 2) + 6 * Math.Pow(xx1, 2) / Math.Pow(x5, 3)) * ww1;
                                p1 = p1 + (-6 * xx2 / Math.Pow(x5, 2) + 6 * Math.Pow(xx2, 2) / Math.Pow(x5, 3)) * ww2;
                                p1 = p1 + (-6 * xx3 / Math.Pow(x5, 2) + 6 * Math.Pow(xx3, 2) / Math.Pow(x5, 3)) * ww3;
                                p1 = -p1 * pow;
                                p2 = -p1;

                                m1 = (1 - 4 * xx1 / x5 + 3 * Math.Pow(xx1, 2) / Math.Pow(x5, 2)) * ww1;
                                m1 = m1 + (1 - 4 * xx2 / x5 + 3 * Math.Pow(xx2, 2) / Math.Pow(x5, 2)) * ww2;
                                m1 = m1 + (1 - 4 * xx3 / x5 + 3 * Math.Pow(xx3, 2) / Math.Pow(x5, 2)) * ww3;
                                m1 = -m1 * pow;

                                m2 = (-2 * xx1 / x5 + 3 * Math.Pow(xx1, 2) / Math.Pow(x5, 2)) * ww1;
                                m2 = m2 + (-2 * xx2 / x5 + 3 * Math.Pow(xx2, 2) / Math.Pow(x5, 2)) * ww2;
                                m2 = m2 + (-2 * xx3 / x5 + 3 * Math.Pow(xx3, 2) / Math.Pow(x5, 2)) * ww3;
                                m2 = -m2 * pow;

                                Globals.ELforceEX[i - 1, 2] = Globals.ELforceEX[i - 1, 2] + p1;//p2
                                Globals.ELforceEX[i - 1, 8] = Globals.ELforceEX[i - 1, 8] + p2;
                                Globals.ELforceEX[i - 1, 4] = Globals.ELforceEX[i - 1, 4] + m1; //Mz',3
                                Globals.ELforceEX[i - 1, 10] = Globals.ELforceEX[i - 1, 10] + m2;

                                Globals.ELforceEXPD[i - 1, j, 2] = p1;
                                Globals.ELforceEXPD[i - 1, j, 8] = p2;
                                Globals.ELforceEXPD[i - 1, j, 4] = m1;
                                Globals.ELforceEXPD[i - 1, j, 10] = m2;
                            }

                            //==== first part from pow1 to pow2       
                            if ((x1 - x2) == 0)
                            {
                                goto End40;
                            }

                            w = pow2 - pow1;
                            w1 = pow1 * x2 - pow2 * x1;
                            xx1 = (x1 + x2) / 2 - Math.Sqrt(0.6) * (x2 - x1) / 2;
                            xx2 = (x1 + x2) / 2;
                            xx3 = (x1 + x2) / 2 + Math.Sqrt(0.6) * (x2 - x1) / 2;
                            ww1 = 5 * (x2 - x1) / 18;
                            ww2 = 8 * (x2 - x1) / 18;
                            ww3 = 5 * (x2 - x1) / 18;
                            p1 = (-6 * xx1 / Math.Pow(x5, 2) + 6 * Math.Pow(xx1, 2) / Math.Pow(x5, 3)) * ww1 * (xx1 * w + w1) / (x2 - x1);
                            p1 = p1 + (-6 * xx2 / Math.Pow(x5, 2) + 6 * Math.Pow(xx2, 2) / Math.Pow(x5, 3)) * ww2 * (xx2 * w + w1) / (x2 - x1);
                            p1 = p1 + (-6 * xx3 / Math.Pow(x5, 2) + 6 * Math.Pow(xx3, 2) / Math.Pow(x5, 3)) * ww3 * (xx3 * w + w1) / (x2 - x1);
                            p1 = -p1;
                            p2 = -p1;

                            m1 = (1 - 4 * xx1 / x5 + 3 * Math.Pow(xx1, 2) / Math.Pow(x5, 2)) * ww1 * (xx1 * w + w1) / (x2 - x1);
                            m1 = m1 + (1 - 4 * xx2 / x5 + 3 * Math.Pow(xx2, 2) / Math.Pow(x5, 2)) * ww2 * (xx2 * w + w1) / (x2 - x1);
                            m1 = m1 + (1 - 4 * xx3 / x5 + 3 * Math.Pow(xx3, 2) / Math.Pow(x5, 2)) * ww3 * (xx3 * w + w1) / (x2 - x1);


                            m2 = (-2 * xx1 / x5 + 3 * Math.Pow(xx1, 2) / Math.Pow(x5, 2)) * ww1 * (xx1 * w + w1) / (x2 - x1);
                            m2 = m2 + (-2 * xx2 / x5 + 3 * Math.Pow(xx2, 2) / Math.Pow(x5, 2)) * ww2 * (xx2 * w + w1) / (x2 - x1);
                            m2 = m2 + (-2 * xx3 / x5 + 3 * Math.Pow(xx3, 2) / Math.Pow(x5, 2)) * ww3 * (xx3 * w + w1) / (x2 - x1);

                            Globals.ELforceEX[i - 1, 2] = Globals.ELforceEX[i - 1, 2] + p1;//p2
                            Globals.ELforceEX[i - 1, 8] = Globals.ELforceEX[i - 1, 8] + p2;
                            Globals.ELforceEX[i - 1, 4] = Globals.ELforceEX[i - 1, 4] + m1; //Mz',3
                            Globals.ELforceEX[i - 1, 10] = Globals.ELforceEX[i - 1, 10] + m2;

                            Globals.ELforceEXPD[i - 1, j, 2] = Globals.ELforceEXPD[i - 1, j, 2] + p1;
                            Globals.ELforceEXPD[i - 1, j, 8] = Globals.ELforceEXPD[i - 1, j, 8] + p2;
                            Globals.ELforceEXPD[i - 1, j, 4] = Globals.ELforceEXPD[i - 1, j, 4] + m1;
                            Globals.ELforceEXPD[i - 1, j, 10] = Globals.ELforceEXPD[i - 1, j, 10] + m2;


                        End40: { }

                            if ((x3 - x2) == 0)
                            {
                                goto End50;
                            }

                            //==== second part from pow2 to pow3

                            w = pow3 - pow2;
                            w1 = pow2 * x3 - pow3 * x2;

                            xx1 = (x3 + x2) / 2 - Math.Sqrt(0.6) * (x3 - x2) / 2;
                            xx2 = (x3 + x2) / 2;
                            xx3 = (x3 + x2) / 2 + Math.Sqrt(0.6) * (x3 - x2) / 2;
                            ww1 = 5 * (x3 - x2) / 18;
                            ww2 = 8 * (x3 - x2) / 18;
                            ww3 = 5 * (x3 - x2) / 18;
                            p1 = (-6 * xx1 / Math.Pow(x5, 2) + 6 * Math.Pow(xx1, 2) / Math.Pow(x5, 3)) * ww1 * (xx1 * w + w1) / (x3 - x2);
                            p1 = p1 + (-6 * xx2 / Math.Pow(x5, 2) + 6 * Math.Pow(xx2, 2) / Math.Pow(x5, 3)) * ww2 * (xx2 * w + w1) / (x3 - x2);
                            p1 = p1 + (-6 * xx3 / Math.Pow(x5, 2) + 6 * Math.Pow(xx3, 2) / Math.Pow(x5, 3)) * ww3 * (xx3 * w + w1) / (x3 - x2);
                            p1 = -p1;
                            p2 = -p1;

                            m1 = (1 - 4 * xx1 / x5 + 3 * Math.Pow(xx1, 2) / Math.Pow(x5, 2)) * ww1 * (xx1 * w + w1) / (x3 - x2);
                            m1 = m1 + (1 - 4 * xx2 / x5 + 3 * Math.Pow(xx2, 2) / Math.Pow(x5, 2)) * ww2 * (xx2 * w + w1) / (x3 - x2);
                            m1 = m1 + (1 - 4 * xx3 / x5 + 3 * Math.Pow(xx3, 2) / Math.Pow(x5, 2)) * ww3 * (xx3 * w + w1) / (x3 - x2);


                            m2 = (-2 * xx1 / x5 + 3 * Math.Pow(xx1, 2) / Math.Pow(x5, 2)) * ww1 * (xx1 * w + w1) / (x3 - x2);
                            m2 = m2 + (-2 * xx2 / x5 + 3 * Math.Pow(xx2, 2) / Math.Pow(x5, 2)) * ww2 * (xx2 * w + w1) / (x3 - x2);
                            m2 = m2 + (-2 * xx3 / x5 + 3 * Math.Pow(xx3, 2) / Math.Pow(x5, 2)) * ww3 * (xx3 * w + w1) / (x3 - x2);

                            Globals.ELforceEX[i - 1, 2] = Globals.ELforceEX[i - 1, 2] + p1;//p2
                            Globals.ELforceEX[i - 1, 8] = Globals.ELforceEX[i - 1, 8] + p2;
                            Globals.ELforceEX[i - 1, 4] = Globals.ELforceEX[i - 1, 4] + m1; //Mz',3
                            Globals.ELforceEX[i - 1, 10] = Globals.ELforceEX[i - 1, 10] + m2;

                            Globals.ELforceEXPD[i - 1, j, 2] = Globals.ELforceEXPD[i - 1, j, 2] + p1;
                            Globals.ELforceEXPD[i - 1, j, 8] = Globals.ELforceEXPD[i - 1, j, 8] + p2;
                            Globals.ELforceEXPD[i - 1, j, 4] = Globals.ELforceEXPD[i - 1, j, 4] + m1;
                            Globals.ELforceEXPD[i - 1, j, 10] = Globals.ELforceEXPD[i - 1, j, 10] + m2;


                        End50: { }

                            if ((x4 - x3) == 0)
                            {
                                goto End60;
                            }
                            //==== third part from pow2 to pow3
                            w = pow4 - pow3;
                            w1 = pow3 * x4 - pow4 * x3;

                            xx1 = (x3 + x4) / 2 - Math.Sqrt(0.6) * (x4 - x3) / 2;
                            xx2 = (x3 + x4) / 2;
                            xx3 = (x3 + x4) / 2 + Math.Sqrt(0.6) * (x4 - x3) / 2;
                            ww1 = 5 * (x4 - x3) / 18;
                            ww2 = 8 * (x4 - x3) / 18;
                            ww3 = 5 * (x4 - x3) / 18;
                            p1 = (-6 * xx1 / Math.Pow(x5, 2) + 6 * Math.Pow(xx1, 2) / Math.Pow(x5, 3)) * ww1 * (xx1 * w + w1) / (x4 - x3);
                            p1 = p1 + (-6 * xx2 / Math.Pow(x5, 2) + 6 * Math.Pow(xx2, 2) / Math.Pow(x5, 3)) * ww2 * (xx2 * w + w1) / (x4 - x3);
                            p1 = p1 + (-6 * xx3 / Math.Pow(x5, 2) + 6 * Math.Pow(xx3, 2) / Math.Pow(x5, 3)) * ww3 * (xx3 * w + w1) / (x4 - x3);
                            p1 = -p1;
                            p2 = -p1;

                            m1 = (1 - 4 * xx1 / x5 + 3 * Math.Pow(xx1, 2) / Math.Pow(x5, 2)) * ww1 * (xx1 * w + w1) / (x4 - x3);
                            m1 = m1 + (1 - 4 * xx2 / x5 + 3 * Math.Pow(xx2, 2) / Math.Pow(x5, 2)) * ww2 * (xx2 * w + w1) / (x4 - x3);
                            m1 = m1 + (1 - 4 * xx3 / x5 + 3 * Math.Pow(xx3, 2) / Math.Pow(x5, 2)) * ww3 * (xx3 * w + w1) / (x4 - x3);

                            m2 = (-2 * xx1 / x5 + 3 * Math.Pow(xx1, 2) / Math.Pow(x5, 2)) * ww1 * (xx1 * w + w1) / (x4 - x3);
                            m2 = m2 + (-2 * xx2 / x5 + 3 * Math.Pow(xx2, 2) / Math.Pow(x5, 2)) * ww2 * (xx2 * w + w1) / (x4 - x3);
                            m2 = m2 + (-2 * xx3 / x5 + 3 * Math.Pow(xx3, 2) / Math.Pow(x5, 2)) * ww3 * (xx3 * w + w1) / (x4 - x3);

                            Globals.ELforceEX[i - 1, 2] = Globals.ELforceEX[i - 1, 2] + p1;//p2
                            Globals.ELforceEX[i - 1, 8] = Globals.ELforceEX[i - 1, 8] + p2;
                            Globals.ELforceEX[i - 1, 4] = Globals.ELforceEX[i - 1, 4] + m1; //Mz',3
                            Globals.ELforceEX[i - 1, 10] = Globals.ELforceEX[i - 1, 10] + m2;

                            Globals.ELforceEXPD[i - 1, j, 2] = Globals.ELforceEXPD[i - 1, j, 2] + p1;
                            Globals.ELforceEXPD[i - 1, j, 8] = Globals.ELforceEXPD[i - 1, j, 8] + p2;
                            Globals.ELforceEXPD[i - 1, j, 4] = Globals.ELforceEXPD[i - 1, j, 4] + m1;
                            Globals.ELforceEXPD[i - 1, j, 10] = Globals.ELforceEXPD[i - 1, j, 10] + m2;

                        End60: { }
                        }

                    }

                    /////////////////////////////////////////////////////////////////////////////////////////////////
                    if (LDir == 2) //=======distributed force or moment along 3 
                    {

                        if (((MainForm)mainForm).FrameElement[i].LoadDType[j] == 1) //=======distributed force along 3
                        {
                            if (pow != 0)  //====uniform load
                            {
                                p1 = pow * Lele[i] / 2;
                                p2 = p1;
                                m1 = -pow * Math.Pow(Lele[i], 2) / 12;
                                m2 = -m1;

                                Globals.ELforceEX[i - 1, 2] = Globals.ELforceEX[i - 1, 2] + p1;//p3
                                Globals.ELforceEX[i - 1, 8] = Globals.ELforceEX[i - 1, 8] + p2;
                                Globals.ELforceEX[i - 1, 4] = Globals.ELforceEX[i - 1, 4] + m1; //M2
                                Globals.ELforceEX[i - 1, 10] = Globals.ELforceEX[i - 1, 10] + m2;

                                Globals.ELforceEXPD[i - 1, j, 2] = p1;
                                Globals.ELforceEXPD[i - 1, j, 8] = p2;
                                Globals.ELforceEXPD[i - 1, j, 4] = m1;
                                Globals.ELforceEXPD[i - 1, j, 10] = m2;
                            }

                            //==== first part from pow1 to pow2       
                            if ((x1 - x2) == 0)
                            {
                                goto End70;
                            }

                            w = pow2 - pow1;
                            w1 = pow1 * x2 - pow2 * x1;

                            p1 = (-2 / Math.Pow(Lele[i], 3)) * ((Math.Pow(x2, 5) - Math.Pow(x1, 5)) * w / 5 + (Math.Pow(x2, 4) - Math.Pow(x1, 4)) * w1 / 4);
                            p1 = p1 + (3 / Math.Pow(Lele[i], 2)) * ((Math.Pow(x2, 4) - Math.Pow(x1, 4)) * w / 4 + (Math.Pow(x2, 3) - Math.Pow(x1, 3)) * w1 / 3);
                            p1 = p1 - ((Math.Pow(x2, 2) - Math.Pow(x1, 2)) * w / 2 + (x2 - x1) * w1);
                            p1 = -p1 / (x2 - x1);

                            p2 = (2 / Math.Pow(Lele[i], 3)) * ((Math.Pow(x2, 5) - Math.Pow(x1, 5)) * w / 5 + (Math.Pow(x2, 4) - Math.Pow(x1, 4)) * w1 / 4);
                            p2 = p2 + (-3 / Math.Pow(Lele[i], 2)) * ((Math.Pow(x2, 4) - Math.Pow(x1, 4)) * w / 4 + (Math.Pow(x2, 3) - Math.Pow(x1, 3)) * w1 / 3);
                            p2 = -p2 / (x2 - x1);

                            m1 = (-1 / Math.Pow(Lele[i], 2)) * ((Math.Pow(x2, 5) - Math.Pow(x1, 5)) * w / 5 + (Math.Pow(x2, 4) - Math.Pow(x1, 4)) * w1 / 4);
                            m1 = m1 + (2 / Lele[i]) * ((Math.Pow(x2, 4) - Math.Pow(x1, 4)) * w / 4 + (Math.Pow(x2, 3) - Math.Pow(x1, 3)) * w1 / 3);
                            m1 = m1 - ((Math.Pow(x2, 3) - Math.Pow(x1, 3)) * w / 3 + (Math.Pow(x2, 2) - Math.Pow(x1, 2)) * w1 / 2);
                            m1 = m1 / (x2 - x1);

                            m2 = (-1 / Math.Pow(Lele[i], 2)) * ((Math.Pow(x2, 5) - Math.Pow(x1, 5)) * w / 5 + (Math.Pow(x2, 4) - Math.Pow(x1, 4)) * w1 / 4);
                            m2 = m2 + (1 / Lele[i]) * ((Math.Pow(x2, 4) - Math.Pow(x1, 4)) * w / 4 + (Math.Pow(x2, 3) - Math.Pow(x1, 3)) * w1 / 3);
                            m2 = m2 / (x2 - x1);

                            Globals.ELforceEX[i - 1, 2] = Globals.ELforceEX[i - 1, 2] + p1;//p3
                            Globals.ELforceEX[i - 1, 8] = Globals.ELforceEX[i - 1, 8] + p2;
                            Globals.ELforceEX[i - 1, 4] = Globals.ELforceEX[i - 1, 4] + m1; //M2
                            Globals.ELforceEX[i - 1, 10] = Globals.ELforceEX[i - 1, 10] + m2;

                            Globals.ELforceEXPD[i - 1, j, 2] = Globals.ELforceEXPD[i - 1, j, 2] + p1;
                            Globals.ELforceEXPD[i - 1, j, 8] = Globals.ELforceEXPD[i - 1, j, 8] + p2;
                            Globals.ELforceEXPD[i - 1, j, 4] = Globals.ELforceEXPD[i - 1, j, 4] + m1;
                            Globals.ELforceEXPD[i - 1, j, 10] = Globals.ELforceEXPD[i - 1, j, 10] + m2;

                        End70: { }


                            if ((x3 - x2) == 0)
                            {
                                goto End80;
                            }

                            //==== second part from pow2 to pow3

                            w = pow3 - pow2;
                            w1 = pow2 * x3 - pow3 * x2;

                            p1 = (-2 / Math.Pow(Lele[i], 3)) * ((Math.Pow(x3, 5) - Math.Pow(x2, 5)) * w / 5 + (Math.Pow(x3, 4) - Math.Pow(x2, 4)) * w1 / 4);
                            p1 = p1 + (3 / Math.Pow(Lele[i], 2)) * ((Math.Pow(x3, 4) - Math.Pow(x2, 4)) * w / 4 + (Math.Pow(x3, 3) - Math.Pow(x2, 3)) * w1 / 3);
                            p1 = p1 - ((Math.Pow(x3, 2) - Math.Pow(x2, 2)) * w / 2 + (x3 - x2) * w1);
                            p1 = -p1 / (x3 - x2);

                            p2 = (2 / Math.Pow(Lele[i], 3)) * ((Math.Pow(x3, 5) - Math.Pow(x2, 5)) * w / 5 + (Math.Pow(x3, 4) - Math.Pow(x2, 4)) * w1 / 4);
                            p2 = p2 + (-3 / Math.Pow(Lele[i], 2)) * ((Math.Pow(x3, 4) - Math.Pow(x2, 4)) * w / 4 + (Math.Pow(x3, 3) - Math.Pow(x2, 3)) * w1 / 3);
                            p2 = -p2 / (x3 - x2);

                            m1 = (-1 / Math.Pow(Lele[i], 2)) * ((Math.Pow(x3, 5) - Math.Pow(x2, 5)) * w / 5 + (Math.Pow(x3, 4) - Math.Pow(x2, 4)) * w1 / 4);
                            m1 = m1 + (2 / Lele[i]) * ((Math.Pow(x3, 4) - Math.Pow(x2, 4)) * w / 4 + (Math.Pow(x3, 3) - Math.Pow(x2, 3)) * w1 / 3);
                            m1 = m1 - ((Math.Pow(x3, 3) - Math.Pow(x2, 3)) * w / 3 + (Math.Pow(x3, 2) - Math.Pow(x2, 2)) * w1 / 2);
                            m1 = m1 / (x3 - x2);

                            m2 = (-1 / Math.Pow(Lele[i], 2)) * ((Math.Pow(x3, 5) - Math.Pow(x2, 5)) * w / 5 + (Math.Pow(x3, 4) - Math.Pow(x2, 4)) * w1 / 4);
                            m2 = m2 + (1 / Lele[i]) * ((Math.Pow(x3, 4) - Math.Pow(x2, 4)) * w / 4 + (Math.Pow(x3, 3) - Math.Pow(x2, 3)) * w1 / 3);
                            m2 = m2 / (x3 - x2);

                            Globals.ELforceEX[i - 1, 2] = Globals.ELforceEX[i - 1, 2] + p1;//p3
                            Globals.ELforceEX[i - 1, 8] = Globals.ELforceEX[i - 1, 8] + p2;
                            Globals.ELforceEX[i - 1, 4] = Globals.ELforceEX[i - 1, 4] + m1; //M2
                            Globals.ELforceEX[i - 1, 10] = Globals.ELforceEX[i - 1, 10] + m2;

                            Globals.ELforceEXPD[i - 1, j, 2] = Globals.ELforceEXPD[i - 1, j, 2] + p1;
                            Globals.ELforceEXPD[i - 1, j, 8] = Globals.ELforceEXPD[i - 1, j, 8] + p2;
                            Globals.ELforceEXPD[i - 1, j, 4] = Globals.ELforceEXPD[i - 1, j, 4] + m1;
                            Globals.ELforceEXPD[i - 1, j, 10] = Globals.ELforceEXPD[i - 1, j, 10] + m2;


                        End80: { }

                            if ((x4 - x3) == 0)
                            {
                                goto End90;
                            }
                            //==== third part from pow2 to pow3
                            w = pow4 - pow3;
                            w1 = pow3 * x4 - pow4 * x3;

                            p1 = (-2 / Math.Pow(Lele[i], 3)) * ((Math.Pow(x4, 5) - Math.Pow(x3, 5)) * w / 5 + (Math.Pow(x4, 4) - Math.Pow(x3, 4)) * w1 / 4);
                            p1 = p1 + (3 / Math.Pow(Lele[i], 2)) * ((Math.Pow(x4, 4) - Math.Pow(x3, 4)) * w / 4 + (Math.Pow(x4, 3) - Math.Pow(x3, 3)) * w1 / 3);
                            p1 = p1 - ((Math.Pow(x4, 2) - Math.Pow(x3, 2)) * w / 2 + (x4 - x3) * w1);
                            p1 = -p1 / (x4 - x3);

                            p2 = (2 / Math.Pow(Lele[i], 3)) * ((Math.Pow(x4, 5) - Math.Pow(x3, 5)) * w / 5 + (Math.Pow(x4, 4) - Math.Pow(x3, 4)) * w1 / 4);
                            p2 = p2 + (-3 / Math.Pow(Lele[i], 2)) * ((Math.Pow(x4, 4) - Math.Pow(x3, 4)) * w / 4 + (Math.Pow(x4, 3) - Math.Pow(x3, 3)) * w1 / 3);
                            p2 = -p2 / (x4 - x3);

                            m1 = (-1 / Math.Pow(Lele[i], 2)) * ((Math.Pow(x4, 5) - Math.Pow(x3, 5)) * w / 5 + (Math.Pow(x4, 4) - Math.Pow(x3, 4)) * w1 / 4);
                            m1 = m1 + (2 / Lele[i]) * ((Math.Pow(x4, 4) - Math.Pow(x3, 4)) * w / 4 + (Math.Pow(x4, 3) - Math.Pow(x3, 3)) * w1 / 3);
                            m1 = m1 - ((Math.Pow(x4, 3) - Math.Pow(x3, 3)) * w / 3 + (Math.Pow(x4, 2) - Math.Pow(x3, 2)) * w1 / 2);
                            m1 = m1 / (x4 - x3);

                            m2 = (-1 / Math.Pow(Lele[i], 2)) * ((Math.Pow(x4, 5) - Math.Pow(x3, 5)) * w / 5 + (Math.Pow(x4, 4) - Math.Pow(x3, 4)) * w1 / 4);
                            m2 = m2 + (1 / Lele[i]) * ((Math.Pow(x4, 4) - Math.Pow(x3, 4)) * w / 4 + (Math.Pow(x4, 3) - Math.Pow(x3, 3)) * w1 / 3);
                            m2 = m2 / (x4 - x3);

                            Globals.ELforceEX[i - 1, 2] = Globals.ELforceEX[i - 1, 2] + p1;//p3
                            Globals.ELforceEX[i - 1, 8] = Globals.ELforceEX[i - 1, 8] + p2;
                            Globals.ELforceEX[i - 1, 4] = Globals.ELforceEX[i - 1, 4] + m1; //M2
                            Globals.ELforceEX[i - 1, 10] = Globals.ELforceEX[i - 1, 10] + m2;

                            Globals.ELforceEXPD[i - 1, j, 2] = Globals.ELforceEXPD[i - 1, j, 2] + p1;
                            Globals.ELforceEXPD[i - 1, j, 8] = Globals.ELforceEXPD[i - 1, j, 8] + p2;
                            Globals.ELforceEXPD[i - 1, j, 4] = Globals.ELforceEXPD[i - 1, j, 4] + m1;
                            Globals.ELforceEXPD[i - 1, j, 10] = Globals.ELforceEXPD[i - 1, j, 10] + m2;
                        End90: { }

                        }

                        /////=======================================================================================

                        if (((MainForm)mainForm).FrameElement[i].LoadDType[j] == 2) //=======distributed moment about 3
                        {
                            if (pow != 0)  //====uniform moment
                            {
                                xx1 = (0 + x5) / 2 - Math.Sqrt(0.6) * (x5 - 0) / 2;
                                xx2 = (0 + x5) / 2;
                                xx3 = (0 + x5) / 2 + Math.Sqrt(0.6) * (x5 - 0) / 2;
                                ww1 = 5 * (x5 - 0) / 18;
                                ww2 = 8 * (x5 - 0) / 18;
                                ww3 = 5 * (x5 - 0) / 18;

                                /* p1 = (-6 * xx1 / Math.Pow(x5, 2) + 6 * Math.Pow(xx1, 2) / Math.Pow(x5, 3)) * ww1;
                                 p1 = p1 + (-6 * xx2 / Math.Pow(x5, 2) + 6 * Math.Pow(xx2, 2) / Math.Pow(x5, 3)) * ww2;
                                 p1 = p1 + (-6 * xx3 / Math.Pow(x5, 2) + 6 * Math.Pow(xx3, 2) / Math.Pow(x5, 3)) * ww3;
                                 p1 = p1 * pow; 
                                 p2 = -p1;

                                 m1 = (1 - 4 * xx1 / x5 + 3 * Math.Pow(xx1, 2) / Math.Pow(x5, 2)) * ww1;
                                 m1 = m1 + (1 - 4 * xx2 / x5 + 3 * Math.Pow(xx2, 2) / Math.Pow(x5, 2)) * ww2;
                                 m1 = m1 + (1 - 4 * xx3 / x5 + 3 * Math.Pow(xx3, 2) / Math.Pow(x5, 2)) * ww3;
                                 m1 =m1 * pow;

                                 m2 = (-2 * xx1 / x5 + 3 * Math.Pow(xx1, 2) / Math.Pow(x5, 2)) * ww1;
                                 m2 = m2 + (-2 * xx2 /x5 + 3 * Math.Pow(xx2, 2) / Math.Pow(x5, 2)) * ww2;
                                 m2 = m2 + (-2 * xx3 / x5 + 3 * Math.Pow(xx3, 2) / Math.Pow(x5, 2)) * ww3;
                                 m2 =m2 * pow;
                                 */


                                p1 = (-6 * xx1 / Math.Pow(x5, 2) + 6 * Math.Pow(xx1, 2) / Math.Pow(x5, 3)) * ww1;
                                p1 = p1 + (-6 * xx2 / Math.Pow(x5, 2) + 6 * Math.Pow(xx2, 2) / Math.Pow(x5, 3)) * ww2;
                                p1 = p1 + (-6 * xx3 / Math.Pow(x5, 2) + 6 * Math.Pow(xx3, 2) / Math.Pow(x5, 3)) * ww3;
                                p1 = p1 * pow;
                                p2 = -p1;

                                m1 = (1 - 4 * xx1 / x5 + 3 * Math.Pow(xx1, 2) / Math.Pow(x5, 2)) * ww1;
                                m1 = m1 + (1 - 4 * xx2 / x5 + 3 * Math.Pow(xx2, 2) / Math.Pow(x5, 2)) * ww2;
                                m1 = m1 + (1 - 4 * xx3 / x5 + 3 * Math.Pow(xx3, 2) / Math.Pow(x5, 2)) * ww3;
                                m1 = -m1 * pow;

                                m2 = (-2 * xx1 / x5 + 3 * Math.Pow(xx1, 2) / Math.Pow(x5, 2)) * ww1;
                                m2 = m2 + (-2 * xx2 / x5 + 3 * Math.Pow(xx2, 2) / Math.Pow(x5, 2)) * ww2;
                                m2 = m2 + (-2 * xx3 / x5 + 3 * Math.Pow(xx3, 2) / Math.Pow(x5, 2)) * ww3;
                                m2 = -m2 * pow;


                                Globals.ELforceEX[i - 1, 1] = Globals.ELforceEX[i - 1, 1] + p1;//p2
                                Globals.ELforceEX[i - 1, 7] = Globals.ELforceEX[i - 1, 7] + p2;
                                Globals.ELforceEX[i - 1, 5] = Globals.ELforceEX[i - 1, 5] + m1; //Mz',3
                                Globals.ELforceEX[i - 1, 11] = Globals.ELforceEX[i - 1, 11] + m2;

                                Globals.ELforceEXPD[i - 1, j, 1] = p1;
                                Globals.ELforceEXPD[i - 1, j, 7] = p2;
                                Globals.ELforceEXPD[i - 1, j, 5] = m1;
                                Globals.ELforceEXPD[i - 1, j, 11] = m2;

                            }

                            //==== first part from pow1 to pow2       
                            if ((x1 - x2) == 0)
                            {
                                goto End100;
                            }

                            w = pow2 - pow1;
                            w1 = pow1 * x2 - pow2 * x1;
                            xx1 = (x1 + x2) / 2 - Math.Sqrt(0.6) * (x2 - x1) / 2;
                            xx2 = (x1 + x2) / 2;
                            xx3 = (x1 + x2) / 2 + Math.Sqrt(0.6) * (x2 - x1) / 2;
                            ww1 = 5 * (x2 - x1) / 18;
                            ww2 = 8 * (x2 - x1) / 18;
                            ww3 = 5 * (x2 - x1) / 18;
                            p1 = (-6 * xx1 / Math.Pow(x5, 2) + 6 * Math.Pow(xx1, 2) / Math.Pow(x5, 3)) * ww1 * (xx1 * w + w1) / (x2 - x1);
                            p1 = p1 + (-6 * xx2 / Math.Pow(x5, 2) + 6 * Math.Pow(xx2, 2) / Math.Pow(x5, 3)) * ww2 * (xx2 * w + w1) / (x2 - x1);
                            p1 = p1 + (-6 * xx3 / Math.Pow(x5, 2) + 6 * Math.Pow(xx3, 2) / Math.Pow(x5, 3)) * ww3 * (xx3 * w + w1) / (x2 - x1);
                            p2 = -p1;

                            m1 = (1 - 4 * xx1 / x5 + 3 * Math.Pow(xx1, 2) / Math.Pow(x5, 2)) * ww1 * (xx1 * w + w1) / (x2 - x1);
                            m1 = m1 + (1 - 4 * xx2 / x5 + 3 * Math.Pow(xx2, 2) / Math.Pow(x5, 2)) * ww2 * (xx2 * w + w1) / (x2 - x1);
                            m1 = m1 + (1 - 4 * xx3 / x5 + 3 * Math.Pow(xx3, 2) / Math.Pow(x5, 2)) * ww3 * (xx3 * w + w1) / (x2 - x1);



                            m2 = (-2 * xx1 / x5 + 3 * Math.Pow(xx1, 2) / Math.Pow(x5, 2)) * ww1 * (xx1 * w + w1) / (x2 - x1);
                            m2 = m2 + (-2 * xx2 / x5 + 3 * Math.Pow(xx2, 2) / Math.Pow(x5, 2)) * ww2 * (xx2 * w + w1) / (x2 - x1);
                            m2 = m2 + (-2 * xx3 / x5 + 3 * Math.Pow(xx3, 2) / Math.Pow(x5, 2)) * ww3 * (xx3 * w + w1) / (x2 - x1);

                            Globals.ELforceEX[i - 1, 1] = Globals.ELforceEX[i - 1, 1] + p1;//p2
                            Globals.ELforceEX[i - 1, 7] = Globals.ELforceEX[i - 1, 7] + p2;
                            Globals.ELforceEX[i - 1, 5] = Globals.ELforceEX[i - 1, 5] + m1; //Mz',3
                            Globals.ELforceEX[i - 1, 11] = Globals.ELforceEX[i - 1, 11] + m2;

                            Globals.ELforceEXPD[i - 1, j, 1] = Globals.ELforceEXPD[i - 1, j, 1] + p1;
                            Globals.ELforceEXPD[i - 1, j, 7] = Globals.ELforceEXPD[i - 1, j, 7] + p2;
                            Globals.ELforceEXPD[i - 1, j, 5] = Globals.ELforceEXPD[i - 1, j, 5] + m1;
                            Globals.ELforceEXPD[i - 1, j, 11] = Globals.ELforceEXPD[i - 1, j, 11] + m2;


                        End100: { }

                            if ((x3 - x2) == 0)
                            {
                                goto End110;
                            }

                            //==== second part from pow2 to pow3

                            w = pow3 - pow2;
                            w1 = pow2 * x3 - pow3 * x2;

                            xx1 = (x3 + x2) / 2 - Math.Sqrt(0.6) * (x3 - x2) / 2;
                            xx2 = (x3 + x2) / 2;
                            xx3 = (x3 + x2) / 2 + Math.Sqrt(0.6) * (x3 - x2) / 2;
                            ww1 = 5 * (x3 - x2) / 18;
                            ww2 = 8 * (x3 - x2) / 18;
                            ww3 = 5 * (x3 - x2) / 18;
                            p1 = (-6 * xx1 / Math.Pow(x5, 2) + 6 * Math.Pow(xx1, 2) / Math.Pow(x5, 3)) * ww1 * (xx1 * w + w1) / (x3 - x2);
                            p1 = p1 + (-6 * xx2 / Math.Pow(x5, 2) + 6 * Math.Pow(xx2, 2) / Math.Pow(x5, 3)) * ww2 * (xx2 * w + w1) / (x3 - x2);
                            p1 = p1 + (-6 * xx3 / Math.Pow(x5, 2) + 6 * Math.Pow(xx3, 2) / Math.Pow(x5, 3)) * ww3 * (xx3 * w + w1) / (x3 - x2);
                            p2 = -p1;

                            m1 = (1 - 4 * xx1 / x5 + 3 * Math.Pow(xx1, 2) / Math.Pow(x5, 2)) * ww1 * (xx1 * w + w1) / (x3 - x2);
                            m1 = m1 + (1 - 4 * xx2 / x5 + 3 * Math.Pow(xx2, 2) / Math.Pow(x5, 2)) * ww2 * (xx2 * w + w1) / (x3 - x2);
                            m1 = m1 + (1 - 4 * xx3 / x5 + 3 * Math.Pow(xx3, 2) / Math.Pow(x5, 2)) * ww3 * (xx3 * w + w1) / (x3 - x2);

                            m2 = (-2 * xx1 / x5 + 3 * Math.Pow(xx1, 2) / Math.Pow(x5, 2)) * ww1 * (xx1 * w + w1) / (x3 - x2);
                            m2 = m2 + (-2 * xx2 / x5 + 3 * Math.Pow(xx2, 2) / Math.Pow(x5, 2)) * ww2 * (xx2 * w + w1) / (x3 - x2);
                            m2 = m2 + (-2 * xx3 / x5 + 3 * Math.Pow(xx3, 2) / Math.Pow(x5, 2)) * ww3 * (xx3 * w + w1) / (x3 - x2);

                            Globals.ELforceEX[i - 1, 1] = Globals.ELforceEX[i - 1, 1] + p1;//p2
                            Globals.ELforceEX[i - 1, 7] = Globals.ELforceEX[i - 1, 7] + p2;
                            Globals.ELforceEX[i - 1, 5] = Globals.ELforceEX[i - 1, 5] + m1; //Mz',3
                            Globals.ELforceEX[i - 1, 11] = Globals.ELforceEX[i - 1, 11] + m2;

                            Globals.ELforceEXPD[i - 1, j, 1] = Globals.ELforceEXPD[i - 1, j, 1] + p1;
                            Globals.ELforceEXPD[i - 1, j, 7] = Globals.ELforceEXPD[i - 1, j, 7] + p2;
                            Globals.ELforceEXPD[i - 1, j, 5] = Globals.ELforceEXPD[i - 1, j, 5] + m1;
                            Globals.ELforceEXPD[i - 1, j, 11] = Globals.ELforceEXPD[i - 1, j, 11] + m2;


                        End110: { }

                            if ((x4 - x3) == 0)
                            {
                                goto End120;
                            }
                            //==== third part from pow2 to pow3
                            w = pow4 - pow3;
                            w1 = pow3 * x4 - pow4 * x3;

                            xx1 = (x3 + x4) / 2 - Math.Sqrt(0.6) * (x4 - x3) / 2;
                            xx2 = (x3 + x4) / 2;
                            xx3 = (x3 + x4) / 2 + Math.Sqrt(0.6) * (x4 - x3) / 2;
                            ww1 = 5 * (x4 - x3) / 18;
                            ww2 = 8 * (x4 - x3) / 18;
                            ww3 = 5 * (x4 - x3) / 18;
                            p1 = (-6 * xx1 / Math.Pow(x5, 2) + 6 * Math.Pow(xx1, 2) / Math.Pow(x5, 3)) * ww1 * (xx1 * w + w1) / (x4 - x3);
                            p1 = p1 + (-6 * xx2 / Math.Pow(x5, 2) + 6 * Math.Pow(xx2, 2) / Math.Pow(x5, 3)) * ww2 * (xx2 * w + w1) / (x4 - x3);
                            p1 = p1 + (-6 * xx3 / Math.Pow(x5, 2) + 6 * Math.Pow(xx3, 2) / Math.Pow(x5, 3)) * ww3 * (xx3 * w + w1) / (x4 - x3);
                            p2 = -p1;

                            m1 = (1 - 4 * xx1 / x5 + 3 * Math.Pow(xx1, 2) / Math.Pow(x5, 2)) * ww1 * (xx1 * w + w1) / (x4 - x3);
                            m1 = m1 + (1 - 4 * xx2 / x5 + 3 * Math.Pow(xx2, 2) / Math.Pow(x5, 2)) * ww2 * (xx2 * w + w1) / (x4 - x3);
                            m1 = m1 + (1 - 4 * xx3 / x5 + 3 * Math.Pow(xx3, 2) / Math.Pow(x5, 2)) * ww3 * (xx3 * w + w1) / (x4 - x3);

                            m2 = (-2 * xx1 / x5 + 3 * Math.Pow(xx1, 2) / Math.Pow(x5, 2)) * ww1 * (xx1 * w + w1) / (x4 - x3);
                            m2 = m2 + (-2 * xx2 / x5 + 3 * Math.Pow(xx2, 2) / Math.Pow(x5, 2)) * ww2 * (xx2 * w + w1) / (x4 - x3);
                            m2 = m2 + (-2 * xx3 / x5 + 3 * Math.Pow(xx3, 2) / Math.Pow(x5, 2)) * ww3 * (xx3 * w + w1) / (x4 - x3);

                            Globals.ELforceEX[i - 1, 1] = Globals.ELforceEX[i - 1, 1] + p1;//p2
                            Globals.ELforceEX[i - 1, 7] = Globals.ELforceEX[i - 1, 7] + p2;
                            Globals.ELforceEX[i - 1, 5] = Globals.ELforceEX[i - 1, 5] + m1; //Mz',3
                            Globals.ELforceEX[i - 1, 11] = Globals.ELforceEX[i - 1, 11] + m2;

                            Globals.ELforceEXPD[i - 1, j, 1] = Globals.ELforceEXPD[i - 1, j, 1] + p1;
                            Globals.ELforceEXPD[i - 1, j, 7] = Globals.ELforceEXPD[i - 1, j, 7] + p2;
                            Globals.ELforceEXPD[i - 1, j, 5] = Globals.ELforceEXPD[i - 1, j, 5] + m1;
                            Globals.ELforceEXPD[i - 1, j, 11] = Globals.ELforceEXPD[i - 1, j, 11] + m2;
                        End120: { }
                        }


                    }


                }
            }
        }
        private void calcaluteglobalequivalentNodalLoadforgeneralsolution()
        {
            //=====transfare equivalent Nodal Load from Local coordinate to  global coordinate
            Globals.EGforceEX = new double[Globals.ElementNumberALL, 12];  //==== equivalent Nodal Load in Global coordinate from all applied loads  on the element
            // Globals.EGforceEX[]= invers[T]*Globals.ELforceEX[]
            int num_rows = 12;
            int k = 0;
            int i = 0;
            int n1, n2;
            double[,] matrix = new double[num_rows, num_rows];
            double[,] b = new double[12, 12];//T-1
            double[,] a = new double[12, 1];//F' Local forces


            for (k = 0; k < Globals.ElementNumberALL; k++)
            {
                for (int row = 0; row < num_rows; row++)
                {
                    for (int col = 0; col < num_rows; col++)
                    {
                        matrix[row, col] = Globals.TFrameElementMatrix[k, row, col];
                    }
                }

                // Find the inverse.
                double[,] inverse = InvertMatrix(matrix);
                b = inverse;

                for (i = 0; i < 12; i++)
                {
                    a[i, 0] = Globals.ELforceEX[k, i];
                }

                MatrixMultiplication(12, 12, 12, 1, b, a);


                for (i = 0; i < 12; i++)
                {
                    Globals.EGforceEX[k, i] = c[i, 0];
                }

                n1 = Globals.ElementInode[k] - 1;
                n2 = Globals.Elementjnode[k] - 1;

                for (i = 0; i < 6; i++)
                {
                    Globals.PNode[i, n1] = Globals.PNode[i, n1] + Globals.EGforceEX[k, i];
                }
                for (i = 6; i < 12; i++)
                {
                    Globals.PNode[i - 6, n2] = Globals.PNode[i - 6, n2] + Globals.EGforceEX[k, i];
                }
            }
        }
        private void calculateNodalGlobalDisplacement()
        {
            if (Globals.NEq == 0)
            {
                goto End40;
            }

            //===============Load vector
            int i, j;
            Globals.RLoad = new double[Globals.NEq];
            for (j = 0; j < 6; j++)
            {
                for (i = 0; i < Globals.NodeNumberALL; i++)
                {

                    if (Globals.IDModifiedMatrix[j, i] > 0)
                    {
                        Globals.RLoad[Globals.IDModifiedMatrix[j, i] - 1] = Globals.PNode[j, i];
                    }

                }
            }

            //====modify load vector:invert R to V

            Globals.VLoad = new double[Globals.NEq];
            for (i = 0; i < Globals.NEq; i++)
            {
                Globals.VLoad[i] = 0;
            }
            double sum = 0;
            Globals.VLoad[0] = Globals.RLoad[0];
            for (i = 1; i < Globals.NEq; i++)
            {
                sum = 0;
                for (int rmm = Globals.mjmatrix[i]; rmm < i; rmm++)
                {
                    sum = sum + Globals.Smvector[Globals.MAXA[i] - (Globals.ColH[i] - rmm) - Globals.mjmatrix[i] + 1] * Globals.VLoad[rmm];
                }
                Globals.VLoad[i] = Globals.RLoad[i] - sum;
            }

            //===============calculate nodal Displacement <> 0

            Globals.UaxialDisp = new double[Globals.NEq];

            double[] vv;
            vv = new double[Globals.NEq];
            sum = 0;

            Globals.UaxialDisp[Globals.NEq - 1] = Globals.VLoad[Globals.NEq - 1] / Globals.Smvector[Globals.Nwk - 1];
            if (Globals.NEq == 1)
            {
                goto End40;
            }

            for (i = 0; i < Globals.NEq; i++)
            {
                vv[i] = Globals.VLoad[i] / Globals.Smvector[Globals.MAXA[i]];
            }

            sum = 0;
            for (int rmm = Globals.mjmatrix[Globals.NEq - 1]; rmm < Globals.NEq - 1; rmm++)
            {
                sum = 0;
                sum = Globals.UaxialDisp[Globals.NEq - 1] * Globals.Smvector[Globals.MAXA[Globals.NEq - 1] - (Globals.ColH[Globals.NEq - 1] - rmm) - Globals.mjmatrix[Globals.NEq - 1] + 1];
                vv[rmm] = vv[rmm] - sum;
            }

            Globals.UaxialDisp[Globals.NEq - 2] = vv[Globals.NEq - 2];

            for (i = Globals.NEq - 2; i > 0; i--)
            {
                sum = 0;
                for (int rmm = Globals.mjmatrix[i]; rmm < i; rmm++)
                {
                    sum = 0;
                    sum = Globals.UaxialDisp[i] * Globals.Smvector[Globals.MAXA[i] - (Globals.ColH[i] - rmm) - Globals.mjmatrix[i] + 1];
                    vv[rmm] = vv[rmm] - sum;
                }

                Globals.UaxialDisp[i - 1] = vv[i - 1];
            }

        End40:
            { }

            //===============calculate nodal Displacements including zero displacements (ALL Disp)

            Globals.UDispAll = new double[6, Globals.NodeNumberALL];
            int m1 = 0;
            for (i = 0; i < Globals.NodeNumberALL; i++)
            {
                for (j = 0; j < 6; j++)
                {

                    if (Globals.IDMatrix[j, i] == 1)
                    {
                        Globals.UDispAll[j, i] = 0;
                    }
                    else
                    {
                        Globals.UDispAll[j, i] = Globals.UaxialDisp[m1];
                        m1 = m1 + 1;
                    }


                }
            }
        }
        private void calculateelementNodalLocalforces()
        {
            //====================for each element calculate nodal global nodal forces forvce=K*U (EGforce)
            int k, i, j = 0;
            int n1, n2 = 0;
            double d1 = 0, d2 = 0, sum1 = 0, sum2 = 0;
            Globals.EGforce = new double[Globals.ElementNumberALL, 12];  //==element nodal force in Global Coor (Globals.EGforce= Kele*Nodal Disp)
            Globals.ELforce = new double[Globals.ElementNumberALL, 12];  //==element nodal force in Local Coor (Globals.ELforce=[T]*Globals.EGforce)
            Globals.elementd = new double[Globals.ElementNumberALL]; // total exial displacement of truss element

            double[,] TT = new double[12, 12];
            double[,] GFNF = new double[12, 1];

            for (k = 0; k < Globals.ElementNumberALL; k++)
            {
                n1 = Globals.ElementInode[k] - 1;
                n2 = Globals.Elementjnode[k] - 1;
                if (Globals.ElementType[k] == 0) //====Truss element
                {
                    ////===============calculate axial load

                    d1 = Globals.UDispAll[0, n1] * Globals.ElementN1[k] + Globals.UDispAll[1, n1] * Globals.ElementN2[k] + Globals.UDispAll[2, n1] * Globals.ElementN3[k];
                    d2 = Globals.UDispAll[0, n2] * Globals.ElementN1[k] + Globals.UDispAll[1, n2] * Globals.ElementN2[k] + Globals.UDispAll[2, n2] * Globals.ElementN3[k];
                    Globals.elementd[k] = d2 - d1;
                    Globals.EGforce[k, 0] = Globals.elementd[k] * Globals.ElementA[k] * Globals.ElementE[k] / Globals.ElementL[k];
                    Globals.EGforce[k, 6] = Globals.EGforce[k, 0];
                    for (i = 1; i < 6; i++)
                    {
                        Globals.EGforce[k, i] = 0;
                    }
                    for (i = 7; i < 12; i++)
                    {
                        Globals.EGforce[k, i] = 0;
                    }
                }


                if (Globals.ElementType[k] == 1) //====Frame element
                {
                    for (i = 0; i < 12; i++)
                    {
                        sum1 = 0;
                        sum2 = 0;
                        for (j = 0; j < 6; j++)
                        {
                            sum1 = sum1 + Globals.KLineElementMatrix[k, i, j] * Globals.UDispAll[j, n1];
                            sum1 = sum1 + Globals.KLineElementMatrix[k, i, j + 6] * Globals.UDispAll[j, n2];
                        }
                        Globals.EGforce[k, i] = sum1;

                    }

                    //===for each element====Local nodal forces  from (nodal displacement + external global loads)===final elemant nodal Local forces
                    for (i = 0; i < 12; i++)
                    {
                        for (j = 0; j < 12; j++)
                        {
                            TT[i, j] = Globals.TFrameElementMatrix[k, i, j];

                        }
                    }

                    for (i = 0; i < 12; i++)
                    {
                        GFNF[i, 0] = Globals.EGforce[k, i];
                    }

                    MatrixMultiplication(12, 12, 12, 1, TT, GFNF);

                    for (i = 0; i < 12; i++)
                    {
                        Globals.ELforce[k, i] = c[i, 0];
                    }

                }

            }

        }

        private void MatrixMultiplication(int row1, int col1, int row2, int col2, double[,] a, double[,] b)
        {
            int i, j;
            // ("Matrix Multiplication is :");
            c = new double[row1, col2];
            for (i = 0; i < row1; i++)
            {
                for (j = 0; j < col2; j++)
                {
                    c[i, j] = 0;
                    for (int k = 0; k < col1; k++)
                    {
                        c[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            return;
        }
        private double[,] InvertMatrix(double[,] matrix)
        {
            //const double tiny = 0.0000000001;
            //const double tiny = 0.0000000000001;
            const double tiny = 0.000000000000000000001;
            // Build the augmented matrix.
            int num_rows = matrix.GetUpperBound(0) + 1;
            double[,] augmented = new double[num_rows, 2 * num_rows];
            for (int row = 0; row < num_rows; row++)
            {
                for (int col = 0; col < num_rows; col++)
                    augmented[row, col] = matrix[row, col];
                augmented[row, row + num_rows] = 1;
            }
            // num_cols is the number of the augmented matrix.
            int num_cols = 2 * num_rows;
            // Solve.
            for (int row = 0; row < num_rows; row++)
            {
                // Zero out all entries in column r after this row.
                // See if this row has a non-zero entry in column r.
                if (Math.Abs(augmented[row, row]) < tiny)
                {
                    // Too close to zero. Try to swap with a later row.
                    for (int r2 = row + 1; r2 < num_rows; r2++)
                    {
                        if (Math.Abs(augmented[r2, row]) > tiny)
                        {
                            // This row will work. Swap them.
                            for (int c = 0; c < num_cols; c++)
                            {
                                double tmp = augmented[row, c];
                                augmented[row, c] = augmented[r2, c];
                                augmented[r2, c] = tmp;
                            }
                            break;
                        }
                    }
                }
                // If this row has a non-zero entry in column r, use it.
                if (Math.Abs(augmented[row, row]) > tiny)
                {
                    // Divide the row by augmented[row, row] to make this entry 1.
                    for (int col = 0; col < num_cols; col++)
                        if (col != row)
                            augmented[row, col] /= augmented[row, row];
                    augmented[row, row] = 1;
                    // Subtract this row from the other rows.
                    for (int row2 = 0; row2 < num_rows; row2++)
                    {
                        if (row2 != row)
                        {
                            double factor = augmented[row2, row] / augmented[row, row];
                            for (int col = 0; col < num_cols; col++)
                                augmented[row2, col] -= factor * augmented[row, col];
                        }
                    }
                }
            }
            // See if we have a solution.
            if (augmented[num_rows - 1, num_rows - 1] == 0) return null;
            // Extract the inverse array.
            double[,] inverse = new double[num_rows, num_rows];
            for (int row = 0; row < num_rows; row++)
            {
                for (int col = 0; col < num_rows; col++)
                {
                    inverse[row, col] = augmented[row, col + num_rows];
                }
            }
            return inverse;
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
        public void AnalysisResults()
        {
            double Xdis = 0;  // x section reletive distance 
            double[,] Xdis1 = new double[Globals.ElementNumberALL, 50]; // load distanse reletive
            double[] Xdis2 = new double[Globals.ElementNumberALL]; //  x section  Absolute distance
            double[,] eleS220 = new double[Globals.ElementNumberALL, 50];
            double[,] eleM33 = new double[Globals.ElementNumberALL, 50];
            double[,] eleS22 = new double[Globals.ElementNumberALL, 50];
            double P22 = 0;
            double MP22 = 0;
            double[,] eleS330 = new double[Globals.ElementNumberALL, 50];
            double[,] eleM22 = new double[Globals.ElementNumberALL, 50];
            double[,] eleS33 = new double[Globals.ElementNumberALL, 50];
            double P33 = 0;
            double MP33 = 0;
            double P11 = 0;
            double[,] eleaxialP0 = new double[Globals.ElementNumberALL, 50];
            double[,] eleaxialP = new double[Globals.ElementNumberALL, 50];
            double M11 = 0;
            double[,] eleM110 = new double[Globals.ElementNumberALL, 50];
            double[,] eleM11 = new double[Globals.ElementNumberALL, 50];
            double M22 = 0;
            double M33 = 0;
            double eleS22Px = 0;
            double eleS33Px = 0;
            double eleM22Px = 0;
            double eleM33Px = 0;
            double eleaxialPx = 0;  //==axial force 
            double eleM11Mx = 0;  //===Torsional Moment
            double eleS22x = 0;
            double eleS33x = 0;
            double eleM22x = 0;
            double eleM33x = 0;
            double eleaxialx = 0;
            double eleM11x = 0;
            //=======for distributed forces
            double x1 = 0;
            double x2 = 0;
            double x3 = 0;
            double x4 = 0;
            double x5 = 0;
            double y1 = 0;
            double dist1 = 0;
            double dist2 = 0;
            double dist3 = 0;
            double dist4 = 0;
            double pow = 0;
            double pow1 = 0;
            double pow2 = 0;
            double pow3 = 0;
            double pow4 = 0;
            double[,] eleDS220 = new double[Globals.ElementNumberALL, 10];
            double[,] eleDM33 = new double[Globals.ElementNumberALL, 10];
            double[,] eleDS22 = new double[Globals.ElementNumberALL, 10];
            double[,] eleDS330 = new double[Globals.ElementNumberALL, 10];
            double[,] eleDM22 = new double[Globals.ElementNumberALL, 10];
            double[,] eleDS33 = new double[Globals.ElementNumberALL, 10];
            double[,] eleaxialDP = new double[Globals.ElementNumberALL, 10];
            double[,] eleDM11 = new double[Globals.ElementNumberALL, 10];
            double eleDS22qx = 0;
            double eleDS33qx = 0;
            double eleDM22qx = 0;
            double eleDM33qx = 0;
            double eleDaxialqx = 0;  //==axial force 
            double eleDM11Mqx = 0;  //===Torsional Moment
            //====================
            //======for displacement 
            double U2 = 0;
            double U3 = 0;
            double Length = 0;
            int sh = 0;
            for (int k = 0; k < Globals.ElementNumberALL; k++)
            {
                double X1 = Joint.XReal[((MainForm)mainForm).FrameElement[k + 1].FirstJoint];
                double Y1 = Joint.YReal[((MainForm)mainForm).FrameElement[k + 1].FirstJoint];
                double Z1 = Joint.ZReal[((MainForm)mainForm).FrameElement[k + 1].FirstJoint];
                double X2 = Joint.XReal[((MainForm)mainForm).FrameElement[k + 1].SecondJoint];
                double Y2 = Joint.YReal[((MainForm)mainForm).FrameElement[k + 1].SecondJoint];
                double Z2 = Joint.ZReal[((MainForm)mainForm).FrameElement[k + 1].SecondJoint];
                Length = Math.Sqrt(Math.Pow(X1 - X2, 2) + Math.Pow(Y1 - Y2, 2) + Math.Pow(Z1 - Z2, 2));
                double plus =Math.Round (  Length / Frame.AnalisesSecNumbers,4);
                for (sh = 0; sh < Frame.AnalisesSecNumbers + 1; sh++)
                {
                    Xdis = sh * plus / Length;
                    if (sh == Frame.AnalisesSecNumbers) Xdis = 1;
                    eleS22Px = 0;
                    eleS33Px = 0;
                    eleM22Px = 0;
                    eleM33Px = 0;
                    eleaxialPx = 0;
                    eleM11Mx = 0;
                    x5 = Globals.ElementL[k];
                    Xdis2[k] = Xdis * x5;
                    ///=================================================point forces
                    if (((MainForm)mainForm).FrameElement[k + 1].LoadPNumber >= 1)
                    {
                        for (int i = 1; i < ((MainForm)mainForm).FrameElement[k + 1].LoadPNumber + 1; i++)  // ==== calculate shear and moment at x=0
                        {
                            P11 = 0;
                            M11 = 0;
                            M22 = 0;
                            M33 = 0;
                            P22 = 0;
                            MP22 = 0;
                            P33 = 0;
                            MP33 = 0;
                            Xdis1[k, i] = ((MainForm)mainForm).FrameElement[k+1].LoadPDistance[i] * Globals.ElementL[k];
                            if (Xdis2[k] > Xdis1[k, i])
                            {
                                if (((MainForm)mainForm).FrameElement[k+1].LoadPDirection[i] == 0 && ((MainForm)mainForm).FrameElement[k + 1].LoadPType[i]== 1) //===point force along 1 axis 
                                {
                                    P11 = ((MainForm)mainForm).FrameElement[k+1].LoadPValue[i];

                                }
                                if (((MainForm)mainForm).FrameElement[k+1].LoadPDirection[i] == 0 && ((MainForm)mainForm).FrameElement[k + 1].LoadPType[i]== 2) //=== point moment about 1 axis
                                {
                                    M11 = ((MainForm)mainForm).FrameElement[k+1].LoadPValue[i];

                                }
                                if (((MainForm)mainForm).FrameElement[k+1].LoadPDirection[i] == 1 && ((MainForm)mainForm).FrameElement[k + 1].LoadPType[i]== 1)  //===point force along 2 axis 
                                {
                                    P22 = ((MainForm)mainForm).FrameElement[k+1].LoadPValue[i];
                                    MP22 = ((MainForm)mainForm).FrameElement[k+1].LoadPValue[i]* (Xdis2[k] - Xdis1[k, i]);
                                }

                                if (((MainForm)mainForm).FrameElement[k+1].LoadPDirection[i] == 1 && ((MainForm)mainForm).FrameElement[k + 1].LoadPType[i]== 2)  //=== point moment about Y',2 axis
                                {
                                    M22 = ((MainForm)mainForm).FrameElement[k+1].LoadPValue[i];
                                }
                                if (((MainForm)mainForm).FrameElement[k+1].LoadPDirection[i] == 2 && ((MainForm)mainForm).FrameElement[k + 1].LoadPType[i]== 1)  //===point force along 3 axis 
                                {
                                    P33 = ((MainForm)mainForm).FrameElement[k+1].LoadPValue[i];
                                    MP33 = ((MainForm)mainForm).FrameElement[k+1].LoadPValue[i]* (Xdis2[k] - Xdis1[k, i]);
                                }
                                if (((MainForm)mainForm).FrameElement[k+1].LoadPDirection[i] == 2 && ((MainForm)mainForm).FrameElement[k + 1].LoadPType[i]== 2)  //===point moment about Z',3 axis
                                {
                                    M33 = ((MainForm)mainForm).FrameElement[k+1].LoadPValue[i];
                                }
                            }
                            eleM110[k, i] = Globals.ELforceEXP[k, i, 3];
                            eleM11[k, i] = Globals.ELforceEXP[k, i, 3] - M11;
                            eleaxialP0[k, i] = Globals.ELforceEXP[k, i, 0];
                            eleaxialP[k, i] = Globals.ELforceEXP[k, i, 0] - P11;
                            eleS220[k, i] = Globals.ELforceEXP[k, i, 1];
                            eleS22[k, i] = Globals.ELforceEXP[k, i, 1] - P22;
                            eleM33[k, i] = Globals.ELforceEXP[k, i, 5] - eleS220[k, i] * Xdis2[k] + MP22 - M33;
                            eleS330[k, i] = Globals.ELforceEXP[k, i, 2];
                            eleS33[k, i] = Globals.ELforceEXP[k, i, 2] - P33;
                            eleM22[k, i] = Globals.ELforceEXP[k, i, 4] + eleS330[k, i] * Xdis2[k] - MP33 - M22;
                        }
                        for (int i = 1; i < ((MainForm)mainForm).FrameElement[k + 1].LoadPNumber + 1; i++)  // ==== calculate shear and moment at x=0 from all external point Loads
                        {
                            eleM11Mx = eleM11Mx + eleM11[k, i];
                            eleaxialPx = eleaxialPx + eleaxialP[k, i];
                            eleS22Px = eleS22Px + eleS22[k, i];
                            eleS33Px = eleS33Px + eleS33[k, i];
                            eleM22Px = eleM22Px + eleM22[k, i];
                            eleM33Px = eleM33Px + eleM33[k, i];
                        }
                    }
                    //============================================================distributed forces
                    eleDS22qx = 0;
                    eleDM33qx = 0;
                    eleDS33qx = 0;
                    eleDM22qx = 0;
                    eleDaxialqx = 0;
                    eleDM11Mqx = 0;
                    if (((MainForm)mainForm).FrameElement[k + 1].LoadDNumber >= 1)
                    {
                        for (int i = 1; i < ((MainForm)mainForm).FrameElement[k + 1].LoadDNumber + 1; i++)  // ==== 
                        {
                            P11 = 0;
                            M11 = 0;
                            M22 = 0;
                            M33 = 0;
                            P22 = 0;
                            MP22 = 0;
                            P33 = 0;
                            MP33 = 0;

                            dist1 = ((MainForm)mainForm).FrameElement[k+1].LoadDDistance1[i];
                            dist2 = ((MainForm)mainForm).FrameElement[k+1].LoadDDistance2[i];
                            dist3 = ((MainForm)mainForm).FrameElement[k+1].LoadDDistance3[i];
                            dist4 = ((MainForm)mainForm).FrameElement[k+1].LoadDDistance4[i];
                            x1 = dist1 * x5;
                            x2 = dist2 * x5;
                            x3 = dist3 * x5;
                            x4 = dist4 * x5;
                            pow1 = ((MainForm)mainForm).FrameElement[k+1].LoadDValue1[i];
                            pow2 = ((MainForm)mainForm).FrameElement[k+1].LoadDValue2[i];
                            pow3 = ((MainForm)mainForm).FrameElement[k+1].LoadDValue3[i];
                            pow4 = ((MainForm)mainForm).FrameElement[k+1].LoadDValue4[i];
                            pow = ((MainForm)mainForm).FrameElement[k+1].LoadDUniform[i];

                            if (((MainForm)mainForm).FrameElement[k+1].LoadDDirection[i] == 0 && ((MainForm)mainForm).FrameElement[k+1].LoadDType[i] == 1) //===distributed force along 1 axis 
                            {
                                P11 = pow * Xdis2[k];
                                if ((x1 - x2) == 0)
                                {
                                    goto End110;
                                }
                                if (x1 <= Xdis2[k] && Xdis2[k] <= x2)
                                {
                                    y1 = pow1 + Xdis2[k] * (pow2 - pow1) / (x2 - x1);
                                    P11 = P11 + (pow1 + y1) * Xdis2[k] / 2;
                                }
                                if (Xdis2[k] > x2 && (x3 - x2) == 0 && (x4 - x3) == 0)
                                {
                                    y1 = pow2;
                                    P11 = P11 + (pow1 + y1) * (x2 - x1) / 2;
                                }
                            End110: { }
                                if ((x3 - x2) == 0)
                                {
                                    goto End120;
                                }
                                if (x2 <= Xdis2[k] && Xdis2[k] <= x3)
                                {
                                    y1 = pow2 + (Xdis2[k] - x2) * (pow3 - pow2) / (x3 - x2);
                                    P11 = P11 + (pow1 + pow2) * (x2 - x1) / 2 + (pow2 + y1) * (Xdis2[k] - x2) / 2;
                                }
                                if (Xdis2[k] > x3 && (x4 - x3) == 0)
                                {
                                    y1 = pow3;
                                    P11 = P11 + (pow1 + pow2) * (x2 - x1) / 2 + (pow2 + y1) * (x3 - x2) / 2;
                                }
                            End120: { }

                                if ((x4 - x3) == 0)
                                {
                                    goto End130;
                                }
                                if (x3 <= Xdis2[k] && Xdis2[k] <= x4)
                                {
                                    y1 = pow3 + (Xdis2[k] - x3) * (pow4 - pow3) / (x4 - x3);
                                    P11 = P11 + (pow1 + pow2) * (x2 - x1) / 2 + (pow2 + pow3) * (x3 - x2) / 2 + (pow3 + y1) * (Xdis2[k] - x3) / 2;

                                }
                                if (Xdis2[k] > x4)
                                {
                                    y1 = pow4;
                                    P11 = P11 + (pow1 + pow2) * (x2 - x1) / 2 + (pow2 + pow3) * (x3 - x2) / 2 + (pow3 + y1) * (x4 - x3) / 2;
                                }
                            End130: { }
                            }
                            if (((MainForm)mainForm).FrameElement[k+1].LoadDDirection[i] == 0 && ((MainForm)mainForm).FrameElement[k+1].LoadDType[i] == 2) //=== distributed moment about 1 axis
                            {
                                M11 = pow * Xdis2[k];
                                if ((x1 - x2) == 0)
                                {
                                    goto End140;
                                }
                                if (x1 <= Xdis2[k] && Xdis2[k] <= x2)
                                {
                                    y1 = pow1 + Xdis2[k] * (pow2 - pow1) / (x2 - x1);
                                    M11 = M11 + (pow1 + y1) * Xdis2[k] / 2;
                                }
                                if (Xdis2[k] > x2 && (x3 - x2) == 0 && (x4 - x3) == 0)
                                {
                                    y1 = pow2;
                                    M11 = M11 + (pow1 + y1) * (x2 - x1) / 2;
                                }
                            End140: { }
                                if ((x3 - x2) == 0)
                                {
                                    goto End150;
                                }
                                if (x2 <= Xdis2[k] && Xdis2[k] <= x3)
                                {
                                    y1 = pow2 + (Xdis2[k] - x2) * (pow3 - pow2) / (x3 - x2);
                                    M11 = M11 + (pow1 + pow2) * (x2 - x1) / 2 + (pow2 + y1) * (Xdis2[k] - x2) / 2;
                                }
                                if (Xdis2[k] > x3 && (x4 - x3) == 0)
                                {
                                    y1 = pow3;
                                    M11 = M11 + (pow1 + pow2) * (x2 - x1) / 2 + (pow2 + y1) * (x3 - x2) / 2;
                                }
                            End150: { }
                                if ((x4 - x3) == 0)
                                {
                                    goto End160;
                                }
                                if (x3 <= Xdis2[k] && Xdis2[k] <= x4)
                                {
                                    y1 = pow3 + (Xdis2[k] - x3) * (pow4 - pow3) / (x4 - x3);
                                    M11 = M11 + (pow1 + pow2) * (x2 - x1) / 2 + (pow2 + pow3) * (x3 - x2) / 2 + (pow3 + y1) * (Xdis2[k] - x3) / 2;
                                }
                                if (Xdis2[k] > x4)
                                {
                                    y1 = pow4;
                                    M11 = M11 + (pow1 + pow2) * (x2 - x1) / 2 + (pow2 + pow3) * (x3 - x2) / 2 + (pow3 + y1) * (x4 - x3) / 2;
                                }
                            End160: { }
                            }
                            if (((MainForm)mainForm).FrameElement[k+1].LoadDDirection[i] == 1 && ((MainForm)mainForm).FrameElement[k+1].LoadDType[i] == 1)  //===distributed force along 2 axis 
                            {
                                P22 = pow * Xdis2[k];
                                MP22 = pow * Xdis2[k] * Xdis2[k] / 2;
                                if ((x1 - x2) == 0)
                                {
                                    goto End10;
                                }
                                if (x1 <= Xdis2[k] && Xdis2[k] <= x2)
                                {
                                    y1 = pow1 + Xdis2[k] * (pow2 - pow1) / (x2 - x1);
                                    P22 = P22 + (pow1 + y1) * Xdis2[k] / 2;
                                    MP22 = MP22 + pow1 * Xdis2[k] * Xdis2[k] / 2 + (y1 - pow1) * Xdis2[k] * Xdis2[k] / 6;
                                }
                                if (Xdis2[k] > x2 && (x3 - x2) == 0 && (x4 - x3) == 0)
                                {
                                    y1 = pow2;
                                    P22 = P22 + (pow1 + y1) * (x2 - x1) / 2;
                                    MP22 = MP22 + pow1 * (x2 - x1) * (Xdis2[k] - (x2 - x1) / 2) + (y1 - pow1) * (x2 - x1) * (Xdis2[k] - 2 * (x2 - x1) / 3) / 2;
                                }
                            End10: { }
                                if ((x3 - x2) == 0)
                                {
                                    goto End20;
                                }
                                if (x2 <= Xdis2[k] && Xdis2[k] <= x3)
                                {
                                    y1 = pow2 + (Xdis2[k] - x2) * (pow3 - pow2) / (x3 - x2);
                                    P22 = P22 + (pow1 + pow2) * (x2 - x1) / 2 + (pow2 + y1) * (Xdis2[k] - x2) / 2;
                                    MP22 = MP22 + pow1 * (x2 - x1) * (Xdis2[k] - (x2 - x1) / 2) + (pow2 - pow1) * (x2 - x1) * (Xdis2[k] - 2 * (x2 - x1) / 3) / 2 + pow2 * (Xdis2[k] - x2) * (Xdis2[k] - x2) / 2 + (y1 - pow2) * (Xdis2[k] - x2) * (Xdis2[k] - x2) / 6;
                                }
                                if (Xdis2[k] > x3 && (x4 - x3) == 0)
                                {
                                    y1 = pow3;
                                    P22 = P22 + (pow1 + pow2) * (x2 - x1) / 2 + (pow2 + y1) * (x3 - x2) / 2;
                                    MP22 = MP22 + pow1 * (x2 - x1) * (Xdis2[k] - (x2 - x1) / 2) + (pow2 - pow1) * (x2 - x1) * (Xdis2[k] - 2 * (x2 - x1) / 3) / 2 + pow2 * (x3 - x2) * (Xdis2[k] - (x3 - x2) / 2 - x2) + (y1 - pow2) * (x3 - x2) * (Xdis2[k] - 2 * (x3 - x2) / 3 - x2) / 2;
                                }
                            End20: { }
                                if ((x4 - x3) == 0)
                                {
                                    goto End30;
                                }
                                if (x3 <= Xdis2[k] && Xdis2[k] <= x4)
                                {
                                    y1 = pow3 + (Xdis2[k] - x3) * (pow4 - pow3) / (x4 - x3);
                                    P22 = P22 + (pow1 + pow2) * (x2 - x1) / 2 + (pow2 + pow3) * (x3 - x2) / 2 + (pow3 + y1) * (Xdis2[k] - x3) / 2;
                                    MP22 = MP22 + pow1 * (x2 - x1) * (Xdis2[k] - (x2 - x1) / 2) + (pow2 - pow1) * (x2 - x1) * (Xdis2[k] - 2 * (x2 - x1) / 3) / 2 + pow2 * (x3 - x2) * (Xdis2[k] - (x3 - x2) / 2 - x2) + (pow3 - pow2) * (x3 - x2) * (Xdis2[k] - 2 * (x3 - x2) / 3 - x2) / 2 + pow3 * (Xdis2[k] - x3) * (Xdis2[k] - x3) / 2 + (y1 - pow3) * (Xdis2[k] - x3) * (Xdis2[k] - x3) / 6;

                                }
                                if (Xdis2[k] > x4)
                                {
                                    y1 = pow4;
                                    P22 = P22 + (pow1 + pow2) * (x2 - x1) / 2 + (pow2 + pow3) * (x3 - x2) / 2 + (pow3 + y1) * (x4 - x3) / 2;
                                    MP22 = MP22 + pow1 * (x2 - x1) * (Xdis2[k] - (x2 - x1) / 2) + (pow2 - pow1) * (x2 - x1) * (Xdis2[k] - 2 * (x2 - x1) / 3) / 2 + pow2 * (x3 - x2) * (Xdis2[k] - (x3 - x2) / 2 - x2) + (pow3 - pow2) * (x3 - x2) * (Xdis2[k] - 2 * (x3 - x2) / 3 - x2) / 2 + pow3 * (x4 - x3) * (Xdis2[k] - (x4 - x3) / 2 - x3) + (y1 - pow3) * (x4 - x3) * (Xdis2[k] - 2 * (x4 - x3) / 3 - x3) / 2;
                                }
                            End30: { }
                            }
                            if (((MainForm)mainForm).FrameElement[k+1].LoadDDirection[i] == 1 && ((MainForm)mainForm).FrameElement[k+1].LoadDType[i] == 2)  //===distributed moment about 2 axis 
                            {
                                M22 = pow * Xdis2[k];
                                if ((x1 - x2) == 0)
                                {
                                    goto End210;
                                }
                                if (x1 <= Xdis2[k] && Xdis2[k] <= x2)
                                {
                                    y1 = pow1 + Xdis2[k] * (pow2 - pow1) / (x2 - x1);
                                    M22 = M22 + (pow1 + y1) * Xdis2[k] / 2;
                                }
                                if (Xdis2[k] > x2 && (x3 - x2) == 0 && (x4 - x3) == 0)
                                {
                                    y1 = pow2;
                                    M22 = M22 + (pow1 + y1) * (x2 - x1) / 2;
                                }
                            End210: { }
                                if ((x3 - x2) == 0)
                                {
                                    goto End220;
                                }
                                if (x2 <= Xdis2[k] && Xdis2[k] <= x3)
                                {
                                    y1 = pow2 + (Xdis2[k] - x2) * (pow3 - pow2) / (x3 - x2);
                                    M22 = M22 + (pow1 + pow2) * (x2 - x1) / 2 + (pow2 + y1) * (Xdis2[k] - x2) / 2;
                                }
                                if (Xdis2[k] > x3 && (x4 - x3) == 0)
                                {
                                    y1 = pow3;
                                    M22 = M22 + (pow1 + pow2) * (x2 - x1) / 2 + (pow2 + y1) * (x3 - x2) / 2;
                                }
                            End220: { }
                                if ((x4 - x3) == 0)
                                {
                                    goto End230;
                                }
                                if (x3 <= Xdis2[k] && Xdis2[k] <= x4)
                                {
                                    y1 = pow3 + (Xdis2[k] - x3) * (pow4 - pow3) / (x4 - x3);
                                    M22 = M22 + (pow1 + pow2) * (x2 - x1) / 2 + (pow2 + pow3) * (x3 - x2) / 2 + (pow3 + y1) * (Xdis2[k] - x3) / 2;
                                }
                                if (Xdis2[k] > x4)
                                {
                                    y1 = pow4;
                                    M22 = M22 + (pow1 + pow2) * (x2 - x1) / 2 + (pow2 + pow3) * (x3 - x2) / 2 + (pow3 + y1) * (x4 - x3) / 2;
                                }
                            End230: { }
                            }
                            if (((MainForm)mainForm).FrameElement[k+1].LoadDDirection[i] == 2 && ((MainForm)mainForm).FrameElement[k+1].LoadDType[i] == 1)  //===distributed force along 3 axis 
                            {
                                P33 = pow * Xdis2[k];
                                MP33 = pow * Xdis2[k] * Xdis2[k] / 2;
                                if ((x1 - x2) == 0)
                                {
                                    goto End10;
                                }
                                if (x1 <= Xdis2[k] && Xdis2[k] <= x2)
                                {
                                    y1 = pow1 + Xdis2[k] * (pow2 - pow1) / (x2 - x1);
                                    P33 = P33 + (pow1 + y1) * Xdis2[k] / 2;
                                    MP33 = MP33 + pow1 * Xdis2[k] * Xdis2[k] / 2 + (y1 - pow1) * Xdis2[k] * Xdis2[k] / 6;
                                }
                                if (Xdis2[k] > x2 && (x3 - x2) == 0 && (x4 - x3) == 0)
                                {
                                    y1 = pow2;
                                    P33 = P33 + (pow1 + y1) * (x2 - x1) / 2;
                                    MP33 = MP33 + pow1 * (x2 - x1) * (Xdis2[k] - (x2 - x1) / 2) + (y1 - pow1) * (x2 - x1) * (Xdis2[k] - 2 * (x2 - x1) / 3) / 2;
                                }
                            End10: { }
                                if ((x3 - x2) == 0)
                                {
                                    goto End20;
                                }
                                if (x2 <= Xdis2[k] && Xdis2[k] <= x3)
                                {
                                    y1 = pow2 + (Xdis2[k] - x2) * (pow3 - pow2) / (x3 - x2);
                                    P33 = P33 + (pow1 + pow2) * (x2 - x1) / 2 + (pow2 + y1) * (Xdis2[k] - x2) / 2;
                                    MP33 = MP33 + pow1 * (x2 - x1) * (Xdis2[k] - (x2 - x1) / 2) + (pow2 - pow1) * (x2 - x1) * (Xdis2[k] - 2 * (x2 - x1) / 3) / 2 + pow2 * (Xdis2[k] - x2) * (Xdis2[k] - x2) / 2 + (y1 - pow2) * (Xdis2[k] - x2) * (Xdis2[k] - x2) / 6;
                                }
                                if (Xdis2[k] > x3 && (x4 - x3) == 0)
                                {
                                    y1 = pow3;
                                    P33 = P33 + (pow1 + pow2) * (x2 - x1) / 2 + (pow2 + y1) * (x3 - x2) / 2;
                                    MP33 = MP33 + pow1 * (x2 - x1) * (Xdis2[k] - (x2 - x1) / 2) + (pow2 - pow1) * (x2 - x1) * (Xdis2[k] - 2 * (x2 - x1) / 3) / 2 + pow2 * (x3 - x2) * (Xdis2[k] - (x3 - x2) / 2 - x2) + (y1 - pow2) * (x3 - x2) * (Xdis2[k] - 2 * (x3 - x2) / 3 - x2) / 2;
                                }
                            End20: { }
                                if ((x4 - x3) == 0)
                                {
                                    goto End30;
                                }
                                if (x3 <= Xdis2[k] && Xdis2[k] <= x4)
                                {
                                    y1 = pow3 + (Xdis2[k] - x3) * (pow4 - pow3) / (x4 - x3);
                                    P33 = P33 + (pow1 + pow2) * (x2 - x1) / 2 + (pow2 + pow3) * (x3 - x2) / 2 + (pow3 + y1) * (Xdis2[k] - x3) / 2;
                                    MP33 = MP33 + pow1 * (x2 - x1) * (Xdis2[k] - (x2 - x1) / 2) + (pow2 - pow1) * (x2 - x1) * (Xdis2[k] - 2 * (x2 - x1) / 3) / 2 + pow2 * (x3 - x2) * (Xdis2[k] - (x3 - x2) / 2 - x2) + (pow3 - pow2) * (x3 - x2) * (Xdis2[k] - 2 * (x3 - x2) / 3 - x2) / 2 + pow3 * (Xdis2[k] - x3) * (Xdis2[k] - x3) / 2 + (y1 - pow3) * (Xdis2[k] - x3) * (Xdis2[k] - x3) / 6;
                                }
                                if (Xdis2[k] > x4)
                                {
                                    y1 = pow4;
                                    P33 = P33 + (pow1 + pow2) * (x2 - x1) / 2 + (pow2 + pow3) * (x3 - x2) / 2 + (pow3 + y1) * (x4 - x3) / 2;
                                    MP33 = MP33 + pow1 * (x2 - x1) * (Xdis2[k] - (x2 - x1) / 2) + (pow2 - pow1) * (x2 - x1) * (Xdis2[k] - 2 * (x2 - x1) / 3) / 2 + pow2 * (x3 - x2) * (Xdis2[k] - (x3 - x2) / 2 - x2) + (pow3 - pow2) * (x3 - x2) * (Xdis2[k] - 2 * (x3 - x2) / 3 - x2) / 2 + pow3 * (x4 - x3) * (Xdis2[k] - (x4 - x3) / 2 - x3) + (y1 - pow3) * (x4 - x3) * (Xdis2[k] - 2 * (x4 - x3) / 3 - x3) / 2;
                                }
                            End30: { }
                            }
                            if (((MainForm)mainForm).FrameElement[k+1].LoadDDirection[i] == 2 && ((MainForm)mainForm).FrameElement[k+1].LoadDType[i] == 2)  //===distributed moment about 3 axis 
                            {
                                M33 = pow * Xdis2[k];
                                if ((x1 - x2) == 0)
                                {
                                    goto End240;
                                }
                                if (x1 <= Xdis2[k] && Xdis2[k] <= x2)
                                {
                                    y1 = pow1 + Xdis2[k] * (pow2 - pow1) / (x2 - x1);
                                    M33 = M33 + (pow1 + y1) * Xdis2[k] / 2;
                                }
                                if (Xdis2[k] > x2 && (x3 - x2) == 0 && (x4 - x3) == 0)
                                {
                                    y1 = pow2;
                                    M33 = M33 + (pow1 + y1) * (x2 - x1) / 2;
                                }
                            End240: { }
                                if ((x3 - x2) == 0)
                                {
                                    goto End250;
                                }
                                if (x2 <= Xdis2[k] && Xdis2[k] <= x3)
                                {
                                    y1 = pow2 + (Xdis2[k] - x2) * (pow3 - pow2) / (x3 - x2);
                                    M33 = M33 + (pow1 + pow2) * (x2 - x1) / 2 + (pow2 + y1) * (Xdis2[k] - x2) / 2;
                                }
                                if (Xdis2[k] > x3 && (x4 - x3) == 0)
                                {
                                    y1 = pow3;
                                    M33 = M33 + (pow1 + pow2) * (x2 - x1) / 2 + (pow2 + y1) * (x3 - x2) / 2;
                                }
                            End250: { }
                                if ((x4 - x3) == 0)
                                {
                                    goto End260;
                                }
                                if (x3 <= Xdis2[k] && Xdis2[k] <= x4)
                                {
                                    y1 = pow3 + (Xdis2[k] - x3) * (pow4 - pow3) / (x4 - x3);
                                    M33 = M33 + (pow1 + pow2) * (x2 - x1) / 2 + (pow2 + pow3) * (x3 - x2) / 2 + (pow3 + y1) * (Xdis2[k] - x3) / 2;
                                }
                                if (Xdis2[k] > x4)
                                {
                                    y1 = pow4;
                                    M33 = M33 + (pow1 + pow2) * (x2 - x1) / 2 + (pow2 + pow3) * (x3 - x2) / 2 + (pow3 + y1) * (x4 - x3) / 2;
                                }
                            End260: { }
                            }
                            eleaxialDP[k, i] = Globals.ELforceEXPD[k, i, 0] - P11;
                            eleDM11[k, i] = Globals.ELforceEXPD[k, i, 3] - M11;
                            eleDS220[k, i] = Globals.ELforceEXPD[k, i, 1];
                            eleDS22[k, i] = eleDS220[k, i] - P22;
                            eleDM33[k, i] = Globals.ELforceEXPD[k, i, 5] - eleDS220[k, i] * Xdis2[k] + MP22 - M33;
                            eleDS330[k, i] = Globals.ELforceEXPD[k, i, 2];
                            eleDS33[k, i] = eleDS330[k, i] - P33;
                            eleDM22[k, i] = Globals.ELforceEXPD[k, i, 4] + eleDS330[k, i] * Xdis2[k] - MP33 - M22;
                        }
                        for (int j = 1; j < ((MainForm)mainForm).FrameElement[k + 1].LoadDNumber + 1; j++)  // ==== calculate  at x
                        {
                            eleDS22qx = eleDS22qx + eleDS22[k, j];
                            eleDM33qx = eleDM33qx + eleDM33[k, j];
                            eleDS33qx = eleDS33qx + eleDS33[k, j];
                            eleDM22qx = eleDM22qx + eleDM22[k, j];
                            eleDaxialqx = eleDaxialqx + eleaxialDP[k, j];
                            eleDM11Mqx = eleDM11Mqx + eleDM11[k, j];
                        }
                    }
                    eleM11x = eleDM11Mqx + eleM11Mx - Globals.ELforce[k, 3];
                    eleaxialx = eleDaxialqx + eleaxialPx - Globals.ELforce[k, 0];
                    eleS22x = eleDS22qx + eleS22Px - Globals.ELforce[k, 1];
                    eleM33x = eleDM33qx + eleM33Px - Globals.ELforce[k, 5] + Globals.ELforce[k, 1] * Xdis2[k];
                    eleS33x = eleDS33qx + eleS33Px - Globals.ELforce[k, 2];
                    eleM22x = -eleDM22qx - eleM22Px + Globals.ELforce[k, 4] + Globals.ELforce[k, 2] * Xdis2[k];
                    //=====Displacement
                    U2 = 0;
                    U3 = 0;
                    int m = Globals.ElementInode[k] - 1;
                    int n = Globals.Elementjnode[k] - 1;
                    //   int row1 = 12;
                    //  int col1 = 12;
                    //  int row2 = 1;
                    //  int col2 = 12;
                    double[,] a = new double[12, 12];
                    double[,] b = new double[12, 1];
                    double[] ULocal = new double[12];
                    for (int i = 0; i < 12; i++)
                    {
                        for (int j = 0; j < 12; j++)
                        {
                            a[i, j] = Globals.TFrameElementMatrix[k, i, j];
                        }
                    }
                    for (int j = 0; j < 6; j++)
                    {
                        b[j, 0] = Globals.UDispAll[j, m];
                        b[j + 6, 0] = Globals.UDispAll[j, n];
                    }
                    MatrixMultiplication(12, 12, 12, 1, a, b);
                    for (int j = 0; j < 12; j++)
                    {
                        ULocal[j] = c[j, 0];
                    }
                    U2 = (2 * Math.Pow(Xdis2[k], 3) - 3 * Math.Pow(Xdis2[k], 2) * x5 + Math.Pow(x5, 3)) * ULocal[1];
                    U2 = U2 + (Math.Pow(Xdis2[k], 3) * x5 - 2 * Math.Pow(Xdis2[k], 2) * x5 * x5 + Xdis2[k] * Math.Pow(x5, 3)) * ULocal[5];
                    U2 = U2 + (-2 * Math.Pow(Xdis2[k], 3) + 3 * Math.Pow(Xdis2[k], 2) * x5) * ULocal[7];
                    U2 = U2 + (Math.Pow(Xdis2[k], 3) * x5 - Math.Pow(Xdis2[k], 2) * x5 * x5) * ULocal[11];
                    U2 = U2 / Math.Pow(x5, 3);
                    U3 = (2 * Math.Pow(Xdis2[k], 3) - 3 * Math.Pow(Xdis2[k], 2) * x5 + Math.Pow(x5, 3)) * ULocal[2];
                    U3 = U3 + (Math.Pow(Xdis2[k], 3) * x5 - 2 * Math.Pow(Xdis2[k], 2) * x5 * x5 + Xdis2[k] * Math.Pow(x5, 3)) * ULocal[4];
                    U3 = U3 + (-2 * Math.Pow(Xdis2[k], 3) + 3 * Math.Pow(Xdis2[k], 2) * x5) * ULocal[8];
                    U3 = U3 + (Math.Pow(Xdis2[k], 3) * x5 - Math.Pow(Xdis2[k], 2) * x5 * x5) * ULocal[10];
                    U3 = U3 / Math.Pow(x5, 3);
                    //print result in table
                    // MSFlex2.Rows[k].Cells[0].Value = k + 1;
                    // MSFlex2.Rows[k].Cells[1].Value = Math.Round(eleaxialx, 0);
                    //   MSFlex2.Rows[k].Cells[2].Value = Math.Round(eleS22x, 0);
                    //   MSFlex2.Rows[k].Cells[5].Value = Math.Round(eleM33x, 0);
                    ///   MSFlex2.Rows[k].Cells[3].Value = Math.Round(eleS33x, 0);
                    //   MSFlex2.Rows[k].Cells[4].Value = Math.Round(eleM22x, 0);
                    //   MSFlex2.Rows[k].Cells[6].Value = Math.Round(eleM11x, 0);
                    //   MSFlex2.Rows[k].Cells[7].Value = Math.Round(U2, 6);
                    //  MSFlex2.Rows[k].Cells[8].Value = Math.Round(U3, 6);
                    Frame.ResultValue1[k + 1, sh] = Math.Round(eleaxialx, 0);
                    Frame.ResultValue2[k + 1, sh] = Math.Round(eleS22x, 0);
                    Frame.ResultValue3[k + 1, sh] = Math.Round(eleM33x, 0);
                    Frame.ResultValue4[k + 1, sh] = Math.Round(eleS33x, 0);
                    Frame.ResultValue5[k + 1, sh] = Math.Round(eleM22x, 0);
                    Frame.ResultValue6[k + 1, sh] = Math.Round(eleM11x, 0);
                    Frame.ResultValue7[k + 1, sh] = Math.Round(U2, 6);
                    Frame.ResultValue8[k + 1, sh] = Math.Round(U3, 6);
                }
            }
        }
        public void DrawResultselev0()
        {
            try
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

                Graphics g = Graphics.FromImage(((MainForm)mainForm).pictureBox5.Image);
                Pen pen = new Pen(Color.Blue, 1f);
                Font drawFont = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
                SolidBrush drawBrush = new SolidBrush(Color.Black);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                int tx1 = 0;
                int ty1 = 0;
                int tx2 = 0;
                int ty2 = 0;
                int tx2last = 0;
                int ty2last = 0;
                double tx1R = 0;
                double ty1R = 0;
                double tz1R = 0;
                double tx2R = 0;
                double ty2R = 0;
                double tz2R = 0;
                double txR = 0;
                double tyR = 0;
                double tzR = 0;
                double txRR = 0;
                double tyRR = 0;
                double tzRR = 0;
                double txR0 = 0;
                double tyR0 = 0;
                double tzR0 = 0;
                double BeamLength = 0;
                double MAXpower = -1000000000;
                double PowerScale = 2;
                double ThePowerValue = 0;
                for (int i = 1; i < Frame.Number + 1; i++)
                {
                    if (((MainForm)mainForm).FrameElement[i].Visible == 0)
                    {
                        int tah1 = 0;
                        int tah2 = 0;
                        int tah = 0;
                        double xx = varA * Joint.XReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        double yy = varB * Joint.YReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        double zz = varC * Joint.ZReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        double sm = xx + yy + zz;
                        if (Math.Abs(sm + varD) <= 0.5) tah1 = 1;
                        xx = varA * Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        yy = varB * Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        zz = varC * Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        sm = xx + yy + zz;
                        if (Math.Abs(sm + varD) <= 0.5) tah2 = 1;
                        if (tah1 == 1 & tah2 == 1) tah = 1;
                        if (tah == 1)
                        {
                            for (int j = 0; j < Frame.AnalisesSecNumbers + 1; j++)
                            {
                                if (Myglobals.AnyDiagram == 1) ThePowerValue = Frame.ResultValue1[i, j];//axial
                                if (Myglobals.AnyDiagram == 2) ThePowerValue = Frame.ResultValue2[i, j];//s22
                                if (Myglobals.AnyDiagram == 3) ThePowerValue = Frame.ResultValue4[i, j];//s33
                                if (Myglobals.AnyDiagram == 4) ThePowerValue = Frame.ResultValue5[i, j];//m22
                                if (Myglobals.AnyDiagram == 5) ThePowerValue = Frame.ResultValue3[i, j];//m33
                                if (Myglobals.AnyDiagram == 6) ThePowerValue = Frame.ResultValue6[i, j];//torsion
                                if (Math.Abs(ThePowerValue) > MAXpower) MAXpower = Math.Abs(ThePowerValue);
                            }
                        }
                    }
                }
                PowerScale = PowerScale * 0.8 / MAXpower;
                if (Myglobals.AnyDiagram == 1) PowerScale = PowerScale * 1.2;
                for (int i = 1; i < Frame.Number + 1; i++)
                {
                    if (((MainForm)mainForm).FrameElement[i].Visible == 0)
                    {
                        int tah1 = 0;
                        int tah2 = 0;
                        int tah = 0;
                        double xx = varA * Joint.XReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        double yy = varB * Joint.YReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        double zz = varC * Joint.ZReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        double sm = xx + yy + zz;
                        if (Math.Abs(sm + varD) <= 0.5) tah1 = 1;
                        xx = varA * Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        yy = varB * Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        zz = varC * Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        sm = xx + yy + zz;
                        if (Math.Abs(sm + varD) <= 0.5) tah2 = 1;
                        if (tah1 == 1 & tah2 == 1) tah = 1;
                        if (tah == 1)
                        {
                            double dx0 = 0;
                            double dy0 = 0;
                            double dz0 = 0;
                            double dx = 0;
                            double dy = 0;
                            double dz = 0;
                            double dx1 = 0;
                            double dy1 = 0;
                            double dz1 = 0;
                            ThePowerValue = 0;
                            double TheDistanceValue = 0;
                            tx1R = Joint.XReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                            ty1R = Joint.YReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                            tz1R = Joint.ZReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                            tx2R = Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                            ty2R = Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                            tz2R = Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                            BeamLength = (Math.Sqrt(Math.Pow(tx1R - tx2R, 2) + Math.Pow(ty1R - ty2R, 2) + Math.Pow(tz1R - tz2R, 2)));
                            double plus = BeamLength / Frame.AnalisesSecNumbers;
                            pen = new Pen(Color.Green, 1f);
                            #region//حساب مكان القيم
                            for (int j = 0; j < Frame.AnalisesSecNumbers + 1; j++)
                            {
                                if (Myglobals.AnyDiagram == 1) ThePowerValue = Frame.ResultValue1[i, j];//axial
                                if (Myglobals.AnyDiagram == 2) ThePowerValue = Frame.ResultValue2[i, j];//s22
                                if (Myglobals.AnyDiagram == 3) ThePowerValue = Frame.ResultValue4[i, j];//s33
                                if (Myglobals.AnyDiagram == 4) ThePowerValue = Frame.ResultValue5[i, j];//m22
                                if (Myglobals.AnyDiagram == 5) ThePowerValue = Frame.ResultValue3[i, j];//m33
                                if (Myglobals.AnyDiagram == 6) ThePowerValue = Frame.ResultValue6[i, j];//torsion

                                TheDistanceValue = j * plus / BeamLength;
                                if (j == Frame.AnalisesSecNumbers) TheDistanceValue = 1;
                                txR0 = tx1R + TheDistanceValue * (tx2R - tx1R);
                                tyR0 = ty1R + TheDistanceValue * (ty2R - ty1R);
                                tzR0 = tz1R + TheDistanceValue * (tz2R - tz1R);
                                double DIS1 = Math.Sqrt(Math.Pow(txR0 - tx1D, 2) + Math.Pow(tyR0 - ty1D, 2));
                                double ALFA1 = (double)Math.Round(Angulo(tx1D, ty1D, txR0, tyR0), 2);
                                ALFA1 = ALFA1 * Math.PI / 180;
                                double shara = Math.Cos(ALFA1) / Math.Abs(Math.Cos(ALFA1));
                                DIS1 = DIS1 * shara;
                                tx1 = Myglobals.startXelev + Convert.ToInt32((DIS1) * Myglobals.Zoomelev);
                                ty1 = Myglobals.startYelev - Convert.ToInt32((tzR0) * Myglobals.Zoomelev);
                                dx0 = (((MainForm)mainForm).FrameElement[i].AxisX2[1] - ((MainForm)mainForm).FrameElement[i].AxisX1[1]);
                                dy0 = (((MainForm)mainForm).FrameElement[i].AxisY2[1] - ((MainForm)mainForm).FrameElement[i].AxisY1[1]);
                                dz0 = (((MainForm)mainForm).FrameElement[i].AxisZ2[1] - ((MainForm)mainForm).FrameElement[i].AxisZ1[1]);
                                dx = (((MainForm)mainForm).FrameElement[i].AxisX2[2] - ((MainForm)mainForm).FrameElement[i].AxisX1[2]);
                                dy = (((MainForm)mainForm).FrameElement[i].AxisY2[2] - ((MainForm)mainForm).FrameElement[i].AxisY1[2]);
                                dz = (((MainForm)mainForm).FrameElement[i].AxisZ2[2] - ((MainForm)mainForm).FrameElement[i].AxisZ1[2]);
                                dx1 = (((MainForm)mainForm).FrameElement[i].AxisX2[3] - ((MainForm)mainForm).FrameElement[i].AxisX1[3]);
                                dy1 = (((MainForm)mainForm).FrameElement[i].AxisY2[3] - ((MainForm)mainForm).FrameElement[i].AxisY1[3]);
                                dz1 = (((MainForm)mainForm).FrameElement[i].AxisZ2[3] - ((MainForm)mainForm).FrameElement[i].AxisZ1[3]);


                                txR = (txR0 - PowerScale * ThePowerValue * dx);
                                tyR = (tyR0 - PowerScale * ThePowerValue * dy);
                                tzR = (tzR0 - PowerScale * ThePowerValue * dz);
                                DIS1 = Math.Sqrt(Math.Pow(txR - tx1D, 2) + Math.Pow(tyR - ty1D, 2));
                                ALFA1 = (double)Math.Round(Angulo(tx1D, ty1D, txR, tyR), 2);
                                ALFA1 = ALFA1 * Math.PI / 180;
                                shara = Math.Cos(ALFA1) / Math.Abs(Math.Cos(ALFA1));
                                DIS1 = DIS1 * shara;
                                tx2 = Myglobals.startXelev + Convert.ToInt32((DIS1) * Myglobals.Zoomelev);/////
                                ty2 = Myglobals.startYelev - Convert.ToInt32((tzR) * Myglobals.Zoomelev);//////
                                g.DrawLine(pen, tx1, ty1, tx2, ty2);
                                if (j > 0) g.DrawLine(pen, tx2last, ty2last, tx2, ty2);
                                tx2last = tx2;
                                ty2last = ty2;
                                #region//رسم أسهم
                                if (ThePowerValue > 0)
                                {
                                    if (Myglobals.DiagramValue == 1) if (j == 0 || j == Frame.AnalisesSecNumbers) g.DrawString(ThePowerValue.ToString(), drawFont, drawBrush, tx2 - 10, ty2 + 5);
                                    txR = (txR0 - 0.2 * dx - 0.2 * dx1);
                                    tyR = (tyR0 - 0.2 * dy - 0.2 * dy1);
                                    tzR = (tzR0 - 0.2 * dz - 0.2 * dz1);
                                    txRR = (txR0 - 0.2 * dx + 0.2 * dx1);
                                    tyRR = (tyR0 - 0.2 * dy + 0.2 * dy1);
                                    tzRR = (tzR0 - 0.2 * dz + 0.2 * dz1);
                                }
                                if (ThePowerValue < 0)
                                {
                                    if (Myglobals.DiagramValue == 1) if (j == 0 || j == Frame.AnalisesSecNumbers) g.DrawString(ThePowerValue.ToString(), drawFont, drawBrush, tx2 - 10, ty2 - 20);
                                    txR = (txR0 + 0.2 * dx - 0.2 * dx1);
                                    tyR = (tyR0 + 0.2 * dy - 0.2 * dy1);
                                    tzR = (tzR0 + 0.2 * dz - 0.2 * dz1);
                                    txRR = (txR0 + 0.2 * dx + 0.2 * dx1);
                                    tyRR = (tyR0 + 0.2 * dy + 0.2 * dy1);
                                    tzRR = (tzR0 + 0.2 * dz + 0.2 * dz1);
                                }
                                if (ThePowerValue != 0)
                                {
                                    DIS1 = Math.Sqrt(Math.Pow(txR - tx1D, 2) + Math.Pow(tyR - ty1D, 2));
                                    ALFA1 = (double)Math.Round(Angulo(tx1D, ty1D, txR, tyR), 2);
                                    ALFA1 = ALFA1 * Math.PI / 180;
                                    shara = Math.Cos(ALFA1) / Math.Abs(Math.Cos(ALFA1));
                                    DIS1 = DIS1 * shara;
                                    tx2 = Myglobals.startXelev + Convert.ToInt32((DIS1) * Myglobals.Zoomelev);
                                    ty2 = Myglobals.startYelev - Convert.ToInt32((tzR) * Myglobals.Zoomelev);
                                    // g.DrawLine(pen, tx1, ty1, tx2, ty2);
                                    DIS1 = Math.Sqrt(Math.Pow(txRR - tx1D, 2) + Math.Pow(tyRR - ty1D, 2));
                                    ALFA1 = (double)Math.Round(Angulo(tx1D, ty1D, txRR, tyRR), 2);
                                    ALFA1 = ALFA1 * Math.PI / 180;
                                    shara = Math.Cos(ALFA1) / Math.Abs(Math.Cos(ALFA1));
                                    DIS1 = DIS1 * shara;
                                    tx2 = Myglobals.startXelev + Convert.ToInt32((DIS1) * Myglobals.Zoomelev);
                                    ty2 = Myglobals.startYelev - Convert.ToInt32((tzRR) * Myglobals.Zoomelev);
                                    //  g.DrawLine(pen, tx1, ty1, tx2, ty2);
                                }
                                #endregion
                            }
                            #endregion
                        }
                    }
                }
            }
            catch { }
        }
        public void DrawResultselev()
        {
            try
            {
                #region//معادلة مستوي المقطع
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
                #endregion
                Graphics g = Graphics.FromImage(((MainForm)mainForm).pictureBox5.Image);
                Pen pen = new Pen(Color.Blue, 1f);
                Font drawFont = new Font("Tahoma", 11, FontStyle.Regular, GraphicsUnit.Pixel);
                SolidBrush drawBrush = new SolidBrush(Color.Black);
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                int tx1 = 0;
                int ty1 = 0;
                int tx2 = 0;
                int ty2 = 0;
                int txD1 = 0;
                int tyD1 = 0;
                int txD2 = 0;
                int tyD2 = 0;
                int tx2last = 0;
                int ty2last = 0;
                double BeamLength = 0;
                #region///تحديد مقياس القوة
                double MAXpower = -1000000000;
                double PowerScale = 2;
                double ThePowerValue = 0;
                for (int i = 1; i < Frame.Number + 1; i++)
                {
                    if (((MainForm)mainForm).FrameElement[i].Visible == 0)
                    {
                        int tah1 = 0;
                        int tah2 = 0;
                        int tah = 0;
                        double xx = varA * Joint.XReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        double yy = varB * Joint.YReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        double zz = varC * Joint.ZReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        double sm = xx + yy + zz;
                        if (Math.Abs(sm + varD) <= 0.5) tah1 = 1;
                        xx = varA * Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        yy = varB * Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        zz = varC * Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        sm = xx + yy + zz;
                        if (Math.Abs(sm + varD) <= 0.5) tah2 = 1;
                        if (tah1 == 1 & tah2 == 1) tah = 1;
                        if (tah == 1)
                        {
                            for (int j = 0; j < Frame.AnalisesSecNumbers + 1; j++)
                            {
                                if (Myglobals.AnyDiagram == 1) ThePowerValue = Frame.ResultValue1[i, j];//axial
                                if (Myglobals.AnyDiagram == 2) ThePowerValue = Frame.ResultValue2[i, j];//s22
                                if (Myglobals.AnyDiagram == 3) ThePowerValue = Frame.ResultValue4[i, j];//s33
                                if (Myglobals.AnyDiagram == 4) ThePowerValue = Frame.ResultValue5[i, j];//m22
                                if (Myglobals.AnyDiagram == 5) ThePowerValue = Frame.ResultValue3[i, j];//m33
                                if (Myglobals.AnyDiagram == 6) ThePowerValue = Frame.ResultValue6[i, j];//torsion
                                if (Math.Abs(ThePowerValue) > MAXpower) MAXpower = Math.Abs(ThePowerValue);
                            }
                        }
                    }
                }
                PowerScale = PowerScale * 0.8 / MAXpower;
                if (Myglobals.AnyDiagram == 1) PowerScale = PowerScale * 1.2;
                #endregion
                for (int i = 1; i < Frame.Number + 1; i++)
                {
                    if (((MainForm)mainForm).FrameElement[i].Visible == 0)
                    {
                   #region//الفرام ينتمي لمستوي المقطع
                        int tah1 = 0;
                        int tah2 = 0;
                        int tah = 0;
                        double xx = varA * Joint.XReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        double yy = varB * Joint.YReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        double zz = varC * Joint.ZReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        double sm = xx + yy + zz;
                        if (Math.Abs(sm + varD) <= 0.5) tah1 = 1;
                        xx = varA * Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        yy = varB * Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        zz = varC * Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        sm = xx + yy + zz;
                        if (Math.Abs(sm + varD) <= 0.5) tah2 = 1;
                        if (tah1 == 1 & tah2 == 1) tah = 1;
                   #endregion
                        if (tah == 1)
                        {
                            ThePowerValue = 0;
                            double TheDistanceValue = 0;
                            tx1 = Joint.Xelev[((MainForm)mainForm).FrameElement[i].FirstJoint];
                            ty1 = Joint.Yelev[((MainForm)mainForm).FrameElement[i].FirstJoint];
                            tx2 = Joint.Xelev[((MainForm)mainForm).FrameElement[i].SecondJoint];
                            ty2 = Joint.Yelev[((MainForm)mainForm).FrameElement[i].SecondJoint];
                            double ALFA = 0;
                            ALFA = (float)Angulo(tx1, ty1, tx2, ty2);
                            if (ALFA >= 180) ALFA = ALFA - 180;
                            double ALFA1 = ALFA * Math.PI / 180;

                            BeamLength = (Math.Sqrt(Math.Pow(tx1 - tx2, 2) + Math.Pow(ty1 - ty2, 2)));
                            double plus = BeamLength / Frame.AnalisesSecNumbers;
                            pen = new Pen(Color.Green, 1f);
                            #region//حساب مكان القيم
                            for (int j = 0; j < Frame.AnalisesSecNumbers + 1; j++)
                            {
                                if (Myglobals.AnyDiagram == 1) ThePowerValue = Frame.ResultValue1[i, j];//axial
                                if (Myglobals.AnyDiagram == 2) ThePowerValue = Frame.ResultValue2[i, j];//s22
                                if (Myglobals.AnyDiagram == 3) ThePowerValue = Frame.ResultValue4[i, j];//s33
                                if (Myglobals.AnyDiagram == 4) ThePowerValue = Frame.ResultValue5[i, j];//m22
                                if (Myglobals.AnyDiagram == 5) ThePowerValue = Frame.ResultValue3[i, j];//m33
                                if (Myglobals.AnyDiagram == 6) ThePowerValue = Frame.ResultValue6[i, j];//torsion
                              
                                TheDistanceValue = j * plus / BeamLength;
                                if (j == Frame.AnalisesSecNumbers) TheDistanceValue = 1;
                                txD1 = (int)(tx1 + TheDistanceValue * (tx2 - tx1));
                                tyD1 = (int)(ty1 + TheDistanceValue * (ty2 - ty1));
                                double pixValue = (PowerScale * ThePowerValue) * Myglobals.Zoomelev;
                                txD2 = (int)(txD1 - pixValue * Math.Sin(ALFA1));
                                tyD2 = (int)(tyD1 + pixValue * Math.Cos(ALFA1));
                                g.DrawLine(pen, txD1, tyD1, txD2, tyD2);
                                if (j > 0) g.DrawLine(pen, tx2last, ty2last, txD2, tyD2);
                                tx2last = txD2;
                                ty2last = tyD2;
                                #region//رسم أسهم
                                
                                if (ThePowerValue > 0)
                                {
                                    if (Myglobals.DiagramValue == 1) if (j == 0 || j == Frame.AnalisesSecNumbers) g.DrawString(ThePowerValue.ToString(), drawFont, drawBrush, txD2 - 10, tyD2 + 5);
                                  //  txR = (txR0 - 0.2 * dx - 0.2 * dx1);
                                  //  tyR = (tyR0 - 0.2 * dy - 0.2 * dy1);
                                 //  tzR = (tzR0 - 0.2 * dz - 0.2 * dz1);
                                  //  txRR = (txR0 - 0.2 * dx + 0.2 * dx1);
                                 //   tyRR = (tyR0 - 0.2 * dy + 0.2 * dy1);
                                   // tzRR = (tzR0 - 0.2 * dz + 0.2 * dz1);
                                }
                                if (ThePowerValue < 0)
                                {
                                    if (Myglobals.DiagramValue == 1) if (j == 0 || j == Frame.AnalisesSecNumbers) g.DrawString(ThePowerValue.ToString(), drawFont, drawBrush, txD2 - 10, tyD2 - 20);
                                  //  txR = (txR0 + 0.2 * dx - 0.2 * dx1);
                                 //   tyR = (tyR0 + 0.2 * dy - 0.2 * dy1);
                                  ///  tzR = (tzR0 + 0.2 * dz - 0.2 * dz1);
                                 //   txRR = (txR0 + 0.2 * dx + 0.2 * dx1);
                                 //   tyRR = (tyR0 + 0.2 * dy + 0.2 * dy1);
                                 //   tzRR = (tzR0 + 0.2 * dz + 0.2 * dz1);
                                }
                                /*
                                if (ThePowerValue != 0)
                                {
                                    DIS1 = Math.Sqrt(Math.Pow(txR - tx1D, 2) + Math.Pow(tyR - ty1D, 2));
                                    ALFA1 = (double)Math.Round(Angulo(tx1D, ty1D, txR, tyR), 2);
                                    ALFA1 = ALFA1 * Math.PI / 180;
                                    shara = Math.Cos(ALFA1) / Math.Abs(Math.Cos(ALFA1));
                                    DIS1 = DIS1 * shara;
                                    tx2 = Myglobals.startXelev + Convert.ToInt32((DIS1) * Myglobals.Zoomelev);
                                    ty2 = Myglobals.startYelev - Convert.ToInt32((tzR) * Myglobals.Zoomelev);
                                    // g.DrawLine(pen, tx1, ty1, tx2, ty2);
                                    DIS1 = Math.Sqrt(Math.Pow(txRR - tx1D, 2) + Math.Pow(tyRR - ty1D, 2));
                                    ALFA1 = (double)Math.Round(Angulo(tx1D, ty1D, txRR, tyRR), 2);
                                    ALFA1 = ALFA1 * Math.PI / 180;
                                    shara = Math.Cos(ALFA1) / Math.Abs(Math.Cos(ALFA1));
                                    DIS1 = DIS1 * shara;
                                    tx2 = Myglobals.startXelev + Convert.ToInt32((DIS1) * Myglobals.Zoomelev);
                                    ty2 = Myglobals.startYelev - Convert.ToInt32((tzRR) * Myglobals.Zoomelev);
                                    //  g.DrawLine(pen, tx1, ty1, tx2, ty2);
                                }
                                */
                                #endregion
                            }
                            #endregion
                        }
                    }
                }
            }
            catch { }
        }
    }
}
