namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Core.Serialization;
    using System;
    using System.Windows;

    public class WindowSerializationProvider : SerializationProvider
    {
        private static void ApplyState(Window w, WindowSerializationInfo state);
        private WindowSerializationInfo GetState(Window w);
        protected internal override void OnEndDeserializing(DependencyObject dObj, string restoredVersion);
        protected internal override void OnEndSerializing(DependencyObject dObj);
        protected internal override void OnStartDeserializing(DependencyObject dObj, LayoutAllowEventArgs ea);
        protected internal override void OnStartSerializing(DependencyObject dObj);
        private static WindowSerializationInfo SaveState(Window w);
        private void SetState(Window w, WindowSerializationInfo state);

        private class WindowStateApplier
        {
            private readonly Window w;
            private readonly WindowSerializationInfo state;

            private WindowStateApplier(Window w, WindowSerializationInfo state);
            private void Maximize();
            private void OnLoaded(object sender, RoutedEventArgs e);
            public static void Run(Window w, WindowSerializationInfo state);
        }
    }
}

