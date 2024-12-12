namespace DevExpress.Mvvm
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Windows.Data;

    public static class EnumSourceHelper
    {
        public static IEnumerable<EnumMemberInfo> GetEnumSource(Type enumType, bool useUnderlyingEnumValue = true, IValueConverter nameConverter = null, bool splitNames = false, EnumMembersSortMode sortMode = 0, Func<string, ImageSource> getSvgImageSource = null) => 
            EnumSourceHelperCore.GetEnumSource(enumType, useUnderlyingEnumValue, nameConverter, splitNames, sortMode, null, true, true, getSvgImageSource);
    }
}

