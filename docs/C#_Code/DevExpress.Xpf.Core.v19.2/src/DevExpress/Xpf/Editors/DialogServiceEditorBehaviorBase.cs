namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Threading;

    [TargetType(typeof(ButtonInfo)), TargetType(typeof(BarButtonItem)), TargetType(typeof(ButtonEdit)), TargetType(typeof(ButtonEditSettings)), TargetType(typeof(Button))]
    public abstract class DialogServiceEditorBehaviorBase : EditorBehavior
    {
        public static readonly DependencyProperty DialogServiceTemplateProperty;
        public static readonly DependencyProperty TitleProperty;

        public event AfterDialogServiceDialogClosedEventHandler AfterDialogClosed;

        public event BeforeDialogServiceDialogShownEventHandler BeforeDialogShown;

        static DialogServiceEditorBehaviorBase()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(DialogServiceEditorBehaviorBase), "owner");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            TitleProperty = DependencyPropertyRegistrator.Register<DialogServiceEditorBehaviorBase, string>(System.Linq.Expressions.Expression.Lambda<Func<DialogServiceEditorBehaviorBase, string>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DialogServiceEditorBehaviorBase.get_Title)), parameters), null);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DialogServiceEditorBehaviorBase), "owner");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            DialogServiceTemplateProperty = DependencyPropertyRegistrator.Register<DialogServiceEditorBehaviorBase, DataTemplate>(System.Linq.Expressions.Expression.Lambda<Func<DialogServiceEditorBehaviorBase, DataTemplate>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DialogServiceEditorBehaviorBase.get_DialogServiceTemplate)), expressionArray2), null);
        }

        protected DialogServiceEditorBehaviorBase()
        {
        }

        protected override void ProcessClick(DependencyObject owner)
        {
            base.ProcessClick(owner);
            ButtonEdit edit = owner as ButtonEdit;
            if (edit != null)
            {
                this.ProcessClickForFrameworkElement(edit, edit.EditValue);
            }
            else
            {
                ButtonInfo buttonInfo = owner as ButtonInfo;
                if (buttonInfo != null)
                {
                    this.ProcessClickForButtonInfo(buttonInfo);
                }
                else
                {
                    FrameworkElement element = owner as FrameworkElement;
                    if (element != null)
                    {
                        this.ProcessClickForFrameworkElement(element, null);
                    }
                }
            }
        }

        private void ProcessClickForButtonInfo(ButtonInfo buttonInfo)
        {
            ButtonEdit ownerEdit = BaseEdit.GetOwnerEdit(buttonInfo) as ButtonEdit;
            if (ownerEdit != null)
            {
                this.ProcessClickForFrameworkElement(ownerEdit, ownerEdit.EditValue);
            }
        }

        private void ProcessClickForFrameworkElement(FrameworkElement owner, object editValue)
        {
            base.Dispatcher.BeginInvoke(DispatcherPriority.Normal, () => this.ProcessClickForFrameworkElementInternal(owner, editValue));
        }

        protected abstract void ProcessClickForFrameworkElementInternal(FrameworkElement owner, object editValue);
        protected AfterDialogServiceDialogClosedEventArgs RaiseDialogClosed(UITypeEditorValue value, object dialogResult)
        {
            AfterDialogServiceDialogClosedEventArgs args = new AfterDialogServiceDialogClosedEventArgs(value, dialogResult);
            this.AfterDialogClosed.Do<AfterDialogServiceDialogClosedEventHandler>(delegate (AfterDialogServiceDialogClosedEventHandler x) {
                x(this, args);
            });
            return args;
        }

        protected void RaiseDialogShown(UITypeEditorValue value)
        {
            BeforeDialogServiceDialogShownEventArgs args = new BeforeDialogServiceDialogShownEventArgs(value);
            this.BeforeDialogShown.Do<BeforeDialogServiceDialogShownEventHandler>(x => x(this, args));
        }

        public string Title
        {
            get => 
                (string) base.GetValue(TitleProperty);
            set => 
                base.SetValue(TitleProperty, value);
        }

        public DataTemplate DialogServiceTemplate
        {
            get => 
                (DataTemplate) base.GetValue(DialogServiceTemplateProperty);
            set => 
                base.SetValue(DialogServiceTemplateProperty, value);
        }
    }
}

