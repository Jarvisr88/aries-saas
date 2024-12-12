namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class WindowAcrylicBackgroundEffect
    {
        private const int ActivateDuration = 120;
        private const int DeactivateDuration = 100;
        private System.Windows.Media.Color color;
        private double opacity;
        private Rectangle acrylicBackground;
        private bool isActive;

        public WindowAcrylicBackgroundEffect(ThemedWindow window);
        private void DisableBlurBehind();
        private void EnableBlurBehind();
        protected internal static System.Windows.Media.Color GetAcrylicBackgroundColor(double opacity, System.Windows.Media.Color color);
        private double GetStateOpacity(bool isActive);
        protected virtual void Initialize();
        private void OnAcrylicBackgroundChanged();
        private void OnActivated(object sender, EventArgs e);
        private void OnAnimationProgress(double currentValue, bool complete);
        private void OnColorChanged();
        private void OnDeactivated(object sender, EventArgs e);
        private void OnIsActiveChanged();
        private void OnOpacityChanged();
        private void OnSourceInitialized(object sender, EventArgs e);
        private void UpdateAcrylicBackground();
        private void UpdateAcrylicBackgroundOpacity(double value);

        public System.Windows.Media.Color Color { get; set; }

        public double Opacity { get; set; }

        public Rectangle AcrylicBackground { get; set; }

        protected bool IsActive { get; set; }

        protected System.Windows.Media.Color SavedCompositionTargetBackgroundColor { get; set; }

        protected ThemedWindow Window { get; private set; }

        protected bool IsBlurBehindEnabled { get; set; }

        protected DoubleAnimator OpacityAnimator { get; private set; }
    }
}

