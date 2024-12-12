namespace DevExpress.Utils.Zip.Internal
{
    using System;

    public class FactorySingleton<T> where T: class, new()
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (FactorySingleton<T>.instance == null)
                {
                    FactorySingleton<T>.instance = Activator.CreateInstance<T>();
                }
                return FactorySingleton<T>.instance;
            }
        }
    }
}

