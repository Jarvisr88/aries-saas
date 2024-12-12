namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Mvvm.UI.Interactivity.Internal;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;

    [TargetType(typeof(LookUpEditBase)), TargetType(typeof(ListBoxEdit)), TargetType(typeof(ItemsControl))]
    public class EnumItemsSourceBehavior : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty ItemTemplateProperty;
        public static readonly DependencyProperty EnumTypeProperty;
        public static readonly DependencyProperty UseNumericEnumValueProperty;
        public static readonly DependencyProperty SplitNamesProperty;
        public static readonly DependencyProperty NameConverterProperty;
        public static readonly DependencyProperty SortModeProperty;
        public static readonly DependencyProperty AllowImagesProperty;
        private DataTemplate defaultDataTemplate;

        static EnumItemsSourceBehavior()
        {
            ItemTemplateProperty = DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(EnumItemsSourceBehavior), new FrameworkPropertyMetadata(null, (d, e) => ((EnumItemsSourceBehavior) d).ChangeItemTemplate()));
            EnumTypeProperty = DependencyProperty.Register("EnumType", typeof(Type), typeof(EnumItemsSourceBehavior), new FrameworkPropertyMetadata(null, (d, e) => ((EnumItemsSourceBehavior) d).ChangeAssociatedObjectItemsSource()));
            UseNumericEnumValueProperty = DependencyProperty.Register("UseNumericEnumValue", typeof(bool), typeof(EnumItemsSourceBehavior), new FrameworkPropertyMetadata(false, (d, e) => ((EnumItemsSourceBehavior) d).ChangeAssociatedObjectItemsSource()));
            SplitNamesProperty = DependencyProperty.Register("SplitNames", typeof(bool), typeof(EnumItemsSourceBehavior), new FrameworkPropertyMetadata(true, (d, e) => ((EnumItemsSourceBehavior) d).ChangeAssociatedObjectItemsSource()));
            NameConverterProperty = DependencyProperty.Register("NameConverter", typeof(IValueConverter), typeof(EnumItemsSourceBehavior), new FrameworkPropertyMetadata(null, (d, e) => ((EnumItemsSourceBehavior) d).ChangeAssociatedObjectItemsSource()));
            SortModeProperty = DependencyProperty.Register("SortMode", typeof(EnumMembersSortMode), typeof(EnumItemsSourceBehavior), new FrameworkPropertyMetadata(EnumMembersSortMode.Default, (d, e) => ((EnumItemsSourceBehavior) d).ChangeAssociatedObjectItemsSource()));
            AllowImagesProperty = DependencyProperty.Register("AllowImages", typeof(bool), typeof(EnumItemsSourceBehavior), new FrameworkPropertyMetadata(true, (d, e) => ((EnumItemsSourceBehavior) d).ChangeAssociatedObjectItemsSource()));
        }

        public EnumItemsSourceBehavior()
        {
            InteractionHelper.SetBehaviorInDesignMode(this, InteractionBehaviorInDesignMode.AsWellAsNotInDesignMode);
            this.GetDefaultDataTemplate();
        }

        private void ChangeAssociatedObjectItemsSource()
        {
            if (base.AssociatedObject != null)
            {
                PropertyDescriptor descriptor = TypeDescriptor.GetProperties(base.AssociatedObject).Find("ItemsSource", true);
                if (descriptor == null)
                {
                    throw new Exception("ItemsSource dependency property required");
                }
                Func<string, ImageSource> getSvgImageSource = <>c.<>9__29_0;
                if (<>c.<>9__29_0 == null)
                {
                    Func<string, ImageSource> local1 = <>c.<>9__29_0;
                    getSvgImageSource = <>c.<>9__29_0 = uri => WpfSvgRenderer.CreateImageSource(new Uri(uri), null, null, null, null, true);
                }
                descriptor.SetValue(base.AssociatedObject, EnumSourceHelperCore.GetEnumSource(this.EnumType, this.UseNumericEnumValue, this.NameConverter, this.SplitNames, this.SortMode, null, this.AllowImages, true, getSvgImageSource));
            }
        }

        private void ChangeItemTemplate()
        {
            ItemsControl associatedObject = base.AssociatedObject as ItemsControl;
            if (associatedObject != null)
            {
                associatedObject.ItemTemplate = this.ItemTemplate;
            }
        }

        private void GetDefaultDataTemplate()
        {
            ResourceDictionary dictionary1 = new ResourceDictionary();
            dictionary1.Source = new Uri($"pack://application:,,,/{"DevExpress.Xpf.Core.v19.2"};component/Mvvm.UI/Behaviors/EnumItemsSourceBehavior/EnumItemsSourceDefaultTemplate.xaml", UriKind.Absolute);
            ResourceDictionary dictionary = dictionary1;
            this.defaultDataTemplate = (DataTemplate) dictionary["ItemsSourceDefaultTemplate"];
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            if (this.ItemTemplate == null)
            {
                base.SetCurrentValue(ItemTemplateProperty, this.defaultDataTemplate);
            }
            else
            {
                this.ChangeItemTemplate();
            }
            this.ChangeAssociatedObjectItemsSource();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
        }

        public bool AllowImages
        {
            get => 
                (bool) base.GetValue(AllowImagesProperty);
            set => 
                base.SetValue(AllowImagesProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ItemTemplateProperty);
            set => 
                base.SetValue(ItemTemplateProperty, value);
        }

        public Type EnumType
        {
            get => 
                (Type) base.GetValue(EnumTypeProperty);
            set => 
                base.SetValue(EnumTypeProperty, value);
        }

        public bool UseNumericEnumValue
        {
            get => 
                (bool) base.GetValue(UseNumericEnumValueProperty);
            set => 
                base.SetValue(UseNumericEnumValueProperty, value);
        }

        public bool SplitNames
        {
            get => 
                (bool) base.GetValue(SplitNamesProperty);
            set => 
                base.SetValue(SplitNamesProperty, value);
        }

        public IValueConverter NameConverter
        {
            get => 
                (IValueConverter) base.GetValue(NameConverterProperty);
            set => 
                base.SetValue(NameConverterProperty, value);
        }

        public EnumMembersSortMode SortMode
        {
            get => 
                (EnumMembersSortMode) base.GetValue(SortModeProperty);
            set => 
                base.SetValue(SortModeProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EnumItemsSourceBehavior.<>c <>9 = new EnumItemsSourceBehavior.<>c();
            public static Func<string, ImageSource> <>9__29_0;

            internal void <.cctor>b__35_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((EnumItemsSourceBehavior) d).ChangeItemTemplate();
            }

            internal void <.cctor>b__35_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((EnumItemsSourceBehavior) d).ChangeAssociatedObjectItemsSource();
            }

            internal void <.cctor>b__35_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((EnumItemsSourceBehavior) d).ChangeAssociatedObjectItemsSource();
            }

            internal void <.cctor>b__35_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((EnumItemsSourceBehavior) d).ChangeAssociatedObjectItemsSource();
            }

            internal void <.cctor>b__35_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((EnumItemsSourceBehavior) d).ChangeAssociatedObjectItemsSource();
            }

            internal void <.cctor>b__35_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((EnumItemsSourceBehavior) d).ChangeAssociatedObjectItemsSource();
            }

            internal void <.cctor>b__35_6(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((EnumItemsSourceBehavior) d).ChangeAssociatedObjectItemsSource();
            }

            internal ImageSource <ChangeAssociatedObjectItemsSource>b__29_0(string uri)
            {
                Size? targetSize = null;
                return WpfSvgRenderer.CreateImageSource(new Uri(uri), null, targetSize, null, null, true);
            }
        }
    }
}

