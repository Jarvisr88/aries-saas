namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows.Media;

    public class ColorAnimator : PropertyAnimator<Color>
    {
        private Action<Color, bool> _onProgress;

        public ColorAnimator(Action<Color, bool> onProgress, int duration);
        protected override Color GetCurrentValue();
        private byte GetProgressValue(byte from, byte to);
        protected override void OnProgress(bool completed);
    }
}

