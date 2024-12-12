namespace DevExpress.Utils
{
    using System;

    public static class MultiplierChooser
    {
        private static readonly double[] multipliers = new double[] { 1.0, 2.0, 3.0, 5.0 };

        public static double ChooseMultiplier(double delta)
        {
            if (delta <= 1.0)
            {
                double num4 = 10.0;
                double num6 = Math.Pow(10.0, -Math.Floor(Math.Log10(Math.Abs(delta))));
                double num7 = delta * num6;
                int index = multipliers.Length - 1;
                while (true)
                {
                    if (index >= 0)
                    {
                        double num9 = multipliers[index];
                        if (num7 <= num9)
                        {
                            num4 = num9;
                            index--;
                            continue;
                        }
                    }
                    return (num4 / num6);
                }
            }
            double num = 1.0;
            while (true)
            {
                int index = 0;
                while (true)
                {
                    if (index >= multipliers.Length)
                    {
                        num *= 10.0;
                        break;
                    }
                    double num3 = multipliers[index] * num;
                    if (delta <= num3)
                    {
                        return num3;
                    }
                    index++;
                }
            }
        }
    }
}

