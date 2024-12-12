namespace Devart.Common
{
    using System;
    using System.Runtime.InteropServices;

    internal static class u
    {
        private static bool a = true;
        private static double b;
        private static double c = 0.0;

        static u()
        {
            b();
        }

        public static long a()
        {
            if (!a)
            {
                return (long) ((ulong) Environment.TickCount);
            }
            long num = 0L;
            a = b(ref num) != 0;
            return num;
        }

        public static double a(long A_0)
        {
            if (!a)
            {
                uint num3 = (uint) A_0;
                uint tickCount = (uint) Environment.TickCount;
                return (((tickCount > num3) ? ((double) (tickCount - num3)) : ((double) (num3 - tickCount))) * 0.001);
            }
            long num = 0L;
            b(ref num);
            double num2 = ((num - A_0) * b) + c;
            if (num2 < 0.0)
            {
                num2 = 0.0;
            }
            return num2;
        }

        private static short a(ref long A_0)
        {
            short num;
            try
            {
                num = QueryPerformanceFrequency(ref A_0);
            }
            catch (EntryPointNotFoundException)
            {
                A_0 = 0L;
                return 0;
            }
            return num;
        }

        private static void b()
        {
            long num = 0L;
            a = a(ref num) != 0;
            if (a)
            {
                b = 1.0 / ((double) num);
                long num2 = a();
                int num3 = 0;
                while (true)
                {
                    if (num3 >= 0x3e7)
                    {
                        c = -a(num2) * 0.001;
                        break;
                    }
                    a(a());
                    num3++;
                }
            }
        }

        private static short b(ref long A_0)
        {
            short num;
            try
            {
                num = QueryPerformanceCounter(ref A_0);
            }
            catch (EntryPointNotFoundException)
            {
                A_0 = 0L;
                return 0;
            }
            return num;
        }

        [DllImport("kernel32.dll")]
        private static extern short QueryPerformanceCounter(ref long A_0);
        [DllImport("kernel32.dll")]
        private static extern short QueryPerformanceFrequency(ref long A_0);
    }
}

