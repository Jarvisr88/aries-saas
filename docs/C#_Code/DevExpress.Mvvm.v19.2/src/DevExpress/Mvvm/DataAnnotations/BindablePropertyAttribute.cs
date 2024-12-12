namespace DevExpress.Mvvm.DataAnnotations
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Property)]
    public class BindablePropertyAttribute : Attribute
    {
        public BindablePropertyAttribute() : this(true)
        {
        }

        public BindablePropertyAttribute(bool isBindable)
        {
            this.IsBindable = isBindable;
        }

        public bool IsBindable { get; private set; }

        public string OnPropertyChangedMethodName { get; set; }

        public string OnPropertyChangingMethodName { get; set; }

        internal MethodInfo OnPropertyChangedMethod { get; set; }

        internal MethodInfo OnPropertyChangingMethod { get; set; }
    }
}

