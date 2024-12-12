namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows.Data;

    public class DetailDescriptorCollectionConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DetailDescriptorCollection))
            {
                return null;
            }
            Func<DetailDescriptorBase, DetailDescriptorContainer> cast = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<DetailDescriptorBase, DetailDescriptorContainer> local1 = <>c.<>9__0_0;
                cast = <>c.<>9__0_0 = detailDescriptor => new DetailDescriptorContainer(detailDescriptor);
            }
            return SimpleBridgeReadonlyObservableCollection<DetailDescriptorContainer, DetailDescriptorBase>.Create((DetailDescriptorCollection) value, cast);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DetailDescriptorCollectionConverter.<>c <>9 = new DetailDescriptorCollectionConverter.<>c();
            public static Func<DetailDescriptorBase, DetailDescriptorContainer> <>9__0_0;

            internal DetailDescriptorContainer <System.Windows.Data.IValueConverter.Convert>b__0_0(DetailDescriptorBase detailDescriptor) => 
                new DetailDescriptorContainer(detailDescriptor);
        }
    }
}

