System.OverflowException: Array dimensions exceeded supported range.
   at Reflector.Application.Languages.CSharpLanguage.LanguageWriter.GetNestedGenericArgumentList(ITypeDeclaration value)
   at Reflector.Application.Languages.CSharpLanguage.LanguageWriter.WriteTypeDeclaration(ITypeDeclaration value)
   at Reflector.Application.Languages.CSharpLanguage.LanguageWriter.WriteNamespace(INamespace value)
   at Reflector.Application.ExportSource.CodeFile.WriteToOutput(ILanguageWriterConfiguration configuration, ILanguage language, ITranslator disassembler)
namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Bars.Native;
    using DevExpress.Xpf.Bars.Themes;
    using DevExpress.Xpf.Core.Internal;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    [TemplateVisualState(Name="Pressed", GroupName="ItemState"), TemplateVisualState(Name="Hot", GroupName="ItemState"), TemplatePart(Name="PART_ArrowControl", Type=typeof(Control)), TemplateVisualState(Name="Disabled", GroupName="ItemState"), TemplatePart(Name="PART_ArrowContent", Type=typeof(ContentControl)), TemplateVisualState(Name="Normal", GroupName="ItemState"), TemplatePart(Name="PART_Popup", Type=typeof(PopupMenuBase)), TemplatePart(Name="PART_ContentToArrowIndent", Type=typeof(Border)), TemplateVisualState(Name="Customization", GroupName="ItemState")]
    public class BarSubItemLinkControl
