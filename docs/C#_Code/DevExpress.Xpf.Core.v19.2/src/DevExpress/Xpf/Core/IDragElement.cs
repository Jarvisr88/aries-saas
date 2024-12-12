namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;

    public interface IDragElement
    {
        void Destroy();
        void UpdateLocation(Point newPos);
    }
}

