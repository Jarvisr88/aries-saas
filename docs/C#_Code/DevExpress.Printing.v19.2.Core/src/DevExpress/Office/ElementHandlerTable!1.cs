namespace DevExpress.Office
{
    using System.Collections.Generic;

    public class ElementHandlerTable<T> : Dictionary<string, ElementHandler<T>> where T: IDestinationImporter
    {
    }
}

