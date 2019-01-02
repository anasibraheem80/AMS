using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMSPRO
{
    class meshclass
    {
        #region//تعريفات
        int INTERSECTION = 0;
        double TheX0 = 0;
        double TheY0 = 0;
        int forarias = 0;
        public int ShapeNumbers;
        public double[] ShapeType = new double[100];
        public double[,] ShapePointX = new double[100,10000];
        public double[,] ShapePointY = new double[100,10000];
        public int TieNumbers ;
        public double[] TieType = new double[100];
        public double[,] TiePointX = new double[100,10000];
        public double[,] TiePointY = new double[100,10000];

        public int AllLine = 0;
        public double[] LineX1Real = new double[10000];
        public double[] LineX2Real = new double[10000];
        public double[] LineY1Real = new double[10000];
        public double[] LineY2Real = new double[10000];
        public int[] LineType = new int[10000];
        public int[] LineShape = new int[10000];
        public int PointNumber2d = 0;
        public double[] PointXReal = new double[10000];
        public double[] PointYReal = new double[10000];
        public int AriaNo = 0;
        public int[] AriaPointNo = new int[10000];
        public double[,] AriaPointX = new double[10000, 100];
        public double[,] AriaPointY = new double[10000, 100];
        public int AriaNoF = 0;
        public int[] AriaPointNoF = new int[10000];
        public int[] AriaTypeF = new int[10000];
        public double[,] AriaPointXF = new double[10000, 100];
        public double[,] AriaPointYF = new double[10000, 100];
        public double[] AriaAriaF = new double[10000];
        public double[] AriaCenterXF = new double[10000];
        public double[] AriaCenterYF = new double[10000];
        bool result = false;
        double Ytest = 0;
        double Xtest = 0;

        double DikkaMilli = 1.5;
        double AttanDikka = 0.001;
        int round =5;// ممكن 4
        int RoundAria = 5;//ممكن 4
        double DikkaIntersection = 0.01;
        double DekkaOnLine = 0.0001;
        double DekkaOnLine1 = 0.0001;

        #endregion
        public void DoMeshPlease()
        {
            CalculateIntersections();
            forarias = 1;
            AriaNo = 0;
            AriaPointNo = new int[1000];
            AriaPointX = new double[1000, 100];
            AriaPointY = new double[1000, 100];
            FindArias();
            FindINArias();
            FindAriasType();
            CalculateAriaProperty();
        }
        private void CalculateIntersections()
        {
            forarias = 0;
            int m = 0;
            int n = AllLine;
            for (int i = 1; i < n + 1; i++)
            {
                LineX1Real[i] = Math.Round(LineX1Real[i], round);
                LineY1Real[i] = Math.Round(LineY1Real[i], round);
                LineX2Real[i] = Math.Round(LineX2Real[i], round);
                LineY2Real[i] = Math.Round(LineY2Real[i], round);
            }
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
                        if (Math.Abs(PointXReal[k] - TheX0) <= DikkaIntersection & Math.Abs(PointYReal[k] - TheY0) <= DikkaIntersection) goto Nextj;
                    }
                    m = m + 1;
                    PointXReal[m] = TheX0;
                    PointYReal[m] = TheY0;
                Nextj: { }
                }
            }
            for (int i = 1; i < n + 1; i++)
            {
                double x0 = (LineX1Real[i]);
                double y0 = (LineY1Real[i]);
                for (int k = 1; k < m + 1; k++)
                {
                    if (Math.Abs(PointXReal[k] - x0) <= DikkaIntersection & Math.Abs(PointYReal[k] - y0) <= DikkaIntersection) goto Nexti;
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
                    if (Math.Abs(PointXReal[k] - x0) <= DikkaIntersection & Math.Abs(PointYReal[k] - y0) <= DikkaIntersection) goto Nexti;
                }
                m = m + 1;
                PointXReal[m] = x0;
                PointYReal[m] = y0;
            Nexti: { }
            }
            PointNumber2d = m;

            int N = 0;
            for (int j = 1; j < ShapeNumbers + 1; j++)
            {
                if (ShapeType[j] == 1) N = 4;
                if (ShapeType[j] == 2) N = 32;
                if (ShapeType[j] == 3) N = 12;
                if (ShapeType[j] == 4) N = 8;
                for (int i = 1; i < N + 1; i++)
                {
                    ShapePointX[j, i] = Math.Round(ShapePointX[j, i], round);
                    ShapePointY[j, i] = Math.Round(ShapePointY[j, i], round);
                }
            }
            for (int j = 1; j < TieNumbers + 1; j++)
            {
                if (TieType[j] == 1) N = 4;
                if (TieType[j] == 2) N = 32;
                for (int i = 1; i < N + 1; i++)
                {
                    TiePointX[j, i] = Math.Round(TiePointX[j, i], round);
                    TiePointY[j, i] = Math.Round(TiePointY[j, i], round);
                }
            }

            for (int i = 1; i < PointNumber2d + 1; i++)
            {
                PointXReal[i] = Math.Round(PointXReal[i], round);
                PointYReal[i] = Math.Round(PointYReal[i], round);
                for (int j = 1; j < ShapeNumbers + 1; j++)
                {
                    if (ShapeType[j] == 1) N = 4;
                    if (ShapeType[j] == 2) N = 32;
                    if (ShapeType[j] == 3) N = 12;
                    if (ShapeType[j] == 4) N = 8;
                    for (int k = 1; k < N + 1; k++)
                    {
                        double x0 = ShapePointX[j,k];
                        double y0 = ShapePointY[j,k];
                        if (Math.Abs(PointXReal[i] - x0) <= DikkaIntersection & Math.Abs(PointYReal[i] - y0) <= DikkaIntersection)
                        {
                            PointXReal[i] = x0;
                            PointYReal[i] = y0;
                            goto nextI;
                        }
                    }
                }

                for (int j = 1; j < TieNumbers + 1; j++)
                {
                    if (TieType[j] == 1) N = 4;
                    if (TieType[j] == 2) N = 32;
                    for (int k = 1; k < N + 1; k++)
                    {
                        double x0 = TiePointX[j, k];
                        double y0 = TiePointY[j, k];
                        if (Math.Abs(PointXReal[i] - x0) <= DikkaIntersection & Math.Abs(PointYReal[i] - y0) <= DikkaIntersection)
                        {
                            PointXReal[i] = x0;
                            PointYReal[i] = y0;
                            goto nextI;
                        }
                    }
                }
            nextI: { };
            }
        }
        private int CheckifTowPointsOnLine(double x, double y, double X1, double Y1, double X2, double Y2)
        {
            int CHK = 0;
            if (X1 == X2 & X1 == x)
            {
                if (y >= Y1 & y - Y2 <= DekkaOnLine) CHK = 1;
                if (y - Y1 <= DekkaOnLine & y >= Y2) CHK = 1;
            }
            if (Y1 == Y2 & Y1 == y)
            {
                if (x >= X1 & x - X2 <= DekkaOnLine) CHK = 1;
                if (x - X1 <= DekkaOnLine & x >= X2) CHK = 1;
            }
            if (Math.Abs((x - X1) / (X2 - X1) - (y - Y1) / (Y2 - Y1)) <= DekkaOnLine) CHK = 1;
            return CHK;
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
                int[] PointNO0 = new int[PointNumber2d + 1];
                X1 = PointXReal[i];
                Y1 = PointYReal[i];
                m = 1;
                pX[m] = X1;
                pY[m] = Y1;
                #region//ايجاد العقد التي تحيط بالنقطة
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
                            #region//التحقق من أن العقدتين على نفس الخط
                            x = X1;
                            y = Y1;
                            int chk1 = CheckifTowPointsOnLine(x, y, X3, Y3, X4, Y4);
                            x = X2;
                            y = Y2;
                            int chk2 = CheckifTowPointsOnLine(x, y, X3, Y3, X4, Y4);
                            if (chk1 == 1 & chk2 == 1) goto nextk;//تقعان على نفس الخط
                            #endregion
                            checkintersection(X1, Y1, X2, Y2, X3, Y3, X4, Y4);//القطعة بين النقطتين متقاطعة مع خط
                            if (INTERSECTION == 1) goto nextj;//القطعة متقاطعة مع خط
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
                    if (Math.Abs(aTanA[j] - Convert.ToDouble((Math.PI))) < AttanDikka) aTanA[j] = -1 * aTanA[j];
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
                    PointNO0[j] = PointNO[j];
                }
                #endregion
                int[] LastJoint1 = new int[PointADD + 1];
                for (int hg = 1; hg < PointADD + 1; hg++)
                {
                    m = 1;
                    pX[m] = PointXReal[i];
                    pY[m] = PointYReal[i];
                    int checkest = 0;
                    #region// إيجاد أول عقدة ترتبط مع العقدة صفر بمحور
                    for (int j = 1; j < PointADD + 1; j++)
                    {
                        if (LastJoint1[j] == 0)
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
                                int chk1 = CheckifTowPointsOnLine(x, y, X3, Y3, X4, Y4);
                                x = X2;
                                y = Y2;
                                int chk2 = CheckifTowPointsOnLine(x, y, X3, Y3, X4, Y4);
                                if (chk1 == 1 & chk2 == 1)
                                {
                                    LastJoint1[j] = 1;
                                    LastJoint = PointADD;
                                    int nm = 0;
                                    for (int l = 1; l < PointADD + 1; l++)
                                    {
                                        if (l > j)
                                        {
                                            nm = nm + 1;
                                            PointNO0[nm] = PointNO[l];
                                        }
                                    }
                                    int nm1 = nm;
                                    for (int l = 1; l < PointADD - nm1 + 1; l++)
                                    {
                                        nm = nm + 1;
                                        PointNO0[nm] = PointNO[l];
                                    }
                                    m = m + 1;
                                    pX[m] = X2;
                                    pY[m] = Y2;
                                    checkest = 1;
                                    goto Endj;
                                }
                            }
                        }
                    }
                Endj: { }
                    #endregion
                    if (checkest == 0) goto nextgh;//لا يوجد عقدة
                    check0 = 0;
                    for (int j = 1; j < PointADD + 1; j++)
                    {
                        X2 = PointXReal[PointNO0[j]];
                        Y2 = PointYReal[PointNO0[j]];
                        #region//التحقق من أن العقدتين على نفس المحور
                        for (int k = 1; k < AllLine + 1; k++)
                        {
                            X3 = LineX1Real[k];
                            Y3 = LineY1Real[k];
                            X4 = LineX2Real[k];
                            Y4 = LineY2Real[k];
                            x = PointXReal[PointNO0[LastJoint]];
                            y = PointYReal[PointNO0[LastJoint]];
                            int chk1 = CheckifTowPointsOnLine(x, y, X3, Y3, X4, Y4);
                            x = X2;
                            y = Y2;
                            int chk2 = CheckifTowPointsOnLine(x, y, X3, Y3, X4, Y4);
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
                                    int chk10 = CheckifTowPointsOnLine(x, y, X3, Y3, X4, Y4);
                                    x = X2;
                                    y = Y2;
                                    int chk20 = CheckifTowPointsOnLine(x, y, X3, Y3, X4, Y4);
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
                    if (m > 3 & check0 == 1)
                    {
                        #region//تزبيط نقاط المساحة
                        for (int j = 1; j < m + 1; j++)
                        {
                            pX[j] = Math.Round(pX[j], RoundAria);
                            pY[j] = Math.Round(pY[j], RoundAria);
                        }
                        int[] Check = new int[200];
                        for (int j = 1; j < m; j++)
                        {
                            if (Check[j] == 0)
                            {
                                for (int k = 1; k < m; k++)
                                {
                                    if (j != k)
                                    {
                                        if (Math.Abs(pX[j] - pX[k]) <= DikkaMilli & Math.Abs(pY[j] - pY[k]) <= DikkaMilli) Check[k] = 1;
                                        // if (pX[j] == pX[k] & pY[j] == pY[k]) Check[k] = 1;
                                    }
                                }
                            }
                        }
                        int n = 0;
                        double[] pXX = new double[200];
                        double[] pYY = new double[200];
                        for (int j = 1; j < m; j++)
                        {
                            if (Check[j] == 0)
                            {
                                n = n + 1;
                                pXX[n] = pX[j];
                                pYY[n] = pY[j];
                            }
                        }
                        n = n + 1;
                        pXX[n] = pX[m];
                        pYY[n] = pY[m];
                        m = n;
                        for (int j = 1; j < m + 1; j++)
                        {
                            pX[j] = pXX[j];
                            pY[j] = pYY[j];
                        }
                        #endregion
                        if (m > 3)
                        {
                            int tah = 0;
                            int s = 0;
                            for (int j = 1; j < AriaNo + 1; j++)
                            {
                                if (AriaPointNo[j] == m)
                                {
                                    s = 0;
                                    //for (int k = 1; k < m + 1; k++)
                                    for (int k = 1; k < m; k++)
                                    {
                                        x = pX[k];
                                        y = pY[k];
                                        // for (int l = 1; l < m + 1; l++)
                                        for (int l = 1; l < m; l++)
                                        {
                                            if (Math.Abs(x - AriaPointX[j, l]) <= DikkaMilli & Math.Abs(y - AriaPointY[j, l]) <= DikkaMilli) s = s + 1;
                                        }
                                    }
                                    //if (s >= m + 1)
                                    if (s >= (m - 1))
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

                                if (pX[1] != pX[m]) tahtah1 = 0;
                                if (pY[1] != pY[m]) tahtah2 = 0;

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
                    }
                    #endregion
                    pX = new double[PointNumber2d + 2];
                    pY = new double[PointNumber2d + 2];
                nextgh: { }
                }
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
                        for (int k = 1; k < AriaPointNoF[m] + 1; k++)
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
        private void CheckifAriainShape()
        {
            for (int i = 1; i < ShapeNumbers + 1; i++)
            {
                int N = 0;
                if (ShapeType[i] == 1) N = 4 + 1;
                if (ShapeType[i] == 2) N = 32 + 1;
                if (ShapeType[i] == 3) N = 12 + 1;
                if (ShapeType[i] == 4) N = 8 + 1;
                double[] polygonX = new double[N + 1];
                double[] polygonY = new double[N + 1];
                for (int j = 1; j < N; j++)
                {
                    polygonX[j] = ShapePointX[i,j];
                    polygonY[j] = ShapePointY[i,j];
                }
                polygonX[N] = polygonX[1];
                polygonY[N] = polygonY[1];
                result = false;
                int nvert = N;
                int k, l;
                for (k = 1, l = nvert - 1; k < nvert; l = k++)
                {
                    double awal = Math.Round((polygonX[l] - polygonX[k]) * (Ytest - polygonY[k]) / (polygonY[l] - polygonY[k]) + polygonX[k], round);
                    if (((polygonY[k] > Ytest) != (polygonY[l] > Ytest)) && (Xtest < awal)) result = !result;
                }
                #region
                double x = Xtest;
                double y = Ytest;
                double X3 = 0;
                double Y3 = 0;
                double X4 = 0;
                double Y4 = 0;
                for (int j = 1; j < N; j++)
                {
                    X3 = polygonX[j];
                    Y3 = polygonY[j];
                    if (j < (N - 1))
                    {
                        X4 = polygonX[j + 1];
                        Y4 = polygonY[j + 1];
                    }
                    else
                    {
                        X4 = polygonX[1];
                        Y4 = polygonY[1];
                    }
                    int chk1 = 0;
                    if (X3 == X4 & X3 == x)
                    {
                        if (y >= Y3 & y - Y4 <= DekkaOnLine1) chk1 = 1;
                        if (y - Y3 <= DekkaOnLine1 & y >= Y4) chk1 = 1;
                    }
                    if (Y3 == Y4 & Y3 == y)
                    {
                        if (x >= X3 & x - X4 <= DekkaOnLine1) chk1 = 1;
                        if (x - X3 <= DekkaOnLine1 & x >= X4) chk1 = 1;
                    }
                    if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= DekkaOnLine1) chk1 = 1;
                    if (chk1 == 1)
                    {
                        result = false;
                        break;
                    }
                }
                #endregion
                if (result == true)
                {
                    goto endend;
                }         
            }
        endend: { };
        }

        private void FindAriasType()
        {
            if (TieNumbers == 0)
            {
                for (int i = 1; i < AriaNoF + 1; i++)
                {
                    AriaTypeF[i] = 2;//OUT
                }
            }
            else
            {
                for (int i = 1; i < AriaNoF + 1; i++)
                {
                    for (int j = 1; j < AriaPointNoF[i] + 1; j++)
                    {
                        Xtest = AriaPointXF[i, j];
                        Ytest = AriaPointYF[i, j];
                        CheckifAriainTie();
                        if (result == true) goto NextI;
                    }
                NextI: { }
                    if (result == false)
                    {
                        AriaTypeF[i] = 2;//OUT
                    }
                    else
                    {
                        AriaTypeF[i] = 1;//IN
                    }
                }
            }
        }
        private void CheckifAriainTie()
        {
            result = false;
            #region
            for (int i = 1; i < TieNumbers + 1; i++)
            {
                int N = 0;
                if (TieType[i] == 1) N = 4 + 1;
                if (TieType[i] == 2) N = 32 + 1;
                double[] polygonX = new double[N + 1];
                double[] polygonY = new double[N + 1];
                for (int j = 1; j < N; j++)
                {
                    polygonX[j] = TiePointX[i, j];
                    polygonY[j] = TiePointY[i, j];
                }
                polygonX[N] = polygonX[1];
                polygonY[N] = polygonY[1];
                int nvert = N;
                int k, l;
                for (k = 1, l = nvert - 1; k < nvert; l = k++)
                {
                    double awal = Math.Round((polygonX[l] - polygonX[k]) * (Ytest - polygonY[k]) / (polygonY[l] - polygonY[k]) + polygonX[k], round);
                    if (((polygonY[k] > Ytest) != (polygonY[l] > Ytest)) && (Xtest < awal)) result = !result;
                }
                double x = Xtest;
                double y = Ytest;
                double X3 = 0;
                double Y3 = 0;
                double X4 = 0;
                double Y4 = 0;
                for (int j = 1; j < N; j++)
                {
                    X3 = polygonX[j];
                    Y3 = polygonY[j];
                    if (j < (N - 1))
                    {
                        X4 = polygonX[j + 1];
                        Y4 = polygonY[j + 1];
                    }
                    else
                    {
                        X4 = polygonX[1];
                        Y4 = polygonY[1];
                    }
                    int chk1 = 0;
                    if (X3 == X4 & X3 == x)
                    {
                        if (y >= Y3 & y - Y4 <= DekkaOnLine1) chk1 = 1;
                        if (y - Y3 <= DekkaOnLine1 & y >= Y4) chk1 = 1;
                    }
                    if (Y3 == Y4 & Y3 == y)
                    {
                        if (x >= X3 & x - X4 <= DekkaOnLine1) chk1 = 1;
                        if (x - X3 <= DekkaOnLine1 & x >= X4) chk1 = 1;
                    }
                    if (Math.Abs((x - X3) / (X4 - X3) - (y - Y3) / (Y4 - Y3)) <= DekkaOnLine1) chk1 = 1;
                    if (chk1 == 1)
                    {
                        result = false;
                        break;
                    }
                }
                if (result == true)
                {
                    goto endend;
                }
            }
            #endregion
        endend: { };
        }

        public void CalculateAriaProperty()
        {
            for (int i = 1; i < AriaNoF + 1; i++)
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
                for (int j = 1; j < AriaPointNoF[i]; j++)
                {
                    P1 = j;
                    P2 = j + 1;
                    ALAWAL = ALAWAL + AriaPointXF[i, P1] * AriaPointYF[i, P2];
                    ALTHANI = ALTHANI + AriaPointYF[i, P1] * AriaPointXF[i, P2];
                    XFORCG = XFORCG + ((AriaPointXF[i, P1] + AriaPointXF[i, P2]) * ((AriaPointXF[i, P1] * AriaPointYF[i, P2]) - (AriaPointYF[i, P1] * AriaPointXF[i, P2]))) / 6;
                    YFORCG = YFORCG + ((AriaPointYF[i, P1] + AriaPointYF[i, P2]) * ((AriaPointYF[i, P1] * AriaPointXF[i, P2]) - (AriaPointXF[i, P1] * AriaPointYF[i, P2]))) / 6;
                }
                ARIA = Math.Abs((ALAWAL - ALTHANI) / 2);
                CENTERX = (XFORCG) / ARIA;
                CENTERY = -(YFORCG) / ARIA;
                AriaAriaF[i] = ARIA;
                AriaCenterXF[i] = CENTERX;
                AriaCenterYF[i] = CENTERY;
            }
        }
        private void checkintersection(double X1, double Y1, double X2, double Y2, double X3, double Y3, double X4, double Y4)
        {
            int THETAHKIK = 0;
            double x0 = 0;
            double y0 = 0;
            #region//شاقولي و أفقي
            if (X2 == X1 & Y3 == Y4)
            {
                int tah = 0;
                if (X1 < X3 & X1 < X4) tah = 1;
                if (X1 > X3 & X1 > X4) tah = 1;
                if (Y3 < Y1 & Y3 < Y2) tah = 1;
                if (Y3 > Y1 & Y3 > Y2) tah = 1;
                if (tah == 1) goto end100;
                x0 = X1;
                y0 = Y3;
                x0 = Math.Round(x0, round);
                y0 = Math.Round(y0, round);
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
            #endregion
            #region//شاقولي و مائل
            if (X2 == X1 & Y3 != Y4 & X4 != X3)
            {
                x0 = X1;
                y0 = ((x0 - X3) / (X4 - X3)) * (Y4 - Y3) + Y3;
                int tah = 0;
                if (y0 < Y1 & y0 < Y2) tah = 1;
                if (y0 > Y1 & y0 > Y2) tah = 1;
                if (tah == 1) goto end100;
                x0 = Math.Round(x0, round);
                y0 = Math.Round(y0, round);
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
            #endregion
            #region//حالة عامة
            double bast = (Y3 - Y1) * (X4 - X3) * (X2 - X1);
            bast = bast + X1 * (Y2 - Y1) * (X4 - X3);
            bast = bast - X3 * (Y4 - Y3) * (X2 - X1);
            double makam = (Y2 - Y1) * (X4 - X3);
            makam = makam - (Y4 - Y3) * (X2 - X1);
            if (makam != 0)
            {
                x0 = bast / makam;
                y0 = (((Y2 - Y1) / (X2 - X1)) * (x0 - X1)) + Y1;
                x0 = Math.Round(x0, round);
                y0 = Math.Round(y0, round);
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
            #endregion
        end100: { }
            if (forarias == 1)
            {
                if (Math.Abs(X1 - x0) < DikkaIntersection & Math.Abs(Y1 - y0) < DikkaIntersection) THETAHKIK = 0;
                if (Math.Abs(X2 - x0) < DikkaIntersection & Math.Abs(Y2 - y0) < DikkaIntersection) THETAHKIK = 0;
            }
            INTERSECTION = THETAHKIK;
            TheX0 = x0;
            TheY0 = y0;
        }
    }
}
