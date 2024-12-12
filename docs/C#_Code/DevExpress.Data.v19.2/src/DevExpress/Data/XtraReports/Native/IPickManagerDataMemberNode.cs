namespace DevExpress.Data.XtraReports.Native
{
    using DevExpress.Data.Browsing.Design;

    public interface IPickManagerDataMemberNode : INode
    {
        IPropertyDescriptor PropertyDescriptor { get; }
    }
}

