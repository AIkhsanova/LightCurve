using System;
using System.IO;
namespace LightCurve
{
    public class LightCurveClass
    {
        double[] times;
        double[] fluxes,errors;
        string filter, timescale;

        public LightCurveClass(string path)
        {
            //Класс для считывания строки. Инициализируется через путь к файлу, где лежат данные
            StreamReader sr = new StreamReader(path);
            string all = sr.ReadToEnd();//весь текст
            sr.Close();
            //весь текст разбиваем на строки. 1й аргумент - разделитель.Второй -член элемент перечисления StringSplitOptions (как enum)
            string[] lines = all.Split(new string[] { "\n", "\r\n" },
                StringSplitOptions.RemoveEmptyEntries);
            string[] parts = lines[0].Split(new string[] { " ", "\t" },
                StringSplitOptions.RemoveEmptyEntries);

            timescale = parts[0];
            filter = parts[1];
            times = new double[lines.Length - 1];
            fluxes = new double[lines.Length - 1];//-1 - не считаем шапку
            errors = new double[lines.Length - 1];

            for (int i = 0; i < times.Length; i++) {
                parts = lines[i + 1].Split(new string[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                //Конвертация строки в double .InvariantCulture - в качестве разделителя используется точка
                times[i] = Convert.ToDouble(parts[0],
                    System.Globalization.CultureInfo.InvariantCulture);
                fluxes[i] = Convert.ToDouble(parts[1],
                    System.Globalization.CultureInfo.InvariantCulture);
                if (parts.Length == 3) {
                    errors[i] = Convert.ToDouble(parts[2],
                        System.Globalization.CultureInfo.InvariantCulture);
                }
            }
        }

        public LightCurveClass(double[] times, double[] fluxes, double[] errors, string filter, string timescale) {
            this.times = times;
            this.fluxes = fluxes;
            this.errors = errors;
            this.filter = filter;
            this.timescale = timescale;
        }
        public LightCurveClass(double[] times, double[] fluxes, string filter, string timescale)
        {
            this.times = times;
            this.fluxes = fluxes;
            this.errors = new double[times.Length];
            this.filter = filter;
            this.timescale = timescale;
        }
        public double[] Times
        {
            get { return times; }
        }

        public double[] Fluxes
        {
            get { return fluxes; }
        }

        public string TimeScale
        {
            get { return timescale; }
        }

        public string Filter
        {
            get { return filter; }
        }

        public void Save(string path)
        {
            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine(timescale + " " + filter);
            for(int i = 0; i < times.Length; i++)
            {
                sw.WriteLine(times[i].ToString().Replace(",", ".")+" "
                    +fluxes[i].ToString().Replace(",", ".")+" "+errors[i].ToString().Replace(",", "."));
            }

            sw.Close();
        }

    }
}
