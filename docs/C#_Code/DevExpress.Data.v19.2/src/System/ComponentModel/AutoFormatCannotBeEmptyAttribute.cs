namespace System.ComponentModel
{
    using System;

    [AttributeUsage(AttributeTargets.Property, Inherited=true, AllowMultiple=false)]
    public class AutoFormatCannotBeEmptyAttribute : Attribute
    {
    }
}

