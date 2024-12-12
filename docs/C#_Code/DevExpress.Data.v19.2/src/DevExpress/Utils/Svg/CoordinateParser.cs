namespace DevExpress.Utils.Svg
{
    using DevExpress.Data.Svg;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;

    public static class CoordinateParser
    {
        private static readonly char[] splitChars = new char[] { ' ', '\r', '\n', '\t', ',' };

        public static double[] GetNumbers(string points)
        {
            List<double> list = new List<double>(4);
            foreach (string str in Normalize(points).Split(splitChars, StringSplitOptions.RemoveEmptyEntries))
            {
                if (str.Length != 0)
                {
                    if (char.IsLetter(str[0]))
                    {
                        str = str.Substring(1);
                    }
                    if (str.Length != 0)
                    {
                        char[] separator = new char[] { '.' };
                        string[] strArray2 = str.Split(separator);
                        if (strArray2.Length <= 2)
                        {
                            list.Add(TransformFloat(str));
                        }
                        else
                        {
                            list.Add(TransformFloat(strArray2[0] + "." + strArray2[1]));
                            for (int i = 2; i < strArray2.Length; i++)
                            {
                                list.Add(TransformFloat("0." + strArray2[i]));
                            }
                        }
                    }
                }
            }
            return list.ToArray();
        }

        public static SvgPoint[] GetPoints(string points)
        {
            double[] numbers = GetNumbers(points);
            SvgPoint[] pointArray = new SvgPoint[numbers.Length / 2];
            for (int i = 0; i < pointArray.Length; i++)
            {
                pointArray[i] = new SvgPoint(numbers[i * 2], numbers[(i * 2) + 1]);
            }
            return pointArray;
        }

        private static string Normalize(string points)
        {
            StringBuilder builder = new StringBuilder(points);
            builder.Replace("-", " -");
            builder.Replace("e -", "e-");
            builder.Replace("E -", "E-");
            return builder.ToString();
        }

        public static double TransformFloat(string value) => 
            Math.Round(double.Parse(value, CultureInfo.InvariantCulture), 4);
    }
}

