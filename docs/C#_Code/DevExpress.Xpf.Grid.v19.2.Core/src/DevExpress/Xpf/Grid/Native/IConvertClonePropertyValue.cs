namespace DevExpress.Xpf.Grid.Native
{
    using System;
    using System.Windows;

    public interface IConvertClonePropertyValue
    {
        object ConvertClonePropertyValue(string propertyName, object sourceValue, DependencyObject destinationObject);
    }
}

