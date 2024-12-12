namespace DevExpress.Data.Access
{
    using DevExpress.Data;
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;

    public class ComplexPropertyDescriptorLinq : ComplexPropertyDescriptor
    {
        private PropertyInfo[] descriptors;
        public Func<object, object> Func;
        private ComplexPropertyDescriptorReflection internalSet;

        public ComplexPropertyDescriptorLinq(DataControllerBase controller, string path);
        public ComplexPropertyDescriptorLinq(object sourceObject, string path);
        private void Compile();
        protected override AttributeCollection CreateAttributeCollection();
        private Expression GenerateTree(ParameterExpression result, Expression propertyResult, int c);
        private PropertyInfo GetDescriptor(string name, object obj, Type type);
        public override object GetOwnerOfLast(object component);
        public override object GetValue(object component);
        protected override void Prepare();
        public override void SetValue(object component, object value);

        private PropertyInfo Last { get; }

        private PropertyInfo Root { get; }

        public override bool IsReadOnly { get; }

        public override Type PropertyType { get; }

        public override Type ComponentType { get; }

        public override string DisplayName { get; }
    }
}

