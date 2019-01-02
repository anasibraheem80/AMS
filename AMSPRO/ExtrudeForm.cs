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
    public partial class ExtrudeForm : Form
    {
        public ExtrudeForm()
        {
            InitializeComponent();
        }
        Form mainForm = Application.OpenForms["MainForm"];
        double Thedistance = 0;
        double TheX = 0;
        double TheY = 0;
        double[] ShellPointXTemp = new double[1000];
        double[] ShellPointYTemp = new double[1000];
        double[] ShellPointZTemp = new double[1000];
        private void button1_Click(object sender, EventArgs e)
        {
            Myglobals.PickPoints = 0;
            Myglobals.PickNumber = 0;
            int ifselected = 0;
            #region//خطي
            if (tabControl1.SelectedIndex == 0)
            {
                double dx = Convert.ToDouble(textBox1.Text);
                double dy = Convert.ToDouble(textBox2.Text);
                double dz = Convert.ToDouble(textBox12.Text);
                int Number = Convert.ToInt32(textBox3.Text);
                if (Myglobals.ExtrudeType == "Joint")
                {
                    int JointNumber = Joint.Number2d;
                    for (int i = 1; i < JointNumber + 1; i++)///////////////////عقد
                    {
                        if (Joint.Selected[i] == 1)
                        {
                            ifselected = 1;
                            double FirstX = Joint.XReal[i];
                            double FirstY = Joint.YReal[i];
                            double FirstZ = Joint.ZReal[i];
                            double SecondX = 0;
                            double SecondY = 0;
                            double SecondZ = 0;
                            int FrameSection = 1;
                            double FrameRotateAngel = 0;
                            for (int j = 1; j < Number + 1; j++)
                            {
                                SecondX = FirstX + dx;
                                SecondY = FirstY + dy;
                                SecondZ = FirstZ + dz;
                                addbeams(FirstX, FirstY, FirstZ, SecondX, SecondY, SecondZ, FrameSection, FrameRotateAngel);
                                FirstX = SecondX;
                                FirstY = SecondY;
                                FirstZ = SecondZ;
                            }
                        }
                    }
                }
                if (Myglobals.ExtrudeType == "Frame")
                {
                    int FrameNumber = Frame.Number;
                    for (int i = 1; i < FrameNumber + 1; i++)///////////////////جوائز
                    {
                        if (((MainForm)mainForm).FrameElement[i].Selected == 1)
                        {
                            ifselected = 1;
                            double FirstX = Joint.XReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                            double FirstY = Joint.YReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                            double FirstZ = Joint.ZReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                            double SecondX = Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                            double SecondY = Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                            double SecondZ = Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                            int FrameSection = ((MainForm)mainForm).FrameElement[i].Section;
                            double FrameRotateAngel = ((MainForm)mainForm).FrameElement[i].RotateAngel;
                            for (int j = 1; j < Number + 1; j++)
                            {
                                Shell.PointNumbersTemp = 4;
                                Shell.PointXTemp[1] = FirstX;
                                Shell.PointYTemp[1] = FirstY;
                                Shell.PointZTemp[1] = FirstZ;
                                Shell.PointXTemp[4] = SecondX;
                                Shell.PointYTemp[4] = SecondY;
                                Shell.PointZTemp[4] = SecondZ;
                                FirstX = FirstX + dx;
                                FirstY = FirstY + dy;
                                FirstZ = FirstZ + dz;
                                SecondX = SecondX + dx;
                                SecondY = SecondY + dy;
                                SecondZ = SecondZ + dz;
                                Shell.PointXTemp[2] = FirstX;
                                Shell.PointYTemp[2] = FirstY;
                                Shell.PointZTemp[2] = FirstZ;
                                Shell.PointXTemp[3] = SecondX;
                                Shell.PointYTemp[3] = SecondY;
                                Shell.PointZTemp[3] = SecondZ;
                                int ShellSection = 1;
                                addshells(ShellSection);
                                Shell.PointNumbersTemp = 0;
                            }
                        }
                    }
                }
            }
            #endregion
            #region//دوران
            if (tabControl1.SelectedIndex == 1)//
            {
                double CenterX = Convert.ToDouble(textBox4.Text);
                double CenterY = Convert.ToDouble(textBox5.Text);
                int Number = Convert.ToInt32(textBox6.Text);
                int Angle = Convert.ToInt32(textBox7.Text);
                double dz = Convert.ToDouble(textBox8.Text) / Number;
                double tah180 = Math.PI / 180;
                #region//عقد
                if (Myglobals.ExtrudeType == "Joint")
                {
                    int JointNumber = Joint.Number2d;
                    for (int i = 1; i < JointNumber + 1; i++)
                    {
                        if (Joint.Selected[i] == 1)
                        {
                            ifselected = 1;
                            int FrameSection = 1;
                            double FrameRotateAngel = ((MainForm)mainForm).FrameElement[i].RotateAngel;
                            double FirstX0 = Joint.XReal[i];
                            double FirstY0 = Joint.YReal[i];
                            double FirstZ0 = Joint.ZReal[i];
                            double SecondX0 = 0;
                            double SecondY0 = 0;
                            double SecondZ0 =0;
                            double FirstLength = Math.Sqrt(Math.Pow(FirstX0 - CenterX, 2) + Math.Pow(FirstY0 - CenterY, 2));
                            double FaiFirst = 0;
                            if (FirstX0 == CenterX)///////////////
                            {
                                if (FirstY0 > CenterY)
                                {
                                    FaiFirst = 90;
                                }
                                else
                                {
                                    FaiFirst = -90;
                                }
                            }
                            else
                            {
                                FaiFirst = Math.Atan(Math.Abs(FirstY0 - CenterY) / Math.Abs(FirstX0 - CenterX)) / tah180;
                                if (FirstX0 >= CenterX & FirstY0 >= CenterY)
                                {
                                    FaiFirst = Math.Abs(FaiFirst);
                                    goto endFaiFirst;
                                }
                                if (FirstX0 <= CenterX & FirstY0 >= CenterY)
                                {
                                    FaiFirst = 180 - Math.Abs(FaiFirst);
                                    goto endFaiFirst;
                                }
                                if (FirstX0 <= CenterX & FirstY0 <= CenterY)
                                {
                                    FaiFirst = 180 + Math.Abs(FaiFirst);
                                    goto endFaiFirst;
                                }
                                if (FirstX0 >= CenterX & FirstY0 <= CenterY)
                                {
                                    FaiFirst = 360 - Math.Abs(FaiFirst);
                                    goto endFaiFirst;
                                }
                            endFaiFirst: { };
                            }

                            double FirstX = FirstX0;
                            double FirstY = FirstY0;
                            double FirstZ = FirstZ0;
                            double SecondX = 0;
                            double SecondY = 0;
                            double SecondZ = 0;
                            for (int j = 1; j < Number + 1; j++)
                            {
                                SecondX = CenterX + FirstLength * Math.Cos(tah180 * (FaiFirst + j * Angle));
                                SecondY = CenterY + FirstLength * Math.Sin(tah180 * (FaiFirst + j * Angle));
                                SecondZ = FirstZ - dz;
                                addbeams(FirstX, FirstY, FirstZ, SecondX, SecondY, SecondZ, FrameSection, FrameRotateAngel);
                                FirstX = SecondX;
                                FirstY = SecondY;
                                FirstZ = SecondZ;
                            }
                        }
                    }
                }
                #endregion
                #region//جوائز
                if (Myglobals.ExtrudeType == "Frame")
                {
                    int FrameNumber = Frame.Number;
                    for (int i = 1; i < FrameNumber + 1; i++)
                    {
                        if (((MainForm)mainForm).FrameElement[i].Selected == 1)
                        {
                            ifselected = 1;
                            int FrameSection = ((MainForm)mainForm).FrameElement[i].Section;
                            double FrameRotateAngel = ((MainForm)mainForm).FrameElement[i].RotateAngel;
                            double FirstX0 = Joint.XReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                            double FirstY0 = Joint.YReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                            double FirstZ0 = Joint.ZReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                            double SecondX0 = Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                            double SecondY0 = Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                            double SecondZ0 = Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                            double FirstLength = Math.Sqrt(Math.Pow(FirstX0 - CenterX, 2) + Math.Pow(FirstY0 - CenterY, 2));
                            double SecondLength = Math.Sqrt(Math.Pow(SecondX0 - CenterX, 2) + Math.Pow(SecondY0 - CenterY, 2));
                            double FaiFirst = 0;
                            double FaiSecond = 0;
                            if (FirstX0 == CenterX)///////////////
                            {
                                if (FirstY0 > CenterY)
                                {
                                    FaiFirst = 90;
                                }
                                else
                                {
                                    FaiFirst = -90;
                                }
                            }
                            else
                            {
                                FaiFirst = Math.Atan(Math.Abs(FirstY0 - CenterY) / Math.Abs(FirstX0 - CenterX)) / tah180;
                                if (FirstX0 >= CenterX & FirstY0 >= CenterY)
                                {
                                    FaiFirst = Math.Abs(FaiFirst);
                                    goto endFaiFirst;
                                }
                                if (FirstX0 <= CenterX & FirstY0 >= CenterY)
                                {
                                    FaiFirst = 180 - Math.Abs(FaiFirst);
                                    goto endFaiFirst;
                                }
                                if (FirstX0 <= CenterX & FirstY0 <= CenterY)
                                {
                                    FaiFirst = 180 + Math.Abs(FaiFirst);
                                    goto endFaiFirst;
                                }
                                if (FirstX0 >= CenterX & FirstY0 <= CenterY)
                                {
                                    FaiFirst = 360 - Math.Abs(FaiFirst);
                                    goto endFaiFirst;
                                }
                            endFaiFirst: { };
                            }
                            if (SecondX0 == CenterX)////////////////
                            {
                                if (SecondY0 > CenterY)
                                {
                                    FaiSecond = 90;
                                }
                                else
                                {
                                    FaiSecond = -90;
                                }
                            }
                            else
                            {
                                FaiSecond = Math.Atan(Math.Abs(SecondY0 - CenterY) / Math.Abs(SecondX0 - CenterX)) / tah180;
                                if (SecondX0 >= CenterX & SecondY0 >= CenterY)
                                {
                                    FaiSecond = Math.Abs(FaiSecond);
                                    goto endFaiSecond;
                                }
                                if (SecondX0 <= CenterX & SecondY0 >= CenterY)
                                {
                                    FaiSecond = 180 - Math.Abs(FaiSecond);
                                    goto endFaiSecond;
                                }
                                if (SecondX0 <= CenterX & SecondY0 <= CenterY)
                                {
                                    FaiSecond = 180 + Math.Abs(FaiSecond);
                                    goto endFaiSecond;
                                }
                                if (SecondX0 >= CenterX & SecondY0 <= CenterY)
                                {
                                    FaiSecond = 360 - Math.Abs(FaiSecond);
                                    goto endFaiSecond;
                                }
                            endFaiSecond: { };
                            }
                            double FirstX = FirstX0;
                            double FirstY = FirstY0;
                            double FirstZ = FirstZ0;
                            double SecondX = SecondX0;
                            double SecondY = SecondY0;
                            double SecondZ = SecondZ0;
                            for (int j = 1; j < Number + 1; j++)
                            {
                                Shell.PointNumbersTemp = 4;
                                Shell.PointXTemp[1] = FirstX;
                                Shell.PointYTemp[1] = FirstY;
                                Shell.PointZTemp[1] = FirstZ;
                                Shell.PointXTemp[4] = SecondX;
                                Shell.PointYTemp[4] = SecondY;
                                Shell.PointZTemp[4] = SecondZ;
                                FirstX = CenterX + FirstLength * Math.Cos(tah180 * (FaiFirst + j * Angle));
                                FirstY = CenterY + FirstLength * Math.Sin(tah180 * (FaiFirst + j * Angle));
                                SecondX = CenterX + SecondLength * Math.Cos(tah180 * (FaiSecond + j * Angle));
                                SecondY = CenterY + SecondLength * Math.Sin(tah180 * (FaiSecond + j * Angle));
                                FirstZ = FirstZ - dz;
                                SecondZ = SecondZ - dz;
                                Shell.PointXTemp[2] = FirstX;
                                Shell.PointYTemp[2] = FirstY;
                                Shell.PointZTemp[2] = FirstZ;
                                Shell.PointXTemp[3] = SecondX;
                                Shell.PointYTemp[3] = SecondY;
                                Shell.PointZTemp[3] = SecondZ;
                                int ShellSection = 1;
                                addshells(ShellSection);
                                Shell.PointNumbersTemp = 0;
                            }
                        }
                    }
                }
                #endregion
            }
            #endregion
            if (ifselected == 1)
            {
                DROWcls callmee = new DROWcls();
                callmee.Render2d();
                callmee.Render3d();
                ((MainForm)mainForm).MakeTempFiles();
            }
        }

        private void addbeams(double FirstX, double FirstY, double FirstZ, double SecondX, double SecondY, double SecondZ, int FrameSection, double FrameRotateAngel)
        {
            FirstX = Math.Round(FirstX, 3);
            FirstY = Math.Round(FirstY, 3);
            FirstZ = Math.Round(FirstZ, 3);
            SecondX = Math.Round(SecondX, 3);
            SecondY = Math.Round(SecondY, 3);
            SecondZ = Math.Round(SecondZ, 3);
            for (int i = 1; i < Frame.Number + 1; i++)
            {
                if (Joint.XReal[((MainForm)mainForm).FrameElement[i].FirstJoint] == FirstX & Joint.YReal[((MainForm)mainForm).FrameElement[i].FirstJoint] == FirstY & Joint.ZReal[((MainForm)mainForm).FrameElement[i].FirstJoint] == FirstZ)
                {
                    if (Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint] == SecondX & Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint] == SecondY & Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint] == SecondZ)
                    {
                        goto ENDBEAM;
                    }
                }
                if (Joint.XReal[((MainForm)mainForm).FrameElement[i].FirstJoint] == SecondX & Joint.YReal[((MainForm)mainForm).FrameElement[i].FirstJoint] == SecondY & Joint.ZReal[((MainForm)mainForm).FrameElement[i].FirstJoint] == SecondZ)
                {
                    if (Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint] == FirstX & Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint] == FirstY & Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint] == FirstZ)
                    {
                        goto ENDBEAM;
                    }
                }
            }
            Frame.Number = Frame.Number + 1;
            ((MainForm)mainForm).FrameElement[Frame.Number].Selected = 0;
            double theX = FirstX;
            double theY = FirstY;
            double theZ = FirstZ;
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
            ((MainForm)mainForm).FrameElement[Frame.Number].FirstJoint = selectedjoint;

            theX = SecondX;
            theY = SecondY;
            theZ = SecondZ;
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
            ((MainForm)mainForm).FrameElement[Frame.Number].SecondJoint = selectedjoint;

            ((MainForm)mainForm).FrameElement[Frame.Number].Section = FrameSection;
            ((MainForm)mainForm).FrameElement[Frame.Number].RotateAngel = FrameRotateAngel;

            int thejoint = ((MainForm)mainForm).FrameElement[Frame.Number].FirstJoint;
            Joint.BeamConnectionN[thejoint] = Joint.BeamConnectionN[thejoint] + 1;
            int thecount = Joint.BeamConnectionN[thejoint];
            Joint.Beam[thejoint, thecount] = Frame.Number;
            thejoint = ((MainForm)mainForm).FrameElement[Frame.Number].SecondJoint;
            Joint.BeamConnectionN[thejoint] = Joint.BeamConnectionN[thejoint] + 1;
            thecount = Joint.BeamConnectionN[thejoint];
            Joint.Beam[thejoint, thecount] = Frame.Number;
        ENDBEAM: { }
        }

        private void addshells(int ShellSection)
        {
            Shell.Number = Shell.Number + 1;
            Shell.PointNumbers[Shell.Number] = Shell.PointNumbersTemp;
            int tah = 1;
            for (int i = 1; i < Shell.PointNumbers[Shell.Number] + 1; i++)
            {
                double theX = Math.Round(Shell.PointXTemp[i], 3);
                double theY = Math.Round(Shell.PointYTemp[i], 3);
                double theZ = Math.Round(Shell.PointZTemp[i], 3);
                int tah1 = 0;
                int selectedjoint = 0;
                for (int j = 1; j < Joint.Number2d + 1; j++)
                {
                    if (Joint.XReal[j] == theX & Joint.YReal[j] == theY & Joint.ZReal[j] == theZ)
                    {
                        tah1 = 1;
                        selectedjoint = j;
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
            Shell.Section[Shell.Number] = ShellSection;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Myglobals.PickPoints = 3;
            Myglobals.PickNumber = 1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Myglobals.PickPoints = 2;
            Myglobals.PickNumber = 1;
        }
        
    
    
    }
}
