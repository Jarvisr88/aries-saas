namespace DevExpress.Xpf.Docking
{
    using DevExpress.Mvvm;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public class LayoutItemData : BindableBase
    {
        public LayoutItemData(BaseLayoutItem item)
        {
            this.LayoutItem = item;
        }

        public static BaseLayoutItem ConvertToBaseLayoutItem(object content) => 
            (content is LayoutItemData) ? ((LayoutItemData) content).LayoutItem : (content as BaseLayoutItem);

        public BaseLayoutItem LayoutItem
        {
            get => 
                base.GetProperty<BaseLayoutItem>(Expression.Lambda<Func<BaseLayoutItem>>(Expression.Property(Expression.Constant(this, typeof(LayoutItemData)), (MethodInfo) methodof(LayoutItemData.get_LayoutItem)), new ParameterExpression[0]));
            set => 
                base.SetProperty<BaseLayoutItem>(Expression.Lambda<Func<BaseLayoutItem>>(Expression.Property(Expression.Constant(this, typeof(LayoutItemData)), (MethodInfo) methodof(LayoutItemData.get_LayoutItem)), new ParameterExpression[0]), value);
        }
    }
}

