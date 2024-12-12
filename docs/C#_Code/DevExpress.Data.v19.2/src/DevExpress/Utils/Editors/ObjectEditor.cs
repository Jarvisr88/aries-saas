namespace DevExpress.Utils.Editors
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public class ObjectEditor : Form
    {
        private Button btnOK;
        private Button btnCancel;
        private ComboBox cmbType;
        private Label lblType;
        private Label lblValue;
        private TextBox txtEditor;
        private ComboBox cmbEditor;
        private DateTimePicker dtEditor;
        private Container components;
        private string objEditValue;
        public static string[] ObjectTypeNames;
        private Control oldEditor;
        private bool lockUpdate;

        static ObjectEditor();
        public ObjectEditor(object editValue);
        private void cmbType_SelectedIndexChanged(object sender, EventArgs e);
        public static object ConvertObject(ObjectEditor.ObjectType type, object val);
        public static string ConvertObjectToString(object val);
        protected override void Dispose(bool disposing);
        private void editor_TextChanged(object sender, EventArgs e);
        public static ObjectEditor.ObjectType GetObjectType(object val);
        private void InitializeComponent();

        private Control Editor { get; }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public object EditValue { get; }

        public enum ObjectType
        {
            public const ObjectEditor.ObjectType String = ObjectEditor.ObjectType.String;,
            public const ObjectEditor.ObjectType Int16 = ObjectEditor.ObjectType.Int16;,
            public const ObjectEditor.ObjectType Int32 = ObjectEditor.ObjectType.Int32;,
            public const ObjectEditor.ObjectType Int64 = ObjectEditor.ObjectType.Int64;,
            public const ObjectEditor.ObjectType Single = ObjectEditor.ObjectType.Single;,
            public const ObjectEditor.ObjectType Double = ObjectEditor.ObjectType.Double;,
            public const ObjectEditor.ObjectType Byte = ObjectEditor.ObjectType.Byte;,
            public const ObjectEditor.ObjectType Decimal = ObjectEditor.ObjectType.Decimal;,
            public const ObjectEditor.ObjectType Char = ObjectEditor.ObjectType.Char;,
            public const ObjectEditor.ObjectType Boolean = ObjectEditor.ObjectType.Boolean;,
            public const ObjectEditor.ObjectType DateTime = ObjectEditor.ObjectType.DateTime;,
            public const ObjectEditor.ObjectType Null = ObjectEditor.ObjectType.Null;
        }
    }
}

