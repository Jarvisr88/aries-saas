namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple=false)]
    public class CommandParameterAttribute : Attribute
    {
        public CommandParameterAttribute(string commandParameter)
        {
            this.CommandParameter = commandParameter;
        }

        public string CommandParameter { get; private set; }
    }
}

