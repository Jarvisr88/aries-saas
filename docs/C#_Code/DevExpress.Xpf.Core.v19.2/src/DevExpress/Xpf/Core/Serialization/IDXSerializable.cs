namespace DevExpress.Xpf.Core.Serialization
{
    using System;
    using System.Windows;

    public interface IDXSerializable
    {
        DependencyObject Source { get; }

        string FullPath { get; }

        object EventTarget { get; set; }
    }
}

