namespace DevExpress.Xpf.Utils
{
    using System;
    using System.Windows;

    public static class PropertyMetadataHelper
    {
        public static PropertyMetadata Create(object defaultValue) => 
            new PropertyMetadata(defaultValue);

        public static PropertyMetadata Create(PropertyChangedCallback propertyChangedCallback) => 
            new PropertyMetadata(propertyChangedCallback);

        public static PropertyMetadata Create(object defaultValue, PropertyChangedCallback propertyChangedCallback) => 
            new PropertyMetadata(defaultValue, propertyChangedCallback);

        public static PropertyMetadata Create(object defaultValue, PropertyChangedCallback propertyChangedCallback, CoerceValueCallback coerceValueCallback) => 
            new PropertyMetadata(defaultValue, propertyChangedCallback, coerceValueCallback);
    }
}

