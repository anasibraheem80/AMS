using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace AMSPRO
{
    class Math3DP1
    {
        const double PIOVER180 = Math.PI / 180;
        #region//الشعاع D3
        public class Vector3D//الشعاع 3د
        {
            public double x;// يأخد إحداثيات تكون معرفة في 
            public double y;
            public double z;
            public Vector3D(double _x, double _y, double _z)
            {
                x = (double)_x;
                y = (double)_y;
                z = (double)_z;
            }
            public Vector3D()
            {
            }
        }
        #endregion
        #region//النقطة
        public class PointG
        {
            internal class POINT
            {
                public Vector3D Coo3D;
                public POINT()
                {
                }
            }
            POINT points;
            public PointG(double X, double Y, double Z)// معرف الاحداثيات
            {
                points = new POINT();
                points.Coo3D = new Vector3D(X - Myglobals.ShellCenterX, Y - Myglobals.ShellCenterY, Z - Myglobals.ShellCenterZ);
            }
            public void RotatePointX(double deltaX)
            {
                points.Coo3D = Math3DP1.RotateX(points.Coo3D, deltaX);
            }
            public void RotatePointY(double deltaY)
            {
                points.Coo3D = Math3DP1.RotateY(points.Coo3D, deltaY);
            }
            public void RotatePointZ(double deltaZ)
            {
                points.Coo3D = Math3DP1.RotateZ(points.Coo3D, deltaZ);
            }
            //Calculates the 2D points 
            public void DrawPoint()
            {
                double zValue =points.Coo3D.z ;
                Myglobals.ShellPointX = points.Coo3D.x / 1;
                Myglobals.ShellPointY = points.Coo3D.y / 1;
            }
        }
        //------------------------------------------------------------------------------------------- 
        #endregion
        public static Vector3D RotateX(Vector3D point3D, double degrees)
        {
            //[ a  b  c ] [ x ]   [ x*a + y*b + z*c ]
            //[ d  e  f ] [ y ] = [ x*d + y*e + z*f ]
            //[ g  h  i ] [ z ]   [ x*g + y*h + z*i ]
            //[ 1    0        0   ]
            //[ 0   cos(x)  sin(x)]
            //[ 0   -sin(x) cos(x)]
            double y = 0;
            double z = 0;
            double cDegrees = (degrees) * PIOVER180;
            double cosDegrees = Math.Cos(cDegrees);
            double sinDegrees = Math.Sin(cDegrees);
            y = (point3D.y * cosDegrees) + (point3D.z * sinDegrees);
            z = (point3D.y * -sinDegrees) + (point3D.z * cosDegrees);
            return new Vector3D(point3D.x, y, z);
        }
        public static Vector3D RotateY(Vector3D point3D, double degrees)
        {
            //[ cos(x)   0    sin(x)]
            //[   0      1      0   ]
            //[-sin(x)   0    cos(x)]
            double x = 0;
            double z = 0;
            double cDegrees = (degrees) * PIOVER180;
            double cosDegrees = Math.Cos(cDegrees);
            double sinDegrees = Math.Sin(cDegrees);
            x = (point3D.x * cosDegrees) + (point3D.z * sinDegrees);
            z = (point3D.x * -sinDegrees) + (point3D.z * cosDegrees);
            return new Vector3D(x, point3D.y, z);
        }
        public static Vector3D RotateZ(Vector3D point3D, double degrees)
        {
            //[ cos(x)  sin(x) 0]
            //[ -sin(x) cos(x) 0]
            //[    0     0     1]
            double x = 0;
            double y = 0;
            double cDegrees = (degrees) * PIOVER180;
            double cosDegrees = Math.Cos(cDegrees);
            double sinDegrees = Math.Sin(cDegrees);
            x = (point3D.x * cosDegrees) + (point3D.y * sinDegrees);
            y = (point3D.x * -sinDegrees) + (point3D.y * cosDegrees);
            return new Vector3D(x, y, point3D.z);
        }
    }
}
