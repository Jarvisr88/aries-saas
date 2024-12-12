namespace System.ComponentModel
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public sealed class DXDisplayNameIgnoreAttribute : Attribute
    {
        public bool IgnoreRecursionOnly { get; set; }
    }
}

