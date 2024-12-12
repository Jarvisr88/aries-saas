namespace DevExpress.Data.Browsing
{
    using System.ComponentModel;

    public interface IRelatedDataBrowser
    {
        PropertyDescriptor RelatedProperty { get; }

        IRelatedDataBrowser Parent { get; }
    }
}

