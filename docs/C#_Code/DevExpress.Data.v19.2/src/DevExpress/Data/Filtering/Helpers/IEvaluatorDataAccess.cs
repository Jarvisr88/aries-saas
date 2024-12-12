namespace DevExpress.Data.Filtering.Helpers
{
    using System;
    using System.ComponentModel;

    public interface IEvaluatorDataAccess
    {
        object GetValue(PropertyDescriptor descriptor, object theObject);
    }
}

