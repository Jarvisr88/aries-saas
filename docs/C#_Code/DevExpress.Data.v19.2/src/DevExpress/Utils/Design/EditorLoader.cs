namespace DevExpress.Utils.Design
{
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;

    public class EditorLoader : UITypeEditor
    {
        private UITypeEditor baseEditor;

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            UITypeEditor baseEditor = this.GetBaseEditor(context);
            return ((baseEditor == null) ? base.EditValue(context, provider, value) : baseEditor.EditValue(context, provider, value));
        }

        private string GetAssemblyQualifiedName(string typeName, string assemblyName)
        {
            char[] separator = new char[] { ',' };
            string[] strArray = base.GetType().AssemblyQualifiedName.Split(separator);
            strArray[0] = typeName;
            strArray[1] = assemblyName;
            return string.Join(",", strArray);
        }

        protected virtual UITypeEditor GetBaseEditor(ITypeDescriptorContext context)
        {
            if (this.baseEditor == null)
            {
                EditorLoaderAttribute editorLoaderAttribute = this.GetEditorLoaderAttribute(context);
                if (editorLoaderAttribute != null)
                {
                    if (string.IsNullOrEmpty(editorLoaderAttribute.TypeName))
                    {
                        throw new ArgumentException("TypeName");
                    }
                    if (string.IsNullOrEmpty(editorLoaderAttribute.AssemblyName))
                    {
                        throw new ArgumentException("AssemblyName");
                    }
                    Type type = Type.GetType(this.GetAssemblyQualifiedName(editorLoaderAttribute.TypeName, editorLoaderAttribute.AssemblyName), false, false);
                    if (type != null)
                    {
                        this.baseEditor = Activator.CreateInstance(type) as UITypeEditor;
                    }
                }
            }
            return this.baseEditor;
        }

        protected EditorLoaderAttribute GetEditorLoaderAttribute(ITypeDescriptorContext context) => 
            (EditorLoaderAttribute) context.PropertyDescriptor.Attributes[typeof(EditorLoaderAttribute)];

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            UITypeEditor baseEditor = this.GetBaseEditor(context);
            return ((baseEditor == null) ? base.GetEditStyle(context) : baseEditor.GetEditStyle(context));
        }

        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            UITypeEditor baseEditor = this.GetBaseEditor(context);
            return ((baseEditor == null) ? base.GetPaintValueSupported(context) : baseEditor.GetPaintValueSupported(context));
        }
    }
}

