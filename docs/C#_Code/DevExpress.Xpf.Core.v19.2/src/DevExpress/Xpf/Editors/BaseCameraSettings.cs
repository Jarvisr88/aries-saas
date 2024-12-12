namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public class BaseCameraSettings : ViewModelBase
    {
        public string Caption
        {
            get => 
                base.GetProperty<string>(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(BaseCameraSettings)), (MethodInfo) methodof(BaseCameraSettings.get_Caption)), new ParameterExpression[0]));
            set => 
                base.SetProperty<string>(Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(BaseCameraSettings)), (MethodInfo) methodof(BaseCameraSettings.get_Caption)), new ParameterExpression[0]), value);
        }
    }
}

