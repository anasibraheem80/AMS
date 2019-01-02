using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMSPRO
{
    class Class1
    {
        #region//تعريفات
        int INTERSECTION = 0;
        double TheX0 = 0;
        double TheY0 = 0;
        int forarias = 0;
        double Distance = 0;

        public int ShapePoints = 0;
        public double[] ShapePointX = new double[1000];
        public double[] ShapePointY = new double[1000];

        public int TiePoints = 0;
        public double[] TiePointX = new double[1000];
        public double[] TiePointY = new double[1000];

        public int AllLine = 0;
        public double[] LineX1Real = new double[1000];
        public double[] LineX2Real = new double[1000];
        public double[] LineY1Real = new double[1000];
        public double[] LineY2Real = new double[1000];

        public int PointNumber2d = 0;
        public double[] PointXReal = new double[1000];
        public double[] PointYReal = new double[1000];

        public int AriaNo = 0;
        public int[] AriaPointNo = new int[1000];
        public double[,] AriaPointX = new double[1000, 100];
        public double[,] AriaPointY = new double[1000, 100];

        public int AriaNoF = 0;
        public int[] AriaPointNoF = new int[1000];
        public int[] AriaTypeF = new int[1000];
        public double[,] AriaPointXF = new double[1000, 100];
        public double[,] AriaPointYF = new double[1000, 100];

        public double[] AriaAriaF = new double[1000];
        public double[] AriaCenterXF = new double[1000];
        public double[] AriaCenterYF = new double[1000];

        bool result = false;
        double Ytest = 0;
        double Xtest = 0;
        #endregion

        public void DoMeshPlease()
        {
            forarias = 1;
            CalculateIntersections();
            forarias = 1;
            AriaNo = 0;
            AriaPointNo = new int[PointNumber2d + 2];
            AriaPointX = new double[PointNumber2d + 2, PointNumber2d + 2];
            AriaPointY = new double[PointNumber2d + 2, PointNumber2d + 2];
            FindArias();
            FindArias1();
            //  FindINArias();
            //   FindAriasType();
            //   CalculateAriaProperty();
        }
        private void CalculateIntersections()
        {
            forarias = 0;
            int m = 0;
            int n = AllLine;
            for (int i = 1; i < n + 1; i++)
            {
                double X1 = LineX1Real[i];
                double Y1 = LineY1Real[i];
                double X2 = LineX2Real[i];
                double Y2 = LineY2Real[i];
                for (int j = 1; j < n + 1; j++)
                {
                    double X3 = LineX1Real[j];
                    double Y3 = LineY1Real[j];
                    double X4 = LineX2Real[j];
                    double Y4 = LineY2Real[j];
                    checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                    if (INTERSECTION == 0) goto Nextj;
                    for (int k = 1; k < m + 1; k++)
                    {
                        if (PointXReal[k] == TheX0 & PointYReal[k] == TheY0) goto Nextj;
                    }
                    m = m + 1;
                    PointXReal[m] = TheX0;
                    PointYReal[m] = TheY0;
                Nextj: { }
                }
            }
            int M = 0;
            for (int i = 1; i < n + 1; i++)
            {
                M = M + 1;
                double x0 = (LineX1Real[i]);
                double y0 = (LineY1Real[i]);
                for (int k = 1; k < m + 1; k++)
                {
                    if (PointXReal[k] == x0 & PointYReal[k] == y0) goto Nexti;
                }
                m = m + 1;
                PointXReal[m] = x0;
                PointYReal[m] = y0;
            Nexti: { }
            }
            for (int i = 1; i < n + 1; i++)
            {
                double x0 = (LineX2Real[i]);
                double y0 = (LineY2Real[i]);
                for (int k = 1; k < m + 1; k++)
                {
                    if (PointXReal[k] == x0 & PointYReal[k] == y0) goto Nexti;
                }
                m = m + 1;
                PointXReal[m] = x0;
                PointYReal[m] = y0;
            Nexti: { }
            }
            PointNumber2d = m;
            for (int i = 1; i < m + 1; i++)
            {
                //  PointXReal[i] =Math.Round (PointXReal[i],3);
                // PointYReal[i] = Math.Round(PointYReal[i], 3);
            }
        }
        private void FindArias()
        {
            #region//تعريفات
            double X1 = 0;
            double Y1 = 0;
            double X2 = 0;
            double Y2 = 0;
            double X3 = 0;
            double Y3 = 0;
            double X4 = 0;
            double Y4 = 0;
            double x = 0;
            double y = 0;
            int StartJoint = 0;
            int LastJoint = 0;
            int PointADD = 0;
            int m = 0;
            #endregion
            for (int i = 1; i < PointNumber2d + 1; i++)
            {
                int check0 = 0;
                double[] pX = new double[PointNumber2d + 2];
                double[] pY = new double[PointNumber2d + 2];
                int[] PointNO = new int[PointNumber2d + 1];
                X1 = PointXReal[i];
                Y1 = PointYReal[i];
                m = 1;
                pX[m] = X1;
                pY[m] = Y1;
                #region//ايجاد العقد التي تقع ضمن المساحة
                PointADD = 0;
                for (int j = 1; j < PointNumber2d + 1; j++)
                {
                    if (j != i)
                    {
                        X2 = PointXReal[j];
                        Y2 = PointYReal[j];
                        for (int k = 1; k < AllLine + 1; k++)
                        {
                            X3 = LineX1Real[k];
                            Y3 = LineY1Real[k];
                            X4 = LineX2Real[k];
                            Y4 = LineY2Real[k];
                            #region//التحقق من أن العقدتين على نفس المحور
                            x = X1;
                            y = Y1;
                            int chk1 = 0;
                            if (X3 == X4 & X3 == x)
                            {
                                if (y >= Y3 & y <= Y4) chk1 = 1;
                                if (y <= Y3 & y >= Y4) chk1 = 1;
                            }
                            if (Y3 == Y4 & Y3 == y)
                            {
                                if (x >= X3 & x <= X4) chk1 = 1;
                                if (x <= X3 & x >= X4) chk1 = 1;
                            }
                            if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk1 = 1;
                            x = X2;
                            y = Y2;
                            int chk2 = 0;
                            if (X3 == X4 & X3 == x)
                            {
                                if (y >= Y3 & y <= Y4) chk2 = 1;
                                if (y <= Y3 & y >= Y4) chk2 = 1;
                            }
                            if (Y3 == Y4 & Y3 == y)
                            {
                                if (x >= X3 & x <= X4) chk2 = 1;
                                if (x <= X3 & x >= X4) chk2 = 1;
                            }
                            if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk2 = 1;
                            if (chk1 == 1 & chk2 == 1)
                            {
                                goto nextk;
                            }
                            #endregion
                            checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                            if (INTERSECTION == 1) goto nextj;
                        nextk: { }
                        }
                        PointADD = PointADD + 1;
                        PointNO[PointADD] = j;
                    }
                nextj: { }
                }
                #endregion
                #region //ترتيب العقد عكس عقارب الساعة
                StartJoint = i;
                double[] aTanA = new double[PointADD + 1];
                for (int j = 1; j < PointADD + 1; j++)
                {
                    aTanA[j] = Math.Atan2(PointYReal[PointNO[j]] - PointYReal[StartJoint], PointXReal[PointNO[j]] - PointXReal[StartJoint]);
                    if (Math.Abs(aTanA[j] - Convert.ToDouble((Math.PI))) < 0.001) aTanA[j] = -1 * aTanA[j];
                }
                double[] Apower = new double[PointADD + 1];
                int[] Bpower = new int[PointADD + 1];
                double[] Cpower = new double[PointADD + 1];
                int[] Dpower = new int[PointADD + 1];
                int[] Epower = new int[PointADD + 1];
                int Row = 0;
                int N = PointADD;
                int NN = PointADD;
                double MAXI = +1000000;
                int MAXId = 0;
                for (int j = 1; j < N + 1; j++)
                {
                    Apower[j] = aTanA[j];
                    Bpower[j] = j;
                    Dpower[j] = PointNO[j];
                }
                for (int j = 1; j < N + 1; j++)
                {
                    MAXI = 1000000;
                    for (int k = 1; k < NN + 1; k++)
                    {
                        if (MAXI >= Apower[k])
                        {
                            MAXI = Apower[k];
                            MAXId = Dpower[k];
                            Row = Bpower[k];
                        }
                    }
                    Cpower[j] = MAXI;
                    Epower[j] = MAXId;
                    NN = NN - 1;
                    for (int k = Row; k < NN + 1; k++)
                    {
                        Apower[k] = Apower[k + 1];
                        Bpower[k] = k;
                        Dpower[k] = Dpower[k + 1];
                    }
                }
                for (int j = 1; j < N + 1; j++)
                {
                    aTanA[j] = Cpower[j];
                    PointNO[j] = Epower[j];
                }
                #endregion
                #region// إيجاد أول عقدة ترتبط مع العقدة صفر بمحور
                for (int j = 1; j < PointADD + 1; j++)
                {
                    X2 = PointXReal[PointNO[j]];
                    Y2 = PointYReal[PointNO[j]];
                    for (int k = 1; k < AllLine + 1; k++)
                    {
                        X3 = LineX1Real[k];
                        Y3 = LineY1Real[k];
                        X4 = LineX2Real[k];
                        Y4 = LineY2Real[k];
                        x = X1;
                        y = Y1;
                        int chk1 = 0;
                        if (X3 == X4 & X3 == x)
                        {
                            if (y >= Y3 & y <= Y4) chk1 = 1;
                            if (y <= Y3 & y >= Y4) chk1 = 1;
                        }
                        if (Y3 == Y4 & Y3 == y)
                        {
                            if (x >= X3 & x <= X4) chk1 = 1;
                            if (x <= X3 & x >= X4) chk1 = 1;
                        }
                        if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk1 = 1;
                        x = X2;
                        y = Y2;
                        int chk2 = 0;
                        if (X3 == X4 & X3 == x)
                        {
                            if (y >= Y3 & y <= Y4) chk2 = 1;
                            if (y <= Y3 & y >= Y4) chk2 = 1;
                        }
                        if (Y3 == Y4 & Y3 == y)
                        {
                            if (x >= X3 & x <= X4) chk2 = 1;
                            if (x <= X3 & x >= X4) chk2 = 1;
                        }
                        if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk2 = 1;
                        if (chk1 == 1 & chk2 == 1)
                        {
                            LastJoint = j;
                            m = m + 1;
                            pX[m] = X2;
                            pY[m] = Y2;
                            goto Endj;
                        }
                    }
                }
            Endj: { }
                #endregion



                for (int j = LastJoint + 1; j < PointADD + 1; j++)
                {
                    X2 = PointXReal[PointNO[j]];
                    Y2 = PointYReal[PointNO[j]];
                    #region//التحقق من أن العقدتين على نفس المحور
                    for (int k = 1; k < AllLine + 1; k++)
                    {
                        X3 = LineX1Real[k];
                        Y3 = LineY1Real[k];
                        X4 = LineX2Real[k];
                        Y4 = LineY2Real[k];
                        x = PointXReal[PointNO[LastJoint]];
                        y = PointYReal[PointNO[LastJoint]];
                        int chk1 = 0;
                        if (X3 == X4 & X3 == x)
                        {
                            if (y >= Y3 & y <= Y4) chk1 = 1;
                            if (y <= Y3 & y >= Y4) chk1 = 1;
                        }
                        if (Y3 == Y4 & Y3 == y)
                        {
                            if (x >= X3 & x <= X4) chk1 = 1;
                            if (x <= X3 & x >= X4) chk1 = 1;
                        }
                        if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk1 = 1;
                        x = X2;
                        y = Y2;
                        int chk2 = 0;
                        if (X3 == X4 & X3 == x)
                        {
                            if (y >= Y3 & y <= Y4) chk2 = 1;
                            if (y <= Y3 & y >= Y4) chk2 = 1;
                        }
                        if (Y3 == Y4 & Y3 == y)
                        {
                            if (x >= X3 & x <= X4) chk2 = 1;
                            if (x <= X3 & x >= X4) chk2 = 1;
                        }
                        if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk2 = 1;
                        if (chk1 == 1 & chk2 == 1)
                        {
                            #region//التحقق من ان العقدة تقع على محور يتصل بالعقدة صفر لننهي المساحة
                            for (int s = 1; s < AllLine + 1; s++)
                            {
                                X3 = LineX1Real[s];
                                Y3 = LineY1Real[s];
                                X4 = LineX2Real[s];
                                Y4 = LineY2Real[s];
                                x = X1;
                                y = Y1;
                                int chk10 = 0;
                                if (X3 == X4 & X3 == x)
                                {
                                    if (y >= Y3 & y <= Y4) chk10 = 1;
                                    if (y <= Y3 & y >= Y4) chk10 = 1;
                                }
                                if (Y3 == Y4 & Y3 == y)
                                {
                                    if (x >= X3 & x <= X4) chk10 = 1;
                                    if (x <= X3 & x >= X4) chk10 = 1;
                                }
                                if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk10 = 1;
                                x = X2;
                                y = Y2;
                                int chk20 = 0;
                                if (X3 == X4 & X3 == x)
                                {
                                    if (y >= Y3 & y <= Y4) chk20 = 1;
                                    if (y <= Y3 & y >= Y4) chk20 = 1;
                                }
                                if (Y3 == Y4 & Y3 == y)
                                {
                                    if (x >= X3 & x <= X4) chk20 = 1;
                                    if (x <= X3 & x >= X4) chk20 = 1;
                                }
                                if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk20 = 1;
                                if (chk10 == 1 & chk20 == 1)
                                {
                                    m = m + 1;//وجدنا المساحة
                                    pX[m] = X2;
                                    pY[m] = Y2;
                                    m = m + 1;
                                    pX[m] = X1;
                                    pY[m] = Y1;
                                    check0 = 1;
                                    goto nexti;
                                }
                            }
                            #endregion
                            LastJoint = j;
                            m = m + 1;
                            pX[m] = X2;
                            pY[m] = Y2;
                            goto nextj;
                        }
                    }
                    goto nexti;
                    #endregion
                nextj: { }
                }
            nexti: { }





                #region//التحقق من أن المساحة غير مكررة
                if (m > 2 & check0 == 1)
                {
                    int tah = 0;
                    int s = 0;
                    for (int j = 1; j < AriaNo + 1; j++)
                    {
                        if (AriaPointNo[j] == m)
                        {
                            s = 0;
                            for (int k = 1; k < m + 1; k++)
                            {
                                x = pX[k];
                                y = pY[k];
                                for (int l = 1; l < m + 1; l++)
                                {
                                    if (x == AriaPointX[j, l] & y == AriaPointY[j, l]) s = s + 1;
                                }
                            }
                            if (s >= m + 1)
                            {
                                tah = 1;
                                break;
                            }
                        }
                    }
                    if (tah == 0)
                    {
                        int tahtah1 = 0;
                        int tahtah2 = 0;
                        for (int j = 1; j < m; j++)
                        {
                            if (pX[j] != pX[j + 1]) tahtah1 = 1;
                            if (pY[j] != pY[j + 1]) tahtah2 = 1;
                        }
                        if (tahtah1 != 0 & tahtah2 != 0)
                        {
                            AriaNo = AriaNo + 1;
                            AriaPointNo[AriaNo] = m;
                            for (int j = 1; j < m + 1; j++)
                            {
                                AriaPointX[AriaNo, j] = pX[j];
                                AriaPointY[AriaNo, j] = pY[j];
                            }
                        }
                    }
                }
                #endregion
                pX = new double[PointNumber2d + 2];
                pY = new double[PointNumber2d + 2];
            }
        }

        private void FindArias1()
        {
            #region//تعريفات
            double X1 = 0;
            double Y1 = 0;
            double X2 = 0;
            double Y2 = 0;
            double X3 = 0;
            double Y3 = 0;
            double X4 = 0;
            double Y4 = 0;
            double x = 0;
            double y = 0;
            int StartJoint = 0;
            int LastJoint = 0;
            int PointADD = 0;
            int m = 0;
            #endregion
            for (int i = 1; i < PointNumber2d + 1; i++)
            {
                int check0 = 0;
                double[] pX = new double[PointNumber2d + 2];
                double[] pY = new double[PointNumber2d + 2];
                int[] PointNO = new int[PointNumber2d + 1];
                X1 = PointXReal[i];
                Y1 = PointYReal[i];
                m = 1;
                pX[m] = X1;
                pY[m] = Y1;
                #region//ايجاد العقد التي تقع ضمن المساحة
                PointADD = 0;
                for (int j = 1; j < PointNumber2d + 1; j++)
                {
                    if (j != i)
                    {
                        X2 = PointXReal[j];
                        Y2 = PointYReal[j];
                        for (int k = 1; k < AllLine + 1; k++)
                        {
                            X3 = LineX1Real[k];
                            Y3 = LineY1Real[k];
                            X4 = LineX2Real[k];
                            Y4 = LineY2Real[k];
                            #region//التحقق من أن العقدتين على نفس المحور
                            x = X1;
                            y = Y1;
                            int chk1 = 0;
                            if (X3 == X4 & X3 == x)
                            {
                                if (y >= Y3 & y <= Y4) chk1 = 1;
                                if (y <= Y3 & y >= Y4) chk1 = 1;
                            }
                            if (Y3 == Y4 & Y3 == y)
                            {
                                if (x >= X3 & x <= X4) chk1 = 1;
                                if (x <= X3 & x >= X4) chk1 = 1;
                            }
                            if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk1 = 1;
                            x = X2;
                            y = Y2;
                            int chk2 = 0;
                            if (X3 == X4 & X3 == x)
                            {
                                if (y >= Y3 & y <= Y4) chk2 = 1;
                                if (y <= Y3 & y >= Y4) chk2 = 1;
                            }
                            if (Y3 == Y4 & Y3 == y)
                            {
                                if (x >= X3 & x <= X4) chk2 = 1;
                                if (x <= X3 & x >= X4) chk2 = 1;
                            }
                            if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk2 = 1;
                            if (chk1 == 1 & chk2 == 1)
                            {
                                goto nextk;
                            }
                            #endregion
                            checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                            if (INTERSECTION == 1) goto nextj;
                        nextk: { }
                        }
                        PointADD = PointADD + 1;
                        PointNO[PointADD] = j;
                    }
                nextj: { }
                }
                #endregion
                #region //ترتيب العقد مع عقارب الساعة
                StartJoint = i;
                double[] aTanA = new double[PointADD + 1];
                for (int j = 1; j < PointADD + 1; j++)
                {
                    aTanA[j] = Math.Atan2(PointYReal[PointNO[j]] - PointYReal[StartJoint], PointXReal[PointNO[j]] - PointXReal[StartJoint]);
                    if (Math.Abs(aTanA[j] - Convert.ToDouble((Math.PI))) < 0.001) aTanA[j] = -1 * aTanA[j];
                }
                double[] Apower = new double[PointADD + 1];
                int[] Bpower = new int[PointADD + 1];
                double[] Cpower = new double[PointADD + 1];
                int[] Dpower = new int[PointADD + 1];
                int[] Epower = new int[PointADD + 1];
                int Row = 0;
                int N = PointADD;
                int NN = PointADD;
                double MAXI = -1000000;
                int MAXId = 0;
                for (int j = 1; j < N + 1; j++)
                {
                    Apower[j] = aTanA[j];
                    Bpower[j] = j;
                    Dpower[j] = PointNO[j];
                }
                for (int j = 1; j < N + 1; j++)
                {
                    MAXI = -1000000;
                    for (int k = 1; k < NN + 1; k++)
                    {
                        if (MAXI <= Apower[k])
                        {
                            MAXI = Apower[k];
                            MAXId = Dpower[k];
                            Row = Bpower[k];
                        }
                    }
                    Cpower[j] = MAXI;
                    Epower[j] = MAXId;
                    NN = NN - 1;
                    for (int k = Row; k < NN + 1; k++)
                    {
                        Apower[k] = Apower[k + 1];
                        Bpower[k] = k;
                        Dpower[k] = Dpower[k + 1];
                    }
                }
                for (int j = 1; j < N + 1; j++)
                {
                    aTanA[j] = Cpower[j];
                    PointNO[j] = Epower[j];
                }
                #endregion
                #region// إيجاد أول عقدة ترتبط مع العقدة صفر بمحور
                for (int j = 1; j < PointADD + 1; j++)
                {
                    X2 = PointXReal[PointNO[j]];
                    Y2 = PointYReal[PointNO[j]];
                    for (int k = 1; k < AllLine + 1; k++)
                    {
                        X3 = LineX1Real[k];
                        Y3 = LineY1Real[k];
                        X4 = LineX2Real[k];
                        Y4 = LineY2Real[k];
                        x = X1;
                        y = Y1;
                        int chk1 = 0;
                        if (X3 == X4 & X3 == x)
                        {
                            if (y >= Y3 & y <= Y4) chk1 = 1;
                            if (y <= Y3 & y >= Y4) chk1 = 1;
                        }
                        if (Y3 == Y4 & Y3 == y)
                        {
                            if (x >= X3 & x <= X4) chk1 = 1;
                            if (x <= X3 & x >= X4) chk1 = 1;
                        }
                        if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk1 = 1;
                        x = X2;
                        y = Y2;
                        int chk2 = 0;
                        if (X3 == X4 & X3 == x)
                        {
                            if (y >= Y3 & y <= Y4) chk2 = 1;
                            if (y <= Y3 & y >= Y4) chk2 = 1;
                        }
                        if (Y3 == Y4 & Y3 == y)
                        {
                            if (x >= X3 & x <= X4) chk2 = 1;
                            if (x <= X3 & x >= X4) chk2 = 1;
                        }
                        if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk2 = 1;
                        if (chk1 == 1 & chk2 == 1)
                        {
                            LastJoint = j;
                            m = m + 1;
                            pX[m] = X2;
                            pY[m] = Y2;
                            goto Endj;
                        }
                    }
                }
            Endj: { }
                #endregion


                for (int j = LastJoint + 1; j < PointADD + 1; j++)
                {
                    X2 = PointXReal[PointNO[j]];
                    Y2 = PointYReal[PointNO[j]];
                    #region//التحقق من أن العقدتين على نفس المحور
                    for (int k = 1; k < AllLine + 1; k++)
                    {
                        X3 = LineX1Real[k];
                        Y3 = LineY1Real[k];
                        X4 = LineX2Real[k];
                        Y4 = LineY2Real[k];
                        x = PointXReal[PointNO[LastJoint]];
                        y = PointYReal[PointNO[LastJoint]];
                        int chk1 = 0;
                        if (X3 == X4 & X3 == x)
                        {
                            if (y >= Y3 & y <= Y4) chk1 = 1;
                            if (y <= Y3 & y >= Y4) chk1 = 1;
                        }
                        if (Y3 == Y4 & Y3 == y)
                        {
                            if (x >= X3 & x <= X4) chk1 = 1;
                            if (x <= X3 & x >= X4) chk1 = 1;
                        }
                        if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk1 = 1;
                        x = X2;
                        y = Y2;
                        int chk2 = 0;
                        if (X3 == X4 & X3 == x)
                        {
                            if (y >= Y3 & y <= Y4) chk2 = 1;
                            if (y <= Y3 & y >= Y4) chk2 = 1;
                        }
                        if (Y3 == Y4 & Y3 == y)
                        {
                            if (x >= X3 & x <= X4) chk2 = 1;
                            if (x <= X3 & x >= X4) chk2 = 1;
                        }
                        if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk2 = 1;
                        if (chk1 == 1 & chk2 == 1)
                        {
                            #region//التحقق من ان العقدة تقع على محور يتصل بالعقدة صفر لننهي المساحة
                            for (int s = 1; s < AllLine + 1; s++)
                            {
                                X3 = LineX1Real[s];
                                Y3 = LineY1Real[s];
                                X4 = LineX2Real[s];
                                Y4 = LineY2Real[s];
                                x = X1;
                                y = Y1;
                                int chk10 = 0;
                                if (X3 == X4 & X3 == x)
                                {
                                    if (y >= Y3 & y <= Y4) chk10 = 1;
                                    if (y <= Y3 & y >= Y4) chk10 = 1;
                                }
                                if (Y3 == Y4 & Y3 == y)
                                {
                                    if (x >= X3 & x <= X4) chk10 = 1;
                                    if (x <= X3 & x >= X4) chk10 = 1;
                                }
                                if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk10 = 1;
                                x = X2;
                                y = Y2;
                                int chk20 = 0;
                                if (X3 == X4 & X3 == x)
                                {
                                    if (y >= Y3 & y <= Y4) chk20 = 1;
                                    if (y <= Y3 & y >= Y4) chk20 = 1;
                                }
                                if (Y3 == Y4 & Y3 == y)
                                {
                                    if (x >= X3 & x <= X4) chk20 = 1;
                                    if (x <= X3 & x >= X4) chk20 = 1;
                                }
                                if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk20 = 1;
                                if (chk10 == 1 & chk20 == 1)
                                {
                                    m = m + 1;//وجدنا المساحة
                                    pX[m] = X2;
                                    pY[m] = Y2;
                                    m = m + 1;
                                    pX[m] = X1;
                                    pY[m] = Y1;
                                    check0 = 1;
                                    goto nexti;
                                }
                            }
                            #endregion
                            LastJoint = j;
                            m = m + 1;
                            pX[m] = X2;
                            pY[m] = Y2;
                            goto nextj;
                        }
                    }
                    goto nexti;
                    #endregion
                nextj: { }
                }
            nexti: { }



                #region//التحقق من أن المساحة غير مكررة
                if (m > 2 & check0 == 1)
                {
                    int tah = 0;
                    int s = 0;
                    for (int j = 1; j < AriaNo + 1; j++)
                    {
                        if (AriaPointNo[j] == m)
                        {
                            s = 0;
                            for (int k = 1; k < m + 1; k++)
                            {
                                x = pX[k];
                                y = pY[k];
                                for (int l = 1; l < m + 1; l++)
                                {
                                    if (x == AriaPointX[j, l] & y == AriaPointY[j, l]) s = s + 1;
                                }
                            }
                            if (s >= m + 1)
                            {
                                tah = 1;
                                break;
                            }
                        }
                    }
                    if (tah == 0)
                    {
                        AriaNo = AriaNo + 1;
                        AriaPointNo[AriaNo] = m;
                        for (int j = 1; j < m + 1; j++)
                        {
                            AriaPointX[AriaNo, j] = pX[j];
                            AriaPointY[AriaNo, j] = pY[j];
                        }
                    }
                }
                #endregion
                pX = new double[PointNumber2d + 2];
                pY = new double[PointNumber2d + 2];
            }
        }
        private void FindArias10()
        {
            #region//تعريفات
            double X1 = 0;
            double Y1 = 0;
            double X2 = 0;
            double Y2 = 0;
            double X3 = 0;
            double Y3 = 0;
            double X4 = 0;
            double Y4 = 0;
            double x = 0;
            double y = 0;
            int StartJoint = 0;
            int LastJoint = 0;
            int PointADD = 0;
            int m = 0;
            #endregion
            for (int i = 1; i < PointNumber2d + 1; i++)
            {
                int check0 = 0;
                double[] pX = new double[PointNumber2d + 2];
                double[] pY = new double[PointNumber2d + 2];
                int[] PointNO = new int[PointNumber2d + 1];
                X1 = GridPoint.XReal[i];
                Y1 = GridPoint.YReal[i];
                m = 1;
                pX[m] = X1;
                pY[m] = Y1;
                #region//ايجاد العقد التي تقع ضمن المساحة
                PointADD = 0;
                for (int j = 1; j < PointNumber2d + 1; j++)
                {
                    if (j != i)
                    {
                        X2 = GridPoint.XReal[j];
                        Y2 = GridPoint.YReal[j];
                        for (int k = 1; k < AllLine + 1; k++)
                        {
                            X3 = GridLine.X1Real[k];
                            Y3 = GridLine.Y1Real[k];
                            X4 = GridLine.X2Real[k];
                            Y4 = GridLine.Y2Real[k];
                            #region//التحقق من أن العقدتين على نفس المحور
                            x = X1;
                            y = Y1;
                            int chk1 = 0;
                            if (X3 == X4 & X3 == x)
                            {
                                if (y >= Y3 & y <= Y4) chk1 = 1;
                                if (y <= Y3 & y >= Y4) chk1 = 1;
                            }
                            if (Y3 == Y4 & Y3 == y)
                            {
                                if (x >= X3 & x <= X4) chk1 = 1;
                                if (x <= X3 & x >= X4) chk1 = 1;
                            }
                            if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk1 = 1;
                            x = X2;
                            y = Y2;
                            int chk2 = 0;
                            if (X3 == X4 & X3 == x)
                            {
                                if (y >= Y3 & y <= Y4) chk2 = 1;
                                if (y <= Y3 & y >= Y4) chk2 = 1;
                            }
                            if (Y3 == Y4 & Y3 == y)
                            {
                                if (x >= X3 & x <= X4) chk2 = 1;
                                if (x <= X3 & x >= X4) chk2 = 1;
                            }
                            if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk2 = 1;
                            if (chk1 == 1 & chk2 == 1)
                            {
                                goto nextk;
                            }
                            #endregion
                            checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);
                            if (INTERSECTION == 1) goto nextj;
                        nextk: { }
                        }
                        PointADD = PointADD + 1;
                        PointNO[PointADD] = j;
                    }
                nextj: { }
                }
                #endregion
                #region //ترتيب العقد مع عقارب الساعة
                StartJoint = i;
                double[] aTanA = new double[PointADD + 1];
                for (int j = 1; j < PointADD + 1; j++)
                {
                    aTanA[j] = Math.Atan2(GridPoint.YReal[PointNO[j]] - GridPoint.YReal[StartJoint], GridPoint.XReal[PointNO[j]] - GridPoint.XReal[StartJoint]);
                    if (Math.Abs(aTanA[j] - Convert.ToDouble((Math.PI))) < 0.001) aTanA[j] = -1 * aTanA[j];
                }
                double[] Apower = new double[PointADD + 1];
                int[] Bpower = new int[PointADD + 1];
                double[] Cpower = new double[PointADD + 1];
                int[] Dpower = new int[PointADD + 1];
                int[] Epower = new int[PointADD + 1];
                int Row = 0;
                int N = PointADD;
                int NN = PointADD;
                double MAXI = -1000000;
                int MAXId = 0;
                for (int j = 1; j < N + 1; j++)
                {
                    Apower[j] = aTanA[j];
                    Bpower[j] = j;
                    Dpower[j] = PointNO[j];
                }
                for (int j = 1; j < N + 1; j++)
                {
                    MAXI = -1000000;
                    for (int k = 1; k < NN + 1; k++)
                    {
                        if (MAXI <= Apower[k])
                        {
                            MAXI = Apower[k];
                            MAXId = Dpower[k];
                            Row = Bpower[k];
                        }
                    }
                    Cpower[j] = MAXI;
                    Epower[j] = MAXId;
                    NN = NN - 1;
                    for (int k = Row; k < NN + 1; k++)
                    {
                        Apower[k] = Apower[k + 1];
                        Bpower[k] = k;
                        Dpower[k] = Dpower[k + 1];
                    }
                }
                for (int j = 1; j < N + 1; j++)
                {
                    aTanA[j] = Cpower[j];
                    PointNO[j] = Epower[j];
                }
                #endregion
                #region// إيجاد أول عقدة ترتبط مع العقدة صفر بمحور
                for (int j = 1; j < PointADD + 1; j++)
                {
                    X2 = GridPoint.XReal[PointNO[j]];
                    Y2 = GridPoint.YReal[PointNO[j]];
                    for (int k = 1; k < AllLine + 1; k++)
                    {
                        X3 = GridLine.X1Real[k];
                        Y3 = GridLine.Y1Real[k];
                        X4 = GridLine.X2Real[k];
                        Y4 = GridLine.Y2Real[k];
                        x = X1;
                        y = Y1;
                        int chk1 = 0;
                        if (X3 == X4 & X3 == x)
                        {
                            if (y >= Y3 & y <= Y4) chk1 = 1;
                            if (y <= Y3 & y >= Y4) chk1 = 1;
                        }
                        if (Y3 == Y4 & Y3 == y)
                        {
                            if (x >= X3 & x <= X4) chk1 = 1;
                            if (x <= X3 & x >= X4) chk1 = 1;
                        }
                        if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk1 = 1;
                        x = X2;
                        y = Y2;
                        int chk2 = 0;
                        if (X3 == X4 & X3 == x)
                        {
                            if (y >= Y3 & y <= Y4) chk2 = 1;
                            if (y <= Y3 & y >= Y4) chk2 = 1;
                        }
                        if (Y3 == Y4 & Y3 == y)
                        {
                            if (x >= X3 & x <= X4) chk2 = 1;
                            if (x <= X3 & x >= X4) chk2 = 1;
                        }
                        if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk2 = 1;
                        if (chk1 == 1 & chk2 == 1)
                        {
                            LastJoint = j;
                            m = m + 1;
                            pX[m] = X2;
                            pY[m] = Y2;
                            goto Endj;
                        }
                    }
                }
            Endj: { }
                #endregion
                for (int j = LastJoint + 1; j < PointADD + 1; j++)
                {
                    X2 = GridPoint.XReal[PointNO[j]];
                    Y2 = GridPoint.YReal[PointNO[j]];
                    #region//التحقق من أن العقدتين على نفس المحور
                    for (int k = 1; k < AllLine + 1; k++)
                    {
                        X3 = GridLine.X1Real[k];
                        Y3 = GridLine.Y1Real[k];
                        X4 = GridLine.X2Real[k];
                        Y4 = GridLine.Y2Real[k];
                        x = GridPoint.XReal[PointNO[LastJoint]];
                        y = GridPoint.YReal[PointNO[LastJoint]];
                        int chk1 = 0;
                        if (X3 == X4 & X3 == x)
                        {
                            if (y >= Y3 & y <= Y4) chk1 = 1;
                            if (y <= Y3 & y >= Y4) chk1 = 1;
                        }
                        if (Y3 == Y4 & Y3 == y)
                        {
                            if (x >= X3 & x <= X4) chk1 = 1;
                            if (x <= X3 & x >= X4) chk1 = 1;
                        }
                        if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk1 = 1;
                        x = X2;
                        y = Y2;
                        int chk2 = 0;
                        if (X3 == X4 & X3 == x)
                        {
                            if (y >= Y3 & y <= Y4) chk2 = 1;
                            if (y <= Y3 & y >= Y4) chk2 = 1;
                        }
                        if (Y3 == Y4 & Y3 == y)
                        {
                            if (x >= X3 & x <= X4) chk2 = 1;
                            if (x <= X3 & x >= X4) chk2 = 1;
                        }
                        if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk2 = 1;
                        if (chk1 == 1 & chk2 == 1)
                        {
                            #region//التحقق من ان العقدة تقع على محور يتصل بالعقدة صفر لننهي المساحة
                            for (int s = 1; s < AllLine + 1; s++)
                            {
                                X3 = GridLine.X1Real[s];
                                Y3 = GridLine.Y1Real[s];
                                X4 = GridLine.X2Real[s];
                                Y4 = GridLine.Y2Real[s];
                                x = X1;
                                y = Y1;
                                int chk10 = 0;
                                if (X3 == X4 & X3 == x)
                                {
                                    if (y >= Y3 & y <= Y4) chk10 = 1;
                                    if (y <= Y3 & y >= Y4) chk10 = 1;
                                }
                                if (Y3 == Y4 & Y3 == y)
                                {
                                    if (x >= X3 & x <= X4) chk10 = 1;
                                    if (x <= X3 & x >= X4) chk10 = 1;
                                }
                                if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk10 = 1;
                                x = X2;
                                y = Y2;
                                int chk20 = 0;
                                if (X3 == X4 & X3 == x)
                                {
                                    if (y >= Y3 & y <= Y4) chk20 = 1;
                                    if (y <= Y3 & y >= Y4) chk20 = 1;
                                }
                                if (Y3 == Y4 & Y3 == y)
                                {
                                    if (x >= X3 & x <= X4) chk20 = 1;
                                    if (x <= X3 & x >= X4) chk20 = 1;
                                }
                                if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= 0.001) chk20 = 1;
                                if (chk10 == 1 & chk20 == 1)
                                {
                                    m = m + 1;//وجدنا المساحة
                                    pX[m] = X2;
                                    pY[m] = Y2;
                                    m = m + 1;
                                    pX[m] = X1;
                                    pY[m] = Y1;
                                    check0 = 1;
                                    goto nexti;
                                }
                            }
                        }
                            #endregion
                        LastJoint = j;
                        m = m + 1;
                        pX[m] = X2;
                        pY[m] = Y2;
                        goto nextj;
                    }
                    goto nexti;
                    #endregion
                nextj: { }
                }
            nexti: { }
                #region//التحقق من أن المساحة غير مكررة
                if (m > 2 & check0 == 1)
                {
                    int tah = 0;
                    int s = 0;
                    for (int j = 1; j < AriaNo + 1; j++)
                    {
                        if (AriaPointNo[j] == m)
                        {
                            s = 0;
                            for (int k = 1; k < m + 1; k++)
                            {
                                x = pX[k];
                                y = pY[k];
                                for (int l = 1; l < m + 1; l++)
                                {
                                    if (x == AriaPointX[j, l] & y == AriaPointY[j, l]) s = s + 1;
                                }
                            }
                            if (s >= m + 1)
                            {
                                tah = 1;
                                break;
                            }
                        }
                    }
                    if (tah == 0)
                    {
                        int tahtah1 = 0;
                        int tahtah2 = 0;
                        for (int j = 1; j < m; j++)
                        {
                            if (pX[j] != pX[j + 1]) tahtah1 = 1;
                            if (pY[j] != pY[j + 1]) tahtah2 = 1;
                        }
                        if (tahtah1 != 0 & tahtah2 != 0)
                        {
                            AriaNo = AriaNo + 1;
                            AriaPointNo[AriaNo] = m;
                            for (int j = 1; j < m + 1; j++)
                            {
                                AriaPointX[AriaNo, j] = pX[j];
                                AriaPointY[AriaNo, j] = pY[j];
                            }
                        }
                    }
                }
                #endregion
                pX = new double[PointNumber2d + 2];
                pY = new double[PointNumber2d + 2];
            }
        }
        private void FindINArias()
        {
            int m = 0;
            for (int i = 1; i < AriaNo + 1; i++)
            {
                for (int j = 1; j < AriaPointNo[i] + 1; j++)
                {
                    Xtest = AriaPointX[i, j];
                    Ytest = AriaPointY[i, j];
                    CheckifAriainShape();
                    if (result == true)
                    {
                        m = m + 1;
                        AriaPointNoF[m] = AriaPointNo[i];
                        for (int k = 1; k < AriaPointNo[m] + 1; k++)
                        {
                            AriaPointXF[m, k] = AriaPointX[i, k];
                            AriaPointYF[m, k] = AriaPointY[i, k];
                        }
                        goto NextI;
                    }
                }
            NextI: { }
            }
            AriaNoF = m;
        }
        private void FindAriasType()
        {
            for (int i = 1; i < AriaNoF + 1; i++)
            {
                for (int j = 1; j < AriaPointNoF[i] + 1; j++)
                {
                    Xtest = AriaPointXF[i, j];
                    Ytest = AriaPointYF[i, j];
                    CheckifAriainTie();
                    if (result == true)
                    {
                        goto NextI;
                    }
                }
            NextI: { }
                if (result == true)
                {
                    AriaTypeF[i] = 1;//IN
                }
                else
                {
                    AriaTypeF[i] = 2;//OUT
                }
            }
        }
        private void CheckifAriainShape()
        {
            int N = ShapePoints + 1;
            double[] polygonX = new double[N + 1];
            double[] polygonY = new double[N + 1];
            for (int j = 1; j < N; j++)
            {
                polygonX[j] = Math.Round(ShapePointX[j], 0);
                polygonY[j] = Math.Round(ShapePointY[j], 0);
            }
            polygonX[N] = polygonX[1];
            polygonY[N] = polygonY[1];
            result = false;
            int nvert = N;
            int k, l;
            for (k = 1, l = nvert - 1; k < nvert; l = k++)
            {
                if (((polygonY[k] > Ytest) != (polygonY[l] > Ytest)) &&
                 (Xtest < (polygonX[l] - polygonX[k]) * (Ytest - polygonY[k]) / (polygonY[l] - polygonY[k]) + polygonX[k]))
                    result = !result;
            }
        }
        private void CheckifAriainTie()
        {
            int N = TiePoints + 1;
            double[] polygonX = new double[N + 1];
            double[] polygonY = new double[N + 1];
            for (int j = 1; j < N; j++)
            {
                polygonX[j] = Math.Round(TiePointX[j], 0);
                polygonY[j] = Math.Round(TiePointY[j], 0);
            }
            polygonX[N] = polygonX[1];
            polygonY[N] = polygonY[1];
            result = false;
            int nvert = N;
            int k, l;
            for (k = 1, l = nvert - 1; k < nvert; l = k++)
            {
                if (((polygonY[k] > Ytest) != (polygonY[l] > Ytest)) &&
                 (Xtest < (polygonX[l] - polygonX[k]) * (Ytest - polygonY[k]) / (polygonY[l] - polygonY[k]) + polygonX[k]))
                    result = !result;
            }
        }
        public void CalculateAriaProperty()
        {
            double ALAWAL = 0;
            double ALTHANI = 0;
            int P1 = 0;
            int P2 = 0;
            double ARIA = 0;
            double XFORCG = 0;
            double YFORCG = 0;
            double CENTERX = 0;
            double CENTERY = 0;
            for (int i = 1; i < AriaNoF + 1; i++)
            {
                for (int j = 1; j < AriaPointNoF[i]; j++)
                {
                    P1 = j;
                    P2 = j + 1;
                    ALAWAL = ALAWAL + AriaPointXF[i, P1] * AriaPointYF[i, P2];
                    ALTHANI = ALTHANI + AriaPointYF[i, P1] * AriaPointXF[i, P2];
                    XFORCG = XFORCG + ((AriaPointXF[i, P1] + AriaPointXF[i, P2]) * ((AriaPointXF[i, P1] * AriaPointYF[i, P2]) - (AriaPointYF[i, P1] * AriaPointXF[i, P2]))) / 6;
                    YFORCG = YFORCG + ((AriaPointYF[i, P1] + AriaPointYF[i, P2]) * ((AriaPointYF[i, P1] * AriaPointXF[i, P2]) - (AriaPointXF[i, P1] * AriaPointYF[i, P2]))) / 6;
                }
                ARIA = Math.Round(Math.Abs((ALAWAL - ALTHANI) / 2), 3);
                CENTERX = Math.Round(Math.Abs(XFORCG) / ARIA, 3);
                CENTERY = Math.Round(Math.Abs(YFORCG) / ARIA, 3);
                AriaAriaF[i] = ARIA;
                AriaCenterXF[i] = CENTERX;
                AriaCenterYF[i] = CENTERY;
            }
            int h = 1;
        }
        private void checkintersection(double X1, double Y1, double X2, double Y2, double X3, double Y3, double X4, double Y4)
        {
            int THETAHKIK = 0;
            double x0 = 0;
            double y0 = 0;
            if (X2 == X1 & Y3 == Y4)//شاقولي و أفقي
            {
                int tah = 0;
                if (X1 < X3 & X1 < X4) tah = 1;
                if (X1 > X3 & X1 > X4) tah = 1;
                if (Y3 < Y1 & Y3 < Y2) tah = 1;
                if (Y3 > Y1 & Y3 > Y2) tah = 1;
                if (tah == 1) goto end100;
                x0 = X1;
                y0 = Y3;
                x0 = Math.Round(x0, 3);
                y0 = Math.Round(y0, 3);
                if (x0 > X3 & x0 > X4) goto end100;
                if (x0 < X3 & x0 < X4) goto end100;
                if (y0 > Y3 & y0 > Y4) goto end100;
                if (y0 < Y3 & y0 < Y4) goto end100;
                if (x0 > X1 & x0 > X2) goto end100;
                if (x0 < X1 & x0 < X2) goto end100;
                if (y0 > Y1 & y0 > Y2) goto end100;
                if (y0 < Y1 & y0 < Y2) goto end100;
                THETAHKIK = 1;
                goto end100;
            }
            if (X2 == X1 & Y3 != Y4 & X4 != X3)//شاقولي و مائل
            {
                x0 = X1;
                y0 = ((x0 - X3) / (X4 - X3)) * (Y4 - Y3) + Y3;
                int tah = 0;
                if (y0 < Y1 & y0 < Y2) tah = 1;
                if (y0 > Y1 & y0 > Y2) tah = 1;
                if (tah == 1) goto end100;
                x0 = Math.Round(x0, 3);
                y0 = Math.Round(y0, 3);
                if (x0 > X3 & x0 > X4) goto end100;
                if (x0 < X3 & x0 < X4) goto end100;
                if (y0 > Y3 & y0 > Y4) goto end100;
                if (y0 < Y3 & y0 < Y4) goto end100;
                if (x0 > X1 & x0 > X2) goto end100;
                if (x0 < X1 & x0 < X2) goto end100;
                if (y0 > Y1 & y0 > Y2) goto end100;
                if (y0 < Y1 & y0 < Y2) goto end100;
                THETAHKIK = 1;
                goto end100;
            }
            double bast = (Y3 - Y1) * (X4 - X3) * (X2 - X1);//حالة عامة
            bast = bast + X1 * (Y2 - Y1) * (X4 - X3);
            bast = bast - X3 * (Y4 - Y3) * (X2 - X1);
            double makam = (Y2 - Y1) * (X4 - X3);
            makam = makam - (Y4 - Y3) * (X2 - X1);
            if (makam != 0)
            {
                x0 = bast / makam;
                y0 = (((Y2 - Y1) / (X2 - X1)) * (x0 - X1)) + Y1;
                x0 = Math.Round(x0, 3);
                y0 = Math.Round(y0, 3);
                if (x0 > X3 & x0 > X4) goto end100;
                if (x0 < X3 & x0 < X4) goto end100;
                if (y0 > Y3 & y0 > Y4) goto end100;
                if (y0 < Y3 & y0 < Y4) goto end100;
                if (x0 > X1 & x0 > X2) goto end100;
                if (x0 < X1 & x0 < X2) goto end100;
                if (y0 > Y1 & y0 > Y2) goto end100;
                if (y0 < Y1 & y0 < Y2) goto end100;
                THETAHKIK = 1;
            }
        end100: { }
            if (forarias == 1)
            {
                if (Math.Abs(X1 - x0) < 0.01 & Math.Abs(Y1 - y0) < 0.01) THETAHKIK = 0;
                if (Math.Abs(X2 - x0) < 0.01 & Math.Abs(Y2 - y0) < 0.01) THETAHKIK = 0;
            }
            INTERSECTION = THETAHKIK;
            TheX0 = x0;
            TheY0 = y0;
        }

    }
}
