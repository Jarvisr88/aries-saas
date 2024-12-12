namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Markup;

    public class TouchPropertyExtension : MarkupExtension
    {
        private static readonly Dictionary<DependencyProperty, DependencyProperty> Properties = new Dictionary<DependencyProperty, DependencyProperty>();
        [IgnoreDependencyPropertiesConsistencyChecker]
        private DependencyProperty key;

        public TouchPropertyExtension()
        {
        }

        public TouchPropertyExtension(DependencyProperty key)
        {
            this.Key = key;
        }

        private static void OnTouchPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            DependencyProperty property = null;
            if (!Properties.TryGetValue(this.Key, out property))
            {
                property = DependencyProperty.RegisterAttached("TouchProperty" + this.Key.OwnerType.FullName + this.Key.Name, typeof(TouchInfo), typeof(TouchInfo), new PropertyMetadata(new PropertyChangedCallback(TouchPropertyExtension.OnTouchPropertyChanged)));
                Properties[this.Key] = property;
            }
            return property;
        }

        public DependencyProperty Key
        {
            get => 
                this.key;
            set => 
                this.key = value;
        }
    }
}

