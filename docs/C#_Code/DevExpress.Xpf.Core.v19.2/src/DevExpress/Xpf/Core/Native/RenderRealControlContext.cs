namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class RenderRealControlContext : RenderControlBaseContext
    {
        public RenderRealControlContext(RenderRealControl factory);
        protected virtual void AttachDataContext(FrameworkElement root);
        protected internal override void AttachToVisualTree(FrameworkElement root);

        public ControlTemplate Template { get; set; }

        private System.Windows.Controls.Control Control { get; }
    }
}

