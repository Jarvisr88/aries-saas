namespace DevExpress.Data
{
    using System;
    using System.ComponentModel;

    public class SelectionChangedEventArgs : EventArgs
    {
        private CollectionChangeAction action;
        private int controllerRow;
        private static SelectionChangedEventArgs refresh;

        public SelectionChangedEventArgs();
        public SelectionChangedEventArgs(CollectionChangeAction action, int controllerRow);

        internal static SelectionChangedEventArgs Refresh { get; }

        public CollectionChangeAction Action { get; }

        public int ControllerRow { get; }
    }
}

