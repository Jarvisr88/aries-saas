namespace DevExpress.Data.Browsing
{
    using System;
    using System.Runtime.CompilerServices;

    public class SimpleDataContextContainer : IDataContextContainer
    {
        public SimpleDataContextContainer(DevExpress.Data.Browsing.DataContext dataContext);

        public DevExpress.Data.Browsing.DataContext DataContext { get; private set; }
    }
}

