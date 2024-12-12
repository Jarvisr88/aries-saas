namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native.ViewGenerator;
    using DevExpress.Utils.Design;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Windows.Data;

    public static class EnumHelper
    {
        public static int GetEnumCount(Type enumType) => 
            EnumSourceHelperCore.GetEnumCount(enumType);

        public static IEnumerable<EnumMemberInfo> GetEnumSource(Type enumType, bool useUnderlyingEnumValue = true, IValueConverter nameConverter = null, bool splitNames = false, EnumMembersSortMode sortMode = 0, bool allowImages = true, bool allowText = true, Func<string, ImageSource> getSvgImageSource = null) => 
            EnumSourceHelperCore.GetEnumSource(enumType, useUnderlyingEnumValue, nameConverter, splitNames, sortMode, ViewModelMetadataSource.GetKnownImageCallback(ImageType.Colored), allowImages, allowText, getSvgImageSource);
    }
}

