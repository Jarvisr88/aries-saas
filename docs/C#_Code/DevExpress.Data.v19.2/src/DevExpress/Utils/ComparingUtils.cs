namespace DevExpress.Utils
{
    using System;

    public static class ComparingUtils
    {
        public static int CompareDoubleArrays(double[] numbers1, double[] numbers2, double epsilon)
        {
            if (numbers1.Length != numbers2.Length)
            {
                throw new ArgumentException();
            }
            for (int i = 0; i < numbers1.Length; i++)
            {
                int num2 = CompareDoubles(numbers1[i], numbers2[i], epsilon);
                if (num2 != 0)
                {
                    return num2;
                }
            }
            return 0;
        }

        public static int CompareDoubles(double number1, double number2, double epsilon)
        {
            double num = number1 - number2;
            return ((Math.Abs(num) > epsilon) ? ((num > 0.0) ? 1 : -1) : 0);
        }
    }
}

