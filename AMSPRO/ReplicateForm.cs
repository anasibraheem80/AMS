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
    public partial class ReplicateForm : Form
    {
        public ReplicateForm()
        {
            InitializeComponent();
        }
        Form mainForm = Application.OpenForms["MainForm"];
        double Thedistance = 0;
        double TheX = 0;
        double TheY = 0;
         double[] ShellPointXTemp = new double [1000];
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
                int Number = Convert.ToInt32(textBox3.Text);
                int FrameNumber = Frame.Number;
                for (int i = 1; i < FrameNumber + 1; i++)///////////////////جوائز
                {
                    if (((MainForm)mainForm).FrameElement[i].Selected == 1)
                    {
                        ifselected = 1;
                        double FirstX = Joint.XReal[((MainForm)mainForm).FrameElement[i].FirstJoint] ;
                        double FirstY = Joint.YReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        double FirstZ = Joint.ZReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        double SecondX = Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        double SecondY = Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        double SecondZ = Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        int FrameSection = ((MainForm)mainForm).FrameElement[i].Section;
                        double FrameRotateAngel = ((MainForm)mainForm).FrameElement[i].RotateAngel;
                        for (int j = 1; j < Number + 1; j++)
                        {
                            FirstX = FirstX +  dx;
                            FirstY = FirstY + dy;
                            SecondX = SecondX +  dx;
                            SecondY = SecondY + dy;
                            addbeams(FirstX, FirstY, FirstZ, SecondX, SecondY, SecondZ, FrameSection, FrameRotateAngel);
                        }
                    }
                }
                int ShellNumber = Shell.Number;
                for (int i = 1; i < ShellNumber + 1; i++)////////////////////بلاطات
                {
                    int selected = 0;
                    for (int j = 1; j < Shell.PointNumbers[i]  + 1; j++)
                    {
                        if (Shell.SelectedLine[i,j] == 1)
                        {
                            selected = 1;
                            break;
                        }
                    }
                    if (selected == 1)
                    {
                        ifselected = 1;
                        Shell.PointNumbersTemp = Shell.PointNumbers[i];
                        for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                        {
                            Shell.PointXTemp[j] = Joint.XReal[Shell.JointNo[i, j]];
                            Shell.PointYTemp[j] = Joint.YReal[Shell.JointNo[i, j]];
                            Shell.PointZTemp[j] = Joint.ZReal[Shell.JointNo[i, j]];
                        }
                        ifselected = 1;
                        int ShellSection = Shell.Section[i];
                        int ShellType = Shell.Type[i];
                        for (int k = 1; k < Number + 1; k++)
                        {
                            for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                            {
                                Shell.PointXTemp[j] = Shell.PointXTemp[j] + dx;
                                Shell.PointYTemp[j] = Shell.PointYTemp[j] + dy; 
                            }
                            addshells(ShellSection);
                        }
                        Shell.PointNumbersTemp = 0;
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
                
                double tah180 = Math.PI /180;
                #region//جوائز
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
                        double FirstZ = Joint.ZReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        double SecondX0 = Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        double SecondY0 = Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        double SecondZ = Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
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
                        for (int j = 1; j < Number + 1; j++)
                        {
                            double FirstX = CenterX + FirstLength * Math.Cos(tah180 * (FaiFirst + j * Angle));
                            double FirstY = CenterY + FirstLength * Math.Sin(tah180 * (FaiFirst + j * Angle));
                            double SecondX = CenterX + SecondLength * Math.Cos(tah180 * (FaiSecond + j * Angle));
                            double SecondY = CenterY + SecondLength * Math.Sin(tah180 * (FaiSecond + j * Angle));
                            addbeams(FirstX, FirstY, FirstZ, SecondX, SecondY, SecondZ, FrameSection, FrameRotateAngel);
                        }
                    }
                }
#endregion
                #region//بلاطات
                int ShellNumber = Shell.Number;
                for (int i = 1; i < ShellNumber + 1; i++)
                {
                    int selected = 0;
                    for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                    {
                        if (Shell.SelectedLine[i, j] == 1)
                        {
                            selected = 1;
                            break;
                        }
                    }
                    if (selected == 1)
                    {
                        ifselected = 1;
                        Shell.PointNumbersTemp = Shell.PointNumbers[i];
                        for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                        {
                            ShellPointXTemp[j] = Joint.XReal[Shell.JointNo[i, j]];
                            ShellPointYTemp[j] = Joint.YReal[Shell.JointNo[i, j]];
                            Shell.PointZTemp[j] = Joint.ZReal[Shell.JointNo[i, j]];
                        }
                        ifselected = 1;
                        int ShellSection = Shell.Section[i];
                        int ShellType = Shell.Type[i];
                        for (int k = 1; k < Number + 1; k++)
                        {
                            for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                            {
                                double FaiFirst = 0;
                                double FirstLength = Math.Sqrt(Math.Pow(ShellPointXTemp[j] - CenterX, 2) + Math.Pow(ShellPointYTemp[j] - CenterY, 2));
                                if (ShellPointXTemp[j] == CenterX)
                                {
                                    if (ShellPointYTemp[j] > CenterY)
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
                                    FaiFirst = Math.Atan(Math.Abs(ShellPointYTemp[j] - CenterY) / Math.Abs(ShellPointXTemp[j] - CenterX)) / tah180;
                                    if (ShellPointXTemp[j] >= CenterX & ShellPointYTemp[j] >= CenterY)
                                    {
                                        FaiFirst = Math.Abs(FaiFirst);
                                        goto endFaiFirst;
                                    }
                                    if (ShellPointXTemp[j] <= CenterX & ShellPointYTemp[j] >= CenterY)
                                    {
                                        FaiFirst = 180 - Math.Abs(FaiFirst);
                                        goto endFaiFirst;
                                    }
                                    if (ShellPointXTemp[j] <= CenterX & ShellPointYTemp[j] <= CenterY)
                                    {
                                        FaiFirst = 180 + Math.Abs(FaiFirst);
                                        goto endFaiFirst;
                                    }
                                    if (ShellPointXTemp[j] >= CenterX & ShellPointYTemp[j] <= CenterY)
                                    {
                                        FaiFirst = 360 - Math.Abs(FaiFirst);
                                        goto endFaiFirst;
                                    }
                                endFaiFirst: { };
                                }
                               Shell.PointXTemp[j] = CenterX + FirstLength * Math.Cos(tah180 * (FaiFirst + k * Angle));
                               Shell.PointYTemp[j]  = CenterY + FirstLength * Math.Sin(tah180 * (FaiFirst + k * Angle));
                            }
                            addshells(ShellSection);
                        }
                    }
                    Shell.PointNumbersTemp = 0;
                }
                #endregion
            }
            #endregion
            #region//مراي
            if (tabControl1.SelectedIndex == 2)
            {
                double tah180 = Math.PI / 180;
                double X1 = Convert.ToDouble(textBox8.Text);
                double Y1 = Convert.ToDouble(textBox9.Text);
                double X2 = Convert.ToDouble(textBox10.Text);
                double Y2 = Convert.ToDouble(textBox11.Text);
                double Fai = 0;
                if (X2 == X1)
                {
                        Fai = 90;
                        goto endFai;
                }
                else
                {
                    Fai = Math.Atan(Math.Abs(Y2 - Y1) / Math.Abs(X2 - X1)) / tah180;
                    if (Y2 == Y1)
                    {
                        Fai = 180;
                        goto endFai;
                    }
                    if (X1 > X2 & Y1 > Y2)
                    {
                        Fai = 180+ Math.Abs(Fai);
                        goto endFai;
                    }
                    if (X2 > X1 & Y2 > Y1)
                    {
                        Fai = Math.Abs(Fai);
                        goto endFai;
                    } 
                    
                    if (X1 < X2 & Y1 > Y2)
                    {
                       Fai = - Math.Abs(Fai);
                        goto endFai;
                    }
                    if (X2 < X1 & Y2 > Y1)
                    {
                        Fai = 180 - Math.Abs(Fai);
                        goto endFai;
                    }
              
                }
            endFai: { };
                #region////جوائز
                int FrameNumber = Frame.Number;
                for (int i = 1; i < FrameNumber + 1; i++)
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
                        DistanceCalc(FirstX, FirstY,  X1,  Y1,  X2,  Y2);
                        FirstX = FirstX - 2*Thedistance * Math.Cos((90 - Fai) * tah180);
                        FirstY = FirstY + 2*Thedistance * Math.Sin((90 - Fai) * tah180);
                        DistanceCalc(SecondX, SecondY, X1, Y1, X2, Y2);
                        SecondX = SecondX - 2*Thedistance * Math.Cos((90 - Fai) * tah180);
                        SecondY = SecondY + 2*Thedistance * Math.Sin((90 - Fai) * tah180);
                        addbeams(FirstX, FirstY, FirstZ, SecondX, SecondY, SecondZ, FrameSection, FrameRotateAngel);
                    }
                }
