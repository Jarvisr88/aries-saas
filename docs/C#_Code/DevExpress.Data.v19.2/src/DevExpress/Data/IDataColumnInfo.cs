namespace DevExpress.Data
{
    using System;
    using System.Collections.Generic;

    public interface IDataColumnInfo
    {
        List<IDataColumnInfo> Columns { get; }

        string UnboundExpression { get; }

        string Caption { get; }

        string FieldName { get; }

        string Name { get; }

        Type FieldType { get; }

        DataControllerBase Controller { get; }
    }
}

