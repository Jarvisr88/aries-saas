namespace DevExpress.XtraReports.Native
{
    using DevExpress.XtraReports.UI;
    using System;

    public interface ICalculatedField
    {
        string Name { get; }

        string DisplayName { get; }

        string Expression { get; }

        DevExpress.XtraReports.UI.FieldType FieldType { get; }

        object DataSource { get; }

        string DataMember { get; }
    }
}

