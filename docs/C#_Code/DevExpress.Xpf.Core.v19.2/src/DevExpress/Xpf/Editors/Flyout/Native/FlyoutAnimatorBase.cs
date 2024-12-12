namespace DevExpress.Xpf.Editors.Flyout.Native
{
    using System;
    using System.Windows;
    using System.Windows.Media.Animation;

    public class FlyoutAnimatorBase
    {
        public virtual Storyboard GetCloseAnimation(FlyoutBase flyout) => 
            null;

        public virtual Storyboard GetMoveAnimation(FlyoutBase flyout, Point from, Point to) => 
            null;

        public virtual Storyboard GetOpenAnimation(FlyoutBase flyout, Point offset) => 
            null;
    }
}

