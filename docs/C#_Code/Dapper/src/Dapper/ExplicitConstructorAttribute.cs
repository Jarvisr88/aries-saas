namespace Dapper
{
    using System;

    [AttributeUsage(AttributeTargets.Constructor, AllowMultiple=false)]
    public sealed class ExplicitConstructorAttribute : Attribute
    {
    }
}

