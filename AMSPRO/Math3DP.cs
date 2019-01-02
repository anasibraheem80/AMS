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
    class Math3DP
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
            public double X = 0;
            public double Y = 0;
            public double Z = 0;
            POINT points;
            public PointG(double x, double y, double z)// معرف الاحداثيات
            {
                X = x;
                Y = y;
                Z = z;
                points = new POINT();
                points.Coo3D = new Vector3D(X - Myglobals.RotatePointX3d, Y + Myglobals.RotatePointY3d, Z + Myglobals.RotatePointZ3d);
            }
            public double RotateX
            {
                get { return 0; }
                set
                {
                    RotatePointX(value);
                }
            }
            public double RotateY
            {
                get { return 0; }
                set
                {
                    RotatePointY(value);
                }
            }
            public double RotateZ
            {
                get { return 0; }
                set
                {
                    RotatePointZ(value);
                }
            }
            private void RotatePointX(double deltaX)
            {
                if (Myglobals.Tadweer == 1 || Myglobals.Tadweer == 3)
                {
                    points.Coo3D = Math3DP.RotateX(points.Coo3D, deltaX);
                }
                if (Myglobals.Tadweer == 2 || Myglobals.Tadweer == 4)
                {
                    points.Coo3D = Math3DP.RotateY(points.Coo3D, deltaX);
                }
            }
            private void RotatePointY(double deltaY)
            {
                if (Myglobals.Tadweer == 1 || Myglobals.Tadweer == 3)
                {
                    points.Coo3D = Math3DP.RotateY(points.Coo3D, deltaY);
                }
                if (Myglobals.Tadweer == 2 || Myglobals.Tadweer == 4)
                {
                    points.Coo3D = Math3DP.RotateX(points.Coo3D, deltaY);
                }
            }
            private void RotatePointZ(double deltaZ)
            {
                points.Coo3D = Math3DP.RotateZ(points.Coo3D, deltaZ);
            }
            //Calculates the 2D points 
            public void DrawPoint()
            {
                double zValue = (points.Coo3D.z + Myglobals.Aperture);
                Myglobals.TheX3d = (int)(Myglobals.startX3d + ((points.Coo3D.x) / zValue) * Myglobals.Zoom3d * 50);
                Myglobals.TheY3d = (int)(Myglobals.startY3d + ((points.Coo3D.y) / zValue) * Myglobals.Zoom3d * 50);
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
            if (Myglobals.Tadweer == 1 || Myglobals.Tadweer == 2)//-------------------------------
            {
                double cDegrees = (degrees) * PIOVER180;
                double cosDegrees = Math.Cos(cDegrees);
                double sinDegrees = Math.Sin(cDegrees);
                y = (point3D.y * cosDegrees) + (point3D.z * sinDegrees);
                z = (point3D.y * -sinDegrees) + (point3D.z * cosDegrees);
            }
            if (Myglobals.Tadweer == 3 || Myglobals.Tadweer == 4)
            {
                double cDegrees = (degrees) * PIOVER180;
                double cosDegrees = Math.Cos(cDegrees);
                double sinDegrees = Math.Sin(cDegrees);
                y = -(point3D.y * cosDegrees) + (point3D.z * sinDegrees);
                z = -(point3D.y * -sinDegrees) + (point3D.z * cosDegrees);
            }
            return new Vector3D(point3D.x, y, z);
        }
        public static Vector3D RotateY(Vector3D point3D, double degrees)
        {
            //[ cos(x)   0    sin(x)]
            //[   0      1      0   ]
            //[-sin(x)   0    cos(x)]
            double x = 0;
            double z = 0;

            if (Myglobals.Tadweer == 1 || Myglobals.Tadweer == 2)//---------------------------------
            {
                double cDegrees = (degrees) * PIOVER180;
                double cosDegrees = Math.Cos(cDegrees);
                double sinDegrees = Math.Sin(cDegrees);
                x = (point3D.x * cosDegrees) + (point3D.z * sinDegrees);
                z = (point3D.x * -sinDegrees) + (point3D.z * cosDegrees);
            }
            if (Myglobals.Tadweer == 3 || Myglobals.Tadweer == 4)
            {
                double cDegrees = (degrees) * PIOVER180;
                double cosDegrees = Math.Cos(cDegrees);
                double sinDegrees = Math.Sin(cDegrees);
                x = -(point3D.x * cosDegrees) + (point3D.z * sinDegrees);
                z = -(point3D.x * -sinDegrees) + (point3D.z * cosDegrees);
            }
            return new Vector3D(x, point3D.y, z);
        }
        public static Vector3D RotateZ(Vector3D point3D, double degrees)
        {
            //[ cos(x)  sin(x) 0]
            //[ -sin(x) cos(x) 0]
            //[    0     0     1]
            double x = 0;
            double y = 0;
            if (Myglobals.Tadweer == 2 || Myglobals.Tadweer == 4)//------------------------
            {
                double cDegrees = (degrees) * PIOVER180;
                double cosDegrees = Math.Cos(cDegrees);
                double sinDegrees = Math.Sin(cDegrees);
                y = -(point3D.x * cosDegrees) + (point3D.y * sinDegrees);
                x = -(point3D.x * -sinDegrees) + (point3D.y * cosDegrees);
            }
            if (Myglobals.Tadweer == 1 || Myglobals.Tadweer == 3)
            {
                double cDegrees = (degrees) * PIOVER180;
                double cosDegrees = Math.Cos(cDegrees);
                double sinDegrees = Math.Sin(cDegrees);
                x = (point3D.x * cosDegrees) + (point3D.y * sinDegrees);
                y = (point3D.x * -sinDegrees) + (point3D.y * cosDegrees);
            }
            return new Vector3D(x, y, point3D.z);
        }
    }
}
