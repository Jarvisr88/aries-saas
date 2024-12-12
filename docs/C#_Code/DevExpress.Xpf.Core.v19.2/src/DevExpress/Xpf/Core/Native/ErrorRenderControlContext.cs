namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class ErrorRenderControlContext : RenderControlBaseContext
    {
        public ErrorRenderControlContext(ErrorRenderControl factory);
        protected internal override void AttachToVisualTree(FrameworkElement root);

        private ContentPresenter ErrorPresenter { get; }
    }
}

