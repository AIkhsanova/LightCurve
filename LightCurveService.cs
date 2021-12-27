using System;
namespace LightCurve
{
    public static class LightCurveService
    {
        public static Polynom Fit(double[] x, double[] y,int power)
        {
            Vector yVector = new Vector(y);
            ValdermorndeMatrix a = new ValdermorndeMatrix(x, power);
            Matrix ata = a.Transpose() * a;
            Vector aty = a.Transpose() * yVector;
            Vector m = ata.Solve(aty);//решается система Ax=y
            Polynom p = new Polynom(m.Elems);
            return p;
        }
    }





    public class Polynom
    {
        double[] m;
        public Polynom(double[] m)
        {
            this.m = m;
        }

        //возвращает значение полинома

        public double getValue(double x)
        {
            double s = 0;
            for (int i = 0; i < m.Length; i++)
            {
                s += m[i] *= Math.Pow(x, i);
            }
            return s;
        }

           
    }

}
