using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra.Double;


namespace CSE462_HW3
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] s = new double[4, 2] { { 1, 2 }, { 5, 3 }, { 4, 10 }, { 1, 6 } };
            double[,] d = new double[4, 2] { { 1, 2 }, { 5, 3 }, { 4, 10 }, { 1, 6 } };
            Tests.Test1_1(s,d);
            double[,] hm = Part1.FindHomographyMatrix(s, d);
            double[,] xy = new double[3, 1] { { s[2, 0] }, { s[2, 1] }, { 1 } };
            Tests.Test1_3(hm, xy);
            double[,] uv = new double[3, 1] { { d[1, 0] }, { d[1, 1] }, { 1 } };
            Tests.Test1_4(hm, uv);
            double[,] s2 = new double[5, 2] { { 100, 100 }, { 300, 200 }, { 100, 400 }, { 200, 500 }, { 200, 300 } };

            // image 1
            double[,] d1 = new double[5, 2] { { 755, 706 }, { 1221, 933 }, { 767, 1393 }, { 997, 1624 }, { 994, 1166 } };
            // image 2
            double[,] d2 = new double[5, 2] { { 740, 508 }, { 1208, 732 }, { 724, 1208 }, { 960, 1448 }, { 968, 968 } };
            // image 3
            double[,] d3 = new double[5, 2] { { 876, 748 }, { 1376, 980 }, { 924, 1460 }, { 1168, 1664 }, { 1148, 1220 } };
            Tests.Test1_5(s2, d1,d2,d3);
            double[,] p1 = new double[3, 1] { { 7.5f }, { 5.5f }, { 1 } };
            double[,] p2 = new double[3, 1] { { 6.3f }, { 3.3f }, { 1 } };
            double[,] p3 = new double[3, 1] { { 0.1f }, { 0.1f }, { 1 } };
            Tests.Test1_6(s2, d1, d2, d3, p1, p2, p3);
            double[,] i1 = new double[3, 1] { { 500 }, { 400 }, { 1 } };
            double[,] i2 = new double[3, 1] { { 86 }, { 167 }, { 1 } };
            double[,] i3 = new double[3, 1] { { 10 }, { 10 }, { 1 } };
            Tests.Test1_7(s2, d1, d2, d3, i1, i2, i3);

        }


    }
    class Tests
    {
        public static void Test1_1(double[,] s, double[,] d)
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine("Test 1_1");
            Console.WriteLine();
            double[,] hm = Part1.FindHomographyMatrix(s, d);

            for (int i = 0; i < hm.GetLength(0); i++)
            {
                for (int j=0; j < hm.GetLength(1); j++)
                {
                    Console.Write(hm[i, j] + "     ");
                }
                Console.WriteLine();
                Console.WriteLine();
            }

        }
        public static void Test1_3(double[,] hm, double[,] xy)
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine("Test 1_3");
            Console.WriteLine();
            double[,] match = Part1.FindProjection(hm, xy);
            Console.WriteLine("(x,y) : " + xy[0, 0] + " , " + xy[1, 0] + " , " + xy[2, 0]);
            Console.WriteLine("(u,v) : " + match[0, 0] + " , " + match[1, 0] + " , " + match[2, 0]);
        }
        public static void Test1_4(double[,] hm, double[,] uv)
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine("Test 1_4");
            Console.WriteLine();
            double[,] match = Part1.FindProjectionInverse(hm, uv);
            Console.WriteLine("(u,v) : " + uv[0, 0] + " , " + uv[1, 0] + " , " + uv[2, 0]);
            Console.WriteLine("(x,y) : " + match[0, 0] + " , " + match[1, 0] + " , " + match[2, 0]);
        }
        public static void Test1_5(double[,] s, double[,] d1, double[,] d2, double[,] d3)
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine("Test 1_5");
            List<double[,]> P = new List<double[,]>();  // points (x, y)
            List<double[,]> aP = new List<double[,]>(); // actual point correspondences (u, v)

            double[,] hm1 = Part1.FindHomographyMatrix(s, d1);
            double[,] hm2 = Part1.FindHomographyMatrix(s, d2);
            double[,] hm3 = Part1.FindHomographyMatrix(s, d3);

            P.Add(new double[3, 1] { { 900 }, { 100 }, { 1 } });
            P.Add(new double[3, 1] { { 800 }, { 300 }, { 1 } });
            P.Add(new double[3, 1] { { 700 }, { 400 }, { 1 } });

            /// Image 1 
            Console.WriteLine("Projecting Image 1");
            aP.Add(new double[3, 1] { { 2612 }, { 681 }, { 1 } });
            aP.Add(new double[3, 1] { { 2379 }, { 1151 }, { 1 } });
            aP.Add(new double[3, 1] { { 2145 }, { 1381 }, { 1 } });

            Part1.FindPointMatchesAndErrors(hm1, P, aP);

            /// Image 2
            Console.WriteLine("Projecting Image 2");
            aP = new List<double[,]>(); // actual point correspondences (u, v)

            aP.Add(new double[3, 1] { { 2612 }, { 476 }, { 1 } });
            aP.Add(new double[3, 1] { { 2396 }, { 948 }, { 1 } });
            aP.Add(new double[3, 1] { { 2160 }, { 988 }, { 1 } });

            Part1.FindPointMatchesAndErrors(hm2, P, aP);


            /// Image 3 
            Console.WriteLine("Projecting Image 3");
            aP = new List<double[,]>(); // actual point correspondences (u, v)

            aP.Add(new double[3, 1] { { 2664 }, { 712 }, { 1 } });
            aP.Add(new double[3, 1] { { 2444 }, { 1152 }, { 1 } });
            aP.Add(new double[3, 1] { { 2240 }, { 1376 }, { 1 } });

            Part1.FindPointMatchesAndErrors(hm3, P, aP);
        }
        public static void Test1_6(double[,] s, double[,] d1, double[,] d2, double[,] d3, double[,] p1, double[,] p2, double[,] p3)
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine("Test 1_6");
            double[,] hm1 = Part1.FindHomographyMatrix(s, d1);
            double[,] hm2 = Part1.FindHomographyMatrix(s, d2);
            double[,] hm3 = Part1.FindHomographyMatrix(s, d3);

            Console.WriteLine("Projection For Image 1");
            double[,] match = Part1.FindProjection(hm1, p1);
            Console.WriteLine("(x,y) : " + p1[0, 0] + " , " + p1[1, 0] + " , " + p1[2, 0]);
            Console.WriteLine("(u,v) : " + match[0, 0] + " , " + match[1, 0] + " , " + match[2, 0]);
            match = Part1.FindProjection(hm1, p2);
            Console.WriteLine("(x,y) : " + p2[0, 0] + " , " + p2[1, 0] + " , " + p2[2, 0]);
            Console.WriteLine("(u,v) : " + match[0, 0] + " , " + match[1, 0] + " , " + match[2, 0]);
            match = Part1.FindProjection(hm1, p3);
            Console.WriteLine("(x,y) : " + p3[0, 0] + " , " + p3[1, 0] + " , " + p3[2, 0]);
            Console.WriteLine("(u,v) : " + match[0, 0] + " , " + match[1, 0] + " , " + match[2, 0]);
            Console.WriteLine("Projection For Image 2");
            match = Part1.FindProjection(hm2, p1);
            Console.WriteLine("(x,y) : " + p1[0, 0] + " , " + p1[1, 0] + " , " + p1[2, 0]);
            Console.WriteLine("(u,v) : " + match[0, 0] + " , " + match[1, 0] + " , " + match[2, 0]);
            match = Part1.FindProjection(hm2, p2);
            Console.WriteLine("(x,y) : " + p2[0, 0] + " , " + p2[1, 0] + " , " + p2[2, 0]);
            Console.WriteLine("(u,v) : " + match[0, 0] + " , " + match[1, 0] + " , " + match[2, 0]);
            match = Part1.FindProjection(hm2, p3);
            Console.WriteLine("(x,y) : " + p3[0, 0] + " , " + p3[1, 0] + " , " + p3[2, 0]);
            Console.WriteLine("(u,v) : " + match[0, 0] + " , " + match[1, 0] + " , " + match[2, 0]);
            Console.WriteLine("Projection For Image 3");
            match = Part1.FindProjection(hm3, p1);
            Console.WriteLine("(x,y) : " + p1[0, 0] + " , " + p1[1, 0] + " , " + p1[2, 0]);
            Console.WriteLine("(u,v) : " + match[0, 0] + " , " + match[1, 0] + " , " + match[2, 0]);
            match = Part1.FindProjection(hm3, p2);
            Console.WriteLine("(x,y) : " + p2[0, 0] + " , " + p2[1, 0] + " , " + p2[2, 0]);
            Console.WriteLine("(u,v) : " + match[0, 0] + " , " + match[1, 0] + " , " + match[2, 0]);
            match = Part1.FindProjection(hm3, p3);
            Console.WriteLine("(x,y) : " + p3[0, 0] + " , " + p3[1, 0] + " , " + p3[2, 0]);
            Console.WriteLine("(u,v) : " + match[0, 0] + " , " + match[1, 0] + " , " + match[2, 0]);
        }
        public static void Test1_7(double[,] s, double[,] d1, double[,] d2, double[,] d3, double[,] i1, double[,] i2, double[,] i3)
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine("Test 1_7");
            double[,] hm1 = Part1.FindHomographyMatrix(s, d1);
            double[,] hm2 = Part1.FindHomographyMatrix(s, d2);
            double[,] hm3 = Part1.FindHomographyMatrix(s, d3);

            Console.WriteLine("Projection For Image 1");
            double[,] match = Part1.FindProjectionInverse(hm1, i1);
            Console.WriteLine("(x,y) : " + i1[0, 0] + " , " + i1[1, 0] + " , " + i1[2, 0]);
            Console.WriteLine("(u,v) : " + match[0, 0] + " , " + match[1, 0] + " , " + match[2, 0]);
            match = Part1.FindProjectionInverse(hm1, i2);
            Console.WriteLine("(x,y) : " + i2[0, 0] + " , " + i2[1, 0] + " , " + i2[2, 0]);
            Console.WriteLine("(u,v) : " + match[0, 0] + " , " + match[1, 0] + " , " + match[2, 0]);
            match = Part1.FindProjectionInverse(hm1, i3);
            Console.WriteLine("(x,y) : " + i3[0, 0] + " , " + i3[1, 0] + " , " + i3[2, 0]);
            Console.WriteLine("(u,v) : " + match[0, 0] + " , " + match[1, 0] + " , " + match[2, 0]);
            Console.WriteLine("Projection For Image 2");
            match = Part1.FindProjectionInverse(hm2, i1);
            Console.WriteLine("(x,y) : " + i1[0, 0] + " , " + i1[1, 0] + " , " + i1[2, 0]);
            Console.WriteLine("(u,v) : " + match[0, 0] + " , " + match[1, 0] + " , " + match[2, 0]);
            match = Part1.FindProjectionInverse(hm2, i2);
            Console.WriteLine("(x,y) : " + i2[0, 0] + " , " + i2[1, 0] + " , " + i2[2, 0]);
            Console.WriteLine("(u,v) : " + match[0, 0] + " , " + match[1, 0] + " , " + match[2, 0]);
            match = Part1.FindProjectionInverse(hm2, i3);
            Console.WriteLine("(x,y) : " + i3[0, 0] + " , " + i3[1, 0] + " , " + i3[2, 0]);
            Console.WriteLine("(u,v) : " + match[0, 0] + " , " + match[1, 0] + " , " + match[2, 0]);
            Console.WriteLine("Projection For Image 3");
            match = Part1.FindProjectionInverse(hm3, i1);
            Console.WriteLine("(x,y) : " + i1[0, 0] + " , " + i1[1, 0] + " , " + i1[2, 0]);
            Console.WriteLine("(u,v) : " + match[0, 0] + " , " + match[1, 0] + " , " + match[2, 0]);
            match = Part1.FindProjectionInverse(hm3, i2);
            Console.WriteLine("(x,y) : " + i2[0, 0] + " , " + i2[1, 0] + " , " + i2[2, 0]);
            Console.WriteLine("(u,v) : " + match[0, 0] + " , " + match[1, 0] + " , " + match[2, 0]);
            match = Part1.FindProjectionInverse(hm3, i3);
            Console.WriteLine("(x,y) : " + i3[0, 0] + " , " + i3[1, 0] + " , " + i3[2, 0]);
            Console.WriteLine("(u,v) : " + match[0, 0] + " , " + match[1, 0] + " , " + match[2, 0]);
        }

    }
    class Part1
    {

        #region Public_Static_Methods

        public static double[,] FindHomographyMatrix(double[,] s, double[,] d)
        {
            var x = FindHomography(s, d);
            double[,] hm = new double[3, 3];

            int row = 0;
            for (int i = 0; i < x.Length; i++)
            {
                if (i % 3 == 0 && i != 0)
                    ++row;
                hm[row, i % 3] = x[i];
            }


            return hm;
        }
        #endregion

        #region Private_Static_Methods

        private static double[] FindHomography(double[,] s, double[,] d)
        {
            int N = s.GetLength(0);
            int M = 9;

            double[,] A = new double[2 * N, M];

            // current point
            int p = 0;
            for (int i = 0; i < 2 * N; i++)
            {
                if (i % 2 == 0)
                {
                    A[i, 0] = -1 * s[p, 0];
                    A[i, 1] = -1 * s[p, 1];
                    A[i, 2] = -1;
                    A[i, 3] = 0;
                    A[i, 4] = 0;
                    A[i, 5] = 0;
                    A[i, 6] = s[p, 0] * d[p, 0];
                    A[i, 7] = d[p, 0] * s[p, 1];
                    A[i, 8] = d[p, 0];
                }
                else
                {
                    A[i, 0] = 0;
                    A[i, 1] = 0;
                    A[i, 2] = 0;
                    A[i, 3] = -1 * s[p, 0];
                    A[i, 4] = -1 * s[p, 1];
                    A[i, 5] = -1;
                    A[i, 6] = s[p, 0] * d[p, 1];
                    A[i, 7] = d[p, 1] * s[p, 1];
                    A[i, 8] = d[p, 1];
                    ++p;
                }
            }

            var mat = DenseMatrix.OfArray(A);
            var svd = mat.Svd(true);

            return svd.VT.Row(svd.VT.RowCount - 1).ToArray();

        }

        public static double[,] FindProjectionInverse(double[,] hm, double[,] a)
        {
            var m = DenseMatrix.OfArray(hm).Inverse();
            return FindProjection(m.ToArray(), a);
        }

        public static double[,] FindProjection(double[,] hm, double[,] a)
        {
            int m = hm.GetLength(0);
            int n = a.GetLength(1);

            double[,] tmp = new double[m, n];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    tmp[i, j] = 0;
                    for (int k = 0; k < a.GetLength(1); k++)
                    {
                        tmp[i, j] += a[i, k] * a[k, j];
                    }
                }
            }
            double[,] res = new double[,] { { tmp[0, 0] / tmp[2, 0] }, { tmp[1, 0] / tmp[2, 0] }, { 1 } };

            return res;
        }
        // Percentage
        public static float CalculateError(double[,] result, double[,] actual)
        {
            var x1 = result[0, 0];
            var x2 = actual[0, 0];

            var y1 = result[1, 0];
            var y2 = actual[1, 0];

            float resDif = MathF.Sqrt(MathF.Pow((float)(x1), 2) + MathF.Pow((float)(y1), 2));
            float actualDif = MathF.Sqrt(MathF.Pow((float)(x2), 2) + MathF.Pow((float)(y2), 2));

            return MathF.Abs((actualDif - resDif) / actualDif * 100);
        }

        public static void FindPointMatchesAndErrors(double[,] hm,List<double[,]> P, List<double[,]> actualP)
        {
            Console.WriteLine("Calculated Homography Matrix : \n\n");
            for (int i = 0; i < hm.GetLength(0); i++)
            {
                for (int j = 0; j < hm.GetLength(1); j++)
                    Console.Write(hm[i, j] + "    ");
                Console.WriteLine();
            }
            Console.WriteLine("Calculating Projection of points...");

            int k = 0;
            foreach (double[,] xy in P)
            {
                double[,] uv = actualP[k++];
                var res = Part1.FindProjection(hm, xy);     // projection
                Console.WriteLine("Error : %" + Part1.CalculateError(res, uv)); // error
            }
        }
        #endregion

    }

}
