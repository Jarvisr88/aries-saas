namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using System;
    using System.Windows;

    public static class AnimationDurationCalculator
    {
        public static double Div(Duration first, Duration second)
        {
            double num = ToMilliseconds(second);
            return ((num != 0.0) ? (ToMilliseconds(first) / num) : 0.0);
        }

        private static Duration FromMilliseconds(double milliseconds) => 
            new Duration(TimeSpan.FromMilliseconds(milliseconds));

        public static Duration Multiply(Duration duration, double multiplicator) => 
            (multiplicator != 1.0) ? FromMilliseconds(ToMilliseconds(duration) * multiplicator) : duration;

        public static Duration Sum(params Duration[] durations)
        {
            double milliseconds = 0.0;
            foreach (Duration duration in durations)
            {
                milliseconds += ToMilliseconds(duration);
            }
            return FromMilliseconds(milliseconds);
        }

        private static double ToMilliseconds(Duration duration) => 
            duration.HasTimeSpan ? duration.TimeSpan.TotalMilliseconds : 0.0;
    }
}

