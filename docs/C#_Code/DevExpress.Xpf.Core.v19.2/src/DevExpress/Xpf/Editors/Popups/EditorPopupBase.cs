namespace DevExpress.Xpf.Editors.Popups
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Internal;
    using DevExpress.Xpf.Editors;
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Input;

    [ToolboxItem(false)]
    public class EditorPopupBase : PopupBase
    {
        private static Action<EditorPopupBase> _RepositionAction;

        static EditorPopupBase()
        {
            BarNameScope.IsScopeOwnerProperty.OverrideMetadata(typeof(EditorPopupBase), new FrameworkPropertyMetadata(true));
        }

        public EditorPopupBase()
        {
            base.AllowsTransparency = true;
        }

        protected override DevExpress.Xpf.Core.PopupBorderControl CreateBorderControl()
        {
            DevExpress.Xpf.Core.PopupBorderControl control1 = new DevExpress.Xpf.Core.PopupBorderControl();
            control1.Focusable = false;
            DevExpress.Xpf.Core.PopupBorderControl control = control1;
            Binding binding = new Binding(DevExpress.Xpf.Editors.PopupViewModel.IsLeftProperty.Name);
            binding.Source = this.PopupViewModel;
            control.SetBinding(DevExpress.Xpf.Core.PopupBorderControl.IsLeftProperty, binding);
            Binding binding2 = new Binding(DevExpress.Xpf.Editors.PopupViewModel.DropOppositeProperty.Name);
            binding2.Source = this.PopupViewModel;
            control.SetBinding(DevExpress.Xpf.Core.PopupBorderControl.DropOppositeProperty, binding2);
            return control;
        }

        private Point GetMousePointerPosition(MouseEventArgs e, IInputElement relativeTo) => 
            e.GetPosition(relativeTo);

        internal DependencyObject GetTemplateChildCore(string childName) => 
            base.GetTemplateChild(childName);

        protected override void OnIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnIsKeyboardFocusWithinChanged(e);
            if (this.OwnerEdit != null)
            {
                this.OwnerEdit.OnPopupIsKeyboardFocusWithinChanged(this);
            }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (base.Child != null)
            {
                Rect rect = new Rect(0.0, 0.0, ((FrameworkElement) base.Child).ActualWidth, ((FrameworkElement) base.Child).ActualHeight);
                if (!rect.Contains(this.GetMousePointerPosition(e, base.Child)))
                {
                    this.OwnerEdit.ClosePopupOnClick();
                }
            }
        }

        internal void RepositionInternal()
        {
            if (_RepositionAction == null)
            {
                int? parametersCount = null;
                _RepositionAction = ReflectionHelper.CreateInstanceMethodHandler<EditorPopupBase, Action<EditorPopupBase>>(null, "Reposition", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance, parametersCount, null, true);
            }
            _RepositionAction(this);
        }

        internal DevExpress.Xpf.Core.PopupBorderControl PopupBorderControl =>
            base.Child as DevExpress.Xpf.Core.PopupBorderControl;

        protected DevExpress.Xpf.Editors.PopupViewModel PopupViewModel =>
            ((PopupBaseEditPropertyProvider) this.OwnerEdit.PropertyProvider).PopupViewModel;

        protected PopupBaseEdit OwnerEdit =>
            BaseEdit.GetOwnerEdit(this) as PopupBaseEdit;
    }
}

