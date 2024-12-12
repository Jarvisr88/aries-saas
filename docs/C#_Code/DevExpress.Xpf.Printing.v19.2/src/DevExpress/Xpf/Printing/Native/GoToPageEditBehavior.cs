namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Printing;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;

    public class GoToPageEditBehavior
    {
        public static readonly DependencyProperty ApplyProperty = DependencyPropertyManager.RegisterAttached("Apply", typeof(bool), typeof(GoToPageEditBehavior), new PropertyMetadata(false, new PropertyChangedCallback(GoToPageEditBehavior.OnApplyChanged)));

        public static bool GetApply(DependencyObject obj) => 
            (bool) obj.GetValue(ApplyProperty);

        private static UIElement GetReturnFocusTarget(BarItem item) => 
            DocumentViewer.GetDocumentViewer(BarManager.GetBarManager(item));

        private static void link_LinkControlLoaded(object sender, BarItemLinkControlLoadedEventArgs e)
        {
            BarEditItem item = (BarEditItem) e.Item;
            IBarEditItemLinkControl linkControl = (IBarEditItemLinkControl) e.LinkControl;
            if ((item != null) && (linkControl != null))
            {
                BaseEdit edit = linkControl.Edit;
                if (edit != null)
                {
                    MouseEventHandler handler1 = <>c.<>9__4_0;
                    if (<>c.<>9__4_0 == null)
                    {
                        MouseEventHandler local1 = <>c.<>9__4_0;
                        handler1 = <>c.<>9__4_0 = (o, args) => ((BaseEdit) o).SelectAll();
                    }
                    edit.GotMouseCapture += handler1;
                    edit.LostKeyboardFocus += (o, args) => RevertEditValue(item);
                    edit.KeyUp += (o, args) => OnKeyUp(args.Key, item);
                }
            }
        }

        private static void OnApplyChanged(object d, DependencyPropertyChangedEventArgs e)
        {
            BarEditItemLink link = d as BarEditItemLink;
            if (link == null)
            {
                throw new NotSupportedException("GoToPageBehavior can be attached to a BarEditItemLink class instance only.");
            }
            if ((bool) e.NewValue)
            {
                link.UseLightweightTemplates = false;
                link.LinkControlLoaded += new BarItemLinkControlLoadedEventHandler(GoToPageEditBehavior.link_LinkControlLoaded);
            }
        }

        private static void OnKeyUp(Key key, BarEditItem item)
        {
            if (key == Key.Return)
            {
                item.GetBindingExpression(BarEditItem.EditValueProperty).UpdateSource();
                UIElement returnFocusTarget = GetReturnFocusTarget(item);
                if (returnFocusTarget != null)
                {
                    returnFocusTarget.Focus();
                }
            }
        }

        private static void RevertEditValue(BarEditItem item)
        {
            item.GetBindingExpression(BarEditItem.EditValueProperty).UpdateTarget();
        }

        public static void SetApply(DependencyObject obj, bool value)
        {
            obj.SetValue(ApplyProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GoToPageEditBehavior.<>c <>9 = new GoToPageEditBehavior.<>c();
            public static MouseEventHandler <>9__4_0;

            internal void <link_LinkControlLoaded>b__4_0(object o, MouseEventArgs args)
            {
                ((BaseEdit) o).SelectAll();
            }
        }
    }
}

