namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Input;

    public class UICommandContainer : DXFrameworkElement
    {
        public static readonly DependencyProperty IdProperty;
        public static readonly DependencyProperty CaptionProperty;
        public static readonly DependencyProperty CommandProperty;
        public static readonly DependencyProperty IsDefaultProperty;
        public static readonly DependencyProperty IsCancelProperty;

        static UICommandContainer()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(DevExpress.Xpf.Editors.UICommandContainer), "owner");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            IdProperty = DependencyPropertyRegistrator.Register<DevExpress.Xpf.Editors.UICommandContainer, object>(System.Linq.Expressions.Expression.Lambda<Func<DevExpress.Xpf.Editors.UICommandContainer, object>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DevExpress.Xpf.Editors.UICommandContainer.get_Id)), parameters), null);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DevExpress.Xpf.Editors.UICommandContainer), "owner");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            CaptionProperty = DependencyPropertyRegistrator.Register<DevExpress.Xpf.Editors.UICommandContainer, object>(System.Linq.Expressions.Expression.Lambda<Func<DevExpress.Xpf.Editors.UICommandContainer, object>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DevExpress.Xpf.Editors.UICommandContainer.get_Caption)), expressionArray2), null);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DevExpress.Xpf.Editors.UICommandContainer), "owner");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            CommandProperty = DependencyPropertyRegistrator.Register<DevExpress.Xpf.Editors.UICommandContainer, ICommand>(System.Linq.Expressions.Expression.Lambda<Func<DevExpress.Xpf.Editors.UICommandContainer, ICommand>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DevExpress.Xpf.Editors.UICommandContainer.get_Command)), expressionArray3), null);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DevExpress.Xpf.Editors.UICommandContainer), "owner");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            IsDefaultProperty = DependencyPropertyRegistrator.Register<DevExpress.Xpf.Editors.UICommandContainer, bool>(System.Linq.Expressions.Expression.Lambda<Func<DevExpress.Xpf.Editors.UICommandContainer, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DevExpress.Xpf.Editors.UICommandContainer.get_IsDefault)), expressionArray4), false);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DevExpress.Xpf.Editors.UICommandContainer), "owner");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            IsCancelProperty = DependencyPropertyRegistrator.Register<DevExpress.Xpf.Editors.UICommandContainer, bool>(System.Linq.Expressions.Expression.Lambda<Func<DevExpress.Xpf.Editors.UICommandContainer, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DevExpress.Xpf.Editors.UICommandContainer.get_IsCancel)), expressionArray5), false);
        }

        public UICommandContainer()
        {
        }

        public UICommandContainer(UICommand command)
        {
            this.Id = command.Id;
            this.Caption = command.Caption;
            this.Command = command.Command;
            this.IsDefault = command.IsDefault;
            this.IsCancel = command.IsCancel;
            base.Tag = command.Tag;
        }

        public object Id
        {
            get => 
                base.GetValue(IdProperty);
            set => 
                base.SetValue(IdProperty, value);
        }

        public object Caption
        {
            get => 
                base.GetValue(CaptionProperty);
            set => 
                base.SetValue(CaptionProperty, value);
        }

        public ICommand Command
        {
            get => 
                (ICommand) base.GetValue(CommandProperty);
            set => 
                base.SetValue(CommandProperty, value);
        }

        public bool IsDefault
        {
            get => 
                (bool) base.GetValue(IsDefaultProperty);
            set => 
                base.SetValue(IsDefaultProperty, value);
        }

        public bool IsCancel
        {
            get => 
                (bool) base.GetValue(IsCancelProperty);
            set => 
                base.SetValue(IsCancelProperty, value);
        }
    }
}

