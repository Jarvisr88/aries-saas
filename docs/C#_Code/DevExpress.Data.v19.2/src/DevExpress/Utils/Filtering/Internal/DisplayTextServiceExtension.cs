namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    public static class DisplayTextServiceExtension
    {
        public static Converter<object, string> GetEnumDisplayTextConverter(this IDisplayTextService displayTextService) => 
            !DefaultDisplayTextServiceFactory.IsNullOrDefault(displayTextService) ? value => displayTextService.GetDisplayText(value) : null;
    }
}

