namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;

    public class DependencyPropertyHelper
    {
        public static DependencyProperty RegisterAttachedProperty<Owner, T>(string name, T defaultValue, FrameworkPropertyMetadataOptions options, DependencyPropertyChangedCallback<Owner, T> changedCallback) where Owner: class;
        public static DependencyProperty RegisterAttachedProperty<Owner, T>(string name, T defaultValue, FrameworkPropertyMetadataOptions options, DependencyPropertyChangedCallback<Owner, T> changedCallback, CoercePropertyValueCallback<Owner, T> coerceValueCallBack) where Owner: class;
        public static DependencyProperty RegisterAttachedPropertyCore<Owner, T>(string name, T defaultValue, FrameworkPropertyMetadataOptions options, PropertyChangedCallback changedCallback);
        public static DependencyProperty RegisterProperty<Owner, T>(string name, T defaultValue) where Owner: class;
        public static DependencyProperty RegisterProperty<Owner, T>(string name, T defaultValue, DependencyPropertyChangedCallback<Owner, T> changedCallback) where Owner: class;
        public static DependencyProperty RegisterProperty<Owner, T>(string name, T defaultValue, FrameworkPropertyMetadataOptions flags) where Owner: class;
        public static DependencyProperty RegisterProperty<Owner, T>(string name, T defaultValue, DependencyPropertyChangedCallback<Owner, T> changedCallback, CoercePropertyValueCallback<Owner, T> coerceValueCallBack) where Owner: class;
        public static DependencyProperty RegisterProperty<Owner, T>(string name, T defaultValue, FrameworkPropertyMetadataOptions flags, DependencyPropertyChangedCallback<Owner, T> changedCallback) where Owner: class;
        public static DependencyProperty RegisterProperty<Owner, T>(string name, T defaultValue, FrameworkPropertyMetadataOptions flags, DependencyPropertyChangedCallback<Owner, T> changedCallback, CoercePropertyValueCallback<Owner, T> coerceValueCallBack) where Owner: class;
        public static DependencyPropertyKey RegisterReadOnlyProperty<Owner, T>(string name, T defaultValue) where Owner: class;
        public static DependencyPropertyKey RegisterReadOnlyProperty<Owner, T>(string name, T defaultValue, DependencyPropertyChangedCallback<Owner, T> changedCallback) where Owner: class;
    }
}

