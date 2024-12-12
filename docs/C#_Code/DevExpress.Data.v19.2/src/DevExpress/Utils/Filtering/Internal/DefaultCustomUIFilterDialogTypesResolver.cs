namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    internal class DefaultCustomUIFilterDialogTypesResolver : ICustomUIFilterDialogTypesResolver
    {
        internal static readonly ICustomUIFilterDialogTypesResolver Instance = new DefaultCustomUIFilterDialogTypesResolver();

        private DefaultCustomUIFilterDialogTypesResolver()
        {
        }

        CustomUIFilterDialogType ICustomUIFilterDialogTypesResolver.Resolve(CustomUIFilterType filterType) => 
            (CustomUIFilterDialogType) 0;
    }
}

