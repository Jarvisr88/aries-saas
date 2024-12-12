namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;

    public class DragElementPopupController : IDragElementController
    {
        private const int CursorHorizontalOffset = 6;
        private const int CursorVerticalOffset = 1;
        private readonly Func<DataTemplate> getTemplate;
        private readonly FrameworkElement associatedObject;
        private IMouseHelper mouseHelperCore = new DevExpress.Xpf.Core.DragDrop.Native.MouseHelper();

        public DragElementPopupController(Func<DataTemplate> getTemplate, FrameworkElement associatedObject)
        {
            Guard.ArgumentNotNull(getTemplate, "getTemplate");
            Guard.ArgumentNotNull(associatedObject, "associatedObject");
            this.getTemplate = getTemplate;
            this.associatedObject = associatedObject;
        }

        private static System.Windows.Controls.Primitives.Popup CreatePopup(ContentControl popupContent)
        {
            System.Windows.Controls.Primitives.Popup popup1 = new System.Windows.Controls.Primitives.Popup();
            popup1.Child = popupContent;
            popup1.Placement = PlacementMode.Absolute;
            popup1.AllowsTransparency = true;
            return popup1;
        }

        private static ContentControl CreatePopupContent(DragDropHintData data, DataTemplate contentTemplate)
        {
            ContentControl control1 = new ContentControl();
            control1.Content = data;
            control1.ContentTemplate = contentTemplate;
            return control1;
        }

        public void Hide()
        {
            if (this.Popup != null)
            {
                this.Popup.IsOpen = false;
                this.LogicalOwner.Do<ILogicalOwner>(x => x.RemoveChild(this.Popup));
                this.Popup = null;
                this.Data = null;
            }
        }

        public void Show(object[] source)
        {
            if (!this.IsPopupOpen)
            {
                DragDropHintData data1 = new DragDropHintData();
                data1.Records = source;
                data1.Owner = this.associatedObject;
                this.Data = data1;
                if (this.GetShowTargetInfoInDragDropHint != null)
                {
                    this.Data.AllowShowTargetInfoInDragDropHint = this.GetShowTargetInfoInDragDropHint();
                }
                ContentControl popupContent = CreatePopupContent(this.Data, this.getTemplate());
                this.Popup = CreatePopup(popupContent);
                this.LogicalOwner.Do<ILogicalOwner>(x => x.AddChild(this.Popup));
                ViewServiceBase.UpdateThemeName(this.Popup, this.associatedObject);
                this.Popup.IsOpen = true;
            }
        }

        public void UpdatePosition()
        {
            if (this.IsPopupOpen)
            {
                Point scaledPoint = ScreenHelper.GetScaledPoint(this.MouseHelper.GetAbsoluteMousePosition());
                Size mouseCursorSize = this.MouseHelper.GetMouseCursorSize();
                this.Popup.HorizontalOffset = (scaledPoint.X + mouseCursorSize.Width) + 6.0;
                this.Popup.VerticalOffset = (scaledPoint.Y + mouseCursorSize.Height) + 1.0;
            }
        }

        public System.Windows.Controls.Primitives.Popup Popup { get; private set; }

        public DragDropHintData Data { get; private set; }

        private bool IsPopupOpen
        {
            get
            {
                Func<System.Windows.Controls.Primitives.Popup, bool> evaluator = <>c.<>9__13_0;
                if (<>c.<>9__13_0 == null)
                {
                    Func<System.Windows.Controls.Primitives.Popup, bool> local1 = <>c.<>9__13_0;
                    evaluator = <>c.<>9__13_0 = x => x.IsOpen;
                }
                return this.Popup.Return<System.Windows.Controls.Primitives.Popup, bool>(evaluator, (<>c.<>9__13_1 ??= () => false));
            }
        }

        private ILogicalOwner LogicalOwner =>
            this.associatedObject as ILogicalOwner;

        public Func<bool> GetShowTargetInfoInDragDropHint { get; set; }

        public IMouseHelper MouseHelper
        {
            get => 
                this.mouseHelperCore;
            set => 
                this.mouseHelperCore = value;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DragElementPopupController.<>c <>9 = new DragElementPopupController.<>c();
            public static Func<Popup, bool> <>9__13_0;
            public static Func<bool> <>9__13_1;

            internal bool <get_IsPopupOpen>b__13_0(Popup x) => 
                x.IsOpen;

            internal bool <get_IsPopupOpen>b__13_1() => 
                false;
        }
    }
}

