System.OverflowException: Array dimensions exceeded supported range.
   at Reflector.Application.Languages.CSharpLanguage.LanguageWriter.GetNestedGenericArgumentList(ITypeDeclaration value)
   at Reflector.Application.Languages.CSharpLanguage.LanguageWriter.WriteTypeDeclaration(ITypeDeclaration value)
   at Reflector.Application.Languages.CSharpLanguage.LanguageWriter.WriteNamespace(INamespace value)
   at Reflector.Application.ExportSource.CodeFile.WriteToOutput(ILanguageWriterConfiguration configuration, ILanguage language, ITranslator disassembler)
namespace DevExpress.Xpf.Bars
{
    using DevExpress.Xpf.Bars.Native;
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Windows.Media;

    [ContentProperty("Caption")]
    public class GalleryItem
