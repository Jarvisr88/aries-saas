namespace DevExpress.Data.Helpers
{
    using System;
    using System.Collections.Generic;

    public class DataControllerNotificationProviders
    {
        private static DataControllerNotificationProviders _default;
        private List<INotificationProvider> providers;

        public DataControllerNotificationProviders();
        public void AddProvider(INotificationProvider provider);
        public INotificationProvider FindProvider(object list);
        public void RemoveProvider(INotificationProvider provider);

        public static DataControllerNotificationProviders Default { get; }
    }
}

