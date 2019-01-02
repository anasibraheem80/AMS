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
    public partial class BeamInformationForm : Form
    {
        public BeamInformationForm()
        {
            InitializeComponent();
        }
        Form mainForm = Application.OpenForms["MainForm"];

        public DynamicCustomTypeDescriptor m_dctd = null;
        string firstjointSTR;
        string secondjointSTR;
        
        private void BeamInformationForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = Frame.SelectedforProp.ToString();
            int i = Frame.SelectedforProp;
            //-------------------------------------------------------
            int firstjoint = ((MainForm)mainForm).FrameElement[i].FirstJoint;
            int secondjoint = ((MainForm)mainForm).FrameElement[i].SecondJoint;
            double XReal1 = Joint.XReal[firstjoint];
            double YReal1 = Joint.YReal[firstjoint];
            double ZReal1 = Joint.ZReal[firstjoint];
            double XReal2 = Joint.XReal[secondjoint];
            double YReal2 = Joint.YReal[secondjoint];
            double ZReal2 = Joint.ZReal[secondjoint];
            double length = Math.Round(Math.Sqrt(Math.Pow(XReal1 - XReal2, 2) + Math.Pow(YReal1 - YReal2, 2) + Math.Pow(ZReal1 - ZReal2, 2)), 3);
            firstjointSTR = XReal1 + "; " + YReal1 + "; " + ZReal1;
            secondjointSTR = XReal2 + "; " + YReal2 + "; " + ZReal2;
            propertyGrid1.Dock = DockStyle.Fill;
            MyFunkyType bill1 = new MyFunkyType();
            bill1.FirstJoint = firstjointSTR;
            bill1.SecondJoint = secondjointSTR;
            bill1.Length = length.ToString();
            propertyGrid1.SelectedObject = bill1;
            //--------------------------
            ForcesTable forcesTable = new ForcesTable();
            propertyGrid3.SelectedObject = forcesTable;
            //--------------------------
            AssignmentTable assignmentTable = new AssignmentTable();
            assignmentTable.sectionproberty = Section.LABEL[((MainForm)mainForm).FrameElement[i].Section];
            propertyGrid2.SelectedObject = assignmentTable;
        }
        #region//جدول المواصفات
        public class AssignmentTable
        {
            [Category("Assignments")]
            [Description("Section property assigned to the fram object")]
            [DisplayName("Section property")]
            [ReadOnly(true)]
            public string sectionproberty { get; set; }
        }
        #endregion
        #region//جدول العقد
        class MyFunkyTypeConverter : ExpandableObjectConverter
        {
            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
            {
                PropertyDescriptorCollection props = base.GetProperties(context, value, attributes);
                List<PropertyDescriptor> list = new List<PropertyDescriptor>(props.Count);
                foreach (PropertyDescriptor prop in props)
                {
                    switch (prop.Name)
                    {
                        case "FirstJoint":
                            list.Add(new DisplayNamePropertyDescriptor(
                                prop, "End I: Joint " + "((MainForm)mainForm).FrameElement[Frame.SelectedforProp].FirstJoint"));
                            break;
                        case "SecondJoint":
                            list.Add(new DisplayNamePropertyDescriptor(
                                prop, "End J: Joint " + "((MainForm)mainForm).FrameElement[Frame.SelectedforProp].SecondJoint"));
                            break;
                        default:
                            list.Add(prop);
                            break;
                    }
                }
                return new PropertyDescriptorCollection(list.ToArray(), true);
            }
        }
        class DisplayNamePropertyDescriptor : PropertyDescriptor
        {
            private readonly string displayName;
            private readonly PropertyDescriptor parent;
            public DisplayNamePropertyDescriptor(
                PropertyDescriptor parent, string displayName)
                : base(parent)
            {
                this.displayName = displayName;
                this.parent = parent;
            }
            public override string DisplayName
            { get { return displayName; } }
            public override bool ShouldSerializeValue(object component)
            { return parent.ShouldSerializeValue(component); }
            public override void SetValue(object component, object value)
            {
                parent.SetValue(component, value);
            }
            public override object GetValue(object component)
            {
                return parent.GetValue(component);
            }
            public override void ResetValue(object component)
            {
                parent.ResetValue(component);
            }
            public override bool CanResetValue(object component)
            {
                return parent.CanResetValue(component);
            }
            public override bool IsReadOnly
            {
                get { return parent.IsReadOnly; }
            }
            public override void AddValueChanged(object component, EventHandler handler)
            {
                parent.AddValueChanged(component, handler);
            }
            public override void RemoveValueChanged(object component, EventHandler handler)
            {
                parent.RemoveValueChanged(component, handler);
            }
            public override bool SupportsChangeEvents
            {
                get { return parent.SupportsChangeEvents; }
            }
            public override Type PropertyType
            {
                get { return parent.PropertyType; }
            }
            public override TypeConverter Converter
            {
                get { return parent.Converter; }
            }
            public override Type ComponentType
            {
                get { return parent.ComponentType; }
            }
            public override string Description
            {
                get { return parent.Description; }
            }
            public override PropertyDescriptorCollection GetChildProperties(object instance, Attribute[] filter)
            {
                return parent.GetChildProperties(instance, filter);
            }
            public override string Name
            {
                get { return parent.Name; }
            }
        }
        [TypeConverter(typeof(MyFunkyTypeConverter))]
        class MyFunkyType
        {
            [Category("Geometry")]
            [Description("joint object at the start (I) of the fram object")]
            [DisplayName("End I:Joint ")]
            [ReadOnly(true)]
            public string FirstJoint { get; set; }
            [Category("Geometry")]
            [Description("joint object at the end (J) of the fram object")]
            [DisplayName("End J:Joint ")]
            [ReadOnly(true)]
            public string SecondJoint { get; set; }
            [Category("Geometry")]
            [Description("The full length of the fram object")]
            [DisplayName("Length")]
            [ReadOnly(true)]
            public string Length { get; set; }
        }
        #endregion
        #region//جدول القوى
        public class ForcesTable
        {
            ForceCollection forces = new ForceCollection();
          //  Force[] emps = new Force[((MainForm)mainForm).FrameElement[Frame.SelectedforProp]].LoadDNumber;
           
            ForceCollectionP forcesP = new ForceCollectionP();
         //   ForceP[] empsP = new ForceP[((MainForm)mainForm).FrameElement[Frame.SelectedforProp].LoadPNumber];

            int i = Frame.SelectedforProp;
            public ForcesTable()
            {
                int firstjoint = 1;//((MainForm)mainForm).FrameElement[i].FirstJoint;
                int secondjoint = 1;//((MainForm)mainForm).FrameElement[i].SecondJoint;
                double XReal1 = Joint.XReal[firstjoint];
                double YReal1 = Joint.YReal[firstjoint];
                double ZReal1 = Joint.ZReal[firstjoint];
                double XReal2 = Joint.XReal[secondjoint];
                double YReal2 = Joint.YReal[secondjoint];
                double ZReal2 = Joint.ZReal[secondjoint];
                double length = Math.Round(Math.Sqrt(Math.Pow(XReal1 - XReal2, 2) + Math.Pow(YReal1 - YReal2, 2) + Math.Pow(ZReal1 - ZReal2, 2)), 3);
              //  for (int j = 0; j < ((MainForm)mainForm).FrameElement[Frame.SelectedforProp].LoadDNumber; j++)
               for (int j = 0; j <55; j++)
 
                {
                    Myglobals.POWERTYPE = 0;
                    Force emp = new Force();
                  //  emp.Pattern = Loads.Load[((MainForm)mainForm).FrameElement[i].LoadDPattern[j + 1] + 1];
                    string teType = "";
               //     if (((MainForm)mainForm).FrameElement[i].LoadDType[j+1]== 1) teType = "Force";
              //      if (((MainForm)mainForm).FrameElement[i].LoadDType[j+1]== 2) teType = "Moment";
                    emp.Type = teType; ;
                    string teDirection = "";
              //      if (((MainForm)mainForm).FrameElement[i].LoadDDirection[j+1]== 0) teDirection = "Local-1";
             //       if (((MainForm)mainForm).FrameElement[i].LoadDDirection[j+1]== 1) teDirection = "Local-2";
              //      if (((MainForm)mainForm).FrameElement[i].LoadDDirection[j+1]== 2) teDirection = "Local-3";
              //      if (((MainForm)mainForm).FrameElement[i].LoadDDirection[j+1]== 3) teDirection = "Global-X";
              //      if (((MainForm)mainForm).FrameElement[i].LoadDDirection[j+1]== 4) teDirection = "Global-Y";
               //     if (((MainForm)mainForm).FrameElement[i].LoadDDirection[j+1]== 5) teDirection = "Gravity";
               //     if (((MainForm)mainForm).FrameElement[i].LoadDDirection[j+1]== 6) teDirection = "Global-X-Proj";
               //     if (((MainForm)mainForm).FrameElement[i].LoadDDirection[j+1]== 7) teDirection = "Global-Y-Proj";
                //    if (((MainForm)mainForm).FrameElement[i].LoadDDirection[j+1]== 8) teDirection = "Global-Z-Proj";
                    emp.Direction = teDirection;
                //    emp.Uniform = Frame.LoadDUniform[i, j + 1].ToString();
                   // emp.Value1 = ((MainForm)mainForm).FrameElement[i].LoadDValue1[j + 1].ToString();
                //    emp.Value2 = ((MainForm)mainForm).FrameElement[i].LoadDValue2[j + 1].ToString();
                  //  emp.Value3 = ((MainForm)mainForm).FrameElement[i].LoadDValue3[j + 1].ToString();
                  //  emp.Value4 = ((MainForm)mainForm).FrameElement[i].LoadDValue4[j + 1].ToString();
                  //  emp.Distance1 = Math.Round(((MainForm)mainForm).FrameElement[i].LoadDDistance1[j + 1] * length, 3).ToString();
                  //  emp.Distance2 = Math.Round(((MainForm)mainForm).FrameElement[i].LoadDDistance2[j + 1] * length, 3).ToString();
                  //  emp.Distance3 = Math.Round(((MainForm)mainForm).FrameElement[i].LoadDDistance3[j + 1] * length, 3).ToString();
                   // emp.Distance4 = Math.Round(((MainForm)mainForm).FrameElement[i].LoadDDistance4[j + 1] * length, 3).ToString();
                    this.forces.Add(emp);
                    //emps[j] = emp;
                }

              // for (int j = 0; j < ((MainForm)mainForm).FrameElement[Frame.SelectedforProp].LoadPNumber; j++)
               for (int j = 0; j < 55; j++)
               {
                    Myglobals.POWERTYPE = 1;
                    ForceP empP = new ForceP();
                  //  empP.Pattern = Loads.Load[((MainForm)mainForm).FrameElement[i].LoadPPattern[j+1] + 1];
                    string teType = "";
                 //   if (((MainForm)mainForm).FrameElement[i].LoadPType[j+1] == 1) teType = "Force";
                //    if (((MainForm)mainForm).FrameElement[i].LoadPType[j+1] == 2) teType = "Moment";
                    empP.Type = teType; ;
                    string teDirection = "";
                 //   if (((MainForm)mainForm).FrameElement[i].LoadPDirection[j+1] == 0) teDirection = "Local-1";
                 //   if (((MainForm)mainForm).FrameElement[i].LoadPDirection[j+1] == 1) teDirection = "Local-2";
                 //   if (((MainForm)mainForm).FrameElement[i].LoadPDirection[j+1] == 2) teDirection = "Local-3";
                 //   if (((MainForm)mainForm).FrameElement[i].LoadPDirection[j+1] == 3) teDirection = "Global-X";
                 //   if (((MainForm)mainForm).FrameElement[i].LoadPDirection[j+1] == 4) teDirection = "Global-Y";
                 //   if (((MainForm)mainForm).FrameElement[i].LoadPDirection[j+1] == 5) teDirection = "Gravity";
                 //   if (((MainForm)mainForm).FrameElement[i].LoadPDirection[j+1] == 6) teDirection = "Global-X-Proj";
                 //   if (((MainForm)mainForm).FrameElement[i].LoadPDirection[j+1] == 7) teDirection = "Global-Y-Proj";
                 //   if (((MainForm)mainForm).FrameElement[i].LoadPDirection[j+1] == 8) teDirection = "Global-Z-Proj";
                    empP.Direction = teDirection;
                  //  empP.Value = ((MainForm)mainForm).FrameElement[i].LoadPValue[j+1].ToString();
                 //   empP.Distance = Math.Round(((MainForm)mainForm).FrameElement[i].LoadPDistance[j+1] * length, 3).ToString();
                    this.forcesP.Add(empP);
                    //empsP[j] = empP;
                }
            }
            [TypeConverter(typeof(ForceCollectionConverter))]
            public ForceCollection Distributed
            {
                get { return forces; }
            }
            [TypeConverter(typeof(ForceCollectionConverterP))]
            public ForceCollectionP Point
            {
                get { return forcesP; }
            }
        }
        [TypeConverter(typeof(ForceConverter))]
        public class Force
        {
            private string load_Value1 = "";
            private string load_Value2 = "";
            private string load_Value3 = "";
            private string load_Value4 = "";
            private string load_Distance1 = "";
            private string load_Distance2 = "";
            private string load_Distance3 = "";
            private string load_Distance4 = "";
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
            public string Type
            {
                get { return load_Type; }
                set { load_Type = value; }
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
            [Category("Required")]
            [ReadOnly(true)]
            public string Value1
            {
                get { return load_Value1; }
                set { load_Value1 = value; }
            }
            [Category("Required")]
            [ReadOnly(true)]
            public string Distance1
            {
                get { return load_Distance1; }
                set { load_Distance1 = value; }
            }
            [Category("Required")]
            [ReadOnly(true)]
            public string Value2
            {
                get { return load_Value2; }
                set { load_Value2 = value; }
            }
            [Category("Required")]
            [ReadOnly(true)]
            public string Distance2
            {
                get { return load_Distance2; }
                set { load_Distance2 = value; }
            }
            [Category("Required")]
            [ReadOnly(true)]
            public string Value3
            {
                get { return load_Value3; }
                set { load_Value3 = value; }
            }
            [Category("Required")]
            [ReadOnly(true)]
            public string Distance3
            {
                get { return load_Distance3; }
                set { load_Distance3 = value; }
            }
            [Category("Required")]
            [ReadOnly(true)]
            public string Value4
            {
                get { return load_Value4; }
                set { load_Value4 = value; }
            }
            [Category("Required")]
            [ReadOnly(true)]
            public string Distance4
            {
                get { return load_Distance4; }
                set { load_Distance4 = value; }
            }
            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(this.load_Value1);
                sb.Append(",");
                sb.Append(this.load_Value2);
                sb.Append(",");
                sb.Append(this.load_Value3);
                sb.Append(",");
                sb.Append(this.load_Value4);
                sb.Append(",");
                sb.Append(this.load_Distance1);
                sb.Append(",");
                sb.Append(this.load_Distance2);
                sb.Append(",");
                sb.Append(this.load_Distance3);
                sb.Append(",");
                sb.Append(this.load_Distance4);
                sb.Append(",");
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
                    return "Distributed Load";
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
    
        [TypeConverter(typeof(ForceConverterP))]
        public class ForceP
        {
            private string load_Value1 = "";
            private string load_Distance1 = "";
            private string load_Type = "";
            private string load_Direction = "";
            private string load_Pattern = "";
            public ForceP()
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
            public string Type
            {
                get { return load_Type; }
                set { load_Type = value; }
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
            public string Value
            {
                get { return load_Value1; }
                set { load_Value1 = value; }
            }
            [Category("Required")]
            [ReadOnly(true)]
            public string Distance
            {
                get { return load_Distance1; }
                set { load_Distance1 = value; }
            }
            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(this.load_Value1);
                sb.Append(",");
                sb.Append(this.load_Distance1);
                sb.Append(",");
                sb.Append(this.load_Type);
                sb.Append(",");
                sb.Append(this.load_Direction);
                sb.Append(",");
                sb.Append(this.load_Pattern);
                return sb.ToString();
            }
        }
        internal class ForceConverterP : ExpandableObjectConverter
        {
            public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destType)
            {
                if (destType == typeof(string) && value is ForceP)
                {
                    ForceP empP = (ForceP)value;
                    return "";
                }
                return base.ConvertTo(context, culture, value, destType);
            }
        }
        internal class ForceCollectionConverterP : ExpandableObjectConverter
        {
            public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destType)
            {
                if (destType == typeof(string) && value is ForceCollectionP)
                {
                    return ""; 
                }
                return base.ConvertTo(context, culture, value, destType);
            }
        }
        public class ForceCollectionPropertyDescriptorP : PropertyDescriptor
        {
            private ForceCollectionP collection = null;
            private int index = -1;
            public ForceCollectionPropertyDescriptorP(ForceCollectionP coll, int idx) :
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
                    ForceP empP = this.collection[index];
                    return "Point Load";
                }
            }
            public override string Description
            {
                get
                {
                    ForceP empP = this.collection[index];
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
        public class ForceCollectionP : CollectionBase, ICustomTypeDescriptor
        {
            #region collection impl
            public void Add(ForceP empP)
            {
                this.List.Add(empP);
            }
            public void Remove(ForceP empP)
            {
                this.List.Remove(empP);
            }
            public ForceP this[int index]
            {
                get
                {
                    return (ForceP)this.List[index];
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
                    ForceCollectionPropertyDescriptorP pd = new ForceCollectionPropertyDescriptorP(this, i);
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
