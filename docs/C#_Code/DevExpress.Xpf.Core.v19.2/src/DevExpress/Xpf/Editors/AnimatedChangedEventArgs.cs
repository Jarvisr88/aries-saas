namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;

    public class AnimatedChangedEventArgs : EventArgs
    {
        public AnimatedChangedEventArgs(bool value)
        {
            this.IsAnimated = value;
        }

        public bool IsAnimated { get; private set; }
    }
}

