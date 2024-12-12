namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Windows;

    internal abstract class BaseWeakEventManager<T> : WeakEventManager where T: WeakEventManager, new()
    {
        protected BaseWeakEventManager()
        {
        }

        protected static T GetManager()
        {
            Type managerType = typeof(T);
            T currentManager = (T) GetCurrentManager(managerType);
            if (currentManager == null)
            {
                currentManager = Activator.CreateInstance<T>();
                SetCurrentManager(managerType, currentManager);
            }
            return currentManager;
        }
    }
}

