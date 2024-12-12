namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple=false)]
    public class CommandAttribute : Attribute
    {
        private bool? useCommandManager;

        public CommandAttribute() : this(true)
        {
        }

        public CommandAttribute(bool isCommand)
        {
            this.IsCommand = isCommand;
        }

        internal bool? GetUseCommandManager() => 
            this.useCommandManager;

        public string Name { get; set; }

        public string CanExecuteMethodName { get; set; }

        public bool IsCommand { get; private set; }

        internal MethodInfo CanExecuteMethod { get; set; }

        public bool UseCommandManager
        {
            get
            {
                throw new NotSupportedException();
            }
            set => 
                this.useCommandManager = new bool?(value);
        }

        internal bool AllowMultipleExecutionCore { get; set; }
    }
}

