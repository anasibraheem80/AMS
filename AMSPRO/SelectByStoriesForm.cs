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
    public partial class SelectByStoriesForm : Form
    {
        public SelectByStoriesForm()
        {
            InitializeComponent();
        }
        Form mainForm = Application.OpenForms["MainForm"];
        private void SelectByStoriesForm_Load(object sender, EventArgs e)
        {
            for (int i = Myglobals.StoryNumbers; i > 0; i--)
            {
                listBox1.Items.Add(Myglobals.StoryName[i]);
            }
            listBox1.Items.Add("Base");
            listBox1.SelectedIndex = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index;
            int ifselect = 0;
            for (int i = 0; i < listBox1.SelectedItems.Count; i++)
            {
                index = Myglobals.StoryNumbers - listBox1.SelectedIndices[i];
                //عقد
                for (int j = 1; j < Joint.Number3d + 1; j++)
                {
                    if (Joint.ZReal[j] == Myglobals.StoryLevel[index])
                    {
                        Joint.Selected[j] = 1;
                        ifselect = 1;
                    }
                }
                //أعمدة وجوائز
                for (int j = 1; j < Frame.Number + 1; j++)
                {
                    int tah = 0;
                    if (index != 0)
                    {
                        if (Joint.ZReal[((MainForm)mainForm).FrameElement[j].FirstJoint] == Myglobals.StoryLevel[index] & Joint.ZReal[((MainForm)mainForm).FrameElement[j].SecondJoint] < Myglobals.StoryLevel[index] & Joint.ZReal[((MainForm)mainForm).FrameElement[j].SecondJoint] >= Myglobals.StoryLevel[index - 1]) tah = 1;
                        if (Joint.ZReal[((MainForm)mainForm).FrameElement[j].SecondJoint] == Myglobals.StoryLevel[index] & Joint.ZReal[((MainForm)mainForm).FrameElement[j].FirstJoint] < Myglobals.StoryLevel[index] & Joint.ZReal[((MainForm)mainForm).FrameElement[j].FirstJoint] >= Myglobals.StoryLevel[index - 1]) tah = 1;
                    }
                    if (Joint.ZReal[((MainForm)mainForm).FrameElement[j].FirstJoint] == Myglobals.StoryLevel[index] & Joint.ZReal[((MainForm)mainForm).FrameElement[j].SecondJoint] == Myglobals.StoryLevel[index]) tah = 1;
                    if (tah == 1)
                    {
                        ((MainForm)mainForm).FrameElement[j].Selected = 1;
                        ifselect = 1;
                    }
                }
                for (int j = 1; j < Shell.Number + 1; j++)
                {
                    //بلاطات
                    if (Shell.Type[j] == 1 & Joint.ZReal[Shell.JointNo[j,1]] == Myglobals.StoryLevel[index])
                    {
                        for (int k = 1; k < Shell.PointNumbers[j] + 1; k++)
                        {
                            Shell.SelectedLine[j, k] = 1;
                            ifselect = 1;
                        }
                    }
                    //جدران
                    if (Shell.Type[j] == 2 & index != 0)
                    {
                        int tah = 0;
                        for (int k = 1; k < Shell.PointNumbers[j] + 1; k++)
                        {
                            if (Joint.ZReal[Shell.JointNo[j, k]] > Myglobals.StoryLevel[index] || Joint.ZReal[Shell.JointNo[j, k]] < Myglobals.StoryLevel[index - 1])
                            {
                                tah = 1;
                            }
                        }
                        if (tah == 0)
                        {
                            for (int k = 1; k < Shell.PointNumbers[j] + 1; k++)
                            {
                                Shell.SelectedLine[j, k] = 1;
                                ifselect = 1;
                            }
                        }
                    }

                }
            }
            if (ifselect == 1)
            {
                DROWcls callmee = new DROWcls();
                callmee.Render2d();
                callmee.Render3d();
                callmee.Renderelev();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int index;
            int ifselect = 0;
            for (int i = 0; i < listBox1.SelectedItems.Count; i++)
            {
                index = Myglobals.StoryNumbers - listBox1.SelectedIndices[i];
                //عقد
                for (int j = 1; j < Joint.Number3d + 1; j++)
                {
                    if (Joint.ZReal[j] == Myglobals.StoryLevel[index])
                    {
                        Joint.Selected[j] = 0;
                        ifselect = 1;
                    }
                }
                //أعمدة وجوائز
                for (int j = 1; j < Frame.Number + 1; j++)
                {
                    int tah = 0;
                    if (index != 0)
                    {
                        if (Joint.ZReal[((MainForm)mainForm).FrameElement[j].FirstJoint] == Myglobals.StoryLevel[index] & Joint.ZReal[((MainForm)mainForm).FrameElement[j].SecondJoint] < Myglobals.StoryLevel[index] & Joint.ZReal[((MainForm)mainForm).FrameElement[j].SecondJoint] >= Myglobals.StoryLevel[index - 1]) tah = 1;
                        if (Joint.ZReal[((MainForm)mainForm).FrameElement[j].SecondJoint] == Myglobals.StoryLevel[index] & Joint.ZReal[((MainForm)mainForm).FrameElement[j].FirstJoint] < Myglobals.StoryLevel[index] & Joint.ZReal[((MainForm)mainForm).FrameElement[j].FirstJoint] >= Myglobals.StoryLevel[index - 1]) tah = 1;
                    }
                    if (Joint.ZReal[((MainForm)mainForm).FrameElement[j].FirstJoint] == Myglobals.StoryLevel[index] & Joint.ZReal[((MainForm)mainForm).FrameElement[j].SecondJoint] == Myglobals.StoryLevel[index]) tah = 1;
                    if (tah == 1)
                    {
                        ((MainForm)mainForm).FrameElement[j].Selected = 0;
                        ifselect = 1;
                    }
                }

                for (int j = 1; j < Shell.Number + 1; j++)
                {
                    //بلاطات
                    if (Shell.Type[j] == 1 & Joint.ZReal[Shell.JointNo[j, 1]] == Myglobals.StoryLevel[index])
                    {
                        for (int k = 1; k < Shell.PointNumbers[j] + 1; k++)
                        {
                            Shell.SelectedLine[j, k] = 0;
                            ifselect = 1;
                        }
                    }
                    //جدران
                    if (Shell.Type[j] == 2 & index != 0)
                    {
                        int tah = 0;
                        for (int k = 1; k < Shell.PointNumbers[j] + 1; k++)
                        {
                            if (Joint.ZReal[Shell.JointNo[j, k]] > Myglobals.StoryLevel[index] || Joint.ZReal[Shell.JointNo[j, k]] < Myglobals.StoryLevel[index - 1])
                            {
                                tah = 1;
                            }
                        }
                        if (tah == 0)
                        {
                            for (int k = 1; k < Shell.PointNumbers[j] + 1; k++)
                            {
                                Shell.SelectedLine[j, k] = 0;
                                ifselect = 1;
                            }
                        }
                    }
                }
            }
            if (ifselect == 1)
            {
                DROWcls callmee = new DROWcls();
                callmee.Render2d();
                callmee.Render3d();
                callmee.Renderelev();
            }
        }
    }
}
