namespace DevExpress.Xpf.Editors.Internal
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public class ColorViewModelValueChangedEventArgs : EventArgs
    {
        public ColorViewModelValueChangedEventArgs(System.Windows.Media.Color color)
        {
            this.Color = color;
        }

        public System.Windows.Media.Color Color { get; private set; }
    }
}

