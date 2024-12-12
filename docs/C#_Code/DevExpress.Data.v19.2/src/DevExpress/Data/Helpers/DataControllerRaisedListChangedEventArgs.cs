namespace DevExpress.Data.Helpers
{
    using System;
    using System.ComponentModel;

    internal class DataControllerRaisedListChangedEventArgs : ListChangedEventArgs
    {
        public DataControllerRaisedListChangedEventArgs(ListChangedType listChangedType, int newIndex);
        public DataControllerRaisedListChangedEventArgs(ListChangedType listChangedType, int newIndex, int oldIndex);
    }
}

