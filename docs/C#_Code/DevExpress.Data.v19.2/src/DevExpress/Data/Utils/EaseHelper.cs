namespace DevExpress.Data.Utils
{
    using System;

    public static class EaseHelper
    {
        public static double Ease(EasingMode easingMode, IEasingFunction easingFunction, double normalizedTime);
        public static EasingMode GetEasingMode(int index);
    }
}

