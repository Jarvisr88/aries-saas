namespace DevExpress.Office
{
    using System;

    public abstract class LeafElementDestination<T> : ElementDestination<T> where T: IDestinationImporter
    {
        private static readonly ElementHandlerTable<T> handlerTable;

        static LeafElementDestination()
        {
            LeafElementDestination<T>.handlerTable = new ElementHandlerTable<T>();
        }

        protected LeafElementDestination(T importer) : base(importer)
        {
        }

        protected internal override ElementHandlerTable<T> ElementHandlerTable =>
            LeafElementDestination<T>.handlerTable;
    }
}

