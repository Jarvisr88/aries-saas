namespace DevExpress.Xpf.Utils
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public static class DependencyPropertyRegistrator
    {
        private static string GetMemberName<TOwner, TProperty>(Expression<Func<TOwner, TProperty>> propertyExpression)
        {
            MemberExpression body = propertyExpression.Body as MemberExpression;
            MemberExpression expression2 = body;
            if (body == null)
            {
                MemberExpression local1 = body;
                expression2 = (propertyExpression.Body as UnaryExpression).With<UnaryExpression, MemberExpression>(<>c__4<TOwner, TProperty>.<>9__4_0 ??= x => (x.Operand as MemberExpression));
            }
            MemberExpression me = expression2;
            Func<MemberExpression, ParameterExpression> evaluator = <>c__4<TOwner, TProperty>.<>9__4_1;
            if (<>c__4<TOwner, TProperty>.<>9__4_1 == null)
            {
                Func<MemberExpression, ParameterExpression> local3 = <>c__4<TOwner, TProperty>.<>9__4_1;
                evaluator = <>c__4<TOwner, TProperty>.<>9__4_1 = x => x.Expression as ParameterExpression;
            }
            return me.With<MemberExpression, ParameterExpression>(evaluator).If<ParameterExpression>(x => (x.Name == propertyExpression.Parameters[0].Name)).Return<ParameterExpression, string>(x => me.Member.Name, null);
        }

        public static DependencyProperty Register<TOwnerType, TPropertyType>(Expression<Func<TOwnerType, TPropertyType>> expression) where TOwnerType: DependencyObject
        {
            TPropertyType defaultValue = default(TPropertyType);
            return Register<TOwnerType, TPropertyType>(expression, defaultValue);
        }

        public static DependencyProperty Register<TOwnerType, TPropertyType>(Expression<Func<TOwnerType, TPropertyType>> expression, TPropertyType defaultValue) where TOwnerType: DependencyObject => 
            DependencyProperty.Register(GetMemberName<TOwnerType, TPropertyType>(expression), typeof(TPropertyType), typeof(TOwnerType), new FrameworkPropertyMetadata(defaultValue, FrameworkPropertyMetadataOptions.None));

        public static DependencyProperty Register<TOwnerType, TPropertyType>(Expression<Func<TOwnerType, TPropertyType>> expression, TPropertyType defaultValue, DependencyPropertyRegistratorChangedCallback<TOwnerType, TPropertyType> changedCallback) where TOwnerType: DependencyObject => 
            DependencyProperty.Register(GetMemberName<TOwnerType, TPropertyType>(expression), typeof(TPropertyType), typeof(TOwnerType), new FrameworkPropertyMetadata(defaultValue, FrameworkPropertyMetadataOptions.None, delegate (DependencyObject o, DependencyPropertyChangedEventArgs args) {
                changedCallback.Do<DependencyPropertyRegistratorChangedCallback<TOwnerType, TPropertyType>>(x => x((TOwnerType) o, (TPropertyType) args.OldValue, (TPropertyType) args.NewValue));
            }));

        public static DependencyProperty Register<TOwnerType, TPropertyType>(Expression<Func<TOwnerType, TPropertyType>> expression, TPropertyType defaultValue, DependencyPropertyRegistratorChangedCallback<TOwnerType, TPropertyType> changedCallback, DependencyPropertyRegistratorCoerceCallback<TOwnerType, TPropertyType> coerceCallback) where TOwnerType: DependencyObject => 
            DependencyProperty.Register(GetMemberName<TOwnerType, TPropertyType>(expression), typeof(TPropertyType), typeof(TOwnerType), new FrameworkPropertyMetadata(defaultValue, FrameworkPropertyMetadataOptions.None, delegate (DependencyObject o, DependencyPropertyChangedEventArgs args) {
                changedCallback.Do<DependencyPropertyRegistratorChangedCallback<TOwnerType, TPropertyType>>(x => x((TOwnerType) o, (TPropertyType) args.OldValue, (TPropertyType) args.NewValue));
            }, (o, value) => coerceCallback.With<DependencyPropertyRegistratorCoerceCallback<TOwnerType, TPropertyType>, object>(x => x((TOwnerType) o, (TPropertyType) value))));

        public static DependencyPropertyKey RegisterReadOnly<TOwnerType, TPropertyType>(Expression<Func<TOwnerType, TPropertyType>> expression, TPropertyType defaultValue, DependencyPropertyRegistratorChangedCallback<TOwnerType, TPropertyType> changedCallback = null) where TOwnerType: DependencyObject => 
            DependencyProperty.RegisterReadOnly(GetMemberName<TOwnerType, TPropertyType>(expression), typeof(TPropertyType), typeof(TOwnerType), new FrameworkPropertyMetadata(defaultValue, FrameworkPropertyMetadataOptions.None, delegate (DependencyObject o, DependencyPropertyChangedEventArgs args) {
                changedCallback.Do<DependencyPropertyRegistratorChangedCallback<TOwnerType, TPropertyType>>(x => x((TOwnerType) o, (TPropertyType) args.OldValue, (TPropertyType) args.NewValue));
            }));

        [Serializable, CompilerGenerated]
        private sealed class <>c__4<TOwner, TProperty>
        {
            public static readonly DependencyPropertyRegistrator.<>c__4<TOwner, TProperty> <>9;
            public static Func<UnaryExpression, MemberExpression> <>9__4_0;
            public static Func<MemberExpression, ParameterExpression> <>9__4_1;

            static <>c__4()
            {
                DependencyPropertyRegistrator.<>c__4<TOwner, TProperty>.<>9 = new DependencyPropertyRegistrator.<>c__4<TOwner, TProperty>();
            }

            internal MemberExpression <GetMemberName>b__4_0(UnaryExpression x) => 
                x.Operand as MemberExpression;

            internal ParameterExpression <GetMemberName>b__4_1(MemberExpression x) => 
                x.Expression as ParameterExpression;
        }
    }
}

