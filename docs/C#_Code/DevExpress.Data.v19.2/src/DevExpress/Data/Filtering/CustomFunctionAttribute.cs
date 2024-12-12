namespace DevExpress.Data.Filtering
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple=true)]
    public class CustomFunctionAttribute : Attribute
    {
        private readonly string functionName;

        public CustomFunctionAttribute(string functionName);

        public string FunctionName { get; }

        public string Image { get; set; }

        public override object TypeId { get; }
    }
}

