namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;

    [TargetType(typeof(ButtonInfo)), TargetType(typeof(PopupContainer))]
    public class PopupEditorBehavior : EditorBehavior, IPopupSource, IEditorSource
    {
        public static readonly DependencyProperty ContentTemplateProperty;
        public static readonly DependencyProperty ContentTemplateSelectorProperty;

        public event EventHandler<EventArgs> PopupClosed;

        public event PopupClosedEventHandler PopupClosing;

        public event PopupOpenedEventHandler PopupOpened;

        static PopupEditorBehavior()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(PopupEditorBehavior), "owner");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            ContentTemplateProperty = DependencyPropertyRegistrator.Register<PopupEditorBehavior, DataTemplate>(System.Linq.Expressions.Expression.Lambda<Func<PopupEditorBehavior, DataTemplate>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PopupEditorBehavior.get_ContentTemplate)), parameters), null);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(PopupEditorBehavior), "owner");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            ContentTemplateSelectorProperty = DependencyPropertyRegistrator.Register<PopupEditorBehavior, DataTemplateSelector>(System.Linq.Expressions.Expression.Lambda<Func<PopupEditorBehavior, DataTemplateSelector>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(PopupEditorBehavior.get_ContentTemplateSelector)), expressionArray2), null);
        }

        private void EditorOnPopupClosed(object sender, ClosePopupEventArgs e)
        {
            this.RaisePopupClosed();
        }

        private void EditorOnPopupClosing(object sender, ClosePopupEventArgs e)
        {
            PopupBaseEdit ownerEdit = BaseEdit.GetOwnerEdit(base.AssociatedObject) as PopupBaseEdit;
            if (ownerEdit != null)
            {
                UITypeEditorValue popupValue = ownerEdit.PopupSettings.PopupValue;
                this.RaisePopupClosing(popupValue);
            }
        }

        private void EditorOnPopupOpened(object sender, RoutedEventArgs e)
        {
            PopupBaseEdit ownerEdit = BaseEdit.GetOwnerEdit(base.AssociatedObject) as PopupBaseEdit;
            if (ownerEdit != null)
            {
                UITypeEditorValue popupValue = ownerEdit.PopupSettings.PopupValue;
                this.RaisePopupOpened(popupValue);
            }
        }

        protected override void ProcessClick(DependencyObject owner)
        {
            PopupBaseEdit popupBaseEdit = owner as PopupBaseEdit;
            if (popupBaseEdit != null)
            {
                this.ProcessClickForPopupBaseEdit(popupBaseEdit);
            }
            else
            {
                ButtonInfo buttonInfo = owner as ButtonInfo;
                if (buttonInfo != null)
                {
                    this.ProcessClickForButtonInfo(buttonInfo);
                }
                FrameworkElement fre = owner as FrameworkElement;
                if (fre != null)
                {
                    this.ProcessClickForFrameworkElement(fre);
                }
            }
        }

        private void ProcessClickForButtonInfo(ButtonInfo buttonInfo)
        {
            PopupBaseEdit ownerEdit = BaseEdit.GetOwnerEdit(buttonInfo) as PopupBaseEdit;
            if (ownerEdit != null)
            {
                ownerEdit.PropertyProvider.GetService<PopupService>().SetPopupSource(buttonInfo.IsDefaultButton ? null : this);
            }
        }

        private void ProcessClickForFrameworkElement(FrameworkElement fre)
        {
        }

        private void ProcessClickForPopupBaseEdit(PopupBaseEdit popupBaseEdit)
        {
            popupBaseEdit.PropertyProvider.GetService<PopupService>().SetPopupSource(null);
        }

        private void RaisePopupClosed()
        {
            this.PopupClosed.Do<EventHandler<EventArgs>>(x => x(this, EventArgs.Empty));
        }

        private void RaisePopupClosing(UITypeEditorValue value)
        {
            this.PopupClosing.Do<PopupClosedEventHandler>(x => x(this, new PopupClosedEventEventArgs(value)));
        }

        private void RaisePopupOpened(UITypeEditorValue value)
        {
            this.PopupOpened.Do<PopupOpenedEventHandler>(x => x(this, new PopupOpenedEventEventArgs(value)));
        }

        protected override void Subscribe()
        {
            base.Subscribe();
            PopupBaseEdit ownerEdit = BaseEdit.GetOwnerEdit(base.AssociatedObject) as PopupBaseEdit;
            if (ownerEdit != null)
            {
                this.SubscribeForPopupContainerEdit(ownerEdit);
            }
        }

        private void SubscribeForPopupContainerEdit(PopupBaseEdit editor)
        {
            editor.PopupOpened += new RoutedEventHandler(this.EditorOnPopupOpened);
            editor.PopupClosing += new ClosingPopupEventHandler(this.EditorOnPopupClosing);
            editor.PopupClosed += new ClosePopupEventHandler(this.EditorOnPopupClosed);
        }

        protected override void Unsubscribe()
        {
            base.Unsubscribe();
            PopupBaseEdit ownerEdit = BaseEdit.GetOwnerEdit(base.AssociatedObject) as PopupBaseEdit;
            if (ownerEdit != null)
            {
                this.UnsubscribeForPopupContainerEdit(ownerEdit);
            }
        }

        private void UnsubscribeForPopupContainerEdit(PopupBaseEdit editor)
        {
            editor.PopupOpened -= new RoutedEventHandler(this.EditorOnPopupOpened);
            editor.PopupClosing -= new ClosingPopupEventHandler(this.EditorOnPopupClosing);
            editor.PopupClosed -= new ClosePopupEventHandler(this.EditorOnPopupClosed);
        }

        public DataTemplate ContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ContentTemplateProperty);
            set => 
                base.SetValue(ContentTemplateProperty, value);
        }

        public DataTemplateSelector ContentTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ContentTemplateSelectorProperty);
            set => 
                base.SetValue(ContentTemplateSelectorProperty, value);
        }
    }
}

