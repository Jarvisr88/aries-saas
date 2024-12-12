namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Bars.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Validation.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Markup;

    [ContentProperty("Content")]
    public abstract class EditorBehavior : Behavior<DependencyObject>, IEditorSource
    {
        public static readonly DependencyProperty ValueProperty;
        public static readonly DependencyProperty ContentProperty;

        static EditorBehavior()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(EditorBehavior), "owner");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            ContentProperty = DependencyPropertyRegistrator.Register<EditorBehavior, object>(System.Linq.Expressions.Expression.Lambda<Func<EditorBehavior, object>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(EditorBehavior.get_Content)), parameters), null);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(EditorBehavior), "owner");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            ValueProperty = DependencyPropertyRegistrator.Register<EditorBehavior, object>(System.Linq.Expressions.Expression.Lambda<Func<EditorBehavior, object>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(EditorBehavior.get_Value)), expressionArray2), null);
        }

        protected EditorBehavior()
        {
        }

        public void AcceptEditableValue(UITypeEditorValue value)
        {
            if (value.ShouldPost())
            {
                this.PostEditValue(value.Owner, value.Value);
            }
        }

        private void BarButtonItemItemClick(object sender, ItemClickEventArgs e)
        {
            this.ProcessClick(e.Link.LinkInfos.FirstOrDefault<BarItemLinkInfo>());
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            this.ProcessClick((FrameworkElement) sender);
        }

        private void ButtonEditButtonClick(object sender, RoutedEventArgs e)
        {
            this.ProcessClick((DependencyObject) sender);
        }

        private void ButtonInfoClick(object sender, RoutedEventArgs e)
        {
            ButtonInfo owner = sender as ButtonInfo;
            if (owner == null)
            {
                FrameworkElement element = sender as FrameworkElement;
                if (element != null)
                {
                    owner = element.DataContext as ButtonInfo;
                }
                else
                {
                    FrameworkRenderElementContext context = sender as FrameworkRenderElementContext;
                    if (context != null)
                    {
                        owner = context.DataContext as ButtonInfo;
                    }
                }
            }
            this.ProcessClick(owner);
        }

        private void DelayedSubscribeOnLoaded(object sender, RoutedEventArgs e)
        {
            if (base.IsAttached)
            {
                ButtonInfo element = (ButtonInfo) sender;
                ButtonEdit ownerEdit = BaseEdit.GetOwnerEdit(element) as ButtonEdit;
                element.Loaded -= new RoutedEventHandler(this.DelayedSubscribeOnLoaded);
                ownerEdit.Do<ButtonEdit>(delegate (ButtonEdit x) {
                    x.DefaultButtonClick += new RoutedEventHandler(this.ButtonEditButtonClick);
                });
            }
        }

        UITypeEditorValue IEditorSource.GetEditableValue(DependencyObject owner, object defaultValue) => 
            this.GetEditableValue(owner, defaultValue);

        protected virtual UITypeEditorValue GetEditableValue(DependencyObject owner, object editValue)
        {
            ButtonEdit editor = owner as ButtonEdit;
            if (editor == null)
            {
                return new UITypeEditorValue(owner, this, this.IsPropertySet(ValueProperty) ? this.Value : editValue, this.Content ?? editValue);
            }
            object editValueForButtonEdit = this.GetEditValueForButtonEdit(editor, editValue);
            object content = this.Content;
            object obj4 = content;
            if (content == null)
            {
                object local1 = content;
                obj4 = editValueForButtonEdit;
            }
            return new UITypeEditorValue(owner, this, editValueForButtonEdit, obj4);
        }

        protected virtual object GetEditValueForButtonEdit(ButtonEdit editor, object defaultValue)
        {
            if (editor.EditMode == EditMode.Standalone)
            {
                return defaultValue;
            }
            Func<DependencyObject, bool> predicate = <>c.<>9__21_0;
            if (<>c.<>9__21_0 == null)
            {
                Func<DependencyObject, bool> local1 = <>c.<>9__21_0;
                predicate = <>c.<>9__21_0 = x => x is InplaceEditorBase;
            }
            ISupportExternalPost post = TreeHelper.GetParent(editor, predicate, false, true) as ISupportExternalPost;
            return ((post != null) ? post.GetEditableValueForExternalEditor() : defaultValue);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            this.Subscribe();
        }

        protected override void OnDetaching()
        {
            this.Unsubscribe();
            base.OnDetaching();
        }

        protected virtual void PostEditValue(DependencyObject owner, object editValue)
        {
            ButtonEdit buttonEdit = owner as ButtonEdit;
            if (buttonEdit != null)
            {
                this.PostEditValueToButtonEdit(buttonEdit, editValue);
            }
            else
            {
                base.SetCurrentValue(ValueProperty, editValue);
            }
        }

        protected virtual void PostEditValueToButtonEdit(ButtonEdit buttonEdit, object editValue)
        {
            if (buttonEdit.EditMode != EditMode.Standalone)
            {
                Func<DependencyObject, bool> predicate = <>c.<>9__23_0;
                if (<>c.<>9__23_0 == null)
                {
                    Func<DependencyObject, bool> local1 = <>c.<>9__23_0;
                    predicate = <>c.<>9__23_0 = x => x is InplaceEditorBase;
                }
                ISupportExternalPost post = TreeHelper.GetParent(buttonEdit, predicate, false, true) as ISupportExternalPost;
                if (post != null)
                {
                    post.SetEditableValueFromExternalEditor(editValue);
                    return;
                }
            }
            buttonEdit.PropertyProvider.GetService<ValueContainerService>().SetEditValue(editValue, UpdateEditorSource.ValueChanging);
            buttonEdit.ForceChangeDisplayText();
        }

        protected virtual void ProcessClick(DependencyObject owner)
        {
        }

        protected virtual void Subscribe()
        {
            ButtonInfo associatedObject = base.AssociatedObject as ButtonInfo;
            if (associatedObject != null)
            {
                associatedObject.Click += new RoutedEventHandler(this.ButtonInfoClick);
                if (associatedObject.IsDefaultButton)
                {
                    ButtonEdit ownerEdit = BaseEdit.GetOwnerEdit(associatedObject) as ButtonEdit;
                    if (ownerEdit == null)
                    {
                        associatedObject.Loaded += new RoutedEventHandler(this.DelayedSubscribeOnLoaded);
                    }
                    else
                    {
                        ownerEdit.Do<ButtonEdit>(delegate (ButtonEdit x) {
                            x.DefaultButtonClick += new RoutedEventHandler(this.ButtonEditButtonClick);
                        });
                    }
                }
            }
            else
            {
                ButtonEdit edit = base.AssociatedObject as ButtonEdit;
                if (edit != null)
                {
                    edit.DefaultButtonClick += new RoutedEventHandler(this.ButtonEditButtonClick);
                }
                else
                {
                    FrameworkElement element = base.AssociatedObject as FrameworkElement;
                    if (element != null)
                    {
                        element.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(this.ButtonClick));
                    }
                    else
                    {
                        BarButtonItem item = base.AssociatedObject as BarButtonItem;
                        if (item != null)
                        {
                            item.ItemClick += new ItemClickEventHandler(this.BarButtonItemItemClick);
                        }
                    }
                }
            }
        }

        protected virtual void Unsubscribe()
        {
            ButtonInfo associatedObject = base.AssociatedObject as ButtonInfo;
            if (associatedObject != null)
            {
                associatedObject.Click -= new RoutedEventHandler(this.ButtonInfoClick);
                if (associatedObject.IsDefaultButton)
                {
                    (BaseEdit.GetOwnerEdit(associatedObject) as ButtonEdit).Do<ButtonEdit>(delegate (ButtonEdit x) {
                        x.DefaultButtonClick -= new RoutedEventHandler(this.ButtonEditButtonClick);
                    });
                }
            }
            else
            {
                ButtonEdit edit = base.AssociatedObject as ButtonEdit;
                if (edit != null)
                {
                    edit.DefaultButtonClick -= new RoutedEventHandler(this.ButtonEditButtonClick);
                }
                else
                {
                    FrameworkElement element = base.AssociatedObject as FrameworkElement;
                    if (element != null)
                    {
                        element.RemoveHandler(ButtonBase.ClickEvent, new RoutedEventHandler(this.ButtonClick));
                    }
                    else
                    {
                        BarButtonItem item = base.AssociatedObject as BarButtonItem;
                        if (item != null)
                        {
                            item.ItemClick -= new ItemClickEventHandler(this.BarButtonItemItemClick);
                        }
                    }
                }
            }
        }

        public object Value
        {
            get => 
                base.GetValue(ValueProperty);
            set => 
                base.SetValue(ValueProperty, value);
        }

        public object Content
        {
            get => 
                base.GetValue(ContentProperty);
            set => 
                base.SetValue(ContentProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EditorBehavior.<>c <>9 = new EditorBehavior.<>c();
            public static Func<DependencyObject, bool> <>9__21_0;
            public static Func<DependencyObject, bool> <>9__23_0;

            internal bool <GetEditValueForButtonEdit>b__21_0(DependencyObject x) => 
                x is InplaceEditorBase;

            internal bool <PostEditValueToButtonEdit>b__23_0(DependencyObject x) => 
                x is InplaceEditorBase;
        }
    }
}

