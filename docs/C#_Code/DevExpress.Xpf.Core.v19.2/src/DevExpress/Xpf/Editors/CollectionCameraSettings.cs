namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    public class CollectionCameraSettings : BaseCameraSettings
    {
        public IEnumerable<object> AvaliableValues
        {
            get => 
                base.GetProperty<IEnumerable<object>>(Expression.Lambda<Func<IEnumerable<object>>>(Expression.Property(Expression.Constant(this, typeof(CollectionCameraSettings)), (MethodInfo) methodof(CollectionCameraSettings.get_AvaliableValues)), new ParameterExpression[0]));
            set => 
                base.SetProperty<IEnumerable<object>>(Expression.Lambda<Func<IEnumerable<object>>>(Expression.Property(Expression.Constant(this, typeof(CollectionCameraSettings)), (MethodInfo) methodof(CollectionCameraSettings.get_AvaliableValues)), new ParameterExpression[0]), value);
        }
    }
}

