namespace DevExpress.Utils.Filtering
{
    using System;

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple=false)]
    public abstract class FilterAttribute : Attribute
    {
        internal static readonly object[] EmptyValues = new object[0];
        internal static readonly string[] EmptyMembers = new string[0];

        protected FilterAttribute()
        {
        }

        protected internal virtual string[] GetMembers() => 
            EmptyMembers;
    }
}

