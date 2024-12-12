namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    internal class MDIStateHelper
    {
        public static readonly DependencyProperty MDIStateProperty;

        static MDIStateHelper()
        {
            new DependencyPropertyRegistrator<MDIStateHelper>().RegisterAttached<MDIState>("MDIState", ref MDIStateProperty, MDIState.Normal, new PropertyChangedCallback(MDIStateHelper.OnMDIStateChanged), null);
        }

        private static Rect GetMDIBounds(DependencyObject dObj) => 
            new Rect(DocumentPanel.GetMDILocation(dObj), DocumentPanel.GetMDISize(dObj));

        public static MDIState GetMDIState(DependencyObject target) => 
            (MDIState) target.GetValue(MDIStateProperty);

        public static void InvalidateMDIContainer(DependencyObject dObj)
        {
            DependencyObject child = dObj;
            if (child != null)
            {
                DocumentMDIContainer container = LayoutHelper.FindParentObject<DocumentMDIContainer>(child);
                if (container != null)
                {
                    container.CoerceValue(DocumentMDIContainer.HasMaximizedDocumentsProperty);
                }
            }
        }

        public static void InvalidateMDIPanel(DependencyObject dObj)
        {
            Panel parent = VisualTreeHelper.GetParent(dObj) as Panel;
            if (parent != null)
            {
                parent.InvalidateMeasure();
            }
        }

        private static void OnMDIStateChanged(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
        {
            InvalidateMDIPanel(dObj);
            InvalidateMDIContainer(dObj);
            DocumentPanel panel = dObj as DocumentPanel;
            if (panel != null)
            {
                MDIState newValue = (MDIState) e.NewValue;
                bool flag2 = newValue == MDIState.Maximized;
                if ((newValue == MDIState.Minimized) | flag2)
                {
                    if (((MDIState) e.OldValue) == MDIState.Normal)
                    {
                        panel.SetValue(DocumentPanel.RestoreBoundsProperty, GetMDIBounds(panel));
                        panel.SetCurrentValue(DocumentPanel.MDISizeProperty, DocumentPanel.DefaultMDISize);
                    }
                }
                else
                {
                    Rect restoreBounds = DocumentPanel.GetRestoreBounds(panel);
                    panel.SetValue(DocumentPanel.MDILocationProperty, new Point(restoreBounds.X, restoreBounds.Y));
                    if (MathHelper.IsEmpty(restoreBounds.GetSize()))
                    {
                        Size mDISize = DocumentPanel.GetMDISize(dObj);
                        restoreBounds.Width = mDISize.Width;
                        restoreBounds.Height = mDISize.Height;
                    }
                    panel.SetValue(DocumentPanel.MDISizeProperty, new Size(restoreBounds.Width, restoreBounds.Height));
                    panel.ClearValue(DocumentPanel.RestoreBoundsProperty);
                }
                DocumentGroup parent = panel.Parent as DocumentGroup;
                if (parent != null)
                {
                    parent.IsMaximized = flag2;
                }
                if (panel.MDIState != newValue)
                {
                    panel.MDIState = newValue;
                }
            }
        }

        public static void SetMDIState(DependencyObject target, MDIState value)
        {
            target.SetValue(MDIStateProperty, value);
        }
    }
}

