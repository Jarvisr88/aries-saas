namespace DevExpress.Xpf.DocumentViewer
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    public class CommandCheckItems : CommandBase
    {
        private IEnumerable<CommandToggleButton> items;

        public void UpdateCheckState(Func<CommandToggleButton, bool> checkItemFunc)
        {
            if (this.Items != null)
            {
                foreach (CommandToggleButton button in this.Items)
                {
                    button.IsChecked = checkItemFunc(button);
                }
            }
        }

        public IEnumerable<CommandToggleButton> Items
        {
            get => 
                this.items;
            set => 
                base.SetProperty<IEnumerable<CommandToggleButton>>(ref this.items, value, Expression.Lambda<Func<IEnumerable<CommandToggleButton>>>(Expression.Property(Expression.Constant(this, typeof(CommandCheckItems)), (MethodInfo) methodof(CommandCheckItems.get_Items)), new ParameterExpression[0]));
        }
    }
}

