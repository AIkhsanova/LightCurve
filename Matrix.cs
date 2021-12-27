using System;
namespace LightCurve
{
    public class Matrix
    {
        protected int nColumns, nRows;
        protected double[][] matrix;
        protected int NColumns { get { return nColumns; } }
        protected int NRows { get { return nRows; } }

        public Matrix(int nRows, int nColumns)

        {
            this.nColumns = nColumns;
            this.nRows = nRows;

            matrix = new double[nRows][];
            for (int i = 0; i < matrix.Length; i++)
            {
                matrix[i] = new double[nColumns];
                for (int j = 0; j < nColumns; j++) { matrix[i][j] = 0; }
            }


        }

        public double[][] getMassive()
        {
            return matrix;
        }
        public double this[int i, int j]
        {

            get { return matrix[i][j]; }
            set { matrix[i][j] = value; }
        }

        public double Norm2()
        {
            double norm2 = 0;
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < nColumns; j++) { norm2 += Math.Pow(matrix[i][j], 2); }
            }
            return Math.Pow(norm2, 0.5);
        }

        public static Matrix SumMatrix(Matrix a, Matrix b)
        {
            Matrix res = new Matrix(a.NRows, a.NColumns);
            for (int i = 0; i < res.NRows; i++)
            {
                for (int j = 0; j < res.NColumns; j++) { res[i, j] = a[i, j] + b[i, j]; }
            }
            return res;

        }

        //сложим используя оператор
        public static Matrix operator +(Matrix a, Matrix b)
        {
            Matrix res = new Matrix(a.NRows, a.NColumns);
            for (int i = 0; i < res.NRows; i++)
            {
                for (int j = 0; j < res.NColumns; j++) { res[i, j] = a[i, j] + b[i, j]; }
            }
            return res;
        }

        //умножение матриц
        public static Matrix operator *(Matrix a, Matrix b)
        {
           
            Matrix matrixC = new Matrix(a.NRows, b.NColumns);

            for (int i = 0; i < matrixC.NRows; i++)
            {
                for (int j = 0; j < matrixC.NColumns; j++)
                {
                    matrixC[i, j] = 0;

                    for (var k = 0; k < b.NColumns; k++)
                    {
                        matrixC[i, j] += a[i, k] * b[k, j];
                    }
                }
            }

            return matrixC;
        }

        //умножение матрицы на вектор
        public static Vector operator *(Matrix a, Vector b)
        {
            Vector res = new Vector(a.nRows);
            for (int i = 0; i < res.Dim; i++)
            {
                double s = 0;
                for (int k = 0; k < res.Dim; k++)
                {
                    s += a[i, k] * b[k];

                }
                res[i] = s;
            }
            return res;
        }

        //решение СЛУ

        public Vector Solve(Vector b)
        {
            Matrix m = new Matrix(nRows, nColumns);
            for (int i = 0; i < m.NRows; i++)
                for (int j = 0; j < m.NColumns; j++)
                    m[i, j] = matrix[i][j];
            Vector l = new Vector(b.Dim);
            for (int i = 0; i < l.Dim; i++)
                l[i] = b[i];

            int N = l.Dim;

            //double[] x = new double[N];
            Vector x = new Vector(m.NRows);

            // Приведение матрицы m к треугольному виду
            for (int s = 0; s <= N - 2; s++)
            {
                double k1 = m[s, s];

                for (int c = s; c <= N - 1; c++)
                {
                    m[s, c] = m[s, c] / k1;
                }

                l[s] = l[s] / k1;
                for (int s1 = s + 1; s1 <= N - 1; s1++)
                {
                    double k2 = m[s1, s];
                    for (int c1 = s; c1 <= N - 1; c1++)
                    {
                        m[s1, c1] = -m[s, c1] * k2 + m[s1, c1];
                    }
                    l[s1] = -l[s] * k2 + l[s1];
                }
            }
            return x;

        }

        public Matrix Transpose()
            {
                Matrix res = new Matrix(nColumns, nRows);
                for (int i = 0; i < NColumns; i++)
                {
                    for (int j = 0; j < nRows; j++)
                    {
                        res[i, j] = this.matrix[j][i]; //Индексаторы матрицы для i,j
                    }
                }
                return res;
            }
    }

}


//    int N = b1.Dim;
                //    double[][] m = Array.Copy(this.getMassive());
                //    double[] x = new double[N];

                //    // Приведение матрицы m к треугольному виду
                //    for (int s = 0; s <= N - 2; s++)
                //    {
                //        double k1 = m[s][s];

                //        for (int c = s; c <= N - 1; c++)
                //        {
                //            m[s][c] = m[s][c] / k1;
                //        }

                //        l[s] = l[s] / k1;
                //        for (int s1 = s + 1; s1 <= N - 1; s1++)
                //        {
                //            double k2 = m[s1][s];
                //            for (int c1 = s; c1 <= N - 1; c1++)
                //            {
                //                m[s1][c1] = -m[s][c1] * k2 + m[s1][c1];
                //            }
                //            l[s1] = -l[s] * k2 + l[s1];
                //        }
                //    }
                //    // обратный ход
                //    x[N - 1] = l[N - 1] / m[N - 1][N - 1];
                //    for (int i = N - 2; i >= 0; i--)
                //    {
                //        double w = 0;
                //        for (int j = N - 1; j > i; j--)
                //        {
                //            w = w + x[j] * m[i][j];
                //        }
                //        x[i] = (l[i] - w);
                //    }
                //    return x;
                //}




                //int N = b.Dim;
                ////Matrix matrix = this.matrix; нужно копировать поэлементно и [][]->[,]
                ////Vector b = b;
                //Matrix matrix=Copy
                //Vector x = new Vector(N);

                //// Приведение матрицы m к треугольному виду
                //for (int s = 0; s <= N - 2; s++)
                //{
                //    double k1 = matrix[s][s];

                //    for (int c = s; c <= N - 1; c++)
                //    {
                //        matrix[s][c] = matrix[s][c] / k1;
                //    }

                //    b[s] = b[s] / k1;
                //    for (int s1 = s + 1; s1 <= N - 1; s1++)
                //    {
                //        double k2 = matrix[s1][s];
                //        for (int c1 = s; c1 <= N - 1; c1++)
                //        {
                //            matrix[s1][c1] = -matrix[s][c1] * k2 + matrix[s1][c1];
                //        }
                //        b[s1] = -b[s] * k2 + b[s1];
                //    }
                //}
                //// обратный ход
                //x[N - 1] = b[N - 1] / matrix[N - 1][N - 1];
                //for (int i = N - 2; i >= 0; i--)
                //{
                //    double w = 0;
                //    for (int j = N - 1; j > i; j--)
                //    {
                //        w = w + x[j] * matrix[i][j];
                //    }
                //    x[i] = (b[i] - w);
                //}
                //return x;