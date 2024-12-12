namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class DropPositionConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            !(value is DropPosition) ? string.Empty : GetLocalizedString(GetId((DropPosition) value));

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static EditorStringId GetId(DropPosition position)
        {
            switch (position)
            {
                case DropPosition.Before:
                    return EditorStringId.DragDropInsertBefore;

                case DropPosition.After:
                    return EditorStringId.DragDropInsertAfter;

                case DropPosition.Inside:
                    return EditorStringId.DragDropMoveToChildren;
            }
            return EditorStringId.DragDropAddRecords;
        }

        private static string GetLocalizedString(EditorStringId id) => 
            EditorLocalizer.GetString(id);

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;
    }
}

