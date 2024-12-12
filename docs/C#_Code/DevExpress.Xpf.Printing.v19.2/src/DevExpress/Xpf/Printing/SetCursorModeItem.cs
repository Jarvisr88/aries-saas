namespace DevExpress.Xpf.Printing
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public class SetCursorModeItem : CommandCheckItem
    {
        private CursorModeType commandValue;

        public CursorModeType CommandValue
        {
            get => 
                this.commandValue;
            set => 
                base.SetProperty<CursorModeType>(ref this.commandValue, value, Expression.Lambda<Func<CursorModeType>>(Expression.Property(Expression.Constant(this, typeof(SetCursorModeItem)), (MethodInfo) methodof(SetCursorModeItem.get_CommandValue)), new ParameterExpression[0]));
        }
    }
}

