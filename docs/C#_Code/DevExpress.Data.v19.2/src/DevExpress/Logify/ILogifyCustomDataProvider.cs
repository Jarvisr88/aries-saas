namespace DevExpress.Logify
{
    using System;
    using System.Collections.Generic;

    public interface ILogifyCustomDataProvider
    {
        void CollectCustomData(IDictionary<string, string> data);
    }
}

