namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Runtime.CompilerServices;

    internal static class StoredIDSerializationManager
    {
        private static volatile int counter = 0;
        private static readonly object syncObject = new object();

        public static void BeginSerialize()
        {
            object syncObject = StoredIDSerializationManager.syncObject;
            lock (syncObject)
            {
                counter++;
            }
        }

        public static void EndSerialize()
        {
            object syncObject = StoredIDSerializationManager.syncObject;
            lock (syncObject)
            {
                if (counter > 0)
                {
                    counter--;
                }
            }
        }

        public static bool ShouldSerializeStoredID =>
            counter > 0;
    }
}

