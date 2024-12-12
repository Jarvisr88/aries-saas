namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Mvvm.UI.Native.ViewGenerator;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class GenerateColumnWrapper
    {
        public readonly int Index;
        public readonly Type ColumnFieldType;
        public readonly ColumnWrapperGeneratorBase Generator;
        public readonly System.ComponentModel.PropertyDescriptor PropertyDescriptor;

        public GenerateColumnWrapper(int propertyIndex, Type columnFieldType, ColumnWrapperGeneratorBase generator, System.ComponentModel.PropertyDescriptor propertyDescriptor)
        {
            this.Index = propertyIndex;
            this.Generator = generator;
            this.PropertyDescriptor = propertyDescriptor;
            this.ColumnFieldType = columnFieldType;
            this.Properties = new Dictionary<DependencyProperty, object>();
            this.Visible = true;
        }

        public bool SetScaffSmartProperty { get; set; }

        public string FieldName { get; set; }

        public string HeaderToolTip { get; set; }

        public bool Visible { get; set; }

        public bool? AllowEditing { get; set; }

        public bool ReadOnly { get; set; }

        public object EditorResourceKey { get; set; }

        public object Header { get; set; }

        public DevExpress.Mvvm.UI.Native.ViewGenerator.EditorsGeneratorBase.Initializer Initializer { get; set; }

        public GenerateEditSettingsWrapper EditSettingsWrapper { get; set; }

        public Dictionary<DependencyProperty, object> Properties { get; private set; }
    }
}

