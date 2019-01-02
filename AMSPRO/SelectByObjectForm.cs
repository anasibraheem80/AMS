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
    public partial class SelectByObjectForm : Form
    {
        public SelectByObjectForm()
        {
            InitializeComponent();
        }
        Form mainForm = Application.OpenForms["MainForm"];
        private void SelectByForm_Load(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = 0;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int index;
            int ifselect = 0;
            for (int i = 0; i < listBox1.SelectedItems.Count; i++)
            {
                index = listBox1.SelectedIndices[i] + 1;
                if (index == 1)//عقد
                {
                    for (int j = 1; j < Joint.Number3d + 1; j++)
                    {
                        Joint.Selected [j]=1;
                        ifselect = 1;
                    }
                }
                if (index == 2)//أعمدة
                {
                    for (int j = 1; j < Frame.Number + 1; j++)
                    {
                        if (Joint.ZReal[((MainForm)mainForm).FrameElement[j].FirstJoint] != Joint.ZReal[((MainForm)mainForm).FrameElement[j].SecondJoint])
                        {
                            ((MainForm)mainForm).FrameElement[j].Selected = 1;
                            ifselect = 1;
                        }
                    }
                }
                if (index == 3)//جوائز
                {
                    for (int j = 1; j < Frame.Number + 1; j++)
                    {
                        if (Joint.ZReal[((MainForm)mainForm).FrameElement[j].FirstJoint] == Joint.ZReal[((MainForm)mainForm).FrameElement[j].SecondJoint])
                        {
                            ((MainForm)mainForm).FrameElement[j].Selected = 1;
                            ifselect = 1;
                        }
                    }
                }
                if (index == 6)//جدران
                {
                    for (int j = 1; j < Shell.Number + 1; j++)
                    {
                        if (Shell.Type[j]  == 2)
                        {
                            for (int k = 1; k < Shell.PointNumbers[j] + 1; k++)
                            {
                                Shell.SelectedLine[j, k] = 1;
                                ifselect = 1;
                            }
                        }
                    }
                }
                if (index == 7)//بلاطات
                {
                    for (int j = 1; j < Shell.Number + 1; j++)
                    {
                        if (Shell.Type[j] == 1)
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
                index = listBox1.SelectedIndices[i] + 1;
                if (index == 1)//عقد
                {
                    for (int j = 1; j < Joint.Number3d + 1; j++)
                    {
                        Joint.Selected[j] = 0;
                        ifselect = 1;
                    }
                }
                if (index == 2)//أعمدة
                {
                    for (int j = 1; j < Frame.Number + 1; j++)
                    {
                        if (Joint.ZReal[((MainForm)mainForm).FrameElement[j].FirstJoint] != Joint.ZReal[((MainForm)mainForm).FrameElement[j].SecondJoint])
                        {
                            ((MainForm)mainForm).FrameElement[j].Selected = 0;
                            ifselect = 1;
                        }
                    }
                }
                if (index == 3)//جوائز
                {
                    for (int j = 1; j < Frame.Number + 1; j++)
                    {
                        if (Joint.ZReal[((MainForm)mainForm).FrameElement[j].FirstJoint] == Joint.ZReal[((MainForm)mainForm).FrameElement[j].SecondJoint])
                        {
                            ((MainForm)mainForm).FrameElement[j].Selected = 0;
                            ifselect = 1;
                        }
                    }
                }

                if (index == 6)//جدران
                {
                    for (int j = 1; j < Shell.Number + 1; j++)
                    {
                        if (Shell.Type[j] == 2)
                        {
                            for (int k = 1; k < Shell.PointNumbers[j] + 1; k++)
                            {
                                Shell.SelectedLine[j, k] = 0;
                                ifselect = 1;
                            }
                        }
                    }
                }
                if (index == 7)//بلاطات
                {
                    for (int j = 1; j < Shell.Number + 1; j++)
                    {
                        if (Shell.Type[j] == 1)
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
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
