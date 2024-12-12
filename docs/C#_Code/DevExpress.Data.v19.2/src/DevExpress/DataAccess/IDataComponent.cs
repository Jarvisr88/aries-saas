namespace DevExpress.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Xml.Linq;

    public interface IDataComponent : IComponent, IDisposable
    {
        void Fill(IEnumerable<IParameter> sourceParameters);
        void LoadFromXml(XElement element);
        XElement SaveToXml();

        string DataMember { get; }

        string Name { get; set; }
    }
}

