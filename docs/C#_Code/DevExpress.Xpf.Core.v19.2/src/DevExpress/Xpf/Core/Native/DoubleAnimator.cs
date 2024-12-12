namespace DevExpress.Xpf.Core.Native
{
    using System;

    public class DoubleAnimator : PropertyAnimator<double>
    {
        private Action<double, bool> _onProgress;

        public DoubleAnimator(Action<double, bool> onProgress, int duration);
        protected override double GetCurrentValue();
        protected override void OnProgress(bool completed);
    }
}

