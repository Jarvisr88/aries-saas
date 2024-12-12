namespace DevExpress.Data
{
    using System.ComponentModel;

    public interface IBoundPropertyEx : IBoundProperty
    {
        PropertyDescriptor Descriptor { get; }
    }
}

