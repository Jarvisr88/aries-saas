namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Media.Animation;

    public class TrapezoidEasingFunction : IEasingFunction
    {
        private IEasingFunction innerEasingFunction;
        private AnimationEasingMode modeCore;

        public TrapezoidEasingFunction()
        {
            this.OnModeChanged();
        }

        private double CalcHidingProgress(double normalizedTime) => 
            (1.0 - normalizedTime) / this.GetNormalizedHideRatio();

        private double CalcShowingProgress(double normalizedTime) => 
            normalizedTime / this.GetNormalizedShowRatio();

        private double CalcSustainProgress() => 
            1.0;

        private IEasingFunction CreateExponentialEase(EasingMode exponentialEasingMode)
        {
            ExponentialEase ease1 = new ExponentialEase();
            ease1.EasingMode = exponentialEasingMode;
            ease1.Exponent = 3.0;
            return ease1;
        }

        private IEasingFunction CreateInnerEasingFunction()
        {
            switch (this.Mode)
            {
                case AnimationEasingMode.Linear:
                    return new LinearEasingFunction();

                case AnimationEasingMode.EaseIn:
                    return this.CreateExponentialEase(EasingMode.EaseIn);

                case AnimationEasingMode.EaseOut:
                    return this.CreateExponentialEase(EasingMode.EaseOut);

                case AnimationEasingMode.EaseInOut:
                    return this.CreateExponentialEase(EasingMode.EaseInOut);
            }
            throw new InvalidOperationException();
        }

        public double Ease(double normalizedTime)
        {
            double num = 0.0;
            num = !this.IsBeforeShow(normalizedTime) ? (!this.IsBeforeHide(normalizedTime) ? this.CalcHidingProgress(normalizedTime) : this.CalcSustainProgress()) : this.CalcShowingProgress(normalizedTime);
            return this.innerEasingFunction.Ease(num);
        }

        private double GetNormalizedHideRatio() => 
            NormalizeRatio(this.HideRatio);

        private double GetNormalizedShowRatio() => 
            NormalizeRatio(this.ShowRatio);

        private bool IsBeforeHide(double normalizedTime) => 
            normalizedTime < (1.0 - this.GetNormalizedHideRatio());

        private bool IsBeforeShow(double normalizedTime) => 
            normalizedTime < this.GetNormalizedShowRatio();

        private static double NormalizeRatio(double ratio) => 
            ((ratio < 0.0) || (ratio > 1.0)) ? 0.0 : ratio;

        private void OnModeChanged()
        {
            this.innerEasingFunction = this.CreateInnerEasingFunction();
        }

        public double HideRatio { get; set; }

        public double ShowRatio { get; set; }

        public AnimationEasingMode Mode
        {
            get => 
                this.modeCore;
            set
            {
                if (this.modeCore != value)
                {
                    this.modeCore = value;
                    this.OnModeChanged();
                }
            }
        }

        private class LinearEasingFunction : IEasingFunction
        {
            public double Ease(double normalizedTime) => 
                normalizedTime;
        }
    }
}

