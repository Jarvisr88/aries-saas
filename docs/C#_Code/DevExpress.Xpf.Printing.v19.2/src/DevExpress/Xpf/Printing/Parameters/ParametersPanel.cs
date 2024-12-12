namespace DevExpress.Xpf.Printing.Parameters
{
    using DevExpress.Xpf.Printing.Parameters.Models;
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;

    [TemplatePart(Name="PART_parametersContainer", Type=typeof(ItemsControl)), TemplatePart(Name="PART_submitButton", Type=typeof(Button)), TemplatePart(Name="PART_resetButton", Type=typeof(Button))]
    public class ParametersPanel : Control
    {
        private const string PART_parametersContainer = "PART_parametersContainer";
        private const string PART_submitButton = "PART_submitButton";
        private const string PART_resetButton = "PART_resetButton";
        public static readonly DependencyProperty ParameterTemplateSelectorProperty = DependencyProperty.Register("ParameterTemplateSelector", typeof(DataTemplateSelector), typeof(ParametersPanel), new PropertyMetadata(new DevExpress.Xpf.Printing.Parameters.ParameterTemplateSelector()));
        public static readonly DependencyProperty ParametersModelProperty = DependencyProperty.Register("ParametersModel", typeof(DevExpress.Xpf.Printing.Parameters.Models.ParametersModel), typeof(ParametersPanel), new PropertyMetadata(null));

        static ParametersPanel()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(ParametersPanel), new FrameworkPropertyMetadata(typeof(ParametersPanel)));
        }

        public DataTemplateSelector ParameterTemplateSelector
        {
            get => 
                (DataTemplateSelector) base.GetValue(ParameterTemplateSelectorProperty);
            set => 
                base.SetValue(ParameterTemplateSelectorProperty, value);
        }

        public DevExpress.Xpf.Printing.Parameters.Models.ParametersModel ParametersModel
        {
            get => 
                DesignerProperties.GetIsInDesignMode(this) ? null : ((DevExpress.Xpf.Printing.Parameters.Models.ParametersModel) base.GetValue(ParametersModelProperty));
            set => 
                base.SetValue(ParametersModelProperty, value);
        }
    }
}

