using System;
namespace LightCurve
{
    public class ValdermorndeMatrix:Matrix
    {
        public ValdermorndeMatrix(double[] t, int power) : base(t.Length, power + 1)
        {
            for (int i = 0; i < t.Length; i++) {
                for (int j = 0; j < power + 1; j++) {

                    this.matrix[i][j] = Math.Pow(t[i], j);
                }
            }
        }
    }
}
