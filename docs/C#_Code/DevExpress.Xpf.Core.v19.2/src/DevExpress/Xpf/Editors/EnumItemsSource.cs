namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;
    using System.Windows.Markup;
    using System.Windows.Media;

    public class EnumItemsSource : MarkupExtension, IEnumerable, IEnumerable<EnumMemberInfo>
    {
        public EnumItemsSource()
        {
            this.SplitNames = true;
            this.AllowImages = true;
        }

        private IEnumerator<EnumMemberInfo> GetEnumerator()
        {
            IEnumerator<EnumMemberInfo> itemsSource = this.ItemsSource;
            IEnumerator<EnumMemberInfo> enumerator3 = itemsSource;
            if (itemsSource == null)
            {
                IEnumerator<EnumMemberInfo> local1 = itemsSource;
                Func<string, ImageSource> getSvgImageSource = <>c.<>9__34_0;
                if (<>c.<>9__34_0 == null)
                {
                    Func<string, ImageSource> local2 = <>c.<>9__34_0;
                    getSvgImageSource = <>c.<>9__34_0 = uri => WpfSvgRenderer.CreateImageSource(new Uri(uri), null, null, null, null, true);
                }
                enumerator3 = this.ItemsSource = EnumHelper.GetEnumSource(this.EnumType, this.UseNumericEnumValue, this.NameConverter, this.SplitNames, this.SortMode, this.AllowImages, true, getSvgImageSource).GetEnumerator();
            }
            return enumerator3;
        }

        public static bool IsEnumItemsSource(object itemsSource) => 
            itemsSource is IEnumerable<EnumMemberInfo>;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            Func<string, ImageSource> getSvgImageSource = <>c.<>9__29_0;
            if (<>c.<>9__29_0 == null)
            {
                Func<string, ImageSource> local1 = <>c.<>9__29_0;
                getSvgImageSource = <>c.<>9__29_0 = uri => WpfSvgRenderer.CreateImageSource(new Uri(uri), null, null, null, null, true);
            }
            return EnumHelper.GetEnumSource(this.EnumType, this.UseNumericEnumValue, this.NameConverter, this.SplitNames, this.SortMode, this.AllowImages, true, getSvgImageSource);
        }

        public static void SetupEnumItemsSource(object itemsSource, Action setupCallback)
        {
            if (IsEnumItemsSource(itemsSource))
            {
                setupCallback();
            }
        }

        IEnumerator<EnumMemberInfo> IEnumerable<EnumMemberInfo>.GetEnumerator() => 
            this.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerator();

        public Type EnumType { get; set; }

        public bool UseNumericEnumValue { get; set; }

        public bool SplitNames { get; set; }

        public IValueConverter NameConverter { get; set; }

        public EnumMembersSortMode SortMode { get; set; }

        public bool AllowImages { get; set; }

        private IEnumerator<EnumMemberInfo> ItemsSource { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EnumItemsSource.<>c <>9 = new EnumItemsSource.<>c();
            public static Func<string, ImageSource> <>9__29_0;
            public static Func<string, ImageSource> <>9__34_0;

            internal ImageSource <GetEnumerator>b__34_0(string uri)
            {
                Size? targetSize = null;
                return WpfSvgRenderer.CreateImageSource(new Uri(uri), null, targetSize, null, null, true);
            }

            internal ImageSource <ProvideValue>b__29_0(string uri)
            {
                Size? targetSize = null;
                return WpfSvgRenderer.CreateImageSource(new Uri(uri), null, targetSize, null, null, true);
            }
        }
    }
}

