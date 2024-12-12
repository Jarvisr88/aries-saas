namespace DevExpress.Office
{
    using System;

    public class EmptyDestination<T> : LeafElementDestination<T> where T: IDestinationImporter
    {
        public EmptyDestination(T importer) : base(importer)
        {
        }
    }
}

