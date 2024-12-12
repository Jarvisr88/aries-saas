namespace DevExpress.Xpf.Editors.Popups
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class PopupContentControl : ContentControl
    {
        private bool loaded;

        public PopupContentControl()
        {
            base.Loaded += new RoutedEventHandler(this.PopupContentControlLoaded);
            base.Unloaded += new RoutedEventHandler(this.PopupContentControlUnloaded);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.Editor != null)
            {
                this.Editor.FindPopupContent();
                this.loaded = true;
            }
        }

        private void PopupContentControlLoaded(object sender, RoutedEventArgs e)
        {
            FrameworkElementHelper.SetIsLoaded(this, true);
            if (this.loaded)
            {
                this.loaded = false;
            }
            else if (this.Editor != null)
            {
                this.Editor.FindPopupContent();
            }
        }

        private void PopupContentControlUnloaded(object sender, RoutedEventArgs e)
        {
            FrameworkElementHelper.SetIsLoaded(this, false);
        }

        public PopupBaseEdit Editor { get; set; }
    }
}

