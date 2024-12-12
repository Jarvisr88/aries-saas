namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public class CommandCheckItem : CommandBase
    {
        private bool isChecked = true;

        protected override void ExecuteInternal(object parameter)
        {
            base.ExecuteInternal(parameter);
        }

        protected internal void UpdateCheckState(Func<bool> actualIsChecked)
        {
            this.IsChecked = actualIsChecked();
            base.RaisePropertyChanged<bool>(Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(CommandCheckItem)), (MethodInfo) methodof(CommandCheckItem.get_IsChecked)), new ParameterExpression[0]));
        }

        public bool IsChecked
        {
            get => 
                this.isChecked;
            private set => 
                base.SetProperty<bool>(ref this.isChecked, value, Expression.Lambda<Func<bool>>(Expression.Property(Expression.Constant(this, typeof(CommandCheckItem)), (MethodInfo) methodof(CommandCheckItem.get_IsChecked)), new ParameterExpression[0]));
        }
    }
}

