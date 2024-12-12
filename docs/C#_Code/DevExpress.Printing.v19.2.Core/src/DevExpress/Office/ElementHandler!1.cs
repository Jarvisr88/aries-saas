namespace DevExpress.Office
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml;

    public delegate Destination ElementHandler<T>(T importer, XmlReader reader) where T: IDestinationImporter;
}

