namespace DevExpress.Xpf.Editors.RangeControl.Internal
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class AnimationEventArgs : EventArgs
    {
        public AnimationEventArgs(AnimationTypes type)
        {
            this.AnimationType = type;
        }

        public AnimationTypes AnimationType { get; set; }

        public FrameworkElement Target { get; set; }
    }
}

