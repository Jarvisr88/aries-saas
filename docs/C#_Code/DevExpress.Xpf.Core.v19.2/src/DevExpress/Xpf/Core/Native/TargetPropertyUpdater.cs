namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class TargetPropertyUpdater
    {
        private FrameworkElement templatedParent;
        private FrameworkElement owner;

        public event EventHandler AnimationUpdated;

        public TargetPropertyUpdater(FrameworkElement owner);
        private string GetCorrectPath(PropertyPath propertyPath);
        protected abstract DependencyProperty GetDependencyPropertyByPath(string path);
        private IList GetVisualStateGroups();
        private void Owner_LayoutUpdated(object sender, EventArgs e);
        private void RaisAnimationUpdated();
        private void UpdateAnimation();
        private void UpdateOwner();

        public bool IsAnimationUpdated { get; private set; }

        public FrameworkElement Owner { get; private set; }

        internal FrameworkElement Target { get; }
    }
}

