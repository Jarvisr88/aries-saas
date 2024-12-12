namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;
    using System.Windows.Media;

    public class OperandMenuItem : ImmutableObject
    {
        internal OperandMenuItem(DevExpress.Xpf.Core.Native.ValueDataKind valueDataKind, ICommand command)
        {
            this.<ValueDataKind>k__BackingField = valueDataKind;
            this.<DisplayText>k__BackingField = GetDisplayTextFromValueDataKind(this.ValueDataKind);
            this.<Image>k__BackingField = FilterImageProvider.GetImage(valueDataKind.ToString());
            this.<Command>k__BackingField = command;
        }

        private static string GetDisplayTextFromValueDataKind(DevExpress.Xpf.Core.Native.ValueDataKind valueDataKind)
        {
            switch (valueDataKind)
            {
                case DevExpress.Xpf.Core.Native.ValueDataKind.Value:
                    return EditorLocalizer.GetString(EditorStringId.FilterOperandValue);

                case DevExpress.Xpf.Core.Native.ValueDataKind.PropertyName:
                    return EditorLocalizer.GetString(EditorStringId.FilterOperandProperty);

                case DevExpress.Xpf.Core.Native.ValueDataKind.LocalDateTimeFunction:
                    return valueDataKind.ToString();

                case DevExpress.Xpf.Core.Native.ValueDataKind.Parameter:
                    return EditorLocalizer.GetString(EditorStringId.FilterOperandParameter);
            }
            throw new ArgumentOutOfRangeException("valueDataKind", valueDataKind, null);
        }

        internal DevExpress.Xpf.Core.Native.ValueDataKind ValueDataKind { get; }

        public string DisplayText { get; }

        public ImageSource Image { get; }

        public ICommand Command { get; }
    }
}

