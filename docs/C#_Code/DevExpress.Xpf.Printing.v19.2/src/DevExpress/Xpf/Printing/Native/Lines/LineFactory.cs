namespace DevExpress.Xpf.Printing.Native.Lines
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.Xpf.Utils;
    using DevExpress.XtraPrinting.Native.Lines;
    using System;
    using System.ComponentModel;

    public class LineFactory : LineFactoryBase
    {
        public override ILine CreateBooleanLine(PropertyDescriptor property, object obj) => 
            new BooleanLine(property, obj);

        public override ILine CreateColorPropertyLine(IStringConverter converter, PropertyDescriptor property, object obj) => 
            new CustomEditorPropertyLine(new PopupColorEdit(), PopupColorEdit.ColorProperty.GetName(), new GdiColorToMediaColorConverter(), null, property, obj);

        public override ILine CreateComboBoxPropertyLine(IStringConverter converter, object[] values, PropertyDescriptor property, object obj) => 
            new ComboBoxPropertyLine(converter, values, property, obj);

        public override ILine CreateDateTimePropertyLine(IStringConverter converter, PropertyDescriptor property, object obj) => 
            new DateTimePropertyLine(converter, property, obj);

        public override ILine CreateEditorPropertyLine(IStringConverter converter, PropertyDescriptor property, object obj) => 
            new ButtonEditPropertyLine(converter, property, obj);

        public override ILine CreateEmptyLine() => 
            new EmptyLine();

        public override ILine CreateNumericPropertyLine(IStringConverter converter, PropertyDescriptor property, object obj) => 
            new NumericPropertyLine(converter, property, obj);

        public override ILine CreateSeparatorLine() => 
            this.CreateEmptyLine();
    }
}

