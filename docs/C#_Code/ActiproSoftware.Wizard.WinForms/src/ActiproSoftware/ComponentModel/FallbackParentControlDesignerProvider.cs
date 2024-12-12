namespace ActiproSoftware.ComponentModel
{
    using System;
    using System.ComponentModel;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class FallbackParentControlDesignerProvider : TypeDescriptionProvider
    {
        public FallbackParentControlDesignerProvider(TypeDescriptionProvider parent);
        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance);
        public static void Register(Type type);

        internal sealed class #Uof : FallbackControlDesignerProvider.#Tof
        {
            public #Uof(ICustomTypeDescriptor parent, IComponent component);

            protected override string FallbackDesignerType { get; }
        }
    }
}

