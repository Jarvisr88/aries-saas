namespace DevExpress.XtraEditors.Filtering
{
    using System;
    using System.Collections.Generic;

    public interface IFilterParametersOwner
    {
        void AddParameter(string name, Type type);

        IList<IFilterParameter> Parameters { get; }

        bool CanAddParameters { get; }
    }
}

