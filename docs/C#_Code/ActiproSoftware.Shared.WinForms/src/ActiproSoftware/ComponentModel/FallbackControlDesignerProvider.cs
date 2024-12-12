namespace ActiproSoftware.ComponentModel
{
    using System;
    using System.ComponentModel;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class FallbackControlDesignerProvider : TypeDescriptionProvider
    {
        public FallbackControlDesignerProvider(TypeDescriptionProvider parent);
        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance);
        public static void Register(Type type);

        internal class #Tof : CustomTypeDescriptor
        {
            private IServiceProvider #NH;

            public #Tof(ICustomTypeDescriptor parent, IComponent component);
            public override AttributeCollection GetAttributes();

            protected virtual string FallbackDesignerType { get; }
        }
    }
}

