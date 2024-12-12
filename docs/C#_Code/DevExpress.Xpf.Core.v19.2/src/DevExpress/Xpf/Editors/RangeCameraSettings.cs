namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public class RangeCameraSettings : BaseCameraSettings
    {
        public int MinValue
        {
            get => 
                base.GetProperty<int>(Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(RangeCameraSettings)), (MethodInfo) methodof(RangeCameraSettings.get_MinValue)), new ParameterExpression[0]));
            set => 
                base.SetProperty<int>(Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(RangeCameraSettings)), (MethodInfo) methodof(RangeCameraSettings.get_MinValue)), new ParameterExpression[0]), value);
        }

        public int MaxValue
        {
            get => 
                base.GetProperty<int>(Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(RangeCameraSettings)), (MethodInfo) methodof(RangeCameraSettings.get_MaxValue)), new ParameterExpression[0]));
            set => 
                base.SetProperty<int>(Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(RangeCameraSettings)), (MethodInfo) methodof(RangeCameraSettings.get_MaxValue)), new ParameterExpression[0]), value);
        }
    }
}

