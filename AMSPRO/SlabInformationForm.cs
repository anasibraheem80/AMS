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

using System.Reflection;
using System.Collections;
using System.Runtime.InteropServices;

namespace AMSPRO
{
    public partial class SlabInformationForm : Form
    {
        public SlabInformationForm()
        {
            InitializeComponent();
        }
        private void SlabInformationForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = Shell.SelectedforProp.ToString();
            int i = Shell.SelectedforProp;
            //------------------------------
            AssignmentTable assignmentTable = new AssignmentTable();
            assignmentTable.sectionproberty = Slab.Name[Shell.Section[i]];
            propertyGrid2.SelectedObject = assignmentTable;
            //--------------------------
            ForcesTable forcesTable = new ForcesTable();
            propertyGrid3.SelectedObject = forcesTable;
            //--------------------------
            GeometryTable geometryTable = new GeometryTable();
            geometryTable.Aria = Shell.Aria[i];
            geometryTable.CentroidX = Shell.CenterX[i];
            geometryTable.CentroidY = Shell.CenterY[i];
            geometryTable.Perimeter = Shell.Perimeter[i];
            propertyGrid1.SelectedObject = geometryTable;
        }
        #region//جدول المواصفات
        public class AssignmentTable
        {
            [Category("Assignments")]
            [Description("Section property assigned to the shell object")]
            [DisplayName("Section property")]
            [ReadOnly(true)]
            public string sectionproberty { get; set; }
        }
        #endregion
        #region//جدول القوى
        public class ForcesTable
        {
            ForceCollection forces = new ForceCollection();
            Force[] emps = new Force[Shell.LoadNumber[Shell.SelectedforProp]];
            int i = Shell.SelectedforProp;
            public ForcesTable()
            {
                for (int j = 0; j < Shell.LoadNumber[i]; j++)
                {
                    Myglobals.POWERTYPE = 0;
                    Force emp = new Force();
                    emp.Pattern = Loads.Load[Shell.LoadPattern[i, j + 1] + 1];
                    string teDirection = "";
                    if (Shell.LoadDirection[i, j + 1] == 0) teDirection = "Local-1";
                    if (Shell.LoadDirection[i, j + 1] == 1) teDirection = "Local-2";
                    if (Shell.LoadDirection[i, j + 1] == 2) teDirection = "Local-3";
                    if (Shell.LoadDirection[i, j + 1] == 3) teDirection = "Global-X";
                    if (Shell.LoadDirection[i, j + 1] == 4) teDirection = "Global-Y";
                    if (Shell.LoadDirection[i, j + 1] == 5) teDirection = "Gravity";
                    if (Shell.LoadDirection[i, j + 1] == 6) teDirection = "Global-X-Proj";
                    if (Shell.LoadDirection[i, j + 1] == 7) teDirection = "Global-Y-Proj";
                    if (Shell.LoadDirection[i, j + 1] == 8) teDirection = "Global-Z-Proj";
                    emp.Direction = teDirection;
                    emp.Uniform = Shell.LoadUniform[i, j + 1].ToString();
                    this.forces.Add(emp);
                    emps[j] = emp;
                }
            }
            [TypeConverter(typeof(ForceCollectionConverter))]
            public ForceCollection Distributed
            {
                get { return forces; }
            }
        }
        [TypeConverter(typeof(ForceConverter))]
        public class Force
        {
            private string load_Type = "";
            private string load_Uniform = "";
            private string load_Direction = "";
            private string load_Pattern = "";
            public Force()
            {
            }
            [Category("Required")]
            [ReadOnly(true)]
            public string Pattern
            {
                get { return load_Pattern; }
                set { load_Pattern = value; }
            }
            [Category("Required")]
            [ReadOnly(true)]
            public string Direction
            {
                get { return load_Direction; }
                set { load_Direction = value; }
            }
            [Category("Required")]
            [ReadOnly(true)]
            public string Uniform
            {
                get { return load_Uniform; }
                set { load_Uniform = value; }
            }
            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(this.load_Type);
                sb.Append(",");
                sb.Append(this.load_Uniform);
                sb.Append(",");
                sb.Append(this.load_Direction);
                sb.Append(",");
                sb.Append(this.load_Pattern);
                return sb.ToString();
            }
        }
        internal class ForceConverter : ExpandableObjectConverter
        {
            public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destType)
            {
                if (destType == typeof(string) && value is Force)
                {
                    Force emp = (Force)value;
                    return "";//emp.load_Value1 + ", " + emp.load_Value2 + ", " + emp.load_Value3 + ", " + emp.load_Value4 + ", " + emp.load_Distance1 + ", " + emp.load_Distance2 + ", " + emp.load_Distance3 + ", " + emp.load_Distance4;
                }
                return base.ConvertTo(context, culture, value, destType);
            }
        }
        internal class ForceCollectionConverter : ExpandableObjectConverter
        {
            public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destType)
            {
                if (destType == typeof(string) && value is ForceCollection)
                {
                    return ""; //"Force";
                }
                return base.ConvertTo(context, culture, value, destType);
            }
        }
        public class ForceCollectionPropertyDescriptor : PropertyDescriptor
        {
            private ForceCollection collection = null;
            private int index = -1;
            public ForceCollectionPropertyDescriptor(ForceCollection coll, int idx) :
                base("#" + idx.ToString(), null)
            {
                this.collection = coll;
                this.index = idx;
            }
            public override AttributeCollection Attributes
            {
                get
                {
                    return new AttributeCollection(null);
                }
            }
            public override bool CanResetValue(object component)
            {
                return true;
            }
            public override Type ComponentType
            {
                get
                {
                    return this.collection.GetType();
                }
            }
            public override string DisplayName
            {
                get
                {
                    Force emp = this.collection[index];
                    //return emp.FirstName + " " + emp.LastName;
                    return "Distributed Force";
                }
            }
            public override string Description
            {
                get
                {
                    Force emp = this.collection[index];
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" ");
                    return sb.ToString();
                }
            }
            public override object GetValue(object component)
            {
                return this.collection[index];
            }
            public override bool IsReadOnly
            {
                get { return false; }
            }
            public override string Name
            {
                get { return "#" + index.ToString(); }
            }
            public override Type PropertyType
            {
                get { return this.collection[index].GetType(); }
            }
            public override void ResetValue(object component)
            {
            }
            public override bool ShouldSerializeValue(object component)
            {
                return true;
            }
            public override void SetValue(object component, object value)
            {
                // this.collection[index] = value;
            }
        }
        public class ForceCollection : CollectionBase, ICustomTypeDescriptor
        {
            #region collection impl

            /// <summary>
            /// Adds an Force object to the collection
            /// </summary>
            /// <param name="emp"></param>
            public void Add(Force emp)
            {
                this.List.Add(emp);
            }

            /// <summary>
            /// Removes an Force object from the collection
            /// </summary>
            /// <param name="emp"></param>
            public void Remove(Force emp)
            {
                this.List.Remove(emp);
            }

            /// <summary>
            /// Returns an Force object at index position.
            /// </summary>
            public Force this[int index]
            {
                get
                {
                    return (Force)this.List[index];
                }
            }

            #endregion

            // Implementation of interface ICustomTypeDescriptor 
            #region ICustomTypeDescriptor impl

            public String GetClassName()
            {
                return TypeDescriptor.GetClassName(this, true);
            }

            public AttributeCollection GetAttributes()
            {
                return TypeDescriptor.GetAttributes(this, true);
            }

            public String GetComponentName()
            {
                return TypeDescriptor.GetComponentName(this, true);
            }

            public TypeConverter GetConverter()
            {
                return TypeDescriptor.GetConverter(this, true);
            }

            public EventDescriptor GetDefaultEvent()
            {
                return TypeDescriptor.GetDefaultEvent(this, true);
            }

            public PropertyDescriptor GetDefaultProperty()
            {
                return TypeDescriptor.GetDefaultProperty(this, true);
            }

            public object GetEditor(Type editorBaseType)
            {
                return TypeDescriptor.GetEditor(this, editorBaseType, true);
            }

            public EventDescriptorCollection GetEvents(Attribute[] attributes)
            {
                return TypeDescriptor.GetEvents(this, attributes, true);
            }

            public EventDescriptorCollection GetEvents()
            {
                return TypeDescriptor.GetEvents(this, true);
            }

            public object GetPropertyOwner(PropertyDescriptor pd)
            {
                return this;
            }
            /// <summary>
            /// Called to get the properties of this type. Returns properties with certain
            /// attributes. this restriction is not implemented here.
            /// </summary>
            /// <param name="attributes"></param>
            /// <returns></returns>
            public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
            {
                return GetProperties();
            }

            /// <summary>
            /// Called to get the properties of this type.
            /// </summary>
            /// <returns></returns>
            public PropertyDescriptorCollection GetProperties()
            {
                // Create a collection object to hold property descriptors
                PropertyDescriptorCollection pds = new PropertyDescriptorCollection(null);

                // Iterate the list of forces
                for (int i = 0; i < this.List.Count; i++)
                {
                    // Create a property descriptor for the Force item and add to the property descriptor collection
                    ForceCollectionPropertyDescriptor pd = new ForceCollectionPropertyDescriptor(this, i);
                    pds.Add(pd);
                }
                // return the property descriptor collection
                return pds;
            }

            #endregion
        }
        #endregion
        #region//جدول لبجيومتري
        public class GeometryTable
        {
            FPointCollection FPoints = new FPointCollection();
            FPoint[] emps = new FPoint[Shell.PointNumbers[Shell.SelectedforProp]];
            int i = Shell.SelectedforProp;
            public GeometryTable()
            {
                for (int j = 0; j < Shell.PointNumbers[i]; j++)
                {
                    FPoint emp = new FPoint();
                    int k = Shell.JointNo[i, j + 1];
                    emp.NO = k.ToString();
                    emp.Coordinate = Joint.XReal[k] + "; " + Joint.YReal[k] + "; " + Joint.ZReal[k];
                    this.FPoints.Add(emp);
                    emps[j] = emp;
                }
            }
            [TypeConverter(typeof(FPointCollectionConverter))]
            public FPointCollection Joints
            {
                get { return FPoints; }
            }
            private double aria;
            private double centroidX;
            private double centroidY;
            private double perimeter;
            [Category("Geometry")]
            [Description(" ")]
            [DisplayName("Aria")]
            [ReadOnly(true)]
            public double Aria
            {
                get { return aria; }

                set { aria = value; }
            }
            [Category("Geometry")]
            [Description(" ")]
            [DisplayName("Centroid X")]
            [ReadOnly(true)]
            public double CentroidX
            {
                get { return centroidX; }

                set { centroidX = value; }
            }
            [Category("Geometry")]
            [Description(" ")]
            [DisplayName("Centroid Y")]
            [ReadOnly(true)]
            public double CentroidY
            {
                get { return centroidY; }

                set { centroidY = value; }
            }
            [Category("Geometry")]
            [Description(" ")]
            [DisplayName("Perimeter")]
            [ReadOnly(true)]
            public double Perimeter
            {
                get { return perimeter; }

                set { perimeter = value; }
            }
        }
        [TypeConverter(typeof(ForceConverter))]
        public class FPoint
        {
            private string joint_No = "";
            private string joint_Co = "";
            public FPoint()
            {
            }
            [Category("Required")]
            [ReadOnly(true)]
            public string NO
            {
                get { return joint_No; }
                set { joint_No = value; }
            }
            [Category("Required")]
            [ReadOnly(true)]
            public string Coordinate
            {
                get { return joint_Co; }
                set { joint_Co = value; }
            }
            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(this.joint_No);
                sb.Append(",");
                sb.Append(this.joint_Co);
                return sb.ToString();
            }
        }
        internal class FPointConverter : ExpandableObjectConverter
        {
            public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destType)
            {
                if (destType == typeof(string) && value is FPoint)
                {
                    FPoint emp = (FPoint)value;
                    return "";
                }
                return base.ConvertTo(context, culture, value, destType);
            }
        }
        internal class FPointCollectionConverter : ExpandableObjectConverter
        {
            public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destType)
            {
                string thereturned = "";
                if (destType == typeof(string) && value is FPointCollection)
                {
                    for (int j = 1; j < Shell.PointNumbers[Shell.SelectedforProp]; j++)
                    {
                        thereturned = thereturned + Shell.JointNo[Shell.SelectedforProp, j] + "; ";
                    }
                    thereturned = thereturned + Shell.JointNo[Shell.SelectedforProp, Shell.PointNumbers[Shell.SelectedforProp]];
                    return thereturned;
                }
                return base.ConvertTo(context, culture, value, destType);
            }
        }
        public class FPointCollectionPropertyDescriptor : PropertyDescriptor
        {
            private FPointCollection collection = null;
            private int index = -1;
            public FPointCollectionPropertyDescriptor(FPointCollection coll, int idx) :
                base("#" + idx.ToString(), null)
            {
                this.collection = coll;
                this.index = idx;
            }
            public override AttributeCollection Attributes
            {
                get
                {
                    return new AttributeCollection(null);
                }
            }
            public override bool CanResetValue(object component)
            {
                return true;
            }
            public override Type ComponentType
            {
                get
                {
                    return this.collection.GetType();
                }
            }
            public override string DisplayName
            {
                get
                {
                    FPoint emp = this.collection[index];
                    return "Joint";
                }
            }
            public override string Description
            {
                get
                {
                    FPoint emp = this.collection[index];
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" ");
                    return sb.ToString();
                }
            }
            public override object GetValue(object component)
            {
                return this.collection[index];
            }
            public override bool IsReadOnly
            {
                get { return false; }
            }
            public override string Name
            {
                get { return "#" + index.ToString(); }
            }
            public override Type PropertyType
            {
                get { return this.collection[index].GetType(); }
            }
            public override void ResetValue(object component)
            {
            }
            public override bool ShouldSerializeValue(object component)
            {
                return true;
            }
            public override void SetValue(object component, object value)
            {
                //collection[index] = "";
            }
        }
        public class FPointCollection : CollectionBase, ICustomTypeDescriptor
        {
            #region collection impl
            public void Add(FPoint emp)
            {
                this.List.Add(emp);
            }
            public void Remove(FPoint emp)
            {
                this.List.Remove(emp);
            }
            public FPoint this[int index]
            {
                get
                {
                    return (FPoint)this.List[index];
                }
            }
            #endregion
            #region ICustomTypeDescriptor impl
            public String GetClassName()
            {
                return TypeDescriptor.GetClassName(this, true);
            }
            public AttributeCollection GetAttributes()
            {
                return TypeDescriptor.GetAttributes(this, true);
            }
            public String GetComponentName()
            {
                return TypeDescriptor.GetComponentName(this, true);
            }
            public TypeConverter GetConverter()
            {
                return TypeDescriptor.GetConverter(this, true);
            }
            public EventDescriptor GetDefaultEvent()
            {
                return TypeDescriptor.GetDefaultEvent(this, true);
            }
            public PropertyDescriptor GetDefaultProperty()
            {
                return TypeDescriptor.GetDefaultProperty(this, true);
            }
            public object GetEditor(Type editorBaseType)
            {
                return TypeDescriptor.GetEditor(this, editorBaseType, true);
            }
            public EventDescriptorCollection GetEvents(Attribute[] attributes)
            {
                return TypeDescriptor.GetEvents(this, attributes, true);
            }
            public EventDescriptorCollection GetEvents()
            {
                return TypeDescriptor.GetEvents(this, true);
            }
            public object GetPropertyOwner(PropertyDescriptor pd)
            {
                return this;
            }
            public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
            {
                return GetProperties();
            }
            public PropertyDescriptorCollection GetProperties()
            {
                PropertyDescriptorCollection pds = new PropertyDescriptorCollection(null);
                for (int i = 0; i < this.List.Count; i++)
                {
                    FPointCollectionPropertyDescriptor pd = new FPointCollectionPropertyDescriptor(this, i);
                    pds.Add(pd);
                }
                return pds;
            }
            #endregion
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
