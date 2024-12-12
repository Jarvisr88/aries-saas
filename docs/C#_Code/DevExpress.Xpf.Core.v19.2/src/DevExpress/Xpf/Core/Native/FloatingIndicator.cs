namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public class FloatingIndicator : SelectionIndicator
    {
        public FloatingIndicator();

        public System.Windows.Media.ScaleTransform ScaleTransform { get; private set; }

        public System.Windows.Media.TranslateTransform TranslateTransform { get; private set; }
    }
}

