namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;

    public interface IViewModelBuilder
    {
        void BuildBindablePropertyAttributes(PropertyInfo property, PropertyBuilder builder);
        bool ForceBindableProperty(PropertyInfo property);

        string TypeNameModifier { get; }
    }
}

