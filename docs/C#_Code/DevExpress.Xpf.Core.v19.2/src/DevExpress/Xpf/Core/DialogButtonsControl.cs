namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    public class DialogButtonsControl : ItemsControl
    {
        public static readonly DependencyProperty CommandsSourceProperty;
        public static readonly DependencyProperty CommandButtonStyleProperty;
        private readonly List<UIElement> logicalChildren = new List<UIElement>();

        static DialogButtonsControl()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(DependencyObject), "d");
            System.Linq.Expressions.Expression[] arguments = new System.Linq.Expressions.Expression[] { expression };
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DependencyObject), "d");
            System.Linq.Expressions.Expression[] expressionArray3 = new System.Linq.Expressions.Expression[] { expression };
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            DependencyPropertyRegistrator<DialogButtonsControl>.New().RegisterAttached<DependencyObject, IEnumerable>(System.Linq.Expressions.Expression.Lambda<Func<DependencyObject, IEnumerable>>(System.Linq.Expressions.Expression.Call(null, (MethodInfo) methodof(DialogButtonsControl.GetCommandsSource), arguments), parameters), out CommandsSourceProperty, null, 0x20).RegisterAttached<DependencyObject, Style>(System.Linq.Expressions.Expression.Lambda<Func<DependencyObject, Style>>(System.Linq.Expressions.Expression.Call(null, (MethodInfo) methodof(DialogButtonsControl.GetCommandButtonStyle), expressionArray3), expressionArray4), out CommandButtonStyleProperty, null, 0x20).OverrideDefaultStyleKey();
        }

        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            DialogButton target = element as DialogButton;
            if (target != null)
            {
                UICommand uICommand = UICommandContainer.GetUICommand(item);
                if (uICommand != null)
                {
                    target.DialogUICommandTag = uICommand;
                }
                BindingOperations.ClearBinding(target, DialogButton.CommandButtonStyleProperty);
            }
            base.ClearContainerForItemOverride(element, item);
        }

        public static Style GetCommandButtonStyle(DependencyObject d) => 
            (Style) d.GetValue(CommandButtonStyleProperty);

        public static IEnumerable GetCommandsSource(DependencyObject d) => 
            (IEnumerable) d.GetValue(CommandsSourceProperty);

        protected override DependencyObject GetContainerForItemOverride() => 
            new DialogButton();

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);
            foreach (UIElement element in this.logicalChildren)
            {
                base.RemoveLogicalChild(element);
            }
            this.logicalChildren.Clear();
            this.logicalChildren.AddRange(base.Items.OfType<UIElement>());
            foreach (UIElement element2 in this.logicalChildren)
            {
                base.AddLogicalChild(element2);
            }
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);
            DialogButton button = element as DialogButton;
            if (button != null)
            {
                Binding binding = new Binding();
                binding.Path = new PropertyPath(CommandButtonStyleProperty);
                binding.Source = this;
                binding.Mode = BindingMode.OneWay;
                button.SetBinding(DialogButton.CommandButtonStyleProperty, binding);
                UICommand uICommand = UICommandContainer.GetUICommand(item);
                if (uICommand != null)
                {
                    button.DialogUICommandTag = uICommand;
                    button.Content = DialogButton.NotSetContent;
                }
            }
        }

        public static void SetCommandButtonStyle(DependencyObject d, Style style)
        {
            d.SetValue(CommandButtonStyleProperty, style);
        }

        public static void SetCommandsSource(DependencyObject d, IEnumerable commandsSource)
        {
            d.SetValue(CommandsSourceProperty, commandsSource);
        }

        protected override IEnumerator LogicalChildren
        {
            get
            {
                IEnumerator[] args = new IEnumerator[] { base.LogicalChildren, this.logicalChildren.GetEnumerator() };
                return new MergedEnumerator(args);
            }
        }
    }
}

