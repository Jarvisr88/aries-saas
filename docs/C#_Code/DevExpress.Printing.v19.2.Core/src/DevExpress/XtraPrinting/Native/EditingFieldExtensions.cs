namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Runtime.CompilerServices;

    public static class EditingFieldExtensions
    {
        public static EditingField CreateEditingField(string editingFieldType) => 
            (editingFieldType != typeof(TextEditingField).Name) ? ((editingFieldType != typeof(CheckEditingField).Name) ? ((editingFieldType != typeof(ImageEditingField).Name) ? null : ((EditingField) new ImageEditingField())) : ((EditingField) new CheckEditingField())) : ((EditingField) new TextEditingField());

        public static EditingField GetNextField(this EditingFieldCollection fields, EditingField field)
        {
            if (field == null)
            {
                return fields[0];
            }
            int num = Math.Max(-1, fields.IndexOf(field)) + 1;
            return ((num < fields.Count) ? fields[num] : ((fields.Count > 0) ? fields[0] : null));
        }

        public static EditingField GetPreviousField(this EditingFieldCollection fields, EditingField field)
        {
            if (field == null)
            {
                return fields[0];
            }
            int num = Math.Max(0, fields.IndexOf(field)) - 1;
            return (((num >= fields.Count) || (num < 0)) ? ((fields.Count > 0) ? fields[fields.Count - 1] : null) : fields[num]);
        }
    }
}

