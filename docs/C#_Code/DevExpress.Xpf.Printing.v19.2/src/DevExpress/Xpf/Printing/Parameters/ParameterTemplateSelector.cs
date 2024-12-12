namespace DevExpress.Xpf.Printing.Parameters
{
    using DevExpress.Xpf.Printing.Parameters.Models;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class ParameterTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            ParameterModel model = item as ParameterModel;
            if (model == null)
            {
                return null;
            }
            if (model.IsRangeParameter && (model.Parameter.Type == typeof(DateTime)))
            {
                return this.DateRangeTemplate;
            }
            if (model.MultiValue)
            {
                return ((model.LookUpValues != null) ? this.MultiValueLookUpTemplate : this.MultiValueTemplate);
            }
            if (model.LookUpValues != null)
            {
                return this.LookUpEditTemplate;
            }
            Type type = model.Parameter.Type;
            return (!(type == typeof(bool)) ? (!(type == typeof(DateTime)) ? (!PSNativeMethods.IsNumericalType(type) ? (!(type == typeof(Guid)) ? this.StringTemplate : this.GuidTemplate) : (!PSNativeMethods.IsFloatType(type) ? this.NumericTemplate : this.NumericFloatTemplate)) : this.DateTimeTemplate) : this.BooleanTemplate);
        }

        public DataTemplate LookUpEditTemplate { get; set; }

        public DataTemplate BooleanTemplate { get; set; }

        public DataTemplate DateTimeTemplate { get; set; }

        public DataTemplate StringTemplate { get; set; }

        public DataTemplate NumericTemplate { get; set; }

        public DataTemplate NumericFloatTemplate { get; set; }

        public DataTemplate GuidTemplate { get; set; }

        public DataTemplate MultiValueTemplate { get; set; }

        public DataTemplate MultiValueLookUpTemplate { get; set; }

        public DataTemplate DateRangeTemplate { get; set; }
    }
}

