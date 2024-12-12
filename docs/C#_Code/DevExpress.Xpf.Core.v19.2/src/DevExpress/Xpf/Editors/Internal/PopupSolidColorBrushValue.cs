namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows.Media;

    public class PopupSolidColorBrushValue : PopupBrushValue
    {
        public SolidColorBrush Color
        {
            get => 
                base.Brush.ToSolidColorBrush();
            set
            {
                if (!Equals(base.Brush, value))
                {
                    base.Brush = value;
                    base.RaisePropertyChanged<SolidColorBrush>(Expression.Lambda<Func<SolidColorBrush>>(Expression.Property(Expression.Constant(this, typeof(PopupSolidColorBrushValue)), (MethodInfo) methodof(PopupSolidColorBrushValue.get_Color)), new ParameterExpression[0]));
                }
            }
        }
    }
}

