namespace DevExpress.Logify
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.All)]
    public class LogifyIgnoreExceptionAttribute : Attribute
    {
        public LogifyIgnoreExceptionAttribute() : this(true)
        {
        }

        public LogifyIgnoreExceptionAttribute(bool ignore)
        {
            this.Ignore = ignore;
        }

        public bool Ignore { get; set; }
    }
}

