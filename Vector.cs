using System;
namespace LightCurve
{
    public class Vector
    {
        private int nElem;
        public double[] a;
        public Vector(int nElem)
        {
            this.nElem = nElem;
            this.a = new double[nElem];
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = 0;
            }
        }

        public Vector(double[] arr)
        {
            this.nElem = arr.Length;
            this.a = new double[nElem];
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = arr[i];
            }
        }


        public void SetElement(double val, int i)
        {
            a[i] = val;
        }

        public double GetElem(int i)
        {
            return a[i];
        }

        //Свойства - ничего непонятно, но очень интересно! Можно обращаться через точку 
        public int Dim
        {
            get { return nElem; }
        }

        public double[] Elems
        {
            get { return a; }
        }


        //так не оч удобно получать элемент, а лучше через Индексатор
        //value - зарезервированное слова в c#
        public double this[int i]
        {
            get { return a[i]; }
            set { a[i] = value; }
        }


        // метод для решения слу
        public Vector Solve(Vector b)
        {
            return b;
        }
    }
}
