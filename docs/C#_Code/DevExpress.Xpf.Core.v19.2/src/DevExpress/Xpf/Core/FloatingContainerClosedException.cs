namespace DevExpress.Xpf.Core
{
    using System;

    public class FloatingContainerClosedException : Exception
    {
        public static void CheckFloatingContainerIsNotClosed(FloatingContainer container)
        {
            if (container.IsClosed)
            {
                throw new FloatingContainerClosedException();
            }
        }
    }
}

