namespace DevExpress.XtraPrinting.XamlExport
{
    using DevExpress.XtraPrinting;
    using System;

    internal class CheckBoxBrickXamlExporter : VisualBrickXamlExporter
    {
        public const string checkedCheckBoxTemplateKey = "checkedCheckBoxTemplate";
        public const string uncheckedCheckBoxTemplateKey = "uncheckedCheckBoxTemplate";
        public const string indeterminateCheckBoxTemplateKey = "indeterminateCheckBoxTemplate";

        protected override XamlBrickExportMode GetBrickExportMode() => 
            XamlBrickExportMode.Content;

        private static string GetCheckTemplate(bool? isChecked)
        {
            if (isChecked == null)
            {
                return "indeterminateCheckBoxTemplate";
            }
            bool? nullable = isChecked;
            bool flag = true;
            return (((nullable.GetValueOrDefault() == flag) ? ((string) (nullable != null)) : ((string) false)) ? "checkedCheckBoxTemplate" : "uncheckedCheckBoxTemplate");
        }

        protected override void WriteBrickToXamlCore(XamlWriter writer, VisualBrick brick, XamlExportContext exportContext)
        {
            CheckBoxBrick brick2 = brick as CheckBoxBrick;
            if (brick2 == null)
            {
                throw new ArgumentException("brick");
            }
            writer.WriteStartElement(XamlTag.ContentPresenter);
            writer.WriteAttribute(XamlAttribute.ContentTemplate, $"{{StaticResource {GetCheckTemplate(brick2.GetCheckValue())}}}");
            writer.WriteEndElement();
        }
    }
}

