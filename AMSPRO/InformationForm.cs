using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.IO;
using DynamicTypeDescriptor;
//using Flobbster.Windows.Forms;
//using System.Collections;


//[Browsable(false)]
//[BindableAttribute(true)]
// [DesignOnly(true)]

//using System.Globalization;
namespace AMSPRO
{
    public partial class JointInformationForm : Form
    {
        public JointInformationForm()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public DynamicCustomTypeDescriptor m_dctd = null;
        private void InformationForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = Joint.SelectedforProp.ToString();
            int i = Joint.SelectedforProp;
            //-------------------------------------------------------
            PointInformation1 bill1 = new PointInformation1();
            bill1.XReal = Joint.XReal[Joint.SelectedforProp];
            bill1.YReal = Joint.YReal[Joint.SelectedforProp];
            bill1.ZReal = Joint.ZReal[Joint.SelectedforProp];
            for (int j = 1; j < Joint.BeamConnectionN[i] + 1; j++)
            {
                bill1.Beam[j - 1] = (Joint.Beam[i, j]);
            }
            for (int j = 1; j < Joint.FloorConnectionN[i] + 1; j++)
            {
                bill1.Floor[j - 1] = Joint.Floor[i, j];
            }
            PointInformation2 bill2 = new PointInformation2();
            #region//set yes or no
            bill2.FixX = false;
            if (Joint.FixX[Joint.SelectedforProp] == 1) bill2.FixX = true;
            bill2.FixY = false;
            if (Joint.FixY[Joint.SelectedforProp] == 1) bill2.FixY = true;
            bill2.FixZ = false;
            if (Joint.FixZ[Joint.SelectedforProp] == 1) bill2.FixZ = true;
            bill2.FixRX = false;
            if (Joint.FixRX[Joint.SelectedforProp] == 1) bill2.FixRX = true;
            bill2.FixRY = false;
            if (Joint.FixRY[Joint.SelectedforProp] == 1) bill2.FixRY = true;
            bill2.FixRZ = false;
            if (Joint.FixRZ[Joint.SelectedforProp] == 1) bill2.FixRZ = true;
            m_dctd = ProviderInstaller.Install(bill2);
            CustomPropertyDescriptor cpd = m_dctd.GetProperty("FixX");
            for (int j = 1; j < 7; j++)
            {
                if (j == 1) cpd = m_dctd.GetProperty("FixX");
                if (j == 2) cpd = m_dctd.GetProperty("FixY");
                if (j == 3) cpd = m_dctd.GetProperty("FixZ");
                if (j == 4) cpd = m_dctd.GetProperty("FixRX");
                if (j == 5) cpd = m_dctd.GetProperty("FixRY");
                if (j == 6) cpd = m_dctd.GetProperty("FixRZ");
                foreach (StandardValueAttribute sva in cpd.StatandardValues)
                {
                    if (sva.DisplayName == "True")
                    {
                        sva.DisplayName = "Yes";
                    }
                    else
                    {
                        sva.DisplayName = "No";
                    }
                }
            }
            #endregion
            PointInformation3 bill3 = new PointInformation3();
            bill3.PowerX = (Joint.PowerX[i]);
            bill3.PowerY = (Joint.PowerY[i]);
            bill3.PowerZ = (Joint.PowerZ[i]);
            bill3.MomentXX = (Joint.MomentXX[i]);
            bill3.MomentYY = (Joint.MomentYY[i]);
            bill3.MomentZZ = (Joint.MomentZZ[i]);
            propertyGrid1.SelectedObject = bill1;
            propertyGrid2.SelectedObject = bill2;
            propertyGrid3.SelectedObject = bill3;
        }
        public class PointInformation1
        {
            private double xReal;
            private double yReal;
            private double zReal;
            public int[] beam = new int[Joint.BeamConnectionN[Joint.SelectedforProp]];
            private int[] floor = new int[Joint.FloorConnectionN[Joint.SelectedforProp]];
            [Category("Connectivity")]
            [Description("Frams connected to the joint object")]
            [DisplayName("Fram")]
            [ReadOnly(true)]
            public int[] Beam
            {
                get { return beam; }
                set { beam = value; }
            }

            [Category("Connectivity")]
            [Description("Floors connected to the joint object")]
            [DisplayName("Floor")]
            [ReadOnly(true)]
            public int[] Floor
            {
                get { return floor; }
                set { floor = value; }
            }

