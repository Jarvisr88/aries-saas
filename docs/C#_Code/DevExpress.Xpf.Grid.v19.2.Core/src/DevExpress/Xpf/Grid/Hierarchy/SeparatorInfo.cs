namespace DevExpress.Xpf.Grid.Hierarchy
{
    using DevExpress.Mvvm;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;

    public class SeparatorInfo : BindableBase
    {
        public Thickness Margin
        {
            get => 
                base.GetProperty<Thickness>(System.Linq.Expressions.Expression.Lambda<Func<Thickness>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(SeparatorInfo)), (MethodInfo) methodof(SeparatorInfo.get_Margin)), new ParameterExpression[0]));
            set => 
                base.SetProperty<Thickness>(System.Linq.Expressions.Expression.Lambda<Func<Thickness>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(SeparatorInfo)), (MethodInfo) methodof(SeparatorInfo.get_Margin)), new ParameterExpression[0]), value);
        }

        public System.Windows.Controls.Orientation Orientation
        {
            get => 
                base.GetProperty<System.Windows.Controls.Orientation>(System.Linq.Expressions.Expression.Lambda<Func<System.Windows.Controls.Orientation>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(SeparatorInfo)), (MethodInfo) methodof(SeparatorInfo.get_Orientation)), new ParameterExpression[0]));
            set => 
                base.SetProperty<System.Windows.Controls.Orientation>(System.Linq.Expressions.Expression.Lambda<Func<System.Windows.Controls.Orientation>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(SeparatorInfo)), (MethodInfo) methodof(SeparatorInfo.get_Orientation)), new ParameterExpression[0]), value);
        }

        public double Length
        {
            get => 
                base.GetProperty<double>(System.Linq.Expressions.Expression.Lambda<Func<double>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(SeparatorInfo)), (MethodInfo) methodof(SeparatorInfo.get_Length)), new ParameterExpression[0]));
            set => 
                base.SetProperty<double>(System.Linq.Expressions.Expression.Lambda<Func<double>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(SeparatorInfo)), (MethodInfo) methodof(SeparatorInfo.get_Length)), new ParameterExpression[0]), value);
        }

        public bool IsVisible
        {
            get => 
                base.GetProperty<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(SeparatorInfo)), (MethodInfo) methodof(SeparatorInfo.get_IsVisible)), new ParameterExpression[0]));
            set => 
                base.SetProperty<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(SeparatorInfo)), (MethodInfo) methodof(SeparatorInfo.get_IsVisible)), new ParameterExpression[0]), value);
        }

        public int RowIndex
        {
            get => 
                base.GetProperty<int>(System.Linq.Expressions.Expression.Lambda<Func<int>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(SeparatorInfo)), (MethodInfo) methodof(SeparatorInfo.get_RowIndex)), new ParameterExpression[0]));
            set => 
                base.SetProperty<int>(System.Linq.Expressions.Expression.Lambda<Func<int>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(SeparatorInfo)), (MethodInfo) methodof(SeparatorInfo.get_RowIndex)), new ParameterExpression[0]), value);
        }
    }
}

