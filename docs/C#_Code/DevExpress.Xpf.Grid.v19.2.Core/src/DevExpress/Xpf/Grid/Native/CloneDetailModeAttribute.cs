namespace DevExpress.Xpf.Grid.Native
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Property, Inherited=true)]
    public class CloneDetailModeAttribute : Attribute
    {
        public CloneDetailModeAttribute(CloneDetailMode mode)
        {
            this.Mode = mode;
        }

        public CloneDetailMode Mode { get; private set; }
    }
}

