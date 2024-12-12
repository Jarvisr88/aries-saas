namespace DevExpress.Data.Utils
{
    using System;

    public class PowerEase : IEasingFunction
    {
        protected int degree;

        public PowerEase(int newDegree);
        public virtual double Ease(double normalizedTime);
    }
}

