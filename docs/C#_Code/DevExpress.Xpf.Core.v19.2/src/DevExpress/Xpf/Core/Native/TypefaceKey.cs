namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    internal class TypefaceKey
    {
        public TypefaceKey(RenderTextBlockMode renderMode, System.Windows.Media.FontFamily family, System.Windows.FontStyle style, System.Windows.FontWeight weight, System.Windows.FontStretch stretch);
        protected bool Equals(TypefaceKey other);
        public override bool Equals(object obj);
        public override int GetHashCode();

        public System.Windows.Media.FontFamily FontFamily { get; private set; }

        public System.Windows.FontStyle FontStyle { get; private set; }

        public System.Windows.FontWeight FontWeight { get; private set; }

        public System.Windows.FontStretch FontStretch { get; private set; }

        public RenderTextBlockMode RenderMode { get; private set; }
    }
}

