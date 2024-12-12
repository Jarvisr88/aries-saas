namespace DevExpress.Data
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class BoundPropertyList
    {
        private List<IBoundProperty> properties;

        public BoundPropertyList(List<IBoundProperty> properties);
        private IBoundProperty FindProperty(string name, bool isDisplayName);
        public IBoundProperty GetBoundPropertyByDisplayName(string displayName);
        protected static bool ResolveByDisplayName(IBoundProperty property, string displayName);
        protected static bool ResolveByName(IBoundProperty property, string name);

        public IBoundProperty this[string fieldName] { get; }

        protected List<IBoundProperty> Properties { get; }
    }
}

