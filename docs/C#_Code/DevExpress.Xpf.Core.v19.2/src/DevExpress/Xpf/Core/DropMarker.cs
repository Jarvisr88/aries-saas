namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.UI.Native;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;

    public class DropMarker : Control
    {
        public static readonly DependencyProperty PositionProperty;
        public static readonly DependencyProperty OrientationProperty;

        static DropMarker()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(DropMarker), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<DropMarker> registrator1 = DependencyPropertyRegistrator<DropMarker>.New().Register<DropPosition>(System.Linq.Expressions.Expression.Lambda<Func<DropMarker, DropPosition>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DropMarker.get_Position)), parameters), out PositionProperty, DropPosition.Before, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DropMarker), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator1.Register<System.Windows.Controls.Orientation>(System.Linq.Expressions.Expression.Lambda<Func<DropMarker, System.Windows.Controls.Orientation>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DropMarker.get_Orientation)), expressionArray2), out OrientationProperty, System.Windows.Controls.Orientation.Horizontal, frameworkOptions);
            Type forType = typeof(DropMarker);
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(forType));
        }

        public DropPosition Position
        {
            get => 
                (DropPosition) base.GetValue(PositionProperty);
            set => 
                base.SetValue(PositionProperty, value);
        }

        public System.Windows.Controls.Orientation Orientation
        {
            get => 
                (System.Windows.Controls.Orientation) base.GetValue(OrientationProperty);
            set => 
                base.SetValue(OrientationProperty, value);
        }
    }
}