#endregion
                #region//بلاطات
                int ShellNumber = Shell.Number;
                for (int i = 1; i < ShellNumber + 1; i++)
                {
                    int selected = 0;
                    for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                    {
                        if (Shell.SelectedLine[i, j] == 1)
                        {
                            selected = 1;
                            break;
                        }
                    }
                    if (selected == 1)
                    {
                        Shell.PointNumbersTemp = Shell.PointNumbers[i];
                        for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                        {
                            Shell.PointXTemp[j] = Joint.XReal[Shell.JointNo[i, j]];
                            Shell.PointYTemp[j] = Joint.YReal[Shell.JointNo[i, j]];
                            Shell.PointZTemp[j] = Joint.ZReal[Shell.JointNo[i, j]];
                        }
                        ifselected = 1;
                        int ShellSection = Shell.Section[i];
                        int ShellType = Shell.Type[i];
                        for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                        {
                            DistanceCalc(Shell.PointXTemp[j], Shell.PointYTemp[j], X1, Y1, X2, Y2);
                            Shell.PointXTemp[j] = Shell.PointXTemp[j] - 2 * Thedistance * Math.Cos((90 - Fai) * tah180);
                            Shell.PointYTemp[j] = Shell.PointYTemp[j] + 2 * Thedistance * Math.Sin((90 - Fai) * tah180);
                        }
                        addshells(ShellSection);
                    }
                    Shell.PointNumbersTemp = 0;
                }
                #endregion
            }
            #endregion
            #region//طوابق
            if (tabControl1.SelectedIndex == 3)
            {

                #region//جوائز
                int FrameNumber = Frame.Number;
                for (int i = 1; i < FrameNumber + 1; i++)
                {
                    if (((MainForm)mainForm).FrameElement[i].Selected == 1)
                    {
                        ifselected = 1;
                        double FirstX = Joint.XReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        double FirstY = Joint.YReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        double FirstZ0 = Joint.ZReal[((MainForm)mainForm).FrameElement[i].FirstJoint];
                        double SecondX = Joint.XReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        double SecondY = Joint.YReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        double SecondZ0 = Joint.ZReal[((MainForm)mainForm).FrameElement[i].SecondJoint];
                        int FrameSection = ((MainForm)mainForm).FrameElement[i].Section;
                        double FrameRotateAngel = ((MainForm)mainForm).FrameElement[i].RotateAngel;
                        int index;
                        double nesba1= 0;
                        double nesba2 = 0;
                        for (int k = 1; k < Myglobals.StoryNumbers+1; k++)
                        {
                            if (FirstZ0 > Myglobals.StoryLevel[k - 1] & FirstZ0 <= Myglobals.StoryLevel[k])
                            {
                                nesba1 = (FirstZ0 - Myglobals.StoryLevel[k - 1]) / Myglobals.StoryHight[k];
                                if (nesba1 == 1 & SecondZ0 > FirstZ0) nesba1 = 0;
                                break;
                            }
                        }
                        for (int k = 1; k < Myglobals.StoryNumbers + 1; k++)
                        {
                            if (SecondZ0 > Myglobals.StoryLevel[k - 1] & SecondZ0 <= Myglobals.StoryLevel[k])
                            {
                                nesba2 = (SecondZ0 - Myglobals.StoryLevel[k - 1]) / Myglobals.StoryHight[k];
                                if (nesba2 == 1 & SecondZ0 < FirstZ0) nesba2 = 0;
                                break;
                            }
                        }

                        for (int j = 0; j < listBox1.SelectedItems.Count; j++)
                        {
                            index = Myglobals.StoryNumbers - listBox1.SelectedIndices[j];
                            double FirstZ = 0;
                            double SecondZ = 0;
                            if (FirstZ0 == SecondZ0)//جائز
                            {
                                FirstZ = Myglobals.StoryLevel[index];
                                SecondZ = Myglobals.StoryLevel[index];
                                if (FirstZ >= 0 & SecondZ >= 0)
                                {
                                    addbeams(FirstX, FirstY, FirstZ, SecondX, SecondY, SecondZ, FrameSection, FrameRotateAngel);
                                }
                            }
                            if (FirstZ0 != SecondZ0 & index > 0)//عمود أو مائل
                            {
                                FirstZ = Myglobals.StoryLevel[index - 1] + nesba1 * Myglobals.StoryHight[index];
                                SecondZ = Myglobals.StoryLevel[index - 1] + nesba2 * Myglobals.StoryHight[index];
                                if (FirstZ >= 0 & SecondZ >= 0)
                                {
                                    addbeams(FirstX, FirstY, FirstZ, SecondX, SecondY, SecondZ, FrameSection, FrameRotateAngel);
                                }
                            }
                        }
                    }
                }
                #endregion
                #region//بلاطات
                int ShellNumber = Shell.Number;
                for (int i = 1; i < ShellNumber + 1; i++)
                {
                    int selected = 0;
                    for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                    {
                        if (Shell.SelectedLine[i, j] == 1)
                        {
                            selected = 1;
                            break;
                        }
                    }
                    if (selected == 1)
                    {
                        Shell.PointNumbersTemp = Shell.PointNumbers[i];
                        double max = -1000000;
                        for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                        {
                            Shell.PointXTemp[j] = Joint.XReal[Shell.JointNo[i, j]];
                            Shell.PointYTemp[j] = Joint.YReal[Shell.JointNo[i, j]];
                            ShellPointZTemp[j] = Joint.ZReal[Shell.JointNo[i, j]];
                            if (ShellPointZTemp[j] > max) max = ShellPointZTemp[j];
                        }
                        ifselected = 1;
                        int ShellSection = Shell.Section[i];
                        int ShellType = Shell.Type[i];
                        int index;
                        for (int j = 0; j < listBox1.SelectedItems.Count; j++)
                        {
                            index = Myglobals.StoryNumbers - listBox1.SelectedIndices[j];
                            if (Shell.Type[i] == 1)//بلاطة
                            {
                                for (int k = 1; k < Shell.PointNumbers[i] + 1; k++)
                                {
                                    Shell.PointZTemp[k] = Myglobals.StoryLevel[index];
                                }
                                addshells(ShellSection);
                            }
                            if (Shell.Type[i] == 2 & index > 0)//جدار أو مائل
                            {
                                for (int k = 1; k < Shell.PointNumbers[i] + 1; k++)
                                {
                                    double nesba = 0;
                                    for (int l = 1; l < Myglobals.StoryNumbers + 1; l++)
                                    {
                                        if (ShellPointZTemp[k] > Myglobals.StoryLevel[l - 1] & ShellPointZTemp[k] <= Myglobals.StoryLevel[l])
                                        {
                                            nesba = (ShellPointZTemp[k] - Myglobals.StoryLevel[l - 1]) / Myglobals.StoryHight[l];
                                            if (nesba == 1 & ShellPointZTemp[k] < max) nesba = 0;
                                            break;
                                        }
                                    }
                                    Shell.PointZTemp[k] = Myglobals.StoryLevel[index - 1] + nesba * Myglobals.StoryHight[index];
                                }
                                addshells(ShellSection);
                            }
                        }
                    }
                    Shell.PointNumbersTemp = 0;
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
            FrameElements emp = new FrameElements();
            ((MainForm)mainForm).FrameElement[Frame.Number] = emp;
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
            int tah0 = 0;
            double FirstX = 0;
            double FirstY = 0;
            double FirstZ = 0;
            for (int i = 1; i < Shell.Number + 1; i++)
            {
                tah0 = 0;
                int m = 0;
                if (Shell.PointNumbersTemp == Shell.PointNumbers[i])
                {
                    for (int k = 1; k < Shell.PointNumbersTemp + 1; k++)
                    {
                        FirstX = Math.Round(Shell.PointXTemp[k], 3);
                        FirstY = Math.Round(Shell.PointYTemp[k], 3);
                        FirstZ = Math.Round(Shell.PointZTemp[k], 3);
                        for (int j = 1; j < Shell.PointNumbers[i] + 1; j++)
                        {
                            if (Joint.XReal[Shell.JointNo[i, j]] == FirstX & Joint.YReal[Shell.JointNo[i, j]] == FirstY & Joint.ZReal[Shell.JointNo[i, j]] == FirstZ)
                            {
                                m = m + 1;
                            }
                        }
                    }
                    if (m == Shell.PointNumbers[i]) goto ENDSHELL;
                }
            }          
            
            Shell.Number = Shell.Number + 1;
            Shell.PointNumbers[Shell.Number] = Shell.PointNumbersTemp;
            int tah = 1;
            for (int i = 1; i < Shell.PointNumbers[Shell.Number] + 1; i++)
            {
                double theX = Math.Round(Shell.PointXTemp[i],3);
                double theY = Math.Round(Shell.PointYTemp[i],3);
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
        ENDSHELL: { };
        }
        
        private void DistanceCalc(double X, double Y, double X1, double Y1, double X2, double Y2)
        {
            double distance = 0;
            int Tah = 0;
            if ((Y2 == Y1))
            {
               // if (X >= X1 & X <= X2) Tah = 1;
                //if (X <= X1 & X >= X2) Tah = 1;
               // if (Tah == 1)
                {
                   // distance = Math.Abs(Y - Y1);
                    distance = (Y - Y1);
                    goto endloop;
                }
            }
            if ((X2 == X1))
            {
                //if (Y >= Y1 & Y <= Y2) Tah = 1;
               // if (Y <= Y1 & Y >= Y2) Tah = 1;
                //if (Tah == 1)
                {
                    //distance = Math.Abs(X - X1);
                    distance = (X - X1);
                    goto endloop;
                }
            }
            if (X2 != X1)
            {
               // if (X >= X1 & X <= X2 & Y >= Y1 & Y <= Y2) Tah = 1;
               // if (X >= X2 & X <= X1 & Y >= Y2 & Y <= Y1) Tah = 1;
               // if (X >= X2 & X <= X1 & Y >= Y1 & Y <= Y2) Tah = 1;
               // if (X >= X1 & X <= X2 & Y >= Y2 & Y <= Y1) Tah = 1;
              //  if (Tah == 1)
                {
                    //distance = Math.Abs((X2 - X1) * (Y1 - Y) - (X1 - X) * (Y2 - Y1)) / Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2));
                    distance = ((X2 - X1) * (Y1 - Y) - (X1 - X) * (Y2 - Y1)) / Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2));
                }
            }
        endloop: { }
        Thedistance = distance;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ReplicateForm_Load(object sender, EventArgs e)
        {
            for (int i = Myglobals.StoryNumbers; i > 0; i--)
            {
                listBox1.Items.Add(Myglobals.StoryName[i]);
            }
            listBox1.Items.Add("Base");
            listBox1.SelectedIndex = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Myglobals.PickPoints = 1;
            Myglobals.PickNumber=1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Myglobals.PickPoints = 2;
            Myglobals.PickNumber = 1;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Myglobals.PickPoints = 3;
            Myglobals.PickNumber = 1;
        }

    }
}
