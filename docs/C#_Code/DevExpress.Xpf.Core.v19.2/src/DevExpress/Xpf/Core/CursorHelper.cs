namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class CursorHelper : DependencyObject
    {
        public static readonly DependencyProperty CursorProperty = DependencyProperty.RegisterAttached("Cursor", typeof(DevExpress.Xpf.Core.CursorType), typeof(CursorHelper), new PropertyMetadata(DevExpress.Xpf.Core.CursorType.Arrow, new PropertyChangedCallback(CursorHelper.OnCursorPropertyChanged)));

        public static DevExpress.Xpf.Core.CursorType GetCursor(DependencyObject d) => 
            (DevExpress.Xpf.Core.CursorType) d.GetValue(CursorProperty);

        protected static void OnCursorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DevExpress.Xpf.Core.CursorType newValue = (DevExpress.Xpf.Core.CursorType) e.NewValue;
            FrameworkElement element = d as FrameworkElement;
            if (element != null)
            {
                switch (newValue)
                {
                    case DevExpress.Xpf.Core.CursorType.AppStarting:
                        element.Cursor = Cursors.AppStarting;
                        return;

                    case DevExpress.Xpf.Core.CursorType.Arrow:
                        element.Cursor = Cursors.Arrow;
                        return;

                    case DevExpress.Xpf.Core.CursorType.ArrowCD:
                        element.Cursor = Cursors.ArrowCD;
                        return;

                    case DevExpress.Xpf.Core.CursorType.Cross:
                        element.Cursor = Cursors.Cross;
                        return;

                    case DevExpress.Xpf.Core.CursorType.Hand:
                        element.Cursor = Cursors.Hand;
                        return;

                    case DevExpress.Xpf.Core.CursorType.Help:
                        element.Cursor = Cursors.Help;
                        return;

                    case DevExpress.Xpf.Core.CursorType.IBeam:
                        element.Cursor = Cursors.IBeam;
                        return;

                    case DevExpress.Xpf.Core.CursorType.No:
                        element.Cursor = Cursors.No;
                        return;

                    case DevExpress.Xpf.Core.CursorType.None:
                        element.Cursor = Cursors.None;
                        return;

                    case DevExpress.Xpf.Core.CursorType.Pen:
                        element.Cursor = Cursors.Pen;
                        return;

                    case DevExpress.Xpf.Core.CursorType.ScrollAll:
                        element.Cursor = Cursors.ScrollAll;
                        return;

                    case DevExpress.Xpf.Core.CursorType.ScrollE:
                        element.Cursor = Cursors.ScrollE;
                        return;

                    case DevExpress.Xpf.Core.CursorType.ScrollN:
                        element.Cursor = Cursors.ScrollN;
                        return;

                    case DevExpress.Xpf.Core.CursorType.ScrollNE:
                        element.Cursor = Cursors.ScrollNE;
                        return;

                    case DevExpress.Xpf.Core.CursorType.ScrollNS:
                        element.Cursor = Cursors.ScrollNS;
                        return;

                    case DevExpress.Xpf.Core.CursorType.ScrollNW:
                        element.Cursor = Cursors.ScrollNW;
                        return;

                    case DevExpress.Xpf.Core.CursorType.ScrollS:
                        element.Cursor = Cursors.ScrollS;
                        return;

                    case DevExpress.Xpf.Core.CursorType.ScrollSE:
                        element.Cursor = Cursors.ScrollSE;
                        return;

                    case DevExpress.Xpf.Core.CursorType.ScrollSW:
                        element.Cursor = Cursors.ScrollSW;
                        return;

                    case DevExpress.Xpf.Core.CursorType.ScrollW:
                        element.Cursor = Cursors.ScrollW;
                        return;

                    case DevExpress.Xpf.Core.CursorType.ScrollWE:
                        element.Cursor = Cursors.ScrollWE;
                        return;

                    case DevExpress.Xpf.Core.CursorType.SizeAll:
                        element.Cursor = Cursors.SizeAll;
                        return;

                    case DevExpress.Xpf.Core.CursorType.SizeNESW:
                        element.Cursor = Cursors.SizeNESW;
                        return;

                    case DevExpress.Xpf.Core.CursorType.SizeNS:
                        element.Cursor = Cursors.SizeNS;
                        return;

                    case DevExpress.Xpf.Core.CursorType.SizeNWSE:
                        element.Cursor = Cursors.SizeNWSE;
                        return;

                    case DevExpress.Xpf.Core.CursorType.SizeWE:
                        element.Cursor = Cursors.SizeWE;
                        return;

                    case DevExpress.Xpf.Core.CursorType.UpArrow:
                        element.Cursor = Cursors.UpArrow;
                        return;

                    case DevExpress.Xpf.Core.CursorType.Wait:
                        element.Cursor = Cursors.Wait;
                        return;
                }
            }
        }

        public static void SetCursor(DependencyObject d, DevExpress.Xpf.Core.CursorType value)
        {
            d.SetValue(CursorProperty, value);
        }
    }
}

