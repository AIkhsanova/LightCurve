using System;

namespace LightCurve
{
    class Program
    {
        static void Main(string[] args)
        {

            //lightCurve.Save("test.csv");

            LightCurveClass lightCurve = new LightCurveClass("20200728_rtt150_results.csv");
            double[] t = lightCurve.Times;
            double[] f = lightCurve.Fluxes;
            double t0 = t[0];
            //foreach (int i in t) { i-= t0; }
            for (int i = 0; i < f.Length; i++) { t[i] -= t0; }
            int power = 2;
            Polynom p=LightCurveService.Fit(t,f,power);
            for (int i = 0; i < f.Length; i++) {
                f[i] = p.getValue(t[i]);
                t[i] += t0;
            }
            LightCurveClass lightCurve1 = new LightCurveClass(t, f, lightCurve.TimeScale, lightCurve.Filter);
            lightCurve1.Save("test.csv");
            Console.WriteLine("THE END!");
           
        }
    }
}
