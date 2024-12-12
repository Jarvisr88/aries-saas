namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public abstract class CommandBase : BindableBase, ICommand
    {
        private ICommand command;
        private Uri smallGlyph;
        private Uri largeGlyph;
        private string caption;
        private string hint;
        private string group;

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        protected CommandBase()
        {
        }

        public bool CanExecute(object parameter) => 
            this.CanExecuteInternal(parameter);

        protected virtual bool CanExecuteInternal(object parameter)
        {
            Func<bool> fallback = <>c.<>9__29_1;
            if (<>c.<>9__29_1 == null)
            {
                Func<bool> local1 = <>c.<>9__29_1;
                fallback = <>c.<>9__29_1 = () => false;
            }
            return this.Command.Return<ICommand, bool>(x => x.CanExecute(parameter), fallback);
        }

        public void Execute(object parameter)
        {
            this.ExecuteInternal(parameter);
        }

        protected virtual void ExecuteInternal(object parameter)
        {
            this.Command.Do<ICommand>(x => x.Execute(parameter));
        }

        public ICommand Command
        {
            get => 
                this.command;
            set => 
                base.SetProperty<ICommand>(ref this.command, value, Expression.Lambda<Func<ICommand>>(Expression.Property(Expression.Constant(this, typeof(DevExpress.Xpf.DocumentViewer.CommandBase)), (MethodInfo) methodof(DevExpress.Xpf.DocumentViewer.CommandBase.get_Command)), new ParameterExpression[0]));
        }

        public Uri SmallGlyph
        {
            get => 
                this.smallGlyph;
            set => 
                base.SetProperty<Uri>(ref this.smallGlyph, value, Expression.Lambda<Func<Uri>>(Expression.Property(Expression.Constant(this, typeof(DevExpress.Xpf.DocumentViewer.CommandBase)), (MethodInfo) methodof(DevExpress.Xpf.DocumentViewer.CommandBase.get_SmallGlyph)), new ParameterExpression[0]));
        }

        public Uri LargeGlyph
        {
            get => 
                this.largeGlyph;
            set => 
                base.SetProperty<Uri>(ref this.largeGlyph, value, Expression.Lambda<Func<Uri>>(Expression.Property(Expression.Constant(this, typeof(DevExpress.Xpf.DocumentViewer.CommandBase)), (MethodInfo) methodof(DevExpress.Xpf.DocumentViewer.CommandBase.get_LargeGlyph)), new ParameterExpression[0]));
        }

        public string Caption
        {
            get => 
                this.caption;
            set => 
                base.SetProperty<string>(ref this.caption, value, Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(DevExpress.Xpf.DocumentViewer.CommandBase)), (MethodInfo) methodof(DevExpress.Xpf.DocumentViewer.CommandBase.get_Caption)), new ParameterExpression[0]));
        }

        public string Hint
        {
            get => 
                this.hint;
            set => 
                base.SetProperty<string>(ref this.hint, value, Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(DevExpress.Xpf.DocumentViewer.CommandBase)), (MethodInfo) methodof(DevExpress.Xpf.DocumentViewer.CommandBase.get_Hint)), new ParameterExpression[0]));
        }

        public string Group
        {
            get => 
                this.group;
            set => 
                base.SetProperty<string>(ref this.group, value, Expression.Lambda<Func<string>>(Expression.Property(Expression.Constant(this, typeof(DevExpress.Xpf.DocumentViewer.CommandBase)), (MethodInfo) methodof(DevExpress.Xpf.DocumentViewer.CommandBase.get_Group)), new ParameterExpression[0]));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DevExpress.Xpf.DocumentViewer.CommandBase.<>c <>9 = new DevExpress.Xpf.DocumentViewer.CommandBase.<>c();
            public static Func<bool> <>9__29_1;

            internal bool <CanExecuteInternal>b__29_1() => 
                false;
        }
    }
}

