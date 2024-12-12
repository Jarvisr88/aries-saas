namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Input;

    public class RaisePostponedMouseEventAction : RaisePosponedEventAction<MouseButtonEventArgs>
    {
        public RaisePostponedMouseEventAction(InplaceEditorBase editor, MouseButtonEventArgs e, Func<bool> condition = null) : base(editor, e, condition)
        {
        }

        protected override MouseButtonEventArgs CloneEventArgs(MouseButtonEventArgs posponedEventArgs) => 
            ReraiseEventHelper.CloneMouseButtonEventArgs(posponedEventArgs);

        protected override UIElement GetElement(MouseButtonEventArgs posponedEventArgs) => 
            GetMouseEventReraiseElement(base.editor, posponedEventArgs.GetPosition(base.editor));

        public static UIElement GetMouseEventReraiseElement(UIElement sourceElement, Point position) => 
            LayoutHelper.HitTest(sourceElement, position);

        protected override RoutedEvent BubblingEvent =>
            UIElement.MouseDownEvent;

        protected override RoutedEvent TunnelingEvent =>
            UIElement.PreviewMouseDownEvent;
    }
}

