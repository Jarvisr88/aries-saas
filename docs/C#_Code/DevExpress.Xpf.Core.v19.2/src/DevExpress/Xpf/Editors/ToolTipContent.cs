namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public class ToolTipContent : BindableBase
    {
        public ToolTipContent(string toolTip)
        {
            this.Content = toolTip;
        }

        public override string ToString() => 
            this.Content;

        public string Content
        {
            get => 
                base.GetProperty<string>(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(ToolTipContent)), (MethodInfo) methodof(ToolTipContent.get_Content)), new ParameterExpression[0]));
            private set => 
                base.SetProperty<string>(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(ToolTipContent)), (MethodInfo) methodof(ToolTipContent.get_Content)), new ParameterExpression[0]), value);
        }
    }
}

