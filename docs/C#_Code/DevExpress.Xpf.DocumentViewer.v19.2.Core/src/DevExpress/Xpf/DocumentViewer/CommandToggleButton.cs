namespace DevExpress.Xpf.DocumentViewer
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public class CommandToggleButton : CommandBase
    {
        private int groupIndex;
        private bool isChecked;

        public bool IsChecked
        {
            get => 
                this.isChecked;
            set => 
                base.SetProperty<bool>(ref this.isChecked, value, Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(CommandToggleButton)), (MethodInfo) methodof(CommandToggleButton.get_IsChecked)), new ParameterExpression[0]));
        }

        public int GroupIndex
        {
            get => 
                this.groupIndex;
            set => 
                base.SetProperty<int>(ref this.groupIndex, value, Expression.Lambda<Func<int>>(Expression.Property(Expression.Constant(this, typeof(CommandToggleButton)), (MethodInfo) methodof(CommandToggleButton.get_GroupIndex)), new ParameterExpression[0]));
        }
    }
}

