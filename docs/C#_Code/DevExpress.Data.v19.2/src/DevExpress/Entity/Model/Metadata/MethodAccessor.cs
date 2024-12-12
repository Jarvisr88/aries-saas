namespace DevExpress.Entity.Model.Metadata
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class MethodAccessor
    {
        private object source;
        private Type sourceType;
        private MethodInfo method;

        private MethodAccessor(string name)
        {
            this.Name = name;
        }

        public MethodAccessor(object source, string name) : this(name)
        {
            this.source = source;
            if (source != null)
            {
                this.sourceType = source.GetType();
            }
        }

        public MethodAccessor(Type sourceType, string name) : this(name)
        {
            this.sourceType = sourceType;
        }

        public object Invoke(Func<object[]> argumentsSource = null) => 
            this.Invoke(this.source, argumentsSource);

        public object Invoke(object target, Func<object[]> argumentsSource = null)
        {
            if (this.method == null)
            {
                this.method = this.sourceType.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.IgnoreCase).FirstOrDefault<MethodInfo>(x => (x.Name == this.Name) && !x.IsGenericMethod);
            }
            return ((this.method != null) ? ((argumentsSource != null) ? this.method.Invoke(target, argumentsSource()) : this.method.Invoke(target, new object[0])) : null);
        }

        public string Name { get; private set; }
    }
}

