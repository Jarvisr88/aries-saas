namespace DevExpress.Data
{
    using System;
    using System.ComponentModel;

    public interface IDataControllerData2 : IDataControllerData
    {
        ComplexColumnInfoCollection GetComplexColumns();
        bool? IsRowFit(int listSourceRow, bool fit);
        PropertyDescriptorCollection PatchPropertyDescriptorCollection(PropertyDescriptorCollection collection);
        void SubstituteFilter(SubstituteFilterEventArgs args);

        bool HasUserFilter { get; }

        bool CanUseFastProperties { get; }
    }
}