            [Category("Geometry")]
            [Description("The global X coordinate of the joint object")]
            [DisplayName("Global X (m)")]
            [ReadOnly(true)]
            public double XReal
            {
                get { return xReal; }

                set { xReal = value; }
            }

            [Category("Geometry")]
            [Description("The global Y coordinate of the joint object")]
            [DisplayName("Global Y (m)")]
            [ReadOnly(true)]
            public double YReal
            {
                get { return yReal; }

                set { yReal = value; }
            }

            [Category("Geometry")]
            [Description("The global Z coordinate of the joint object")]
            [DisplayName("Global Z (m)")]
            [ReadOnly(true)]
            public double ZReal
            {
                get { return zReal; }

                set { zReal = value; }
            }
            public PointInformation1()
            {
            }
        }
        public class PointInformation2
        {
            private bool fixX;
            private bool fixY;
            private bool fixZ;
            private bool fixRX;
            private bool fixRY;
            private bool fixRZ;
            private int name;

            [Category("Assignment")]
            [Description("Restrainted degree of fredom of the joint object")]
            [DisplayName("Restraints")]
            [ReadOnly(true)]
            [Browsable(false)]
            public int Name
            {
                get { return name; }
                set { name = value; }
            }

            [Category("Restraints")]
            [Description("Translational restraint exists in the global X direction")]
            [DisplayName("UX Restrained")]
            //[ReadOnly(true)]
            public bool FixX
            {
                get { return fixX; }
                set { fixX = value; }
            }

            [Category("Restraints")]
            [Description("Translational restraint exists in the global Y direction")]
            [DisplayName("UY Restrained")]
            //[ReadOnly(true)]
            public bool FixY
            {
                get { return fixY; }
                set { fixY = value; }
            }

            [Category("Restraints")]
            [Description("Translational restraint exists in the global Z direction")]
            [DisplayName("UZ Restrained")]
            //[ReadOnly(true)]
            public bool FixZ
            {
                get { return fixZ; }
                set { fixZ = value; }
            }

            [Category("Restraints")]
            [Description("Rotational restraint exists in the global X direction")]
            [DisplayName("RX Restrained")]
            //[ReadOnly(true)]
            public bool FixRX
            {
                get { return fixRX; }
                set { fixRX = value; }
            }

            [Category("Restraints")]
            [Description("Rotational restraint exists in the global Y direction")]
            [DisplayName("RY Restrained")]
            //[ReadOnly(true)]
            public bool FixRY
            {
                get { return fixRY; }
                set { fixRY = value; }
            }

            [Category("Restraints")]
            [Description("Rotational restraint exists in the global Z direction")]
            [DisplayName("RZ Restrained")]
            //[ReadOnly(true)]
            public bool FixRZ
            {
                get { return fixRZ; }
                set { fixRZ = value; }
            }

            public PointInformation2()
            {
            }
        }
        public class PointInformation3
        {
            private double powerX;
            private double powerY;
            private double powerZ;
            private double momentXX;
            private double momentYY;
            private double momentZZ;

            [Category("Force")]
            [Description("X direction force")]
            [DisplayName("FX")]
            //[ReadOnly(true)]
            public double PowerX
            {
                get { return powerX; }
                set { powerX = value; }
            }

            [Category("Force")]
            [Description("Y direction force")]
            [DisplayName("FY")]
            //[ReadOnly(true)]
            public double PowerY
            {
                get { return powerY; }
                set { powerY = value; }
            }

            [Category("Force")]
            [Description("Z direction force")]
            [DisplayName("FZ")]
            //[ReadOnly(true)]
            public double PowerZ
            {
                get { return powerZ; }
                set { powerZ = value; }
            }

            [Category("Force")]
            [Description("Moment about X axis")]
            [DisplayName("MX")]
            //[ReadOnly(true)]
            public double MomentXX
            {
                get { return momentXX; }
                set { momentXX = value; }
            }

            [Category("Force")]
            [Description("Moment about Y axis")]
            [DisplayName("MY")]
            //[ReadOnly(true)]
            public double MomentYY
            {
                get { return momentYY; }
                set { momentYY = value; }
            }

            [Category("Force")]
            [Description("Moment about Z axis")]
            [DisplayName("MZ")]
            //[ReadOnly(true)]
            public double MomentZZ
            {
                get { return momentZZ; }
                set { momentZZ = value; }
            }

            public PointInformation3()
            {
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
