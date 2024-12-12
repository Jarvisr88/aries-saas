System.OverflowException: Array dimensions exceeded supported range.
   at Reflector.Application.Languages.CSharpLanguage.LanguageWriter.GetNestedGenericArgumentList(ITypeDeclaration value)
   at Reflector.Application.Languages.CSharpLanguage.LanguageWriter.WriteTypeDeclaration(ITypeDeclaration value)
   at Reflector.Application.Languages.CSharpLanguage.LanguageWriter.WriteNamespace(INamespace value)
   at Reflector.Application.ExportSource.CodeFile.WriteToOutput(ILanguageWriterConfiguration configuration, ILanguage language, ITranslator disassembler)
namespace DevExpress.Xpf.Bars
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [ToolboxTabName("DX.19.2: Navigation & Layout"), DXToolboxBrowsable]
    public class ToolBarControl
