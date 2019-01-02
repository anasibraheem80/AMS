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
    public partial class SelectByMaterialForm : Form
    {
        public SelectByMaterialForm()
        {
            InitializeComponent();
        }
        Form mainForm = Application.OpenForms["MainForm"];
        private void SelectByMaterialForm_Load(object sender, EventArgs e)
        {
            for (int i = 1; i < Material.Number + 1; i++)
            {
                listBox1.Items.Add(Material.Name[i]);
            }
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
            for (int i = 1; i < Shell.Number + 1; i++)
            {
                for (int j = 0; j < listBox1.SelectedItems.Count; j++)
                {
                    index = listBox1.SelectedIndices[j] + 1;
                    if (Slab.Material[Shell.Section[i]] == index)
                    {
                        ifselect = 1;
                        for (int k = 1; k < Shell.PointNumbers[i] + 1; k++)
                        {
                            Shell.SelectedLine[i, k] = 1;
                        }
                        break;
                    }
                }
            }
            for (int i = 1; i < Frame.Number + 1; i++)
            {
                for (int j = 0; j < listBox1.SelectedItems.Count; j++)
                {
                    index = listBox1.SelectedIndices[j] + 1;
                    if (Section.Material[((MainForm)mainForm).FrameElement[i].Section] == index)
                    {
                        ifselect = 1;
                        ((MainForm)mainForm).FrameElement[i].Selected = 1;
                        break;
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
            for (int i = 1; i < Shell.Number + 1; i++)
            {
                for (int j = 0; j < listBox1.SelectedItems.Count; j++)
                {
                    index = listBox1.SelectedIndices[j] + 1;
                    if (Slab.Material[Shell.Section[i]] == index)
                    {
                        ifselect = 1;
                        for (int k = 1; k < Shell.PointNumbers[i] + 1; k++)
                        {
                            Shell.SelectedLine[i, k] = 0;
                        }
                        break;
                    }
                }
            }
            for (int i = 1; i < Frame.Number + 1; i++)
            {
                for (int j = 0; j < listBox1.SelectedItems.Count; j++)
                {
                    index = listBox1.SelectedIndices[j] + 1;
                    if (Section.Material[((MainForm)mainForm).FrameElement[i].Section] == index)
                    {
                        ifselect = 1;
                        ((MainForm)mainForm).FrameElement[i].Selected = 0;
                        break;
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
