namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public class PageControlNumericItem : BindableBase
    {
        public PageControlNumericItem()
        {
            this.Number = -1;
        }

        public int Number
        {
            get => 
                base.GetProperty<int>(Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(PageControlNumericItem)), (MethodInfo) methodof(PageControlNumericItem.get_Number)), new ParameterExpression[0]));
            set => 
                base.SetProperty<int>(Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(PageControlNumericItem)), (MethodInfo) methodof(PageControlNumericItem.get_Number)), new ParameterExpression[0]), value);
        }

        public bool ShowEllipsis
        {
            get => 
                base.GetProperty<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(PageControlNumericItem)), (MethodInfo) methodof(PageControlNumericItem.get_ShowEllipsis)), new ParameterExpression[0]));
            set => 
                base.SetProperty<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(PageControlNumericItem)), (MethodInfo) methodof(PageControlNumericItem.get_ShowEllipsis)), new ParameterExpression[0]), value);
        }

        public bool IsSelected
        {
            get => 
                base.GetProperty<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(PageControlNumericItem)), (MethodInfo) methodof(PageControlNumericItem.get_IsSelected)), new ParameterExpression[0]));
            set => 
                base.SetProperty<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(PageControlNumericItem)), (MethodInfo) methodof(PageControlNumericItem.get_IsSelected)), new ParameterExpression[0]), value);
        }
    }
}

